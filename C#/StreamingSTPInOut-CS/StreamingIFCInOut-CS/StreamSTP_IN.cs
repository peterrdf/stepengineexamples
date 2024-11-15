using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using StepEngine;

#if _WIN64
using int_t = System.Int64;
#else
using int_t = System.Int32;
#endif

namespace StreamSTP_IN
{
    /// <summary>
    /// An STP item
    /// </summary>
    public class IN
    {
        public const int_t  flagbit20 = 1048576;		// 2^^20   0000.0000..0001.0000  0000.0000..0000.0000
        public const int_t  flagbit21 = 2097152;		// 2^^21   0000.0000..0010.0000  0000.0000..0000.0000
        public const int_t  flagbit22 = 4194304;		// 2^^22   0000.0000..0100.0000  0000.0000..0000.0000

        public const int    BLOCK_LENGTH_READ = 20000; // MAX: 65535

        public  FileStream  fs;

        public  int_t       mySTPModel = 0;

        public IN()
        {

            // define a progress callback delegate
            StepEngine.x86_64.ReadCallBackFunction callback =
                (value) =>
                {
                    byte[] buffer = new byte[BLOCK_LENGTH_READ]; 

                    int   size = fs.Read(buffer, 0, BLOCK_LENGTH_READ);

                    if (size > 0)
                    {
                        Marshal.Copy(buffer, 0, value, size);

                        return (int_t) size;
                    }
                    else
                    {
                        return -1;
                    }
                };

            fs = File.Open("data\\StreamingSTPInOut-CS_as1-oc-214.stp", FileMode.Open);

            if (fs != null)
            {
                mySTPModel = StepEngine.x86_64.engiOpenModelByStream(0, callback, "");

                fs.Close();
            }
        } 
    }
}
