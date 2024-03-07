package com.android.gl2jni;

import android.util.Log;
import android.util.Pair;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * IFC Model
 */
public class IFCModel {

    // X, Y, Z, Nx, Ny, Nz, Ambient, Diffuse, Emissive, Specular
    public static final long VERTEX_LENGTH = 10;

    public static final long MAX_INDICES_COUNT = 65000;

    private static final long flagbit0 = 1; // 2^^0 0000.0000..0000.0001
    private static final long flagbit1 = 2; // 2^^1 0000.0000..0000.0010
    private static final long flagbit2 = 4; // 2^^2 0000.0000..0000.0100
    private static final long flagbit3 = 8; // 2^^3 0000.0000..0000.1000
    private static final long flagbit4 = 16; // 2^^4 0000.0000..0001.0000
    private static final long flagbit5 = 32; // 2^^5 0000.0000..0010.0000
    private static final long flagbit6 = 64; // 2^^6 0000.0000..0100.0000
    private static final long flagbit7 = 128; // 2^^7 0000.0000..1000.0000
    private static final long flagbit8 = 256; // 2^^8 0000.0001..0000.0000
    private static final long flagbit9 = 512; // 2^^9 0000.0010..0000.0000
    private static final long flagbit10 = 1024; // 2^^10 0000.0100..0000.0000
    private static final long flagbit11 = 2048; // 2^^11 0000.1000..0000.0000
    private static final long flagbit12 = 4096; // 2^^12 0001.0000..0000.0000
    private static final long flagbit13 = 8192; // 2^^13 0010.0000..0000.0000
    private static final long flagbit14 = 16384; // 2^^14 0100.0000..0000.0000
    private static final long flagbit15 = 32768; // 2^^15 1000.0000..0000.0000
    private static final long flagbit17 = 131072; // 2^^15 1000.0000..0000.0000
    private static final long flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
    private static final long flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
    private static final long flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000
    private static final long flagbit23 = 8388608;		// 2^^23   0000.0000..1000.0000  0000.0000..0000.0000
    private static final long flagbit24 = 16777216;
    private static final long flagbit25 = 33554432;
    private static final long flagbit26 = 67108864;
    private static final long flagbit27 = 134217728;

    private static final String TAG = "IFCViewerAndroidJ";
    private static final long DEFAULT_CIRCLE_SEGMENTS = 36;
    private String mIFCFile = "";
    private long mModel = 0L;
    List<IFCObject> mIFCObjects = new ArrayList<>();
    private long mIfcProjectEntity = 0L;
    private long mIfcSpaceEntity = 0L;
    private long mIfcOpeningElementEntity = 0L;
    private long mIfcDistributionElementEntity = 0L;
    private long mIfcElectricalElementEntity = 0L;
    private long mIfcElementAssemblyEntity = 0L;
    private long mIfcElementComponentEntity = 0L;
    private long mIfcEquipmentElementEntity = 0L;
    private long mIfcFeatureElementEntity = 0L;
    private long mIfcFeatureElementSubtractionEntity = 0L;
    private long mIfcFurnishingElementEntity = 0L;
    private long mIfcReinforcingElementEntity = 0L;
    private long mIfcTransportElementEntity = 0L;
    private long mIfcVirtualElementEntity = 0L;

    /**
     * ctor
     */
    public IFCModel() {
    }

