using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Threading.Tasks;
using Unowhy_Tools_WPF.Views;
using System.Windows;
using System.Diagnostics;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;
using Unowhy_Tools;
using System.Windows.Media;
using Microsoft.Win32;
using System;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class About : INavigableView<DashboardViewModel>
{
    private readonly ISnackbarService _snackbarService;

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void GoForw(object sender, RoutedEventArgs e)
    {
        UT.anim.TransitionForw(RootGrid);
    }

    public void applylang()
    {
        ubtnlab.Text = UT.GetLang("udcheck");
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        string ver = "Version " + UT.version.getverfull().ToString().Insert(2, ".") + " (Build " + UT.version.getverbuild().ToString() + ") ";

        if (UT.version.isdeb()) ver = ver + "(Debug)";
        else ver = ver + "(Release)";

        verlab.Text = ver;

        if (UT.CheckInternet())
        {
            RegistryKey lcs = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utcuab = lcs.GetValue("UpdateStart").ToString();
            if (utcuab == "1")
            {
                ubtnlab.Text = UT.GetLang("update.check");
                if (await UT.version.newver())
                {
                    Color white = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                    Color gray = (Color)ColorConverter.ConvertFromString("#bebebe");
                    ubtnlab.Text = UT.GetLang("newver");
                    ubtnlab.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    ubtnlab.Foreground = new SolidColorBrush(white);
                }
                else
                {
                    ubtnlab.Text = UT.GetLang("udcheck");
                }
            }
        }
        else
        {
            ubtnlab.Text = UT.GetLang("udcheck");
        }
    }

    public async void InitAnim(object sender, System.Windows.RoutedEventArgs e)
    {
        foreach (UIElement element in utlabs.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        foreach (UIElement element in OpGrid.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

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
                From = 150,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;

            element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);

            await Task.Delay(50);
        }

        foreach (UIElement element in OpGrid.Children)
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
                From = 150,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;

            element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);

            await Task.Delay(50);
        }
    }

    public About(DashboardViewModel viewModel, ISnackbarService snackbarService)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }

    public void Github_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "https://github.com/STY1001/Unowhy-Tools",
                        UseShellExecute = true
        });
    }

    public void STY1001_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "https://sty1001.fr",
            UseShellExecute = true
        });
    }

    public void Discord_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "https://discord.com/invite/dw3ZJ9u7WS",
            UseShellExecute = true
        });
    }
}
