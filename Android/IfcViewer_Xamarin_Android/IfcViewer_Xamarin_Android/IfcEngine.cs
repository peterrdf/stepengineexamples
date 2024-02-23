using Java.Util.Functions;
using System;
using System.Runtime.InteropServices;using static Android.Renderscripts.Sampler;
using static IfcEngine.x86_64;
namespace IfcEngine
{
    class x86_64
    {

        #region Constants

        public const long flagbit0 = 1;           // 2^^0    0000.0000..0000.0001
        public const long flagbit1 = 2;           // 2^^1    0000.0000..0000.0010
        public const long flagbit2 = 4;           // 2^^2    0000.0000..0000.0100
        public const long flagbit3 = 8;           // 2^^3    0000.0000..0000.1000
        public const long flagbit4 = 16;          // 2^^4    0000.0000..0001.0000
        public const long flagbit5 = 32;          // 2^^5    0000.0000..0010.0000
        public const long flagbit6 = 64;          // 2^^6    0000.0000..0100.0000
        public const long flagbit7 = 128;         // 2^^7    0000.0000..1000.0000
        public const long flagbit8 = 256;         // 2^^8    0000.0001..0000.0000
        public const long flagbit9 = 512;         // 2^^9    0000.0010..0000.0000
        public const long flagbit10 = 1024;       // 2^^10   0000.0100..0000.0000
        public const long flagbit11 = 2048;       // 2^^11   0000.1000..0000.0000
        public const long flagbit12 = 4096;       // 2^^12   0001.0000..0000.0000
        public const long flagbit13 = 8192;       // 2^^13   0010.0000..0000.0000
        public const long flagbit14 = 16384;      // 2^^14   0100.0000..0000.0000
        public const long flagbit15 = 32768;      // 2^^15   1000.0000..0000.0000

        public const long flagbit24 = 16777216;

        public const long flagbit25 = 33554432;

        public const long flagbit26 = 67108864;

        public const long flagbit27 = 134217728;



        public const long sdaiADB = 1;
        public const long sdaiAGGR = sdaiADB + 1;
        public const long sdaiBINARY = sdaiAGGR + 1;
        public const long sdaiBOOLEAN = sdaiBINARY + 1;
        public const long sdaiENUM = sdaiBOOLEAN + 1;
        public const long sdaiINSTANCE = sdaiENUM + 1;
        public const long sdaiINTEGER = sdaiINSTANCE + 1;
        public const long sdaiLOGICAL = sdaiINTEGER + 1;
        public const long sdaiREAL = sdaiLOGICAL + 1;
        public const long sdaiSTRING = sdaiREAL + 1;
        public const long sdaiUNICODE = sdaiSTRING + 1;
        public const long sdaiEXPRESSSTRING = sdaiUNICODE + 1;
        public const long engiGLOBALID = sdaiEXPRESSSTRING + 1;

        public const long OBJECTPROPERTY_TYPE = 1;
        public const long DATATYPEPROPERTY_TYPE_BOOLEAN = 2;
        public const long DATATYPEPROPERTY_TYPE_CHAR = 3;
        public const long DATATYPEPROPERTY_TYPE_INTEGER = 4;
        public const long DATATYPEPROPERTY_TYPE_DOUBLE = 5;

        public const string IFCEngineDLL = @"libifcengine.so";



        #endregion // Constants

        //

        //  Architecture

        //

        private static bool _x86 = IntPtr.Size == 4;
        //
        //  File IO API Calls
        //

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate long ReadCallBackFunction(IntPtr value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void WriteCallBackFunction(IntPtr value, long size);



        //

        //		sdaiCreateModelBN                           (http://rdf.bg/ifcdoc/CS64/sdaiCreateModelBN.html)

        //

        //	This function creates and empty model (we expect with a schema file given).

        //	Attributes repository and fileName will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        public static long sdaiCreateModelBN(long repository, string fileName, string schemaName)
        {
            if (_x86)
            {
                return x86.sdaiCreateModelBN((int)repository, fileName, schemaName);
            }

            return x64.sdaiCreateModelBN(repository, fileName, schemaName);
        }

        public static long sdaiCreateModelBN(long repository, string fileName, byte[] schemaName)
        {
            if (_x86)
            {
                return x86.sdaiCreateModelBN((int)repository, fileName, schemaName);
            }

            return x64.sdaiCreateModelBN(repository, fileName, schemaName);
        }

        public static long sdaiCreateModelBN(long repository, byte[] fileName, string schemaName)
        {
            if (_x86)
            {
                return x86.sdaiCreateModelBN((int)repository, fileName, schemaName);
            }

            return x64.sdaiCreateModelBN(repository, fileName, schemaName);
        }

        public static long sdaiCreateModelBN(long repository, byte[] fileName, byte[] schemaName)
        {
            if (_x86)
            {
                return x86.sdaiCreateModelBN((int)repository, fileName, schemaName);
            }

            return x64.sdaiCreateModelBN(repository, fileName, schemaName);
        }



        //

        //		sdaiCreateModelBNUnicode                    (http://rdf.bg/ifcdoc/CS64/sdaiCreateModelBNUnicode.html)

        //

        //	This function creates and empty model (we expect with a schema file given).

        //	Attributes repository and fileName will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        public static long sdaiCreateModelBNUnicode(long repository, string fileName, string schemaName)
        {
            if (_x86)
            {
                x86.sdaiCreateModelBNUnicode((int)repository, fileName, schemaName);
            }

            return x64.sdaiCreateModelBNUnicode(repository, fileName, schemaName);
        }

        public static long sdaiCreateModelBNUnicode(long repository, string fileName, byte[] schemaName)
        {
            if (_x86)
            {
                x86.sdaiCreateModelBNUnicode((int)repository, fileName, schemaName);
            }

            return x64.sdaiCreateModelBNUnicode(repository, fileName, schemaName);
        }

        public static long sdaiCreateModelBNUnicode(long repository, byte[] fileName, string schemaName)
        {
            if (_x86)
            {
                x86.sdaiCreateModelBNUnicode((int)repository, fileName, schemaName);
            }

            return x64.sdaiCreateModelBNUnicode(repository, fileName, schemaName);
        }

        public static long sdaiCreateModelBNUnicode(long repository, byte[] fileName, byte[] schemaName)
        {
            if (_x86)
            {
                x86.sdaiCreateModelBNUnicode((int)repository, fileName, schemaName);
            }

            return x64.sdaiCreateModelBNUnicode(repository, fileName, schemaName);
        }



        //

        //		sdaiOpenModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBN.html)

        //

        //	This function opens the model on location fileName.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        public static long sdaiOpenModelBN(long repository, string fileName, string schemaName)
        {
            if (_x86)
            {
                return x86.sdaiOpenModelBN((int)repository, fileName, schemaName);
            }

            return x64.sdaiOpenModelBN(repository, fileName, schemaName);
        }

        public static long sdaiOpenModelBN(long repository, string fileName, byte[] schemaName)
        {
            if (_x86)
            {
                return x86.sdaiOpenModelBN((int)repository, fileName, schemaName);
            }

            return x64.sdaiOpenModelBN(repository, fileName, schemaName);
        }

        public static long sdaiOpenModelBN(long repository, byte[] fileName, string schemaName)
        {
            if (_x86)
            {
                return x86.sdaiOpenModelBN((int)repository, fileName, schemaName);
            }

            return x64.sdaiOpenModelBN(repository, fileName, schemaName);
        }

        public static long sdaiOpenModelBN(long repository, byte[] fileName, byte[] schemaName)
        {
            if (_x86)
            {
                return x86.sdaiOpenModelBN((int)repository, fileName, schemaName);
            }

            return x64.sdaiOpenModelBN(repository, fileName, schemaName);
        }



        //

        //		sdaiOpenModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiOpenModelBNUnicode.html)

        //

        //	This function opens the model on location fileName.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        public static long sdaiOpenModelBNUnicode(long repository, string fileName, string schemaName)
        {
            if (_x86)
            {
                x86.sdaiOpenModelBNUnicode((int)repository, fileName, schemaName);
            }

            return x64.sdaiOpenModelBNUnicode(repository, fileName, schemaName);
        }

        public static long sdaiOpenModelBNUnicode(long repository, string fileName, byte[] schemaName)
        {
            if (_x86)
            {
                x86.sdaiOpenModelBNUnicode((int)repository, fileName, schemaName);
            }

            return x64.sdaiOpenModelBNUnicode(repository, fileName, schemaName);
        }

        public static long sdaiOpenModelBNUnicode(long repository, byte[] fileName, string schemaName)
        {
            if (_x86)
            {
                x86.sdaiOpenModelBNUnicode((int)repository, fileName, schemaName);
            }

            return x64.sdaiOpenModelBNUnicode(repository, fileName, schemaName);
        }

        public static long sdaiOpenModelBNUnicode(long repository, byte[] fileName, byte[] schemaName)
        {
            if (_x86)
            {
                x86.sdaiOpenModelBNUnicode((int)repository, fileName, schemaName);
            }

            return x64.sdaiOpenModelBNUnicode(repository, fileName, schemaName);
        }



        //

        //		engiOpenModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiOpenModelByStream.html)

        //

        //	This function opens the model via a stream.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        public static long engiOpenModelByStream(long repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, string schemaName)        {            if (_x86)
            {
                return x86.engiOpenModelByStream((int)repository, callback, schemaName);
            }

            return x64.engiOpenModelByStream(repository, callback, schemaName);
        }



        public static long engiOpenModelByStream(long repository, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, byte[] schemaName)
        {            if (_x86)
            {
                return x86.engiOpenModelByStream((int)repository, callback, schemaName);
            }

            return x64.engiOpenModelByStream(repository, callback, schemaName);
        }



        //

        //		engiOpenModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiOpenModelByArray.html)

        //

        //	This function opens the model via an array.

        //	Attribute repository will be ignored, they are their because of backward compatibility.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        public static long engiOpenModelByArray(long repository, byte[] content, long size, string schemaName)
        {
            if (_x86)
            {
                x86.engiOpenModelByArray((int)repository, content, (int)size, schemaName);
            }

            return x64.engiOpenModelByArray(repository, content, size, schemaName);
        }



        public static long engiOpenModelByArray(long repository, byte[] content, long size, byte[] schemaName)
        {
            if (_x86)
            {
                x86.engiOpenModelByArray((int)repository, content, (int)size, schemaName);
            }

            return x64.engiOpenModelByArray(repository, content, size, schemaName);
        }



        //

        //		sdaiSaveModelBN                             (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBN.html)

        //

        //	This function saves the model (char file name).

        //

