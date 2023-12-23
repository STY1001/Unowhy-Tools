using System;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Mvvm.Contracts;
using Unowhy_Tools;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Diagnostics;
using System.IO;
using System.Windows.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net.Http;
using System.Xml.Linq;
using System.Windows.Navigation;
using System.Windows.Input;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.ApplicationServices;

namespace Unowhy_Tools_WPF.Views;

public partial class TrayWindow : Window
{
    private System.Windows.Forms.NotifyIcon trayIcon = new System.Windows.Forms.NotifyIcon();

    public string perf = "ded574b5-45a0-4f42-8737-46345c09c238";
    public string balanced = "00000000-0000-0000-0000-000000000000";
    public string efficiency = "961cc777-2547-4f9d-8174-7d86181b8a7a";

    public bool CamOn;
    public bool MicOn;

    public RegistryKey _camerakey;
    public RegistryKey _microkey;

    public bool ToTray = true;
    public bool IsPause = false;

    public DispatcherTimer _timerStats;
    public DispatcherTimer _timerPower;
    public DispatcherTimer _timerPriv;
    public DispatcherTimer _timerTimeDate;

    public ImageSource editimg = UT.GetImgSource("customize.png");
    public string editpath = "null";

    public ImageSource taskimg;
    public ImageSource cmdimg;
    public ImageSource regimg;
    public ImageSource gpimg;

    public string taskicon;
    public string cmdicon;
    public string regicon;
    public string gpicon;

    public string tasklab;
    public string cmdlab;
    public string reglab;
    public string gplab;

    public string taskpath;
    public string cmdpath;
    public string regpath;
    public string gppath;

    public PerformanceCounter cpuCounter1;
    public PerformanceCounter cpucapp1;
    public PerformanceCounter cpucapp2;
    public PerformanceCounter ramCounter1;
    public PerformanceCounter ramCounter2;
    public ulong totalram = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
    public DriveInfo driveInfos = new DriveInfo("C");

    [DllImport("powrprof.dll", EntryPoint = "PowerGetEffectiveOverlayScheme")]
    private static extern uint PowerGetEffectiveOverlayScheme(out Guid EffectiveOverlayPolicyGuid);

    [DllImportAttribute("powrprof.dll", EntryPoint = "PowerSetActiveOverlayScheme")]
    private static extern uint PowerSetActiveOverlayScheme(Guid OverlaySchemeGuid);

    public async Task InitTimer()
    {
        _timerStats = new DispatcherTimer();
        _timerStats.Interval = TimeSpan.FromSeconds(1);
        _timerStats.Tick += async (sender, e) => await CheckStats();
        _timerStats.Start();

        _timerPower = new DispatcherTimer();
        _timerPower.Interval = TimeSpan.FromSeconds(1);
        _timerPower.Tick += async (sender, e) => await CheckPower();
        _timerPower.Start();

        _timerPriv = new DispatcherTimer();
        _timerPriv.Interval = TimeSpan.FromSeconds(1);
        _timerPriv.Tick += async (sender, e) => await CheckPriv();
        _timerPriv.Start();

        _timerTimeDate = new DispatcherTimer();
        _timerTimeDate.Interval = TimeSpan.FromSeconds(1);
        _timerTimeDate.Tick += async (sender, e) => await UpdateTimeDate();
        _timerTimeDate.Start();
    }

    public async Task StartTimer()
    {
        IsPause = false;
    }

    public async Task StopTimer()
    {
        IsPause = true;
    }