    /**
     * Load
     * @param ifcFile
     * @return
     */
    public boolean loadModel(String ifcFile) {
        mModel = IFCEngineLib.sdaiOpenModelBN(ifcFile, null);

        if (mModel == 0) {
            Log.e(TAG, "Can't load '" + ifcFile + "'");
            return false;
        }

        Log.v(TAG, "sdaiOpenModelBN() => " + mModel);

        /*
         * Detect the schema
         */
        String schema = IFCEngineLib.GetSPFFHeaderItem(mModel, 9, 0);
        Log.v(TAG, schema);

        /*
         * Close the model
         */
        IFCEngineLib.sdaiCloseModel(mModel);
        mModel = 0;

        /*
         * Use embedded schema
         */
        if (schema.indexOf("IFC2") != -1)
        {
            IFCEngineLib.setFilter(0, 2097152, 1048576 + 2097152 + 4194304);
        }
        else
        {
            if ((schema.indexOf("IFC4x") != -1) || (schema.indexOf("IFC4X") != -1))
            {
                IFCEngineLib.setFilter(0, 1048576 + 2097152, 1048576 + 2097152 + 4194304);
            }
            else
            {
                if ((schema.indexOf("IFC4") != -1) ||
                    (schema.indexOf("IFC2x4") != -1) ||
                    (schema.indexOf("IFC2X4") != -1))
                {
                    IFCEngineLib.setFilter(0, 1048576, 1048576 + 2097152 + 4194304);
                }
                else
                {
                    Log.e(TAG, "Unknown schema.");

                    return false;
                }
            }
        }

        mModel = IFCEngineLib.sdaiOpenModelBN(ifcFile, "");

        if (mModel == 0) {
            Log.e(TAG, "Can't load '" + ifcFile + "'");
            return false;
        }

        Log.v(TAG, "sdaiOpenModelBN() => " + mModel);

        mIFCFile = ifcFile;

        long ifcObjectEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCOBJECT");
        long ifcProductEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCPRODUCT");
        mIfcProjectEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCPROJECT");
        mIfcSpaceEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCSPACE");
        mIfcOpeningElementEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCOPENINGELEMENT");
        mIfcDistributionElementEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCDISTRIBUTIONELEMENT");
        mIfcElectricalElementEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCELECTRICALELEMENT");
        mIfcElementAssemblyEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCELEMENTASSEMBLY");
        mIfcElementComponentEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCELEMENTCOMPONENT");
        mIfcEquipmentElementEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCEQUIPMENTELEMENT");
        mIfcFeatureElementEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCFEATUREELEMENT");
        mIfcFeatureElementSubtractionEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCFEATUREELEMENTSUBTRACTION");
        mIfcFurnishingElementEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCFURNISHINGELEMENT");
        mIfcReinforcingElementEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCREINFORCINGELEMENT");
        mIfcTransportElementEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCTRANSPORTELEMENT");
        mIfcVirtualElementEntity = IFCEngineLib.sdaiGetEntity(mModel, "IFCVIRTUALELEMENT");

        retrieveObjects_depth(ifcObjectEntity, DEFAULT_CIRCLE_SEGMENTS, 0);
        retrieveObjects("IFCRELSPACEBOUNDARY", DEFAULT_CIRCLE_SEGMENTS);

        Log.v(TAG, "IFC Object: " + getIFCObjectsCount());

        /*
        Properties
         */
        LoadHeader();
        LoadProjects();
        LoadUnreferenced();

        return true;
    }

    /**
     * Close
     */
    public void closeModel() {
        if (mModel != 0) {
            IFCEngineLib.sdaiCloseModel(mModel);
            mModel = 0;

            Log.v(TAG, "sdaiCloseModel() => '" + mIFCFile + "'");
        }
    }

    /**
     * Getter
     * @return
     */
    public List<IFCObject> getIFCObjects() {
        return mIFCObjects;
    }

    /**
     * Getter
     * @return
     */
    public long getIFCObjectsCount() {
        return mIFCObjects.size();
    }

    /**
     * Geometry
     * @param entityName
     * @param circleSegements
     */
    private void retrieveObjects(String entityName, long circleSegements) {
        long ifcCObjectInstances = IFCEngineLib.sdaiGetEntityExtentBN(mModel, entityName);

        long ifcObjectInstancesCount = IFCEngineLib.sdaiGetMemberCount(ifcCObjectInstances);
        if (ifcObjectInstancesCount == 0)
        {
            return;
        }

        for (long i = 0; i < ifcObjectInstancesCount; ++i)
        {
            long instance = IFCEngineLib.engiGetAggrElement(ifcCObjectInstances, i, IFCEngineLib.sdaiINSTANCE);

            IFCObject ifcObject = retrieveGeometry(entityName, instance, circleSegements);
            mIFCObjects.add(ifcObject);
        }
    }

    /**
     * Geometry
     * @param parentEntity
     * @param circleSegments
     * @param depth
     */
    private void retrieveObjects_depth(long parentEntity, long circleSegments, long depth)
    {
        if ((parentEntity == mIfcDistributionElementEntity) ||
                (parentEntity == mIfcElectricalElementEntity) ||
                (parentEntity == mIfcElementAssemblyEntity) ||
                (parentEntity == mIfcElementComponentEntity) ||
                (parentEntity == mIfcEquipmentElementEntity) ||
                (parentEntity == mIfcFeatureElementEntity) ||
                (parentEntity == mIfcFurnishingElementEntity) ||
                (parentEntity == mIfcTransportElementEntity) ||
                (parentEntity == mIfcVirtualElementEntity))
        {
            circleSegments = 12;
        }

        if (parentEntity == mIfcReinforcingElementEntity)
        {
            circleSegments = 6;
        }

        long ifcObjectInstances = IFCEngineLib.sdaiGetEntityExtent(mModel, parentEntity);
        long noIfcObjectIntances = IFCEngineLib.sdaiGetMemberCount(ifcObjectInstances);

        if (noIfcObjectIntances != 0)
        {
            String parentName = IFCEngineLib.engiGetEntityName(parentEntity);

            retrieveObjects(parentName, circleSegments);

            if (parentEntity == mIfcProjectEntity) {
                for (long i = 0; i < noIfcObjectIntances; i++) {
                    long instance = IFCEngineLib.engiGetAggrElement(ifcObjectInstances, i, IFCEngineLib.sdaiINSTANCE);

                    IFCObject ifcObject = retrieveGeometry(parentName, instance, circleSegments);
                    mIFCObjects.add(ifcObject);
                }
            }
        } // if (noIfcObjectIntances != 0)

        noIfcObjectIntances = IFCEngineLib.engiGetEntityCount(mModel);
        for (long i = 0; i < noIfcObjectIntances; i++)
        {
            long ifcEntity = IFCEngineLib.engiGetEntityElement(mModel, i);
            if (IFCEngineLib.engiGetEntityParent(ifcEntity) == parentEntity)
            {
                retrieveObjects_depth(ifcEntity, circleSegments, depth + 1);
            }
        }
    }

