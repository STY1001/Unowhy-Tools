using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
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
public partial class Compatibility : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void GoForw(object sender, RoutedEventArgs e)
    {
        //UT.anim.TransitionForw(RootGrid);
    }

    public async void Init(object sender, EventArgs e)
    {
        string currentsku = UT.GetWMI("Win32_ComputerSystem", "SystemSKUNumber");
        captiontext.Text = captiontext.Text.Replace("[sku]", currentsku);
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        UT.anim.BorderZoomIn(RootBorder);
    }

    public async void UnInitAnim(object sender, RoutedEventArgs e)
    {

    }

    public Compatibility(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public async Task applylang()
    {

    }

    List<string> amidefiles = new List<string>
    {
        UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe",
        UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIFLDRV64.sys"
    };

    private async void editbtn2_Click(object sender, RoutedEventArgs e)
    {
        bool compatible = false;
        string currentsku = skubox.Text;
        currentsku = currentsku.Replace(" ", "").Replace("\r", "").Replace("\n", "");
        if (UT.skumodel.ContainsKey(currentsku))
        {
            compatible = true;
        }
        else
        {
            compatible = false;
        }
        if (compatible)
        {
            string model = await UT.GetModelWithSKU(currentsku);
            if (UT.DialogQShow(currentsku + " (" + model + ")", "barcode.png"))
            {
                if (amidefiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        var progress = new System.Progress<double>();
                        var cancellationToken = new CancellationTokenSource();
                        string dl = await UT.GetLang("wait.download");
                        progress.ProgressChanged += async (sender, value) =>
                        {
                            await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "download.png");
                        };
                        await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("amidewin"), UT.utpath + "\\Unowhy Tools\\Temps\\AMIDEWin.zip", progress, cancellationToken.Token);
                        await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
                        await Task.Delay(1000);
                        await Task.Run(() =>
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ZipFile.ExtractToDirectory(UT.utpath + "\\Unowhy Tools\\Temps\\AMIDEWin.zip", UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE", true);
                            });
                        });
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (amidefiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.apply"), "customize.png");
                    string newtxt = skubox.Text;
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                        p.StartInfo.Arguments = $"/SK \"{newtxt}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.WaitForExit();
                    });
                    await Task.Delay(1000);
                    await UT.waitstatus.close();

                    UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
                    await UT.PowerReboot();
                }
            }
            else
            {

            }
        }
        else
        {
            UT.DialogIShow("SKU not compatible", "no.png");
        }
    }

    private void exitbtn_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private async void editbtn_Click(object sender, RoutedEventArgs e)
    {
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = -1000;
            anim.Duration = TimeSpan.FromMilliseconds(300);
            anim.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn };

            TranslateTransform trans = new TranslateTransform();
            msggrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            await Task.Delay(300);
            msggrid.Visibility = Visibility.Collapsed;
            skugrid.Visibility = Visibility.Visible;
            editbtn.Click -= editbtn_Click;
            editbtn.Click += editbtn2_Click;
            editbtn.HorizontalAlignment = HorizontalAlignment.Right;
            exitbtn.HorizontalAlignment = HorizontalAlignment.Left;

            anim = new DoubleAnimation();
            anim.From = 1000;
            anim.To = 0;
            anim.Duration = TimeSpan.FromMilliseconds(500);
            anim.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };

            trans = new TranslateTransform();
            skugrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }
}
