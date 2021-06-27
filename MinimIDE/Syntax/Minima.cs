using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimIDE.Syntax
{
    public static class Minima
    {
        public static void NewInstance(string filePath)
        {
            if(!File.Exists("minima.exe"))
            {
                MessageBox.Show("Please insure than Minima is installed in order to run Minima programs.", "Unable to launch Minima", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ProcessStartInfo startInfo = new ProcessStartInfo("minima.exe");
            if (filePath != null)
                startInfo.Arguments = filePath;
            Process.Start(startInfo);
        }
    }
}