    /**
     * Geometry
     * @param ifcObjectInstance
     * @param circleSegments
     * @return
     */
    private IFCObject retrieveGeometry(String entityName, long ifcObjectInstance, long circleSegments) {
        String globalId = IFCEngineLib.sdaiGetAttrBNStr(ifcObjectInstance, "GlobalId");
        Log.v(TAG, "*** Entity: " + entityName + ", GlobalId: " + globalId);

        // Set up format
        long setting = 0, mask = 0;
        mask += flagbit2;        // PRECISION (32/64 bit)
        mask += flagbit3;        //	INDEX ARRAY (32/64 bit)
        mask += flagbit5;        // NORMALS
        mask += flagbit6;        // TEXTURE
        mask += flagbit8;        // TRIANGLES
        mask += flagbit9;        // LINES
        mask += flagbit10;       // POINTS
        mask += flagbit12;       // WIREFRAME
        mask += flagbit17;       // OPENGL
        mask += flagbit24;		 //	AMBIENT
        mask += flagbit25;		 //	DIFFUSE
        mask += flagbit26;		 //	EMISSIVE
        mask += flagbit27;		 //	SPECULAR

        setting += 0;		     // SINGLE PRECISION (float)
        setting += 0;            // 32 BIT INDEX ARRAY (Int32)
        setting += flagbit5;     // NORMALS ON
        setting += 0;			 // TEXTURE OFF
        setting += flagbit8;     // TRIANGLES ON
        setting += flagbit9;     // LINES ON
        setting += 0;            // POINTS OFF
        setting += flagbit12;    // WIREFRAME ON
        setting += flagbit17;    // OPENGL
        setting += flagbit24;	 //	AMBIENT
        setting += flagbit25;	 //	DIFFUSE
        setting += flagbit26;	 //	EMISSIVE
        setting += flagbit27;	 //	SPECULAR
        IFCEngineLib.setFormat(mModel, setting, mask);

	    /*
	    * Set up circleSegments()
        */
        if (circleSegments != DEFAULT_CIRCLE_SEGMENTS)
        {
            IFCEngineLib.circleSegments(circleSegments, 5);
        }

        IFCObject ifcObject = new IFCObject(entityName);
        IFCEngineLib.CalculateInstance(ifcObjectInstance, ifcObject);

        if ((ifcObject.getVerticesCount() > 0) && (ifcObject.getIndicesCount() > 0))
        {
            Log.v(TAG, "Vertices: " + ifcObject.getVerticesCount() + ", Indices: " + ifcObject.getIndicesCount());

            long owlModel = IFCEngineLib.owlGetModel(mModel);

            long owlInstance = IFCEngineLib.owlGetInstance(mModel, ifcObjectInstance);

            try {
                float[] vertices = IFCEngineLib.UpdateInstanceVertexBuffer(owlInstance, ifcObject.getVerticesCount() * VERTEX_LENGTH);
                ifcObject.setVertices(vertices);
            }
            catch (Exception ex) {
                Log.e(TAG, "UpdateInstanceVertexBuffer() failed.");
            }

            try {
                int[] indices = IFCEngineLib.UpdateInstanceIndexBuffer(owlInstance, ifcObject.getIndicesCount());
                ifcObject.setIndices(indices);
            }
            catch (Exception ex) {
                Log.e(TAG, "UpdateInstanceIndexBuffer() failed.");
            }

            // Material : List of Conceptual Faces
            HashMap<IFCMaterial, ArrayList<ConceptualFace>> material2ConceptualFaces = new HashMap<IFCMaterial, ArrayList<ConceptualFace>>();

            List<Pair<Long, Long>> wireframesIndices = new ArrayList<>();

            long conceptualFacesCount = IFCEngineLib.getConceptualFaceCnt(owlInstance);
            for (long faceIndex = 0; faceIndex < conceptualFacesCount; faceIndex++) {
                ConceptualFace conceptualFace = new ConceptualFace();
                IFCEngineLib.getConceptualFaceEx(owlInstance, faceIndex, conceptualFace);

                // Triangles
                if (conceptualFace.getTrianglesIndicesCount() > 0) {
                    // Material
                    long iIndexValue = ifcObject.getIndices()[(int)conceptualFace.getTrianglesStartIndex()];
                    iIndexValue *= VERTEX_LENGTH;

                    long ambientColor = IFCEngineLib.retrieveColor(ifcObject.getVertices()[(int)(iIndexValue + 6)]);
                    float transparency = IFCEngineLib.retrieveColorA(ambientColor);

                    long diffuseColor = IFCEngineLib.retrieveColor(ifcObject.getVertices()[(int)(iIndexValue + 7)]);
                    long emissiveColor = IFCEngineLib.retrieveColor(ifcObject.getVertices()[(int)(iIndexValue + 8)]);
                    long specularColor = IFCEngineLib.retrieveColor(ifcObject.getVertices()[(int)(iIndexValue + 9)]);

                    IFCMaterial ifcMaterial = new IFCMaterial(ambientColor, diffuseColor, emissiveColor, specularColor, transparency);
                    if (!material2ConceptualFaces.containsKey(ifcMaterial)) {
                        ArrayList<ConceptualFace> conceptualFaces = new ArrayList<ConceptualFace>();
                        conceptualFaces.add(conceptualFace);

                        material2ConceptualFaces.put(ifcMaterial, conceptualFaces);
                    }
                    else {
                        material2ConceptualFaces.get(ifcMaterial).add(conceptualFace);
                    }
                } // if (conceptualFace.getTrianglesIndicesCount() > 0)

                // Wireframes
                if (conceptualFace.getFacesPolygonsIndicesCount() > 0) {
                    wireframesIndices.add(new Pair<Long, Long>(conceptualFace.getFacesPolygonsStartIndex(), conceptualFace.getFacesPolygonsIndicesCount()));
                }

                // Lines
                // TODO

                // Points
                // TODO
            } // for (long faceIndex = ...

            // Group the triangles
            Log.v(TAG, "Materials count: " + material2ConceptualFaces.size());

            for (HashMap.Entry<IFCMaterial, ArrayList<ConceptualFace>> entry : material2ConceptualFaces.entrySet()) {
                IFCMaterial material = null;

                for (int concepualFaceIndex = 0; concepualFaceIndex < entry.getValue().size(); concepualFaceIndex++) {
                    ConceptualFace conceptualFace = entry.getValue().get(concepualFaceIndex);

                    long trianglesStartIndex = conceptualFace.getTrianglesStartIndex();
                    long trianglesIndicesCount = conceptualFace.getTrianglesIndicesCount();

                    // Split the conceptual face - isolated case
                    if (trianglesIndicesCount > MAX_INDICES_COUNT)
                    {
                        while (trianglesIndicesCount > MAX_INDICES_COUNT)
                        {
                            // Indices
                            IFCMaterial newMaterial = new IFCMaterial(entry.getKey());
                            for (long index = trianglesStartIndex; index < trianglesStartIndex + MAX_INDICES_COUNT; index++) {
                                newMaterial.indices().add(ifcObject.getIndices()[(int)index]);
                            }

                            ifcObject.materials().add(newMaterial);

                            trianglesIndicesCount -= MAX_INDICES_COUNT;
                            trianglesStartIndex += MAX_INDICES_COUNT;
                        }

                        if (trianglesIndicesCount > 0)
                        {
                            // Indices
                            IFCMaterial newMaterial = new IFCMaterial(entry.getKey());
                            for (long index = trianglesStartIndex; index < trianglesStartIndex + trianglesIndicesCount; index++) {
                                newMaterial.indices().add(ifcObject.getIndices()[(int)index]);
                            }

                            ifcObject.materials().add(newMaterial);
                        }

                        continue;
                    } // if (trianglesIndicesCount > MAX_INDICES_COUNT)

                    // Create material
                    if (material == null)
                    {
                        material = new IFCMaterial(entry.getKey());

                        ifcObject.materials().add(material);
                    }

                    // Check the limit
                    if (material.indices().size() + trianglesIndicesCount > MAX_INDICES_COUNT)
                    {
                        material = new IFCMaterial(entry.getKey());

                        ifcObject.materials().add(material);
                    }

                    // Indices
                    for (long index = trianglesStartIndex; index < trianglesStartIndex + trianglesIndicesCount; index++) {
                        material.indices().add(ifcObject.getIndices()[(int)index]);
                    }
                } // for (int concepualFaceIndex = ...
            } // for (HashMap.Entry< ...

            // Group the wireframes
            if (wireframesIndices.size() > 0) {
                WireframesCohort wireframesCohort = null;

                for (int face = 0; face < wireframesIndices.size(); face++)
                {
                    long startIndex = wireframesIndices.get(face).first;
                    long indicesCount =  wireframesIndices.get(face).second;

                    /*
                     * Create the cohort
                     */
                    if (wireframesCohort == null)
                    {
                        wireframesCohort = new WireframesCohort();
                        ifcObject.getWireframesCohorts().add(wireframesCohort);
                    }

                    /*
                     * Check the limit
                     */
                    if (wireframesCohort.indices().size() + indicesCount > MAX_INDICES_COUNT)
                    {
                        wireframesCohort = new WireframesCohort();
                        ifcObject.getWireframesCohorts().add(wireframesCohort);
                    }

                    long previousIndex = -1;
                    for (long index = startIndex; index < startIndex + indicesCount; index++)
                    {
                        if (ifcObject.getIndices()[(int)index] < 0)
                        {
                            previousIndex = -1;

                            continue;
                        }

                        if (previousIndex != -1)
                        {
                            wireframesCohort.indices().add(ifcObject.getIndices()[(int)previousIndex]);
                            wireframesCohort.indices().add(ifcObject.getIndices()[(int)index]);
                        } // if (iPreviousIndex != -1)

                        previousIndex = index;
                    } // for (long index ...
                } // for (int face = ...
            } // if (wireframesIndices.size() > 0)
        } // if ((ifcObject.getVerticesCount() > 0) && ...

        // Restore circleSegments()
        if (circleSegments != DEFAULT_CIRCLE_SEGMENTS)
        {
            IFCEngineLib.circleSegments(DEFAULT_CIRCLE_SEGMENTS, 5);
        }

        IFCEngineLib.cleanMemory(mModel, 0);

        entityName = entityName.toUpperCase();
        ifcObject.setVisible(!entityName.equals("IFCSPACE") && !entityName.equals("IFCRELSPACEBOUNDARY") && !entityName.equals("IFCOPENINGELEMENT"));

        return ifcObject;
    }

