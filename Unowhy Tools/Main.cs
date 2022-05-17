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
        public string resxFile = "null";

        //Set dark mode title bar
        [DllImport("DwmApi")] 
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }


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

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools");       //Open key
            object o = key.GetValue("Lang", null);
            if (o != null)
            {

            }
            else
            {
                System.Diagnostics.Process.Start(".\\langset.exe");
                System.Threading.Thread.Sleep(1000);                     //Wait the registery editing
                var s = new Settings();
                s.StartPosition = FormStartPosition.WindowsDefaultLocation;
                s.ShowDialog();                                                //Show settings
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
                        s.ShowDialog();
                    }
                    else
                    {
                        
                    }
                }
            }
            else
            {
                System.Diagnostics.Process.Start(".\\cuabon.exe");      //Set check boot at startup at on
                System.Threading.Thread.Sleep(1000);    //Wait the registery editing
            }

            //Check the current saved language
            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            string enresx = @".\en.resx" ;
            string frresx = @".\fr.resx" ;
                                                     //Chose the ResX file
            if (utls == "EN")resxFile = enresx ;    //English   
            else resxFile = frresx ;               //French

            

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

        private void starthis_Click(object sender, EventArgs e)
        {
            string msg = starthis.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\starthis.exe");    // Start HiSqool Manager  
            }
                      
        }

        private void stophis_Click(object sender, EventArgs e)
        {
            string msg = stophis.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\stophis.exe");     // Stop HiSqool Manager
            }
        }

        private void enhis_Click(object sender, EventArgs e)
        {
            string msg = enhis.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\enhis.exe");       // Enable HiSqool Manager
            }
        }

        private void dishis_Click(object sender, EventArgs e)
        {
            string msg = dishis.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\dishis.exe");      // Disable HiSqool Manager
            }
        }

        private void delhis_Click(object sender, EventArgs e)
        {
            string msg = delhis.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\delhisqool.exe");      // Launch Uninstaller of  HiSqool
            }
        }

        private void delhism_Click(object sender, EventArgs e)
        {
            string msg = delhism.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\rmdirhismgr.exe");     // Remove HiSqool Manager folder
            }
        }

        private void shell_Click(object sender, EventArgs e)
        {
            string msg = shell.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\shell.exe");       // Change Shell value
            }
        }

        private void delent_Click(object sender, EventArgs e)
        {
            string msg = ent.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\delent.exe");      // Delete ENT account
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void about_Click(object sender, EventArgs e)
        {
            var a = new About();    //Launch About form
            a.ShowDialog();
        }

        private void fixboot_Click(object sender, EventArgs e)
        {
            string msg = fixboot.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\fixti.exe");   //Delete silent_*.vbs.lnk
            }
        }

        private void delti_Click(object sender, EventArgs e)
        {
            string msg = delti.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\rdti.exe");    //Remove "TO_INSTALL"
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {
            var s = new Settings();     //Show settings
            s.ShowDialog();
        }

        private void delridf_Click(object sender, EventArgs e)
        {
            string msg = delridf.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\delridf.exe");     //Delete "RIDF"
            }
        }

        private void winre_Click(object sender, EventArgs e)
        {
            string msg = winre.Text;
            dialog d = new dialog(msg);
            d.ShowDialog();
            if (d.DialogResult.Equals(DialogResult.Yes))
            {
                System.Diagnostics.Process.Start(".\\winre.exe");       //Enable reagent
            }
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void pcname_Click(object sender, EventArgs e)
        {
            var pcn = new pcname();
            pcn.ShowDialog();
        }
    }
}
