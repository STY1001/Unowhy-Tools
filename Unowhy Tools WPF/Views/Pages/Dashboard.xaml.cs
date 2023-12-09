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
using System.Xml.Linq;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Dashboard : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();
    public bool utadeployed = false;

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void GoForw(object sender, RoutedEventArgs e)
    {
        //UT.anim.TransitionForw(RootGrid);
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
            if (await UT.Config.Get("UpdateStart") == "1")
            {
                lababout2.Text = UT.GetLang("update.check");
                if (await UT.version.newver())
                {
                    Color white = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                    Color gray = (Color)ColorConverter.ConvertFromString("#bebebe");
                    var web = new HttpClient();
                    string newver = await UT.OnlineDatas.GetUpdates("utnewver");
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
        await UT.UnDeployBack();

        DoubleAnimation animqo = new DoubleAnimation();
        animqo.From = 0;
        animqo.To = 0;
        animqo.Duration = TimeSpan.FromMilliseconds(0);
        animqo.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform transqo = new TranslateTransform();
        qogrid.RenderTransform = transqo;
        transqo.BeginAnimation(TranslateTransform.YProperty, animqo);

        DoubleAnimation animlogo = new DoubleAnimation();
        animlogo.From = 0;
        animlogo.To = 0;
        animlogo.Duration = TimeSpan.FromMilliseconds(0);
        animlogo.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        DoubleAnimation animlogo2 = new DoubleAnimation();
        animlogo2.From = 0;
        animlogo2.To = 0;
        animlogo2.Duration = TimeSpan.FromMilliseconds(0);
        animlogo2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform translogo = new TranslateTransform();
        LogoGrid.RenderTransform = translogo;
        translogo.BeginAnimation(TranslateTransform.YProperty, animlogo);
        translogo.BeginAnimation(TranslateTransform.XProperty, animlogo2);

        DoubleAnimation preanimsb = new DoubleAnimation();
        preanimsb.From = 0;
        preanimsb.To = 0;
        preanimsb.Duration = TimeSpan.FromMilliseconds(0);
        preanimsb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        DoubleAnimation preanimsb2 = new DoubleAnimation();
        preanimsb2.From = 0;
        preanimsb2.To = 0;
        preanimsb2.Duration = TimeSpan.FromMilliseconds(0);
        preanimsb2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform pretranssb = new TranslateTransform();
        TranslateTransform pretranssb2 = new TranslateTransform();
        SB1.RenderTransform = pretranssb;
        SB2.RenderTransform = pretranssb2;
        pretranssb.BeginAnimation(TranslateTransform.XProperty, preanimsb);
        pretranssb.BeginAnimation(TranslateTransform.YProperty, preanimsb2);
        pretranssb2.BeginAnimation(TranslateTransform.XProperty, preanimsb2);
        pretranssb2.BeginAnimation(TranslateTransform.YProperty, preanimsb);

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

        foreach (UIElement element in qogrid.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

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

        DoubleAnimation animsb = new DoubleAnimation();
        animsb.From = 0;
        animsb.To = 20;
        animsb.Duration = TimeSpan.FromMilliseconds(1800);
        animsb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        DoubleAnimation animsb2 = new DoubleAnimation();
        animsb2.From = 0;
        animsb2.To = -20;
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
    }

    public async void UnInitAnim(object sender, RoutedEventArgs e)
    {
        if (utadeployed)
        {
            utadeployed = false;
            uta.Click -= Switch_UT2QO;
            uta.Click += Switch_QO2UT;

            utaimg.Source = UT.GetImgSource("UT.png");
            utalab.Text = "Unowhy Tools Apps";
            utalab2.Text = "Wifi, BIOS";
            uta.IsChevronVisible = true;
            {
                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation translateAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 80,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                TranslateTransform transform = new TranslateTransform();
                utagrid.RenderTransform = transform;

                utagrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
            }

            //await Task.Delay(500);

            {
                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation translateAnimation = new DoubleAnimation
                {
                    From = -80,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                TranslateTransform transform = new TranslateTransform();
                qogrid.RenderTransform = transform;

                qogrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
            }

            await Task.Delay(500);
            utagrid.Visibility = Visibility.Collapsed;
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

    public async void About(object sender, RoutedEventArgs e)
    {
        if (utadeployed)
        {
            utadeployed = false;
            uta.Click -= Switch_UT2QO;
            uta.Click += Switch_QO2UT;

            utaimg.Source = UT.GetImgSource("UT.png");
            utalab.Text = "Unowhy Tools Apps";
            utalab2.Text = "Wifi, BIOS";
            uta.IsChevronVisible = true;
            {
                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation translateAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 80,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                TranslateTransform transform = new TranslateTransform();
                utagrid.RenderTransform = transform;

                utagrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
            }

            //await Task.Delay(500);

            {
                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation translateAnimation = new DoubleAnimation
                {
                    From = -80,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                TranslateTransform transform = new TranslateTransform();
                qogrid.RenderTransform = transform;

                qogrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
            }

            await Task.Delay(500);
            utagrid.Visibility = Visibility.Collapsed;
        }

        DoubleAnimation animsb = new DoubleAnimation();
        animsb.From = 20;
        animsb.To = 0;
        animsb.Duration = TimeSpan.FromMilliseconds(500);
        animsb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        DoubleAnimation animsb2 = new DoubleAnimation();
        animsb2.From = -20;
        animsb2.To = 0;
        animsb2.Duration = TimeSpan.FromMilliseconds(500);
        animsb2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform transsb = new TranslateTransform();
        TranslateTransform transsb2 = new TranslateTransform();
        SB1.RenderTransform = transsb;
        SB2.RenderTransform = transsb2;
        transsb.BeginAnimation(TranslateTransform.XProperty, animsb);
        transsb.BeginAnimation(TranslateTransform.YProperty, animsb2);
        transsb2.BeginAnimation(TranslateTransform.XProperty, animsb2);
        transsb2.BeginAnimation(TranslateTransform.YProperty, animsb);

        DoubleAnimation animqo = new DoubleAnimation();
        animqo.From = 0;
        animqo.To = 250;
        animqo.Duration = TimeSpan.FromMilliseconds(500);
        animqo.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform transqo = new TranslateTransform();
        qogrid.RenderTransform = transqo;
        transqo.BeginAnimation(TranslateTransform.YProperty, animqo);

        DoubleAnimation animlogo = new DoubleAnimation();
        animlogo.From = 0;
        animlogo.To = 0;
        animlogo.Duration = TimeSpan.FromMilliseconds(500);
        animlogo.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        DoubleAnimation animlogo2 = new DoubleAnimation();
        animlogo2.From = 0;
        animlogo2.To = -187;
        animlogo2.Duration = TimeSpan.FromMilliseconds(500);
        animlogo2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        TranslateTransform translogo = new TranslateTransform();
        LogoGrid.RenderTransform = translogo;
        translogo.BeginAnimation(TranslateTransform.YProperty, animlogo);
        translogo.BeginAnimation(TranslateTransform.XProperty, animlogo2);

        await Task.Delay(500);

        var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
        mainWindow.RootNavigation.TransitionType = Wpf.Ui.Animations.TransitionType.None;
        UT.NavigateTo(typeof(About));
        await Task.Delay(1000);
        mainWindow.RootNavigation.TransitionType = Wpf.Ui.Animations.TransitionType.None;
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

    public async void UTW(object sender, RoutedEventArgs e)
    {
        UT.anim.RegisterParent(RootGrid, RootBorder);
        UT.anim.AnimParent("zoomout2");
        await Task.Delay(500);
        UT.NavigateTo(typeof(Wifi));
    }

    public async void UTB(object sender, RoutedEventArgs e)
    {
        UT.anim.RegisterParent(RootGrid, RootBorder);
        UT.anim.AnimParent("zoomout2");
        await Task.Delay(500);
        UT.NavigateTo(typeof(Bios));
    }

    public async void SysInfo(object sender, RoutedEventArgs e)
    {
        UT.anim.RegisterParent(RootGrid, RootBorder);
        UT.anim.AnimParent("zoomout2");
        await Task.Delay(500);
        UT.NavigateTo(typeof(PCinfo));
    }
    public async void GoSettings(object sender, RoutedEventArgs e)
    {
        UT.anim.RegisterParent(RootGrid, RootBorder);
        UT.anim.AnimParent("zoomout2");
        await Task.Delay(500);
        UT.NavigateTo(typeof(Settings));
    }

    public async void Switch_QO2UT(object sender, RoutedEventArgs e)
    {
        utadeployed = true;
        uta.Click += Switch_UT2QO;
        uta.Click -= Switch_QO2UT;

        utaimg.Source = UT.GetImgSource("back.png");
        utalab.Text = "Collapse";
        utalab2.Text = "Go Back";
        uta.IsChevronVisible = false;
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = -80,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            qogrid.RenderTransform = transform;

            qogrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }

        //await Task.Delay(500);

        {
            utagrid.Visibility = Visibility.Visible;
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 77,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            utagrid.RenderTransform = transform;

            utagrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }
    }

    public async void Switch_UT2QO(object sender, RoutedEventArgs e)
    {
        utadeployed = false;
        uta.Click -= Switch_UT2QO;
        uta.Click += Switch_QO2UT;

        utaimg.Source = UT.GetImgSource("UT.png");
        utalab.Text = "Unowhy Tools Apps";
        utalab2.Text = "Wifi, BIOS";
        uta.IsChevronVisible = true;
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 80,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            utagrid.RenderTransform = transform;

            utagrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }

        //await Task.Delay(500);

        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = -80,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            qogrid.RenderTransform = transform;

            qogrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }
        
        await Task.Delay(500);
        utagrid.Visibility = Visibility.Collapsed;
    }

    private async void LogoImg_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        {
            DoubleAnimation animsb = new DoubleAnimation();
            animsb.From = 20;
            animsb.To = 0;
            animsb.Duration = TimeSpan.FromMilliseconds(500);
            animsb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseIn, Power = 5 };
            DoubleAnimation animsb2 = new DoubleAnimation();
            animsb2.From = -20;
            animsb2.To = 0;
            animsb2.Duration = TimeSpan.FromMilliseconds(500);
            animsb2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseIn, Power = 5 };
            TranslateTransform transsb = new TranslateTransform();
            TranslateTransform transsb2 = new TranslateTransform();
            SB1.RenderTransform = transsb;
            SB2.RenderTransform = transsb2;
            transsb.BeginAnimation(TranslateTransform.XProperty, animsb);
            transsb.BeginAnimation(TranslateTransform.YProperty, animsb2);
            transsb2.BeginAnimation(TranslateTransform.XProperty, animsb2);
            transsb2.BeginAnimation(TranslateTransform.YProperty, animsb);
        }
        await Task.Delay(500);
        {
            DoubleAnimation animsb = new DoubleAnimation();
            animsb.From = 0;
            animsb.To = 20;
            animsb.Duration = TimeSpan.FromMilliseconds(1800);
            animsb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
            DoubleAnimation animsb2 = new DoubleAnimation();
            animsb2.From = 0;
            animsb2.To = -20;
            animsb2.Duration = TimeSpan.FromMilliseconds(1800);
            animsb2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
            TranslateTransform transsb = new TranslateTransform();
            TranslateTransform transsb2 = new TranslateTransform();
            SB1.RenderTransform = transsb;
            SB2.RenderTransform = transsb2;
            transsb.BeginAnimation(TranslateTransform.YProperty, animsb);
            transsb.BeginAnimation(TranslateTransform.XProperty, animsb2);
            transsb2.BeginAnimation(TranslateTransform.YProperty, animsb2);
            transsb2.BeginAnimation(TranslateTransform.XProperty, animsb);
        }
        await Task.Delay(1800);
        {
            DoubleAnimation animsb = new DoubleAnimation();
            animsb.From = 20;
            animsb.To = 0;
            animsb.Duration = TimeSpan.FromMilliseconds(500);
            animsb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseIn, Power = 5 };
            DoubleAnimation animsb2 = new DoubleAnimation();
            animsb2.From = -20;
            animsb2.To = 0;
            animsb2.Duration = TimeSpan.FromMilliseconds(500);
            animsb2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseIn, Power = 5 };
            TranslateTransform transsb = new TranslateTransform();
            TranslateTransform transsb2 = new TranslateTransform();
            SB1.RenderTransform = transsb;
            SB2.RenderTransform = transsb2;
            transsb.BeginAnimation(TranslateTransform.YProperty, animsb);
            transsb.BeginAnimation(TranslateTransform.XProperty, animsb2);
            transsb2.BeginAnimation(TranslateTransform.YProperty, animsb2);
            transsb2.BeginAnimation(TranslateTransform.XProperty, animsb);
        }
        await Task.Delay(500);
        {
            DoubleAnimation animsb = new DoubleAnimation();
            animsb.From = 0;
            animsb.To = 20;
            animsb.Duration = TimeSpan.FromMilliseconds(1800);
            animsb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
            DoubleAnimation animsb2 = new DoubleAnimation();
            animsb2.From = 0;
            animsb2.To = -20;
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
        }
    }
}
