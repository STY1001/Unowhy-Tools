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

        public Main()
        {
            //Check the current saved language

            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            string enresx = @".\en.resx";
            string frresx = @".\fr.resx";
            //Chose the ResX file
            if (utls == "EN") resxFile = enresx;    //English   
            else resxFile = frresx;               //French

            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            lab.Text = resxSet.GetString("update.updating");
            status.Text = resxSet.GetString("update.kill");

            delay(1000);

            Process p = new Process();
            p.StartInfo.FileName = "taskkill";
            p.StartInfo.Arguments = "/f /im \"Unowhy Tools.exe\"";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();
            p.WaitForExit();

            delay(500);

            progbar.Value = 10;

            //TaskbarManager.Instance.SetProgressValue(10, 100);

            status.Text = resxSet.GetString("update.dl");

            WebClient client = new WebClient();
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(dl_complete);
            client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/update.zip"), ".\\update.zip");
            client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Uninstall/uninstall.exe", ".\\uninstall.exe");
            client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Extras/7zip.exe", ".\\7zip.exe");
            client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Extras/7z.dll", ".\\7z.dll");
        }

        private void dl_complete(object sender, AsyncCompletedEventArgs e)
        {
            //Check the current saved language

            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            string enresx = @".\en.resx";
            string frresx = @".\fr.resx";
                                                     //Chose the ResX file
            if (utls == "EN") resxFile = enresx;    //English   
            else resxFile = frresx;                //French
            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            delay(500);

            progbar.Value = 25;

            TaskbarManager.Instance.SetProgressValue(25, 100);

            status.Text = resxSet.GetString("update.ext");

            Process p1 = new Process();
            p1.StartInfo.FileName = ".\\7zip.exe";
            p1.StartInfo.Arguments = "e \"update.zip\" -aoa";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p1.Start();
            p1.WaitForExit();

            delay(1000);

            progbar.Value = 30;

            TaskbarManager.Instance.SetProgressValue(30, 100);

            status.Text = resxSet.GetString("update.sdl");

            using (var client = new WebClient())
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/script.exe", ".\\script.exe");
            }

            delay(1000);

            progbar.Value = 45;

            TaskbarManager.Instance.SetProgressValue(45, 100);

            status.Text = resxSet.GetString("update.se");

            Process p2 = new Process();
            p2.StartInfo.FileName = ".\\script.exe";
            p2.StartInfo.Arguments = "";
            p2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p2.Start();
            p2.WaitForExit();

            delay(1000);

            progbar.Value = 60;

            TaskbarManager.Instance.SetProgressValue(60, 100);

            status.Text = resxSet.GetString("update.del");

            if (File.Exists("update.zip"))    //Check if the file exist
            {
                File.Delete("update.zip");    //Delete it if exist
            }

            if (Directory.Exists("update"))
            {
                Directory.Delete("update");
            }

            if (Directory.Exists("script.exe"))
            {
                Directory.Delete("script.exe");
            }

            delay(1000);

            progbar.Value = 85;

            TaskbarManager.Instance.SetProgressValue(85, 100);

            status.Text = resxSet.GetString("update.start");

            delay(500);

            progbar.Value = 100;

            TaskbarManager.Instance.SetProgressValue(100, 100);

            System.Diagnostics.Process.Start(".\\Unowhy Tools.exe");

            this.Close();
        }
    }
}