    public async Task CheckStats()
    {
        if (!IsPause)
        {
            try
            {
                cpuCounter1.NextValue();
                cpucapp1.NextValue();
                cpucapp2.NextValue();
                await Task.Delay(100);
                var cpurint = Convert.ToInt32(cpuCounter1.NextValue());
                if (cpurint > 100) cpurint = 100;
                var cpupstring = $"{cpurint.ToString("0")} %";
                var cpucstring = $"{((ulong)cpucapp1.NextValue() * (ulong)cpucapp2.NextValue() / 100000.0).ToString("0.00")} GHz / {(cpucapp1.NextValue() / 1000.0).ToString("0.00")} GHz";

                var ramAvail = ramCounter2.NextValue();
                var ramAvail2 = ramCounter1.NextValue();
                var ramint = ((totalram - ramAvail2) / totalram) * 100;
                var rampstring = $"{ramint.ToString("0")} %";
                var ramcstring = $"{((totalram / 1024.0 / 1024.0 / 1024.0) - (ramAvail / 1024.0)).ToString("0.00")} GB / {(totalram / 1024.0 / 1024.0 / 1024.0).ToString("0.00")} GB";

                var totalFreeSpace = driveInfos.TotalFreeSpace;
                var totalSize = driveInfos.TotalSize;
                var storint = (int)(((double)(totalSize - totalFreeSpace) / (double)totalSize) * 100);
                var storpstring = $"{storint} %";
                var storcstring = $"{((totalSize - totalFreeSpace) / 1024.0 / 1024.0 / 1024.0).ToString("0.00")} GB / {(totalSize / 1024.0 / 1024.0 / 1024.0).ToString("0.00")} GB";

                cpuring.Progress = cpurint;
                cpuper.Text = cpupstring;
                cpucap.Text = cpucstring;
                ramring.Progress = ramint;
                ramper.Text = rampstring;
                ramcap.Text = ramcstring;
                storring.Progress = storint;
                storper.Text = storpstring;
                storcap.Text = storcstring;
            }
            catch
            {
                try
                {


                    cpuCounter1.NextValue();
                    cpucapp1.NextValue();
                    cpucapp2.NextValue();
                    await Task.Delay(100);
                    var cpurint = Convert.ToInt32(cpuCounter1.NextValue());
                    if (cpurint > 100) cpurint = 100;
                    var cpupstring = $"{cpurint.ToString("0")} %";
                    var cpucstring = $"{((ulong)cpucapp1.NextValue() * (ulong)cpucapp2.NextValue() / 100000.0).ToString("0.00")} GHz / {(cpucapp1.NextValue() / 1000.0).ToString("0.00")} GHz";



                    var ramAvail = ramCounter2.NextValue();
                    var ramAvail2 = ramCounter1.NextValue();
                    var ramint = (((new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory) - ramAvail2) / new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory) * 100;
                    var rampstring = $"{ramint.ToString("0")} %";
                    var ramcstring = $"{((new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024.0 / 1024.0 / 1024.0) - (ramAvail / 1024.0)).ToString("0.00")} Go / {(new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024.0 / 1024.0 / 1024.0).ToString("0.00")} Go";

                    var driveInfos = new DriveInfo("C");
                    var totalFreeSpace = driveInfos.TotalFreeSpace;
                    var totalSize = driveInfos.TotalSize;
                    var storint = (int)(((double)(totalSize - totalFreeSpace) / (double)totalSize) * 100);
                    var storpstring = $"{storint} %";
                    var storcstring = $"{((totalSize - totalFreeSpace) / 1024.0 / 1024.0 / 1024.0).ToString("0.00")} Go / {(totalSize / 1024.0 / 1024.0 / 1024.0).ToString("0.00")} Go";

                    cpuring.Progress = cpurint;
                    cpuper.Text = cpupstring;
                    cpucap.Text = cpucstring;
                    ramring.Progress = ramint;
                    ramper.Text = rampstring;
                    ramcap.Text = ramcstring;
                    storring.Progress = storint;
                    storper.Text = storpstring;
                    storcap.Text = storcstring;
                }
                catch
                {

                }
            }
        }
    }

    public void switch_Changed(object sender, RoutedEventArgs e)
    {
        if (camswitch.IsChecked == true)
        {
            CamOn = true;
        }
        else
        {
            CamOn = false;
        }

        if (micswitch.IsChecked == true)
        {
            MicOn = true;
        }
        else
        {
            MicOn = false;
        }

        if (CamOn)
        {
            _camerakey.SetValue("Value", "Allow");
        }
        else
        {
            _camerakey.SetValue("Value", "Deny");
        }

        if (MicOn)
        {

            _microkey.SetValue("Value", "Allow");
        }
        else
        {

            _microkey.SetValue("Value", "Deny");
        }

        CheckPriv();
    }

    public async Task CheckPriv()
    {
        if (!IsPause)
        {
            if (_camerakey.GetValue("Value") != null)
            {
                string cam = _camerakey.GetValue("Value").ToString();
                camswitch.IsEnabled = true;

                if (cam == "Allow")
                {
                    CamOn = true;
                }
                else if (cam == "Deny")
                {
                    CamOn = false;
                }

                if (CamOn)
                {
                    camswitch.IsChecked = true;
                }
                else
                {
                    camswitch.IsChecked = false;
                }
            }
            else
            {
                camswitch.IsEnabled = false;
            }
            
            if(_microkey.GetValue("Value") != null)
            {
                string mic = _microkey.GetValue("Value").ToString();
                micswitch.IsEnabled = true;

                if (mic == "Allow")
                {
                    MicOn = true;
                }
                else if (mic == "Deny")
                {
                    MicOn = false;
                }

                if (MicOn)
                {
                    micswitch.IsChecked = true;
                }
                else
                {
                    micswitch.IsChecked = false;
                }
            }
            else
            {
                micswitch.IsEnabled = false;
            }
        }
    }

