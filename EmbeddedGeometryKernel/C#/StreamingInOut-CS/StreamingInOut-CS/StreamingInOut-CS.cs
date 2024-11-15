using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StreamIN;

namespace StreamingInOut_CS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamIN.IN myINStream = new StreamIN.IN();

            StreamOUT.OUT myOUTStream = new StreamOUT.OUT(myINStream.myModel);
        }
    }
}
