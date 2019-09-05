using System;

namespace Toolbox
{
    using Inventor;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class ModelWrapper
    {
        private Inventor.Application m_inventorInstance;
        private PartDocument m_partInstance;
        private UserParameters m_partParams;
        private string[] m_partParamNames;
        private string[] m_partParamUnits;

        public ModelWrapper()
        {
            Console.WriteLine("loaded test model");

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

                // init partparams names array
                IEnumerator partParamsEnum = m_partParams.GetEnumerator();
                m_partParamNames = new string[m_partParams.Count - 1];
                m_partParamUnits = new string[m_partParams.Count - 1];
                UserParameter temp;
                int index = 0;
                while (partParamsEnum.MoveNext())
                {
                    temp = partParamsEnum.Current as UserParameter;
                    if (temp.Name != "type")
                    {
                        //Console.WriteLine(temp.get_Units());
                        m_partParamNames[index] = temp.Name;
                        m_partParamUnits[index] = temp.get_Units();
                        index++;
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void setDims(Dictionary<string, double> nameDimsPair)
        {
            UserParameter temp;
            foreach (KeyValuePair<string, double> pair in nameDimsPair)
            {
                temp = m_partParams[pair.Key];
                if (temp == null)
                {
                    Console.WriteLine("dimension with name " + pair.Key + " not found in " + m_partParams["type"].Value);
                } else
                {
                    Console.WriteLine(pair.Value + temp.get_Units()); // inventor promptly ignores current unit and juts inputs cm
                    temp.Value = pair.Value;
                }
            }
        }

        public string[] getDims()
        {
            return m_partParamNames;
        }

        public string[] getUnits()
        {
            return m_partParamUnits;
        }

        public string getName()
        {
            return m_partParams["type"].Value as string;
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