    public async Task CheckPower()
    {
        if (!IsPause)
        {
            var batringint = ((double)SystemInformation.PowerStatus.BatteryLifePercent) * 100;
            var isOnAC = (bool)(SystemInformation.PowerStatus.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Online);

            PowerGetEffectiveOverlayScheme(out Guid modeguid);

            var powerscheme = modeguid.ToString();
            ImageSource batstatussource = null;

            if (isOnAC)
            {
                batstatussource = UT.GetImgSource("charge.png");
            }
            else
            {
                batstatussource = null;
            }
            
            string batcapstring = "null";
            ImageSource batmodesource = null;

            if (powerscheme.Contains(perf))
            {
                batcapstring = await UT.GetLang("bat.perf");
                batmodesource = UT.GetImgSource("power.png");
                pmodeperf.IsSelected = true;
            }
            else if (powerscheme.Contains(balanced))
            {
                batcapstring = await UT.GetLang("bat.balanced");
                batmodesource = UT.GetImgSource("balanced.png");
                pmodebalanced.IsSelected = true;
            }
            else if (powerscheme.Contains(efficiency))
            {
                batcapstring = await UT.GetLang("bat.efficiency");
                batmodesource = UT.GetImgSource("efficiency.png");
                pmodeefficiency.IsSelected = true;
            }

            ImageSource batimgsource = null;

            if(batringint > 90)
            {
                batimgsource = UT.GetImgSource("bat100.png");
            }
            else if (batringint > 75)
            {
                batimgsource = UT.GetImgSource("bat75.png");
            }
            else if(batringint > 50)
            {
                batimgsource = UT.GetImgSource("bat50.png");
            }
            else if(batringint > 25)
            {
                batimgsource = UT.GetImgSource("bat25.png");
            }
            else if(batringint > 10)
            {
                batimgsource = UT.GetImgSource("bat15.png");
            }
            else
            {
                batimgsource = UT.GetImgSource("bat0.png");
            }

            batring.Progress = batringint;
            batper.Text = $"{batringint.ToString("###")} %";
            batcap.Text = batcapstring;
            batimg.Source = batimgsource;
            qobatimg.Source = batimgsource;
            batmodeimg.Source = batmodesource;
            qopmodeimg.Source = batmodesource;
            batstatusimg.Source = batstatussource;
        }
    }

    public async Task applylang()
    {
        cpulab.Text = await UT.GetLang("usecpu");
        ramlab.Text = await UT.GetLang("useram");
        storlab.Text = await UT.GetLang("usestor");
        batlab.Text = await UT.GetLang("batinfo");
        labqo.Text = await UT.GetLang("quickoption");
        labqodesc.Text = await UT.GetLang("quickoptiondesc");
        labedit.Text = await UT.GetLang("quicklaunchedit");
        labeditdesc.Text = await UT.GetLang("quicklauncheditdesc");
        labprivacy.Text = await UT.GetLang("privacy");
        labpower.Text = await UT.GetLang("power");
        labpowermode.Text = await UT.GetLang("powermode");
        pmodebalancedtext.Text = await UT.GetLang("bat.balancedshort");
        pmodeefficiencytext.Text = await UT.GetLang("bat.efficiencyshort");
        pmodeperftext.Text = await UT.GetLang("bat.perfshort");
        labpvback.Text = await UT.GetLang("sysmonitor");
        labpvdescback.Text = await UT.GetLang("sysmonitordesc");
        labeditedit.Text = await UT.GetLang("quicklaunchsave");
        labeditdescedit.Text = await UT.GetLang("quicklaunchsavedesc");
        string UTsver = " (Version " + UT.version.getverfull().ToString().Insert(2, ".") + " (Build " + UT.version.getverbuild().ToString() + ") ";
        if (UT.version.isdeb()) UTsver = UTsver + "(Debug))";
        else UTsver = UTsver + "(Release))";
        labout.Text = await UT.GetLang("openut") + UTsver;
        labtask.Text = await UT.GetLang("opentask");
        labcmd.Text = await UT.GetLang("opencmd");
        labreg.Text = await UT.GetLang("openreg");
        labgp.Text = await UT.GetLang("opengp");
        editpath = await UT.GetLang("qleditclick");
    }

    public async Task UpdateTimeDate()
    {
        if (!IsPause)
        {
            DateTime now = DateTime.Now;
            string HM = now.ToString("HH:mm");
            string S = now.ToString("ss");
            string DMY = now.ToString("dd/MM/yyyy");

            time.Text = HM;
            time2.Text = S;
            date.Text = DMY;
        }
    }

