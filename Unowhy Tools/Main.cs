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

namespace Unowhy_Tools
{   
    public partial class main : Form
    {
        

        public main()
        {
            RegistryKey keysty = Registry.CurrentUser.OpenSubKey(@"Software\STY1001", false);
            if (keysty != null)
            {

            }
            else
            {
                RegistryKey stykey = Registry.CurrentUser.OpenSubKey(@"Software", true);
                stykey.CreateSubKey("STY1001");
            }

            RegistryKey keyut = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            if (keyut != null)
            {

            }
            else
            {
                RegistryKey utkey = Registry.CurrentUser.OpenSubKey(@"Software\STY1001", true);
                utkey.CreateSubKey("Unowhy Tools");
                System.Diagnostics.Process.Start(".\\langset.exe");
            }

            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();
            if (utls == "EN")
            {
                
            }
            else
            {
                
            }


            InitializeComponent();

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            System.Diagnostics.Process.Start(".\\starthis.exe");    // Start HiSqool Manager
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\stophis.exe");     // Stop HiSqool Manager
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\enhis.exe");       // Enable HiSqool Manager
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\dishis.exe");      // Disable HiSqool Manager
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\delhisqool.exe");      // Launch Uninstaller of  HiSqool
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\rmdirhismgr.exe");     // Remove HiSqool Manager folder
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\shell.exe");       // Change Shell value
        }

        private void button8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\delent.exe");      // Delete ENT account
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            var a = new About();    //Launch About form
            a.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\fixti.exe");   //Delete silent_*.vbs.lnk
        }

        private void button11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\rdti.exe");    //Remove "TO_INSTALL"
        }

        private void settings_Click(object sender, EventArgs e)
        {
            var s = new Settings();
            s.Show();
            //this.Close();
        }
    }
}
