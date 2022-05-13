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

namespace Unowhy_Tools
{   
    public partial class main : Form
    {

        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }


        public string resxFile = "null";

        public main()
        {
            
            RegistryKey keysty = Registry.CurrentUser.OpenSubKey(@"Software\STY1001", false);   //Check if the  "STY1001" key exist
            if (keysty != null)
            {

            }
            else
            {
                RegistryKey stykey = Registry.CurrentUser.OpenSubKey(@"Software", true);    //Create it
                stykey.CreateSubKey("STY1001");
            }

            RegistryKey keyut = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);   //Check if "UT" key exist
            if (keyut != null)
            {

            }
            else
            {
                RegistryKey utkey = Registry.CurrentUser.OpenSubKey(@"Software\STY1001", true);     //Create it with "Lang" value
                utkey.CreateSubKey("Unowhy Tools");
                
            }

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools");
            object o = key.GetValue("Lang", null);
            if (o != null)
            {

            }
            else
            {
                System.Diagnostics.Process.Start(".\\langset.exe");
                System.Threading.Thread.Sleep(1000);    //Wait the registery editing
                var s = new Settings();
                s.StartPosition = FormStartPosition.WindowsDefaultLocation;
                s.Show();
                s.StartPosition = FormStartPosition.CenterScreen;
            }

            object u = key.GetValue("UpdateStart", null);
            if (u != null)
            {
                string utlu = key.GetValue("UpdateStart").ToString();
                if (utlu == "1")
                {
                    if (File.Exists("tversion.txt"))    //Check if the file exist
                    {
                        File.Delete("tversion.txt");    //Delete it if exist
                    }

                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/Version.txt", ".\\tversion.txt");     //Download Version file
                    }

                    string gitver = System.IO.File.ReadAllText(".\\tversion.txt");      //Convert text to string
                    string progver = System.IO.File.ReadAllText(".\\version.txt");

                    int gitint = Convert.ToInt32(gitver);       //Convert string to int
                    int progint = Convert.ToInt32(progver);

                    if (progint < gitint)        //Check if there is a new vertion of UT
                    {
                        var s = new newver();
                        s.StartPosition = FormStartPosition.WindowsDefaultLocation;
                        s.Show();
                        s.StartPosition = FormStartPosition.CenterScreen;
                    }
                    else
                    {
                        
                    }
                }
            }
            else
            {
                System.Diagnostics.Process.Start(".\\cuabon.exe");
                System.Threading.Thread.Sleep(1000);    //Wait the registery editing
            }

            //Check the current saved language
            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            if(utls == "EN")resxFile = @".\en.resx";    //English   //Chose the ResX file
            else resxFile = @".\fr.resx";               //French

            

            InitializeComponent();


            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            //Apply Resources Strings
            starthis.Text = resxSet.GetString("starthis");
            stophis.Text = resxSet.GetString("stophis");
            enhis.Text = resxSet.GetString("enhis");
            dishis.Text = resxSet.GetString("dishis");
            delhis.Text = resxSet.GetString("delhis");
            delhism.Text = resxSet.GetString("delhism");
            delcat.Text = resxSet.GetString("delcat");
            repaircat.Text = resxSet.GetString("repaircat");
            servicecat.Text = resxSet.GetString("servicecat");
            shell.Text = resxSet.GetString("shell");
            fixboot.Text = resxSet.GetString("fixboot");
            ent.Text = resxSet.GetString("ent");
            delti.Text = resxSet.GetString("delti");
            about.Text = resxSet.GetString("about");
            settings.Text = resxSet.GetString("settings");
            deschism.Text = resxSet.GetString("deschism");
            descshell.Text = resxSet.GetString("descshell");
            descstart.Text = resxSet.GetString("descstart");
            deschisdel.Text = resxSet.GetString("deschisdel");
            deschismdel.Text = resxSet.GetString("deschismdel");
            desctidel.Text = resxSet.GetString("desctidel");
            descent.Text = resxSet.GetString("descent");
            delridf.Text = resxSet.GetString("delridf");
            descridf.Text = resxSet.GetString("descridf");
            winre.Text = resxSet.GetString("winre");
            descwinre.Text = resxSet.GetString("descwinre");


            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = starthis.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question);  // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\starthis.exe");    // Start HiSqool Manager
            }
            if (conf == DialogResult.No)
            {

            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg = stophis.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\stophis.exe");     // Stop HiSqool Manager
            }
            if (conf == DialogResult.No)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string msg = enhis.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\enhis.exe");       // Enable HiSqool Manager
            }
            if (conf == DialogResult.No)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg = dishis.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\dishis.exe");      // Disable HiSqool Manager
            }
            if (conf == DialogResult.No)
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string msg = delhis.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\delhisqool.exe");      // Launch Uninstaller of  HiSqool
            }
            if (conf == DialogResult.No)
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string msg = delhism.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\rmdirhismgr.exe");     // Remove HiSqool Manager folder
            }
            if (conf == DialogResult.No)
            {

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string msg = shell.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\shell.exe");       // Change Shell value
            }
            if (conf == DialogResult.No)
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string msg = ent.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\delent.exe");      // Delete ENT account
            }
            if (conf == DialogResult.No)
            {

            }
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
            string msg = fixboot.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\fixti.exe");   //Delete silent_*.vbs.lnk
            }
            if (conf == DialogResult.No)
            {

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string msg = delti.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\rdti.exe");    //Remove "TO_INSTALL"
            }
            if (conf == DialogResult.No)
            {

            }
        }

        private void settings_Click(object sender, EventArgs e)
        {
            var s = new Settings();     //Show settings
            s.Show();
            //this.Close();
        }

        private void delridf_Click(object sender, EventArgs e)
        {
            string msg = delridf.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\delridf.exe");
            }
            if (conf == DialogResult.No)
            {

            }
        }

        private void winre_Click(object sender, EventArgs e)
        {
            string msg = winre.Text;
            string msg2 = "Confirmation";
            string msg1 = msg + " ?";
            DialogResult conf = MessageBox.Show(msg1, msg2, MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Confirmation
            if (conf == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(".\\winre.exe");
            }
            if (conf == DialogResult.No)
            {

            }
        }
    }
}
