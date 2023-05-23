using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class AdminUser : INavigableView<DashboardViewModel>
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
        UT.NavigateTo(typeof(Customize));
    }

    public void applylang()
    {
        snpw.Text = UT.GetLang("snpw");
        en.Text = UT.GetLang("enablea");
        dis.Text = UT.GetLang("disablea");
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootGrid2.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(Customize), RootGrid);

        await Task.Delay(150);

        foreach (UIElement element in RootGrid2.Children)
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

            await Task.Delay(50);
        }
    }

    public AdminUser(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public async void Pass_Click(object sender, RoutedEventArgs e)
    {
        if(UT.DialogQShow(UT.GetLang("snpw"), "key.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("net", $"user {UTdata.AdminName} \"{passbox.Text}\"");
            await UT.waitstatus.close();
        }
    }
    
    public async void Ena_Click(object sender, RoutedEventArgs e)
    {
        if(UT.DialogQShow(UT.GetLang("enablea"), "enable.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("net", $"user {UTdata.AdminName} /active:yes");
            await UT.waitstatus.close();
        }
    }
    
    public async void Dis_Click(object sender, RoutedEventArgs e)
    {
        if(UT.DialogQShow(UT.GetLang("disablea"), "disable.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("net", $"user {UTdata.AdminName} /active:no");
            await UT.waitstatus.close();
        }
    }
}
