using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Resources;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Unowhy_Tools
{
    public partial class Settings : Form
    {
        

        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        public string resxFile = "null";
        public string fsr = "0";
        public Settings(string fs)
        {
            if(fs == "1")
            {
                fsr = "1";
            }

            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            if (utls == "EN") resxFile = @".\en.resx";
            else resxFile = @".\fr.resx";

            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            
            this.Text = resxSet.GetString("settings");
            cbupdate.Text = resxSet.GetString("cuab");

            RegistryKey lcs = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utlst = lcs.GetValue("Lang").ToString();
            if (utlst == "EN")
            {
                langsel.Text = "English";
            }
            else
            {
                langsel.Text = "French";
            }
            string utcuab = lcs.GetValue("UpdateStart").ToString();
            if (utcuab == "1")
            {
                cbupdate.Checked = true;
            }
            else
            {
                cbupdate.Checked = false;
            }

            
        }

        public void okbtn_Click(object sender, EventArgs e)
        {

            if (langsel.Text == "English")
            {
                Process p = new Process();
                p.StartInfo.FileName = ".\\langen.exe";
                p.StartInfo.Arguments = "";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();
            }
            else
            {
                Process p = new Process();
                p.StartInfo.FileName = ".\\langfr.exe";
                p.StartInfo.Arguments = "";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();
            }

            if (cbupdate.Checked == true)
            {
                Process p = new Process();
                p.StartInfo.FileName = ".\\cuabon.exe";
                p.StartInfo.Arguments = "";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();
            }
            else
            {
                Process p = new Process();
                p.StartInfo.FileName = ".\\cuaboff.exe";
                p.StartInfo.Arguments = "";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();
            }

            //System.Diagnostics.Process.Start(".\\restart.exe");
            if (fsr == "1")
            {
                this.Close();
            }
            else
            {
                Application.Restart();
            }
            
        }



        private void langsel_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void cbupdate_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
