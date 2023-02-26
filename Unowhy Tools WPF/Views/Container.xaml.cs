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
        home.Content = UT.GetLang("titlehome");
        hsqm.Content = UT.GetLang("titlehsqm");
        repair.Content = UT.GetLang("titlerepair");
        delete.Content = UT.GetLang("titledelete");
        customize.Content = UT.GetLang("titlecust");
        drivers.Content = UT.GetLang("titledrv");
        pcname.Content = UT.GetLang("titlepcn");
        wre.Content = UT.GetLang("titlewre");
        adduser.Content = UT.GetLang("titleadduser");
        adminset.Content = UT.GetLang("titleadminset");
        about.Content = UT.GetLang("titleabout");
        drvbk.Content = UT.GetLang("titledrvbk");
        drvrt.Content = UT.GetLang("titledrvrt");
        drvconv.Content = UT.GetLang("titleconv");
        settings.Content = UT.GetLang("titlesettings");
        pcinfo.Content = UT.GetLang("titlepci");
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

    public async Task ShowWait()
    {
        await WaitRoot.Show();
    }

    public async Task HideWait()
    {
        await WaitRoot.Hide();
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

        SplashText.Text = "Preparing...";
        await Task.Delay(10);
        bool fs = await UT.FirstStart();
        await UT.Check();
        SplashText.Text = "Welcome";
        await Task.Delay(10);

        RootWelcomeGrid.Visibility = Visibility.Hidden;
        RootMainGrid.Visibility = Visibility.Visible;

        Navigate(typeof(Dashboard));
        if (fs)
        {
            UT.DialogIShow("Hello, select your language and click \"OK\" \n\nBonjour, séléctionner votre langue et cliquer sur \"OK\"", "hi.png");
            Navigate(typeof(Settings));
        }
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

