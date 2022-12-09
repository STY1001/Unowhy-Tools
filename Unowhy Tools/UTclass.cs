using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unowhy_Tools
{
    public static class UTclass
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        public static class serv
        {
            public static void stop(string service)
            {
                runmin("net", "stop \"" + service + "\" /y", true);
            }

            public static void start(string service)
            {
                runmin("net", "start \"" + service + "\"", true);
            }

            public static void auto(string service)
            {
                runmin("powershell", "-Name \"" + service + "\" -StartupType Automatic", true);
            }

            public static void dis(string service)
            {
                serv.stop(service);
                runmin("powershell", "-Name \"" + service + "\" -StartupType Disabled", true);
            }

            public static void del(string service)
            {
                serv.stop(service);
                runmin("sc", "delete \"" + service + "\"", true);
            }
        }

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

        public static string getlang(string name)
        {
            //Check the current saved language
            string resxFile;
            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();
            string enresx = @".\en.resx";
            string frresx = @".\fr.resx";
            //Chose the ResX file
            if (utls == "EN") resxFile = enresx;    //English   
            else resxFile = frresx;               //French
            ResXResourceSet resxSet = new ResXResourceSet(resxFile);
            return resxSet.GetString(name);
        }

        public static void runmin(string file, string args, bool msg)
        {
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);

            if (msg == true)
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
            }

            Thread t = new Thread(new ThreadStart(WaitScreen));
            if (msg == true)
            {
                t.Start();
            }

            Process p = new Process();
            p.StartInfo.FileName = file;
            p.StartInfo.Arguments = args;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit();
            if (msg == true)
            {
                t.Abort();
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            }
        }

        public static void WaitScreen()
        {
            Application.Run(new wait());
        }

    }
}
