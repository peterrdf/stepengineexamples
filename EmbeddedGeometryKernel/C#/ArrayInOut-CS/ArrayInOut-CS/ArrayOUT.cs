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

namespace ArrayOUT
{
    /// <summary>
    /// An IFC item
    /// </summary>
    public class OUT
    {
        public  Int64   myModel = 0;

        public OUT(Int64 myModel)
        {
            FileStream fs = File.Open("data\\ArrayInOut-CS_exportedFile.bin", FileMode.Create);

            Int64 size = 0;
            RDF.engine.SaveModelA(myModel, null, out size);

            byte[] buffer = new byte[size];
            RDF.engine.SaveModelA(myModel, buffer, out size);

            fs.Write(buffer, 0, (int)size);

            fs.Close();
        } 
    }
}
