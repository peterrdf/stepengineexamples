using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using IfcEngine;

public class STRUCT_COLOR
{
    public float R;
    public float G;
    public float B;
    public float A;
};

public class STRUCT_MATERIAL
{
    public UInt32 ambientColor;
    public UInt32 diffuseColor;
    public UInt32 emissiveColor;
    public UInt32 specularColor;

    public float[] myArrayForFaces;
    public uint[] ids;

    public IFCViewerSGL.IFCItem firstIfcItem;

    public bool active;

    public STRUCT_COLOR ambient;
    public STRUCT_COLOR diffuse;
    public STRUCT_COLOR specular;
    public STRUCT_COLOR emissive;

    public double transparency;
    public double shininess;

    //public Material? MTRL = null;

    public STRUCT_MATERIAL next;
    public STRUCT_MATERIAL prev;
};

public class STRUCT_MATERIALS
{
    public long __indexArrayOffset;
    public long __noPrimitivesForFaces;

    public long __indexOffsetForFaces;
    public long __indexBufferSize;

    public float[] myArray;
    public uint[] ids;
    public STRUCT_MATERIAL material;

    public IFCViewerSGL.IFCItem nextSameMaterialIfcItem;
    public STRUCT_MATERIALS next;
};

public class STRUCT_MATERIAL_META_INFO
{
    public bool isPoint;
    public bool isEdge;
    public bool isShell;

    public long expressID;
    public STRUCT_MATERIAL material;

    public STRUCT_MATERIAL_META_INFO child;
    public STRUCT_MATERIAL_META_INFO next;
};

namespace IFCViewerSGL
{
    public class IFCMaterialsBuilder
    {
        #region Members

        private readonly long _rdfClassTransformation;
        private readonly long _rdfClassCollection;
        private readonly long _owlDataTypePropertyExpressID;
        private readonly long _owlObjectTypePropertyMatrix; 
        private readonly long _owlObjectTypePropertyObject;
        private readonly long _owlObjectTypePropertyObjects;

        /// <summary>
        /// Model
        /// </summary>
        private long _ifcModel = 0;

        /// <summary>
        /// Counter
        /// </summary>
        private long _iTotalAllocatedMaterials = 0;

        public STRUCT_MATERIAL _firstMaterial = null;
        private STRUCT_MATERIAL _lastMaterial = null;
        private STRUCT_MATERIAL _defaultMaterial = null;
        private STRUCT_MATERIALS _materialsRoot = null;

        #endregion // Members

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="ifcModel"></param>
        public IFCMaterialsBuilder(long ifcModel)
        {
            if (ifcModel == 0)
            {
                throw new ArgumentException();
            }

            _ifcModel = ifcModel;

            _defaultMaterial = newMaterial();

            _defaultMaterial.ambient.R = 0;
            _defaultMaterial.ambient.G = 1f;
            _defaultMaterial.ambient.B = 0;
            _defaultMaterial.ambient.A = 0;
            _defaultMaterial.diffuse.R = 0;
            _defaultMaterial.diffuse.G = 0.8f;
            _defaultMaterial.diffuse.B = 0;
            _defaultMaterial.diffuse.A = 0;
            _defaultMaterial.specular.R = 0;
            _defaultMaterial.specular.G = 0.8f;
            _defaultMaterial.specular.B = 0;
            _defaultMaterial.specular.A = 0;
            _defaultMaterial.emissive.R = 0;
            _defaultMaterial.emissive.G = 0.8f;
            _defaultMaterial.emissive.B = 0;
            _defaultMaterial.emissive.A = 0;

            _defaultMaterial.transparency = 1;
            _defaultMaterial.shininess = 1;

            _defaultMaterial.active = true;

#if _WIN64
            _rdfClassTransformation = IfcEngineAnyCPU.GetClassByName(_ifcModel, "Transformation");
            _rdfClassCollection = IfcEngineAnyCPU.GetClassByName(_ifcModel, "Collection");
            _owlDataTypePropertyExpressID = IfcEngineAnyCPU.GetPropertyByName(_ifcModel, "expressID");
            _owlObjectTypePropertyMatrix = IfcEngineAnyCPU.GetPropertyByName(_ifcModel, "matrix");
            _owlObjectTypePropertyObject = IfcEngineAnyCPU.GetPropertyByName(_ifcModel, "object");
            _owlObjectTypePropertyObjects = IfcEngineAnyCPU.GetPropertyByName(_ifcModel, "objects");
#else
            _rdfClassTransformation = (int)IfcEngineAnyCPU.GetClassByName(_ifcModel, "Transformation");
            _rdfClassCollection = (int)IfcEngineAnyCPU.GetClassByName(_ifcModel, "Collection");
            _owlDataTypePropertyExpressID = (int)IfcEngineAnyCPU.GetPropertyByName(_ifcModel, "expressID");
            _owlObjectTypePropertyMatrix = (int)IfcEngineAnyCPU.GetPropertyByName(_ifcModel, "matrix");
            _owlObjectTypePropertyObject = (int)IfcEngineAnyCPU.GetPropertyByName(_ifcModel, "object");
            _owlObjectTypePropertyObjects = (int)IfcEngineAnyCPU.GetPropertyByName(_ifcModel, "objects");
#endif            
        }

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="ifcInstance"></param>
        public STRUCT_MATERIALS extractMaterials(IFCItem ifcItem, float[] vertices, int[] indices)
        {
            int index = ifcItem._facesIndices[0];

            UInt32 ambient = BitConverter.ToUInt32(BitConverter.GetBytes(vertices[(index * 10) + 6]), 0),
                   diffuse = BitConverter.ToUInt32(BitConverter.GetBytes(vertices[(index * 10) + 7]), 0),
                   emissive = BitConverter.ToUInt32(BitConverter.GetBytes(vertices[(index * 10) + 8]), 0),
                   specular = BitConverter.ToUInt32(BitConverter.GetBytes(vertices[(index * 10) + 9]), 0);

			STRUCT_MATERIALS materials = new_STRUCT_MATERIALS(newMaterial(ambient, diffuse, specular, emissive));
			ifcItem._materials = materials;

            for (int k = 0; k < ifcItem._noPrimitivesForFaces; k++) 
            {
                index = ifcItem._facesIndices[3 * k];

                UInt32 currentAmbient = BitConverter.ToUInt32(BitConverter.GetBytes(vertices[(index * 10) + 6]), 0),
                       currentDiffuse = BitConverter.ToUInt32(BitConverter.GetBytes(vertices[(index * 10) + 7]), 0),
                       currentEmissive = BitConverter.ToUInt32(BitConverter.GetBytes(vertices[(index * 10) + 8]), 0),
                       currentSpecular = BitConverter.ToUInt32(BitConverter.GetBytes(vertices[(index * 10) + 9]), 0);

                if ((ambient  == currentAmbient) &&
                    (diffuse  == currentDiffuse) &&
                    (emissive == currentEmissive) &&
                    (specular == currentSpecular)) 
                {
                    materials.__noPrimitivesForFaces++;
                }
                else 
                {
                    ambient = currentAmbient;
                    diffuse = currentDiffuse;
                    emissive = currentEmissive;
                    specular = currentSpecular;

                    materials.next = new_STRUCT_MATERIALS(newMaterial(ambient, diffuse, specular, emissive));
                    materials.next.__indexArrayOffset = materials.__indexArrayOffset + (3 * materials.__noPrimitivesForFaces);
                    materials = materials.next;

                    materials.__noPrimitivesForFaces = 1;
                }
            } // for (int k = ...

            return materials;
        }

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="ifcInstance"></param>
        //public STRUCT_MATERIALS extractMaterials(long ifcInstance)
        //{
        //    if (ifcInstance == 0)
        //    {
        //        throw new ArgumentException();
        //    }

