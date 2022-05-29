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
using System.Threading.Tasks;
using System.Windows.Input;

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
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        public PCInfo()
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

            string filePath = ".\\fullpcinfo.txt";
            StreamReader inputFile = new StreamReader(filePath);
            int lineNumber = 1;
            for (int i = 1; i < lineNumber; i++)
            {
                inputFile.ReadLine();
            }
            string hnpcname = inputFile.ReadLine();

            int lineNumber2 = 1;
            for (int i = 1; i < lineNumber2; i++)
            {
                inputFile.ReadLine();
            }
            string mfs = inputFile.ReadLine();

            int lineNumber3 = 1;
            for (int i = 1; i < lineNumber3; i++)
            {
                inputFile.ReadLine();
            }
            string models = inputFile.ReadLine();

            int lineNumber4 = 1;
            for (int i = 1; i < lineNumber4; i++)
            {
                inputFile.ReadLine();
            }
            string enes = inputFile.ReadLine();

            int lineNumber5 = 1;
            for (int i = 1; i < lineNumber5; i++)
            {
                inputFile.ReadLine();
            }
            string ifps = inputFile.ReadLine();
            
            int lineNumber6 = 1;
            for (int i = 1; i < lineNumber6; i++)
            {
                inputFile.ReadLine();
            }
            string oss = inputFile.ReadLine();

            string mfms = mfs + models;

            pcname.Text = hnpcname;
            mfm.Text = mfms;
            serial.Text = ifps;
            biosver.Text = enes;
            os.Text = oss;

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

            string ene21 = "_ENE_";

            Boolean result21 = enes.Contains(ene21);
            if (result21 == true)
            {
                string mdate = mfms + "(2021)";
                mfm.Text = mdate;
            }
            else
            {
                string mdate = mfms + "(2020)";
                mfm.Text = mdate;
            }

        }

        private void PCInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
