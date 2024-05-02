using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

#if _WIN64
using int_t = System.Int64;
#else
using int_t = System.Int32;
#endif

namespace IfcEngine
{
    class x86_64
    {
        public const int_t flagbit0 = 1;           // 2^^0    0000.0000..0000.0001
        public const int_t flagbit1 = 2;           // 2^^1    0000.0000..0000.0010
        public const int_t flagbit2 = 4;           // 2^^2    0000.0000..0000.0100
        public const int_t flagbit3 = 8;           // 2^^3    0000.0000..0000.1000
        public const int_t flagbit4 = 16;          // 2^^4    0000.0000..0001.0000
        public const int_t flagbit5 = 32;          // 2^^5    0000.0000..0010.0000
        public const int_t flagbit6 = 64;          // 2^^6    0000.0000..0100.0000
        public const int_t flagbit7 = 128;         // 2^^7    0000.0000..1000.0000
        public const int_t flagbit8 = 256;         // 2^^8    0000.0001..0000.0000
        public const int_t flagbit9 = 512;         // 2^^9    0000.0010..0000.0000
        public const int_t flagbit10 = 1024;       // 2^^10   0000.0100..0000.0000
        public const int_t flagbit11 = 2048;       // 2^^11   0000.1000..0000.0000
        public const int_t flagbit12 = 4096;       // 2^^12   0001.0000..0000.0000
        public const int_t flagbit13 = 8192;       // 2^^13   0010.0000..0000.0000
        public const int_t flagbit14 = 16384;      // 2^^14   0100.0000..0000.0000
        public const int_t flagbit15 = 32768;      // 2^^15   1000.0000..0000.0000
        public const int_t flagbit24 = 16777216;
        public const int_t flagbit25 = 33554432;
        public const int_t flagbit26 = 67108864;
        public const int_t flagbit27 = 134217728;

        public const int_t sdaiADB = 1;
        public const int_t sdaiAGGR = sdaiADB + 1;
        public const int_t sdaiBINARY = sdaiAGGR + 1;
        public const int_t sdaiBOOLEAN = sdaiBINARY + 1;
        public const int_t sdaiENUM = sdaiBOOLEAN + 1;
        public const int_t sdaiINSTANCE = sdaiENUM + 1;
        public const int_t sdaiINTEGER = sdaiINSTANCE + 1;
        public const int_t sdaiLOGICAL = sdaiINTEGER + 1;
        public const int_t sdaiREAL = sdaiLOGICAL + 1;
        public const int_t sdaiSTRING = sdaiREAL + 1;
        public const int_t sdaiUNICODE = sdaiSTRING + 1;
        public const int_t sdaiEXPRESSSTRING = sdaiUNICODE + 1;
        public const int_t engiGLOBALID = sdaiEXPRESSSTRING + 1;

        public const string IFCEngineDLL = @"ifcengine.dll";


