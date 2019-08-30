using System;

namespace Toolbox
{
    using Inventor;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ModelWrapper
    {
        private Inventor.Application m_inventorInstance;
        private PartDocument m_partInstance;
        private UserParameters m_partParams;
        private string[] m_partParamNames;

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
                UserParameter temp;
                int index = 0;
                while (partParamsEnum.MoveNext())
                {
                    temp = partParamsEnum.Current as UserParameter;
                    if (temp.Name != "type")
                    {
                        m_partParamNames[index] = temp.Name;
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
                    temp.Value = pair.Value;
                }
            }
        }

        public string[] getDims()
        {
            return m_partParamNames;
        }

        public string getName()
        {
            return m_partParams["type"].Value as string;
        }

    }
}