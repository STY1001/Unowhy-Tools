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
using System.Threading;
using System.IO.Compression;

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

        public Main(string args)
        {
            InitializeComponent();

            runmin("taskkill", "/f /im \"Unowhy Tools.exe\"");
            runmin("taskkill", "/f /im \"Unowhy Tools Updater.exe\"");
            runmin("taskkill", "/f /im \"uninstall.exe\"");
            runmin("net", "stop UTS");

            status.Text = "Ready !";
            pictureBox3.Visible = false;
            run.Visible = false;
            ok.Visible = false;
            pictureBox6.Visible = false;

            if (args.Contains("-s"))
            {
                install_silent();
            }
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

        public static void delay(int Time_delay)
        {
            int i = 0;
            System.Timers.Timer _delayTimer = new System.Timers.Timer();
            _delayTimer.Interval = Time_delay;
            _delayTimer.AutoReset = false; 
            _delayTimer.Elapsed += (s, args) => i = 1;
            _delayTimer.Start();
            while (i == 0) { };
        }

        public static void runmin(string file, string args)
        {
            Process p = new Process();
            p.StartInfo.FileName = file;
            p.StartInfo.Arguments = args;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
        }

        public async Task install_check()
        {
            DriveInfo c = new DriveInfo("C");
            if (c.AvailableFreeSpace < 100000000)
            {
                var i = new info("100 MB free space required");
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
                    await install_pre();
                }
            }
        }

        public async Task install_pre()
        {
            statusbar.Value = 0;
            TaskbarManager.Instance.SetProgressValue(0, 100);

            status.Text = "Preparing...";

            if (Directory.Exists("C:\\Program Files (x86)\\Unowhy Tools"))
            {
                Directory.Delete("C:\\Program Files (x86)\\Unowhy Tools", true);
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools");
            }
            else
            {
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools");
            }

            if (Directory.Exists("C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools Service"))
            {
                Directory.Delete("C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools Service", true);
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools Service");
            }
            else
            {
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools Service");
            }

            statusbar.Value = 5;
            TaskbarManager.Instance.SetProgressValue(5, 100);

            if (Directory.Exists("C:\\Program Files (x86)\\Unowhy Tools\\insttemp") == true)
            {
                Directory.Delete("C:\\Program Files (x86)\\Unowhy Tools\\insttemp", true);
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools\\insttemp");
            }
            else
            {
                Directory.CreateDirectory("C:\\Program Files (x86)\\Unowhy Tools\\insttemp");
            }

            statusbar.Value = 10;
            TaskbarManager.Instance.SetProgressValue(10, 100);

            await install_dl();
        }

        public async Task install_dl()
        {
            status.Text = "Extracting...";
            delay(1000);
            statusbar.Value = 15;
            TaskbarManager.Instance.SetProgressValue(15, 100);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\service.zip", "Files", "service.zip");
            statusbar.Value = 20;
            TaskbarManager.Instance.SetProgressValue(20, 100);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\install.zip", "Files", "install.zip");
            statusbar.Value = 25;
            TaskbarManager.Instance.SetProgressValue(25, 100);
            Extract("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\insttemp\\dotnetwdrt6.0.16.exe", "Files", "dotnetwdrt6.0.16.exe");
            statusbar.Value = 30;
            TaskbarManager.Instance.SetProgressValue(30, 100);
            statusbar.Value = 35;
            TaskbarManager.Instance.SetProgressValue(35, 100);
            statusbar.Value = 40;
            TaskbarManager.Instance.SetProgressValue(40, 100);
            statusbar.Value = 45;
            TaskbarManager.Instance.SetProgressValue(45, 100);
            statusbar.Value = 50;
            TaskbarManager.Instance.SetProgressValue(50, 100);

            status.Text = "Installing...";
            
            await install_inst();
        }

        public async Task install_inst()
        {
            Process p12 = new Process();
            p12.StartInfo.FileName = "C:\\Program Files (x86)\\Unowhy Tools\\insttemp\\dotnetwdrt6.0.16.exe";
            p12.StartInfo.Arguments = "/install /passive";
            p12.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\Unowhy Tools\\insttemp";
            p12.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p12.StartInfo.CreateNoWindow = true;
            p12.Start();
            p12.WaitForExit();
            
            statusbar.Value = 55;
            TaskbarManager.Instance.SetProgressValue(55, 100);

            ZipFile.ExtractToDirectory("C:\\Program Files (x86)\\Unowhy Tools\\install.zip", "C:\\Program Files (x86)\\Unowhy Tools");


            statusbar.Value = 60;
            TaskbarManager.Instance.SetProgressValue(60, 100);

            ZipFile.ExtractToDirectory("C:\\Program Files (x86)\\Unowhy Tools\\service.zip", "C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools Service");


            statusbar.Value = 65;
            TaskbarManager.Instance.SetProgressValue(65, 100);

            runmin("reg", "add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"InstallLocation\" /t REG_SZ /d \"C:\\Program Files (x86)\\Unowhy Tools\\\\\" /f");
            runmin("reg", "add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"DisplayName\" /t REG_SZ /d \"Unowhy Tools\" /f");
            runmin("reg", "add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"DisplayIcon\" /t REG_SZ /d \"\\\"C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools.exe\\\"\" /f");
            runmin("reg", "add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"UninstallString\" /t REG_SZ /d \"\\\"C:\\Program Files (x86)\\Unowhy Tools\\uninstall.exe\\\"\" /f");
            runmin("reg", "add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"DisplayVersion\" /t REG_SZ /d \"Release\" /f");
            runmin("reg", "add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"Publisher\" /t REG_SZ /d \"STY1001\" /f");
            runmin("reg", "add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"URLInfoAbout\" /t REG_SZ /d \"https://github.com/STY1001/Unowhy-Tools/\" /f");
            runmin("reg", "add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"NoModify\" /t REG_DWORD /d \"1\" /f");
            runmin("reg", "add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"NoRepair\" /t REG_DWORD /d \"1\" /f");

            statusbar.Value = 70;
            TaskbarManager.Instance.SetProgressValue(70, 100);

            await install_post();
        }

        public async Task install_post()
        {
            status.Text = "Finalizing...";

            Extract("Unowhy_Tools_Installer", "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Unowhy Tools.lnk", "Files", "Unowhy Tools.lnk");

            statusbar.Value = 75;
            TaskbarManager.Instance.SetProgressValue(75, 100);

            if (desktop.Checked == true)
            {
                Extract("Unowhy_Tools_Installer", "C:\\Users\\Public\\Desktop\\Unowhy Tools.lnk", "Files", "Unowhy Tools.lnk");
            }

            statusbar.Value = 80;
            TaskbarManager.Instance.SetProgressValue(80, 100);

            await install_clean();
        }

        public async Task install_clean()
        {
            status.Text = "Cleaning...";

            if (Directory.Exists("C:\\Program Files (x86)\\Unowhy Tools\\insttemp"))
            {
                Directory.Delete("C:\\Program Files (x86)\\Unowhy Tools\\insttemp", true);
            }

            statusbar.Value = 85;
            TaskbarManager.Instance.SetProgressValue(85, 100);

            if (File.Exists("C:\\Program Files (x86)\\Unowhy Tools\\service.zip"))
            {
                File.Delete("C:\\Program Files (x86)\\Unowhy Tools\\service.zip");
            }

            statusbar.Value = 90;
            TaskbarManager.Instance.SetProgressValue(90, 100);

            if (File.Exists("C:\\Program Files (x86)\\Unowhy Tools\\install.zip"))
            {
                File.Delete("C:\\Program Files (x86)\\Unowhy Tools\\install.zip");
            }

            statusbar.Value = 95;
            TaskbarManager.Instance.SetProgressValue(95, 100);

            if (Directory.Exists(".\\temp"))
            {
                Directory.Delete(".\\temp", true);
            }

            await install_finish();
        }

        public async Task install_finish()
        {
            statusbar.Value = 100;
            TaskbarManager.Instance.SetProgressValue(100, 100);
            status.Text = "Finished !";
            //run.Visible = true;
            //pictureBox3.Visible = true;
            ok.Visible = true;
            pictureBox6.Visible = true;
            
        }

        public void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public async void install_Click(object sender, EventArgs e)
        {
            await install_check();
        }

        public async void install_silent()
        {
            await install_check();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            /*if (run.Checked == true)
            {
                Process p = new Process();
                p.StartInfo.FileName = "C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools.exe";
                p.StartInfo.WorkingDirectory = "C:\\Program Files (x86)\\Unowhy Tools";
                p.Start();
            }*/

            delay(100);

            this.Close();
        }

        public void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
