using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unowhy_Tools.UTclass
{
    public static class UTclass
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);


        public static void delay(int Time_delay)
        {
            int i = 0;
            System.Timers.Timer _delayTimer = new System.Timers.Timer();
            _delayTimer.Interval = Time_delay;
            _delayTimer.AutoReset = false;
            _delayTimer.Elapsed += (s, args) => i = 1;
            _delayTimer.Start();
            while (i == 0) { };
        }
        public static void ebtn(Control btn)
        {
            btn.Enabled = true;
            btn.ForeColor = Color.White;
        }

        public static void dbtn(Control btn)
        {
            btn.Enabled = false;
            btn.ForeColor = Color.Gray;
        }

        public static string returncmd(string file, string args)
        {
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);

            Process get = new Process();
            get.StartInfo.FileName = file;
            get.StartInfo.Arguments = args;
            get.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            get.StartInfo.UseShellExecute = false;
            get.StartInfo.RedirectStandardOutput = true;
            get.Start();
            get.WaitForExit();
            string output = get.StandardOutput.ReadToEnd();
            return output;
        }

        public static string getline(string text, int line)
        {
            int line2 = line - 1;
            var lines = text.Split('\n');
            return lines[line2];
        }
    }
}