    public ImageSource GetImgSourceFromPath(string path)
    {
        if (File.Exists(path))
        {
            string ext = Path.GetExtension(Path.GetFileName(path));
            if (ext == ".exe")
            {
                return UT.GetImageSourceFromExe(path);
            }
            else if (ext == ".ico")
            {
                return UT.GetImageSourceFromIco(path);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public TrayWindow(IThemeService themeService)
    {
        base.Visibility = Visibility.Visible;

        InitializeComponent();
        if (!UT.CheckAdmin())
        {
            UT.RunAdmin("-tray");
        }

        base.Deactivated += TrayWindow_Deactivated;

        base.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        trayIcon.Icon = UT.GetIconFromRes("UT.png");
        trayIcon.Text = "Unowhy Tools";
        trayIcon.Visible = true;
        trayIcon.MouseClick += TrayIcon_Click;

        var contextMenuStrip = new ContextMenuStrip();
        var opentray = new ToolStripButton("Open Tray");
        opentray.Click += TCM_Open;
        var closetray = new ToolStripButton("Exit");
        closetray.Click += TCM_Close;
        contextMenuStrip.Items.Add(opentray);
        contextMenuStrip.Items.Add(closetray);
        trayIcon.ContextMenuStrip = contextMenuStrip;
    }

    public async Task SetQL()
    {
        await UT.Config.Set("QLtaskicon", taskicon);
        await UT.Config.Set("QLtasklab", tasklab);
        await UT.Config.Set("QLtaskpath", taskpath);
        await UT.Config.Set("QLcmdicon", cmdicon);
        await UT.Config.Set("QLcmdlab", cmdlab);
        await UT.Config.Set("QLcmdpath", cmdpath);
        await UT.Config.Set("QLregicon", regicon);
        await UT.Config.Set("QLreglab", reglab);
        await UT.Config.Set("QLregpath", regpath);
        await UT.Config.Set("QLgpicon", gpicon);
        await UT.Config.Set("QLgplab", gplab);
        await UT.Config.Set("QLgppath", gppath);
        await CheckQL();
    }

    public async Task SetQL_Default()
    {
        await UT.Config.Set("QLtaskicon", "default");
        await UT.Config.Set("QLtasklab", "default");
        await UT.Config.Set("QLtaskpath", "default");
        await UT.Config.Set("QLcmdicon", "default");
        await UT.Config.Set("QLcmdlab", "default");
        await UT.Config.Set("QLcmdpath", "default");
        await UT.Config.Set("QLregicon", "default");
        await UT.Config.Set("QLreglab", "default");
        await UT.Config.Set("QLregpath", "default");
        await UT.Config.Set("QLgpicon", "default");
        await UT.Config.Set("QLgplab", "default");
        await UT.Config.Set("QLgppath", "default");
        await CheckQL();
    }

    public async Task CheckQL()
    {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools", true);
        taskicon = await UT.Config.Get("QLtaskicon");
        tasklab = await UT.Config.Get("QLtasklab");
        taskpath = await UT.Config.Get("QLtaskpath");
        cmdicon = await UT.Config.Get("QLcmdicon");
        cmdlab = await UT.Config.Get("QLcmdlab");
        cmdpath = await UT.Config.Get("QLcmdpath");
        regicon = await UT.Config.Get("QLregicon");
        reglab = await UT.Config.Get("QLreglab");
        regpath = await UT.Config.Get("QLregpath");
        gpicon = await UT.Config.Get("QLgpicon");
        gplab = await UT.Config.Get("QLgplab");
        gppath = await UT.Config.Get("QLgppath");

        if (taskicon.Contains("default")) taskimg = UT.GetImgSource("taskmgr.png");
        else taskimg = GetImgSourceFromPath(taskicon);
        if (cmdicon.Contains("default")) cmdimg = UT.GetImgSource("cmd.png");
        else cmdimg = GetImgSourceFromPath(cmdicon);
        if (regicon.Contains("default")) regimg = UT.GetImgSource("registry.png");
        else regimg = GetImgSourceFromPath(regicon);
        if (gpicon.Contains("default")) gpimg = UT.GetImgSource("script.png");
        else gpimg = GetImgSourceFromPath(gpicon);

        imgtask.Source = taskimg;
        imgcmd.Source = cmdimg;
        imgreg.Source = regimg;
        imggp.Source = gpimg;

        imgtaskedit.Source = taskimg;
        imgcmdedit.Source = cmdimg;
        imgregedit.Source = regimg;
        imggpedit.Source = gpimg;

        if (taskpath.Contains("default")) labtaskdesc.Text = "taskmgr.exe";
        else if (File.Exists(taskpath))labtaskdesc.Text = Path.GetFileName(taskpath);
        else labtaskdesc.Text = Path.GetFileName(taskpath) + " (error)";
        if (cmdpath.Contains("default")) labcmddesc.Text = "cmd.exe";
        else if (File.Exists(cmdpath)) labcmddesc.Text = Path.GetFileName(cmdpath);
        else labcmddesc.Text = Path.GetFileName(cmdpath) + " (error)";
        if (regpath.Contains("default")) labregdesc.Text = "regedit.exe";
        else if (File.Exists(regpath)) labregdesc.Text = Path.GetFileName(regpath);
        else labregdesc.Text = Path.GetFileName(regpath) + " (error)";
        if (gppath.Contains("default")) labgpdesc.Text = "gpedit.msc";
        else if (File.Exists(gppath)) labgpdesc.Text = Path.GetFileName(gppath);
        else labgpdesc.Text = Path.GetFileName(gppath) + " (error)";

        if (taskpath.Contains("default")) labtaskdescedit.Text = "taskmgr.exe";
        else if (File.Exists(taskpath)) labtaskdescedit.Text = Path.GetFileName(taskpath);
        else labtaskdescedit.Text = Path.GetFileName(taskpath) + " (error)";
        if (cmdpath.Contains("default")) labcmddescedit.Text = "cmd.exe";
        else if (File.Exists(cmdpath)) labcmddescedit.Text = Path.GetFileName(cmdpath);
        else labcmddescedit.Text = Path.GetFileName(cmdpath) + " (error)";
        if (regpath.Contains("default")) labregdescedit.Text = "regedit.exe";
        else if (File.Exists(regpath)) labregdescedit.Text = Path.GetFileName(regpath);
        else labregdescedit.Text = Path.GetFileName(regpath) + " (error)";
        if (gppath.Contains("default")) labgpdescedit.Text = "gpedit.msc";
        else if (File.Exists(gppath)) labgpdescedit.Text = Path.GetFileName(gppath);
        else labgpdescedit.Text = Path.GetFileName(gppath) + " (error)";

        if (tasklab.Contains("default")) labtaskedit.Text = await UT.GetLang("opentask");
        else labtaskedit.Text = tasklab;
        if (cmdlab.Contains("default")) labcmdedit.Text = await UT.GetLang("opencmd");
        else labcmdedit.Text = cmdlab;
        if (reglab.Contains("default")) labregedit.Text = await UT.GetLang("openreg");
        else labregedit.Text = reglab;
        if (gplab.Contains("default")) labgpedit.Text = await UT.GetLang("opengp");
        else labgpedit.Text = gplab;

        if (tasklab.Contains("default")) labtask.Text = await UT.GetLang("opentask");
        else labtask.Text = tasklab;
        if (cmdlab.Contains("default")) labcmd.Text = await UT.GetLang("opencmd");
        else labcmd.Text = cmdlab;
        if (reglab.Contains("default")) labreg.Text = await UT.GetLang("openreg");
        else labreg.Text = reglab;
        if (gplab.Contains("default")) labgp.Text = await UT.GetLang("opengp");
        else labgp.Text = gplab;
    }

    private async void Window_Initialized(object sender, EventArgs e)
    {
        await Task.Delay(1000);
        await ShowTray();
        await WaitControl.Show(await UT.GetLang("wait.check"), "check.png");
        await Task.Delay(500);

        _camerakey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\webcam", true);
        _microkey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\microphone", true);

        string utpath = Process.GetCurrentProcess().MainModule.FileName;
        UTbtndesc.Text = utpath;
        applylang();

        await CheckQL();

        await CheckPriv();
        try
        {
            cpuCounter1 = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            cpucapp1 = new PerformanceCounter("Processor Information", "Processor Frequency", "_Total");
            cpucapp2 = new PerformanceCounter("Processor Information", "% Processor Performance", "_Total");
            ramCounter1 = new PerformanceCounter("Memory", "Available Bytes");
            ramCounter2 = new PerformanceCounter("Memory", "Available MBytes");
        }
        catch
        {
            cpuCounter1 = new PerformanceCounter("Informations sur le processeur", "Pourcentage de rendement du processeur", "_Total");
            cpucapp1 = new PerformanceCounter("Informations sur le processeur", "Fréquence du processeur", "_Total");
            cpucapp2 = new PerformanceCounter("Informations sur le processeur", "Pourcentage de performances du processeur", "_Total");
            ramCounter1 = new PerformanceCounter("Mémoire", "Octets disponibles");
            ramCounter2 = new PerformanceCounter("Mémoire", "Mégaoctets disponibles");
        }
        await InitTimer();
        await Task.Delay(1000);
        await WaitControl.Hide();
        await Task.Delay(300);
        await HideTray();
        await Task.Delay(1000);
        if (UT.CheckInternet())
        {
            if (await UT.Config.Get("UpdateStart") == "1")
            {
                if (await UT.version.newver())
                {
                    var web = new HttpClient();
                    string newver = await UT.OnlineDatas.GetUpdates("utnewver");
                    newver = newver.Insert(2, ".");
                    newver = newver.Replace("\n", "");
                    string newverfull = await UT.GetLang("newver") + " (" + UT.version.getverfull().ToString().Insert(2, ".") + " -> " + newver + ")";
                    trayIcon.ShowBalloonTip(3000, "Unowhy Tools Updater", newverfull, ToolTipIcon.Info);
                }
            }
            try
            {
                await UT.SendStats("tray");
            }
            catch
            {

            }
        }
    }

    public async void TrayWindow_Deactivated(object sender, EventArgs e)
    {
        if (ToTray)
        {
            await HideTray();
        }
    }

    public async void TrayIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            await ShowTray();
        }
        else if (e.Button == MouseButtons.Right)
        {
            var contextMenuStrip = new ContextMenuStrip();

            var opentray = new ToolStripButton("Open Tray");
            opentray.Click += TCM_Open;

            var closetray = new ToolStripButton("Exit");
            closetray.Click += TCM_Close;

            contextMenuStrip.Items.Add(opentray);
            contextMenuStrip.Items.Add(closetray);

            trayIcon.ContextMenuStrip = contextMenuStrip;
        }
    }

    public async void TCM_Close(object sender, EventArgs e)
    {
        base.Close();
    }

    public async void TCM_Open(object sender, EventArgs e)
    {
        await ShowTray();
    }

    public async void Close_Click(object sender, RoutedEventArgs e)
    {
        await HideTray();
    }

    public async void Show_Command()
    {
        await ShowTray();
    }

    public async Task ShowTray()
    {
        await StartTimer();
        Topmost = true;
        Activate();

        var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
        Left = desktopWorkingArea.Right - Width;
        Top = desktopWorkingArea.Bottom - Height;

        base.IsEnabled = true;
        base.Visibility = Visibility.Visible;

        DoubleAnimation animg = new DoubleAnimation();
        animg.From = 1000;
        animg.To = 0;
        animg.Duration = TimeSpan.FromMilliseconds(300);
        animg.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform transg = new TranslateTransform();
        TrayGrid.RenderTransform = transg;
        transg.BeginAnimation(TranslateTransform.XProperty, animg);
        transg.BeginAnimation(TranslateTransform.YProperty, animg);

        await Task.Delay(300);

        DoubleAnimation animb = new DoubleAnimation();
        animb.From = 0;
        animb.To = 20;
        animb.Duration = TimeSpan.FromMilliseconds(1000);
        animb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        DoubleAnimation anims2 = new DoubleAnimation();
        anims2.From = 0;
        anims2.To = -20;
        anims2.Duration = TimeSpan.FromMilliseconds(1000);
        anims2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform transb = new TranslateTransform();
        TranslateTransform transb2 = new TranslateTransform();
        Border2.RenderTransform = transb;
        Border1.RenderTransform = transb2;
        transb.BeginAnimation(TranslateTransform.XProperty, animb);
        transb.BeginAnimation(TranslateTransform.YProperty, anims2);
        transb2.BeginAnimation(TranslateTransform.XProperty, anims2);
        transb2.BeginAnimation(TranslateTransform.YProperty, animb);
    }

    public async Task HideTray()
    {
        Topmost = false;
        await Task.Delay(150);
        Topmost = true;

        DoubleAnimation animb = new DoubleAnimation();
        animb.From = 20;
        animb.To = 0;
        animb.Duration = TimeSpan.FromMilliseconds(600);
        animb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        DoubleAnimation anims2 = new DoubleAnimation();
        anims2.From = -20;
        anims2.To = 0;
        anims2.Duration = TimeSpan.FromMilliseconds(600);
        anims2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform transb = new TranslateTransform();
        TranslateTransform transb2 = new TranslateTransform();
        Border2.RenderTransform = transb;
        Border1.RenderTransform = transb2;
        transb.BeginAnimation(TranslateTransform.XProperty, animb);
        transb.BeginAnimation(TranslateTransform.YProperty, anims2);
        transb2.BeginAnimation(TranslateTransform.XProperty, anims2);
        transb2.BeginAnimation(TranslateTransform.YProperty, animb);

        await Task.Delay(150);

        DoubleAnimation animg = new DoubleAnimation();
        animg.From = 0;
        animg.To = 1000;
        animg.Duration = TimeSpan.FromMilliseconds(300);
        animg.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseIn, Power = 5 };
        TranslateTransform transg = new TranslateTransform();
        TrayGrid.RenderTransform = transg;
        transg.BeginAnimation(TranslateTransform.XProperty, animg);
        transg.BeginAnimation(TranslateTransform.YProperty, animg);

        await Task.Delay(300);

        base.Visibility = Visibility.Collapsed;
        base.IsEnabled = false;
        await StopTimer();
    }

