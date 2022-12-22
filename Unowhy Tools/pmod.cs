using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unowhy_Tools
{
    public partial class pmod : Form
    {
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

        public pmod()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = new About();
            a.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var a = new Adduser();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var a = new AdminSet();
            a.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var a = new changelog();
            a.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var a = new dialog("This is dialog", Properties.Resources.question);
            a.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var a = new newver();
            a.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var a = new nonew();
            a.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var a = new nonet();
            a.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var a = new PCInfo("STY1001");
            a.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var a = new PCName();
            a.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var a = new backup();
            a.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var a = new reboot();
            a.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var a = new Settings("1");
            a.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var a = new Splash();
            a.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var a = new userid("STY1001");
            a.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var a = new wait();
            a.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var a = new restore();
            a.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var a = new winreset();
            a.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var a = new info("This is an info", Properties.Resources.about);
            a.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var a = new bkconv();
            a.Show();
        }
    }
}
