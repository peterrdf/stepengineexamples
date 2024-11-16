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

namespace StreamOUT
{
    /// <summary>
    /// An STP item
    /// </summary>
    public class OUT
    {
        public  const int BLOCK_LENGTH_WRITE = 20000;

        public  FileStream fs;

        public  Int64   myModel = 0;

        public OUT(Int64 myModel)
        {

            // define a progress callback delegate
            RDF.engine.WriteCallBackFunction callback =
                (value, size) =>
                {
                    byte[] buffer = new byte[size];

                    Marshal.Copy(value, buffer, 0, (int) size);

                    fs.Write(buffer, 0, (int) size);
                };

            fs = File.Open("EGK-StreamingInOut-CS_exampleFile_exportedFile.bin", FileMode.Create);

            RDF.engine.SaveModelS(myModel, callback, BLOCK_LENGTH_WRITE);

            fs.Close();
        } 
    }
}
