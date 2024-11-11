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

namespace StreamIN
{
    /// <summary>
    /// An STP item
    /// </summary>
    public class IN
    {
        public  const int BLOCK_LENGTH_READ = 20000; // MAX: 65535

        public  FileStream fs;

        public  Int64   myModel = 0;

        public IN()
        {

            // define a progress callback delegate
            RDF.engine.ReadCallBackFunction callback =
                (value) =>
                {
                    byte[] buffer = new byte[BLOCK_LENGTH_READ]; 

                    int   size = fs.Read(buffer, 0, BLOCK_LENGTH_READ);

                    Marshal.Copy(buffer, 0, value, size);

                    return (Int64) size;
                };

            fs = File.Open("exampleFile.bin", FileMode.Open);
            
            myModel = RDF.engine.OpenModelS(callback);

            fs.Close();
        } 
    }
}
