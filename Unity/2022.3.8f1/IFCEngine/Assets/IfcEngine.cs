using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

#if UNITY_64
using int_t = System.Int64;
#else
using int_t = System.Int32;
#endif

namespace IfcEngine
{
    class x64
    {
        public const Int64 flagbit0 = 1;           // 2^^0    0000.0000..0000.0001
        public const Int64 flagbit1 = 2;           // 2^^1    0000.0000..0000.0010
        public const Int64 flagbit2 = 4;           // 2^^2    0000.0000..0000.0100
        public const Int64 flagbit3 = 8;           // 2^^3    0000.0000..0000.1000
        public const Int64 flagbit4 = 16;          // 2^^4    0000.0000..0001.0000
        public const Int64 flagbit5 = 32;          // 2^^5    0000.0000..0010.0000
        public const Int64 flagbit6 = 64;          // 2^^6    0000.0000..0100.0000
        public const Int64 flagbit7 = 128;         // 2^^7    0000.0000..1000.0000
        public const Int64 flagbit8 = 256;         // 2^^8    0000.0001..0000.0000
        public const Int64 flagbit9 = 512;         // 2^^9    0000.0010..0000.0000
        public const Int64 flagbit10 = 1024;       // 2^^10   0000.0100..0000.0000
        public const Int64 flagbit11 = 2048;       // 2^^11   0000.1000..0000.0000
        public const Int64 flagbit12 = 4096;       // 2^^12   0001.0000..0000.0000
        public const Int64 flagbit13 = 8192;       // 2^^13   0010.0000..0000.0000
        public const Int64 flagbit14 = 16384;      // 2^^14   0100.0000..0000.0000
        public const Int64 flagbit15 = 32768;      // 2^^15   1000.0000..0000.0000

		public const Int64 flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
		public const Int64 flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
		public const Int64 flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000
		public const Int64 flagbit23 = 8388608;		// 2^^23   0000.0000..1000.0000  0000.0000..0000.0000

        public const Int64 sdaiADB = 1;
        public const Int64 sdaiAGGR = sdaiADB + 1;
        public const Int64 sdaiBINARY = sdaiAGGR + 1;
        public const Int64 sdaiBOOLEAN = sdaiBINARY + 1;
        public const Int64 sdaiENUM = sdaiBOOLEAN + 1;
        public const Int64 sdaiINSTANCE = sdaiENUM + 1;
        public const Int64 sdaiINTEGER = sdaiINSTANCE + 1;
        public const Int64 sdaiLOGICAL = sdaiINTEGER + 1;
        public const Int64 sdaiREAL = sdaiLOGICAL + 1;
        public const Int64 sdaiSTRING = sdaiREAL + 1;
        public const Int64 sdaiUNICODE = sdaiSTRING + 1;
        public const Int64 sdaiEXPRESSSTRING = sdaiUNICODE + 1;
        public const Int64 engiGLOBALID = sdaiEXPRESSSTRING + 1;

		public const string IFCEngineDLL = @"ifcengine";


