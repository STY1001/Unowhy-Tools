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
using static Unowhy_Tools.UTclass;

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

        private int Out;
        private string noco;

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int state, int value);

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
            InitializeComponent();

            ena.Text = getlang("enable");
            dis.Text = getlang("disable");
            rep.Text = getlang("repair");
            this.Text = getlang("winre.title");

            if (File.Exists("debug"))
            {
                debwinre.Visible = true;
            }

            check();
        }

        private void ena_Click(object sender, EventArgs e)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
            waitstatus.open();
            debwinre.Text = "1";
            runmin("reagentc.exe", "/enable", true);
        }

        private void check()
        {

            string out1 = returncmd("reagentc.exe", "/info");
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
                    repmsg.Text = getlang("winremsg");
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
            debwinre.Text = "Winre";
            var p = new Process();
            runmin("reagentc.exe", "/disable", true);
            check();
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
                waitstatus.open();

                using (var web = new WebClient())
                {
                    web.DownloadFile("https://dl.dropbox.com/s/lahofrvpejlclkx/Winre.wim", "C:\\Windows\\System32\\Recovery\\WinRE.wim");
                }

                runmin("reagentc.exe", "/setreimage /path C:\\Windows\\System32\\Recovery", false);
                runmin("reagentc.exe", "/enable", false);

                debwinre.Text = "0";

                check();
                waitstatus.close();
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            }
        }
    }
}
