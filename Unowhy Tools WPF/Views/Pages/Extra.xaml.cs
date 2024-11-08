using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Unowhy_Tools;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Common.Interfaces;

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
        opencru_btn.Content = await UT.GetLang("open");
        opencru_txt.Text = await UT.GetLang("opencru");
        opencru_desc.Text = await UT.GetLang("desccru");
        installms365_txt.Text = await UT.GetLang("installms365pp");
        installms365_desc.Text = await UT.GetLang("descinstallms365pp");
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
            if (File.Exists(Path.GetTempPath() + "\\rufus.exe"))
            {
                Process.Start(Path.GetTempPath() + "\\rufus.exe");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
            }
        }
    }

    private async void openmas_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("OpenMAS");
        if (await UT.CheckInternet())
        {
            Process.Start("powershell", "\"irm https://get.activated.win | iex\"");
        }
        else
        {
            UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
        }
    }

    private async void opencru_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("OpenCRU");
        if (!File.Exists(Path.GetTempPath() + "\\CRU\\cru.exe"))
        {
            if (await UT.CheckInternet())
            {
                if (Directory.Exists(Path.GetTempPath() + "\\CRU")) Directory.Delete(Path.GetTempPath() + "\\CRU", true);
                var progress = new System.Progress<double>();
                var cancellationToken = new CancellationTokenSource();
                string dl = await UT.GetLang("wait.download");
                await UT.waitstatus.open(dl, "clouddl.png");
                progress.ProgressChanged += async (sender, value) =>
                {
                    await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "null");
                };
                await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("cru"), Path.GetTempPath() + "\\cru.zip", progress, cancellationToken.Token);
                await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
                Directory.CreateDirectory(Path.GetTempPath() + "\\CRU");
                ZipFile.ExtractToDirectory(Path.GetTempPath() + "\\cru.zip", Path.GetTempPath() + "\\CRU", true);
                Process.Start(Path.GetTempPath() + "\\CRU\\cru.exe");
                await UT.waitstatus.close();
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
            }

        }
        else
        {
            Process.Start(Path.GetTempPath() + "\\CRU\\cru.exe");
        }
    }

    private async void opencrurestart_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("OpenCRURestart");
        if (!File.Exists(Path.GetTempPath() + "\\CRU\\restart64.exe"))
        {
            if (await UT.CheckInternet())
            {
                if (Directory.Exists(Path.GetTempPath() + "\\CRU")) Directory.Delete(Path.GetTempPath() + "\\CRU", true);
                var progress = new System.Progress<double>();
                var cancellationToken = new CancellationTokenSource();
                string dl = await UT.GetLang("wait.download");
                await UT.waitstatus.open(dl, "clouddl.png");
                progress.ProgressChanged += async (sender, value) =>
                {
                    await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "null");
                };
                await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("cru"), Path.GetTempPath() + "\\cru.zip", progress, cancellationToken.Token);
                await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
                Directory.CreateDirectory(Path.GetTempPath() + "\\CRU");
                ZipFile.ExtractToDirectory(Path.GetTempPath() + "\\cru.zip", Path.GetTempPath() + "\\CRU", true);
                Process.Start(Path.GetTempPath() + "\\CRU\\restart64.exe");
                await UT.waitstatus.close();
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
            }

        }
        else
        {
            Process.Start(Path.GetTempPath() + "\\CRU\\restart64.exe");
        }
    }

    private async void opencrureset_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("OpenCRUReset");
        if (!File.Exists(Path.GetTempPath() + "\\CRU\\reset-all.exe"))
        {
            if (await UT.CheckInternet())
            {
                if (Directory.Exists(Path.GetTempPath() + "\\CRU")) Directory.Delete(Path.GetTempPath() + "\\CRU", true);
                var progress = new System.Progress<double>();
                var cancellationToken = new CancellationTokenSource();
                string dl = await UT.GetLang("wait.download");
                await UT.waitstatus.open(dl, "clouddl.png");
                progress.ProgressChanged += async (sender, value) =>
                {
                    await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "null");
                };
                await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("cru"), Path.GetTempPath() + "\\cru.zip", progress, cancellationToken.Token);
                await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
                Directory.CreateDirectory(Path.GetTempPath() + "\\CRU");
                ZipFile.ExtractToDirectory(Path.GetTempPath() + "\\cru.zip", Path.GetTempPath() + "\\CRU", true);
                Process.Start(Path.GetTempPath() + "\\CRU\\reset-all.exe");
                await UT.waitstatus.close();
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
            }

        }
        else
        {
            Process.Start(Path.GetTempPath() + "\\CRU\\reset-all.exe");
        }
    }

    private async void installms365fr_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("InstallMS365Fr");
        if (await UT.CheckInternet())
        {
            if (File.Exists(Path.GetTempPath() + "\\ms365ppsetupfr.exe")) File.Delete(Path.GetTempPath() + "\\ms365ppsetupfr.exe");

            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = await UT.GetLang("wait.download");
            await UT.waitstatus.open(dl, "clouddl.png");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "null");
            };
            await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("ms365ppfr"), Path.GetTempPath() + "\\ms365ppsetupfr.exe", progress, cancellationToken.Token);
            Process.Start(Path.GetTempPath() + "\\ms365ppsetupfr.exe");
            await UT.waitstatus.close();
        }
        else
        {
            if (File.Exists(Path.GetTempPath() + "\\ms365ppsetupfr.exe"))
            {
                Process.Start(Path.GetTempPath() + "\\ms365ppsetupfr.exe");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
            }
        }
    }

    private async void installms365en_btn_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("InstallMS365En");
        if (await UT.CheckInternet())
        {
            if (File.Exists(Path.GetTempPath() + "\\ms365ppsetupen.exe")) File.Delete(Path.GetTempPath() + "\\ms365ppsetupen.exe");

            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = await UT.GetLang("wait.download");
            await UT.waitstatus.open(dl, "clouddl.png");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "null");
            };
            await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("ms365ppen"), Path.GetTempPath() + "\\ms365ppsetupen.exe", progress, cancellationToken.Token);
            Process.Start(Path.GetTempPath() + "\\ms365ppsetupen.exe");
            await UT.waitstatus.close();
        }
        else
        {
            if (File.Exists(Path.GetTempPath() + "\\ms365ppsetupen.exe"))
            {
                Process.Start(Path.GetTempPath() + "\\ms365ppsetupen.exe");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
            }
        }
    }
}
