using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unowhy_Tools.Properties;

namespace Unowhy_Tools
{
    public partial class AdminSet : Form
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

        public AdminSet()
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

            this.Text = resxSet.GetString("adminset");
            enable.Text = resxSet.GetString("enablea");
            disable.Text = resxSet.GetString("disablea");
            passbtn.Text = resxSet.GetString("snpw");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = passbtn.Text;
            Image ico = Resources.key;
            dialog d = new dialog(msg, ico);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                string pass = passbox.Text;
                string arg = ($"user Administrateur \"{pass}\"");
                Process p = new Process();
                p.StartInfo.FileName = "net";
                p.StartInfo.Arguments = arg;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();
            }
        }

        private void enable_Click(object sender, EventArgs e)
        {
            string msg = enable.Text;
            Image ico = Resources.enable;
            dialog d = new dialog(msg, ico);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                Process p = new Process();
                p.StartInfo.FileName = ".\\enadmin.exe";
                p.StartInfo.Arguments = "";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();
            }
        }

        private void disable_Click(object sender, EventArgs e)
        {
            string msg = disable.Text;
            Image ico = Resources.disable;
            dialog d = new dialog(msg, ico);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                Process p = new Process();
                p.StartInfo.FileName = ".\\disadmin.exe";
                p.StartInfo.Arguments = "";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();
            }
        }
    }
}
