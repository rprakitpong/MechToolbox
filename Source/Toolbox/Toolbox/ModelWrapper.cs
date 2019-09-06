using System;

namespace Toolbox
{
    using Inventor;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public abstract class ModelWrapper
    {
        string filePath;
        Dictionary<string, string> partParamNameUnitPairs;
        string partName;

        public ModelWrapper(string path)
        {
            filePath = path;
            initPart(path);
            partParamNameUnitPairs = initPartParamNameUnit();
            partName = initPartName();
        }

        public abstract void initPart(string path);
        public abstract Dictionary<string, string> initPartParamNameUnit();
        public abstract string initPartName();

        public abstract void setDimsNum(Dictionary<string, double> nameDimsPair);
        public string[] getDimsName() { return partParamNameUnitPairs.Keys.ToArray(); }
        public string[] getDimsUnit() { return partParamNameUnitPairs.Values.ToArray(); }
        public string getModelName() { return partName; }
    }

    public class InventorModelWrapper : ModelWrapper
    {
        private Inventor.Application m_inventorInstance;
        private PartDocument m_partInstance;
        private UserParameters m_partParams;

        public InventorModelWrapper(string path) : base(path)
        {
            //
        }

        public override void initPart(string path)
        {
            //TODO do something with path
            string filePath = path;

            try
            {
                try
                {
                    // Get active inventor object
                    m_inventorInstance = System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application") as Inventor.Application;
                }
                catch (COMException)
                {
                    MessageBox.Show("Inventor must be running.");
                }

                m_partInstance = m_inventorInstance.ActiveDocument as PartDocument;
                if (m_partInstance == null)
                {
                    MessageBox.Show("Part from parts library must be opened");
                }

                // init partparams collection
                m_partParams = m_partInstance.ComponentDefinition.Parameters.UserParameters;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override Dictionary<string, string> initPartParamNameUnit()
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();

            // init partparams names array
            IEnumerator partParamsEnum = m_partParams.GetEnumerator();
            
            UserParameter temp;
            while (partParamsEnum.MoveNext())
            {
                temp = partParamsEnum.Current as UserParameter;
                if (temp.Name != "type")
                {
                    pairs.Add(temp.Name, temp.get_Units());
                }
            }

            return pairs;
        }

        public override string initPartName()
        {
            return m_partParams["type"].Value as string;
        }

        public override void setDimsNum(Dictionary<string, double> nameDimsPair)
        {
            UserParameter temp;
            foreach (KeyValuePair<string, double> pair in nameDimsPair)
            {
                temp = m_partParams[pair.Key];
                if (temp == null)
                {
                    Console.WriteLine("dimension with name " + pair.Key + " not found in " + m_partParams["type"].Value);
                }
                else
                {
                    Console.WriteLine(pair.Value + temp.get_Units()); // inventor promptly ignores current unit and juts inputs cm
                    temp.Value = pair.Value;
                }
            }
        }

        private string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}