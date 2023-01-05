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
    public partial class backup : Form
    {
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

        public backup()
        {
            InitializeComponent();

            this.Text = getlang("drvgui");
            labapp.Text = getlang("bkapp");
            labbak.Text = getlang("backup");
            browse.Text = getlang("browse");
            back.Text = getlang("backup.short");
        }

        private void browse_Click(object sender, EventArgs e)
        {
            using (var fb = new SaveFileDialog())
            {
                fb.FileName = "UT-Drv_" + DateTime.Now.ToString("HH-mm_dd-MM-yy");
                fb.DefaultExt = "zip";
                fb.Filter = "zip (*.zip)|*.zip";
                fb.FilterIndex = 1;
                fb.Title = "Unowhy Tools";
                DialogResult result = fb.ShowDialog();
                if (result == DialogResult.OK)
                {
                    path.Text = fb.FileName;
                }
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            if (path.Text == "")
            {

            }
            else
            {
                string drivel = path.Text.Substring(0, 1);
                DriveInfo di = new DriveInfo(drivel);
                long afs = di.AvailableFreeSpace;

                if (afs > 6000000000)
                {
                    Directory.CreateDirectory(drivel + ":\\UT-Drv-TMP");
                    string backuptemp = drivel + ":\\UT-Drv-TMP";
                    File.Copy(".\\UT-Restore.exe", backuptemp + "\\UT-Restore.exe");
                    if (dism.Checked)
                    {
                        runmin("dism.exe", "/online /export-driver /destination:\"" + backuptemp + "\"", true);
                    }
                    else if (ps.Checked)
                    {
                        runmin("powershell.exe", "Export-WindowsDriver -Online -Destination \"" + backuptemp + "\"", true);
                    }
                    runmin("7zip.exe", "a \"" + path.Text + "\" \"" + backuptemp + "\\*\" -tzip -mx=0", true);
                    Directory.Delete(backuptemp, true);
                    FileInfo fi = new FileInfo(path.Text);
                    if (fi.Length > 1000000)
                    {
                        var d = new info(getlang("done"), Properties.Resources.yes);
                        d.ShowDialog();
                    }
                    else
                    {
                        var d = new info(getlang("bkfail"), Properties.Resources.no);
                        d.ShowDialog();
                    }
                }
                else
                {
                    var d = new info(getlang("space6gusb"), Properties.Resources.no);
                    d.ShowDialog();
                }
            }
        }
    }
}