    /**
     * Properties
     */
    private void LoadHeader() {

        // Description
        int item = 0;
        String description;
        while ((description = IFCEngineLib.GetSPFFHeaderItem(mModel, 0, item++)) != null) {
            Log.v(TAG, "Header, Description: " + description);
        }

        // PreprocessorVersion
        String preprocessorVersion = IFCEngineLib.GetSPFFHeaderItem(mModel, 6, 0);
        Log.v(TAG, "Header, PreprocessorVersion: " + preprocessorVersion);

        // OriginatingSystem
        String originatingSystem = IFCEngineLib.GetSPFFHeaderItem(mModel, 7, 0);
        Log.v(TAG, "Header, OriginatingSystem: " + originatingSystem);

        // Authorization
        String authorization = IFCEngineLib.GetSPFFHeaderItem(mModel, 8, 0);
        Log.v(TAG, "Header, Authorization: " + authorization);

        // FileSchemas
        item = 0;
        String fileSchema;
        while ((fileSchema = IFCEngineLib.GetSPFFHeaderItem(mModel, 9, item++)) != null) {
            Log.v(TAG, "Header, FileSchemas: " + fileSchema);
        }
    }

    /**
     * IFC items
     */
    private void LoadProjects() {
        long ifcCObjectInstances = IFCEngineLib.sdaiGetEntityExtentBN(mModel, "IfcProject");

        long ifcObjectInstancesCount = IFCEngineLib.sdaiGetMemberCount(ifcCObjectInstances);
        for (long i = 0; i < ifcObjectInstancesCount; ++i)
        {
            /*
            Project
             */
            long instance = IFCEngineLib.engiGetAggrElement(ifcCObjectInstances, i, IFCEngineLib.sdaiINSTANCE);
            Log.v(TAG, "Project: " + instance);

            /*
             * decomposition/contains
             */
            LoadIsDecomposedBy(instance);
            LoadContainsElements(instance);

            /*
            Units TODO
             */
        }
    }

