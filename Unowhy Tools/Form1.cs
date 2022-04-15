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
            string starthisqool = "start HiSqoolManager";
            System.Diagnostics.Process.Start("net.exe", starthisqool);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string stophisqool = "stop HiSqoolManager";
            System.Diagnostics.Process.Start("net.exe", stophisqool);
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string enablehisqool = "config hisqoolmanager start=auto";
            System.Diagnostics.Process.Start("sc.exe", enablehisqool);
            string starthisqool = "start HiSqoolManager";
            System.Diagnostics.Process.Start("net.exe", starthisqool);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string stophisqool = "stop HiSqoolManager";
            System.Diagnostics.Process.Start("net.exe", stophisqool);
            string disablehisqool = "config hisqoolmanager start=disabled";
            System.Diagnostics.Process.Start("sc.exe", disablehisqool);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:\\Program Files\\Unowhy\\Hisqool\\uninstall hisqool.exe");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string stophisqool = "stop HiSqoolManager";
            System.Diagnostics.Process.Start("net.exe", stophisqool);
            string disablehisqool = "config hisqoolmanager start=disabled";
            System.Diagnostics.Process.Start("sc.exe", disablehisqool);
            string removehisqool = @"/c rmdir c:\Program Files\Unowhy\Hisqool Manager";
            System.Diagnostics.Process.Start("cmd.exe", removehisqool);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string shell = "add '\\Computer\\HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon' /v 'Shell' /t REG_SZ /d 'explorer.exe'";
            System.Diagnostics.Process.Start("reg.exe", shell);
        }
    }
}
