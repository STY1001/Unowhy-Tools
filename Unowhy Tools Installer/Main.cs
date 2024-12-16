using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.IO.Compression;
using Unowhy_Tools;
using Microsoft.Win32;

namespace Unowhy_Tools_Installer
{
    public partial class Main : Form
    {
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int state, int value);

        private bool silent;

        public Main(string args)
        {
            InitializeComponent();
            TerminateProcesses();
            InitializeUI();

            if (args.Contains("-s"))
            {
                silent = true;
                InstallSilent();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            var attributes = new[] { 19, 20, 35, 38 };
            foreach (var attr in attributes)
            {
                DwmSetWindowAttribute(Handle, attr, new[] { 1 }, 4);
            }
        }

        private void TerminateProcesses()
        {
            string[] processes = { "Unowhy Tools.exe", "Unowhy Tools Updater.exe", "uninstall.exe" };
            foreach (var process in processes)
            {
                RunMinimized("taskkill", $"/f /im \"{process}\"");
            }
            RunMinimized("net", "stop UTS");
        }

        private void InitializeUI()
        {
            status.Text = "Ready!";
            ok_btn.Visible = ok_img.Visible = false;
            KeyPreview = true;
        }

        public static async Task RunMinimized(string file, string args)
        {
            await Task.Run(() =>
            {
                using (var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = file,
                        Arguments = args,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true
                    }
                })
                {
                    process.Start();
                    process.WaitForExit();
                }
            });
        }

        public static void ExtractResource(string namespaceName, string outputPath, string internalPath, string resourceName)
        {
            var assembly = Assembly.GetCallingAssembly();
            var resourcePath = $"{namespaceName}.{(string.IsNullOrEmpty(internalPath) ? "" : internalPath + ".")}{resourceName}";

            using (var resourceStream = assembly.GetManifestResourceStream(resourcePath))
            using (var fileStream = new FileStream(outputPath, FileMode.OpenOrCreate))
                resourceStream?.CopyTo(fileStream);
        }

        public async Task InstallCheckAsync()
        {
            var drive = new DriveInfo("C");
            if (drive.AvailableFreeSpace < 100_000_000)
            {
                ShowInfoDialog("100 MB free space required");
                return;
            }

            install_img.Visible = install_btn.Visible = false;
            desktop_img.Visible = desktop_check.Visible = false;

            await PrepareInstallationAsync();
        }

        private async Task PrepareInstallationAsync()
        {
            UpdateStatus("Preparing...");
            await CreateOrCleanDirectoryAsync("C:\\Program Files (x86)\\Unowhy Tools");
            await CreateOrCleanDirectoryAsync("C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools Service");
            await CreateOrCleanDirectoryAsync("C:\\Program Files (x86)\\Unowhy Tools\\insttemp");

            UpdateProgress(10);
            await ExtractFilesAsync();
        }

        private async Task ExtractFilesAsync()
        {
            UpdateStatus("Extracting...");
            await Task.Delay(1000);

            ExtractResource("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\service.zip", "Files", "service.zip");
            ExtractResource("Unowhy_Tools_Installer", "C:\\Program Files (x86)\\Unowhy Tools\\install.zip", "Files", "install.zip");

            UpdateProgress(30);
            await InstallFilesAsync();
        }

        private async Task InstallFilesAsync()
        {
            UpdateStatus("Installing...");
            ZipFile.ExtractToDirectory("C:\\Program Files (x86)\\Unowhy Tools\\install.zip", "C:\\Program Files (x86)\\Unowhy Tools");
            ZipFile.ExtractToDirectory("C:\\Program Files (x86)\\Unowhy Tools\\service.zip", "C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools Service");

            RegistryKey wdkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows Defender\\Exclusions\\Paths");
            if (wdkey != null)
            {
                await RunMinimized("powershell", "Add-MpPreference -ExclusionPath 'C:\\Program Files (x86)\\Unowhy Tools'");
                wdkey.Close();
            }
            
            await RegisterApplicationAsync();

            UpdateProgress(70);
            await FinalizeInstallationAsync();
        }

        private async Task RegisterApplicationAsync()
        {
            if(Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools") == null)
            {
                Registry.LocalMachine.CreateSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools");
            }
            RegistryKey utkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools", true);

            var registryEntries = new[]
            {
                ("InstallLocation", "C:\\Program Files (x86)\\Unowhy Tools\\"),
                ("DisplayName", "Unowhy Tools"),
                ("DisplayIcon", "\"C:\\Program Files (x86)\\Unowhy Tools\\Unowhy Tools.exe\""),
                ("UninstallString", "\"C:\\Program Files (x86)\\Unowhy Tools\\uninstall.exe\""),
                ("DisplayVersion", "Release"),
                ("Publisher", "STY Inc. (STY1001)"),
                ("URLInfoAbout", "https://github.com/STY1001/Unowhy-Tools/"),
                ("NoModify", "1"),
                ("NoRepair", "1")
            };

            foreach (var (key, value) in registryEntries)
            {
                utkey.SetValue(key, value, RegistryValueKind.String);
            }
        }

        private async Task FinalizeInstallationAsync()
        {
            UpdateStatus("Finalizing...");

            ExtractResource("Unowhy_Tools_Installer", "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Unowhy Tools.lnk", "Files", "Unowhy Tools.lnk");

            if (desktop_check.Checked)
            {
                ExtractResource("Unowhy_Tools_Installer", "C:\\Users\\Public\\Desktop\\Unowhy Tools.lnk", "Files", "Unowhy Tools.lnk");
            }

            await CleanUpAsync();
        }

        private async Task CleanUpAsync()
        {
            UpdateStatus("Cleaning...");
            await DeleteIfExistsAsync("C:\\Program Files (x86)\\Unowhy Tools\\insttemp");
            await DeleteIfExistsAsync("C:\\Program Files (x86)\\Unowhy Tools\\service.zip");
            await DeleteIfExistsAsync("C:\\Program Files (x86)\\Unowhy Tools\\install.zip");

            UpdateProgress(100);
            UpdateStatus("Finished!");

            ok_btn.Visible = ok_img.Visible = true;

            if (silent)
            {
                Close();
            }
        }

        private void UpdateStatus(string message) => status.Text = message;

        private void UpdateProgress(int value)
        {
            statusbar.Value = value;
            TaskbarManager.Instance.SetProgressValue(value, 100);
        }

        private async Task CreateOrCleanDirectoryAsync(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
        }

        private async Task DeleteIfExistsAsync(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public void ShowInfoDialog(string message) => new info(message).ShowDialog();

        private void ShowErrorDialog(string message)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
            new noco().ShowDialog();
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
        }

        public async void InstallSilent()
        {
            await Task.Delay(1000);
            await InstallCheckAsync();
        }

        bool ipressed = false;
        private async void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.I && !ipressed)
            {
                ipressed = true;
                await InstallCheckAsync();
            }
        }

        public void Cancel_Click(object sender, EventArgs e) => Close();

        public async void Install_Click(object sender, EventArgs e) => await InstallCheckAsync();
    }
}
