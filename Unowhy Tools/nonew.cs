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

namespace Unowhy_Tools
{
    public partial class nonew : Form
    {
        public string resxFile = "null";

        public nonew()
        {

            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            if (utls == "EN") resxFile = @".\en.resx";
            else resxFile = @".\fr.resx";

            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            noverlab.Text = resxSet.GetString("nover");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nonew_Load(object sender, EventArgs e)
        {

        }
    }
}
