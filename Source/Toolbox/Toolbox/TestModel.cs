using System;

namespace Toolbox
{
    using Inventor;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ModelWrapper
    {
        private Inventor.Application m_inventorInstance;
        private PartDocument m_partInstance;

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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void setDims(double[] dims)
        {
            UserParameters featureDims = m_partInstance.ComponentDefinition.Parameters.UserParameters;

            for (int i = 0; i < featureDims.Count; i++)
            {
                featureDims["dim" + i].Value = dims[i]; // TODO always in cm
            }
        }
        
    }
}