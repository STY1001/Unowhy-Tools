using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Resources;
using Microsoft.Win32;

namespace Unowhy_Tools
{
    public partial class dialog : Form
    {
        public string resxFile = "null";


        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
        }

        public dialog(string msg)
        {
            //Check the current saved language
            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            if (utls == "EN") resxFile = @".\en.resx";    //English   //Chose the ResX file
            else resxFile = @".\fr.resx";               //French

            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            yes.Text = resxSet.GetString("yes").ToString();
            no.Text = resxSet.GetString("no").ToString();

            string msgfull = msg + " ?";
            label.Text = msgfull;

        }

        private void dialog_Load(object sender, EventArgs e)
        {

        }

        private void yes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void no_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
