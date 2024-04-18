using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Security.RightsManagement;
using System.Threading;
using System.Windows.Documents;
using System.Diagnostics;
using System.IO;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Customize : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void GoForw(object sender, RoutedEventArgs e)
    {

    }

    public async void AdminSet_Click(object sender, RoutedEventArgs e)
    {
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = 300;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        auset_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        UT.anim.RegisterParent(RootGrid, RootBorder);
        UT.anim.AnimParent("zoomout2");
        await Task.Delay(500);
        UT.NavigateTo(typeof(AdminUser));

        anim.From = 0;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        auset_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
    }

    public async void AddUser_Click(object sender, RoutedEventArgs e)
    {
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = 300;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        adduser_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        UT.anim.RegisterParent(RootGrid, RootBorder);
        UT.anim.AnimParent("zoomout2");
        await Task.Delay(500);
        UT.NavigateTo(typeof(AddUser));

        anim.From = 0;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        adduser_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
    }

    public async void PCname_Click(object sender, RoutedEventArgs e)
    {
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = 300;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        pcname_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        UT.anim.RegisterParent(RootGrid, RootBorder);
        UT.anim.AnimParent("zoomout2");
        await Task.Delay(500);
        UT.NavigateTo(typeof(PCname));

        anim.From = 0;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        pcname_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
    }

    public async void HackBGRTSet_Click(object sender, RoutedEventArgs e)
    {
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = 300;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        hackbgrtset_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        UT.anim.RegisterParent(RootGrid, RootBorder);
        UT.anim.AnimParent("zoomout2");
        await Task.Delay(500);
        UT.NavigateTo(typeof(HackBGRT));

        anim.From = 0;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        hackbgrtset_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
    }

    public async Task applylang()
    {
        pcname_txt.Text = await UT.GetLang("pcname");
        pcname_desc.Text = await UT.GetLang("descpcname");
        pcname_btn.Content = await UT.GetLang("change");
        adminset_txt.Text = await UT.GetLang("admin");
        adminset_desc.Text = await UT.GetLang("descadmin");
        adminset_btn.Content = await UT.GetLang("set");
        adduser_txt.Text = await UT.GetLang("adduser");
        adduser_desc.Text = await UT.GetLang("descadduser");
        adduser_btn.Content = await UT.GetLang("create");
        auset_txt.Text = await UT.GetLang("adminset");
        auset_desc.Text = await UT.GetLang("descadminset");
        auset_btn.Content = await UT.GetLang("open");
        camoverena_txt.Text = await UT.GetLang("camoverena");
        camoverena_desc.Text = await UT.GetLang("desccamoverena");
        camoverena_btn.Content = await UT.GetLang("enable");
        verbstatena_txt.Text = await UT.GetLang("verbstatena");
        verbstatena_desc.Text = await UT.GetLang("descverbstatena");
        verbstatena_btn.Content = await UT.GetLang("enable");
        edgeset_txt.Text = await UT.GetLang("edgeset");
        edgeset_desc.Text = await UT.GetLang("descedgeset");
        edgedel_btn.Content = await UT.GetLang("uninstall");
        edgereg_btn.Content = await UT.GetLang("block");
        windefset_txt.Text = await UT.GetLang("windefset");
        windefset_desc.Text = await UT.GetLang("descwindefset");
        windefdis_btn.Content = await UT.GetLang("disable");
        windefena_btn.Content = await UT.GetLang("enable");
        hackbgrtset_txt.Text = await UT.GetLang("hackbgrtset");
        hackbgrtset_desc.Text = await UT.GetLang("deschackbgrtset");
        hackbgrtset_btn.Content = await UT.GetLang("open");
    }

    public async Task CheckBTN(bool check, string step)
    {
        if (check)
        {
            await UT.Check(step);
        }
        if (UTdata.Admin == true) adminset.IsEnabled = false;
        else adminset.IsEnabled = true;
        if (UTdata.CamOver == true) camoverena.IsEnabled = false;
        else camoverena.IsEnabled = true;
        if (UTdata.VerbStat == true) verbstatena.IsEnabled = false;
        else verbstatena.IsEnabled = true;
        if (UTdata.EdgeInstalled == true) edgedel_btn.IsEnabled = true;
        else edgedel_btn.IsEnabled = false;
        if (UTdata.NoEdgeReg == true) edgereg_btn.IsEnabled = false;
        else edgereg_btn.IsEnabled = true;
        if (UTdata.WinDefEnabled)
        {
            windefena_btn.IsEnabled = false;
            windefdis_btn.IsEnabled = true;
        }
        else
        {
            windefena_btn.IsEnabled = true;
            windefdis_btn.IsEnabled = false;
        }
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN(false, "none");
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.UnDeployBack();

        await CheckBTN(false, "none");

        foreach (UIElement element in RootStack.Children)
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
                From = 30,
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

    public Customize(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public async void adminset_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("admin"), "admin.png"))
        {
            UT.SendAction("SetAsAdmin");
            await UT.waitstatus.open(await UT.GetLang("wait.set"), "admin.png");
            await UT.RunMin("net", $"localgroup {UTdata.AdminsName} /add \"{UTdata.User}\"");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "admins");
            await UT.waitstatus.close();
            if (!adminset.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void camover_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("camoverena"), "camera.png"))
        {
            UT.SendAction("EnableCamOverlay");
            await UT.waitstatus.open(await UT.GetLang("wait.enable"), "camera.png");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Microsoft\\OEM\\Device\\Capture\" /v \"NoPhysicalCameraLED\" /t REG_DWORD /d \"1\" /f");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "cpo");
            await UT.waitstatus.close();
            if (!camoverena.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void verbstatena_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("verbstatena"), "verbose.png"))
        {
            UT.SendAction("EnableVerbose");
            await UT.waitstatus.open(await UT.GetLang("wait.enable"), "verbose.png");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v \"VerboseStatus\" /t REG_DWORD /d \"1\" /f");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "verbose");
            await UT.waitstatus.close();
            if (!verbstatena.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void edgedel_Click(object sender, RoutedEventArgs e)
    {
        if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"))
        {
            UT.DialogIShow(await UT.GetLang("needres"), "download.png");
        }

        if (UT.CheckInternet() || File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"))
        {
            if (UT.DialogQShow(await UT.GetLang("edgeun"), "uninstall.png"))
            {
                UT.SendAction("UninstallEdge");
                await UT.waitstatus.open(await UT.GetLang("wait.uninstall"), "uninstall.png");
                if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"))
                {
                    var progress = new System.Progress<double>();
                    var cancellationToken = new CancellationTokenSource();
                    string dl = await UT.GetLang("wait.download");
                    progress.ProgressChanged += async (sender, value) =>
                    {
                        await UT.waitstatus.open(dl + " (" + value.ToString("###.#") + "%)", "clouddl.png");
                    };
                    await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("edgesetup"), UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe", progress, cancellationToken.Token);
                }
                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.uninstall"), "uninstall.png");
                    await UT.RunMin("powershell", $"start-process -FilePath '{UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"}' -ArgumentList '--uninstall --system-level --force-uninstall' -nonewwindow -wait");
                }
                await Task.Delay(1000);
                await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
                await CheckBTN(true, "edge");
                await UT.waitstatus.close();
                if (!edgedel_btn.IsEnabled)
                {
                    UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                }
                else
                {
                    UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                }
            }
        }
        else
        {
            UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
        }
    }

    public async void edgereg_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("edgeblock"), "block.png"))
        {
            UT.SendAction("BlockEdge");
            await UT.waitstatus.open(await UT.GetLang("wait.block"), "block.png");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Microsoft\\EdgeUpdate\" /v \"DoNotUpdateToEdgeWithChromium\" /t REG_DWORD /d \"1\" /f");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "edge");
            await UT.waitstatus.close();
            if (!edgereg_btn.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void windefena_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("enable"), "enable.png"))
        {
            UT.SendAction("EnableWinDef");
            await UT.waitstatus.open(await UT.GetLang("wait.enable"), "enable.png");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v \"DisableAntiSpyware\" /t REG_DWORD /d \"0\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v \"DisableRealtimeMonitoring\" /t REG_DWORD /d \"0\" /f");
            await UT.RunMin("powershell", "Set-MpPreference -DisableRealtimeMonitoring $false");
            await UT.UTS.UTSmsg("UTSWD", "SetWDS:False");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "windef");
            await UT.waitstatus.close();
            if (!windefena_btn.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }
    public async void windefdis_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("disable"), "disable.png"))
        {
            UT.SendAction("DisableWinDef");
            await UT.waitstatus.open(await UT.GetLang("wait.disable"), "disable.png");
            await UT.RunMin("powershell", "Set-MpPreference -DisableRealtimeMonitoring $true");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v \"DisableAntiSpyware\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v \"DisableRealtimeMonitoring\" /t REG_DWORD /d \"1\" /f");
            await UT.UTS.UTSmsg("UTSWD", "SetWDS:True");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "windef");
            await UT.waitstatus.close();
            UT.DialogIShow(await UT.GetLang("windefsettamper"), "windef.png");
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "windowsdefender://threatsettings",
                UseShellExecute = true
            });
            if (!windefdis_btn.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }
}
