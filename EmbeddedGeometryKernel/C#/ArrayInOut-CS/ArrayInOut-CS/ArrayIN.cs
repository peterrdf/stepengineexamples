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

namespace ArrayIN
{
    /// <summary>
    /// An IFC item
    /// </summary>
    public class IN
    {
        public  Int64   myModel = 0;

        public IN()
        {
            FileStream fs = File.Open("exampleFile.bin", FileMode.Open);

            if (fs != null)
            {
                Int64 size = (Int64) fs.Length;

                byte[] content = new byte[size];
                fs.Read(content, 0, (int) size);
                fs.Close();

                myModel = RDF.engine.OpenModelA(content, size);
            }
        } 
    }
}
