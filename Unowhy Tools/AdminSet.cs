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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unowhy_Tools.Properties;
using static Unowhy_Tools.UTclass;

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
            InitializeComponent();

            this.Text = getlang("adminset");
            enable.Text = getlang("enablea");
            disable.Text = getlang("disablea");
            passbtn.Text = getlang("snpw");
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
                runmin("net", arg, true);
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
                runmin("net", "user Administrateur /active:yes", true);
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
                runmin("net", "user Administrateur /active:no", true);
            }
        }
    }
}
