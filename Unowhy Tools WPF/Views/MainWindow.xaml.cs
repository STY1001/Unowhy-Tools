using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Controls.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Mvvm.Contracts;
using Unowhy_Tools;
using Unowhy_Tools_WPF.Views.Pages;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Xml.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Input;
using System.IO;

namespace Unowhy_Tools_WPF.Views;

/// <summary>
/// Interaction logic for Container.xaml
/// </summary>
public partial class MainWindow : INavigationWindow
{
    UT.Data UTdata = new UT.Data();
    private bool _initialized = false;
    private readonly IThemeService _themeService;
    private readonly ITaskBarService _taskBarService;
    private readonly ISnackbarService _snackbarService;
    private readonly IDialogService _dialogService;

    public async Task applylang()
    {
        home.Content = await UT.GetLang("titlehome");
        hsqm.Content = await UT.GetLang("titlehsqm");
        repair.Content = await UT.GetLang("titlerepair");
        delete.Content = await UT.GetLang("titledelete");
        customize.Content = await UT.GetLang("titlecust");
        drivers.Content = await UT.GetLang("titledrv");
        pcname.Content = await UT.GetLang("titlepcn");
        wre.Content = await UT.GetLang("titlewre");
        adduser.Content = await UT.GetLang("titleadduser");
        adminset.Content = await UT.GetLang("titleadminset");
        about.Content = await UT.GetLang("titleabout");
        drvbk.Content = await UT.GetLang("titledrvbk");
        drvrt.Content = await UT.GetLang("titledrvrt");
        drvconv.Content = await UT.GetLang("titleconv");
        settings.Content = await UT.GetLang("titlesettings");
        pcinfo.Content = await UT.GetLang("titlepci");
        mes.Content = await UT.GetLang("titleedge");
        wds.Content = await UT.GetLang("titlewindef");
    }

    public MainWindow(INavigationService navigationService, IPageService pageService, IThemeService themeService, ITaskBarService taskBarService, ISnackbarService snackbarService, IDialogService dialogService)
    {
        DataContext = this;
        _themeService = themeService;
        _taskBarService = taskBarService;
        InitializeComponent();
        SetPageService(pageService);
        navigationService.SetNavigationControl(RootNavigation);
        snackbarService.SetSnackbarControl(RootSnackbar);
        dialogService.SetDialogControl(RootDialog);

        _snackbarService = snackbarService;
        _dialogService = dialogService;

        //ChangeTheme();
        //ChangeTheme();

        applylang();
        this.KeyDown += MainWindow_KonamiKeyDown;
        this.KeyUp += MainWindow_KonamiKeyUp;
    }

    private void MainWindow_KeyUp(object sender, KeyEventArgs e)
    {
        throw new NotImplementedException();
    }

    #region Dialog and Wait
    public bool ShowDialogQ(string msg, BitmapImage img)
    {
        var result = DialogQRoot.ShowDialog(msg, img);
        if (result)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowDialogI(string msg, BitmapImage img)
    {
        DialogIRoot.ShowDialog(msg, img);
    }

    public async Task ShowWait(string title, string img)
    {
        await WaitRoot.Show(title, img);
    }

    public async Task HideWait()
    {
        await WaitRoot.Hide();
    }
    #endregion

    /// <summary>
    /// Raises the closed event.
    /// </summary>
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        // Make sure that closing this window will begin the process of closing the application.
        System.Windows.Application.Current.Shutdown();
    }

    #region INavigationWindow methods

    public Frame GetFrame()
        => RootFrame;

    public INavigation GetNavigation()
        => RootNavigation;

    public bool Navigate(Type pageType)
        => RootNavigation.Navigate(pageType);

    public void SetPageService(IPageService pageService)
        => RootNavigation.PageService = pageService;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();

    #endregion INavigationWindow methods

    private async void Init(object sender, RoutedEventArgs e)
    {
        await Load();
    }

    #region Back System

    private bool backisdeployed = false;
    private Type backtype = null;
    private Grid backgrid = null;
    private Border backborder = null;

