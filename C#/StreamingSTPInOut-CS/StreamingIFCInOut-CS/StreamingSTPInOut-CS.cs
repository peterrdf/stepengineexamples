using RDF;
using StreamSTP_IN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamingSTPInOut_CS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamSTP_IN.IN mySTP_INStream = new StreamSTP_IN.IN();

            StreamSTP_OUT.OUT mySTP_OUTStream = new StreamSTP_OUT.OUT(mySTP_INStream.mySTPModel);

            stepengine.sdaiCloseModel(mySTP_INStream.mySTPModel);
        }
    }
}
