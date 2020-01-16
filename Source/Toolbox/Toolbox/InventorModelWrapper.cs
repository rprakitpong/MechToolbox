using System;
using Inventor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Toolbox
{
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
            initUnitsTable();
            m_inventorInstance = null;
            //m_inventorInstance will be init by initApplication()
        }

        public InventorModelWrapper(string path, object application) : base(path, application)
        {
            initUnitsTable();
            m_inventorInstance = application as Inventor.Application;
        }

        private void initUnitsTable()
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
            //assume m_inventorInstance is init

            try
            {
                

                int copyNum = 0;
                string newPath = path;
                bool madeFile = false;
                while (!madeFile)
                {
                    newPath = path.Insert(path.IndexOf(".ipt"), "Copy" + copyNum);
                    if (System.IO.File.Exists(newPath))
                    {
                        copyNum++;
                    }
                    else
                    {
                        madeFile = true;
                    }
                }
                System.IO.File.Copy(path, newPath);
                System.Diagnostics.Process.Start(newPath);

                //m_inventorInstance = await GetInventorInstance();

                // TODO remove polling, use async
                /*
                bool polling1 = true;
                while (polling1)
                {
                    try
                    {
                        // Get active inventor object
                        m_inventorInstance = System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application") as Inventor.Application;
                        m_inventorInstance.Visible = false;
                        polling1 = false;
                    }
                    catch (COMException)
                    {
                        System.Threading.Thread.Sleep(1000);
                        //MessageBox.Show("Inventor must be running.");
                    }
                }

                bool polling2 = true;
                while (polling2)
                {
                    m_partInstance = m_inventorInstance.ActiveDocument as PartDocument;
                    if (m_partInstance == null)
                    {
                        System.Threading.Thread.Sleep(1000);
                        //MessageBox.Show("Part from parts library must be opened");
                    }
                    else
                    {
                        polling2 = false;
                    }
                }
                */
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

        public override async Task initApplication()
        {
            m_inventorInstance = await ToolBox.InventorInstanceSingleton.Instance.getInventor();
        }

        public override void initApplication(object application)
        {
            m_inventorInstance = application as Inventor.Application;
        }
    }
}