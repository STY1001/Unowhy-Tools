﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Unowhy_Tools;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class PCinfo : INavigableView<DashboardViewModel>
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
        UT.NavigateTo(typeof(Dashboard));
    }

    public async Task applylang()
    {
        labuid.Text = await UT.GetLang("domuser");
        labsys.Text = await UT.GetLang("sysmfmd");
        labsku.Text = "SKU";
        labsn.Text = await UT.GetLang("serial");
        labbb.Text = await UT.GetLang("bbmfmd");
        labbv.Text = await UT.GetLang("biosversion");
        labwv.Text = await UT.GetLang("os");
        labcpu.Text = await UT.GetLang("cpuinfo");
        labram.Text = await UT.GetLang("raminfo");
        labstor.Text = await UT.GetLang("storageinfo");
    }

    public async Task infoapply()
    {
        string realmodel = await UT.GetModelWithSKU(UTdata.sku);
        List<string> defaultosfile = new List<string>()
            {
                "C:\\Windows\\ver.txt",
                "C:\\Windows\\version_gpo.txt",
                "C:\\Windows\\version_master.txt"
            };

        bool defaultos = false;
        foreach (string file in defaultosfile)
        {
            if (File.Exists(file))
            {
                defaultos = true;
            }
        }
        string stockos = "";
        if (defaultos)
        {
            stockos = " (Stock deploy)";
        }
        uid.Text = UTdata.UserID;
        pcn.Text = UTdata.HostName;
        sys.Text = UTdata.mf + " " + UTdata.md;
        bb.Text = UTdata.mbmf + " " + UTdata.mbmd;
        sku.Text = UTdata.sku + " (" + realmodel + ")";
        sn.Text = UTdata.sn;
        bv.Text = UTdata.bios;
        wv.Text = UTdata.os + stockos;
        cpu.Text = UTdata.cpu;
        string sram = UTdata.ram;
        double preram = Convert.ToDouble(sram);
        double postram = preram / (1024 * 1024 * 1024);
        ram.Text = String.Format("{0:0.0} GB", postram);
        if (wv.Text.Contains("11"))
        {
            imgwv.Source = new BitmapImage(new System.Uri("pack://application:,,,/Resources/win11.png"));
        }
        else if (wv.Text.Contains("10"))
        {
            imgwv.Source = new BitmapImage(new System.Uri("pack://application:,,,/Resources/win10.png"));
        }
        Random rand = new Random();
        int random = rand.Next(0, 1000000);
        File.Copy(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Roaming\\Microsoft\\Windows\\Themes\\TranscodedWallpaper", Environment.GetEnvironmentVariable("TEMP") + $"\\TranscodedWallpaper_{random}.jpg");
        string curentbgpath = Environment.GetEnvironmentVariable("TEMP") + $"\\TranscodedWallpaper_{random}.jpg";
        bgimg.Source = new BitmapImage(new System.Uri(curentbgpath));
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        UT.SendAction("OpenPCInfo");
        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in RootStack2.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        bgimg.Visibility = Visibility.Hidden;

        await UT.DeployBack(typeof(Dashboard), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);

        await infoapply();

        DriveInfo drive = new DriveInfo("C");
        double totalSpace = drive.TotalSize / 1024.0 / 1024.0 / 1024.0;
        double freeSpace = drive.AvailableFreeSpace / 1024.0 / 1024.0 / 1024.0;
        double usedSpace = totalSpace - freeSpace;
        int percentFree = (int)((freeSpace / totalSpace) * 100);

        stor.Text = String.Format("{0:0.00} GB / {1:0.00} GB ({2}% {3})", usedSpace, totalSpace, percentFree, await UT.GetLang("free"));

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
        foreach (UIElement element in RootStack2.Children)
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
        bgimg.Visibility = Visibility.Visible;
        {
            var fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.30),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            var zoomAnimation1 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.50),
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut, Power = 5 }
            };

            Storyboard.SetTarget(fadeInAnimation, bgimg);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTarget(zoomAnimation1, bgimg);
            Storyboard.SetTargetProperty(zoomAnimation1, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
            var storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);
            storyboard.Children.Add(zoomAnimation1);
            storyboard.Begin();
        }
    }

    public PCinfo(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
