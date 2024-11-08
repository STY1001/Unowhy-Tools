using System;
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
public partial class DrvBack : INavigableView<DashboardViewModel>
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

    public async Task applylang()
    {
        labbkapp.Text = await UT.GetLang("bkapp");
        labpath.Text = await UT.GetLang("bkpath");
        bk_btn.Text = await UT.GetLang("backup");
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

    public DrvBack(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public void Browse_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-Drv_" + DateTime.Now.ToString("HH-mm_dd-MM-yy");
            fb.DefaultExt = "zip";
            fb.Filter = "Unowhy Tools Driver file (*.zip)|*.zip";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                bkpath.Text = fb.FileName;
            }
        }
    }

    public async void Backup_Click(object sender, RoutedEventArgs e)
    {
        if (bkpath.Text == "")
        {

        }
        else
        {
            UT.SendAction("DrvBackup");
            string drivel = bkpath.Text.Substring(0, 1);
            DriveInfo di = new DriveInfo(drivel);
            long afs = di.AvailableFreeSpace;

            long drvfolder = await UT.FolderSizeGet("C:\\Windows\\System32\\DriverStore\\FileRepository");

            if (afs > drvfolder)
            {
                await UT.waitstatus.open(await UT.GetLang("wait.backup"), "upload.png");
                Directory.CreateDirectory(drivel + ":\\UT-Drv-TMP");
                string backuptemp = drivel + ":\\UT-Drv-TMP";
                await UT.Extract("UT-Restore.exe", backuptemp + "\\UT-Restore.exe");
                if (dismbk.IsChecked == true)
                {
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = "dism.exe";
                        p.StartInfo.Arguments = "/online /export-driver /destination:\"" + backuptemp + "\"";
                        p.Start();
                        p.WaitForExit();
                    });
                }
                else if (psbk.IsChecked == true)
                {
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = "powershell.exe";
                        p.StartInfo.Arguments = "Export-WindowsDriver -Online -Destination \"" + backuptemp + "\"";
                        p.Start();
                        p.WaitForExit();
                    });
                }
                await UT.waitstatus.open(await UT.GetLang("wait.compress"), "zip.png");
                await Task.Delay(1000);
                string source = backuptemp;
                string dest = bkpath.Text;
                if (File.Exists(dest))
                {
                    File.Delete(dest);
                }
                await Task.Run(() =>
                {
                    ZipFile.CreateFromDirectory(source, dest, CompressionLevel.NoCompression, false);
                });
                Directory.Delete(backuptemp, true);
                FileInfo fi = new FileInfo(bkpath.Text);
                await UT.waitstatus.close();
                if (fi.Length > 1000000)
                {
                    UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                }
                else
                {
                    UT.DialogIShow(await UT.GetLang("bkfail"), "no.png");
                }
            }
            else
            {
                long needsize = drvfolder - afs;
                string needsizes = UT.FormatSize(needsize);
                UT.DialogIShow(await UT.GetLang("spaceusb") + " " + needsizes, "no.png");
            }
        }
    }
}
