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
using System.Runtime.CompilerServices;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace UTwait
{
    public partial class wait : Form
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

        public wait()
        {
            InitializeComponent();
            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();
            if (utls == "EN")
            {
                this.Text = "Please wait";
                msg.Text = "Please wait...";
            }
            else
            {
                this.Text = "Veuillez patienter";
                msg.Text = "Veuillez patienter...";
            }
        }

        private void wait_Load(object sender, EventArgs e)
        {

        }
    }
}
