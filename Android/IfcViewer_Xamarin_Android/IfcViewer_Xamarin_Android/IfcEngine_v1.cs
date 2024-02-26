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

		public const int_t sdaiADB           = 1;
        public const int_t sdaiAGGR          = sdaiADB + 1;
        public const int_t sdaiBINARY        = sdaiAGGR + 1;
        public const int_t sdaiBOOLEAN       = sdaiBINARY + 1;
        public const int_t sdaiENUM          = sdaiBOOLEAN + 1;
        public const int_t sdaiINSTANCE      = sdaiENUM + 1;
        public const int_t sdaiINTEGER       = sdaiINSTANCE + 1;
        public const int_t sdaiLOGICAL       = sdaiINTEGER + 1;
        public const int_t sdaiREAL          = sdaiLOGICAL + 1;
        public const int_t sdaiSTRING        = sdaiREAL + 1;
        public const int_t sdaiUNICODE       = sdaiSTRING + 1;
        public const int_t sdaiEXPRESSSTRING = sdaiUNICODE + 1;
        public const int_t engiGLOBALID      = sdaiEXPRESSSTRING + 1;

        public const Int64 OBJECTPROPERTY_TYPE             = 1;
        public const Int64 DATATYPEPROPERTY_TYPE_BOOLEAN   = 2;
        public const Int64 DATATYPEPROPERTY_TYPE_CHAR      = 3;
        public const Int64 DATATYPEPROPERTY_TYPE_INTEGER   = 4;
        public const Int64 DATATYPEPROPERTY_TYPE_DOUBLE    = 5;

        public const string IFCEngineDLL = @"libifcengine.so";

        //
        //  File IO API Calls
        //

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate Int64 ReadCallBackFunction(IntPtr value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void WriteCallBackFunction(IntPtr value, Int64 size);

		//
		//		sdaiCreateModelBN                           (http://rdf.bg/ifcdoc/CS64/sdaiCreateModelBN.html)
		//
		//	This function creates and empty model (we expect with a schema file given).
		//	Attributes repository and fileName will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern int_t sdaiCreateModelBN(int_t repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern int_t sdaiCreateModelBN(int_t repository, string fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern int_t sdaiCreateModelBN(int_t repository, byte[] fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern int_t sdaiCreateModelBN(int_t repository, byte[] fileName, byte[] schemaName);

		//
		//		sdaiCreateModelBNUnicode                    (http://rdf.bg/ifcdoc/CS64/sdaiCreateModelBNUnicode.html)
		//
		//	This function creates and empty model (we expect with a schema file given).
		//	Attributes repository and fileName will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern int_t sdaiCreateModelBNUnicode(int_t repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern int_t sdaiCreateModelBNUnicode(int_t repository, string fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern int_t sdaiCreateModelBNUnicode(int_t repository, byte[] fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern int_t sdaiCreateModelBNUnicode(int_t repository, byte[] fileName, byte[] schemaName);

		//
		//		sdaiOpenModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBN.html)
		//
		//	This function opens the model on location fileName.
		//	Attribute repository will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern int_t sdaiOpenModelBN(int_t repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern int_t sdaiOpenModelBN(int_t repository, string fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern int_t sdaiOpenModelBN(int_t repository, byte[] fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern int_t sdaiOpenModelBN(int_t repository, byte[] fileName, byte[] schemaName);

		//
		//		sdaiOpenModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBNUnicode.html)
		//
		//	This function opens the model on location fileName.
		//	Attribute repository will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern int_t sdaiOpenModelBNUnicode(int_t repository, string fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern int_t sdaiOpenModelBNUnicode(int_t repository, string fileName, byte[] schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern int_t sdaiOpenModelBNUnicode(int_t repository, byte[] fileName, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern int_t sdaiOpenModelBNUnicode(int_t repository, byte[] fileName, byte[] schemaName);

		//
		//		engiOpenModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiOpenModelByStream.html)
		//
		//	This function opens the model via a stream.
		//	Attribute repository will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByStream")]
        public static extern int_t engiOpenModelByStream(int_t repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByStream")]
        public static extern int_t engiOpenModelByStream(int_t repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, byte[] schemaName);

		//
		//		engiOpenModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiOpenModelByArray.html)
		//
		//	This function opens the model via an array.
		//	Attribute repository will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByArray")]
        public static extern int_t engiOpenModelByArray(int_t repository, byte[] content, int_t size, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByArray")]
        public static extern int_t engiOpenModelByArray(int_t repository, byte[] content, int_t size, byte[] schemaName);

		//
		//		sdaiSaveModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBN.html)
		//
		//	This function saves the model (char file name).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(int_t model, byte[] fileName);

		//
		//		sdaiSaveModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBNUnicode.html)
		//
		//	This function saves the model (wchar, i.e. Unicode file name).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]
        public static extern void sdaiSaveModelBNUnicode(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]
        public static extern void sdaiSaveModelBNUnicode(int_t model, byte[] fileName);

		//
		//		engiSaveModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiSaveModelByStream.html)
		//
		//	This function saves the model as an array.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiSaveModelByStream")]
        public static extern void engiSaveModelByStream(int_t model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, Int64 size);

		//
		//		engiSaveModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiSaveModelByArray.html)
		//
		//	This function saves the model as an array.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiSaveModelByArray")]
        public static extern void engiSaveModelByArray(int_t model, byte[] content, out int_t size);

		//
		//		sdaiSaveModelAsXmlBN                        (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBN.html)
		//
		//	This function saves the model as XML according to IFC2x3's way of XML serialization (char file name).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(int_t model, byte[] fileName);

		//
		//		sdaiSaveModelAsXmlBNUnicode                 (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBNUnicode.html)
		//
		//	This function saves the model as XML according to IFC2x3's way of XML serialization (wchar, i.e. Unicode file name).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]
        public static extern void sdaiSaveModelAsXmlBNUnicode(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]
        public static extern void sdaiSaveModelAsXmlBNUnicode(int_t model, byte[] fileName);

		//
		//		sdaiSaveModelAsSimpleXmlBN                  (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBN.html)
		//
		//	This function saves the model as XML according to IFC4's way of XML serialization (char file name).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(int_t model, byte[] fileName);

		//
		//		sdaiSaveModelAsSimpleXmlBNUnicode           (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBNUnicode.html)
		//
		//	This function saves the model as XML according to IFC4's way of XML serialization (wchar, i.e. Unicode file name).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]
        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]
        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(int_t model, byte[] fileName);

		//
		//		sdaiCloseModel                              (http://rdf.bg/ifcdoc/CS64/sdaiCloseModel.html)
		//
		//	This function closes the model. After this call no instance handles will be available including all
		//	handles referencing the geometry of this specific file, in default compilation the model itself will
		//	be known in the kernel, however known to be disabled. Calls containing the model reference will be
		//	protected from crashing when called.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCloseModel")]
        public static extern void sdaiCloseModel(int_t model);

		//
		//		setPrecisionDoubleExport                    (http://rdf.bg/ifcdoc/CS64/setPrecisionDoubleExport.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "setPrecisionDoubleExport")]
        public static extern void setPrecisionDoubleExport(int_t model, int_t precisionCap, int_t precisionRound, byte clean);

        //
        //  Schema Reading API Calls
        //

		//
		//		sdaiGetEntity                               (http://rdf.bg/ifcdoc/CS64/sdaiGetEntity.html)
		//
		//	This call retrieves a handle to an entity based on a given entity name.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern int_t sdaiGetEntity(int_t model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern int_t sdaiGetEntity(int_t model, byte[] entityName);

		//
		//		engiGetEntityArgument                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgument.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgument")]
        public static extern int_t engiGetEntityArgument(int_t entity, string argumentName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgument")]
        public static extern int_t engiGetEntityArgument(int_t entity, byte[] argumentName);

		//
		//		engiGetEntityArgumentIndex                  (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentIndex.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentIndex")]
        public static extern int_t engiGetEntityArgumentIndex(int_t entity, string argumentName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentIndex")]
        public static extern int_t engiGetEntityArgumentIndex(int_t entity, byte[] argumentName);

		//
		//		engiGetEntityArgumentName                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentName.html)
		//
		//	This call can be used to retrieve the name of the n-th argument of the given entity. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentName")]
        public static extern void engiGetEntityArgumentName(int_t entity, int_t index, int_t valueType, out IntPtr argumentName);

		//
		//		engiGetEntityArgumentType                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentType.html)
		//
		//	This call can be used to retrieve the type of the n-th argument of the given entity. In case of a select argument no relevant information is given by this call as it depends on the instance. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentType")]
        public static extern void engiGetEntityArgumentType(int_t entity, int_t index, out int_t argumentType);

		//
		//		engiGetEntityCount                          (http://rdf.bg/ifcdoc/CS64/engiGetEntityCount.html)
		//
		//	Returns the total number of entities within the loaded schema.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityCount")]
        public static extern int_t engiGetEntityCount(int_t model);

		//
		//		engiGetEntityElement                        (http://rdf.bg/ifcdoc/CS64/engiGetEntityElement.html)
		//
		//	This call returns a specific entity based on an index, the index needs to be 0 or higher but lower then the number of entities in the loaded schema.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityElement")]
        public static extern int_t engiGetEntityElement(int_t model, int_t index);

		//
		//		sdaiGetEntityExtent                         (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtent.html)
		//
		//	This call retrieves an aggregation that contains all instances of the entity given.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtent")]
        public static extern int_t sdaiGetEntityExtent(int_t model, int_t entity);

		//
		//		sdaiGetEntityExtentBN                       (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtentBN.html)
		//
		//	This call retrieves an aggregation that contains all instances of the entity given.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern int_t sdaiGetEntityExtentBN(int_t model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern int_t sdaiGetEntityExtentBN(int_t model, byte[] entityName);

		//
		//		engiGetEntityName                           (http://rdf.bg/ifcdoc/CS64/engiGetEntityName.html)
		//
		//	This call can be used to get the name of the given entity.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityName")]
        public static extern void engiGetEntityName(int_t entity, int_t valueType, out IntPtr entityName);

		//
		//		engiGetEntityNoArguments                    (http://rdf.bg/ifcdoc/CS64/engiGetEntityNoArguments.html)
		//
		//	This call returns the number of arguments, this includes the arguments of its (nested) parents and inverse argumnets.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityNoArguments")]
        public static extern int_t engiGetEntityNoArguments(int_t entity);

		//
		//		engiGetEntityParent                         (http://rdf.bg/ifcdoc/CS64/engiGetEntityParent.html)
		//
		//	Returns the direct parent entity, for example the parent of IfcObject is IfcObjectDefinition, of IfcObjectDefinition is IfcRoot and of IfcRoot is 0.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityParent")]
        public static extern int_t engiGetEntityParent(int_t entity);

		//
		//		engiGetAttrOptional                         (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptional.html)
		//
		//	This call can be used to check if an attribute is optional
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrOptional")]
        public static extern int_t engiGetAttrOptional(ref int_t attribute);

		//
		//		engiGetAttrOptionalBN                       (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptionalBN.html)
		//
		//	This call can be used to check if an attribute is optional
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrOptionalBN")]
        public static extern int_t engiGetAttrOptionalBN(int_t entity, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrOptionalBN")]
        public static extern int_t engiGetAttrOptionalBN(int_t entity, byte[] attributeName);

		//
		//		engiGetAttrInverse                          (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverse.html)
		//
		//	This call can be used to check if an attribute is an inverse relation
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrInverse")]
        public static extern int_t engiGetAttrInverse(ref int_t attribute);

		//
		//		engiGetAttrInverseBN                        (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverseBN.html)
		//
		//	This call can be used to check if an attribute is an inverse relation
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrInverseBN")]
        public static extern int_t engiGetAttrInverseBN(int_t entity, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrInverseBN")]
        public static extern int_t engiGetAttrInverseBN(int_t entity, byte[] attributeName);

		//
		//		engiGetEnumerationValue                     (http://rdf.bg/ifcdoc/CS64/engiGetEnumerationValue.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEnumerationValue")]
        public static extern void engiGetEnumerationValue(int_t attribute, int_t index, int_t valueType, out IntPtr enumerationValue);

		//
		//		engiGetEntityProperty                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityProperty.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityProperty")]
        public static extern void engiGetEntityProperty(int_t entity, int_t index, out IntPtr propertyName, out int_t optional, out int_t type, out int_t _array, out int_t set, out int_t list, out int_t bag, out int_t min, out int_t max, out int_t referenceEntity, out int_t inverse);

        //
        //  Instance Header API Calls
        //

		//
		//		SetSPFFHeader                               (http://rdf.bg/ifcdoc/CS64/SetSPFFHeader.html)
		//
		//	This call is an aggregate of several SetSPFFHeaderItem calls. In several cases the header can be set easily with this call. In case an argument is zero, this argument will not be updated, i.e. it will not be filled with 0.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(int_t model, string description, string implementationLevel, string name, string timeStamp, string author, string organization, string preprocessorVersion, string originatingSystem, string authorization, string fileSchema);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(int_t model, byte[] description, byte[] implementationLevel, byte[] name, byte[] timeStamp, byte[] author, byte[] organization, byte[] preprocessorVersion, byte[] originatingSystem, byte[] authorization, byte[] fileSchema);

		//
		//		SetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/SetSPFFHeaderItem.html)
		//
		//	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern int_t SetSPFFHeaderItem(int_t model, int_t itemIndex, int_t itemSubIndex, int_t valueType, string value);

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern int_t SetSPFFHeaderItem(int_t model, int_t itemIndex, int_t itemSubIndex, int_t valueType, byte[] value);

		//
		//		GetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItem.html)
		//
		//	This call can be used to read a specific header item, the source code example is larger to show and explain how this call can be used.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItem")]
        public static extern int_t GetSPFFHeaderItem(int_t model, int_t itemIndex, int_t itemSubIndex, int_t valueType, out IntPtr value);

		//
		//		GetSPFFHeaderItemUnicode                    (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItemUnicode.html)
		//
		//	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItemUnicode")]
        public static extern int_t GetSPFFHeaderItemUnicode(int_t model, int_t itemIndex, int_t itemSubIndex, string buffer, int_t bufferLength);

        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItemUnicode")]
        public static extern int_t GetSPFFHeaderItemUnicode(int_t model, int_t itemIndex, int_t itemSubIndex, byte[] buffer, int_t bufferLength);

        //
        //  Instance Reading API Calls
        //

		//
		//		sdaiGetADBType                              (http://rdf.bg/ifcdoc/CS64/sdaiGetADBType.html)
		//
		//	This call can be used to get the used type within this ADB type.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBType")]
        public static extern int_t sdaiGetADBType(ref int_t ADB);

		//
		//		sdaiGetADBTypePath                          (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePath.html)
		//
		//	This call can be used to get the path of an ADB type.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePath")]
        public static extern IntPtr sdaiGetADBTypePath(ref int_t ADB, int_t typeNameNumber);

		//
		//		sdaiGetADBTypePathx                         (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePathx.html)
		//
		//	This call is the same as sdaiGetADBTypePath, however can be used by porting to languages that have issues with returned char arrays.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePathx")]
        public static extern void sdaiGetADBTypePathx(ref int_t ADB, int_t typeNameNumber, out IntPtr path);

		//
		//		sdaiGetADBValue                             (http://rdf.bg/ifcdoc/CS64/sdaiGetADBValue.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(ref int_t ADB, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(ref int_t ADB, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(ref int_t ADB, int_t valueType, out IntPtr value);

		//
		//		engiGetAggrElement                          (http://rdf.bg/ifcdoc/CS64/engiGetAggrElement.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern int_t engiGetAggrElement(int_t aggregate, int_t elementIndex, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern int_t engiGetAggrElement(int_t aggregate, int_t elementIndex, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern int_t engiGetAggrElement(int_t aggregate, int_t elementIndex, int_t valueType, out IntPtr value);

		//
		//		engiGetAggrType                             (http://rdf.bg/ifcdoc/CS64/engiGetAggrType.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrType")]
        public static extern void engiGetAggrType(int_t aggregate, out int_t aggragateType);

		//
		//		engiGetAggrTypex                            (http://rdf.bg/ifcdoc/CS64/engiGetAggrTypex.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrTypex")]
        public static extern void engiGetAggrTypex(int_t aggregate, out int_t aggragateType);

		//
		//		sdaiGetAttr                                 (http://rdf.bg/ifcdoc/CS64/sdaiGetAttr.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern int_t sdaiGetAttr(int_t instance, int_t attribute, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern int_t sdaiGetAttr(int_t instance, int_t attribute, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern int_t sdaiGetAttr(int_t instance, int_t attribute, int_t valueType, out IntPtr value);

		//
		//		sdaiGetAttrBN                               (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBN.html)
		//
		//	...
		//
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

		//
		//		sdaiGetAttrBNUnicode                        (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBNUnicode.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode")]
        public static extern int_t sdaiGetAttrBNUnicode(int_t instance, string attributeName, string buffer, int_t bufferLength);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode")]
        public static extern int_t sdaiGetAttrBNUnicode(int_t instance, byte[] attributeName, byte[] buffer, int_t bufferLength);

		//
		//		sdaiGetStringAttrBN                         (http://rdf.bg/ifcdoc/CS64/sdaiGetStringAttrBN.html)
		//
		//	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiSTRING.
		//	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,
		//	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetStringAttrBN")]
        public static extern IntPtr sdaiGetStringAttrBN(int_t instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetStringAttrBN")]
        public static extern IntPtr sdaiGetStringAttrBN(int_t instance, byte[] attributeName);

		//
		//		sdaiGetInstanceAttrBN                       (http://rdf.bg/ifcdoc/CS64/sdaiGetInstanceAttrBN.html)
		//
		//	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiINSTANCE.
		//	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,
		//	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceAttrBN")]
        public static extern int_t sdaiGetInstanceAttrBN(int_t instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceAttrBN")]
        public static extern int_t sdaiGetInstanceAttrBN(int_t instance, byte[] attributeName);

		//
		//		sdaiGetAggregationAttrBN                    (http://rdf.bg/ifcdoc/CS64/sdaiGetAggregationAttrBN.html)
		//
		//	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiAGGR.
		//	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,
		//	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggregationAttrBN")]
        public static extern int_t sdaiGetAggregationAttrBN(int_t instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggregationAttrBN")]
        public static extern int_t sdaiGetAggregationAttrBN(int_t instance, byte[] attributeName);

		//
		//		sdaiGetAttrDefinition                       (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrDefinition.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern int_t sdaiGetAttrDefinition(int_t entity, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern int_t sdaiGetAttrDefinition(int_t entity, byte[] attributeName);

		//
		//		sdaiGetInstanceType                         (http://rdf.bg/ifcdoc/CS64/sdaiGetInstanceType.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceType")]
        public static extern int_t sdaiGetInstanceType(int_t instance);

		//
		//		sdaiGetMemberCount                          (http://rdf.bg/ifcdoc/CS64/sdaiGetMemberCount.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetMemberCount")]
        public static extern int_t sdaiGetMemberCount(int_t aggregate);

		//
		//		sdaiIsKindOf                                (http://rdf.bg/ifcdoc/CS64/sdaiIsKindOf.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsKindOf")]
        public static extern int_t sdaiIsKindOf(int_t instance, int_t entity);

		//
		//		engiGetAttrType                             (http://rdf.bg/ifcdoc/CS64/engiGetAttrType.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrType")]
        public static extern int_t engiGetAttrType(int_t instance, ref int_t attribute);

		//
		//		engiGetAttrTypeBN                           (http://rdf.bg/ifcdoc/CS64/engiGetAttrTypeBN.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrTypeBN")]
        public static extern int_t engiGetAttrTypeBN(int_t instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrTypeBN")]
        public static extern int_t engiGetAttrTypeBN(int_t instance, byte[] attributeName);

		//
		//		sdaiIsInstanceOf                            (http://rdf.bg/ifcdoc/CS64/sdaiIsInstanceOf.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsInstanceOf")]
        public static extern int_t sdaiIsInstanceOf(int_t instance, int_t entity);

		//
		//		sdaiIsInstanceOfBN                          (http://rdf.bg/ifcdoc/CS64/sdaiIsInstanceOfBN.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsInstanceOfBN")]
        public static extern int_t sdaiIsInstanceOfBN(int_t instance, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsInstanceOfBN")]
        public static extern int_t sdaiIsInstanceOfBN(int_t instance, byte[] entityName);

		//
		//		engiValidateAttr                            (http://rdf.bg/ifcdoc/CS64/engiValidateAttr.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiValidateAttr")]
        public static extern int_t engiValidateAttr(int_t instance, ref int_t attribute);

		//
		//		engiValidateAttrBN                          (http://rdf.bg/ifcdoc/CS64/engiValidateAttrBN.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiValidateAttrBN")]
        public static extern int_t engiValidateAttrBN(int_t instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "engiValidateAttrBN")]
        public static extern int_t engiValidateAttrBN(int_t instance, byte[] attributeName);

		//
		//		sdaiCreateInstanceEI                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceEI.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceEI")]
        public static extern int_t sdaiCreateInstanceEI(int_t model, int_t entity, int_t express_id);

		//
		//		sdaiCreateInstanceBNEI                      (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceBNEI.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBNEI")]
        public static extern int_t sdaiCreateInstanceBNEI(int_t model, string entityName, int_t express_id);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBNEI")]
        public static extern int_t sdaiCreateInstanceBNEI(int_t model, byte[] entityName, int_t express_id);

        //
        //  Instance Writing API Calls
        //

		//
		//		sdaiPrepend                                 (http://rdf.bg/ifcdoc/CS64/sdaiPrepend.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrepend")]
        public static extern void sdaiPrepend(int_t list, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrepend")]
        public static extern void sdaiPrepend(int_t list, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrepend")]
        public static extern void sdaiPrepend(int_t list, int_t valueType, out IntPtr value);

		//
		//		sdaiAppend                                  (http://rdf.bg/ifcdoc/CS64/sdaiAppend.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(int_t list, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(int_t list, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(int_t list, int_t valueType, out IntPtr value);

		//
		//		engiAppend                                  (http://rdf.bg/ifcdoc/CS64/engiAppend.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiAppend")]
        public static extern void engiAppend(int_t list, int_t valueType, out IntPtr values, int_t card);

		//
		//		sdaiCreateADB                               (http://rdf.bg/ifcdoc/CS64/sdaiCreateADB.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern int_t sdaiCreateADB(int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern int_t sdaiCreateADB(int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern int_t sdaiCreateADB(int_t valueType, out IntPtr value);

		//
		//		sdaiCreateAggr                              (http://rdf.bg/ifcdoc/CS64/sdaiCreateAggr.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggr")]
        public static extern int_t sdaiCreateAggr(int_t instance, ref int_t attribute);

		//
		//		sdaiCreateAggrBN                            (http://rdf.bg/ifcdoc/CS64/sdaiCreateAggrBN.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern int_t sdaiCreateAggrBN(int_t instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern int_t sdaiCreateAggrBN(int_t instance, byte[] attributeName);

		//
		//		sdaiCreateNestedAggr                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateNestedAggr.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateNestedAggr")]
        public static extern int_t sdaiCreateNestedAggr(out int_t aggr);

		//
		//		sdaiCreateInstance                          (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstance.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstance")]
        public static extern int_t sdaiCreateInstance(int_t model, int_t entity);

		//
		//		sdaiCreateInstanceBN                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceBN.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern int_t sdaiCreateInstanceBN(int_t model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern int_t sdaiCreateInstanceBN(int_t model, byte[] entityName);

		//
		//		sdaiDeleteInstance                          (http://rdf.bg/ifcdoc/CS64/sdaiDeleteInstance.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteInstance")]
        public static extern void sdaiDeleteInstance(int_t instance);

		//
		//		sdaiPutADBTypePath                          (http://rdf.bg/ifcdoc/CS64/sdaiPutADBTypePath.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(string ADB, int_t pathCount, string path);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(byte[] ADB, int_t pathCount, byte[] path);

		//
		//		sdaiPutAttr                                 (http://rdf.bg/ifcdoc/CS64/sdaiPutAttr.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(int_t instance, ref int_t attribute, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(int_t instance, ref int_t attribute, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(int_t instance, ref int_t attribute, int_t valueType, out IntPtr value);

		//
		//		sdaiPutAttrBN                               (http://rdf.bg/ifcdoc/CS64/sdaiPutAttrBN.html)
		//
		//	...
		//
		[DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
		public static extern void sdaiPutAttrBN(int_t instance, string attributeName, int_t valueType, int_t value);

		[DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
		public static extern void sdaiPutAttrBN(int_t instance, string attributeName, int_t valueType, ref double value);

		[DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
		public static extern void sdaiPutAttrBN(int_t instance, string attributeName, int_t valueType, ref IntPtr value);

		[DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
		public static extern void sdaiPutAttrBN(int_t instance, byte[] attributeName, int_t valueType, int_t value);

		[DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
		public static extern void sdaiPutAttrBN(int_t instance, byte[] attributeName, int_t valueType, ref double value);

		[DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]
		public static extern void sdaiPutAttrBN(int_t instance, byte[] attributeName, int_t valueType, ref IntPtr value);

		//
		//		engiSetComment                              (http://rdf.bg/ifcdoc/CS64/engiSetComment.html)
		//
		//	This call can be used to add a comment to an instance when exporting the content. The comment is available in the exported/saved IFC file.
		//
		[DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(int_t instance, string comment);

        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(int_t instance, byte[] comment);

		//
		//		engiGetInstanceLocalId                      (http://rdf.bg/ifcdoc/CS64/engiGetInstanceLocalId.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceLocalId")]
        public static extern int_t engiGetInstanceLocalId(int_t instance);

		//
		//		sdaiTestAttr                                (http://rdf.bg/ifcdoc/CS64/sdaiTestAttr.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiTestAttr")]
        public static extern int_t sdaiTestAttr(int_t instance, ref int_t attribute);

		//
		//		sdaiTestAttrBN                              (http://rdf.bg/ifcdoc/CS64/sdaiTestAttrBN.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiTestAttrBN")]
        public static extern int_t sdaiTestAttrBN(int_t instance, string attributeName);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiTestAttrBN")]
        public static extern int_t sdaiTestAttrBN(int_t instance, byte[] attributeName);

		//
		//		engiGetInstanceClassInfo                    (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfo.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceClassInfo")]
        public static extern IntPtr engiGetInstanceClassInfo(int_t instance);

		//
		//		engiGetInstanceClassInfoUC                  (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfoUC.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceClassInfoUC")]
        public static extern IntPtr engiGetInstanceClassInfoUC(int_t instance);

		//
		//		engiGetInstanceClassInfoEx                  (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfoEx.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceClassInfoEx")]
        public static extern void engiGetInstanceClassInfoEx(int_t instance, out IntPtr classInfo);

		//
		//		engiGetInstanceMetaInfo                     (http://rdf.bg/ifcdoc/CS64/engiGetInstanceMetaInfo.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceMetaInfo")]
        public static extern int_t engiGetInstanceMetaInfo(out int_t instance, out int_t localId, out IntPtr entityName, out IntPtr entityNameUC);

        //
        //  Controling API Calls
        //

		//
		//		circleSegments                              (http://rdf.bg/ifcdoc/CS64/circleSegments.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "circleSegments")]
        public static extern void circleSegments(int_t circles, int_t smallCircles);

		//
		//		setMaximumSegmentationLength                (http://rdf.bg/ifcdoc/CS64/setMaximumSegmentationLength.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "setMaximumSegmentationLength")]
        public static extern void setMaximumSegmentationLength(int_t model, double length);

		//
		//		getUnitConversionFactor                     (http://rdf.bg/ifcdoc/CS64/getUnitConversionFactor.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "getUnitConversionFactor")]
        public static extern double getUnitConversionFactor(int_t model, string unitType, out IntPtr unitPrefix, out IntPtr unitName, out IntPtr SIUnitName);

        [DllImport(IFCEngineDLL, EntryPoint = "getUnitConversionFactor")]
        public static extern double getUnitConversionFactor(int_t model, byte[] unitType, out IntPtr unitPrefix, out IntPtr unitName, out IntPtr SIUnitName);

		//
		//		setBRepProperties                           (http://rdf.bg/ifcdoc/CS64/setBRepProperties.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "setBRepProperties")]
        public static extern void setBRepProperties(int_t model, Int64 consistencyCheck, double fraction, double epsilon, int_t maxVerticesSize);

		//
		//		cleanMemory                                 (http://rdf.bg/ifcdoc/CS64/cleanMemory.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "cleanMemory")]
        public static extern void cleanMemory(int_t model, int_t mode);

		//
		//		internalGetP21Line                          (http://rdf.bg/ifcdoc/CS64/internalGetP21Line.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "internalGetP21Line")]
        public static extern int_t internalGetP21Line(int_t instance);

		//
		//		internalGetInstanceFromP21Line              (http://rdf.bg/ifcdoc/CS64/internalGetInstanceFromP21Line.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "internalGetInstanceFromP21Line")]
        public static extern int_t internalGetInstanceFromP21Line(int_t model, int_t P21Line);

		//
		//		internalGetXMLID                            (http://rdf.bg/ifcdoc/CS64/internalGetXMLID.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "internalGetXMLID")]
        public static extern void internalGetXMLID(int_t instance, out IntPtr XMLID);

		//
		//		setStringUnicode                            (http://rdf.bg/ifcdoc/CS64/setStringUnicode.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "setStringUnicode")]
        public static extern int_t setStringUnicode(int_t unicode);

		//
		//		setFilter                                   (http://rdf.bg/ifcdoc/CS64/setFilter.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "setFilter")]
        public static extern void setFilter(int_t model, int_t setting, int_t mask);

		//
		//		getFilter                                   (http://rdf.bg/ifcdoc/CS64/getFilter.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "getFilter")]
        public static extern int_t getFilter(int_t model, int_t mask);

        //
        //  Uncategorized API Calls
        //

		//
		//		xxxxGetEntityAndSubTypesExtent              (http://rdf.bg/ifcdoc/CS64/xxxxGetEntityAndSubTypesExtent.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtent")]
        public static extern int_t xxxxGetEntityAndSubTypesExtent(int_t model, int_t entity);

		//
		//		xxxxGetEntityAndSubTypesExtentBN            (http://rdf.bg/ifcdoc/CS64/xxxxGetEntityAndSubTypesExtentBN.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtentBN")]
        public static extern int_t xxxxGetEntityAndSubTypesExtentBN(int_t model, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtentBN")]
        public static extern int_t xxxxGetEntityAndSubTypesExtentBN(int_t model, byte[] entityName);

		//
		//		xxxxGetInstancesUsing                       (http://rdf.bg/ifcdoc/CS64/xxxxGetInstancesUsing.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetInstancesUsing")]
        public static extern int_t xxxxGetInstancesUsing(int_t instance);

		//
		//		xxxxDeleteFromAggregation                   (http://rdf.bg/ifcdoc/CS64/xxxxDeleteFromAggregation.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "xxxxDeleteFromAggregation")]
        public static extern int_t xxxxDeleteFromAggregation(int_t instance, out int_t aggregate, int_t elementIndex);

		//
		//		xxxxGetAttrDefinitionByValue                (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrDefinitionByValue.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]
        public static extern int_t xxxxGetAttrDefinitionByValue(int_t instance, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]
        public static extern int_t xxxxGetAttrDefinitionByValue(int_t instance, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]
        public static extern int_t xxxxGetAttrDefinitionByValue(int_t instance, out IntPtr value);

		//
		//		xxxxGetAttrNameByIndex                      (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrNameByIndex.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrNameByIndex")]
        public static extern void xxxxGetAttrNameByIndex(int_t instance, int_t index, out IntPtr name);

		//
		//		iterateOverInstances                        (http://rdf.bg/ifcdoc/CS64/iterateOverInstances.html)
		//
		//	This function interates over all available instances loaded in memory, it is the fastest way to find all instances.
		//	Argument entity and entityName are both optional and if non-zero are filled with respectively the entity handle and entity name as char array.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "iterateOverInstances")]
        public static extern int_t iterateOverInstances(int_t model, int_t instance, out int_t entity, string entityName);

        [DllImport(IFCEngineDLL, EntryPoint = "iterateOverInstances")]
        public static extern int_t iterateOverInstances(int_t model, int_t instance, out int_t entity, byte[] entityName);

		//
		//		iterateOverProperties                       (http://rdf.bg/ifcdoc/CS64/iterateOverProperties.html)
		//
		//	This function iterated over all available attributes of a specific given entity.
		//	This call is typically used in combination with iterateOverInstances(..).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "iterateOverProperties")]
        public static extern int_t iterateOverProperties(int_t entity, int_t index);

		//
		//		sdaiGetAggrByIterator                       (http://rdf.bg/ifcdoc/CS64/sdaiGetAggrByIterator.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]
        public static extern int_t sdaiGetAggrByIterator(int_t iterator, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]
        public static extern int_t sdaiGetAggrByIterator(int_t iterator, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]
        public static extern int_t sdaiGetAggrByIterator(int_t iterator, int_t valueType, out IntPtr value);

		//
		//		sdaiPutAggrByIterator                       (http://rdf.bg/ifcdoc/CS64/sdaiPutAggrByIterator.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]
        public static extern void sdaiPutAggrByIterator(int_t iterator, int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]
        public static extern void sdaiPutAggrByIterator(int_t iterator, int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]
        public static extern void sdaiPutAggrByIterator(int_t iterator, int_t valueType, out IntPtr value);

		//
		//		internalSetLink                             (http://rdf.bg/ifcdoc/CS64/internalSetLink.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "internalSetLink")]
        public static extern void internalSetLink(int_t instance, string attributeName, int_t linked_id);

        [DllImport(IFCEngineDLL, EntryPoint = "internalSetLink")]
        public static extern void internalSetLink(int_t instance, byte[] attributeName, int_t linked_id);

		//
		//		internalAddAggrLink                         (http://rdf.bg/ifcdoc/CS64/internalAddAggrLink.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "internalAddAggrLink")]
        public static extern void internalAddAggrLink(int_t list, int_t linked_id);

		//
		//		engiGetNotReferedAggr                       (http://rdf.bg/ifcdoc/CS64/engiGetNotReferedAggr.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetNotReferedAggr")]
        public static extern void engiGetNotReferedAggr(int_t model, out int_t value);

		//
		//		engiGetAttributeAggr                        (http://rdf.bg/ifcdoc/CS64/engiGetAttributeAggr.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttributeAggr")]
        public static extern void engiGetAttributeAggr(int_t instance, out int_t value);

		//
		//		engiGetAggrUnknownElement                   (http://rdf.bg/ifcdoc/CS64/engiGetAggrUnknownElement.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]
        public static extern void engiGetAggrUnknownElement(out int_t aggregate, int_t elementIndex, out int_t valueType, out int_t value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]
        public static extern void engiGetAggrUnknownElement(out int_t aggregate, int_t elementIndex, out int_t valueType, out double value);

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]
        public static extern void engiGetAggrUnknownElement(out int_t aggregate, int_t elementIndex, out int_t valueType, out IntPtr value);

		//
		//		sdaiErrorQuery                              (http://rdf.bg/ifcdoc/CS64/sdaiErrorQuery.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiErrorQuery")]
        public static extern int_t sdaiErrorQuery();

        //
        //  Geometry Kernel related API Calls
        //

		//
		//		owlGetModel                                 (http://rdf.bg/ifcdoc/CS64/owlGetModel.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "owlGetModel")]
        public static extern void owlGetModel(int_t model, out Int64 owlModel);

		//
		//		owlGetInstance                              (http://rdf.bg/ifcdoc/CS64/owlGetInstance.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "owlGetInstance")]
        public static extern void owlGetInstance(int_t model, int_t instance, out Int64 owlInstance);

		//
		//		owlBuildInstance                            (http://rdf.bg/ifcdoc/CS64/owlBuildInstance.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "owlBuildInstance")]
        public static extern void owlBuildInstance(int_t model, int_t instance, out Int64 owlInstance);

		//
		//		owlBuildInstances                           (http://rdf.bg/ifcdoc/CS64/owlBuildInstances.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "owlBuildInstances")]
        public static extern void owlBuildInstances(int_t model, int_t instance, out Int64 owlInstanceComplete, out Int64 owlInstanceSolids, out Int64 owlInstanceVoids);

		//
		//		owlGetMappedItem                            (http://rdf.bg/ifcdoc/CS64/owlGetMappedItem.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "owlGetMappedItem")]
        public static extern void owlGetMappedItem(int_t model, int_t instance, out Int64 owlInstance, out double transformationMatrix);

		//
		//		getInstanceDerivedPropertiesInModelling     (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedPropertiesInModelling.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceDerivedPropertiesInModelling")]
        public static extern int_t getInstanceDerivedPropertiesInModelling(int_t model, int_t instance, out double height, out double width, out double thickness);

		//
		//		getInstanceDerivedBoundingBox               (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedBoundingBox.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceDerivedBoundingBox")]
        public static extern int_t getInstanceDerivedBoundingBox(int_t model, int_t instance, out double Ox, out double Oy, out double Oz, out double Vx, out double Vy, out double Vz);

		//
		//		getInstanceTransformationMatrix             (http://rdf.bg/ifcdoc/CS64/getInstanceTransformationMatrix.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceTransformationMatrix")]
        public static extern int_t getInstanceTransformationMatrix(int_t model, int_t instance, out double _11, out double _12, out double _13, out double _14, out double _21, out double _22, out double _23, out double _24, out double _31, out double _32, out double _33, out double _34, out double _41, out double _42, out double _43, out double _44);

		//
		//		getInstanceDerivedTransformationMatrix      (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedTransformationMatrix.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceDerivedTransformationMatrix")]
        public static extern int_t getInstanceDerivedTransformationMatrix(int_t model, int_t instance, out double _11, out double _12, out double _13, out double _14, out double _21, out double _22, out double _23, out double _24, out double _31, out double _32, out double _33, out double _34, out double _41, out double _42, out double _43, out double _44);

		//
		//		internalGetBoundingBox                      (http://rdf.bg/ifcdoc/CS64/internalGetBoundingBox.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "internalGetBoundingBox")]
        public static extern int_t internalGetBoundingBox(int_t model, int_t instance);

		//
		//		internalGetCenter                           (http://rdf.bg/ifcdoc/CS64/internalGetCenter.html)
		//
		//	...
		//
        [DllImport(IFCEngineDLL, EntryPoint = "internalGetCenter")]
        public static extern int_t internalGetCenter(int_t model, int_t instance);

        //
        //  Deprecated API Calls (GENERIC)
        //

		//
		//		engiAttrIsInverse                           (http://rdf.bg/ifcdoc/CS64/engiAttrIsInverse.html)
		//
		//	This call is deprecated, please use call engiAttrIsInverse.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "engiAttrIsInverse")]
        public static extern int_t engiAttrIsInverse(ref int_t attribute);

		//
		//		xxxxOpenModelByStream                       (http://rdf.bg/ifcdoc/CS64/xxxxOpenModelByStream.html)
		//
		//	This call is deprecated, please use call engiOpenModelByStream.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "xxxxOpenModelByStream")]
        public static extern int_t xxxxOpenModelByStream(int_t repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, string schemaName);

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxOpenModelByStream")]
        public static extern int_t xxxxOpenModelByStream(int_t repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, byte[] schemaName);

		//
		//		sdaiCreateIterator                          (http://rdf.bg/ifcdoc/CS64/sdaiCreateIterator.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateIterator")]
        public static extern int_t sdaiCreateIterator(ref int_t aggregate);

		//
		//		sdaiDeleteIterator                          (http://rdf.bg/ifcdoc/CS64/sdaiDeleteIterator.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteIterator")]
        public static extern void sdaiDeleteIterator(int_t iterator);

		//
		//		sdaiBeginning                               (http://rdf.bg/ifcdoc/CS64/sdaiBeginning.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiBeginning")]
        public static extern void sdaiBeginning(int_t iterator);

		//
		//		sdaiNext                                    (http://rdf.bg/ifcdoc/CS64/sdaiNext.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiNext")]
        public static extern int_t sdaiNext(int_t iterator);

		//
		//		sdaiPrevious                                (http://rdf.bg/ifcdoc/CS64/sdaiPrevious.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrevious")]
        public static extern int_t sdaiPrevious(int_t iterator);

		//
		//		sdaiEnd                                     (http://rdf.bg/ifcdoc/CS64/sdaiEnd.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiEnd")]
        public static extern void sdaiEnd(int_t iterator);

		//
		//		sdaiplusGetAggregationType                  (http://rdf.bg/ifcdoc/CS64/sdaiplusGetAggregationType.html)
		//
		//	This call is deprecated, please use call ....
		//
        [DllImport(IFCEngineDLL, EntryPoint = "sdaiplusGetAggregationType")]
        public static extern int_t sdaiplusGetAggregationType(int_t instance, out int_t aggregation);

		//
		//		xxxxGetAttrTypeBN                           (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrTypeBN.html)
		//
		//	This call is deprecated, please use calls engiGetAttrTypeBN(..).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrTypeBN")]
        public static extern int_t xxxxGetAttrTypeBN(int_t instance, string attributeName, out IntPtr attributeType);

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrTypeBN")]
        public static extern int_t xxxxGetAttrTypeBN(int_t instance, byte[] attributeName, out IntPtr attributeType);

        //
        //  Deprecated API Calls (GEOMETRY)
        //

		//
		//		initializeModellingInstance                 (http://rdf.bg/ifcdoc/CS64/initializeModellingInstance.html)
		//
		//	This call is deprecated, please use call CalculateInstance().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstance")]
        public static extern int_t initializeModellingInstance(int_t model, out int_t noVertices, out int_t noIndices, double scale, int_t instance);

		//
		//		finalizeModelling                           (http://rdf.bg/ifcdoc/CS64/finalizeModelling.html)
		//
		//	This call is deprecated, please use call UpdateInstanceVertexBuffer() and UpdateInstanceIndexBuffer().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern int_t finalizeModelling(int_t model, float[] vertices, out int_t indices, int_t FVF);

		//
		//		getInstanceInModelling                      (http://rdf.bg/ifcdoc/CS64/getInstanceInModelling.html)
		//
		//	This call is deprecated, there is no direct / easy replacement although the functionality is present. If you still use this call please contact RDF to find a solution together.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceInModelling")]
        public static extern int_t getInstanceInModelling(int_t model, int_t instance, int_t mode, out int_t startVertex, out int_t startIndex, out int_t primitiveCount);

		//
		//		setVertexOffset                             (http://rdf.bg/ifcdoc/CS64/setVertexOffset.html)
		//
		//	This call is deprecated, please use call SetVertexBufferOffset().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "setVertexOffset")]
        public static extern void setVertexOffset(int_t model, double x, double y, double z);

		//
		//		setFormat                                   (http://rdf.bg/ifcdoc/CS64/setFormat.html)
		//
		//	This call is deprecated, please use call SetFormat().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "setFormat")]
        public static extern void setFormat(int_t model, int_t setting, int_t mask);

		//
		//		getConceptualFaceCnt                        (http://rdf.bg/ifcdoc/CS64/getConceptualFaceCnt.html)
		//
		//	This call is deprecated, please use call GetConceptualFaceCnt().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceCnt")]
        public static extern int_t getConceptualFaceCnt(int_t instance);

		//
		//		getConceptualFaceEx                         (http://rdf.bg/ifcdoc/CS64/getConceptualFaceEx.html)
		//
		//	This call is deprecated, please use call GetConceptualFaceEx().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceEx")]
        public static extern int_t getConceptualFaceEx(int_t instance, int_t index, out int_t startIndexTriangles, out int_t noIndicesTriangles, out int_t startIndexLines, out int_t noIndicesLines, out int_t startIndexPoints, out int_t noIndicesPoints, out int_t startIndexFacesPolygons, out int_t noIndicesFacesPolygons, out int_t startIndexConceptualFacePolygons, out int_t noIndicesConceptualFacePolygons);

		//
		//		createGeometryConversion                    (http://rdf.bg/ifcdoc/CS64/createGeometryConversion.html)
		//
		//	This call is deprecated, please use call ... .
		//
        [DllImport(IFCEngineDLL, EntryPoint = "createGeometryConversion")]
        public static extern void createGeometryConversion(int_t instance, out Int64 owlInstance);

		//
		//		convertInstance                             (http://rdf.bg/ifcdoc/CS64/convertInstance.html)
		//
		//	This call is deprecated, please use call ... .
		//
        [DllImport(IFCEngineDLL, EntryPoint = "convertInstance")]
        public static extern void convertInstance(int_t instance);

		//
		//		initializeModellingInstanceEx               (http://rdf.bg/ifcdoc/CS64/initializeModellingInstanceEx.html)
		//
		//	This call is deprecated, please use call CalculateInstance().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstanceEx")]
        public static extern int_t initializeModellingInstanceEx(int_t model, out int_t noVertices, out int_t noIndices, double scale, int_t instance, int_t instanceList);

		//
		//		exportModellingAsOWL                        (http://rdf.bg/ifcdoc/CS64/exportModellingAsOWL.html)
		//
		//	This call is deprecated, please contact us if you use this call.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "exportModellingAsOWL")]
        public static extern void exportModellingAsOWL(int_t model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "exportModellingAsOWL")]
        public static extern void exportModellingAsOWL(int_t model, byte[] fileName);


        //
        //  Meta information API Calls
        //

		//
		//		GetRevision                                 (http://rdf.bg/gkdoc/CS64/GetRevision.html)
		//
		//	Returns the revision number.
		//	The timeStamp is generated by the SVN system used during development.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetRevision")]
        public static extern Int64 GetRevision(out IntPtr timeStamp);

		//
		//		GetRevisionW                                (http://rdf.bg/gkdoc/CS64/GetRevisionW.html)
		//
		//	Returns the revision number.
		//	The timeStamp is generated by the SVN system used during development.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetRevisionW")]
        public static extern Int64 GetRevisionW(out IntPtr timeStamp);

		//
		//		GetProtection                               (http://rdf.bg/gkdoc/CS64/GetProtection.html)
		//
		//	This call is required to be called to enable the DLL to work if protection is active.
		//
		//	Returns the number of days (incl. this one) that this version is still active or 0 if no protection is embedded.
		//	In case no days are left and protection is active this call will return -1.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetProtection")]
        public static extern Int64 GetProtection();

		//
		//		GetEnvironment                              (http://rdf.bg/gkdoc/CS64/GetEnvironment.html)
		//
		//	Returns the revision number similar to the call GetRevision.
		//	The environment variables will show known environment variables
		//	and if they are set, for example environment variables ABC known
		//	and unset and DEF as well as GHI known and set:
		//		environmentVariables = "ABC:F;DEF:T;GHI:T"
		//	Development variables are depending on the build environment
		//	As an example in windows systems where Visual Studio is used:
		//		developmentVariables = "...."
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetEnvironment")]
        public static extern Int64 GetEnvironment(out IntPtr environmentVariables, out IntPtr developmentVariables);

		//
		//		GetEnvironmentW                             (http://rdf.bg/gkdoc/CS64/GetEnvironmentW.html)
		//
		//	Returns the revision number similar to the call GetRevision[W].
		//	The environment variables will show known environment variables
		//	and if they are set, for example environment variables ABC known
		//	and unset and DEF as well as GHI known and set:
		//		environmentVariables = "ABC:F;DEF:T;GHI:T"
		//	Development variables are depending on the build environment
		//	As an example in windows systems where Visual Studio is used:
		//		developmentVariables = "...."
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetEnvironmentW")]
        public static extern Int64 GetEnvironmentW(out IntPtr environmentVariables, out IntPtr developmentVariables);

		//
		//		SetAssertionFile                            (http://rdf.bg/gkdoc/CS64/SetAssertionFile.html)
		//
		//	This function sets the file location where internal assertions should be written to.
		//	If the filename is not set (default) many internal control procedures are not executed
		//	and the code will be faster.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetAssertionFile")]
        public static extern void SetAssertionFile(string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "SetAssertionFile")]
        public static extern void SetAssertionFile(byte[] fileName);

		//
		//		SetAssertionFileW                           (http://rdf.bg/gkdoc/CS64/SetAssertionFileW.html)
		//
		//	This function sets the file location where internal assertions should be written to.
		//	If the filename is not set (default) many internal control procedures are not executed
		//	and the code will be faster.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetAssertionFileW")]
        public static extern void SetAssertionFileW(string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "SetAssertionFileW")]
        public static extern void SetAssertionFileW(byte[] fileName);

		//
		//		GetAssertionFile                            (http://rdf.bg/gkdoc/CS64/GetAssertionFile.html)
		//
		//	This function gets the file location as stored/set internally where internal assertions should be written to.
		//	It works independent if the file location is set through SetAssertionFile() or SetAssertionFileW().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetAssertionFile")]
        public static extern void GetAssertionFile(out IntPtr fileName);

		//
		//		GetAssertionFileW                           (http://rdf.bg/gkdoc/CS64/GetAssertionFileW.html)
		//
		//	This function gets the file location as stored/set internally where internal assertions should be written to.
		//	It works independent if the file location is set through SetAssertionFile() or SetAssertionFileW().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetAssertionFileW")]
        public static extern void GetAssertionFileW(out IntPtr fileName);

		//
		//		SetCharacterSerialization                   (http://rdf.bg/gkdoc/CS64/SetCharacterSerialization.html)
		//
		//	This call defines how characters for names, strings will be serializaed and how
		//	they are expected to be serialized. An exception are the Open / Import / Save calls,
		//	these calls have a fixed way of serialization of path / file names.
		//
		//	If the encoding value is non-zero the following values are possible (if zero encoding is kept as defined)
		//		 32 [default]	encoding ignored
		//		 64				encoding Windows 1250
		//		 65				encoding Windows 1251
		//		 66				encoding Windows 1252
		//		 67				encoding Windows 1253
		//		 68				encoding Windows 1254
		//		 69				encoding Windows 1255
		//		 70				encoding Windows 1256
		//		 71				encoding Windows 1257
		//		 72				encoding Windows 1258
		//		128				encoding ISO8859 1
		//		129				encoding ISO8859 2
		//		130				encoding ISO8859 3
		//		131				encoding ISO8859 4
		//		132				encoding ISO8859 5
		//		133				encoding ISO8859 6
		//		134				encoding ISO8859 7
		//		135				encoding ISO8859 8
		//		136				encoding ISO8859 9
		//		137				encoding ISO8859 10
		//		138				encoding ISO8859 11
		//						encoding ISO8859 12 => does not exist
		//		140				encoding ISO8859 13
		//		141				encoding ISO8859 14
		//		142				encoding ISO8859 15
		//		143				encoding ISO8859 16
		//		160				encoding MACINTOSH CENTRAL EUROPEAN
		//		192				encoding SHIFT JIS X 213
		//
		//	The wcharBitSizeOverride value overrides the OS based size of wchar_t, the following values can be applied:
		//		0			wcharBitSizeOverride is ignored, override is not changed
		//		16			wchar_t interpreted as being 2 bytes wide (size of wchar_t in bits)
		//		32			wchar_t interpreted as being 4 bytes wide (size of wchar_t in bits)
		//		Any other value will reset the override and wchar_t will follow the OS based size of wchar_t
		//	Note: this setting is independent from the model, this call can also be called without a model defined.
		//
		//	The ascii value defines
		//		true [default]	8 bit serializatiom (size of char returned in bits)
		//		false			16/32 bit serialization (depending on the operating system, i.e. sizeof of wchar_t returned in number of bits)
		//	Note: this setting is model-dependent and requires a model present to have any effect.
		//
		//	The return value is the size of a single character in bits, i.e. 1 byte is 8 bits, the value for a wchar_t can be 16 or 32 depending on settings and operating system
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetCharacterSerialization")]
        public static extern Int64 SetCharacterSerialization(Int64 model, Int64 encoding, Int64 wcharBitSizeOverride, byte ascii);

		//
		//		GetCharacterSerialization                   (http://rdf.bg/gkdoc/CS64/GetCharacterSerialization.html)
		//
		//	This call retrieves the values as set by 
		//
		//	The returns the size of a single character in bits, i.e. 1 byte is 8 bits, this can be 8, 16 or 32 depending on settings and operating system
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetCharacterSerialization")]
        public static extern Int64 GetCharacterSerialization(Int64 model, out Int64 encoding, out byte ascii);

		//
		//		AbortModel                                  (http://rdf.bg/gkdoc/CS64/AbortModel.html)
		//
		//	This function abort running processes for a model. It can be used when a task takes more time than
		//	expected / available, or in case the requested results are not relevant anymore.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "AbortModel")]
        public static extern Int64 AbortModel(Int64 model, Int64 setting);

		//
		//		GetSessionMetaInfo                          (http://rdf.bg/gkdoc/CS64/GetSessionMetaInfo.html)
		//
		//	This function is meant for debugging purposes and return statistics during processing.
		//	The return value represents the number of active models within the session (or zero if the model was not recognized).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetSessionMetaInfo")]
        public static extern Int64 GetSessionMetaInfo(out Int64 allocatedBlocks, out Int64 allocatedBytes, out Int64 nonUsedBlocks, out Int64 nonUsedBytes);

		//
		//		GetModelMetaInfo                            (http://rdf.bg/gkdoc/CS64/GetModelMetaInfo.html)
		//
		//	This function is meant for debugging purposes and return statistics during processing.
		//	The return value represents the number of active models within the session (or zero if the model was not recognized).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out Int64 activeInstances, out Int64 deletedInstances, out Int64 inactiveInstances);

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, IntPtr activeClasses, IntPtr deletedClasses, out Int64 activeProperties, out Int64 deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, IntPtr activeClasses, IntPtr deletedClasses, out Int64 activeProperties, out Int64 deletedProperties, out Int64 activeInstances, out Int64 deletedInstances, out Int64 inactiveInstances);

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, out Int64 activeClasses, out Int64 deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, out Int64 activeClasses, out Int64 deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out Int64 activeInstances, out Int64 deletedInstances, out Int64 inactiveInstances);

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, out Int64 activeClasses, out Int64 removedClasses, out Int64 activeProperties, out Int64 deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, out Int64 activeClasses, out Int64 removedClasses, out Int64 activeProperties, out Int64 deletedProperties, out Int64 activeInstances, out Int64 deletedInstances, out Int64 inactiveInstances);

		//
		//		GetInstanceMetaInfo                         (http://rdf.bg/gkdoc/CS64/GetInstanceMetaInfo.html)
		//
		//	This function is meant for debugging purposes and return statistics during processing.
		//	The return value represents the number of active instances within the model (or zero if the instance was not recognized).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceMetaInfo")]
        public static extern Int64 GetInstanceMetaInfo(Int64 owlInstance, out Int64 allocatedBlocks, out Int64 allocatedBytes);

		//
		//		GetSmoothness                               (http://rdf.bg/gkdoc/CS64/GetSmoothness.html)
		//
		//	This function returns the smoothness of a line or surface.
		//	In case the smoothness can be defined the degree will get assigned either
		//		0 - continuous curve / surface (i.e. degree 9)
		//		1 - the direction of the curve / surface is gradually changing (i.e. degree 1)
		//		2 - the change of direction of the curve / surface is gradually changing (i.e. degree 2)
		//	In return value of this function retuns the dimension of the found smoothness:
		//		0 - smoothness could not be defined
		//		1 - found the smoothness of a curve
		//		2 - found the smoothness of a surface
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetSmoothness")]
        public static extern Int64 GetSmoothness(Int64 owlInstance, out Int64 degree);

		//
		//		AddState                                    (http://rdf.bg/gkdoc/CS64/AddState.html)
		//
		//	This call will integrate the current state information into the model.
		//
		//	Model should be non-zero.
		//
		//	If owlInstance is given the state is only applied on the owlInstance and all its children.
		//	If owlInstance is zero the state is applied on all owlInstances within a model.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "AddState")]
        public static extern void AddState(Int64 model, Int64 owlInstance);

		//
		//		GetModel                                    (http://rdf.bg/gkdoc/CS64/GetModel.html)
		//
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetModel")]
        public static extern Int64 GetModel(Int64 owlInstance);

		//
		//		OrderedHandles                              (http://rdf.bg/gkdoc/CS64/OrderedHandles.html)
		//
		//	This call can be used in two ways. The optional arguments classCnt,
		//	propertyCnt and instanceCnt can be used to get the total amount of active classes,
		//	properies and instances available within the model.
		//
		//	The setting and mask can be used to order the handles given for classes,
		//	properties and instances.
		//		1 - if set this will number all classes with possible values [1 .. classCnt]
		//		2 - if set this will number all classes with possible values [1 .. propertyCnt]
		//		4 - if set this will number all classes with possible values [1 .. instanceCnt]
		//
		//	Note: when enabling ordered handles be aware that classes, properties and instances
		//		  can share the same handles, using the correct argument cannot be checked anymore
		//		  by the library itself. This could result in crashes in case of incorrect assignments
		//		  by the hosting application.
		//	Note: internally there is no performance gain / loss. This is purely meant for situations
		//		  where the hosting application can benefit performance wise from having an ordered list.
		//	Note: use in combination with other libraries is not adviced, i.e. when combined with the
		//		  IFC generation from the IFC Engine component for example
		//
        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, out Int64 classCnt, out Int64 propertyCnt, out Int64 instanceCnt, Int64 setting, Int64 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, out Int64 classCnt, out Int64 propertyCnt, IntPtr instanceCnt, Int64 setting, Int64 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, out Int64 classCnt, IntPtr propertyCnt, out Int64 instanceCnt, Int64 setting, Int64 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, out Int64 classCnt, IntPtr propertyCnt, IntPtr instanceCnt, Int64 setting, Int64 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, IntPtr classCnt, out Int64 propertyCnt, out Int64 instanceCnt, Int64 setting, Int64 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, IntPtr classCnt, out Int64 propertyCnt, IntPtr instanceCnt, Int64 setting, Int64 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, IntPtr classCnt, IntPtr propertyCnt, out Int64 instanceCnt, Int64 setting, Int64 mask);

        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, IntPtr classCnt, IntPtr propertyCnt, IntPtr instanceCnt, Int64 setting, Int64 mask);

		//
		//		PeelArray                                   (http://rdf.bg/gkdoc/CS64/PeelArray.html)
		//
		//	This function introduces functionality that is missing or complicated in some programming languages.
		//	The attribute inValue is a reference to an array of references. The attribute outValue is a reference to the same array,
		//	however a number of elements earlier or further, i.e. number of elements being attribute elementSize. Be aware that as
		//	we are talking about references the offset is depending on 32 bit / 64 bit compilation.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "PeelArray")]
        public static extern void PeelArray(ref byte[] inValue, out byte outValue, Int64 elementSize);

		//
		//		CloseSession                                (http://rdf.bg/gkdoc/CS64/CloseSession.html)
		//
		//	This function closes the session, after this call the geometry kernel cannot be used anymore.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CloseSession")]
        public static extern Int64 CloseSession();

		//
		//		CleanMemory                                 (http://rdf.bg/gkdoc/CS64/CleanMemory.html)
		//
		//		This function ..
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CleanMemory")]
        public static extern void CleanMemory();

		//
		//		ClearCache                                  (http://rdf.bg/gkdoc/CS64/ClearCache.html)
		//
		//		This function ..
		//
        [DllImport(IFCEngineDLL, EntryPoint = "ClearCache")]
        public static extern void ClearCache(Int64 model);

        //
        //  File IO API Calls
        //

		//
		//		CreateModel                                 (http://rdf.bg/gkdoc/CS64/CreateModel.html)
		//
		//	This function creates and empty model.
		//	References inside to other ontologies will be included.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreateModel")]
        public static extern Int64 CreateModel();

		//
		//		OpenModel                                   (http://rdf.bg/gkdoc/CS64/OpenModel.html)
		//
		//	This function opens the model on location fileName.
		//	References inside to other ontologies will be included.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "OpenModel")]
        public static extern Int64 OpenModel(string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModel")]
        public static extern Int64 OpenModel(byte[] fileName);

		//
		//		OpenModelW                                  (http://rdf.bg/gkdoc/CS64/OpenModelW.html)
		//
		//	This function opens the model on location fileName.
		//	References inside to other ontologies will be included.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelW")]
        public static extern Int64 OpenModelW(string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelW")]
        public static extern Int64 OpenModelW(byte[] fileName);

		//
		//		OpenModelS                                  (http://rdf.bg/gkdoc/CS64/OpenModelS.html)
		//
		//	This function opens the model via a stream.
		//	References inside to other ontologies will be included.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelS")]
        public static extern Int64 OpenModelS([MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback);

		//
		//		OpenModelA                                  (http://rdf.bg/gkdoc/CS64/OpenModelA.html)
		//
		//	This function opens the model via an array.
		//	References inside to other ontologies will be included.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelA")]
        public static extern Int64 OpenModelA(byte[] content, Int64 size);

		//
		//		ImportModel                                 (http://rdf.bg/gkdoc/CS64/ImportModel.html)
		//
		//	This function imports a design tree on location fileName.
		//	The design tree will be added to the given existing model.
		//	The return value contains the first instance not referenced by any other instance or zero 
		//	if it does not exist. In case the imported model is created with SaveInstanceTree() this instance is 
		//	unique and equal to the instance used within the call SaveInstanceTree().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "ImportModel")]
        public static extern Int64 ImportModel(Int64 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "ImportModel")]
        public static extern Int64 ImportModel(Int64 model, byte[] fileName);

		//
		//		ImportModelW                                (http://rdf.bg/gkdoc/CS64/ImportModelW.html)
		//
		//	This function imports a design tree on location fileName.
		//	The design tree will be added to the given existing model.
		//	The return value contains the first instance not referenced by any other instance or zero 
		//	if it does not exist. In case the imported model is created with SaveInstanceTree() this instance is 
		//	unique and equal to the instance used within the call SaveInstanceTree().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "ImportModelW")]
        public static extern Int64 ImportModelW(Int64 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "ImportModelW")]
        public static extern Int64 ImportModelW(Int64 model, byte[] fileName);

		//
		//		ImportModelS                                (http://rdf.bg/gkdoc/CS64/ImportModelS.html)
		//
		//	This function imports a design tree via a stream.
		//	The design tree will be added to the given existing model.
		//	The return value contains the first instance not referenced by any other instance or zero 
		//	if it does not exist. In case the imported model is created with SaveInstanceTree() this instance is 
		//	unique and equal to the instance used within the call SaveInstanceTree().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "ImportModelS")]
        public static extern Int64 ImportModelS(Int64 model, [MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback);

		//
		//		ImportModelA                                (http://rdf.bg/gkdoc/CS64/ImportModelA.html)
		//
		//	This function imports a design tree via an array.
		//	The design tree will be added to the given existing model.
		//	The return value contains the first instance not referenced by any other instance or zero 
		//	if it does not exist. In case the imported model is created with SaveInstanceTree() this instance is 
		//	unique and equal to the instance used within the call SaveInstanceTree().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "ImportModelA")]
        public static extern Int64 ImportModelA(Int64 model, byte[] content, Int64 size);

		//
		//		SaveInstanceTree                            (http://rdf.bg/gkdoc/CS64/SaveInstanceTree.html)
		//
		//	This function saves the selected instance and its dependancies on location fileName.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTree")]
        public static extern Int64 SaveInstanceTree(Int64 owlInstance, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTree")]
        public static extern Int64 SaveInstanceTree(Int64 owlInstance, byte[] fileName);

		//
		//		SaveInstanceTreeW                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeW.html)
		//
		//	This function saves the selected instance and its dependancies on location fileName.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeW")]
        public static extern Int64 SaveInstanceTreeW(Int64 owlInstance, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeW")]
        public static extern Int64 SaveInstanceTreeW(Int64 owlInstance, byte[] fileName);

		//
		//		SaveInstanceTreeS                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeS.html)
		//
		//	This function saves the selected instance and its dependancies in a stream.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeS")]
        public static extern Int64 SaveInstanceTreeS(Int64 owlInstance, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, Int64 size);

		//
		//		SaveInstanceTreeA                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeA.html)
		//
		//	This function saves the selected instance and its dependancies in an array.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeA")]
        public static extern Int64 SaveInstanceTreeA(Int64 owlInstance, byte[] content, out Int64 size);

		//
		//		SaveModel                                   (http://rdf.bg/gkdoc/CS64/SaveModel.html)
		//
		//	This function saves the current model on location fileName.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SaveModel")]
        public static extern Int64 SaveModel(Int64 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModel")]
        public static extern Int64 SaveModel(Int64 model, byte[] fileName);

		//
		//		SaveModelW                                  (http://rdf.bg/gkdoc/CS64/SaveModelW.html)
		//
		//	This function saves the current model on location fileName.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelW")]
        public static extern Int64 SaveModelW(Int64 model, string fileName);

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelW")]
        public static extern Int64 SaveModelW(Int64 model, byte[] fileName);

		//
		//		SaveModelS                                  (http://rdf.bg/gkdoc/CS64/SaveModelS.html)
		//
		//	This function saves the current model in a stream.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelS")]
        public static extern Int64 SaveModelS(Int64 model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, Int64 size);

		//
		//		SaveModelA                                  (http://rdf.bg/gkdoc/CS64/SaveModelA.html)
		//
		//	This function saves the current model in an array.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelA")]
        public static extern Int64 SaveModelA(Int64 model, byte[] content, out Int64 size);

		//
		//		SetOverrideFileIO                           (http://rdf.bg/gkdoc/CS64/SetOverrideFileIO.html)
		//
		//	This function overrides the type of file saved / exported independent of the extension given.
		//	By default the extension of the filename will define the type saved / exported:
		//		.rdf => generated RDF serialized content
		//		.ttl => generated TTL serialized content
		//		.bin => generated BIN/X serialized content
		//
		//	Available formats
		//		RDF
		//		TTL
		//		BIN/L - readible but large BIN format
		//		BIN/S - Optimized Binary, only running within given revision 
		//		BIN/X - Optimized Binary, running in all revisions supporting BIN/X
		//
		//	Force file type (overrides extension), works only on save (open selects correct type automatically)
		//		bit0	bit1	bit2
		//		  0		  0		  0		[default] unset forced file type
		//		  0		  0		  1		RESERVED
		//		  0		  1		  0		TTL
		//		  0		  1		  1		RDF
		//		  1		  0		  0		BIN/X
		//		  1		  0		  1		BIN/S
		//		  1		  1		  0		RESERVED
		//		  1		  1		  1		BIN/L
		//
		//	Force exporting as Base64
		//		bit4
		//		  0		do not use Base64
		//		  1		use Base64 (only works for BIN/S and BIN/X), on other formats no effect
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetOverrideFileIO")]
        public static extern void SetOverrideFileIO(Int64 model, Int64 setting, Int64 mask);

		//
		//		GetOverrideFileIO                           (http://rdf.bg/gkdoc/CS64/GetOverrideFileIO.html)
		//
		//	This function get the current overrides for type of file saved / exported independent of the extension given.
		//	By default the extension of the filename will define the type saved / exported:
		//		.rdf => generated RDF serialized content
		//		.ttl => generated TTL serialized content
		//		.bin => generated BIN/X serialized content
		//
		//	Available formats
		//		RDF
		//		TTL
		//		BIN/L - readible but large BIN format
		//		BIN/S - Optimized Binary, only running within given revision 
		//		BIN/X - Optimized Binary, running in all revisions supporting BIN/X
		//
		//	Force file type (overrides extension), works only on save (open selects correct type automatically)
		//		bit0	bit1	bit2
		//		  0		  0		  0		[default] unset forced file type
		//		  0		  0		  1		RESERVED
		//		  0		  1		  0		TTL
		//		  0		  1		  1		RDF
		//		  1		  0		  0		BIN/X
		//		  1		  0		  1		BIN/S
		//		  1		  1		  0		RESERVED
		//		  1		  1		  1		BIN/L
		//
		//	Force exporting as Base64
		//		bit4
		//		  0		do not use Base64
		//		  1		use Base64 (only works for BIN/S and BIN/X), on other formats no effect
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetOverrideFileIO")]
        public static extern Int64 GetOverrideFileIO(Int64 model, Int64 mask);

		//
		//		CloseModel                                  (http://rdf.bg/gkdoc/CS64/CloseModel.html)
		//
		//	This function closes the model. After this call none of the instances and classes within the model
		//	can be used anymore, also garbage collection is not allowed anymore, in default compilation the
		//	model itself will be known in the kernel, however known to be disabled. Calls containing the model
		//	reference will be protected from crashing when called.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CloseModel")]
        public static extern Int64 CloseModel(Int64 model);

        //
        //  Design Tree Classes API Calls
        //

		//
		//		CreateClass                                 (http://rdf.bg/gkdoc/CS64/CreateClass.html)
		//
		//	Returns a handle to an on the fly created class.
		//	If the model input is zero or not a model handle 0 will be returned,
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreateClass")]
        public static extern Int64 CreateClass(Int64 model, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "CreateClass")]
        public static extern Int64 CreateClass(Int64 model, byte[] name);

		//
		//		CreateClassW                                (http://rdf.bg/gkdoc/CS64/CreateClassW.html)
		//
		//	Returns a handle to an on the fly created class.
		//	If the model input is zero or not a model handle 0 will be returned,
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreateClassW")]
        public static extern Int64 CreateClassW(Int64 model, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "CreateClassW")]
        public static extern Int64 CreateClassW(Int64 model, byte[] name);

		//
		//		GetClassByName                              (http://rdf.bg/gkdoc/CS64/GetClassByName.html)
		//
		//	Returns a handle to the class as stored inside.
		//	When the class does not exist yet and the name is unique
		//	the class will be created on the fly and the handle will be returned.
		//	When the name is not unique and given to an instance, objectTypeProperty
		//	or dataTypeProperty 0 will be returned.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByName")]
        public static extern Int64 GetClassByName(Int64 model, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByName")]
        public static extern Int64 GetClassByName(Int64 model, byte[] name);

		//
		//		GetClassByNameW                             (http://rdf.bg/gkdoc/CS64/GetClassByNameW.html)
		//
		//	Returns a handle to the class as stored inside.
		//	When the class does not exist yet and the name is unique
		//	the class will be created on the fly and the handle will be returned.
		//	When the name is not unique and given to an instance, objectTypeProperty
		//	or dataTypeProperty 0 will be returned.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByNameW")]
        public static extern Int64 GetClassByNameW(Int64 model, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByNameW")]
        public static extern Int64 GetClassByNameW(Int64 model, byte[] name);

		//
		//		GetClassesByIterator                        (http://rdf.bg/gkdoc/CS64/GetClassesByIterator.html)
		//
		//	Returns a handle to an class.
		//	If input class is zero, the handle will point to the first relevant class.
		//	If all classes are past (or no relevant classes are found), the function will return 0.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetClassesByIterator")]
        public static extern Int64 GetClassesByIterator(Int64 model, Int64 owlClass);

		//
		//		SetClassParent                              (http://rdf.bg/gkdoc/CS64/SetClassParent.html)
		//
		//	Defines (set/unset) the parent class of a given class. Multiple-inheritence is supported and behavior
		//	of parent classes is also inherited as well as cardinality restrictions on datatype properties and
		//	object properties (relations).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetClassParent")]
        public static extern void SetClassParent(Int64 owlClass, Int64 parentOwlClass, Int64 setting);

		//
		//		SetClassParentEx                            (http://rdf.bg/gkdoc/CS64/SetClassParentEx.html)
		//
		//	Defines (set/unset) the parent class of a given class. Multiple-inheritence is supported and behavior
		//	of parent classes is also inherited as well as cardinality restrictions on datatype properties and
		//	object properties (relations).
		//
		//	This call has the same behavior as SetClassParent, however needs to be
		//	used in case classes are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetClassParentEx")]
        public static extern void SetClassParentEx(Int64 model, Int64 owlClass, Int64 parentOwlClass, Int64 setting);

		//
		//		GetParentsByIterator                        (http://rdf.bg/gkdoc/CS64/GetParentsByIterator.html)
		//
		//	Returns the next parent of the class.
		//	If input parent is zero, the handle will point to the first relevant parent.
		//	If all parent are past (or no relevant parent are found), the function will return 0.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetParentsByIterator")]
        public static extern Int64 GetParentsByIterator(Int64 owlClass, Int64 parentOwlClass);

		//
		//		SetNameOfClass                              (http://rdf.bg/gkdoc/CS64/SetNameOfClass.html)
		//
		//	Sets/updates the name of the class, if no error it returns 0.
		//	In case class does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClass")]
        public static extern Int64 SetNameOfClass(Int64 owlClass, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClass")]
        public static extern Int64 SetNameOfClass(Int64 owlClass, byte[] name);

		//
		//		SetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/SetNameOfClassW.html)
		//
		//	Sets/updates the name of the class, if no error it returns 0.
		//	In case class does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassW")]
        public static extern Int64 SetNameOfClassW(Int64 owlClass, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassW")]
        public static extern Int64 SetNameOfClassW(Int64 owlClass, byte[] name);

		//
		//		SetNameOfClassEx                            (http://rdf.bg/gkdoc/CS64/SetNameOfClassEx.html)
		//
		//	Sets/updates the name of the class, if no error it returns 0.
		//	In case class does not exist it returns 1, when name cannot be updated 2.
		//
		//	This call has the same behavior as SetNameOfClass, however needs to be
		//	used in case classes are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassEx")]
        public static extern Int64 SetNameOfClassEx(Int64 model, Int64 owlClass, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassEx")]
        public static extern Int64 SetNameOfClassEx(Int64 model, Int64 owlClass, byte[] name);

		//
		//		SetNameOfClassWEx                           (http://rdf.bg/gkdoc/CS64/SetNameOfClassWEx.html)
		//
		//	Sets/updates the name of the class, if no error it returns 0.
		//	In case class does not exist it returns 1, when name cannot be updated 2.
		//
		//	This call has the same behavior as SetNameOfClassW, however needs to be
		//	used in case classes are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassWEx")]
        public static extern Int64 SetNameOfClassWEx(Int64 model, Int64 owlClass, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassWEx")]
        public static extern Int64 SetNameOfClassWEx(Int64 model, Int64 owlClass, byte[] name);

		//
		//		GetNameOfClass                              (http://rdf.bg/gkdoc/CS64/GetNameOfClass.html)
		//
		//	Returns the name of the class, if the class does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClass")]
        public static extern void GetNameOfClass(Int64 owlClass, out IntPtr name);

		//
		//		GetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/GetNameOfClassW.html)
		//
		//	Returns the name of the class, if the class does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassW")]
        public static extern void GetNameOfClassW(Int64 owlClass, out IntPtr name);

		//
		//		GetNameOfClassEx                            (http://rdf.bg/gkdoc/CS64/GetNameOfClassEx.html)
		//
		//	Returns the name of the class, if the class does not exist it returns nullptr.
		//
		//	This call has the same behavior as GetNameOfClass, however needs to be
		//	used in case properties are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassEx")]
        public static extern void GetNameOfClassEx(Int64 model, Int64 owlClass, out IntPtr name);

		//
		//		GetNameOfClassWEx                           (http://rdf.bg/gkdoc/CS64/GetNameOfClassWEx.html)
		//
		//	Returns the name of the class, if the class does not exist it returns nullptr.
		//
		//	This call has the same behavior as GetNameOfClassW, however needs to be
		//	used in case classes are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassWEx")]
        public static extern void GetNameOfClassWEx(Int64 model, Int64 owlClass, out IntPtr name);

		//
		//		SetClassPropertyCardinalityRestriction      (http://rdf.bg/gkdoc/CS64/SetClassPropertyCardinalityRestriction.html)
		//
		//	This function sets the minCard and maxCard of a certain property in the context of a class.
		//	The cardinality of a property in an instance has to be between minCard and maxCard (as well 
		//	as within the cardinality restrictions as given by the property in context of any of its
		//	(indirect) parent classes).
		//	If undefined minCard and/or maxCard will be of value -1, this means
		//	for minCard that it is 0 and for maxCard it means infinity.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetClassPropertyCardinalityRestriction")]
        public static extern void SetClassPropertyCardinalityRestriction(Int64 owlClass, Int64 rdfProperty, Int64 minCard, Int64 maxCard);

		//
		//		SetClassPropertyCardinalityRestrictionEx    (http://rdf.bg/gkdoc/CS64/SetClassPropertyCardinalityRestrictionEx.html)
		//
		//	This function sets the minCard and maxCard of a certain property in the context of a class.
		//	The cardinality of a property in an instance has to be between minCard and maxCard (as well 
		//	as within the cardinality restrictions as given by the property in context of any of its
		//	(indirect) parent classes).
		//	If undefined minCard and/or maxCard will be of value -1, this means
		//	for minCard that it is 0 and for maxCard it means infinity.
		//
		//	This call has the same behavior as SetClassPropertyCardinalityRestriction, however needs to be
		//	used in case classes or properties are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetClassPropertyCardinalityRestrictionEx")]
        public static extern void SetClassPropertyCardinalityRestrictionEx(Int64 model, Int64 owlClass, Int64 rdfProperty, Int64 minCard, Int64 maxCard);

		//
		//		GetClassPropertyCardinalityRestriction      (http://rdf.bg/gkdoc/CS64/GetClassPropertyCardinalityRestriction.html)
		//
		//	This function returns the minCard and maxCard of a certain
		//	property in the context of a class. The cardinality of a property in 
		//	an instance has to be between minCard and maxCard (as well as within the cardinality restrictions
		//	as given by the property in context of any of its (indirect) parent classes).
		//	If undefined minCard and/or maxCard will be of value -1, this means
		//	for minCard that it is 0 and for maxCard it means infinity.
		//
		//	Note: this function does not return inherited restrictions. The example shows how to retrieve
		//	this knowledge, as it is derived knowledge the call that used to be available is removed.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetClassPropertyCardinalityRestriction")]
        public static extern void GetClassPropertyCardinalityRestriction(Int64 owlClass, Int64 rdfProperty, out Int64 minCard, out Int64 maxCard);

		//
		//		GetClassPropertyCardinalityRestrictionEx    (http://rdf.bg/gkdoc/CS64/GetClassPropertyCardinalityRestrictionEx.html)
		//
		//	This function returns the minCard and maxCard of a certain
		//	property in the context of a class. The cardinality of a property in 
		//	an instance has to be between minCard and maxCard (as well as within the cardinality restrictions
		//	as given by the property in context of any of its (indirect) parent classes).
		//	If undefined minCard and/or maxCard will be of value -1, this means
		//	for minCard that it is 0 and for maxCard it means infinity.
		//
		//	This call has the same behavior as GetClassPropertyCardinalityRestriction, however needs to be
		//	used in case classes or properties are exchanged as a successive series of integers.
		//
		//	Note: this function does not return inherited restrictions. The example shows how to retrieve
		//	this knowledge, as it is derived knowledge the call that used to be available is removed.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetClassPropertyCardinalityRestrictionEx")]
        public static extern void GetClassPropertyCardinalityRestrictionEx(Int64 model, Int64 owlClass, Int64 rdfProperty, out Int64 minCard, out Int64 maxCard);

		//
		//		GetGeometryClass                            (http://rdf.bg/gkdoc/CS64/GetGeometryClass.html)
		//
		//	Returns non-zero if the owlClass is a geometry type. This call will return the input class
		//	for all classes initially available. It will return as well non-for all classes created by the
		//	user or loaded / imported through a model that (indirectly) inherit one of the
		//	original classes available. in this case it returns the original available class
		//	it inherits the behavior from.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetGeometryClass")]
        public static extern Int64 GetGeometryClass(Int64 owlClass);

		//
		//		GetGeometryClassEx                          (http://rdf.bg/gkdoc/CS64/GetGeometryClassEx.html)
		//
		//	Returns non-zero if the owlClass is a geometry type. This call will return the input class
		//	for all classes initially available. It will return as well non-for all classes created by the
		//	user or loaded / imported through a model that (indirectly) inherit one of the
		//	original classes available. in this case it returns the original available class
		//	it inherits the behavior from.
		//
		//	This call has the same behavior as GetGeometryClass, however needs to be
		//	used in case classes are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetGeometryClassEx")]
        public static extern Int64 GetGeometryClassEx(Int64 model, Int64 owlClass);

        //
        //  Design Tree Properties API Calls
        //

		//
		//		CreateProperty                              (http://rdf.bg/gkdoc/CS64/CreateProperty.html)
		//
		//	Returns a handle to an on the fly created property.
		//	If the model input is zero or not a model handle 0 will be returned,
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreateProperty")]
        public static extern Int64 CreateProperty(Int64 model, Int64 rdfPropertyType, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "CreateProperty")]
        public static extern Int64 CreateProperty(Int64 model, Int64 rdfPropertyType, byte[] name);

		//
		//		CreatePropertyW                             (http://rdf.bg/gkdoc/CS64/CreatePropertyW.html)
		//
		//	Returns a handle to an on the fly created property.
		//	If the model input is zero or not a model handle 0 will be returned,
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreatePropertyW")]
        public static extern Int64 CreatePropertyW(Int64 model, Int64 rdfPropertyType, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "CreatePropertyW")]
        public static extern Int64 CreatePropertyW(Int64 model, Int64 rdfPropertyType, byte[] name);

		//
		//		GetPropertyByName                           (http://rdf.bg/gkdoc/CS64/GetPropertyByName.html)
		//
		//	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.
		//	When the property does not exist yet and the name is unique
		//	the property will be created on-the-fly and the handle will be returned.
		//	When the name is not unique and given to a class or instance 0 will be returned.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName")]
        public static extern Int64 GetPropertyByName(Int64 model, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName")]
        public static extern Int64 GetPropertyByName(Int64 model, byte[] name);

		//
		//		GetPropertyByNameW                          (http://rdf.bg/gkdoc/CS64/GetPropertyByNameW.html)
		//
		//	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.
		//	When the property does not exist yet and the name is unique
		//	the property will be created on-the-fly and the handle will be returned.
		//	When the name is not unique and given to a class or instance 0 will be returned.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameW")]
        public static extern Int64 GetPropertyByNameW(Int64 model, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameW")]
        public static extern Int64 GetPropertyByNameW(Int64 model, byte[] name);

		//
		//		GetPropertiesByIterator                     (http://rdf.bg/gkdoc/CS64/GetPropertiesByIterator.html)
		//
		//	Returns a handle to a property.
		//	If input property is zero, the handle will point to the first relevant property.
		//	If all properties are past (or no relevant properties are found), the function will return 0.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertiesByIterator")]
        public static extern Int64 GetPropertiesByIterator(Int64 model, Int64 rdfProperty);

		//
		//		GetRangeRestrictionsByIterator              (http://rdf.bg/gkdoc/CS64/GetRangeRestrictionsByIterator.html)
		//
		//	Returns the next class the property is restricted to.
		//	If input class is zero, the handle will point to the first relevant class.
		//	If all classes are past (or no relevant classes are found), the function will return 0.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetRangeRestrictionsByIterator")]
        public static extern Int64 GetRangeRestrictionsByIterator(Int64 rdfProperty, Int64 owlClass);

		//
		//		SetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/SetNameOfProperty.html)
		//
		//	Sets/updates the name of the property, if no error it returns 0.
		//	In case property does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfProperty")]
        public static extern Int64 SetNameOfProperty(Int64 rdfProperty, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfProperty")]
        public static extern Int64 SetNameOfProperty(Int64 rdfProperty, byte[] name);

		//
		//		SetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyW.html)
		//
		//	Sets/updates the name of the property, if no error it returns 0.
		//	In case property does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyW")]
        public static extern Int64 SetNameOfPropertyW(Int64 rdfProperty, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyW")]
        public static extern Int64 SetNameOfPropertyW(Int64 rdfProperty, byte[] name);

		//
		//		SetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyEx.html)
		//
		//	Sets/updates the name of the property, if no error it returns 0.
		//	In case property does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyEx")]
        public static extern Int64 SetNameOfPropertyEx(Int64 model, Int64 rdfProperty, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyEx")]
        public static extern Int64 SetNameOfPropertyEx(Int64 model, Int64 rdfProperty, byte[] name);

		//
		//		SetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyWEx.html)
		//
		//	Sets/updates the name of the property, if no error it returns 0.
		//	In case property does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]
        public static extern Int64 SetNameOfPropertyWEx(Int64 model, Int64 rdfProperty, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]
        public static extern Int64 SetNameOfPropertyWEx(Int64 model, Int64 rdfProperty, byte[] name);

		//
		//		GetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/GetNameOfProperty.html)
		//
		//	Returns the name of the property, if the property does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfProperty")]
        public static extern void GetNameOfProperty(Int64 rdfProperty, out IntPtr name);

		//
		//		GetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyW.html)
		//
		//	Returns the name of the property, if the property does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyW")]
        public static extern void GetNameOfPropertyW(Int64 rdfProperty, out IntPtr name);

		//
		//		GetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyEx.html)
		//
		//	Returns the name of the property, if the property does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyEx")]
        public static extern void GetNameOfPropertyEx(Int64 model, Int64 rdfProperty, out IntPtr name);

		//
		//		GetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyWEx.html)
		//
		//	Returns the name of the property, if the property does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyWEx")]
        public static extern void GetNameOfPropertyWEx(Int64 model, Int64 rdfProperty, out IntPtr name);

		//
		//		SetPropertyType                             (http://rdf.bg/gkdoc/CS64/SetPropertyType.html)
		//
		//	This function sets the type of the property. This is only allowed
		//	if the type of the property was not set before.
		//
		//	The following values are possible for propertyType:
		//			1	The property is an Object Property
		//			2	The property is an Datatype Property of type Boolean
		//			3	The property is an Datatype Property of type Char
		//			4	The property is an Datatype Property of type Integer
		//			5	The property is an Datatype Property of type Double
		//	The return value of this call is GetPropertyType/Ex applied after applying
		//	the type, normally this corresponds with the propertyType requested
		//	to be set unless the property already has a different propertyType set before.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetPropertyType")]
        public static extern Int64 SetPropertyType(Int64 rdfProperty, Int64 propertyType);

		//
		//		SetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/SetPropertyTypeEx.html)
		//
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetPropertyTypeEx")]
        public static extern Int64 SetPropertyTypeEx(Int64 model, Int64 rdfProperty, Int64 propertyType);

		//
		//		GetPropertyType                             (http://rdf.bg/gkdoc/CS64/GetPropertyType.html)
		//
		//	This function returns the type of the property.
		//	The following return values are possible:
		//		0	The property is not defined yet
		//		1	The property is an Object Type Property
		//		2	The property is an Data Type Property of type Boolean
		//		3	The property is an Data Type Property of type Char
		//		4	The property is an Data Type Property of type Integer
		//		5	The property is an Data Type Property of type Double
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyType")]
        public static extern Int64 GetPropertyType(Int64 rdfProperty);

		//
		//		GetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/GetPropertyTypeEx.html)
		//
		//	This call has the same behavior as GetPropertyType, however needs to be
		//	used in case properties are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyTypeEx")]
        public static extern Int64 GetPropertyTypeEx(Int64 model, Int64 rdfProperty);

        //
        //  Design Tree Instances API Calls
        //

		//
		//		CreateInstance                              (http://rdf.bg/gkdoc/CS64/CreateInstance.html)
		//
		//	Returns a handle to an on the fly created instance.
		//	If the owlClass input is zero or not a class handle 0 will be returned,
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstance")]
        public static extern Int64 CreateInstance(Int64 owlClass, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstance")]
        public static extern Int64 CreateInstance(Int64 owlClass, byte[] name);

		//
		//		CreateInstanceW                             (http://rdf.bg/gkdoc/CS64/CreateInstanceW.html)
		//
		//	Returns a handle to an on the fly created instance.
		//	If the owlClass input is zero or not a class handle 0 will be returned,
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceW")]
        public static extern Int64 CreateInstanceW(Int64 owlClass, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceW")]
        public static extern Int64 CreateInstanceW(Int64 owlClass, byte[] name);

		//
		//		CreateInstanceEx                            (http://rdf.bg/gkdoc/CS64/CreateInstanceEx.html)
		//
		//	Returns a handle to an on the fly created instance.
		//	If the owlClass input is zero or not a class handle 0 will be returned,
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceEx")]
        public static extern Int64 CreateInstanceEx(Int64 model, Int64 owlClass, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceEx")]
        public static extern Int64 CreateInstanceEx(Int64 model, Int64 owlClass, byte[] name);

		//
		//		CreateInstanceWEx                           (http://rdf.bg/gkdoc/CS64/CreateInstanceWEx.html)
		//
		//	Returns a handle to an on the fly created instance.
		//	If the owlClass input is zero or not a class handle 0 will be returned,
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceWEx")]
        public static extern Int64 CreateInstanceWEx(Int64 model, Int64 owlClass, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceWEx")]
        public static extern Int64 CreateInstanceWEx(Int64 model, Int64 owlClass, byte[] name);

		//
		//		GetInstancesByIterator                      (http://rdf.bg/gkdoc/CS64/GetInstancesByIterator.html)
		//
		//	Returns a handle to an instance.
		//	If input instance is zero, the handle will point to the first relevant instance.
		//	If all instances are past (or no relevant instances are found), the function will return 0.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancesByIterator")]
        public static extern Int64 GetInstancesByIterator(Int64 model, Int64 owlInstance);

		//
		//		GetInstanceClass                            (http://rdf.bg/gkdoc/CS64/GetInstanceClass.html)
		//
		//	Returns the handle to the class of which an instances is instantiated.
		//
		//	Note: internally this function is not rich enough as support for multiple inheritance
		//		  is making it impossible to answer this request with always one class handle.
		//		  Even in the case of pure single inheritance an instance of a class is also an instance of 
		//		  every parent classes in the inheritance tree. For now we expect single inheritance use
		//		  and the returned class handle points to the 'lowest' class in the inheritance tree.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceClass")]
        public static extern Int64 GetInstanceClass(Int64 owlInstance);

		//
		//		GetInstanceClassEx                          (http://rdf.bg/gkdoc/CS64/GetInstanceClassEx.html)
		//
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceClassEx")]
        public static extern Int64 GetInstanceClassEx(Int64 model, Int64 owlInstance);

		//
		//		GetInstancePropertyByIterator               (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIterator.html)
		//
		//	Returns a handle to the objectTypeProperty or dataTypeProperty connected to
		//	the instance, this property can also contain a value, but for example also
		//	the knowledge about cardinality restrictions in the context of this instance's class
		//	and the exact cardinality in context of its instance.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancePropertyByIterator")]
        public static extern Int64 GetInstancePropertyByIterator(Int64 owlInstance, Int64 rdfProperty);

		//
		//		GetInstancePropertyByIteratorEx             (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIteratorEx.html)
		//
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancePropertyByIteratorEx")]
        public static extern Int64 GetInstancePropertyByIteratorEx(Int64 model, Int64 owlInstance, Int64 rdfProperty);

		//
		//		GetInstanceInverseReferencesByIterator      (http://rdf.bg/gkdoc/CS64/GetInstanceInverseReferencesByIterator.html)
		//
		//	Returns a handle to the owlInstances refering this instance
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceInverseReferencesByIterator")]
        public static extern Int64 GetInstanceInverseReferencesByIterator(Int64 owlInstance, Int64 referencingOwlInstance);

		//
		//		GetInstanceReferencesByIterator             (http://rdf.bg/gkdoc/CS64/GetInstanceReferencesByIterator.html)
		//
		//	Returns a handle to the owlInstance refered by this instance
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceReferencesByIterator")]
        public static extern Int64 GetInstanceReferencesByIterator(Int64 owlInstance, Int64 referencedOwlInstance);

		//
		//		SetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/SetNameOfInstance.html)
		//
		//	Sets/updates the name of the instance, if no error it returns 0.
		//	In case instance does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstance")]
        public static extern Int64 SetNameOfInstance(Int64 owlInstance, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstance")]
        public static extern Int64 SetNameOfInstance(Int64 owlInstance, byte[] name);

		//
		//		SetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceW.html)
		//
		//	Sets/updates the name of the instance, if no error it returns 0.
		//	In case instance does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceW")]
        public static extern Int64 SetNameOfInstanceW(Int64 owlInstance, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceW")]
        public static extern Int64 SetNameOfInstanceW(Int64 owlInstance, byte[] name);

		//
		//		SetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceEx.html)
		//
		//	Sets/updates the name of the instance, if no error it returns 0.
		//	In case instance does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceEx")]
        public static extern Int64 SetNameOfInstanceEx(Int64 model, Int64 owlInstance, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceEx")]
        public static extern Int64 SetNameOfInstanceEx(Int64 model, Int64 owlInstance, byte[] name);

		//
		//		SetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceWEx.html)
		//
		//	Sets/updates the name of the instance, if no error it returns 0.
		//	In case instance does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]
        public static extern Int64 SetNameOfInstanceWEx(Int64 model, Int64 owlInstance, string name);

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]
        public static extern Int64 SetNameOfInstanceWEx(Int64 model, Int64 owlInstance, byte[] name);

		//
		//		GetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/GetNameOfInstance.html)
		//
		//	Returns the name of the instance, if the instance does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstance")]
        public static extern void GetNameOfInstance(Int64 owlInstance, out IntPtr name);

		//
		//		GetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceW.html)
		//
		//	Returns the name of the instance, if the instance does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceW")]
        public static extern void GetNameOfInstanceW(Int64 owlInstance, out IntPtr name);

		//
		//		GetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceEx.html)
		//
		//	Returns the name of the instance, if the instance does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceEx")]
        public static extern void GetNameOfInstanceEx(Int64 model, Int64 owlInstance, out IntPtr name);

		//
		//		GetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceWEx.html)
		//
		//	Returns the name of the instance, if the instance does not exist it returns nullptr.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceWEx")]
        public static extern void GetNameOfInstanceWEx(Int64 model, Int64 owlInstance, out IntPtr name);

		//
		//		SetDatatypeProperty                         (http://rdf.bg/gkdoc/CS64/SetDatatypeProperty.html)
		//
		//	This function sets the value(s) of a certain datatypeTypeProperty
		//	in the context of an instance.
		//	The value of card gives the actual card of the values list.
		//	The list values of undefined (void) items is a list of booleans, chars, integers
		//	or doubles, this list has a length as givin in the values card. The actual used type
		//	is given by the definition of the dataTypeProperty.
		//	The return value always should be 0, if not something is wrong in the way this property is called.
		//
		//	Note: the client application needs to make sure the cardinality of
		//		  the property is within the boundaries.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, ref byte values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, byte[] values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, Int64[] values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, ref double values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, double[] values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, ref string values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, string[] values, Int64 card);

		//
		//		SetDatatypePropertyEx                       (http://rdf.bg/gkdoc/CS64/SetDatatypePropertyEx.html)
		//
		//	This function sets the value(s) of a certain datatypeTypeProperty
		//	in the context of an instance.
		//	The value of card gives the actual card of the values list.
		//	The list values of undefined (void) items is a list of booleans, chars, integers
		//	or doubles, this list has a length as givin in the values card. The actual used type
		//	is given by the definition of the dataTypeProperty.
		//	The return value always should be 0, if not something is wrong in the way this property is called.
		//
		//	This call has the same behavior as SetDatatypeProperty, however needs to be
		//	used in case properties are exchanged as a successive series of integers.
		//
		//	Note: the client application needs to make sure the cardinality of
		//		  the property is within the boundaries.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, ref byte values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, byte[] values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, Int64[] values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, ref double values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, double[] values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, ref string values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, string[] values, Int64 card);

		//
		//		GetDatatypeProperty                         (http://rdf.bg/gkdoc/CS64/GetDatatypeProperty.html)
		//
		//	This function gets the value(s) of a certain datatypeTypeProperty
		//	in the context of an instance.
		//	The value of card gives the actual card of the values list.
		//	The list values of undefined (void) items is a list of booleans, chars, integers
		//	or doubles, this list has a length as givin in the value card. The actual used type
		//	is given by the definition of the dataTypeProperty.
		//	The return value always should be 0, if not something is wrong in the way this property is called.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetDatatypeProperty")]
        public static extern Int64 GetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr values, out Int64 card);

		//
		//		GetDatatypePropertyEx                       (http://rdf.bg/gkdoc/CS64/GetDatatypePropertyEx.html)
		//
		//	This function gets the value(s) of a certain datatypeTypeProperty
		//	in the context of an instance.
		//	The value of card gives the actual card of the values list.
		//	The list values of undefined (void) items is a list of booleans, chars, integers
		//	or doubles, this list has a length as givin in the value card. The actual used type
		//	is given by the definition of the dataTypeProperty.
		//	The return value always should be 0, if not something is wrong in the way this property is called.
		//
		//	This call has the same behavior as GetDatatypeProperty, however needs to be
		//	used in case properties are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetDatatypePropertyEx")]
        public static extern Int64 GetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, out IntPtr values, out Int64 card);

		//
		//		SetObjectProperty                           (http://rdf.bg/gkdoc/CS64/SetObjectProperty.html)
		//
		//	This function sets the value(s) of a certain objectTypeProperty
		//	in the context of an instance.
		//	The value of card gives the actual card of the values list.
		//	The list values of integers is a list of handles to instances, this list
		//	has a length as givin in the values card.
		//	The return value always should be 0, if not something is wrong in the way this property is called.
		//
		//	Note: the client application needs to make sure the cardinality of
		//		  the property is within the boundaries.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetObjectProperty")]
        public static extern Int64 SetObjectProperty(Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

		//
		//		SetObjectPropertyEx                         (http://rdf.bg/gkdoc/CS64/SetObjectPropertyEx.html)
		//
		//	This function sets the value(s) of a certain objectTypeProperty
		//	in the context of an instance.
		//	The value of card gives the actual card of the values list.
		//	The list values of integers is a list of handles to instances, this list
		//	has a length as givin in the values card.
		//	The return value always should be 0, if not something is wrong in the way this property is called.
		//
		//	This call has the same behavior as SetObjectProperty, however needs to be
		//	used in case properties are exchanged as a successive series of integers.
		//
		//	Note: the client application needs to make sure the cardinality of
		//		  the property is within the boundaries.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetObjectPropertyEx")]
        public static extern Int64 SetObjectPropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

		//
		//		GetObjectProperty                           (http://rdf.bg/gkdoc/CS64/GetObjectProperty.html)
		//
		//	This function gets the value(s) of a certain objectProperty
		//	in the context of an instance.
		//	The value of card gives the actual card of the values list.
		//	The list values of integers is a list of handles to instances, this list
		//	has a length as givin in the value card.
		//	The return value always should be 0, if not something is wrong in the way this property is called.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetObjectProperty")]
        public static extern Int64 GetObjectProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr values, out Int64 card);

		//
		//		GetObjectPropertyEx                         (http://rdf.bg/gkdoc/CS64/GetObjectPropertyEx.html)
		//
		//	This function gets the value(s) of a certain objectProperty
		//	in the context of an instance.
		//	The value of card gives the actual card of the values list.
		//	The list values of integers is a list of handles to instances, this list
		//	has a length as givin in the values card.
		//	The return value always should be 0, if not something is wrong in the way this property is called.
		//
		//	This call has the same behavior as GetObjectProperty, however needs to be
		//	used in case properties are exchanged as a successive series of integers.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetObjectPropertyEx")]
        public static extern Int64 GetObjectPropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, out IntPtr values, out Int64 card);

		//
		//		CreateInstanceInContextStructure            (http://rdf.bg/gkdoc/CS64/CreateInstanceInContextStructure.html)
		//
		//	InstanceInContext structures give you more detailed information about
		//	individual parts of the geometry of a certain instance viualized.
		//	It is allowed to have more then 1 InstanceInContext structures per instance.
		//	InstanceInContext structures are updated dynamically when the geometry
		//	structure is updated.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceInContextStructure")]
        public static extern Int64 CreateInstanceInContextStructure(Int64 owlInstance);

		//
		//		DestroyInstanceInContextStructure           (http://rdf.bg/gkdoc/CS64/DestroyInstanceInContextStructure.html)
		//
		//	InstanceInContext structures are updated dynamically and therfore even while the cost
		//	in performance and memory is limited it is advised to destroy structures as soon
		//	as they are obsolete.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "DestroyInstanceInContextStructure")]
        public static extern void DestroyInstanceInContextStructure(Int64 owlInstanceInContext);

		//
		//		InstanceInContextChild                      (http://rdf.bg/gkdoc/CS64/InstanceInContextChild.html)
		//
		//
        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextChild")]
        public static extern Int64 InstanceInContextChild(Int64 owlInstanceInContext);

		//
		//		InstanceInContextNext                       (http://rdf.bg/gkdoc/CS64/InstanceInContextNext.html)
		//
		//
        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextNext")]
        public static extern Int64 InstanceInContextNext(Int64 owlInstanceInContext);

		//
		//		InstanceInContextIsUpdated                  (http://rdf.bg/gkdoc/CS64/InstanceInContextIsUpdated.html)
		//
		//
        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextIsUpdated")]
        public static extern Int64 InstanceInContextIsUpdated(Int64 owlInstanceInContext);

		//
		//		RemoveInstance                              (http://rdf.bg/gkdoc/CS64/RemoveInstance.html)
		//
		//	This function removes an instance from the internal structure.
		//	In case copies are created by the host this function checks if all
		//	copies are removed otherwise the instance will stay available.
		//	Return value is 0 if everything went ok and positive in case of an error
		//
        [DllImport(IFCEngineDLL, EntryPoint = "RemoveInstance")]
        public static extern Int64 RemoveInstance(Int64 owlInstance);

		//
		//		RemoveInstanceRecursively                   (http://rdf.bg/gkdoc/CS64/RemoveInstanceRecursively.html)
		//
		//	This function removes an instance recursively from the internal structure.
		//	In case checkInverseRelations is non-zero only instances that are not referenced
		//	by other existing instances.
		//
		//	Return value is total number of removed instances
		//
        [DllImport(IFCEngineDLL, EntryPoint = "RemoveInstanceRecursively")]
        public static extern Int64 RemoveInstanceRecursively(Int64 owlInstance);

		//
		//		RemoveInstances                             (http://rdf.bg/gkdoc/CS64/RemoveInstances.html)
		//
		//	This function removes all available instances for the given model 
		//	from the internal structure.
		//	Return value is the number of removed instances.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "RemoveInstances")]
        public static extern Int64 RemoveInstances(Int64 model);

        //
        //  Retrieve Geometry API Calls
        //

		//
		//		CalculateInstance                           (http://rdf.bg/gkdoc/CS64/CalculateInstance.html)
		//
		//	This function prepares the content to be ready so the buffers can be filled.
		//	It returns the minimum size the buffers should be. This is only the case
		//	when the pointer is given, all arguments are allowed to be nullptr.
		//
		//	Note: This function needs to be called directly before UpdateVertexBuffer(),
		//		  UpdateIndexBuffer() and UpdateTransformationBuffer().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]
        public static extern Int64 CalculateInstance(Int64 owlInstance, out Int64 vertexBufferSize, out Int64 indexBufferSize, out Int64 transformationBufferSize);

        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]
        public static extern Int64 CalculateInstance(Int64 owlInstance, out Int64 vertexBufferSize, out Int64 indexBufferSize, IntPtr transformationBufferSize);

        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]
        public static extern Int64 CalculateInstance(Int64 owlInstance, out Int64 vertexBufferSize, IntPtr indexBufferSize, IntPtr transformationBufferSize);

        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]
        public static extern Int64 CalculateInstance(Int64 owlInstance, IntPtr vertexBufferSize, IntPtr indexBufferSize, IntPtr transformationBufferSize);

		//
		//		UpdateInstance                              (http://rdf.bg/gkdoc/CS64/UpdateInstance.html)
		//
		//	This function prepares the content to be ready without filling the buffers
		//	as done within CalculateInstance(). CalculateInstance calls this function as a start.
		//	This function will also set the 'derived' values for the instance passed as argument.
		//	For example the coordinates values of a MultiplicationMatrix will be set if the array is
		//	defined.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstance")]
        public static extern Int64 UpdateInstance(Int64 owlInstance);

		//
		//		InferenceInstance                           (http://rdf.bg/gkdoc/CS64/InferenceInstance.html)
		//
		//	This function fills in values that are implicitely known but not given by the user. This function
		//	can also be used to identify default values of properties if not given.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "InferenceInstance")]
        public static extern Int64 InferenceInstance(Int64 owlInstance);

		//
		//		UpdateInstanceVertexBuffer                  (http://rdf.bg/gkdoc/CS64/UpdateInstanceVertexBuffer.html)
		//
		//	This function should be preceded by the function CalculateInstances(),
		//	the only allowed other API functions in between are UpdateIndexBuffer()
		//	and UpdateTransformationBuffer().
		//	It is expected to be called with a buffer vertexBuffer of at least the size as 
		//	given by CalculateInstances().
		//	If not called for the first time it will expect to contain the same content as
		//	from previous call, even is size is changed. This can be overruled by 
		//	the function ClearedExternalBuffers().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
        public static extern Int64 UpdateInstanceVertexBuffer(Int64 owlInstance, out float vertexBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
        public static extern Int64 UpdateInstanceVertexBuffer(Int64 owlInstance, float[] vertexBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
        public static extern Int64 UpdateInstanceVertexBuffer(Int64 owlInstance, out double vertexBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
        public static extern Int64 UpdateInstanceVertexBuffer(Int64 owlInstance, double[] vertexBuffer);

		//
		//		UpdateInstanceIndexBuffer                   (http://rdf.bg/gkdoc/CS64/UpdateInstanceIndexBuffer.html)
		//
		//	This function should be preceded by the function CalculateInstances(),
		//	the only allowed other API functions in between are UpdateVertexBuffer()
		//	and UpdateTransformationBuffer().
		//	It is expected to be called with a buffer indexBuffer of at least the size as 
		//	given by CalculateInstances().
		//	If not called for the first time it will expect to contain the same content as
		//	from previous call, even is size is changed. This can be overruled by 
		//	the function ClearedExternalBuffers().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
        public static extern Int64 UpdateInstanceIndexBuffer(Int64 owlInstance, out Int32 indexBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
        public static extern Int64 UpdateInstanceIndexBuffer(Int64 owlInstance, Int32[] indexBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
        public static extern Int64 UpdateInstanceIndexBuffer(Int64 owlInstance, out Int64 indexBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
        public static extern Int64 UpdateInstanceIndexBuffer(Int64 owlInstance, Int64[] indexBuffer);

		//
		//		UpdateInstanceTransformationBuffer          (http://rdf.bg/gkdoc/CS64/UpdateInstanceTransformationBuffer.html)
		//
		//	This function should be preceded by the function CalculateInstances(),
		//	the only allowed other API functions in between are UpdateVertexBuffer()
		//	and UpdateIndexBuffer().
		//	It is expected to be called with a buffer vertexBuffer of at least the size as 
		//	given by CalculateInstances().
		//	If not called for the first time it will expect to contain the same content as
		//	from previous call, even is size is changed. This can be overruled by 
		//	the function ClearedExternalBuffers().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceTransformationBuffer")]
        public static extern Int64 UpdateInstanceTransformationBuffer(Int64 owlInstance, out double transformationBuffer);

        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceTransformationBuffer")]
        public static extern Int64 UpdateInstanceTransformationBuffer(Int64 owlInstance, double[] transformationBuffer);

		//
		//		ClearedInstanceExternalBuffers              (http://rdf.bg/gkdoc/CS64/ClearedInstanceExternalBuffers.html)
		//
		//	This function tells the engine that all buffers have no memory of earlier filling 
		//	for a certain instance.
		//	This means that even when buffer content didn't changed it will be updated when
		//	functions UpdateVertexBuffer(), UpdateIndexBuffer() and/or transformationBuffer()
		//	are called for this specific instance.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "ClearedInstanceExternalBuffers")]
        public static extern void ClearedInstanceExternalBuffers(Int64 owlInstance);

		//
		//		ClearedExternalBuffers                      (http://rdf.bg/gkdoc/CS64/ClearedExternalBuffers.html)
		//
		//	This function tells the engine that all buffers have no memory of earlier filling.
		//	This means that even when buffer content didn't changed it will be updated when
		//	functions UpdateVertexBuffer(), UpdateIndexBuffer() and/or transformationBuffer()
		//	are called.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "ClearedExternalBuffers")]
        public static extern void ClearedExternalBuffers(Int64 model);

		//
		//		GetConceptualFaceCnt                        (http://rdf.bg/gkdoc/CS64/GetConceptualFaceCnt.html)
		//
		//	This function returns the number of conceptual faces for a certain instance.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceCnt")]
        public static extern Int64 GetConceptualFaceCnt(Int64 owlInstance);

		//
		//		GetConceptualFace                           (http://rdf.bg/gkdoc/CS64/GetConceptualFace.html)
		//
		//	This function returns a handle to the conceptual face. Be aware that different
		//	instances can return the same handles (however with possible different startIndices and noTriangles).
		//	Argument index should be at least zero and smaller then return value of GetConceptualFaceCnt().
		//	Argument startIndex shows the first index used.
		//	Argument noTriangles returns the number of triangles, each triangle is existing of 3 unique indices.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFace")]
        public static extern Int64 GetConceptualFace(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noTriangles);

		//
		//		GetConceptualFaceEx                         (http://rdf.bg/gkdoc/CS64/GetConceptualFaceEx.html)
		//
		//	This function returns a handle to the conceptual face. Be aware that different
		//	instances can return the same handles (however with possible different startIndices and noTriangles).
		//	Argument index should be at least zero and smaller then return value of GetConceptualFaceCnt().
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

		//
		//		GetConceptualFaceMaterial                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceMaterial.html)
		//
		//	This function returns the material instance relevant for this
		//	conceptual face.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceMaterial")]
        public static extern Int64 GetConceptualFaceMaterial(Int64 conceptualFace);

		//
		//		GetConceptualFaceOriginCnt                  (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginCnt.html)
		//
		//	This function returns the number of instances that are the source primitive/concept
		//	for this conceptual face.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOriginCnt")]
        public static extern Int64 GetConceptualFaceOriginCnt(Int64 conceptualFace);

		//
		//		GetConceptualFaceOrigin                     (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOrigin.html)
		//
		//	This function returns a handle to the instance that is the source primitive/concept
		//	for this conceptual face.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOrigin")]
        public static extern Int64 GetConceptualFaceOrigin(Int64 conceptualFace, Int64 index);

		//
		//		GetConceptualFaceOriginEx                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginEx.html)
		//
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOriginEx")]
        public static extern void GetConceptualFaceOriginEx(Int64 conceptualFace, Int64 index, out Int64 originatingOwlInstance, out Int64 originatingConceptualFace);

		//
		//		GetFaceCnt                                  (http://rdf.bg/gkdoc/CS64/GetFaceCnt.html)
		//
		//	This function returns the number of faces for a certain instance.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetFaceCnt")]
        public static extern Int64 GetFaceCnt(Int64 owlInstance);

		//
		//		GetFace                                     (http://rdf.bg/gkdoc/CS64/GetFace.html)
		//
		//	This function gets the individual faces including the meta data, i.e. the number of openings within this specific face.
		//	This call is for very dedicated use, it would be more common to iterate over the individual conceptual faces.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetFace")]
        public static extern void GetFace(Int64 owlInstance, Int64 index, out Int64 startIndex, out Int64 noOpenings);

		//
		//		GetDependingPropertyCnt                     (http://rdf.bg/gkdoc/CS64/GetDependingPropertyCnt.html)
		//
		//	This function returns the number of properties that are of influence on the
		//	location and form of the conceptualFace.
		//
		//	Note: BE AWARE, THIS FUNCTION EXPECTS A TREE, NOT A NETWORK, IN CASE OF A NETWORK THIS FUNCTION CAN LOCK THE ENGINE
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetDependingPropertyCnt")]
        public static extern Int64 GetDependingPropertyCnt(Int64 baseOwlInstance, Int64 conceptualFace);

		//
		//		GetDependingProperty                        (http://rdf.bg/gkdoc/CS64/GetDependingProperty.html)
		//
		//	This function returns a handle to the property that is the 'index'-th property
		//	of influence on the form. It also returns the handle to instance this property
		//	belongs to.
		//
		//	Note: the returned property is always a datatypeProperty
		//	Note: if input is incorrect (for example index is in wrong domain) _property and
		//		  instance will be both zero.
		//	Note: BE AWARE, THIS FUNCTION EXPECTS A TREE, NOT A NETWORK, IN CASE OF A NETWORK THIS FUNCTION CAN LOCK THE ENGINE
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetDependingProperty")]
        public static extern void GetDependingProperty(Int64 baseOwlInstance, Int64 conceptualFace, Int64 index, out Int64 owlInstance, out Int64 datatypeProperty);

		//
		//		SetFormat                                   (http://rdf.bg/gkdoc/CS64/SetFormat.html)
		//
		//	This function sets the type of export format, by setting a mask
		//		bit 0 & 1:
		//			00	Each vertex is unique (although mostly irrelevant UpdateIndexBuffer() and 
		//				UpdateTransformationBuffer() are still returning information)
		//			01	Each index is unique => vertices are not necessarily (although mostly
		//				irrelevant UpdateTransformationBuffer() is still returning information)
		//			10	Single level Transformations are used, most optimal when using DirectX till version 11
		//				and OpenGL till version 2
		//			11	Nested Transformations are used, most optimal but till 2011 no known support of
		//				low level 3D interfaces like DirectX and OpenGL
		//		bit 2:
		//			0	Vertex items returned as float (4 byte/32 bit)
		//			1	Vertex items returned as double (8 byte/64 bit)
		//		bit 3:
		//			0	Index items returned as int32_t (4 byte/32 bit)
		//			1	Index items returned as int64_t (8 byte/64 bit) (only available in 64 bit mode)
		//
		//		bit 4:
		//			0	Vertex does not contain 3D point info
		//			1	Vertex does contain 3D point info
		//		bit 5:
		//			0	Vertex does not contain 3D normal vector info
		//			1	Vertex does contain 3D normal vector info => if set, bit 4 will also be set
		//		bit 6:
		//			0	Vertex does not contain first 2D texture info
		//			1	Vertex does contain first 2D texture info
		//		bit 7:
		//			0	Vertex does not contain second 2D texture info
		//			1	Vertex does contain second 2D texture info => if set, bit 6 will also be set
		//
		//		bit 8:	
		//			0	No object form triangles are exported
		//			1	Object form triangles are exported (effective if instance contains faces and/or solids)
		//		bit 9:
		//			0	No object polygon lines are exported
		//			1	Object polygon lines are exported (effective if instance contains line representations)
		//		bit 10:
		//			0	No object points are exported
		//			1	Object points are exported (effective if instance contains point representations)
		//
		//		bit 11:	Reserved, by default 0
		//
		//		bit 12:
		//			0	No object face polygon lines are exported
		//			1	Object face polygon lines (dense wireframe) are exported => if set, bit 8 will also be set
		//		bit 13:
		//			0	No object conceptual face polygon lines are exported
		//			1	Object conceptual face polygon lines (wireframe) are exported => if set, bit 12 will also be set
		//		bit 14:	
		//			0	Polygon lines (wireframe) exported as list, i.e. typical 4 point polygon exported as  0 1 2 3 0 -1
		//			1	Polygon lines (wireframe) exported as tuples, i.e. typical 4 point polygon exported as 0 1 1 2 2 3 3 0
		//
		//		bit 15:
		//			0	All normals of triangles are transformed orthogonal to the 2D face they belong to
		//			1	Normals are exported to be in line with the original semantic form description (could be non orthogonal to the 2D face) 
		//
		//		bit 16: 
		//			0	no specific behavior
		//			1	Where possible DirectX compatibility is given to exported data (i.e. order of components in vertices)
		//					 => [bit 20, bit 21 both UNSET]
		//					 => if set, bit 17 will be unset
		//
		//		bit 17: 
		//			0	no specific behavior
		//			1	Where possible OpenGL compatibility is given to exported data (i.e. order of components in vertices and inverted texture coordinates in Y direction)
		//					 => [bit 20, bit 21 both SET]
		//					 => if set, bit 16 will be unset
		//
		//		bit 18:
		//			0	All faces are defined as calculated
		//			1	Every face has exactly one opposite face (normally both index and vertex array are doubled in size)
		//
		//		bit 19:	Reserved, by default 0
		//
		//		bit 20-23:
		//			0000	version 0 (used in case there is different behavior between versions in DirectX or OpenGL)
		//			....	...
		//			1111	version 15
		//
		//		bit 20:
		//			0	Standard Triangle Rotation (LHS as expected by DirectX) 
		//			1	Opposite Triangle Rotation (RHS as expected by OpenGL)
		//		bit 21:
		//			0	X, Y, Z (nX, nY, nZ) formatted as <X Y Z> considering internal concepts
		//			1	X, Y, Z (nX, nY, nZ) formatted as <X -Z Y>, i.e. X, -Z, Y (nX, -nZ, nY) considering internal concepts (OpenGL)
		//
		//		bit 24:
		//			0	Vertex does not contain Ambient color information
		//			1	Vertex does contain Ambient color information
		//		bit 25:
		//			0	Vertex does not contain Diffuse color information
		//			1	Vertex does contain Diffuse color information
		//		bit 26:
		//			0	Vertex does not contain Emissive color information
		//			1	Vertex does contain Emissive color information
		//		bit 27:
		//			0	Vertex does not contain Specular color information
		//			1	Vertex does contain Specular color information
		//
		//		bit 28:
		//			0	Vertex does not contain tangent vector for first texture
		//			1	Vertex does contain tangent vector for first texture => if set, bit 6 will also be set
		//		bit 29:
		//			0	Vertex does not contain binormal vector for first texture
		//			1	Vertex does contain binormal vector for first texture => if set, bit 6 will also be set
		//		bit 30:			ONLY WORKS IN 64 BIT MODE
		//			0	Vertex does not contain tangent vector for second texture
		//			1	Vertex does contain tangent vector for second texture => if set, bit 6 will also be set
		//		bit 31:			ONLY WORKS IN 64 BIT MODE
		//			0	Vertex does not contain binormal vector for second texture
		//			1	Vertex does contain binormal vector for second texture => if set, bit 6 will also be set
		//
		//		bit 26-31:	Reserved, by default 0
		//
		//		bit 32-63:	Reserved, by default 0
		//
		//	Note: default setting is 0000 0000 0000 0000   0000 0000 0000 0000  -  0000 0000 0000 0000   1000 0001  0011 0000 = h0000 0000 - 0000 8130 = 33072
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetFormat")]
        public static extern Int64 SetFormat(Int64 model, Int64 setting, Int64 mask);

		//
		//		GetFormat                                   (http://rdf.bg/gkdoc/CS64/GetFormat.html)
		//
		//	Returns the current format given a mask.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetFormat")]
        public static extern Int64 GetFormat(Int64 model, Int64 mask);

		//
		//		SetBehavior                                 (http://rdf.bg/gkdoc/CS64/SetBehavior.html)
		//
		//	This function sets the type of behavior, by setting a mask
		//
		//		bit 0-7:	Reserved, by default 0
		//
		//		bit 8:
		//			0	Do not optimize
		//			1	Vertex items returned as double (8 byte/64 bit)
		//
		//		bit 9-31:	Reserved, by default 0
		//
		//		bit 32-63:	Reserved, by default 0
		//
		//	Note: default setting is 0000 0000 0000 0000   0000 0000 0000 0000  -  0000 0000 0000 0000   0000 0001  0000 0000 = h0000 0000 - 0000 0100 = 256
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetBehavior")]
        public static extern void SetBehavior(Int64 model, Int64 setting, Int64 mask);

		//
		//		GetBehavior                                 (http://rdf.bg/gkdoc/CS64/GetBehavior.html)
		//
		//	Returns the current behavior given a mask.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetBehavior")]
        public static extern Int64 GetBehavior(Int64 model, Int64 mask);

		//
		//		SetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/SetVertexBufferTransformation.html)
		//
		//	Sets the transformation for a Vertex Buffer.
		//	The transformation will always be calculated in 64 bit, even if the vertex element size is 32 bit.
		//	This function can be called just before updating the vertex buffer.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetVertexBufferTransformation")]
        public static extern void SetVertexBufferTransformation(Int64 model, out double matrix);

		//
		//		GetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/GetVertexBufferTransformation.html)
		//
		//	Gets the transformation for a Vertex Buffer.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetVertexBufferTransformation")]
        public static extern void GetVertexBufferTransformation(Int64 model, out double matrix);

		//
		//		SetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/SetIndexBufferOffset.html)
		//
		//	Sets the offset for an Index Buffer.
		//	It is important call this function before updating the vertex buffer. 
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetIndexBufferOffset")]
        public static extern void SetIndexBufferOffset(Int64 model, Int64 offset);

		//
		//		GetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/GetIndexBufferOffset.html)
		//
		//	Gets the current offset for an Index Buffer.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetIndexBufferOffset")]
        public static extern Int64 GetIndexBufferOffset(Int64 model);

		//
		//		SetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/SetVertexBufferOffset.html)
		//
		//	Sets the offset for a Vertex Buffer.
		//	The offset will always be calculated in 64 bit, even if the vertex element size is 32 bit.
		//	This function can be called just before updating the vertex buffer.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetVertexBufferOffset")]
        public static extern void SetVertexBufferOffset(Int64 model, double x, double y, double z);

		//
		//		GetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/GetVertexBufferOffset.html)
		//
		//	Gets the offset for a Vertex Buffer.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetVertexBufferOffset")]
        public static extern void GetVertexBufferOffset(Int64 model, out double x, out double y, out double z);

		//
		//		SetDefaultColor                             (http://rdf.bg/gkdoc/CS64/SetDefaultColor.html)
		//
		//	Set the default values for the colors defined as argument.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetDefaultColor")]
        public static extern void SetDefaultColor(Int64 model, Int32 ambient, Int32 diffuse, Int32 emissive, Int32 specular);

		//
		//		GetDefaultColor                             (http://rdf.bg/gkdoc/CS64/GetDefaultColor.html)
		//
		//	Retrieve the default values for the colors defined as argument.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetDefaultColor")]
        public static extern void GetDefaultColor(Int64 model, out Int32 ambient, out Int32 diffuse, out Int32 emissive, out Int32 specular);

		//
		//		CheckConsistency                            (http://rdf.bg/gkdoc/CS64/CheckConsistency.html)
		//
		//	This function returns information about the consistency of each instance.
		//
		//	The mask defined what type of information can be retrieved from this call, the mask is a bitwise definition.
		//
		//		bit 0:	Check Design Tree Consistency
		//		bit 1:	Check Consistency for Triangle Output (through API)
		//		bit 2:	Check Consistency for Line Output (through API)
		//		bit 3:	Check Consistency for Point Output (through API)
		//		bit 4:	Check Consistency for Generated Surfaces (through API)
		//		bit 5:	Check Consistency for Generated Surfaces (internal)
		//		bit 6:	Check Consistency for Generated Solids (through API)
		//		bit 7:	Check Consistency for Generated Solids (internal)
		//		bit 8:	Check Consistency for BoundingBox's
		//		bit 9:	Check Consistency for Triangulation
		//		bit 10: Check Consistency for Relations (through API)
		//
		//		bit 16:	Contains (Closed) Solid(s)
		//		bit 18:	Contains (Closed) Infinite Solid(s)
		//		bit 20:	Contains Closed Surface(s)
		//		bit 21:	Contains Open Surface(s)
		//		bit 22:	Contains Closed Infinite Surface(s)
		//		bit 23:	Contains Open Infinite Surface(s)
		//		bit 24:	Contains Closed Line(s)
		//		bit 25:	Contains Open Line(s)
		//		bit 26:	Contains Closed Infinite Line(s) [i.e. both ends in infinity]
		//		bit 27:	Contains Open Infinite Line(s) [i.e. one end in infinity]
		//		bit 28:	Contains (Closed) Point(s)
		//
		//	If a bit in the mask is set and the result of the check has an issue, the resulting value will have this bit set.
		//	i.e. any non-zero return value in Check Consistency is an indication that something is wrong or unexpected; 
		//	any non-zero return value in Contains is an indication that this type of geometry is expected in one of the instances; 
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CheckConsistency")]
        public static extern Int64 CheckConsistency(Int64 model, Int64 mask);

		//
		//		CheckInstanceConsistency                    (http://rdf.bg/gkdoc/CS64/CheckInstanceConsistency.html)
		//
		//	This function returns information about the consistency of the instance and indirectly referenced instances.
		//
		//	The mask defined what type of information can be retrieved from this call, the mask is a bitwise definition.
		//
		//		bit 0:	Check Design Tree Consistency
		//		bit 1:	Check Consistency for Triangle Output (through API)
		//		bit 2:	Check Consistency for Line Output (through API)
		//		bit 3:	Check Consistency for Point Output (through API)
		//		bit 4:	Check Consistency for Generated Surfaces (through API)
		//		bit 5:	Check Consistency for Generated Surfaces (internal)
		//		bit 6:	Check Consistency for Generated Solids (through API)
		//		bit 7:	Check Consistency for Generated Solids (internal)
		//		bit 8:	Check Consistency for BoundingBox's
		//		bit 9:	Check Consistency for Triangulation
		//		bit 10: Check Consistency for Relations (through API)
		//
		//		bit 16:	Contains (Closed) Solid(s)
		//		bit 18:	Contains (Closed) Infinite Solid(s)
		//		bit 20:	Contains Closed Surface(s)
		//		bit 21:	Contains Open Surface(s)
		//		bit 22:	Contains Closed Infinite Surface(s)
		//		bit 23:	Contains Open Infinite Surface(s)
		//		bit 24:	Contains Closed Line(s)
		//		bit 25:	Contains Open Line(s)
		//		bit 26:	Contains Closed Infinite Line(s) [i.e. both ends in infinity]
		//		bit 27:	Contains Open Infinite Line(s) [i.e. one end in infinity]
		//		bit 28:	Contains (Closed) Point(s)
		//
		//	If a bit in the mask is set and the result of the check has an issue, the resulting value will have this bit set.
		//	i.e. any non-zero return value in Check Consistency is an indication that something is wrong or unexpected regarding the given instance; 
		//	any non-zero return value in Contains is an indication that this type of geometry is expected regarding the given instance; 
		//
        [DllImport(IFCEngineDLL, EntryPoint = "CheckInstanceConsistency")]
        public static extern Int64 CheckInstanceConsistency(Int64 owlInstance, Int64 mask);

        //
        //  Derived Geometry API Calls
        //

		//
		//		GetPerimeter                                (http://rdf.bg/gkdoc/CS64/GetPerimeter.html)
		//
		//	This function calculates the perimeter of an instance.
		//
		//	Note: internally the call does not store its results, any optimization based on known
		//		  dependancies between instances need to be implemented on the client.
		//	Note: due to internal structure using already calculated vertices/indices does not
		//		  give any performance benefits, in opposite to GetVolume and GetArea
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPerimeter")]
        public static extern double GetPerimeter(Int64 owlInstance);

		//
		//		GetArea                                     (http://rdf.bg/gkdoc/CS64/GetArea.html)
		//
		//	This function calculates the area of an instance.
		//	For perfomance reasons it is benefitial to call it with vertex and index array when
		//	the arrays are calculated anyway or Volume and Area are needed.
		//
		//	There are two ways to call GetVolume:
		//		vertices and indices are both zero: in this case the instance will be
		//				recalculated when needed. It is expected the client does not
		//				need the arrays itself or there is no performance issue.
		//		vertices and indices are both given: the call is placed directly after
		//				updateBuffer calls and no structural change to depending instances have 
		//				been done in between. The transformationMatrix array is not needed,
		//				even if it is being used due to not giving any performance gain to this
		//				operation.
		//
		//	Note: internally the call does not store its results, any optimization based on known
		//		  dependancies between instances need to be implemented on the client.
		//	Note: in case precision is important and vertex array is 32 bit it is advised to
		//		  set vertices and indices to 0 even if arrays are existing.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]
        public static extern double GetArea(Int64 owlInstance, ref float vertices, ref Int32 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]
        public static extern double GetArea(Int64 owlInstance, ref float vertices, ref Int64 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]
        public static extern double GetArea(Int64 owlInstance, ref double vertices, ref Int32 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]
        public static extern double GetArea(Int64 owlInstance, ref double vertices, ref Int64 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]
        public static extern double GetArea(Int64 owlInstance, IntPtr vertices, IntPtr indices);

		//
		//		GetVolume                                   (http://rdf.bg/gkdoc/CS64/GetVolume.html)
		//
		//	This function calculates the volume of an instance.
		//	For perfomance reasons it is benefitial to call it with vertex and index array when
		//	the arrays are calculated anyway or Volume and Area are needed.
		//
		//	There are two ways to call GetVolume:
		//		vertices and indices are both zero: in this case the instance will be
		//				recalculated when needed. It is expected the client does not
		//				need the arrays itself or there is no performance issue.
		//		vertices and indices are both given: the call is placed directly after
		//				updateBuffer calls and no structural change to depending instances have 
		//				been done in between. The transformationMatrix array is not needed,
		//				even if it is being used due to not giving any performance gain to this
		//				operation.
		//
		//	Note: internally the call does not store its results, any optimization based on known
		//		  dependancies between instances need to be implemented on the client.
		//	Note: in case precision is important and vertex array is 32 bit it is advised to
		//		  set vertices and indices to 0 even if arrays are existing.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, ref float vertices, ref Int32 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, ref float vertices, ref Int64 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, ref double vertices, ref Int32 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, ref double vertices, ref Int64 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, IntPtr vertices, IntPtr indices);

		//
		//		GetCentroid                                 (http://rdf.bg/gkdoc/CS64/GetCentroid.html)
		//
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, ref float vertices, ref Int32 indices, out double centroid);

        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, ref float vertices, ref Int64 indices, out double centroid);

        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, ref double vertices, ref Int32 indices, out double centroid);

        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, ref double vertices, ref Int64 indices, out double centroid);

        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, IntPtr vertices, IntPtr indices, out double centroid);

		//
		//		GetConceptualFacePerimeter                  (http://rdf.bg/gkdoc/CS64/GetConceptualFacePerimeter.html)
		//
		//	This function returns the perimeter of a given Conceptual Face.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFacePerimeter")]
        public static extern double GetConceptualFacePerimeter(Int64 conceptualFace);

		//
		//		GetConceptualFaceArea                       (http://rdf.bg/gkdoc/CS64/GetConceptualFaceArea.html)
		//
		//	This function returns the area of a given Conceptual Face. The attributes vertices
		//	and indices are optional but will improve performance if defined.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(Int64 conceptualFace, ref float vertices, ref Int32 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(Int64 conceptualFace, ref float vertices, ref Int64 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(Int64 conceptualFace, ref double vertices, ref Int32 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(Int64 conceptualFace, ref double vertices, ref Int64 indices);

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(Int64 conceptualFace, IntPtr vertices, IntPtr indices);

		//
		//		SetBoundingBoxReference                     (http://rdf.bg/gkdoc/CS64/SetBoundingBoxReference.html)
		//
		//	This function passes addresses from the hosting application. This enables
		//	the engine to update these values without extra need for API calls. This is
		//	especially of interest because the hosting application is not aware of what
		//	instances are updated and 
		//	The transformationMatrix has 12 double values: _11, _12, _13, _21, _22, _23, 
		//	_31, _32, _33, _41, _42, _43.
		//	The startVector is the leftundernear vector and the endVector is the 
		//	rightupperfar vector, in all cases values are doubles (64 bit).
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetBoundingBoxReference")]
        public static extern void SetBoundingBoxReference(Int64 owlInstance, out double transformationMatrix, out double startVector, out double endVector);

		//
		//		GetBoundingBox                              (http://rdf.bg/gkdoc/CS64/GetBoundingBox.html)
		//
		//	When the transformationMatrix is given, it will fill an array of 12 double values.
		//	When the transformationMatrix is left empty and both startVector and endVector are
		//	given the boundingbox without transformation is calculated and returned.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetBoundingBox")]
        public static extern void GetBoundingBox(Int64 owlInstance, out double transformationMatrix, out double startVector, out double endVector);

        [DllImport(IFCEngineDLL, EntryPoint = "GetBoundingBox")]
        public static extern void GetBoundingBox(Int64 owlInstance, IntPtr transformationMatrix, out double startVector, out double endVector);

		//
		//		GetRelativeTransformation                   (http://rdf.bg/gkdoc/CS64/GetRelativeTransformation.html)
		//
		//	This function returns the relative transformation matrix between two instances, i.e. in practise
		//	this means the matrices connected to the Transformation instances in the path in between.
		//	The matrix is only given when a unique path through inverse relations can be found,
		//	otherwise the identity matrix is returned.
		//	owlInstanceHead is allowed to be not defined, i.e. zero.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetRelativeTransformation")]
        public static extern void GetRelativeTransformation(Int64 owlInstanceHead, Int64 owlInstanceTail, out double transformationMatrix);

        //
        //  Deprecated API Calls
        //

		//
		//		GetTriangles                                (http://rdf.bg/gkdoc/CS64/GetTriangles___.html)
		//
		//	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant triangles when
		//	SetFormat() is setting bit 8 and unsetting with bit 9, 10, 12 and 13 
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetTriangles")]
        public static extern void GetTriangles(Int64 owlInstance, out Int64 startIndex, out Int64 noTriangles, out Int64 startVertex, out Int64 firstNotUsedVertex);

		//
		//		GetLines                                    (http://rdf.bg/gkdoc/CS64/GetLines___.html)
		//
		//	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant lines when
		//	SetFormat() is setting bit 9 and unsetting with bit 8, 10, 12 and 13 
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetLines")]
        public static extern void GetLines(Int64 owlInstance, out Int64 startIndex, out Int64 noLines, out Int64 startVertex, out Int64 firstNotUsedVertex);

		//
		//		GetPoints                                   (http://rdf.bg/gkdoc/CS64/GetPoints___.html)
		//
		//	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant points when
		//	SetFormat() is setting bit 10 and unsetting with bit 8, 9, 12 and 13 
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPoints")]
        public static extern void GetPoints(Int64 owlInstance, out Int64 startIndex, out Int64 noPoints, out Int64 startVertex, out Int64 firstNotUsedVertex);

		//
		//		GetPropertyRestrictions                     (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictions___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyRestrictions")]
        public static extern void GetPropertyRestrictions(Int64 owlClass, Int64 rdfProperty, out Int64 minCard, out Int64 maxCard);

		//
		//		GetPropertyRestrictionsConsolidated         (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictionsConsolidated___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,
		//	just rename the function name.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyRestrictionsConsolidated")]
        public static extern void GetPropertyRestrictionsConsolidated(Int64 owlClass, Int64 rdfProperty, out Int64 minCard, out Int64 maxCard);

		//
		//		IsGeometryType                              (http://rdf.bg/gkdoc/CS64/IsGeometryType___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetGeometryClass instead, rename the function name
		//	and interpret non-zero as true and zero as false.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "IsGeometryType")]
        public static extern byte IsGeometryType(Int64 owlClass);

		//
		//		SetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/SetObjectTypeProperty___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call SetObjectProperty instead, just rename the function name.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetObjectTypeProperty")]
        public static extern Int64 SetObjectTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

		//
		//		GetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/GetObjectTypeProperty___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetObjectProperty instead, just rename the function name.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetObjectTypeProperty")]
        public static extern Int64 GetObjectTypeProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr values, out Int64 card);

		//
		//		SetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/SetDataTypeProperty___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call SetDatatypeProperty instead, just rename the function name.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref byte values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, byte[] values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, Int64[] values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref double values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, double[] values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref string values, Int64 card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, string[] values, Int64 card);

		//
		//		GetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/GetDataTypeProperty___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetDatatypeProperty instead, just rename the function name.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetDataTypeProperty")]
        public static extern Int64 GetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr values, out Int64 card);

		//
		//		InstanceCopyCreated                         (http://rdf.bg/gkdoc/CS64/InstanceCopyCreated___.html)
		//
		//	This call is deprecated as the Copy concept is also deprecated and will be removed by end of 2020.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "InstanceCopyCreated")]
        public static extern void InstanceCopyCreated(Int64 owlInstance);

		//
		//		GetPropertyByNameAndType                    (http://rdf.bg/gkdoc/CS64/GetPropertyByNameAndType___.html)
		//
		//	This call is deprecated and will be removed by end of 2020.
		//	Please use the call GetPropertyByName(Ex) / GetPropertyByNameW(Ex) + GetPropertyType(Ex) instead, just rename the function name.
		//
        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameAndType")]
        public static extern Int64 GetPropertyByNameAndType(Int64 model, string name, Int64 rdfPropertyType);

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameAndType")]
        public static extern Int64 GetPropertyByNameAndType(Int64 model, byte[] name, Int64 rdfPropertyType);
    }
}