        //    STRUCT_MATERIAL_META_INFO materialMetaInfo = null;

        //    IntPtr ifcProductRepresentationInstance;
        //    IfcEngineAnyCPU.sdaiGetAttrBN(ifcInstance, "Representation", IfcEngineAnyCPU.sdaiINSTANCE, out ifcProductRepresentationInstance);

        //    if (ifcProductRepresentationInstance != IntPtr.Zero)
        //    {
        //        getRGB_productDefinitionShape((long)ifcProductRepresentationInstance, ref materialMetaInfo);
        //    }

        //    bool noMaterialFound = false;
        //    if ((materialMetaInfo != null) && (materialMetaInfo.next == null) && (materialMetaInfo.child == null))
        //    {
        //        if ((materialMetaInfo.material.ambient.A == -1) &&
        //              (materialMetaInfo.material.diffuse.A == -1) &&
        //              (materialMetaInfo.material.emissive.A == -1) &&
        //              (materialMetaInfo.material.specular.A == -1))
        //        {
        //            noMaterialFound = true;
        //        }
        //    }

        //    if (noMaterialFound)
        //    {
        //        long ifcRelAssociatesMaterialEntity = IfcEngineAnyCPU.sdaiGetEntity(_ifcModel, "IFCRELASSOCIATESMATERIAL");								
        //        long ifcRelAssociatesAggr;
        //        IfcEngineAnyCPU.sdaiGetAttrBN(ifcInstance, "HasAssociations", IfcEngineAnyCPU.sdaiAGGR, out ifcRelAssociatesAggr);

        //        long ifcRelAssociatesAggrCnt = IfcEngineAnyCPU.sdaiGetMemberCount(ifcRelAssociatesAggr);

        //        long i = 0;
        //        while  (i < ifcRelAssociatesAggrCnt) {
        //            long	ifcRelAssociatesInstance;
        //            IfcEngineAnyCPU.engiGetAggrElement(ifcRelAssociatesAggr, i, IfcEngineAnyCPU.sdaiINSTANCE, out ifcRelAssociatesInstance);

        //            if (IfcEngineAnyCPU.sdaiGetInstanceType(ifcRelAssociatesInstance) == ifcRelAssociatesMaterialEntity)
        //            {
        //                getRGB_relAssociatesMaterial(_ifcModel, ifcRelAssociatesInstance, ref materialMetaInfo.material);
        //            }
        //            i++;
        //        }
        //    }

        //    if (materialMetaInfo != null)
        //    {
        //        bool bUnique = true;
        //        bool bDefaultColorIsUsed = false;
        //        STRUCT_MATERIAL returnedMaterial = null;
        //        minimizeMaterialItems(materialMetaInfo, ref returnedMaterial, ref bUnique, ref bDefaultColorIsUsed);

        //        if (!bUnique)
        //        {
        //            returnedMaterial = null;
        //        }

        //        if (returnedMaterial != null)
        //        {
        //            return new_STRUCT_MATERIALS(returnedMaterial, ifcInstance);
        //        }
        //    } // if (materialMetaInfo != null)

        //    _materialsRoot = null;
        //    if (materialMetaInfo != null)
        //    {
        //        long setting = 0;
        //        long mask = 0;
        //        mask += IfcEngineAnyCPU.flagbit12;   //    WIREFRAME
        //        setting += 0;                         //    WIREFRAME OFF
        //        IfcEngineAnyCPU.setFormat(_ifcModel, setting, mask);
                
        //        STRUCT_MATERIALS materials = null;
        //        walkThroughGeometry__transformation(ifcInstance, ref materials, ref materialMetaInfo);
        //    }

