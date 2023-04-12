using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class PCname : INavigableView<DashboardViewModel>
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
        labold.Text = UT.GetLang("ancat");
        labnew.Text = UT.GetLang("cncat");
        pcn_btn.Text = UT.GetLang("change");
    }

    private void CheckBTN()
    {
        old.Text = UTdata.HostName;
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootGrid2.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        UT.anim.BackBtnAnimForw(BackBTN);

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

    public PCname(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
        CheckBTN();
    }

    public async void Change_Click(object sender, RoutedEventArgs e)
    {
        Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""]");

        if (r.IsMatch(newbox.Text.ToString().Trim()) || newbox.Text.Length < 2 || newbox.Text.Length > 15 || newbox.Text.Contains(" "))
        {
            UT.DialogIShow(UT.GetLang("avert"), "no.png");
        }
        else
        {
            if (UT.DialogQShow(UT.GetLang("pcname"), "customize.png"))
            {
                await UT.waitstatus.open();
                string name = newbox.Text;
                string arg = ($"-Command \"& {{Rename-Computer -NewName \"{name}\" -Force}}\"");

                await UT.RunMin("powershell", arg);
                old.Text = newbox.Text;
                await UT.waitstatus.close();
                UT.DialogIShow(UT.GetLang("rebootmsg"), "reboot.png");
                Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
            }
        }
    }
}