        //
        //  Calls for File IO 
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCloseModel")]
        public static extern void sdaiCloseModel(int_t model);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern int_t sdaiCreateModelBN(int_t repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern int_t sdaiCreateModelBN(int_t repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern int_t sdaiCreateModelBNUnicode(int_t repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern int_t sdaiOpenModelBN(int_t repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern int_t sdaiOpenModelBN(int_t repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern int_t sdaiOpenModelBNUnicode(int_t repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(int_t model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]
        public static extern void sdaiSaveModelBNUnicode(int_t model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(int_t model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]
        public static extern void sdaiSaveModelAsXmlBNUnicode(int_t model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(int_t model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]
        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(int_t model, byte[] fileName);


        //
        //  Schema Reading
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern int_t sdaiGetEntity(int_t model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern int_t sdaiGetEntity(int_t model, byte[] entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentName")]
        public static extern void engiGetEntityArgumentName(int_t entity, int_t index, int_t valueType, out IntPtr argumentName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentType")]
        public static extern void engiGetEntityArgumentType(int_t entity, int_t index, ref int_t argumentType);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityCount")]
        public static extern int_t engiGetEntityCount(int_t model);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityElement")]
        public static extern int_t engiGetEntityElement(int_t model, int_t index);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtent")]
        public static extern int_t sdaiGetEntityExtent(int_t model, int_t entity);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern int_t sdaiGetEntityExtentBN(int_t model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern int_t sdaiGetEntityExtentBN(int_t model, byte[] entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityName")]
        public static extern void engiGetEntityName(int_t entity, int_t valueType, out IntPtr entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityNoArguments")]
        public static extern int_t engiGetEntityNoArguments(int_t entity);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityParent")]
        public static extern int_t engiGetEntityParent(int_t entity);


        //
        //  Instance Header
        //


        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItem")]
        public static extern int_t GetSPFFHeaderItem(int_t model, int_t itemIndex, int_t itemSubIndex, int_t valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(int_t model, string description, string implementationLevel, string name, string timeStamp, string author, string organization, string preprocessorVersion, string originatingSystem, string authorization, string fileSchema);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(int_t model, byte[] description, byte[] implementationLevel, byte[] name, byte[] timeStamp, byte[] author, byte[] organization, byte[] preprocessorVersion, byte[] originatingSystem, byte[] authorization, byte[] fileSchema);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern int_t SetSPFFHeaderItem(int_t model, int_t itemIndex, int_t itemSubIndex, int_t valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern int_t SetSPFFHeaderItem(int_t model, int_t itemIndex, int_t itemSubIndex, int_t valueType, byte[] value);


        //
        //  Instance Reading
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBType")]
        public static extern int_t sdaiGetADBType(int_t ADB);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePathx")]
        public static extern void sdaiGetADBTypePath(int_t ADB, int_t typeNameNumber, out IntPtr path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(int_t ADB, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(int_t ADB, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(int_t ADB, int_t valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern int_t engiGetAggrElement(int_t aggregate, int_t elementIndex, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern int_t engiGetAggrElement(int_t aggregate, int_t elementIndex, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern int_t engiGetAggrElement(int_t aggregate, int_t elementIndex, int_t valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrType")]
        public static extern void engiGetAggrType(int_t aggregate, ref int_t aggragateType);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern int_t sdaiGetAttr(int_t instance, int_t attribute, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern int_t sdaiGetAttr(int_t instance, int_t attribute, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern int_t sdaiGetAttr(int_t instance, int_t attribute, int_t valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern int_t sdaiGetAttrBN(int_t instance, string attributeName, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern int_t sdaiGetAttrBN(int_t instance, string attributeName, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern int_t sdaiGetAttrBN(int_t instance, string attributeName, int_t valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern int_t sdaiGetAttrBN(int_t instance, byte[] attributeName, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern int_t sdaiGetAttrBN(int_t instance, byte[] attributeName, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern int_t sdaiGetAttrBN(int_t instance, byte[] attributeName, int_t valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern int_t sdaiGetAttrDefinition(int_t entity, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern int_t sdaiGetAttrDefinition(int_t entity, byte[] attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceType")]
        public static extern int_t sdaiGetInstanceType(int_t instance);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetMemberCount")]
        public static extern int_t sdaiGetMemberCount(int_t aggregate);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsKindOf")]
        public static extern int_t sdaiIsKindOf(int_t instance, int_t entity);


        //
        //  Instance Writing
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(int_t list, int_t valueType, ref int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(int_t list, int_t valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(int_t list, int_t valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(int_t list, int_t valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern int_t sdaiCreateADB(int_t valueType, ref int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern int_t sdaiCreateADB(int_t valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern int_t sdaiCreateADB(int_t valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern int_t sdaiCreateADB(int_t valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggr")]
        public static extern int_t sdaiCreateAggr(int_t instance, int_t attribute);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern int_t sdaiCreateAggrBN(int_t instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern int_t sdaiCreateAggrBN(int_t instance, byte[] attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstance")]
        public static extern int_t sdaiCreateInstance(int_t model, int_t entity);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern int_t sdaiCreateInstanceBN(int_t model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern int_t sdaiCreateInstanceBN(int_t model, byte[] entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteInstance")]
        public static extern void sdaiDeleteInstance(int_t instance);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(int_t ADB, int_t pathCount, string path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(int_t ADB, int_t pathCount, byte[] path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(int_t instance, int_t attribute, int_t valueType, ref int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(int_t instance, int_t attribute, int_t valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(int_t instance, int_t attribute, int_t valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(int_t instance, int_t attribute, int_t valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(int_t instance, string attributeName, int_t valueType, ref int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(int_t instance, string attributeName, int_t valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(int_t instance, string attributeName, int_t valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(int_t instance, string attributeName, int_t valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(int_t instance, byte[] attributeName, int_t valueType, ref int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(int_t instance, byte[] attributeName, int_t valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(int_t instance, byte[] attributeName, int_t valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(int_t instance, byte[] attributeName, int_t valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(int_t instance, string comment);

        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(int_t instance, byte[] comment);


        //
        //  Controling Calls
        //


        [DllImport(IFCEngineDLL, EntryPoint = "circleSegments")]
        public static extern void circleSegments(int_t circles, int_t smallCircles);

        [DllImport(IFCEngineDLL, EntryPoint = "cleanMemory")]
        public static extern void cleanMemory(int_t model, int_t mode);

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetP21Line")]
        public static extern int_t internalGetP21Line(int_t instance);

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetInstanceFromP21Line")]
        public static extern int_t internalGetInstanceFromP21Line(int_t model, int_t P21Line);

        [DllImport(IFCEngineDLL, EntryPoint = "setStringUnicode")]
        public static extern int_t setStringUnicode(int_t unicode);


        //
        //  Geometry Interaction
        //


        [DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstance")]
        public static extern int_t initializeModellingInstance(int_t model, ref int_t noVertices, ref int_t noIndices, double scale, int_t instance);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern int_t finalizeModelling(int_t model, float[] vertices, int[] indices, int_t FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern int_t finalizeModelling(int_t model, double[] vertices, int_t[] indices, int_t FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceInModelling")]
        public static extern int_t getInstanceInModelling(int_t model, int_t instance, int_t mode, ref int_t startVertex, ref int_t startIndex, ref int_t primitiveCount);

        [DllImport(IFCEngineDLL, EntryPoint = "setVertexOffset")]
        public static extern void setVertexOffset(int_t model, double x, double y, double z);

        [DllImport(IFCEngineDLL, EntryPoint = "setFilter")]
        public static extern void setFilter(int_t model, int_t setting, int_t mask);

        [DllImport(IFCEngineDLL, EntryPoint = "setFormat")]
        public static extern void setFormat(int_t model, int_t setting, int_t mask);

        [DllImport(IFCEngineDLL, EntryPoint = "SetFormat")]
        public static extern int_t SetFormat(int_t model, int_t setting, int_t mask);
        
        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceCnt")]
        public static extern int_t getConceptualFaceCnt(int_t instance);

        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceEx")]
        public static extern int_t getConceptualFaceEx(int_t instance, int_t index, ref int_t startIndexTriangles, ref int_t noIndicesTriangles, ref int_t startIndexLines, ref int_t noIndicesLines, ref int_t startIndexPoints, ref int_t noIndicesPoints, ref int_t startIndexFacesPolygons, ref int_t noIndicesFacesPolygons, ref int_t startIndexConceptualFacePolygons, ref int_t noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]
        public static extern Int64 CalculateInstance(Int64 owlInstance, ref Int64 vertexBufferSize, ref Int64 indexBufferSize, ref Int64 transformationBufferSize);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
        public static extern Int64 UpdateInstanceVertexBuffer(Int64 owlInstance, float[] vertexBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
        public static extern Int64 UpdateInstanceIndexBuffer(Int64 owlInstance, int[] indexBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceClass")]
        public static extern Int64 GetInstanceClass(Int64 owlInstance);

        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByName")]
        public static extern Int64 GetClassByName(Int64 model, string owlClassName);

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName")]
        public static extern Int64 GetPropertyByName(Int64 model, string rdfPropertyName);

        [DllImport(IFCEngineDLL, EntryPoint = "GetObjectTypeProperty")]
        public static extern Int64 GetObjectTypeProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr value, ref Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "GetDataTypeProperty")]
        public static extern Int64 GetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr value, ref Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "owlGetModel")]
        public static extern void owlGetModel(int_t model, ref Int64 owlModel);

        [DllImport(IFCEngineDLL, EntryPoint = "owlGetInstance")]
        public static extern void owlGetInstance(int_t model, int_t instance, ref Int64 owlInstance);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDefaultColor")]
        public static extern void SetDefaultColor(Int64 model, UInt32 ambient, UInt32 diffuse, UInt32 emissive, UInt32 specular);
    }
}
