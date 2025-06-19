﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Unowhy_Tools;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class DrvRest : INavigableView<DashboardViewModel>
{
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
        UT.NavigateTo(typeof(Drivers));
    }

    public async void GoForw(object sender, RoutedEventArgs e)
    {
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = 300;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        convbtn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);

        //UT.anim.RegisterParent(RootGrid, RootBorder);
        UT.anim.BorderZoomIn2(RootBorder);
        await Task.Delay(500);
        UT.NavigateTo(typeof(DrvConv));

        anim.From = 0;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(500);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        convbtn.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
    }

    public async Task applylang()
    {
        labconv.Text = await UT.GetLang("convdesc");
        convbtn.Content = await UT.GetLang("conv");
        labpath.Text = await UT.GetLang("bkpath");
        rest_btn.Text = await UT.GetLang("restore");
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(Drivers), RootGrid, RootBorder);
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

    public DrvRest(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public void Browse_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new OpenFileDialog())
        {
            fb.DefaultExt = "zip";
            fb.Filter = "Unowhy Tools Driver file (*.zip)|*.zip";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                rtpath.Text = fb.FileName;
            }
        }
    }

    public async void Restore_Click(object sender, RoutedEventArgs e)
    {
        if (rtpath.Text == "")
        {

        }
        else
        {
            UT.SendAction("DrvRestore");
            DriveInfo di = new DriveInfo("C");
            FileInfo fi = new FileInfo(rtpath.Text);
            if (di.AvailableFreeSpace > fi.Length * 3)
            {
                await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
                await Task.Delay(1000);
                string rttemps = UT.utpath + "\\Unowhy Tools\\Temps\\Drivers";
                string source = rtpath.Text;
                string dest = rttemps;
                await Task.Run(() =>
                {
                    ZipFile.ExtractToDirectory(source, dest, true);
                });
                await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
                if (File.Exists(rttemps + "\\UT-Restore.exe"))
                {
                    List<string> list = new List<string>();
                    foreach (string filePath in Directory.GetFiles(rttemps, "*.inf"))
                    {
                        list.Add(filePath);
                    }
                    foreach (string subDirectory in Directory.GetDirectories(rttemps))
                    {
                        foreach (string filePath in Directory.GetFiles(subDirectory, "*.inf"))
                        {
                            list.Add(filePath);
                        }
                    }

                    string result = list.Count.ToString();
                    int status = 0;

                    foreach (string filePath in list)
                    {
                        status++;

                        string percentage = ((status * 100) / list.Count).ToString("##0") + " %";

                        await UT.waitstatus.open(await UT.GetLang("wait.restore") + " (" + percentage + ")", "download.png");

                        await Task.Run(() =>
                        {
                            var process = new Process();
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.FileName = "pnputil.exe";
                            process.StartInfo.Arguments = "/add-driver \"" + filePath + "\" /install";
                            process.Start();
                            process.WaitForExit();
                            process.Dispose();
                        });
                    }

                    /*await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = rttemps + "\\UT-Restore.exe";
                        p.StartInfo.WorkingDirectory = rttemps;
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.WaitForExit();
                    });*/

                }
                else
                {
                    await UT.waitstatus.close();
                    UT.DialogIShow(await UT.GetLang("conv.nout"), "no.png");
                }
                Directory.Delete(rttemps, true);
                Directory.CreateDirectory(rttemps);
                await UT.waitstatus.close();
                UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
                await UT.PowerReboot();
            }
            else
            {
                long needsize = di.AvailableFreeSpace - (fi.Length * Convert.ToInt64(3));
                string needsizes = UT.FormatSize(needsize);
                UT.DialogIShow(await UT.GetLang("spacepc") + " " + needsizes, "no.png");
            }
        }
    }
}
