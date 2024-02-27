using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBSLFormsApp.Model.Libraries
{
    internal class DebugLog
    {
        public async Task debuglog(string datalog) {

            string startFolderPath = Application.StartupPath;

            string pathfilename = "\\log\\";

            //const string filename = "D:\\SahaLife\\PkiDemo\\IBSLPortal\\IBSLFormsApp\\IBSLFormsApp\\log\\DebugLog.txt";
            string filename = startFolderPath + pathfilename + "DebugLog.txt";

            if (!File.Exists(filename))
            {
               File.WriteAllText(filename, datalog.ToString() + Environment.NewLine);
            }
            else
            {
               File.AppendAllText(filename, datalog.ToString() + Environment.NewLine);
            }

        }

        internal void debuglog(string v, string message)
        {
            throw new NotImplementedException();
        }
    }
}