    private void T_Click(object sender, RoutedEventArgs e)
    {
        if (taskpath.Contains("default"))
        {
            System.Diagnostics.Process.Start("taskmgr.exe");
        }
        else if(File.Exists(taskpath))
        {
            Process p = new Process();
            p.StartInfo.FileName = taskpath;
            p.StartInfo.WorkingDirectory = taskpath.Replace(Path.GetFileName(taskpath), "");
            p.Start();
        }
    }

    private void C_Click(object sender, RoutedEventArgs e)
    {
        if(cmdpath.Contains("default"))
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = "C:\\Windows\\System32\\";
            p.Start();
        }
        else if (File.Exists(cmdpath))
        {
            Process p = new Process();
            p.StartInfo.FileName = cmdpath;
            p.StartInfo.WorkingDirectory = cmdpath.Replace(Path.GetFileName(cmdpath), "");
            p.Start();
        }
    }

    private void R_Click(object sender, RoutedEventArgs e)
    {
        if(regpath.Contains("default"))
        {
            System.Diagnostics.Process.Start("regedit.exe");
        }
        else if (File.Exists(regpath))
        {
            Process p = new Process();
            p.StartInfo.FileName = regpath;
            p.StartInfo.WorkingDirectory = regpath.Replace(Path.GetFileName(regpath), "");
            p.Start();
        }
    }

    private void G_Click(object sender, RoutedEventArgs e)
    {
        if (gppath.Contains("default"))
        {
            System.Diagnostics.Process.Start("mmc.exe", "gpedit.msc");
        }
        else if (File.Exists(gppath))
        {
            Process p = new Process();
            p.StartInfo.FileName = gppath;
            p.StartInfo.WorkingDirectory = gppath.Replace(Path.GetFileName(gppath), "");
            p.Start();
        }
    }

    private async void E_Click(object sender, RoutedEventArgs e)
    {
        ToTray = false;
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = -50,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            QL.RenderTransform = transform;

            QL.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }

        await Task.Delay(250);

        QL.Visibility = Visibility.Hidden;
        QLedit.Visibility = Visibility.Visible;

        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            QLedit.RenderTransform = transform;

            QLedit.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }
    }

    private async void E_Back_Click(object sender, RoutedEventArgs e)
    {
        await SetQL();
        ToTray = true;
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 50,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            QLedit.RenderTransform = transform;

            QLedit.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }

        await Task.Delay(250);

        QLedit.Visibility = Visibility.Hidden;
        QL.Visibility = Visibility.Visible;

        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            QL.RenderTransform = transform;

            QL.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }
    }

    private async void QO_Click(object sender, RoutedEventArgs e)
    {
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = -50,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            PerfViewer.RenderTransform = transform;

            PerfViewer.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }

        await Task.Delay(250);

        PerfViewer.Visibility = Visibility.Hidden;
        QuickOption.Visibility = Visibility.Visible;

        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            QuickOption.RenderTransform = transform;

            QuickOption.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }
    }

    private async void PV_Click(object sender, RoutedEventArgs e)
    {
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = -50,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            QuickOption.RenderTransform = transform;

            QuickOption.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }

        await Task.Delay(250);

        QuickOption.Visibility = Visibility.Hidden;
        PerfViewer.Visibility = Visibility.Visible;

        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            PerfViewer.RenderTransform = transform;

            PerfViewer.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }
    }

    private async void E_Back_Click_Default(object sender, MouseButtonEventArgs e)
    {
        await SetQL_Default();
    }

    private void UTbtn_Click(object sender, RoutedEventArgs e)
    {
        string utpath = Process.GetCurrentProcess().MainModule.FileName;
        System.Diagnostics.Process.Start(utpath);
    }

    private void labtaskedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        labtaskedit.Visibility = Visibility.Collapsed;
        boxtaskedit.Visibility = Visibility.Visible;
        boxtaskedit.Text = labtaskedit.Text;
    }

    private void boxtaskedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        labtaskedit.Visibility = Visibility.Visible;
        boxtaskedit.Visibility = Visibility.Collapsed;
        tasklab = boxtaskedit.Text;
        labtaskedit.Text = boxtaskedit.Text;
    }

    private void labcmdedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        labcmdedit.Visibility = Visibility.Collapsed;
        boxcmdedit.Visibility = Visibility.Visible;
        boxcmdedit.Text = labcmdedit.Text;
    }

    private void boxcmdedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        labcmdedit.Visibility = Visibility.Visible;
        boxcmdedit.Visibility = Visibility.Collapsed;
        cmdlab = boxcmdedit.Text;
        labcmdedit.Text = boxcmdedit.Text;
    }

    private void labregedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        labregedit.Visibility = Visibility.Collapsed;
        boxregedit.Visibility = Visibility.Visible;
        boxregedit.Text = labregedit.Text;
    }

    private void boxregedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        labregedit.Visibility = Visibility.Visible;
        boxregedit.Visibility = Visibility.Collapsed;
        reglab = boxregedit.Text;
        labregedit.Text = boxregedit.Text;
    }

    private void labgpedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        labgpedit.Visibility = Visibility.Collapsed;
        boxgpedit.Visibility = Visibility.Visible;
        boxgpedit.Text = labgpedit.Text;
    }

    private void boxgpedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        labgpedit.Visibility = Visibility.Visible;
        boxgpedit.Visibility = Visibility.Collapsed;
        gplab = boxgpedit.Text;
        labgpedit.Text = boxgpedit.Text;
    }

    private void imgtaskedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        imgtaskedit.Source = editimg;
    }

    private void imgtaskedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        imgtaskedit.Source = taskimg;
    }

    private void imgcmdedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        imgcmdedit.Source = editimg;
    }

    private void imgcmdedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        imgcmdedit.Source = cmdimg;
    }

    private void imgregedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        imgregedit.Source = editimg;
    }

    private void imgregedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        imgregedit.Source = regimg;
    }

    private void imggpedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        imggpedit.Source = editimg;
    }

    private void imggpedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        imggpedit.Source = gpimg;
    }

    private void labtaskdescedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (taskpath.Contains("default"))
        {
            labtaskdescedit.Text = "taskmgr.exe" + " " + editpath;
        }
        else if (File.Exists(taskpath))
        {
            labtaskdescedit.Text = Path.GetFileName(taskpath) + " " + editpath;
        }
        else
        {
            labtaskdescedit.Text = Path.GetFileName(taskpath) + " (error)" + " " + editpath;
        }
    }

    private void labtaskdescedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if(taskpath.Contains("default"))
        {
            labtaskdescedit.Text = "taskmgr.exe";
        }
        else if (File.Exists(taskpath))
        {
            labtaskdescedit.Text = Path.GetFileName(taskpath);
        }
        else
        {
            labtaskdescedit.Text = Path.GetFileName(taskpath) + " (error)";
        }
    }

    private void labcmddescedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (cmdpath.Contains("default"))
        {
            labcmddescedit.Text = "cmd.exe" + " " + editpath;
        }
        else if (File.Exists(cmdpath))
        {
            labcmddescedit.Text = Path.GetFileName(cmdpath) + " " + editpath;
        }
        else
        {
            labcmddescedit.Text = Path.GetFileName(cmdpath) + " (error)" + " " + editpath;
        }
    }

    private void labcmddescedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (cmdpath.Contains("default"))
        {
            labcmddescedit.Text = "cmd.exe";
        }
        else if (File.Exists(cmdpath))
        {
            labcmddescedit.Text = Path.GetFileName(cmdpath);
        }
        else
        {
            labcmddescedit.Text = Path.GetFileName(cmdpath) + " (error)";
        }
    }

    private void labregdescedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (regpath.Contains("default"))
        {
            labregdescedit.Text = "regedit.exe" + " " + editpath;
        }
        else if (File.Exists(regpath))
        {
            labregdescedit.Text = Path.GetFileName(regpath) + " " + editpath;
        }
        else
        {
            labregdescedit.Text = Path.GetFileName(regpath) + " (error)" + " " + editpath;
        }
    }

    private void labregdescedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (regpath.Contains("default"))
        {
            labregdescedit.Text = "regedit.exe";
        }
        else if (File.Exists(regpath))
        {
            labregdescedit.Text = Path.GetFileName(regpath);
        }
        else
        {
            labregdescedit.Text = Path.GetFileName(regpath) + " (error)";
        }
    }

    private void labgpdescedit_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (gppath.Contains("default"))
        {
            labgpdescedit.Text = "gpedit.msc" + " " + editpath;
        }
        else if (File.Exists(gppath))
        {
            labgpdescedit.Text = Path.GetFileName(gppath) + " " + editpath;
        }
        else
        {
            labgpdescedit.Text = Path.GetFileName(gppath) + " (error)" + " " + editpath;
        }
    }

    private void labgpdescedit_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (gppath.Contains("default"))
        {
            labgpdescedit.Text = "gpedit.msc";
        }
        else if (File.Exists(gppath))
        {
            labgpdescedit.Text = Path.GetFileName(gppath);
        }
        else
        {
            labgpdescedit.Text = Path.GetFileName(gppath) + " (error)";
        }
    }

    private void imgtaskedit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "ico";
            fb.Filter = "Icon file (*.ico)|*.ico|Executable file (*.exe)|*.exe";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                taskicon = fb.FileName;
                taskimg = GetImgSourceFromPath(taskicon);
                imgtaskedit.Source = taskimg;
            }
        }
    }

    private void imgcmdedit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "ico";
            fb.Filter = "Icon file (*.ico)|*.ico|Executable file (*.exe)|*.exe";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                cmdicon = fb.FileName;
                cmdimg = GetImgSourceFromPath(cmdicon);
                imgcmdedit.Source = cmdimg;
            }
        }
    }

    private void imgregedit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "ico";
            fb.Filter = "Icon file (*.ico)|*.ico|Executable file (*.exe)|*.exe";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                regicon = fb.FileName;
                regimg = GetImgSourceFromPath(regicon);
                imgregedit.Source = regimg;
            }
        }
    }

    private void imggpedit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "ico";
            fb.Filter = "Icon file (*.ico)|*.ico|Executable file (*.exe)|*.exe";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                gpicon = fb.FileName;
                gpimg = GetImgSourceFromPath(gpicon);
                imggpedit.Source = gpimg;
            }
        }
    }

    private void labtaskdescedit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "exe";
            fb.Filter = "Executable file (*.exe)|*.exe";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                taskicon = fb.FileName;
                taskimg = GetImgSourceFromPath(taskicon);
                imgtaskedit.Source = taskimg;
                taskpath = fb.FileName;
                labtaskdescedit.Text = Path.GetFileName(taskpath);
                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(fb.FileName);
                tasklab = fileInfo.FileDescription;
                labtaskedit.Text = tasklab;
            }
        }
    }

    private void labcmddescedit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "exe";
            fb.Filter = "Executable file (*.exe)|*.exe";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                cmdpath = fb.FileName;
                labcmddescedit.Text = Path.GetFileName(cmdpath);
                cmdicon = fb.FileName;
                cmdimg = GetImgSourceFromPath(cmdicon);
                imgcmdedit.Source = cmdimg;
                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(fb.FileName);
                cmdlab = fileInfo.FileDescription;
                labcmdedit.Text = cmdlab;
            }
        }
    }

    private void labregdescedit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "exe";
            fb.Filter = "Executable file (*.exe)|*.exe";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                regpath = fb.FileName;
                labregdescedit.Text = Path.GetFileName(regpath);
                regicon = fb.FileName;
                regimg = GetImgSourceFromPath(regicon);
                imgregedit.Source = regimg;
                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(fb.FileName);
                reglab = fileInfo.FileDescription;
                labregedit.Text = reglab;
            }
        }
    }

    private void labgpdescedit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "exe";
            fb.Filter = "Executable file (*.exe)|*.exe";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                gppath = fb.FileName;
                labgpdescedit.Text = Path.GetFileName(gppath);
                gpicon = fb.FileName;
                gpimg = GetImgSourceFromPath(gpicon);
                imggpedit.Source = gpimg;
                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(fb.FileName);
                gplab = fileInfo.FileDescription;
                labgpedit.Text = gplab;
            }
        }
    }

    private async void pmodebox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (QuickOption.Visibility == Visibility.Visible)
        {
            Guid newmode = new(balanced);

            if (pmodeperf.IsSelected)
            {
                newmode = new(perf);
            }
            else if(pmodebalanced.IsSelected)
            {
                newmode = new(balanced);
            }
            else if(pmodeefficiency.IsSelected)
            {
                newmode = new(efficiency);
            }

            PowerSetActiveOverlayScheme(newmode);
        }
    }
}

