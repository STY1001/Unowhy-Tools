using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using Unowhy_Tools_WPF.ViewModels;
using System.ComponentModel;
using Unowhy_Tools_WPF.Views;
using System.Windows.Forms;
using Unowhy_Tools_WPF.Views.Pages;
using System.Security.Principal;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Runtime.CompilerServices;
using Unowhy_Tools_WPF.Views.Windows;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Net;
using System.Net.Http;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Interop;
using System.Threading.Tasks;
using System.Reflection;
using System.IO.Packaging;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Navigation;
using Wpf.Ui.Controls;
using System.IO.Pipes;
using System.IO.Compression;
using System.Threading;
using System.Drawing;
using TaskScheduler = Microsoft.Win32.TaskScheduler;

namespace Unowhy_Tools
{
    public partial class UT
    {
        #region DLL
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int state, int value);
        #endregion

        public static int verfull = 2404;
        public static string verbuild = "1500230623";
        public static bool verisdeb = true;

        public class version
        {

            public static int getverfull()
            {
                return verfull;
            }

            public static bool isdeb()
            {
                return verisdeb;
            }

            public static string getverbuild()
            {
                return verbuild;
            }

            public static async Task<bool> newver()
            {
                var web = new HttpClient();
                string newver = await web.GetStringAsync("https://bit.ly/UTnvTXT");
                int newverint = Convert.ToInt32(newver);
                if (verfull < newverint)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public class anim
        {
            public static Grid _grid;
            public static Wpf.Ui.Controls.Button _button;

            public static void TransitionForw(Grid grid)
            {
                var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
                mainWindow.NavAnimRight();
                _grid = grid;
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 0;
                anim.To = -100;
                anim.Duration = TimeSpan.FromMilliseconds(300);
                anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

                TranslateTransform trans = new TranslateTransform();
                _grid.RenderTransform = trans;
                anim.Completed += setnormalForw;
                trans.BeginAnimation(TranslateTransform.XProperty, anim);
            }

            public static void TransitionBack(Grid grid)
            {
                var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
                mainWindow.NavAnimLeft();
                _grid = grid;
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 0;
                anim.To = 100;
                anim.Duration = TimeSpan.FromMilliseconds(300);
                anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

                TranslateTransform trans = new TranslateTransform();
                _grid.RenderTransform = trans;
                anim.Completed += setnormalBack;
                trans.BeginAnimation(TranslateTransform.XProperty, anim);
            }

            public static void BackBtnAnim(Wpf.Ui.Controls.Button btn)
            {
                _button = btn;
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 0;
                anim.To = -150;
                anim.Duration = TimeSpan.FromMilliseconds(500);
                anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

                TranslateTransform trans = new TranslateTransform();
                _button.RenderTransform = trans;
                anim.Completed += setnormalBackBTN;
                trans.BeginAnimation(TranslateTransform.XProperty, anim);
            }

            public static void BackBtnAnimForw(Wpf.Ui.Controls.Button btn)
            {
                _button = btn;
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = -150;
                anim.To = 0;
                anim.Duration = TimeSpan.FromMilliseconds(500);
                anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

                TranslateTransform trans = new TranslateTransform();
                _button.RenderTransform = trans;
                anim.Completed += setnormalBackBTN;
                trans.BeginAnimation(TranslateTransform.XProperty, anim);
            }

            public static void setnormalBackBTN(object sender, EventArgs e)
            {
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = -150;
                anim.To = 0;
                anim.Duration = TimeSpan.FromMilliseconds(0);
                anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
                TranslateTransform trans = new TranslateTransform();
                _button.RenderTransform = trans;
                trans.BeginAnimation(TranslateTransform.XProperty, anim);
            }

            public static void setnormalBack(object sender, EventArgs e)
            {
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = -100;
                anim.To = 0;
                anim.Duration = TimeSpan.FromMilliseconds(300);
                anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

                TranslateTransform trans = new TranslateTransform();
                _grid.RenderTransform = trans;
                anim.Completed += setnormalAnim;
                trans.BeginAnimation(TranslateTransform.XProperty, anim);
            }

            public static void setnormalForw(object sender, EventArgs e)
            {
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 100;
                anim.To = 0;
                anim.Duration = TimeSpan.FromMilliseconds(300);
                anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

                TranslateTransform trans = new TranslateTransform();
                _grid.RenderTransform = trans;
                anim.Completed += setnormalAnim;
                trans.BeginAnimation(TranslateTransform.XProperty, anim);
            }

            public static void setnormalAnim(object sender, EventArgs e)
            {
                var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
                mainWindow.NavAnimNormal();
            }
        }

        public class serv
        {
            public static async Task<bool> exist(string service)
            {
                Write2Log("Check " + service);
                string s = await RunReturn("sc", "query \"" + service + "\"");
                if (s.Contains("1060"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public static async Task stop(string service)
            {
                Write2Log("Stop " + service);
                await RunMin("net", "stop \"" + service + "\" /y");
            }

            public static async Task start(string service)
            {
                Write2Log("Start " + service);
                await RunMin("net", "start \"" + service + "\"");
            }

            public static async Task auto(string service)
            {
                Write2Log("Enable " + service);
                await RunMin("sc", "config \"" + service + "\" start=auto");
            }

            public static async Task dis(string service)
            {
                Write2Log("Disable " + service);
                await serv.stop(service);
                await RunMin("sc", "config \"" + service + "\" start=disabled");
            }

            public static async Task del(string service)
            {
                Write2Log("Delete " + service);
                await serv.stop(service);
                await RunMin("sc", "delete \"" + service + "\"");
            }
        }

        public class waitstatus
        {
            public async static Task close()
            {
                var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
                await mainWindow.HideWait();
                Write2Log("Close wait");
            }

            public async static Task open()
            {
                var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
                await mainWindow.ShowWait();
                Write2Log("Open wait");
            }
        }

        public class UTS
        {
            public static async Task<string> UTSmsg(string pipe, string msg)
            {
                string ret = null;

                try
                {
                    using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipe, PipeDirection.InOut, PipeOptions.Asynchronous))
                    {
                        await pipeClient.ConnectAsync();
                        if (pipeClient.IsConnected)
                        {
                            using (var writer = new StreamWriter(pipeClient))
                            using (var reader = new StreamReader(pipeClient))
                            {
                                await writer.WriteLineAsync(msg);
                                await writer.FlushAsync();

                                if (pipeClient.IsConnected)
                                {
                                    ret = await reader.ReadLineAsync();
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }

                return ret;
            }

            public static async Task UTScheck()
            {
                var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
                mainWindow.SplashText.Text = "Preparing UTS... (Checking)";
                string instdir = Directory.GetCurrentDirectory() + "\\Unowhy Tools Service";

                if (!Directory.Exists(instdir))
                {
                    Directory.CreateDirectory(instdir);
                }
                mainWindow.SplashBar.Value++;
                mainWindow.SplashBar.Value++;
                if (!File.Exists(instdir + "\\Unowhy Tools Service.exe"))
                {
                    if (CheckInternet())
                    {
                        mainWindow.SplashText.Text = "Preparing UTS... (Downloading)";
                        var web = new HttpClient();
                        var filebyte = await web.GetByteArrayAsync("https://bit.ly/UTSzip");
                        string utemp = Path.GetTempPath() + "Unowhy Tools\\Temps";
                        File.WriteAllBytes(utemp + "\\service.zip", filebyte);
                        mainWindow.SplashText.Text = "Preparing UTS... (Extracting)";
                        await Task.Delay(100);
                        ZipFile.ExtractToDirectory(utemp + "\\service.zip", instdir);
                        await Task.Delay(100);
                    }
                }
                mainWindow.SplashBar.Value++;
                mainWindow.SplashBar.Value++;
                string utspath = instdir + "\\Unowhy Tools Service.exe";

                if (await UT.serv.exist("UTS"))
                {
                    ServiceController sc = new ServiceController();
                    sc.ServiceName = "UTS";

                    if (sc.ServiceType.HasFlag(ServiceType.InteractiveProcess) && sc.ServiceType.HasFlag(ServiceType.Win32OwnProcess))
                    {

                    }
                    else
                    {
                        await UT.RunMin("sc", "config UTS type=own type=interact");
                        mainWindow.SplashText.Text = "Preparing UTS... (Restarting)";
                        await UT.serv.stop("UTS");
                        await UT.serv.start("UTS");
                    }

                    if (sc.StartType == ServiceStartMode.Automatic)
                    {
                        if (sc.Status == ServiceControllerStatus.Running)
                        {
                            mainWindow.SplashText.Text = "Preparing UTS... (Running)";
                        }
                        else
                        {
                            await UT.serv.start("UTS");
                            mainWindow.SplashText.Text = "Preparing UTS... (Starting)";
                        }
                    }
                    else
                    {
                        await UT.serv.auto("UTS");
                        await UT.serv.start("UTS");
                        mainWindow.SplashText.Text = "Preparing UTS... (Starting)";
                    }
                }
                else
                {
                    mainWindow.SplashText.Text = "Preparing UTS... (Installing)";
                    await UT.RunMin("sc", $"create UTS binpath=\"\\\"{utspath}\\\"\" displayname=\"Unowhy Tools Service\" start=auto type=own type=interact");
                    await UT.serv.start("UTS");
                }
                mainWindow.SplashBar.Value++;
                mainWindow.SplashBar.Value++;
                mainWindow.SplashText.Text = "Preparing UTS... (Checking Update)";
                await Task.Delay(100);
                await UT.UTS.UTSupdate();
                mainWindow.SplashBar.Value++;
                mainWindow.SplashBar.Value++;
            }

            public static async Task UTSupdate()
            {
                var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;

                if (CheckInternet())
                {
                    var web = new HttpClient();
                    string newver = await web.GetStringAsync("https://bit.ly/UTStext");
                    newver = newver.Replace("\n", "").Replace("\r", "").Replace(" ", "");

                    if (!verisdeb)
                    {
                        string ver = await UT.UTS.UTSmsg("UTS", "GetVer");

                        if (!(newver == ver))
                        {
                            mainWindow.SplashText.Text = "Preparing UTS... (" + ver + " => " + newver + ")";
                            await Task.Delay(300);
                            mainWindow.SplashText.Text = "Preparing UTS... (Shutting down)";
                            await UT.serv.stop("UTS");
                            mainWindow.SplashText.Text = "Preparing UTS... (Downloading)";
                            string instdir = Directory.GetCurrentDirectory() + "\\Unowhy Tools Service";
                            Directory.Delete(instdir, true);
                            await Task.Delay(100);
                            Directory.CreateDirectory(instdir);
                            web = new HttpClient();
                            var filebyte = await web.GetByteArrayAsync("https://bit.ly/UTSzip");
                            string utemp = Path.GetTempPath() + "Unowhy Tools\\Temps";
                            File.WriteAllBytes(utemp + "\\service.zip", filebyte);
                            mainWindow.SplashText.Text = "Preparing UTS... (Extracting)";
                            await Task.Delay(100);
                            ZipFile.ExtractToDirectory(utemp + "\\service.zip", instdir);
                            await Task.Delay(100);
                            mainWindow.SplashText.Text = "Preparing UTS... (Starting up)";
                            await UT.serv.start("UTS");
                        }
                    }
                }
            }
        }
        public static async Task DeployDABack()
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.DeployDABack();
        }

        public static async Task DeployBack(Type type, Grid grid)
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.DeployBack(type, grid);
        }

        public static async Task UnDeployBack()
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.UnDeployBack();
        }

        public static async void NavigateTo(Type page)
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.Navigate(page);
        }

        public static async Task Cleanup()
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            /*
            List<string> oldfiles = new List<string>()
            {
                "temp\\adminusers.txt",
                "temp\\azure.txt",
                "temp\\ene.txt",
                "temp\\entuser.txt",
                "temp\\gitversion.txt",
                "temp\\hsmst.txt",
                "temp\\ifp.txt",
                "temp\\mf.txt",
                "temp\\model.txt",
                "temp\\os.txt",
                "temp\\pcname.txt",
                "temp\\rs.txt",
                "temp\\shell.txt",
                "temp\\username.txt",
                "azureleave.exe",
                "bootim.exe",
                "cuaboff.exe",
                "cuabon.exe",
                "delent.exe",
                "delentf.exe",
                "delhismserv.exe",
                "deloem.exe",
                "delridf.exe",
                "disadmin.exe",
                "dishis.exe",
                "enadmin.exe",
                "enhis.exe",
                "fixti.exe",
                "fullsoftinfo.txt",
                "fullpcinfo.txt",
                "getpcinfo.exe",
                "getsoftinfo.exe",
                "getuserinfo.exe",
                "langen.exe",
                "langfr.exe",
                "old2new",
                "rdti.exe",
                "reboot.exe",
                "rmdirhismgr.exe",
                "shell.exe",
                "starthis.exe",
                "stophis.exe",
                "Unowhy Tools Updater.exe",
                "utconfdel.exe",
                "utkeydel.exe",
                "version.txt",
                "winhelloent.exe",
                "winre.exe",
                "update.zip",
                "delhisqool.exe",
                "ene.txt",
                "model.txt",
                "os.txt",
                "pcname.txt",
                "tversion.txt",
                "username.txt",
                "clog.html",
                "fr.resx",
                "en.resx",
                "UTwait.exe",
                "UTsplash.exe",
                "7z.dll",
                "7zip.exe"
            };
            
            foreach(string file in oldfiles)
            {
                mainWindow.SplashText.Text = "Cleanup... (Checking)";
                mainWindow.SplashBar.Value++;
                await Task.Delay(1);
                if (File.Exists(file))
                {
                    mainWindow.SplashText.Text = "Cleanup... (" + file + ")";
                    File.Delete(file);
                }
            }
            */
            mainWindow.SplashBar.Value = 10;
            mainWindow.SplashText.Text = "Cleanup... (Checking)";
            if (Directory.Exists("temp"))
            {
                mainWindow.SplashText.Text = "Cleanup... (\\temp)";
                Directory.Delete("temp", true);
            }
            await Task.Delay(300);
            mainWindow.SplashBar.Value = 20;
            mainWindow.SplashText.Text = "Cleanup... (Checking)";
            if (Directory.Exists(Path.GetTempPath() + "\\Unowhy Tools\\Temps"))
            {
                mainWindow.SplashText.Text = "Cleanup... (%temp%\\Unowhy Tools\\Temps)";
                Directory.Delete(Path.GetTempPath() + "\\Unowhy Tools\\Temps", true);
            }
            await Task.Delay(300);
            mainWindow.SplashBar.Value = 30;
            await Task.Delay(300);
        }

        public static async Task SendStats(string launchmode)
        {
            Guid uuid = Guid.NewGuid();
            string uuidString = uuid.ToString();

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools", true);
            object id = key.GetValue("ID", null);
            if (id == null)
            {
                key.SetValue("ID", uuidString, RegistryValueKind.String);
            }

            string idString = key.GetValue("ID", null).ToString();
            string langString = key.GetValue("Lang", null).ToString();

            bool tray;
            TaskScheduler.TaskService taskService = new TaskScheduler.TaskService();
            TaskScheduler.Task uttask = taskService.GetTask("Unowhy Tools Tray Launch");
            if(uttask != null)
            {
                if (uttask.Definition.Settings.Enabled)
                {
                    tray = true;
                }
                else
                {
                    tray = false;
                }
            }
            else
            {
                tray = false;
            }

            if(UT.CheckInternet())
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string url = "https://api.sty1001.fr/ut-stats";
                        string jsonData = "{ \"id\" : \"" + idString + "\", \"version\" : \"" + UT.version.getverfull().ToString().Insert(2, ".") + "\", \"build\" : \"" + UT.version.getverbuild().ToString() + "\", \"lang\" : \"" + langString + "\", \"launchmode\" : \"" + launchmode + "\", \"trayena\" : " + tray.ToString().ToLower() + ",  \"isdeb\" : " + UT.version.isdeb().ToString().ToLower() + " }";
                        Write2Log("Sending Stats to \"" + url + "\" with \"" + jsonData + "\"");
                        StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(url, content);
                        if(response.StatusCode == HttpStatusCode.OK) 
                        {
                            Write2Log("Sent! Response:" + response.Content + "(" + response.StatusCode + ")");
                        }
                        else
                        {
                            Write2Log("Error! Response:" + response.Content + "(" + response.StatusCode + ")");
                        }
                    }
                    catch (Exception e)
                    {
                        Write2Log("Failed: " + e);
                    }
                    
                }
            }
        }

        public static async Task TrayCheck()
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.SplashText.Text = "Preparing Tray... (Checking)";
            await Task.Delay(300);
            if (await CheckTray())
            {

            }
            else
            {
                TaskScheduler.TaskService taskService = new TaskScheduler.TaskService();

                TaskScheduler.Task uttask = taskService.GetTask("Unowhy Tools Tray Launch");
                if (uttask == null)
                {
                    mainWindow.SplashText.Text = "Preparing Tray... (Creating)";

                    TaskScheduler.TaskDefinition taskDefinition = taskService.NewTask();
                    taskDefinition.RegistrationInfo.Date = DateTime.Now;
                    taskDefinition.RegistrationInfo.Author = "Unowhy Tools";
                    taskDefinition.RegistrationInfo.Description = "Launch Unowhy Tools Tray at user logon. If your account isn't set as admin, tray startup can fail. Go to Unowhy Tools and set your account as admin.";
                    taskDefinition.RegistrationInfo.URI = @"\STY1001\Unowhy Tools\Unowhy Tools Tray Launch";
                    taskDefinition.Principal.GroupId = new SecurityIdentifier("S-1-5-32-544").ToString();
                    taskDefinition.Principal.RunLevel = TaskScheduler.TaskRunLevel.Highest;
                    taskDefinition.Settings.DisallowStartIfOnBatteries = false;
                    taskDefinition.Settings.StopIfGoingOnBatteries = false;
                    taskDefinition.Settings.AllowHardTerminate = true;
                    taskDefinition.Settings.StartWhenAvailable = true;
                    taskDefinition.Settings.RunOnlyIfNetworkAvailable = false;
                    taskDefinition.Settings.IdleSettings.StopOnIdleEnd = true;
                    taskDefinition.Settings.IdleSettings.RestartOnIdle = false;
                    taskDefinition.Settings.Enabled = true;
                    taskDefinition.Settings.Hidden = false;
                    taskDefinition.Settings.RunOnlyIfIdle = false;
                    taskDefinition.Settings.DisallowStartOnRemoteAppSession = false;
                    taskDefinition.Settings.UseUnifiedSchedulingEngine = true;
                    taskDefinition.Settings.WakeToRun = false;
                    taskDefinition.Settings.ExecutionTimeLimit = TimeSpan.Zero;
                    taskDefinition.Settings.Priority = System.Diagnostics.ProcessPriorityClass.Normal;
                    taskDefinition.Triggers.Add(new TaskScheduler.LogonTrigger { Enabled = true });
                    taskDefinition.Actions.Add(new TaskScheduler.ExecAction(Process.GetCurrentProcess().MainModule.FileName, "-tray", Directory.GetCurrentDirectory()));
                    taskService.RootFolder.RegisterTaskDefinition(@"Unowhy Tools Tray Launch", taskDefinition);

                    try
                    {
                        await Task.Delay(1000);
                        mainWindow.SplashText.Text = "Preparing Tray... (Launching)";
                        taskService = new TaskScheduler.TaskService();
                        uttask = taskService.GetTask("Unowhy Tools Tray Launch");
                        uttask.Run();
                    }
                    catch
                    {
                        mainWindow.SplashText.Text = "Preparing Tray... (Waiting)";
                        await Task.Delay(5000);
                        mainWindow.SplashText.Text = "Preparing Tray... (Launching)";
                        taskService = new TaskScheduler.TaskService();
                        uttask = taskService.GetTask("Unowhy Tools Tray Launch");
                        uttask.Run();
                    }
                }
                else
                {
                    if (uttask.Definition.Settings.Enabled)
                    {
                        mainWindow.SplashText.Text = "Preparing Tray... (Launching)";
                        uttask.Run();
                    }
                }
            }
        }

        public static async Task<bool> FirstStart()
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.SplashText.Text = "Checking... (Folder)";
            await Task.Delay(100);

            if (!Directory.Exists("C:\\UTSConfig"))
            {
                Directory.CreateDirectory("C:\\UTSConfig");
            }

            if (!File.Exists("C:\\UTSConfig\\serial.txt"))
            {
                File.WriteAllText("C:\\UTSConfig\\serial.txt", "Null");
            }

            mainWindow.SplashBar.Value++;

            if (!Directory.Exists(Path.GetTempPath() + "\\Unowhy Tools"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\Unowhy Tools");
            }

            if (!Directory.Exists(Path.GetTempPath() + "\\Unowhy Tools\\Logs"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\Unowhy Tools\\Logs");
            }

            if (!Directory.Exists(Path.GetTempPath() + "\\Unowhy Tools\\Temps"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\Unowhy Tools\\Temps");
            }

            if (!Directory.Exists(Path.GetTempPath() + "\\Unowhy Tools\\Temps\\Update"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\Unowhy Tools\\Temps\\Update");
            }

            if (!Directory.Exists(Path.GetTempPath() + "\\Unowhy Tools\\Temps\\Drivers"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\Unowhy Tools\\Temps\\Drivers");
            }

            if (!Directory.Exists(Path.GetTempPath() + "\\Unowhy Tools\\Temps\\WebView2"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\Unowhy Tools\\Temps\\WebView2");
            }

            if (!Directory.Exists(Path.GetTempPath() + "\\Unowhy Tools\\Temps\\Service"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "\\Unowhy Tools\\Temps\\Service");
            }

            if (!File.Exists(Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt"))
            {
                var f = File.CreateText(Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt");
                f.Close();
                Write2Log("============");
                Write2Log("Unowhy Tools");
                Write2Log(" by STY1001 ");
                Write2Log("============");
                Write2Log("");
                Write2Log("Start of logs, logs are saved locally and can help to fix issues");
                Write2Log("If you have an issue, please, open an issue on Github and give me logs to help you");
                Write2Log("You can cleanup logs on UT's settings");
                Write2Log("Last logs are on bottom\n\n\n");
            }

            mainWindow.SplashBar.Value++;
            mainWindow.SplashText.Text = "Checking... (Registry)";
            await Task.Delay(100);

            RegistryKey keysoft = Registry.CurrentUser.OpenSubKey(@"Software", true);

            RegistryKey keysty = Registry.CurrentUser.OpenSubKey(@"Software\STY1001", true);
            if (keysty == null)
            {
                keysoft.CreateSubKey("STY1001");
            }

            keysty = Registry.CurrentUser.OpenSubKey(@"Software\STY1001", true);

            RegistryKey keyut = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", true);
            if (keyut == null)
            {
                keysty.CreateSubKey("Unowhy Tools");
            }

            mainWindow.SplashBar.Value++;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools", true);
            object us = key.GetValue("UpdateStart", null);
            if (us == null)
            {
                //await RunMin("reg", "add \"HKCU\\Software\\STY1001\\Unowhy Tools\" /v UpdateStart /d 1 /t REG_SZ /f");
                key.SetValue("UpdateStart", "1", RegistryValueKind.String);
            }

            object o = key.GetValue("Lang", null);
            if (o == null)
            {
                //await RunMin("reg", "add \"HKCU\\Software\\STY1001\\Unowhy Tools\" /v Lang /d EN /t REG_SZ /f");
                key.SetValue("Lang", "EN", RegistryValueKind.String);
            }

            object i = key.GetValue("Init2", null);
            if (i == null)
            {
                key.SetValue("Init2", "0", RegistryValueKind.String);
            }

            mainWindow.SplashBar.Value++;

            string i2 = key.GetValue("Init2").ToString();
            if (i2 == "1")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static async Task<bool> CheckTray()
        {
            bool trayrun = false;
            string tl = await RunReturn("powershell", "start-process -FilePath \"tasklist\" -ArgumentList \"/v\" -nonewwindow");
            if (tl.Contains("Unowhy Tools Tray"))
            {
                trayrun = true;
                Write2Log("UT Tray is already running");
            }
            else
            {
                Write2Log("UT Tray is not running");
            }

            return trayrun;
        }

        public static async Task<string> FolderSizeString(string directoryPath)
        {
            try
            {
                long size = 0;
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

                await Task.Run(() =>
                {
                    FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

                    foreach (FileInfo file in files)
                    {
                        Interlocked.Add(ref size, file.Length);
                    }
                });

                string rep = "0.00 B";
                rep = FormatSize(size);
                return rep;
            }
            catch
            {
                return "-.-- B";
            }
        }

        public static string FormatSize(long byteSize)
        {
            const int scale = 1024;
            string[] orders = { "GB", "MB", "KB", "B" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (byteSize > max)
                    return string.Format("{0:0.00} {1}", (double)byteSize / max, order);

                max /= scale;
            }

            return "0.00 B";
        }

        public static void Delay(int Time_delay)
        {
            int i = 0;
            System.Timers.Timer _delayTimer = new System.Timers.Timer();
            _delayTimer.Interval = Time_delay;
            _delayTimer.AutoReset = false;
            _delayTimer.Elapsed += (s, args) => i = 1;
            _delayTimer.Start();
            while (i == 0) { };
        }

        public static async Task<string> RunReturn(string file, string args)
        {
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);

            Write2Log("RunReturn " + file + " " + args);

            string output = await Task.Run(() =>
            {
                Process get = new Process();
                get.StartInfo.FileName = file;
                get.StartInfo.Arguments = args;
                get.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                get.StartInfo.UseShellExecute = false;
                get.StartInfo.RedirectStandardOutput = true;
                get.StartInfo.CreateNoWindow = true;
                get.Start();
                get.WaitForExit();
                return get.StandardOutput.ReadToEnd();
            });

            Write2Log("Done RunReturn " + file + " " + args + " => " + output);

            return output;
        }

        public static string GetLine(string text, int line)
        {
            int line2 = line - 1;
            var lines = text.Split('\n');
            return lines[line2].Replace("\n", "").Replace("\r", "");
        }

        public static string GetLang(string name)
        {
            string resxFile = @".\lang\en.resx";
            string enresx = @".\lang\en.resx";
            string frresx = @".\lang\fr.resx";
            RegistryKey ut = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            if (ut != null)
            {
                object utl = ut.GetValue("Lang", null);
                if (utl != null)
                {
                    if (ut.GetValue("Lang").ToString() == "EN") resxFile = enresx;
                    else if (ut.GetValue("Lang").ToString() == "FR") resxFile = frresx;
                    ResXResourceSet resxSet1 = new ResXResourceSet(resxFile);
                    Write2Log("Get lang " + name + " => " + resxSet1.GetString(name));
                    return resxSet1.GetString(name);
                }

                ResXResourceSet resxSet2 = new ResXResourceSet(resxFile);
                Write2Log("Get lang " + name + " => " + resxSet2.GetString(name));
                return resxSet2.GetString(name);
            }

            ResXResourceSet resxSet3 = new ResXResourceSet(resxFile);
            Write2Log("Get lang " + name + " => " + resxSet3.GetString(name));
            return resxSet3.GetString(name);


            /*
            //Check the current saved language
            string resxFile = @".\lang\en.resx";
            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();
            string enresx = @".\lang\en.resx";
            string frresx = @".\lang\fr.resx";
            //Chose the ResX file
            if (utls == "EN") resxFile = enresx;                     //English   
            else if (utls == "FR") resxFile = frresx;               //French
            ResXResourceSet resxSet = new ResXResourceSet(resxFile);
            Write2Log("Get lang " + name + " => " + resxSet.GetString(name));
            return resxSet.GetString(name);
            */
        }

        public async static Task RunMin(string file, string args)
        {
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);

            Write2Log("RunMin " + file + " " + args);

            await Task.Run(() =>
            {
                Process p = new Process();
                p.StartInfo.FileName = file;
                p.StartInfo.Arguments = args;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            });

            Write2Log("Done RunMin " + file + " " + args);
        }

        public static void Write2Log(string log)
        {
            if (File.Exists(Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt"))
            {
                File.AppendAllText(Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt", DateTime.Now.ToString() + " : " + log + Environment.NewLine);
            }
        }

        public static void applylang_global()
        {

        }

        public static bool CheckAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RunAdmin(string args)
        {
            // Restart and run as admin
            var exeName = Process.GetCurrentProcess().MainModule.FileName;
            ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            startInfo.Verb = "runas";
            startInfo.Arguments = $"{args}";
            Process.Start(startInfo);
            System.Windows.Application.Current.Shutdown();
        }

        public static bool CheckInternet()
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true) return true;
            else return false;
        }

        public static async Task Check()
        {
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;

            Data UTdata = new Data();

            mainWindow.SplashText.Text = "Checking... (Getting Hardware Info)";
            await Task.Delay(100);
            Write2Log("Getting PC Infos");

            string hn = await RunReturn("hostname", "");
            string mf = await RunReturn("wmic", "computersystem get manufacturer");
            string md = await RunReturn("wmic", "computersystem get model");
            string os = await RunReturn("wmic", "os get caption");
            string bios = await RunReturn("wmic", "bios get smbiosbiosversion");
            string sn = await RunReturn("wmic", "bios get serialnumber");
            string cpu = await RunReturn("wmic", "cpu get name");
            string ram = await RunReturn("wmic", "computersystem get totalphysicalmemory");

            mainWindow.SplashBar.Value++;

            UTdata.HostName = hn.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            UTdata.mf = GetLine(mf, 2);
            UTdata.md = GetLine(md, 2);
            UTdata.os = GetLine(os, 2);
            UTdata.bios = GetLine(bios, 2);
            UTdata.sn = GetLine(sn, 2);
            UTdata.cpu = GetLine(cpu, 2);
            UTdata.ram = GetLine(ram, 2);

            if (UTdata.UserID.Contains(UTdata.HostName.ToLower()))
            {
                UTdata.User = UTdata.UserID.Replace(UTdata.HostName.ToLower() + "\\", "");
            }
            else if (UTdata.UserID.Contains("azuread"))
            {
                UTdata.User = UTdata.UserID;
                UTdata.AADUser = true;
            }

            mainWindow.SplashBar.Value++;

            Write2Log(UTdata.HostName);
            Write2Log(UTdata.User);
            Write2Log(UTdata.UserID);
            Write2Log(UTdata.mf);
            Write2Log(UTdata.md);
            Write2Log(UTdata.os);
            Write2Log(UTdata.bios);
            Write2Log(UTdata.sn);
            Write2Log(UTdata.cpu);
            Write2Log(UTdata.ram);

            Write2Log("Done");

            mainWindow.SplashBar.Value++;
            mainWindow.SplashText.Text = "Checking... (Getting Software Info)";
            await Task.Delay(100);

            Write2Log("Checking if is Administrator or Adminitrateur");

            string adminname = await RunReturn("net", "user");
            if (adminname.Contains("Administrateur"))
            {
                UTdata.AdminName = "Administrateur";
            }
            else if (adminname.Contains("Administrator"))
            {
                UTdata.AdminName = "Administrator";
            }

            Write2Log("=> " + UTdata.AdminName);

            Write2Log("Checking if is Administrators or Adminitrateurs");

            string adminsname = await RunReturn("net", "localgroup");
            if (adminsname.Contains("Administrateurs"))
            {
                UTdata.AdminsName = "Administrateurs";
            }
            else if (adminsname.Contains("Administrators"))
            {
                UTdata.AdminsName = "Administrators";
            }

            Write2Log("=> " + UTdata.AdminsName);

            mainWindow.SplashBar.Value++;

            Write2Log("====== Dynamic Buttons ======");

            string preazure = await RunReturn("powershell", "start-process -FilePath \"dsregcmd\" -ArgumentList \"/status\" -nonewwindow");
            string preadmins = await RunReturn("net", $"localgroup {UTdata.AdminsName}");
            string users = await RunReturn("net", "user");
            string bcd = await RunReturn("bcdedit", "");

            string admins = preadmins.ToLower();
            string azure = GetLine(preazure, 6);

            mainWindow.SplashBar.Value++;
            mainWindow.SplashText.Text = "Checking... (Analysing PC Info)";
            await Task.Delay(300);

            #region Hisqool Manager

            Write2Log("=== HSM service ===");
            bool hsme = await UT.serv.exist("HiSqoolManager");
            if (hsme)
            {
                ServiceController sc = new ServiceController();
                sc.ServiceName = "HiSqoolManager";

                if (sc.StartType == ServiceStartMode.Automatic)
                {
                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        UTdata.HSMRunning = true;
                        UTdata.HSMEnabled = true;
                        UTdata.HSMExist = true;
                        UTdata.HSMExeExist = true;
                        Write2Log("HSM is running");
                    }
                    else
                    {
                        UTdata.HSMRunning = false;
                        UTdata.HSMEnabled = true;
                        UTdata.HSMExist = true;
                        UTdata.HSMExeExist = true;
                        Write2Log("HSM is stopped");
                    }
                }
                else
                {
                    UTdata.HSMRunning = false;
                    UTdata.HSMEnabled = false;
                    UTdata.HSMExist = true;
                    UTdata.HSMExeExist = true;
                    Write2Log("HSM is disabled");
                }
                if (!File.Exists("C:\\Program Files\\Unowhy\\HiSqool Manager\\HiSqoolManager.exe"))
                {
                    UTdata.HSMRunning = true;
                    UTdata.HSMEnabled = true;
                    UTdata.HSMExist = true;
                    UTdata.HSMExeExist = false;
                    Write2Log("HSM is deleted");
                }
            }
            else
            {
                UTdata.HSMRunning = false;
                UTdata.HSMEnabled = false;
                UTdata.HSMExist = false;
                UTdata.HSMExeExist = false;
                Write2Log("HSM services is not present");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region Azure

            Write2Log("=== Azure AD ===");
            if (azure.Contains("NO"))
            {
                UTdata.AAD = false;
                Write2Log("Azure AD joined: NO");
            }
            else
            {
                UTdata.AAD = true;
                Write2Log("Azure AD joined: YES");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region Admins

            Write2Log("=== Admins ===");
            if (admins.Contains(UTdata.User))
            {
                UTdata.Admin = true;
                Write2Log("User is admin");
            }
            else
            {
                UTdata.Admin = false;
                Write2Log("User is not admin");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region Folders

            Write2Log("=== Folders ===");
            if (Directory.Exists("C:\\ProgramData\\RIDF") == false)
            {
                UTdata.RIDFFolderExist = false;
                Write2Log("RIDF: False");
            }
            else
            {
                UTdata.RIDFFolderExist = true;
                Write2Log("RIDF: True");
            }
            if (Directory.Exists("C:\\ProgramData\\ENT") == false)
            {
                UTdata.ENTFolderExist = false;
                Write2Log("ENT: False");
            }
            else
            {
                UTdata.ENTFolderExist = true;
                Write2Log("ENT: True");
            }
            if (Directory.Exists("C:\\Windows\\system32\\OEM") == false)
            {
                UTdata.OEMFolderExist = false;
                Write2Log("OEM: False");
            }
            else
            {
                UTdata.OEMFolderExist = true;
                Write2Log("OEM: True");
            }
            if (Directory.Exists("C:\\Program Files\\Unowhy\\TO_INSTALL") == false)
            {
                UTdata.TIFolderExist = false;
                Write2Log("TO_INSTALL: False");
            }
            else
            {
                UTdata.TIFolderExist = true;
                Write2Log("TO_INSTALL: True");
            }
            if (Directory.Exists("C:\\Program Files\\Unowhy\\HiSqool Manager") == false)
            {
                UTdata.HSQMFolderExist = false;
                Write2Log("Hisqool Manager: False");
            }
            else
            {
                UTdata.HSQMFolderExist = true;
                Write2Log("Hisqool Manager: True");
            }
            if (Directory.Exists("C:\\Program Files\\Unowhy\\HiSqool") == false)
            {
                UTdata.HSQFolderExist = false;
                Write2Log("Hisqool: False");
            }
            else
            {
                UTdata.HSQFolderExist = true;
                Write2Log("Hisqool: True");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region ENT user

            Write2Log("=== ./ENT ===");
            if (users.Contains("ENT"))
            {
                UTdata.ENTUser = true;
                Write2Log("ENT User is present");
            }
            else
            {
                UTdata.ENTUser = false;
                Write2Log("ENT User is not present");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region Windows Hello Enterprise

            Write2Log("=== Win Hello Ent ===");
            Write2Log("Open Key");
            RegistryKey whe1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\PassportForWork");
            //RegistryKey whe2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WinBio\\Credential Provider");
            RegistryKey whe3 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\PassportForWork\\DynamicLock");
            Write2Log("Open Key End");
            if (whe1 == null || /*whe2 == null ||*/ whe3 == null)
            {
                UTdata.WHE = false;
                Write2Log("WHE No Key");
            }
            else
            {
                Write2Log("Open Val");
                var val5 = whe3.GetValue("DynamicLock");
                //string val2 = whe2.GetValue("Domain Accounts");
                var val1 = whe1.GetValue("Enabled");
                var val3 = whe1.GetValue("RequireSecurityDevice");
                //string val4 = whe1.GetValue("UseCertificateForOnPremAuth").ToString();
                Write2Log("Open Val End");

                if (val1 == null || val3 == null || val5 == null)
                {
                    UTdata.WHE = false;
                    Write2Log("WHE No Value");
                }
                else
                {
                    string val21 = val1.ToString();
                    string val23 = val3.ToString();
                    string val25 = val5.ToString();
                    Write2Log("Read Val");
                    if (val21.Contains("1") && /*val2.Contains("1") &&*/ val23.Contains("1") && /*val4.Contains("1") &&*/ val25.Contains("1"))
                    {
                        UTdata.WHE = true;
                        Write2Log("WHE OK");
                    }
                    else
                    {
                        UTdata.WHE = false;
                        Write2Log("WHE Not OK");
                    }
                    Write2Log("Read Val End");
                }
            }
            if (whe1 != null)
            {
                whe1.Close();
            }
            /*if (whe2 != null)
            {
                whe2.Close();
            }*/
            if (whe3 != null)
            {
                whe3.Close();
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region TI Start

            Write2Log("=== Auto script of TI ===");
            DirectoryInfo dir = new DirectoryInfo("C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup");
            FileInfo[] files = dir.GetFiles("silent_" + "*.*");
            if (files.Length > 0)
            {
                UTdata.TIStartup = true;
                Write2Log("Present");
            }
            else
            {
                UTdata.TIStartup = false;
                Write2Log("Not present");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region BootIM

            Write2Log("=== BootIM ===");
            RegistryKey bim1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\bootim.exe");
            if (bim1 == null)
            {
                UTdata.BIM = true;
                Write2Log("OK");
            }
            else
            {
                UTdata.BIM = false;
                Write2Log("Redirected");
            }
            if (bim1 != null)
            {
                bim1.Close();
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region BCDedit

            Write2Log("=== BCD ===");
            if (bcd.Contains("IgnoreAllFailures"))
            {
                UTdata.BCD = false;
                Write2Log("BCD patched");
            }
            else
            {
                UTdata.BCD = true;
                Write2Log("BCD normal");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region Shell

            Write2Log("=== Shell ===");
            RegistryKey shellkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon");
            string shellval = shellkey.GetValue("Shell").ToString();
            if (shellval == "explorer.exe")
            {
                UTdata.ShellOK = true;
            }
            else
            {
                UTdata.ShellOK = false;
            }
            Write2Log($"Shell: {shellval}");
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region TaskMGR

            Write2Log("=== TaskMGR ===");
            RegistryKey tmgr = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            if (tmgr != null)
            {
                object t = tmgr.GetValue("DisableTaskMGR", null);
                if (t == null)
                {
                    UTdata.TaskMGR = true;
                    Write2Log("TaskMGR is enabled");
                }
                else
                {
                    UTdata.TaskMGR = false;
                    Write2Log("TaskMGR is disabled");
                }
            }
            else
            {
                UTdata.TaskMGR = true;
                Write2Log("TaskMGR is enabled");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region Account Lockout

            Write2Log("=== Account Lockout ===");
            RegistryKey la = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            if (la != null)
            {
                object l = la.GetValue("DisableLockWorkstation", null);
                if (l == null)
                {
                    UTdata.LockA = true;
                    Write2Log("Account Lockout is enabled");
                }
                else
                {
                    UTdata.LockA = false;
                    Write2Log("Account Lockout is disabled");
                }
            }
            else
            {
                UTdata.LockA = true;
                Write2Log("Account Lockout is enabled");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region Camera Privacy Overlay

            Write2Log("=== Camera Privacy Overlay ===");
            RegistryKey co = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\OEM\\Device\\Capture");
            if (co != null)
            {
                object c = co.GetValue("NoPhysicalCameraLED", null);
                if (c == null)
                {
                    UTdata.CamOver = false;
                    Write2Log("Camera Privacy Overlay No val");
                }
                else
                {
                    int lc2 = (int)co.GetValue("NoPhysicalCameraLED", 0);
                    if (lc2 == 1)
                    {
                        UTdata.CamOver = true;
                        Write2Log("Camera Privacy Overlay OK");
                    }
                    else
                    {
                        UTdata.CamOver = false;
                        Write2Log("Camera Privacy Overlay is 0");
                    }
                }
            }
            else
            {
                UTdata.CamOver = false;
                Write2Log("Camera Privacy Overlay No key");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            #region Windows verbose status

            Write2Log("=== Windows verbose status ===");
            RegistryKey vo = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            if (vo != null)
            {
                object v = vo.GetValue("VerboseStatus", null);
                if (v == null)
                {
                    UTdata.VerbStat = false;
                    Write2Log("Windows verbose status No val");
                }
                else
                {
                    int lv2 = (int)vo.GetValue("VerboseStatus", 0);
                    if (lv2 == 1)
                    {
                        UTdata.VerbStat = true;
                        Write2Log("Windows verbose status OK");
                    }
                    else
                    {
                        UTdata.VerbStat = false;
                        Write2Log("Windows verbose status is 0");
                    }
                }
            }
            else
            {
                UTdata.VerbStat = false;
                Write2Log("Windows verbose status No key");
            }
            Write2Log("=== End ===" + Environment.NewLine);

            #endregion

            Write2Log("====== End ======");

            mainWindow.SplashBar.Value++;
        }

        public static async Task Extract(string file, string outPath)
        {
            string packUri = "pack://application:,,,/Resources/" + file;
            var uri = new Uri(packUri);
            var partUri = PackUriHelper.GetPartUri(uri);
            var streamInfo = System.Windows.Application.GetResourceStream(uri);

            using (var inputStream = streamInfo.Stream)
            using (var outputStream = File.Create(outPath))
            {
                await inputStream.CopyToAsync(outputStream);
            }
        }

        public static BitmapImage GetImgSource(string resname)
        {
            BitmapImage bmp = new BitmapImage(new System.Uri("pack://application:,,,/Resources/" + resname));
            return bmp;
        }

        public static Icon GetIconFromRes(string resname)
        {
            var imgsource = UT.GetImgSource(resname);

            Bitmap bitmap = null;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imgsource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                bitmap = new Bitmap(ms);
            }

            Icon icon = Icon.FromHandle(bitmap.GetHicon());
            return icon;
        }

        public static bool DialogQShow(string msg, string img)
        {
            //var mainWindow = Window.GetWindow(this) as Unowhy_Tools_WPF.Views.MainWindow;
            Write2Log("DialogQ: " + msg + " " + img);
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            if (mainWindow.ShowDialogQ(msg, GetImgSource(img)))
            {
                Write2Log("Result: Yes");
                return true;
            }
            else
            {
                Write2Log("Result: No");
                return false;
            }
        }

        public static void DialogIShow(string msg, string img)
        {
            Write2Log("DialogI: " + msg + " " + img);
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.ShowDialogI(msg, GetImgSource(img));
        }

        public class Data : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private static string _hostname;
            private static string _userid;
            private static string _user;
            private static string _sn;
            private static string _mf;
            private static string _md;
            private static string _os;
            private static string _bios;
            private static string _cpu;
            private static string _ram;
            private static string _adminsname;
            private static string _adminname;
            private static bool _taskmgr;
            private static bool _locka;
            private static bool _admin;
            private static bool _aad;
            private static bool _aaduser;
            private static bool _bcd;
            private static bool _entuser;
            private static bool _whe;
            private static bool _bim;
            private static bool _shellok;
            private static bool _tistartup;
            private static bool _hsmexist;
            private static bool _hsmenabled;
            private static bool _hsmrunning;
            private static bool _hsmexeexist;
            private static bool _ridffolderexist;
            private static bool _entfolderexist;
            private static bool _oemfolderexist;
            private static bool _tifolderexist;
            private static bool _hsqmfolderexist;
            private static bool _hsqfolderexist;
            private static bool _winre;
            private static bool _camover;
            private static bool _verbstat;

            private static bool _trayrunok;
            private static bool _runtray;

            public string HostName
            {
                get { return _hostname; }
                set
                {
                    _hostname = value;
                    OnPropertyChanged();
                }
            }
            public string UserID
            {
                get { return _userid; }
                set
                {
                    _userid = value;
                    OnPropertyChanged();
                }
            }
            public string User
            {
                get { return _user; }
                set
                {
                    _user = value;
                    OnPropertyChanged();
                }
            }
            public string sn
            {
                get { return _sn; }
                set
                {
                    _sn = value;
                    OnPropertyChanged();
                }
            }
            public string mf
            {
                get { return _mf; }
                set
                {
                    _mf = value;
                    OnPropertyChanged();
                }
            }
            public string md
            {
                get { return _md; }
                set
                {
                    _md = value;
                    OnPropertyChanged();
                }
            }
            public string os
            {
                get { return _os; }
                set
                {
                    _os = value;
                    OnPropertyChanged();
                }
            }
            public string bios
            {
                get { return _bios; }
                set
                {
                    _bios = value;
                    OnPropertyChanged();
                }
            }
            public string cpu
            {
                get { return _cpu; }
                set
                {
                    _cpu = value;
                    OnPropertyChanged();
                }
            }
            public string ram
            {
                get { return _ram; }
                set
                {
                    _ram = value;
                    OnPropertyChanged();
                }
            }
            public string AdminsName
            {
                get { return _adminsname; }
                set
                {
                    _adminsname = value;
                    OnPropertyChanged();
                }
            }
            public string AdminName
            {
                get { return _adminname; }
                set
                {
                    _adminname = value;
                    OnPropertyChanged();
                }
            }
            public bool TaskMGR
            {
                get { return _taskmgr; }
                set
                {
                    _taskmgr = value;
                    OnPropertyChanged();
                }
            }
            public bool LockA
            {
                get { return _locka; }
                set
                {
                    _locka = value;
                    OnPropertyChanged();
                }
            }
            public bool Admin
            {
                get { return _admin; }
                set
                {
                    _admin = value;
                    OnPropertyChanged();
                }
            }
            public bool AAD
            {
                get { return _aad; }
                set
                {
                    _aad = value;
                    OnPropertyChanged();
                }
            }
            public bool AADUser
            {
                get { return _aaduser; }
                set
                {
                    _aaduser = value;
                    OnPropertyChanged();
                }
            }
            public bool BCD
            {
                get { return _bcd; }
                set
                {
                    _bcd = value;
                    OnPropertyChanged();
                }
            }
            public bool ENTUser
            {
                get { return _entuser; }
                set
                {
                    _entuser = value;
                    OnPropertyChanged();
                }
            }
            public bool WHE
            {
                get { return _whe; }
                set
                {
                    _whe = value;
                    OnPropertyChanged();
                }
            }
            public bool BIM
            {
                get { return _bim; }
                set
                {
                    _bim = value;
                    OnPropertyChanged();
                }
            }
            public bool ShellOK
            {
                get { return _shellok; }
                set
                {
                    _shellok = value;
                    OnPropertyChanged();
                }
            }
            public bool TIStartup
            {
                get { return _tistartup; }
                set
                {
                    _tistartup = value;
                    OnPropertyChanged();
                }
            }
            public bool HSMExist
            {
                get { return _hsmexist; }
                set
                {
                    _hsmexist = value;
                    OnPropertyChanged();
                }
            }
            public bool HSMEnabled
            {
                get { return _hsmenabled; }
                set
                {
                    _hsmenabled = value;
                    OnPropertyChanged();
                }
            }
            public bool HSMRunning
            {
                get { return _hsmrunning; }
                set
                {
                    _hsmrunning = value;
                    OnPropertyChanged();
                }
            }
            public bool HSMExeExist
            {
                get { return _hsmexeexist; }
                set
                {
                    _hsmexeexist = value;
                    OnPropertyChanged();
                }
            }
            public bool RIDFFolderExist
            {
                get { return _ridffolderexist; }
                set
                {
                    _ridffolderexist = value;
                    OnPropertyChanged();
                }
            }
            public bool ENTFolderExist
            {
                get { return _entfolderexist; }
                set
                {
                    _entfolderexist = value;
                    OnPropertyChanged();
                }
            }
            public bool OEMFolderExist
            {
                get { return _oemfolderexist; }
                set
                {
                    _oemfolderexist = value;
                    OnPropertyChanged();
                }
            }
            public bool TIFolderExist
            {
                get { return _tifolderexist; }
                set
                {
                    _tifolderexist = value;
                    OnPropertyChanged();
                }
            }
            public bool HSQMFolderExist
            {
                get { return _hsqmfolderexist; }
                set
                {
                    _hsqmfolderexist = value;
                    OnPropertyChanged();
                }
            }
            public bool HSQFolderExist
            {
                get { return _hsqfolderexist; }
                set
                {
                    _hsqfolderexist = value;
                    OnPropertyChanged();
                }
            }
            public bool WinRE
            {
                get { return _winre; }
                set
                {
                    _winre = value;
                    OnPropertyChanged();
                }
            }
            public bool CamOver
            {
                get { return _camover; }
                set
                {
                    _camover = value;
                    OnPropertyChanged();
                }
            }
            public bool VerbStat
            {
                get { return _verbstat; }
                set
                {
                    _verbstat = value;
                    OnPropertyChanged();
                }
            }

            public bool TrayRunOK
            {
                get { return _trayrunok; }
                set
                {
                    _trayrunok = value;
                    OnPropertyChanged();
                }
            }
            public bool RunTray
            {
                get { return _runtray; }
                set
                {
                    _runtray = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
