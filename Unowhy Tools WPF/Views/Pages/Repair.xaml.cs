using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Repair : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void GoForw(object sender, RoutedEventArgs e)
    {
        UT.anim.TransitionForw(RootGrid);
    }

    public async void winre_Click(object sender, RoutedEventArgs e)
    {
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = 300;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        wre_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        await Task.Delay(200);
        UT.anim.TransitionForw(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(WinRE));

        anim.From = 0;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        wre_btn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
    }

    public void applylang()
    {
        shell_txt.Text = UT.GetLang("shell");
        shell_desc.Text = UT.GetLang("descshell");
        shell_btn.Content = UT.GetLang("change");
        tis_txt.Text = UT.GetLang("fixboot");
        tis_desc.Text = UT.GetLang("descstart");
        tis_btn.Content = UT.GetLang("del");
        wre_txt.Text = UT.GetLang("winre");
        wre_desc.Text = UT.GetLang("descwinre");
        wre_btn.Content = UT.GetLang("open");
        bim_txt.Text = UT.GetLang("bootim");
        bim_desc.Text = UT.GetLang("descbootim");
        bim_btn.Content = UT.GetLang("repair");
        whe_txt.Text = UT.GetLang("enwhe");
        whe_desc.Text = UT.GetLang("descenwhe");
        whe_btn.Content = UT.GetLang("enable");
        iaf_txt.Text = UT.GetLang("bcdfail");
        iaf_desc.Text = UT.GetLang("descbcdfail");
        iaf_btn.Content = UT.GetLang("del");
        tmgr_txt.Text = UT.GetLang("taskmgr");
        tmgr_desc.Text = UT.GetLang("desctaskmgr");
        tmgr_btn.Content = UT.GetLang("enable");
        locka_txt.Text = UT.GetLang("locka");
        locka_desc.Text = UT.GetLang("desclocka");
        locka_btn.Content = UT.GetLang("enable");
    }

    public async Task CheckBTN(bool check)
    {
        if (check)
        {
            await UT.Check();
        }
        shell.IsEnabled = true;
        tis.IsEnabled = true;
        bim.IsEnabled = true;
        whe.IsEnabled = true;
        iaf.IsEnabled = true;
        if (UTdata.WHE) whe.IsEnabled = false;
        else whe.IsEnabled = true;
        if(UTdata.BIM) bim.IsEnabled = false;
        else bim.IsEnabled = true;
        if(UTdata.BCD) iaf.IsEnabled = false;
        else iaf.IsEnabled = true;
        if(UTdata.ShellOK) shell.IsEnabled = false;
        else shell.IsEnabled = true;
        if(UTdata.TIStartup) tis.IsEnabled = true;
        else tis.IsEnabled = false;
        if (UTdata.TaskMGR) tmgr.IsEnabled = false;
        else tmgr.IsEnabled = true;
        if (UTdata.LockA) locka.IsEnabled = false;
        else locka.IsEnabled = true;
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN(false);
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await CheckBTN(false);

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
                From = -50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;

            element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);

            await Task.Delay(50);
        }
    }

    public Repair(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public async void shell_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("shell"), "explorer.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("reg", "add \"HKLM\\Software\\Microsoft\\Windows NT\\CurrentVersion\\WinLogon\" /v Shell /d explorer.exe /t REG_SZ /f");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!shell.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void tis_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("fixboot"), "script.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("cmd", "/w /c del /q /f \"c:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\silent_*.*\"");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!tis.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void bim_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("bootim"), "registry.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("reg", "delete \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\bootim.exe\" /f");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!bim.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void whe_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("enwhe"), "fp.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\" /v \"EnablePinRecovery\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\" /v \"RequireSecurityDevice\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\" /v \"Enabled\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\" /v \"DisablePostLogonProvisioning\" /t REG_DWORD /d \"0\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\\DynamicLock\" /v \"DynamicLock\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\\DynamicLock\" /v \"Plugins\" /t REG_SZ /d \"<rule schemaVersion=\\\"1.0\\\"> <signal type=\\\"bluetooth\\\" scenario=\\\"Dynamic Lock\\\" classOfDevice=\\\"512\\\" rssiMin=\\\"-10\\\" rssiMaxDelta=\\\"-10\\\"/> </rule> \" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WinBio\\Credential Provider\" /v \"Domain Accounts\" /t REG_DWORD /d \"1\" /f");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!whe.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void iaf_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("bcdfail"), "boot.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("bcdedit", "/deletevalue bootstatuspolicy");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!iaf.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void tmgr_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("taskmgr"), "taskmgr.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("reg", "delete \"HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DisableTaskMgr /f");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!tmgr.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }
    
    public async void locka_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("locka"), "key.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("reg", "delete \"HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DisableLockWorkstation /f");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!tmgr.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }
}
