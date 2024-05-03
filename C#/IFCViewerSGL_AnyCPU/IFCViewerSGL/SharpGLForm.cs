using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using System.Runtime.InteropServices;
using System.Diagnostics;
using SharpGL.Enumerations;

namespace IFCViewerSGL
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form, IIFCController
    {
        #region Constants

        /// <summary>
        /// Selection buffer
        /// </summary>
        private const int SELECTION_BUFFER_SIZE = 512;

        #endregion // Constants

        #region Fields

        /// <summary>
        /// Model
        /// </summary>
        IFCModel _ifcModel;
        
        /// <summary>
        /// Rotation - X
        /// </summary>
        float _fXAngle;

        /// <summary>
        /// Rotation - Y
        /// </summary> 
        float _fYAngle;

        /// <summary>
        /// Translation - X
        /// </summary>
        float _fXTranslation;

        /// <summary>
        /// Translation - Y
        /// </summary>
        float _fYTranslation;

        /// <summary>
        /// Translation - Z
        /// </summary>
        float _fZTranslation;

        /// <summary>
        /// Mouse position
        /// </summary>
        Point _ptPrevMousePosition;

        /// <summary>
        /// Interaction in progress
        /// </summary>
        bool _bInteractionInProgress;

        /// <summary>
        /// Selection
        /// </summary>
        uint _iSelectionFrameBuffer;

        /// <summary>
        /// Selection
        /// </summary>
        uint _iSelectionTextureBuffer;

        /// <summary>
        /// Selection
        /// </summary>
        uint _iSelectionDepthRenderBuffer;

        /// <summary>
        /// Selection
        /// </summary>
        IFCItem _pointedItem;

        /// <summary>
        /// Selection
        /// </summary>
        IFCItem _selectedItem;

        /// <summary>
        /// IFC Tree
        /// </summary>
        IFCTreeForm _ifcTreeForm;

        /// <summary>
        /// IFCView-s
        /// </summary>
        HashSet<IIFCView> _hsIFCViews;

        #endregion // Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="SharpGLForm"/> class.
        /// </summary>
        public SharpGLForm()
        {
            InitializeComponent();

            /*
             * Model
             */
            _ifcModel = new IFCModel();

            /*
             * ModelLoaded event handler
             */
            _ifcModel.ModelLoaded += (s, e) =>
            {
                foreach (var ifcView in _hsIFCViews)
                {
                    ifcView.OnModelLoaded();
                }
            };

            /*
             * Rotate, Move, Zoom
             */
            _fXAngle = 30.0f;
            _fYAngle = 30.0f;
            _fXTranslation = 0.0f;
            _fYTranslation = 0.0f;
            _fZTranslation = -5.0f;
            _ptPrevMousePosition = new Point(-1, -1);
            _bInteractionInProgress = false;

            /*
             * Selection
             */
            _iSelectionFrameBuffer = 0;
            _iSelectionTextureBuffer = 0;
            _iSelectionDepthRenderBuffer = 0;

            /*
             * IIFCController
             */
            _hsIFCViews = new HashSet<IIFCView>();

            /*
             * IFC Tree
             */
            _ifcTreeForm = new IFCTreeForm();
            _ifcTreeForm.SetController(this);
            _ifcTreeForm.Show(this);
        }

        /// <summary>
        /// Clean up
        /// </summary>
        private void Clean()
        {
            _pointedItem = null;
            _selectedItem = null;

            OpenGL gl = openGLControl.OpenGL;

            if (_iSelectionFrameBuffer != 0)
            {
                gl.DeleteFramebuffersEXT(1, new uint[] { _iSelectionFrameBuffer });
                _iSelectionFrameBuffer = 0;
            }

            if (_iSelectionTextureBuffer != 0)
            {
                gl.DeleteTextures(1, new uint[] { _iSelectionTextureBuffer });
                _iSelectionTextureBuffer = 0;
            }

            if (_iSelectionDepthRenderBuffer != 0)
            {
                gl.DeleteRenderbuffersEXT(1, new uint[] { _iSelectionDepthRenderBuffer });
                _iSelectionDepthRenderBuffer = 0;
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Clean();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);            
        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);

            // Set the clear color.
            gl.ClearColor(1f, 1f, 1f, 1f);

            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);

            gl.Enable(OpenGL.GL_COLOR_MATERIAL);

            gl.ShadeModel(OpenGL.GL_SMOOTH);

            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);

            float[] ambientLight = { 0.75f, 0.75f, 0.75f, 1.0f };
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, ambientLight);

            float[] diffuseLight = { 0.1f, 0.1f, 0.1f, 1.0f };
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, diffuseLight);

            float[] specularLight = { 0.1f, 0.1f, 0.1f, 0.5f };
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, specularLight);

            float[] lightPosition = { -2.0f, -2.0f, -2.0f, 0.0f };
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightPosition);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            // fovY     - Field of vision in degrees in the y direction
            // aspect   - Aspect ratio of the viewport
            // zNear    - The near clipping distance
            // zFar     - The far clipping distance
            double fovY = 45.0;
            double aspect = (double)openGLControl.Width / (double)openGLControl.Height;
            double zNear = 0.01;
            double zFar = 100000.0;

            double fH = Math.Tan(fovY / 360 * Math.PI) * zNear;
            double fW = fH * aspect;

            gl.Frustum(-fW, fW, -fH, fH, zNear, zFar);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();

            gl.Translate(_fXTranslation, _fYTranslation, _fZTranslation);
            gl.Rotate(_fXAngle, 1.0f, 0.0f, 0.0f);
            gl.Rotate(_fYAngle, 0.0f, 1.0f, 0.0f);

            /**************************************************************************************
             * Faces
             */
  //          if (_selectedItem == null)
  //          {
                DrawFaces(false);
                DrawFaces(true);
   //         }            

            /**************************************************************************************
             * Selection
             */
