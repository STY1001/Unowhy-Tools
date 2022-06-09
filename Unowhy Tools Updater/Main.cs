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
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text;

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
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
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

            Process p = new Process();
            p.StartInfo.FileName = "taskkill";
            p.StartInfo.Arguments = "/f /im \"Unowhy Tools.exe\"";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();
            p.WaitForExit();

            System.Threading.Thread.Sleep(500);

            status.Text = resxSet.GetString("update.dl");

            WebClient client = new WebClient();
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(dl_complete);
            client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/update.zip"), ".\\update.zip");

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

            System.Threading.Thread.Sleep(1000);

            status.Text = resxSet.GetString("update.ext");

            Process p = new Process();
            p.StartInfo.FileName = ".\\7zip.exe";
            p.StartInfo.Arguments = "e \"update.zip\" -aoa";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();
            p.WaitForExit();

            System.Threading.Thread.Sleep(1000);

            status.Text = resxSet.GetString("update.del");

            if (File.Exists("update.zip"))    //Check if the file exist
            {
                File.Delete("update.zip");    //Delete it if exist
            }

            if (Directory.Exists("update"))
            {
                Directory.Delete("update");
            }

            System.Threading.Thread.Sleep(1000);

            status.Text = resxSet.GetString("update.start");

            System.Threading.Thread.Sleep(500);

            System.Diagnostics.Process.Start(".\\Unowhy Tools.exe");

            this.Close();
        }
    }
}