    /**
     * IFC items
     * @param parentInstance
     */
    private void LoadIsDecomposedBy(long parentInstance) {
        // check for decomposition
        long decompositionInstance = IFCEngineLib.sdaiGetAttrBNLong(parentInstance, "IsDecomposedBy", IFCEngineLib.sdaiAGGR);
        if (decompositionInstance == 0)
        {
            return;
        }

        long iDecompositionsCount = IFCEngineLib.sdaiGetMemberCount(decompositionInstance);
        if (iDecompositionsCount == 0)
        {
            return;
        }

        for (long iDecomposition = 0; iDecomposition < iDecompositionsCount; iDecomposition++)
        {
            long iDecompositionInstance = IFCEngineLib.engiGetAggrElement(decompositionInstance, iDecomposition, IFCEngineLib.sdaiINSTANCE);
            if (!IsInstanceOf(iDecompositionInstance, "IFCRELAGGREGATES"))
            {
                continue;
            }

            long objectInstances = IFCEngineLib.sdaiGetAttrBNLong(iDecompositionInstance, "RelatedObjects", IFCEngineLib.sdaiAGGR);
            long iObjectsCount = IFCEngineLib.sdaiGetMemberCount(objectInstances);

            for (long iObject = 0; iObject < iObjectsCount; iObject++)
            {
                long iObjectInstance = IFCEngineLib.engiGetAggrElement(objectInstances, iObject, IFCEngineLib.sdaiINSTANCE);
                Log.v(TAG, "IsDecomposedBy: " + iObjectInstance);

                LogObject(iObjectInstance);

                /*
                 * decomposition/contains
                 */
                LoadIsDecomposedBy(iObjectInstance);
                LoadContainsElements(iObjectInstance);
            } // for (long iObject = ...
        } // for (long iDecomposition = ...
    }

