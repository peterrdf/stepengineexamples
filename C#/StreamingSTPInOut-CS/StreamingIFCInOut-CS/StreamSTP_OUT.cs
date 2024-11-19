using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using RDF;


#if _WIN64
using int_t = System.Int64;
#else
using int_t = System.Int32;
#endif

namespace StreamSTP_OUT
{
    /// <summary>
    /// An STP item
    /// </summary>
    public class OUT
    {
        public  const int   BLOCK_LENGTH_WRITE = 20000;

        public  FileStream  fs;

        public OUT(int_t mySTPModel)
        {

            // define a progress callback delegate
            stepengine.WriteCallBackFunction callback =
                (value, size) =>
                {
                    byte[] buffer = new byte[size];

                    Marshal.Copy(value, buffer, 0, (int) size);

                    fs.Write(buffer, 0, (int) size);
                };

            fs = File.Open("StreamingSTPInOut-CS_exported_as1-oc-214.stp", FileMode.Create);

            stepengine.engiSaveModelByStream(mySTPModel, callback, BLOCK_LENGTH_WRITE);

            fs.Close();
        } 
    }
}
