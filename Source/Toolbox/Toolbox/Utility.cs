using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox
{
    public enum CADtype { Inventor, Solidworks, Unknown}

    public sealed class Utility
    {
        private static Utility instance = null;
        private static readonly object padlock = new object();

        Utility()
        {
        }

        public static Utility Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Utility();
                    }
                    return instance;
                }
            }
        }

        public string CopyFile(string path)
        {
            int copyNum = 0;
            string newPath = path;
            bool madeFile = false;
            while (!madeFile)
            {
                int index = path.LastIndexOf('.');
                newPath = path.Insert(index, "Copy" + copyNum);
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
            return newPath;
        }

        public CADtype FileType(string path)
        {
            int indexOfFileType = path.LastIndexOf('.');
            string fileType = path.Substring(indexOfFileType + 1);
            if (fileType == "ipt")
            {
                return CADtype.Inventor;
            } else if (fileType == "SLDPRT")
            {
                return CADtype.Solidworks;
            } else
            {
                return CADtype.Unknown;
            }
        }

    }
}