    /**
     * IFC items
     * @param objectInstance
     */
    private void LoadContainsElements(long objectInstance) {
        long containsElementsInstance = IFCEngineLib.sdaiGetAttrBNLong(objectInstance, "ContainsElements", IFCEngineLib.sdaiAGGR);
        if (containsElementsInstance == 0)
        {
            return;
        }

        long iContainsElementsInstancesCount = IFCEngineLib.sdaiGetMemberCount(containsElementsInstance);
        if (iContainsElementsInstancesCount == 0)
        {
            return;
        }

        for (long iContainsElements = 0; iContainsElements < iContainsElementsInstancesCount; iContainsElements++) {
            long iContainsElementsInstance = IFCEngineLib.engiGetAggrElement(containsElementsInstance, iContainsElements, IFCEngineLib.sdaiINSTANCE);
            if (!IsInstanceOf(iContainsElementsInstance, "IFCRELCONTAINEDINSPATIALSTRUCTURE")) {
                continue;
            }

            long objectInstances = IFCEngineLib.sdaiGetAttrBNLong(iContainsElementsInstance, "RelatedElements", IFCEngineLib.sdaiAGGR);
            long iObjectsCount = IFCEngineLib.sdaiGetMemberCount(objectInstances);

            for (long iObject = 0; iObject < iObjectsCount; iObject++)
            {
                long iObjectInstance = IFCEngineLib.engiGetAggrElement(objectInstances, iObject, IFCEngineLib.sdaiINSTANCE);
                Log.v(TAG, "ContainsElements: " + iObjectInstance);

                /*
                Log
                 */
                LogObject(iObjectInstance);

                /*
                Properties
                 */
                LoadProperties(iObjectInstance);

                /*
                 * decomposition/contains
                 */
                LoadIsDecomposedBy(iObjectInstance);
                LoadContainsElements(iObjectInstance);
            } // for (long iObject = ...
        } // for (long iContainsElements = ...
    }

    /**
     * Log
     * @param ifcObjectInstance
     */
    private void LogObject(long ifcObjectInstance) {
        Log.v(TAG, "Instance: " + ifcObjectInstance);

        String name = IFCEngineLib.sdaiGetAttrBNStr(ifcObjectInstance, "Name");
        String description = IFCEngineLib.sdaiGetAttrBNStr(ifcObjectInstance, "Description");

        Log.v(TAG, "Name: " + name);
    }

    /**
     * Properties
     * @param ifcObjectInstance
     */
    private void LoadProperties(long ifcObjectInstance) {
        long iIFCIsDefinedByInstances = IFCEngineLib.sdaiGetAttrBNLong(ifcObjectInstance, "IsDefinedBy", IFCEngineLib.sdaiAGGR);
        if (iIFCIsDefinedByInstances == 0)
        {
            return;
        }

        long iIFCIsDefinedByInstancesCount = IFCEngineLib.sdaiGetMemberCount(iIFCIsDefinedByInstances);
        for (long i = 0; i < iIFCIsDefinedByInstancesCount; ++i)
        {
            long iIFCIsDefinedByInstance = IFCEngineLib.engiGetAggrElement(iIFCIsDefinedByInstances, i, IFCEngineLib.sdaiINSTANCE);

            if (IsInstanceOf(iIFCIsDefinedByInstance, "IFCRELDEFINESBYPROPERTIES"))
            {
                LoadRelDefinesByProperties(iIFCIsDefinedByInstance);
            }
            else
            {
                if (IsInstanceOf(iIFCIsDefinedByInstance,"IFCRELDEFINESBYTYPE"))
                {
                    LoadRelDefinesByType(iIFCIsDefinedByInstance);
                }
            }
        }
    }

    /**
     * Properties
     * @param iIFCIsDefinedByInstance
     */
    private void LoadRelDefinesByProperties(long iIFCIsDefinedByInstance) {
        long iIFCPropertyDefinitionInstance = IFCEngineLib.sdaiGetAttrBNLong(iIFCIsDefinedByInstance, "RelatingPropertyDefinition", IFCEngineLib.sdaiINSTANCE);

        if (IsInstanceOf(iIFCPropertyDefinitionInstance, "IFCELEMENTQUANTITY"))
        {
            LoadQuantites(iIFCPropertyDefinitionInstance);
        }
        else
        {
            if (IsInstanceOf(iIFCPropertyDefinitionInstance, "IFCPROPERTYSET"))
            {
                LoadPropertySet(iIFCPropertyDefinitionInstance);
            }
        }
    }

    /**
     * Properties
     * @param iIFCIsDefinedByInstance
     */
    private void LoadRelDefinesByType(long iIFCIsDefinedByInstance) {
        long iIFCRelatingType = IFCEngineLib.sdaiGetAttrBNLong(iIFCIsDefinedByInstance, "RelatingType", IFCEngineLib.sdaiINSTANCE);

        if (iIFCRelatingType == 0)
        {
            return;
        }

        long piIFCHasPropertySets = IFCEngineLib.sdaiGetAttrBNLong(iIFCRelatingType, "HasPropertySets", IFCEngineLib.sdaiAGGR);

        long iIFCHasPropertySetsCount = IFCEngineLib.sdaiGetMemberCount(piIFCHasPropertySets);
        for (long i = 0; i < iIFCHasPropertySetsCount; ++i)
        {
            long iIFCHasPropertySetInstance = IFCEngineLib.engiGetAggrElement(piIFCHasPropertySets, i, IFCEngineLib.sdaiINSTANCE);
            if (IsInstanceOf(iIFCHasPropertySetInstance, "IFCELEMENTQUANTITY"))
            {
                LoadQuantites(iIFCHasPropertySetInstance);
            }
            else
                if (IsInstanceOf(iIFCHasPropertySetInstance, "IFCPROPERTYSET"))
                {
                    LoadPropertySet(iIFCHasPropertySetInstance);
                }
                else
                {
                    Log.e(TAG, "Unknown property type.");
                }
        }
    }

