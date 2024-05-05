using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Windows;

using Unowhy_Tools;
using System.Diagnostics;
using Microsoft.Web.WebView2.Core;
using System.Windows.Controls;
using System.Net.Http;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Wpf;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System;
using static Unowhy_Tools.UT;
using System.Net;
using System.Reflection.Metadata;
using System.Threading;
using Microsoft.Win32.TaskScheduler;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Updater : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        //UT.anim.BackBtnAnim(BackBTN);
        await System.Threading.Tasks.Task.Delay(150);
        //UT.anim.TransitionBack(RootGrid);
        await System.Threading.Tasks.Task.Delay(200);
        UT.NavigateTo(typeof(About));
    }

    public async Task applylang()
    {
        updatebtntext.Text = await UT.GetLang("udcheck");
    }

    public async void Init(object sender, System.EventArgs e)
    {
        string path = UT.utpath + "\\Unowhy Tools\\Temps\\WebView2";

        browser.CreationProperties = new CoreWebView2CreationProperties()
        {
            UserDataFolder = path
        };

        if (await UT.CheckInternet())
        {
            noco.Visibility = Visibility.Collapsed;
            browser.Source = new System.Uri(await UT.OnlineDatas.GetUrls("utcloghtmlprev"));
        }
        else
        {
            browser.Visibility = Visibility.Hidden;
            gitbtn.Visibility = Visibility.Hidden;
            UpdateBTN.Visibility = Visibility.Hidden;
        }
        labimg.Visibility = Visibility.Hidden;
        labtext.Visibility = Visibility.Hidden;
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        await UT.DeployBack(typeof(About), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);
    }

    public Updater(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public async void GithubButton_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("Updater.GithubClick");
        browser.Source = new System.Uri(await UT.OnlineDatas.GetUrls("utreleases"));

        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = 150;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        gitbtn.RenderTransform = trans;

        trans.BeginAnimation(TranslateTransform.YProperty, anim);

        await System.Threading.Tasks.Task.Delay(600);

        gitbtn.Click -= GithubButton_Click;
        gitbtn.Click += GithubButton2_Click;
        gitimg.Source = UT.GetImgSource("back.png");
        gittxt.Text = "Change Log";

        anim.From = -150;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        trans = new TranslateTransform();
        gitbtn.RenderTransform = trans;

        trans.BeginAnimation(TranslateTransform.XProperty, anim);
    }
    
    public async void GithubButton2_Click(object sender, RoutedEventArgs e)
    {
        browser.Source = new System.Uri(await UT.OnlineDatas.GetUrls("utcloghtmlprev"));

        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = -150;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        gitbtn.RenderTransform = trans;

        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        await System.Threading.Tasks.Task.Delay(600);

        gitbtn.Click += GithubButton_Click;
        gitbtn.Click -= GithubButton2_Click;
        gitimg.Source = UT.GetImgSource("github.png");
        gittxt.Text = "Github";

        anim.From = 150;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        trans = new TranslateTransform();
        gitbtn.RenderTransform = trans;

        trans.BeginAnimation(TranslateTransform.YProperty, anim);
    }
    
    public async void CheckButton_Click(object sender, RoutedEventArgs e)
    {
        labtext.Text = await UT.GetLang("update.check");
        labimg.Source = UT.GetImgSource("wait.png");

        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 150;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        labimg.RenderTransform = trans;
        labtext.RenderTransform = trans;

        if(labimg.Visibility == Visibility.Hidden && labtext.Visibility == Visibility.Hidden)
        {
            labtext.Visibility = Visibility.Visible;
            labimg.Visibility = Visibility.Visible;
            trans.BeginAnimation(TranslateTransform.YProperty, anim);
        }

        await System.Threading.Tasks.Task.Delay(600);
        
        if (await UT.version.newver())
        {
            labimg.Source = UT.GetImgSource("yes.png");
            var web = new HttpClient();
            string newver = await UT.OnlineDatas.GetUpdates("utnewver");
            newver = newver.Insert(2, ".");
            newver = newver.Replace("\n", "");
            string newverfull = await UT.GetLang("newver") + " (" + UT.version.getverfull().ToString().Insert(2, ".") + " -> " + newver + ")";
            labtext.Text = newverfull;
            UpdateBTN.Click -= CheckButton_Click;
            UpdateBTN.Click += InstallButton_Click;
            updatebtnimg.Source = UT.GetImgSource("download.png");
            updatebtntext.Text = await UT.GetLang("unow");
        }
        else if (UT.version.isdeb())
        {
            labimg.Source = UT.GetImgSource("yes.png");
            var web = new HttpClient();
            string newver = await UT.OnlineDatas.GetUpdates("utnewver");
            newver = newver.Insert(2, ".");
            newver = newver.Replace("\n", "");
            string newverfull = "Update to latest release (" + UT.version.getverfull().ToString().Insert(2, ".") + " -> " + newver + ")";
            labtext.Text = newverfull;
            UpdateBTN.Click -= CheckButton_Click;
            UpdateBTN.Click += InstallButton_Click;
            updatebtnimg.Source = UT.GetImgSource("download.png");
            updatebtntext.Text = await UT.GetLang("unow");
        }
        else
        {
            labtext.Text = await UT.GetLang("nover");
            labimg.Source = UT.GetImgSource("no.png");
        }
    }

    public async void InstallButton_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("Updater.Update");
        string utemp = UT.utpath + "\\Unowhy Tools\\Temps";
        Color disabled = (Color)ColorConverter.ConvertFromString("#888888");
        updatebtntext.Foreground = new SolidColorBrush(disabled);
        UpdateBTN.IsEnabled = false;
        labimg.Source = UT.GetImgSource("clouddl.png");
        labtext.Text = await UT.GetLang("update.dl");
        await System.Threading.Tasks.Task.Delay(1000);
        string uddl = await UT.GetLang("update.dl");
        var progress = new System.Progress<double>();
        var cancellationToken = new CancellationTokenSource();
        progress.ProgressChanged += (sender, value) =>
        {
            labtext.Text = uddl + " (" + value.ToString("##0") + "%)";
        };
        await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("utupdatezip"), utemp + "\\update.zip", progress, cancellationToken.Token);
        progress = new System.Progress<double>();
        cancellationToken = new CancellationTokenSource();
        progress.ProgressChanged += (sender, value) =>
        {
            labtext.Text = uddl + " (" + value.ToString("##0") + "%)";
        };
        await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("utszip"), utemp + "\\service.zip", progress, cancellationToken.Token);
        labtext.Text = await UT.GetLang("update.ext");
        labimg.Source = UT.GetImgSource("zip.png");
        await System.Threading.Tasks.Task.Delay(500);
        ZipFile.ExtractToDirectory(utemp + "\\update.zip", utemp + "\\Update", true);
        labtext.Text = await UT.GetLang("update.sext");
        await System.Threading.Tasks.Task.Delay(500);
        Directory.CreateDirectory(utemp + "\\Update\\Unowhy Tools Service");
        ZipFile.ExtractToDirectory(utemp + "\\service.zip", utemp + "\\Update\\Unowhy Tools Service", true);
        labtext.Text = await UT.GetLang("update.updating");
        labimg.Source = UT.GetImgSource("update.png");
        await System.Threading.Tasks.Task.Delay(1000);
        string pre = utemp + "\\update";
        string post = Directory.GetCurrentDirectory();

        bool trayenabled = false;
        TaskService ts = new TaskService();
        Microsoft.Win32.TaskScheduler.Task uttltask = ts.GetTask("Unowhy Tools Tray Launch");
        if (uttltask != null)
        {
            if (uttltask.Definition.Settings.Enabled)
            {
                trayenabled = true;
            }
            else
            {
                trayenabled = false;
            }
        }
        else
        {
            trayenabled = false;
        }

        string args = $"-user {UTdata.UserID} ";
        if (UTdata.RunConsole)
        {
            args = args + "-console ";
        }

        if (UTdata.RunUpdater && trayenabled)
        {
            args = args + "-tray ";
        }

        Process.Start("cmd.exe", $"/c echo Unowhy Tools by STY1001 & echo Please wait, Unowhy Tools is updating... & echo Killing Unowhy Tools... & taskkill /f /im \"Unowhy Tools.exe\" & echo Stopping Unowhy Tools Service... & net stop UTS & timeout -t 3 & echo Copying files... & del /s /q \"{post}\\*\" & xcopy \"{pre}\" \"{post}\" /e /h /c /i /y & cls & echo Update done... & echo Starting Unowhy Tools Service... & net start UTS & echo Restarting Unowhy Tools... & powershell -windows hidden -command \"\" & \"Unowhy Tools.exe\" {args}");
    }
}

