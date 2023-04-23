using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Windows;

using Unowhy_Tools;
using System.Diagnostics;
using System;
using Microsoft.Win32;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using static Unowhy_Tools.UT;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Dashboard : INavigableView<DashboardViewModel>
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

    public async void Init(object sender, EventArgs e)
    {
        /*string ver = "Version " + UT.version.getverfull().ToString().Insert(2, ".") + " (Build " + UT.version.getverbuild().ToString() + ") ";

        if (UT.version.isdeb()) ver = ver + "(Debug)";
        else ver = ver + "(Release)";

        LogoVer.Text = ver;*/
        LogoVer.Text = "";

        string ver = "Version " + UT.version.getverfull().ToString().Insert(2, ".");

        if (UT.version.isdeb()) ver = ver + " (Debug)";
        else ver = ver + " (Release)";

        lababout2.Text = ver;

        applylang();
        pcname.Text = UT.GetLine(UTdata.HostName, 1);
        if (pcname.Text.Contains("Lenovo"))
        {
            pcname.Text = "Unowhy-Win11";
        }

        if (UT.CheckInternet())
        {
            RegistryKey lcs = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utcuab = lcs.GetValue("UpdateStart").ToString();
            if (utcuab == "1")
            {
                lababout2.Text = UT.GetLang("update.check");
                if (await UT.version.newver())
                {
                    Color white = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                    Color gray = (Color)ColorConverter.ConvertFromString("#bebebe");
                    var web = new HttpClient();
                    string newver = await web.GetStringAsync("https://bit.ly/UTnvTXT");
                    newver = newver.Insert(2, ".");
                    newver = newver.Replace("\n", "");
                    string newverfull = UT.version.getverfull().ToString().Insert(2, ".") + " -> " + newver;
                    string labnewver = UT.GetLang("newver");

                    DoubleAnimation anim = new DoubleAnimation();
                    anim.From = 0;
                    anim.To = 250;
                    anim.Duration = TimeSpan.FromMilliseconds(500);
                    anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseIn, Power = 5 };
                    DoubleAnimation anim2 = new DoubleAnimation();
                    anim2.From = -250;
                    anim2.To = 0;
                    anim2.Duration = TimeSpan.FromMilliseconds(500);
                    anim2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };

                    TranslateTransform trans = new TranslateTransform();
                    lababout2.RenderTransform = trans;

                    while (true)
                    {
                        lababout2.Foreground = new SolidColorBrush(white);
                        await Task.Delay(500);
                        lababout2.Foreground = new SolidColorBrush(gray);
                        await Task.Delay(500);
                        lababout2.Foreground = new SolidColorBrush(white);
                        await Task.Delay(500);
                        lababout2.Foreground = new SolidColorBrush(gray);
                        trans.BeginAnimation(TranslateTransform.XProperty, anim);
                        await Task.Delay(500);
                        lababout2.Text = labnewver;
                        trans.BeginAnimation(TranslateTransform.XProperty, anim2);
                        lababout2.Foreground = new SolidColorBrush(white);
                        await Task.Delay(500);
                        lababout2.Foreground = new SolidColorBrush(gray);
                        await Task.Delay(500);
                        lababout2.Foreground = new SolidColorBrush(white);
                        await Task.Delay(500);
                        lababout2.Foreground = new SolidColorBrush(gray);
                        trans.BeginAnimation(TranslateTransform.XProperty, anim);
                        await Task.Delay(500);
                        lababout2.Text = newverfull;
                        trans.BeginAnimation(TranslateTransform.XProperty, anim2);


                    }
                }
                else
                {
                    lababout2.Text = ver;
                }
            }
        }
        else
        {
            lababout2.Text = ver;
        }
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        quickoption.RenderTransform = trans;

        DoubleAnimation anim2 = new DoubleAnimation();
        anim2.From = 1;
        anim2.To = 1;
        anim2.Duration = TimeSpan.FromMilliseconds(600);
        anim2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

        quickoption.BeginAnimation(Border.OpacityProperty, anim2);
        trans.BeginAnimation(TranslateTransform.YProperty, anim);

        DoubleAnimation animsb = new DoubleAnimation();
        animsb.From = 0;
        animsb.To = 50;
        animsb.Duration = TimeSpan.FromMilliseconds(1800);
        animsb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        DoubleAnimation animsb2 = new DoubleAnimation();
        animsb2.From = 0;
        animsb2.To = -50;
        animsb2.Duration = TimeSpan.FromMilliseconds(1800);
        animsb2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform transsb = new TranslateTransform();
        TranslateTransform transsb2 = new TranslateTransform();
        SB1.RenderTransform = transsb;
        SB2.RenderTransform = transsb2;
        transsb.BeginAnimation(TranslateTransform.XProperty, animsb);
        transsb.BeginAnimation(TranslateTransform.YProperty, animsb2);
        transsb2.BeginAnimation(TranslateTransform.XProperty, animsb2);
        transsb2.BeginAnimation(TranslateTransform.YProperty, animsb);

        /*
        foreach (UIElement element in utlabs.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        */

        foreach (UIElement element in qogrid.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        /*
        foreach (UIElement element in utlabs.Children)
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
                From = 50,
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
        */

        foreach (UIElement element in qogrid.Children)
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
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);

            await Task.Delay(50);
        }
    }

    public Dashboard(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public void applylang()
    {
        lababout.Text = UT.GetLang("about");
        labinfo.Text = UT.GetLang("pcinfo");
        labset.Text = UT.GetLang("settings");
    }

    public void Taskmgr(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start("taskmgr.exe");
    }
    
    public void CMD(object sender, RoutedEventArgs e)
    {
        //System.Diagnostics.Process.Start("cmd.exe");
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.WorkingDirectory = "C:\\Windows\\System32\\";
        p.Start();
    }
    
    public void Regedit(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start("regedit.exe");
    }

    public void Gpedit(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start("mmc.exe", "gpedit.msc");
    }

    public async void Guide(object sender, RoutedEventArgs e)
    {
        UT.anim.TransitionForw(RootGrid);
        await Task.Delay(150);
        UT.NavigateTo(typeof(Wifi));
    }
}
