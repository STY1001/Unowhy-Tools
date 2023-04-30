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
        UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(About));
    }

    public void applylang()
    {
        updatebtntext.Text = UT.GetLang("udcheck");
    }

    public async void Init(object sender, System.EventArgs e)
    {
        string path = Path.GetTempPath() + "\\Unowhy Tools\\Temps\\WebView2";

        browser.CreationProperties = new CoreWebView2CreationProperties()
        {
            UserDataFolder = path
        };

        if (UT.CheckInternet())
        {
            noco.Visibility = Visibility.Collapsed;
            browser.Source = new System.Uri("https://bit.ly/UTclogHTMLPrev");
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
        UT.anim.BackBtnAnimForw(BackBTN);
    }

    public Updater(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public async void GithubButton_Click(object sender, RoutedEventArgs e)
    {
        browser.Source = new System.Uri("https://bit.ly/UTreleases");

        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = 150;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        gitbtn.RenderTransform = trans;

        trans.BeginAnimation(TranslateTransform.YProperty, anim);

        await Task.Delay(600);

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
        browser.Source = new System.Uri("https://bit.ly/UTclogHTMLPrev");

        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = -150;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        gitbtn.RenderTransform = trans;

        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        await Task.Delay(600);

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
        labtext.Text = UT.GetLang("update.check");
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

        await Task.Delay(600);
        
        if (await UT.version.newver())
        {
            labimg.Source = UT.GetImgSource("yes.png");
            var web = new HttpClient();
            string newver = await web.GetStringAsync("https://bit.ly/UTnvTXT");
            newver = newver.Insert(2, ".");
            newver = newver.Replace("\n", "");
            string newverfull = UT.GetLang("newver") + " (" + UT.version.getverfull().ToString().Insert(2, ".") + " -> " + newver + ")";
            labtext.Text = newverfull;
            UpdateBTN.Click -= CheckButton_Click;
            UpdateBTN.Click += InstallButton_Click;
            updatebtnimg.Source = UT.GetImgSource("download.png");
            updatebtntext.Text = UT.GetLang("unow");
        }
        else
        {
            labtext.Text = UT.GetLang("nover");
            labimg.Source = UT.GetImgSource("no.png");
        }
    }

    public async void InstallButton_Click(object sender, RoutedEventArgs e)
    {
        Color disabled = (Color)ColorConverter.ConvertFromString("#888888");
        updatebtntext.Foreground = new SolidColorBrush(disabled);
        UpdateBTN.IsEnabled = false;
        labimg.Source = UT.GetImgSource("download.png");
        labtext.Text = UT.GetLang("update.dl");
        await Task.Delay(1000);
        var web = new HttpClient();
        var filebyte = await web.GetByteArrayAsync("https://bit.ly/UTupdateZIP");
        var filebyte2 = await web.GetByteArrayAsync("https://bit.ly/UTuninstaller");
        string utemp = Path.GetTempPath() + "Unowhy Tools\\Temps";
        File.WriteAllBytes(utemp + "\\update.zip", filebyte);
        File.WriteAllBytes(utemp + "\\Update\\uninstall.exe", filebyte2);
        labtext.Text = UT.GetLang("update.ext");
        labimg.Source = UT.GetImgSource("zip.png");
        await Task.Delay(1000);
        ZipFile.ExtractToDirectory(utemp + "\\update.zip", utemp + "\\Update");
        labtext.Text = UT.GetLang("update.updating");
        labimg.Source = UT.GetImgSource("update.png");
        await Task.Delay(1000);
        string pre = utemp + "\\update";
        string post = Directory.GetCurrentDirectory();

        Process.Start("cmd.exe", $"/c echo Updating Unowhy Tools... & taskkill /f /im \"Unowhy Tools.exe\" & net stop UTS & timeout -t 3 & del /s /q \"{post}\\*\" & xcopy \"{pre}\" \"{post}\" /e /h /c /i /y & echo Done ! & powershell -windows hidden -command \"\" & \"Unowhy Tools.exe\" -user {UTdata.UserID}");

    }
}
