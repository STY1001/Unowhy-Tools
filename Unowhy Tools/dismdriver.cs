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
using System.ServiceProcess;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using static Unowhy_Tools.UTclass;

namespace Unowhy_Tools
{
    public partial class dismdriver : Form
    {
        public string resxFile = "null";

        //Set dark mode title bar and bypass wow64 redirection

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);
        }

        public dismdriver()
        {
            InitializeComponent();

            if (File.Exists("debug"))
            {
                deb.Visible = true;
            }

            oeb.Text = getlang("browse");
            ieb.Text = getlang("browse");
            bb.Text = getlang("backup.short");
            rb.Text = getlang("restore.short");
            this.Text = getlang("dismdrv.title");
            labb.Text = getlang("backup");
            labr.Text = getlang("restore");
        }

        private void oeb_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    opb.Text = fbd.SelectedPath + "\\UT-DISM-Drv_" + DateTime.Now.ToString("HH-mm_dd-MM-yy");
                    string prec = opb.Text;
                    string postc = prec.Replace("\\\\", "\\");
                    opb.Text = postc;
                }
            }
        }

        private void bb_Click(object sender, EventArgs e)
        {
            if (opb.Text == "")
            {

            }
            else
            {
                Directory.CreateDirectory(opb.Text);
                string arg = "/online /export-driver /destination:" + "\"" + opb.Text + "\"";

                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
                Thread t = new Thread(new ThreadStart(WaitScreen));               
                t.Start();

                runmin("dism.exe", arg, false);

                File.Copy(".\\UT-Restore.exe", opb.Text + "\\UT-Restore.exe");

                t.Abort();
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
        }

        private void ieb_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ipb.Text = fbd.SelectedPath + "\\UT-Restore.exe";
                    deb.Text = fbd.SelectedPath;
                }
            }
        }

        private void rb_Click(object sender, EventArgs e)
        {
            if (ipb.Text == "")
            {

            }
            else
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
                Thread t2 = new Thread(new ThreadStart(WaitScreen));
                t2.Start();

                if (File.Exists(ipb.Text) == false)
                {
                    File.Copy(".\\UT-Restore.exe", ipb.Text);
                }

                Process p = new Process();
                p.StartInfo.FileName = ipb.Text;
                p.StartInfo.Arguments = "";
                p.StartInfo.WorkingDirectory = deb.Text;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();

                t2.Abort();
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                var r = new reboot();
                r.ShowDialog();
            }
        }
    }
}
