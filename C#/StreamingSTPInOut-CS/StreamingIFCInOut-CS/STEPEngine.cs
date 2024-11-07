using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
#if _WIN64
		using int_t = System.Int64;
#else
		using int_t = System.Int32;
#endif

namespace StepEngine
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

        public const string STEPEngineDLL = @"STEPEngine.dll";

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
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern Int64 sdaiCreateModelBN(Int64 repository, string fileName, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern Int64 sdaiCreateModelBN(Int64 repository, string fileName, byte[] schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern Int64 sdaiCreateModelBN(Int64 repository, byte[] fileName, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateModelBN")]
        public static extern Int64 sdaiCreateModelBN(Int64 repository, byte[] fileName, byte[] schemaName);

		//
		//		sdaiCreateModelBNUnicode                    (http://rdf.bg/ifcdoc/CS64/sdaiCreateModelBNUnicode.html)
		//
		//	This function creates and empty model (we expect with a schema file given).
		//	Attributes repository and fileName will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern Int64 sdaiCreateModelBNUnicode(Int64 repository, string fileName, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern Int64 sdaiCreateModelBNUnicode(Int64 repository, string fileName, byte[] schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern Int64 sdaiCreateModelBNUnicode(Int64 repository, byte[] fileName, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]
        public static extern Int64 sdaiCreateModelBNUnicode(Int64 repository, byte[] fileName, byte[] schemaName);

		//
		//		sdaiOpenModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBN.html)
		//
		//	This function opens the model on location fileName.
		//	Attribute repository will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern Int64 sdaiOpenModelBN(Int64 repository, string fileName, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern Int64 sdaiOpenModelBN(Int64 repository, string fileName, byte[] schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern Int64 sdaiOpenModelBN(Int64 repository, byte[] fileName, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiOpenModelBN")]
        public static extern Int64 sdaiOpenModelBN(Int64 repository, byte[] fileName, byte[] schemaName);

		//
		//		sdaiOpenModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBNUnicode.html)
		//
		//	This function opens the model on location fileName.
		//	Attribute repository will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern Int64 sdaiOpenModelBNUnicode(Int64 repository, string fileName, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern Int64 sdaiOpenModelBNUnicode(Int64 repository, string fileName, byte[] schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern Int64 sdaiOpenModelBNUnicode(Int64 repository, byte[] fileName, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]
        public static extern Int64 sdaiOpenModelBNUnicode(Int64 repository, byte[] fileName, byte[] schemaName);

		//
		//		engiOpenModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiOpenModelByStream.html)
		//
		//	This function opens the model via a stream.
		//	Attribute repository will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiOpenModelByStream")]
        public static extern int_t engiOpenModelByStream(int_t repository, [MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "engiOpenModelByStream")]
        public static extern int_t engiOpenModelByStream(int_t repository, [MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback, byte[] schemaName);

		//
		//		engiOpenModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiOpenModelByArray.html)
		//
		//	This function opens the model via an array.
		//	Attribute repository will be ignored, they are their because of backward compatibility.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiOpenModelByArray")]
        public static extern Int64 engiOpenModelByArray(Int64 repository, byte[] content, Int64 size, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "engiOpenModelByArray")]
        public static extern Int64 engiOpenModelByArray(Int64 repository, byte[] content, Int64 size, byte[] schemaName);

		//
		//		sdaiSaveModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBN.html)
		//
		//	This function saves the model (char file name).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelBN")]
        public static extern void sdaiSaveModelBN(Int64 model, byte[] fileName);

		//
		//		sdaiSaveModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBNUnicode.html)
		//
		//	This function saves the model (wchar, i.e. Unicode file name).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]
        public static extern void sdaiSaveModelBNUnicode(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]
        public static extern void sdaiSaveModelBNUnicode(Int64 model, byte[] fileName);

		//
		//		engiSaveModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiSaveModelByStream.html)
		//
		//	This function saves the model as an array.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiSaveModelByStream")]
        public static extern void engiSaveModelByStream(Int64 model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, Int64 size);

		//
		//		engiSaveModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiSaveModelByArray.html)
		//
		//	This function saves the model as an array.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiSaveModelByArray")]
        public static extern void engiSaveModelByArray(Int64 model, byte[] content, out int_t size);

		//
		//		sdaiSaveModelAsXmlBN                        (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBN.html)
		//
		//	This function saves the model as XML according to IFC2x3's way of XML serialization (char file name).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]
        public static extern void sdaiSaveModelAsXmlBN(Int64 model, byte[] fileName);

		//
		//		sdaiSaveModelAsXmlBNUnicode                 (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBNUnicode.html)
		//
		//	This function saves the model as XML according to IFC2x3's way of XML serialization (wchar, i.e. Unicode file name).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]
        public static extern void sdaiSaveModelAsXmlBNUnicode(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]
        public static extern void sdaiSaveModelAsXmlBNUnicode(Int64 model, byte[] fileName);

		//
		//		sdaiSaveModelAsSimpleXmlBN                  (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBN.html)
		//
		//	This function saves the model as XML according to IFC4's way of XML serialization (char file name).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]
        public static extern void sdaiSaveModelAsSimpleXmlBN(Int64 model, byte[] fileName);

		//
		//		sdaiSaveModelAsSimpleXmlBNUnicode           (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBNUnicode.html)
		//
		//	This function saves the model as XML according to IFC4's way of XML serialization (wchar, i.e. Unicode file name).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]
        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]
        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(Int64 model, byte[] fileName);

		//
		//		sdaiCloseModel                              (http://rdf.bg/ifcdoc/CS64/sdaiCloseModel.html)
		//
		//	This function closes the model. After this call no instance handles will be available including all
		//	handles referencing the geometry of this specific file, in default compilation the model itself will
		//	be known in the kernel, however known to be disabled. Calls containing the model reference will be
		//	protected from crashing when called.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCloseModel")]
        public static extern void sdaiCloseModel(Int64 model);

		//
		//		setPrecisionDoubleExport                    (http://rdf.bg/ifcdoc/CS64/setPrecisionDoubleExport.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "setPrecisionDoubleExport")]
        public static extern void setPrecisionDoubleExport(Int64 model, Int64 precisionCap, Int64 precisionRound, byte clean);

        //
        //  Schema Reading API Calls
        //

		//
		//		sdaiGetEntity                               (http://rdf.bg/ifcdoc/CS64/sdaiGetEntity.html)
		//
		//	This call retrieves a handle to an entity based on a given entity name.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern Int64 sdaiGetEntity(Int64 model, string entityName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetEntity")]
        public static extern Int64 sdaiGetEntity(Int64 model, byte[] entityName);

		//
		//		engiGetEntityArgument                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgument.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityArgument")]
        public static extern Int64 engiGetEntityArgument(Int64 entity, string argumentName);

        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityArgument")]
        public static extern Int64 engiGetEntityArgument(Int64 entity, byte[] argumentName);

		//
		//		engiGetEntityArgumentIndex                  (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentIndex.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityArgumentIndex")]
        public static extern Int64 engiGetEntityArgumentIndex(Int64 entity, string argumentName);

        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityArgumentIndex")]
        public static extern Int64 engiGetEntityArgumentIndex(Int64 entity, byte[] argumentName);

		//
		//		engiGetEntityArgumentName                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentName.html)
		//
		//	This call can be used to retrieve the name of the n-th argument of the given entity. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityArgumentName")]
        public static extern void engiGetEntityArgumentName(Int64 entity, Int64 index, Int64 valueType, out IntPtr argumentName);

		//
		//		engiGetEntityArgumentType                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentType.html)
		//
		//	This call can be used to retrieve the type of the n-th argument of the given entity. In case of a select argument no relevant information is given by this call as it depends on the instance. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityArgumentType")]
        public static extern void engiGetEntityArgumentType(Int64 entity, Int64 index, out int_t argumentType);

		//
		//		engiGetEntityCount                          (http://rdf.bg/ifcdoc/CS64/engiGetEntityCount.html)
		//
		//	Returns the total number of entities within the loaded schema.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityCount")]
        public static extern Int64 engiGetEntityCount(Int64 model);

		//
		//		engiGetEntityElement                        (http://rdf.bg/ifcdoc/CS64/engiGetEntityElement.html)
		//
		//	This call returns a specific entity based on an index, the index needs to be 0 or higher but lower then the number of entities in the loaded schema.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityElement")]
        public static extern Int64 engiGetEntityElement(Int64 model, Int64 index);

		//
		//		sdaiGetEntityExtent                         (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtent.html)
		//
		//	This call retrieves an aggregation that contains all instances of the entity given.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetEntityExtent")]
        public static extern Int64 sdaiGetEntityExtent(Int64 model, Int64 entity);

		//
		//		sdaiGetEntityExtentBN                       (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtentBN.html)
		//
		//	This call retrieves an aggregation that contains all instances of the entity given.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern Int64 sdaiGetEntityExtentBN(Int64 model, string entityName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]
        public static extern Int64 sdaiGetEntityExtentBN(Int64 model, byte[] entityName);

		//
		//		engiGetEntityName                           (http://rdf.bg/ifcdoc/CS64/engiGetEntityName.html)
		//
		//	This call can be used to get the name of the given entity.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityName")]
        public static extern void engiGetEntityName(Int64 entity, Int64 valueType, out IntPtr entityName);

		//
		//		engiGetEntityNoArguments                    (http://rdf.bg/ifcdoc/CS64/engiGetEntityNoArguments.html)
		//
		//	This call returns the number of arguments, this includes the arguments of its (nested) parents and inverse argumnets.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityNoArguments")]
        public static extern Int64 engiGetEntityNoArguments(Int64 entity);

		//
		//		engiGetEntityParent                         (http://rdf.bg/ifcdoc/CS64/engiGetEntityParent.html)
		//
		//	Returns the direct parent entity, for example the parent of IfcObject is IfcObjectDefinition, of IfcObjectDefinition is IfcRoot and of IfcRoot is 0.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityParent")]
        public static extern Int64 engiGetEntityParent(Int64 entity);

		//
		//		engiGetAttrOptional                         (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptional.html)
		//
		//	This call can be used to check if an attribute is optional
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttrOptional")]
        public static extern Int64 engiGetAttrOptional(ref int_t attribute);

		//
		//		engiGetAttrOptionalBN                       (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptionalBN.html)
		//
		//	This call can be used to check if an attribute is optional
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttrOptionalBN")]
        public static extern Int64 engiGetAttrOptionalBN(Int64 entity, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttrOptionalBN")]
        public static extern Int64 engiGetAttrOptionalBN(Int64 entity, byte[] attributeName);

		//
		//		engiGetAttrInverse                          (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverse.html)
		//
		//	This call can be used to check if an attribute is an inverse relation
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttrInverse")]
        public static extern Int64 engiGetAttrInverse(ref int_t attribute);

		//
		//		engiGetAttrInverseBN                        (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverseBN.html)
		//
		//	This call can be used to check if an attribute is an inverse relation
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttrInverseBN")]
        public static extern Int64 engiGetAttrInverseBN(Int64 entity, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttrInverseBN")]
        public static extern Int64 engiGetAttrInverseBN(Int64 entity, byte[] attributeName);

		//
		//		engiGetEnumerationValue                     (http://rdf.bg/ifcdoc/CS64/engiGetEnumerationValue.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEnumerationValue")]
        public static extern void engiGetEnumerationValue(Int64 attribute, Int64 index, Int64 valueType, out IntPtr enumerationValue);

		//
		//		engiGetEntityProperty                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityProperty.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetEntityProperty")]
        public static extern void engiGetEntityProperty(Int64 entity, Int64 index, out IntPtr propertyName, out int_t optional, out int_t type, out int_t _array, out int_t set, out int_t list, out int_t bag, out int_t min, out int_t max, out int_t referenceEntity, out int_t inverse);

        //
        //  Instance Header API Calls
        //

		//
		//		SetSPFFHeader                               (http://rdf.bg/ifcdoc/CS64/SetSPFFHeader.html)
		//
		//	This call is an aggregate of several SetSPFFHeaderItem calls. In several cases the header can be set easily with this call. In case an argument is zero, this argument will not be updated, i.e. it will not be filled with 0.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(Int64 model, string description, string implementationLevel, string name, string timeStamp, string author, string organization, string preprocessorVersion, string originatingSystem, string authorization, string fileSchema);

        [DllImport(STEPEngineDLL, EntryPoint = "SetSPFFHeader")]
        public static extern void SetSPFFHeader(Int64 model, byte[] description, byte[] implementationLevel, byte[] name, byte[] timeStamp, byte[] author, byte[] organization, byte[] preprocessorVersion, byte[] originatingSystem, byte[] authorization, byte[] fileSchema);

		//
		//		SetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/SetSPFFHeaderItem.html)
		//
		//	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern Int64 SetSPFFHeaderItem(Int64 model, Int64 itemIndex, Int64 itemSubIndex, Int64 valueType, string value);

        [DllImport(STEPEngineDLL, EntryPoint = "SetSPFFHeaderItem")]
        public static extern Int64 SetSPFFHeaderItem(Int64 model, Int64 itemIndex, Int64 itemSubIndex, Int64 valueType, byte[] value);

		//
		//		GetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItem.html)
		//
		//	This call can be used to read a specific header item, the source code example is larger to show and explain how this call can be used.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetSPFFHeaderItem")]
        public static extern Int64 GetSPFFHeaderItem(Int64 model, Int64 itemIndex, Int64 itemSubIndex, Int64 valueType, out IntPtr value);

		//
		//		GetSPFFHeaderItemUnicode                    (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItemUnicode.html)
		//
		//	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetSPFFHeaderItemUnicode")]
        public static extern Int64 GetSPFFHeaderItemUnicode(Int64 model, Int64 itemIndex, Int64 itemSubIndex, string buffer, Int64 bufferLength);

        [DllImport(STEPEngineDLL, EntryPoint = "GetSPFFHeaderItemUnicode")]
        public static extern Int64 GetSPFFHeaderItemUnicode(Int64 model, Int64 itemIndex, Int64 itemSubIndex, byte[] buffer, Int64 bufferLength);

        //
        //  Instance Reading API Calls
        //

		//
		//		sdaiGetADBType                              (http://rdf.bg/ifcdoc/CS64/sdaiGetADBType.html)
		//
		//	This call can be used to get the used type within this ADB type.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetADBType")]
        public static extern Int64 sdaiGetADBType(ref int_t ADB);

		//
		//		sdaiGetADBTypePath                          (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePath.html)
		//
		//	This call can be used to get the path of an ADB type.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetADBTypePath")]
        public static extern IntPtr sdaiGetADBTypePath(ref int_t ADB, Int64 typeNameNumber);

		//
		//		sdaiGetADBTypePathx                         (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePathx.html)
		//
		//	This call is the same as sdaiGetADBTypePath, however can be used by porting to languages that have issues with returned char arrays.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetADBTypePathx")]
        public static extern void sdaiGetADBTypePathx(ref int_t ADB, Int64 typeNameNumber, out IntPtr path);

		//
		//		sdaiGetADBValue                             (http://rdf.bg/ifcdoc/CS64/sdaiGetADBValue.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(ref int_t ADB, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(ref int_t ADB, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetADBValue")]
        public static extern void sdaiGetADBValue(ref int_t ADB, Int64 valueType, out IntPtr value);

		//
		//		engiGetAggrElement                          (http://rdf.bg/ifcdoc/CS64/engiGetAggrElement.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern Int64 engiGetAggrElement(Int64 aggregate, Int64 elementIndex, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern Int64 engiGetAggrElement(Int64 aggregate, Int64 elementIndex, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAggrElement")]
        public static extern Int64 engiGetAggrElement(Int64 aggregate, Int64 elementIndex, Int64 valueType, out IntPtr value);

		//
		//		engiGetAggrType                             (http://rdf.bg/ifcdoc/CS64/engiGetAggrType.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAggrType")]
        public static extern void engiGetAggrType(Int64 aggregate, out int_t aggragateType);

		//
		//		engiGetAggrTypex                            (http://rdf.bg/ifcdoc/CS64/engiGetAggrTypex.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAggrTypex")]
        public static extern void engiGetAggrTypex(Int64 aggregate, out int_t aggragateType);

		//
		//		sdaiGetAttr                                 (http://rdf.bg/ifcdoc/CS64/sdaiGetAttr.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern Int64 sdaiGetAttr(Int64 instance, Int64 attribute, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern Int64 sdaiGetAttr(Int64 instance, Int64 attribute, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttr")]
        public static extern Int64 sdaiGetAttr(Int64 instance, Int64 attribute, Int64 valueType, out IntPtr value);

		//
		//		sdaiGetAttrBN                               (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBN.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, string attributeName, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, string attributeName, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, string attributeName, Int64 valueType, out IntPtr value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrBN")]
        public static extern Int64 sdaiGetAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, out IntPtr value);

		//
		//		sdaiGetAttrBNUnicode                        (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBNUnicode.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode")]
        public static extern Int64 sdaiGetAttrBNUnicode(Int64 instance, string attributeName, string buffer, Int64 bufferLength);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode")]
        public static extern Int64 sdaiGetAttrBNUnicode(Int64 instance, byte[] attributeName, byte[] buffer, Int64 bufferLength);

		//
		//		sdaiGetStringAttrBN                         (http://rdf.bg/ifcdoc/CS64/sdaiGetStringAttrBN.html)
		//
		//	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiSTRING.
		//	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,
		//	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetStringAttrBN")]
        public static extern IntPtr sdaiGetStringAttrBN(Int64 instance, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetStringAttrBN")]
        public static extern IntPtr sdaiGetStringAttrBN(Int64 instance, byte[] attributeName);

		//
		//		sdaiGetInstanceAttrBN                       (http://rdf.bg/ifcdoc/CS64/sdaiGetInstanceAttrBN.html)
		//
		//	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiINSTANCE.
		//	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,
		//	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetInstanceAttrBN")]
        public static extern Int64 sdaiGetInstanceAttrBN(Int64 instance, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetInstanceAttrBN")]
        public static extern Int64 sdaiGetInstanceAttrBN(Int64 instance, byte[] attributeName);

		//
		//		sdaiGetAggregationAttrBN                    (http://rdf.bg/ifcdoc/CS64/sdaiGetAggregationAttrBN.html)
		//
		//	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiAGGR.
		//	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,
		//	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAggregationAttrBN")]
        public static extern Int64 sdaiGetAggregationAttrBN(Int64 instance, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAggregationAttrBN")]
        public static extern Int64 sdaiGetAggregationAttrBN(Int64 instance, byte[] attributeName);

		//
		//		sdaiGetAttrDefinition                       (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrDefinition.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern Int64 sdaiGetAttrDefinition(Int64 entity, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        public static extern Int64 sdaiGetAttrDefinition(Int64 entity, byte[] attributeName);

		//
		//		sdaiGetInstanceType                         (http://rdf.bg/ifcdoc/CS64/sdaiGetInstanceType.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetInstanceType")]
        public static extern Int64 sdaiGetInstanceType(Int64 instance);

		//
		//		sdaiGetMemberCount                          (http://rdf.bg/ifcdoc/CS64/sdaiGetMemberCount.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetMemberCount")]
        public static extern Int64 sdaiGetMemberCount(Int64 aggregate);

		//
		//		sdaiIsKindOf                                (http://rdf.bg/ifcdoc/CS64/sdaiIsKindOf.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiIsKindOf")]
        public static extern Int64 sdaiIsKindOf(Int64 instance, Int64 entity);

		//
		//		engiGetAttrType                             (http://rdf.bg/ifcdoc/CS64/engiGetAttrType.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttrType")]
        public static extern Int64 engiGetAttrType(Int64 instance, ref int_t attribute);

		//
		//		engiGetAttrTypeBN                           (http://rdf.bg/ifcdoc/CS64/engiGetAttrTypeBN.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttrTypeBN")]
        public static extern Int64 engiGetAttrTypeBN(Int64 instance, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttrTypeBN")]
        public static extern Int64 engiGetAttrTypeBN(Int64 instance, byte[] attributeName);

		//
		//		sdaiIsInstanceOf                            (http://rdf.bg/ifcdoc/CS64/sdaiIsInstanceOf.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiIsInstanceOf")]
        public static extern Int64 sdaiIsInstanceOf(Int64 instance, Int64 entity);

		//
		//		sdaiIsInstanceOfBN                          (http://rdf.bg/ifcdoc/CS64/sdaiIsInstanceOfBN.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiIsInstanceOfBN")]
        public static extern Int64 sdaiIsInstanceOfBN(Int64 instance, string entityName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiIsInstanceOfBN")]
        public static extern Int64 sdaiIsInstanceOfBN(Int64 instance, byte[] entityName);

		//
		//		engiValidateAttr                            (http://rdf.bg/ifcdoc/CS64/engiValidateAttr.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiValidateAttr")]
        public static extern Int64 engiValidateAttr(Int64 instance, ref int_t attribute);

		//
		//		engiValidateAttrBN                          (http://rdf.bg/ifcdoc/CS64/engiValidateAttrBN.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiValidateAttrBN")]
        public static extern Int64 engiValidateAttrBN(Int64 instance, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "engiValidateAttrBN")]
        public static extern Int64 engiValidateAttrBN(Int64 instance, byte[] attributeName);

		//
		//		sdaiCreateInstanceEI                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceEI.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateInstanceEI")]
        public static extern Int64 sdaiCreateInstanceEI(Int64 model, Int64 entity, Int64 express_id);

		//
		//		sdaiCreateInstanceBNEI                      (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceBNEI.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateInstanceBNEI")]
        public static extern Int64 sdaiCreateInstanceBNEI(Int64 model, string entityName, Int64 express_id);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateInstanceBNEI")]
        public static extern Int64 sdaiCreateInstanceBNEI(Int64 model, byte[] entityName, Int64 express_id);

        //
        //  Instance Writing API Calls
        //

		//
		//		sdaiPrepend                                 (http://rdf.bg/ifcdoc/CS64/sdaiPrepend.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPrepend")]
        public static extern void sdaiPrepend(Int64 list, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPrepend")]
        public static extern void sdaiPrepend(Int64 list, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPrepend")]
        public static extern void sdaiPrepend(Int64 list, Int64 valueType, out IntPtr value);

		//
		//		sdaiAppend                                  (http://rdf.bg/ifcdoc/CS64/sdaiAppend.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int64 list, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int64 list, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiAppend")]
        public static extern void sdaiAppend(Int64 list, Int64 valueType, out IntPtr value);

		//
		//		engiAppend                                  (http://rdf.bg/ifcdoc/CS64/engiAppend.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiAppend")]
        public static extern void engiAppend(Int64 list, Int64 valueType, out IntPtr values, Int64 card);

		//
		//		sdaiCreateADB                               (http://rdf.bg/ifcdoc/CS64/sdaiCreateADB.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int64 sdaiCreateADB(Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int64 sdaiCreateADB(Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateADB")]
        public static extern Int64 sdaiCreateADB(Int64 valueType, out IntPtr value);

		//
		//		sdaiCreateAggr                              (http://rdf.bg/ifcdoc/CS64/sdaiCreateAggr.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateAggr")]
        public static extern Int64 sdaiCreateAggr(Int64 instance, ref int_t attribute);

		//
		//		sdaiCreateAggrBN                            (http://rdf.bg/ifcdoc/CS64/sdaiCreateAggrBN.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern Int64 sdaiCreateAggrBN(Int64 instance, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateAggrBN")]
        public static extern Int64 sdaiCreateAggrBN(Int64 instance, byte[] attributeName);

		//
		//		sdaiCreateNestedAggr                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateNestedAggr.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateNestedAggr")]
        public static extern Int64 sdaiCreateNestedAggr(out int_t aggr);

		//
		//		sdaiCreateInstance                          (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstance.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateInstance")]
        public static extern Int64 sdaiCreateInstance(Int64 model, Int64 entity);

		//
		//		sdaiCreateInstanceBN                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceBN.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern Int64 sdaiCreateInstanceBN(Int64 model, string entityName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]
        public static extern Int64 sdaiCreateInstanceBN(Int64 model, byte[] entityName);

		//
		//		sdaiDeleteInstance                          (http://rdf.bg/ifcdoc/CS64/sdaiDeleteInstance.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiDeleteInstance")]
        public static extern void sdaiDeleteInstance(Int64 instance);

		//
		//		sdaiPutADBTypePath                          (http://rdf.bg/ifcdoc/CS64/sdaiPutADBTypePath.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(string ADB, Int64 pathCount, string path);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutADBTypePath")]
        public static extern void sdaiPutADBTypePath(byte[] ADB, Int64 pathCount, byte[] path);

		//
		//		sdaiPutAttr                                 (http://rdf.bg/ifcdoc/CS64/sdaiPutAttr.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int64 instance, ref int_t attribute, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int64 instance, ref int_t attribute, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAttr")]
        public static extern void sdaiPutAttr(Int64 instance, ref int_t attribute, Int64 valueType, out IntPtr value);

		//
		//		sdaiPutAttrBN                               (http://rdf.bg/ifcdoc/CS64/sdaiPutAttrBN.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, string attributeName, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, string attributeName, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, string attributeName, Int64 valueType, out IntPtr value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAttrBN")]
        public static extern void sdaiPutAttrBN(Int64 instance, byte[] attributeName, Int64 valueType, out IntPtr value);

		//
		//		engiSetComment                              (http://rdf.bg/ifcdoc/CS64/engiSetComment.html)
		//
		//	This call can be used to add a comment to an instance when exporting the content. The comment is available in the exported/saved IFC file.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(Int64 instance, string comment);

        [DllImport(STEPEngineDLL, EntryPoint = "engiSetComment")]
        public static extern void engiSetComment(Int64 instance, byte[] comment);

		//
		//		engiGetInstanceLocalId                      (http://rdf.bg/ifcdoc/CS64/engiGetInstanceLocalId.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetInstanceLocalId")]
        public static extern Int64 engiGetInstanceLocalId(Int64 instance);

		//
		//		sdaiTestAttr                                (http://rdf.bg/ifcdoc/CS64/sdaiTestAttr.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiTestAttr")]
        public static extern Int64 sdaiTestAttr(Int64 instance, ref int_t attribute);

		//
		//		sdaiTestAttrBN                              (http://rdf.bg/ifcdoc/CS64/sdaiTestAttrBN.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiTestAttrBN")]
        public static extern Int64 sdaiTestAttrBN(Int64 instance, string attributeName);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiTestAttrBN")]
        public static extern Int64 sdaiTestAttrBN(Int64 instance, byte[] attributeName);

		//
		//		engiGetInstanceClassInfo                    (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfo.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetInstanceClassInfo")]
        public static extern IntPtr engiGetInstanceClassInfo(Int64 instance);

		//
		//		engiGetInstanceClassInfoUC                  (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfoUC.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetInstanceClassInfoUC")]
        public static extern IntPtr engiGetInstanceClassInfoUC(Int64 instance);

		//
		//		engiGetInstanceClassInfoEx                  (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfoEx.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetInstanceClassInfoEx")]
        public static extern void engiGetInstanceClassInfoEx(Int64 instance, out IntPtr classInfo);

		//
		//		engiGetInstanceMetaInfo                     (http://rdf.bg/ifcdoc/CS64/engiGetInstanceMetaInfo.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetInstanceMetaInfo")]
        public static extern Int64 engiGetInstanceMetaInfo(out int_t instance, out int_t localId, out IntPtr entityName, out IntPtr entityNameUC);

        //
        //  Controling API Calls
        //

		//
		//		circleSegments                              (http://rdf.bg/ifcdoc/CS64/circleSegments.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "circleSegments")]
        public static extern void circleSegments(Int64 circles, Int64 smallCircles);

		//
		//		setMaximumSegmentationLength                (http://rdf.bg/ifcdoc/CS64/setMaximumSegmentationLength.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "setMaximumSegmentationLength")]
        public static extern void setMaximumSegmentationLength(Int64 model, double length);

		//
		//		getUnitConversionFactor                     (http://rdf.bg/ifcdoc/CS64/getUnitConversionFactor.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "getUnitConversionFactor")]
        public static extern double getUnitConversionFactor(Int64 model, string unitType, out IntPtr unitPrefix, out IntPtr unitName, out IntPtr SIUnitName);

        [DllImport(STEPEngineDLL, EntryPoint = "getUnitConversionFactor")]
        public static extern double getUnitConversionFactor(Int64 model, byte[] unitType, out IntPtr unitPrefix, out IntPtr unitName, out IntPtr SIUnitName);

		//
		//		setBRepProperties                           (http://rdf.bg/ifcdoc/CS64/setBRepProperties.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "setBRepProperties")]
        public static extern void setBRepProperties(Int64 model, Int64 consistencyCheck, double fraction, double epsilon, Int64 maxVerticesSize);

		//
		//		cleanMemory                                 (http://rdf.bg/ifcdoc/CS64/cleanMemory.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "cleanMemory")]
        public static extern void cleanMemory(Int64 model, Int64 mode);

		//
		//		internalGetP21Line                          (http://rdf.bg/ifcdoc/CS64/internalGetP21Line.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "internalGetP21Line")]
        public static extern Int64 internalGetP21Line(Int64 instance);

		//
		//		internalGetInstanceFromP21Line              (http://rdf.bg/ifcdoc/CS64/internalGetInstanceFromP21Line.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "internalGetInstanceFromP21Line")]
        public static extern Int64 internalGetInstanceFromP21Line(Int64 model, Int64 P21Line);

		//
		//		internalGetXMLID                            (http://rdf.bg/ifcdoc/CS64/internalGetXMLID.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "internalGetXMLID")]
        public static extern void internalGetXMLID(Int64 instance, out IntPtr XMLID);

		//
		//		setStringUnicode                            (http://rdf.bg/ifcdoc/CS64/setStringUnicode.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "setStringUnicode")]
        public static extern Int64 setStringUnicode(Int64 unicode);

		//
		//		setFilter                                   (http://rdf.bg/ifcdoc/CS64/setFilter.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "setFilter")]
        public static extern void setFilter(Int64 model, Int64 setting, Int64 mask);

		//
		//		getFilter                                   (http://rdf.bg/ifcdoc/CS64/getFilter.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "getFilter")]
        public static extern Int64 getFilter(Int64 model, Int64 mask);

        //
        //  Uncategorized API Calls
        //

		//
		//		xxxxGetEntityAndSubTypesExtent              (http://rdf.bg/ifcdoc/CS64/xxxxGetEntityAndSubTypesExtent.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtent")]
        public static extern Int64 xxxxGetEntityAndSubTypesExtent(Int64 model, Int64 entity);

		//
		//		xxxxGetEntityAndSubTypesExtentBN            (http://rdf.bg/ifcdoc/CS64/xxxxGetEntityAndSubTypesExtentBN.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtentBN")]
        public static extern Int64 xxxxGetEntityAndSubTypesExtentBN(Int64 model, string entityName);

        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtentBN")]
        public static extern Int64 xxxxGetEntityAndSubTypesExtentBN(Int64 model, byte[] entityName);

		//
		//		xxxxGetInstancesUsing                       (http://rdf.bg/ifcdoc/CS64/xxxxGetInstancesUsing.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetInstancesUsing")]
        public static extern Int64 xxxxGetInstancesUsing(Int64 instance);

		//
		//		xxxxDeleteFromAggregation                   (http://rdf.bg/ifcdoc/CS64/xxxxDeleteFromAggregation.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "xxxxDeleteFromAggregation")]
        public static extern Int64 xxxxDeleteFromAggregation(Int64 instance, out int_t aggregate, Int64 elementIndex);

		//
		//		xxxxGetAttrDefinitionByValue                (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrDefinitionByValue.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]
        public static extern Int64 xxxxGetAttrDefinitionByValue(Int64 instance, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]
        public static extern Int64 xxxxGetAttrDefinitionByValue(Int64 instance, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]
        public static extern Int64 xxxxGetAttrDefinitionByValue(Int64 instance, out IntPtr value);

		//
		//		xxxxGetAttrNameByIndex                      (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrNameByIndex.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetAttrNameByIndex")]
        public static extern void xxxxGetAttrNameByIndex(Int64 instance, Int64 index, out IntPtr name);

		//
		//		iterateOverInstances                        (http://rdf.bg/ifcdoc/CS64/iterateOverInstances.html)
		//
		//	This function interates over all available instances loaded in memory, it is the fastest way to find all instances.
		//	Argument entity and entityName are both optional and if non-zero are filled with respectively the entity handle and entity name as char array.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "iterateOverInstances")]
        public static extern Int64 iterateOverInstances(Int64 model, Int64 instance, out int_t entity, string entityName);

        [DllImport(STEPEngineDLL, EntryPoint = "iterateOverInstances")]
        public static extern Int64 iterateOverInstances(Int64 model, Int64 instance, out int_t entity, byte[] entityName);

		//
		//		iterateOverProperties                       (http://rdf.bg/ifcdoc/CS64/iterateOverProperties.html)
		//
		//	This function iterated over all available attributes of a specific given entity.
		//	This call is typically used in combination with iterateOverInstances(..).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "iterateOverProperties")]
        public static extern Int64 iterateOverProperties(Int64 entity, Int64 index);

		//
		//		sdaiGetAggrByIterator                       (http://rdf.bg/ifcdoc/CS64/sdaiGetAggrByIterator.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]
        public static extern Int64 sdaiGetAggrByIterator(Int64 iterator, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]
        public static extern Int64 sdaiGetAggrByIterator(Int64 iterator, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]
        public static extern Int64 sdaiGetAggrByIterator(Int64 iterator, Int64 valueType, out IntPtr value);

		//
		//		sdaiPutAggrByIterator                       (http://rdf.bg/ifcdoc/CS64/sdaiPutAggrByIterator.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]
        public static extern void sdaiPutAggrByIterator(Int64 iterator, Int64 valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]
        public static extern void sdaiPutAggrByIterator(Int64 iterator, Int64 valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]
        public static extern void sdaiPutAggrByIterator(Int64 iterator, Int64 valueType, out IntPtr value);

		//
		//		internalSetLink                             (http://rdf.bg/ifcdoc/CS64/internalSetLink.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "internalSetLink")]
        public static extern void internalSetLink(Int64 instance, string attributeName, Int64 linked_id);

        [DllImport(STEPEngineDLL, EntryPoint = "internalSetLink")]
        public static extern void internalSetLink(Int64 instance, byte[] attributeName, Int64 linked_id);

		//
		//		internalAddAggrLink                         (http://rdf.bg/ifcdoc/CS64/internalAddAggrLink.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "internalAddAggrLink")]
        public static extern void internalAddAggrLink(Int64 list, Int64 linked_id);

		//
		//		engiGetNotReferedAggr                       (http://rdf.bg/ifcdoc/CS64/engiGetNotReferedAggr.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetNotReferedAggr")]
        public static extern void engiGetNotReferedAggr(Int64 model, out int_t value);

		//
		//		engiGetAttributeAggr                        (http://rdf.bg/ifcdoc/CS64/engiGetAttributeAggr.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAttributeAggr")]
        public static extern void engiGetAttributeAggr(Int64 instance, out int_t value);

		//
		//		engiGetAggrUnknownElement                   (http://rdf.bg/ifcdoc/CS64/engiGetAggrUnknownElement.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]
        public static extern void engiGetAggrUnknownElement(out int_t aggregate, Int64 elementIndex, out int_t valueType, out int_t value);

        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]
        public static extern void engiGetAggrUnknownElement(out int_t aggregate, Int64 elementIndex, out int_t valueType, out double value);

        [DllImport(STEPEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]
        public static extern void engiGetAggrUnknownElement(out int_t aggregate, Int64 elementIndex, out int_t valueType, out IntPtr value);

		//
		//		sdaiErrorQuery                              (http://rdf.bg/ifcdoc/CS64/sdaiErrorQuery.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiErrorQuery")]
        public static extern Int64 sdaiErrorQuery();

        //
        //  Geometry Kernel related API Calls
        //

		//
		//		owlGetModel                                 (http://rdf.bg/ifcdoc/CS64/owlGetModel.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "owlGetModel")]
        public static extern void owlGetModel(Int64 model, out Int64 owlModel);

		//
		//		owlGetInstance                              (http://rdf.bg/ifcdoc/CS64/owlGetInstance.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "owlGetInstance")]
        public static extern void owlGetInstance(Int64 model, Int64 instance, out Int64 owlInstance);

		//
		//		owlBuildInstance                            (http://rdf.bg/ifcdoc/CS64/owlBuildInstance.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "owlBuildInstance")]
        public static extern void owlBuildInstance(Int64 model, Int64 instance, out Int64 owlInstance);

		//
		//		owlBuildInstances                           (http://rdf.bg/ifcdoc/CS64/owlBuildInstances.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "owlBuildInstances")]
        public static extern void owlBuildInstances(Int64 model, Int64 instance, out Int64 owlInstanceComplete, out Int64 owlInstanceSolids, out Int64 owlInstanceVoids);

		//
		//		owlGetMappedItem                            (http://rdf.bg/ifcdoc/CS64/owlGetMappedItem.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "owlGetMappedItem")]
        public static extern void owlGetMappedItem(Int64 model, Int64 instance, out Int64 owlInstance, out double transformationMatrix);

		//
		//		getInstanceDerivedPropertiesInModelling     (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedPropertiesInModelling.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "getInstanceDerivedPropertiesInModelling")]
        public static extern Int64 getInstanceDerivedPropertiesInModelling(Int64 model, Int64 instance, out double height, out double width, out double thickness);

		//
		//		getInstanceDerivedBoundingBox               (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedBoundingBox.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "getInstanceDerivedBoundingBox")]
        public static extern Int64 getInstanceDerivedBoundingBox(Int64 model, Int64 instance, out double Ox, out double Oy, out double Oz, out double Vx, out double Vy, out double Vz);

		//
		//		getInstanceTransformationMatrix             (http://rdf.bg/ifcdoc/CS64/getInstanceTransformationMatrix.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "getInstanceTransformationMatrix")]
        public static extern Int64 getInstanceTransformationMatrix(Int64 model, Int64 instance, out double _11, out double _12, out double _13, out double _14, out double _21, out double _22, out double _23, out double _24, out double _31, out double _32, out double _33, out double _34, out double _41, out double _42, out double _43, out double _44);

		//
		//		getInstanceDerivedTransformationMatrix      (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedTransformationMatrix.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "getInstanceDerivedTransformationMatrix")]
        public static extern Int64 getInstanceDerivedTransformationMatrix(Int64 model, Int64 instance, out double _11, out double _12, out double _13, out double _14, out double _21, out double _22, out double _23, out double _24, out double _31, out double _32, out double _33, out double _34, out double _41, out double _42, out double _43, out double _44);

		//
		//		internalGetBoundingBox                      (http://rdf.bg/ifcdoc/CS64/internalGetBoundingBox.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "internalGetBoundingBox")]
        public static extern Int64 internalGetBoundingBox(Int64 model, Int64 instance);

		//
		//		internalGetCenter                           (http://rdf.bg/ifcdoc/CS64/internalGetCenter.html)
		//
		//	...
		//
        [DllImport(STEPEngineDLL, EntryPoint = "internalGetCenter")]
        public static extern Int64 internalGetCenter(Int64 model, Int64 instance);

        //
        //  Deprecated API Calls (GENERIC)
        //

		//
		//		engiAttrIsInverse                           (http://rdf.bg/ifcdoc/CS64/engiAttrIsInverse.html)
		//
		//	This call is deprecated, please use call engiAttrIsInverse.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "engiAttrIsInverse")]
        public static extern Int64 engiAttrIsInverse(ref int_t attribute);

		//
		//		xxxxOpenModelByStream                       (http://rdf.bg/ifcdoc/CS64/xxxxOpenModelByStream.html)
		//
		//	This call is deprecated, please use call engiOpenModelByStream.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "xxxxOpenModelByStream")]
        public static extern Int64 xxxxOpenModelByStream(Int64 repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, string schemaName);

        [DllImport(STEPEngineDLL, EntryPoint = "xxxxOpenModelByStream")]
        public static extern Int64 xxxxOpenModelByStream(Int64 repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, byte[] schemaName);

		//
		//		sdaiCreateIterator                          (http://rdf.bg/ifcdoc/CS64/sdaiCreateIterator.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiCreateIterator")]
        public static extern Int64 sdaiCreateIterator(ref int_t aggregate);

		//
		//		sdaiDeleteIterator                          (http://rdf.bg/ifcdoc/CS64/sdaiDeleteIterator.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiDeleteIterator")]
        public static extern void sdaiDeleteIterator(Int64 iterator);

		//
		//		sdaiBeginning                               (http://rdf.bg/ifcdoc/CS64/sdaiBeginning.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiBeginning")]
        public static extern void sdaiBeginning(Int64 iterator);

		//
		//		sdaiNext                                    (http://rdf.bg/ifcdoc/CS64/sdaiNext.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiNext")]
        public static extern Int64 sdaiNext(Int64 iterator);

		//
		//		sdaiPrevious                                (http://rdf.bg/ifcdoc/CS64/sdaiPrevious.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiPrevious")]
        public static extern Int64 sdaiPrevious(Int64 iterator);

		//
		//		sdaiEnd                                     (http://rdf.bg/ifcdoc/CS64/sdaiEnd.html)
		//
		//	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiEnd")]
        public static extern void sdaiEnd(Int64 iterator);

		//
		//		sdaiplusGetAggregationType                  (http://rdf.bg/ifcdoc/CS64/sdaiplusGetAggregationType.html)
		//
		//	This call is deprecated, please use call ....
		//
        [DllImport(STEPEngineDLL, EntryPoint = "sdaiplusGetAggregationType")]
        public static extern Int64 sdaiplusGetAggregationType(Int64 instance, out int_t aggregation);

		//
		//		xxxxGetAttrTypeBN                           (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrTypeBN.html)
		//
		//	This call is deprecated, please use calls engiGetAttrTypeBN(..).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetAttrTypeBN")]
        public static extern Int64 xxxxGetAttrTypeBN(Int64 instance, string attributeName, out IntPtr attributeType);

        [DllImport(STEPEngineDLL, EntryPoint = "xxxxGetAttrTypeBN")]
        public static extern Int64 xxxxGetAttrTypeBN(Int64 instance, byte[] attributeName, out IntPtr attributeType);

        //
        //  Deprecated API Calls (GEOMETRY)
        //

		//
		//		initializeModellingInstance                 (http://rdf.bg/ifcdoc/CS64/initializeModellingInstance.html)
		//
		//	This call is deprecated, please use call CalculateInstance().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "initializeModellingInstance")]
        public static extern Int64 initializeModellingInstance(Int64 model, out int_t noVertices, out int_t noIndices, double scale, Int64 instance);

		//
		//		finalizeModelling                           (http://rdf.bg/ifcdoc/CS64/finalizeModelling.html)
		//
		//	This call is deprecated, please use call UpdateInstanceVertexBuffer() and UpdateInstanceIndexBuffer().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "finalizeModelling")]
        public static extern Int64 finalizeModelling(Int64 model, float[] vertices, out int_t indices, Int64 FVF);

		//
		//		getInstanceInModelling                      (http://rdf.bg/ifcdoc/CS64/getInstanceInModelling.html)
		//
		//	This call is deprecated, there is no direct / easy replacement although the functionality is present. If you still use this call please contact RDF to find a solution together.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "getInstanceInModelling")]
        public static extern Int64 getInstanceInModelling(Int64 model, Int64 instance, Int64 mode, out int_t startVertex, out int_t startIndex, out int_t primitiveCount);

		//
		//		setVertexOffset                             (http://rdf.bg/ifcdoc/CS64/setVertexOffset.html)
		//
		//	This call is deprecated, please use call SetVertexBufferOffset().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "setVertexOffset")]
        public static extern void setVertexOffset(Int64 model, double x, double y, double z);

		//
		//		setFormat                                   (http://rdf.bg/ifcdoc/CS64/setFormat.html)
		//
		//	This call is deprecated, please use call SetFormat().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "setFormat")]
        public static extern void setFormat(Int64 model, Int64 setting, Int64 mask);

		//
		//		getConceptualFaceCnt                        (http://rdf.bg/ifcdoc/CS64/getConceptualFaceCnt.html)
		//
		//	This call is deprecated, please use call GetConceptualFaceCnt().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "getConceptualFaceCnt")]
        public static extern Int64 getConceptualFaceCnt(Int64 instance);

		//
		//		getConceptualFaceEx                         (http://rdf.bg/ifcdoc/CS64/getConceptualFaceEx.html)
		//
		//	This call is deprecated, please use call GetConceptualFaceEx().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "getConceptualFaceEx")]
        public static extern Int64 getConceptualFaceEx(Int64 instance, Int64 index, out int_t startIndexTriangles, out int_t noIndicesTriangles, out int_t startIndexLines, out int_t noIndicesLines, out int_t startIndexPoints, out int_t noIndicesPoints, out int_t startIndexFacesPolygons, out int_t noIndicesFacesPolygons, out int_t startIndexConceptualFacePolygons, out int_t noIndicesConceptualFacePolygons);

		//
		//		createGeometryConversion                    (http://rdf.bg/ifcdoc/CS64/createGeometryConversion.html)
		//
		//	This call is deprecated, please use call ... .
		//
        [DllImport(STEPEngineDLL, EntryPoint = "createGeometryConversion")]
        public static extern void createGeometryConversion(Int64 instance, out Int64 owlInstance);

		//
		//		convertInstance                             (http://rdf.bg/ifcdoc/CS64/convertInstance.html)
		//
		//	This call is deprecated, please use call ... .
		//
        [DllImport(STEPEngineDLL, EntryPoint = "convertInstance")]
        public static extern void convertInstance(Int64 instance);

		//
		//		initializeModellingInstanceEx               (http://rdf.bg/ifcdoc/CS64/initializeModellingInstanceEx.html)
		//
		//	This call is deprecated, please use call CalculateInstance().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "initializeModellingInstanceEx")]
        public static extern Int64 initializeModellingInstanceEx(Int64 model, out int_t noVertices, out int_t noIndices, double scale, Int64 instance, Int64 instanceList);

		//
		//		exportModellingAsOWL                        (http://rdf.bg/ifcdoc/CS64/exportModellingAsOWL.html)
		//
		//	This call is deprecated, please contact us if you use this call.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "exportModellingAsOWL")]
        public static extern void exportModellingAsOWL(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "exportModellingAsOWL")]
        public static extern void exportModellingAsOWL(Int64 model, byte[] fileName);


        //
        //  Meta information API Calls
        //

		//
		//		GetRevision                                 (http://rdf.bg/gkdoc/CS64/GetRevision.html)
		//
		//	Returns the revision number.
		//	The timeStamp is generated by the SVN system used during development.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetRevision")]
        public static extern Int64 GetRevision(out IntPtr timeStamp);

		//
		//		GetRevisionW                                (http://rdf.bg/gkdoc/CS64/GetRevisionW.html)
		//
		//	Returns the revision number.
		//	The timeStamp is generated by the SVN system used during development.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetRevisionW")]
        public static extern Int64 GetRevisionW(out IntPtr timeStamp);

		//
		//		GetProtection                               (http://rdf.bg/gkdoc/CS64/GetProtection.html)
		//
		//	This call is required to be called to enable the DLL to work if protection is active.
		//
		//	Returns the number of days (incl. this one) that this version is still active or 0 if no protection is embedded.
		//	In case no days are left and protection is active this call will return -1.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetProtection")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetEnvironment")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetEnvironmentW")]
        public static extern Int64 GetEnvironmentW(out IntPtr environmentVariables, out IntPtr developmentVariables);

		//
		//		SetAssertionFile                            (http://rdf.bg/gkdoc/CS64/SetAssertionFile.html)
		//
		//	This function sets the file location where internal assertions should be written to.
		//	If the filename is not set (default) many internal control procedures are not executed
		//	and the code will be faster.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetAssertionFile")]
        public static extern void SetAssertionFile(string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "SetAssertionFile")]
        public static extern void SetAssertionFile(byte[] fileName);

		//
		//		SetAssertionFileW                           (http://rdf.bg/gkdoc/CS64/SetAssertionFileW.html)
		//
		//	This function sets the file location where internal assertions should be written to.
		//	If the filename is not set (default) many internal control procedures are not executed
		//	and the code will be faster.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetAssertionFileW")]
        public static extern void SetAssertionFileW(string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "SetAssertionFileW")]
        public static extern void SetAssertionFileW(byte[] fileName);

		//
		//		GetAssertionFile                            (http://rdf.bg/gkdoc/CS64/GetAssertionFile.html)
		//
		//	This function gets the file location as stored/set internally where internal assertions should be written to.
		//	It works independent if the file location is set through SetAssertionFile() or SetAssertionFileW().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetAssertionFile")]
        public static extern void GetAssertionFile(out IntPtr fileName);

		//
		//		GetAssertionFileW                           (http://rdf.bg/gkdoc/CS64/GetAssertionFileW.html)
		//
		//	This function gets the file location as stored/set internally where internal assertions should be written to.
		//	It works independent if the file location is set through SetAssertionFile() or SetAssertionFileW().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetAssertionFileW")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetCharacterSerialization")]
        public static extern Int64 SetCharacterSerialization(Int64 model, Int64 encoding, Int64 wcharBitSizeOverride, byte ascii);

		//
		//		GetCharacterSerialization                   (http://rdf.bg/gkdoc/CS64/GetCharacterSerialization.html)
		//
		//	This call retrieves the values as set by 
		//
		//	The returns the size of a single character in bits, i.e. 1 byte is 8 bits, this can be 8, 16 or 32 depending on settings and operating system
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetCharacterSerialization")]
        public static extern Int64 GetCharacterSerialization(Int64 model, out Int64 encoding, out byte ascii);

		//
		//		AbortModel                                  (http://rdf.bg/gkdoc/CS64/AbortModel.html)
		//
		//	This function abort running processes for a model. It can be used when a task takes more time than
		//	expected / available, or in case the requested results are not relevant anymore.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "AbortModel")]
        public static extern Int64 AbortModel(Int64 model, Int64 setting);

		//
		//		GetSessionMetaInfo                          (http://rdf.bg/gkdoc/CS64/GetSessionMetaInfo.html)
		//
		//	This function is meant for debugging purposes and return statistics during processing.
		//	The return value represents the number of active models within the session (or zero if the model was not recognized).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetSessionMetaInfo")]
        public static extern Int64 GetSessionMetaInfo(out Int64 allocatedBlocks, out Int64 allocatedBytes, out Int64 nonUsedBlocks, out Int64 nonUsedBytes);

		//
		//		GetModelMetaInfo                            (http://rdf.bg/gkdoc/CS64/GetModelMetaInfo.html)
		//
		//	This function is meant for debugging purposes and return statistics during processing.
		//	The return value represents the number of active models within the session (or zero if the model was not recognized).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);

        [DllImport(STEPEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out Int64 activeInstances, out Int64 deletedInstances, out Int64 inactiveInstances);

        [DllImport(STEPEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, IntPtr activeClasses, IntPtr deletedClasses, out Int64 activeProperties, out Int64 deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);

        [DllImport(STEPEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, IntPtr activeClasses, IntPtr deletedClasses, out Int64 activeProperties, out Int64 deletedProperties, out Int64 activeInstances, out Int64 deletedInstances, out Int64 inactiveInstances);

        [DllImport(STEPEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, out Int64 activeClasses, out Int64 deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);

        [DllImport(STEPEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, out Int64 activeClasses, out Int64 deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out Int64 activeInstances, out Int64 deletedInstances, out Int64 inactiveInstances);

        [DllImport(STEPEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, out Int64 activeClasses, out Int64 removedClasses, out Int64 activeProperties, out Int64 deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);

        [DllImport(STEPEngineDLL, EntryPoint = "GetModelMetaInfo")]
        public static extern Int64 GetModelMetaInfo(Int64 model, out Int64 activeClasses, out Int64 removedClasses, out Int64 activeProperties, out Int64 deletedProperties, out Int64 activeInstances, out Int64 deletedInstances, out Int64 inactiveInstances);

		//
		//		GetInstanceMetaInfo                         (http://rdf.bg/gkdoc/CS64/GetInstanceMetaInfo.html)
		//
		//	This function is meant for debugging purposes and return statistics during processing.
		//	The return value represents the number of active instances within the model (or zero if the instance was not recognized).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetInstanceMetaInfo")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetSmoothness")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "AddState")]
        public static extern void AddState(Int64 model, Int64 owlInstance);

		//
		//		GetModel                                    (http://rdf.bg/gkdoc/CS64/GetModel.html)
		//
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetModel")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, out Int64 classCnt, out Int64 propertyCnt, out Int64 instanceCnt, Int64 setting, Int64 mask);

        [DllImport(STEPEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, out Int64 classCnt, out Int64 propertyCnt, IntPtr instanceCnt, Int64 setting, Int64 mask);

        [DllImport(STEPEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, out Int64 classCnt, IntPtr propertyCnt, out Int64 instanceCnt, Int64 setting, Int64 mask);

        [DllImport(STEPEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, out Int64 classCnt, IntPtr propertyCnt, IntPtr instanceCnt, Int64 setting, Int64 mask);

        [DllImport(STEPEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, IntPtr classCnt, out Int64 propertyCnt, out Int64 instanceCnt, Int64 setting, Int64 mask);

        [DllImport(STEPEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, IntPtr classCnt, out Int64 propertyCnt, IntPtr instanceCnt, Int64 setting, Int64 mask);

        [DllImport(STEPEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, IntPtr classCnt, IntPtr propertyCnt, out Int64 instanceCnt, Int64 setting, Int64 mask);

        [DllImport(STEPEngineDLL, EntryPoint = "OrderedHandles")]
        public static extern void OrderedHandles(Int64 model, IntPtr classCnt, IntPtr propertyCnt, IntPtr instanceCnt, Int64 setting, Int64 mask);

		//
		//		PeelArray                                   (http://rdf.bg/gkdoc/CS64/PeelArray.html)
		//
		//	This function introduces functionality that is missing or complicated in some programming languages.
		//	The attribute inValue is a reference to an array of references. The attribute outValue is a reference to the same array,
		//	however a number of elements earlier or further, i.e. number of elements being attribute elementSize. Be aware that as
		//	we are talking about references the offset is depending on 32 bit / 64 bit compilation.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "PeelArray")]
        public static extern void PeelArray(ref byte[] inValue, out byte outValue, Int64 elementSize);

		//
		//		CloseSession                                (http://rdf.bg/gkdoc/CS64/CloseSession.html)
		//
		//	This function closes the session, after this call the geometry kernel cannot be used anymore.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "CloseSession")]
        public static extern Int64 CloseSession();

		//
		//		CleanMemory                                 (http://rdf.bg/gkdoc/CS64/CleanMemory.html)
		//
		//		This function ..
		//
        [DllImport(STEPEngineDLL, EntryPoint = "CleanMemory")]
        public static extern void CleanMemory();

		//
		//		ClearCache                                  (http://rdf.bg/gkdoc/CS64/ClearCache.html)
		//
		//		This function ..
		//
        [DllImport(STEPEngineDLL, EntryPoint = "ClearCache")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "CreateModel")]
        public static extern Int64 CreateModel();

		//
		//		OpenModel                                   (http://rdf.bg/gkdoc/CS64/OpenModel.html)
		//
		//	This function opens the model on location fileName.
		//	References inside to other ontologies will be included.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "OpenModel")]
        public static extern Int64 OpenModel(string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "OpenModel")]
        public static extern Int64 OpenModel(byte[] fileName);

		//
		//		OpenModelW                                  (http://rdf.bg/gkdoc/CS64/OpenModelW.html)
		//
		//	This function opens the model on location fileName.
		//	References inside to other ontologies will be included.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "OpenModelW")]
        public static extern Int64 OpenModelW(string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "OpenModelW")]
        public static extern Int64 OpenModelW(byte[] fileName);

		//
		//		OpenModelS                                  (http://rdf.bg/gkdoc/CS64/OpenModelS.html)
		//
		//	This function opens the model via a stream.
		//	References inside to other ontologies will be included.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "OpenModelS")]
        public static extern Int64 OpenModelS([MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback);

		//
		//		OpenModelA                                  (http://rdf.bg/gkdoc/CS64/OpenModelA.html)
		//
		//	This function opens the model via an array.
		//	References inside to other ontologies will be included.
		//	A handle to the model will be returned, or 0 in case something went wrong.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "OpenModelA")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "ImportModel")]
        public static extern Int64 ImportModel(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "ImportModel")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "ImportModelW")]
        public static extern Int64 ImportModelW(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "ImportModelW")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "ImportModelS")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "ImportModelA")]
        public static extern Int64 ImportModelA(Int64 model, byte[] content, Int64 size);

		//
		//		SaveInstanceTree                            (http://rdf.bg/gkdoc/CS64/SaveInstanceTree.html)
		//
		//	This function saves the selected instance and its dependancies on location fileName.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SaveInstanceTree")]
        public static extern Int64 SaveInstanceTree(Int64 owlInstance, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "SaveInstanceTree")]
        public static extern Int64 SaveInstanceTree(Int64 owlInstance, byte[] fileName);

		//
		//		SaveInstanceTreeW                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeW.html)
		//
		//	This function saves the selected instance and its dependancies on location fileName.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SaveInstanceTreeW")]
        public static extern Int64 SaveInstanceTreeW(Int64 owlInstance, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "SaveInstanceTreeW")]
        public static extern Int64 SaveInstanceTreeW(Int64 owlInstance, byte[] fileName);

		//
		//		SaveInstanceTreeS                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeS.html)
		//
		//	This function saves the selected instance and its dependancies in a stream.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SaveInstanceTreeS")]
        public static extern Int64 SaveInstanceTreeS(Int64 owlInstance, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, Int64 size);

		//
		//		SaveInstanceTreeA                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeA.html)
		//
		//	This function saves the selected instance and its dependancies in an array.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SaveInstanceTreeA")]
        public static extern Int64 SaveInstanceTreeA(Int64 owlInstance, byte[] content, out Int64 size);

		//
		//		SaveModel                                   (http://rdf.bg/gkdoc/CS64/SaveModel.html)
		//
		//	This function saves the current model on location fileName.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SaveModel")]
        public static extern Int64 SaveModel(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "SaveModel")]
        public static extern Int64 SaveModel(Int64 model, byte[] fileName);

		//
		//		SaveModelW                                  (http://rdf.bg/gkdoc/CS64/SaveModelW.html)
		//
		//	This function saves the current model on location fileName.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SaveModelW")]
        public static extern Int64 SaveModelW(Int64 model, string fileName);

        [DllImport(STEPEngineDLL, EntryPoint = "SaveModelW")]
        public static extern Int64 SaveModelW(Int64 model, byte[] fileName);

		//
		//		SaveModelS                                  (http://rdf.bg/gkdoc/CS64/SaveModelS.html)
		//
		//	This function saves the current model in a stream.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SaveModelS")]
        public static extern Int64 SaveModelS(Int64 model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, Int64 size);

		//
		//		SaveModelA                                  (http://rdf.bg/gkdoc/CS64/SaveModelA.html)
		//
		//	This function saves the current model in an array.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SaveModelA")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetOverrideFileIO")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetOverrideFileIO")]
        public static extern Int64 GetOverrideFileIO(Int64 model, Int64 mask);

		//
		//		CloseModel                                  (http://rdf.bg/gkdoc/CS64/CloseModel.html)
		//
		//	This function closes the model. After this call none of the instances and classes within the model
		//	can be used anymore, also garbage collection is not allowed anymore, in default compilation the
		//	model itself will be known in the kernel, however known to be disabled. Calls containing the model
		//	reference will be protected from crashing when called.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "CloseModel")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "CreateClass")]
        public static extern Int64 CreateClass(Int64 model, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "CreateClass")]
        public static extern Int64 CreateClass(Int64 model, byte[] name);

		//
		//		CreateClassW                                (http://rdf.bg/gkdoc/CS64/CreateClassW.html)
		//
		//	Returns a handle to an on the fly created class.
		//	If the model input is zero or not a model handle 0 will be returned,
		//
        [DllImport(STEPEngineDLL, EntryPoint = "CreateClassW")]
        public static extern Int64 CreateClassW(Int64 model, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "CreateClassW")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetClassByName")]
        public static extern Int64 GetClassByName(Int64 model, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "GetClassByName")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetClassByNameW")]
        public static extern Int64 GetClassByNameW(Int64 model, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "GetClassByNameW")]
        public static extern Int64 GetClassByNameW(Int64 model, byte[] name);

		//
		//		GetClassesByIterator                        (http://rdf.bg/gkdoc/CS64/GetClassesByIterator.html)
		//
		//	Returns a handle to an class.
		//	If input class is zero, the handle will point to the first relevant class.
		//	If all classes are past (or no relevant classes are found), the function will return 0.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetClassesByIterator")]
        public static extern Int64 GetClassesByIterator(Int64 model, Int64 owlClass);

		//
		//		SetClassParent                              (http://rdf.bg/gkdoc/CS64/SetClassParent.html)
		//
		//	Defines (set/unset) the parent class of a given class. Multiple-inheritence is supported and behavior
		//	of parent classes is also inherited as well as cardinality restrictions on datatype properties and
		//	object properties (relations).
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetClassParent")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetClassParentEx")]
        public static extern void SetClassParentEx(Int64 model, Int64 owlClass, Int64 parentOwlClass, Int64 setting);

		//
		//		GetParentsByIterator                        (http://rdf.bg/gkdoc/CS64/GetParentsByIterator.html)
		//
		//	Returns the next parent of the class.
		//	If input parent is zero, the handle will point to the first relevant parent.
		//	If all parent are past (or no relevant parent are found), the function will return 0.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetParentsByIterator")]
        public static extern Int64 GetParentsByIterator(Int64 owlClass, Int64 parentOwlClass);

		//
		//		SetNameOfClass                              (http://rdf.bg/gkdoc/CS64/SetNameOfClass.html)
		//
		//	Sets/updates the name of the class, if no error it returns 0.
		//	In case class does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfClass")]
        public static extern Int64 SetNameOfClass(Int64 owlClass, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfClass")]
        public static extern Int64 SetNameOfClass(Int64 owlClass, byte[] name);

		//
		//		SetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/SetNameOfClassW.html)
		//
		//	Sets/updates the name of the class, if no error it returns 0.
		//	In case class does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfClassW")]
        public static extern Int64 SetNameOfClassW(Int64 owlClass, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfClassW")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfClassEx")]
        public static extern Int64 SetNameOfClassEx(Int64 model, Int64 owlClass, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfClassEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfClassWEx")]
        public static extern Int64 SetNameOfClassWEx(Int64 model, Int64 owlClass, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfClassWEx")]
        public static extern Int64 SetNameOfClassWEx(Int64 model, Int64 owlClass, byte[] name);

		//
		//		GetNameOfClass                              (http://rdf.bg/gkdoc/CS64/GetNameOfClass.html)
		//
		//	Returns the name of the class, if the class does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfClass")]
        public static extern void GetNameOfClass(Int64 owlClass, out IntPtr name);

		//
		//		GetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/GetNameOfClassW.html)
		//
		//	Returns the name of the class, if the class does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfClassW")]
        public static extern void GetNameOfClassW(Int64 owlClass, out IntPtr name);

		//
		//		GetNameOfClassEx                            (http://rdf.bg/gkdoc/CS64/GetNameOfClassEx.html)
		//
		//	Returns the name of the class, if the class does not exist it returns nullptr.
		//
		//	This call has the same behavior as GetNameOfClass, however needs to be
		//	used in case properties are exchanged as a successive series of integers.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfClassEx")]
        public static extern void GetNameOfClassEx(Int64 model, Int64 owlClass, out IntPtr name);

		//
		//		GetNameOfClassWEx                           (http://rdf.bg/gkdoc/CS64/GetNameOfClassWEx.html)
		//
		//	Returns the name of the class, if the class does not exist it returns nullptr.
		//
		//	This call has the same behavior as GetNameOfClassW, however needs to be
		//	used in case classes are exchanged as a successive series of integers.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfClassWEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetClassPropertyCardinalityRestriction")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetClassPropertyCardinalityRestrictionEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetClassPropertyCardinalityRestriction")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetClassPropertyCardinalityRestrictionEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetGeometryClass")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetGeometryClassEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "CreateProperty")]
        public static extern Int64 CreateProperty(Int64 model, Int64 rdfPropertyType, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "CreateProperty")]
        public static extern Int64 CreateProperty(Int64 model, Int64 rdfPropertyType, byte[] name);

		//
		//		CreatePropertyW                             (http://rdf.bg/gkdoc/CS64/CreatePropertyW.html)
		//
		//	Returns a handle to an on the fly created property.
		//	If the model input is zero or not a model handle 0 will be returned,
		//
        [DllImport(STEPEngineDLL, EntryPoint = "CreatePropertyW")]
        public static extern Int64 CreatePropertyW(Int64 model, Int64 rdfPropertyType, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "CreatePropertyW")]
        public static extern Int64 CreatePropertyW(Int64 model, Int64 rdfPropertyType, byte[] name);

		//
		//		GetPropertyByName                           (http://rdf.bg/gkdoc/CS64/GetPropertyByName.html)
		//
		//	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.
		//	When the property does not exist yet and the name is unique
		//	the property will be created on-the-fly and the handle will be returned.
		//	When the name is not unique and given to a class or instance 0 will be returned.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyByName")]
        public static extern Int64 GetPropertyByName(Int64 model, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyByName")]
        public static extern Int64 GetPropertyByName(Int64 model, byte[] name);

		//
		//		GetPropertyByNameW                          (http://rdf.bg/gkdoc/CS64/GetPropertyByNameW.html)
		//
		//	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.
		//	When the property does not exist yet and the name is unique
		//	the property will be created on-the-fly and the handle will be returned.
		//	When the name is not unique and given to a class or instance 0 will be returned.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyByNameW")]
        public static extern Int64 GetPropertyByNameW(Int64 model, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyByNameW")]
        public static extern Int64 GetPropertyByNameW(Int64 model, byte[] name);

		//
		//		GetPropertiesByIterator                     (http://rdf.bg/gkdoc/CS64/GetPropertiesByIterator.html)
		//
		//	Returns a handle to a property.
		//	If input property is zero, the handle will point to the first relevant property.
		//	If all properties are past (or no relevant properties are found), the function will return 0.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertiesByIterator")]
        public static extern Int64 GetPropertiesByIterator(Int64 model, Int64 rdfProperty);

		//
		//		GetRangeRestrictionsByIterator              (http://rdf.bg/gkdoc/CS64/GetRangeRestrictionsByIterator.html)
		//
		//	Returns the next class the property is restricted to.
		//	If input class is zero, the handle will point to the first relevant class.
		//	If all classes are past (or no relevant classes are found), the function will return 0.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetRangeRestrictionsByIterator")]
        public static extern Int64 GetRangeRestrictionsByIterator(Int64 rdfProperty, Int64 owlClass);

		//
		//		SetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/SetNameOfProperty.html)
		//
		//	Sets/updates the name of the property, if no error it returns 0.
		//	In case property does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfProperty")]
        public static extern Int64 SetNameOfProperty(Int64 rdfProperty, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfProperty")]
        public static extern Int64 SetNameOfProperty(Int64 rdfProperty, byte[] name);

		//
		//		SetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyW.html)
		//
		//	Sets/updates the name of the property, if no error it returns 0.
		//	In case property does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfPropertyW")]
        public static extern Int64 SetNameOfPropertyW(Int64 rdfProperty, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfPropertyW")]
        public static extern Int64 SetNameOfPropertyW(Int64 rdfProperty, byte[] name);

		//
		//		SetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyEx.html)
		//
		//	Sets/updates the name of the property, if no error it returns 0.
		//	In case property does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfPropertyEx")]
        public static extern Int64 SetNameOfPropertyEx(Int64 model, Int64 rdfProperty, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfPropertyEx")]
        public static extern Int64 SetNameOfPropertyEx(Int64 model, Int64 rdfProperty, byte[] name);

		//
		//		SetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyWEx.html)
		//
		//	Sets/updates the name of the property, if no error it returns 0.
		//	In case property does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]
        public static extern Int64 SetNameOfPropertyWEx(Int64 model, Int64 rdfProperty, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]
        public static extern Int64 SetNameOfPropertyWEx(Int64 model, Int64 rdfProperty, byte[] name);

		//
		//		GetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/GetNameOfProperty.html)
		//
		//	Returns the name of the property, if the property does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfProperty")]
        public static extern void GetNameOfProperty(Int64 rdfProperty, out IntPtr name);

		//
		//		GetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyW.html)
		//
		//	Returns the name of the property, if the property does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfPropertyW")]
        public static extern void GetNameOfPropertyW(Int64 rdfProperty, out IntPtr name);

		//
		//		GetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyEx.html)
		//
		//	Returns the name of the property, if the property does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfPropertyEx")]
        public static extern void GetNameOfPropertyEx(Int64 model, Int64 rdfProperty, out IntPtr name);

		//
		//		GetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyWEx.html)
		//
		//	Returns the name of the property, if the property does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfPropertyWEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetPropertyType")]
        public static extern Int64 SetPropertyType(Int64 rdfProperty, Int64 propertyType);

		//
		//		SetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/SetPropertyTypeEx.html)
		//
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetPropertyTypeEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyType")]
        public static extern Int64 GetPropertyType(Int64 rdfProperty);

		//
		//		GetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/GetPropertyTypeEx.html)
		//
		//	This call has the same behavior as GetPropertyType, however needs to be
		//	used in case properties are exchanged as a successive series of integers.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyTypeEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "CreateInstance")]
        public static extern Int64 CreateInstance(Int64 owlClass, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "CreateInstance")]
        public static extern Int64 CreateInstance(Int64 owlClass, byte[] name);

		//
		//		CreateInstanceW                             (http://rdf.bg/gkdoc/CS64/CreateInstanceW.html)
		//
		//	Returns a handle to an on the fly created instance.
		//	If the owlClass input is zero or not a class handle 0 will be returned,
		//
        [DllImport(STEPEngineDLL, EntryPoint = "CreateInstanceW")]
        public static extern Int64 CreateInstanceW(Int64 owlClass, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "CreateInstanceW")]
        public static extern Int64 CreateInstanceW(Int64 owlClass, byte[] name);

		//
		//		CreateInstanceEx                            (http://rdf.bg/gkdoc/CS64/CreateInstanceEx.html)
		//
		//	Returns a handle to an on the fly created instance.
		//	If the owlClass input is zero or not a class handle 0 will be returned,
		//
        [DllImport(STEPEngineDLL, EntryPoint = "CreateInstanceEx")]
        public static extern Int64 CreateInstanceEx(Int64 model, Int64 owlClass, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "CreateInstanceEx")]
        public static extern Int64 CreateInstanceEx(Int64 model, Int64 owlClass, byte[] name);

		//
		//		CreateInstanceWEx                           (http://rdf.bg/gkdoc/CS64/CreateInstanceWEx.html)
		//
		//	Returns a handle to an on the fly created instance.
		//	If the owlClass input is zero or not a class handle 0 will be returned,
		//
        [DllImport(STEPEngineDLL, EntryPoint = "CreateInstanceWEx")]
        public static extern Int64 CreateInstanceWEx(Int64 model, Int64 owlClass, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "CreateInstanceWEx")]
        public static extern Int64 CreateInstanceWEx(Int64 model, Int64 owlClass, byte[] name);

		//
		//		GetInstancesByIterator                      (http://rdf.bg/gkdoc/CS64/GetInstancesByIterator.html)
		//
		//	Returns a handle to an instance.
		//	If input instance is zero, the handle will point to the first relevant instance.
		//	If all instances are past (or no relevant instances are found), the function will return 0.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetInstancesByIterator")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetInstanceClass")]
        public static extern Int64 GetInstanceClass(Int64 owlInstance);

		//
		//		GetInstanceClassEx                          (http://rdf.bg/gkdoc/CS64/GetInstanceClassEx.html)
		//
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetInstanceClassEx")]
        public static extern Int64 GetInstanceClassEx(Int64 model, Int64 owlInstance);

		//
		//		GetInstancePropertyByIterator               (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIterator.html)
		//
		//	Returns a handle to the objectTypeProperty or dataTypeProperty connected to
		//	the instance, this property can also contain a value, but for example also
		//	the knowledge about cardinality restrictions in the context of this instance's class
		//	and the exact cardinality in context of its instance.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetInstancePropertyByIterator")]
        public static extern Int64 GetInstancePropertyByIterator(Int64 owlInstance, Int64 rdfProperty);

		//
		//		GetInstancePropertyByIteratorEx             (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIteratorEx.html)
		//
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetInstancePropertyByIteratorEx")]
        public static extern Int64 GetInstancePropertyByIteratorEx(Int64 model, Int64 owlInstance, Int64 rdfProperty);

		//
		//		GetInstanceInverseReferencesByIterator      (http://rdf.bg/gkdoc/CS64/GetInstanceInverseReferencesByIterator.html)
		//
		//	Returns a handle to the owlInstances refering this instance
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetInstanceInverseReferencesByIterator")]
        public static extern Int64 GetInstanceInverseReferencesByIterator(Int64 owlInstance, Int64 referencingOwlInstance);

		//
		//		GetInstanceReferencesByIterator             (http://rdf.bg/gkdoc/CS64/GetInstanceReferencesByIterator.html)
		//
		//	Returns a handle to the owlInstance refered by this instance
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetInstanceReferencesByIterator")]
        public static extern Int64 GetInstanceReferencesByIterator(Int64 owlInstance, Int64 referencedOwlInstance);

		//
		//		SetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/SetNameOfInstance.html)
		//
		//	Sets/updates the name of the instance, if no error it returns 0.
		//	In case instance does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfInstance")]
        public static extern Int64 SetNameOfInstance(Int64 owlInstance, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfInstance")]
        public static extern Int64 SetNameOfInstance(Int64 owlInstance, byte[] name);

		//
		//		SetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceW.html)
		//
		//	Sets/updates the name of the instance, if no error it returns 0.
		//	In case instance does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfInstanceW")]
        public static extern Int64 SetNameOfInstanceW(Int64 owlInstance, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfInstanceW")]
        public static extern Int64 SetNameOfInstanceW(Int64 owlInstance, byte[] name);

		//
		//		SetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceEx.html)
		//
		//	Sets/updates the name of the instance, if no error it returns 0.
		//	In case instance does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfInstanceEx")]
        public static extern Int64 SetNameOfInstanceEx(Int64 model, Int64 owlInstance, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfInstanceEx")]
        public static extern Int64 SetNameOfInstanceEx(Int64 model, Int64 owlInstance, byte[] name);

		//
		//		SetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceWEx.html)
		//
		//	Sets/updates the name of the instance, if no error it returns 0.
		//	In case instance does not exist it returns 1, when name cannot be updated 2.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]
        public static extern Int64 SetNameOfInstanceWEx(Int64 model, Int64 owlInstance, string name);

        [DllImport(STEPEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]
        public static extern Int64 SetNameOfInstanceWEx(Int64 model, Int64 owlInstance, byte[] name);

		//
		//		GetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/GetNameOfInstance.html)
		//
		//	Returns the name of the instance, if the instance does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfInstance")]
        public static extern void GetNameOfInstance(Int64 owlInstance, out IntPtr name);

		//
		//		GetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceW.html)
		//
		//	Returns the name of the instance, if the instance does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfInstanceW")]
        public static extern void GetNameOfInstanceW(Int64 owlInstance, out IntPtr name);

		//
		//		GetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceEx.html)
		//
		//	Returns the name of the instance, if the instance does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfInstanceEx")]
        public static extern void GetNameOfInstanceEx(Int64 model, Int64 owlInstance, out IntPtr name);

		//
		//		GetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceWEx.html)
		//
		//	Returns the name of the instance, if the instance does not exist it returns nullptr.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetNameOfInstanceWEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, ref byte values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, byte[] values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, Int64[] values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, ref double values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, double[] values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypeProperty")]
        public static extern Int64 SetDatatypeProperty(Int64 owlInstance, Int64 rdfProperty, ref string values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypeProperty")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, ref byte values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, byte[] values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, Int64[] values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, ref double values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, double[] values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
        public static extern Int64 SetDatatypePropertyEx(Int64 model, Int64 owlInstance, Int64 rdfProperty, ref string values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDatatypePropertyEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetDatatypeProperty")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetDatatypePropertyEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetObjectProperty")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetObjectPropertyEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetObjectProperty")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetObjectPropertyEx")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "CreateInstanceInContextStructure")]
        public static extern Int64 CreateInstanceInContextStructure(Int64 owlInstance);

		//
		//		DestroyInstanceInContextStructure           (http://rdf.bg/gkdoc/CS64/DestroyInstanceInContextStructure.html)
		//
		//	InstanceInContext structures are updated dynamically and therfore even while the cost
		//	in performance and memory is limited it is advised to destroy structures as soon
		//	as they are obsolete.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "DestroyInstanceInContextStructure")]
        public static extern void DestroyInstanceInContextStructure(Int64 owlInstanceInContext);

		//
		//		InstanceInContextChild                      (http://rdf.bg/gkdoc/CS64/InstanceInContextChild.html)
		//
		//
        [DllImport(STEPEngineDLL, EntryPoint = "InstanceInContextChild")]
        public static extern Int64 InstanceInContextChild(Int64 owlInstanceInContext);

		//
		//		InstanceInContextNext                       (http://rdf.bg/gkdoc/CS64/InstanceInContextNext.html)
		//
		//
        [DllImport(STEPEngineDLL, EntryPoint = "InstanceInContextNext")]
        public static extern Int64 InstanceInContextNext(Int64 owlInstanceInContext);

		//
		//		InstanceInContextIsUpdated                  (http://rdf.bg/gkdoc/CS64/InstanceInContextIsUpdated.html)
		//
		//
        [DllImport(STEPEngineDLL, EntryPoint = "InstanceInContextIsUpdated")]
        public static extern Int64 InstanceInContextIsUpdated(Int64 owlInstanceInContext);

		//
		//		RemoveInstance                              (http://rdf.bg/gkdoc/CS64/RemoveInstance.html)
		//
		//	This function removes an instance from the internal structure.
		//	In case copies are created by the host this function checks if all
		//	copies are removed otherwise the instance will stay available.
		//	Return value is 0 if everything went ok and positive in case of an error
		//
        [DllImport(STEPEngineDLL, EntryPoint = "RemoveInstance")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "RemoveInstanceRecursively")]
        public static extern Int64 RemoveInstanceRecursively(Int64 owlInstance);

		//
		//		RemoveInstances                             (http://rdf.bg/gkdoc/CS64/RemoveInstances.html)
		//
		//	This function removes all available instances for the given model 
		//	from the internal structure.
		//	Return value is the number of removed instances.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "RemoveInstances")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "CalculateInstance")]
        public static extern Int64 CalculateInstance(Int64 owlInstance, out Int64 vertexBufferSize, out Int64 indexBufferSize, out Int64 transformationBufferSize);

        [DllImport(STEPEngineDLL, EntryPoint = "CalculateInstance")]
        public static extern Int64 CalculateInstance(Int64 owlInstance, out Int64 vertexBufferSize, out Int64 indexBufferSize, IntPtr transformationBufferSize);

        [DllImport(STEPEngineDLL, EntryPoint = "CalculateInstance")]
        public static extern Int64 CalculateInstance(Int64 owlInstance, out Int64 vertexBufferSize, IntPtr indexBufferSize, IntPtr transformationBufferSize);

        [DllImport(STEPEngineDLL, EntryPoint = "CalculateInstance")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstance")]
        public static extern Int64 UpdateInstance(Int64 owlInstance);

		//
		//		InferenceInstance                           (http://rdf.bg/gkdoc/CS64/InferenceInstance.html)
		//
		//	This function fills in values that are implicitely known but not given by the user. This function
		//	can also be used to identify default values of properties if not given.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "InferenceInstance")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
        public static extern Int64 UpdateInstanceVertexBuffer(Int64 owlInstance, out float vertexBuffer);

        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
        public static extern Int64 UpdateInstanceVertexBuffer(Int64 owlInstance, float[] vertexBuffer);

        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
        public static extern Int64 UpdateInstanceVertexBuffer(Int64 owlInstance, out double vertexBuffer);

        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
        public static extern Int64 UpdateInstanceIndexBuffer(Int64 owlInstance, out Int32 indexBuffer);

        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
        public static extern Int64 UpdateInstanceIndexBuffer(Int64 owlInstance, Int32[] indexBuffer);

        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
        public static extern Int64 UpdateInstanceIndexBuffer(Int64 owlInstance, out Int64 indexBuffer);

        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceTransformationBuffer")]
        public static extern Int64 UpdateInstanceTransformationBuffer(Int64 owlInstance, out double transformationBuffer);

        [DllImport(STEPEngineDLL, EntryPoint = "UpdateInstanceTransformationBuffer")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "ClearedInstanceExternalBuffers")]
        public static extern void ClearedInstanceExternalBuffers(Int64 owlInstance);

		//
		//		ClearedExternalBuffers                      (http://rdf.bg/gkdoc/CS64/ClearedExternalBuffers.html)
		//
		//	This function tells the engine that all buffers have no memory of earlier filling.
		//	This means that even when buffer content didn't changed it will be updated when
		//	functions UpdateVertexBuffer(), UpdateIndexBuffer() and/or transformationBuffer()
		//	are called.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "ClearedExternalBuffers")]
        public static extern void ClearedExternalBuffers(Int64 model);

		//
		//		GetConceptualFaceCnt                        (http://rdf.bg/gkdoc/CS64/GetConceptualFaceCnt.html)
		//
		//	This function returns the number of conceptual faces for a certain instance.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceCnt")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFace")]
        public static extern Int64 GetConceptualFace(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noTriangles);

		//
		//		GetConceptualFaceEx                         (http://rdf.bg/gkdoc/CS64/GetConceptualFaceEx.html)
		//
		//	This function returns a handle to the conceptual face. Be aware that different
		//	instances can return the same handles (however with possible different startIndices and noTriangles).
		//	Argument index should be at least zero and smaller then return value of GetConceptualFaceCnt().
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, out Int64 startIndexTriangles, out Int64 noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out Int64 startIndexLines, out Int64 noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out Int64 startIndexPoints, out Int64 noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out Int64 startIndexFacePolygons, out Int64 noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out Int64 startIndexConceptualFacePolygons, out Int64 noIndicesConceptualFacePolygons);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceEx")]
        public static extern Int64 GetConceptualFaceEx(Int64 owlInstance, Int64 index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);

		//
		//		GetConceptualFaceMaterial                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceMaterial.html)
		//
		//	This function returns the material instance relevant for this
		//	conceptual face.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceMaterial")]
        public static extern Int64 GetConceptualFaceMaterial(Int64 conceptualFace);

		//
		//		GetConceptualFaceOriginCnt                  (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginCnt.html)
		//
		//	This function returns the number of instances that are the source primitive/concept
		//	for this conceptual face.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceOriginCnt")]
        public static extern Int64 GetConceptualFaceOriginCnt(Int64 conceptualFace);

		//
		//		GetConceptualFaceOrigin                     (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOrigin.html)
		//
		//	This function returns a handle to the instance that is the source primitive/concept
		//	for this conceptual face.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceOrigin")]
        public static extern Int64 GetConceptualFaceOrigin(Int64 conceptualFace, Int64 index);

		//
		//		GetConceptualFaceOriginEx                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginEx.html)
		//
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceOriginEx")]
        public static extern void GetConceptualFaceOriginEx(Int64 conceptualFace, Int64 index, out Int64 originatingOwlInstance, out Int64 originatingConceptualFace);

		//
		//		GetFaceCnt                                  (http://rdf.bg/gkdoc/CS64/GetFaceCnt.html)
		//
		//	This function returns the number of faces for a certain instance.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetFaceCnt")]
        public static extern Int64 GetFaceCnt(Int64 owlInstance);

		//
		//		GetFace                                     (http://rdf.bg/gkdoc/CS64/GetFace.html)
		//
		//	This function gets the individual faces including the meta data, i.e. the number of openings within this specific face.
		//	This call is for very dedicated use, it would be more common to iterate over the individual conceptual faces.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetFace")]
        public static extern void GetFace(Int64 owlInstance, Int64 index, out Int64 startIndex, out Int64 noOpenings);

		//
		//		GetDependingPropertyCnt                     (http://rdf.bg/gkdoc/CS64/GetDependingPropertyCnt.html)
		//
		//	This function returns the number of properties that are of influence on the
		//	location and form of the conceptualFace.
		//
		//	Note: BE AWARE, THIS FUNCTION EXPECTS A TREE, NOT A NETWORK, IN CASE OF A NETWORK THIS FUNCTION CAN LOCK THE ENGINE
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetDependingPropertyCnt")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetDependingProperty")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetFormat")]
        public static extern Int64 SetFormat(Int64 model, Int64 setting, Int64 mask);

		//
		//		GetFormat                                   (http://rdf.bg/gkdoc/CS64/GetFormat.html)
		//
		//	Returns the current format given a mask.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetFormat")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetBehavior")]
        public static extern void SetBehavior(Int64 model, Int64 setting, Int64 mask);

		//
		//		GetBehavior                                 (http://rdf.bg/gkdoc/CS64/GetBehavior.html)
		//
		//	Returns the current behavior given a mask.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetBehavior")]
        public static extern Int64 GetBehavior(Int64 model, Int64 mask);

		//
		//		SetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/SetVertexBufferTransformation.html)
		//
		//	Sets the transformation for a Vertex Buffer.
		//	The transformation will always be calculated in 64 bit, even if the vertex element size is 32 bit.
		//	This function can be called just before updating the vertex buffer.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetVertexBufferTransformation")]
        public static extern void SetVertexBufferTransformation(Int64 model, out double matrix);

		//
		//		GetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/GetVertexBufferTransformation.html)
		//
		//	Gets the transformation for a Vertex Buffer.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetVertexBufferTransformation")]
        public static extern void GetVertexBufferTransformation(Int64 model, out double matrix);

		//
		//		SetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/SetIndexBufferOffset.html)
		//
		//	Sets the offset for an Index Buffer.
		//	It is important call this function before updating the vertex buffer. 
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetIndexBufferOffset")]
        public static extern void SetIndexBufferOffset(Int64 model, Int64 offset);

		//
		//		GetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/GetIndexBufferOffset.html)
		//
		//	Gets the current offset for an Index Buffer.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetIndexBufferOffset")]
        public static extern Int64 GetIndexBufferOffset(Int64 model);

		//
		//		SetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/SetVertexBufferOffset.html)
		//
		//	Sets the offset for a Vertex Buffer.
		//	The offset will always be calculated in 64 bit, even if the vertex element size is 32 bit.
		//	This function can be called just before updating the vertex buffer.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetVertexBufferOffset")]
        public static extern void SetVertexBufferOffset(Int64 model, double x, double y, double z);

		//
		//		GetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/GetVertexBufferOffset.html)
		//
		//	Gets the offset for a Vertex Buffer.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetVertexBufferOffset")]
        public static extern void GetVertexBufferOffset(Int64 model, out double x, out double y, out double z);

		//
		//		SetDefaultColor                             (http://rdf.bg/gkdoc/CS64/SetDefaultColor.html)
		//
		//	Set the default values for the colors defined as argument.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetDefaultColor")]
        public static extern void SetDefaultColor(Int64 model, Int32 ambient, Int32 diffuse, Int32 emissive, Int32 specular);

		//
		//		GetDefaultColor                             (http://rdf.bg/gkdoc/CS64/GetDefaultColor.html)
		//
		//	Retrieve the default values for the colors defined as argument.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetDefaultColor")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "CheckConsistency")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "CheckInstanceConsistency")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetPerimeter")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetArea")]
        public static extern double GetArea(Int64 owlInstance, ref float vertices, ref Int32 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetArea")]
        public static extern double GetArea(Int64 owlInstance, ref float vertices, ref Int64 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetArea")]
        public static extern double GetArea(Int64 owlInstance, ref double vertices, ref Int32 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetArea")]
        public static extern double GetArea(Int64 owlInstance, ref double vertices, ref Int64 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetArea")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, ref float vertices, ref Int32 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, ref float vertices, ref Int64 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, ref double vertices, ref Int32 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, ref double vertices, ref Int64 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetVolume")]
        public static extern double GetVolume(Int64 owlInstance, IntPtr vertices, IntPtr indices);

		//
		//		GetCentroid                                 (http://rdf.bg/gkdoc/CS64/GetCentroid.html)
		//
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, ref float vertices, ref Int32 indices, out double centroid);

        [DllImport(STEPEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, ref float vertices, ref Int64 indices, out double centroid);

        [DllImport(STEPEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, ref double vertices, ref Int32 indices, out double centroid);

        [DllImport(STEPEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, ref double vertices, ref Int64 indices, out double centroid);

        [DllImport(STEPEngineDLL, EntryPoint = "GetCentroid")]
        public static extern double GetCentroid(Int64 owlInstance, IntPtr vertices, IntPtr indices, out double centroid);

		//
		//		GetConceptualFacePerimeter                  (http://rdf.bg/gkdoc/CS64/GetConceptualFacePerimeter.html)
		//
		//	This function returns the perimeter of a given Conceptual Face.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFacePerimeter")]
        public static extern double GetConceptualFacePerimeter(Int64 conceptualFace);

		//
		//		GetConceptualFaceArea                       (http://rdf.bg/gkdoc/CS64/GetConceptualFaceArea.html)
		//
		//	This function returns the area of a given Conceptual Face. The attributes vertices
		//	and indices are optional but will improve performance if defined.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(Int64 conceptualFace, ref float vertices, ref Int32 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(Int64 conceptualFace, ref float vertices, ref Int64 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(Int64 conceptualFace, ref double vertices, ref Int32 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(Int64 conceptualFace, ref double vertices, ref Int64 indices);

        [DllImport(STEPEngineDLL, EntryPoint = "GetConceptualFaceArea")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "SetBoundingBoxReference")]
        public static extern void SetBoundingBoxReference(Int64 owlInstance, out double transformationMatrix, out double startVector, out double endVector);

		//
		//		GetBoundingBox                              (http://rdf.bg/gkdoc/CS64/GetBoundingBox.html)
		//
		//	When the transformationMatrix is given, it will fill an array of 12 double values.
		//	When the transformationMatrix is left empty and both startVector and endVector are
		//	given the boundingbox without transformation is calculated and returned.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetBoundingBox")]
        public static extern void GetBoundingBox(Int64 owlInstance, out double transformationMatrix, out double startVector, out double endVector);

        [DllImport(STEPEngineDLL, EntryPoint = "GetBoundingBox")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetRelativeTransformation")]
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
        [DllImport(STEPEngineDLL, EntryPoint = "GetTriangles")]
        public static extern void GetTriangles(Int64 owlInstance, out Int64 startIndex, out Int64 noTriangles, out Int64 startVertex, out Int64 firstNotUsedVertex);

		//
		//		GetLines                                    (http://rdf.bg/gkdoc/CS64/GetLines___.html)
		//
		//	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant lines when
		//	SetFormat() is setting bit 9 and unsetting with bit 8, 10, 12 and 13 
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetLines")]
        public static extern void GetLines(Int64 owlInstance, out Int64 startIndex, out Int64 noLines, out Int64 startVertex, out Int64 firstNotUsedVertex);

		//
		//		GetPoints                                   (http://rdf.bg/gkdoc/CS64/GetPoints___.html)
		//
		//	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant points when
		//	SetFormat() is setting bit 10 and unsetting with bit 8, 9, 12 and 13 
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetPoints")]
        public static extern void GetPoints(Int64 owlInstance, out Int64 startIndex, out Int64 noPoints, out Int64 startVertex, out Int64 firstNotUsedVertex);

		//
		//		GetPropertyRestrictions                     (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictions___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyRestrictions")]
        public static extern void GetPropertyRestrictions(Int64 owlClass, Int64 rdfProperty, out Int64 minCard, out Int64 maxCard);

		//
		//		GetPropertyRestrictionsConsolidated         (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictionsConsolidated___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,
		//	just rename the function name.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyRestrictionsConsolidated")]
        public static extern void GetPropertyRestrictionsConsolidated(Int64 owlClass, Int64 rdfProperty, out Int64 minCard, out Int64 maxCard);

		//
		//		IsGeometryType                              (http://rdf.bg/gkdoc/CS64/IsGeometryType___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetGeometryClass instead, rename the function name
		//	and interpret non-zero as true and zero as false.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "IsGeometryType")]
        public static extern byte IsGeometryType(Int64 owlClass);

		//
		//		SetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/SetObjectTypeProperty___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call SetObjectProperty instead, just rename the function name.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetObjectTypeProperty")]
        public static extern Int64 SetObjectTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

		//
		//		GetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/GetObjectTypeProperty___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetObjectProperty instead, just rename the function name.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetObjectTypeProperty")]
        public static extern Int64 GetObjectTypeProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr values, out Int64 card);

		//
		//		SetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/SetDataTypeProperty___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call SetDatatypeProperty instead, just rename the function name.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref byte values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, byte[] values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref Int64 values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, Int64[] values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref double values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, double[] values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, ref string values, Int64 card);

        [DllImport(STEPEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern Int64 SetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, string[] values, Int64 card);

		//
		//		GetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/GetDataTypeProperty___.html)
		//
		//	This call is deprecated and will be removed by end of 2020. Please use the call GetDatatypeProperty instead, just rename the function name.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetDataTypeProperty")]
        public static extern Int64 GetDataTypeProperty(Int64 owlInstance, Int64 rdfProperty, out IntPtr values, out Int64 card);

		//
		//		InstanceCopyCreated                         (http://rdf.bg/gkdoc/CS64/InstanceCopyCreated___.html)
		//
		//	This call is deprecated as the Copy concept is also deprecated and will be removed by end of 2020.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "InstanceCopyCreated")]
        public static extern void InstanceCopyCreated(Int64 owlInstance);

		//
		//		GetPropertyByNameAndType                    (http://rdf.bg/gkdoc/CS64/GetPropertyByNameAndType___.html)
		//
		//	This call is deprecated and will be removed by end of 2020.
		//	Please use the call GetPropertyByName(Ex) / GetPropertyByNameW(Ex) + GetPropertyType(Ex) instead, just rename the function name.
		//
        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyByNameAndType")]
        public static extern Int64 GetPropertyByNameAndType(Int64 model, string name, Int64 rdfPropertyType);

        [DllImport(STEPEngineDLL, EntryPoint = "GetPropertyByNameAndType")]
        public static extern Int64 GetPropertyByNameAndType(Int64 model, byte[] name, Int64 rdfPropertyType);
    }
}
