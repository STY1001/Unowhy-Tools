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
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;

namespace Unowhy_Tools
{
    public partial class newver : Form
    {
        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        public string resxFile = "null";

        public newver()
        {
            string sgitver = System.IO.File.ReadAllText(".\\gitversion.txt");      //Convert text to string
            int igitver = Convert.ToInt32(sgitver);
            string gitver = Convert.ToString(igitver);
            string sprogver = System.IO.File.ReadAllText(".\\version.txt");
            int iprogver = Convert.ToInt32(sprogver);
            string progver = Convert.ToString(iprogver);

            string fullver = progver + ".0" + " -> " + gitver + ".0";


            using(WebClient web = new WebClient())
            {
                web.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/changelog.txt", ".\\temp\\changelog.txt");
            }


            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            if (utls == "EN") resxFile = @".\en.resx";
            else resxFile = @".\fr.resx";

            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            newverlab.Text = resxSet.GetString("newver");
            updatenow.Text = resxSet.GetString("unow");

            vud.Text = fullver;
            clog.Text = File.ReadAllText(".\\temp\\changelog.txt");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/STY1001/Unowhy-Tools/releases/latest");
        }

        private void newver_Load(object sender, EventArgs e)
        {

        }

        private void updatenow_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(dl_complete);
            client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/Unowhy%20Tools%20Updater.exe"), ".\\Unowhy Tools Updater.exe");
            
        }

        private void dl_complete(object sender, AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(".\\Unowhy Tools Updater.exe");
        }
    }
}
