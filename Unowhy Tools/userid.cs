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

namespace Unowhy_Tools
{
    public partial class userid : Form
    {

        string resxFile = "null";

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

        public userid(string username)
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

            lab.Text = resxSet.GetString("newuserid");

            string sid = ".\\" + username;
            id.Text = sid;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userid_Load(object sender, EventArgs e)
        {

        }
    }
}