    /**
     * Properties
     * @param iIFCPropertySetInstance
     */
    private void LoadPropertySet(long iIFCPropertySetInstance) {
        String property = "--- Property set: ";

        String name = IFCEngineLib.sdaiGetAttrBNStr(iIFCPropertySetInstance, "Name");
        if (name != null) {
            property += name;
        }
        else {
            property += "<empty>";
        }

        String description = IFCEngineLib.sdaiGetAttrBNStr(iIFCPropertySetInstance, "Description");
        if (description != null) {
            property += " (";
            property += description;
            property += ")";
        }

        Log.v(TAG, property);

        long iIFCHasPropertiesInstances = IFCEngineLib.sdaiGetAttrBNLong(iIFCPropertySetInstance, "HasProperties", IFCEngineLib.sdaiAGGR);
        if (iIFCHasPropertiesInstances == 0)
        {
            return;
        }

        long iIFCHasPropertiesInstancesCount = IFCEngineLib.sdaiGetMemberCount(iIFCHasPropertiesInstances);
        for (long i = 0; i < iIFCHasPropertiesInstancesCount; ++i)
        {
            long iIFCHasPropertiesInstance = IFCEngineLib.engiGetAggrElement(iIFCHasPropertiesInstances, i, IFCEngineLib.sdaiINSTANCE);

            name = IFCEngineLib.sdaiGetAttrBNStr(iIFCHasPropertiesInstance, "Name");
            description = IFCEngineLib.sdaiGetAttrBNStr(iIFCHasPropertiesInstance, "Description");

            String strItem = "--- ";
            if (name != null)
            {
                strItem += name;
            }
            else
            {
                strItem += "<empty>";
            }

            if (description != null)
            {
                strItem += " ('";
                strItem += description;
                strItem += "')";
            }

            if (IsInstanceOf(iIFCHasPropertiesInstance, "IFCPROPERTYSINGLEVALUE"))
            {
                String strValue = GetPropertyValue(iIFCHasPropertiesInstance);
                if (strValue == null)
                {
                    strValue = "<empty>";
                }

                strItem += " = ";
                strItem += strValue;
            }

            Log.v(TAG, strItem);
        }
    }

    /**
     * Properties
     * @param iIFCPropertySetInstance
     */
    private void LoadQuantites(long iIFCPropertySetInstance) {
        String property = "--- Property set: ";

        String name = IFCEngineLib.sdaiGetAttrBNStr(iIFCPropertySetInstance, "Name");
        if (name != null) {
            property += name;
        }
        else {
            property += "<empty>";
        }

        String description = IFCEngineLib.sdaiGetAttrBNStr(iIFCPropertySetInstance, "Description");
        if (description != null) {
            property += " (";
            property += description;
            property += ")";
        }

        Log.v(TAG, property);

        long iIFCQuantities = IFCEngineLib.sdaiGetAttrBNLong(iIFCPropertySetInstance, "Quantities", IFCEngineLib.sdaiAGGR);

        long iIFCQuantitiesCount = IFCEngineLib.sdaiGetMemberCount(iIFCQuantities);
        for (long i = 0; i < iIFCQuantitiesCount; ++i)
        {
            long iIFCQuantityInstance = IFCEngineLib.engiGetAggrElement(iIFCQuantities, i, IFCEngineLib.sdaiINSTANCE);

            if (IsInstanceOf(iIFCQuantityInstance, "IFCQUANTITYLENGTH"))
            {
                LoadIFCQuantityLength(iIFCQuantityInstance);
            }
            else
            if (IsInstanceOf(iIFCQuantityInstance, "IFCQUANTITYAREA"))
            {
                LoadIFCQuantityArea(iIFCQuantityInstance);
            }
            else
            if (IsInstanceOf(iIFCQuantityInstance, "IFCQUANTITYVOLUME"))
            {
                LoadIFCQuantityVolume(iIFCQuantityInstance);
            }
            else
            if (IsInstanceOf(iIFCQuantityInstance, "IFCQUANTITYCOUNT"))
            {
                LoadIFCQuantityCount(iIFCQuantityInstance);
            }
            else
            if (IsInstanceOf(iIFCQuantityInstance, "IFCQUANTITYWEIGHT"))
            {
                LoadIFCQuantityWeight(iIFCQuantityInstance);
            }
            else
            if (IsInstanceOf(iIFCQuantityInstance, "IFCQUANTITYTIME"))
            {
                LoadIFCQuantityTime(iIFCQuantityInstance);
            }
            else
            {
                Log.e(TAG, "Unknown quantity.");
            }
        }
    }

