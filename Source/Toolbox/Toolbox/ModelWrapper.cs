using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toolbox
{
    public abstract class ModelWrapper
    {
        public string filePath;
        public Dictionary<string, string> partParamNameUnitPairs;
        public string partName;

        //subclass must implement application and part

        public ModelWrapper(string path)
        {
            filePath = path;
            initApplication();
            initPart(path);
            partParamNameUnitPairs = initPartParamNameUnit();
            partName = initPartName();
        }

        public ModelWrapper(string path, object application)
        {
            filePath = path;
            initApplication(application);
            initPart(path);
            partParamNameUnitPairs = initPartParamNameUnit();
            partName = initPartName();
        }

        public abstract Task initApplication();
        public abstract void initApplication(object application);
        public abstract void initPart(string path);
        public abstract Dictionary<string, string> initPartParamNameUnit();
        public abstract string initPartName();

        // set in the units according to getDimsUnit
        // wrapper's job to convert to inventor's default unit
        public abstract void setDimsNum(Dictionary<string, double> nameDimsPair);
        public string[] getDimsName() { return partParamNameUnitPairs.Keys.ToArray(); }
        public string[] getDimsUnit() { return partParamNameUnitPairs.Values.ToArray(); }
        public string getModelName() { return partName; }
    }
}