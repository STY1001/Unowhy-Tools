using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Drivers : INavigableView<DashboardViewModel>
{
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
        dlcloud_txt.Text = UT.GetLang("bkcloud");
        dlcloud_desc.Text = UT.GetLang("descbkcloud");
        dlcloud_btn.Text = UT.GetLang("dl");
        bk_txt.Text = UT.GetLang("back");
        bk_desc.Text = UT.GetLang("drvbr");
        bk_btn.Text = UT.GetLang("backup");
        rt_txt.Text = UT.GetLang("rest");
        rt_desc.Text = UT.GetLang("drvbr");
        rt_btn.Text = UT.GetLang("restore");
    }

    public async void Init(object sender, EventArgs e)
    {
        RootStack.Visibility = Visibility.Hidden;

        await UT.waitstatus.open();
        applylang();
        await UT.waitstatus.close();

        RootStack.Visibility = Visibility.Visible;

        foreach (UIElement element in RootStack.Children)
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
                From = 50,
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

    public Drivers(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public void dl_Click(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "https://bit.ly/UTbkcloud",
            UseShellExecute = true
        });
    }
}
