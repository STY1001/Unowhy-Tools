using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int state, int value);

        public static class serv
        {
            public static void stop(string service)
            {
                Write2Log("Stop " + service);
                runmin("net", "stop \"" + service + "\" /y", true);
            }

            public static void start(string service)
            {
                Write2Log("Start " + service);
                runmin("net", "start \"" + service + "\"", true);
            }

            public static void auto(string service)
            {
                Write2Log("Enable " + service);
                runmin("sc", "config \"" + service + "\" start=auto", true);
            }

            public static void dis(string service)
            {
                Write2Log("Disable " + service);
                serv.stop(service);
                runmin("sc", "config \"" + service + "\" start=disabled", true);
            }

            public static void del(string service)
            {
                Write2Log("Delete " + service);
                serv.stop(service);
                runmin("sc", "delete \"" + service + "\"", true);
            }
        }

        public static class waitstatus
        {
            public static void close()
            {
                Write2Log("Close wait");
                runmin("taskkill", "/f /im UTwait.exe", false);
            }

            public static void open()
            {
                Write2Log("Open wait");
                Process.Start(".\\UTwait.exe");
            }
        }

        public static class splashstatus
        {
            public static void close()
            {
                Write2Log("Close splash");
                runmin("taskkill", "/f /im UTsplash.exe", false);
            }

            public static void open()
            {
                Write2Log("Open splash");
                if (Properties.Resources.Debug.Contains("Debug"))
                {
                    Process.Start(".\\UTsplash.exe", $"-d -v \"{Properties.Resources.Version}\"");
                }
                else
                {
                    Process.Start(".\\UTsplash.exe", $"-v \"{Properties.Resources.Version}\"");
                }
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

            Write2Log("ReturnCMD " + file + " " + args);

            Process get = new Process();
            get.StartInfo.FileName = file;
            get.StartInfo.Arguments = args;
            get.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            get.StartInfo.UseShellExecute = false;
            get.StartInfo.RedirectStandardOutput = true;
            get.StartInfo.CreateNoWindow = true;
            get.Start();
            get.WaitForExit();
            string output = get.StandardOutput.ReadToEnd();

            Write2Log("Done ReturnCMD " + file + " " + args + " => " + output);

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
            string enresx = @".\lang\en.resx";
            string frresx = @".\lang\fr.resx";
            //Chose the ResX file
            if (utls == "EN") resxFile = enresx;    //English   
            else resxFile = frresx;               //French
            ResXResourceSet resxSet = new ResXResourceSet(resxFile);
            Write2Log("Get lang " + name + " => " + resxSet.GetString(name));
            return resxSet.GetString(name);
        }

        public static void runmin(string file, string args, bool msg)
        {
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);

            Write2Log("RunMin " + file + " " + args);

            if (msg == true)
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
            }

            if (msg == true)
            {
                waitstatus.open();
            }

            Process p = new Process();
            p.StartInfo.FileName = file;
            p.StartInfo.Arguments = args;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            if (msg == true)
            {
                waitstatus.close();
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }

            Write2Log("Done RunMin " + file + " " + args);
        }

        public static void Write2Log(string log)
        {
            if(!File.Exists(Path.GetTempPath() + "\\UT_Logs.txt"))
            {
                var f = File.CreateText(Path.GetTempPath() + "\\UT_Logs.txt");
                f.Close();
            }

            File.AppendAllText(Path.GetTempPath() + "\\UT_Logs.txt", DateTime.Now.ToString() + " : " + log + Environment.NewLine);
        }
    }
}
