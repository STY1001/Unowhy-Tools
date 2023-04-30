using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Controls.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Mvvm.Contracts;
using Unowhy_Tools;
using Unowhy_Tools_WPF.Views.Pages;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Wpf.Ui.Controls;
using System.Management;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Linq;

namespace Unowhy_Tools_WPF.Views;

public partial class TrayWindow : Window
{
    private DispatcherTimer _timer;

    public async Task StartTimer()
    {
        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromSeconds(0.5);
        _timer.Tick += async (sender, e) => await CheckCPU();
        _timer.Start();
    }


    public async Task CheckCPU()
    {
        var cpuCounter1 = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
        cpuCounter1.NextValue();
        await Task.Delay(100);
        var cpurint = Convert.ToInt32(cpuCounter1.NextValue());
        if(cpurint > 100) cpurint = 100;
        var cpupstring = $"{cpurint.ToString("0")} %";
        var cpucapp1 = new PerformanceCounter("Processor Information", "Processor Frequency", "_Total");
        var cpucapp2 = new PerformanceCounter("Processor Information", "% Processor Performance", "_Total");
        cpucapp1.NextValue();
        await Task.Delay(100);
        var cpucstring = $"{((cpucapp1.NextValue() / 1000.0) * (cpucapp2.NextValue() / 100.0)).ToString("0.00")} GHz / {(cpucapp1.NextValue() / 1000.0).ToString("0.00")} GHz";
        Debug.WriteLine(cpurint + "1");
        Debug.WriteLine(cpucapp2.NextValue()+ "2");
        Debug.WriteLine(cpucapp1.NextValue() + "3");

        var ramCounter1 = new PerformanceCounter("Memory", "Available Bytes");
        var ramCounter2 = new PerformanceCounter("Memory", "Available MBytes");
        var ramAvail = ramCounter2.NextValue();
        var ramAvail2 = ramCounter1.NextValue();
        var ramint = (((new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory) - ramAvail2) / new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory) * 100;
        var rampstring = $"{ramint.ToString("0")} %";
        var ramcstring = $"{((new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024.0 / 1024.0 / 1024.0) - (ramAvail / 1024.0)).ToString("0.00")} Go / {(new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024.0 / 1024.0 / 1024.0).ToString("0.00")} Go";
        
        var driveInfos = DriveInfo.GetDrives();
        var totalFreeSpace = driveInfos.Sum(d => d.TotalFreeSpace);
        var totalSize = driveInfos.Sum(d => d.TotalSize);
        var storint = (int)(((double)(totalSize - totalFreeSpace) / (double)totalSize) * 100);
        var storpstring = $"{storint }%";
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

    public void applylang()
    {

    }

    public TrayWindow()
    {
        InitializeComponent();
        StartTimer();
        base.Deactivated += TrayWindow_Deactivated;
        var trayIcon = new System.Windows.Forms.NotifyIcon();
        trayIcon.Icon = UT.GetIconFromRes("UT.png");
        trayIcon.Visible = true;
        trayIcon.Click += TrayIcon_Click;
        ShowTray();
    }

    private async void TrayWindow_Deactivated(object sender, EventArgs e)
    {
        await HideTray();
    }

    private async void TrayIcon_Click(object? sender, EventArgs e)
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
        Topmost = true;
        Activate();

        var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
        Left = desktopWorkingArea.Right - Width;
        Top = desktopWorkingArea.Bottom - Height;

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

        base.Visibility = Visibility.Hidden;
    }
}

