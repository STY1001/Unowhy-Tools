using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Threading;
using System.Timers;

namespace Unowhy_Tools_Updater_Script
{
    public partial class Main : Form
    {
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
        }

        private static void delay(int Time_delay)
        {
            int i = 0;
            System.Timers.Timer _delayTimer = new System.Timers.Timer();
            _delayTimer.Interval = Time_delay;
            _delayTimer.AutoReset = false;
            _delayTimer.Elapsed += (s, args) => i = 1;
            _delayTimer.Start();
            while (i == 0) { };
        }

        public Main()
        {
            InitializeComponent();

            //TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);

            if (File.Exists("old2new"))
            {

            }
            else
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/script/utdeloldkey.exe", ".\\utdeloldkey.exe");
                }

                delay(100);

                Process p1 = new Process();
                p1.StartInfo.FileName = ".\\utdeloldkey.exe";
                p1.StartInfo.Arguments = "";
                p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p1.Start();
                p1.WaitForExit();

                delay(100);

                File.Delete("utdeloldkey.exe");

                if (Directory.Exists("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Unowhy Tools"))
                {
                    Directory.Delete("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Unowhy Tools", true);
                }

                if (File.Exists("C:\\Users\\Public\\Desktop\\Unowhy Tools.lnk"))
                {
                    File.Delete("C:\\Users\\Public\\Desktop\\Unowhy Tools.lnk");
                }

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Install/utkeyinst.exe", ".\\utkeyinst.exe");
                }

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Install/utkeyinst.reg", ".\\utkeyinst.reg");
                }

                delay(100);

                Process p2 = new Process();
                p2.StartInfo.FileName = ".\\utkeyinst.exe";
                p2.StartInfo.Arguments = "";
                p2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p2.Start();
                p2.WaitForExit();

                delay(100);

                File.Delete("utkeyinst.exe");
                File.Delete("utkeyinst.reg");

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Uninstall/uninstall.exe", ".\\uninstall.exe");
                }

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Uninstall/utconfdel.exe", ".\\utconfdel.exe");
                }

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Uninstall/utkeydel.exe", ".\\utkeydel.exe");
                }

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Install/Unowhy%20Tools.lnk", "C:\\Users\\Public\\Desktop\\Unowhy Tools.lnk");
                }

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Install/Unowhy%20Tools.lnk", "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Unowhy Tools.lnk");
                }

                File.Create("old2new");
            }

            this.Close();
        }
    }
}
