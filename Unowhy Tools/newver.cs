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
using System.Net;
using System.IO;
using static Unowhy_Tools.UTclass;
using Unowhy_Tools.Properties;

namespace Unowhy_Tools
{
    public partial class newver : Form
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

        public newver()
        {
            WebClient wc = new WebClient();
            string sgitver = wc.DownloadString("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/Version.txt");
            int igitver = Convert.ToInt32(sgitver);
            string gitver = Convert.ToString(igitver);
            string sprogver = Resources.ver.ToString();
            int iprogver = Convert.ToInt32(sprogver);
            string progver = Convert.ToString(iprogver);

            string fullver = progver + ".0" + " -> " + gitver + ".0";

            InitializeComponent();

            newverlab.Text = getlang("newver");
            updatenow.Text = getlang("unow");

            vud.Text = fullver;
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
            client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/Unowhy%20Tools%20Updater.exe"), ".\\updater.exe");
            
        }

        private void dl_complete(object sender, AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(".\\updater.exe");
        }

        private void clog_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void log_Click(object sender, EventArgs e)
        {
            var l = new changelog();
            l.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
