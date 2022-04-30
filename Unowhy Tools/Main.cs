using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace Unowhy_Tools
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\starthis.exe");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\stophis.exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\enhis.exe");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\dishis.exe");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\delhisqool.exe");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\rmdirhismgr.exe");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\shell.exe");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\delent.exe");
        }
    }
}
