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
using Newtonsoft.Json.Linq;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Extra : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void GoForw(object sender, RoutedEventArgs e)
    {

    }

    public async Task applylang()
    {
        installhsqm_txt.Text = await UT.GetLang("installhsm");
        installhsmq_desc.Text = await UT.GetLang("deschism");
        installridfcert_txt.Text = await UT.GetLang("installridfcert");
        installridfcert_desc.Text = await UT.GetLang("descinstridfcert");
        openrufus_txt.Text = await UT.GetLang("openrufus");
        openrufus_desc.Text = await UT.GetLang("descrufus");
        openmas_txt.Text = await UT.GetLang("openmas");
        openmas_desc.Text = await UT.GetLang("descmas");
        usefulcat.Text = await UT.GetLang("usefulcat");
        unowhyprodcat.Text = await UT.GetLang("unoprodcat");
        openmas_btn.Content = await UT.GetLang("open");
        openrufus_btn.Content = await UT.GetLang("open");
        installridfcert_btn.Content = await UT.GetLang("install");
        installhsqm_btn.Content = await UT.GetLang("install");
    }

    public async Task CheckBTN(bool check, string step)
    {
        if (check)
        {
            await UT.Check(step);
        }
        if (UTdata.HSMExist) installhsqm.IsEnabled = false;
        else installhsqm.IsEnabled = true;
        if (UTdata.RIDFCertInstalled) installridfcert.IsEnabled = false;
        else installridfcert.IsEnabled = true;
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

    public Extra(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    private async void installhsqm_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("InstallHSM");
        if (File.Exists("C:\\Program Files\\Unowhy\\HiSqool Manager\\HiSqoolManager.exe"))
        {
            Process.Start("C:\\Program Files\\Unowhy\\HiSqool Manager\\HiSqoolManager.exe");
        }
        else
        {
            if (await UT.CheckInternet())
            {
                if (File.Exists(Path.GetTempPath() + "\\hsm.exe")) File.Delete(Path.GetTempPath() + "\\hsm.exe");

                var progress = new System.Progress<double>();
                var cancellationToken = new CancellationTokenSource();
                string dl = await UT.GetLang("wait.download");
                await UT.waitstatus.open(dl, "clouddl.png");
                progress.ProgressChanged += async (sender, value) =>
                {
                    await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "null");
                };
                await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("hsm"), Path.GetTempPath() + "\\hsm.exe", progress, cancellationToken.Token);
                Process.Start(Path.GetTempPath() + "\\hsm.exe");
                await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
                CheckBTN(true, "hsqm");
                await UT.waitstatus.close();
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
            }
        }
    }

    private async void installridfcert_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("InstallRIDFCA");
        if (await UT.CheckInternet())
        {
            if (File.Exists(Path.GetTempPath() + "\\RIDF_CA.crt")) File.Delete(Path.GetTempPath() + "\\RIDF_CA.crt");

            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = await UT.GetLang("wait.download");
            await UT.waitstatus.open(dl, "clouddl.png");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "null");
            };
            await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("ridfca"), Path.GetTempPath() + "\\RIDF_CA.crt", progress, cancellationToken.Token);
            await UT.waitstatus.open(await UT.GetLang("wait.install"), "download.png");
            await UT.RunMin("powershell", $"Import-Certificate -Filepath \"{Path.GetTempPath() + "\\RIDF_CA.crt"}\" -CertStoreLocation Cert:\\LocalMachine\\Root");
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "ridfcert");
            await UT.waitstatus.close();
            if (!installridfcert.IsEnabled)
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

    private async void openrufus_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("OpenRufus");
        if (await UT.CheckInternet())
        {
            if (File.Exists(Path.GetTempPath() + "\\rufus.exe")) File.Delete(Path.GetTempPath() + "\\rufus.exe");

            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = await UT.GetLang("wait.download");
            await UT.waitstatus.open(dl, "clouddl.png");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "null");
            };
            await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("rufus"), Path.GetTempPath() + "\\rufus.exe", progress, cancellationToken.Token);
            Process.Start(Path.GetTempPath() + "\\rufus.exe");
            await UT.waitstatus.close();
        }
        else
        {
            UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
        }
    }

    private async void openmas_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("OpenMAS");
        if (await UT.CheckInternet())
        {
            Process.Start("powershell", "\"irm https://massgrave.dev/get | iex\"");
        }
        else
        {
            UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
        }
    }
}
