using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Controls.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.TaskBar;
using Unowhy_Tools_WPF.Services;

using Unowhy_Tools;
using Wpf.Ui.Mvvm.Interfaces;
using Wpf.Ui.Mvvm.Services;
using Unowhy_Tools_WPF.Views.Pages;
using System.Windows.Controls.Primitives;
using Unowhy_Tools_WPF.Views.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Unowhy_Tools_WPF.Views;

/// <summary>
/// Interaction logic for Container.xaml
/// </summary>
public partial class Container : INavigationWindow
{
    UT.Data UTdata = new UT.Data();
    private bool _initialized = false;

    private readonly IThemeService _themeService;

    private readonly ITaskBarService _taskBarService;

    public ContainerViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        home.Content = UT.getlang("titlehome");
        hsqm.Content = UT.getlang("titlehsqm");
        repair.Content = UT.getlang("titlerepair");
        delete.Content = UT.getlang("titledelete");
        customize.Content = UT.getlang("titlecust");
        drivers.Content = UT.getlang("titledrv");
        pcname.Content = UT.getlang("titlepcn");
        wre.Content = UT.getlang("titlewre");
        adduser.Content = UT.getlang("titleadduser");
        adminset.Content = UT.getlang("titleadminset");
        about.Content = UT.getlang("titleabout");
        drvbk.Content = UT.getlang("titledrvbk");
        drvrt.Content = UT.getlang("titledrvrt");
        drvconv.Content = UT.getlang("titleconv");
        settings.Content = UT.getlang("titlesettings");
        pcinfo.Content = UT.getlang("titlepci");
    }

    public Container(ContainerViewModel viewModel, INavigationService navigationService, IPageService pageService, IThemeService themeService, ITaskBarService taskBarService, ISnackbarService snackbarService, IDialogService dialogService)
    {
        ViewModel = viewModel;
        DataContext = this;
        _themeService = themeService;
        _taskBarService = taskBarService;
        InitializeComponent();
        SetPageService(pageService);
        navigationService.SetNavigationControl(RootNavigation);
        snackbarService.SetSnackbarControl(RootSnackbar);
        dialogService.SetDialogControl(RootDialog);

        _themeService.SetTheme(_themeService.GetTheme() == ThemeType.Dark ? ThemeType.Light : ThemeType.Dark);
        _themeService.SetTheme(_themeService.GetTheme() == ThemeType.Dark ? ThemeType.Light : ThemeType.Dark);

        applylang();
    }

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

    public void ShowWait()
    {
        WaitRoot.Show();
    }

    public void HideWait()
    {
        WaitRoot.Hide();
    }

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

    private async Task Load()
    {
        RootMainGrid.Visibility = Visibility.Collapsed;
        RootWelcomeGrid.Visibility = Visibility.Visible;

        await Task.Delay(1000);

        Navigate(typeof(HisqoolManager));
        SplashText.Text = "Loading panels (Customize)...";
        SplashBar.Value = 25;
        await Task.Delay(100);
        Navigate(typeof(Customize));
        SplashText.Text = "Loading panels (Delete)...";
        SplashBar.Value = 35;
        await Task.Delay(100);
        Navigate(typeof(Delete));
        SplashText.Text = "Loading panels (Repair)...";
        SplashBar.Value = 50;
        await Task.Delay(100);
        Navigate(typeof(Repair));
        SplashText.Text = "Loading panels (Drivers)...";
        SplashBar.Value = 75;
        await Task.Delay(100);
        Navigate(typeof(Drivers));
        SplashText.Text = "Welcome";
        SplashBar.Value = 100;
        await Task.Delay(100);
        UT.check();
        RootWelcomeGrid.Visibility = Visibility.Hidden;
        RootMainGrid.Visibility = Visibility.Visible;

        Navigate(typeof(Dashboard));
    }

    private void NavigationButtonTheme_OnClick(object sender, RoutedEventArgs e)
    {
        _themeService.SetTheme(_themeService.GetTheme() == ThemeType.Dark ? ThemeType.Light : ThemeType.Dark);
    }

    private void TrayMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem)
            return;

        System.Diagnostics.Debug.WriteLine($"DEBUG | WPF UI Tray clicked: {menuItem.Tag}", "Unowhy_Tools_WPF");
    }

    private void RootNavigation_OnNavigated(INavigation sender, RoutedNavigationEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"DEBUG | WPF UI Navigated to: {sender?.Current ?? null}", "Unowhy_Tools_WPF");

        // This funky solution allows us to impose a negative
        // margin for Frame only for the Dashboard page, thanks
        // to which the banner will cover the entire page nicely.
        RootFrame.Margin = new Thickness(
            left: 0,
            top: sender?.Current?.PageTag == "" ? -69 : 0,
            right: 0,
            bottom: 0);
    }
}

