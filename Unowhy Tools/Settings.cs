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

namespace Unowhy_Tools
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();
            if (utls == "EN")
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
           
            this.Close();
            
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
    }
}
