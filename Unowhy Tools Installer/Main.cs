using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Net;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.Reflection;
using Unowhy_Tools;

namespace Unowhy_Tools_Installer
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

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int state, int value);

        public Main()
        {
            InitializeComponent();

            System.Diagnostics.Process.Start("taskkill", "/f /im \"Unowhy Tools.exe\"");
            System.Diagnostics.Process.Start("taskkill", "/f /im \"Unowhy Tools Updater.exe\"");
            System.Diagnostics.Process.Start("taskkill", "/f /im \"uninstall.exe\"");

            status.Text = "Ready !";
            pictureBox3.Visible = false;
            run.Visible = false;
            ok.Visible = false;
            pictureBox6.Visible = false;
        }

        public static void Extract(string nameSpace, string outFile, string internalFilePath, string resourceName)
        {
            //nameSpace = Project
            //outDirectory = Out File
            //internalFilePath = Res Folder
            //resourceName = Res Name
            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outFile, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
            {
                w.Write(r.ReadBytes((int)s.Length));
            }
        }

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

        private void install_pre()
        {
            statusbar.Value = 0;
            TaskbarManager.Instance.SetProgressValue(0, 100);

            status.Text = "Preparing...";
            delay(1000);

            if (Directory.Exists("C:\\Program Files (x86)\\Unowhy Tools"))
            {
                Directory.Delete("C:\\Program Files (x86)\\Unowhy Tools", true);
                delay(300);
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools");
            }
            else
            {
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools");
            }

            delay(300);
            statusbar.Value = 5;
            TaskbarManager.Instance.SetProgressValue(5, 100);
            delay(600);

            if (Directory.Exists("C:\\Program Files (x86)\\Unowhy Tools\\insttemp") == true)
            {
                Directory.Delete("C:\\Program Files (x86)\\Unowhy Tools\\insttemp", true);
                delay(300);
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools\\insttemp");
            }
            else
            {
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools\\insttemp");
            }

            delay(300);
            statusbar.Value = 10;
            TaskbarManager.Instance.SetProgressValue(10, 100);

            install_dl();
        }

        private void install_dl()
        {
            status.Text = "Extracting...";
            delay(1000);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\7zip.exe", "Files", "7zip.exe");
            statusbar.Value = 15;
            TaskbarManager.Instance.SetProgressValue(15, 100);
            delay(1000);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\7z.dll", "Files", "7z.dll");
            statusbar.Value = 20;
            TaskbarManager.Instance.SetProgressValue(20, 100);
            delay(1000);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\insttemp\\utkeyinst.reg", "Files", "utkeyinst.reg");
            statusbar.Value = 25;
            TaskbarManager.Instance.SetProgressValue(25, 100);
            delay(1000);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\insttemp\\utkeyinst.exe", "Files", "utkeyinst.exe");
            statusbar.Value = 30;
            TaskbarManager.Instance.SetProgressValue(30, 100);
            delay(1000);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\uninstall.exe", "Files", "uninstall.exe");
            statusbar.Value = 35;
            TaskbarManager.Instance.SetProgressValue(35, 100);
            delay(1000);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\utconfdel.exe", "Files", "utconfdel.exe");
            statusbar.Value = 40;
            TaskbarManager.Instance.SetProgressValue(40, 100);
            delay(1000);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\utkeydel.exe", "Files", "utkeydel.exe");
            statusbar.Value = 45;
            TaskbarManager.Instance.SetProgressValue(45, 100);
            delay(1000);
            //Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\fr.resx", "Files", "fr.resx");
            statusbar.Value = 50;
            TaskbarManager.Instance.SetProgressValue(50, 100);
            delay(1000);
            //Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\en.resx", "Files", "en.resx");
            statusbar.Value = 55;
            TaskbarManager.Instance.SetProgressValue(55, 100);
            delay(1000);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\update.zip", "Files", "install.zip");
            statusbar.Value = 60;
            TaskbarManager.Instance.SetProgressValue(60, 100);
            delay(1000);

            #region Old
            /*using (var client = new WebClient())
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Extras/7zip.exe", "C:\\Program Files (x86)\\Unowhy Tools\\7zip.exe");
            }
            statusbar.Value = 15;
            TaskbarManager.Instance.SetProgressValue(15, 100);
            delay(1000);
            using (var client = new WebClient()) 
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Extras/7z.dll", "C:\\Program Files (x86)\\Unowhy Tools\\7z.dll"); 
            }
            statusbar.Value = 20;
            TaskbarManager.Instance.SetProgressValue(20, 100);
            delay(600);
            using (var client = new WebClient()) 
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Install/utkeyinst.reg", "C:\\Program Files (x86)\\Unowhy Tools\\insttemp\\utkeyinst.reg"); 
            }
            statusbar.Value = 25;
            TaskbarManager.Instance.SetProgressValue(25, 100);
            delay(600);
            using (var client = new WebClient()) 
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Install/utkeyinst.exe", "C:\\Program Files (x86)\\Unowhy Tools\\insttemp\\utkeyinst.exe"); 
            }
            statusbar.Value = 30;
            TaskbarManager.Instance.SetProgressValue(30, 100);
            delay(600);
            using (var client = new WebClient()) 
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Uninstall/uninstall.exe", "C:\\Program Files (x86)\\Unowhy Tools\\uninstall.exe"); 
            }
            statusbar.Value = 35;
            TaskbarManager.Instance.SetProgressValue(35, 100);
            delay(300);
            using (var client = new WebClient()) 
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Uninstall/utconfdel.exe", "C:\\Program Files (x86)\\Unowhy Tools\\utconfdel.exe"); 
            }
            statusbar.Value = 40;
            TaskbarManager.Instance.SetProgressValue(40, 100);
            delay(600);
            using (var client = new WebClient()) 
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Uninstall/utkeydel.exe", "C:\\Program Files (x86)\\Unowhy Tools\\utkeydel.exe"); 
            }
            statusbar.Value = 45;
            TaskbarManager.Instance.SetProgressValue(45, 100);
            delay(600);
            using (var client = new WebClient()) 
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Unowhy%20Tools/Lang/fr.resx", "C:\\Program Files (x86)\\Unowhy Tools\\fr.resx"); 
            }
            statusbar.Value = 50;
            TaskbarManager.Instance.SetProgressValue(50, 100);
            delay(600);
            using (var client = new WebClient())
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Unowhy%20Tools/Lang/en.resx", "C:\\Program Files (x86)\\Unowhy Tools\\en.resx");
            }
            statusbar.Value = 55;
            TaskbarManager.Instance.SetProgressValue(55, 100);
            delay(1000);
            using (var client = new WebClient())
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Install/install.zip", "C:\\Program Files (x86)\\Unowhy Tools\\update.zip");
            }
            statusbar.Value = 60;
            TaskbarManager.Instance.SetProgressValue(60, 100);*/
            #endregion

            status.Text = "Installing...";
            delay(600);
            
            install_inst();
        }

        private void install_inst()
        {
            
            delay(1000);

            Process p1 = new Process();
            p1.StartInfo.FileName = "C:\\Program Files (x86)\\Unowhy Tools\\7zip.exe";
            p1.StartInfo.Arguments = "e \"C:\\Program Files (x86)\\Unowhy Tools\\update.zip\" -aoa";
            p1.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\Unowhy Tools";
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p1.Start();
            p1.WaitForExit();

            statusbar.Value = 65;
            TaskbarManager.Instance.SetProgressValue(65, 100);
            delay(1000);

            Process p2 = new Process();
            p2.StartInfo.FileName = "C:\\Program Files (x86)\\Unowhy Tools\\insttemp\\utkeyinst.exe";
            p2.StartInfo.Arguments = "";
            p2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p2.Start();
            p2.WaitForExit();

            statusbar.Value = 70;
            TaskbarManager.Instance.SetProgressValue(70, 100);
            install_post();
        }

        private void install_post()
        {
            delay(1000);
            status.Text = "Finalizing...";
            delay(1000);

            Extract("Unowhy_Tools_Installer", "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Unowhy Tools.lnk", "Files", "Unowhy Tools.lnk");

            /*
            using (var client = new WebClient())
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Install/Unowhy%20Tools.lnk", "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Unowhy Tools.lnk");
            }
            */

            statusbar.Value = 75;
            TaskbarManager.Instance.SetProgressValue(75, 100);
            delay(1000);

            if (desktop.Checked == true)
            {
                Extract("Unowhy_Tools_Installer", "C:\\Users\\Public\\Desktop\\Unowhy Tools.lnk", "Files", "Unowhy Tools.lnk");

                /*
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Install/Unowhy%20Tools.lnk", "C:\\Users\\Public\\Desktop\\Unowhy Tools.lnk");
                }
                */
            }

            statusbar.Value = 80;
            TaskbarManager.Instance.SetProgressValue(80, 100);
            delay(1000);
            install_clean();
        }
        
        private void install_clean()
        {
            status.Text = "Cleaning...";
            delay(1000);

            if (Directory.Exists("C:\\Program Files (x86)\\Unowhy Tools\\insttemp"))
            {
                Directory.Delete("C:\\Program Files (x86)\\Unowhy Tools\\insttemp", true);
            }

            statusbar.Value = 85;
            TaskbarManager.Instance.SetProgressValue(85, 100);
            delay(1000);

            if (Directory.Exists("C:\\Program Files (x86)\\Unowhy Tools\\update"))
            {
                Directory.Delete("C:\\Program Files (x86)\\Unowhy Tools\\update", true);
            }

            statusbar.Value = 90;
            TaskbarManager.Instance.SetProgressValue(90, 100);
            delay(1000);

            if (File.Exists("C:\\Program Files (x86)\\Unowhy Tools\\update.zip"))
            {
                File.Delete("C:\\Program Files (x86)\\Unowhy Tools\\update.zip");
            }

            statusbar.Value = 95;
            TaskbarManager.Instance.SetProgressValue(95, 100);
            delay(1000);

            if (Directory.Exists(".\\temp"))
            {
                Directory.Delete(".\\temp", true);
            }

            delay(1000);
            install_finish();
        }

        private void install_finish()
        {
            statusbar.Value = 100;
            TaskbarManager.Instance.SetProgressValue(100, 100);
            status.Text = "Finished !";
            run.Visible = true;
            pictureBox3.Visible = true;
            ok.Visible = true;
            pictureBox6.Visible = true;
            
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void install_Click(object sender, EventArgs e)
        {
            DriveInfo c = new DriveInfo("C");
            if (c.AvailableFreeSpace < 30000000)
            {
                var i = new info("30 MB free space required");
                i.ShowDialog();
            }
            else
            {
                string noco = "0";

                if (noco == "1")
                {
                    var co = new noco();
                    TaskbarManager.Instance.SetProgressValue(100, 100);
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
                    co.ShowDialog();
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                }
                else
                {
                    install.Visible = false;
                    desktop.Visible = false;
                    pictureBox2.Visible = false;
                    cancel.Visible = false;
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;

                    status.Text = "";
                    install_pre();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (run.Checked == true)
            {
                Process p = new Process();
                p.StartInfo.FileName = "C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools.exe";
                p.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\Unowhy Tools";
                p.Start();
            }

            delay(100);

            this.Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