    public async Task DeployDABack()
    {
        back.Click -= GoBack;
        back.Click += GoDABack;

        if (!backisdeployed)
        {
            backisdeployed = true;

            foreach (UIElement elements in RootNavigation.Items)
            {
                DoubleAnimation opacityAnim = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation translateAnim = new DoubleAnimation
                {
                    From = 0,
                    To = -50,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                TranslateTransform trans = new TranslateTransform();
                elements.RenderTransform = trans;

                elements.BeginAnimation(UIElement.OpacityProperty, opacityAnim);
                trans.BeginAnimation(TranslateTransform.XProperty, translateAnim);

                await Task.Delay(50);

                elements.Visibility = Visibility.Hidden;
                back.Visibility = Visibility.Collapsed;
            }

            foreach (UIElement elements in RootNavigation.Items)
            {
                elements.Visibility = Visibility.Collapsed;
            }

            back.Visibility = Visibility.Visible;

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
            back.RenderTransform = transform;

            back.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }
    }

    public async void GoDABack(object sender, RoutedEventArgs eventArgs)
    {
        await About.DABack();
    }

    public async Task DeployBack(Type type, Grid grid, Border border)
    {
        back.Click += GoBack;
        back.Click -= GoDABack;

        backtype = type;
        backgrid = grid;
        backborder = border;

        if (!backisdeployed)
        {
            backisdeployed = true;

            foreach (UIElement elements in RootNavigation.Items)
            {
                DoubleAnimation opacityAnim = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation translateAnim = new DoubleAnimation
                {
                    From = 0,
                    To = -50,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                TranslateTransform trans = new TranslateTransform();
                elements.RenderTransform = trans;

                elements.BeginAnimation(UIElement.OpacityProperty, opacityAnim);
                trans.BeginAnimation(TranslateTransform.XProperty, translateAnim);

                await Task.Delay(50);

                elements.Visibility = Visibility.Hidden;
                back.Visibility = Visibility.Collapsed;
            }

            foreach (UIElement elements in RootNavigation.Items)
            {
                elements.Visibility = Visibility.Collapsed;
            }

            back.Visibility = Visibility.Visible;

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
            back.RenderTransform = transform;

            back.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }
    }

    public async Task UnDeployBack()
    {
        back.Click -= GoBack;
        back.Click -= GoDABack;

        if (backisdeployed)
        {
            backisdeployed = false;

            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = -50,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            back.RenderTransform = transform;

            back.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);

            await Task.Delay(500);

            foreach (UIElement elements in RootNavigation.Items)
            {
                elements.Visibility = Visibility.Visible;
                back.Visibility = Visibility.Collapsed;

                DoubleAnimation opacityAnim = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation translateAnim = new DoubleAnimation
                {
                    From = -50,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                TranslateTransform trans = new TranslateTransform();
                elements.RenderTransform = trans;

                elements.BeginAnimation(UIElement.OpacityProperty, opacityAnim);
                trans.BeginAnimation(TranslateTransform.XProperty, translateAnim);

                await Task.Delay(50);
            }
        }
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        UT.anim.BorderZoomIn2(backborder);
        DoubleAnimation translateAnim = new DoubleAnimation
        {
            From = 0,
            To = -10,
            Duration = TimeSpan.FromSeconds(0.2),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        TranslateTransform trans = new TranslateTransform();
        back.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, translateAnim);
        await System.Threading.Tasks.Task.Delay(200);
        DoubleAnimation translateAnim2 = new DoubleAnimation
        {
            From = -10,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.2),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        TranslateTransform trans2 = new TranslateTransform();
        back.RenderTransform = trans2;
        trans2.BeginAnimation(TranslateTransform.XProperty, translateAnim2);
        await System.Threading.Tasks.Task.Delay(200);
        UT.NavigateTo(backtype);
        await UT.anim.AnimParent("zoomin");
    }

    #endregion

    private async void NavClick(object sender, RoutedEventArgs e)
    {
        /*
        DoubleAnimation opacityAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.1),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };

        DoubleAnimation translateAnimation = new DoubleAnimation
        {
            From = 150,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.1),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };

        TranslateTransform transform = new TranslateTransform();
        RootFrame.RenderTransform = transform;
        RootFrame.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        */
    }

    #region Splash Bar system

    public static async Task USSwB(string status)
    {
        var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
        await mainWindow.updatesplashstatus(status, false);
    }

    public static async Task USS(string status)
    {
        var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
        await mainWindow.updatesplashstatus(status, true);
    }

    public int totalstep = 57;
    public int actualstep = 0;

    public async Task updatesplashstatus(string status, bool bar)
    {
        SplashText.Text = status;

        if (bar)
        {
            SplashBar.IsIndeterminate = false;
            SplashPercent.Visibility = Visibility.Visible;
            actualstep++;
            SplashBar.Value = (100 * actualstep) / totalstep;
            SplashPercent.Text = ((100 * actualstep) / totalstep).ToString() + "%";
        }
        else
        {
            SplashBar.IsIndeterminate = true;
            SplashPercent.Visibility = Visibility.Hidden;
        }
    }

    #endregion

    private async Task Load()
    {
        if (UT.version.isdeb())
        {
            RootNavigation.Visibility = Visibility.Collapsed;
            await Task.Delay(150);
            await _snackbarService.ShowAsync("Warning, Take note !", "You are using a debug version of Unowhy Tools, this debug version might be bugged", SymbolRegular.Edit32, ControlAppearance.Danger);
            await Task.Delay(150);
            RootNavigation.Visibility = Visibility.Visible;
            RootMainGrid.Visibility = Visibility.Collapsed;
            RootTitleBar.Visibility = Visibility.Collapsed;
        }

        if (!UT.version.isdeb())
        {
            RootMainGrid.Visibility = Visibility.Collapsed;
            RootTitleBar.Visibility = Visibility.Collapsed;
            await Task.Delay(1000);
        }


        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 1000;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        DoubleAnimation animopac = new DoubleAnimation();
        animopac.From = 0;
        animopac.To = 1;
        animopac.Duration = TimeSpan.FromMilliseconds(500);
        animopac.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        //RootWelcomeGrid.RenderTransform = trans;
        //RootWelcomeGrid.BeginAnimation(UIElement.OpacityProperty, animopac);
        //trans.BeginAnimation(TranslateTransform.XProperty, anim);

        RootWelcomeGrid.Visibility = Visibility.Visible;

        Visibility = Visibility.Visible;
        var fadeInAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.30),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var zoomAnimation1 = new DoubleAnimation
        {
            From = 1.1,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.50),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var zoomAnimation2 = new DoubleAnimation
        {
            From = 1.1,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.50),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        Storyboard.SetTarget(fadeInAnimation, RootWelcomeGrid);
        Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(OpacityProperty));
        Storyboard.SetTarget(zoomAnimation1, RootWelcomeGrid);
        Storyboard.SetTarget(zoomAnimation2, RootWelcomeGrid);
        Storyboard.SetTargetProperty(zoomAnimation1, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
        Storyboard.SetTargetProperty(zoomAnimation2, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
        var storyboard = new Storyboard();
        storyboard.Children.Add(fadeInAnimation);
        storyboard.Children.Add(zoomAnimation1);
        storyboard.Children.Add(zoomAnimation2);
        storyboard.Begin();

        anim = new DoubleAnimation();
        anim.From = -1000;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(1000);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        trans = new TranslateTransform();
        SplashImg.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.YProperty, anim);

        anim = new DoubleAnimation();
        anim.From = -1000;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(1000);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        trans = new TranslateTransform();
        SplashDesc.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        anim = new DoubleAnimation();
        anim.From = 1000;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(1000);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        trans = new TranslateTransform();
        SplashCredit.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        anim = new DoubleAnimation();
        anim.From = 1000;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(1000);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        trans = new TranslateTransform();
        SplashBar.RenderTransform = trans;
        SplashText.RenderTransform = trans;
        SplashPercent.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.YProperty, anim);

        string ver = "Version " + UT.version.getverfull().ToString().Insert(2, ".") + " (Build " + UT.version.getverbuild().ToString() + ") ";
        if (UT.version.isdeb()) ver = ver + "(Debug)";
        else ver = ver + "(Release)";
        SplashVer.Text = ver;

        anim = new DoubleAnimation();
        anim.From = 1000;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
        trans = new TranslateTransform();
        SplashVer.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.YProperty, anim);