        public static void sdaiSaveModelBN(long model, string fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelBN((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelBN(model, fileName);
            }
        }



        public static void sdaiSaveModelBN(long model, byte[] fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelBN((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelBN(model, fileName);
            }
        }



        //

        //		sdaiSaveModelBNUnicode                      (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelBNUnicode.html)

        //

        //	This function saves the model (wchar, i.e. Unicode file name).

        //

        public static void sdaiSaveModelBNUnicode(long model, string fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelBNUnicode((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelBNUnicode(model, fileName);
            }
        }



        public static void sdaiSaveModelBNUnicode(long model, byte[] fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelBNUnicode((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelBNUnicode(model, fileName);
            }
        }



        //

        //		engiSaveModelByStream                       (http://rdf.bg/ifcdoc/CS64/engiSaveModelByStream.html)

        //

        //	This function saves the model as an array.

        //

        public static void engiSaveModelByStream(long model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, long size)
        {
            if (_x86)
            {
                x86.engiSaveModelByStream((int)model, callback, size);
            }
            else
            {
                x64.engiSaveModelByStream(model, callback, size);
            }
        }



        //

        //		engiSaveModelByArray                        (http://rdf.bg/ifcdoc/CS64/engiSaveModelByArray.html)

        //

        //	This function saves the model as an array.

        //

        public static void engiSaveModelByArray(long model, byte[] content, out long size)
        {
            if (_x86)
            {
                x86.engiSaveModelByArray((int)model, content, out int iSize);

                size = iSize;
            }
            else
            {
                x64.engiSaveModelByArray(model, content, out size);
            }
        }



        //

        //		sdaiSaveModelAsXmlBN                        (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBN.html)

        //

        //	This function saves the model as XML according to IFC2x3's way of XML serialization (char file name).

        //

        public static void sdaiSaveModelAsXmlBN(long model, string fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelAsXmlBN((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelAsXmlBN(model, fileName);
            }
        }



        public static void sdaiSaveModelAsXmlBN(long model, byte[] fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelAsXmlBN((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelAsXmlBN(model, fileName);
            }
        }



        //

        //		sdaiSaveModelAsXmlBNUnicode                 (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsXmlBNUnicode.html)

        //

        //	This function saves the model as XML according to IFC2x3's way of XML serialization (wchar, i.e. Unicode file name).

        //

        public static void sdaiSaveModelAsXmlBNUnicode(long model, string fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelAsXmlBNUnicode((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelAsXmlBNUnicode(model, fileName);
            }
        }



        public static void sdaiSaveModelAsXmlBNUnicode(long model, byte[] fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelAsXmlBNUnicode((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelAsXmlBNUnicode(model, fileName);
            }
        }



        //

        //		sdaiSaveModelAsSimpleXmlBN                  (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBN.html)

        //

        //	This function saves the model as XML according to IFC4's way of XML serialization (char file name).

        //

        public static void sdaiSaveModelAsSimpleXmlBN(long model, string fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelAsSimpleXmlBN((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelAsSimpleXmlBN(model, fileName);
            }
        }



        public static void sdaiSaveModelAsSimpleXmlBN(long model, byte[] fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelAsSimpleXmlBN((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelAsSimpleXmlBN(model, fileName);
            }
        }



        //

        //		sdaiSaveModelAsSimpleXmlBNUnicode           (http://rdf.bg/ifcdoc/CS64/sdaiSaveModelAsSimpleXmlBNUnicode.html)

        //

        //	This function saves the model as XML according to IFC4's way of XML serialization (wchar, i.e. Unicode file name).

        //

        public static void sdaiSaveModelAsSimpleXmlBNUnicode(long model, string fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelAsSimpleXmlBNUnicode((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelAsSimpleXmlBNUnicode(model, fileName);
            }
        }



        public static void sdaiSaveModelAsSimpleXmlBNUnicode(long model, byte[] fileName)
        {
            if (_x86)
            {
                x86.sdaiSaveModelAsSimpleXmlBNUnicode((int)model, fileName);
            }
            else
            {
                x64.sdaiSaveModelAsSimpleXmlBNUnicode(model, fileName);
            }
        }



        //

        //		sdaiCloseModel                              (http://rdf.bg/ifcdoc/CS64/sdaiCloseModel.html)

        //

        //	This function closes the model. After this call no instance handles will be available including all

        //	handles referencing the geometry of this specific file, in default compilation the model itself will

        //	be known in the kernel, however known to be disabled. Calls containing the model reference will be

        //	protected from crashing when called.

        //

        public static void sdaiCloseModel(long model)
        {
            if (_x86)
            {
                x86.sdaiCloseModel((int)model);
            }
            else
            {
                x64.sdaiCloseModel(model);
            }
        }



        //

        //		setPrecisionDoubleExport                    (http://rdf.bg/ifcdoc/CS64/setPrecisionDoubleExport.html)

        //

        //	...

        //

        public static void setPrecisionDoubleExport(long model, long precisionCap, long precisionRound, byte clean)
        {
            if (_x86)
            {
                x86.setPrecisionDoubleExport((int)model, (int)precisionCap, (int)precisionRound, clean);
            }
            else
            {
                x64.setPrecisionDoubleExport(model, precisionCap, precisionRound, clean);
            }
        }



        //

        //  Schema Reading API Calls

        //



        //

        //		sdaiGetEntity                               (http://rdf.bg/ifcdoc/CS64/sdaiGetEntity.html)

        //

        //	This call retrieves a handle to an entity based on a given entity name.

        //

        public static long sdaiGetEntity(long model, string entityName)
        {
            if (_x86)
            {
                return x86.sdaiGetEntity((int)model, entityName);
            }

            return x64.sdaiGetEntity(model, entityName);
        }



        public static long sdaiGetEntity(long model, byte[] entityName)
        {
            if (_x86)
            {
                return x86.sdaiGetEntity((int)model, entityName);
            }

            return x64.sdaiGetEntity(model, entityName);
        }



        //

        //		engiGetEntityArgument                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgument.html)

        //

        //	...

        //

        public static long engiGetEntityArgument(long entity, string argumentName)
        {
            if (_x86)
            {
                return x86.engiGetEntityArgument((int)entity, argumentName);
            }

            return x64.engiGetEntityArgument(entity, argumentName);
        }



        public static long engiGetEntityArgument(long entity, byte[] argumentName)
        {
            if (_x86)
            {
                return x86.engiGetEntityArgument((int)entity, argumentName);
            }

            return x64.engiGetEntityArgument(entity, argumentName);
        }



        //

        //		engiGetEntityArgumentIndex                  (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentIndex.html)

        //

        //	...

        //

        public static long engiGetEntityArgumentIndex(long entity, string argumentName)
        {
            if (_x86)
            {
                return x86.engiGetEntityArgumentIndex((int)entity, argumentName);
            }

            return x64.engiGetEntityArgumentIndex(entity, argumentName);
        }



        public static long engiGetEntityArgumentIndex(long entity, byte[] argumentName)
        {
            if (_x86)
            {
                return x86.engiGetEntityArgumentIndex((int)entity, argumentName);
            }

            return x64.engiGetEntityArgumentIndex(entity, argumentName);
        }



        //

        //		engiGetEntityArgumentName                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentName.html)

        //

        //	This call can be used to retrieve the name of the n-th argument of the given entity. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.

        //

        public static void engiGetEntityArgumentName(long entity, long index, long valueType, out IntPtr argumentName)
        {
            if (_x86)
            {
                x86.engiGetEntityArgumentName((int)entity, (int)index, (int)valueType, out argumentName);
            }
            else
            {
                x64.engiGetEntityArgumentName(entity, index, valueType, out argumentName);
            }
        }



        //

        //		engiGetEntityArgumentType                   (http://rdf.bg/ifcdoc/CS64/engiGetEntityArgumentType.html)

        //

        //	This call can be used to retrieve the type of the n-th argument of the given entity. In case of a select argument no relevant information is given by this call as it depends on the instance. Arguments of parent entities are included in the index. Both direct and inverse arguments are included.

        //

        public static void engiGetEntityArgumentType(long entity, long index, out long argumentType)
        {
            if (_x86)
            {
                x86.engiGetEntityArgumentType((int)entity, (int)index, out int iArgumentType);

                argumentType = iArgumentType;
            }
            else
            {
                x64.engiGetEntityArgumentType(entity, index, out argumentType);
            }
        }



        //

        //		engiGetEntityCount                          (http://rdf.bg/ifcdoc/CS64/engiGetEntityCount.html)

        //

        //	Returns the total number of entities within the loaded schema.

        //

        public static long engiGetEntityCount(long model)
        {
            if (_x86)
            {
                return x86.engiGetEntityCount((int)model);
            }

            return x64.engiGetEntityCount(model);
        }



        //

        //		engiGetEntityElement                        (http://rdf.bg/ifcdoc/CS64/engiGetEntityElement.html)

        //

        //	This call returns a specific entity based on an index, the index needs to be 0 or higher but lower then the number of entities in the loaded schema.

        //

        public static long engiGetEntityElement(long model, long index)
        {
            if (_x86)
            {
                return x86.engiGetEntityElement((int)model, (int)index);
            }

            return x64.engiGetEntityElement(model, index);
        }



        //

        //		sdaiGetEntityExtent                         (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtent.html)

        //

        //	This call retrieves an aggregation that contains all instances of the entity given.

        //

        public static long sdaiGetEntityExtent(long model, long entity)
        {
            if (_x86)
            {
                return x86.sdaiGetEntityExtent((int)model, (int)entity);
            }

            return x64.sdaiGetEntityExtent(model, entity);
        }



        //

        //		sdaiGetEntityExtentBN                       (http://rdf.bg/ifcdoc/CS64/sdaiGetEntityExtentBN.html)

        //

        //	This call retrieves an aggregation that contains all instances of the entity given.

        //

        public static long sdaiGetEntityExtentBN(long model, string entityName)
        {
            if (_x86)
            {
                return x86.sdaiGetEntityExtentBN((int)model, entityName);
            }

            return x64.sdaiGetEntityExtentBN(model, entityName);
        }



        public static long sdaiGetEntityExtentBN(long model, byte[] entityName)
        {
            if (_x86)
            {
                return x86.sdaiGetEntityExtentBN((int)model, entityName);
            }

            return x64.sdaiGetEntityExtentBN(model, entityName);
        }



        //

        //		engiGetEntityName                           (http://rdf.bg/ifcdoc/CS64/engiGetEntityName.html)

        //

        //	This call can be used to get the name of the given entity.

        //

        public static void engiGetEntityName(long entity, long valueType, out IntPtr entityName)
        {
            if (_x86)
            {
                x86.engiGetEntityName((int)entity, (int)valueType, out entityName);
            }
            else
            {
                x64.engiGetEntityName(entity, valueType, out entityName);
            }
        }



        //

        //		engiGetEntityNoArguments                    (http://rdf.bg/ifcdoc/CS64/engiGetEntityNoArguments.html)

        //

        //	This call returns the number of arguments, this includes the arguments of its (nested) parents and inverse argumnets.

        //

        public static long engiGetEntityNoArguments(long entity)
        {
            if (_x86)
            {
                return x86.engiGetEntityNoArguments((int)entity);
            }

            return x64.engiGetEntityNoArguments(entity);
        }



		//

		//		engiGetEntityParent                         (http://rdf.bg/ifcdoc/CS64/engiGetEntityParent.html)

		//

		//	Returns the direct parent entity, for example the parent of IfcObject is IfcObjectDefinition, of IfcObjectDefinition is IfcRoot and of IfcRoot is 0.

		//

        public static long engiGetEntityParent(long entity)
        {
            if (_x86)
            {
                return x86.engiGetEntityParent((int)entity);
            }

            return x64.engiGetEntityParent(entity);
        }



        //

        //		engiGetAttrOptional                         (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptional.html)

        //

        //	This call can be used to check if an attribute is optional

        //

        public static long engiGetAttrOptional(ref long attribute)
        {
            if (_x86)
            {
                int iAttribute = (int)attribute;
                return x86.engiGetAttrOptional(ref iAttribute);
            }

            return x64.engiGetAttrOptional(ref attribute);
        }



		//

		//		engiGetAttrOptionalBN                       (http://rdf.bg/ifcdoc/CS64/engiGetAttrOptionalBN.html)

		//

		//	This call can be used to check if an attribute is optional

		//

        public static long engiGetAttrOptionalBN(long entity, string attributeName)
        {
            if (_x86)
            {
                return x86.engiGetAttrOptionalBN((int)entity, attributeName);
            }

            return x64.engiGetAttrOptionalBN(entity, attributeName);
        }



        public static long engiGetAttrOptionalBN(long entity, byte[] attributeName)
        {
            if (_x86)
            {
                return x86.engiGetAttrOptionalBN((int)entity, attributeName);
            }

            return x64.engiGetAttrOptionalBN(entity, attributeName);
        }



        //

        //		engiGetAttrInverse                          (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverse.html)

        //

        //	This call can be used to check if an attribute is an inverse relation

        //

        public static long engiGetAttrInverse(ref long attribute)
        {
            if (_x86)
            {
                int iAttribute = (int)attribute;
                return x86.engiGetAttrInverse(ref iAttribute);
            }

            return x64.engiGetAttrInverse(ref attribute);
        }



		//

		//		engiGetAttrInverseBN                        (http://rdf.bg/ifcdoc/CS64/engiGetAttrInverseBN.html)

		//

		//	This call can be used to check if an attribute is an inverse relation

		//

        public static long engiGetAttrInverseBN(long entity, string attributeName)
        {
            if (_x86)
            {
                return x86.engiGetAttrInverseBN((int)entity, attributeName);
            }

            return x64.engiGetAttrInverseBN(entity, attributeName);
        }



        public static long engiGetAttrInverseBN(long entity, byte[] attributeName)
        {
            if (_x86)
            {
                return x86.engiGetAttrInverseBN((int)entity, attributeName);
            }

            return x64.engiGetAttrInverseBN(entity, attributeName);
        }



        //

        //		engiGetEnumerationValue                     (http://rdf.bg/ifcdoc/CS64/engiGetEnumerationValue.html)

        //

        //	...

        //

        public static void engiGetEnumerationValue(long attribute, long index, long valueType, out IntPtr enumerationValue)
        {
            if (_x86)
            {
                x86.engiGetEnumerationValue((int)attribute, (int)index, (int)valueType, out enumerationValue);
            }
            else
            {
                x64.engiGetEnumerationValue(attribute, index, valueType, out enumerationValue);
            }            
        }



		//

		//		engiGetEntityProperty                       (http://rdf.bg/ifcdoc/CS64/engiGetEntityProperty.html)

		//

		//	...

		//

        public static void engiGetEntityProperty(long entity, long index, out IntPtr propertyName, out long optional, out long type, out long _array, out long set, out long list, out long bag, out long min, out long max, out long referenceEntity, out long inverse)
        {
            if (_x86)
            {
                x86.engiGetEntityProperty((int)entity, (int)index, out propertyName, out int iOptional, out int iType, out int i_array, out int iSet, out int iList, out int iBag, out int iMin, out int iMax, out int iReferenceEntity, out int iInverse);
                optional = iOptional;
                type = iType;
                _array = i_array;
                set = iSet;
                list = iList;
                bag = iBag;
                min = iMin; 
                max = iMax; 
                referenceEntity = iReferenceEntity; 
                inverse = iInverse;
            }
            else
            {
                x64.engiGetEntityProperty(entity, index, out propertyName, out optional, out type, out _array, out set, out list, out bag, out min, out max, out referenceEntity, out inverse);
            }
        }



        //

        //  Instance Header API Calls

        //



		//

		//		SetSPFFHeader                               (http://rdf.bg/ifcdoc/CS64/SetSPFFHeader.html)

		//

		//	This call is an aggregate of several SetSPFFHeaderItem calls. In several cases the header can be set easily with this call. In case an argument is zero, this argument will not be updated, i.e. it will not be filled with 0.

		//

        public static void SetSPFFHeader(long model, string description, string implementationLevel, string name, string timeStamp, string author, string organization, string preprocessorVersion, string originatingSystem, string authorization, string fileSchema)
        {
            if (_x86)
            {
                x86.SetSPFFHeader((int)model, description, implementationLevel, name, timeStamp, author, organization, preprocessorVersion, originatingSystem, authorization, fileSchema);
            }
            else
            {
                x64.SetSPFFHeader(model, description, implementationLevel, name, timeStamp, author, organization, preprocessorVersion, originatingSystem, authorization, fileSchema);
            }            
        }



        public static void SetSPFFHeader(long model, byte[] description, byte[] implementationLevel, byte[] name, byte[] timeStamp, byte[] author, byte[] organization, byte[] preprocessorVersion, byte[] originatingSystem, byte[] authorization, byte[] fileSchema)
        {
            if (_x86)
            {
                x86.SetSPFFHeader((int)model, description, implementationLevel, name, timeStamp, author, organization, preprocessorVersion, originatingSystem, authorization, fileSchema);
            }
            else
            {
                x64.SetSPFFHeader(model, description, implementationLevel, name, timeStamp, author, organization, preprocessorVersion, originatingSystem, authorization, fileSchema);
            }
        }



        //

        //		SetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/SetSPFFHeaderItem.html)

        //

        //	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.

        //

        public static long SetSPFFHeaderItem(long model, long itemIndex, long itemSubIndex, long valueType, string value)
        {
            if (_x86)
            {
                return x86.SetSPFFHeaderItem((int)model, (int)itemIndex, (int)itemSubIndex, (int)valueType, value);
            }

            return x64.SetSPFFHeaderItem(model, itemIndex, itemSubIndex, valueType, value);
        }



        public static long SetSPFFHeaderItem(long model, long itemIndex, long itemSubIndex, long valueType, byte[] value)
        {
            if (_x86)
            {
                return x86.SetSPFFHeaderItem((int)model, (int)itemIndex, (int)itemSubIndex, (int)valueType, value);
            }

            return x64.SetSPFFHeaderItem(model, itemIndex, itemSubIndex, valueType, value);
        }



        //

        //		GetSPFFHeaderItem                           (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItem.html)

        //

        //	This call can be used to read a specific header item, the source code example is larger to show and explain how this call can be used.

        //

        public static long GetSPFFHeaderItem(long model, long itemIndex, long itemSubIndex, long valueType, out IntPtr value)
        {
            if (_x86)
            {
                return x86.GetSPFFHeaderItem((int)model, (int)itemIndex, (int)itemSubIndex, (int)valueType, out value);
            }

            return x64.GetSPFFHeaderItem(model, itemIndex, itemSubIndex, valueType, out value);
        }



		//

		//		GetSPFFHeaderItemUnicode                    (http://rdf.bg/ifcdoc/CS64/GetSPFFHeaderItemUnicode.html)

		//

		//	This call can be used to write a specific header item, the source code example is larger to show and explain how this call can be used.

		//

        public static long GetSPFFHeaderItemUnicode(long model, long itemIndex, long itemSubIndex, string buffer, long bufferLength)
        {
            if (_x86)
            {
                return x86.GetSPFFHeaderItemUnicode((int)model, (int)itemIndex, (int)itemSubIndex, buffer, (int)bufferLength);
            }

            return x64.GetSPFFHeaderItemUnicode(model, itemIndex, itemSubIndex, buffer, bufferLength);
        }



        public static long GetSPFFHeaderItemUnicode(long model, long itemIndex, long itemSubIndex, byte[] buffer, long bufferLength)
        {
            if (_x86)
            {
                return x86.GetSPFFHeaderItemUnicode((int)model, (int)itemIndex, (int)itemSubIndex, buffer, (int)bufferLength);
            }

            return x64.GetSPFFHeaderItemUnicode(model, itemIndex, itemSubIndex, buffer, bufferLength);
        }



        //

        //  Instance Reading API Calls

        //



        //

        //		sdaiGetADBType                              (http://rdf.bg/ifcdoc/CS64/sdaiGetADBType.html)

        //

        //	This call can be used to get the used type within this ADB type.

        //

        public static long sdaiGetADBType(ref long ADB)
        {
            if (_x86)
            {
                int iADB = (int)ADB;
                return x86.sdaiGetADBType(ref iADB);
            }

            return x64.sdaiGetADBType(ref ADB);
        }



		//

		//		sdaiGetADBTypePath                          (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePath.html)

		//

		//	This call can be used to get the path of an ADB type.

		//

        public static IntPtr sdaiGetADBTypePath(ref long ADB, long typeNameNumber)
        {
            if (_x86)
            {
                int iADB = (int)ADB;
                return x86.sdaiGetADBTypePath(ref iADB, (int)typeNameNumber);
            }

            return x64.sdaiGetADBTypePath(ref ADB, typeNameNumber);
        }



		//

		//		sdaiGetADBTypePathx                         (http://rdf.bg/ifcdoc/CS64/sdaiGetADBTypePathx.html)

		//

		//	This call is the same as sdaiGetADBTypePath, however can be used by porting to languages that have issues with returned char arrays.

		//

        public static void sdaiGetADBTypePathx(ref long ADB, long typeNameNumber, out IntPtr path)
        {
            if (_x86)
            {
                int iADB = (int)ADB;
                x86.sdaiGetADBTypePathx(ref iADB, (int)typeNameNumber, out path);
            }
            else
            {
                x64.sdaiGetADBTypePathx(ref ADB, typeNameNumber, out path);
            }            
        }



		//

		//		sdaiGetADBValue                             (http://rdf.bg/ifcdoc/CS64/sdaiGetADBValue.html)

		//

		//	...

		//

        public static void sdaiGetADBValue(ref long ADB, long valueType, out long value)
        {
            if (_x86)
            {
                int iADB = (int)ADB;
                x86.sdaiGetADBValue(ref iADB, (int)valueType, out int iValue);
                value = iValue;
            }
            else
            {
                x64.sdaiGetADBValue(ref ADB, valueType, out value);
            }            
        }



        public static void sdaiGetADBValue(ref long ADB, long valueType, out double value)
        {
            if (_x86)
            {
                int iADB = (int)ADB;
                x86.sdaiGetADBValue(ref iADB, (int)valueType, out double dValue);
                value = dValue;
            }
            else
            {
                x64.sdaiGetADBValue(ref ADB, valueType, out value);
            }
        }



        public static void sdaiGetADBValue(ref long ADB, long valueType, out IntPtr value)
        {
            if (_x86)
            {
                int iADB = (int)ADB;
                x86.sdaiGetADBValue(ref iADB, (int)valueType, out value);
            }
            else
            {
                x64.sdaiGetADBValue(ref ADB, valueType, out value);
            }
        }



        //

        //		engiGetAggrElement                          (http://rdf.bg/ifcdoc/CS64/engiGetAggrElement.html)

        //

        //	...

        //

        public static long engiGetAggrElement(long aggregate, long elementIndex, long valueType, out long value)
        {
            if (_x86)
            {
                long lResult = x86.engiGetAggrElement((int)aggregate, (int)elementIndex, (int)valueType, out int iValue);
                value = iValue;
                return lResult;
            }

            return x64.engiGetAggrElement(aggregate, elementIndex, valueType, out value);
        }



        public static long engiGetAggrElement(long aggregate, long elementIndex, long valueType, out double value)
        {
            if (_x86)
            {
                long lResult = x86.engiGetAggrElement((int)aggregate, (int)elementIndex, (int)valueType, out double dValue);
                value = dValue;
                return lResult;
            }

            return x64.engiGetAggrElement(aggregate, elementIndex, valueType, out value);
        }



        public static long engiGetAggrElement(long aggregate, long elementIndex, long valueType, out IntPtr value)
        {
            if (_x86)
            {
                return x86.engiGetAggrElement((int)aggregate, (int)elementIndex, (int)valueType, out value);
            }

            return x64.engiGetAggrElement(aggregate, elementIndex, valueType, out value);
        }



        //

        //		engiGetAggrType                             (http://rdf.bg/ifcdoc/CS64/engiGetAggrType.html)

        //

        //	...

        //

        public static void engiGetAggrType(long aggregate, out long aggragateType)
        {
            if (_x86)
            {
                x86.engiGetAggrType((int)aggregate, out int iAggragateType);
                aggragateType = iAggragateType;
            }

            x64.engiGetAggrType(aggregate, out aggragateType);
        }



		//

		//		engiGetAggrTypex                            (http://rdf.bg/ifcdoc/CS64/engiGetAggrTypex.html)

		//

		//	...

		//

        public static void engiGetAggrTypex(long aggregate, out long aggragateType)
        {
            if (_x86)
            {
                x86.engiGetAggrTypex((int)aggregate, out int iAggragateType);
                aggragateType = iAggragateType;
            }

            x64.engiGetAggrTypex(aggregate, out aggragateType);
        }


        //

        //		sdaiGetAttr                                 (http://rdf.bg/ifcdoc/CS64/sdaiGetAttr.html)

        //

        //	...

        //

        public static long sdaiGetAttr(long instance, long attribute, long valueType, out long value)
        {
            if (_x86)
            {
                long lResult = x86.sdaiGetAttr((int)instance, (int)attribute, (int)valueType, out int iValue);
                value = iValue;
                return lResult;
            }

            return x64.sdaiGetAttr(instance, attribute, valueType, out value);
        }



        public static long sdaiGetAttr(long instance, long attribute, long valueType, out double value)
        {
            if (_x86)
            {
                long lResult = x86.sdaiGetAttr((int)instance, (int)attribute, (int)valueType, out double dValue);
                value = dValue;
                return lResult;
            }

            return x64.sdaiGetAttr(instance, attribute, valueType, out value);
        }



        public static long sdaiGetAttr(long instance, long attribute, long valueType, out IntPtr value)
        {
            if (_x86)
            {
                return x86.sdaiGetAttr((int)instance, (int)attribute, (int)valueType, out value);
            }

            return x64.sdaiGetAttr(instance, attribute, valueType, out value);
        }



		//

		//		sdaiGetAttrBN                               (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBN.html)

		//

		//	...

		//

        public static long sdaiGetAttrBN(long instance, string attributeName, long valueType, out long value)
        {
            if (_x86)
            {
                long lResult = x86.sdaiGetAttrBN((int)instance, attributeName, (int)valueType, out int iValue);
                value = iValue;
                return lResult;
            }

            return x64.sdaiGetAttrBN(instance, attributeName, valueType, out value);
        }



        public static long sdaiGetAttrBN(long instance, string attributeName, long valueType, out double value)
        {
            if (_x86)
            {
                long lResult = x86.sdaiGetAttrBN((int)instance, attributeName, (int)valueType, out double dValue);
                value = dValue;
                return lResult;
            }

            return x64.sdaiGetAttrBN(instance, attributeName, valueType, out value);
        }



        public static long sdaiGetAttrBN(long instance, string attributeName, long valueType, out IntPtr value)
        {
            if (_x86)
            {
                return x86.sdaiGetAttrBN((int)instance, attributeName, (int)valueType, out value);
            }

            return x64.sdaiGetAttrBN(instance, attributeName, valueType, out value);
        }



        public static long sdaiGetAttrBN(long instance, byte[] attributeName, long valueType, out long value)
        {
            if (_x86)
            {
                long lResult = x86.sdaiGetAttrBN((int)instance, attributeName, (int)valueType, out int iValue);
                value = iValue;
                return lResult;
            }

            return x64.sdaiGetAttrBN(instance, attributeName, valueType, out value);
        }



        public static long sdaiGetAttrBN(long instance, byte[] attributeName, long valueType, out double value)
        {
            if (_x86)
            {
                long lResult = x86.sdaiGetAttrBN((int)instance, attributeName, (int)valueType, out double dValue);
                value = dValue;
                return lResult;
            }

            return x64.sdaiGetAttrBN(instance, attributeName, valueType, out value);
        }



        public static long sdaiGetAttrBN(long instance, byte[] attributeName, long valueType, out IntPtr value)
        {
            if (_x86)
            {
                return x86.sdaiGetAttrBN((int)instance, attributeName, (int)valueType, out value);
            }

            return x64.sdaiGetAttrBN(instance, attributeName, valueType, out value);
        }



		//

		//		sdaiGetAttrBNUnicode                        (http://rdf.bg/ifcdoc/CS64/sdaiGetAttrBNUnicode.html)

		//

		//	...

		//

        public static long sdaiGetAttrBNUnicode(long instance, string attributeName, string buffer, long bufferLength)
        {
            if (_x86)
            {
                return x86.sdaiGetAttrBNUnicode((int)instance, attributeName, buffer, (int)bufferLength);
            }

            return x64.sdaiGetAttrBNUnicode(instance, attributeName, buffer, bufferLength);
        }



        public static long sdaiGetAttrBNUnicode(long instance, byte[] attributeName, byte[] buffer, long bufferLength)
        {
            if (_x86)
            {
                return x86.sdaiGetAttrBNUnicode((int)instance, attributeName, buffer, (int)bufferLength);
            }

            return x64.sdaiGetAttrBNUnicode(instance, attributeName, buffer, bufferLength);
        }



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

        public static extern long engiGetInstanceMetaInfo(out long instance, out long localId, out IntPtr entityName, out IntPtr entityNameUC);



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

        
        public static void setFilter(long model, long setting, long mask)
        {
            if (_x86)
            {
                x86.setFilter((int)model, (int)setting, (int)mask);
            }
            else
            {
                x64.setFilter(model, setting, mask);
            }
        }



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

        public static extern void engiGetAggrUnknownElement(out long aggregate, long elementIndex, out long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(out long aggregate, long elementIndex, out long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(out long aggregate, long elementIndex, out long valueType, out IntPtr value);



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

        public static extern long GetRevision(out IntPtr timeStamp);



		//

		//		GetRevisionW                                (http://rdf.bg/gkdoc/CS64/GetRevisionW.html)

		//

		//	Returns the revision number.

		//	The timeStamp is generated by the SVN system used during development.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetRevisionW")]

        public static extern long GetRevisionW(out IntPtr timeStamp);



		//

		//		GetProtection                               (http://rdf.bg/gkdoc/CS64/GetProtection.html)

		//

		//	This call is required to be called to enable the DLL to work if protection is active.

		//

		//	Returns the number of days (incl. this one) that this version is still active or 0 if no protection is embedded.

		//	In case no days are left and protection is active this call will return -1.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetProtection")]

        public static extern long GetProtection();



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

        public static extern long GetEnvironment(out IntPtr environmentVariables, out IntPtr developmentVariables);



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

        public static extern long GetEnvironmentW(out IntPtr environmentVariables, out IntPtr developmentVariables);



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

        public static extern long SetCharacterSerialization(long model, long encoding, long wcharBitSizeOverride, byte ascii);



		//

		//		GetCharacterSerialization                   (http://rdf.bg/gkdoc/CS64/GetCharacterSerialization.html)

		//

		//	This call retrieves the values as set by 

		//

		//	The returns the size of a single character in bits, i.e. 1 byte is 8 bits, this can be 8, 16 or 32 depending on settings and operating system

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetCharacterSerialization")]

        public static extern long GetCharacterSerialization(long model, out long encoding, out byte ascii);



		//

		//		AbortModel                                  (http://rdf.bg/gkdoc/CS64/AbortModel.html)

		//

		//	This function abort running processes for a model. It can be used when a task takes more time than

		//	expected / available, or in case the requested results are not relevant anymore.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "AbortModel")]

        public static extern long AbortModel(long model, long setting);



		//

		//		GetSessionMetaInfo                          (http://rdf.bg/gkdoc/CS64/GetSessionMetaInfo.html)

		//

		//	This function is meant for debugging purposes and return statistics during processing.

		//	The return value represents the number of active models within the session (or zero if the model was not recognized).

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetSessionMetaInfo")]

        public static extern long GetSessionMetaInfo(out long allocatedBlocks, out long allocatedBytes, out long nonUsedBlocks, out long nonUsedBytes);



		//

		//		GetModelMetaInfo                            (http://rdf.bg/gkdoc/CS64/GetModelMetaInfo.html)

		//

		//	This function is meant for debugging purposes and return statistics during processing.

		//	The return value represents the number of active models within the session (or zero if the model was not recognized).

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, out long activeProperties, out long deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, out long activeProperties, out long deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long removedClasses, out long activeProperties, out long deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long removedClasses, out long activeProperties, out long deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



		//

		//		GetInstanceMetaInfo                         (http://rdf.bg/gkdoc/CS64/GetInstanceMetaInfo.html)

		//

		//	This function is meant for debugging purposes and return statistics during processing.

		//	The return value represents the number of active instances within the model (or zero if the instance was not recognized).

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceMetaInfo")]

        public static extern long GetInstanceMetaInfo(long owlInstance, out long allocatedBlocks, out long allocatedBytes);



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

        public static extern long GetSmoothness(long owlInstance, out long degree);



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

        public static extern void AddState(long model, long owlInstance);



		//

		//		GetModel                                    (http://rdf.bg/gkdoc/CS64/GetModel.html)

		//

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetModel")]

        public static extern long GetModel(long owlInstance);



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

        public static extern void OrderedHandles(long model, out long classCnt, out long propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, out long classCnt, out long propertyCnt, IntPtr instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, out long classCnt, IntPtr propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, out long classCnt, IntPtr propertyCnt, IntPtr instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, out long propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, out long propertyCnt, IntPtr instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, IntPtr propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, IntPtr propertyCnt, IntPtr instanceCnt, long setting, long mask);



		//

		//		PeelArray                                   (http://rdf.bg/gkdoc/CS64/PeelArray.html)

		//

		//	This function introduces functionality that is missing or complicated in some programming languages.

		//	The attribute inValue is a reference to an array of references. The attribute outValue is a reference to the same array,

		//	however a number of elements earlier or further, i.e. number of elements being attribute elementSize. Be aware that as

		//	we are talking about references the offset is depending on 32 bit / 64 bit compilation.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "PeelArray")]

        public static extern void PeelArray(ref byte[] inValue, out byte outValue, long elementSize);



		//

		//		CloseSession                                (http://rdf.bg/gkdoc/CS64/CloseSession.html)

		//

		//	This function closes the session, after this call the geometry kernel cannot be used anymore.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "CloseSession")]

        public static extern long CloseSession();



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

        public static extern void ClearCache(long model);



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

        public static extern long CreateModel();



		//

		//		OpenModel                                   (http://rdf.bg/gkdoc/CS64/OpenModel.html)

		//

		//	This function opens the model on location fileName.

		//	References inside to other ontologies will be included.

		//	A handle to the model will be returned, or 0 in case something went wrong.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModel")]

        public static extern long OpenModel(string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "OpenModel")]

        public static extern long OpenModel(byte[] fileName);



		//

		//		OpenModelW                                  (http://rdf.bg/gkdoc/CS64/OpenModelW.html)

		//

		//	This function opens the model on location fileName.

		//	References inside to other ontologies will be included.

		//	A handle to the model will be returned, or 0 in case something went wrong.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelW")]

        public static extern long OpenModelW(string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelW")]

        public static extern long OpenModelW(byte[] fileName);



		//

		//		OpenModelS                                  (http://rdf.bg/gkdoc/CS64/OpenModelS.html)

		//

		//	This function opens the model via a stream.

		//	References inside to other ontologies will be included.

		//	A handle to the model will be returned, or 0 in case something went wrong.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelS")]

        public static extern long OpenModelS([MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback);



		//

		//		OpenModelA                                  (http://rdf.bg/gkdoc/CS64/OpenModelA.html)

		//

		//	This function opens the model via an array.

		//	References inside to other ontologies will be included.

		//	A handle to the model will be returned, or 0 in case something went wrong.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelA")]

        public static extern long OpenModelA(byte[] content, long size);



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

        public static extern long ImportModel(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "ImportModel")]

        public static extern long ImportModel(long model, byte[] fileName);



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

        public static extern long ImportModelW(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "ImportModelW")]

        public static extern long ImportModelW(long model, byte[] fileName);



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

        public static extern long ImportModelS(long model, [MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback);



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

        public static extern long ImportModelA(long model, byte[] content, long size);



		//

		//		SaveInstanceTree                            (http://rdf.bg/gkdoc/CS64/SaveInstanceTree.html)

		//

		//	This function saves the selected instance and its dependancies on location fileName.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTree")]

        public static extern long SaveInstanceTree(long owlInstance, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTree")]

        public static extern long SaveInstanceTree(long owlInstance, byte[] fileName);



		//

		//		SaveInstanceTreeW                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeW.html)

		//

		//	This function saves the selected instance and its dependancies on location fileName.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeW")]

        public static extern long SaveInstanceTreeW(long owlInstance, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeW")]

        public static extern long SaveInstanceTreeW(long owlInstance, byte[] fileName);



		//

		//		SaveInstanceTreeS                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeS.html)

		//

		//	This function saves the selected instance and its dependancies in a stream.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeS")]

        public static extern long SaveInstanceTreeS(long owlInstance, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, long size);



		//

		//		SaveInstanceTreeA                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeA.html)

		//

		//	This function saves the selected instance and its dependancies in an array.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeA")]

        public static extern long SaveInstanceTreeA(long owlInstance, byte[] content, out long size);



		//

		//		SaveModel                                   (http://rdf.bg/gkdoc/CS64/SaveModel.html)

		//

		//	This function saves the current model on location fileName.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModel")]

        public static extern long SaveModel(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveModel")]

        public static extern long SaveModel(long model, byte[] fileName);



		//

		//		SaveModelW                                  (http://rdf.bg/gkdoc/CS64/SaveModelW.html)

		//

		//	This function saves the current model on location fileName.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelW")]

        public static extern long SaveModelW(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelW")]

        public static extern long SaveModelW(long model, byte[] fileName);



		//

		//		SaveModelS                                  (http://rdf.bg/gkdoc/CS64/SaveModelS.html)

		//

		//	This function saves the current model in a stream.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelS")]

        public static extern long SaveModelS(long model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, long size);



		//

		//		SaveModelA                                  (http://rdf.bg/gkdoc/CS64/SaveModelA.html)

		//

		//	This function saves the current model in an array.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelA")]

        public static extern long SaveModelA(long model, byte[] content, out long size);



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

        public static extern void SetOverrideFileIO(long model, long setting, long mask);



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

        public static extern long GetOverrideFileIO(long model, long mask);



		//

		//		CloseModel                                  (http://rdf.bg/gkdoc/CS64/CloseModel.html)

		//

		//	This function closes the model. After this call none of the instances and classes within the model

		//	can be used anymore, also garbage collection is not allowed anymore, in default compilation the

		//	model itself will be known in the kernel, however known to be disabled. Calls containing the model

		//	reference will be protected from crashing when called.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "CloseModel")]

        public static extern long CloseModel(long model);



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

        public static extern long CreateClass(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateClass")]

        public static extern long CreateClass(long model, byte[] name);



		//

		//		CreateClassW                                (http://rdf.bg/gkdoc/CS64/CreateClassW.html)

		//

		//	Returns a handle to an on the fly created class.

		//	If the model input is zero or not a model handle 0 will be returned,

		//

        [DllImport(IFCEngineDLL, EntryPoint = "CreateClassW")]

        public static extern long CreateClassW(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateClassW")]

        public static extern long CreateClassW(long model, byte[] name);



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

        public static extern long GetClassByName(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByName")]

        public static extern long GetClassByName(long model, byte[] name);



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

        public static extern long GetClassByNameW(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByNameW")]

        public static extern long GetClassByNameW(long model, byte[] name);



		//

		//		GetClassesByIterator                        (http://rdf.bg/gkdoc/CS64/GetClassesByIterator.html)

		//

		//	Returns a handle to an class.

		//	If input class is zero, the handle will point to the first relevant class.

		//	If all classes are past (or no relevant classes are found), the function will return 0.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetClassesByIterator")]

        public static extern long GetClassesByIterator(long model, long owlClass);



		//

		//		SetClassParent                              (http://rdf.bg/gkdoc/CS64/SetClassParent.html)

		//

		//	Defines (set/unset) the parent class of a given class. Multiple-inheritence is supported and behavior

		//	of parent classes is also inherited as well as cardinality restrictions on datatype properties and

		//	object properties (relations).

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetClassParent")]

        public static extern void SetClassParent(long owlClass, long parentOwlClass, long setting);



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

        public static extern void SetClassParentEx(long model, long owlClass, long parentOwlClass, long setting);



		//

		//		GetParentsByIterator                        (http://rdf.bg/gkdoc/CS64/GetParentsByIterator.html)

		//

		//	Returns the next parent of the class.

		//	If input parent is zero, the handle will point to the first relevant parent.

		//	If all parent are past (or no relevant parent are found), the function will return 0.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetParentsByIterator")]

        public static extern long GetParentsByIterator(long owlClass, long parentOwlClass);



		//

		//		SetNameOfClass                              (http://rdf.bg/gkdoc/CS64/SetNameOfClass.html)

		//

		//	Sets/updates the name of the class, if no error it returns 0.

		//	In case class does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClass")]

        public static extern long SetNameOfClass(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClass")]

        public static extern long SetNameOfClass(long owlClass, byte[] name);



		//

		//		SetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/SetNameOfClassW.html)

		//

		//	Sets/updates the name of the class, if no error it returns 0.

		//	In case class does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassW")]

        public static extern long SetNameOfClassW(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassW")]

        public static extern long SetNameOfClassW(long owlClass, byte[] name);



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

        public static extern long SetNameOfClassEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassEx")]

        public static extern long SetNameOfClassEx(long model, long owlClass, byte[] name);



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

        public static extern long SetNameOfClassWEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassWEx")]

        public static extern long SetNameOfClassWEx(long model, long owlClass, byte[] name);



		//

		//		GetNameOfClass                              (http://rdf.bg/gkdoc/CS64/GetNameOfClass.html)

		//

		//	Returns the name of the class, if the class does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClass")]

        public static extern void GetNameOfClass(long owlClass, out IntPtr name);



		//

		//		GetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/GetNameOfClassW.html)

		//

		//	Returns the name of the class, if the class does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassW")]

        public static extern void GetNameOfClassW(long owlClass, out IntPtr name);



		//

		//		GetNameOfClassEx                            (http://rdf.bg/gkdoc/CS64/GetNameOfClassEx.html)

		//

		//	Returns the name of the class, if the class does not exist it returns nullptr.

		//

		//	This call has the same behavior as GetNameOfClass, however needs to be

		//	used in case properties are exchanged as a successive series of integers.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassEx")]

        public static extern void GetNameOfClassEx(long model, long owlClass, out IntPtr name);



		//

		//		GetNameOfClassWEx                           (http://rdf.bg/gkdoc/CS64/GetNameOfClassWEx.html)

		//

		//	Returns the name of the class, if the class does not exist it returns nullptr.

		//

		//	This call has the same behavior as GetNameOfClassW, however needs to be

		//	used in case classes are exchanged as a successive series of integers.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassWEx")]

        public static extern void GetNameOfClassWEx(long model, long owlClass, out IntPtr name);



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

        public static extern void SetClassPropertyCardinalityRestriction(long owlClass, long rdfProperty, long minCard, long maxCard);



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

        public static extern void SetClassPropertyCardinalityRestrictionEx(long model, long owlClass, long rdfProperty, long minCard, long maxCard);



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

        public static extern void GetClassPropertyCardinalityRestriction(long owlClass, long rdfProperty, out long minCard, out long maxCard);



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

        public static extern void GetClassPropertyCardinalityRestrictionEx(long model, long owlClass, long rdfProperty, out long minCard, out long maxCard);



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

        public static extern long GetGeometryClass(long owlClass);



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

        public static extern long GetGeometryClassEx(long model, long owlClass);



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

        public static extern long CreateProperty(long model, long rdfPropertyType, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateProperty")]

        public static extern long CreateProperty(long model, long rdfPropertyType, byte[] name);



		//

		//		CreatePropertyW                             (http://rdf.bg/gkdoc/CS64/CreatePropertyW.html)

		//

		//	Returns a handle to an on the fly created property.

		//	If the model input is zero or not a model handle 0 will be returned,

		//

        [DllImport(IFCEngineDLL, EntryPoint = "CreatePropertyW")]

        public static extern long CreatePropertyW(long model, long rdfPropertyType, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreatePropertyW")]

        public static extern long CreatePropertyW(long model, long rdfPropertyType, byte[] name);



		//

		//		GetPropertyByName                           (http://rdf.bg/gkdoc/CS64/GetPropertyByName.html)

		//

		//	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.

		//	When the property does not exist yet and the name is unique

		//	the property will be created on-the-fly and the handle will be returned.

		//	When the name is not unique and given to a class or instance 0 will be returned.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName")]

        public static extern long GetPropertyByName(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName")]

        public static extern long GetPropertyByName(long model, byte[] name);



		//

		//		GetPropertyByNameW                          (http://rdf.bg/gkdoc/CS64/GetPropertyByNameW.html)

		//

		//	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.

		//	When the property does not exist yet and the name is unique

		//	the property will be created on-the-fly and the handle will be returned.

		//	When the name is not unique and given to a class or instance 0 will be returned.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameW")]

        public static extern long GetPropertyByNameW(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameW")]

        public static extern long GetPropertyByNameW(long model, byte[] name);



		//

		//		GetPropertiesByIterator                     (http://rdf.bg/gkdoc/CS64/GetPropertiesByIterator.html)

		//

		//	Returns a handle to a property.

		//	If input property is zero, the handle will point to the first relevant property.

		//	If all properties are past (or no relevant properties are found), the function will return 0.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertiesByIterator")]

        public static extern long GetPropertiesByIterator(long model, long rdfProperty);



		//

		//		GetRangeRestrictionsByIterator              (http://rdf.bg/gkdoc/CS64/GetRangeRestrictionsByIterator.html)

		//

		//	Returns the next class the property is restricted to.

		//	If input class is zero, the handle will point to the first relevant class.

		//	If all classes are past (or no relevant classes are found), the function will return 0.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetRangeRestrictionsByIterator")]

        public static extern long GetRangeRestrictionsByIterator(long rdfProperty, long owlClass);



		//

		//		SetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/SetNameOfProperty.html)

		//

		//	Sets/updates the name of the property, if no error it returns 0.

		//	In case property does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfProperty")]

        public static extern long SetNameOfProperty(long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfProperty")]

        public static extern long SetNameOfProperty(long rdfProperty, byte[] name);



		//

		//		SetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyW.html)

		//

		//	Sets/updates the name of the property, if no error it returns 0.

		//	In case property does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyW")]

        public static extern long SetNameOfPropertyW(long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyW")]

        public static extern long SetNameOfPropertyW(long rdfProperty, byte[] name);



		//

		//		SetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyEx.html)

		//

		//	Sets/updates the name of the property, if no error it returns 0.

		//	In case property does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyEx")]

        public static extern long SetNameOfPropertyEx(long model, long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyEx")]

        public static extern long SetNameOfPropertyEx(long model, long rdfProperty, byte[] name);



		//

		//		SetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyWEx.html)

		//

		//	Sets/updates the name of the property, if no error it returns 0.

		//	In case property does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]

        public static extern long SetNameOfPropertyWEx(long model, long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]

        public static extern long SetNameOfPropertyWEx(long model, long rdfProperty, byte[] name);



		//

		//		GetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/GetNameOfProperty.html)

		//

		//	Returns the name of the property, if the property does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfProperty")]

        public static extern void GetNameOfProperty(long rdfProperty, out IntPtr name);



		//

		//		GetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyW.html)

		//

		//	Returns the name of the property, if the property does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyW")]

        public static extern void GetNameOfPropertyW(long rdfProperty, out IntPtr name);



		//

		//		GetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyEx.html)

		//

		//	Returns the name of the property, if the property does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyEx")]

        public static extern void GetNameOfPropertyEx(long model, long rdfProperty, out IntPtr name);



		//

		//		GetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyWEx.html)

		//

		//	Returns the name of the property, if the property does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyWEx")]

        public static extern void GetNameOfPropertyWEx(long model, long rdfProperty, out IntPtr name);



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

        public static extern long SetPropertyType(long rdfProperty, long propertyType);



		//

		//		SetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/SetPropertyTypeEx.html)

		//

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetPropertyTypeEx")]

        public static extern long SetPropertyTypeEx(long model, long rdfProperty, long propertyType);



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

        public static extern long GetPropertyType(long rdfProperty);



		//

		//		GetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/GetPropertyTypeEx.html)

		//

		//	This call has the same behavior as GetPropertyType, however needs to be

		//	used in case properties are exchanged as a successive series of integers.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyTypeEx")]

        public static extern long GetPropertyTypeEx(long model, long rdfProperty);



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

        public static extern long CreateInstance(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstance")]

        public static extern long CreateInstance(long owlClass, byte[] name);



		//

		//		CreateInstanceW                             (http://rdf.bg/gkdoc/CS64/CreateInstanceW.html)

		//

		//	Returns a handle to an on the fly created instance.

		//	If the owlClass input is zero or not a class handle 0 will be returned,

		//

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceW")]

        public static extern long CreateInstanceW(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceW")]

        public static extern long CreateInstanceW(long owlClass, byte[] name);



		//

		//		CreateInstanceEx                            (http://rdf.bg/gkdoc/CS64/CreateInstanceEx.html)

		//

		//	Returns a handle to an on the fly created instance.

		//	If the owlClass input is zero or not a class handle 0 will be returned,

		//

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceEx")]

        public static extern long CreateInstanceEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceEx")]

        public static extern long CreateInstanceEx(long model, long owlClass, byte[] name);



		//

		//		CreateInstanceWEx                           (http://rdf.bg/gkdoc/CS64/CreateInstanceWEx.html)

		//

		//	Returns a handle to an on the fly created instance.

		//	If the owlClass input is zero or not a class handle 0 will be returned,

		//

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceWEx")]

        public static extern long CreateInstanceWEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceWEx")]

        public static extern long CreateInstanceWEx(long model, long owlClass, byte[] name);



		//

		//		GetInstancesByIterator                      (http://rdf.bg/gkdoc/CS64/GetInstancesByIterator.html)

		//

		//	Returns a handle to an instance.

		//	If input instance is zero, the handle will point to the first relevant instance.

		//	If all instances are past (or no relevant instances are found), the function will return 0.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancesByIterator")]

        public static extern long GetInstancesByIterator(long model, long owlInstance);



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

        public static extern long GetInstanceClass(long owlInstance);



		//

		//		GetInstanceClassEx                          (http://rdf.bg/gkdoc/CS64/GetInstanceClassEx.html)

		//

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceClassEx")]

        public static extern long GetInstanceClassEx(long model, long owlInstance);



		//

		//		GetInstancePropertyByIterator               (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIterator.html)

		//

		//	Returns a handle to the objectTypeProperty or dataTypeProperty connected to

		//	the instance, this property can also contain a value, but for example also

		//	the knowledge about cardinality restrictions in the context of this instance's class

		//	and the exact cardinality in context of its instance.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancePropertyByIterator")]

        public static extern long GetInstancePropertyByIterator(long owlInstance, long rdfProperty);



		//

		//		GetInstancePropertyByIteratorEx             (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIteratorEx.html)

		//

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancePropertyByIteratorEx")]

        public static extern long GetInstancePropertyByIteratorEx(long model, long owlInstance, long rdfProperty);



		//

		//		GetInstanceInverseReferencesByIterator      (http://rdf.bg/gkdoc/CS64/GetInstanceInverseReferencesByIterator.html)

		//

		//	Returns a handle to the owlInstances refering this instance

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceInverseReferencesByIterator")]

        public static extern long GetInstanceInverseReferencesByIterator(long owlInstance, long referencingOwlInstance);



		//

		//		GetInstanceReferencesByIterator             (http://rdf.bg/gkdoc/CS64/GetInstanceReferencesByIterator.html)

		//

		//	Returns a handle to the owlInstance refered by this instance

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceReferencesByIterator")]

        public static extern long GetInstanceReferencesByIterator(long owlInstance, long referencedOwlInstance);



		//

		//		SetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/SetNameOfInstance.html)

		//

		//	Sets/updates the name of the instance, if no error it returns 0.

		//	In case instance does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstance")]

        public static extern long SetNameOfInstance(long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstance")]

        public static extern long SetNameOfInstance(long owlInstance, byte[] name);



		//

		//		SetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceW.html)

		//

		//	Sets/updates the name of the instance, if no error it returns 0.

		//	In case instance does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceW")]

        public static extern long SetNameOfInstanceW(long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceW")]

        public static extern long SetNameOfInstanceW(long owlInstance, byte[] name);



		//

		//		SetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceEx.html)

		//

		//	Sets/updates the name of the instance, if no error it returns 0.

		//	In case instance does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceEx")]

        public static extern long SetNameOfInstanceEx(long model, long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceEx")]

        public static extern long SetNameOfInstanceEx(long model, long owlInstance, byte[] name);



		//

		//		SetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceWEx.html)

		//

		//	Sets/updates the name of the instance, if no error it returns 0.

		//	In case instance does not exist it returns 1, when name cannot be updated 2.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]

        public static extern long SetNameOfInstanceWEx(long model, long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]

        public static extern long SetNameOfInstanceWEx(long model, long owlInstance, byte[] name);



		//

		//		GetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/GetNameOfInstance.html)

		//

		//	Returns the name of the instance, if the instance does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstance")]

        public static extern void GetNameOfInstance(long owlInstance, out IntPtr name);



		//

		//		GetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceW.html)

		//

		//	Returns the name of the instance, if the instance does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceW")]

        public static extern void GetNameOfInstanceW(long owlInstance, out IntPtr name);



		//

		//		GetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceEx.html)

		//

		//	Returns the name of the instance, if the instance does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceEx")]

        public static extern void GetNameOfInstanceEx(long model, long owlInstance, out IntPtr name);



		//

		//		GetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceWEx.html)

		//

		//	Returns the name of the instance, if the instance does not exist it returns nullptr.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceWEx")]

        public static extern void GetNameOfInstanceWEx(long model, long owlInstance, out IntPtr name);



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

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref byte values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, byte[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref long values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, long[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref double values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, double[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref string values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, string[] values, long card);



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

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref byte values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, byte[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref long values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, long[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref double values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, double[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref string values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, string[] values, long card);



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

        public static extern long GetDatatypeProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long GetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long SetObjectProperty(long owlInstance, long rdfProperty, ref long values, long card);



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

        public static extern long SetObjectPropertyEx(long model, long owlInstance, long rdfProperty, ref long values, long card);



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

        public static extern long GetObjectProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long GetObjectPropertyEx(long model, long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long CreateInstanceInContextStructure(long owlInstance);



		//

		//		DestroyInstanceInContextStructure           (http://rdf.bg/gkdoc/CS64/DestroyInstanceInContextStructure.html)

		//

		//	InstanceInContext structures are updated dynamically and therfore even while the cost

		//	in performance and memory is limited it is advised to destroy structures as soon

		//	as they are obsolete.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "DestroyInstanceInContextStructure")]

        public static extern void DestroyInstanceInContextStructure(long owlInstanceInContext);



		//

		//		InstanceInContextChild                      (http://rdf.bg/gkdoc/CS64/InstanceInContextChild.html)

		//

		//

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextChild")]

        public static extern long InstanceInContextChild(long owlInstanceInContext);



		//

		//		InstanceInContextNext                       (http://rdf.bg/gkdoc/CS64/InstanceInContextNext.html)

		//

		//

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextNext")]

        public static extern long InstanceInContextNext(long owlInstanceInContext);



		//

		//		InstanceInContextIsUpdated                  (http://rdf.bg/gkdoc/CS64/InstanceInContextIsUpdated.html)

		//

		//

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextIsUpdated")]

        public static extern long InstanceInContextIsUpdated(long owlInstanceInContext);



		//

		//		RemoveInstance                              (http://rdf.bg/gkdoc/CS64/RemoveInstance.html)

		//

		//	This function removes an instance from the internal structure.

		//	In case copies are created by the host this function checks if all

		//	copies are removed otherwise the instance will stay available.

		//	Return value is 0 if everything went ok and positive in case of an error

		//

        [DllImport(IFCEngineDLL, EntryPoint = "RemoveInstance")]

        public static extern long RemoveInstance(long owlInstance);



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

        public static extern long RemoveInstanceRecursively(long owlInstance);



		//

		//		RemoveInstances                             (http://rdf.bg/gkdoc/CS64/RemoveInstances.html)

		//

		//	This function removes all available instances for the given model 

		//	from the internal structure.

		//	Return value is the number of removed instances.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "RemoveInstances")]

        public static extern long RemoveInstances(long model);



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

        public static extern long CalculateInstance(long owlInstance, out long vertexBufferSize, out long indexBufferSize, out long transformationBufferSize);



        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]

        public static extern long CalculateInstance(long owlInstance, out long vertexBufferSize, out long indexBufferSize, IntPtr transformationBufferSize);



        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]

        public static extern long CalculateInstance(long owlInstance, out long vertexBufferSize, IntPtr indexBufferSize, IntPtr transformationBufferSize);



        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]

        public static extern long CalculateInstance(long owlInstance, IntPtr vertexBufferSize, IntPtr indexBufferSize, IntPtr transformationBufferSize);



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

        public static extern long UpdateInstance(long owlInstance);



		//

		//		InferenceInstance                           (http://rdf.bg/gkdoc/CS64/InferenceInstance.html)

		//

		//	This function fills in values that are implicitely known but not given by the user. This function

		//	can also be used to identify default values of properties if not given.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "InferenceInstance")]

        public static extern long InferenceInstance(long owlInstance);



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

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, out float vertexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, float[] vertexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, out double vertexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, double[] vertexBuffer);



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

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, out Int32 indexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, Int32[] indexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, out long indexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, long[] indexBuffer);



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

        public static extern long UpdateInstanceTransformationBuffer(long owlInstance, out double transformationBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceTransformationBuffer")]

        public static extern long UpdateInstanceTransformationBuffer(long owlInstance, double[] transformationBuffer);



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

        public static extern void ClearedInstanceExternalBuffers(long owlInstance);



		//

		//		ClearedExternalBuffers                      (http://rdf.bg/gkdoc/CS64/ClearedExternalBuffers.html)

		//

		//	This function tells the engine that all buffers have no memory of earlier filling.

		//	This means that even when buffer content didn't changed it will be updated when

		//	functions UpdateVertexBuffer(), UpdateIndexBuffer() and/or transformationBuffer()

		//	are called.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "ClearedExternalBuffers")]

        public static extern void ClearedExternalBuffers(long model);



		//

		//		GetConceptualFaceCnt                        (http://rdf.bg/gkdoc/CS64/GetConceptualFaceCnt.html)

		//

		//	This function returns the number of conceptual faces for a certain instance.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceCnt")]

        public static extern long GetConceptualFaceCnt(long owlInstance);



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

        public static extern long GetConceptualFace(long owlInstance, long index, out long startIndexTriangles, out long noTriangles);



		//

		//		GetConceptualFaceEx                         (http://rdf.bg/gkdoc/CS64/GetConceptualFaceEx.html)

		//

		//	This function returns a handle to the conceptual face. Be aware that different

		//	instances can return the same handles (however with possible different startIndices and noTriangles).

		//	Argument index should be at least zero and smaller then return value of GetConceptualFaceCnt().

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



		//

		//		GetConceptualFaceMaterial                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceMaterial.html)

		//

		//	This function returns the material instance relevant for this

		//	conceptual face.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceMaterial")]

        public static extern long GetConceptualFaceMaterial(long conceptualFace);



		//

		//		GetConceptualFaceOriginCnt                  (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginCnt.html)

		//

		//	This function returns the number of instances that are the source primitive/concept

		//	for this conceptual face.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOriginCnt")]

        public static extern long GetConceptualFaceOriginCnt(long conceptualFace);



		//

		//		GetConceptualFaceOrigin                     (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOrigin.html)

		//

		//	This function returns a handle to the instance that is the source primitive/concept

		//	for this conceptual face.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOrigin")]

        public static extern long GetConceptualFaceOrigin(long conceptualFace, long index);



		//

		//		GetConceptualFaceOriginEx                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginEx.html)

		//

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOriginEx")]

        public static extern void GetConceptualFaceOriginEx(long conceptualFace, long index, out long originatingOwlInstance, out long originatingConceptualFace);



		//

		//		GetFaceCnt                                  (http://rdf.bg/gkdoc/CS64/GetFaceCnt.html)

		//

		//	This function returns the number of faces for a certain instance.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetFaceCnt")]

        public static extern long GetFaceCnt(long owlInstance);



		//

		//		GetFace                                     (http://rdf.bg/gkdoc/CS64/GetFace.html)

		//

		//	This function gets the individual faces including the meta data, i.e. the number of openings within this specific face.

		//	This call is for very dedicated use, it would be more common to iterate over the individual conceptual faces.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetFace")]

        public static extern void GetFace(long owlInstance, long index, out long startIndex, out long noOpenings);



		//

		//		GetDependingPropertyCnt                     (http://rdf.bg/gkdoc/CS64/GetDependingPropertyCnt.html)

		//

		//	This function returns the number of properties that are of influence on the

		//	location and form of the conceptualFace.

		//

		//	Note: BE AWARE, THIS FUNCTION EXPECTS A TREE, NOT A NETWORK, IN CASE OF A NETWORK THIS FUNCTION CAN LOCK THE ENGINE

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetDependingPropertyCnt")]

        public static extern long GetDependingPropertyCnt(long baseOwlInstance, long conceptualFace);



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

        public static extern void GetDependingProperty(long baseOwlInstance, long conceptualFace, long index, out long owlInstance, out long datatypeProperty);



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

		//			1	Index items returned as long_t (8 byte/64 bit) (only available in 64 bit mode)

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

        public static extern long SetFormat(long model, long setting, long mask);



		//

		//		GetFormat                                   (http://rdf.bg/gkdoc/CS64/GetFormat.html)

		//

		//	Returns the current format given a mask.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetFormat")]

        public static extern long GetFormat(long model, long mask);



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

        public static extern void SetBehavior(long model, long setting, long mask);



		//

		//		GetBehavior                                 (http://rdf.bg/gkdoc/CS64/GetBehavior.html)

		//

		//	Returns the current behavior given a mask.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetBehavior")]

        public static extern long GetBehavior(long model, long mask);



		//

		//		SetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/SetVertexBufferTransformation.html)

		//

		//	Sets the transformation for a Vertex Buffer.

		//	The transformation will always be calculated in 64 bit, even if the vertex element size is 32 bit.

		//	This function can be called just before updating the vertex buffer.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetVertexBufferTransformation")]

        public static extern void SetVertexBufferTransformation(long model, out double matrix);



		//

		//		GetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/GetVertexBufferTransformation.html)

		//

		//	Gets the transformation for a Vertex Buffer.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetVertexBufferTransformation")]

        public static extern void GetVertexBufferTransformation(long model, out double matrix);



		//

		//		SetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/SetIndexBufferOffset.html)

		//

		//	Sets the offset for an Index Buffer.

		//	It is important call this function before updating the vertex buffer. 

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetIndexBufferOffset")]

        public static extern void SetIndexBufferOffset(long model, long offset);



		//

		//		GetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/GetIndexBufferOffset.html)

		//

		//	Gets the current offset for an Index Buffer.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetIndexBufferOffset")]

        public static extern long GetIndexBufferOffset(long model);



		//

		//		SetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/SetVertexBufferOffset.html)

		//

		//	Sets the offset for a Vertex Buffer.

		//	The offset will always be calculated in 64 bit, even if the vertex element size is 32 bit.

		//	This function can be called just before updating the vertex buffer.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetVertexBufferOffset")]

        public static extern void SetVertexBufferOffset(long model, double x, double y, double z);



		//

		//		GetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/GetVertexBufferOffset.html)

		//

		//	Gets the offset for a Vertex Buffer.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetVertexBufferOffset")]

        public static extern void GetVertexBufferOffset(long model, out double x, out double y, out double z);



		//

		//		SetDefaultColor                             (http://rdf.bg/gkdoc/CS64/SetDefaultColor.html)

		//

		//	Set the default values for the colors defined as argument.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetDefaultColor")]

        public static extern void SetDefaultColor(long model, Int32 ambient, Int32 diffuse, Int32 emissive, Int32 specular);



		//

		//		GetDefaultColor                             (http://rdf.bg/gkdoc/CS64/GetDefaultColor.html)

		//

		//	Retrieve the default values for the colors defined as argument.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetDefaultColor")]

        public static extern void GetDefaultColor(long model, out Int32 ambient, out Int32 diffuse, out Int32 emissive, out Int32 specular);



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

        public static extern long CheckConsistency(long model, long mask);



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

        public static extern long CheckInstanceConsistency(long owlInstance, long mask);



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

        public static extern double GetPerimeter(long owlInstance);



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

        public static extern double GetArea(long owlInstance, ref float vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, ref float vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, ref double vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, ref double vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, IntPtr vertices, IntPtr indices);



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

        public static extern double GetVolume(long owlInstance, ref float vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, ref float vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, ref double vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, ref double vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, IntPtr vertices, IntPtr indices);



		//

		//		GetCentroid                                 (http://rdf.bg/gkdoc/CS64/GetCentroid.html)

		//

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref float vertices, ref Int32 indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref float vertices, ref long indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref double vertices, ref Int32 indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref double vertices, ref long indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, IntPtr vertices, IntPtr indices, out double centroid);



		//

		//		GetConceptualFacePerimeter                  (http://rdf.bg/gkdoc/CS64/GetConceptualFacePerimeter.html)

		//

		//	This function returns the perimeter of a given Conceptual Face.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFacePerimeter")]

        public static extern double GetConceptualFacePerimeter(long conceptualFace);



		//

		//		GetConceptualFaceArea                       (http://rdf.bg/gkdoc/CS64/GetConceptualFaceArea.html)

		//

		//	This function returns the area of a given Conceptual Face. The attributes vertices

		//	and indices are optional but will improve performance if defined.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref float vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref float vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref double vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref double vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, IntPtr vertices, IntPtr indices);



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

        public static extern void SetBoundingBoxReference(long owlInstance, out double transformationMatrix, out double startVector, out double endVector);



		//

		//		GetBoundingBox                              (http://rdf.bg/gkdoc/CS64/GetBoundingBox.html)

		//

		//	When the transformationMatrix is given, it will fill an array of 12 double values.

		//	When the transformationMatrix is left empty and both startVector and endVector are

		//	given the boundingbox without transformation is calculated and returned.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetBoundingBox")]

        public static extern void GetBoundingBox(long owlInstance, out double transformationMatrix, out double startVector, out double endVector);



        [DllImport(IFCEngineDLL, EntryPoint = "GetBoundingBox")]

        public static extern void GetBoundingBox(long owlInstance, IntPtr transformationMatrix, out double startVector, out double endVector);



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

        public static extern void GetRelativeTransformation(long owlInstanceHead, long owlInstanceTail, out double transformationMatrix);



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

        public static extern void GetTriangles(long owlInstance, out long startIndex, out long noTriangles, out long startVertex, out long firstNotUsedVertex);



		//

		//		GetLines                                    (http://rdf.bg/gkdoc/CS64/GetLines___.html)

		//

		//	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant lines when

		//	SetFormat() is setting bit 9 and unsetting with bit 8, 10, 12 and 13 

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetLines")]

        public static extern void GetLines(long owlInstance, out long startIndex, out long noLines, out long startVertex, out long firstNotUsedVertex);



		//

		//		GetPoints                                   (http://rdf.bg/gkdoc/CS64/GetPoints___.html)

		//

		//	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant points when

		//	SetFormat() is setting bit 10 and unsetting with bit 8, 9, 12 and 13 

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetPoints")]

        public static extern void GetPoints(long owlInstance, out long startIndex, out long noPoints, out long startVertex, out long firstNotUsedVertex);



		//

		//		GetPropertyRestrictions                     (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictions___.html)

		//

		//	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyRestrictions")]

        public static extern void GetPropertyRestrictions(long owlClass, long rdfProperty, out long minCard, out long maxCard);



		//

		//		GetPropertyRestrictionsConsolidated         (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictionsConsolidated___.html)

		//

		//	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,

		//	just rename the function name.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyRestrictionsConsolidated")]

        public static extern void GetPropertyRestrictionsConsolidated(long owlClass, long rdfProperty, out long minCard, out long maxCard);



		//

		//		IsGeometryType                              (http://rdf.bg/gkdoc/CS64/IsGeometryType___.html)

		//

		//	This call is deprecated and will be removed by end of 2020. Please use the call GetGeometryClass instead, rename the function name

		//	and interpret non-zero as true and zero as false.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "IsGeometryType")]

        public static extern byte IsGeometryType(long owlClass);



		//

		//		SetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/SetObjectTypeProperty___.html)

		//

		//	This call is deprecated and will be removed by end of 2020. Please use the call SetObjectProperty instead, just rename the function name.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetObjectTypeProperty")]

        public static extern long SetObjectTypeProperty(long owlInstance, long rdfProperty, ref long values, long card);



		//

		//		GetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/GetObjectTypeProperty___.html)

		//

		//	This call is deprecated and will be removed by end of 2020. Please use the call GetObjectProperty instead, just rename the function name.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetObjectTypeProperty")]

        public static extern long GetObjectTypeProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



		//

		//		SetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/SetDataTypeProperty___.html)

		//

		//	This call is deprecated and will be removed by end of 2020. Please use the call SetDatatypeProperty instead, just rename the function name.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref byte values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, byte[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref long values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, long[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref double values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, double[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref string values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, string[] values, long card);



		//

		//		GetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/GetDataTypeProperty___.html)

		//

		//	This call is deprecated and will be removed by end of 2020. Please use the call GetDatatypeProperty instead, just rename the function name.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetDataTypeProperty")]

        public static extern long GetDataTypeProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



		//

		//		InstanceCopyCreated                         (http://rdf.bg/gkdoc/CS64/InstanceCopyCreated___.html)

		//

		//	This call is deprecated as the Copy concept is also deprecated and will be removed by end of 2020.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceCopyCreated")]

        public static extern void InstanceCopyCreated(long owlInstance);



		//

		//		GetPropertyByNameAndType                    (http://rdf.bg/gkdoc/CS64/GetPropertyByNameAndType___.html)

		//

		//	This call is deprecated and will be removed by end of 2020.

		//	Please use the call GetPropertyByName(Ex) / GetPropertyByNameW(Ex) + GetPropertyType(Ex) instead, just rename the function name.

		//

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameAndType")]

        public static extern long GetPropertyByNameAndType(long model, string name, long rdfPropertyType);



        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameAndType")]

        public static extern long GetPropertyByNameAndType(long model, byte[] name, long rdfPropertyType);

    }

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

        public static extern int engiGetInstanceMetaInfo(out int instance, out int localId, out IntPtr entityName, out IntPtr entityNameUC);



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

        public static extern void engiGetAggrUnknownElement(out int aggregate, int elementIndex, out int valueType, out int value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(out int aggregate, int elementIndex, out int valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(out int aggregate, int elementIndex, out int valueType, out IntPtr value);



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

        public static extern long GetRevision(out IntPtr timeStamp);



        //

        //		GetRevisionW                                (http://rdf.bg/gkdoc/CS64/GetRevisionW.html)

        //

        //	Returns the revision number.

        //	The timeStamp is generated by the SVN system used during development.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetRevisionW")]

        public static extern long GetRevisionW(out IntPtr timeStamp);



        //

        //		GetProtection                               (http://rdf.bg/gkdoc/CS64/GetProtection.html)

        //

        //	This call is required to be called to enable the DLL to work if protection is active.

        //

        //	Returns the number of days (incl. this one) that this version is still active or 0 if no protection is embedded.

        //	In case no days are left and protection is active this call will return -1.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetProtection")]

        public static extern long GetProtection();



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

        public static extern long GetEnvironment(out IntPtr environmentVariables, out IntPtr developmentVariables);



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

        public static extern long GetEnvironmentW(out IntPtr environmentVariables, out IntPtr developmentVariables);



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

        public static extern long SetCharacterSerialization(long model, long encoding, long wcharBitSizeOverride, byte ascii);



        //

        //		GetCharacterSerialization                   (http://rdf.bg/gkdoc/CS64/GetCharacterSerialization.html)

        //

        //	This call retrieves the values as set by 

        //

        //	The returns the size of a single character in bits, i.e. 1 byte is 8 bits, this can be 8, 16 or 32 depending on settings and operating system

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetCharacterSerialization")]

        public static extern long GetCharacterSerialization(long model, out long encoding, out byte ascii);



        //

        //		AbortModel                                  (http://rdf.bg/gkdoc/CS64/AbortModel.html)

        //

        //	This function abort running processes for a model. It can be used when a task takes more time than

        //	expected / available, or in case the requested results are not relevant anymore.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "AbortModel")]

        public static extern long AbortModel(long model, long setting);



        //

        //		GetSessionMetaInfo                          (http://rdf.bg/gkdoc/CS64/GetSessionMetaInfo.html)

        //

        //	This function is meant for debugging purposes and return statistics during processing.

        //	The return value represents the number of active models within the session (or zero if the model was not recognized).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetSessionMetaInfo")]

        public static extern long GetSessionMetaInfo(out long allocatedBlocks, out long allocatedBytes, out long nonUsedBlocks, out long nonUsedBytes);



        //

        //		GetModelMetaInfo                            (http://rdf.bg/gkdoc/CS64/GetModelMetaInfo.html)

        //

        //	This function is meant for debugging purposes and return statistics during processing.

        //	The return value represents the number of active models within the session (or zero if the model was not recognized).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, out long activeProperties, out long deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, out long activeProperties, out long deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long removedClasses, out long activeProperties, out long deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long removedClasses, out long activeProperties, out long deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        //

        //		GetInstanceMetaInfo                         (http://rdf.bg/gkdoc/CS64/GetInstanceMetaInfo.html)

        //

        //	This function is meant for debugging purposes and return statistics during processing.

        //	The return value represents the number of active instances within the model (or zero if the instance was not recognized).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceMetaInfo")]

        public static extern long GetInstanceMetaInfo(long owlInstance, out long allocatedBlocks, out long allocatedBytes);



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

        public static extern long GetSmoothness(long owlInstance, out long degree);



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

        public static extern void AddState(long model, long owlInstance);



        //

        //		GetModel                                    (http://rdf.bg/gkdoc/CS64/GetModel.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetModel")]

        public static extern long GetModel(long owlInstance);



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

        public static extern void OrderedHandles(long model, out long classCnt, out long propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, out long classCnt, out long propertyCnt, IntPtr instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, out long classCnt, IntPtr propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, out long classCnt, IntPtr propertyCnt, IntPtr instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, out long propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, out long propertyCnt, IntPtr instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, IntPtr propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, IntPtr propertyCnt, IntPtr instanceCnt, long setting, long mask);



        //

        //		PeelArray                                   (http://rdf.bg/gkdoc/CS64/PeelArray.html)

        //

        //	This function introduces functionality that is missing or complicated in some programming languages.

        //	The attribute inValue is a reference to an array of references. The attribute outValue is a reference to the same array,

        //	however a number of elements earlier or further, i.e. number of elements being attribute elementSize. Be aware that as

        //	we are talking about references the offset is depending on 32 bit / 64 bit compilation.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "PeelArray")]

        public static extern void PeelArray(ref byte[] inValue, out byte outValue, long elementSize);



        //

        //		CloseSession                                (http://rdf.bg/gkdoc/CS64/CloseSession.html)

        //

        //	This function closes the session, after this call the geometry kernel cannot be used anymore.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CloseSession")]

        public static extern long CloseSession();



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

        public static extern void ClearCache(long model);



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

        public static extern long CreateModel();



        //

        //		OpenModel                                   (http://rdf.bg/gkdoc/CS64/OpenModel.html)

        //

        //	This function opens the model on location fileName.

        //	References inside to other ontologies will be included.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModel")]

        public static extern long OpenModel(string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "OpenModel")]

        public static extern long OpenModel(byte[] fileName);



        //

        //		OpenModelW                                  (http://rdf.bg/gkdoc/CS64/OpenModelW.html)

        //

        //	This function opens the model on location fileName.

        //	References inside to other ontologies will be included.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelW")]

        public static extern long OpenModelW(string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelW")]

        public static extern long OpenModelW(byte[] fileName);



        //

        //		OpenModelS                                  (http://rdf.bg/gkdoc/CS64/OpenModelS.html)

        //

        //	This function opens the model via a stream.

        //	References inside to other ontologies will be included.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelS")]

        public static extern long OpenModelS([MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback);



        //

        //		OpenModelA                                  (http://rdf.bg/gkdoc/CS64/OpenModelA.html)

        //

        //	This function opens the model via an array.

        //	References inside to other ontologies will be included.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelA")]

        public static extern long OpenModelA(byte[] content, long size);



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

        public static extern long ImportModel(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "ImportModel")]

        public static extern long ImportModel(long model, byte[] fileName);



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

        public static extern long ImportModelW(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "ImportModelW")]

        public static extern long ImportModelW(long model, byte[] fileName);



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

        public static extern long ImportModelS(long model, [MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback);



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

        public static extern long ImportModelA(long model, byte[] content, long size);



        //

        //		SaveInstanceTree                            (http://rdf.bg/gkdoc/CS64/SaveInstanceTree.html)

        //

        //	This function saves the selected instance and its dependancies on location fileName.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTree")]

        public static extern long SaveInstanceTree(long owlInstance, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTree")]

        public static extern long SaveInstanceTree(long owlInstance, byte[] fileName);



        //

        //		SaveInstanceTreeW                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeW.html)

        //

        //	This function saves the selected instance and its dependancies on location fileName.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeW")]

        public static extern long SaveInstanceTreeW(long owlInstance, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeW")]

        public static extern long SaveInstanceTreeW(long owlInstance, byte[] fileName);



        //

        //		SaveInstanceTreeS                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeS.html)

        //

        //	This function saves the selected instance and its dependancies in a stream.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeS")]

        public static extern long SaveInstanceTreeS(long owlInstance, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, long size);



        //

        //		SaveInstanceTreeA                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeA.html)

        //

        //	This function saves the selected instance and its dependancies in an array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeA")]

        public static extern long SaveInstanceTreeA(long owlInstance, byte[] content, out long size);



        //

        //		SaveModel                                   (http://rdf.bg/gkdoc/CS64/SaveModel.html)

        //

        //	This function saves the current model on location fileName.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModel")]

        public static extern long SaveModel(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveModel")]

        public static extern long SaveModel(long model, byte[] fileName);



        //

        //		SaveModelW                                  (http://rdf.bg/gkdoc/CS64/SaveModelW.html)

        //

        //	This function saves the current model on location fileName.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelW")]

        public static extern long SaveModelW(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelW")]

        public static extern long SaveModelW(long model, byte[] fileName);



        //

        //		SaveModelS                                  (http://rdf.bg/gkdoc/CS64/SaveModelS.html)

        //

        //	This function saves the current model in a stream.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelS")]

        public static extern long SaveModelS(long model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, long size);



        //

        //		SaveModelA                                  (http://rdf.bg/gkdoc/CS64/SaveModelA.html)

        //

        //	This function saves the current model in an array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelA")]

        public static extern long SaveModelA(long model, byte[] content, out long size);



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

        public static extern void SetOverrideFileIO(long model, long setting, long mask);



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

        public static extern long GetOverrideFileIO(long model, long mask);



        //

        //		CloseModel                                  (http://rdf.bg/gkdoc/CS64/CloseModel.html)

        //

        //	This function closes the model. After this call none of the instances and classes within the model

        //	can be used anymore, also garbage collection is not allowed anymore, in default compilation the

        //	model itself will be known in the kernel, however known to be disabled. Calls containing the model

        //	reference will be protected from crashing when called.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CloseModel")]

        public static extern long CloseModel(long model);



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

        public static extern long CreateClass(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateClass")]

        public static extern long CreateClass(long model, byte[] name);



        //

        //		CreateClassW                                (http://rdf.bg/gkdoc/CS64/CreateClassW.html)

        //

        //	Returns a handle to an on the fly created class.

        //	If the model input is zero or not a model handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreateClassW")]

        public static extern long CreateClassW(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateClassW")]

        public static extern long CreateClassW(long model, byte[] name);



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

        public static extern long GetClassByName(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByName")]

        public static extern long GetClassByName(long model, byte[] name);



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

        public static extern long GetClassByNameW(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByNameW")]

        public static extern long GetClassByNameW(long model, byte[] name);



        //

        //		GetClassesByIterator                        (http://rdf.bg/gkdoc/CS64/GetClassesByIterator.html)

        //

        //	Returns a handle to an class.

        //	If input class is zero, the handle will point to the first relevant class.

        //	If all classes are past (or no relevant classes are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetClassesByIterator")]

        public static extern long GetClassesByIterator(long model, long owlClass);



        //

        //		SetClassParent                              (http://rdf.bg/gkdoc/CS64/SetClassParent.html)

        //

        //	Defines (set/unset) the parent class of a given class. Multiple-inheritence is supported and behavior

        //	of parent classes is also inherited as well as cardinality restrictions on datatype properties and

        //	object properties (relations).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetClassParent")]

        public static extern void SetClassParent(long owlClass, long parentOwlClass, long setting);



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

        public static extern void SetClassParentEx(long model, long owlClass, long parentOwlClass, long setting);



        //

        //		GetParentsByIterator                        (http://rdf.bg/gkdoc/CS64/GetParentsByIterator.html)

        //

        //	Returns the next parent of the class.

        //	If input parent is zero, the handle will point to the first relevant parent.

        //	If all parent are past (or no relevant parent are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetParentsByIterator")]

        public static extern long GetParentsByIterator(long owlClass, long parentOwlClass);



        //

        //		SetNameOfClass                              (http://rdf.bg/gkdoc/CS64/SetNameOfClass.html)

        //

        //	Sets/updates the name of the class, if no error it returns 0.

        //	In case class does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClass")]

        public static extern long SetNameOfClass(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClass")]

        public static extern long SetNameOfClass(long owlClass, byte[] name);



        //

        //		SetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/SetNameOfClassW.html)

        //

        //	Sets/updates the name of the class, if no error it returns 0.

        //	In case class does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassW")]

        public static extern long SetNameOfClassW(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassW")]

        public static extern long SetNameOfClassW(long owlClass, byte[] name);



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

        public static extern long SetNameOfClassEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassEx")]

        public static extern long SetNameOfClassEx(long model, long owlClass, byte[] name);



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

        public static extern long SetNameOfClassWEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassWEx")]

        public static extern long SetNameOfClassWEx(long model, long owlClass, byte[] name);



        //

        //		GetNameOfClass                              (http://rdf.bg/gkdoc/CS64/GetNameOfClass.html)

        //

        //	Returns the name of the class, if the class does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClass")]

        public static extern void GetNameOfClass(long owlClass, out IntPtr name);



        //

        //		GetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/GetNameOfClassW.html)

        //

        //	Returns the name of the class, if the class does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassW")]

        public static extern void GetNameOfClassW(long owlClass, out IntPtr name);



        //

        //		GetNameOfClassEx                            (http://rdf.bg/gkdoc/CS64/GetNameOfClassEx.html)

        //

        //	Returns the name of the class, if the class does not exist it returns nullptr.

        //

        //	This call has the same behavior as GetNameOfClass, however needs to be

        //	used in case properties are exchanged as a successive series of integers.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassEx")]

        public static extern void GetNameOfClassEx(long model, long owlClass, out IntPtr name);



        //

        //		GetNameOfClassWEx                           (http://rdf.bg/gkdoc/CS64/GetNameOfClassWEx.html)

        //

        //	Returns the name of the class, if the class does not exist it returns nullptr.

        //

        //	This call has the same behavior as GetNameOfClassW, however needs to be

        //	used in case classes are exchanged as a successive series of integers.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassWEx")]

        public static extern void GetNameOfClassWEx(long model, long owlClass, out IntPtr name);



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

        public static extern void SetClassPropertyCardinalityRestriction(long owlClass, long rdfProperty, long minCard, long maxCard);



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

        public static extern void SetClassPropertyCardinalityRestrictionEx(long model, long owlClass, long rdfProperty, long minCard, long maxCard);



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

        public static extern void GetClassPropertyCardinalityRestriction(long owlClass, long rdfProperty, out long minCard, out long maxCard);



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

        public static extern void GetClassPropertyCardinalityRestrictionEx(long model, long owlClass, long rdfProperty, out long minCard, out long maxCard);



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

        public static extern long GetGeometryClass(long owlClass);



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

        public static extern long GetGeometryClassEx(long model, long owlClass);



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

        public static extern long CreateProperty(long model, long rdfPropertyType, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateProperty")]

        public static extern long CreateProperty(long model, long rdfPropertyType, byte[] name);



        //

        //		CreatePropertyW                             (http://rdf.bg/gkdoc/CS64/CreatePropertyW.html)

        //

        //	Returns a handle to an on the fly created property.

        //	If the model input is zero or not a model handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreatePropertyW")]

        public static extern long CreatePropertyW(long model, long rdfPropertyType, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreatePropertyW")]

        public static extern long CreatePropertyW(long model, long rdfPropertyType, byte[] name);



        //

        //		GetPropertyByName                           (http://rdf.bg/gkdoc/CS64/GetPropertyByName.html)

        //

        //	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.

        //	When the property does not exist yet and the name is unique

        //	the property will be created on-the-fly and the handle will be returned.

        //	When the name is not unique and given to a class or instance 0 will be returned.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName")]

        public static extern long GetPropertyByName(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName")]

        public static extern long GetPropertyByName(long model, byte[] name);



        //

        //		GetPropertyByNameW                          (http://rdf.bg/gkdoc/CS64/GetPropertyByNameW.html)

        //

        //	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.

        //	When the property does not exist yet and the name is unique

        //	the property will be created on-the-fly and the handle will be returned.

        //	When the name is not unique and given to a class or instance 0 will be returned.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameW")]

        public static extern long GetPropertyByNameW(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameW")]

        public static extern long GetPropertyByNameW(long model, byte[] name);



        //

        //		GetPropertiesByIterator                     (http://rdf.bg/gkdoc/CS64/GetPropertiesByIterator.html)

        //

        //	Returns a handle to a property.

        //	If input property is zero, the handle will point to the first relevant property.

        //	If all properties are past (or no relevant properties are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertiesByIterator")]

        public static extern long GetPropertiesByIterator(long model, long rdfProperty);



        //

        //		GetRangeRestrictionsByIterator              (http://rdf.bg/gkdoc/CS64/GetRangeRestrictionsByIterator.html)

        //

        //	Returns the next class the property is restricted to.

        //	If input class is zero, the handle will point to the first relevant class.

        //	If all classes are past (or no relevant classes are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetRangeRestrictionsByIterator")]

        public static extern long GetRangeRestrictionsByIterator(long rdfProperty, long owlClass);



        //

        //		SetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/SetNameOfProperty.html)

        //

        //	Sets/updates the name of the property, if no error it returns 0.

        //	In case property does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfProperty")]

        public static extern long SetNameOfProperty(long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfProperty")]

        public static extern long SetNameOfProperty(long rdfProperty, byte[] name);



        //

        //		SetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyW.html)

        //

        //	Sets/updates the name of the property, if no error it returns 0.

        //	In case property does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyW")]

        public static extern long SetNameOfPropertyW(long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyW")]

        public static extern long SetNameOfPropertyW(long rdfProperty, byte[] name);



        //

        //		SetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyEx.html)

        //

        //	Sets/updates the name of the property, if no error it returns 0.

        //	In case property does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyEx")]

        public static extern long SetNameOfPropertyEx(long model, long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyEx")]

        public static extern long SetNameOfPropertyEx(long model, long rdfProperty, byte[] name);



        //

        //		SetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyWEx.html)

        //

        //	Sets/updates the name of the property, if no error it returns 0.

        //	In case property does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]

        public static extern long SetNameOfPropertyWEx(long model, long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]

        public static extern long SetNameOfPropertyWEx(long model, long rdfProperty, byte[] name);



        //

        //		GetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/GetNameOfProperty.html)

        //

        //	Returns the name of the property, if the property does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfProperty")]

        public static extern void GetNameOfProperty(long rdfProperty, out IntPtr name);



        //

        //		GetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyW.html)

        //

        //	Returns the name of the property, if the property does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyW")]

        public static extern void GetNameOfPropertyW(long rdfProperty, out IntPtr name);



        //

        //		GetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyEx.html)

        //

        //	Returns the name of the property, if the property does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyEx")]

        public static extern void GetNameOfPropertyEx(long model, long rdfProperty, out IntPtr name);



        //

        //		GetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyWEx.html)

        //

        //	Returns the name of the property, if the property does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyWEx")]

        public static extern void GetNameOfPropertyWEx(long model, long rdfProperty, out IntPtr name);



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

        public static extern long SetPropertyType(long rdfProperty, long propertyType);



        //

        //		SetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/SetPropertyTypeEx.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetPropertyTypeEx")]

        public static extern long SetPropertyTypeEx(long model, long rdfProperty, long propertyType);



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

        public static extern long GetPropertyType(long rdfProperty);



        //

        //		GetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/GetPropertyTypeEx.html)

        //

        //	This call has the same behavior as GetPropertyType, however needs to be

        //	used in case properties are exchanged as a successive series of integers.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyTypeEx")]

        public static extern long GetPropertyTypeEx(long model, long rdfProperty);



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

        public static extern long CreateInstance(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstance")]

        public static extern long CreateInstance(long owlClass, byte[] name);



        //

        //		CreateInstanceW                             (http://rdf.bg/gkdoc/CS64/CreateInstanceW.html)

        //

        //	Returns a handle to an on the fly created instance.

        //	If the owlClass input is zero or not a class handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceW")]

        public static extern long CreateInstanceW(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceW")]

        public static extern long CreateInstanceW(long owlClass, byte[] name);



        //

        //		CreateInstanceEx                            (http://rdf.bg/gkdoc/CS64/CreateInstanceEx.html)

        //

        //	Returns a handle to an on the fly created instance.

        //	If the owlClass input is zero or not a class handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceEx")]

        public static extern long CreateInstanceEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceEx")]

        public static extern long CreateInstanceEx(long model, long owlClass, byte[] name);



        //

        //		CreateInstanceWEx                           (http://rdf.bg/gkdoc/CS64/CreateInstanceWEx.html)

        //

        //	Returns a handle to an on the fly created instance.

        //	If the owlClass input is zero or not a class handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceWEx")]

        public static extern long CreateInstanceWEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceWEx")]

        public static extern long CreateInstanceWEx(long model, long owlClass, byte[] name);



        //

        //		GetInstancesByIterator                      (http://rdf.bg/gkdoc/CS64/GetInstancesByIterator.html)

        //

        //	Returns a handle to an instance.

        //	If input instance is zero, the handle will point to the first relevant instance.

        //	If all instances are past (or no relevant instances are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancesByIterator")]

        public static extern long GetInstancesByIterator(long model, long owlInstance);



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

        public static extern long GetInstanceClass(long owlInstance);



        //

        //		GetInstanceClassEx                          (http://rdf.bg/gkdoc/CS64/GetInstanceClassEx.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceClassEx")]

        public static extern long GetInstanceClassEx(long model, long owlInstance);



        //

        //		GetInstancePropertyByIterator               (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIterator.html)

        //

        //	Returns a handle to the objectTypeProperty or dataTypeProperty connected to

        //	the instance, this property can also contain a value, but for example also

        //	the knowledge about cardinality restrictions in the context of this instance's class

        //	and the exact cardinality in context of its instance.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancePropertyByIterator")]

        public static extern long GetInstancePropertyByIterator(long owlInstance, long rdfProperty);



        //

        //		GetInstancePropertyByIteratorEx             (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIteratorEx.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancePropertyByIteratorEx")]

        public static extern long GetInstancePropertyByIteratorEx(long model, long owlInstance, long rdfProperty);



        //

        //		GetInstanceInverseReferencesByIterator      (http://rdf.bg/gkdoc/CS64/GetInstanceInverseReferencesByIterator.html)

        //

        //	Returns a handle to the owlInstances refering this instance

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceInverseReferencesByIterator")]

        public static extern long GetInstanceInverseReferencesByIterator(long owlInstance, long referencingOwlInstance);



        //

        //		GetInstanceReferencesByIterator             (http://rdf.bg/gkdoc/CS64/GetInstanceReferencesByIterator.html)

        //

        //	Returns a handle to the owlInstance refered by this instance

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceReferencesByIterator")]

        public static extern long GetInstanceReferencesByIterator(long owlInstance, long referencedOwlInstance);



        //

        //		SetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/SetNameOfInstance.html)

        //

        //	Sets/updates the name of the instance, if no error it returns 0.

        //	In case instance does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstance")]

        public static extern long SetNameOfInstance(long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstance")]

        public static extern long SetNameOfInstance(long owlInstance, byte[] name);



        //

        //		SetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceW.html)

        //

        //	Sets/updates the name of the instance, if no error it returns 0.

        //	In case instance does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceW")]

        public static extern long SetNameOfInstanceW(long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceW")]

        public static extern long SetNameOfInstanceW(long owlInstance, byte[] name);



        //

        //		SetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceEx.html)

        //

        //	Sets/updates the name of the instance, if no error it returns 0.

        //	In case instance does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceEx")]

        public static extern long SetNameOfInstanceEx(long model, long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceEx")]

        public static extern long SetNameOfInstanceEx(long model, long owlInstance, byte[] name);



        //

        //		SetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceWEx.html)

        //

        //	Sets/updates the name of the instance, if no error it returns 0.

        //	In case instance does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]

        public static extern long SetNameOfInstanceWEx(long model, long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]

        public static extern long SetNameOfInstanceWEx(long model, long owlInstance, byte[] name);



        //

        //		GetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/GetNameOfInstance.html)

        //

        //	Returns the name of the instance, if the instance does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstance")]

        public static extern void GetNameOfInstance(long owlInstance, out IntPtr name);



        //

        //		GetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceW.html)

        //

        //	Returns the name of the instance, if the instance does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceW")]

        public static extern void GetNameOfInstanceW(long owlInstance, out IntPtr name);



        //

        //		GetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceEx.html)

        //

        //	Returns the name of the instance, if the instance does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceEx")]

        public static extern void GetNameOfInstanceEx(long model, long owlInstance, out IntPtr name);



        //

        //		GetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceWEx.html)

        //

        //	Returns the name of the instance, if the instance does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceWEx")]

        public static extern void GetNameOfInstanceWEx(long model, long owlInstance, out IntPtr name);



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

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref byte values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, byte[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref long values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, long[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref double values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, double[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref string values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, string[] values, long card);



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

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref byte values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, byte[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref long values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, long[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref double values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, double[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref string values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, string[] values, long card);



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

        public static extern long GetDatatypeProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long GetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long SetObjectProperty(long owlInstance, long rdfProperty, ref long values, long card);



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

        public static extern long SetObjectPropertyEx(long model, long owlInstance, long rdfProperty, ref long values, long card);



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

        public static extern long GetObjectProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long GetObjectPropertyEx(long model, long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long CreateInstanceInContextStructure(long owlInstance);



        //

        //		DestroyInstanceInContextStructure           (http://rdf.bg/gkdoc/CS64/DestroyInstanceInContextStructure.html)

        //

        //	InstanceInContext structures are updated dynamically and therfore even while the cost

        //	in performance and memory is limited it is advised to destroy structures as soon

        //	as they are obsolete.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "DestroyInstanceInContextStructure")]

        public static extern void DestroyInstanceInContextStructure(long owlInstanceInContext);



        //

        //		InstanceInContextChild                      (http://rdf.bg/gkdoc/CS64/InstanceInContextChild.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextChild")]

        public static extern long InstanceInContextChild(long owlInstanceInContext);



        //

        //		InstanceInContextNext                       (http://rdf.bg/gkdoc/CS64/InstanceInContextNext.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextNext")]

        public static extern long InstanceInContextNext(long owlInstanceInContext);



        //

        //		InstanceInContextIsUpdated                  (http://rdf.bg/gkdoc/CS64/InstanceInContextIsUpdated.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextIsUpdated")]

        public static extern long InstanceInContextIsUpdated(long owlInstanceInContext);



        //

        //		RemoveInstance                              (http://rdf.bg/gkdoc/CS64/RemoveInstance.html)

        //

        //	This function removes an instance from the internal structure.

        //	In case copies are created by the host this function checks if all

        //	copies are removed otherwise the instance will stay available.

        //	Return value is 0 if everything went ok and positive in case of an error

        //

        [DllImport(IFCEngineDLL, EntryPoint = "RemoveInstance")]

        public static extern long RemoveInstance(long owlInstance);



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

        public static extern long RemoveInstanceRecursively(long owlInstance);



        //

        //		RemoveInstances                             (http://rdf.bg/gkdoc/CS64/RemoveInstances.html)

        //

        //	This function removes all available instances for the given model 

        //	from the internal structure.

        //	Return value is the number of removed instances.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "RemoveInstances")]

        public static extern long RemoveInstances(long model);



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

        public static extern long CalculateInstance(long owlInstance, out long vertexBufferSize, out long indexBufferSize, out long transformationBufferSize);



        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]

        public static extern long CalculateInstance(long owlInstance, out long vertexBufferSize, out long indexBufferSize, IntPtr transformationBufferSize);



        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]

        public static extern long CalculateInstance(long owlInstance, out long vertexBufferSize, IntPtr indexBufferSize, IntPtr transformationBufferSize);



        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]

        public static extern long CalculateInstance(long owlInstance, IntPtr vertexBufferSize, IntPtr indexBufferSize, IntPtr transformationBufferSize);



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

        public static extern long UpdateInstance(long owlInstance);



        //

        //		InferenceInstance                           (http://rdf.bg/gkdoc/CS64/InferenceInstance.html)

        //

        //	This function fills in values that are implicitely known but not given by the user. This function

        //	can also be used to identify default values of properties if not given.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InferenceInstance")]

        public static extern long InferenceInstance(long owlInstance);



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

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, out float vertexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, float[] vertexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, out double vertexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, double[] vertexBuffer);



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

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, out Int32 indexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, Int32[] indexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, out long indexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, long[] indexBuffer);



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

        public static extern long UpdateInstanceTransformationBuffer(long owlInstance, out double transformationBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceTransformationBuffer")]

        public static extern long UpdateInstanceTransformationBuffer(long owlInstance, double[] transformationBuffer);



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

        public static extern void ClearedInstanceExternalBuffers(long owlInstance);



        //

        //		ClearedExternalBuffers                      (http://rdf.bg/gkdoc/CS64/ClearedExternalBuffers.html)

        //

        //	This function tells the engine that all buffers have no memory of earlier filling.

        //	This means that even when buffer content didn't changed it will be updated when

        //	functions UpdateVertexBuffer(), UpdateIndexBuffer() and/or transformationBuffer()

        //	are called.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "ClearedExternalBuffers")]

        public static extern void ClearedExternalBuffers(long model);



        //

        //		GetConceptualFaceCnt                        (http://rdf.bg/gkdoc/CS64/GetConceptualFaceCnt.html)

        //

        //	This function returns the number of conceptual faces for a certain instance.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceCnt")]

        public static extern long GetConceptualFaceCnt(long owlInstance);



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

        public static extern long GetConceptualFace(long owlInstance, long index, out long startIndexTriangles, out long noTriangles);



        //

        //		GetConceptualFaceEx                         (http://rdf.bg/gkdoc/CS64/GetConceptualFaceEx.html)

        //

        //	This function returns a handle to the conceptual face. Be aware that different

        //	instances can return the same handles (however with possible different startIndices and noTriangles).

        //	Argument index should be at least zero and smaller then return value of GetConceptualFaceCnt().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        //

        //		GetConceptualFaceMaterial                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceMaterial.html)

        //

        //	This function returns the material instance relevant for this

        //	conceptual face.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceMaterial")]

        public static extern long GetConceptualFaceMaterial(long conceptualFace);



        //

        //		GetConceptualFaceOriginCnt                  (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginCnt.html)

        //

        //	This function returns the number of instances that are the source primitive/concept

        //	for this conceptual face.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOriginCnt")]

        public static extern long GetConceptualFaceOriginCnt(long conceptualFace);



        //

        //		GetConceptualFaceOrigin                     (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOrigin.html)

        //

        //	This function returns a handle to the instance that is the source primitive/concept

        //	for this conceptual face.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOrigin")]

        public static extern long GetConceptualFaceOrigin(long conceptualFace, long index);



        //

        //		GetConceptualFaceOriginEx                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginEx.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOriginEx")]

        public static extern void GetConceptualFaceOriginEx(long conceptualFace, long index, out long originatingOwlInstance, out long originatingConceptualFace);



        //

        //		GetFaceCnt                                  (http://rdf.bg/gkdoc/CS64/GetFaceCnt.html)

        //

        //	This function returns the number of faces for a certain instance.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetFaceCnt")]

        public static extern long GetFaceCnt(long owlInstance);



        //

        //		GetFace                                     (http://rdf.bg/gkdoc/CS64/GetFace.html)

        //

        //	This function gets the individual faces including the meta data, i.e. the number of openings within this specific face.

        //	This call is for very dedicated use, it would be more common to iterate over the individual conceptual faces.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetFace")]

        public static extern void GetFace(long owlInstance, long index, out long startIndex, out long noOpenings);



        //

        //		GetDependingPropertyCnt                     (http://rdf.bg/gkdoc/CS64/GetDependingPropertyCnt.html)

        //

        //	This function returns the number of properties that are of influence on the

        //	location and form of the conceptualFace.

        //

        //	Note: BE AWARE, THIS FUNCTION EXPECTS A TREE, NOT A NETWORK, IN CASE OF A NETWORK THIS FUNCTION CAN LOCK THE ENGINE

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetDependingPropertyCnt")]

        public static extern long GetDependingPropertyCnt(long baseOwlInstance, long conceptualFace);



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

        public static extern void GetDependingProperty(long baseOwlInstance, long conceptualFace, long index, out long owlInstance, out long datatypeProperty);



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

        //			1	Index items returned as long_t (8 byte/64 bit) (only available in 64 bit mode)

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

        public static extern long SetFormat(long model, long setting, long mask);



        //

        //		GetFormat                                   (http://rdf.bg/gkdoc/CS64/GetFormat.html)

        //

        //	Returns the current format given a mask.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetFormat")]

        public static extern long GetFormat(long model, long mask);



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

        public static extern void SetBehavior(long model, long setting, long mask);



        //

        //		GetBehavior                                 (http://rdf.bg/gkdoc/CS64/GetBehavior.html)

        //

        //	Returns the current behavior given a mask.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetBehavior")]

        public static extern long GetBehavior(long model, long mask);



        //

        //		SetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/SetVertexBufferTransformation.html)

        //

        //	Sets the transformation for a Vertex Buffer.

        //	The transformation will always be calculated in 64 bit, even if the vertex element size is 32 bit.

        //	This function can be called just before updating the vertex buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetVertexBufferTransformation")]

        public static extern void SetVertexBufferTransformation(long model, out double matrix);



        //

        //		GetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/GetVertexBufferTransformation.html)

        //

        //	Gets the transformation for a Vertex Buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetVertexBufferTransformation")]

        public static extern void GetVertexBufferTransformation(long model, out double matrix);



        //

        //		SetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/SetIndexBufferOffset.html)

        //

        //	Sets the offset for an Index Buffer.

        //	It is important call this function before updating the vertex buffer. 

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetIndexBufferOffset")]

        public static extern void SetIndexBufferOffset(long model, long offset);



        //

        //		GetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/GetIndexBufferOffset.html)

        //

        //	Gets the current offset for an Index Buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetIndexBufferOffset")]

        public static extern long GetIndexBufferOffset(long model);



        //

        //		SetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/SetVertexBufferOffset.html)

        //

        //	Sets the offset for a Vertex Buffer.

        //	The offset will always be calculated in 64 bit, even if the vertex element size is 32 bit.

        //	This function can be called just before updating the vertex buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetVertexBufferOffset")]

        public static extern void SetVertexBufferOffset(long model, double x, double y, double z);



        //

        //		GetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/GetVertexBufferOffset.html)

        //

        //	Gets the offset for a Vertex Buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetVertexBufferOffset")]

        public static extern void GetVertexBufferOffset(long model, out double x, out double y, out double z);



        //

        //		SetDefaultColor                             (http://rdf.bg/gkdoc/CS64/SetDefaultColor.html)

        //

        //	Set the default values for the colors defined as argument.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetDefaultColor")]

        public static extern void SetDefaultColor(long model, Int32 ambient, Int32 diffuse, Int32 emissive, Int32 specular);



        //

        //		GetDefaultColor                             (http://rdf.bg/gkdoc/CS64/GetDefaultColor.html)

        //

        //	Retrieve the default values for the colors defined as argument.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetDefaultColor")]

        public static extern void GetDefaultColor(long model, out Int32 ambient, out Int32 diffuse, out Int32 emissive, out Int32 specular);



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

        public static extern long CheckConsistency(long model, long mask);



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

        public static extern long CheckInstanceConsistency(long owlInstance, long mask);



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

        public static extern double GetPerimeter(long owlInstance);



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

        public static extern double GetArea(long owlInstance, ref float vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, ref float vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, ref double vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, ref double vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, IntPtr vertices, IntPtr indices);



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

        public static extern double GetVolume(long owlInstance, ref float vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, ref float vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, ref double vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, ref double vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, IntPtr vertices, IntPtr indices);



        //

        //		GetCentroid                                 (http://rdf.bg/gkdoc/CS64/GetCentroid.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref float vertices, ref Int32 indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref float vertices, ref long indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref double vertices, ref Int32 indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref double vertices, ref long indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, IntPtr vertices, IntPtr indices, out double centroid);



        //

        //		GetConceptualFacePerimeter                  (http://rdf.bg/gkdoc/CS64/GetConceptualFacePerimeter.html)

        //

        //	This function returns the perimeter of a given Conceptual Face.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFacePerimeter")]

        public static extern double GetConceptualFacePerimeter(long conceptualFace);



        //

        //		GetConceptualFaceArea                       (http://rdf.bg/gkdoc/CS64/GetConceptualFaceArea.html)

        //

        //	This function returns the area of a given Conceptual Face. The attributes vertices

        //	and indices are optional but will improve performance if defined.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref float vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref float vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref double vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref double vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, IntPtr vertices, IntPtr indices);



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

        public static extern void SetBoundingBoxReference(long owlInstance, out double transformationMatrix, out double startVector, out double endVector);



        //

        //		GetBoundingBox                              (http://rdf.bg/gkdoc/CS64/GetBoundingBox.html)

        //

        //	When the transformationMatrix is given, it will fill an array of 12 double values.

        //	When the transformationMatrix is left empty and both startVector and endVector are

        //	given the boundingbox without transformation is calculated and returned.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetBoundingBox")]

        public static extern void GetBoundingBox(long owlInstance, out double transformationMatrix, out double startVector, out double endVector);



        [DllImport(IFCEngineDLL, EntryPoint = "GetBoundingBox")]

        public static extern void GetBoundingBox(long owlInstance, IntPtr transformationMatrix, out double startVector, out double endVector);



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

        public static extern void GetRelativeTransformation(long owlInstanceHead, long owlInstanceTail, out double transformationMatrix);



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

        public static extern void GetTriangles(long owlInstance, out long startIndex, out long noTriangles, out long startVertex, out long firstNotUsedVertex);



        //

        //		GetLines                                    (http://rdf.bg/gkdoc/CS64/GetLines___.html)

        //

        //	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant lines when

        //	SetFormat() is setting bit 9 and unsetting with bit 8, 10, 12 and 13 

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetLines")]

        public static extern void GetLines(long owlInstance, out long startIndex, out long noLines, out long startVertex, out long firstNotUsedVertex);



        //

        //		GetPoints                                   (http://rdf.bg/gkdoc/CS64/GetPoints___.html)

        //

        //	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant points when

        //	SetFormat() is setting bit 10 and unsetting with bit 8, 9, 12 and 13 

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPoints")]

        public static extern void GetPoints(long owlInstance, out long startIndex, out long noPoints, out long startVertex, out long firstNotUsedVertex);



        //

        //		GetPropertyRestrictions                     (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictions___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyRestrictions")]

        public static extern void GetPropertyRestrictions(long owlClass, long rdfProperty, out long minCard, out long maxCard);



        //

        //		GetPropertyRestrictionsConsolidated         (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictionsConsolidated___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,

        //	just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyRestrictionsConsolidated")]

        public static extern void GetPropertyRestrictionsConsolidated(long owlClass, long rdfProperty, out long minCard, out long maxCard);



        //

        //		IsGeometryType                              (http://rdf.bg/gkdoc/CS64/IsGeometryType___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetGeometryClass instead, rename the function name

        //	and interpret non-zero as true and zero as false.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "IsGeometryType")]

        public static extern byte IsGeometryType(long owlClass);



        //

        //		SetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/SetObjectTypeProperty___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call SetObjectProperty instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetObjectTypeProperty")]

        public static extern long SetObjectTypeProperty(long owlInstance, long rdfProperty, ref long values, long card);



        //

        //		GetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/GetObjectTypeProperty___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetObjectProperty instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetObjectTypeProperty")]

        public static extern long GetObjectTypeProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



        //

        //		SetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/SetDataTypeProperty___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call SetDatatypeProperty instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref byte values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, byte[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref long values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, long[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref double values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, double[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref string values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]

        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, string[] values, long card);



        //

        //		GetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/GetDataTypeProperty___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetDatatypeProperty instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetDataTypeProperty")]

        public static extern long GetDataTypeProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



        //

        //		InstanceCopyCreated                         (http://rdf.bg/gkdoc/CS64/InstanceCopyCreated___.html)

        //

        //	This call is deprecated as the Copy concept is also deprecated and will be removed by end of 2020.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceCopyCreated")]

        public static extern void InstanceCopyCreated(long owlInstance);



        //

        //		GetPropertyByNameAndType                    (http://rdf.bg/gkdoc/CS64/GetPropertyByNameAndType___.html)

        //

        //	This call is deprecated and will be removed by end of 2020.

        //	Please use the call GetPropertyByName(Ex) / GetPropertyByNameW(Ex) + GetPropertyType(Ex) instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameAndType")]

        public static extern long GetPropertyByNameAndType(long model, string name, long rdfPropertyType);



        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameAndType")]

        public static extern long GetPropertyByNameAndType(long model, byte[] name, long rdfPropertyType);

    }

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

        public static extern long engiGetInstanceMetaInfo(out long instance, out long localId, out IntPtr entityName, out IntPtr entityNameUC);



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

        public static extern void engiGetAggrUnknownElement(out long aggregate, long elementIndex, out long valueType, out long value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(out long aggregate, long elementIndex, out long valueType, out double value);



        [DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrUnknownElement")]

        public static extern void engiGetAggrUnknownElement(out long aggregate, long elementIndex, out long valueType, out IntPtr value);



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

        public static extern long GetRevision(out IntPtr timeStamp);



        //

        //		GetRevisionW                                (http://rdf.bg/gkdoc/CS64/GetRevisionW.html)

        //

        //	Returns the revision number.

        //	The timeStamp is generated by the SVN system used during development.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetRevisionW")]

        public static extern long GetRevisionW(out IntPtr timeStamp);



        //

        //		GetProtection                               (http://rdf.bg/gkdoc/CS64/GetProtection.html)

        //

        //	This call is required to be called to enable the DLL to work if protection is active.

        //

        //	Returns the number of days (incl. this one) that this version is still active or 0 if no protection is embedded.

        //	In case no days are left and protection is active this call will return -1.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetProtection")]

        public static extern long GetProtection();



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

        public static extern long GetEnvironment(out IntPtr environmentVariables, out IntPtr developmentVariables);



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

        public static extern long GetEnvironmentW(out IntPtr environmentVariables, out IntPtr developmentVariables);



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

        public static extern long SetCharacterSerialization(long model, long encoding, long wcharBitSizeOverride, byte ascii);



        //

        //		GetCharacterSerialization                   (http://rdf.bg/gkdoc/CS64/GetCharacterSerialization.html)

        //

        //	This call retrieves the values as set by 

        //

        //	The returns the size of a single character in bits, i.e. 1 byte is 8 bits, this can be 8, 16 or 32 depending on settings and operating system

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetCharacterSerialization")]

        public static extern long GetCharacterSerialization(long model, out long encoding, out byte ascii);



        //

        //		AbortModel                                  (http://rdf.bg/gkdoc/CS64/AbortModel.html)

        //

        //	This function abort running processes for a model. It can be used when a task takes more time than

        //	expected / available, or in case the requested results are not relevant anymore.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "AbortModel")]

        public static extern long AbortModel(long model, long setting);



        //

        //		GetSessionMetaInfo                          (http://rdf.bg/gkdoc/CS64/GetSessionMetaInfo.html)

        //

        //	This function is meant for debugging purposes and return statistics during processing.

        //	The return value represents the number of active models within the session (or zero if the model was not recognized).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetSessionMetaInfo")]

        public static extern long GetSessionMetaInfo(out long allocatedBlocks, out long allocatedBytes, out long nonUsedBlocks, out long nonUsedBytes);



        //

        //		GetModelMetaInfo                            (http://rdf.bg/gkdoc/CS64/GetModelMetaInfo.html)

        //

        //	This function is meant for debugging purposes and return statistics during processing.

        //	The return value represents the number of active models within the session (or zero if the model was not recognized).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, out long activeProperties, out long deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, IntPtr activeClasses, IntPtr deletedClasses, out long activeProperties, out long deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long deletedClasses, IntPtr activeProperties, IntPtr deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long removedClasses, out long activeProperties, out long deletedProperties, IntPtr activeInstances, IntPtr deletedInstances, IntPtr inactiveInstances);



        [DllImport(IFCEngineDLL, EntryPoint = "GetModelMetaInfo")]

        public static extern long GetModelMetaInfo(long model, out long activeClasses, out long removedClasses, out long activeProperties, out long deletedProperties, out long activeInstances, out long deletedInstances, out long inactiveInstances);



        //

        //		GetInstanceMetaInfo                         (http://rdf.bg/gkdoc/CS64/GetInstanceMetaInfo.html)

        //

        //	This function is meant for debugging purposes and return statistics during processing.

        //	The return value represents the number of active instances within the model (or zero if the instance was not recognized).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceMetaInfo")]

        public static extern long GetInstanceMetaInfo(long owlInstance, out long allocatedBlocks, out long allocatedBytes);



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

        public static extern long GetSmoothness(long owlInstance, out long degree);



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

        public static extern void AddState(long model, long owlInstance);



        //

        //		GetModel                                    (http://rdf.bg/gkdoc/CS64/GetModel.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetModel")]

        public static extern long GetModel(long owlInstance);



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

        public static extern void OrderedHandles(long model, out long classCnt, out long propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, out long classCnt, out long propertyCnt, IntPtr instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, out long classCnt, IntPtr propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, out long classCnt, IntPtr propertyCnt, IntPtr instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, out long propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, out long propertyCnt, IntPtr instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, IntPtr propertyCnt, out long instanceCnt, long setting, long mask);



        [DllImport(IFCEngineDLL, EntryPoint = "OrderedHandles")]

        public static extern void OrderedHandles(long model, IntPtr classCnt, IntPtr propertyCnt, IntPtr instanceCnt, long setting, long mask);



        //

        //		PeelArray                                   (http://rdf.bg/gkdoc/CS64/PeelArray.html)

        //

        //	This function introduces functionality that is missing or complicated in some programming languages.

        //	The attribute inValue is a reference to an array of references. The attribute outValue is a reference to the same array,

        //	however a number of elements earlier or further, i.e. number of elements being attribute elementSize. Be aware that as

        //	we are talking about references the offset is depending on 32 bit / 64 bit compilation.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "PeelArray")]

        public static extern void PeelArray(ref byte[] inValue, out byte outValue, long elementSize);



        //

        //		CloseSession                                (http://rdf.bg/gkdoc/CS64/CloseSession.html)

        //

        //	This function closes the session, after this call the geometry kernel cannot be used anymore.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CloseSession")]

        public static extern long CloseSession();



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

        public static extern void ClearCache(long model);



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

        public static extern long CreateModel();



        //

        //		OpenModel                                   (http://rdf.bg/gkdoc/CS64/OpenModel.html)

        //

        //	This function opens the model on location fileName.

        //	References inside to other ontologies will be included.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModel")]

        public static extern long OpenModel(string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "OpenModel")]

        public static extern long OpenModel(byte[] fileName);



        //

        //		OpenModelW                                  (http://rdf.bg/gkdoc/CS64/OpenModelW.html)

        //

        //	This function opens the model on location fileName.

        //	References inside to other ontologies will be included.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelW")]

        public static extern long OpenModelW(string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelW")]

        public static extern long OpenModelW(byte[] fileName);



        //

        //		OpenModelS                                  (http://rdf.bg/gkdoc/CS64/OpenModelS.html)

        //

        //	This function opens the model via a stream.

        //	References inside to other ontologies will be included.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelS")]

        public static extern long OpenModelS([MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback);



        //

        //		OpenModelA                                  (http://rdf.bg/gkdoc/CS64/OpenModelA.html)

        //

        //	This function opens the model via an array.

        //	References inside to other ontologies will be included.

        //	A handle to the model will be returned, or 0 in case something went wrong.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "OpenModelA")]

        public static extern long OpenModelA(byte[] content, long size);



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

        public static extern long ImportModel(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "ImportModel")]

        public static extern long ImportModel(long model, byte[] fileName);



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

        public static extern long ImportModelW(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "ImportModelW")]

        public static extern long ImportModelW(long model, byte[] fileName);



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

        public static extern long ImportModelS(long model, [MarshalAs(UnmanagedType.FunctionPtr)] ReadCallBackFunction callback);



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

        public static extern long ImportModelA(long model, byte[] content, long size);



        //

        //		SaveInstanceTree                            (http://rdf.bg/gkdoc/CS64/SaveInstanceTree.html)

        //

        //	This function saves the selected instance and its dependancies on location fileName.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTree")]

        public static extern long SaveInstanceTree(long owlInstance, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTree")]

        public static extern long SaveInstanceTree(long owlInstance, byte[] fileName);



        //

        //		SaveInstanceTreeW                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeW.html)

        //

        //	This function saves the selected instance and its dependancies on location fileName.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeW")]

        public static extern long SaveInstanceTreeW(long owlInstance, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeW")]

        public static extern long SaveInstanceTreeW(long owlInstance, byte[] fileName);



        //

        //		SaveInstanceTreeS                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeS.html)

        //

        //	This function saves the selected instance and its dependancies in a stream.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeS")]

        public static extern long SaveInstanceTreeS(long owlInstance, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, long size);



        //

        //		SaveInstanceTreeA                           (http://rdf.bg/gkdoc/CS64/SaveInstanceTreeA.html)

        //

        //	This function saves the selected instance and its dependancies in an array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveInstanceTreeA")]

        public static extern long SaveInstanceTreeA(long owlInstance, byte[] content, out long size);



        //

        //		SaveModel                                   (http://rdf.bg/gkdoc/CS64/SaveModel.html)

        //

        //	This function saves the current model on location fileName.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModel")]

        public static extern long SaveModel(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveModel")]

        public static extern long SaveModel(long model, byte[] fileName);



        //

        //		SaveModelW                                  (http://rdf.bg/gkdoc/CS64/SaveModelW.html)

        //

        //	This function saves the current model on location fileName.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelW")]

        public static extern long SaveModelW(long model, string fileName);



        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelW")]

        public static extern long SaveModelW(long model, byte[] fileName);



        //

        //		SaveModelS                                  (http://rdf.bg/gkdoc/CS64/SaveModelS.html)

        //

        //	This function saves the current model in a stream.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelS")]

        public static extern long SaveModelS(long model, [MarshalAs(UnmanagedType.FunctionPtr)] WriteCallBackFunction callback, long size);



        //

        //		SaveModelA                                  (http://rdf.bg/gkdoc/CS64/SaveModelA.html)

        //

        //	This function saves the current model in an array.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SaveModelA")]

        public static extern long SaveModelA(long model, byte[] content, out long size);



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

        public static extern void SetOverrideFileIO(long model, long setting, long mask);



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

        public static extern long GetOverrideFileIO(long model, long mask);



        //

        //		CloseModel                                  (http://rdf.bg/gkdoc/CS64/CloseModel.html)

        //

        //	This function closes the model. After this call none of the instances and classes within the model

        //	can be used anymore, also garbage collection is not allowed anymore, in default compilation the

        //	model itself will be known in the kernel, however known to be disabled. Calls containing the model

        //	reference will be protected from crashing when called.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CloseModel")]

        public static extern long CloseModel(long model);



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

        public static extern long CreateClass(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateClass")]

        public static extern long CreateClass(long model, byte[] name);



        //

        //		CreateClassW                                (http://rdf.bg/gkdoc/CS64/CreateClassW.html)

        //

        //	Returns a handle to an on the fly created class.

        //	If the model input is zero or not a model handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreateClassW")]

        public static extern long CreateClassW(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateClassW")]

        public static extern long CreateClassW(long model, byte[] name);



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

        public static extern long GetClassByName(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByName")]

        public static extern long GetClassByName(long model, byte[] name);



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

        public static extern long GetClassByNameW(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetClassByNameW")]

        public static extern long GetClassByNameW(long model, byte[] name);



        //

        //		GetClassesByIterator                        (http://rdf.bg/gkdoc/CS64/GetClassesByIterator.html)

        //

        //	Returns a handle to an class.

        //	If input class is zero, the handle will point to the first relevant class.

        //	If all classes are past (or no relevant classes are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetClassesByIterator")]

        public static extern long GetClassesByIterator(long model, long owlClass);



        //

        //		SetClassParent                              (http://rdf.bg/gkdoc/CS64/SetClassParent.html)

        //

        //	Defines (set/unset) the parent class of a given class. Multiple-inheritence is supported and behavior

        //	of parent classes is also inherited as well as cardinality restrictions on datatype properties and

        //	object properties (relations).

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetClassParent")]

        public static extern void SetClassParent(long owlClass, long parentOwlClass, long setting);



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

        public static extern void SetClassParentEx(long model, long owlClass, long parentOwlClass, long setting);



        //

        //		GetParentsByIterator                        (http://rdf.bg/gkdoc/CS64/GetParentsByIterator.html)

        //

        //	Returns the next parent of the class.

        //	If input parent is zero, the handle will point to the first relevant parent.

        //	If all parent are past (or no relevant parent are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetParentsByIterator")]

        public static extern long GetParentsByIterator(long owlClass, long parentOwlClass);



        //

        //		SetNameOfClass                              (http://rdf.bg/gkdoc/CS64/SetNameOfClass.html)

        //

        //	Sets/updates the name of the class, if no error it returns 0.

        //	In case class does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClass")]

        public static extern long SetNameOfClass(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClass")]

        public static extern long SetNameOfClass(long owlClass, byte[] name);



        //

        //		SetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/SetNameOfClassW.html)

        //

        //	Sets/updates the name of the class, if no error it returns 0.

        //	In case class does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassW")]

        public static extern long SetNameOfClassW(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassW")]

        public static extern long SetNameOfClassW(long owlClass, byte[] name);



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

        public static extern long SetNameOfClassEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassEx")]

        public static extern long SetNameOfClassEx(long model, long owlClass, byte[] name);



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

        public static extern long SetNameOfClassWEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfClassWEx")]

        public static extern long SetNameOfClassWEx(long model, long owlClass, byte[] name);



        //

        //		GetNameOfClass                              (http://rdf.bg/gkdoc/CS64/GetNameOfClass.html)

        //

        //	Returns the name of the class, if the class does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClass")]

        public static extern void GetNameOfClass(long owlClass, out IntPtr name);



        //

        //		GetNameOfClassW                             (http://rdf.bg/gkdoc/CS64/GetNameOfClassW.html)

        //

        //	Returns the name of the class, if the class does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassW")]

        public static extern void GetNameOfClassW(long owlClass, out IntPtr name);



        //

        //		GetNameOfClassEx                            (http://rdf.bg/gkdoc/CS64/GetNameOfClassEx.html)

        //

        //	Returns the name of the class, if the class does not exist it returns nullptr.

        //

        //	This call has the same behavior as GetNameOfClass, however needs to be

        //	used in case properties are exchanged as a successive series of integers.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassEx")]

        public static extern void GetNameOfClassEx(long model, long owlClass, out IntPtr name);



        //

        //		GetNameOfClassWEx                           (http://rdf.bg/gkdoc/CS64/GetNameOfClassWEx.html)

        //

        //	Returns the name of the class, if the class does not exist it returns nullptr.

        //

        //	This call has the same behavior as GetNameOfClassW, however needs to be

        //	used in case classes are exchanged as a successive series of integers.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfClassWEx")]

        public static extern void GetNameOfClassWEx(long model, long owlClass, out IntPtr name);



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

        public static extern void SetClassPropertyCardinalityRestriction(long owlClass, long rdfProperty, long minCard, long maxCard);



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

        public static extern void SetClassPropertyCardinalityRestrictionEx(long model, long owlClass, long rdfProperty, long minCard, long maxCard);



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

        public static extern void GetClassPropertyCardinalityRestriction(long owlClass, long rdfProperty, out long minCard, out long maxCard);



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

        public static extern void GetClassPropertyCardinalityRestrictionEx(long model, long owlClass, long rdfProperty, out long minCard, out long maxCard);



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

        public static extern long GetGeometryClass(long owlClass);



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

        public static extern long GetGeometryClassEx(long model, long owlClass);



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

        public static extern long CreateProperty(long model, long rdfPropertyType, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateProperty")]

        public static extern long CreateProperty(long model, long rdfPropertyType, byte[] name);



        //

        //		CreatePropertyW                             (http://rdf.bg/gkdoc/CS64/CreatePropertyW.html)

        //

        //	Returns a handle to an on the fly created property.

        //	If the model input is zero or not a model handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreatePropertyW")]

        public static extern long CreatePropertyW(long model, long rdfPropertyType, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreatePropertyW")]

        public static extern long CreatePropertyW(long model, long rdfPropertyType, byte[] name);



        //

        //		GetPropertyByName                           (http://rdf.bg/gkdoc/CS64/GetPropertyByName.html)

        //

        //	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.

        //	When the property does not exist yet and the name is unique

        //	the property will be created on-the-fly and the handle will be returned.

        //	When the name is not unique and given to a class or instance 0 will be returned.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName")]

        public static extern long GetPropertyByName(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByName")]

        public static extern long GetPropertyByName(long model, byte[] name);



        //

        //		GetPropertyByNameW                          (http://rdf.bg/gkdoc/CS64/GetPropertyByNameW.html)

        //

        //	Returns a handle to the objectTypeProperty or dataTypeProperty as stored inside.

        //	When the property does not exist yet and the name is unique

        //	the property will be created on-the-fly and the handle will be returned.

        //	When the name is not unique and given to a class or instance 0 will be returned.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameW")]

        public static extern long GetPropertyByNameW(long model, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameW")]

        public static extern long GetPropertyByNameW(long model, byte[] name);



        //

        //		GetPropertiesByIterator                     (http://rdf.bg/gkdoc/CS64/GetPropertiesByIterator.html)

        //

        //	Returns a handle to a property.

        //	If input property is zero, the handle will point to the first relevant property.

        //	If all properties are past (or no relevant properties are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertiesByIterator")]

        public static extern long GetPropertiesByIterator(long model, long rdfProperty);



        //

        //		GetRangeRestrictionsByIterator              (http://rdf.bg/gkdoc/CS64/GetRangeRestrictionsByIterator.html)

        //

        //	Returns the next class the property is restricted to.

        //	If input class is zero, the handle will point to the first relevant class.

        //	If all classes are past (or no relevant classes are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetRangeRestrictionsByIterator")]

        public static extern long GetRangeRestrictionsByIterator(long rdfProperty, long owlClass);



        //

        //		SetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/SetNameOfProperty.html)

        //

        //	Sets/updates the name of the property, if no error it returns 0.

        //	In case property does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfProperty")]

        public static extern long SetNameOfProperty(long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfProperty")]

        public static extern long SetNameOfProperty(long rdfProperty, byte[] name);



        //

        //		SetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyW.html)

        //

        //	Sets/updates the name of the property, if no error it returns 0.

        //	In case property does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyW")]

        public static extern long SetNameOfPropertyW(long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyW")]

        public static extern long SetNameOfPropertyW(long rdfProperty, byte[] name);



        //

        //		SetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyEx.html)

        //

        //	Sets/updates the name of the property, if no error it returns 0.

        //	In case property does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyEx")]

        public static extern long SetNameOfPropertyEx(long model, long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyEx")]

        public static extern long SetNameOfPropertyEx(long model, long rdfProperty, byte[] name);



        //

        //		SetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfPropertyWEx.html)

        //

        //	Sets/updates the name of the property, if no error it returns 0.

        //	In case property does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]

        public static extern long SetNameOfPropertyWEx(long model, long rdfProperty, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfPropertyWEx")]

        public static extern long SetNameOfPropertyWEx(long model, long rdfProperty, byte[] name);



        //

        //		GetNameOfProperty                           (http://rdf.bg/gkdoc/CS64/GetNameOfProperty.html)

        //

        //	Returns the name of the property, if the property does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfProperty")]

        public static extern void GetNameOfProperty(long rdfProperty, out IntPtr name);



        //

        //		GetNameOfPropertyW                          (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyW.html)

        //

        //	Returns the name of the property, if the property does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyW")]

        public static extern void GetNameOfPropertyW(long rdfProperty, out IntPtr name);



        //

        //		GetNameOfPropertyEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyEx.html)

        //

        //	Returns the name of the property, if the property does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyEx")]

        public static extern void GetNameOfPropertyEx(long model, long rdfProperty, out IntPtr name);



        //

        //		GetNameOfPropertyWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfPropertyWEx.html)

        //

        //	Returns the name of the property, if the property does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfPropertyWEx")]

        public static extern void GetNameOfPropertyWEx(long model, long rdfProperty, out IntPtr name);



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

        public static extern long SetPropertyType(long rdfProperty, long propertyType);



        //

        //		SetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/SetPropertyTypeEx.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetPropertyTypeEx")]

        public static extern long SetPropertyTypeEx(long model, long rdfProperty, long propertyType);



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

        public static extern long GetPropertyType(long rdfProperty);



        //

        //		GetPropertyTypeEx                           (http://rdf.bg/gkdoc/CS64/GetPropertyTypeEx.html)

        //

        //	This call has the same behavior as GetPropertyType, however needs to be

        //	used in case properties are exchanged as a successive series of integers.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyTypeEx")]

        public static extern long GetPropertyTypeEx(long model, long rdfProperty);



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

        public static extern long CreateInstance(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstance")]

        public static extern long CreateInstance(long owlClass, byte[] name);



        //

        //		CreateInstanceW                             (http://rdf.bg/gkdoc/CS64/CreateInstanceW.html)

        //

        //	Returns a handle to an on the fly created instance.

        //	If the owlClass input is zero or not a class handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceW")]

        public static extern long CreateInstanceW(long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceW")]

        public static extern long CreateInstanceW(long owlClass, byte[] name);



        //

        //		CreateInstanceEx                            (http://rdf.bg/gkdoc/CS64/CreateInstanceEx.html)

        //

        //	Returns a handle to an on the fly created instance.

        //	If the owlClass input is zero or not a class handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceEx")]

        public static extern long CreateInstanceEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceEx")]

        public static extern long CreateInstanceEx(long model, long owlClass, byte[] name);



        //

        //		CreateInstanceWEx                           (http://rdf.bg/gkdoc/CS64/CreateInstanceWEx.html)

        //

        //	Returns a handle to an on the fly created instance.

        //	If the owlClass input is zero or not a class handle 0 will be returned,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceWEx")]

        public static extern long CreateInstanceWEx(long model, long owlClass, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "CreateInstanceWEx")]

        public static extern long CreateInstanceWEx(long model, long owlClass, byte[] name);



        //

        //		GetInstancesByIterator                      (http://rdf.bg/gkdoc/CS64/GetInstancesByIterator.html)

        //

        //	Returns a handle to an instance.

        //	If input instance is zero, the handle will point to the first relevant instance.

        //	If all instances are past (or no relevant instances are found), the function will return 0.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancesByIterator")]

        public static extern long GetInstancesByIterator(long model, long owlInstance);



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

        public static extern long GetInstanceClass(long owlInstance);



        //

        //		GetInstanceClassEx                          (http://rdf.bg/gkdoc/CS64/GetInstanceClassEx.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceClassEx")]

        public static extern long GetInstanceClassEx(long model, long owlInstance);



        //

        //		GetInstancePropertyByIterator               (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIterator.html)

        //

        //	Returns a handle to the objectTypeProperty or dataTypeProperty connected to

        //	the instance, this property can also contain a value, but for example also

        //	the knowledge about cardinality restrictions in the context of this instance's class

        //	and the exact cardinality in context of its instance.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancePropertyByIterator")]

        public static extern long GetInstancePropertyByIterator(long owlInstance, long rdfProperty);



        //

        //		GetInstancePropertyByIteratorEx             (http://rdf.bg/gkdoc/CS64/GetInstancePropertyByIteratorEx.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstancePropertyByIteratorEx")]

        public static extern long GetInstancePropertyByIteratorEx(long model, long owlInstance, long rdfProperty);



        //

        //		GetInstanceInverseReferencesByIterator      (http://rdf.bg/gkdoc/CS64/GetInstanceInverseReferencesByIterator.html)

        //

        //	Returns a handle to the owlInstances refering this instance

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceInverseReferencesByIterator")]

        public static extern long GetInstanceInverseReferencesByIterator(long owlInstance, long referencingOwlInstance);



        //

        //		GetInstanceReferencesByIterator             (http://rdf.bg/gkdoc/CS64/GetInstanceReferencesByIterator.html)

        //

        //	Returns a handle to the owlInstance refered by this instance

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetInstanceReferencesByIterator")]

        public static extern long GetInstanceReferencesByIterator(long owlInstance, long referencedOwlInstance);



        //

        //		SetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/SetNameOfInstance.html)

        //

        //	Sets/updates the name of the instance, if no error it returns 0.

        //	In case instance does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstance")]

        public static extern long SetNameOfInstance(long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstance")]

        public static extern long SetNameOfInstance(long owlInstance, byte[] name);



        //

        //		SetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceW.html)

        //

        //	Sets/updates the name of the instance, if no error it returns 0.

        //	In case instance does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceW")]

        public static extern long SetNameOfInstanceW(long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceW")]

        public static extern long SetNameOfInstanceW(long owlInstance, byte[] name);



        //

        //		SetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceEx.html)

        //

        //	Sets/updates the name of the instance, if no error it returns 0.

        //	In case instance does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceEx")]

        public static extern long SetNameOfInstanceEx(long model, long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceEx")]

        public static extern long SetNameOfInstanceEx(long model, long owlInstance, byte[] name);



        //

        //		SetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/SetNameOfInstanceWEx.html)

        //

        //	Sets/updates the name of the instance, if no error it returns 0.

        //	In case instance does not exist it returns 1, when name cannot be updated 2.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]

        public static extern long SetNameOfInstanceWEx(long model, long owlInstance, string name);



        [DllImport(IFCEngineDLL, EntryPoint = "SetNameOfInstanceWEx")]

        public static extern long SetNameOfInstanceWEx(long model, long owlInstance, byte[] name);



        //

        //		GetNameOfInstance                           (http://rdf.bg/gkdoc/CS64/GetNameOfInstance.html)

        //

        //	Returns the name of the instance, if the instance does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstance")]

        public static extern void GetNameOfInstance(long owlInstance, out IntPtr name);



        //

        //		GetNameOfInstanceW                          (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceW.html)

        //

        //	Returns the name of the instance, if the instance does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceW")]

        public static extern void GetNameOfInstanceW(long owlInstance, out IntPtr name);



        //

        //		GetNameOfInstanceEx                         (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceEx.html)

        //

        //	Returns the name of the instance, if the instance does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceEx")]

        public static extern void GetNameOfInstanceEx(long model, long owlInstance, out IntPtr name);



        //

        //		GetNameOfInstanceWEx                        (http://rdf.bg/gkdoc/CS64/GetNameOfInstanceWEx.html)

        //

        //	Returns the name of the instance, if the instance does not exist it returns nullptr.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetNameOfInstanceWEx")]

        public static extern void GetNameOfInstanceWEx(long model, long owlInstance, out IntPtr name);



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

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref byte values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, byte[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref long values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, long[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref double values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, double[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, ref string values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypeProperty")]

        public static extern long SetDatatypeProperty(long owlInstance, long rdfProperty, string[] values, long card);



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

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref byte values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, byte[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref long values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, long[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref double values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, double[] values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, ref string values, long card);



        [DllImport(IFCEngineDLL, EntryPoint = "SetDatatypePropertyEx")]

        public static extern long SetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, string[] values, long card);



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

        public static extern long GetDatatypeProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long GetDatatypePropertyEx(long model, long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long SetObjectProperty(long owlInstance, long rdfProperty, ref long values, long card);



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

        public static extern long SetObjectPropertyEx(long model, long owlInstance, long rdfProperty, ref long values, long card);



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

        public static extern long GetObjectProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long GetObjectPropertyEx(long model, long owlInstance, long rdfProperty, out IntPtr values, out long card);



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

        public static extern long CreateInstanceInContextStructure(long owlInstance);



        //

        //		DestroyInstanceInContextStructure           (http://rdf.bg/gkdoc/CS64/DestroyInstanceInContextStructure.html)

        //

        //	InstanceInContext structures are updated dynamically and therfore even while the cost

        //	in performance and memory is limited it is advised to destroy structures as soon

        //	as they are obsolete.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "DestroyInstanceInContextStructure")]

        public static extern void DestroyInstanceInContextStructure(long owlInstanceInContext);



        //

        //		InstanceInContextChild                      (http://rdf.bg/gkdoc/CS64/InstanceInContextChild.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextChild")]

        public static extern long InstanceInContextChild(long owlInstanceInContext);



        //

        //		InstanceInContextNext                       (http://rdf.bg/gkdoc/CS64/InstanceInContextNext.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextNext")]

        public static extern long InstanceInContextNext(long owlInstanceInContext);



        //

        //		InstanceInContextIsUpdated                  (http://rdf.bg/gkdoc/CS64/InstanceInContextIsUpdated.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceInContextIsUpdated")]

        public static extern long InstanceInContextIsUpdated(long owlInstanceInContext);



        //

        //		RemoveInstance                              (http://rdf.bg/gkdoc/CS64/RemoveInstance.html)

        //

        //	This function removes an instance from the internal structure.

        //	In case copies are created by the host this function checks if all

        //	copies are removed otherwise the instance will stay available.

        //	Return value is 0 if everything went ok and positive in case of an error

        //

        [DllImport(IFCEngineDLL, EntryPoint = "RemoveInstance")]

        public static extern long RemoveInstance(long owlInstance);



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

        public static extern long RemoveInstanceRecursively(long owlInstance);



        //

        //		RemoveInstances                             (http://rdf.bg/gkdoc/CS64/RemoveInstances.html)

        //

        //	This function removes all available instances for the given model 

        //	from the internal structure.

        //	Return value is the number of removed instances.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "RemoveInstances")]

        public static extern long RemoveInstances(long model);



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

        public static extern long CalculateInstance(long owlInstance, out long vertexBufferSize, out long indexBufferSize, out long transformationBufferSize);



        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]

        public static extern long CalculateInstance(long owlInstance, out long vertexBufferSize, out long indexBufferSize, IntPtr transformationBufferSize);



        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]

        public static extern long CalculateInstance(long owlInstance, out long vertexBufferSize, IntPtr indexBufferSize, IntPtr transformationBufferSize);



        [DllImport(IFCEngineDLL, EntryPoint = "CalculateInstance")]

        public static extern long CalculateInstance(long owlInstance, IntPtr vertexBufferSize, IntPtr indexBufferSize, IntPtr transformationBufferSize);



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

        public static extern long UpdateInstance(long owlInstance);



        //

        //		InferenceInstance                           (http://rdf.bg/gkdoc/CS64/InferenceInstance.html)

        //

        //	This function fills in values that are implicitely known but not given by the user. This function

        //	can also be used to identify default values of properties if not given.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InferenceInstance")]

        public static extern long InferenceInstance(long owlInstance);



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

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, out float vertexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, float[] vertexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, out double vertexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceVertexBuffer")]

        public static extern long UpdateInstanceVertexBuffer(long owlInstance, double[] vertexBuffer);



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

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, out Int32 indexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, Int32[] indexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, out long indexBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceIndexBuffer")]

        public static extern long UpdateInstanceIndexBuffer(long owlInstance, long[] indexBuffer);



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

        public static extern long UpdateInstanceTransformationBuffer(long owlInstance, out double transformationBuffer);



        [DllImport(IFCEngineDLL, EntryPoint = "UpdateInstanceTransformationBuffer")]

        public static extern long UpdateInstanceTransformationBuffer(long owlInstance, double[] transformationBuffer);



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

        public static extern void ClearedInstanceExternalBuffers(long owlInstance);



        //

        //		ClearedExternalBuffers                      (http://rdf.bg/gkdoc/CS64/ClearedExternalBuffers.html)

        //

        //	This function tells the engine that all buffers have no memory of earlier filling.

        //	This means that even when buffer content didn't changed it will be updated when

        //	functions UpdateVertexBuffer(), UpdateIndexBuffer() and/or transformationBuffer()

        //	are called.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "ClearedExternalBuffers")]

        public static extern void ClearedExternalBuffers(long model);



        //

        //		GetConceptualFaceCnt                        (http://rdf.bg/gkdoc/CS64/GetConceptualFaceCnt.html)

        //

        //	This function returns the number of conceptual faces for a certain instance.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceCnt")]

        public static extern long GetConceptualFaceCnt(long owlInstance);



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

        public static extern long GetConceptualFace(long owlInstance, long index, out long startIndexTriangles, out long noTriangles);



        //

        //		GetConceptualFaceEx                         (http://rdf.bg/gkdoc/CS64/GetConceptualFaceEx.html)

        //

        //	This function returns a handle to the conceptual face. Be aware that different

        //	instances can return the same handles (however with possible different startIndices and noTriangles).

        //	Argument index should be at least zero and smaller then return value of GetConceptualFaceCnt().

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, out long startIndexTriangles, out long noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, out long startIndexLines, out long noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, out long startIndexPoints, out long noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, out long startIndexFacePolygons, out long noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, out long startIndexConceptualFacePolygons, out long noIndicesConceptualFacePolygons);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceEx")]

        public static extern long GetConceptualFaceEx(long owlInstance, long index, IntPtr startIndexTriangles, IntPtr noIndicesTriangles, IntPtr startIndexLines, IntPtr noIndicesLines, IntPtr startIndexPoints, IntPtr noIndicesPoints, IntPtr startIndexFacePolygons, IntPtr noIndicesFacePolygons, IntPtr startIndexConceptualFacePolygons, IntPtr noIndicesConceptualFacePolygons);



        //

        //		GetConceptualFaceMaterial                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceMaterial.html)

        //

        //	This function returns the material instance relevant for this

        //	conceptual face.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceMaterial")]

        public static extern long GetConceptualFaceMaterial(long conceptualFace);



        //

        //		GetConceptualFaceOriginCnt                  (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginCnt.html)

        //

        //	This function returns the number of instances that are the source primitive/concept

        //	for this conceptual face.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOriginCnt")]

        public static extern long GetConceptualFaceOriginCnt(long conceptualFace);



        //

        //		GetConceptualFaceOrigin                     (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOrigin.html)

        //

        //	This function returns a handle to the instance that is the source primitive/concept

        //	for this conceptual face.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOrigin")]

        public static extern long GetConceptualFaceOrigin(long conceptualFace, long index);



        //

        //		GetConceptualFaceOriginEx                   (http://rdf.bg/gkdoc/CS64/GetConceptualFaceOriginEx.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceOriginEx")]

        public static extern void GetConceptualFaceOriginEx(long conceptualFace, long index, out long originatingOwlInstance, out long originatingConceptualFace);



        //

        //		GetFaceCnt                                  (http://rdf.bg/gkdoc/CS64/GetFaceCnt.html)

        //

        //	This function returns the number of faces for a certain instance.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetFaceCnt")]

        public static extern long GetFaceCnt(long owlInstance);



        //

        //		GetFace                                     (http://rdf.bg/gkdoc/CS64/GetFace.html)

        //

        //	This function gets the individual faces including the meta data, i.e. the number of openings within this specific face.

        //	This call is for very dedicated use, it would be more common to iterate over the individual conceptual faces.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetFace")]

        public static extern void GetFace(long owlInstance, long index, out long startIndex, out long noOpenings);



        //

        //		GetDependingPropertyCnt                     (http://rdf.bg/gkdoc/CS64/GetDependingPropertyCnt.html)

        //

        //	This function returns the number of properties that are of influence on the

        //	location and form of the conceptualFace.

        //

        //	Note: BE AWARE, THIS FUNCTION EXPECTS A TREE, NOT A NETWORK, IN CASE OF A NETWORK THIS FUNCTION CAN LOCK THE ENGINE

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetDependingPropertyCnt")]

        public static extern long GetDependingPropertyCnt(long baseOwlInstance, long conceptualFace);



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

        public static extern void GetDependingProperty(long baseOwlInstance, long conceptualFace, long index, out long owlInstance, out long datatypeProperty);



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

        //			1	Index items returned as long_t (8 byte/64 bit) (only available in 64 bit mode)

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

        public static extern long SetFormat(long model, long setting, long mask);



        //

        //		GetFormat                                   (http://rdf.bg/gkdoc/CS64/GetFormat.html)

        //

        //	Returns the current format given a mask.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetFormat")]

        public static extern long GetFormat(long model, long mask);



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

        public static extern void SetBehavior(long model, long setting, long mask);



        //

        //		GetBehavior                                 (http://rdf.bg/gkdoc/CS64/GetBehavior.html)

        //

        //	Returns the current behavior given a mask.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetBehavior")]

        public static extern long GetBehavior(long model, long mask);



        //

        //		SetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/SetVertexBufferTransformation.html)

        //

        //	Sets the transformation for a Vertex Buffer.

        //	The transformation will always be calculated in 64 bit, even if the vertex element size is 32 bit.

        //	This function can be called just before updating the vertex buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetVertexBufferTransformation")]

        public static extern void SetVertexBufferTransformation(long model, out double matrix);



        //

        //		GetVertexBufferTransformation               (http://rdf.bg/gkdoc/CS64/GetVertexBufferTransformation.html)

        //

        //	Gets the transformation for a Vertex Buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetVertexBufferTransformation")]

        public static extern void GetVertexBufferTransformation(long model, out double matrix);



        //

        //		SetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/SetIndexBufferOffset.html)

        //

        //	Sets the offset for an Index Buffer.

        //	It is important call this function before updating the vertex buffer. 

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetIndexBufferOffset")]

        public static extern void SetIndexBufferOffset(long model, long offset);



        //

        //		GetIndexBufferOffset                        (http://rdf.bg/gkdoc/CS64/GetIndexBufferOffset.html)

        //

        //	Gets the current offset for an Index Buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetIndexBufferOffset")]

        public static extern long GetIndexBufferOffset(long model);



        //

        //		SetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/SetVertexBufferOffset.html)

        //

        //	Sets the offset for a Vertex Buffer.

        //	The offset will always be calculated in 64 bit, even if the vertex element size is 32 bit.

        //	This function can be called just before updating the vertex buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetVertexBufferOffset")]

        public static extern void SetVertexBufferOffset(long model, double x, double y, double z);



        //

        //		GetVertexBufferOffset                       (http://rdf.bg/gkdoc/CS64/GetVertexBufferOffset.html)

        //

        //	Gets the offset for a Vertex Buffer.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetVertexBufferOffset")]

        public static extern void GetVertexBufferOffset(long model, out double x, out double y, out double z);



        //

        //		SetDefaultColor                             (http://rdf.bg/gkdoc/CS64/SetDefaultColor.html)

        //

        //	Set the default values for the colors defined as argument.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetDefaultColor")]

        public static extern void SetDefaultColor(long model, Int32 ambient, Int32 diffuse, Int32 emissive, Int32 specular);



        //

        //		GetDefaultColor                             (http://rdf.bg/gkdoc/CS64/GetDefaultColor.html)

        //

        //	Retrieve the default values for the colors defined as argument.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetDefaultColor")]

        public static extern void GetDefaultColor(long model, out Int32 ambient, out Int32 diffuse, out Int32 emissive, out Int32 specular);



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

        public static extern long CheckConsistency(long model, long mask);



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

        public static extern long CheckInstanceConsistency(long owlInstance, long mask);



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

        public static extern double GetPerimeter(long owlInstance);



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

        public static extern double GetArea(long owlInstance, ref float vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, ref float vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, ref double vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, ref double vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetArea")]

        public static extern double GetArea(long owlInstance, IntPtr vertices, IntPtr indices);



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

        public static extern double GetVolume(long owlInstance, ref float vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, ref float vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, ref double vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, ref double vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetVolume")]

        public static extern double GetVolume(long owlInstance, IntPtr vertices, IntPtr indices);



        //

        //		GetCentroid                                 (http://rdf.bg/gkdoc/CS64/GetCentroid.html)

        //

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref float vertices, ref Int32 indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref float vertices, ref long indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref double vertices, ref Int32 indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, ref double vertices, ref long indices, out double centroid);



        [DllImport(IFCEngineDLL, EntryPoint = "GetCentroid")]

        public static extern double GetCentroid(long owlInstance, IntPtr vertices, IntPtr indices, out double centroid);



        //

        //		GetConceptualFacePerimeter                  (http://rdf.bg/gkdoc/CS64/GetConceptualFacePerimeter.html)

        //

        //	This function returns the perimeter of a given Conceptual Face.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFacePerimeter")]

        public static extern double GetConceptualFacePerimeter(long conceptualFace);



        //

        //		GetConceptualFaceArea                       (http://rdf.bg/gkdoc/CS64/GetConceptualFaceArea.html)

        //

        //	This function returns the area of a given Conceptual Face. The attributes vertices

        //	and indices are optional but will improve performance if defined.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref float vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref float vertices, ref long indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref double vertices, ref Int32 indices);



        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]

        public static extern double GetConceptualFaceArea(long conceptualFace, ref double vertices, ref long indices);


        [DllImport(IFCEngineDLL, EntryPoint = "GetConceptualFaceArea")]
        public static extern double GetConceptualFaceArea(long conceptualFace, IntPtr vertices, IntPtr indices);



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
        public static extern void SetBoundingBoxReference(long owlInstance, out double transformationMatrix, out double startVector, out double endVector);



        //

        //		GetBoundingBox                              (http://rdf.bg/gkdoc/CS64/GetBoundingBox.html)

        //

        //	When the transformationMatrix is given, it will fill an array of 12 double values.

        //	When the transformationMatrix is left empty and both startVector and endVector are

        //	given the boundingbox without transformation is calculated and returned.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetBoundingBox")]
        public static extern void GetBoundingBox(long owlInstance, out double transformationMatrix, out double startVector, out double endVector);

        [DllImport(IFCEngineDLL, EntryPoint = "GetBoundingBox")]
        public static extern void GetBoundingBox(long owlInstance, IntPtr transformationMatrix, out double startVector, out double endVector);



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
        public static extern void GetRelativeTransformation(long owlInstanceHead, long owlInstanceTail, out double transformationMatrix);



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
        public static extern void GetTriangles(long owlInstance, out long startIndex, out long noTriangles, out long startVertex, out long firstNotUsedVertex);



        //

        //		GetLines                                    (http://rdf.bg/gkdoc/CS64/GetLines___.html)

        //

        //	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant lines when

        //	SetFormat() is setting bit 9 and unsetting with bit 8, 10, 12 and 13 

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetLines")]
        public static extern void GetLines(long owlInstance, out long startIndex, out long noLines, out long startVertex, out long firstNotUsedVertex);



        //

        //		GetPoints                                   (http://rdf.bg/gkdoc/CS64/GetPoints___.html)

        //

        //	This call is deprecated as it became trivial and will be removed by end of 2020. The result from CalculateInstance exclusively exists of the relevant points when

        //	SetFormat() is setting bit 10 and unsetting with bit 8, 9, 12 and 13 

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPoints")]
        public static extern void GetPoints(long owlInstance, out long startIndex, out long noPoints, out long startVertex, out long firstNotUsedVertex);



        //

        //		GetPropertyRestrictions                     (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictions___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyRestrictions")]
        public static extern void GetPropertyRestrictions(long owlClass, long rdfProperty, out long minCard, out long maxCard);



        //

        //		GetPropertyRestrictionsConsolidated         (http://rdf.bg/gkdoc/CS64/GetPropertyRestrictionsConsolidated___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetClassPropertyCardinalityRestriction instead,

        //	just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyRestrictionsConsolidated")]
        public static extern void GetPropertyRestrictionsConsolidated(long owlClass, long rdfProperty, out long minCard, out long maxCard);



        //

        //		IsGeometryType                              (http://rdf.bg/gkdoc/CS64/IsGeometryType___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetGeometryClass instead, rename the function name

        //	and interpret non-zero as true and zero as false.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "IsGeometryType")]
        public static extern byte IsGeometryType(long owlClass);



        //

        //		SetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/SetObjectTypeProperty___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call SetObjectProperty instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetObjectTypeProperty")]
        public static extern long SetObjectTypeProperty(long owlInstance, long rdfProperty, ref long values, long card);



        //

        //		GetObjectTypeProperty                       (http://rdf.bg/gkdoc/CS64/GetObjectTypeProperty___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetObjectProperty instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetObjectTypeProperty")]
        public static extern long GetObjectTypeProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



        //

        //		SetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/SetDataTypeProperty___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call SetDatatypeProperty instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref byte values, long card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, byte[] values, long card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref long values, long card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, long[] values, long card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref double values, long card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, double[] values, long card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, ref string values, long card);

        [DllImport(IFCEngineDLL, EntryPoint = "SetDataTypeProperty")]
        public static extern long SetDataTypeProperty(long owlInstance, long rdfProperty, string[] values, long card);



        //

        //		GetDataTypeProperty                         (http://rdf.bg/gkdoc/CS64/GetDataTypeProperty___.html)

        //

        //	This call is deprecated and will be removed by end of 2020. Please use the call GetDatatypeProperty instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetDataTypeProperty")]
        public static extern long GetDataTypeProperty(long owlInstance, long rdfProperty, out IntPtr values, out long card);



        //

        //		InstanceCopyCreated                         (http://rdf.bg/gkdoc/CS64/InstanceCopyCreated___.html)

        //

        //	This call is deprecated as the Copy concept is also deprecated and will be removed by end of 2020.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "InstanceCopyCreated")]
        public static extern void InstanceCopyCreated(long owlInstance);



        //

        //		GetPropertyByNameAndType                    (http://rdf.bg/gkdoc/CS64/GetPropertyByNameAndType___.html)

        //

        //	This call is deprecated and will be removed by end of 2020.

        //	Please use the call GetPropertyByName(Ex) / GetPropertyByNameW(Ex) + GetPropertyType(Ex) instead, just rename the function name.

        //

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameAndType")]
        public static extern long GetPropertyByNameAndType(long model, string name, long rdfPropertyType);

        [DllImport(IFCEngineDLL, EntryPoint = "GetPropertyByNameAndType")]
        public static extern long GetPropertyByNameAndType(long model, byte[] name, long rdfPropertyType);
    }
}
