using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Windows;

using Unowhy_Tools;
using System;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Threading;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Bios : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void GoForw(object sender, RoutedEventArgs e)
    {
        //UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        //UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(Dashboard));
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        await CheckBTN(false);

        foreach (UIElement element in GridDump.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in GridFlash.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in GridSeparator.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in StackSMBIOS1.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in StackSMBIOS2.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in StackButton.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(Dashboard), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);

        foreach (UIElement element in GridDump.Children)
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
        foreach (UIElement element in GridFlash.Children)
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
        foreach (UIElement element in GridSeparator.Children)
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
        foreach (UIElement element in StackSMBIOS1.Children)
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
        foreach (UIElement element in StackSMBIOS2.Children)
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
        foreach (UIElement element in StackButton.Children)
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
        }
    }

    public async Task CheckBTN(bool check)
    {
        if (check)
        {
            await UT.Check();
        }
        mfbox.PlaceholderText = UTdata.mf;
        mdbox.PlaceholderText = UTdata.md;
        skubox.PlaceholderText = UTdata.sku;
        snbox.PlaceholderText = UTdata.sn;
        biosvbox.PlaceholderText = UTdata.biosv;
        mbmfbox.PlaceholderText = UTdata.mbmf;
        mbmdbox.PlaceholderText = UTdata.mbmd;
        mbvbox.PlaceholderText = UTdata.mbv;
        biosmfbox.PlaceholderText = UTdata.biosmf;
        biosdbox.Text = UTdata.biosd; 
    }

    public void applylang()
    {

    }

    List<string> afufiles = new List<string>
    {
        Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe",
        Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AFU\\" + "amigendrv64.sys"
    };

    List<string> amidefiles = new List<string>
    {
        Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE" + "AMIDEWINx64.exe",
        Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE" + "AMIFLDRV64.sys"
    };

    public async Task DlRes(string resname)
    {
        if (resname == "AFU")
        {
            if (afufiles.Any(file => !File.Exists(file)))
            {
                if (UT.CheckInternet())
                {
                    dlstatus.Visibility = Visibility.Visible;
                    var progress = new System.Progress<double>();
                    var cancellationToken = new CancellationTokenSource();
                    progress.ProgressChanged += (sender, value) =>
                    {
                        dlstatus.Text = "Downloading... (" + value.ToString("###.#") + "%)";
                    };
                    await UT.DlFilewithProgress("https://bit.ly/UTAFUWin", Path.GetTempPath() + "Unowhy Tools\\Temps\\AFUWin.zip", progress, cancellationToken.Token);
                    dlstatus.Visibility = Visibility.Collapsed;
                }
            }
        }
        if (resname == "AMIDE")
        {
            if (amidefiles.Any(file => !File.Exists(file)))
            {
                if (UT.CheckInternet())
                {
                    dlstatus.Visibility = Visibility.Visible;
                    var progress = new System.Progress<double>();
                    var cancellationToken = new CancellationTokenSource();
                    progress.ProgressChanged += (sender, value) =>
                    {
                        dlstatus.Text = "Downloading... (" + value.ToString("###.#") + "%)";
                    };
                    await UT.DlFilewithProgress("https://bit.ly/UTAMIDEWin", Path.GetTempPath() + "Unowhy Tools\\Temps\\AMIDEWin.zip", progress, cancellationToken.Token);
                    dlstatus.Visibility = Visibility.Collapsed;
                }
            }
        }
    }

    public Bios(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    private void dumpexp_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-BIOS_" + UTdata.sn.Replace(" ","");
            fb.DefaultExt = "rom";
            fb.Filter = "Unowhy Tools BIOS file (*.rom)|*.rom";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                dumppath.Text = fb.FileName;
            }
        }
    }

    private void flashexp_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "rom";
            fb.Filter = "Unowhy Tools BIOS file (*.rom)|*.rom";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                flashpath.Text = fb.FileName;
            }
        }
    }

    private void dumpbtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void flashbtn_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ButtonExport_Click(object sender, RoutedEventArgs e)
    {

    }
}