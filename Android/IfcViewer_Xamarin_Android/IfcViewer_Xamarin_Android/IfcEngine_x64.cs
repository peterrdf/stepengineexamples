using System;
using System.Runtime.InteropServices;
using static Android.Graphics.ColorSpace;
using static Android.Icu.Text.Edits;
using static Android.Renderscripts.Sampler;
using static IfcEngine.x86_64;

namespace IfcEngine
{

    class x64

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

        public static extern long sdaiCreateModelBN(long repository, string fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]

        public static extern long sdaiCreateModelBN(long repository, string fileName, byte[] schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]

        public static extern long sdaiCreateModelBN(long repository, byte[] fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBN")]

        public static extern long sdaiCreateModelBN(long repository, byte[] fileName, byte[] schemaName);



        //

        //		sdaiCreateModelBNUnicode                    (http://rdf.bg/ifcdoc/CS64/sdaiCreateModelBNUnicode.html)

        //

        //	This function creates and empty model (we expect with a schema file given).

        //	Attributes repository and fileName will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]

        public static extern long sdaiCreateModelBNUnicode(long repository, string fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]

        public static extern long sdaiCreateModelBNUnicode(long repository, string fileName, byte[] schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]

        public static extern long sdaiCreateModelBNUnicode(long repository, byte[] fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateModelBNUnicode")]

        public static extern long sdaiCreateModelBNUnicode(long repository, byte[] fileName, byte[] schemaName);



        //

        //		sdaiOpenModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBN.html)

        //

        //	This function opens the model on location fileName.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]

        public static extern long sdaiOpenModelBN(long repository, string fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]

        public static extern long sdaiOpenModelBN(long repository, string fileName, byte[] schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]

        public static extern long sdaiOpenModelBN(long repository, byte[] fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBN")]

        public static extern long sdaiOpenModelBN(long repository, byte[] fileName, byte[] schemaName);



        //

        //		sdaiOpenModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBNUnicode.html)

        //

        //	This function opens the model on location fileName.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]

        public static extern long sdaiOpenModelBNUnicode(long repository, string fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]

        public static extern long sdaiOpenModelBNUnicode(long repository, string fileName, byte[] schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]

        public static extern long sdaiOpenModelBNUnicode(long repository, byte[] fileName, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiOpenModelBNUnicode")]

        public static extern long sdaiOpenModelBNUnicode(long repository, byte[] fileName, byte[] schemaName);



        //

        //		engiOpenModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiOpenModelByStream.html)

        //

        //	This function opens the model via a stream.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByStream")]

        public static extern long engiOpenModelByStream(long repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByStream")]

        public static extern long engiOpenModelByStream(long repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, byte[] schemaName);



        //

        //		engiOpenModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiOpenModelByArray.html)

        //

        //	This function opens the model via an array.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByArray")]

        public static extern long engiOpenModelByArray(long repository, byte[] content, long size, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiOpenModelByArray")]

        public static extern long engiOpenModelByArray(long repository, byte[] content, long size, byte[] schemaName);



        //

        //		sdaiSaveModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBN.html)

        //

        //	This function saves the model (char file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]

        public static extern void sdaiSaveModelBN(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBN")]

        public static extern void sdaiSaveModelBN(long model, byte[] fileName);



        //

        //		sdaiSaveModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBNUnicode.html)

        //

        //	This function saves the model (wchar, i.e. Unicode file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]

        public static extern void sdaiSaveModelBNUnicode(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelBNUnicode")]

        public static extern void sdaiSaveModelBNUnicode(long model, byte[] fileName);



        //

        //		engiSaveModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiSaveModelByStream.html)

        //

        //	This function saves the model as an array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiSaveModelByStream")]

        public static extern void engiSaveModelByStream(long model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, long size);



        //

        //		engiSaveModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiSaveModelByArray.html)

        //

        //	This function saves the model as an array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiSaveModelByArray")]

        public static extern void engiSaveModelByArray(long model, byte[] content, out long size);



        //

        //		sdaiSaveModelAsXmlBN                        (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBN.html)

        //

        //	This function saves the model as XML according to IFC2x3's way of XML serialization (char file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]

        public static extern void sdaiSaveModelAsXmlBN(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBN")]

        public static extern void sdaiSaveModelAsXmlBN(long model, byte[] fileName);



        //

        //		sdaiSaveModelAsXmlBNUnicode                 (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBNUnicode.html)

        //

        //	This function saves the model as XML according to IFC2x3's way of XML serialization (wchar, i.e. Unicode file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]

        public static extern void sdaiSaveModelAsXmlBNUnicode(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsXmlBNUnicode")]

        public static extern void sdaiSaveModelAsXmlBNUnicode(long model, byte[] fileName);



        //

        //		sdaiSaveModelAsSimpleXmlBN                  (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBN.html)

        //

        //	This function saves the model as XML according to IFC4's way of XML serialization (char file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]

        public static extern void sdaiSaveModelAsSimpleXmlBN(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBN")]

        public static extern void sdaiSaveModelAsSimpleXmlBN(long model, byte[] fileName);



        //

        //		sdaiSaveModelAsSimpleXmlBNUnicode           (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBNUnicode.html)

        //

        //	This function saves the model as XML according to IFC4's way of XML serialization (wchar, i.e. Unicode file name).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]

        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiSaveModelAsSimpleXmlBNUnicode")]

        public static extern void sdaiSaveModelAsSimpleXmlBNUnicode(long model, byte[] fileName);



        //

        //		sdaiCloseModel                              (http://rdf.bg/ifcdoc/CS64/sdaiCloseModel.html)

        //

        //	This function closes the model. After this call no instance handles will be available including all

        //	handles referencing the geometry of this specific file, in default compilation the model itself will

        //	be known in the kernel, however known to be disabled. Calls containing the model reference will be

        //	protected from crashing when called.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCloseModel")]

        public static extern void sdaiCloseModel(long model);



        //

        //		setPrecisionDoubleExport                    (http://rdf.bg/ifcdoc/CS64/setPrecisionDoubleExport.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setPrecisionDoubleExport")]

        public static extern void setPrecisionDoubleExport(long model, long precisionCap, long precisionRound, byte clean);



        //

        //  Schema Reading API Calls

        //



        //

        //		sdaiGetEntity                               (http://rdf.bg/ifcdoc/CS64/sdaiGetEntity.html)

        //

        //	This call retrieves a handle to an entity based on a given entity name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]

        public static extern long sdaiGetEntity(long model, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntity")]

        public static extern long sdaiGetEntity(long model, byte[] entityName);



        //

        //		engiGetEntityArgument                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgument.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgument")]

        public static extern long engiGetEntityArgument(long entity, string argumentName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgument")]

        public static extern long engiGetEntityArgument(long entity, byte[] argumentName);



        //

        //		engiGetEntityArgumentIndex                  (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentIndex.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentIndex")]

        public static extern long engiGetEntityArgumentIndex(long entity, string argumentName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentIndex")]

        public static extern long engiGetEntityArgumentIndex(long entity, byte[] argumentName);



        //

        //		engiGetEntityArgumentName                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentName.html)

        //

        //	This call can be used to retrieve the name of the n-th argument of the given entity. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentName")]

        public static extern void engiGetEntityArgumentName(long entity, long index, long valueType, out IntPtr argumentName);



        //

        //		engiGetEntityArgumentType                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentType.html)

        //

        //	This call can be used to retrieve the type of the n-th argument of the given entity. In case of a select argument no relevant information is given by this call as it depends on the instance. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentType")]

        public static extern void engiGetEntityArgumentType(long entity, long index, out long argumentType);



        //

        //		engiGetEntityCount                          (http://rdf.bg/ifcdoc/CS64/engiGetEntityCount.html)

        //

        //	Returns the total number of entities within the loaded schema.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityCount")]

        public static extern long engiGetEntityCount(long model);



        //

        //		engiGetEntityElement                        (http://rdf.bg/ifcdoc/CS64/engiGetEntityElement.html)

        //

        //	This call returns a specific entity based on an index, the index needs to be 0 or higher but lower then the number of entities in the loaded schema.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityElement")]

        public static extern long engiGetEntityElement(long model, long index);



        //

        //		sdaiGetEntityExtent                         (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtent.html)

        //

        //	This call retrieves an aggregation that contains all instances of the entity given.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtent")]

        public static extern long sdaiGetEntityExtent(long model, long entity);



        //

        //		sdaiGetEntityExtentBN                       (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtentBN.html)

        //

        //	This call retrieves an aggregation that contains all instances of the entity given.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]

        public static extern long sdaiGetEntityExtentBN(long model, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetEntityExtentBN")]

        public static extern long sdaiGetEntityExtentBN(long model, byte[] entityName);



        //

        //		engiGetEntityName                           (http://rdf.bg/ifcdoc/CS64/engiGetEntityName.html)

        //

        //	This call can be used to get the name of the given entity.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityName")]

        public static extern void engiGetEntityName(long entity, long valueType, out IntPtr entityName);



        //

        //		engiGetEntityNoArguments                    (http://rdf.bg/ifcdoc/CS64/engiGetEntityNoArguments.html)

        //

        //	This call returns the number of arguments, this includes the arguments of its (nested) parents and inverse argumnets.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityNoArguments")]

        public static extern long engiGetEntityNoArguments(long entity);



        //

        //		engiGetEntityParent                         (http://rdf.bg/ifcdoc/CS64/engiGetEntityParent.html)

        //

        //	Returns the direct parent entity, for example the parent of IfcObject is IfcObjectDefinition, of IfcObjectDefinition is IfcRoot and of IfcRoot is 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityParent")]

        public static extern long engiGetEntityParent(long entity);



        //

        //		engiGetAttrOptional                         (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptional.html)

        //

        //	This call can be used to check if an attribute is optional

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrOptional")]

        public static extern long engiGetAttrOptional(ref long attribute);



        //

        //		engiGetAttrOptionalBN                       (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptionalBN.html)

        //

        //	This call can be used to check if an attribute is optional

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrOptionalBN")]

        public static extern long engiGetAttrOptionalBN(long entity, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrOptionalBN")]

        public static extern long engiGetAttrOptionalBN(long entity, byte[] attributeName);



        //

        //		engiGetAttrInverse                          (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverse.html)

        //

        //	This call can be used to check if an attribute is an inverse relation

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrInverse")]

        public static extern long engiGetAttrInverse(ref long attribute);



        //

        //		engiGetAttrInverseBN                        (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverseBN.html)

        //

        //	This call can be used to check if an attribute is an inverse relation

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrInverseBN")]

        public static extern long engiGetAttrInverseBN(long entity, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrInverseBN")]

        public static extern long engiGetAttrInverseBN(long entity, byte[] attributeName);



        //

        //		engiGetEnumerationValue                     (http://rdf.bg/ifcdoc/CS64/engiGetEnumerationValue.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEnumerationValue")]

        public static extern void engiGetEnumerationValue(long attribute, long index, long valueType, out IntPtr enumerationValue);



        //

        //		engiGetEntityProperty                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityProperty.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityProperty")]

        public static extern void engiGetEntityProperty(long entity, long index, out IntPtr propertyName, out long optional, out long type, out long _array, out long set, out long list, out long bag, out long min, out long max, out long referenceEntity, out long inverse);



        //

        //  Instance Header API Calls

        //



        //

        //		SetSPFFHeader                               (http://rdf.bg/ifcdoc/CS64/SetSPFFHeader.html)

        //

        //	This call is an aggregate of several SetSPFFHeaderItem calls. In several cases the header can be set easily with this call. In case an argument is zero, this argument will not be updated, i.e. it will not be filled with 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]

        public static extern void SetSPFFHeader(long model, string description, string implementationLevel, string name, string timeStamp, string author, string organization, string preprocessorVersion, string originatingSystem, string authorization, string fileSchema);



        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeader")]

        public static extern void SetSPFFHeader(long model, byte[] description, byte[] implementationLevel, byte[] name, byte[] timeStamp, byte[] author, byte[] organization, byte[] preprocessorVersion, byte[] originatingSystem, byte[] authorization, byte[] fileSchema);



        //

        //		SetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/SetSPFFHeaderItem.html)

        //

        //	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]

        public static extern long SetSPFFHeaderItem(long model, long itemIndex, long itemSubIndex, long valueType, string value);



        [DllImport(IFCEngineDLL, EntryPoint = "SetSPFFHeaderItem")]

        public static extern long SetSPFFHeaderItem(long model, long itemIndex, long itemSubIndex, long valueType, byte[] value);



        //

        //		GetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItem.html)

        //

        //	This call can be used to read a specific header item, the source code example is larger to show and explain how this call can be used.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItem")]

        public static extern long GetSPFFHeaderItem(long model, long itemIndex, long itemSubIndex, long valueType, out IntPtr value);



        //

        //		GetSPFFHeaderItemUnicode                    (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItemUnicode.html)

        //

        //	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItemUnicode")]

        public static extern long GetSPFFHeaderItemUnicode(long model, long itemIndex, long itemSubIndex, string buffer, long bufferLength);



        [DllImport(IFCEngineDLL, EntryPoint = "GetSPFFHeaderItemUnicode")]

        public static extern long GetSPFFHeaderItemUnicode(long model, long itemIndex, long itemSubIndex, byte[] buffer, long bufferLength);



        //

        //  Instance Reading API Calls

        //



        //

        //		sdaiGetADBType                              (http://rdf.bg/ifcdoc/CS64/sdaiGetADBType.html)

        //

        //	This call can be used to get the used type within this ADB type.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBType")]

        public static extern long sdaiGetADBType(ref long ADB);



        //

        //		sdaiGetADBTypePath                          (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePath.html)

        //

        //	This call can be used to get the path of an ADB type.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePath")]

        public static extern IntPtr sdaiGetADBTypePath(ref long ADB, long typeNameNumber);



        //

        //		sdaiGetADBTypePathx                         (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePathx.html)

        //

        //	This call is the same as sdaiGetADBTypePath, however can be used by porting to languages that have issues with returned char arrays.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePathx")]

        public static extern void sdaiGetADBTypePathx(ref long ADB, long typeNameNumber, out IntPtr path);



        //

        //		sdaiGetADBValue                             (http://rdf.bg/ifcdoc/CS64/sdaiGetADBValue.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]

        public static extern void sdaiGetADBValue(ref long ADB, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]

        public static extern void sdaiGetADBValue(ref long ADB, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBValue")]

        public static extern void sdaiGetADBValue(ref long ADB, long valueType, out IntPtr value);



        //

        //		engiGetAggrElement                          (http://rdf.bg/ifcdoc/CS64/engiGetAggrElement.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]

        public static extern long engiGetAggrElement(long aggregate, long elementIndex, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]

        public static extern long engiGetAggrElement(long aggregate, long elementIndex, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrElement")]

        public static extern long engiGetAggrElement(long aggregate, long elementIndex, long valueType, out IntPtr value);



        //

        //		engiGetAggrType                             (http://rdf.bg/ifcdoc/CS64/engiGetAggrType.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrType")]

        public static extern void engiGetAggrType(long aggregate, out long aggragateType);



        //

        //		engiGetAggrTypex                            (http://rdf.bg/ifcdoc/CS64/engiGetAggrTypex.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrTypex")]

        public static extern void engiGetAggrTypex(long aggregate, out long aggragateType);



        //

        //		sdaiGetAttr                                 (http://rdf.bg/ifcdoc/CS64/sdaiGetAttr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]

        public static extern long sdaiGetAttr(long instance, long attribute, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]

        public static extern long sdaiGetAttr(long instance, long attribute, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]

        public static extern long sdaiGetAttr(long instance, long attribute, long valueType, out IntPtr value);



        //

        //		sdaiGetAttrBN                               (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern long sdaiGetAttrBN(long instance, string attributeName, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern long sdaiGetAttrBN(long instance, string attributeName, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern long sdaiGetAttrBN(long instance, string attributeName, long valueType, out IntPtr value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern long sdaiGetAttrBN(long instance, byte[] attributeName, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern long sdaiGetAttrBN(long instance, byte[] attributeName, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBN")]

        public static extern long sdaiGetAttrBN(long instance, byte[] attributeName, long valueType, out IntPtr value);



        //

        //		sdaiGetAttrBNUnicode                        (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBNUnicode.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode")]

        public static extern long sdaiGetAttrBNUnicode(long instance, string attributeName, string buffer, long bufferLength);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrBNUnicode")]

        public static extern long sdaiGetAttrBNUnicode(long instance, byte[] attributeName, byte[] buffer, long bufferLength);



        //

        //		sdaiGetStringAttrBN                         (http://rdf.bg/ifcdoc/CS64/sdaiGetStringAttrBN.html)

        //

        //	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiSTRING.

        //	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,

        //	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetStringAttrBN")]

        public static extern IntPtr sdaiGetStringAttrBN(long instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetStringAttrBN")]

        public static extern IntPtr sdaiGetStringAttrBN(long instance, byte[] attributeName);



        //

        //		sdaiGetInstanceAttrBN                       (http://rdf.bg/ifcdoc/CS64/sdaiGetInstanceAttrBN.html)

        //

        //	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiINSTANCE.

        //	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,

        //	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceAttrBN")]

        public static extern long sdaiGetInstanceAttrBN(long instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceAttrBN")]

        public static extern long sdaiGetInstanceAttrBN(long instance, byte[] attributeName);



        //

        //		sdaiGetAggregationAttrBN                    (http://rdf.bg/ifcdoc/CS64/sdaiGetAggregationAttrBN.html)

        //

        //	This function is a specific version of sdaiGetAttrBN(..), where the valueType is sdaiAGGR.

        //	This call can be usefull in case of specific programming languages that cannot map towards sdaiGetAttrBN(..) directly,

        //	this function is useless for languages as C, C++, C#, JAVA, VB.NET, Delphi and similar as they are able to map sdaiGetAttrBN(..) directly.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggregationAttrBN")]

        public static extern long sdaiGetAggregationAttrBN(long instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggregationAttrBN")]

        public static extern long sdaiGetAggregationAttrBN(long instance, byte[] attributeName);



        //

        //		sdaiGetAttrDefinition                       (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrDefinition.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]

        public static extern long sdaiGetAttrDefinition(long entity, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]

        public static extern long sdaiGetAttrDefinition(long entity, byte[] attributeName);



        //

        //		sdaiGetInstanceType                         (http://rdf.bg/ifcdoc/CS64/sdaiGetInstanceType.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetInstanceType")]

        public static extern long sdaiGetInstanceType(long instance);



        //

        //		sdaiGetMemberCount                          (http://rdf.bg/ifcdoc/CS64/sdaiGetMemberCount.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetMemberCount")]

        public static extern long sdaiGetMemberCount(long aggregate);



        //

        //		sdaiIsKindOf                                (http://rdf.bg/ifcdoc/CS64/sdaiIsKindOf.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsKindOf")]

        public static extern long sdaiIsKindOf(long instance, long entity);



        //

        //		engiGetAttrType                             (http://rdf.bg/ifcdoc/CS64/engiGetAttrType.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrType")]

        public static extern long engiGetAttrType(long instance, ref long attribute);



        //

        //		engiGetAttrTypeBN                           (http://rdf.bg/ifcdoc/CS64/engiGetAttrTypeBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrTypeBN")]

        public static extern long engiGetAttrTypeBN(long instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttrTypeBN")]

        public static extern long engiGetAttrTypeBN(long instance, byte[] attributeName);



        //

        //		sdaiIsInstanceOf                            (http://rdf.bg/ifcdoc/CS64/sdaiIsInstanceOf.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsInstanceOf")]

        public static extern long sdaiIsInstanceOf(long instance, long entity);



        //

        //		sdaiIsInstanceOfBN                          (http://rdf.bg/ifcdoc/CS64/sdaiIsInstanceOfBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsInstanceOfBN")]

        public static extern long sdaiIsInstanceOfBN(long instance, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiIsInstanceOfBN")]

        public static extern long sdaiIsInstanceOfBN(long instance, byte[] entityName);



        //

        //		engiValidateAttr                            (http://rdf.bg/ifcdoc/CS64/engiValidateAttr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiValidateAttr")]

        public static extern long engiValidateAttr(long instance, ref long attribute);



        //

        //		engiValidateAttrBN                          (http://rdf.bg/ifcdoc/CS64/engiValidateAttrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiValidateAttrBN")]

        public static extern long engiValidateAttrBN(long instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "engiValidateAttrBN")]

        public static extern long engiValidateAttrBN(long instance, byte[] attributeName);



        //

        //		sdaiCreateInstanceEI                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceEI.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceEI")]

        public static extern long sdaiCreateInstanceEI(long model, long entity, long express_id);



        //

        //		sdaiCreateInstanceBNEI                      (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceBNEI.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBNEI")]

        public static extern long sdaiCreateInstanceBNEI(long model, string entityName, long express_id);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBNEI")]

        public static extern long sdaiCreateInstanceBNEI(long model, byte[] entityName, long express_id);



        //

        //  Instance Writing API Calls

        //



        //

        //		sdaiPrepend                                 (http://rdf.bg/ifcdoc/CS64/sdaiPrepend.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrepend")]

        public static extern void sdaiPrepend(long list, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrepend")]

        public static extern void sdaiPrepend(long list, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrepend")]

        public static extern void sdaiPrepend(long list, long valueType, out IntPtr value);



        //

        //		sdaiAppend                                  (http://rdf.bg/ifcdoc/CS64/sdaiAppend.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]

        public static extern void sdaiAppend(long list, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]

        public static extern void sdaiAppend(long list, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiAppend")]

        public static extern void sdaiAppend(long list, long valueType, out IntPtr value);



        //

        //		engiAppend                                  (http://rdf.bg/ifcdoc/CS64/engiAppend.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiAppend")]

        public static extern void engiAppend(long list, long valueType, out IntPtr values, long card);



        //

        //		sdaiCreateADB                               (http://rdf.bg/ifcdoc/CS64/sdaiCreateADB.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]

        public static extern long sdaiCreateADB(long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]

        public static extern long sdaiCreateADB(long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateADB")]

        public static extern long sdaiCreateADB(long valueType, out IntPtr value);



        //

        //		sdaiCreateAggr                              (http://rdf.bg/ifcdoc/CS64/sdaiCreateAggr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggr")]

        public static extern long sdaiCreateAggr(long instance, ref long attribute);



        //

        //		sdaiCreateAggrBN                            (http://rdf.bg/ifcdoc/CS64/sdaiCreateAggrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]

        public static extern long sdaiCreateAggrBN(long instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggrBN")]

        public static extern long sdaiCreateAggrBN(long instance, byte[] attributeName);



        //

        //		sdaiCreateNestedAggr                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateNestedAggr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateNestedAggr")]

        public static extern long sdaiCreateNestedAggr(out long aggr);



        //

        //		sdaiCreateInstance                          (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstance.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstance")]

        public static extern long sdaiCreateInstance(long model, long entity);



        //

        //		sdaiCreateInstanceBN                        (http://rdf.bg/ifcdoc/CS64/sdaiCreateInstanceBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]

        public static extern long sdaiCreateInstanceBN(long model, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstanceBN")]

        public static extern long sdaiCreateInstanceBN(long model, byte[] entityName);



        //

        //		sdaiDeleteInstance                          (http://rdf.bg/ifcdoc/CS64/sdaiDeleteInstance.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteInstance")]

        public static extern void sdaiDeleteInstance(long instance);



        //

        //		sdaiPutADBTypePath                          (http://rdf.bg/ifcdoc/CS64/sdaiPutADBTypePath.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]

        public static extern void sdaiPutADBTypePath(string ADB, long pathCount, string path);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutADBTypePath")]

        public static extern void sdaiPutADBTypePath(byte[] ADB, long pathCount, byte[] path);



        //

        //		sdaiPutAttr                                 (http://rdf.bg/ifcdoc/CS64/sdaiPutAttr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]

        public static extern void sdaiPutAttr(long instance, ref long attribute, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]

        public static extern void sdaiPutAttr(long instance, ref long attribute, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttr")]

        public static extern void sdaiPutAttr(long instance, ref long attribute, long valueType, out IntPtr value);



        //

        //		sdaiPutAttrBN                               (http://rdf.bg/ifcdoc/CS64/sdaiPutAttrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(long instance, string attributeName, long valueType, long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(long instance, string attributeName, long valueType, ref double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(long instance, string attributeName, long valueType, ref IntPtr value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(long instance, byte[] attributeName, long valueType, long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(long instance, byte[] attributeName, long valueType, ref double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAttrBN")]

        public static extern void sdaiPutAttrBN(long instance, byte[] attributeName, long valueType, ref IntPtr value);



        //

        //		engiSetComment                              (http://rdf.bg/ifcdoc/CS64/engiSetComment.html)

        //

        //	This call can be used to add a comment to an instance when exporting the content. The comment is available in the exported/saved IFC file.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]

        public static extern void engiSetComment(long instance, string comment);



        [DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]

        public static extern void engiSetComment(long instance, byte[] comment);



        //

        //		engiGetInstanceLocalId                      (http://rdf.bg/ifcdoc/CS64/engiGetInstanceLocalId.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceLocalId")]

        public static extern long engiGetInstanceLocalId(long instance);



        //

        //		sdaiTestAttr                                (http://rdf.bg/ifcdoc/CS64/sdaiTestAttr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiTestAttr")]

        public static extern long sdaiTestAttr(long instance, ref long attribute);



        //

        //		sdaiTestAttrBN                              (http://rdf.bg/ifcdoc/CS64/sdaiTestAttrBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiTestAttrBN")]

        public static extern long sdaiTestAttrBN(long instance, string attributeName);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiTestAttrBN")]

        public static extern long sdaiTestAttrBN(long instance, byte[] attributeName);



        //

        //		engiGetInstanceClassInfo                    (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfo.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceClassInfo")]

        public static extern IntPtr engiGetInstanceClassInfo(long instance);



        //

        //		engiGetInstanceClassInfoUC                  (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfoUC.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceClassInfoUC")]

        public static extern IntPtr engiGetInstanceClassInfoUC(long instance);



        //

        //		engiGetInstanceClassInfoEx                  (http://rdf.bg/ifcdoc/CS64/engiGetInstanceClassInfoEx.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceClassInfoEx")]

        public static extern void engiGetInstanceClassInfoEx(long instance, out IntPtr classInfo);



        //

        //		engiGetInstanceMetaInfo                     (http://rdf.bg/ifcdoc/CS64/engiGetInstanceMetaInfo.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetInstanceMetaInfo")]

        public static extern long engiGetInstanceMetaInfo(long instance, out long localId, out IntPtr entityName, out IntPtr entityNameUC);



        //

        //  Controling API Calls

        //



        //

        //		circleSegments                              (http://rdf.bg/ifcdoc/CS64/circleSegments.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "circleSegments")]

        public static extern void circleSegments(long circles, long smallCircles);



        //

        //		setMaximumSegmentationLength                (http://rdf.bg/ifcdoc/CS64/setMaximumSegmentationLength.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setMaximumSegmentationLength")]

        public static extern void setMaximumSegmentationLength(long model, double length);



        //

        //		getUnitConversionFactor                     (http://rdf.bg/ifcdoc/CS64/getUnitConversionFactor.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getUnitConversionFactor")]

        public static extern double getUnitConversionFactor(long model, string unitType, out IntPtr unitPrefix, out IntPtr unitName, out IntPtr SIUnitName);



        [DllImport(IFCEngineDLL, EntryPoint = "getUnitConversionFactor")]

        public static extern double getUnitConversionFactor(long model, byte[] unitType, out IntPtr unitPrefix, out IntPtr unitName, out IntPtr SIUnitName);



        //

        //		setBRepProperties                           (http://rdf.bg/ifcdoc/CS64/setBRepProperties.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setBRepProperties")]

        public static extern void setBRepProperties(long model, long consistencyCheck, double fraction, double epsilon, long maxVerticesSize);



        //

        //		cleanMemory                                 (http://rdf.bg/ifcdoc/CS64/cleanMemory.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "cleanMemory")]

        public static extern void cleanMemory(long model, long mode);



        //

        //		internalGetP21Line                          (http://rdf.bg/ifcdoc/CS64/internalGetP21Line.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetP21Line")]

        public static extern long internalGetP21Line(long instance);



        //

        //		internalGetInstanceFromP21Line              (http://rdf.bg/ifcdoc/CS64/internalGetInstanceFromP21Line.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetInstanceFromP21Line")]

        public static extern long internalGetInstanceFromP21Line(long model, long P21Line);



        //

        //		internalGetXMLID                            (http://rdf.bg/ifcdoc/CS64/internalGetXMLID.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetXMLID")]

        public static extern void internalGetXMLID(long instance, out IntPtr XMLID);



        //

        //		setStringUnicode                            (http://rdf.bg/ifcdoc/CS64/setStringUnicode.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setStringUnicode")]

        public static extern long setStringUnicode(long unicode);



        //

        //		setFilter                                   (http://rdf.bg/ifcdoc/CS64/setFilter.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setFilter")]

        public static extern void setFilter(long model, long setting, long mask);



        //

        //		getFilter                                   (http://rdf.bg/ifcdoc/CS64/getFilter.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getFilter")]

        public static extern long getFilter(long model, long mask);



        //

        //  Uncategorized API Calls

        //



        //

        //		xxxxGetEntityAndSubTypesExtent              (http://rdf.bg/ifcdoc/CS64/xxxxGetEntityAndSubTypesExtent.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtent")]

        public static extern long xxxxGetEntityAndSubTypesExtent(long model, long entity);



        //

        //		xxxxGetEntityAndSubTypesExtentBN            (http://rdf.bg/ifcdoc/CS64/xxxxGetEntityAndSubTypesExtentBN.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtentBN")]

        public static extern long xxxxGetEntityAndSubTypesExtentBN(long model, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetEntityAndSubTypesExtentBN")]

        public static extern long xxxxGetEntityAndSubTypesExtentBN(long model, byte[] entityName);



        //

        //		xxxxGetInstancesUsing                       (http://rdf.bg/ifcdoc/CS64/xxxxGetInstancesUsing.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetInstancesUsing")]

        public static extern long xxxxGetInstancesUsing(long instance);



        //

        //		xxxxDeleteFromAggregation                   (http://rdf.bg/ifcdoc/CS64/xxxxDeleteFromAggregation.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxDeleteFromAggregation")]

        public static extern long xxxxDeleteFromAggregation(long instance, out long aggregate, long elementIndex);



        //

        //		xxxxGetAttrDefinitionByValue                (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrDefinitionByValue.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]

        public static extern long xxxxGetAttrDefinitionByValue(long instance, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]

        public static extern long xxxxGetAttrDefinitionByValue(long instance, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrDefinitionByValue")]

        public static extern long xxxxGetAttrDefinitionByValue(long instance, out IntPtr value);



        //

        //		xxxxGetAttrNameByIndex                      (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrNameByIndex.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrNameByIndex")]

        public static extern void xxxxGetAttrNameByIndex(long instance, long index, out IntPtr name);



        //

        //		iterateOverInstances                        (http://rdf.bg/ifcdoc/CS64/iterateOverInstances.html)

        //

        //	This function interates over all available instances loaded in memory, it is the fastest way to find all instances.

        //	Argument entity and entityName are both optional and if non-zero are filled with respectively the entity handle and entity name as char array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "iterateOverInstances")]

        public static extern long iterateOverInstances(long model, long instance, out long entity, string entityName);



        [DllImport(IFCEngineDLL, EntryPoint = "iterateOverInstances")]

        public static extern long iterateOverInstances(long model, long instance, out long entity, byte[] entityName);



        //

        //		iterateOverProperties                       (http://rdf.bg/ifcdoc/CS64/iterateOverProperties.html)

        //

        //	This function iterated over all available attributes of a specific given entity.

        //	This call is typically used in combination with iterateOverInstances(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "iterateOverProperties")]

        public static extern long iterateOverProperties(long entity, long index);



        //

        //		sdaiGetAggrByIterator                       (http://rdf.bg/ifcdoc/CS64/sdaiGetAggrByIterator.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]

        public static extern long sdaiGetAggrByIterator(long iterator, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]

        public static extern long sdaiGetAggrByIterator(long iterator, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAggrByIterator")]

        public static extern long sdaiGetAggrByIterator(long iterator, long valueType, out IntPtr value);



        //

        //		sdaiPutAggrByIterator                       (http://rdf.bg/ifcdoc/CS64/sdaiPutAggrByIterator.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]

        public static extern void sdaiPutAggrByIterator(long iterator, long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]

        public static extern void sdaiPutAggrByIterator(long iterator, long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPutAggrByIterator")]

        public static extern void sdaiPutAggrByIterator(long iterator, long valueType, out IntPtr value);



        //

        //		internalSetLink                             (http://rdf.bg/ifcdoc/CS64/internalSetLink.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalSetLink")]

        public static extern void internalSetLink(long instance, string attributeName, long linked_id);



        [DllImport(IFCEngineDLL, EntryPoint = "internalSetLink")]

        public static extern void internalSetLink(long instance, byte[] attributeName, long linked_id);



        //

        //		internalAddAggrLink                         (http://rdf.bg/ifcdoc/CS64/internalAddAggrLink.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalAddAggrLink")]

        public static extern void internalAddAggrLink(long list, long linked_id);



        //

        //		engiGetNotReferedAggr                       (http://rdf.bg/ifcdoc/CS64/engiGetNotReferedAggr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetNotReferedAggr")]

        public static extern void engiGetNotReferedAggr(long model, out long value);



        //

        //		engiGetAttributeAggr                        (http://rdf.bg/ifcdoc/CS64/engiGetAttributeAggr.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAttributeAggr")]

        public static extern void engiGetAttributeAggr(long instance, out long value);



        //

        //		engiGetAggrUnknownElement                   (http://rdf.bg/ifcdoc/CS64/engiGetAggrUnknownElement.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(long aggregate, long elementIndex, out long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(long aggregate, long elementIndex, out long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(long aggregate, long elementIndex, out long valueType, out IntPtr value);



        //

        //		sdaiErrorQuery                              (http://rdf.bg/ifcdoc/CS64/sdaiErrorQuery.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiErrorQuery")]

        public static extern long sdaiErrorQuery();



        //

        //  Geometry Kernel related API Calls

        //



        //

        //		owlGetModel                                 (http://rdf.bg/ifcdoc/CS64/owlGetModel.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlGetModel")]

        public static extern void owlGetModel(long model, out long owlModel);



        //

        //		owlGetInstance                              (http://rdf.bg/ifcdoc/CS64/owlGetInstance.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlGetInstance")]

        public static extern void owlGetInstance(long model, long instance, out long owlInstance);



        //

        //		owlBuildInstance                            (http://rdf.bg/ifcdoc/CS64/owlBuildInstance.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlBuildInstance")]

        public static extern void owlBuildInstance(long model, long instance, out long owlInstance);



        //

        //		owlBuildInstances                           (http://rdf.bg/ifcdoc/CS64/owlBuildInstances.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlBuildInstances")]

        public static extern void owlBuildInstances(long model, long instance, out long owlInstanceComplete, out long owlInstanceSolids, out long owlInstanceVoids);



        //

        //		owlGetMappedItem                            (http://rdf.bg/ifcdoc/CS64/owlGetMappedItem.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "owlGetMappedItem")]

        public static extern void owlGetMappedItem(long model, long instance, out long owlInstance, out double transformationMatrix);



        //

        //		getInstanceDerivedPropertiesInModelling     (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedPropertiesInModelling.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceDerivedPropertiesInModelling")]

        public static extern long getInstanceDerivedPropertiesInModelling(long model, long instance, out double height, out double width, out double thickness);



        //

        //		getInstanceDerivedBoundingBox               (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedBoundingBox.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceDerivedBoundingBox")]

        public static extern long getInstanceDerivedBoundingBox(long model, long instance, out double Ox, out double Oy, out double Oz, out double Vx, out double Vy, out double Vz);



        //

        //		getInstanceTransformationMatrix             (http://rdf.bg/ifcdoc/CS64/getInstanceTransformationMatrix.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceTransformationMatrix")]

        public static extern long getInstanceTransformationMatrix(long model, long instance, out double _11, out double _12, out double _13, out double _14, out double _21, out double _22, out double _23, out double _24, out double _31, out double _32, out double _33, out double _34, out double _41, out double _42, out double _43, out double _44);



        //

        //		getInstanceDerivedTransformationMatrix      (http://rdf.bg/ifcdoc/CS64/getInstanceDerivedTransformationMatrix.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceDerivedTransformationMatrix")]

        public static extern long getInstanceDerivedTransformationMatrix(long model, long instance, out double _11, out double _12, out double _13, out double _14, out double _21, out double _22, out double _23, out double _24, out double _31, out double _32, out double _33, out double _34, out double _41, out double _42, out double _43, out double _44);



        //

        //		internalGetBoundingBox                      (http://rdf.bg/ifcdoc/CS64/internalGetBoundingBox.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetBoundingBox")]

        public static extern long internalGetBoundingBox(long model, long instance);



        //

        //		internalGetCenter                           (http://rdf.bg/ifcdoc/CS64/internalGetCenter.html)

        //

        //	...

        //

        [DllImport(IFCEngineDLL, EntryPoint = "internalGetCenter")]

        public static extern long internalGetCenter(long model, long instance);



        //

        //  Deprecated API Calls (GENERIC)

        //



        //

        //		engiAttrIsInverse                           (http://rdf.bg/ifcdoc/CS64/engiAttrIsInverse.html)

        //

        //	This call is deprecated, please use call engiAttrIsInverse.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "engiAttrIsInverse")]

        public static extern long engiAttrIsInverse(ref long attribute);



        //

        //		xxxxOpenModelByStream                       (http://rdf.bg/ifcdoc/CS64/xxxxOpenModelByStream.html)

        //

        //	This call is deprecated, please use call engiOpenModelByStream.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxOpenModelByStream")]

        public static extern long xxxxOpenModelByStream(long repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, string schemaName);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxOpenModelByStream")]

        public static extern long xxxxOpenModelByStream(long repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, byte[] schemaName);



        //

        //		sdaiCreateIterator                          (http://rdf.bg/ifcdoc/CS64/sdaiCreateIterator.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateIterator")]

        public static extern long sdaiCreateIterator(ref long aggregate);



        //

        //		sdaiDeleteIterator                          (http://rdf.bg/ifcdoc/CS64/sdaiDeleteIterator.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteIterator")]

        public static extern void sdaiDeleteIterator(long iterator);



        //

        //		sdaiBeginning                               (http://rdf.bg/ifcdoc/CS64/sdaiBeginning.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiBeginning")]

        public static extern void sdaiBeginning(long iterator);



        //

        //		sdaiNext                                    (http://rdf.bg/ifcdoc/CS64/sdaiNext.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiNext")]

        public static extern long sdaiNext(long iterator);



        //

        //		sdaiPrevious                                (http://rdf.bg/ifcdoc/CS64/sdaiPrevious.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiPrevious")]

        public static extern long sdaiPrevious(long iterator);



        //

        //		sdaiEnd                                     (http://rdf.bg/ifcdoc/CS64/sdaiEnd.html)

        //

        //	This call is deprecated, please use calls sdaiGetMemberCount(..) and engiGetEntityElement(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiEnd")]

        public static extern void sdaiEnd(long iterator);



        //

        //		sdaiplusGetAggregationType                  (http://rdf.bg/ifcdoc/CS64/sdaiplusGetAggregationType.html)

        //

        //	This call is deprecated, please use call ....

        //

        [DllImport(IFCEngineDLL, EntryPoint = "sdaiplusGetAggregationType")]

        public static extern long sdaiplusGetAggregationType(long instance, out long aggregation);



        //

        //		xxxxGetAttrTypeBN                           (http://rdf.bg/ifcdoc/CS64/xxxxGetAttrTypeBN.html)

        //

        //	This call is deprecated, please use calls engiGetAttrTypeBN(..).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrTypeBN")]

        public static extern long xxxxGetAttrTypeBN(long instance, string attributeName, out IntPtr attributeType);



        [DllImport(IFCEngineDLL, EntryPoint = "xxxxGetAttrTypeBN")]

        public static extern long xxxxGetAttrTypeBN(long instance, byte[] attributeName, out IntPtr attributeType);



        //

        //  Deprecated API Calls (GEOMETRY)

        //



        //

        //		initializeModellingInstance                 (http://rdf.bg/ifcdoc/CS64/initializeModellingInstance.html)

        //

        //	This call is deprecated, please use call CalculateInstance().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstance")]

        public static extern long initializeModellingInstance(long model, out long noVertices, out long noIndices, double scale, long instance);



        //

        //		finalizeModelling                           (http://rdf.bg/ifcdoc/CS64/finalizeModelling.html)

        //

        //	This call is deprecated, please use call UpdateInstanceVertexBuffer() and UpdateInstanceIndexBuffer().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]

        public static extern long finalizeModelling(long model, float[] vertices, out long indices, long FVF);



        //

        //		getInstanceInModelling                      (http://rdf.bg/ifcdoc/CS64/getInstanceInModelling.html)

        //

        //	This call is deprecated, there is no direct / easy replacement although the functionality is present. If you still use this call please contact RDF to find a solution together.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getInstanceInModelling")]

        public static extern long getInstanceInModelling(long model, long instance, long mode, out long startVertex, out long startIndex, out long primitiveCount);



        //

        //		setVertexOffset                             (http://rdf.bg/ifcdoc/CS64/setVertexOffset.html)

        //

        //	This call is deprecated, please use call SetVertexBufferOffset().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setVertexOffset")]

        public static extern void setVertexOffset(long model, double x, double y, double z);



        //

        //		setFormat                                   (http://rdf.bg/ifcdoc/CS64/setFormat.html)

        //

        //	This call is deprecated, please use call SetFormat().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "setFormat")]

        public static extern void setFormat(long model, long setting, long mask);



        //

        //		getConceptualFaceCnt                        (http://rdf.bg/ifcdoc/CS64/getConceptualFaceCnt.html)

        //

        //	This call is deprecated, please use call GetConceptualFaceCnt().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceCnt")]

        public static extern long getConceptualFaceCnt(long instance);



        //

        //		getConceptualFaceEx                         (http://rdf.bg/ifcdoc/CS64/getConceptualFaceEx.html)

        //

        //	This call is deprecated, please use call GetConceptualFaceEx().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "getConceptualFaceEx")]

        public static extern long getConceptualFaceEx(long instance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacesPolygons, out long noIndicesFacesPolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        //

        //		createGeometryConversion                    (http://rdf.bg/ifcdoc/CS64/createGeometryConversion.html)

        //

        //	This call is deprecated, please use call ... .

        //

        [DllImport(IFCEngineDLL, EntryPoint = "createGeometryConversion")]

        public static extern void createGeometryConversion(long instance, out long owlInstance);



        //

        //		convertInstance                             (http://rdf.bg/ifcdoc/CS64/convertInstance.html)

        //

        //	This call is deprecated, please use call ... .

        //

        [DllImport(IFCEngineDLL, EntryPoint = "convertInstance")]

        public static extern void convertInstance(long instance);



        //

        //		initializeModellingInstanceEx               (http://rdf.bg/ifcdoc/CS64/initializeModellingInstanceEx.html)

        //

        //	This call is deprecated, please use call CalculateInstance().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstanceEx")]

        public static extern long initializeModellingInstanceEx(long model, out long noVertices, out long noIndices, double scale, long instance, long instanceList);



        //

        //		exportModellingAsOWL                        (http://rdf.bg/ifcdoc/CS64/exportModellingAsOWL.html)

        //

        //	This call is deprecated, please contact us if you use this call.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "exportModellingAsOWL")]

        public static extern void exportModellingAsOWL(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "exportModellingAsOWL")]

        public static extern void exportModellingAsOWL(long model, byte[] fileName);
    }
}