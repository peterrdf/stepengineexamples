using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArrayInOut_CS;

namespace ArrayInOut_CS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayIN.IN myINArray = new ArrayIN.IN();

            ArrayOUT.OUT myOUTArray = new ArrayOUT.OUT(myINArray.myModel);
        }
    }
}
