﻿using System;
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
            
            Process p2 = new Process();
            p2.StartInfo.FileName = "net";
            p2.StartInfo.Arguments = "stop UTS";
            p2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p2.StartInfo.CreateNoWindow = true;
            p2.Start();
            p2.WaitForExit();
            
            Process p4 = new Process();
            p4.StartInfo.FileName = "taskkill";
            p4.StartInfo.Arguments = "/f /im \"Unowhy Tools Service.exe\"";
            p4.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p4.StartInfo.CreateNoWindow = true;
            p4.Start();
            p4.WaitForExit();
            
            Process p5 = new Process();
            p5.StartInfo.FileName = "sc";
            p5.StartInfo.Arguments = "delete UTS";
            p5.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p5.StartInfo.CreateNoWindow = true;
            p5.Start();
            p5.WaitForExit();
            
            Process p6 = new Process();
            p6.StartInfo.FileName = "schtasks";
            p6.StartInfo.Arguments = "/Delete /TN \"Unowhy Tools Tray Launch\" /F";
            p6.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p6.StartInfo.CreateNoWindow = true;
            p6.Start();
            p6.WaitForExit();

            Process p1 = new Process();
            p1.StartInfo.FileName = "reg";
            p1.StartInfo.Arguments = "delete \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /f";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p1.StartInfo.CreateNoWindow = true;
            p1.Start();
            p1.WaitForExit();
            
            Process p7 = new Process();
            p7.StartInfo.FileName = "powershell";
            p7.StartInfo.Arguments = "Remove-MpPreference -ExclusionPath 'C:\\Program Files (x86)\\Unowhy Tools'";
            p7.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p7.StartInfo.CreateNoWindow = true;
            p7.Start();
            p7.WaitForExit();

            Process p3 = new Process();
            p3.StartInfo.FileName = "cmd.exe";
            p3.StartInfo.Arguments = "/c echo \"Wait...\" & timeout /t 1 & taskkill /f /im uninstall.exe & timeout /t 1 & rmdir /q /s \"C:\\Program Files (x86)\\Unowhy Tools\" & rmdir /q /s \"C:\\Unowhy Tools\" & echo \"Done\" & timeout /t 1 & exit";
            p3.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p3.StartInfo.CreateNoWindow = true;
            p3.Start();
        }
    }
}
