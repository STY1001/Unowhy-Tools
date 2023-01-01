using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using System.Threading;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Unowhy_Tools_Updater
{
    public partial class Main : Form
    {
        public string resxFile = "null";

        //Set dark mode title bar

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

        public void view()
        {
            var v = new View();
            v.ShowDialog();
        }

        public Main()
        {
            Thread t = new Thread(new ThreadStart(view));
            t.Start();

            InitializeComponent();

            Process p = new Process();
            p.StartInfo.FileName = "taskkill";
            p.StartInfo.Arguments = "/f /im \"Unowhy Tools.exe\"";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();
            p.WaitForExit();

            WebClient client = new WebClient();

            if (File.Exists("debug"))
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/updatedebug.zip", ".\\update.zip");
            }
            else
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/update.zip", ".\\update.zip");
            }
            
            delay(3000);

            client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Extras/7zip.exe", ".\\7zip.exe");

            delay(500);

            client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Extras/7z.dll", ".\\7z.dll");

            delay(1000);

            Process p1 = new Process();
            p1.StartInfo.FileName = ".\\7zip.exe";
            p1.StartInfo.Arguments = "e \"update.zip\" -aoa";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p1.Start();
            p1.WaitForExit();

            System.Diagnostics.Process.Start(".\\Unowhy Tools.exe");

            this.Close();
        }
    }
}
