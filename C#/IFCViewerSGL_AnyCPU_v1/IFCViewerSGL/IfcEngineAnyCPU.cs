using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace IfcEngine
{
    /// <summary>
    /// https://social.msdn.microsoft.com/Forums/en-US/200e01d5-bc2b-4688-92af-570688636b7c/dllimport-using-dynamic-file-path?forum=csharplanguage
    /// </summary>
    class IfcEngineAnyCPU
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

        #endregion // Constants

        #region Fields

        #endregion // Fields

        #region Native

        /// <summary>
        /// LoadLibraryEx
        /// </summary>
        /// <param name="dllFilePath"></param>
        /// <param name="hFile"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllFilePath);

        /// <summary>
        /// FreeLibrary
        /// </summary>
        /// <param name="dllPointer"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public extern static bool FreeLibrary(IntPtr dllPointer);

        /// <summary>
        /// GetProcAddress
        /// </summary>
        /// <param name="dllPointer"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr GetProcAddress(IntPtr dllPointer, string functionName);

        #endregion // Native

        #region Common

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="dllFilePath"></param>
        /// <returns></returns>
        public static void Init(string rootPath)
        {
            var dllFilePath = rootPath;
            dllFilePath += Environment.Is64BitProcess ? "x64" : "x86";
            dllFilePath += "\\ifcengine.dll";

            IntPtr moduleHandle = LoadLibrary(dllFilePath);
            if (moduleHandle == IntPtr.Zero)
            {
                int errorCode = Marshal.GetLastWin32Error();

                throw new ApplicationException(string.Format("Can't load ifcengine.dll: {0}, error: {1}", dllFilePath, errorCode));
            }

            IfcEngineDll = moduleHandle;
        }

        /// <summary>
        /// Handle
        /// </summary>
        public static IntPtr IfcEngineDll
        {
            get;
            private set;
        }

        #endregion // Common

        #region Helpers

        /// <summary>
        /// double => IntPtr
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static IntPtr ToPtr(double val)
        {
            IntPtr ptr = Marshal.AllocHGlobal(sizeof(double));

            byte[] byteDouble = BitConverter.GetBytes(val);
            Marshal.Copy(byteDouble, 0, ptr, byteDouble.Length);

            return ptr;
        }

        /// <summary>
        /// int => IntPtr
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static IntPtr ToPtr(int val)
        {
            IntPtr ptr = Marshal.AllocHGlobal(sizeof(int));

            byte[] byteVal = BitConverter.GetBytes(val);
            Marshal.Copy(byteVal, 0, ptr, byteVal.Length);

            return ptr;
        }

        /// <summary>
        /// IntPtr => long
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static long ToLong(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                byte[] bytes = new byte[sizeof(long)];

                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = Marshal.ReadByte(ptr, i);

                long value = BitConverter.ToInt64(bytes, 0);

                return value;
            }

            return 0;
        }

        /// <summary>
        /// IntPtr => int
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static int ToInt(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                byte[] bytes = new byte[sizeof(int)];

                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = Marshal.ReadByte(ptr, i);

                int value = BitConverter.ToInt32(bytes, 0);

                return value;
            }

            return 0;
        }

        /// <summary>
        /// IntPtr => double
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static double ToDouble(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                byte[] bytes = new byte[sizeof(double)];

                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = Marshal.ReadByte(ptr, i);

                double value = BitConverter.ToDouble(bytes, 0);

                return value;
            }

            return 0;
        }

        #endregion // Helpers

        //
        //  Calls for File IO 
        //

        public delegate void sdaiCloseModelDelegate(IntPtr model);
        public static void sdaiCloseModel(long model)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiCloseModel");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiCloseModelDelegate delegateForFunction = (sdaiCloseModelDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiCloseModelDelegate));

            delegateForFunction(new IntPtr(model));
        }

        public delegate IntPtr sdaiCreateModelBNUnicodeDelegate(IntPtr repository, IntPtr fileName, IntPtr schemaName);
        public static long sdaiCreateModelBNUnicode(long repository, string fileName, string schemaName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiCreateModelBNUnicode");
            Debug.Assert(procAddress != IntPtr.Zero);

            var ptrFileName = Marshal.StringToCoTaskMemAuto(fileName);
            var ptrSchemaName = Marshal.StringToCoTaskMemAuto(schemaName);

            sdaiCreateModelBNUnicodeDelegate delegateForFunction = (sdaiCreateModelBNUnicodeDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiCreateModelBNUnicodeDelegate));

            var model = delegateForFunction(new IntPtr(repository), ptrFileName, ptrSchemaName).ToInt64();

            Marshal.FreeCoTaskMem(ptrFileName);
            Marshal.FreeCoTaskMem(ptrSchemaName);

            return model;
        }

        public delegate IntPtr sdaiOpenModelBNUnicodeDelegate(IntPtr repository, IntPtr fileName, IntPtr schemaName);
        public static long sdaiOpenModelBNUnicode(long repository, string fileName, string schemaName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiOpenModelBNUnicode");
            Debug.Assert(procAddress != IntPtr.Zero);

            var ptrFileName = Marshal.StringToCoTaskMemAuto(fileName);
            var ptrSchemaName = Marshal.StringToCoTaskMemAuto(schemaName);

            sdaiOpenModelBNUnicodeDelegate delegateForFunction = (sdaiOpenModelBNUnicodeDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiOpenModelBNUnicodeDelegate));

            var model = delegateForFunction(new IntPtr(repository), ptrFileName, ptrSchemaName).ToInt64();

            Marshal.FreeCoTaskMem(ptrFileName);
            Marshal.FreeCoTaskMem(ptrSchemaName);

            return model;
        }

        public delegate void sdaiSaveModelBNUnicodeDelegate(IntPtr model, IntPtr fileName);
        public static void sdaiSaveModelBNUnicode(long model, string fileName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiSaveModelBNUnicode");
            Debug.Assert(procAddress != IntPtr.Zero);

            var ptrFileName = Marshal.StringToCoTaskMemAuto(fileName);

            sdaiSaveModelBNUnicodeDelegate delegateForFunction = (sdaiSaveModelBNUnicodeDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiSaveModelBNUnicodeDelegate));

            delegateForFunction(new IntPtr(model), ptrFileName);

            Marshal.FreeCoTaskMem(ptrFileName);
        }

        public delegate void sdaiSaveModelAsXmlBNUnicodeDelegate(IntPtr model, IntPtr fileName);
        public static void sdaiSaveModelAsXmlBNUnicode(long model, string fileName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiSaveModelAsXmlBNUnicode");
            Debug.Assert(procAddress != IntPtr.Zero);

            var ptrFileName = Marshal.StringToCoTaskMemAuto(fileName);

            sdaiSaveModelAsXmlBNUnicodeDelegate delegateForFunction = (sdaiSaveModelAsXmlBNUnicodeDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiSaveModelAsXmlBNUnicodeDelegate));

            delegateForFunction(new IntPtr(model), ptrFileName);

            Marshal.FreeCoTaskMem(ptrFileName);
        }

        public delegate void sdaiSaveModelAsSimpleXmlBNUnicodeDelegate(IntPtr model, IntPtr fileName);
        public static void sdaiSaveModelAsSimpleXmlBNUnicode(long model, string fileName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiSaveModelAsSimpleXmlBNUnicode");
            Debug.Assert(procAddress != IntPtr.Zero);

            var ptrFileName = Marshal.StringToCoTaskMemAuto(fileName);

            sdaiSaveModelAsSimpleXmlBNUnicodeDelegate delegateForFunction = (sdaiSaveModelAsSimpleXmlBNUnicodeDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiSaveModelAsSimpleXmlBNUnicodeDelegate));

            delegateForFunction(new IntPtr(model), ptrFileName);

            Marshal.FreeCoTaskMem(ptrFileName);
        }


        public delegate void cleanMemoryDelegate(IntPtr model, IntPtr fileName);
        public static void cleanMemory(long model, long mode)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "cleanMemory");
            Debug.Assert(procAddress != IntPtr.Zero);

            cleanMemoryDelegate delegateForFunction = (cleanMemoryDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(cleanMemoryDelegate));

            delegateForFunction(new IntPtr(model), new IntPtr(mode));
        }

        //
        //  Schema Reading
        //

        public delegate IntPtr sdaiGetEntityDelegate(IntPtr model, IntPtr entityName);
        public static long sdaiGetEntity(long model, string entityName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetEntity");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetEntityDelegate delegateForFunction = (sdaiGetEntityDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetEntityDelegate));

            var ptrEntityName = Marshal.StringToCoTaskMemAuto(entityName);

            var entity = delegateForFunction(new IntPtr(model), ptrEntityName).ToInt64();

            Marshal.FreeCoTaskMem(ptrEntityName);

            return entity;
        }

        //[DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentName")]
        //public static extern void engiGetEntityArgumentName(int_t entity, int_t index, int_t valueType, out IntPtr argumentName);

        //[DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityArgumentType")]
        //public static extern void engiGetEntityArgumentType(int_t entity, int_t index, ref int_t argumentType);

        public delegate IntPtr engiGetEntityCountDelegate(IntPtr model);
        public static long engiGetEntityCount(long model)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "engiGetEntityCount");
            Debug.Assert(procAddress != IntPtr.Zero);

            engiGetEntityCountDelegate delegateForFunction = (engiGetEntityCountDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(engiGetEntityCountDelegate));

            return delegateForFunction(new IntPtr(model)).ToInt64();
        }

        public delegate IntPtr engiGetEntityElementDelegate(IntPtr model, IntPtr index);
        public static long engiGetEntityElement(long model, long index)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "engiGetEntityElement");
            Debug.Assert(procAddress != IntPtr.Zero);

            engiGetEntityElementDelegate delegateForFunction = (engiGetEntityElementDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(engiGetEntityElementDelegate));

            return delegateForFunction(new IntPtr(model), new IntPtr(index)).ToInt64();
        }
        
        public delegate IntPtr sdaiGetEntityExtentDelegate(IntPtr model, IntPtr entity);
        public static long sdaiGetEntityExtent(long model, long entity)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetEntityExtent");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetEntityExtentDelegate delegateForFunction = (sdaiGetEntityExtentDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetEntityExtentDelegate));

            return delegateForFunction(new IntPtr(model), new IntPtr(entity)).ToInt64();
        }
        
        public delegate IntPtr sdaiGetEntityExtentBNDelegate(IntPtr model, IntPtr entityName);
        public static long sdaiGetEntityExtentBN(long model, string entityName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetEntityExtentBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            var ptrEntityName = Marshal.StringToCoTaskMemAuto(entityName);

            sdaiGetEntityExtentBNDelegate delegateForFunction = (sdaiGetEntityExtentBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetEntityExtentBNDelegate));

            var entity = delegateForFunction(new IntPtr(model), ptrEntityName).ToInt64();

            Marshal.FreeCoTaskMem(ptrEntityName);

            return entity;
        }

        public delegate void engiGetEntityNameDelegate(IntPtr entity, IntPtr valueType, out IntPtr entityName);
        public static void engiGetEntityName(long entity, long valueType, out IntPtr entityName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "engiGetEntityName");
            Debug.Assert(procAddress != IntPtr.Zero);

            engiGetEntityNameDelegate delegateForFunction = (engiGetEntityNameDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(engiGetEntityNameDelegate));

            delegateForFunction(new IntPtr(entity), new IntPtr(valueType), out entityName);
        }

        //[DllImport(IFCEngineDLL, EntryPoint = "engiGetEntityNoArguments")]
        //public static extern int_t engiGetEntityNoArguments(int_t entity);
        
        public delegate IntPtr engiGetEntityParentDelegate(IntPtr entity);
        public static long engiGetEntityParent(long entity)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "engiGetEntityParent");
            Debug.Assert(procAddress != IntPtr.Zero);

            engiGetEntityParentDelegate delegateForFunction = (engiGetEntityParentDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(engiGetEntityParentDelegate));

            return delegateForFunction(new IntPtr(entity)).ToInt64();
        }


        //
        //  Instance Header
        //
        

        public delegate IntPtr GetSPFFHeaderItemDelegate(IntPtr model, IntPtr itemIndex, IntPtr itemSubIndex, IntPtr valueType, out IntPtr value);
        public static long GetSPFFHeaderItem(long model, long itemIndex, long itemSubIndex, long valueType, out IntPtr value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "GetSPFFHeaderItem");
            Debug.Assert(procAddress != IntPtr.Zero);

            GetSPFFHeaderItemDelegate delegateForFunction = (GetSPFFHeaderItemDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(GetSPFFHeaderItemDelegate));

            return delegateForFunction(new IntPtr(model), new IntPtr(itemIndex), new IntPtr(itemSubIndex), new IntPtr(valueType), out value).ToInt64();
        }
        
        public delegate void SetSPFFHeaderDelegate(IntPtr model, IntPtr description, IntPtr implementationLevel, IntPtr name, IntPtr timeStamp, IntPtr author, IntPtr organization, IntPtr preprocessorVersion, IntPtr originatingSystem, IntPtr authorization, IntPtr fileSchema);
        public static void SetSPFFHeader(long model, string description, string implementationLevel, string name, string timeStamp, string author, string organization, string preprocessorVersion, string originatingSystem, string authorization, string fileSchema)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "SetSPFFHeader");
            Debug.Assert(procAddress != IntPtr.Zero);

            var ptrDescription = Marshal.StringToCoTaskMemAuto(description);
            var ptrImplementationLevel = Marshal.StringToCoTaskMemAuto(implementationLevel);
            var ptrName = Marshal.StringToCoTaskMemAuto(name);
            var ptrTimeStamp = Marshal.StringToCoTaskMemAuto(timeStamp);
            var ptrAuthor = Marshal.StringToCoTaskMemAuto(author);
            var ptrOrganization = Marshal.StringToCoTaskMemAuto(organization);
            var ptrPreprocessorVersion = Marshal.StringToCoTaskMemAuto(preprocessorVersion);
            var ptrOriginatingSystem = Marshal.StringToCoTaskMemAuto(originatingSystem);
            var ptrAuthorization = Marshal.StringToCoTaskMemAuto(authorization);
            var ptrFileSchema = Marshal.StringToCoTaskMemAuto(fileSchema);

            SetSPFFHeaderDelegate delegateForFunction = (SetSPFFHeaderDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(SetSPFFHeaderDelegate));

            delegateForFunction(new IntPtr(model), ptrDescription, ptrImplementationLevel, ptrName, ptrTimeStamp, ptrAuthor, ptrOrganization, ptrPreprocessorVersion, ptrOriginatingSystem, ptrAuthorization, ptrFileSchema);

            Marshal.FreeCoTaskMem(ptrDescription);
        }

        public delegate void SetSPFFHeaderItemDelegate(IntPtr model, IntPtr itemIndex, IntPtr itemSubIndex, IntPtr valueType, string value);
        public static void SetSPFFHeaderItem(long model, long itemIndex, long itemSubIndex, long valueType, string value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "SetSPFFHeaderItem");
            Debug.Assert(procAddress != IntPtr.Zero);           

            SetSPFFHeaderItemDelegate delegateForFunction = (SetSPFFHeaderItemDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(SetSPFFHeaderItemDelegate));

            delegateForFunction(new IntPtr(model), new IntPtr(itemIndex), new IntPtr(itemSubIndex), new IntPtr(valueType), value);
        }


        //
        //  Instance Reading
        //


        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBType")]
        //public static extern int_t sdaiGetADBType(int_t ADB);

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiGetADBTypePathx")]
        //public static extern void sdaiGetADBTypePath(int_t ADB, int_t typeNameNumber, out IntPtr path);

        public delegate void sdaiGetADBValueDelegate(IntPtr ADB, IntPtr valueType, out IntPtr value);
        public static void sdaiGetADBValue(long ADB, long valueType, out long value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetADBValue");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetADBValueDelegate delegateForFunction = (sdaiGetADBValueDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetADBValueDelegate));

            IntPtr ptrValue;
            delegateForFunction(new IntPtr(ADB), new IntPtr(valueType), out ptrValue);

            value = ptrValue.ToInt64();
        }

        public static void sdaiGetADBValue(long ADB, long valueType, out double value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetADBValue");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetADBValueDelegate delegateForFunction = (sdaiGetADBValueDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetADBValueDelegate));

            IntPtr ptrValue;
            delegateForFunction(new IntPtr(ADB), new IntPtr(valueType), out ptrValue);

            value = ptrValue.ToInt64();
        } 

        public delegate void engiGetAggrElementDelegate(IntPtr aggregate, IntPtr elementIndex, IntPtr valueType, out IntPtr value);
        public static void engiGetAggrElement(long aggregate, long elementIndex, long valueType, out long value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "engiGetAggrElement");
            Debug.Assert(procAddress != IntPtr.Zero);

            engiGetAggrElementDelegate delegateForFunction = (engiGetAggrElementDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(engiGetAggrElementDelegate));

            IntPtr ptrValue;
            delegateForFunction(new IntPtr(aggregate), new IntPtr(elementIndex), new IntPtr(valueType), out ptrValue);

            value = ptrValue.ToInt64();
        }

        //[DllImport(IFCEngineDLL, EntryPoint = "engiGetAggrType")]
        //public static extern void engiGetAggrType(int_t aggregate, ref int_t aggragateType);

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        //public static extern int_t sdaiGetAttr(int_t instance, int_t attribute, int_t valueType, out int_t value);

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        //public static extern int_t sdaiGetAttr(int_t instance, int_t attribute, int_t valueType, out double value);

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttr")]
        //public static extern int_t sdaiGetAttr(int_t instance, int_t attribute, int_t valueType, out IntPtr value);

        public delegate void sdaiGetAttrBNDelegate(IntPtr instance, IntPtr attributeName, IntPtr valueType, out IntPtr value);
        public static void sdaiGetAttrBN(long instance, string attributeName, long valueType, out long value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetAttrBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetAttrBNDelegate delegateForFunction = (sdaiGetAttrBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetAttrBNDelegate));

            var ptrAttributeName = Marshal.StringToCoTaskMemAuto(attributeName);

            IntPtr ptrValue;
            delegateForFunction(new IntPtr(instance), ptrAttributeName, new IntPtr(valueType), out ptrValue);

            Marshal.FreeCoTaskMem(ptrAttributeName);

            value = ptrValue.ToInt64();
        }

        public static void sdaiGetAttrBN(long instance, string attributeName, long valueType, out int value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetAttrBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetAttrBNDelegate delegateForFunction = (sdaiGetAttrBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetAttrBNDelegate));

            var ptrAttributeName = Marshal.StringToCoTaskMemAuto(attributeName);

            IntPtr ptrValue;
            delegateForFunction(new IntPtr(instance), ptrAttributeName, new IntPtr(valueType), out ptrValue);

            Marshal.FreeCoTaskMem(ptrAttributeName);

            value = ToInt(ptrValue);
        }        

        public static void sdaiGetAttrBN(long instance, string attributeName, long valueType, out double value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetAttrBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetAttrBNDelegate delegateForFunction = (sdaiGetAttrBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetAttrBNDelegate));

            var ptrAttributeName = Marshal.StringToCoTaskMemAuto(attributeName);

            IntPtr ptrValue;
            delegateForFunction(new IntPtr(instance), ptrAttributeName, new IntPtr(valueType), out ptrValue);

            Marshal.FreeCoTaskMem(ptrAttributeName);

            value = ToDouble(ptrValue);
        }

        public static void sdaiGetAttrBN(long instance, string attributeName, long valueType, out IntPtr value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetAttrBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetAttrBNDelegate delegateForFunction = (sdaiGetAttrBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetAttrBNDelegate));

            var ptrAttributeName = Marshal.StringToCoTaskMemAuto(attributeName);

            delegateForFunction(new IntPtr(instance), ptrAttributeName, new IntPtr(valueType), out value);

            Marshal.FreeCoTaskMem(ptrAttributeName);
        }

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        //public static extern int_t sdaiGetAttrDefinition(int_t entity, string attributeName);

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiGetAttrDefinition")]
        //public static extern int_t sdaiGetAttrDefinition(int_t entity, byte[] attributeName); 

        public delegate IntPtr sdaiGetInstanceTypeDelegate(IntPtr instance);
        public static long sdaiGetInstanceType(long instance)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetInstanceType");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetInstanceTypeDelegate delegateForFunction = (sdaiGetInstanceTypeDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetInstanceTypeDelegate));

            return delegateForFunction(new IntPtr(instance)).ToInt64();
        } 

        public delegate IntPtr sdaiGetMemberCountDelegate(IntPtr aggregate);
        public static long sdaiGetMemberCount(long aggregate)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiGetMemberCount");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiGetMemberCountDelegate delegateForFunction = (sdaiGetMemberCountDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiGetMemberCountDelegate));

            return delegateForFunction(new IntPtr(aggregate)).ToInt64();
        }

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiIsKindOf")]
        //public static extern int_t sdaiIsKindOf(int_t instance, int_t entity);


        //
        //  Instance Writing
        //
        

        public delegate void sdaiAppendDelegate(IntPtr aggregate, IntPtr valueType, IntPtr value);
        public static void sdaiAppend(long aggregate, long valueType, long value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiAppend");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiAppendDelegate delegateForFunction = (sdaiAppendDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiAppendDelegate));

            delegateForFunction(new IntPtr(aggregate), new IntPtr(valueType), new IntPtr(value));
        }

        public static void sdaiAppend(long aggregate, long valueType, int value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiAppend");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiAppendDelegate delegateForFunction = (sdaiAppendDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiAppendDelegate));

            IntPtr ptrInt = ToPtr(value);

            delegateForFunction(new IntPtr(aggregate), new IntPtr(valueType), ptrInt);

            Marshal.FreeHGlobal(ptrInt);
        }

        public static void sdaiAppend(long aggregate, long valueType, double value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiAppend");
            Debug.Assert(procAddress != IntPtr.Zero);
            
            sdaiAppendDelegate delegateForFunction = (sdaiAppendDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiAppendDelegate));

            IntPtr ptrDouble = ToPtr(value);

            delegateForFunction(new IntPtr(aggregate), new IntPtr(valueType), ptrDouble);

            Marshal.FreeHGlobal(ptrDouble);
        }

        public static void sdaiAppend(long aggregate, long valueType, string value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiAppend");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiAppendDelegate delegateForFunction = (sdaiAppendDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiAppendDelegate));

            var ptrValue = Marshal.StringToCoTaskMemAuto(value);

            delegateForFunction(new IntPtr(aggregate), new IntPtr(valueType), ptrValue);

            Marshal.FreeCoTaskMem(ptrValue);
        }

        public delegate void sdaiCreateADBDelegate(IntPtr aggregate, IntPtr value);
        public static void sdaiCreateADB(long aggregate, long value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiCreateADB");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiCreateADBDelegate delegateForFunction = (sdaiCreateADBDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiCreateADBDelegate));

            delegateForFunction(new IntPtr(aggregate), new IntPtr(value));
        }

        public static void sdaiCreateADB(long aggregate, int value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiCreateADB");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiCreateADBDelegate delegateForFunction = (sdaiCreateADBDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiCreateADBDelegate));

            IntPtr ptrInt = ToPtr(value);

            delegateForFunction(new IntPtr(aggregate), ptrInt);

            Marshal.FreeHGlobal(ptrInt);
        }

        public static void sdaiCreateADB(long aggregate, double value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiCreateADB");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiCreateADBDelegate delegateForFunction = (sdaiCreateADBDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiCreateADBDelegate));

            IntPtr ptrDouble = ToPtr(value);

            delegateForFunction(new IntPtr(aggregate), ptrDouble);

            Marshal.FreeHGlobal(ptrDouble);
        }

        public static void sdaiCreateADB(long aggregate, string value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiCreateADB");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiCreateADBDelegate delegateForFunction = (sdaiCreateADBDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiCreateADBDelegate));

            var ptrValue = Marshal.StringToCoTaskMemAuto(value);

            delegateForFunction(new IntPtr(aggregate), ptrValue);

            Marshal.FreeCoTaskMem(ptrValue);
        }

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateAggr")]
        //public static extern int_t sdaiCreateAggr(int_t instance, int_t attribute);

        public delegate void sdaiCreateAggrBNDelegate(IntPtr instance, IntPtr attributeName);
        public static void sdaiCreateAggrBN(long instance, string attributeName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiCreateAggrBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiCreateAggrBNDelegate delegateForFunction = (sdaiCreateAggrBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiCreateAggrBNDelegate));

            var ptrAttributeName = Marshal.StringToCoTaskMemAuto(attributeName);

            delegateForFunction(new IntPtr(instance), ptrAttributeName);

            Marshal.FreeCoTaskMem(ptrAttributeName);
        }

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiCreateInstance")]
        //public static extern int_t sdaiCreateInstance(int_t model, int_t entity);

        public delegate IntPtr sdaiCreateInstanceBNDelegate(IntPtr model, string entityName);
        public static long sdaiCreateInstanceBN(long model, string entityName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiCreateInstanceBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiCreateInstanceBNDelegate delegateForFunction = (sdaiCreateInstanceBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiCreateInstanceBNDelegate));

            long result = delegateForFunction(new IntPtr(model), entityName).ToInt64();

            return result;
        }

        //[DllImport(IFCEngineDLL, EntryPoint = "sdaiDeleteInstance")]
        //public static extern void sdaiDeleteInstance(int_t instance);

        public delegate void sdaiPutADBTypePathDelegate(IntPtr ADB, IntPtr pathCount, IntPtr path);
        public static void sdaiPutADBTypePath(long ADB, long pathCount, string path)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiPutADBTypePath");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiPutADBTypePathDelegate delegateForFunction = (sdaiPutADBTypePathDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiPutADBTypePathDelegate));

            var ptrPath = Marshal.StringToCoTaskMemAuto(path);

            delegateForFunction(new IntPtr(ADB), new IntPtr(pathCount), ptrPath);

            Marshal.FreeCoTaskMem(ptrPath);
        }        

        public delegate void sdaiPutAttrBNDelegate(IntPtr instance, string attributeName, IntPtr valueType, IntPtr value);        

        public static void sdaiPutAttrBN(long instance, string attributeName, long valueType, long value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiPutAttrBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiPutAttrBNDelegate delegateForFunction = (sdaiPutAttrBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiPutAttrBNDelegate));

            delegateForFunction(new IntPtr(instance), attributeName, new IntPtr(valueType), new IntPtr(value));
        }

        public static void sdaiPutAttrBN(long instance, string attributeName, long valueType, ref long value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiPutAttrBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiPutAttrBNDelegate delegateForFunction = (sdaiPutAttrBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiPutAttrBNDelegate));

            var ptrValue = ToPtr(value);

            delegateForFunction(new IntPtr(instance), attributeName, new IntPtr(valueType), ptrValue);

            Marshal.FreeHGlobal(ptrValue);
        }

        public static void sdaiPutAttrBN(long instance, string attributeName, long valueType, ref double value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiPutAttrBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiPutAttrBNDelegate delegateForFunction = (sdaiPutAttrBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiPutAttrBNDelegate));

            var ptrValue = ToPtr(value);

            delegateForFunction(new IntPtr(instance), attributeName, new IntPtr(valueType), ptrValue);

            Marshal.FreeHGlobal(ptrValue);
        }

        public static void sdaiPutAttrBN(long instance, string attributeName, long valueType, string value)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "sdaiPutAttrBN");
            Debug.Assert(procAddress != IntPtr.Zero);

            sdaiPutAttrBNDelegate delegateForFunction = (sdaiPutAttrBNDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(sdaiPutAttrBNDelegate));

            var ptrValue = Marshal.StringToCoTaskMemAuto(value);

            delegateForFunction(new IntPtr(instance), attributeName, new IntPtr(valueType), ptrValue);

            Marshal.FreeHGlobal(ptrValue);
        }     

        //[DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        //public static extern void engiSetComment(int_t instance, string comment);

        //[DllImport(IFCEngineDLL, EntryPoint = "engiSetComment")]
        //public static extern void engiSetComment(int_t instance, byte[] comment);


        //
        //  Controling Calls
        //


        //[DllImport(IFCEngineDLL, EntryPoint = "circleSegments")]
        //public static extern void circleSegments(int_t circles, int_t smallCircles);

        //[DllImport(IFCEngineDLL, EntryPoint = "cleanMemory")]
        //public static extern void cleanMemory(int_t model, int_t mode);        

        public delegate IntPtr internalGetP21LineDelegate(IntPtr instance);
        public static long internalGetP21Line(long instance)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "internalGetP21Line");
            Debug.Assert(procAddress != IntPtr.Zero);

            internalGetP21LineDelegate delegateForFunction = (internalGetP21LineDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(internalGetP21LineDelegate));

            return delegateForFunction(new IntPtr(instance)).ToInt64();
        }        

        public delegate IntPtr setStringUnicodeDelegate(IntPtr unicode);
        public static IntPtr setStringUnicode(long unicode)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "setStringUnicode");
            Debug.Assert(procAddress != IntPtr.Zero);

            setStringUnicodeDelegate delegateForFunction = (setStringUnicodeDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(setStringUnicodeDelegate));

            return delegateForFunction(new IntPtr(unicode));
        }

        public delegate IntPtr getTimeStampDelegate(IntPtr unicode);
        public static long getTimeStamp(long model)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "getTimeStamp");
            Debug.Assert(procAddress != IntPtr.Zero);

            getTimeStampDelegate delegateForFunction = (getTimeStampDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(getTimeStampDelegate));

            return delegateForFunction(new IntPtr(model)).ToInt64();
        }


        //
        //  Geometry Interaction
        //


        //[DllImport(IFCEngineDLL, EntryPoint = "initializeModellingInstance")]
        //public static extern int_t initializeModellingInstance(int_t model, ref int_t noVertices, ref int_t noIndices, double scale, int_t instance);

        //[DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        //public static extern int_t finalizeModelling(int_t model, float[] vertices, int[] indices, int_t FVF);

        //[DllImport(IFCEngineDLL, EntryPoint = "finalizeModelling")]
        //public static extern int_t finalizeModelling(int_t model, double[] vertices, int_t[] indices, int_t FVF);

        //[DllImport(IFCEngineDLL, EntryPoint = "getInstanceInModelling")]
        //public static extern int_t getInstanceInModelling(int_t model, int_t instance, int_t mode, ref int_t startVertex, ref int_t startIndex, ref int_t primitiveCount);

        //[DllImport(IFCEngineDLL, EntryPoint = "setVertexOffset")]
        //public static extern void setVertexOffset(int_t model, double x, double y, double z);

        public delegate IntPtr setFilterDelegate(IntPtr model, IntPtr setting, IntPtr mask);
        public static long setFilter(long model, long setting, long mask)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "setFilter");
            Debug.Assert(procAddress != IntPtr.Zero);

            setFilterDelegate delegateForFunction = (setFilterDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(setFilterDelegate));

            return delegateForFunction(new IntPtr(model), new IntPtr(setting), new IntPtr(mask)).ToInt64();
        } 

        public delegate void setFormatDelegate(IntPtr model, IntPtr setting, IntPtr mask);
        public static void setFormat(long model, long setting, long mask)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "setFormat");
            Debug.Assert(procAddress != IntPtr.Zero);

            setFormatDelegate delegateForFunction = (setFormatDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(setFormatDelegate));

            delegateForFunction(new IntPtr(model), new IntPtr(setting), new IntPtr(mask));
        }
        
        public delegate IntPtr getConceptualFaceCntDelegate(IntPtr instance);
        public static long getConceptualFaceCnt(long instance)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "getConceptualFaceCnt");
            Debug.Assert(procAddress != IntPtr.Zero);

            getConceptualFaceCntDelegate delegateForFunction = (getConceptualFaceCntDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(getConceptualFaceCntDelegate));

            return delegateForFunction(new IntPtr(instance)).ToInt64();
        }

        public delegate IntPtr getConceptualFaceExDelegate(IntPtr instance, IntPtr index, ref IntPtr startIndexTriangles, ref IntPtr noIndicesTriangles, ref IntPtr startIndexLines, ref IntPtr noIndicesLines, ref IntPtr startIndexPoints, ref IntPtr noIndicesPoints, ref IntPtr startIndexFacesPolygons, ref IntPtr noIndicesFacesPolygons, ref IntPtr startIndexConceptualFacePolygons, ref IntPtr noIndicesConceptualFacePolygons);
        public static long getConceptualFaceEx(long instance, long index, ref long startIndexTriangles, ref long noIndicesTriangles, ref long startIndexLines, ref long noIndicesLines, ref long startIndexPoints, ref long noIndicesPoints, ref long startIndexFacesPolygons, ref long noIndicesFacesPolygons, ref long startIndexConceptualFacePolygons, ref long noIndicesConceptualFacePolygons)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "getConceptualFaceEx");
            Debug.Assert(procAddress != IntPtr.Zero);

            IntPtr startIndexTrianglesPtr = IntPtr.Zero;
            IntPtr noIndicesTrianglesPtr = IntPtr.Zero;
            IntPtr startIndexLinesPtr = IntPtr.Zero;
            IntPtr noIndicesLinesPtr = IntPtr.Zero;
            IntPtr startIndexPointsPtr = IntPtr.Zero;
            IntPtr noIndicesPointsPtr = IntPtr.Zero;
            IntPtr startIndexFacesPolygonsPtr = IntPtr.Zero;
            IntPtr noIndicesFacesPolygonsPtr = IntPtr.Zero;
            IntPtr startIndexConceptualFacePolygonsPtr = IntPtr.Zero;
            IntPtr noIndicesConceptualFacePolygonsPtr = IntPtr.Zero;
            getConceptualFaceExDelegate delegateForFunction = (getConceptualFaceExDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(getConceptualFaceExDelegate));

            var result = delegateForFunction(new IntPtr(instance), new IntPtr(index),
                ref startIndexTrianglesPtr, ref noIndicesTrianglesPtr, 
                ref startIndexLinesPtr, ref noIndicesLinesPtr, 
                ref startIndexPointsPtr, ref noIndicesPointsPtr,
                ref startIndexFacesPolygonsPtr, ref noIndicesFacesPolygonsPtr, 
                ref startIndexConceptualFacePolygonsPtr, ref noIndicesConceptualFacePolygonsPtr).ToInt64();

            startIndexTriangles = startIndexTrianglesPtr.ToInt64();
            noIndicesTriangles = noIndicesTrianglesPtr.ToInt64();
            startIndexLines = startIndexLinesPtr.ToInt64();
            noIndicesLines = noIndicesLinesPtr.ToInt64();
            startIndexPoints = startIndexPointsPtr.ToInt64();
            noIndicesPoints = noIndicesPointsPtr.ToInt64();
            startIndexFacesPolygons = startIndexFacesPolygonsPtr.ToInt64();
            noIndicesFacesPolygons = noIndicesFacesPolygonsPtr.ToInt64();
            startIndexConceptualFacePolygons = startIndexConceptualFacePolygonsPtr.ToInt64();
            noIndicesConceptualFacePolygons = noIndicesConceptualFacePolygonsPtr.ToInt64();

            return result;
        }

        public delegate long CalculateInstanceDelegate(long owlInstance, ref long vertexBufferSize, ref long indexBufferSize, ref long transformationBufferSize);
        public static long CalculateInstance(long owlInstance, ref long vertexBufferSize, ref long indexBufferSize, ref long transformationBufferSize)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "CalculateInstance");
            Debug.Assert(procAddress != IntPtr.Zero);

            CalculateInstanceDelegate delegateForFunction = (CalculateInstanceDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(CalculateInstanceDelegate));

            return delegateForFunction(owlInstance, ref vertexBufferSize, ref indexBufferSize, ref transformationBufferSize);
        }
        
        public delegate long UpdateInstanceVertexBufferDelegate(long owlInstance, float[] vertexBuffer);
        public static long UpdateInstanceVertexBuffer(long owlInstance, float[] vertexBuffer)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "UpdateInstanceVertexBuffer");
            Debug.Assert(procAddress != IntPtr.Zero);

            UpdateInstanceVertexBufferDelegate delegateForFunction = (UpdateInstanceVertexBufferDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(UpdateInstanceVertexBufferDelegate));

            return delegateForFunction(owlInstance, vertexBuffer);
        }

        public delegate long UpdateInstanceIndexBufferDelegate(long owlInstance, int[] indexBuffer);
        public static long UpdateInstanceIndexBuffer(long owlInstance, int[] indexBuffer)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "UpdateInstanceIndexBuffer");
            Debug.Assert(procAddress != IntPtr.Zero);

            UpdateInstanceIndexBufferDelegate delegateForFunction = (UpdateInstanceIndexBufferDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(UpdateInstanceIndexBufferDelegate));

            return delegateForFunction(owlInstance, indexBuffer);
        }

        public delegate long GetInstanceClassDelegate(long owlInstance);
        public static long GetInstanceClass(long owlInstance)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "GetInstanceClass");
            Debug.Assert(procAddress != IntPtr.Zero);

            GetInstanceClassDelegate delegateForFunction = (GetInstanceClassDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(GetInstanceClassDelegate));

            return delegateForFunction(owlInstance);
        }

        public delegate long GetClassByNameDelegate(long model, string owlClassName);
        public static long GetClassByName(long model, string owlClassName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "GetClassByName");
            Debug.Assert(procAddress != IntPtr.Zero);

            GetClassByNameDelegate delegateForFunction = (GetClassByNameDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(GetClassByNameDelegate));

            return delegateForFunction(model, owlClassName);
        }

        public delegate long GetPropertyByNameDelegate(long model, string rdfPropertyName);
        public static long GetPropertyByName(long model, string rdfPropertyName)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "GetPropertyByName");
            Debug.Assert(procAddress != IntPtr.Zero);

            GetPropertyByNameDelegate delegateForFunction = (GetPropertyByNameDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(GetPropertyByNameDelegate));

            return delegateForFunction(model, rdfPropertyName);
        }     

        public delegate long GetObjectTypePropertyDelegate(long owlInstance, long rdfProperty, out IntPtr value, ref long card);
        public static long GetObjectTypeProperty(long owlInstance, long rdfProperty, out IntPtr value, ref long card)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "GetObjectTypeProperty");
            Debug.Assert(procAddress != IntPtr.Zero);

            GetObjectTypePropertyDelegate delegateForFunction = (GetObjectTypePropertyDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(GetObjectTypePropertyDelegate));

            return delegateForFunction(owlInstance, rdfProperty, out value, ref card);
        }

        public delegate long GetDataTypePropertyDelegate(long owlInstance, long rdfProperty, out IntPtr value, ref long card);
        public static long GetDataTypeProperty(long owlInstance, long rdfProperty, out IntPtr value, ref long card)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "GetDataTypeProperty");
            Debug.Assert(procAddress != IntPtr.Zero);

            GetDataTypePropertyDelegate delegateForFunction = (GetDataTypePropertyDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(GetDataTypePropertyDelegate));

            return delegateForFunction(owlInstance, rdfProperty, out value, ref card);
        }

        public delegate void owlGetModelDelegate(IntPtr model, ref long owlModel);
        public static void owlGetModel(long model, ref long owlModel)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "owlGetModel");
            Debug.Assert(procAddress != IntPtr.Zero);

            owlGetModelDelegate delegateForFunction = (owlGetModelDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(owlGetModelDelegate));

            delegateForFunction(new IntPtr(model), ref owlModel);
        }

        public delegate void owlGetInstanceDelegate(IntPtr model, IntPtr instance, ref long owlInstance);
        public static void owlGetInstance(long model, long instance, ref long owlInstance)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "owlGetInstance");
            Debug.Assert(procAddress != IntPtr.Zero);

            owlGetInstanceDelegate delegateForFunction = (owlGetInstanceDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(owlGetInstanceDelegate));

            delegateForFunction(new IntPtr(model), new IntPtr(instance), ref owlInstance);
        }        

        public delegate void SetDefaultColorDelegate(long model, uint ambient, uint diffuse, uint emissive, uint specular);
        public static void SetDefaultColor(long model, uint ambient, uint diffuse, uint emissive, uint specular)
        {
            Debug.Assert(IfcEngineDll != IntPtr.Zero);

            IntPtr procAddress = GetProcAddress(IfcEngineDll, "SetDefaultColor");
            Debug.Assert(procAddress != IntPtr.Zero);

            SetDefaultColorDelegate delegateForFunction = (SetDefaultColorDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(SetDefaultColorDelegate));

            delegateForFunction(model, ambient, diffuse, emissive, specular);
        }
    }
}
