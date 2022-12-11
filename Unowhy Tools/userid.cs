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
using static Unowhy_Tools.UTclass;

namespace Unowhy_Tools
{
    public partial class userid : Form
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

        public userid(string username)
        {
            InitializeComponent();

            lab.Text = getlang("newuserid");

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