        //    if (_materialsRoot != null)
        //    {
        //        return _materialsRoot;
        //    } 
        //    else 
        //    {
        //        return new_STRUCT_MATERIALS(_firstMaterial, ifcInstance);
        //    }
        //}

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="owlInstance"></param>
        /// <param name="materials"></param>
        /// <param name="materialMetaInfo"></param>
        private void walkThroughGeometry__transformation(long owlInstance, ref STRUCT_MATERIALS materials, ref STRUCT_MATERIAL_META_INFO materialMetaInfo)
        {
            System.Diagnostics.Debug.Assert(IfcEngineAnyCPU.GetInstanceClass(owlInstance) == _rdfClassTransformation);

            IntPtr owlInstanceObjectsPtr;
            Int64 iObjectCards = 0;
            IfcEngineAnyCPU.GetObjectTypeProperty(owlInstance, _owlObjectTypePropertyObject, out owlInstanceObjectsPtr, ref iObjectCards);

            if (iObjectCards == 1)
            {
                Int64[] owlInstanceObjects = new Int64[iObjectCards];
                Marshal.Copy(owlInstanceObjectsPtr, owlInstanceObjects, 0, (int)iObjectCards);
#if _WIN64
                walkThroughGeometry__collection(owlInstanceObjects[0], ref materials, ref materialMetaInfo);
#else
                walkThroughGeometry__collection((int)owlInstanceObjects[0], ref materials, ref materialMetaInfo);
#endif
            }
            else
            {
                System.Diagnostics.Debug.Assert(false);
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="owlInstance"></param>
        /// <param name="materials"></param>
        /// <param name="materialMetaInfo"></param>
        private void walkThroughGeometry__collection(long owlInstance, ref STRUCT_MATERIALS materials, ref STRUCT_MATERIAL_META_INFO materialMetaInfo)
        {
            if (IfcEngineAnyCPU.GetInstanceClass(owlInstance) == _rdfClassCollection)
            {
                IntPtr owlInstanceObjectsPtr;
                Int64 iObjectCards = 0;
                IfcEngineAnyCPU.GetObjectTypeProperty(owlInstance, _owlObjectTypePropertyObjects, out owlInstanceObjectsPtr, ref iObjectCards);

                if (iObjectCards > 0)
                {
                    Int64[] owlInstanceObjects = new Int64[iObjectCards];
                    Marshal.Copy(owlInstanceObjectsPtr, owlInstanceObjects, 0, (int)iObjectCards);

                    for (long i = 0; i < iObjectCards; i++)
                    {
#if _WIN64
                        walkThroughGeometry__object(owlInstanceObjects[i], ref materials, materialMetaInfo);
#else
                        walkThroughGeometry__object((int)owlInstanceObjects[i], ref materials, materialMetaInfo);
#endif
                    }
                }                
            }
            else
            {
                walkThroughGeometry__object(owlInstance, ref materials, materialMetaInfo);
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="owlInstance"></param>
        /// <param name="materials"></param>
        /// <param name="materialMetaInfo"></param>
        private void walkThroughGeometry__object(long owlInstance, ref STRUCT_MATERIALS materials, STRUCT_MATERIAL_META_INFO materialMetaInfo)
        {
            STRUCT_MATERIAL_META_INFO materialMetaInfoIterator = materialMetaInfo;

            IntPtr owlInstanceExpressIDPtr;
            Int64 expressIDCard = 0;
            IfcEngineAnyCPU.GetDataTypeProperty(owlInstance, _owlDataTypePropertyExpressID, out owlInstanceExpressIDPtr, ref expressIDCard);
            if (expressIDCard == 1)
            {
                Int64[] owlInstanceExpressID = new Int64[expressIDCard];
                Marshal.Copy(owlInstanceExpressIDPtr, owlInstanceExpressID, 0, (int)expressIDCard);

#if _WIN64
                long expressID = owlInstanceExpressID[0];
#else
                long expressID = (int)owlInstanceExpressID[0];
#endif
                
                while ((materialMetaInfoIterator != null) && (materialMetaInfoIterator.expressID != expressID))
                {
                    materialMetaInfoIterator = materialMetaInfoIterator.next;
                }

                if (materialMetaInfoIterator != null)
                {
                    System.Diagnostics.Debug.Assert(materialMetaInfoIterator.expressID == expressID);

                    if (materialMetaInfoIterator.child != null)
                    {
                        if (IfcEngineAnyCPU.GetInstanceClass(owlInstance) == _rdfClassTransformation)
                        {
                            IntPtr owlInstanceObjectPtr;
                            Int64 objectCard = 0;
                            IfcEngineAnyCPU.GetObjectTypeProperty(owlInstance, _owlObjectTypePropertyObject, out owlInstanceObjectPtr, ref objectCard);

                            if (objectCard == 1)
                            {
                                Int64[] owlInstanceObject = new Int64[objectCard];
                                Marshal.Copy(owlInstanceObjectPtr, owlInstanceObject, 0, (int)objectCard);
#if _WIN64
                                walkThroughGeometry__object(owlInstanceObject[0], ref materials, materialMetaInfoIterator.child);
#else
                                walkThroughGeometry__object((int)owlInstanceObject[0], ref materials, materialMetaInfoIterator.child);
#endif
                            }
                            else
                            {
                                System.Diagnostics.Debug.Assert(false);
                            }
                        }
                        else if (IfcEngineAnyCPU.GetInstanceClass(owlInstance) == _rdfClassCollection)
                        {
                            IntPtr owlInstanceObjectsPtr;
                            Int64 objectsCard = 0;
                            IfcEngineAnyCPU.GetObjectTypeProperty(owlInstance, _owlObjectTypePropertyObjects, out owlInstanceObjectsPtr, ref objectsCard);

                            Int64[] owlInstanceObjects = new Int64[objectsCard];
                            Marshal.Copy(owlInstanceObjectsPtr, owlInstanceObjects, 0, (int)objectsCard);

                            for (long i = 0; i < objectsCard; i++)
                            {
#if _WIN64
                                walkThroughGeometry__object(owlInstanceObjects[i], ref materials, materialMetaInfoIterator.child);
#else
                                walkThroughGeometry__object((int)owlInstanceObjects[i], ref materials, materialMetaInfoIterator.child);
#endif
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.Assert(false);
                        }
                    } // if (materialMetaInfoIterator.child != null)
                    else
                    {
                        //
                        //	Now recreate the geometry for this object
                        //
                        Int64 vertexBufferSize = 0;
                        Int64 indexBufferSize = 0;
                        Int64 transformationBufferSize = 0;
                        IfcEngineAnyCPU.CalculateInstance(owlInstance, ref vertexBufferSize, ref indexBufferSize, ref transformationBufferSize);

                        if (materials != null)
                        {
                            materials.next = new_STRUCT_MATERIALS(materialMetaInfoIterator.material);
                            materials.next.__indexBufferSize = (long)indexBufferSize;

                            materials = materials.next;                            
                        }
                        else
                        {
                            materials = new_STRUCT_MATERIALS(materialMetaInfoIterator.material);
                            materials.__indexBufferSize = (long)indexBufferSize;
                        }       
                 
                        if (_materialsRoot == null)
                        {
                            _materialsRoot = materials;
                        }
                    }

                    materialMetaInfoIterator = materialMetaInfoIterator.next;
                } // if (materialMetaInfoIterator != null)
                else
                {
                    System.Diagnostics.Debug.Assert(false);
                }
            }
            else
            {
                if (IfcEngineAnyCPU.GetInstanceClass(owlInstance) == _rdfClassTransformation)
                {
                    IntPtr owlInstanceObjectPtr;
                    Int64 objectCard = 0;
                    IfcEngineAnyCPU.GetObjectTypeProperty(owlInstance, _owlObjectTypePropertyObject, out owlInstanceObjectPtr, ref objectCard);

                    if (objectCard == 1)
                    {
                        Int64[] owlInstanceObject = new Int64[objectCard];
                        Marshal.Copy(owlInstanceObjectPtr, owlInstanceObject, 0, (int)objectCard);
#if _WIN64
                        walkThroughGeometry__object(owlInstanceObject[0], ref materials, materialMetaInfoIterator);
#else
                        walkThroughGeometry__object((int)owlInstanceObject[0], ref materials, materialMetaInfoIterator);
#endif
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                }
                else if (IfcEngineAnyCPU.GetInstanceClass(owlInstance) == _rdfClassCollection)
                {
                    IntPtr owlInstanceObjectsPtr;
                    Int64 objectsCard = 0;
                    IfcEngineAnyCPU.GetObjectTypeProperty(owlInstance, _owlObjectTypePropertyObjects, out owlInstanceObjectsPtr, ref objectsCard);

                    Int64[] owlInstanceObjects = new Int64[objectsCard];
                    Marshal.Copy(owlInstanceObjectsPtr, owlInstanceObjects, 0, (int)objectsCard);

                    long i = 0;
                    while (i < objectsCard)
                    {
#if _WIN64
                        walkThroughGeometry__object(owlInstanceObjects[i], ref materials, materialMetaInfoIterator);
#else
                        walkThroughGeometry__object((int)owlInstanceObjects[i], ref materials, materialMetaInfoIterator);
#endif
                        i++;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false);
                }
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="materialMetaInfo"></param>
        /// <param name="ppMaterial"></param>
        /// <param name="bUnique"></param>
        /// <param name="bDefaultColorIsUsed"></param>
        private void minimizeMaterialItems(STRUCT_MATERIAL_META_INFO materialMetaInfo, ref STRUCT_MATERIAL ppMaterial, ref bool bUnique, ref bool bDefaultColorIsUsed)
        {
            while (materialMetaInfo != null)
            {
                //
                //	Check nested child object (i.e. Mapped Items)
                //
                if (materialMetaInfo.child != null)
                {
                    System.Diagnostics.Debug.Assert(materialMetaInfo.material.ambient.R == -1);
                    System.Diagnostics.Debug.Assert(materialMetaInfo.material.active == false);

                    deleteMaterial(materialMetaInfo.material);

                    materialMetaInfo.material = null;
                    minimizeMaterialItems(materialMetaInfo.child, ref ppMaterial, ref bUnique, ref bDefaultColorIsUsed);
                }

                //
                //	Complete Color
                //
                STRUCT_MATERIAL material = materialMetaInfo.material;
                if (material != null)
                {
                    if (material.ambient.R == -1)
                    {
                        materialMetaInfo.material = _defaultMaterial;
                        deleteMaterial(material);
                    }
                    else
                    {
                        if (material.diffuse.R == -1)
                        {
                            material.diffuse.R = material.ambient.R;
                            material.diffuse.G = material.ambient.G;
                            material.diffuse.B = material.ambient.B;
                            material.diffuse.A = material.ambient.A;
                        }
                        if (material.specular.R == -1)
                        {
                            material.specular.R = material.ambient.R;
                            material.specular.G = material.ambient.G;
                            material.specular.B = material.ambient.B;
                            material.specular.A = material.ambient.A;
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.Assert(materialMetaInfo.child != null);
                }

                //
                //	Merge the same colors
                //
                material = materialMetaInfo.material;
                if ((material != null) && (material != _defaultMaterial))
                {
                    System.Diagnostics.Debug.Assert(material.active == false);
                    System.Diagnostics.Debug.Assert(_firstMaterial == _defaultMaterial);

                    bool bAdjusted = false;
                    STRUCT_MATERIAL materialLoop = _firstMaterial.next;                                        

                    while (materialLoop != null)
                    {
                        if ((materialLoop.active) &&
                              (material.transparency == materialLoop.transparency) &&
                              (material.ambient.R == materialLoop.ambient.R) &&
                              (material.ambient.G == materialLoop.ambient.G) &&
                              (material.ambient.B == materialLoop.ambient.B) &&
                              (material.ambient.A == materialLoop.ambient.A) &&
                              (material.diffuse.R == materialLoop.diffuse.R) &&
                              (material.diffuse.G == materialLoop.diffuse.G) &&
                              (material.diffuse.B == materialLoop.diffuse.B) &&
                              (material.diffuse.A == materialLoop.diffuse.A) &&
                              (material.specular.R == materialLoop.specular.R) &&
                              (material.specular.G == materialLoop.specular.G) &&
                              (material.specular.B == materialLoop.specular.B) &&
                              (material.specular.A == materialLoop.specular.A))
                        {
                            materialMetaInfo.material = materialLoop;

                            deleteMaterial(material);

                            materialLoop = null;
                            bAdjusted = true;
                        }
                        else
                        {
                            if (materialLoop.active == false)
                            {
                                while (materialLoop != null)
                                {
                                    System.Diagnostics.Debug.Assert(materialLoop.active == false);

                                    materialLoop = materialLoop.next;
                                }

                                materialLoop = null;
                            }
                            else
                            {
                                materialLoop = materialLoop.next;
                            }
                        }
                    } // while (materialLoop != null)

                    if (bAdjusted)
                    {
                        System.Diagnostics.Debug.Assert(materialMetaInfo.material.active);
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(materialMetaInfo.material.active == false);
                        materialMetaInfo.material.active = true;
                    }

                    System.Diagnostics.Debug.Assert((materialLoop == null) || ((materialLoop == _defaultMaterial) && (materialLoop.next == null)));
                }

                //
                //	Assign default color in case of no color and no children
                //
                if ((materialMetaInfo.material == null) && (materialMetaInfo.child == null))
                {
                    materialMetaInfo.material = _defaultMaterial;
                }

                //
                //	Check if unique
                //
                material = materialMetaInfo.material;
                if (ppMaterial != null)
                {
                    if (ppMaterial != material)
                    {
                        if ((material == null) && (materialMetaInfo.child != null))
                        {
                            // NA
                        }
                        else
                        {
                            bUnique = false;
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.Assert(bUnique);

                    ppMaterial = material;
                }

                if (material == _defaultMaterial)
                {
                    bDefaultColorIsUsed = true;
                }

                materialMetaInfo = materialMetaInfo.next;
            } // while (materialMetaInfo != null)
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="material"></param>
        private void deleteMaterial(STRUCT_MATERIAL material)
        {
	        _iTotalAllocatedMaterials--;

	        System.Diagnostics.Debug.Assert(material.active == false);

            STRUCT_MATERIAL prev = material.prev;
            STRUCT_MATERIAL next = material.next;

	        if	(prev == null) 
            {
		        System.Diagnostics.Debug.Assert(_firstMaterial == next.prev);
	        }

            if (next == null)
            {
		        System.Diagnostics.Debug.Assert(_lastMaterial == prev.next);
	        }

            if (prev != null)
            {
		        System.Diagnostics.Debug.Assert(prev.next == material);
		        prev.next = next;
	        } 
            else 
            {
		        System.Diagnostics.Debug.Assert(_firstMaterial == material);

		        _firstMaterial = next;

		        next.prev = null;
	        }

            if (next != null)
            {
		        System.Diagnostics.Debug.Assert(next.prev == material);

		        next.prev = prev;

	        } 
            else 
            {
		        System.Diagnostics.Debug.Assert(_lastMaterial == material);

		        _lastMaterial = prev;

		        System.Diagnostics.Debug.Assert(prev.next == null);
	        }

	        material.active = false;
        }

        private void getRGB_relAssociatesMaterial(long model, long ifcRelAssociatesMaterialInstance, ref STRUCT_MATERIAL material)
        {
            long ifcMaterialEntity = IfcEngineAnyCPU.sdaiGetEntity(_ifcModel, "IFCMATERIAL"),
                    ifcMaterialLayerSetUsageEntity = IfcEngineAnyCPU.sdaiGetEntity(_ifcModel, "IFCMATERIALLAYERSETUSAGE"),
                    ifcMaterialLayerSetEntity = IfcEngineAnyCPU.sdaiGetEntity(_ifcModel, "IFCMATERIALLAYERSET"),
                    ifcMaterialLayerEntity = IfcEngineAnyCPU.sdaiGetEntity(_ifcModel, "IFCMATERIALLAYER"),
			        ifcMaterialSelectInstance = 0;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcRelAssociatesMaterialInstance, "RelatingMaterial", IfcEngineAnyCPU.sdaiINSTANCE, out ifcMaterialSelectInstance);

            if (IfcEngineAnyCPU.sdaiGetInstanceType(ifcMaterialSelectInstance) == ifcMaterialEntity)
            {
                getRGB_ifcMaterial(_ifcModel, ifcMaterialSelectInstance, ref material);
            }
            else if (IfcEngineAnyCPU.sdaiGetInstanceType(ifcMaterialSelectInstance) == ifcMaterialLayerSetUsageEntity)
            {
                getRGB_ifcMaterialLayerSetUsage(_ifcModel, ifcMaterialSelectInstance, ref material);
            }
            else if (IfcEngineAnyCPU.sdaiGetInstanceType(ifcMaterialSelectInstance) == ifcMaterialLayerSetEntity)
            {
                getRGB_ifcMaterialLayerSet(_ifcModel, ifcMaterialSelectInstance, ref material);
            }
            else if (IfcEngineAnyCPU.sdaiGetInstanceType(ifcMaterialSelectInstance) == ifcMaterialLayerEntity)
            {
                getRGB_ifcMaterialLayer(_ifcModel, ifcMaterialSelectInstance, ref material);
	        }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ifcMaterialLayerSet"></param>
        /// <param name="material"></param>
        void getRGB_ifcMaterialLayerSet(long model, long ifcMaterialLayerSet, ref STRUCT_MATERIAL material)
        {
            long ifcMaterialLayerAggr;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcMaterialLayerSet, "MaterialLayers", IfcEngineAnyCPU.sdaiAGGR, out ifcMaterialLayerAggr);

            long ifcMaterialLayerAggrCnt = IfcEngineAnyCPU.sdaiGetMemberCount(ifcMaterialLayerAggr);
	        if	(ifcMaterialLayerAggrCnt > 0) {
                long ifcMaterialLayer;
                IfcEngineAnyCPU.engiGetAggrElement((long)ifcMaterialLayerAggr, 0, IfcEngineAnyCPU.sdaiINSTANCE, out ifcMaterialLayer);

                if (IfcEngineAnyCPU.sdaiGetInstanceType(ifcMaterialLayer) == IfcEngineAnyCPU.sdaiGetEntity(model, "IFCMATERIALLAYER"))
                {
			        getRGB_ifcMaterialLayer(model, ifcMaterialLayer, ref material);
		        } else {
			        Debug.Assert(false);
		        }
	        }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ifcMaterialLayer"></param>
        /// <param name="material"></param>
        void getRGB_ifcMaterialLayer(long model, long ifcMaterialLayer, ref STRUCT_MATERIAL material)
        {
            long ifcMaterialInstance;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcMaterialLayer, "Material", IfcEngineAnyCPU.sdaiINSTANCE, out ifcMaterialInstance);

            Debug.Assert(IfcEngineAnyCPU.sdaiGetInstanceType(ifcMaterialInstance) == IfcEngineAnyCPU.sdaiGetEntity(model, "IFCMATERIAL"));

	        getRGB_ifcMaterial(model, ifcMaterialInstance, ref material);
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ifcMaterialLayerSetUsage"></param>
        /// <param name="material"></param>
        void getRGB_ifcMaterialLayerSetUsage(long model, long ifcMaterialLayerSetUsage, ref STRUCT_MATERIAL material)
        {
            long ifcMaterialLayerSetInstance;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcMaterialLayerSetUsage, "ForLayerSet", IfcEngineAnyCPU.sdaiINSTANCE, out ifcMaterialLayerSetInstance);

            Debug.Assert(IfcEngineAnyCPU.sdaiGetInstanceType(ifcMaterialLayerSetInstance) == IfcEngineAnyCPU.sdaiGetEntity(model, "IFCMATERIALLAYERSET"));

	        getRGB_ifcMaterialLayerSet(model, ifcMaterialLayerSetInstance, ref material);
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ifcMaterialInstance"></param>
        /// <param name="material"></param>
        void getRGB_ifcMaterial(long model, long ifcMaterialInstance, ref STRUCT_MATERIAL material)
        {
            long ifcMaterialDefinitionRepresentationAggr;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcMaterialInstance, "HasRepresentation", IfcEngineAnyCPU.sdaiAGGR, out ifcMaterialDefinitionRepresentationAggr);

	        long ifcMaterialDefinitionRepresentationAggrCnt = IfcEngineAnyCPU.sdaiGetMemberCount((long)ifcMaterialDefinitionRepresentationAggr);

            long  i = 0;
	        while  (i < ifcMaterialDefinitionRepresentationAggrCnt) {
		        long	ifcMaterialDefinitionRepresentationInstance = 0;
                IfcEngineAnyCPU.engiGetAggrElement((long)ifcMaterialDefinitionRepresentationAggr, i, IfcEngineAnyCPU.sdaiINSTANCE, out ifcMaterialDefinitionRepresentationInstance);

                if (IfcEngineAnyCPU.sdaiGetInstanceType(ifcMaterialDefinitionRepresentationInstance) == IfcEngineAnyCPU.sdaiGetEntity(model, "IFCMATERIALDEFINITIONREPRESENTATION"))
                {
			        getRGB_ifcMaterialDefinitionRepresentation(_ifcModel, ifcMaterialDefinitionRepresentationInstance, ref material);
		        } else {
			        Debug.Assert(false);
		        }
		        i++;
	        }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ifcMaterialDefinitionRepresentationInstance"></param>
        /// <param name="material"></param>
        void getRGB_ifcMaterialDefinitionRepresentation(long model, long ifcMaterialDefinitionRepresentationInstance, ref STRUCT_MATERIAL material)
        {
            long ifcRepresentationAggr;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcMaterialDefinitionRepresentationInstance, "Representations", IfcEngineAnyCPU.sdaiAGGR, out ifcRepresentationAggr);

            long ifcRepresentationAggrCnt = IfcEngineAnyCPU.sdaiGetMemberCount((long)ifcRepresentationAggr);

            long i = 0;
	        while  (i < ifcRepresentationAggrCnt) {
		        long	ifcRepresentationInstance;
                IfcEngineAnyCPU.engiGetAggrElement((long)ifcRepresentationAggr, i, IfcEngineAnyCPU.sdaiINSTANCE, out ifcRepresentationInstance);

                if (IfcEngineAnyCPU.sdaiGetInstanceType(ifcRepresentationInstance) == IfcEngineAnyCPU.sdaiGetEntity(model, "IFCSTYLEDREPRESENTATION"))
                {
			        getRGB_ifcStyledRepresentation(model, ifcRepresentationInstance, ref material);
		        }
		        i++;
	        }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ifcStyledRepresentationInstance"></param>
        /// <param name="material"></param>
        void getRGB_ifcStyledRepresentation(long model, long ifcStyledRepresentationInstance, ref STRUCT_MATERIAL material)
        {
            long ifcRepresentationItemAggr;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcStyledRepresentationInstance, "Items", IfcEngineAnyCPU.sdaiAGGR, out ifcRepresentationItemAggr);

	        long ifcRepresentationItemAggrCnt = IfcEngineAnyCPU.sdaiGetMemberCount((long)ifcRepresentationItemAggr);
            
            long i = 0;
	        while  (i < ifcRepresentationItemAggrCnt) {
		        long ifcRepresentationItemInstance;
                IfcEngineAnyCPU.engiGetAggrElement((long)ifcRepresentationItemAggr, i, IfcEngineAnyCPU.sdaiINSTANCE, out ifcRepresentationItemInstance);

		        if	(IfcEngineAnyCPU.sdaiGetInstanceType(ifcRepresentationItemInstance) == IfcEngineAnyCPU.sdaiGetEntity(model, "IFCSTYLEDITEM")) {
			        getRGB_styledItem(ifcRepresentationItemInstance, material);
		        }
		        i++;
	        }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="ifcObjectInstance"></param>
        /// <param name="materialMetaInfo"></param>
        private void getRGB_productDefinitionShape(long ifcObjectInstance, ref STRUCT_MATERIAL_META_INFO materialMetaInfo)
        {
            long representationsSet;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcObjectInstance, "Representations", IfcEngineAnyCPU.sdaiAGGR, out representationsSet);

            long iRepresentationsCount = IfcEngineAnyCPU.sdaiGetMemberCount((long)representationsSet);
            for (long iRepresentation = 0; iRepresentation < iRepresentationsCount; iRepresentation++)
            {
                long ifcShapeRepresentation;
                IfcEngineAnyCPU.engiGetAggrElement((long)representationsSet, iRepresentation, IfcEngineAnyCPU.sdaiINSTANCE, out ifcShapeRepresentation);

                if (ifcShapeRepresentation != 0)
                {
                    getRGB_shapeRepresentation(ifcShapeRepresentation, ref materialMetaInfo);
                }
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="ifcShapeRepresentationInstance"></param>
        /// <param name="materialMetaInfo"></param>
        private void getRGB_shapeRepresentation(long ifcShapeRepresentationInstance, ref STRUCT_MATERIAL_META_INFO materialMetaInfo)
        {
            IntPtr representationIdentifier;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcShapeRepresentationInstance, "RepresentationIdentifier", IfcEngineAnyCPU.sdaiUNICODE, out representationIdentifier);

            string strRepresentationIdentifier = Marshal.PtrToStringUni(representationIdentifier);

            IntPtr representationType;
            IfcEngineAnyCPU.sdaiGetAttrBN(ifcShapeRepresentationInstance, "RepresentationType", IfcEngineAnyCPU.sdaiUNICODE, out representationType);

            string strRepresentationType = Marshal.PtrToStringUni(representationType);

            if (((strRepresentationIdentifier == "Body") || (strRepresentationIdentifier == "Mesh") || (strRepresentationIdentifier == "Facetation")) &&
                (strRepresentationType != "BoundingBox"))
            {
                long geometrySet;
                IfcEngineAnyCPU.sdaiGetAttrBN(ifcShapeRepresentationInstance, "Items", IfcEngineAnyCPU.sdaiAGGR, out geometrySet);

                STRUCT_MATERIAL_META_INFO materialMetaInfoIterator = materialMetaInfo;

                long iGeometryCount = IfcEngineAnyCPU.sdaiGetMemberCount((long)geometrySet);
                for (long iGeometry = 0; iGeometry < iGeometryCount; iGeometry++)
                {
                    long iGeometryInstance = 0;
                    IfcEngineAnyCPU.engiGetAggrElement((long)geometrySet, iGeometry, IfcEngineAnyCPU.sdaiINSTANCE, out iGeometryInstance);

                    if (materialMetaInfoIterator == null)
                    {
                        materialMetaInfoIterator = materialMetaInfo = newMaterialMetaInfo(iGeometryInstance);
                    }
                    else
                    {
                        materialMetaInfoIterator = materialMetaInfoIterator.next = newMaterialMetaInfo(iGeometryInstance);
                    }

                    IntPtr styledItemInstance;
                    IfcEngineAnyCPU.sdaiGetAttrBN(iGeometryInstance, "StyledByItem", IfcEngineAnyCPU.sdaiINSTANCE, out styledItemInstance);

                    if (styledItemInstance != IntPtr.Zero)
                    {
                        getRGB_styledItem((long)styledItemInstance, materialMetaInfoIterator.material);
                    }
                    else
                    {
                        searchDeeper(iGeometryInstance, ref materialMetaInfoIterator, materialMetaInfoIterator.material);
                    } // else if (iItemInstance != 0)
                } // for (int iGeometry = ...
            }
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="iGeometryInstance"></param>
        /// <param name="materialMetaInfo"></param>
        /// <param name="material"></param>
        private void searchDeeper(long iGeometryInstance, ref STRUCT_MATERIAL_META_INFO materialMetaInfo, STRUCT_MATERIAL material)
        {
            IntPtr styledItemInstance;
            IfcEngineAnyCPU.sdaiGetAttrBN(iGeometryInstance, "StyledByItem", IfcEngineAnyCPU.sdaiINSTANCE, out styledItemInstance);

            if (styledItemInstance != IntPtr.Zero)
            {
                getRGB_styledItem((long)styledItemInstance, materialMetaInfo.material);

                if (material.ambient.R >= 0)
                {
                    return;
                }
            }

            long booleanClippingResultEntity = IfcEngineAnyCPU.sdaiGetEntity(_ifcModel, "IFCBOOLEANCLIPPINGRESULT");
            long booleanResultEntity = IfcEngineAnyCPU.sdaiGetEntity(_ifcModel, "IFCBOOLEANRESULT");
			long mappedItemEntity = IfcEngineAnyCPU.sdaiGetEntity(_ifcModel, "IFCMAPPEDITEM");

            if (IsInstanceOf(iGeometryInstance, "IFCBOOLEANRESULT") || IsInstanceOf(iGeometryInstance, "IFCBOOLEANCLIPPINGRESULT"))
            {
                IntPtr geometryChildInstance;
                IfcEngineAnyCPU.sdaiGetAttrBN(iGeometryInstance, "FirstOperand", IfcEngineAnyCPU.sdaiINSTANCE, out geometryChildInstance);

                if (geometryChildInstance != IntPtr.Zero)
                {
                    searchDeeper((long)geometryChildInstance, ref materialMetaInfo, material);
                }
            } // if (IsInstanceOf(iGeometryInstance, "IFCBOOLEANRESULT") || ...
            else
            {
                if (IsInstanceOf(iGeometryInstance, "IFCMAPPEDITEM"))
                {
                    IntPtr representationMapInstance;
                    IfcEngineAnyCPU.sdaiGetAttrBN(iGeometryInstance, "MappingSource", IfcEngineAnyCPU.sdaiINSTANCE, out representationMapInstance);

                    IntPtr shapeRepresentationInstance;
                    IfcEngineAnyCPU.sdaiGetAttrBN((long)representationMapInstance, "MappedRepresentation", IfcEngineAnyCPU.sdaiINSTANCE, out shapeRepresentationInstance);

                    if (shapeRepresentationInstance != IntPtr.Zero)
                    {
                        System.Diagnostics.Debug.Assert(materialMetaInfo.child == null);

                        getRGB_shapeRepresentation((long)shapeRepresentationInstance, ref materialMetaInfo.child);
                    }
                } // if (IsInstanceOf(iParentInstance, "IFCMAPPEDITEM"))
                else
                {
                    if (IsInstanceOf(iGeometryInstance, "IFCSHELLBASEDSURFACEMODEL"))
                    {
                        IntPtr geometryChildAggr;
                        IfcEngineAnyCPU.sdaiGetAttrBN(iGeometryInstance, "SbsmBoundary", IfcEngineAnyCPU.sdaiAGGR, out geometryChildAggr);

                        STRUCT_MATERIAL_META_INFO materialMetaInfoIterator = materialMetaInfo;

                        long iGeometryChildAggrCount = IfcEngineAnyCPU.sdaiGetMemberCount((long)geometryChildAggr);
                        for (int iGeometryChildAggr = 0; iGeometryChildAggr < iGeometryChildAggrCount; iGeometryChildAggr++)
                        {
                            long iGeometryChildInstance = 0;
                            IfcEngineAnyCPU.engiGetAggrElement((long)geometryChildAggr, iGeometryChildAggr, IfcEngineAnyCPU.sdaiINSTANCE, out iGeometryChildInstance);

                            if (iGeometryChildInstance != 0)
                            {
                                if (materialMetaInfoIterator == null)
                                {
                                    materialMetaInfoIterator = materialMetaInfo.child = newMaterialMetaInfo(iGeometryChildInstance);
                                }
                                else
                                {
                                    materialMetaInfoIterator = materialMetaInfoIterator.next = newMaterialMetaInfo(iGeometryChildInstance);
                                }

                                searchDeeper(iGeometryChildInstance, ref materialMetaInfo, materialMetaInfo.material);
                            }
                        } // for (int iGeometryChildAggr = ...
                    } // if (IsInstanceOf(iGeometryInstance, "IFCSHELLBASEDSURFACEMODEL"))
                } // else if (IsInstanceOf(iGeometryInstance, "IFCMAPPEDITEM"))
            } // else if (IsInstanceOf(iGeometryInstance, "IFCBOOLEANRESULT") || ...
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="iStyledItemInstance"></param>
        /// <param name="material"></param>
        private void getRGB_styledItem(long iStyledItemInstance, STRUCT_MATERIAL material)
        {
            IntPtr stylesSet;
            IfcEngineAnyCPU.sdaiGetAttrBN(iStyledItemInstance, "Styles", IfcEngineAnyCPU.sdaiAGGR, out stylesSet);

            long iStylesCount = IfcEngineAnyCPU.sdaiGetMemberCount((long)stylesSet);
            for (long iStyle = 0; iStyle < iStylesCount; iStyle++)
            {
                long iPresentationStyleAssignmentInstance = 0;
                IfcEngineAnyCPU.engiGetAggrElement((long)stylesSet, iStyle, IfcEngineAnyCPU.sdaiINSTANCE, out iPresentationStyleAssignmentInstance);

                if (iPresentationStyleAssignmentInstance != 0)
                {
                    getRGB_presentationStyleAssignment(iPresentationStyleAssignmentInstance, ref material);
                }                
            } // for (int iStyle = ...
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="iPresentationStyleAssignmentInstance"></param>
        /// <param name="material"></param>
        private void getRGB_presentationStyleAssignment(long iPresentationStyleAssignmentInstance, ref STRUCT_MATERIAL material)
        {
            IntPtr stylesSet;
            IfcEngineAnyCPU.sdaiGetAttrBN(iPresentationStyleAssignmentInstance, "Styles", IfcEngineAnyCPU.sdaiAGGR, out stylesSet);

            long iStylesCount = IfcEngineAnyCPU.sdaiGetMemberCount((long)stylesSet);
            for (long iStyle = 0; iStyle < iStylesCount; iStyle++)
            {
                long iSurfaceStyleInstance = 0;
                IfcEngineAnyCPU.engiGetAggrElement((long)stylesSet, iStyle, IfcEngineAnyCPU.sdaiINSTANCE, out iSurfaceStyleInstance);

                if (iSurfaceStyleInstance != 0)
                {
                    getRGB_surfaceStyle(iSurfaceStyleInstance, ref material);
                }                
            } // for (int iStyle = ...
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="iSurfaceStyleInstance"></param>
        /// <param name="material"></param>
        private void getRGB_surfaceStyle(long iSurfaceStyleInstance, ref STRUCT_MATERIAL material)
        {
            IntPtr stylesSet;
            IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceStyleInstance, "Styles", IfcEngineAnyCPU.sdaiAGGR, out stylesSet);

            long iStylesCount = IfcEngineAnyCPU.sdaiGetMemberCount((long)stylesSet);
            for (long iStyle = 0; iStyle < iStylesCount; iStyle++)
            {
                long iSurfaceStyleRenderingInstance = 0;
                IfcEngineAnyCPU.engiGetAggrElement((long)stylesSet, iStyle, IfcEngineAnyCPU.sdaiINSTANCE, out iSurfaceStyleRenderingInstance);

                double	dTransparency = 0;
                IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceStyleRenderingInstance, "Transparency", IfcEngineAnyCPU.sdaiREAL, out dTransparency);
		        material.transparency = 1.0 - dTransparency;

                IntPtr surfaceColourInstance;
                IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceStyleRenderingInstance, "SurfaceColour", IfcEngineAnyCPU.sdaiINSTANCE, out surfaceColourInstance);

                if (surfaceColourInstance != IntPtr.Zero)
                {
                    getRGB_colourRGB((long)surfaceColourInstance, material.ambient);
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false);
                }

                IntPtr diffuseColourInstance;
                IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceStyleRenderingInstance, "DiffuseColour", IfcEngineAnyCPU.sdaiINSTANCE, out diffuseColourInstance);

                if (diffuseColourInstance != IntPtr.Zero)
                {
                    getRGB_colourRGB((long)diffuseColourInstance, material.diffuse);
                }
                else
                {
                    long iADB = 0;
                    IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceStyleRenderingInstance, "DiffuseColour", IfcEngineAnyCPU.sdaiADB, out iADB);

                    if (iADB != 0)
                    {
                        double dValue;
                        IfcEngineAnyCPU.sdaiGetADBValue(iADB, IfcEngineAnyCPU.sdaiREAL, out dValue);

                        material.diffuse.R = (float)dValue * material.ambient.R;
                        material.diffuse.G = (float)dValue * material.ambient.G;
                        material.diffuse.B = (float)dValue * material.ambient.B;
                    }
                }

                IntPtr specularColourInstance;
                IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceStyleRenderingInstance, "SpecularColour", IfcEngineAnyCPU.sdaiINSTANCE, out specularColourInstance);

                if (specularColourInstance != IntPtr.Zero)
                {
                    getRGB_colourRGB((long)specularColourInstance, material.specular);
                }
                else
                {
                    long iADB = 0;
                    IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceStyleRenderingInstance, "SpecularColour", IfcEngineAnyCPU.sdaiADB, out iADB);

                    if (iADB != 0)
                    {
                        double dValue;
                        IfcEngineAnyCPU.sdaiGetADBValue(iADB, IfcEngineAnyCPU.sdaiREAL, out dValue);

                        material.specular.R = (float)dValue * material.ambient.R;
                        material.specular.G = (float)dValue * material.ambient.G;
                        material.specular.B = (float)dValue * material.ambient.B;
                    }
                }
            } // for (int iStyle = ...
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="iSurfaceColourInstance"></param>
        /// <param name="color"></param>
        private void getRGB_colourRGB(long iSurfaceColourInstance, STRUCT_COLOR color)
        {
            double	dRed = 0, dGreen = 0, dBlue = 0;
            IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceColourInstance, "Red", IfcEngineAnyCPU.sdaiREAL, out dRed);
            IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceColourInstance, "Green", IfcEngineAnyCPU.sdaiREAL, out dGreen);
            IfcEngineAnyCPU.sdaiGetAttrBN(iSurfaceColourInstance, "Blue", IfcEngineAnyCPU.sdaiREAL, out dBlue);
                     
            color.R = (float)dRed;
            color.G = (float)dGreen;
            color.B = (float)dBlue;
        }        

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="iInstance"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        private bool IsInstanceOf(long iInstance, string strType)
        {
            if (IfcEngineAnyCPU.sdaiGetInstanceType(iInstance) == IfcEngineAnyCPU.sdaiGetEntity(_ifcModel, strType))
            {
                return true;
            }

            return false;
        }

        private STRUCT_MATERIAL newMaterial(UInt32 ambientColor, UInt32 diffuseColor, UInt32 specularColor, UInt32 emissiveColor)
        {
            STRUCT_MATERIAL	material = _firstMaterial;
	        while (material != null) {
		        if ((material.ambientColor == ambientColor) &&
			        (material.diffuseColor == diffuseColor) &&
			        (material.emissiveColor == emissiveColor) &&
			        (material.specularColor == specularColor)) {
			        return  material;
		        }
		        material = material.next;
	        }

	        material = new STRUCT_MATERIAL();

            material.ambientColor = ambientColor;
            material.diffuseColor = diffuseColor;
            material.emissiveColor = emissiveColor;
            material.specularColor = specularColor;            

            _iTotalAllocatedMaterials++;

            material.ambient = CreateColor(ambientColor);
            material.diffuse = CreateColor(diffuseColor);
            material.specular = CreateColor(specularColor);
            material.emissive = CreateColor(emissiveColor);
            material.emissive.R /= 2;
            material.emissive.G /= 2;
            material.emissive.B /= 2;

            material.transparency = -1;
            material.shininess = -1;

            material.next = null;
            material.prev = _lastMaterial;

            if (_firstMaterial == null)
            {
                _firstMaterial = material;
            }

            _lastMaterial = material;

            if (_lastMaterial.prev != null)
            {
                _lastMaterial.prev.next = _lastMaterial;
            }

            material.active = false;

            return material;
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="iColor"></param>
        /// <returns></returns>
        private STRUCT_COLOR CreateColor(UInt32 iColor)
        {
            float R = (float)(iColor & ((UInt32)255 * 256 * 256 * 256)) / (256 * 256 * 256);
            R /= 255f;

            float G = (float)(iColor & (255 * 256 * 256)) / (256 * 256);
            G /= 255f;

            float B = (float)(iColor & (255 * 256)) / 256;
            B /= 255f;

            float A = (float)(iColor & (255));
            A /= 255f;

            STRUCT_COLOR color = new STRUCT_COLOR();
            color.R = R;
            color.G = G;
            color.B = B;
            color.A = A;

            return color;
        }

        /// <summary>
        /// Factory
        /// </summary>
        /// <returns></returns>
        private STRUCT_MATERIAL newMaterial()
        {
            _iTotalAllocatedMaterials++;

	        STRUCT_MATERIAL	material = new STRUCT_MATERIAL();

            material.ambient = new STRUCT_COLOR();
	        material.ambient.R = -1;
	        material.ambient.G = -1;
	        material.ambient.B = -1;
	        material.ambient.A = -1;

            material.diffuse = new STRUCT_COLOR();
	        material.diffuse.R = -1;
	        material.diffuse.G = -1;
	        material.diffuse.B = -1;
	        material.diffuse.A = -1;

            material.specular = new STRUCT_COLOR();
	        material.specular.R = -1;
	        material.specular.G = -1;
	        material.specular.B = -1;
	        material.specular.A = -1;

            material.emissive = new STRUCT_COLOR();
	        material.emissive.R = -1;
	        material.emissive.G = -1;
	        material.emissive.B = -1;
	        material.emissive.A = -1;

	        material.transparency = -1;
	        material.shininess = -1;

	        material.next = null;
	        material.prev = _lastMaterial;

	        _lastMaterial = material;

	        if	(_lastMaterial.prev != null) 
            {
		        _lastMaterial.prev.next = _lastMaterial;
	        }

	        material.active = false;

	        return	material;
        }

        /// <summary>
        /// Factory
        /// </summary>
        /// <param name="ifcInstance"></param>
        /// <returns></returns>
        private STRUCT_MATERIAL_META_INFO newMaterialMetaInfo(long ifcInstance)
        {
	        STRUCT_MATERIAL_META_INFO materialMetaInfo = new STRUCT_MATERIAL_META_INFO();

	        if	(ifcInstance != 0) 
            {
                materialMetaInfo.expressID = IfcEngineAnyCPU.internalGetP21Line(ifcInstance);
	        } 
            else 
            {
		        materialMetaInfo.expressID = -1;
	        }

	        materialMetaInfo.isPoint = false;
	        materialMetaInfo.isEdge = false;
	        materialMetaInfo.isShell = false;
	
	        materialMetaInfo.material = newMaterial();

	        materialMetaInfo.child = null;
	        materialMetaInfo.next = null;

	        return	materialMetaInfo;
        }

        /// <summary>
        /// Factory
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        private STRUCT_MATERIALS new_STRUCT_MATERIALS(STRUCT_MATERIAL material)
        {
	        STRUCT_MATERIALS materials = new STRUCT_MATERIALS();

            materials.__indexOffsetForFaces = 0;
            materials.__indexArrayOffset = 0;

            materials.__noPrimitivesForFaces = 0;
            materials.__indexBufferSize = 0;

            materials.myArray = null;

	        materials.material = material;
	        materials.next = null;

	        return	materials;
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <param name="material"></param>
        /// <param name="ifcInstance"></param>
        /// <returns></returns>
        private STRUCT_MATERIALS new_STRUCT_MATERIALS(STRUCT_MATERIAL material, long ifcInstance)
        {
            STRUCT_MATERIALS materials = new_STRUCT_MATERIALS(material);

            materials.__noPrimitivesForFaces = 0;
            materials.__indexBufferSize = -1;

            return materials;
        }
    }
}