    /**
     *
     * @param iIFCQuantity
     */
    private void LoadIFCQuantityLength(long iIFCQuantity) {
        String name = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Name");
        String description = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Description");
        String value = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "LengthValue");
        String unit = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Unit");

        String item = name;
        item += " = ";
        item += value;

        if (unit != null) {
            // TODO: LENGTHUNIT
        }

        if (description != null) {
            item += " ( ";
            item += description;
            item += ")";
        }

        Log.v(TAG, "--- Lenght: " + item);
    }

    /**
     *
     * @param iIFCQuantity
     */
    private void LoadIFCQuantityArea(long iIFCQuantity) {
        String name = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Name");
        String description = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Description");
        String value = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "AreaValue");
        String unit = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Unit");

        String item = name;
        item += " = ";
        item += value;

        if (unit != null) {
            // TODO: AREAUNIT
        }

        if (description != null) {
            item += " ( ";
            item += description;
            item += ")";
        }

        Log.v(TAG, "--- Area: " + item);
    }

    /**
     *
     * @param iIFCQuantity
     */
    private void LoadIFCQuantityVolume(long iIFCQuantity) {
        String name = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Name");
        String description = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Description");
        String value = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "VolumeValue");
        String unit = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Unit");

        String item = name;
        item += " = ";
        item += value;

        if (unit != null) {
            // TODO: VOLUMEUNIT
        }

        if (description != null) {
            item += " ( ";
            item += description;
            item += ")";
        }

        Log.v(TAG, "--- Volume: " + item);
    }

    /**
     *
     * @param iIFCQuantity
     */
    private void LoadIFCQuantityCount(long iIFCQuantity) {
        String name = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Name");
        String description = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Description");
        String value = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "CountValue");
        String unit = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Unit");

        String item = name;
        item += " = ";
        item += value;

        if (unit != null) {
            item += " ";
            item += value;
        }

        if (description != null) {
            item += " ( ";
            item += description;
            item += ")";
        }

        Log.v(TAG, "--- Count: " + item);
    }

    /**
     *
     * @param iIFCQuantity
     */
    private void LoadIFCQuantityWeight(long iIFCQuantity) {
        String name = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Name");
        String description = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Description");
        String value = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "WeigthValue");
        String unit = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Unit");

        String item = name;
        item += " = ";
        item += value;

        if (unit != null) {
            // TODO: MASSUNIT
        }

        if (description != null) {
            item += " ( ";
            item += description;
            item += ")";
        }

        Log.v(TAG, "--- Weight: " + item);
    }

    /**
     *
     * @param iIFCQuantity
     */
    private void LoadIFCQuantityTime(long iIFCQuantity) {
        String name = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Name");
        String description = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Description");
        String value = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "TimeValue");
        String unit = IFCEngineLib.sdaiGetAttrBNStr(iIFCQuantity, "Unit");

        String item = name;
        item += " = ";
        item += value;

        if (unit != null) {
            // TODO: TIMEUNIT
        }

        if (description != null) {
            item += " ( ";
            item += description;
            item += ")";
        }

        Log.v(TAG, "--- Time: " + item);
    }

    /**
     * Properties
     */
    private void LoadUnreferenced() {
        // TODO
    }

    private String GetPropertyValue(long iIFCPropertySingleValue) {
        String nominalValueADB = IFCEngineLib.sdaiGetAttrBNStr(iIFCPropertySingleValue, "NominalValue");
        if (nominalValueADB == null)
        {
            return null;
        }

        String unitADB = IFCEngineLib.sdaiGetAttrBNStr(iIFCPropertySingleValue, "Unit");

        String typePath = IFCEngineLib.sdaiGetADBTypePath(nominalValueADB, 0);
        if (typePath == null)
        {
            return nominalValueADB;
        }

        if (typePath == "IFCBOOLEAN")
        {
            return null;
        }

        if (typePath == "IFCIDENTIFIER")
        {
            return null;
        }

        if (typePath == "IFCINTEGER")
        {
            return null;
        }

        if (typePath == "IFCLABEL")
        {
            return null;
        }

        if (typePath == "IFCTEXT")
        {
            return null;
        }

        if (typePath == "IFCREAL")
        {
            return null;
        }

        if (typePath == "IFCCOUNTMEASURE")
        {
            return null;
        }

        if (typePath == "IFCPOSITIVERATIOMEASURE")
        {
            return null;
        }

        if (typePath == "IFCVOLUMETRICFLOWRATEMEASURE")
        {
            return null;
        }

        return null;
    }

    /**
     * Compare
     * @param iInstance
     * @param strType
     * @return
     */
    private boolean IsInstanceOf(long iInstance, String strType) {
        if (IFCEngineLib.sdaiGetInstanceType(iInstance) == IFCEngineLib.sdaiGetEntity(mModel, strType))
        {
            return true;
        }

        return false;
    }
}
