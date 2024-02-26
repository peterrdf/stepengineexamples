using System;
using System.Runtime.InteropServices;
using static Android.Graphics.ColorSpace;
using static Android.Icu.Text.Edits;
using static Android.Renderscripts.Sampler;
using static IfcEngine.x86_64;

namespace IfcEngine
{

    class x86

    {
        public const string IFCEngineDLL = @"libifcengine.so";


        //

        //  File IO API Calls

        //


        //

        //		sdaiCreateModelBN                           (http://rdf.bg/ifcdoc/CS64/sdaiCreateModelBN.html)

        //

        //	This function creates and empty model (we expect with a schema file given).

        //	Attributes repository and fileName will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]

        public static extern int sdaiCreateModelBN(int repository, string fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]

        public static extern int sdaiCreateModelBN(int repository, string fileName, byte[] schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]

        public static extern int sdaiCreateModelBN(int repository, byte[] fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]

        public static extern int sdaiCreateModelBN(int repository, byte[] fileName, byte[] schemaName);



        //

        //		sdaiCreateModelBNUnicode                    (http://rdf.bg/ifcdoc/CS64/sdaiCreateModelBNUnicode.html)

        //

        //	This function creates and empty model (we expect with a schema file given).

        //	Attributes repository and fileName will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]

        public static extern int sdaiCreateModelBNUnicode(int repository, string fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]

        public static extern int sdaiCreateModelBNUnicode(int repository, string fileName, byte[] schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]

        public static extern int sdaiCreateModelBNUnicode(int repository, byte[] fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]

        public static extern int sdaiCreateModelBNUnicode(int repository, byte[] fileName, byte[] schemaName);



        //

        //		sdaiOpenModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBN.html)

        //

        //	This function opens the model on location fileName.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]

        public static extern int sdaiOpenModelBN(int repository, string fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]

        public static extern int sdaiOpenModelBN(int repository, string fileName, byte[] schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]

        public static extern int sdaiOpenModelBN(int repository, byte[] fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]

        public static extern int sdaiOpenModelBN(int repository, byte[] fileName, byte[] schemaName);



        //

        //		sdaiOpenModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBNUnicode.html)

        //

        //	This function opens the model on location fileName.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]

        public static extern int sdaiOpenModelBNUnicode(int repository, string fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]

        public static extern int sdaiOpenModelBNUnicode(int repository, string fileName, byte[] schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]

        public static extern int sdaiOpenModelBNUnicode(int repository, byte[] fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]

        public static extern int sdaiOpenModelBNUnicode(int repository, byte[] fileName, byte[] schemaName);



        //

        //		engiOpenModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiOpenModelByStream.html)

        //

        //	This function opens the model via a stream.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByStream")]