//            DrawPointedFaces();
//            DrawSelectedFaces();

            /**************************************************************************************
             * Faces polygons
             */
            DrawFacesPolygons();            

            /**************************************************************************************
             * Lines
             */
//            DrawLines();

            /**************************************************************************************
             * Points
             */
//            DrawPoints();

            /**************************************************************************************
             * Selection
             */
            DrawFacesFrameBuffer();
        }

        private void InitDrawFaces()
        {
            OpenGL gl = openGLControl.OpenGL;

            STRUCT_MATERIAL myMaterial = _ifcModel.MaterailsBuilder._firstMaterial;
            while (myMaterial != null)
            {
                STRUCT_MATERIALS lastMaterials = null;
                myMaterial.firstIfcItem = null;
                foreach (var ifcItem in _ifcModel.Geometry)
                {
                    var myMaterials = ifcItem._materials;
                    while (myMaterials != null)
                    {
                        if (myMaterials.material == myMaterial)
                        {
                            if (myMaterial.firstIfcItem == null)
                            {
                                myMaterial.firstIfcItem = ifcItem;
                            }
                            else
                            {
                                lastMaterials.nextSameMaterialIfcItem = ifcItem;
                            }

                            myMaterials.nextSameMaterialIfcItem = null;
                            lastMaterials = myMaterials;
                        }
                        myMaterials = myMaterials.next;
                    }
                }

                myMaterial = myMaterial.next;
            }

            myMaterial = _ifcModel.MaterailsBuilder._firstMaterial;
            while (myMaterial != null)
            {
                //
                //  Calculate arrays for this material
                //
                long primitivesForFaces = 0;

                var ifcItem = myMaterial.firstIfcItem;
                while (ifcItem != null) {
                    var myMaterials = ifcItem._materials;
                    ifcItem = null;
                    while (myMaterials != null) {
                        if (myMaterials.material == myMaterial) {
                            primitivesForFaces += myMaterials.__noPrimitivesForFaces;
                            ifcItem = myMaterials.nextSameMaterialIfcItem;
                        }
                        myMaterials =myMaterials.next;
                    }
                }

                myMaterial.myArrayForFaces = new float[6 * 3 * primitivesForFaces];

                long offset = 0;

                ifcItem = myMaterial.firstIfcItem;
                while (ifcItem != null) {
                    var myMaterials = ifcItem._materials;
                    IFCViewerSGL.IFCItem nextIfcItem = null;
                    while (myMaterials != null) {
                        if (myMaterials.material == myMaterial) {
                            for (long iIndex = 0; iIndex < myMaterials.__noPrimitivesForFaces * 3; iIndex++)
                            {
                               myMaterial.myArrayForFaces[3 * (iIndex + offset) + 0] = 
                                    ifcItem._vertices[(ifcItem._facesIndices[myMaterials.__indexArrayOffset + iIndex] * 6) + 0];
                               myMaterial.myArrayForFaces[3 * (iIndex + offset) + 1] =
                                    ifcItem._vertices[(ifcItem._facesIndices[myMaterials.__indexArrayOffset + iIndex] * 6) + 1];
                               myMaterial.myArrayForFaces[3 * (iIndex + offset) + 2] =
                                    ifcItem._vertices[(ifcItem._facesIndices[myMaterials.__indexArrayOffset + iIndex] * 6) + 2];
                            }

                            offset += myMaterials.__noPrimitivesForFaces * 3;
                            nextIfcItem = myMaterials.nextSameMaterialIfcItem;
                        }

                        myMaterials = myMaterials.next;
                    }
                    ifcItem = nextIfcItem;
                }

                myMaterial.ids = new uint[1];
                gl.GenBuffers(1, myMaterial.ids);

                myMaterial = myMaterial.next;
            }
        }

        /// <summary>
        /// Faces
        /// </summary>
        private void DrawFaces(bool bTransparent)
        {
            if (_ifcModel.MaterailsBuilder == null) {
                return;
            }

            if (_ifcModel.facesInitialized == false)
            {
                InitDrawFaces();
                _ifcModel.facesInitialized = true;
            }

            OpenGL gl = openGLControl.OpenGL;

            if (bTransparent)
            {
                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            }
            else
            {
                gl.Enable(OpenGL.GL_CULL_FACE);
                gl.CullFace(OpenGL.GL_FRONT);
            }

            STRUCT_MATERIAL myMaterial = _ifcModel.MaterailsBuilder._firstMaterial;
            while (myMaterial != null)
            {
                bool skip = false;
                if (bTransparent)
                {
                    if (myMaterial.ambient.A == 1.0)
                    {
                        skip = true;
                    }
                }
                else
                {
                    if (myMaterial.ambient.A < 1.0)
                    {
                        skip = true;
                    }
                }

                if (skip == false)
                {
                    /*
                    * Material - Ambient color
                    */
                    gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT);
                    gl.Color(
                        myMaterial.ambient.R,
                        myMaterial.ambient.G,
                        myMaterial.ambient.B,
                        myMaterial.ambient.A);

                    /*
                    * Material - Diffuse color
                    */
                    gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_DIFFUSE);
                    gl.Color(
                        myMaterial.diffuse.R,
                        myMaterial.diffuse.G,
                        myMaterial.diffuse.B,
                        myMaterial.diffuse.A);

                    /*
                    * Material - Specular color
                    */
                    gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SPECULAR);
                    gl.Color(
                        myMaterial.specular.R,
                        myMaterial.specular.G,
                        myMaterial.specular.B,
                        myMaterial.specular.A);

                    /*
                    * Material - Emissive color
                    */
                    gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_EMISSION);
                    gl.Color(
                        myMaterial.emissive.R,
                        myMaterial.emissive.G,
                        myMaterial.emissive.B,
                        myMaterial.emissive.A);


                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, myMaterial.ids[0]);
                    gl.BufferData(OpenGL.GL_ARRAY_BUFFER, myMaterial.myArrayForFaces, OpenGL.GL_STATIC_DRAW);
                    gl.VertexAttribPointer(0, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
                    gl.EnableVertexAttribArray(0);

                    int   size = 0, start = 0;

                    var ifcItem = myMaterial.firstIfcItem;
                    while (ifcItem != null) {
                        var myMaterials = ifcItem._materials;
                        IFCViewerSGL.IFCItem nextIfcItem = null;
                        while (myMaterials != null) {
                            if (myMaterials.material == myMaterial) {
                                if (ifcItem._visible == false) {
                                    if (size > 0) {
                                        gl.DrawArrays(OpenGL.GL_TRIANGLES, start, size);
                                    }

                                    start += size + (int) myMaterials.__noPrimitivesForFaces * 3;
                                    size = 0;
                                } else {
                                    size += (int) myMaterials.__noPrimitivesForFaces * 3;
                                }

                                nextIfcItem = myMaterials.nextSameMaterialIfcItem;
                            }
                            myMaterials =myMaterials.next;
                        }

                        ifcItem = nextIfcItem;
                    }

                    if (size > 0)
                    {
                        gl.DrawArrays(OpenGL.GL_TRIANGLES, start, size);
                    }
                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
                }

                myMaterial = myMaterial.next;
            }

            if (bTransparent)
            {
                gl.Disable(OpenGL.GL_BLEND);
            }
            else
            {
                gl.Disable(OpenGL.GL_CULL_FACE);
            }
        }

        /// <summary>
        /// Selection support
        /// </summary>
        private void DrawFacesFrameBuffer()
        {
            OpenGL gl = openGLControl.OpenGL;

            if (_iSelectionFrameBuffer == 0)
            {
                /*
		        * Frame buffer
		        */
                uint[] arFrameBuffers = new uint[1];
                gl.GenFramebuffersEXT(1, arFrameBuffers);

                _iSelectionFrameBuffer = arFrameBuffers[0];
                System.Diagnostics.Debug.Assert(_iSelectionFrameBuffer != 0);

		        gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _iSelectionFrameBuffer);

		        /*
		        * Texture buffer
		        */
 /*               uint[] arTextures = new uint[1];
		        gl.GenTextures(1, arTextures);

                _iSelectionTextureBuffer = arTextures[0];
                System.Diagnostics.Debug.Assert(_iSelectionTextureBuffer != 0);

                gl.BindTexture(OpenGL.GL_TEXTURE_2D, _iSelectionTextureBuffer);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_CLAMP_TO_EDGE);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_CLAMP_TO_EDGE);

//                gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGBA, SELECTION_BUFFER_SIZE, SELECTION_BUFFER_SIZE, 0, OpenGL.GL_RGBA, OpenGL.GL_UNSIGNED_BYTE, IntPtr.Zero);

 //               gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

                gl.FramebufferTexture2DEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_COLOR_ATTACHMENT0_EXT, OpenGL.GL_TEXTURE_2D, _iSelectionTextureBuffer, 0);

		        /*
		        * Depth buffer
		        */
                uint[] arRenderBuffers = new uint[1];
                gl.GenRenderbuffersEXT(1, arRenderBuffers);

                _iSelectionDepthRenderBuffer = arRenderBuffers[0];
                System.Diagnostics.Debug.Assert(_iSelectionDepthRenderBuffer != 0);

                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER_EXT, _iSelectionDepthRenderBuffer);
                gl.RenderbufferStorageEXT(OpenGL.GL_RENDERBUFFER_EXT, OpenGL.GL_DEPTH_COMPONENT, SELECTION_BUFFER_SIZE, SELECTION_BUFFER_SIZE);

                gl.BindRenderbufferEXT(OpenGL.GL_RENDERBUFFER_EXT, 0);

                gl.FramebufferRenderbufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, OpenGL.GL_DEPTH_ATTACHMENT_EXT, OpenGL.GL_RENDERBUFFER_EXT, _iSelectionDepthRenderBuffer);

                uint[] arDrawBuffers = new uint[] { OpenGL.GL_COLOR_ATTACHMENT0_EXT };
		        gl.DrawBuffers(1, arDrawBuffers);

		        gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, 0);
            } // if (_iSelectionFrameBuffer == 0)

            /*
	        * Draw
	        */

	        gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _iSelectionFrameBuffer);

	        gl.Viewport(0, 0, SELECTION_BUFFER_SIZE, SELECTION_BUFFER_SIZE);

            // Set the clear color.
            gl.ClearColor(0f, 0f, 0f, 0f);

            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);

            gl.Enable(OpenGL.GL_COLOR_MATERIAL);

            gl.ShadeModel(OpenGL.GL_FLAT);

            gl.Disable(OpenGL.GL_LIGHTING);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            // fovY     - Field of vision in degrees in the y direction
            // aspect   - Aspect ratio of the viewport
            // zNear    - The near clipping distance
            // zFar     - The far clipping distance
            double fovY = 45.0;
            double aspect = (double)openGLControl.Width / (double)openGLControl.Height;
            double zNear = 0.01;
            double zFar = 100000.0;

            double fH = Math.Tan(fovY / 360 * Math.PI) * zNear;
            double fW = fH * aspect;

            gl.Frustum(-fW, fW, -fH, fH, zNear, zFar);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();

            gl.Translate(_fXTranslation, _fYTranslation, _fZTranslation);
            gl.Rotate(_fXAngle, 1.0f, 0.0f, 0.0f);
            gl.Rotate(_fYAngle, 0.0f, 1.0f, 0.0f);

            foreach (var ifcItem in _ifcModel.Geometry)
            {
                /*
			    * Material - Ambient color
			    */
                gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT);
                gl.Color(
                    ifcItem._selectionColor.R,
                    ifcItem._selectionColor.G,
                    ifcItem._selectionColor.B,
                    1.0);

                gl.Begin(OpenGL.GL_TRIANGLES);

                for (long iIndex = 0; iIndex < ifcItem._facesIndices.Length; iIndex++)
                {
                    gl.Vertex(
                            ifcItem._vertices[(ifcItem._facesIndices[iIndex] * 6) + 0],
                            ifcItem._vertices[(ifcItem._facesIndices[iIndex] * 6) + 1],
                            ifcItem._vertices[(ifcItem._facesIndices[iIndex] * 6) + 2]);
                }

                 gl.End();
            } // foreach (var ifcItem in _model.Geometry)

            gl.Enable(OpenGL.GL_LIGHTING);

            gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, 0);            
        }

        /// <summary>
        /// Selection support
        /// </summary>
        private void DrawPointedFaces()
        {
            if ((_pointedItem == null) || (_pointedItem == _selectedItem))
            {
                return;
            }

            OpenGL gl = openGLControl.OpenGL;

            /*
            * Material - Ambient color
            */
            gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT);
            gl.Color(
                0f,
                0f,
                1f,
                1.0);

            gl.Begin(OpenGL.GL_TRIANGLES);

            for (long iIndex = 0; iIndex < _pointedItem._facesIndices.Length; iIndex++)
            {
                gl.Normal(
                    _pointedItem._vertices[(_pointedItem._facesIndices[iIndex] * 6) + 3],
                    _pointedItem._vertices[(_pointedItem._facesIndices[iIndex] * 6) + 4],
                    _pointedItem._vertices[(_pointedItem._facesIndices[iIndex] * 6) + 5]);

                gl.Vertex(
                    _pointedItem._vertices[(_pointedItem._facesIndices[iIndex] * 6) + 0],
                    _pointedItem._vertices[(_pointedItem._facesIndices[iIndex] * 6) + 1],
                    _pointedItem._vertices[(_pointedItem._facesIndices[iIndex] * 6) + 2]);
            }

            gl.End();
        }

        /// <summary>
        /// Selection support
        /// </summary>
        private void DrawSelectedFaces()
        {
            if (_selectedItem == null)
            {
                return;
            }

            OpenGL gl = openGLControl.OpenGL;

            /*
             * Draw the selected object
             */
            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.CullFace(OpenGL.GL_FRONT);

            /*
            * Material - Ambient color
            */
            gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT);
            gl.Color(
                1.0f,
                0.0f,
                0.0f,
                1.0f);

            gl.Begin(OpenGL.GL_TRIANGLES);

            for (long iIndex = 0; iIndex < _selectedItem._facesIndices.Length; iIndex++)
            {
                gl.Normal(
                    _selectedItem._vertices[(_selectedItem._facesIndices[iIndex] * 6) + 3],
                    _selectedItem._vertices[(_selectedItem._facesIndices[iIndex] * 6) + 4],
                    _selectedItem._vertices[(_selectedItem._facesIndices[iIndex] * 6) + 5]);

                gl.Vertex(
                    _selectedItem._vertices[(_selectedItem._facesIndices[iIndex] * 6) + 0],
                    _selectedItem._vertices[(_selectedItem._facesIndices[iIndex] * 6) + 1],
                    _selectedItem._vertices[(_selectedItem._facesIndices[iIndex] * 6) + 2]);
            }

            gl.End();

            gl.Disable(OpenGL.GL_CULL_FACE);

            /*
             * Draw all objects transparent
             */
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

            /*
            * Material - Ambient color
            */
            gl.ColorMaterial(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT);
            gl.Color(
                0.25f,
                0.25f,
                0.25f,
                0.1f);

            foreach (var ifcItem in _ifcModel.Geometry)
            {
                if (!ifcItem._visible || (ifcItem == _selectedItem))
                {
                    continue;
                }

                gl.Begin(OpenGL.GL_TRIANGLES);

                for (long iIndex = 0; iIndex < ifcItem._facesIndices.Length; iIndex++)
                {
                    gl.Normal(
                        ifcItem._vertices[(ifcItem._facesIndices[iIndex] * 6) + 3],
                        ifcItem._vertices[(ifcItem._facesIndices[iIndex] * 6) + 4],
                        ifcItem._vertices[(ifcItem._facesIndices[iIndex] * 6) + 5]);

                    gl.Vertex(
                        ifcItem._vertices[(ifcItem._facesIndices[iIndex] * 6) + 0],
                        ifcItem._vertices[(ifcItem._facesIndices[iIndex] * 6) + 1],
                        ifcItem._vertices[(ifcItem._facesIndices[iIndex] * 6) + 2]);
                }

                gl.End();
            } // foreach (var ifcItem ...

            gl.Disable(OpenGL.GL_BLEND);
        }

        /// <summary>
        /// Faces polygons
        /// </summary>
        /// 
        private void InitDrawFacesPolygons()
        {
            OpenGL gl = openGLControl.OpenGL;

            long size = 0;
            foreach (var ifcItem in _ifcModel.Geometry)
            {
                size += ifcItem._facesPolygonsIndices.Length;
            }

            _ifcModel.myFaceLinesArray = new float[3 * size];

            _ifcModel.myFaceLinesIds = new uint[1];
            gl.GenBuffers(1, _ifcModel.myFaceLinesIds);

            long offsetFaceLines = 0;
            foreach (var ifcItem in _ifcModel.Geometry)
            {
                int u = 0;
                while (u < ifcItem._facesPolygonsIndices.Length)
                {
                    _ifcModel.myFaceLinesArray[3 * (offsetFaceLines + u) + 0] = ifcItem._vertices[6 * ifcItem._facesPolygonsIndices[u] + 0];
                    _ifcModel.myFaceLinesArray[3 * (offsetFaceLines + u) + 1] = ifcItem._vertices[6 * ifcItem._facesPolygonsIndices[u] + 1];
                    _ifcModel.myFaceLinesArray[3 * (offsetFaceLines + u) + 2] = ifcItem._vertices[6 * ifcItem._facesPolygonsIndices[u] + 2];
                    u++;
                }

                offsetFaceLines += ifcItem._facesPolygonsIndices.Length;

                ifcItem.offsetFaceLines = offsetFaceLines;
            }
        }

        private void DrawFacesPolygons()
        {
            if (_ifcModel.myFaceLinesArray == null)
            {
                InitDrawFacesPolygons();
            }

            OpenGL gl = openGLControl.OpenGL;            

            gl.Disable(OpenGL.GL_LIGHTING);

            gl.LineWidth(1.0f);
            gl.Color(0.0f, 0.0f, 0.0f, 1.0f);

            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, _ifcModel.myFaceLinesIds[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, _ifcModel.myFaceLinesArray, OpenGL.GL_STATIC_DRAW);

            gl.VertexAttribPointer(0, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(0);

            int start = 0, size = 0;
            foreach (var ifcItem in _ifcModel.Geometry)
            {
                if (!ifcItem._visible)
                {
                    if (size > 0)
                    {
                        gl.DrawArrays(OpenGL.GL_LINES, start, size);
                    }

                    start += size + ifcItem._facesPolygonsIndices.Length;
                    size = 0;
                    continue;
                }

                size += ifcItem._facesPolygonsIndices.Length;
            } // foreach (var ifcItem in ...

            if (size > 0)
            {
                gl.DrawArrays(OpenGL.GL_LINES, start, size);
            }

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);

            gl.Enable(OpenGL.GL_LIGHTING);
        }

        /// <summary>
        /// Lines
        /// </summary>
        private void DrawLines()
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.Disable(OpenGL.GL_LIGHTING);

            gl.LineWidth(1.0f);
            gl.Color(0.0f, 0.0f, 0.0f, 1.0f);

            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            foreach (var ifcItem in _ifcModel.Geometry)
            {
                if (!ifcItem._visible)
                {
                    continue;
                }

                gl.VertexPointer(3, 6 * Marshal.SizeOf(typeof(float)), ifcItem._vertices);
                
                if (ifcItem._linesIndices.Length > 0)
                {
                    gl.DrawElements(OpenGL.GL_LINES, ifcItem._linesIndices.Length, ifcItem._linesIndices);
                }

                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            } // foreach (var ifcItem in ...

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);

            gl.Enable(OpenGL.GL_LIGHTING);
        }

        /// <summary>
        /// Points
        /// </summary>
        private void DrawPoints()
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.Disable(OpenGL.GL_LIGHTING);

            gl.LineWidth(1.0f);
            gl.Color(0.0f, 0.0f, 0.0f, 1.0f);

            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            foreach (var ifcItem in _ifcModel.Geometry)
            {
                if (!ifcItem._visible)
                {
                    continue;
                }

                gl.VertexPointer(3, 6 * Marshal.SizeOf(typeof(float)), ifcItem._vertices);
                
                if (ifcItem._pointsIndices.Length > 0)
                {
                    gl.DrawElements(OpenGL.GL_POINT, ifcItem._pointsIndices.Length, ifcItem._pointsIndices);
                }

                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            } // foreach (var ifcItem in ...

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);

            gl.Enable(OpenGL.GL_LIGHTING);
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            gl.Hint(HintTarget.PerspectiveCorrection, HintMode.Nicest);
            gl.Hint(HintTarget.LineSmooth, HintMode.Nicest);
            gl.Hint(HintTarget.PointSmooth, HintMode.Nicest);
            gl.Hint(HintTarget.PolygonSmooth, HintMode.Nicest);

            gl.Enable(OpenGL.GL_SMOOTH);
            gl.Enable(OpenGL.GL_LINE_SMOOTH);
            gl.Enable(OpenGL.GL_POINT_SMOOTH);
            gl.Enable(OpenGL.GL_MULTISAMPLE);

            gl.MinSampleShading(4.0f);
        }

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            //OpenGL gl = openGLControl.OpenGL;

            //  Set the projection matrix.
            //gl.MatrixMode(OpenGL.GL_PROJECTION);

            ////  Load the identity.
            //gl.LoadIdentity();

            ////  Create a perspective transformation.
            //gl.Perspective(45.0f, (double)Width / (double)Height, 0.0001, 100000.0);

            ////  Use the 'look at' helper function to position and aim the camera.
            //gl.LookAt(-5, -5, -15, 0, 0, 0, 0, 1, 0);

            ////  Set the modelview matrix.
            //gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IFC files (*.ifc)|*.ifc|All files (*.*)|*.*";
            dialog.Title = "Open";

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (_ifcModel.Load(dialog.FileName))
            {
                Clean();

                this.Text = string.Format("{0} - IFCViewer", System.IO.Path.GetFileNameWithoutExtension(dialog.FileName));
            }
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            _ptPrevMousePosition = new Point(-1, -1);

            /*
	        * Selection
	        */
            if (!_bInteractionInProgress)
            {
                SelectItem(this, GetSelectedItem(e.Location));
            }

            _bInteractionInProgress = false;
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL; 

            _ptPrevMousePosition = e.Location;
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL; 

            /*
	        * Selection
	        */
            if (!_bInteractionInProgress)
            {
                _pointedItem = GetSelectedItem(e.Location);
            }
            else
            {
                _pointedItem = null;
            }

            if (_ptPrevMousePosition.X == -1)
	        {
		        return;
	        }

            /*
	        * Rotate
	        */
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                _bInteractionInProgress = true;

                float fXAngle = ((float)e.Location.Y - (float)_ptPrevMousePosition.Y);
                float fYAngle = ((float)e.Location.X - (float)_ptPrevMousePosition.X);

                _fXAngle += fXAngle;
                if (_fXAngle > 360.0)
                {
                    _fXAngle = _fXAngle - 360.0f;
                }
                else
                {
                    if (_fXAngle < 0.0)
                    {
                        _fXAngle = _fXAngle + 360.0f;
                    }
                }

                _fYAngle += fYAngle;
                if (_fYAngle > 360.0)
                {
                    _fYAngle = _fYAngle - 360.0f;
                }
                else
                {
                    if (_fYAngle < 0.0)
                    {
                        _fYAngle = _fYAngle + 360.0f;
                    }
                }

                _ptPrevMousePosition = e.Location;

                return;
            } // if ((e.Button & MouseButtons.Left) == MouseButtons.Left)

            /*
	        * Zoom
	        */
            if ((e.Button & MouseButtons.Middle) == MouseButtons.Middle)
            {
                _bInteractionInProgress = true;

                _fZTranslation += e.Location.Y - _ptPrevMousePosition.Y > 0 ? -0.1f : 0.1f;

                _ptPrevMousePosition = e.Location;

                return;
            } // if ((e.Button & MouseButtons.Middle) == MouseButtons.Middle)

            /*
	        * Move
	        */
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                _bInteractionInProgress = true;

                _fXTranslation += (((float)e.Location.X - (float)_ptPrevMousePosition.X) / openGLControl.Width) * 10f;
                _fYTranslation -= (((float)e.Location.Y - (float)_ptPrevMousePosition.Y) / openGLControl.Height) * 10f;

                _ptPrevMousePosition = e.Location;

                return;
            } // if ((e.Button & MouseButtons.Middle) == MouseButtons.Middle)
        }

        /// <summary>
        /// Selection support
        /// </summary>
        /// <returns></returns>
        private IFCItem GetSelectedItem(Point point)
        {
            OpenGL gl = openGLControl.OpenGL; 

            IFCItem selectedItem = null;

            if (_iSelectionFrameBuffer != 0)
            {
                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, _iSelectionFrameBuffer);

                byte[] arPixels = new byte[4];

                double dX = (double)point.X * ((double)SELECTION_BUFFER_SIZE / (double)openGLControl.Width);
                double dY = ((double)openGLControl.Height - (double)point.Y) * ((double)SELECTION_BUFFER_SIZE / (double)openGLControl.Height);

                gl.ReadPixels(
                    (int)dX,
                    (int)dY,
                    1, 1,
                    OpenGL.GL_RGBA,
                    OpenGL.GL_UNSIGNED_BYTE,
                    arPixels);

                if (arPixels[3] != 0)
                {
                    long iObjectID =
                        (arPixels[0/*R*/] * (255 * 255)) +
                        (arPixels[1/*G*/] * 255) +
                        arPixels[2/*B*/];

                    
                    foreach (var ifcItem in _ifcModel.Geometry)
                    {
                        if (ifcItem._ID == iObjectID)
                        {
                            selectedItem = ifcItem;

                            break;
                        }
                    }
                } // if (arPixels[3] != 0)

                gl.BindFramebufferEXT(OpenGL.GL_FRAMEBUFFER_EXT, 0);
            } // if (_iSelectionFrameBuffer != 0)

            return selectedItem;
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iFCTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ifcTreeForm.Visible)
            {
                _ifcTreeForm.Hide();
            }
            else
            {
                _ifcTreeForm = new IFCTreeForm();
                _ifcTreeForm.SetController(this);
                _ifcTreeForm.Show(this);
            }            
        }

        /// <summary>
        /// Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iFCTreeToolStripMenuItem.Checked = _ifcTreeForm.Visible;
        }

        #region IIFCController

        /// <summary>
        /// IIFCController
        /// </summary>
        public IFCModel Model
        {
            get
            {
                return _ifcModel;
            }
        }

        /// <summary>
        /// IIFCController
        /// </summary>
        /// <param name="ifcView"></param>
        public void RegisterView(IIFCView ifcView)
        {
            System.Diagnostics.Debug.Assert(ifcView != null);
            System.Diagnostics.Debug.Assert(!_hsIFCViews.Contains(ifcView));

            _hsIFCViews.Add(ifcView);
        }

        /// <summary>
        /// IIFCController
        /// </summary>
        /// <param name="ifcView"></param>
        public void UnRegisterView(IIFCView ifcView)
        {
            System.Diagnostics.Debug.Assert(ifcView != null);
            System.Diagnostics.Debug.Assert(_hsIFCViews.Contains(ifcView));

            _hsIFCViews.Remove(ifcView);
        }

        /// <summary>
        /// IIFCController
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ifcItem"></param>
        public void SelectItem(object sender, IFCItem ifcItem)
        {
            _selectedItem = (ifcItem != null) && ifcItem.hasGeometry ? ifcItem : null;         

            foreach (var ifcView in _hsIFCViews)
            {
                ifcView.OnItemSelected(sender, ifcItem);
            }
        }

        #endregion // IIFCController
    }
}
