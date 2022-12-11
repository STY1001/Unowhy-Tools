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
using System.Text.RegularExpressions;
using Microsoft.WindowsAPICodePack.Taskbar;
using Microsoft.WindowsAPICodePack.Shell;
using System.Threading;
using Unowhy_Tools.Properties;
using static Unowhy_Tools.UTclass;

namespace Unowhy_Tools
{
    public partial class PCName : Form
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

        public PCName()
        {
            InitializeComponent();

            cncat.Text = getlang("cncat");
            ancat.Text = getlang("ancat");
            close.Text = getlang("cancel");
            avert.Text = getlang("avert");
            this.Text = getlang("pcname");

            actualname.Text = returncmd("hostname","");
        }

        private void pcname_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ok_Click(object sender, EventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_]");

            if (r.IsMatch(changename.Text.ToString().Trim()) || changename.Text.Length < 2 || changename.Text.Length > 15 || changename.Text.Contains(" "))
            {
                this.Height = 244;
                this.Width = 236;
                avert.Visible = true;
            }
            else
            {
                this.Height = 169;
                this.Width = 236;
                string msg = this.Text;
                Image ico = Resources.customize;
                dialog d = new dialog(msg, ico);
                d.ShowDialog();
                if (d.DialogResult.Equals(DialogResult.Yes))
                {
                    avert.Visible = false;
                    string name = changename.Text;
                    string arg = ($"-Command \"& {{Rename-Computer -NewName \"{name}\" -Force}}\"");

                    runmin("powershell", arg, true);
                    actualname.Text = returncmd("hostname", "");

                    var f = new reboot();
                    f.ShowDialog();
                }
            }
        }
    }
}
