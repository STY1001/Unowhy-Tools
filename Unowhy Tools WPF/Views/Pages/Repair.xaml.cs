using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Net;
using System.Threading;

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
        //UT.anim.TransitionForw(RootGrid);
    }

    public async void winreena_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("winre.ena"), "enable.png"))
        {
            UT.SendAction("EnableWinRE");
            await UT.waitstatus.open(await UT.GetLang("wait.enable"), "enable.png");
            UTdata.WinRE = true;
            await UT.RunMin("reagentc.exe", "/enable");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "winre");
            await UT.waitstatus.close();
            if (!wreena_btn.IsEnabled & !wrerep_btn.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("winremsg"), "no.png");
                wrerep_btn.IsEnabled = true;
                wreena_btn.IsEnabled = false;
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void winredis_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("winre.dis"), "disable.png"))
        {
            UT.SendAction("DisableWinRE");
            await UT.waitstatus.open(await UT.GetLang("wait.disable"), "disable.png");
            UTdata.WinRE = false;
            await UT.RunMin("reagentc.exe", "/disable");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "winre");
            await UT.waitstatus.close();
            if (!wredis_btn.IsEnabled & !wrerep_btn.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }
    public async void winrerep_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("RepairWinRE");
        if (await UT.CheckInternet())
        {
            await UT.waitstatus.open(await UT.GetLang("wait.repair"), "repair.png");
            await Task.Delay(1000);
            var progress = new System.Progress<double>();
            string dl = await UT.GetLang("wait.download");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "clouddl.png");
            };
            var cancellationToken = new CancellationTokenSource();
            await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("winre"), "C:\\Windows\\System32\\Recovery\\WinRE.wim", progress, cancellationToken.Token);

            await UT.waitstatus.open(await UT.GetLang("wait.enable"), "enable.png");

            await UT.RunMin("reagentc.exe", "/setreimage /path C:\\Windows\\System32\\Recovery");
            await UT.RunMin("reagentc.exe", "/enable");

            UTdata.WinRE = true;

            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "winre");
            await UT.waitstatus.close();
            if (wredis_btn.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
        else
        {
            UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
        }
    }

    public async Task applylang()
    {
        shell_txt.Text = await UT.GetLang("shell");
        shell_desc.Text = await UT.GetLang("descshell");
        shell_btn.Content = await UT.GetLang("change");
        tis_txt.Text = await UT.GetLang("fixboot");
        tis_desc.Text = await UT.GetLang("descstart");
        tis_btn.Content = await UT.GetLang("del");
        wre_txt.Text = await UT.GetLang("winre");
        wre_desc.Text = await UT.GetLang("descwinre");
        wreena_btn.Content = await UT.GetLang("enable");
        wredis_btn.Content = await UT.GetLang("disable");
        wrerep_btn.Content = await UT.GetLang("repair");
        bim_txt.Text = await UT.GetLang("bootim");
        bim_desc.Text = await UT.GetLang("descbootim");
        bim_btn.Content = await UT.GetLang("repair");
        whe_txt.Text = await UT.GetLang("enwhe");
        whe_desc.Text = await UT.GetLang("descenwhe");
        whe_btn.Content = await UT.GetLang("enable");
        iaf_txt.Text = await UT.GetLang("bcdfail");
        iaf_desc.Text = await UT.GetLang("descbcdfail");
        iaf_btn.Content = await UT.GetLang("del");
        tmgr_txt.Text = await UT.GetLang("taskmgr");
        tmgr_desc.Text = await UT.GetLang("desctaskmgr");
        tmgr_btn.Content = await UT.GetLang("enable");
        locka_txt.Text = await UT.GetLang("locka");
        locka_desc.Text = await UT.GetLang("desclocka");
        locka_btn.Content = await UT.GetLang("enable");
    }

    public async Task CheckBTN(bool check, string step)
    {
        if (check)
        {
            await UT.Check(step);
        }
        shell.IsEnabled = true;
        tis.IsEnabled = true;
        bim.IsEnabled = true;
        whe.IsEnabled = true;
        iaf.IsEnabled = true;
        wreena_btn.IsEnabled = false;
        wredis_btn.IsEnabled = false;
        wrerep_btn.IsEnabled = false;
        if (UTdata.WHE) whe.IsEnabled = false;
        else whe.IsEnabled = true;
        if (UTdata.BIM) bim.IsEnabled = false;
        else bim.IsEnabled = true;
        if (UTdata.BCD) iaf.IsEnabled = false;
        else iaf.IsEnabled = true;
        if (UTdata.ShellOK) shell.IsEnabled = false;
        else shell.IsEnabled = true;
        if (UTdata.TIStartup) tis.IsEnabled = true;
        else tis.IsEnabled = false;
        if (UTdata.TaskMGR) tmgr.IsEnabled = false;
        else tmgr.IsEnabled = true;
        if (UTdata.LockA) locka.IsEnabled = false;
        else locka.IsEnabled = true;
        if (UTdata.WinRE) wredis_btn.IsEnabled = true;
        else wreena_btn.IsEnabled = true;
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

    public Repair(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public async void shell_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("shell"), "explorer.png"))
        {
            UT.SendAction("RestoreShell");
            await UT.waitstatus.open(await UT.GetLang("wait.apply"), "explorer.png");
            await UT.RunMin("reg", "add \"HKLM\\Software\\Microsoft\\Windows NT\\CurrentVersion\\WinLogon\" /v Shell /d explorer.exe /t REG_SZ /f");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "shell");
            await UT.waitstatus.close();
            if (!shell.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void tis_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("fixboot"), "script.png"))
        {
            UT.SendAction("FixToInstall");
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "script.png");
            await UT.RunMin("cmd", "/w /c del /q /f \"c:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\silent_*.*\"");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "tistart");
            await UT.waitstatus.close();
            if (!tis.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void bim_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("bootim"), "registry.png"))
        {
            UT.SendAction("EnableBootIM");
            await UT.waitstatus.open(await UT.GetLang("wait.repair"), "registry.png");
            await UT.RunMin("reg", "delete \"HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Image File Execution Options\\bootim.exe\" /f");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "bootim");
            await UT.waitstatus.close();
            if (!bim.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void whe_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("enwhe"), "fp.png"))
        {
            UT.SendAction("EnableWinHelloEnterprise");
            await UT.waitstatus.open(await UT.GetLang("wait.enable"), "fp.png");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\" /v \"EnablePinRecovery\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\" /v \"RequireSecurityDevice\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\" /v \"Enabled\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\" /v \"DisablePostLogonProvisioning\" /t REG_DWORD /d \"0\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\\DynamicLock\" /v \"DynamicLock\" /t REG_DWORD /d \"1\" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\PassportForWork\\DynamicLock\" /v \"Plugins\" /t REG_SZ /d \"<rule schemaVersion=\\\"1.0\\\"> <signal type=\\\"bluetooth\\\" scenario=\\\"Dynamic Lock\\\" classOfDevice=\\\"512\\\" rssiMin=\\\"-10\\\" rssiMaxDelta=\\\"-10\\\"/> </rule> \" /f");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WinBio\\Credential Provider\" /v \"Domain Accounts\" /t REG_DWORD /d \"1\" /f");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "whe");
            await UT.waitstatus.close();
            if (!whe.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void iaf_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("bcdfail"), "boot.png"))
        {
            UT.SendAction("DisableIgnoreAllFailure");
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "boot.png");
            await UT.RunMin("bcdedit", "/deletevalue bootstatuspolicy");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "bcdedit");
            await UT.waitstatus.close();
            if (!iaf.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void tmgr_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("taskmgr"), "taskmgr.png"))
        {
            UT.SendAction("EnableTaskmgr");
            await UT.waitstatus.open(await UT.GetLang("wait.enable"), "taskmgr.png");
            await UT.RunMin("reg", "delete \"HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DisableTaskMgr /f");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "taskmgr");
            await UT.waitstatus.close();
            if (!tmgr.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void locka_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("locka"), "key.png"))
        {
            UT.SendAction("EnableLockOut");
            await UT.waitstatus.open(await UT.GetLang("wait.enable"), "key.png");
            await UT.RunMin("reg", "delete \"HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v DisableLockWorkstation /f");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "lockout");
            await UT.waitstatus.close();
            if (!locka.IsEnabled)
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
