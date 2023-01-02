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
using static Unowhy_Tools.UTclass;

namespace Unowhy_Tools
{
    public partial class Settings : Form
    {
        

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
        }

        public string fsr = "0";
        public Settings(string fs)
        {
            if(fs == "1")
            {
                fsr = "1";
            }

            InitializeComponent();

            string fp = Path.GetTempPath() + "\\UT_Logs.txt";
            FileInfo fi = new FileInfo(fp);
            string size;
            if (fi.Length > 1000000) size = (fi.Length / 1000000).ToString() + " MB";
            else size = (fi.Length / 1000).ToString() + " KB";

            this.Text = getlang("settings");
            cbupdate.Text = getlang("cuab");
            loglab.Text = getlang("logsclean");
            clean.Text = getlang("clean") + " (" + size + ")";

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
                runmin("reg", "add \"HKCU\\Software\\STY1001\\Unowhy Tools\" /v Lang /d EN /t REG_SZ /f", true);
            }
            else
            {
                runmin("reg", "add \"HKCU\\Software\\STY1001\\Unowhy Tools\" /v Lang /d FR /t REG_SZ /f", true);
            }

            if (cbupdate.Checked == true)
            {
                runmin("reg", "add \"HKCU\\Software\\STY1001\\Unowhy Tools\" /v UpdateStart /d 1 /t REG_SZ /f", true);
            }
            else
            {
                runmin("reg", "add \"HKCU\\Software\\STY1001\\Unowhy Tools\" /v UpdateStart /d 0 /t REG_SZ /f", true);
            }


            if (fsr == "1")
            {
                this.Close();
            }
            else
            {
                this.Close();
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

        private void clean_Click(object sender, EventArgs e)
        {
            string fp = Path.GetTempPath() + "\\UT_Logs.txt";
            File.Create(fp).Close();
            FileInfo fi = new FileInfo(fp);
            string size;
            if (fi.Length > 1000000) size = (fi.Length / 1000000).ToString() + " MB";
            else size = (fi.Length / 1000).ToString() + "KB";
            clean.Text = getlang("clean") + " (" + size + ")";
        }
    }
}
