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
    public partial class Form : System.Windows.Forms.Form
    {
        // model wrapper, interface to inventor window
        InventorModelWrapper mw = new InventorModelWrapper("stub");

        // unit table for conversion to inventor's default units (learned from trial and error)
        // length: cm
        // angle: rad
        // angle^2: sr
        // ul: ul
        Dictionary<string, double> unitsTable = new Dictionary<string, double>();
        
        // input array fields
        string inputName = "val_dim";
        string labelName = "name_dim";
        int inputFieldsCount = 12;
        Dictionary<string, dimensionProperty> inputs = new Dictionary<string, dimensionProperty>();

        // form name
        string formName = " Generater";

        public Form()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            // initialize converter table
            unitsTable.Add("mm", 0.1);
            unitsTable.Add("cm", 1);
            unitsTable.Add("m", 100);
            unitsTable.Add("in", 2.54);
            unitsTable.Add("ft", 30.48);
            unitsTable.Add("micron", 0.0001);
            unitsTable.Add("nauticalMile", 185200);
            unitsTable.Add("mil", 0.00254);

            unitsTable.Add("rad", 1);
            unitsTable.Add("deg", 0.0174532925);
            unitsTable.Add("grad", 0.015707963267949);

            unitsTable.Add("sr", 1);

            unitsTable.Add("ul", 1);

            base.OnLoad(e);

            initFormDimFields(mw.getDimsName(), mw.getDimsUnit());
            initFormName(mw.getModelName());
        }

        private void initFormName(string v)
        {
            this.Text = v + formName;
        }

        private void initFormDimFields(string[] name, string[] units)
        {
            
            for (int i = 0; i < inputFieldsCount; i++)
            {
                if (i < name.Length)
                {
                    inputs.Add(name[i], new dimensionProperty((TextBox)this.Controls.Find(inputName + i, true)[0], units[i]));
                    this.Controls.Find(labelName + i, true)[0].Text = name[i] + " (" + units[i] + ")";
                } else
                {
                    this.Controls.Find(inputName + i, true)[0].Visible = false;
                    this.Controls.Find(labelName + i, true)[0].Visible = false;
                }
            }
        }


        /*
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        */

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, double> dims = new Dictionary<string, double>();
            foreach (KeyValuePair<string, dimensionProperty> inputPair in inputs)
            {
                try
                {
                    dims.Add(inputPair.Key, unitsTable[inputPair.Value.unit] * Convert.ToDouble(inputPair.Value.inputField.Text));
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Invalid input: please input number.");
                }
            }
            mw.setDimsNum(dims);
        }

        public class dimensionProperty
        {
            public TextBox inputField;
            public string unit;

            public dimensionProperty(TextBox thisInputField, string thisUnit)
            {
                inputField = thisInputField;
                unit = thisUnit;
            }
        }

        private void fileDirBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                Console.WriteLine(openFileDialog1.FileName); 
            }
        }
    }
}
