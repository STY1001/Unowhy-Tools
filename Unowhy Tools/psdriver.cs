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
using System.ServiceProcess;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Unowhy_Tools
{
    public partial class psdriver : Form
    {
        public string resxFile = "null";

        public void WaitScreen()
        {
            Application.Run(new wait());
        }

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

        //Wait fonc

        private static void delay(int Time_delay)
        {
            int i = 0;
            System.Timers.Timer _delayTimer = new System.Timers.Timer();
            _delayTimer.Interval = Time_delay;
            _delayTimer.AutoReset = false;
            _delayTimer.Elapsed += (s, args) => i = 1;
            _delayTimer.Start();
            while (i == 0) { };
        }

        public psdriver()
        {
            //Check the current saved language

            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            string enresx = @".\en.resx";
            string frresx = @".\fr.resx";
            //Chose the ResX file
            if (utls == "EN") resxFile = enresx;    //English   
            else resxFile = frresx;               //French

            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            oeb.Text = resxSet.GetString("browse");
            ieb.Text = resxSet.GetString("browse");
            bb.Text = resxSet.GetString("backup.short");
            rb.Text = resxSet.GetString("restore.short");
            this.Text = resxSet.GetString("psdrv.title");
            labb.Text = resxSet.GetString("backup");
            labr.Text = resxSet.GetString("restore");
        }

        private void oeb_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    opb.Text = fbd.SelectedPath + "\\UT-PS-Drv_" + DateTime.Now.ToString("HHmm-ddMMyy");
                }
            }
        }

        private void bb_Click(object sender, EventArgs e)
        {
            if (opb.Text == "")
            {

            }
            else
            {
                Directory.CreateDirectory(opb.Text);
                string arg = "Export-WindowsDriver -Online -Destination " + "\"" + opb.Text + "\"";

                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
                Thread t = new Thread(new ThreadStart(WaitScreen));               
                t.Start();
                Process p = new Process();
                p.StartInfo.FileName = "powershell.exe";
                p.StartInfo.Arguments = arg;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();
                t.Abort();
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
        }

        private void ieb_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ipb.Text = fbd.SelectedPath + "\\*.inf";
                }
            }
        }

        private void rb_Click(object sender, EventArgs e)
        {
            if (ipb.Text == "")
            {

            }
            else
            {
                string arg = "pnputil /add-driver \"" + ipb.Text + "\" /subdirs /install";

                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
                Thread t = new Thread(new ThreadStart(WaitScreen));
                t.Start();
                Process p = new Process();
                p.StartInfo.FileName = "pnputil";
                p.StartInfo.Arguments = arg;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                p.Start();
                p.WaitForExit();
                t.Abort();
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                var r = new reboot();
                r.ShowDialog();
            }
        }
    }
}
