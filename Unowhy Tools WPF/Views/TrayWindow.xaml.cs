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

namespace Unowhy_Tools_WPF.Views;

public partial class TrayWindow : Window
{
    private System.Windows.Forms.NotifyIcon trayIcon = new System.Windows.Forms.NotifyIcon();

    private bool CamOn;
    private bool MicOn;

    private RegistryKey _camerakey;
    private RegistryKey _microkey;

    private bool IsPause = false;

    private DispatcherTimer _timerStats;
    private DispatcherTimer _timerPriv;
    private DispatcherTimer _timerTimeDate;

    public async Task InitTimer()
    {
        _timerStats = new DispatcherTimer();
        _timerStats.Interval = TimeSpan.FromSeconds(1);
        _timerStats.Tick += async (sender, e) => await CheckStats();
        _timerStats.Start();

        _timerPriv = new DispatcherTimer();
        _timerPriv.Interval = TimeSpan.FromSeconds(5);
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

    private PerformanceCounter cpuCounter1;
    private PerformanceCounter cpucapp1;
    private PerformanceCounter cpucapp2;
    private PerformanceCounter ramCounter1;
    private PerformanceCounter ramCounter2;
    private ulong totalram = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
    private DriveInfo driveInfos = new DriveInfo("C");

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

        if(micswitch.IsChecked == true)
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
            string cam = _camerakey.GetValue("Value").ToString();
            string mic = _microkey.GetValue("Value").ToString();

            if (cam == "Allow")
            {
                CamOn = true;
            }
            else if (cam == "Deny")
            {
                CamOn = false;
            }

            if (mic == "Allow")
            {
                MicOn = true;
            }
            else if (mic == "Deny")
            {
                MicOn = false;
            }

            if (CamOn)
            {
                camswitch.IsChecked = true;
            }
            else
            {
                camswitch.IsChecked = false;
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
    }

    public void applylang()
    {
        cpulab.Text = UT.GetLang("usecpu");
        ramlab.Text = UT.GetLang("useram");
        storlab.Text = UT.GetLang("usestor");
        labout.Text = UT.GetLang("openut");
        labtask.Text = UT.GetLang("opentask");
        labcmd.Text = UT.GetLang("opencmd");
        labreg.Text = UT.GetLang("openreg");
        labgp.Text = UT.GetLang("opengp");
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

    private async void Window_Initialized(object sender, EventArgs e)
    {
        await Task.Delay(1000);
        await ShowTray();
        await WaitControl.Show(UT.GetLang("wait.check"), "check.png");
        await Task.Delay(500);

        _camerakey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\webcam", true);
        _microkey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\microphone", true);

        string utpath = Process.GetCurrentProcess().MainModule.FileName;
        UTbtndesc.Text = utpath;

        applylang();
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
            RegistryKey lcs = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utcuab = lcs.GetValue("UpdateStart").ToString();
            if (utcuab == "1")
            {
                if (await UT.version.newver())
                {
                    var web = new HttpClient();
                    string newver = await web.GetStringAsync("https://bit.ly/UTnvTXT");
                    newver = newver.Insert(2, ".");
                    newver = newver.Replace("\n", "");
                    string newverfull = UT.GetLang("newver") + " (" + UT.version.getverfull().ToString().Insert(2, ".") + " -> " + newver + ")";
                    trayIcon.ShowBalloonTip(3000, "Unowhy Tools Updater", newverfull, ToolTipIcon.Info);
                }
            }

            await UT.SendStats("tray");
        }
    }

    public async void TrayWindow_Deactivated(object sender, EventArgs e)
    {
        await HideTray();
    }

    public async void TrayIcon_Click(object sender, MouseEventArgs e)
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
        System.Diagnostics.Process.Start("taskmgr.exe");
    }

    private void C_Click(object sender, RoutedEventArgs e)
    {
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.WorkingDirectory = "C:\\Windows\\System32\\";
        p.Start();
    }

    private void R_Click(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start("regedit.exe");
    }

    private void G_Click(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start("mmc.exe", "gpedit.msc");
    }

    private void UTbtn_Click(object sender, RoutedEventArgs e)
    {
        string utpath = Process.GetCurrentProcess().MainModule.FileName;
        System.Diagnostics.Process.Start(utpath);
    }
}

