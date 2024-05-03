using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using IfcEngine;

namespace IFCViewerSGL
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }

    /// <summary>
    /// IFC Model
    /// </summary>
    public class IFCModel
    {
        #region Members

        /// <summary>
        /// File
        /// </summary>
        private string _strIFCFile = string.Empty;

        /// <summary>
        /// IFC Model
        /// </summary>
        private long _model = 0;

        /// <summary>
        /// List of the IFCItems with a geometry
        /// </summary>
        private List<IFCItem> _lsGeometry = new List<IFCItem>();

        /// <summary>
        /// ID : IFCItems
        /// </summary>
        private Dictionary<long, IFCItem> _dicItems = new Dictionary<long, IFCItem>();

        /// <summary>
        /// Materials
        /// </summary>
        private IFCMaterialsBuilder _materailsBuilder = null;

        /// <summary>
        /// Min
        /// </summary>
        private Vector3 _vecMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        /// <summary>
        /// Max
        /// </summary>
        private Vector3 _vecMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        /// <summary>
        /// Groups of faces
        /// </summary>
        //private Dictionary<Material, List<KeyValuePair<IFCItem, STRUCT_MATERIALS>>> _dicFacesGroups = new Dictionary<Material, List<KeyValuePair<IFCItem, STRUCT_MATERIALS>>>();

        #endregion // Members

        #region Events

        /// <summary>
        /// An IFC model has been loaded
        /// </summary>
        public event EventHandler ModelLoaded;

        long ifcSpace_TYPE;
        long ifcOpeningElement_TYPE;
        long ifcDistributionElement_TYPE;
        long ifcElectricalElement_TYPE;
        long ifcElementAssembly_TYPE;
        long ifcElementComponent_TYPE;
        long ifcEquipmentElement_TYPE;
        long ifcFeatureElement_TYPE;
        long ifcFeatureElementSubtraction_TYPE;
        long ifcFurnishingElement_TYPE;
        long ifcReinforcingElement_TYPE;
        long ifcTransportElement_TYPE;
        long ifcVirtualElement_TYPE;

        #endregion // Events

        /// <summary>
        /// ctor
        /// </summary>
        public IFCModel()
        {
            string strRootPath = Assembly.GetExecutingAssembly().Location.ToUpper().Replace("IFCVIEWERSGL.DLL", "");

            IfcEngineAnyCPU.Init(strRootPath);

            /////////////////////////////////////////////////////////////////////////////
            // BEGIN TEST 
            CreateModelAnyCPU createModelAnyCPU = new CreateModelAnyCPU();
            createModelAnyCPU.Run();
            // END TEST 
            /////////////////////////////////////////////////////////////////////////////
        }        

        /// <summary>
        /// Loads an IFC file
        /// </summary>
        /// <param name="strIfcFilePath"></param>
        /// <returns></returns>
        public bool Load(string strIfcFilePath)
        {
            _strIFCFile = Path.GetFileName(strIfcFilePath);

            if (_model != 0)
            {
                IfcEngineAnyCPU.sdaiCloseModel(_model);
                _model = 0;
            }

            _dicItems = new Dictionary<long, IFCItem>();
            _lsGeometry = new List<IFCItem>();
            _materailsBuilder = null;

            return ParseIFCFile(strIfcFilePath);
        }

        /// <summary>
        /// Getter
        /// </summary>
        public string IFCFile
        {
            get
            {
                return _strIFCFile;
            }
        }

        /// <summary>
        /// Getter
        /// </summary>
        public long Model
        {
            get
            {
                return _model;
            }
        }

        public float[] myFaceLinesArray = null;
        public uint[] myFaceLinesIds = null;
        public bool facesInitialized = false;

        /// <summary>
        /// Getter
        /// </summary>
        public List<IFCItem> Geometry
        {
            get
            {
                return _lsGeometry;
            }
        }

        public Dictionary<long, IFCItem> Items
        {
            get
            {
                return _dicItems;
            }
        }

        /// <summary>
        /// Getter
        /// </summary>
        public IFCMaterialsBuilder MaterailsBuilder
        {
            get
            {
                return _materailsBuilder;
            }
        }

        /// <summary>
        /// Fires ModelLoaded event
        /// </summary>
        private void FireModelLoaded()
        {
            if (ModelLoaded != null)
            {
                ModelLoaded(this, new EventArgs());
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private bool ParseIFCFile(string strPath)
        {
            if (!File.Exists(strPath))
            {
                return false;
            }

            IfcEngineAnyCPU.setStringUnicode(1);

            _model = IfcEngineAnyCPU.sdaiOpenModelBNUnicode(0, strPath, null);
            if (_model == 0)
            {
                return false;
            }

            string strExpFile = Assembly.GetExecutingAssembly().Location.ToUpper().Replace("IFCVIEWERSGL.DLL", "");

            IntPtr outputValue = IntPtr.Zero;
            IfcEngineAnyCPU.GetSPFFHeaderItem(_model, 9, 0, IfcEngineAnyCPU.sdaiUNICODE, out outputValue);

            string strVersion = Marshal.PtrToStringUni(outputValue);

            IfcEngineAnyCPU.sdaiCloseModel(_model);
            _model = 0;

            if (strVersion.Contains("IFC2"))
            {
                strExpFile += "IFC2X3_TC1.exp";
                _model = IfcEngineAnyCPU.sdaiOpenModelBNUnicode(0, strPath, strExpFile);
            }
            else
            {
                if (strVersion.Contains("IFC4x") || strVersion.Contains("IFC4X"))
                {
                    strExpFile += "IFC4x1_FINAL.exp";
                    _model = IfcEngineAnyCPU.sdaiOpenModelBNUnicode(0, strPath, strExpFile);
                }
                else
                {
                    if (strVersion.Contains("IFC4") ||
                        strVersion.Contains("IFC2x4") ||
                        strVersion.Contains("IFC2X4"))
                    {
                        strExpFile += "IFC4_ADD2.exp";
                        _model = IfcEngineAnyCPU.sdaiOpenModelBNUnicode(0, strPath, strExpFile);
                    }
                }
            }

            if (_model == 0)
            {
                return false;
            }

            ifcSpace_TYPE						= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCSPACE");
            ifcOpeningElement_TYPE              = IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCOPENINGELEMENT");
		    ifcDistributionElement_TYPE			= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCDISTRIBUTIONELEMENT");
            ifcElectricalElement_TYPE			= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCELECTRICALELEMENT");
            ifcElementAssembly_TYPE				= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCELEMENTASSEMBLY");
            ifcElementComponent_TYPE			= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCELEMENTCOMPONENT");
            ifcEquipmentElement_TYPE			= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCEQUIPMENTELEMENT");
            ifcFeatureElement_TYPE				= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCFEATUREELEMENT");
            ifcFeatureElementSubtraction_TYPE	= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCFEATUREELEMENTSUBTRACTION");
            ifcFurnishingElement_TYPE			= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCFURNISHINGELEMENT");
            ifcReinforcingElement_TYPE			= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCREINFORCINGELEMENT");
            ifcTransportElement_TYPE			= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCTRANSPORTELEMENT");
            ifcVirtualElement_TYPE				= IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCVIRTUALELEMENT");

            ifcFeatureElementSubtraction_TYPE = IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCPRODUCT");

            long iCircleSegments = IFCItem.DEFAULT_CIRCLE_SEGMENTS;
            RetrieveObjects(_model, IfcEngineAnyCPU.sdaiGetEntity(_model, "IFCPRODUCT"), iCircleSegments);

            RetrieveObjects(_model, "IFCRELSPACEBOUNDARY", iCircleSegments);

            _materailsBuilder = new IFCMaterialsBuilder(_model);

            GenerateGeometry(_model);

            /*
             * Calculate min/max
             */
            _vecMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            _vecMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            foreach (var ifcObject in Geometry)
            {
                for (int i = 0; i < ifcObject._vertices.Length; i += 6)
                {
                    _vecMin.x = Math.Min(_vecMin.x, ifcObject._vertices[i]);
                    _vecMin.y = Math.Min(_vecMin.y, ifcObject._vertices[i + 1]);
                    _vecMin.z = Math.Min(_vecMin.z, ifcObject._vertices[i + 2]);

                    _vecMax.x = Math.Max(_vecMax.x, ifcObject._vertices[i]);
                    _vecMax.y = Math.Max(_vecMax.y, ifcObject._vertices[i + 1]);
                    _vecMax.z = Math.Max(_vecMax.z, ifcObject._vertices[i + 2]);
                }
            } // foreach (var ifcObject in ...

            float fMax = _vecMax.x - _vecMin.x;
            fMax = Math.Max(fMax, _vecMax.y - _vecMin.y);
            fMax = Math.Max(fMax, _vecMax.z - _vecMin.z);


            /*
             * Center and scale
             */
            foreach (var ifcObject in Geometry)
            {
                for (int i = 0; i < ifcObject._vertices.Length; i += 6)
                {
                    // Move => [0.0 -> X/Y/Zmin + X/Y/Zmax]
                    ifcObject._vertices[i] -= _vecMin.x;
                    ifcObject._vertices[i + 1] -= _vecMin.y;
                    ifcObject._vertices[i + 2] -= _vecMin.z;

                    // Center
                    ifcObject._vertices[i] -= (_vecMax.x - _vecMin.x) / 2f;
                    ifcObject._vertices[i + 1] -= (_vecMax.y - _vecMin.y) / 2f;
                    ifcObject._vertices[i + 2] -= (_vecMax.z - _vecMin.z) / 2f;

                    // Scale => [-1.0 -> 1.0]
                    ifcObject._vertices[i] /= fMax / 2f;
                    ifcObject._vertices[i + 1] /= fMax / 2f;
                    ifcObject._vertices[i + 2] /= fMax / 2f;
                } // for (int i = ...
            } // foreach (var ifcObject in ...

            myFaceLinesArray = null;
            facesInitialized = false;

            /*
            * Assign unique IDs
            */

            long iID = 1;
            foreach (var ifcObject in Geometry)
            {
                ifcObject._ID = iID++;
            }

            /*
            * Build the selection colors
            */

            const float STEP = 1.0f / 255.0f;	

            foreach (var ifcObject in Geometry)
            {
                float fR = (float)Math.Floor((float)ifcObject._ID / (255.0f * 255.0f));
                if (fR >= 1.0f)
                {
                    fR *= STEP;
                }

                float fG = (float)Math.Floor((float)ifcObject._ID / 255.0f);
                if (fG >= 1.0f)
                {
                    fG *= STEP;
                }

                float fB = (float)(ifcObject._ID % 255);
                fB *= STEP;

                ifcObject._selectionColor = new STRUCT_COLOR();
                ifcObject._selectionColor.R = fR;
                ifcObject._selectionColor.G = fG;
                ifcObject._selectionColor.B = fB;
            } // foreach (var ifcObject in ...

            /*
             * Notify the clients
             */
            FireModelLoaded();

            ///////////////////////////////////////////////////////////////////////////////////////
            // PostgreSQL
            //try
            //{
            //    var start = DateTime.Now;

            //    var connectionString = "Host=localhost;Username=postgres;Password=qwe123;Database=ifcmodels";
            //    PostgreSQL postgreSQL = new PostgreSQL(connectionString);

            //    postgreSQL.Publish(this);

            //    var end = DateTime.Now;
            //    var duration = start - end;
            //}
            //catch (Exception ex)
            //{
            //    Debug.Assert(false, ex.GetBaseException().Message);
            //}
            ///////////////////////////////////////////////////////////////////////////////////////

            return true;
        }

        /// <summary>
        /// Generates the indices/vertices
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="ifcItem"></param>
        private void GenerateGeometry(long ifcModel)
        {
            foreach (var ifcItem in _dicItems.Values)
            {
                RetrieveGeometry(ifcModel, ifcItem);
            }
        }

        /// <summary>
        /// Retrieves the geometry
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="ifcItem"></param>
        private void RetrieveGeometry(long ifcModel, IFCItem ifcItem)
        {
            if (ifcItem._instance == 0)
            {
                return;
            }

            /**************************************************************************************
             * Faces, faces polygons, lines and points
             */

            SetFormat(ifcModel);

            IfcEngineAnyCPU.setFilter(ifcModel, IfcEngineAnyCPU.flagbit1, IfcEngineAnyCPU.flagbit1);

            //IfcEngineAnyCPU.circleSegments(ifcItem.circleSegments, 5);

            Int64 iVerticesCount = 0;
            Int64 iIndicesCount = 0;
            Int64 iTransformationBufferSize = 0;
            IfcEngineAnyCPU.CalculateInstance(ifcItem._instance, ref iVerticesCount, ref iIndicesCount, ref iTransformationBufferSize);

            if ((iVerticesCount == 0) || (iIndicesCount == 0))
            {
                // the items without a geometry are hidden by default
                ifcItem._visible = false;

                return;
            }

            float[] vertices = new float[10 * iVerticesCount];
            int[] indices = new int[iIndicesCount];

            Int64 owlModel = 0;
            IfcEngineAnyCPU.owlGetModel(ifcModel, ref owlModel);

            Int64 owlInstance = 0;
            IfcEngineAnyCPU.owlGetInstance(ifcModel, ifcItem._instance, ref owlInstance);

            // ((R * 255 + G) * 255 + B) * 255 + A
            UInt32 R = 10,
            G = 200,
            B = 10,
            A = 255;
            UInt32 defaultColor = 256 * 256 * 256 * R +
                                  256 * 256 * G +
                                  256 * B +
                                  A;
            IfcEngineAnyCPU.SetDefaultColor(ifcModel, defaultColor, defaultColor, defaultColor, defaultColor);
            IfcEngineAnyCPU.UpdateInstanceVertexBuffer(owlInstance, vertices);
            IfcEngineAnyCPU.UpdateInstanceIndexBuffer(owlInstance, indices);

            ifcItem._vertices = new float[6 * iVerticesCount];
            for (int iVertex = 0; iVertex < iVerticesCount; iVertex++)
            {
                ifcItem._vertices[(iVertex * 6) + 0] = vertices[(iVertex * 10) + 0];
                ifcItem._vertices[(iVertex * 6) + 1] = vertices[(iVertex * 10) + 1];
                ifcItem._vertices[(iVertex * 6) + 2] = vertices[(iVertex * 10) + 2];
                ifcItem._vertices[(iVertex * 6) + 3] = vertices[(iVertex * 10) + 3];
                ifcItem._vertices[(iVertex * 6) + 4] = vertices[(iVertex * 10) + 4];
                ifcItem._vertices[(iVertex * 6) + 5] = vertices[(iVertex * 10) + 5];

                if ((ifcItem._vertices[(iVertex * 6) + 0] < -100000000 || ifcItem._vertices[(iVertex * 6) + 0] > 10000000) ||
                   (ifcItem._vertices[(iVertex * 6) + 1] < -100000000 || ifcItem._vertices[(iVertex * 6) + 1] > 10000000) ||
                   (ifcItem._vertices[(iVertex * 6) + 2] < -100000000 || ifcItem._vertices[(iVertex * 6) + 2] > 10000000) ||
                   (ifcItem._vertices[(iVertex * 6) + 3] < -100000000 || ifcItem._vertices[(iVertex * 6) + 3] > 10000000) ||
                   (ifcItem._vertices[(iVertex * 6) + 4] < -100000000 || ifcItem._vertices[(iVertex * 6) + 4] > 10000000) ||
                   (ifcItem._vertices[(iVertex * 6) + 5] < -100000000 || ifcItem._vertices[(iVertex * 6) + 5] > 10000000))
                {
                    //int u = 0;
                }
            }

            List<int> lsFacesIndices = new List<int>();
            List<uint> lsFacesPolygonsIndices = new List<uint>();
            List<uint> lsLinesIndices = new List<uint>();
            List<uint> lsPointsIndices = new List<uint>();

            long iFacesCount = IfcEngineAnyCPU.getConceptualFaceCnt(ifcItem._instance);

            List<long> lsMaxIndex = new List<long>();

            for (long iFace = 0; iFace < iFacesCount; iFace++)
            {
                long iStartIndexTriangles = 0;
                long iIndicesCountTriangles = 0;

                long iStartIndexLines = 0;
                long iIndicesCountLines = 0;

                long iStartIndexPoints = 0;
                long iIndicesCountPoints = 0;

                long iStartIndexFacesPolygons = 0;
                long iIndicesCountFacesPolygons = 0;

                long iValue = 0;
                IfcEngineAnyCPU.getConceptualFaceEx(ifcItem._instance,
                    iFace,
                    ref iStartIndexTriangles,
                    ref iIndicesCountTriangles,
                    ref iStartIndexLines,
                    ref iIndicesCountLines,
                    ref iStartIndexPoints,
                    ref iIndicesCountPoints,
                    ref iStartIndexFacesPolygons,
                    ref iIndicesCountFacesPolygons,
                    ref iValue,
                    ref iValue);

                ifcItem._noPrimitivesForFaces += iIndicesCountTriangles / 3;

                /*
                 * Faces
                 */
                for (long iIndexTriangles = iStartIndexTriangles; iIndexTriangles < iStartIndexTriangles + iIndicesCountTriangles; iIndexTriangles++)
                {
                    lsFacesIndices.Add(indices[iIndexTriangles]);
                }

                /*
                 * Faces polygons
                 */
                long iIndexWireframes = 0;
                int iLastIndex = -1;
                while (iIndexWireframes < iIndicesCountFacesPolygons)
                {
                    if ((iLastIndex >= 0) && (indices[iStartIndexFacesPolygons + iIndexWireframes] >= 0))
                    {
                        lsFacesPolygonsIndices.Add((uint)iLastIndex);

                        lsFacesPolygonsIndices.Add((uint)indices[iStartIndexFacesPolygons + iIndexWireframes]);
                    }

                    iLastIndex = indices[iStartIndexFacesPolygons + iIndexWireframes];
                    iIndexWireframes++;
                }

                /*
                 * Lines
                 */
                for (long iIndex = iStartIndexLines; iIndex < iStartIndexLines + iIndicesCountLines; iIndex++)
                {
                    if (indices[iIndex] < 0)
                    {
                        continue;
                    }

                    lsLinesIndices.Add((uint)indices[iIndex]);
                }

                /*
                 * Points
                 */
                for (long iIndex = iStartIndexLines; iIndex < iStartIndexPoints + iIndicesCountPoints; iIndex++)
                {
                    lsPointsIndices.Add((uint)indices[iIndex]);
                }
            } // for (long iFace ...

            ifcItem._facesIndices = lsFacesIndices.ToArray();
            ifcItem._facesPolygonsIndices = lsFacesPolygonsIndices.ToArray();
            ifcItem._linesIndices = lsLinesIndices.ToArray();
            ifcItem._pointsIndices = lsPointsIndices.ToArray();

            /**************************************************************************************
             * Materials
             */

            if (ifcItem._noPrimitivesForFaces > 0)
            {
                ifcItem._materials = null;
                _materailsBuilder.extractMaterials(ifcItem, vertices, indices);
            }

            _lsGeometry.Add(ifcItem);

            IfcEngineAnyCPU.cleanMemory(ifcModel, 0);
        }
        
        /// <summary>
        /// Format
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="bWireframes"></param>
        private void SetFormat(long ifcModel)
        {
            long setting = 0, mask = 0;
            mask += IfcEngineAnyCPU.flagbit2;        //    PRECISION (32/64 bit)
            mask += IfcEngineAnyCPU.flagbit3;        //	INDEX ARRAY (32/64 bit)
            mask += IfcEngineAnyCPU.flagbit5;        //    NORMALS
            mask += IfcEngineAnyCPU.flagbit8;        //    TRIANGLES
            mask += IfcEngineAnyCPU.flagbit9;        //    LINES
            mask += IfcEngineAnyCPU.flagbit10;       //    POINTS
            mask += IfcEngineAnyCPU.flagbit12;       //    WIREFRAME
            mask += IfcEngineAnyCPU.flagbit24;		  //	AMBIENT
            mask += IfcEngineAnyCPU.flagbit25;		  //	DIFFUSE
            mask += IfcEngineAnyCPU.flagbit26;		  //	EMISSIVE
            mask += IfcEngineAnyCPU.flagbit27;		  //	SPECULAR

		    setting += 0;		                      //    SINGLE PRECISION (float)
		    setting += 0;                             //    32 BIT INDEX ARRAY (Int32)
            setting += IfcEngineAnyCPU.flagbit5;     //    NORMALS ON
            setting += IfcEngineAnyCPU.flagbit8;     //    TRIANGLES ON
            setting += IfcEngineAnyCPU.flagbit9;     //    LINES ON
            setting += IfcEngineAnyCPU.flagbit10;    //    POINTS ON
            setting += IfcEngineAnyCPU.flagbit12;    //    WIREFRAME ON
            setting += IfcEngineAnyCPU.flagbit24;	  //	AMBIENT
            setting += IfcEngineAnyCPU.flagbit25;	  //	DIFFUSE
            setting += IfcEngineAnyCPU.flagbit26;	  //	EMISSIVE
            setting += IfcEngineAnyCPU.flagbit27;	  //	SPECULAR

            IfcEngineAnyCPU.setFormat(ifcModel, setting, mask);
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="strIfcType"></param>
        /// <param name="iCircleSegments"></param>
        private void RetrieveObjects(long ifcModel, string strIfcType, long iCircleSegments)
        {
            long ifcObjectInstances = IfcEngineAnyCPU.sdaiGetEntityExtentBN(ifcModel, strIfcType);
            long noIfcObjectIntances = IfcEngineAnyCPU.sdaiGetMemberCount(ifcObjectInstances);

            for (long i = 0; i < noIfcObjectIntances; ++i)
            {
                long ifcObjectIns = 0;
                IfcEngineAnyCPU.engiGetAggrElement(ifcObjectInstances, i, IfcEngineAnyCPU.sdaiINSTANCE, out ifcObjectIns);

                IFCItem ifcItem = new IFCItem();
                // hide by default
                ifcItem._visible = (strIfcType.ToUpper() != "IFCSPACE") && (strIfcType.ToUpper() != "IFCRELSPACEBOUNDARY") && (strIfcType.ToUpper() != "IFCOPENINGELEMENT");
                ifcItem.circleSegments = iCircleSegments;
                ifcItem._instance = ifcObjectIns;
                ifcItem._ifcType = strIfcType;

                _dicItems.Add(ifcItem._instance, ifcItem);
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="iParent"></param>
        /// <param name="iCircleSegments"></param>
        private void RetrieveObjects(long ifcModel, long iParent, long iCircleSegments)
        {
            if ((iParent == ifcDistributionElement_TYPE) ||
                (iParent == ifcElectricalElement_TYPE) ||
                (iParent == ifcElementAssembly_TYPE) ||
                (iParent == ifcElementComponent_TYPE) ||
                (iParent == ifcEquipmentElement_TYPE) ||
                (iParent == ifcFeatureElement_TYPE) ||
                (iParent == ifcFurnishingElement_TYPE) ||
                (iParent == ifcTransportElement_TYPE) ||
                (iParent == ifcVirtualElement_TYPE))
            {
                iCircleSegments = 12;
            }

            if (iParent == ifcReinforcingElement_TYPE)
            {
                iCircleSegments = 6;
            }

            long ifcObjectInstances = IfcEngineAnyCPU.sdaiGetEntityExtent(ifcModel, iParent);
            long noIfcObjectIntances = IfcEngineAnyCPU.sdaiGetMemberCount(ifcObjectInstances);

            if (noIfcObjectIntances != 0)
            {
                IntPtr name;
                IfcEngineAnyCPU.engiGetEntityName(iParent, IfcEngineAnyCPU.sdaiUNICODE, out name);

                string strIfcParentEntityName = Marshal.PtrToStringUni(name);

                RetrieveObjects(ifcModel, strIfcParentEntityName, iCircleSegments);
            } // if (noIfcObjectIntances != 0)

            noIfcObjectIntances = IfcEngineAnyCPU.engiGetEntityCount(ifcModel);
            for (long i = 0; i < noIfcObjectIntances; i++)
            {
                long ifcEntity = IfcEngineAnyCPU.engiGetEntityElement(ifcModel, i);
                if (IfcEngineAnyCPU.engiGetEntityParent(ifcEntity) == iParent)
                {
                    RetrieveObjects(ifcModel, ifcEntity, iCircleSegments);
                }
            }
        }
    }
}
