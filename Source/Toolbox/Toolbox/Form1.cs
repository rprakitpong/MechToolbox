using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolbox
{
    using InventorUtilities;

    public partial class Form1 : Form
    {
        ModelWrapper tm;

        double[] dims = { 0, 0, 0, 0, 0 }; 
        // TODO length fixed at 5
        // dimension order in array tightly coupled with CAD model dimension list

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            tm = new ModelWrapper();

            base.OnLoad(e); 
        }

        /*
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        */
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dims[0] = Convert.ToDouble(diamText.Text);
                dims[1] = Convert.ToDouble(thickText.Text);

                tm.setDims(dims);
            } catch (System.FormatException)
            {
                MessageBox.Show("Invalid input: please input number.");
            }
        }
    }
}
