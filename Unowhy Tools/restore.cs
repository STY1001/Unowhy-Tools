using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Unowhy_Tools.UTclass;

namespace Unowhy_Tools
{
    public partial class restore : Form
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

        public restore()
        {
            InitializeComponent();
            this.Text = getlang("drvgui");
            labconv.Text = getlang("conv");
            labres.Text = getlang("restore");
            textconv.Text = getlang("convdesc");
            browse.Text = getlang("browse");
            rest.Text = getlang("restore.short");
            conv.Text = getlang("conv");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fb = new OpenFileDialog())
            {
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

        private void rest_Click(object sender, EventArgs e)
        {
            if(path.Text == "")
            {

            }
            else
            {
                DriveInfo di = new DriveInfo("C");
                FileInfo fi = new FileInfo(path.Text);
                if (di.AvailableFreeSpace > fi.Length * 2.5)
                {
                    Directory.CreateDirectory(".\\temp\\drv-temp");
                    runmin("7zip.exe", "x \"" + path.Text + "\" -o\".\\temp\\drv-temp\" -aoa", true);
                    if (File.Exists(".\\temp\\drv-temp\\UT-Restore.exe"))
                    {
                        runmin(".\\temp\\drv-temp\\UT-Restore.exe", "", true);
                    }
                    Directory.Delete(".\\temp\\drv-temp", true);
                    var r = new reboot();
                    r.ShowDialog();
                }
                else
                {
                    var d = new info(getlang("space6gpc"), Properties.Resources.no);
                    d.ShowDialog();
                }
            }
        }

        private void conv_Click(object sender, EventArgs e)
        {
            var c = new bkconv();
            c.ShowDialog();
        }
    }
}
