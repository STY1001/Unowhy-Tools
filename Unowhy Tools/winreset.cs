using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unowhy_Tools
{
    public partial class winreset : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        public string resxFile;
        private int Out;
        private string noco;

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int state, int value);

        public void WaitScreen()
        {
            Application.Run(new wait());
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);
        }

        public winreset()
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

            ena.Text = resxSet.GetString("enable");
            dis.Text = resxSet.GetString("disable");
            rep.Text = resxSet.GetString("repair");
            this.Text = resxSet.GetString("winre.title");

            if (File.Exists("debug"))
            {
                debwinre.Visible = true;
            }

            check();
        }
        
        private void ebtn(Control btn)
        {
            btn.Enabled = true;
            btn.ForeColor = Color.White;
        }

        private void dbtn(Control btn)
        {
            btn.Enabled = false;
            btn.ForeColor = Color.Gray;
        }

        private void ena_Click(object sender, EventArgs e)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
            Thread t = new Thread(new ThreadStart(WaitScreen));
            t.Start();
            debwinre.Text = "1";
            var p = new Process();
            p.StartInfo.FileName = "reagentc";
            p.StartInfo.Arguments = "/enable";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();
            p.WaitForExit();
            check();
            t.Abort();
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
        }

        private void check()
        {
            var p = new Process();
            p.StartInfo.FileName = "reagentc";
            p.StartInfo.Arguments = "/info";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.WaitForExit();
            string out1 = p.StandardOutput.ReadToEnd();
            if (out1.Contains("Enable"))
            {
                dbtn(ena);
                dbtn(rep);
                ebtn(dis);
                repmsg.Text = "";
            }
            else
            {
                if(debwinre.Text == "1")
                {
                    dbtn(ena);
                    ebtn(rep);
                    dbtn(dis);
                    //Check the current saved language

                    RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
                    string utls = utl.GetValue("Lang").ToString();

                    string enresx = @".\en.resx";
                    string frresx = @".\fr.resx";
                    //Chose the ResX file
                    if (utls == "EN") resxFile = enresx;    //English   
                    else resxFile = frresx;               //French
                    ResXResourceSet resxSet = new ResXResourceSet(resxFile);
                    repmsg.Text = resxSet.GetString("winremsg");
                }
                else
                {
                    ebtn(ena);
                    dbtn(rep);
                    dbtn(dis);
                    repmsg.Text = "";
                }
            }
        }

        private void dis_Click(object sender, EventArgs e)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
            Thread t = new Thread(new ThreadStart(WaitScreen));
            t.Start();
            debwinre.Text = "Winre";
            var p = new Process();
            p.StartInfo.FileName = "reagentc";
            p.StartInfo.Arguments = "/disable";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();
            p.WaitForExit();
            check();
            t.Abort();
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
        }

        private void rep_Click(object sender, EventArgs e)
        {
            if (InternetGetConnectedState(out Out, 0) == true) noco = "0";
            else noco = "1";

            if (noco == "1")
            {
                var s = new nonet();
                s.ShowDialog();
                this.Close();
            }
            else
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
                Thread t = new Thread(new ThreadStart(WaitScreen));
                t.Start();

                using (var web = new WebClient())
                {
                    web.DownloadFile("https://dl.dropbox.com/s/lahofrvpejlclkx/Winre.wim", "C:\\Windows\\System32\\Recovery\\WinRE.wim");
                }

                var p = new Process();
                p.StartInfo.FileName = "reagentc";
                p.StartInfo.Arguments = "/setreimage /path C:\\Windows\\System32\\Recovery";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();

                var p2 = new Process();
                p2.StartInfo.FileName = "reagentc";
                p2.StartInfo.Arguments = "/enable";
                p2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p2.Start();
                p2.WaitForExit();
                debwinre.Text = "0";

                check();
                t.Abort();
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            }
        }
    }
}
