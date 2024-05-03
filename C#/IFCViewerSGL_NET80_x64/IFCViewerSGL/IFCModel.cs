using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

#if _WIN64
using int_t = System.Int64;
#else
using int_t = System.Int32;
#endif

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
        private int_t _iModel = 0;

        /// <summary>
        /// List of the IFCItems with a geometry
        /// </summary>
        private List<IFCItem> _lsGeometry = new List<IFCItem>();

        /// <summary>
        /// ID : IFCItems
        /// </summary>
        private Dictionary<int_t, IFCItem> _dicItems = new Dictionary<int_t, IFCItem>();

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

        int_t ifcSpace_TYPE;
        int_t ifcOpeningElement_TYPE;
		int_t ifcDistributionElement_TYPE;
		int_t ifcElectricalElement_TYPE;
		int_t ifcElementAssembly_TYPE;
		int_t ifcElementComponent_TYPE;
		int_t ifcEquipmentElement_TYPE;
		int_t ifcFeatureElement_TYPE;
		int_t ifcFeatureElementSubtraction_TYPE;
		int_t ifcFurnishingElement_TYPE;
		int_t ifcReinforcingElement_TYPE;
		int_t ifcTransportElement_TYPE;
		int_t ifcVirtualElement_TYPE;

        #endregion // Events

        /// <summary>
        /// ctor
        /// </summary>
        public IFCModel()
        {
        }

        /// <summary>
        /// Loads an IFC file
        /// </summary>
        /// <param name="strIfcFilePath"></param>
        /// <returns></returns>
        public bool Load(string strIfcFilePath)
        {
            _strIFCFile = Path.GetFileName(strIfcFilePath);
            _iModel = 0;
            _dicItems = new Dictionary<int_t, IFCItem>();
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
        public int_t Model
        {
            get
            {
                return _iModel;
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

        public Dictionary<int_t, IFCItem> Items
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

            if (_iModel != 0)
            {
                IfcEngine.x86_64.sdaiCloseModel(_iModel);
                _iModel = 0;
            }

            IfcEngine.x86_64.setStringUnicode(1);

            _iModel = IfcEngine.x86_64.sdaiOpenModelBNUnicode(0, Encoding.Unicode.GetBytes(strPath), null);
            if (_iModel == 0)
            {
                return false;
            }

            string strExpFile = Assembly.GetExecutingAssembly().Location.ToUpper().Replace("IFCVIEWERSGL.DLL", "");

            IntPtr outputValue = IntPtr.Zero;
            IfcEngine.x86_64.GetSPFFHeaderItem(_iModel, 9, 0, IfcEngine.x86_64.sdaiUNICODE, out outputValue);

            string strVersion = Marshal.PtrToStringUni(outputValue);

            IfcEngine.x86_64.sdaiCloseModel(_iModel);
            _iModel = 0;

            if (strVersion.Contains("IFC2"))
            {
                strExpFile += "IFC2X3_TC1.exp";

                _iModel = IfcEngine.x86_64.sdaiOpenModelBNUnicode(0, Encoding.Unicode.GetBytes(strPath), Encoding.Unicode.GetBytes(strExpFile));
            }
            else
            {
                if (strVersion.Contains("IFC4x") || strVersion.Contains("IFC4X"))
                {
                    strExpFile += "IFC4x1_FINAL.exp";

                    _iModel = IfcEngine.x86_64.sdaiOpenModelBNUnicode(0, Encoding.Unicode.GetBytes(strPath), Encoding.Unicode.GetBytes(strExpFile));
                }
                else
                {
                    if (strVersion.Contains("IFC4") ||
                        strVersion.Contains("IFC2x4") ||
                        strVersion.Contains("IFC2X4"))
                    {
                        strExpFile += "IFC4_ADD2.exp";

                        _iModel = IfcEngine.x86_64.sdaiOpenModelBNUnicode(0, Encoding.Unicode.GetBytes(strPath), Encoding.Unicode.GetBytes(strExpFile));
                    }
                }
            }

            if (_iModel == 0)
            {
                return false;
            }

            ifcSpace_TYPE						= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCSPACE"));
            ifcOpeningElement_TYPE              = IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCOPENINGELEMENT"));
		    ifcDistributionElement_TYPE			= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCDISTRIBUTIONELEMENT"));
		    ifcElectricalElement_TYPE			= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCELECTRICALELEMENT"));
		    ifcElementAssembly_TYPE				= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCELEMENTASSEMBLY"));
		    ifcElementComponent_TYPE			= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCELEMENTCOMPONENT"));
		    ifcEquipmentElement_TYPE			= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCEQUIPMENTELEMENT"));
		    ifcFeatureElement_TYPE				= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCFEATUREELEMENT"));
		    ifcFeatureElementSubtraction_TYPE	= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCFEATUREELEMENTSUBTRACTION"));
		    ifcFurnishingElement_TYPE			= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCFURNISHINGELEMENT"));
		    ifcReinforcingElement_TYPE			= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCREINFORCINGELEMENT"));
		    ifcTransportElement_TYPE			= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCTRANSPORTELEMENT"));
		    ifcVirtualElement_TYPE				= IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCVIRTUALELEMENT"));

            int_t iCircleSegments = IFCItem.DEFAULT_CIRCLE_SEGMENTS;
            RetrieveObjects(_iModel, IfcEngine.x86_64.sdaiGetEntity(_iModel, Encoding.Unicode.GetBytes("IFCPRODUCT")), iCircleSegments);

            RetrieveObjects(_iModel, "IFCRELSPACEBOUNDARY", iCircleSegments);

            _materailsBuilder = new IFCMaterialsBuilder(_iModel);

            GenerateGeometry(_iModel);

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

            int_t iID = 1;
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

            return true;
        }

        /// <summary>
        /// Generates the indices/vertices
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="ifcItem"></param>
        private void GenerateGeometry(int_t ifcModel)
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
        private void RetrieveGeometry(int_t ifcModel, IFCItem ifcItem)
        {
            if (ifcItem._instance == 0)
            {
                return;
            }

            /**************************************************************************************
             * Faces, faces polygons, lines and points
             */

            SetFormat(ifcModel);

            IfcEngine.x86_64.setFilter(ifcModel, IfcEngine.x86_64.flagbit1, IfcEngine.x86_64.flagbit1);

            IfcEngine.x86_64.circleSegments(ifcItem.circleSegments, 5);

            Int64 iVerticesCount = 0;
            Int64 iIndicesCount = 0;
            Int64 iTransformationBufferSize = 0;
            IfcEngine.x86_64.CalculateInstance(ifcItem._instance, ref iVerticesCount, ref iIndicesCount, ref iTransformationBufferSize);

       

            if ((iVerticesCount == 0) || (iIndicesCount == 0))
            {
                // the items without a geometry are hidden by default
                ifcItem._visible = false;

                return;
            }

            float[] vertices = new float[10 * iVerticesCount];
            int[] indices = new int[iIndicesCount];

            Int64 owlModel = 0;
            IfcEngine.x86_64.owlGetModel(ifcModel, ref owlModel);

            Int64 owlInstance = 0;
            IfcEngine.x86_64.owlGetInstance(ifcModel, ifcItem._instance, ref owlInstance);

            // ((R * 255 + G) * 255 + B) * 255 + A
            UInt32 R = 10,
            G = 200,
            B = 10,
            A = 255;
            UInt32 defaultColor = 256 * 256 * 256 * R +
                                  256 * 256 * G +
                                  256 * B +
                                  A;
            IfcEngine.x86_64.SetDefaultColor(ifcModel, defaultColor, defaultColor, defaultColor, defaultColor);
            IfcEngine.x86_64.UpdateInstanceVertexBuffer(owlInstance, vertices);
            IfcEngine.x86_64.UpdateInstanceIndexBuffer(owlInstance, indices);

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
                    int u = 0;
                }
            }

            List<int> lsFacesIndices = new List<int>();
            List<uint> lsFacesPolygonsIndices = new List<uint>();
            List<uint> lsLinesIndices = new List<
                
                uint>();
            List<uint> lsPointsIndices = new List<uint>();          

            int_t iFacesCount = IfcEngine.x86_64.getConceptualFaceCnt(ifcItem._instance);

            List<int_t> lsMaxIndex = new List<int_t>();

            for (int_t iFace = 0; iFace < iFacesCount; iFace++)
            {
                int_t iStartIndexTriangles = 0;
                int_t iIndicesCountTriangles = 0;

                int_t iStartIndexLines = 0;
                int_t iIndicesCountLines = 0;

                int_t iStartIndexPoints = 0;
                int_t iIndicesCountPoints = 0;

                int_t iStartIndexFacesPolygons = 0;
                int_t iIndicesCountFacesPolygons = 0;

                int_t iValue = 0;
                IfcEngine.x86_64.getConceptualFaceEx(ifcItem._instance,
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
                for (int_t iIndexTriangles = iStartIndexTriangles; iIndexTriangles < iStartIndexTriangles + iIndicesCountTriangles; iIndexTriangles++)
                {
                    lsFacesIndices.Add(indices[iIndexTriangles]);
                }

                /*
                 * Faces polygons
                 */
                Int32 iIndexWireframes = 0;
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
                for (int_t iIndex = iStartIndexLines; iIndex < iStartIndexLines + iIndicesCountLines; iIndex++)
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
                for (int_t iIndex = iStartIndexLines; iIndex < iStartIndexPoints + iIndicesCountPoints; iIndex++)
                {
                    lsPointsIndices.Add((uint)indices[iIndex]);
                }
            } // for (int_t iFace ...

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
        }
        
        /// <summary>
        /// Format
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="bWireframes"></param>
        private void SetFormat(int_t ifcModel)
        {
            int_t setting = 0, mask = 0;
            mask += IfcEngine.x86_64.flagbit2;        //    PRECISION (32/64 bit)
            mask += IfcEngine.x86_64.flagbit3;        //	INDEX ARRAY (32/64 bit)
            mask += IfcEngine.x86_64.flagbit5;        //    NORMALS
            mask += IfcEngine.x86_64.flagbit8;        //    TRIANGLES
            mask += IfcEngine.x86_64.flagbit9;        //    LINES
            mask += IfcEngine.x86_64.flagbit10;       //    POINTS
            mask += IfcEngine.x86_64.flagbit12;       //    WIREFRAME
            mask += IfcEngine.x86_64.flagbit24;		  //	AMBIENT
            mask += IfcEngine.x86_64.flagbit25;		  //	DIFFUSE
            mask += IfcEngine.x86_64.flagbit26;		  //	EMISSIVE
            mask += IfcEngine.x86_64.flagbit27;		  //	SPECULAR

		    setting += 0;		                      //    SINGLE PRECISION (float)
		    setting += 0;                             //    32 BIT INDEX ARRAY (Int32)
            setting += IfcEngine.x86_64.flagbit5;     //    NORMALS ON
            setting += IfcEngine.x86_64.flagbit8;     //    TRIANGLES ON
            setting += IfcEngine.x86_64.flagbit9;     //    LINES ON
            setting += IfcEngine.x86_64.flagbit10;    //    POINTS ON
            setting += IfcEngine.x86_64.flagbit12;    //    WIREFRAME ON
            setting += IfcEngine.x86_64.flagbit24;	  //	AMBIENT
            setting += IfcEngine.x86_64.flagbit25;	  //	DIFFUSE
            setting += IfcEngine.x86_64.flagbit26;	  //	EMISSIVE
            setting += IfcEngine.x86_64.flagbit27;	  //	SPECULAR

            IfcEngine.x86_64.setFormat(ifcModel, setting, mask);
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="strIfcType"></param>
        /// <param name="iCircleSegments"></param>
        private void RetrieveObjects(int_t ifcModel, string strIfcType, int_t iCircleSegments)
        {
            int_t ifcObjectInstances = IfcEngine.x86_64.sdaiGetEntityExtentBN(ifcModel, Encoding.Unicode.GetBytes(strIfcType));
            int_t noIfcObjectIntances = IfcEngine.x86_64.sdaiGetMemberCount(ifcObjectInstances);

            for (int_t i = 0; i < noIfcObjectIntances; ++i)
            {
                int_t ifcObjectIns = 0;
                IfcEngine.x86_64.engiGetAggrElement(ifcObjectInstances, i, IfcEngine.x86_64.sdaiINSTANCE, out ifcObjectIns);

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
        private void RetrieveObjects(int_t ifcModel, int_t iParent, int_t iCircleSegments)
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

            int_t ifcObjectInstances = IfcEngine.x86_64.sdaiGetEntityExtent(ifcModel, iParent);
            int_t noIfcObjectIntances = IfcEngine.x86_64.sdaiGetMemberCount(ifcObjectInstances);

            if (noIfcObjectIntances != 0)
            {
                IntPtr name;                
                IfcEngine.x86_64.engiGetEntityName(iParent, IfcEngine.x86_64.sdaiUNICODE, out name);

                string strIfcParentEntityName = Marshal.PtrToStringUni(name);

                RetrieveObjects(ifcModel, strIfcParentEntityName, iCircleSegments);
            } // if (noIfcObjectIntances != 0)

            noIfcObjectIntances = IfcEngine.x86_64.engiGetEntityCount(ifcModel);
            for (int_t i = 0; i < noIfcObjectIntances; i++)
            {
                int_t ifcEntity = IfcEngine.x86_64.engiGetEntityElement(ifcModel, i);
                if (IfcEngine.x86_64.engiGetEntityParent(ifcEntity) == iParent)
                {
                    RetrieveObjects(ifcModel, ifcEntity, iCircleSegments);
                }
            }
        }
    }
}
