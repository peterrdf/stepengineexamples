using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace IFCViewer_Xamarin_iOS
{    public class IfcModel
    {
        /// <summary>
        /// Model
        /// </summary>
        private Int64 _model;

        /// <summary>
        /// Entities
        /// </summary>
        Int64 _ifcProjectEntity;
        Int64 _ifcSpaceEntity;
        Int64 _ifcOpeningElementEntity;
        Int64 _ifcDistributionElementEntity;
        Int64 _ifcElectricalElementEntity;
        Int64 _ifcElementAssemblyEntity;
        Int64 _ifcElementComponentEntity;
        Int64 _ifcEquipmentElementEntity;
        Int64 _ifcFeatureElementEntity;
        Int64 _ifcFeatureElementSubtractionEntity;
        Int64 _ifcFurnishingElementEntity;
        Int64 _ifcReinforcingElementEntity;
        Int64 _ifcTransportElementEntity;
        Int64 _ifcVirtualElementEntity;

        /// <summary>
        /// ID : IFCItems
        /// </summary>
        private Dictionary<Int64, IFCItem> _dicIFCItems = new Dictionary<Int64, IFCItem>();

        /// <summary>
        /// Min
        /// </summary>
        private Vector3 _vecMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        /// <summary>
        /// Max
        /// </summary>
        private Vector3 _vecMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        /// <summary>
        /// Loads a file/resource
        /// </summary>
        public void LoadModel(string strIfcFile, bool bResource)
        {
            if (_model != 0)
            {
                IfcEngine.x64.sdaiCloseModel(_model);
                _model = 0;
            }

            string strIfcFileFullPath = string.Empty;

            // Clean up
            _dicIFCItems = new Dictionary<Int64, IFCItem>();

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Debug.WriteLine(documentsPath);

            if (bResource)
            {
                /*
                * Load embedded resource
                */
                var ifcContent = LoadResource(strIfcFile);

                strIfcFileFullPath = Path.Combine(documentsPath, strIfcFile);
                Debug.WriteLine(strIfcFileFullPath);

                /*
                 * Save as a file
                 */
                File.WriteAllText(strIfcFileFullPath, ifcContent);
            }
            else
            {
                strIfcFileFullPath = strIfcFile;
            }

            _model = IfcEngine.x64.sdaiOpenModelBN(0, strIfcFileFullPath, null);
            Debug.WriteLine("sdaiOpenModelBN(): " + _model.ToString());

            if (_model == 0)
            {
                Debug.WriteLine("Failed to load: " + strIfcFile);

                return;
            }

            IntPtr schema;
            IfcEngine.x64.GetSPFFHeaderItem(_model, 9, 0, IfcEngine.x64.sdaiSTRING, out schema);

            string strSchema = Marshal.PtrToStringAuto(schema);
            Debug.WriteLine("Schema: " + strSchema);

            IfcEngine.x64.sdaiCloseModel(_model);

            /*
             * Embedded Schema
             */
            if (strSchema.IndexOf("IFC2") != -1)
            {
                IfcEngine.x64.setFilter(0, 2097152, 1048576 + 2097152 + 4194304);
            }
            else
            {
                if ((strSchema.IndexOf("IFC4x") != -1) || (strSchema.IndexOf("IFC4X") != -1))
                {
                    IfcEngine.x64.setFilter(0, 1048576 + 2097152, 1048576 + 2097152 + 4194304);
                }
                else
                {
                    if ((strSchema.IndexOf("IFC4") != -1) ||
                        (strSchema.IndexOf("IFC2x4") != -1) ||
                        (strSchema.IndexOf("IFC2X4") != -1))
                    {
                        IfcEngine.x64.setFilter(0, 1048576, 1048576 + 2097152 + 4194304);
                    }
                    else
                    {
                        Debug.Assert(false);
                    }
                }
            }

            _model = IfcEngine.x64.sdaiOpenModelBN(0, strIfcFileFullPath, "");

            /*
             * External Schema
             */
            //if (strSchema.IndexOf("IFC2") != -1)
            //{
            //    /*
            //    * Load embedded resource
            //    */
            //    var schemaContent = LoadResource("IFC2X3_TC1.exp");

            //    var schemaFileFullPath = Path.Combine(documentsPath, "IFC2X3_TC1.exp");
            //    Debug.WriteLine(schemaFileFullPath);

            //    /*
            //     * Save as a file
            //     */
            //    File.WriteAllText(schemaFileFullPath, schemaContent);

            //    _model = IfcEngine.x64.sdaiOpenModelBN(0, strIfcFileFullPath, schemaFileFullPath);
            //}
            //else
            //{
            //    if ((strSchema.IndexOf("IFC4x") != -1) || (strSchema.IndexOf("IFC4X") != -1))
            //    {
            //        Debug.Assert(false); // TODO
            //    }
            //    else
            //    {
            //        if ((strSchema.IndexOf("IFC4") != -1) ||
            //            (strSchema.IndexOf("IFC2x4") != -1) ||
            //            (strSchema.IndexOf("IFC2X4") != -1))
            //        {
            //            Debug.Assert(false); // TODO
            //        }
            //        else
            //        {
            //            Debug.Assert(false);
            //        }
            //    }
            //}

            if (_model == 0)
            {
                Debug.WriteLine("Failed to load: " + strIfcFile);

                return;
            }

            Debug.WriteLine("IFC Model: " + strIfcFile);
            Debug.WriteLine("Model: " + _model);

            Int64 ifcObjectEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCOBJECT");
            _ifcProjectEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCPROJECT");
            _ifcSpaceEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCSPACE");
            _ifcOpeningElementEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCOPENINGELEMENT");
            _ifcDistributionElementEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCDISTRIBUTIONELEMENT");
            _ifcElectricalElementEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCELECTRICALELEMENT");
            _ifcElementAssemblyEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCELEMENTASSEMBLY");
            _ifcElementComponentEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCELEMENTCOMPONENT");
            _ifcEquipmentElementEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCEQUIPMENTELEMENT");
            _ifcFeatureElementEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCFEATUREELEMENT");
            _ifcFeatureElementSubtractionEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCFEATUREELEMENTSUBTRACTION");
            _ifcFurnishingElementEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCFURNISHINGELEMENT");
            _ifcReinforcingElementEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCREINFORCINGELEMENT");
            _ifcTransportElementEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCTRANSPORTELEMENT");
            _ifcVirtualElementEntity = IfcEngine.x64.sdaiGetEntity(_model, "IFCVIRTUALELEMENT");

            RetrieveObjects_depth(ifcObjectEntity, IFCItem.DEFAULT_CIRCLE_SEGMENTS, 0);
            RetrieveObjects("IFCRELSPACEBOUNDARY", IFCItem.DEFAULT_CIRCLE_SEGMENTS);

            Debug.WriteLine("IFC Objects: " + _dicIFCItems.Count);

            PostProcessing();
        }

        /// <summary>
        /// Getter
        /// </summary>
        /// <returns></returns>
        public IDictionary<Int64, IFCItem> getIFCtems()
        {
            return _dicIFCItems;
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="strIfcType"></param>
        /// <param name="iCircleSegments"></param>
        private void RetrieveObjects(string strIfcType, Int64 iCircleSegments)
        {
            Int64 ifcObjectInstances = IfcEngine.x64.sdaiGetEntityExtentBN(_model, strIfcType);
            Int64 noIfcObjectIntances = IfcEngine.x64.sdaiGetMemberCount(ifcObjectInstances);

            for (Int64 i = 0; i < noIfcObjectIntances; ++i)
            {
                Int64 ifcObjectIns = 0;
                IfcEngine.x64.engiGetAggrElement(ifcObjectInstances, i, IfcEngine.x64.sdaiINSTANCE, out ifcObjectIns);

                IFCItem ifcItem = new IFCItem();
                ifcItem._ifcType = strIfcType;
                ifcItem._visible = IsVisible(ifcItem._ifcType);
                ifcItem.circleSegments = iCircleSegments;
                ifcItem._instance = ifcObjectIns;

                RetrieveGeometry(ifcItem, iCircleSegments);

                _dicIFCItems.Add(_dicIFCItems.Count + 1, ifcItem);
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="ifcModel"></param>
        /// <param name="iParentEntity"></param>
        /// <param name="iCircleSegments"></param>
        private void RetrieveObjects_depth(Int64 iParentEntity, Int64 iCircleSegments, Int64 depth)
        {
            if ((iParentEntity == _ifcDistributionElementEntity) ||
                (iParentEntity == _ifcElectricalElementEntity) ||
                (iParentEntity == _ifcElementAssemblyEntity) ||
                (iParentEntity == _ifcElementComponentEntity) ||
                (iParentEntity == _ifcEquipmentElementEntity) ||
                (iParentEntity == _ifcFeatureElementEntity) ||
                (iParentEntity == _ifcFurnishingElementEntity) ||
                (iParentEntity == _ifcTransportElementEntity) ||
                (iParentEntity == _ifcVirtualElementEntity))
            {
                iCircleSegments = 12;
            }

            if (iParentEntity == _ifcReinforcingElementEntity)
            {
                iCircleSegments = 6;
            }

            Int64 ifcObjectInstances = IfcEngine.x64.sdaiGetEntityExtent(_model, iParentEntity);
            Int64 noIfcObjectIntances = IfcEngine.x64.sdaiGetMemberCount(ifcObjectInstances);

            if (noIfcObjectIntances != 0)
            {
                IntPtr name;
                IfcEngine.x64.engiGetEntityName(iParentEntity, IfcEngine.x64.sdaiSTRING, out name);

                string strIfcParentEntityName = Marshal.PtrToStringAuto(name);

                RetrieveObjects(strIfcParentEntityName, iCircleSegments);

                if (iParentEntity == _ifcProjectEntity)
                {
                    for (Int64 i = 0; i < noIfcObjectIntances; ++i)
                    {
                        Int64 ifcObjectIns = 0;
                        IfcEngine.x64.engiGetAggrElement(ifcObjectInstances, i, IfcEngine.x64.sdaiINSTANCE, out ifcObjectIns);

                        IFCItem ifcItem = new IFCItem();
                        ifcItem._ifcType = strIfcParentEntityName;
                        ifcItem._visible = IsVisible(ifcItem._ifcType);
                        ifcItem.circleSegments = iCircleSegments;
                        ifcItem._instance = ifcObjectIns;

                        RetrieveGeometry(ifcItem, iCircleSegments);

                        _dicIFCItems.Add(_dicIFCItems.Count + 1, ifcItem);
                    }
                }
            } // if (noIfcObjectIntances != 0)

            noIfcObjectIntances = IfcEngine.x64.engiGetEntityCount(_model);
            for (Int64 i = 0; i < noIfcObjectIntances; i++)
            {
                Int64 ifcEntity = IfcEngine.x64.engiGetEntityElement(_model, i);
                if (IfcEngine.x64.engiGetEntityParent(ifcEntity) == iParentEntity)
                {
                    RetrieveObjects_depth(ifcEntity, iCircleSegments, depth + 1);
                }
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ifcInstance"></param>
        private void RetrieveGeometry(IFCItem ifcItem, Int64 iCircleSegments)
        {
            if (!ifcItem._visible)
            {
                return;
            }

            // -----------------------------------------------------------------------------------------------
            /*
             *  Faces
             */
            Int64 setting = 0, mask = 0;
            mask += IfcEngine.x64.flagbit2;        // PRECISION (32/64 bit)
            mask += IfcEngine.x64.flagbit3;        // INDEX ARRAY (32/64 bit)
            mask += IfcEngine.x64.flagbit5;        // NORMALS
            mask += IfcEngine.x64.flagbit8;        // TRIANGLES
            mask += IfcEngine.x64.flagbit12;       // WIREFRAME
            mask += IfcEngine.x64.flagbit8;        // TRIANGLES
            mask += IfcEngine.x64.flagbit9;        // LINES
            mask += IfcEngine.x64.flagbit10;       // POINTS            
            mask += IfcEngine.x64.flagbit24;       // AMBIENT
            mask += IfcEngine.x64.flagbit25;       // DIFFUSE
            mask += IfcEngine.x64.flagbit26;       // EMISSIVE
            mask += IfcEngine.x64.flagbit27;       // SPECULAR

            setting += 0;                          // SINGLE PRECISION (float)
            setting += 0;                          // 32 BIT INDEX ARRAY (Int32)
            setting += IfcEngine.x64.flagbit5;     // NORMALS ON
            setting += IfcEngine.x64.flagbit8;     // TRIANGLES ON
            setting += IfcEngine.x64.flagbit9;     // LINES ON
            setting += IfcEngine.x64.flagbit10;    // POINTS ON
            setting += IfcEngine.x64.flagbit12;    // WIREFRAME ON
            setting += IfcEngine.x64.flagbit24;    // AMBIENT
            setting += IfcEngine.x64.flagbit25;    // DIFFUSE
            setting += IfcEngine.x64.flagbit26;    // EMISSIVE
            setting += IfcEngine.x64.flagbit27;    // SPECULAR
            IfcEngine.x64.setFormat(_model, setting, mask);

            /*
            * Set up circleSegments()
            */
            if (iCircleSegments != IFCItem.DEFAULT_CIRCLE_SEGMENTS)
            {
                IfcEngine.x64.circleSegments(iCircleSegments, 5);
            }

            Debug.WriteLine("CalculateInstance()... ");

            Int32 noVertices = 0, noIndices = 0;

            Int64 iVertexBufferSize = 0;
            Int64 iIndexBufferSize = 0;
            Int64 iTransformationBufferSize = 0;
            IfcEngine.x64.CalculateInstance(ifcItem._instance, ref iVertexBufferSize, ref iIndexBufferSize, ref iTransformationBufferSize);

            noVertices = (Int32)iVertexBufferSize;
            noIndices = (Int32)iIndexBufferSize;

            if (noVertices != 0 && noIndices != 0)
            {
                if (noIndices > 65000)
                {
                    Debug.WriteLine("Found > 65000 indices: " + noIndices);

                    return;
                }

                Debug.WriteLine("Retrieve Geometry BEGIN... ");

                ifcItem._vertices = new float[noVertices * 10];
                IfcEngine.x64.UpdateInstanceVertexBuffer(ifcItem._instance, ifcItem._vertices);

                int[] indices = new int[noIndices];
                IfcEngine.x64.UpdateInstanceIndexBuffer(ifcItem._instance, indices);

                Int64 iFacesIndicesCount = 0;
                List<int> lsFacesPolygonsIndices = new List<int>();
                List<int> lsLinesIndices = new List<int>();
                List<int> lsPointsIndices = new List<int>();

                Int64 iFacesCount = IfcEngine.x64.getConceptualFaceCnt(ifcItem._instance);
                for (Int32 iFace = 0; iFace < iFacesCount; iFace++)
                {
                    Int64 iStartIndexTriangles = 0;
                    Int64 iIndicesCountTriangles = 0;

                    Int64 iStartIndexLines = 0;
                    Int64 iIndicesCountLines = 0;

                    Int64 iStartIndexPoints = 0;
                    Int64 iIndicesCountPoints = 0;

                    Int64 iStartIndexFacesPolygons = 0;
                    Int64 iIndicesCountFacesPolygons = 0;

                    Int64 iValue = 0;
                    IfcEngine.x64.getConceptualFaceEx(ifcItem._instance,
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

                    /*
                     * Faces
                     */
                    iFacesIndicesCount += iIndicesCountTriangles;

                    /*
                    * Materials
                    */
                    if (iIndicesCountTriangles > 0)
                    {
                        int iIndexValue = indices[iStartIndexTriangles];
                        iIndexValue *= 10;

                        float ambient = ifcItem._vertices[iIndexValue + 6];
                        float diffuse = ifcItem._vertices[iIndexValue + 7];
                        float emissive = ifcItem._vertices[iIndexValue + 8];
                        float specular = ifcItem._vertices[iIndexValue + 9];

                        IFCMaterial material = new IFCMaterial(ambient, diffuse, emissive, specular);

                        if (ifcItem._materials.ContainsKey(material))
                        {
                            ifcItem._materials[material].Add(new KeyValuePair<Int64, Int64>(iStartIndexTriangles, iIndicesCountTriangles));
                        }
                        else
                        {
                            List<KeyValuePair<Int64, Int64>> triangles = new List<KeyValuePair<Int64, Int64>>();
                            triangles.Add(new KeyValuePair<Int64, Int64>(iStartIndexTriangles, iIndicesCountTriangles));

                            ifcItem._materials.Add(material, triangles);
                        }
                    } // if (iIndicesCountTriangles > 0)

                    /*
                     * Conceptual faces polygons
                     */
                    Int32 iIndexWireframes = 0;
                    int iLastIndex = -1;
                    while (iIndexWireframes < iIndicesCountFacesPolygons)
                    {
                        if ((iLastIndex >= 0) && (indices[iStartIndexFacesPolygons + iIndexWireframes] >= 0))
                        {
                            lsFacesPolygonsIndices.Add(iLastIndex);

                            lsFacesPolygonsIndices.Add(indices[iStartIndexFacesPolygons + iIndexWireframes]);
                        }

                        iLastIndex = indices[iStartIndexFacesPolygons + iIndexWireframes];
                        iIndexWireframes++;
                    }

                    /*
                     * Lines
                     */
                    for (Int64 iIndex = iStartIndexLines; iIndex < iStartIndexLines + iIndicesCountLines; iIndex++)
                    {
                        if (indices[iIndex] < 0)
                        {
                            continue;
                        }

                        lsLinesIndices.Add(indices[iIndex]);
                    }

                    /*
                     * Points
                     */
                    for (Int64 iIndex = iStartIndexLines; iIndex < iStartIndexPoints + iIndicesCountPoints; iIndex++)
                    {
                        lsPointsIndices.Add(indices[iIndex]);
                    }
                } // for (Int32 iFace ...

                Debug.WriteLine("Retrieve Geometry END... ");

                /*
                * Group the conceptual faces
                */
                List<int> lsFacesIndices = new List<int>();
                Int64 iMaterialIndicesOffset = 0;
                foreach (var materialInfo in ifcItem._materials)
                {
                    Int64 iMaterialIndicesCount = 0;
                    foreach (var triangles in materialInfo.Value)
                    {
                        for (Int64 iIndex = triangles.Key; iIndex < triangles.Key + triangles.Value; iIndex++)
                        {
                            lsFacesIndices.Add(indices[iIndex]);
                        }

                        iMaterialIndicesCount += triangles.Value;
                    } // for (var triangles in ...

                    materialInfo.Key.IndicesOffset = (int)iMaterialIndicesOffset;
                    materialInfo.Key.IndicesCount = (int)iMaterialIndicesCount;

                    iMaterialIndicesOffset += iMaterialIndicesCount;
                } // for (var material in ...

                /*
                * Copy
                */
                if (lsFacesIndices.Count <= 65000)
                {
                    ifcItem._facesIndices = lsFacesIndices.ToArray();
                }
                else
                {
                    Debug.WriteLine("Found > 65000 indices for the faces: " + lsFacesIndices.Count);
                }

                /*
                * Copy
                */
                if (lsFacesPolygonsIndices.Count <= 65000)
                {
                    ifcItem._facesPolygonsIndices = lsFacesPolygonsIndices.ToArray();
                }
                else
                {
                    Debug.WriteLine("Found > 65000 indices for the faces polygons: " + lsFacesPolygonsIndices.Count);
                }

                /*
                * Copy
                */
                if (lsLinesIndices.Count <= 65000)
                {
                    ifcItem._linesIndices = lsLinesIndices.ToArray();
                }
                else
                {
                    Debug.WriteLine("Found > 65000 indices for the lines: " + lsLinesIndices.Count);
                }

                /*
                * Copy
                */
                if (lsPointsIndices.Count <= 65000)
                {
                    ifcItem._pointsIndices = lsPointsIndices.ToArray();
                }
                else
                {
                    Debug.WriteLine("Found > 65000 indices for the points: " + lsPointsIndices.Count);
                }
            } // if (noVertices != 0 && noIndices != 0)

            Debug.WriteLine("Materials count: " + ifcItem._materials.Count);

            if (iCircleSegments != IFCItem.DEFAULT_CIRCLE_SEGMENTS)
            {
                IfcEngine.x64.circleSegments(IFCItem.DEFAULT_CIRCLE_SEGMENTS, 5);
            }

            IfcEngine.x64.cleanMemory(_model, 0);
        }

        /// <summary>
        /// Min/Max; Scale/Center
        /// </summary>
        private void PostProcessing()
        {
            if (_dicIFCItems.Count == 0)
            {
                return;
            }

            /*
             * Calculate min/max
             */
            _vecMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            _vecMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            foreach (var ifcItem in _dicIFCItems.Values)
            {
                /*
                * Check for geometry
                */
                if (ifcItem._vertices == null)
                {
                    continue;
                }

                /*
                * Faces, Conceptual faces polygons, lines and points
                */
                for (int i = 0; i < ifcItem._vertices.Length; i += 10)
                {
                    _vecMin.X = Math.Min(_vecMin.X, ifcItem._vertices[i]);
                    _vecMin.Y = Math.Min(_vecMin.Y, ifcItem._vertices[i + 1]);
                    _vecMin.Z = Math.Min(_vecMin.Z, ifcItem._vertices[i + 2]);

                    _vecMax.X = Math.Max(_vecMax.X, ifcItem._vertices[i]);
                    _vecMax.Y = Math.Max(_vecMax.Y, ifcItem._vertices[i + 1]);
                    _vecMax.Z = Math.Max(_vecMax.Z, ifcItem._vertices[i + 2]);
                }
            } //  foreach (var ifcItem in ...

            float fMax = _vecMax.X - _vecMin.X;
            fMax = Math.Max(fMax, _vecMax.Y - _vecMin.Y);
            fMax = Math.Max(fMax, _vecMax.Z - _vecMin.Z);

            /*
             * Center and scale
             */
            foreach (var ifcItem in _dicIFCItems.Values)
            {
                /*
                * Check for geometry
                */
                if (ifcItem._vertices == null)
                {
                    continue;
                }

                /*
                * Faces, Conceptual faces polygons, lines and points
                */
                for (int i = 0; i < ifcItem._vertices.Length; i += 10)
                {
                    // Move => [0.0 -> X/Y/Zmin + X/Y/Zmax]
                    ifcItem._vertices[i] -= _vecMin.X;
                    ifcItem._vertices[i + 1] -= _vecMin.Y;
                    ifcItem._vertices[i + 2] -= _vecMin.Z;

                    // Center
                    ifcItem._vertices[i] -= (_vecMax.X - _vecMin.X) / 2f;
                    ifcItem._vertices[i + 1] -= (_vecMax.Y - _vecMin.Y) / 2f;
                    ifcItem._vertices[i + 2] -= (_vecMax.Z - _vecMin.Z) / 2f;

                    // Scale => [-1.0 -> 1.0]
                    ifcItem._vertices[i] /= fMax / 2f;
                    ifcItem._vertices[i + 1] /= fMax / 2f;
                    ifcItem._vertices[i + 2] /= fMax / 2f;
                } // for (int i = ...
            } // foreach (var ifcObject in ...
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static T[] SubArray<T>(T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);

            return result;
        }

        /// <summary>
        /// Show/hide items
        /// </summary>
        /// <param name="strIfcType"></param>
        /// <returns></returns>
        private bool IsVisible(string strIfcType)
        {
            if ((strIfcType.ToUpper() == "IFCSPACE") ||
                (strIfcType.ToUpper() == "IFCRELSPACEBOUNDARY") ||
                (strIfcType.ToUpper() == "IFCOPENINGELEMENT"))
            {
                return false;
            }

            //if ((strIfcType.ToUpper() == "IFCFURNISHINGELEMENT") ||
            //    (strIfcType.ToUpper() == "IFCWALL") ||
            //    (strIfcType.ToUpper() == "IFCWALLSTANDARDCASE") ||
            //    (strIfcType.ToUpper() == "IFCWINDOW") ||
            //    (strIfcType.ToUpper() == "IFCDOOR") ||
            //    (strIfcType.ToUpper() == "IFCROOF"))
            //{
            //    return true;
            //}

            //return false;

            return true;
        }

        private void AddChildrenItems(Int64 iParentInstance, string strEntityName)
        {
            Debug.WriteLine("XR start of AddChildrenItems, strEntityName: " + strEntityName);

            // check for decomposition
            IntPtr decompositionInstance;
            IfcEngine.x64.sdaiGetAttrBN(iParentInstance, "IsDecomposedBy", IfcEngine.x64.sdaiAGGR, out decompositionInstance);

            if (decompositionInstance == IntPtr.Zero)
            {
                return;
            }

            Int64 iDecompositionsCount = IfcEngine.x64.sdaiGetMemberCount((Int64)decompositionInstance);
            if (iDecompositionsCount == 0)
            {
                return;
            }

            for (Int64 i = 0; i < iDecompositionsCount; i++)
            {
                Int64 iDecompositionInstance = 0;
                IfcEngine.x64.engiGetAggrElement((Int64)decompositionInstance, i, IfcEngine.x64.sdaiINSTANCE, out iDecompositionInstance);

                if (!IsInstanceOf(iDecompositionInstance, "IFCRELAGGREGATES"))
                {
                    Debug.WriteLine("XR iDecompositionInstance not instance of IFCRELAGGREGATES");
                    continue;
                }

                IntPtr objectInstances;
                IfcEngine.x64.sdaiGetAttrBN(iDecompositionInstance, "RelatedObjects", IfcEngine.x64.sdaiAGGR, out objectInstances);

                Int64 iObjectsCount = IfcEngine.x64.sdaiGetMemberCount((Int64)objectInstances);
                for (Int64 iObject = 0; iObject < iObjectsCount; iObject++)
                {
                    Int64 iObjectInstance = 0;
                    IfcEngine.x64.engiGetAggrElement((Int64)objectInstances, iObject, IfcEngine.x64.sdaiINSTANCE, out iObjectInstance);

                    if (!IsInstanceOf(iObjectInstance, strEntityName))
                    {
                        Debug.WriteLine("XR iObjectInstance not instance of strEntityName");
                        continue;
                    }

                    switch (strEntityName)
                    {
                        case "IfcSite":
                            {
                                Debug.WriteLine("IfcSite");
                                AddChildrenItems(iObjectInstance, "IfcBuilding");
                            }
                            break;

                        case "IfcBuilding":
                            {
                                Debug.WriteLine("IfcBuilding");
                                AddChildrenItems(iObjectInstance, "IfcBuildingStorey");
                            }
                            break;

                        case "IfcBuildingStorey":
                            {
                                Debug.WriteLine("IfcBuilding");
                                // Å¸ÀÔº°·Î ¾ÆÀÌÅÛµéÀ» ºÐ·ù, ÀúÀå
                                /*
                                Dictionary<string, List<IFCObject>> storeyTreeByType =
                                    new Dictionary<string, List<IFCObject>>();
                                AddElementTreeItems2(iObjectInstance, ref storeyTreeByType);

                                storeyByTypesList.Add(storeyTreeByType);

                                Debug.WriteLine("XR storeyByTypesList.Add storeyByTypesList.Count: " + storeyByTypesList.Count);
                                */
                            }
                            break;

                        case "IfcSpace":
                            {
                                Debug.WriteLine("IfcSpace");
                                //AddElementTreeItems(ifcTreeItem, iObjectInstance,	parentNode);
                            }
                            break;

                        default:
                            Debug.WriteLine("IfcSite");
                            break;
                    } // switch (strEntityName)
                } // for (Int64 iObject = ...
            } // for (Int64 iDecomposition = ...
        }

        private bool IsInstanceOf(Int64 iInstance, string strType)
        {
            if (IfcEngine.x64.sdaiGetInstanceType(iInstance) == IfcEngine.x64.sdaiGetEntity(_model, strType))
            {
                return true;
            }

            return false;
        }

        private string LoadResource(string name)
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            var resources = assembly.GetManifestResourceNames();
            var resourceName = resources.Single(r => r.EndsWith(name, StringComparison.OrdinalIgnoreCase));
            var stream = assembly.GetManifestResourceStream(resourceName);

            try
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    var content = reader.ReadToEnd();

                    return content;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);

                throw ex;
            }
        }
    }
}
