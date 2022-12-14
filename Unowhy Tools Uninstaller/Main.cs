using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Unowhy_Tools_Uninstaller
{
    public partial class Main : Form
    {
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
        }

        public Main()
        {
            InitializeComponent();

            lab.Text = "Uninstall Unowhy Tools ? / Désinstaller Unowhy Tools ?";

            
        }

        private void no_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yes_Click(object sender, EventArgs e)
        {
            lab.Text = "Uninstalling Unowhy Tools. Please wait ...";
            pyes.Visible = false;
            pno.Visible = false;
            yes.Visible = false;
            no.Visible = false;
            progbar.Visible = true;
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);

            if (File.Exists("C:\\Users\\Public\\Desktop\\Unowhy Tools.lnk"))
            {
                File.Delete("C:\\Users\\Public\\Desktop\\Unowhy Tools.lnk");
            }

            if (File.Exists("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Unowhy Tools.lnk"))
            {
                File.Delete("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Unowhy Tools.lnk");
            }

            Process p = new Process();
            p.StartInfo.FileName = "taskkill";
            p.StartInfo.Arguments = "/f /im \"Unowhy Tools.exe\"";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            Process p1 = new Process();
            p1.StartInfo.FileName = "reg";
            p1.StartInfo.Arguments = "delete \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /f";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p1.StartInfo.CreateNoWindow = true;
            p1.Start();
            p1.WaitForExit();

            Process p3 = new Process();
            p3.StartInfo.FileName = "cmd.exe";
            p3.StartInfo.Arguments = "/c echo \"Wait...\" & timeout /t 1 & taskkill /f /im uninstall.exe & timeout /t 1 & rmdir /q /s \"C:\\Program Files (x86)\\Unowhy Tools\" & echo \"Done\" & timeout /t 1 & exit";
            p3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p3.StartInfo.CreateNoWindow = true;
            p3.Start();
        }
    }
}
