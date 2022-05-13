using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Unowhy_Tools
{
    public partial class About : Form
    {
        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        public string resxFile = "null";

        public About()
        {
            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            if (utls == "EN") resxFile = @".\en.resx";
            else resxFile = @".\fr.resx";

            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            update.Text = resxSet.GetString("udcheck");
            this.Text = resxSet.GetString("about");
        }

        private void button3_Click(object sender, EventArgs e)
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

            if(progint < gitint)        //Check if there is a new vertion of UT
            {
                var nv = new newver();  
                nv.Show();
            }
            else
            {
                var nov = new nonew();
                nov.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://sty1001.wordpress.com");  //Link button
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/STY1001/Unowhy-Tools");    //Link button
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}
