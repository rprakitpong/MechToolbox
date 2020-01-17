using System;

namespace Toolbox
{
    using SldWorks;
    using Inventor;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using SwConst;

    public abstract class ModelWrapper
    {
        public string filePath;
        public Dictionary<string, string> partParamNameUnitPairs;
        public string partName;

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

        // set in the units according to getDimsUnit
        // wrapper's job to convert to inventor's default unit
        public abstract void setDimsNum(Dictionary<string, double> nameDimsPair);
        public string[] getDimsName() { return partParamNameUnitPairs.Keys.ToArray(); }
        public string[] getDimsUnit() { return partParamNameUnitPairs.Values.ToArray(); }
        public string getModelName() { return partName; }

        ~ModelWrapper() { closeInstance(); }
        public abstract void closeInstance();
    }

    public class SolidworksModelWrapper : ModelWrapper
    {
        //TODO: finish this
        private SldWorks m_solidworksInstance;
        private ModelDoc2 m_partInstance;
        private EquationMgr m_partParams;
        Dictionary<string, int> nameIndexPair = new Dictionary<string, int>();
        Dictionary<string, string> nameUnitPair = new Dictionary<string, string>();

        public SolidworksModelWrapper(string path) : base(path)
        {

        }

        public override void initPart(string path)
        {
            try
            {
                string newPath = Utility.Instance.CopyFile(path);

                m_solidworksInstance = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")) as SldWorks;
                m_solidworksInstance.Visible = false;
                int error = 0;
                m_partInstance = m_solidworksInstance.OpenDocSilent(newPath, (int)swDocumentTypes_e.swDocPART, ref error) as ModelDoc2;
                if (error != 0)
                {
                    throw new Exception();
                }
                //m_partParams = null;
                m_partParams = m_partInstance.GetEquationMgr();
                //if (m_partParams == null)
                //{
                //    throw new Exception();
                //} else
                //{
                //    Console.WriteLine("yessss");
                //}

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override string initPartName()
        {
            return m_partInstance.GetTitle();
        }
        
        private string getUnit(string value)
        {
            if (value.Contains("A"))
            {
                return "A";
            }
            else if (value.Contains("cm"))
            {
                return "cm";
            }
            else if (value.Contains("ft"))
            {
                return "ft";
            }
            else if (value.Contains("uin"))
            {
                return "uin";
            }
            else if (value.Contains("um"))
            {
                return "um";
            }
            else if (value.Contains("mil"))
            {
                return "mil";
            }
            else if (value.Contains("mm"))
            {
                return "mm";
            }
            else if (value.Contains("nm"))
            {
                return "nm";
            }
            else if (value.Contains("deg"))
            {
                return "deg";
            }
            else if (value.Contains("rad"))
            {
                return "rad";
            }
            else if (value.Contains("m"))
            {
                return "m";
            }
            else if (value.Contains("in"))
            {
                return "in";
            }
            else
            {
                return "unitless";
            }
        }

        public override Dictionary<string, string> initPartParamNameUnit()
        {            
            for (int i = 0; i < m_partParams.GetCount(); i++)
            {
                if (m_partParams.get_GlobalVariable(i))
                {
                    string equation = m_partParams.get_Equation(i);
                    string name = equation.Substring(1, equation.IndexOf('=')-2);
                    string value = equation.Substring(equation.IndexOf('=')+2);
                    //Console.WriteLine(name);
                    //Console.WriteLine(value);
                    nameIndexPair.Add(name, i);
                    string unit = getUnit(value);
                    //Console.WriteLine(unit);
                    nameUnitPair.Add(name, unit);
                }
            }
            return nameUnitPair;
        }

        public override void setDimsNum(Dictionary<string, double> nameDimsPair)
        {
            foreach (KeyValuePair<string, double> kvp in nameDimsPair)
            {
                string unit = "";
                if (nameUnitPair[kvp.Key] != "unitless")
                {
                    unit = nameUnitPair[kvp.Key];
                }
                string equation = "\"" + kvp.Key + "\"=" + kvp.Value.ToString() + unit;
                m_partParams.set_Equation(nameIndexPair[kvp.Key], equation);
            }

            m_partInstance.SaveSilent();
        }

        public override void closeInstance()
        {
            m_partInstance.Quit();
            m_solidworksInstance.ExitApp();
        }

    }

    public class InventorModelWrapper : ModelWrapper
    {

        // unit table for conversion to inventor's default units (learned from trial and error)
        // length: cm
        // angle: rad
        // angle^2: sr
        // ul: ul
        Dictionary<string, double> unitsTable = new Dictionary<string, double>();

        private Inventor.Application m_inventorInstance;
        private PartDocument m_partInstance;
        private UserParameters m_partParams;

        public InventorModelWrapper(string path) : base(path)
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
        }

        public override void initPart(string path)
        {
            string newPath = Utility.Instance.CopyFile(path);

            try
            {
                // TODO remove polling, use async

                m_inventorInstance = Activator.CreateInstance(Type.GetTypeFromProgID("Inventor.Application")) as Inventor.Application;
                m_inventorInstance.Visible = false;

                bool polling2 = true;
                while (polling2)
                {
                    m_partInstance = m_inventorInstance.Documents.Open(@newPath) as PartDocument;
                    if (m_partInstance == null)
                    {
                        System.Threading.Thread.Sleep(10);
                        //MessageBox.Show("Part from parts library must be opened");
                    } else
                    {
                        polling2 = false;
                    }
                }
                
                //Console.WriteLine((string)m_partInstance.DisplayName);

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
                    //Console.WriteLine(pair.Value + temp.get_Units()); // inventor promptly ignores current unit and juts inputs cm
                    temp.Value = pair.Value * unitsTable[partParamNameUnitPairs[pair.Key]];
                }
            }

            m_partInstance.Update();
            m_partInstance.Save();
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

        public override void closeInstance()
        {
            m_inventorInstance.Quit();
        }
    }
}