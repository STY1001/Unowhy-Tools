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
using static Unowhy_Tools.UTclass;

namespace Unowhy_Tools
{
    public partial class reboot : Form
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


        public reboot()
        {
            InitializeComponent();

            msg.Text = getlang("reboot");
            restart.Text = getlang("restart");

        }

        private void reboot_Load(object sender, EventArgs e)
        {

        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            runmin("shutdown.exe", "-r -t 10 -c \"Unowhy Tools\"", false);
            Application.Exit();
        }
    }
}
