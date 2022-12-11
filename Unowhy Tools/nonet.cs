using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Globalization;
using System.Resources;
using System.Runtime.InteropServices;
using static Unowhy_Tools.UTclass;

namespace Unowhy_Tools
{
    public partial class nonet : Form
    {
        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
        }

        public string resxFile = "null";

        public nonet()
        {
            InitializeComponent();

            noco.Text = getlang("nonet");
        }

        private void ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void noco_Click(object sender, EventArgs e)
        {

        }
    }
}