        public static extern int engiOpenModelByStream(int repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByStream")]

        public static extern int engiOpenModelByStream(int repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, byte[] schemaName);



        //

        //		engiOpenModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiOpenModelByArray.html)

        //

        //	This function opens the model via an array.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByArray")]

        public static extern int engiOpenModelByArray(int repository, byte[] content, int size, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByArray")]

        public static extern int engiOpenModelByArray(int repository, byte[] content, int size, byte[] schemaName);



        //

        //		sdaiSaveModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBN.html)

        //

        //	This function saves the model (char file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]

        public static extern void sdaiSaveModelBN(int model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]

        public static extern void sdaiSaveModelBN(int model, byte[] fileName);



        //

        //		sdaiSaveModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBNUnicode.html)

        //

        //	This function saves the model (wchar, i.e. Unicode file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]

        public static extern void sdaiSaveModelBNUnicode(int model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]

        public static extern void sdaiSaveModelBNUnicode(int model, byte[] fileName);



        //

        //		engiSaveModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiSaveModelByStream.html)

        //

        //	This function saves the model as an array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiSaveModelByStream")]

        public static extern void engiSaveModelByStream(int model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, long size);



        //

        //		engiSaveModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiSaveModelByArray.html)

        //

        //	This function saves the model as an array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiSaveModelByArray")]

        public static extern void engiSaveModelByArray(int model, byte[] content, out int size);



        //

        //		sdaiSaveModelAsXmlBN                        (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBN.html)

        //

        //	This function saves the model as XML according to IFC2x3's way of XML serialization (char file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]

        public static extern void sdaiSaveModelAsXmlBN(int model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]

        public static extern void sdaiSaveModelAsXmlBN(int model, byte[] fileName);



        //

        //		sdaiSaveModelAsXmlBNUnicode                 (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBNUnicode.html)

        //

        //	This function saves the model as XML according to IFC2x3's way of XML serialization (wchar, i.e. Unicode file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]

        public static extern void sdaiSaveModelAsXmlBNUnicode(int model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]

        public static extern void sdaiSaveModelAsXmlBNUnicode(int model, byte[] fileName);



        //

        //		sdaiSaveModelAsSimpleXmlBN                  (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBN.html)

        //

        //	This function saves the model as XML according to IFC4's way of XML serialization (char file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]

        public static extern void sdaiSaveModelAsSimpleXmlBN(int model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]

        public static extern void sdaiSaveModelAsSimpleXmlBN(int model, byte[] fileName);



        //

        //		sdaiSaveModelAsSimpleXmlBNUnicode           (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBNUnicode.html)

        //

        //	This function saves the model as XML according to IFC4's way of XML serialization (wchar, i.e. Unicode file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]

        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(int model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]

        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(int model, byte[] fileName);



        //

        //		sdaiCloseModel                              (http://rdf.bg/ifcdoc/CS64/sdaiCloseModel.html)

        //

        //	This function closes the model. After this call no instance handles will be available including all

        //	handles referencing the geometry of this specific file, in default compilation the model itself will

        //	be known in the kernel, however known to be disabled. Calls containing the model reference will be

        //	protected from crashing when called.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCloseModel")]

        public static extern void sdaiCloseModel(int model);



        //

        //		setPrecisionDoubleExport                    (http://rdf.bg/ifcdoc/CS64/setPrecisionDoubleExport.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setPrecisionDoubleExport")]

        public static extern void setPrecisionDoubleExport(int model, int precisionCap, int precisionRound, byte clean);



        //

        //  Schema Reading API Calls

        //



        //

        //		sdaiGetEntity                               (http://rdf.bg/ifcdoc/CS64/sdaiGetEntity.html)

        //

        //	This call retrieves a handle to an entity based on a given entity name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]

        public static extern int sdaiGetEntity(int model, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]

        public static extern int sdaiGetEntity(int model, byte[] entityName);



        //

        //		engiGetEntityArgument                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgument.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgument")]

        public static extern int engiGetEntityArgument(int entity, string argumentName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgument")]

        public static extern int engiGetEntityArgument(int entity, byte[] argumentName);



        //

        //		engiGetEntityArgumentIndex                  (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentIndex.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentIndex")]

        public static extern int engiGetEntityArgumentIndex(int entity, string argumentName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentIndex")]

        public static extern int engiGetEntityArgumentIndex(int entity, byte[] argumentName);



        //

        //		engiGetEntityArgumentName                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentName.html)

        //

        //	This call can be used to retrieve the name of the n-th argument of the given entity. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentName")]

        public static extern void engiGetEntityArgumentName(int entity, int index, int valueType, out IntPtr argumentName);



        //

        //		engiGetEntityArgumentType                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentType.html)

        //

        //	This call can be used to retrieve the type of the n-th argument of the given entity. In case of a select argument no relevant information is given by this call as it depends on the instance. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentType")]

        public static extern void engiGetEntityArgumentType(int entity, int index, out int argumentType);



        //

        //		engiGetEntityCount                          (http://rdf.bg/ifcdoc/CS64/engiGetEntityCount.html)

        //

        //	Returns the total number of entities within the loaded schema.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityCount")]

        public static extern int engiGetEntityCount(int model);



        //

        //		engiGetEntityElement                        (http://rdf.bg/ifcdoc/CS64/engiGetEntityElement.html)

        //

        //	This call returns a specific entity based on an index, the index needs to be 0 or higher but lower then the number of entities in the loaded schema.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityElement")]

        public static extern int engiGetEntityElement(int model, int index);



        //

        //		sdaiGetEntityExtent                         (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtent.html)

        //

        //	This call retrieves an aggregation that contains all instances of the entity given.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtent")]

        public static extern int sdaiGetEntityExtent(int model, int entity);



        //

        //		sdaiGetEntityExtentBN                       (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtentBN.html)

        //

        //	This call retrieves an aggregation that contains all instances of the entity given.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]

        public static extern int sdaiGetEntityExtentBN(int model, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]

        public static extern int sdaiGetEntityExtentBN(int model, byte[] entityName);



        //

        //		engiGetEntityName                           (http://rdf.bg/ifcdoc/CS64/engiGetEntityName.html)

        //

        //	This call can be used to get the name of the given entity.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityName")]

        public static extern void engiGetEntityName(int entity, int valueType, out IntPtr entityName);



        //

        //		engiGetEntityNoArguments                    (http://rdf.bg/ifcdoc/CS64/engiGetEntityNoArguments.html)

        //

        //	This call returns the number of arguments, this includes the arguments of its (nested) parents and inverse argumnets.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityNoArguments")]

        public static extern int engiGetEntityNoArguments(int entity);



        //

        //		engiGetEntityParent                         (http://rdf.bg/ifcdoc/CS64/engiGetEntityParent.html)

        //

        //	Returns the direct parent entity, for example the parent of IfcObject is IfcObjectDefinition, of IfcObjectDefinition is IfcRoot and of IfcRoot is 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityParent")]

        public static extern int engiGetEntityParent(int entity);



        //

        //		engiGetAttrOptional                         (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptional.html)

        //

        //	This call can be used to check if an attribute is optional

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrOptional")]

        public static extern int engiGetAttrOptional(ref int attribute);



        //

        //		engiGetAttrOptionalBN                       (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptionalBN.html)

        //

        //	This call can be used to check if an attribute is optional

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrOptionalBN")]

        public static extern int engiGetAttrOptionalBN(int entity, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrOptionalBN")]

        public static extern int engiGetAttrOptionalBN(int entity, byte[] attributeName);



        //

        //		engiGetAttrInverse                          (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverse.html)

        //

        //	This call can be used to check if an attribute is an inverse relation

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrInverse")]

        public static extern int engiGetAttrInverse(ref int attribute);



        //

        //		engiGetAttrInverseBN                        (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverseBN.html)

        //

        //	This call can be used to check if an attribute is an inverse relation

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrInverseBN")]

        public static extern int engiGetAttrInverseBN(int entity, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrInverseBN")]

        public static extern int engiGetAttrInverseBN(int entity, byte[] attributeName);



        //

        //		engiGetEnumerationValue                     (http://rdf.bg/ifcdoc/CS64/engiGetEnumerationValue.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEnumerationValue")]

        public static extern void engiGetEnumerationValue(int attribute, int index, int valueType, out IntPtr enumerationValue);



        //

        //		engiGetEntityProperty                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityProperty.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityProperty")]

        public static extern void engiGetEntityProperty(int entity, int index, out IntPtr propertyName, out int optional, out int type, out int _array, out int set, out int list, out int bag, out int min, out int max, out int referenceEntity, out int inverse);



        //

        //  Instance Header API Calls

        //



        //

        //		SetSPFFHeader                               (http://rdf.bg/ifcdoc/CS64/SetSPFFHeader.html)

        //

        //	This call is an aggregate of several SetSPFFHeaderItem calls. In several cases the header can be set easily with this call. In case an argument is zero, this argument will not be updated, i.e. it will not be filled with 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]

        public static extern void SetSPFFHeader(int model, string description, string implementationLevel, string name, string timeStamp, string author, string organization, string preprocessorVersion, string originatingSystem, string authorization, string fileSchema);



        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]

        public static extern void SetSPFFHeader(int model, byte[] description, byte[] implementationLevel, byte[] name, byte[] timeStamp, byte[] author, byte[] organization, byte[] preprocessorVersion, byte[] originatingSystem, byte[] authorization, byte[] fileSchema);



        //

        //		SetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/SetSPFFHeaderItem.html)

        //

        //	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]

        public static extern int SetSPFFHeaderItem(int model, int itemIndex, int itemSubIndex, int valueType, string value);



        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]

        public static extern int SetSPFFHeaderItem(int model, int itemIndex, int itemSubIndex, int valueType, byte[] value);



        //

        //		GetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItem.html)

        //

        //	This call can be used to read a specific header item, the source code example is larger to show and explain how this call can be used.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItem")]

        public static extern int GetSPFFHeaderItem(int model, int itemIndex, int itemSubIndex, int valueType, out IntPtr value);



        //

        //		GetSPFFHeaderItemUnicode                    (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItemUnicode.html)

        //

        //	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItemUnicode")]

        public static extern int GetSPFFHeaderItemUnicode(int model, int itemIndex, int itemSubIndex, string buffer, int bufferLength);



        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItemUnicode")]

        public static extern int GetSPFFHeaderItemUnicode(int model, int itemIndex, int itemSubIndex, byte[] buffer, int bufferLength);



        //

        //  Instance Reading API Calls

        //



        //

        //		sdaiGetADBType                              (http://rdf.bg/ifcdoc/CS64/sdaiGetADBType.html)

        //

        //	This call can be used to get the used type within this ADB type.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBType")]

        public static extern int sdaiGetADBType(ref int ADB);



        //

        //		sdaiGetADBTypePath                          (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePath.html)

        //

        //	This call can be used to get the path of an ADB type.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePath")]

        public static extern IntPtr sdaiGetADBTypePath(ref int ADB, int typeNameNumber);



        //

        //		sdaiGetADBTypePathx                         (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePathx.html)

        //

        //	This call is the same as sdaiGetADBTypePath, however can be used by porting to languages that have issues with returned char arrays.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePathx")]

        public static extern void sdaiGetADBTypePathx(ref int ADB, int typeNameNumber, out IntPtr path);



        //

        //		sdaiGetADBValue                             (http://rdf.bg/ifcdoc/CS64/sdaiGetADBValue.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]

        public static extern void sdaiGetADBValue(ref int ADB, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]

        public static extern void sdaiGetADBValue(ref int ADB, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]

        public static extern void sdaiGetADBValue(ref int ADB, int valueType, out IntPtr value);



        //

        //		engiGetAggrElement                          (http://rdf.bg/ifcdoc/CS64/engiGetAggrElement.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]

        public static extern int engiGetAggrElement(int aggregate, int elementIndex, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]

        public static extern int engiGetAggrElement(int aggregate, int elementIndex, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]

        public static extern int engiGetAggrElement(int aggregate, int elementIndex, int valueType, out IntPtr value);



        //

        //		engiGetAggrType                             (http://rdf.bg/ifcdoc/CS64/engiGetAggrType.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrType")]

        public static extern void engiGetAggrType(int aggregate, out int aggragateType);



        //

        //		engiGetAggrTypex                            (http://rdf.bg/ifcdoc/CS64/engiGetAggrTypex.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrTypex")]

        public static extern void engiGetAggrTypex(int aggregate, out int aggragateType);



        //

        //		sdaiGetAttr                                 (http://rdf.bg/ifcdoc/CS64/sdaiGetAttr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]

        public static extern int sdaiGetAttr(int instance, int attribute, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]

        public static extern int sdaiGetAttr(int instance, int attribute, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]

        public static extern int sdaiGetAttr(int instance, int attribute, int valueType, out IntPtr value);



        //

        //		sdaiGetAttrBN                               (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern int sdaiGetAttrBN(int instance, string attributeName, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern int sdaiGetAttrBN(int instance, string attributeName, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern int sdaiGetAttrBN(int instance, string attributeName, int valueType, out IntPtr value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern int sdaiGetAttrBN(int instance, byte[] attributeName, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern int sdaiGetAttrBN(int instance, byte[] attributeName, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern int sdaiGetAttrBN(int instance, byte[] attributeName, int valueType, out IntPtr value);



        //

        //		sdaiGetAttrBNUnicode                        (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBNUnicode.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode")]

        public static extern int sdaiGetAttrBNUnicode(int instance, string attributeName, string buffer, int bufferLength);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode")]

        public static extern int sdaiGetAttrBNUnicode(int instance, byte[] attributeName, byte[] buffer, int bufferLength);



        //

        //		sdaiGetStringAttrBN                         (http://rdf.bg/ifcdoc/CS64/sdaiGetStringAttrBN.html)

        //

        //	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiSTRING.

        //	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,

        //	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetStringAttrBN")]

        public static extern IntPtr sdaiGetStringAttrBN(int instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetStringAttrBN")]

        public static extern IntPtr sdaiGetStringAttrBN(int instance, byte[] attributeName);



        //

        //		sdaiGetInstanceAttrBN                       (http://rdf.bg/ifcdoc/CS64/sdaiGetInstanceAttrBN.html)

        //

        //	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiINSTANCE.

        //	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,

        //	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceAttrBN")]

        public static extern int sdaiGetInstanceAttrBN(int instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceAttrBN")]

        public static extern int sdaiGetInstanceAttrBN(int instance, byte[] attributeName);



        //

        //		sdaiGetAggregationAttrBN                    (http://rdf.bg/ifcdoc/CS64/sdaiGetAggregationAttrBN.html)

        //

        //	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiAGGR.

        //	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,

        //	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggregationAttrBN")]

        public static extern int sdaiGetAggregationAttrBN(int instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggregationAttrBN")]

        public static extern int sdaiGetAggregationAttrBN(int instance, byte[] attributeName);



        //

        //		sdaiGetAttrDefinition                       (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrDefinition.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]

        public static extern int sdaiGetAttrDefinition(int entity, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]

        public static extern int sdaiGetAttrDefinition(int entity, byte[] attributeName);



        //

        //		sdaiGetInstanceType                         (http://rdf.bg/ifcdoc/CS64/sdaiGetInstanceType.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceType")]

        public static extern int sdaiGetInstanceType(int instance);



        //

        //		sdaiGetMemberCount                          (http://rdf.bg/ifcdoc/CS64/sdaiGetMemberCount.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetMemberCount")]

        public static extern int sdaiGetMemberCount(int aggregate);



        //

        //		sdaiIsKindOf                                (http://rdf.bg/ifcdoc/CS64/sdaiIsKindOf.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsKindOf")]

        public static extern int sdaiIsKindOf(int instance, int entity);



        //

        //		engiGetAttrType                             (http://rdf.bg/ifcdoc/CS64/engiGetAttrType.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrType")]

        public static extern int engiGetAttrType(int instance, ref int attribute);



        //

        //		engiGetAttrTypeBN                           (http://rdf.bg/ifcdoc/CS64/engiGetAttrTypeBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrTypeBN")]

        public static extern int engiGetAttrTypeBN(int instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrTypeBN")]

        public static extern int engiGetAttrTypeBN(int instance, byte[] attributeName);



        //

        //		sdaiIsInstanceOf                            (http://rdf.bg/ifcdoc/CS64/sdaiIsInstanceOf.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsInstanceOf")]

        public static extern int sdaiIsInstanceOf(int instance, int entity);



        //

        //		sdaiIsInstanceOfBN                          (http://rdf.bg/ifcdoc/CS64/sdaiIsInstanceOfBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsInstanceOfBN")]

        public static extern int sdaiIsInstanceOfBN(int instance, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsInstanceOfBN")]

        public static extern int sdaiIsInstanceOfBN(int instance, byte[] entityName);



        //

        //		engiValidateAttr                            (http://rdf.bg/ifcdoc/CS64/engiValidateAttr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiValidateAttr")]

        public static extern int engiValidateAttr(int instance, ref int attribute);



        //

        //		engiValidateAttrBN                          (http://rdf.bg/ifcdoc/CS64/engiValidateAttrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiValidateAttrBN")]

        public static extern int engiValidateAttrBN(int instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiValidateAttrBN")]

        public static extern int engiValidateAttrBN(int instance, byte[] attributeName);



        //

        //		sdaiCreateInstanceEI                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceEI.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceEI")]

        public static extern int sdaiCreateInstanceEI(int model, int entity, int express_id);



        //

        //		sdaiCreateInstanceBNEI                      (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceBNEI.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBNEI")]

        public static extern int sdaiCreateInstanceBNEI(int model, string entityName, int express_id);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBNEI")]

        public static extern int sdaiCreateInstanceBNEI(int model, byte[] entityName, int express_id);



        //

        //  Instance Writing API Calls

        //



        //

        //		sdaiPrepend                                 (http://rdf.bg/ifcdoc/CS64/sdaiPrepend.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrepend")]

        public static extern void sdaiPrepend(int list, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrepend")]

        public static extern void sdaiPrepend(int list, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrepend")]

        public static extern void sdaiPrepend(int list, int valueType, out IntPtr value);



        //

        //		sdaiAppend                                  (http://rdf.bg/ifcdoc/CS64/sdaiAppend.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]

        public static extern void sdaiAppend(int list, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]

        public static extern void sdaiAppend(int list, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]

        public static extern void sdaiAppend(int list, int valueType, out IntPtr value);



        //

        //		engiAppend                                  (http://rdf.bg/ifcdoc/CS64/engiAppend.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiAppend")]

        public static extern void engiAppend(int list, int valueType, out IntPtr values, int card);



        //

        //		sdaiCreateADB                               (http://rdf.bg/ifcdoc/CS64/sdaiCreateADB.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]

        public static extern int sdaiCreateADB(int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]

        public static extern int sdaiCreateADB(int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]

        public static extern int sdaiCreateADB(int valueType, out IntPtr value);



        //

        //		sdaiCreateAggr                              (http://rdf.bg/ifcdoc/CS64/sdaiCreateAggr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggr")]

        public static extern int sdaiCreateAggr(int instance, ref int attribute);



        //

        //		sdaiCreateAggrBN                            (http://rdf.bg/ifcdoc/CS64/sdaiCreateAggrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]

        public static extern int sdaiCreateAggrBN(int instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]

        public static extern int sdaiCreateAggrBN(int instance, byte[] attributeName);



        //

        //		sdaiCreateNestedAggr                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateNestedAggr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateNestedAggr")]

        public static extern int sdaiCreateNestedAggr(out int aggr);



        //

        //		sdaiCreateInstance                          (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstance.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstance")]

        public static extern int sdaiCreateInstance(int model, int entity);



        //

        //		sdaiCreateInstanceBN                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]

        public static extern int sdaiCreateInstanceBN(int model, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]

        public static extern int sdaiCreateInstanceBN(int model, byte[] entityName);



        //

        //		sdaiDeleteInstance                          (http://rdf.bg/ifcdoc/CS64/sdaiDeleteInstance.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteInstance")]

        public static extern void sdaiDeleteInstance(int instance);



        //

        //		sdaiPutADBTypePath                          (http://rdf.bg/ifcdoc/CS64/sdaiPutADBTypePath.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]

        public static extern void sdaiPutADBTypePath(string ADB, int pathCount, string path);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]

        public static extern void sdaiPutADBTypePath(byte[] ADB, int pathCount, byte[] path);



        //

        //		sdaiPutAttr                                 (http://rdf.bg/ifcdoc/CS64/sdaiPutAttr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]

        public static extern void sdaiPutAttr(int instance, ref int attribute, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]

        public static extern void sdaiPutAttr(int instance, ref int attribute, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]

        public static extern void sdaiPutAttr(int instance, ref int attribute, int valueType, out IntPtr value);



        //

        //		sdaiPutAttrBN                               (http://rdf.bg/ifcdoc/CS64/sdaiPutAttrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(int instance, string attributeName, int valueType, int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(int instance, string attributeName, int valueType, ref double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(int instance, string attributeName, int valueType, ref IntPtr value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(int instance, byte[] attributeName, int valueType, int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(int instance, byte[] attributeName, int valueType, ref double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(int instance, byte[] attributeName, int valueType, ref IntPtr value);



        //

        //		engiSetComment                              (http://rdf.bg/ifcdoc/CS64/engiSetComment.html)

        //

        //	This call can be used to add a comment to an instance when exporting the content. The comment is available in the exported/saved IFC file.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]

        public static extern void engiSetComment(int instance, string comment);



        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]

        public static extern void engiSetComment(int instance, byte[] comment);



        //

        //		engiGetInstanceLocalId                      (http://rdf.bg/ifcdoc/CS64/engiGetInstanceLocalId.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceLocalId")]

        public static extern int engiGetInstanceLocalId(int instance);



        //

        //		sdaiTestAttr                                (http://rdf.bg/ifcdoc/CS64/sdaiTestAttr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiTestAttr")]

        public static extern int sdaiTestAttr(int instance, ref int attribute);



        //

        //		sdaiTestAttrBN                              (http://rdf.bg/ifcdoc/CS64/sdaiTestAttrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiTestAttrBN")]

        public static extern int sdaiTestAttrBN(int instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiTestAttrBN")]

        public static extern int sdaiTestAttrBN(int instance, byte[] attributeName);



        //

        //		engiGetInstanceClassInfo                    (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfo.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceClassInfo")]

        public static extern IntPtr engiGetInstanceClassInfo(int instance);



        //

        //		engiGetInstanceClassInfoUC                  (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfoUC.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceClassInfoUC")]

        public static extern IntPtr engiGetInstanceClassInfoUC(int instance);



        //

        //		engiGetInstanceClassInfoEx                  (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfoEx.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceClassInfoEx")]

        public static extern void engiGetInstanceClassInfoEx(int instance, out IntPtr classInfo);



        //

        //		engiGetInstanceMetaInfo                     (http://rdf.bg/ifcdoc/CS64/engiGetInstanceMetaInfo.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceMetaInfo")]

        public static extern int engiGetInstanceMetaInfo(int instance, out int localId, out IntPtr entityName, out IntPtr entityNameUC);



        //

        //  Controling API Calls

        //



        //

        //		circleSegments                              (http://rdf.bg/ifcdoc/CS64/circleSegments.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "circleSegments")]

        public static extern void circleSegments(int circles, int smallCircles);



        //

        //		setMaximumSegmentationLength                (http://rdf.bg/ifcdoc/CS64/setMaximumSegmentationLength.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setMaximumSegmentationLength")]

        public static extern void setMaximumSegmentationLength(int model, double length);



        //

        //		getUnitConversionFactor                     (http://rdf.bg/ifcdoc/CS64/getUnitConversionFactor.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getUnitConversionFactor")]

        public static extern double getUnitConversionFactor(int model, string unitType, out IntPtr unitPrefix, out IntPtr unitName, out IntPtr SIUnitName);



        [DllImport(IFCEngineDLL, EntryPoint = "getUnitConversionFactor")]

        public static extern double getUnitConversionFactor(int model, byte[] unitType, out IntPtr unitPrefix, out IntPtr unitName, out IntPtr SIUnitName);



        //

        //		setBRepProperties                           (http://rdf.bg/ifcdoc/CS64/setBRepProperties.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setBRepProperties")]

        public static extern void setBRepProperties(int model, long consistencyCheck, double fraction, double epsilon, int maxVerticesSize);



        //

        //		cleanMemory                                 (http://rdf.bg/ifcdoc/CS64/cleanMemory.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "cleanMemory")]

        public static extern void cleanMemory(int model, int mode);



        //

        //		internalGetP21Line                          (http://rdf.bg/ifcdoc/CS64/internalGetP21Line.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetP21Line")]

        public static extern int internalGetP21Line(int instance);



        //

        //		internalGetInstanceFromP21Line              (http://rdf.bg/ifcdoc/CS64/internalGetInstanceFromP21Line.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetInstanceFromP21Line")]

        public static extern int internalGetInstanceFromP21Line(int model, int P21Line);



        //

        //		internalGetXMLID                            (http://rdf.bg/ifcdoc/CS64/internalGetXMLID.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetXMLID")]

        public static extern void internalGetXMLID(int instance, out IntPtr XMLID);



        //

        //		setStringUnicode                            (http://rdf.bg/ifcdoc/CS64/setStringUnicode.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setStringUnicode")]

        public static extern int setStringUnicode(int unicode);



        //

        //		setFilter                                   (http://rdf.bg/ifcdoc/CS64/setFilter.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setFilter")]

        public static extern void setFilter(int model, int setting, int mask);



        //

        //		getFilter                                   (http://rdf.bg/ifcdoc/CS64/getFilter.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getFilter")]

        public static extern int getFilter(int model, int mask);



        //

        //  Uncategorized API Calls

        //



        //

        //		xxxxGetEntityAndSubTypesExtent              (http://rdf.bg/ifcdoc/CS64/xxxxGetEntityAndSubTypesExtent.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtent")]

        public static extern int xxxxGetEntityAndSubTypesExtent(int model, int entity);



        //

        //		xxxxGetEntityAndSubTypesExtentBN            (http://rdf.bg/ifcdoc/CS64/xxxxGetEntityAndSubTypesExtentBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtentBN")]

        public static extern int xxxxGetEntityAndSubTypesExtentBN(int model, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtentBN")]

        public static extern int xxxxGetEntityAndSubTypesExtentBN(int model, byte[] entityName);



        //

        //		xxxxGetInstancesUsing                       (http://rdf.bg/ifcdoc/CS64/xxxxGetInstancesUsing.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetInstancesUsing")]

        public static extern int xxxxGetInstancesUsing(int instance);



        //

        //		xxxxDeleteFromAggregation                   (http://rdf.bg/ifcdoc/CS64/xxxxDeleteFromAggregation.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxDeleteFromAggregation")]

        public static extern int xxxxDeleteFromAggregation(int instance, out int aggregate, int elementIndex);



        //

        //		xxxxGetAttrDefinitionByValue                (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrDefinitionByValue.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]

        public static extern int xxxxGetAttrDefinitionByValue(int instance, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]

        public static extern int xxxxGetAttrDefinitionByValue(int instance, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]

        public static extern int xxxxGetAttrDefinitionByValue(int instance, out IntPtr value);



        //

        //		xxxxGetAttrNameByIndex                      (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrNameByIndex.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrNameByIndex")]

        public static extern void xxxxGetAttrNameByIndex(int instance, int index, out IntPtr name);



        //

        //		iterateOverInstances                        (http://rdf.bg/ifcdoc/CS64/iterateOverInstances.html)

        //

        //	This function interates over all available instances loaded in memory, it is the fastest way to find all instances.

        //	Argument entity and entityName are both optional and if non-zero are filled with respectively the entity handle and entity name as char array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "iterateOverInstances")]

        public static extern int iterateOverInstances(int model, int instance, out int entity, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "iterateOverInstances")]

        public static extern int iterateOverInstances(int model, int instance, out int entity, byte[] entityName);



        //

        //		iterateOverProperties                       (http://rdf.bg/ifcdoc/CS64/iterateOverProperties.html)

        //

        //	This function iterated over all available attributes of a specific given entity.

        //	This call is typically used in combination with iterateOverInstances(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "iterateOverProperties")]

        public static extern int iterateOverProperties(int entity, int index);



        //

        //		sdaiGetAggrByIterator                       (http://rdf.bg/ifcdoc/CS64/sdaiGetAggrByIterator.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]

        public static extern int sdaiGetAggrByIterator(int iterator, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]

        public static extern int sdaiGetAggrByIterator(int iterator, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]

        public static extern int sdaiGetAggrByIterator(int iterator, int valueType, out IntPtr value);



        //

        //		sdaiPutAggrByIterator                       (http://rdf.bg/ifcdoc/CS64/sdaiPutAggrByIterator.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]

        public static extern void sdaiPutAggrByIterator(int iterator, int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]

        public static extern void sdaiPutAggrByIterator(int iterator, int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]

        public static extern void sdaiPutAggrByIterator(int iterator, int valueType, out IntPtr value);



        //

        //		internalSetLink                             (http://rdf.bg/ifcdoc/CS64/internalSetLink.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalSetLink")]

        public static extern void internalSetLink(int instance, string attributeName, int linked_id);



        [DllImport(IFCEngineDLL, EntryPoint = "internalSetLink")]

        public static extern void internalSetLink(int instance, byte[] attributeName, int linked_id);



        //

        //		internalAddAggrLink                         (http://rdf.bg/ifcdoc/CS64/internalAddAggrLink.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalAddAggrLink")]

        public static extern void internalAddAggrLink(int list, int linked_id);



        //

        //		engiGetNotReferedAggr                       (http://rdf.bg/ifcdoc/CS64/engiGetNotReferedAggr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetNotReferedAggr")]

        public static extern void engiGetNotReferedAggr(int model, out int value);



        //

        //		engiGetAttributeAggr                        (http://rdf.bg/ifcdoc/CS64/engiGetAttributeAggr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttributeAggr")]

        public static extern void engiGetAttributeAggr(int instance, out int value);



        //

        //		engiGetAggrUnknownElement                   (http://rdf.bg/ifcdoc/CS64/engiGetAggrUnknownElement.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(int aggregate, int elementIndex, out int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(int aggregate, int elementIndex, out int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(int aggregate, int elementIndex, out int valueType, out IntPtr value);



        //

        //		sdaiErrorQuery                              (http://rdf.bg/ifcdoc/CS64/sdaiErrorQuery.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiErrorQuery")]

        public static extern int sdaiErrorQuery();



        //

        //  Geometry Kernel related API Calls

        //



        //

        //		owlGetModel                                 (http://rdf.bg/ifcdoc/CS64/owlGetModel.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlGetModel")]

        public static extern void owlGetModel(int model, out long owlModel);



        //

        //		owlGetInstance                              (http://rdf.bg/ifcdoc/CS64/owlGetInstance.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlGetInstance")]

        public static extern void owlGetInstance(int model, int instance, out long owlInstance);



        //

        //		owlBuildInstance                            (http://rdf.bg/ifcdoc/CS64/owlBuildInstance.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlBuildInstance")]

        public static extern void owlBuildInstance(int model, int instance, out long owlInstance);



        //

        //		owlBuildInstances                           (http://rdf.bg/ifcdoc/CS64/owlBuildInstances.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlBuildInstances")]

        public static extern void owlBuildInstances(int model, int instance, out long owlInstanceComplete, out long owlInstanceSolids, out long owlInstanceVoids);



        //

        //		owlGetMappedItem                            (http://rdf.bg/ifcdoc/CS64/owlGetMappedItem.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlGetMappedItem")]

        public static extern void owlGetMappedItem(int model, int instance, out long owlInstance, out double transformationMatrix);



        //

        //		getInstanceDerivedPropertiesInModelling     (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedPropertiesInModelling.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceDerivedPropertiesInModelling")]

        public static extern int getInstanceDerivedPropertiesInModelling(int model, int instance, out double height, out double width, out double thickness);



        //

        //		getInstanceDerivedBoundingBox               (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedBoundingBox.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceDerivedBoundingBox")]

        public static extern int getInstanceDerivedBoundingBox(int model, int instance, out double Ox, out double Oy, out double Oz, out double Vx, out double Vy, out double Vz);



        //

        //		getInstanceTransformationMatrix             (http://rdf.bg/ifcdoc/CS64/getInstanceTransformationMatrix.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceTransformationMatrix")]

        public static extern int getInstanceTransformationMatrix(int model, int instance, out double _11, out double _12, out double _13, out double _14, out double _21, out double _22, out double _23, out double _24, out double _31, out double _32, out double _33, out double _34, out double _41, out double _42, out double _43, out double _44);



        //

        //		getInstanceDerivedTransformationMatrix      (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedTransformationMatrix.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceDerivedTransformationMatrix")]

        public static extern int getInstanceDerivedTransformationMatrix(int model, int instance, out double _11, out double _12, out double _13, out double _14, out double _21, out double _22, out double _23, out double _24, out double _31, out double _32, out double _33, out double _34, out double _41, out double _42, out double _43, out double _44);



        //

        //		internalGetBoundingBox                      (http://rdf.bg/ifcdoc/CS64/internalGetBoundingBox.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetBoundingBox")]

        public static extern int internalGetBoundingBox(int model, int instance);



        //

        //		internalGetCenter                           (http://rdf.bg/ifcdoc/CS64/internalGetCenter.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetCenter")]

        public static extern int internalGetCenter(int model, int instance);



        //

        //  Deprecated API Calls (GENERIC)

        //



        //

        //		engiAttrIsInverse                           (http://rdf.bg/ifcdoc/CS64/engiAttrIsInverse.html)

        //

        //	This call is deprecated, please use call engiAttrIsInverse.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiAttrIsInverse")]

        public static extern int engiAttrIsInverse(ref int attribute);



        //

        //		xxxxOpenModelByStream                       (http://rdf.bg/ifcdoc/CS64/xxxxOpenModelByStream.html)

        //

        //	This call is deprecated, please use call engiOpenModelByStream.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxOpenModelByStream")]

        public static extern int xxxxOpenModelByStream(int repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxOpenModelByStream")]

        public static extern int xxxxOpenModelByStream(int repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, byte[] schemaName);



        //

        //		sdaiCreateIterator                          (http://rdf.bg/ifcdoc/CS64/sdaiCreateIterator.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateIterator")]

        public static extern int sdaiCreateIterator(ref int aggregate);



        //

        //		sdaiDeleteIterator                          (http://rdf.bg/ifcdoc/CS64/sdaiDeleteIterator.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteIterator")]

        public static extern void sdaiDeleteIterator(int iterator);



        //

        //		sdaiBeginning                               (http://rdf.bg/ifcdoc/CS64/sdaiBeginning.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiBeginning")]

        public static extern void sdaiBeginning(int iterator);



        //

        //		sdaiNext                                    (http://rdf.bg/ifcdoc/CS64/sdaiNext.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiNext")]

        public static extern int sdaiNext(int iterator);



        //

        //		sdaiPrevious                                (http://rdf.bg/ifcdoc/CS64/sdaiPrevious.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrevious")]

        public static extern int sdaiPrevious(int iterator);



        //

        //		sdaiEnd                                     (http://rdf.bg/ifcdoc/CS64/sdaiEnd.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiEnd")]

        public static extern void sdaiEnd(int iterator);



        //

        //		sdaiplusGetAggregationType                  (http://rdf.bg/ifcdoc/CS64/sdaiplusGetAggregationType.html)

        //

        //	This call is deprecated, please use call ....

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiplusGetAggregationType")]

        public static extern int sdaiplusGetAggregationType(int instance, out int aggregation);



        //

        //		xxxxGetAttrTypeBN                           (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrTypeBN.html)

        //

        //	This call is deprecated, please use calls engiGetAttrTypeBN(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrTypeBN")]

        public static extern int xxxxGetAttrTypeBN(int instance, string attributeName, out IntPtr attributeType);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrTypeBN")]

        public static extern int xxxxGetAttrTypeBN(int instance, byte[] attributeName, out IntPtr attributeType);



        //

        //  Deprecated API Calls (GEOMETRY)

        //



        //

        //		initializeModellingInstance                 (http://rdf.bg/ifcdoc/CS64/initializeModellingInstance.html)

        //

        //	This call is deprecated, please use call CalculateInstance().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstance")]

        public static extern int initializeModellingInstance(int model, out int noVertices, out int noIndices, double scale, int instance);



        //

        //		finalizeModelling                           (http://rdf.bg/ifcdoc/CS64/finalizeModelling.html)

        //

        //	This call is deprecated, please use call UpdateInstanceVertexBuffer() and UpdateInstanceIndexBuffer().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]

        public static extern int finalizeModelling(int model, float[] vertices, out int indices, int FVF);



        //

        //		getInstanceInModelling                      (http://rdf.bg/ifcdoc/CS64/getInstanceInModelling.html)

        //

        //	This call is deprecated, there is no direct / easy replacement although the functionality is present. If you still use this call please contact RDF to find a solution together.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceInModelling")]

        public static extern int getInstanceInModelling(int model, int instance, int mode, out int startVertex, out int startIndex, out int primitiveCount);



        //

        //		setVertexOffset                             (http://rdf.bg/ifcdoc/CS64/setVertexOffset.html)

        //

        //	This call is deprecated, please use call SetVertexBufferOffset().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setVertexOffset")]

        public static extern void setVertexOffset(int model, double x, double y, double z);



        //

        //		setFormat                                   (http://rdf.bg/ifcdoc/CS64/setFormat.html)

        //

        //	This call is deprecated, please use call SetFormat().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setFormat")]

        public static extern void setFormat(int model, int setting, int mask);



        //

        //		getConceptualFaceCnt                        (http://rdf.bg/ifcdoc/CS64/getConceptualFaceCnt.html)

        //

        //	This call is deprecated, please use call GetConceptualFaceCnt().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceCnt")]

        public static extern int getConceptualFaceCnt(int instance);



        //

        //		getConceptualFaceEx                         (http://rdf.bg/ifcdoc/CS64/getConceptualFaceEx.html)

        //

        //	This call is deprecated, please use call GetConceptualFaceEx().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceEx")]

        public static extern int getConceptualFaceEx(int instance, int index, out int startIndexTriangles, out int noIndicesTriangles, out int startIndexLines, out int noIndicesLines, out int startIndexPoints, out int noIndicesPoints, out int startIndexFacesPolygons, out int noIndicesFacesPolygons, out int startIndexConceptualFacePolygons, out int noIndicesConceptualFacePolygons);



        //

        //		createGeometryConversion                    (http://rdf.bg/ifcdoc/CS64/createGeometryConversion.html)

        //

        //	This call is deprecated, please use call ... .

        //

        [DllImport(IFCEngineDLL, EntryPoint = "createGeometryConversion")]

        public static extern void createGeometryConversion(int instance, out long owlInstance);



        //

        //		convertInstance                             (http://rdf.bg/ifcdoc/CS64/convertInstance.html)

        //

        //	This call is deprecated, please use call ... .

        //

        [DllImport(IFCEngineDLL, EntryPoint = "convertInstance")]

        public static extern void convertInstance(int instance);



        //

        //		initializeModellingInstanceEx               (http://rdf.bg/ifcdoc/CS64/initializeModellingInstanceEx.html)

        //

        //	This call is deprecated, please use call CalculateInstance().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstanceEx")]

        public static extern int initializeModellingInstanceEx(int model, out int noVertices, out int noIndices, double scale, int instance, int instanceList);



        //

        //		exportModellingAsOWL                        (http://rdf.bg/ifcdoc/CS64/exportModellingAsOWL.html)

        //

        //	This call is deprecated, please contact us if you use this call.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "exportModellingAsOWL")]

        public static extern void exportModellingAsOWL(int model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "exportModellingAsOWL")]

        public static extern void exportModellingAsOWL(int model, byte[] fileName);
    }
}