        //
        //  Calls for File IO
        //			

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCloseModel")]
        public static extern void sdaiCloseModel(Int64 model);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern Int64 sdaiCreateModelBN(Int64 repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern Int64 sdaiCreateModelBN(Int64 repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern Int64 sdaiCreateModelBNUnicode(Int64 repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern Int64 sdaiOpenModelBN(Int64 repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern Int64 sdaiOpenModelBN(Int64 repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern Int64 sdaiOpenModelBNUnicode(Int64 repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(Int64 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(Int64 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]
        public static extern void sdaiSaveModelBNUnicode(Int64 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(Int64 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(Int64 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]
        public static extern void sdaiSaveModelAsXmlBNUnicode(Int64 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(Int64 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(Int64 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]
        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(Int64 model, byte[] fileName);


        //
        //  Schema Reading
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern Int64 sdaiGetEntity(Int64 model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern Int64 sdaiGetEntity(Int64 model, byte[] entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentName")]
        public static extern void engiGetEntityArgumentName(Int64 entity, Int64 index, Int64 valueType, out IntPtr argumentName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentType")]
        public static extern void engiGetEntityArgumentType(Int64 entity, Int64 index, ref Int64 argumentType);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityCount")]
        public static extern Int64 engiGetEntityCount(Int64 model);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityElement")]
        public static extern Int64 engiGetEntityElement(Int64 model, Int64 index);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtent")]
        public static extern Int64 sdaiGetEntityExtent(Int64 model, Int64 entity);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern Int64 sdaiGetEntityExtentBN(Int64 model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern Int64 sdaiGetEntityExtentBN(Int64 model, byte[] entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityName")]
        public static extern void engiGetEntityName(Int64 entity, Int64 valueType, out IntPtr entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityNoArguments")]
        public static extern Int64 engiGetEntityNoArguments(Int64 entity);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityParent")]
        public static extern Int64 engiGetEntityParent(Int64 entity);


        //
        //  Instance Header
        //


        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItem")]
        public static extern Int64 GetSPFFHeaderItem(Int64 model, Int64 itemIndex, Int64 itemSubIndex, Int64 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(Int64 model, string description, string implementationLevel, string name, string timeStamp, string author, string organization, string preprocessorVersion, string originatingSystem, string authorization, string fileSchema);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(Int64 model, byte[] description, byte[] implementationLevel, byte[] name, byte[] timeStamp, byte[] author, byte[] organization, byte[] preprocessorVersion, byte[] originatingSystem, byte[] authorization, byte[] fileSchema);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern Int64 SetSPFFHeaderItem(Int64 model, Int64 itemIndex, Int64 itemSubIndex, Int64 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern Int64 SetSPFFHeaderItem(Int64 model, Int64 itemIndex, Int64 itemSubIndex, Int64 valueType, byte[] value);


        //
        //  Instance Reading
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBType")]
        public static extern Int64 sdaiGetADBType(Int64 ADB);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePathx")]
        public static extern void sdaiGetADBTypePath(Int64 ADB, Int64 typeNameNumber, out IntPtr path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(Int64 ADB, Int64 valueType, out Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(Int64 ADB, Int64 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(Int64 ADB, Int64 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern Int64 engiGetAggrElement(Int64 aggregate, Int64 elementIndex, Int64 valueType, out Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern Int64 engiGetAggrElement(Int64 aggregate, Int64 elementIndex, Int64 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern Int64 engiGetAggrElement(Int64 aggregate, Int64 elementIndex, Int64 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrType")]
        public static extern void engiGetAggrType(Int64 aggregate, ref Int64 aggragateType);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern Int64 sdaiGetAttr(Int64 instance, Int64 attribute, Int64 valueType, out Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern Int64 sdaiGetAttr(Int64 instance, Int64 attribute, Int64 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern Int64 sdaiGetAttr(Int64 instance, Int64 attribute, Int64 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, string attributeName, Int64 valueType, out Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, string attributeName, Int64 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, string attributeName, Int64 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, out Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern Int64 sdaiGetAttrDefinition(Int64 entity, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern Int64 sdaiGetAttrDefinition(Int64 entity, byte[] attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceType")]
        public static extern Int64 sdaiGetInstanceType(Int64 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetMemberCount")]
        public static extern Int64 sdaiGetMemberCount(Int64 aggregate);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsKindOf")]
        public static extern Int64 sdaiIsKindOf(Int64 instance, Int64 entity);


        //
        //  Instance Writing
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int64 list, Int64 valueType, ref Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int64 list, Int64 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int64 list, Int64 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int64 list, Int64 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int64 sdaiCreateADB(Int64 valueType, ref Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int64 sdaiCreateADB(Int64 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int64 sdaiCreateADB(Int64 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int64 sdaiCreateADB(Int64 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggr")]
        public static extern Int64 sdaiCreateAggr(Int64 instance, Int64 attribute);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern Int64 sdaiCreateAggrBN(Int64 instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern Int64 sdaiCreateAggrBN(Int64 instance, byte[] attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstance")]
        public static extern Int64 sdaiCreateInstance(Int64 model, Int64 entity);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern Int64 sdaiCreateInstanceBN(Int64 model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern Int64 sdaiCreateInstanceBN(Int64 model, byte[] entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteInstance")]
        public static extern void sdaiDeleteInstance(Int64 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(Int64 ADB, Int64 pathCount, string path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(Int64 ADB, Int64 pathCount, byte[] path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int64 instance, Int64 attribute, Int64 valueType, ref Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int64 instance, Int64 attribute, Int64 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int64 instance, Int64 attribute, Int64 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int64 instance, Int64 attribute, Int64 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, string attributeName, Int64 valueType, ref Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, string attributeName, Int64 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, string attributeName, Int64 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, string attributeName, Int64 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, ref Int64 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(Int64 instance, string comment);

        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(Int64 instance, byte[] comment);


        //
        //  Controling Calls
        //


        [DllImport(IFCEngineDLL, EntryPoint = "circleSegments")]
        public static extern void circleSegments(Int64 circles, Int64 smallCircles);

        [DllImport(IFCEngineDLL, EntryPoint = "cleanMemory")]
        public static extern void cleanMemory(Int64 model, Int64 mode);

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetP21Line")]
        public static extern Int64 internalGetP21Line(Int64 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetInstanceFromP21Line")]
        public static extern Int64 internalGetInstanceFromP21Line(Int64 model, Int64 P21Line);

        [DllImport(IFCEngineDLL, EntryPoint = "setStringUnicode")]
        public static extern Int64 setStringUnicode(Int64 unicode);


        //
        //  Geometry Interaction
        //


        [DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstance")]
        public static extern Int64 initializeModellingInstance(Int64 model, ref Int64 noVertices, ref Int64 noIndices, double scale, Int64 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern Int64 finalizeModelling(Int64 model, float[] vertices, Int32[] indices, Int64 FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern Int64 finalizeModelling(Int64 model, float[] vertices, Int64[] indices, Int64 FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern Int64 finalizeModelling(Int64 model, double[] vertices, Int32[] indices, Int64 FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern Int64 finalizeModelling(Int64 model, double[] vertices, Int64[] indices, Int64 FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceInModelling")]
        public static extern Int64 getInstanceInModelling(Int64 model, Int64 instance, Int64 mode, ref Int64 startVertex, ref Int64 startIndex, ref Int64 primitiveCount);

        [DllImport(IFCEngineDLL, EntryPoint = "setVertexOffset")]
        public static extern void setVertexOffset(Int64 model, double x, double y, double z);

        [DllImport(IFCEngineDLL, EntryPoint = "setFilter")]
        public static extern void setFilter(Int64 model, Int64 setting, Int64 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "setFormat")]
        public static extern void setFormat(Int64 model, Int64 setting, Int64 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceCnt")]
        public static extern Int64 getConceptualFaceCnt(Int64 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceEx")]
        public static extern Int64 getConceptualFaceEx(Int64 instance, Int64 index, ref Int64 startIndexTriangles, ref Int64 noIndicesTriangles, ref Int64 startIndexLines, ref Int64 noIndicesLines, ref Int64 startIndexPoints, ref Int64 noIndicesPoints, ref Int64 startIndexFacesPolygons, ref Int64 noIndicesFacesPolygons, ref Int64 startIndexConceptualFacePolygons, ref Int64 noIndicesConceptualFacePolygons);
    }

    class x86
    {
        public const Int32 flagbit0 = 1;           // 2^^0    0000.0000..0000.0001
        public const Int32 flagbit1 = 2;           // 2^^1    0000.0000..0000.0010
        public const Int32 flagbit2 = 4;           // 2^^2    0000.0000..0000.0100
        public const Int32 flagbit3 = 8;           // 2^^3    0000.0000..0000.1000
        public const Int32 flagbit4 = 16;          // 2^^4    0000.0000..0001.0000
        public const Int32 flagbit5 = 32;          // 2^^5    0000.0000..0010.0000
        public const Int32 flagbit6 = 64;          // 2^^6    0000.0000..0100.0000
        public const Int32 flagbit7 = 128;         // 2^^7    0000.0000..1000.0000
        public const Int32 flagbit8 = 256;         // 2^^8    0000.0001..0000.0000
        public const Int32 flagbit9 = 512;         // 2^^9    0000.0010..0000.0000
        public const Int32 flagbit10 = 1024;       // 2^^10   0000.0100..0000.0000
        public const Int32 flagbit11 = 2048;       // 2^^11   0000.1000..0000.0000
        public const Int32 flagbit12 = 4096;       // 2^^12   0001.0000..0000.0000
        public const Int32 flagbit13 = 8192;       // 2^^13   0010.0000..0000.0000
        public const Int32 flagbit14 = 16384;      // 2^^14   0100.0000..0000.0000
        public const Int32 flagbit15 = 32768;      // 2^^15   1000.0000..0000.0000

		public const Int32 flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
		public const Int32 flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
		public const Int32 flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000
		public const Int32 flagbit23 = 8388608;		// 2^^23   0000.0000..1000.0000  0000.0000..0000.0000

        public const Int32 sdaiADB = 1;
        public const Int32 sdaiAGGR = sdaiADB + 1;
        public const Int32 sdaiBINARY = sdaiAGGR + 1;
        public const Int32 sdaiBOOLEAN = sdaiBINARY + 1;
        public const Int32 sdaiENUM = sdaiBOOLEAN + 1;
        public const Int32 sdaiINSTANCE = sdaiENUM + 1;
        public const Int32 sdaiINTEGER = sdaiINSTANCE + 1;
        public const Int32 sdaiLOGICAL = sdaiINTEGER + 1;
        public const Int32 sdaiREAL = sdaiLOGICAL + 1;
        public const Int32 sdaiSTRING = sdaiREAL + 1;
        public const Int32 sdaiUNICODE = sdaiSTRING + 1;
        public const Int32 sdaiEXPRESSSTRING = sdaiUNICODE + 1;
        public const Int32 engiGLOBALID = sdaiEXPRESSSTRING + 1;

		public const string IFCEngineDLL = "ifcengine.dll";


        //
        //  Calls for File IO
        //		

        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByArray")]
        public static extern Int32 engiOpenModelByArray(Int32 repository, byte[] ifcContent, Int32 length, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCloseModel")]
        public static extern void sdaiCloseModel(Int32 model);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern Int32 sdaiCreateModelBN(Int32 repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern Int32 sdaiCreateModelBN(Int32 repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern Int32 sdaiCreateModelBNUnicode(Int32 repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern Int32 sdaiOpenModelBN(Int32 repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern Int32 sdaiOpenModelBN(Int32 repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern Int32 sdaiOpenModelBNUnicode(Int32 repository, byte[] fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(Int32 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(Int32 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]
        public static extern void sdaiSaveModelBNUnicode(Int32 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(Int32 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(Int32 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]
        public static extern void sdaiSaveModelAsXmlBNUnicode(Int32 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(Int32 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(Int32 model, byte[] fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]
        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(Int32 model, byte[] fileName);


        //
        //  Schema Reading
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern Int32 sdaiGetEntity(Int32 model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern Int32 sdaiGetEntity(Int32 model, byte[] entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentName")]
        public static extern void engiGetEntityArgumentName(Int32 entity, Int32 index, Int32 valueType, out IntPtr argumentName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentType")]
        public static extern void engiGetEntityArgumentType(Int32 entity, Int32 index, ref Int32 argumentType);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityCount")]
        public static extern Int32 engiGetEntityCount(Int32 model);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityElement")]
        public static extern Int32 engiGetEntityElement(Int32 model, Int32 index);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtent")]
        public static extern Int32 sdaiGetEntityExtent(Int32 model, Int32 entity);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern Int32 sdaiGetEntityExtentBN(Int32 model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern Int32 sdaiGetEntityExtentBN(Int32 model, byte[] entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityName")]
        public static extern void engiGetEntityName(Int32 entity, Int32 valueType, out IntPtr entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityNoArguments")]
        public static extern Int32 engiGetEntityNoArguments(Int32 entity);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityParent")]
        public static extern Int32 engiGetEntityParent(Int32 entity);


        //
        //  Instance Header
        //


        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItem")]
        public static extern Int32 GetSPFFHeaderItem(Int32 model, Int32 itemIndex, Int32 itemSubIndex, Int32 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(Int32 model, string description, string implementationLevel, string name, string timeStamp, string author, string organization, string preprocessorVersion, string originatingSystem, string authorization, string fileSchema);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(Int32 model, byte[] description, byte[] implementationLevel, byte[] name, byte[] timeStamp, byte[] author, byte[] organization, byte[] preprocessorVersion, byte[] originatingSystem, byte[] authorization, byte[] fileSchema);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern Int32 SetSPFFHeaderItem(Int32 model, Int32 itemIndex, Int32 itemSubIndex, Int32 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern Int32 SetSPFFHeaderItem(Int32 model, Int32 itemIndex, Int32 itemSubIndex, Int32 valueType, byte[] value);


        //
        //  Instance Reading
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBType")]
        public static extern Int32 sdaiGetADBType(Int32 ADB);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePathx")]
        public static extern void sdaiGetADBTypePath(Int32 ADB, Int32 typeNameNumber, out IntPtr path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(Int32 ADB, Int32 valueType, out Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(Int32 ADB, Int32 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(Int32 ADB, Int32 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern Int32 engiGetAggrElement(Int32 aggregate, Int32 elementIndex, Int32 valueType, out Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern Int32 engiGetAggrElement(Int32 aggregate, Int32 elementIndex, Int32 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern Int32 engiGetAggrElement(Int32 aggregate, Int32 elementIndex, Int32 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrType")]
        public static extern void engiGetAggrType(Int32 aggregate, ref Int32 aggragateType);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern Int32 sdaiGetAttr(Int32 instance, Int32 attribute, Int32 valueType, out Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern Int32 sdaiGetAttr(Int32 instance, Int32 attribute, Int32 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern Int32 sdaiGetAttr(Int32 instance, Int32 attribute, Int32 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int32 sdaiGetAttrBN(Int32 instance, string attributeName, Int32 valueType, out Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int32 sdaiGetAttrBN(Int32 instance, string attributeName, Int32 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
		public static extern Int32 sdaiGetAttrBN(Int32 instance, string attributeName, Int32 valueType, [MarshalAs(UnmanagedType.LPWStr)] out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int32 sdaiGetAttrBN(Int32 instance, byte[] attributeName, Int32 valueType, out Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int32 sdaiGetAttrBN(Int32 instance, byte[] attributeName, Int32 valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int32 sdaiGetAttrBN(Int32 instance, byte[] attributeName, Int32 valueType, out IntPtr value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern Int32 sdaiGetAttrDefinition(Int32 entity, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern Int32 sdaiGetAttrDefinition(Int32 entity, byte[] attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceType")]
        public static extern Int32 sdaiGetInstanceType(Int32 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetMemberCount")]
        public static extern Int32 sdaiGetMemberCount(Int32 aggregate);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsKindOf")]
        public static extern Int32 sdaiIsKindOf(Int32 instance, Int32 entity);


        //
        //  Instance Writing
        //


        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int32 list, Int32 valueType, ref Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int32 list, Int32 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int32 list, Int32 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int32 list, Int32 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int32 sdaiCreateADB(Int32 valueType, ref Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int32 sdaiCreateADB(Int32 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int32 sdaiCreateADB(Int32 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int32 sdaiCreateADB(Int32 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggr")]
        public static extern Int32 sdaiCreateAggr(Int32 instance, Int32 attribute);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern Int32 sdaiCreateAggrBN(Int32 instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern Int32 sdaiCreateAggrBN(Int32 instance, byte[] attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstance")]
        public static extern Int32 sdaiCreateInstance(Int32 model, Int32 entity);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern Int32 sdaiCreateInstanceBN(Int32 model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern Int32 sdaiCreateInstanceBN(Int32 model, byte[] entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteInstance")]
        public static extern void sdaiDeleteInstance(Int32 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(Int32 ADB, Int32 pathCount, string path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(Int32 ADB, Int32 pathCount, byte[] path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int32 instance, Int32 attribute, Int32 valueType, ref Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int32 instance, Int32 attribute, Int32 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int32 instance, Int32 attribute, Int32 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int32 instance, Int32 attribute, Int32 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int32 instance, string attributeName, Int32 valueType, ref Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int32 instance, string attributeName, Int32 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int32 instance, string attributeName, Int32 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int32 instance, string attributeName, Int32 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int32 instance, byte[] attributeName, Int32 valueType, ref Int32 value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int32 instance, byte[] attributeName, Int32 valueType, ref double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int32 instance, byte[] attributeName, Int32 valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int32 instance, byte[] attributeName, Int32 valueType, byte[] value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(Int32 instance, string comment);

        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(Int32 instance, byte[] comment);


        //
        //  Controling Calls
        //


        [DllImport(IFCEngineDLL, EntryPoint = "circleSegments")]
        public static extern void circleSegments(Int32 circles, Int32 smallCircles);

        [DllImport(IFCEngineDLL, EntryPoint = "cleanMemory")]
        public static extern void cleanMemory(Int32 model, Int32 mode);

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetP21Line")]
        public static extern Int32 internalGetP21Line(Int32 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetInstanceFromP21Line")]
        public static extern Int32 internalGetInstanceFromP21Line(Int32 model, Int32 P21Line);

        [DllImport(IFCEngineDLL, EntryPoint = "setStringUnicode")]
        public static extern Int32 setStringUnicode(Int32 unicode);


        //
        //  Geometry Interaction
        //


        [DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstance")]
        public static extern Int32 initializeModellingInstance(Int32 model, ref Int32 noVertices, ref Int32 noIndices, double scale, Int32 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern Int32 finalizeModelling(Int32 model, float[] vertices, Int32[] indices, Int32 FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern Int32 finalizeModelling(Int32 model, float[] vertices, Int64[] indices, Int32 FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern Int32 finalizeModelling(Int32 model, double[] vertices, Int32[] indices, Int32 FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern Int32 finalizeModelling(Int32 model, double[] vertices, Int64[] indices, Int32 FVF);

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceInModelling")]
        public static extern Int32 getInstanceInModelling(Int32 model, Int32 instance, Int32 mode, ref Int32 startVertex, ref Int32 startIndex, ref Int32 primitiveCount);

        [DllImport(IFCEngineDLL, EntryPoint = "setVertexOffset")]
        public static extern void setVertexOffset(Int32 model, double x, double y, double z);

        [DllImport(IFCEngineDLL, EntryPoint = "setFilter")]
        public static extern void setFilter(Int32 model, Int32 setting, Int32 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "setFormat")]
        public static extern void setFormat(Int32 model, Int32 setting, Int32 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceCnt")]
        public static extern Int32 getConceptualFaceCnt(Int32 instance);

        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceEx")]
        public static extern Int32 getConceptualFaceEx(Int32 instance, Int32 index, ref Int32 startIndexTriangles, ref Int32 noIndicesTriangles, ref Int32 startIndexLines, ref Int32 noIndicesLines, ref Int32 startIndexPoints, ref Int32 noIndicesPoints, ref Int32 startIndexFacesPolygons, ref Int32 noIndicesFacesPolygons, ref Int32 startIndexConceptualFacePolygons, ref Int32 noIndicesConceptualFacePolygons);

		[DllImport(IFCEngineDLL, EntryPoint = "GetInstanceClass")]
		public static extern Int64 GetInstanceClass(Int64 owlInstance);

		[DllImport(IFCEngineDLL, EntryPoint = "GetClassByName", CharSet = CharSet.Auto)]
		public static extern Int64 GetClassByName(Int64 model, string owlClassName);

		[DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName", CharSet = CharSet.Auto)]
		public static extern Int64 GetPropertyByName(Int64 model, string rdfPropertyName);

		[DllImport(IFCEngineDLL, EntryPoint = "GetObjectTypeProperty")]
		public static extern Int64 GetObjectTypeProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr value, ref Int64 card);

		[DllImport(IFCEngineDLL, EntryPoint = "GetDataTypeProperty")]
		public static extern Int64 GetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr value, ref Int64 card);

		[DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]
		public static extern Int64 CalculateInstance(Int64 owlInstance, ref Int64 vertexBufferSize, ref Int64 indexBufferSize, ref Int64 transformationBufferSize);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
        public static extern Int64 UpdateInstanceVertexBuffer(Int64 owlInstance, float[] vertexBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
        public static extern Int64 UpdateInstanceIndexBuffer(Int64 owlInstance, int[] indexBuffer);

        // Android
        /*
		 * Returns:
		 * 0 - Failed
		 * > 0 - OK; number of bytes
		 * < 0 - insufficient buffer; number of bytes needed
		 */
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		public static extern Int32 _sdaiGetAttrBNUnicode(Int32 instance, string attributeName, byte[] buffer, Int32 bufferLength);

		// Android
		public static Int32 sdaiGetAttrBNUnicode(Int32 instance, string attributeName, out string value)
		{
			value = string.Empty;

			byte[] buffer = new byte[4000];

			var bytesCount = _sdaiGetAttrBNUnicode (instance, attributeName, buffer, buffer.Length);
			if (bytesCount == 0) {
				// Failed
				return 0;
			}

			if (bytesCount < 0) {
				buffer = new byte[Math.Abs(bytesCount)];
			}

			bytesCount = _sdaiGetAttrBNUnicode (instance, attributeName, buffer, buffer.Length);
			if (bytesCount <= 0) {
				// Failed; internal error
				return 0;
			}

			value = Encoding.UTF32.GetString (buffer, 0, bytesCount);

			return bytesCount;
		}
    }

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

		public const int_t flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
		public const int_t flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
		public const int_t flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000
		public const int_t flagbit23 = 8388608;		// 2^^23   0000.0000..1000.0000  0000.0000..0000.0000

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

		// Android
		/*
		 * Returns:
		 * 0 - Failed
		 * > 0 - OK; number of bytes
		 * < 0 - insufficient buffer; number of bytes needed
		 */
		[DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		public static extern int_t _sdaiGetAttrBNUnicode(int_t instance, string attributeName, byte[] buffer, int_t bufferLength);

		// Android
		public static int_t sdaiGetAttrBNUnicode(int_t instance, string attributeName, out string value)
		{
			value = string.Empty;

			byte[] buffer = new byte[4000];

			var bytesCount = _sdaiGetAttrBNUnicode (instance, attributeName, buffer, buffer.Length);
			if (bytesCount == 0) {
				// Failed
				return 0;
			}

			if (bytesCount < 0) {
				buffer = new byte[Math.Abs(bytesCount)];
			}

			bytesCount = _sdaiGetAttrBNUnicode (instance, attributeName, buffer, buffer.Length);
			if (bytesCount <= 0) {
				// Failed; internal error
				return 0;
			}

			value = Encoding.UTF32.GetString (buffer, 0, (int)bytesCount);

			return bytesCount;
		}
	}
}
