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
    public partial class bkconv : Form
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

        public bkconv()
        {
            InitializeComponent();
            browsen.Text = getlang("browse");
            browseo.Text = getlang("browse");
            labnew.Text = getlang("new");
            labold.Text = getlang("old");
            conv.Text = getlang("conv");
            this.Text = getlang("conv.form");
        }

        private void browseo_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    patho.Text = fbd.SelectedPath;
                    if(!File.Exists(patho.Text + "\\UT-Restore.exe"))
                    {
                        var d = new info(getlang("conv.nout"), Properties.Resources.no);
                        d.ShowDialog();
                        patho.Text = "";
                    }
                }
            }
        }

        private void browsen_Click(object sender, EventArgs e)
        {
            using(var sfd = new SaveFileDialog())
            {
                sfd.FileName = "UT-Drv_" + DateTime.Now.ToString("HH-mm_dd-MM-yy");
                sfd.DefaultExt = "zip";
                sfd.Filter = "zip (*.zip)|*.zip";
                sfd.FilterIndex = 1;
                sfd.Title = "Unowhy Tools";
                DialogResult result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pathn.Text = sfd.FileName;
                }
            }
        }

        private void conv_Click(object sender, EventArgs e)
        {
            if(pathn.Text == "" || patho.Text == "")
            {

            }
            else
            {
                runmin("7zip.exe", "a \"" + pathn.Text + "\" \"" + patho.Text + "\\*\" -tzip -mx=0", true);
                var d = new info(getlang("done"), Properties.Resources.yes);
                d.ShowDialog();
            }
        }
    }
}
