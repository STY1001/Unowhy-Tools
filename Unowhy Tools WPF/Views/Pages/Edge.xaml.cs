using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using System.Windows.Input;
using System.Threading;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Edge : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();
    public string edgever;

    Color disabled = (Color)ColorConverter.ConvertFromString("#888888");
    Color enabled = (Color)ColorConverter.ConvertFromString("#FFFFFF");

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

    public async Task applylang()
    {
        uninstall_txt.Text = await UT.GetLang("edgeun");
        block_txt.Text = await UT.GetLang("edgeblock");

    }

    public async Task CheckBTN()
    {
        string out1 = await UT.RunReturn("powershell.exe", "\"(Get-Item 'C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe').VersionInfo.FileVersion\"");
        if (out1.Contains("Get-Item"))
        {
            uninstall.IsEnabled = false;
            uninstall_txt.Foreground = new SolidColorBrush(disabled);
        }
        else
        {
            uninstall_txt.Text = await UT.GetLang("edgeun") + " (" + out1.Replace("\n", "").Replace("\r", "").Replace(" ", "") + ")";
            edgever = out1.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            uninstall.IsEnabled = true;
            uninstall_txt.Foreground = new SolidColorBrush(enabled);
        }

        RegistryKey eu = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\EdgeUpdate");
        if (eu != null)
        {
            object dnu = eu.GetValue("DoNotUpdateToEdgeWithChromium", null);
            if (dnu != null)
            {
                int dnu2 = (int)eu.GetValue("DoNotUpdateToEdgeWithChromium", 0);
                if (dnu2 == 1)
                {
                    block.IsEnabled = false;
                    block_txt.Foreground = new SolidColorBrush(disabled);
                }
                else
                {
                    block.IsEnabled = true;
                    block_txt.Foreground = new SolidColorBrush(enabled);
                }
            }
            else
            {
                block.IsEnabled = true;
                block_txt.Foreground = new SolidColorBrush(enabled);
            }
        }
        else
        {
            block.IsEnabled = true;
            block_txt.Foreground = new SolidColorBrush(enabled);
        }

    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in btngrid.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(Customize), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);

        foreach (UIElement element in btngrid.Children)
        {
            element.Visibility = Visibility.Visible;
            dlstate.Visibility = Visibility.Collapsed;
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

    public async void Uninstall_Click(object sender, RoutedEventArgs e)
    {
        if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"))
        {
            UT.DialogIShow(await UT.GetLang("needres"), "download.png");
        }

        if (UT.CheckInternet() || File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"))
        {
            if (UT.DialogQShow(await UT.GetLang("edgeun"), "uninstall.png"))
            {
                await UT.waitstatus.open(await UT.GetLang("wait.uninstall"), "uninstall.png");
                if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"))
                {
                    var progress = new System.Progress<double>();
                    var cancellationToken = new CancellationTokenSource();
                    string dl = await UT.GetLang("wait.download");
                    progress.ProgressChanged += async (sender, value) =>
                    {
                        await UT.waitstatus.open(dl + " (" + value.ToString("###.#") + "%)", "clouddl.png");
                    };
                    await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("edgesetup"), UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe", progress, cancellationToken.Token);
                }
                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.uninstall"), "uninstall.png");
                    await UT.RunMin("powershell", $"start-process -FilePath '{UT.utpath + "\\Unowhy Tools\\Temps\\Edge\\edgesetup.exe"}' -ArgumentList '--uninstall --system-level --force-uninstall' -nonewwindow -wait");
                }
                await Task.Delay(1000);
                await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
                await CheckBTN();
                await UT.waitstatus.close();
                if (!uninstall.IsEnabled)
                {
                    UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                }
                else
                {
                    UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                }
            }
        }
        else
        {
            UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
        }
    }

    public async void Block_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("edgeblock"), "block.png"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.block"), "block.png");
            await UT.RunMin("reg", "add \"HKLM\\SOFTWARE\\Microsoft\\EdgeUpdate\" /v \"DoNotUpdateToEdgeWithChromium\" /t REG_DWORD /d \"1\" /f");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN();
            await UT.waitstatus.close();
            if (!block.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN();
    }

    public Edge(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
