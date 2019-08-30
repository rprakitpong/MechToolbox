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
    public partial class Form1 : Form
    {
        // model wrapper, interface to inventor window
        ModelWrapper mw = new ModelWrapper();

        // unit/cm ratio table for conversion
        Dictionary<string, double> unitsTable = new Dictionary<string, double>();

        // unit fields
        string unitName = "unit";
        int unitFieldsCount = 3;
        Dictionary<string, string> unitFieldTable = new Dictionary<string, string>();

        // input array fields
        string inputName = "val_dim";
        string labelName = "name_dim";
        int inputFieldsCount = 4;
        Dictionary<string, TextBox> inputs = new Dictionary<string, TextBox>();

        // form name
        string formName = " Generater";

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            // initialize converter table
            unitsTable.Add("cm", 1); // cm must be first, dependency with form design
            unitsTable.Add("mm", 0.1);
            unitsTable.Add("in", 2.54);

            base.OnLoad(e);

            initFormDimFields(mw.getDims());
            initFormName(mw.getName());
            initUnitButtons();
        }

        private void initUnitButtons()
        {
            int i = 0;
            RadioButton temp;
            foreach (KeyValuePair<string, double> unit in unitsTable)
            {
                temp = (RadioButton)this.Controls.Find(unitName + i, true)[0];
                if (temp != null)
                {
                    temp.Text = unit.Key;
                    unitFieldTable.Add(temp.Name, unit.Key);
                }
                i++;
            }
            if (i < unitFieldsCount)
            {
                i--;
                while (i + 1 < unitFieldsCount)
                {
                    i++;
                    this.Controls.Find(unitName + i, true)[0].Visible = false;
                }
            }
        }

        private void initFormName(string v)
        {
            this.Text = v + formName;
        }

        private void initFormDimFields(string[] v)
        {
            for (int i = 0; i < inputFieldsCount; i++)
            {
                if (i < v.Length)
                {
                    inputs.Add(v[i], (TextBox)this.Controls.Find(inputName + i, true)[0]);
                    this.Controls.Find(labelName + i, true)[0].Text = v[i];
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
            var temp = from RadioButton r in unitsGroup.Controls where r.Checked == true select r.Name;
            double unitConversion = unitsTable[unitFieldTable[temp.First()]];
            Dictionary<string, double> dims = new Dictionary<string, double>();
            foreach (KeyValuePair<string, TextBox> inputPair in inputs)
            {
                try
                {
                    dims.Add(inputPair.Key, unitConversion * Convert.ToDouble(inputPair.Value.Text));
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Invalid input: please input number.");
                }
            }
            mw.setDims(dims);
        }
        
    }
}
