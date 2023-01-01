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
using Unowhy_Tools.Properties;

namespace Unowhy_Tools
{
    public partial class About : Form
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

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int state, int value);

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

            string ver = Unowhy_Tools.Properties.Resources.Version.ToString() + Environment.NewLine + Unowhy_Tools.Properties.Resources.Debug.ToString();
            aver.Text = ver;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string noco = "0";

            int Out;
            if (InternetGetConnectedState(out Out, 0) == true) noco = "0";
            else noco = "1";

            if (noco == "1")
            {
                var s = new nonet();
                s.ShowDialog();
            }
            else
            {
                string gitver;

                using (var client = new WebClient())
                {
                    gitver = client.DownloadString("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/Version.txt");
                }

                string progver = Resources.ver.ToString();

                int gitint = Convert.ToInt32(gitver);
                int progint = Convert.ToInt32(progver);

                if (progint < gitint)        //Check if there is a new vertion of UT
                {
                    var s = new newver();
                    s.ShowDialog();
                }
                else
                {
                    var s = new nonew();
                    s.ShowDialog();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://sty1001.cf");  //Link button
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void discord_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.com/invite/dw3ZJ9u7WS");
        }
    }
}
