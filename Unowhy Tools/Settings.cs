using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Resources;

namespace Unowhy_Tools
{
    public partial class Settings : Form
    {
        public string resxFile = "null";

        public Settings()
        {

            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            if (utls == "EN") resxFile = @".\en.resx";
            else resxFile = @".\fr.resx";

            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            
            this.Text = resxSet.GetString("settings");

            RegistryKey lcs = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utlst = lcs.GetValue("Lang").ToString();
            if (utlst == "EN")
            {
                langsel.Text = "English";
            }
            else
            {
                langsel.Text = "French";
            }
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(".\\restart.exe");
        }



        private void langsel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (langsel.Text == "English")
            {
                System.Diagnostics.Process.Start(".\\langen.exe");
            }
            else
            {
                System.Diagnostics.Process.Start(".\\langfr.exe");
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }
    }
}
