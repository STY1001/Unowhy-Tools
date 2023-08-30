using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System;
using System.Windows.Media;

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

    public void applylang()
    {
        labconv.Text = UT.GetLang("convdesc");
        convbtn.Content = UT.GetLang("conv");
        labpath.Text = UT.GetLang("bkpath");
        rest_btn.Text = UT.GetLang("restore");
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
            DriveInfo di = new DriveInfo("C");
            FileInfo fi = new FileInfo(rtpath.Text);
            if (di.AvailableFreeSpace > fi.Length * 2.5)
            {
                await UT.waitstatus.open(UT.GetLang("wait.extract"), "zip.png");
                await Task.Delay(1000);
                string rttemps = Path.GetTempPath() + "\\Unowhy Tools\\Temps\\Drivers";
                string source = rtpath.Text;
                string dest = rttemps;
                await Task.Run(() =>
                {
                    ZipFile.ExtractToDirectory(source, dest);
                });
                await UT.waitstatus.open(UT.GetLang("wait.restore"), "download.png");
                if (File.Exists(rttemps + "\\UT-Restore.exe"))
                {
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = rttemps + "\\UT-Restore.exe";
                        p.StartInfo.WorkingDirectory = rttemps;
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.WaitForExit();
                    });

                }
                else
                {
                    await UT.waitstatus.close();
                    UT.DialogIShow(UT.GetLang("conv.nout"), "no.png");
                }
                Directory.Delete(rttemps, true);
                Directory.CreateDirectory(rttemps);
                await UT.waitstatus.close();
                UT.DialogIShow(UT.GetLang("rebootmsg"), "reboot.png");
                Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("space8gpc"), "no.png");
            }
        }
    }
}
