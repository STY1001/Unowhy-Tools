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
using System.Windows;
using System.Threading;
using System.Windows.Input;

using static Unowhy_Tools.UTclass;

namespace Unowhy_Tools
{
    public partial class PCInfo : Form
    {

        public string resxFile = "null";

                               //Set dark mode title bar
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
        }

        public PCInfo(string us)
        {
            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            string enresx = @".\en.resx";
            string frresx = @".\fr.resx";
            //Chose the ResX file
            if (utls == "EN") resxFile = enresx;    //English   
            else resxFile = frresx;               //French

            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);


            //Apply Language

            this.Text = resxSet.GetString("pcinfo");
            labpcname.Text = resxSet.GetString("pcn");
            labserial.Text = resxSet.GetString("serial");
            labmfm.Text = resxSet.GetString("mfm");
            labbiosver.Text = resxSet.GetString("biosversion");
            labos.Text = resxSet.GetString("os");
            labuser.Text = resxSet.GetString("domuser");

            string comp = returncmd("hostname", "");
            string mf = getline(returncmd("wmic", "computersystem get manufacturer"), 2);
            string md = getline(returncmd("wmic", "computersystem get model"), 2);
            string oss = getline(returncmd("wmic", "os get caption"), 2);
            string bios = getline(returncmd("wmic", "bios get smbiosbiosversion"), 2);
            string sn = getline(returncmd("wmic", "bios get serialnumber"), 2);

            string mfms = mf + md;

            pcname.Text = comp;
            mfm.Text = mfms;
            serial.Text = sn;
            biosver.Text = bios;
            os.Text = oss;
            user.Text = us;

            string os10 = "10";

            Boolean resultos = oss.Contains(os10);
            if(resultos == true)
            {
                osimg.Image = Unowhy_Tools.Properties.Resources.win10;
            }
            else
            {
                osimg.Image = Unowhy_Tools.Properties.Resources.win11;
            }

            if (sn.Contains("IFP0"))
            {
                string mdate = mfms + "(<2020)";
                mfm.Text = mdate;
            }

            if (sn.Contains("IFP1"))
            {
                string mdate = mfms + "(2021)";
                mfm.Text = mdate;
            }

            if (sn.Contains("IFP2"))
            {
                string mdate = mfms + "(2022)";
                mfm.Text = mdate;
            }

            if (sn.Contains("STY1001"))
            {
                string mdate = mfms + "(2021)";
                mfm.Text = mdate;
            }
        }

        private void PCInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
