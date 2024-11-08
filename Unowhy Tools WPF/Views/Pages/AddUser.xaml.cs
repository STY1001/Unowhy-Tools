﻿using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Unowhy_Tools;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class AddUser : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        //UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        //UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(Customize));
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(Customize), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);

        foreach (UIElement element in RootStack.Children)
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

    public async Task applylang()
    {
        labu.Text = await UT.GetLang("name");
        labp.Text = await UT.GetLang("password");
        labc.Text = await UT.GetLang("confpw");
        laba.Text = await UT.GetLang("la");
        labb.Text = await UT.GetLang("create");
    }

    public AddUser(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public void pass_Changed(object sender, RoutedEventArgs e)
    {
        if (passbox1.Text == "")
        {
            pimg1.Source = UT.GetImgSource("passno.png");
        }
        else
        {
            pimg1.Source = UT.GetImgSource("passyes.png");
        }

        if (passbox1.Text == passbox2.Text)
        {
            pimg2.Source = UT.GetImgSource("pwcyes.png");
        }
        else
        {
            pimg2.Source = UT.GetImgSource("pwcno.png");
        }
    }

    public void name_Changed(object sender, RoutedEventArgs e)
    {
        Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""]");

        if (r.IsMatch(namebox.Text.ToString().Trim()) || namebox.Text == "" || namebox.Text.Contains(" "))
        {
            uimg1.Source = UT.GetImgSource("deluser.png");
        }
        else
        {
            uimg1.Source = UT.GetImgSource("user.png");
        }
    }

    public async void Create_Changed(object sender, RoutedEventArgs e)
    {
        UT.SendAction("AddUser");
        Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""]");

        if (r.IsMatch(namebox.Text.ToString().Trim()) || namebox.Text == "" || namebox.Text.Contains(" "))
        {
            UT.DialogIShow(await UT.GetLang("an"), "no.png");
        }
        else
        {
            if (passbox1.Text == passbox2.Text)
            {
                if (UT.DialogQShow(await UT.GetLang("adduser"), "adduser.png"))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.add"), "adduser.png");

                    if (passbox1.Text == "")
                    {
                        await UT.RunMin("net", $"user \"{namebox.Text}\" /add");

                        if (acheck.IsChecked == true)
                        {
                            await UT.RunMin("net", $"localgroup {UTdata.AdminsName} \"{namebox.Text}\" /add");
                        }
                    }
                    else
                    {
                        await UT.RunMin("net", $"user \"{namebox.Text}\" \"{passbox1.Text}\" /add");

                        if (acheck.IsChecked == true)
                        {
                            await UT.RunMin("net", $"localgroup {UTdata.AdminsName} \"{namebox.Text}\" /add");
                        }
                    }

                    await UT.waitstatus.close();

                    UT.DialogIShow(await UT.GetLang("adduser.id") + $": \".\\{namebox.Text}\"", "user.png");
                }
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("pwmis"), "no.png");
            }
        }
    }
}