        if (!UT.CheckAdmin())
        {
            SplashText.Text = "Restarting as admin...";
            SplashBar.Value = 100;
            await Task.Delay(500);

            anim.From = 0;
            anim.To = -10000;
            anim.Duration = TimeSpan.FromMilliseconds(500);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
            RootWelcomeGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);

            UT.RunAdmin($"-user {UTdata.UserID}");
        }
        else
        {
            SplashText.Text = "Hi !";
            await Task.Delay(1500);

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

            await USS("Loading... (Updating version in reg)");
            string regver;
            if (UT.version.isdeb()) regver = "Debug";
            else regver = "Release";
            UT.Write2Log("Updating version in registry: " + regver);
            await UT.RunMin("reg", $"add \"HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\UnowhyTools\" /v \"DisplayVersion\" /t REG_SZ /d \"{regver}\" /f");

            await USS("Loading... (Checking WD Exclusion)");
            UT.Write2Log("Checking Unowhy Tools in Windows Defender exclusion list");
            string expath = await UT.RunReturn("powershell", "\"Get-MpPreference | Select-Object ExclusionPath\"");
            if (!expath.Contains("C:\\Program Files (x86)\\Unowhy Tools"))
            {
                await USSwB("Loading... (Adding WD Exclusion)");
                UT.Write2Log("Unowhy Tools isn't in Windows Defender exclusion list, adding....");
                await UT.RunMin("powershell", "Add-MpPreference -ExclusionPath 'C:\\Program Files (x86)\\Unowhy Tools'");
            }
            else
            {
                UT.Write2Log("Unowhy Tools is in Windows Defender exclusion list");
            }

            await USS("Loading... (Online Data)");
            if (File.Exists(UT.utpath + "\\Unowhy Tools\\settings.json"))
            {
                UT.online_datas = await UT.Config.Get("OnlineData");
            }

            await USS("Loading... (Cleaning)");
            await UT.Cleanup();
            await USS("Loading... (Checking Files)");
            bool fs = await UT.FirstStart();
            await USS("Loading... (Checking System)");
            await UT.Check();
            await USS("Loading... (Checking Unowhy Tools Service)");
            await UT.UTS.UTScheck();
            await USS("Loading... (Tray)");
            await UT.TrayCheck();
            await USS("Loading... (Preloading pages)");
            await USS("Preloading pages... (Dashboard)");
            Navigate(typeof(Dashboard));
            await USS("Preloading pages... (Settings)");
            Navigate(typeof(Settings));
            await USS("Preloading pages... (About)");
            Navigate(typeof(About));
            await USS("Preloading pages... (Updater)");
            Navigate(typeof(Updater));
            await USS("Preloading pages... (HisqoolManager)");
            Navigate(typeof(HisqoolManager));
            await USS("Preloading pages... (Repair)");
            Navigate(typeof(Repair));
            await USS("Preloading pages... (Delete)");
            Navigate(typeof(Delete));
            await USS("Preloading pages... (Customize)");
            Navigate(typeof(Customize));
            await USS("Preloading pages... (Drivers)");
            Navigate(typeof(Drivers));
            await Task.Delay(500);
            animsb = new DoubleAnimation();
            animsb.From = 20;
            animsb.To = 0;
            animsb.Duration = TimeSpan.FromMilliseconds(1000);
            animsb.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
            animsb2 = new DoubleAnimation();
            animsb2.From = -20;
            animsb2.To = 0;
            animsb2.Duration = TimeSpan.FromMilliseconds(1000);
            animsb2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
            transsb = new TranslateTransform();
            transsb2 = new TranslateTransform();
            SB1.RenderTransform = transsb;
            SB2.RenderTransform = transsb2;
            transsb.BeginAnimation(TranslateTransform.XProperty, animsb);
            transsb.BeginAnimation(TranslateTransform.YProperty, animsb2);
            transsb2.BeginAnimation(TranslateTransform.XProperty, animsb2);
            transsb2.BeginAnimation(TranslateTransform.YProperty, animsb);

            SplashText.Text = "Ready !";
            SplashBar.Value = 100;
            SplashPercent.Text = "100%";
            await Task.Delay(500);

            RootNavigation.TransitionType = Wpf.Ui.Animations.TransitionType.None;
            foreach (UIElement elements in RootNavigation.Items)
            {
                elements.Visibility = Visibility.Hidden;
            }

            await Task.Delay(500);

            anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = -1000;
            anim.Duration = TimeSpan.FromMilliseconds(1000);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
            trans = new TranslateTransform();
            SplashImg.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.YProperty, anim);

            anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = -1000;
            anim.Duration = TimeSpan.FromMilliseconds(1000);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
            trans = new TranslateTransform();
            SplashDesc.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);

            anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = 1000;
            anim.Duration = TimeSpan.FromMilliseconds(1000);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
            trans = new TranslateTransform();
            SplashCredit.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);

            anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = 1000;
            anim.Duration = TimeSpan.FromMilliseconds(1000);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
            trans = new TranslateTransform();
            SplashBar.RenderTransform = trans;
            SplashText.RenderTransform = trans;
            SplashPercent.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.YProperty, anim);

            anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = 1000;
            anim.Duration = TimeSpan.FromMilliseconds(500);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseIn, Power = 5 };
            trans = new TranslateTransform();
            SplashVer.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.YProperty, anim);

            await Task.Delay(250);

            fadeInAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.50),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            zoomAnimation1 = new DoubleAnimation
            {
                From = 1,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.30),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            zoomAnimation2 = new DoubleAnimation
            {
                From = 1,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.30),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(fadeInAnimation, RootWelcomeGrid);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(OpacityProperty));
            Storyboard.SetTarget(zoomAnimation1, RootWelcomeGrid);
            Storyboard.SetTarget(zoomAnimation2, RootWelcomeGrid);
            Storyboard.SetTargetProperty(zoomAnimation1, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(zoomAnimation2, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
            storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);
            storyboard.Children.Add(zoomAnimation1);
            storyboard.Children.Add(zoomAnimation2);
            storyboard.Begin();

            await Task.Delay(500);

            RootWelcomeGrid.Visibility = Visibility.Hidden;
            RootTitleBar.Visibility = Visibility.Visible;
            RootMainGrid.Visibility = Visibility.Visible;

            anim = new DoubleAnimation();
            anim.From = -50;
            anim.To = 0;
            anim.Duration = TimeSpan.FromMilliseconds(1000);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
            trans = new TranslateTransform();
            RootTitleBar.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.YProperty, anim);

            fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.30),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            zoomAnimation1 = new DoubleAnimation
            {
                From = 0.9,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.50),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            zoomAnimation2 = new DoubleAnimation
            {
                From = 0.9,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.50),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(fadeInAnimation, RootMainGrid);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(OpacityProperty));
            Storyboard.SetTarget(zoomAnimation1, RootMainGrid);
            Storyboard.SetTarget(zoomAnimation2, RootMainGrid);
            Storyboard.SetTargetProperty(zoomAnimation1, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(zoomAnimation2, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
            storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);
            storyboard.Children.Add(zoomAnimation1);
            storyboard.Children.Add(zoomAnimation2);
            storyboard.Begin();

            if (fs)
            {
                RootNavigation.Visibility = Visibility.Collapsed;
                Navigate(typeof(FirstConfig));
            }
            else
            {
                Navigate(typeof(Dashboard));

                backisdeployed = false;
                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation translateAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = -50,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                TranslateTransform transform = new TranslateTransform();
                back.RenderTransform = transform;

                back.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);

                await Task.Delay(500);

                foreach (UIElement elements in RootNavigation.Items)
                {
                    elements.Visibility = Visibility.Visible;
                    back.Visibility = Visibility.Collapsed;

                    DoubleAnimation opacityAnim = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.5),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                    };

                    DoubleAnimation translateAnim = new DoubleAnimation
                    {
                        From = -50,
                        To = 0,
                        Duration = TimeSpan.FromSeconds(0.5),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                    };

                    TranslateTransform trans2 = new TranslateTransform();
                    elements.RenderTransform = trans2;

                    elements.BeginAnimation(UIElement.OpacityProperty, opacityAnim);
                    trans2.BeginAnimation(TranslateTransform.XProperty, translateAnim);

                    await Task.Delay(50);
                }
                await Task.Delay(1000);
                RootNavigation.TransitionType = Wpf.Ui.Animations.TransitionType.None;
            }

            await UT.SendStats("normal");
            UT.Write2Log("\n\n\nReady !\n");
        }
    }

    private List<Key> konamiDebugKey = new List<Key>
    {
        Key.U,
        Key.T,
        Key.Up,
        Key.Up,
        Key.Down,
        Key.Down,
        Key.Left,
        Key.Right,
        Key.Left,
        Key.Right,
        Key.A,
        Key.B,
        Key.Enter
    };

    private int konamiDebugKeyIndex = 0;
    private bool konamiCtrlDown = false;
    private async void MainWindow_KonamiKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
        {
            konamiCtrlDown = true;
        }
        else if (konamiCtrlDown && e.Key == konamiDebugKey[konamiDebugKeyIndex])
        {
            konamiDebugKeyIndex++;

            if (konamiDebugKeyIndex == 2)
            {
                await ViewKonamiDebug();
            }

            if (konamiDebugKeyIndex == konamiDebugKey.Count)
            {
                await ViewKonamiDebugKey(konamienter, "none");

                await KonamiOpenDebug();

                konamiDebugKeyIndex = 0;
            }
        }
        else if(!konamiCtrlDown && e.Key == konamiDebugKey[konamiDebugKeyIndex] && konamiDebugKeyIndex > 1)
        {
            konamiDebugKeyIndex++;

            if (konamiDebugKeyIndex == 3)
            {
                await ViewKonamiDebugKey(konamiup1, "up");
            }
            if (konamiDebugKeyIndex == 4)
            {
                await ViewKonamiDebugKey(konamiup2, "up");
            }
            if (konamiDebugKeyIndex == 5)
            {
                await ViewKonamiDebugKey(konamidown1, "down");
            }
            if (konamiDebugKeyIndex == 6)
            {
                await ViewKonamiDebugKey(konamidown2, "down");
            }
            if (konamiDebugKeyIndex == 7)
            {
                await ViewKonamiDebugKey(konamileft1, "left");
            }
            if (konamiDebugKeyIndex == 8)
            {
                await ViewKonamiDebugKey(konamiright1, "right");
            }
            if (konamiDebugKeyIndex == 9)
            {
                await ViewKonamiDebugKey(konamileft2, "left");
            }
            if (konamiDebugKeyIndex == 10)
            {
                await ViewKonamiDebugKey(konamiright2, "right");
            }
            if (konamiDebugKeyIndex == 11)
            {
                await ViewKonamiDebugKey(konamia, "none");
            }
            if (konamiDebugKeyIndex == 12)
            {
                await ViewKonamiDebugKey(konamib, "none");
            }

            if (konamiDebugKeyIndex == konamiDebugKey.Count)
            {
                await ViewKonamiDebugKey(konamienter, "none");

                await KonamiOpenDebug();

                konamiDebugKeyIndex = 0;
            }
        }
        else
        {
            await RemoveKonamiDebug();
            konamiDebugKeyIndex = 0;
            konamiCtrlDown = false;
        }
    }

    private async void MainWindow_KonamiKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
        {
            konamiCtrlDown = false;
        }
    }

    private async Task ViewKonamiDebug()
    {
        konamiText2.Visibility = Visibility.Hidden;
        foreach (UIElement element in konamiKeyGrid.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        RootKonamiDebug.Visibility = Visibility.Visible;
        DoubleAnimation opacityAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.5),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        RootKonamiDebug.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        RootKonamiDebug.Focus();
    }

    private async Task ViewKonamiDebugKey(UIElement element, string direction)
    {
        if (direction == "none")
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
                From = 0,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;

            element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }
        if (direction == "up")
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
        }
        if (direction == "down")
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
                From = -10,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;

            element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }
        if (direction == "right")
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
                From = -10,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;

            element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }
        if (direction == "left")
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
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }

        await Task.Delay(100);
        RootKonamiDebug.Focus();
    }

    private async Task RemoveKonamiDebug()
    {
        DoubleAnimation opacityAnimation = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.5),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        RootKonamiDebug.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        await Task.Delay(500);
        RootKonamiDebug.Visibility = Visibility.Collapsed;
    }

    private async Task KonamiOpenDebug()
    {
        foreach (UIElement element in konamiKeyGrid.Children)
        {
            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 15,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }

        await Task.Delay(100);

        foreach (UIElement element in konamiKeyGrid.Children)
        {
            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 15,
                To = -15,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
            await Task.Delay(50);
        }

        await Task.Delay(100);

        foreach (UIElement element in konamiKeyGrid.Children)
        {
            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = -15,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }

        konamiText2.Visibility = Visibility.Visible;
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = -15,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            TranslateTransform transform = new TranslateTransform();
            konamiText2.RenderTransform = transform;
            konamiText2.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
        }

        await Task.Delay(1000);
        await RemoveKonamiDebug();
        UT.NavigateTo(typeof(DebugPage));
    }

    #region Old Stuff
    public void ChangeTheme()
    {
        _themeService.SetTheme(_themeService.GetTheme() == ThemeType.Dark ? ThemeType.Light : ThemeType.Dark);
    }

    private void TrayMenuItem_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void RootNavigation_OnNavigated(INavigation sender, RoutedNavigationEventArgs e)
    {

    }
    #endregion
}

