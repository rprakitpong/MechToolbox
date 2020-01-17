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
        ModelWrapper mw; 

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
            base.OnLoad(e);

            hideParamFields();
        }

        private void hideParamFields()
        {
            dims_group.Visible = false;
            okBtn.Visible = false;
        }

        private void initFormName(string v)
        {
            this.Text = v + formName;
        }

        private void initFormDimFields(string[] name, string[] units)
        {
            dims_group.Visible = true;
            okBtn.Visible = true;

            for (int i = 0; i < inputFieldsCount; i++)
            {
                if (i < name.Length)
                {
                    inputs.Add(name[i], new dimensionProperty((TextBox)this.Controls.Find(inputName + i, true)[0], units[i]));
                    this.Controls.Find(labelName + i, true)[0].Visible = true;
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
                    dims.Add(inputPair.Key, Convert.ToDouble(inputPair.Value.inputField.Text));
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
                bool fileOK = false;
                CADtype typeOfFile = Utility.Instance.FileType(openFileDialog1.FileName);
                if (typeOfFile == CADtype.Inventor)
                {
                    mw = new InventorModelWrapper(@openFileDialog1.FileName);
                    fileOK = true;
                } else if (typeOfFile == CADtype.Solidworks)
                {
                    mw = new SolidworksModelWrapper(@openFileDialog1.FileName);
                    fileOK = true;
                } else
                {
                    MessageBox.Show("Invalid file;please select Inventor or Solidworks part file");
                }

                if (fileOK)
                {
                    initFormDimFields(mw.getDimsName(), mw.getDimsUnit());
                    initFormName(mw.getModelName());
                    fileDirBtn.Visible = false;
                }
            }
        }
    }
}
