using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using System.Windows.Input;
using System.Diagnostics;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class WinDef : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();
    public string edgever;

    Color disabled = (Color)ColorConverter.ConvertFromString("#888888");
    Color enabled = (Color)ColorConverter.ConvertFromString("#FFFFFF");

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        //UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        //UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(Customize));
    }

    public void applylang()
    {
        disable_txt.Text = UT.GetLang("disable");
        enable_txt.Text = UT.GetLang("enable");

    }

    public async Task CheckBTN()
    {
        enable.IsEnabled = true;
        enable_txt.Foreground = new SolidColorBrush(enabled);
        disable.IsEnabled = true;
        disable_txt.Foreground = new SolidColorBrush(enabled);

        RegistryKey wd = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender");
        RegistryKey rtp = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection");
        if (wd != null && rtp != null)
        {
            object das = wd.GetValue("DisableAntiSpyware", null);
            object drm = rtp.GetValue("DisableRealtimeMonitoring", null);
            if (das != null && drm != null)
            {
                int das2 = (int)wd.GetValue("DisableAntiSpyware", 0);
                int drm2 = (int)rtp.GetValue("DisableRealtimeMonitoring", 0);
                if (das2 == 1 && drm2 == 1 && (await UT.UTS.UTSmsg("UTSWD", "GetWDS")).Contains("True"))
                {
                    disable.IsEnabled = false;
                    disable_txt.Foreground = new SolidColorBrush(disabled);
                    UT.Write2Log("WinDef Disabled");
                }
                else
                {
                    enable.IsEnabled = false;
                    enable_txt.Foreground = new SolidColorBrush(disabled);
                    UT.Write2Log("WinDef Enabled (Not 1)");
                }
            }
            else
            {
                enable.IsEnabled = false;
                enable_txt.Foreground = new SolidColorBrush(disabled);
                UT.Write2Log("WinDef Enabled (Not Value)");
            }
        }
        else
        {
            enable.IsEnabled = false;
            enable_txt.Foreground = new SolidColorBrush(disabled);
            UT.Write2Log("WinDef Enabled (Not Key)");
        }

    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in btngrid.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(Customize), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);

        foreach (UIElement element in btngrid.Children)
        {
            element.Visibility = Visibility.Visible;
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 10,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;

            element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);

            await Task.Delay(50);
        }
    }

    public async void Enable_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("enable"), "enable.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v \"DisableAntiSpyware\" /t REG_DWORD /d \"0\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v \"DisableRealtimeMonitoring\" /t REG_DWORD /d \"0\" /f");
            await UT.RunMin("powershell", "Set-MpPreference -DisableRealtimeMonitoring $false");
            await UT.UTS.UTSmsg("UTSWD", "SetWDS:False");
            await CheckBTN();
            await UT.waitstatus.close();
            if (!enable.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void Disable_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("disable"), "disable.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("powershell", "Set-MpPreference -DisableRealtimeMonitoring $true");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v \"DisableAntiSpyware\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v \"DisableRealtimeMonitoring\" /t REG_DWORD /d \"1\" /f");
            await UT.UTS.UTSmsg("UTSWD", "SetWDS:True");
            await CheckBTN();
            await UT.waitstatus.close();
            UT.DialogIShow(UT.GetLang("windefsettamper"), "windef.png");
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "windowsdefender://threatsettings",
                UseShellExecute = true
            });
            if (!disable.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN();
    }

    public WinDef(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
