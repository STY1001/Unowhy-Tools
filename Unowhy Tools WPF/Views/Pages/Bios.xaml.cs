using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Unowhy_Tools;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Common.Interfaces;

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
        await CheckBTN(false, "none");

        foreach (UIElement element in RootGrid2.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(Dashboard), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);


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
        string ifpt_skykaby = "20180329314";
        string ifpt_apollo = "Y13G002S4EI Y13G002S4E";
        string ifpt_gemini = "Y13G012S4EI Y13G011S4EI Y13G010S4EI Y13G012S4E Y13G011S4E Y13G010S4E Y11G001S4E";
        string ifpt_jasper = "Y13G113S4EI Y13G113S4E Y11G201S2M Y13G201S4E Y13G201S4EI STYL13G1 STYL13G2 STYD5100SFF";
        string ifpt_tiger = "OPSG310S2M OPSG530S2M Y14G520S2M Y14G310S2M Y14G520S2MI Y14G310S2MI STYDS5OPS";
        string afu = "";
        string currentsku = UT.GetWMI("Win32_ComputerSystem", "SystemSKUNumber");
        if (ifpt_skykaby.Contains(currentsku))
        {
            ExpIFPT_SkyKaby.Visibility = Visibility.Visible;
            ExpIFPT_Apollo.Visibility = Visibility.Collapsed;
            ExpIFPT_Gemini.Visibility = Visibility.Collapsed;
            ExpIFPT_Jasper.Visibility = Visibility.Collapsed;
            ExpIFPT_Tiger.Visibility = Visibility.Collapsed;
            ExpAFU.Visibility = Visibility.Collapsed;
        }
        if (ifpt_apollo.Contains(currentsku))
        {
            ExpIFPT_SkyKaby.Visibility = Visibility.Collapsed;
            ExpIFPT_Apollo.Visibility = Visibility.Visible;
            ExpIFPT_Gemini.Visibility = Visibility.Collapsed;
            ExpIFPT_Jasper.Visibility = Visibility.Collapsed;
            ExpIFPT_Tiger.Visibility = Visibility.Collapsed;
            ExpAFU.Visibility = Visibility.Collapsed;
        }
        if (ifpt_gemini.Contains(currentsku))
        {
            ExpIFPT_SkyKaby.Visibility = Visibility.Collapsed;
            ExpIFPT_Apollo.Visibility = Visibility.Collapsed;
            ExpIFPT_Gemini.Visibility = Visibility.Visible;
            ExpIFPT_Jasper.Visibility = Visibility.Collapsed;
            ExpIFPT_Tiger.Visibility = Visibility.Collapsed;
            ExpAFU.Visibility = Visibility.Collapsed;
        }
        if (ifpt_jasper.Contains(currentsku))
        {
            ExpIFPT_SkyKaby.Visibility = Visibility.Collapsed;
            ExpIFPT_Apollo.Visibility = Visibility.Collapsed;
            ExpIFPT_Gemini.Visibility = Visibility.Collapsed;
            ExpIFPT_Jasper.Visibility = Visibility.Visible;
            ExpIFPT_Tiger.Visibility = Visibility.Collapsed;
            ExpAFU.Visibility = Visibility.Collapsed;
        }
        if (ifpt_tiger.Contains(currentsku))
        {
            ExpIFPT_SkyKaby.Visibility = Visibility.Collapsed;
            ExpIFPT_Apollo.Visibility = Visibility.Collapsed;
            ExpIFPT_Gemini.Visibility = Visibility.Collapsed;
            ExpIFPT_Jasper.Visibility = Visibility.Collapsed;
            ExpIFPT_Tiger.Visibility = Visibility.Visible;
            ExpAFU.Visibility = Visibility.Collapsed;
        }
        if (afu.Contains(currentsku))
        {
            ExpIFPT_SkyKaby.Visibility = Visibility.Collapsed;
            ExpIFPT_Apollo.Visibility = Visibility.Collapsed;
            ExpIFPT_Gemini.Visibility = Visibility.Collapsed;
            ExpIFPT_Jasper.Visibility = Visibility.Collapsed;
            ExpIFPT_Tiger.Visibility = Visibility.Collapsed;
            ExpAFU.Visibility = Visibility.Collapsed;
        }
    }

    public async Task CheckBTN(bool check, string step)
    {
        if (check)
        {
            await UT.Check(step);
        }
        mfbox.PlaceholderText = UTdata.mf; // /SM
        mdbox.PlaceholderText = UTdata.md; // /SP
        familybox.PlaceholderText = UTdata.family; // /SF
        snbox.PlaceholderText = UTdata.sn; // /SS
        biosvbox.PlaceholderText = UTdata.biosv; // /IV
        mbmfbox.PlaceholderText = UTdata.mbmf; // /BM
        mbmdbox.PlaceholderText = UTdata.mbmd; // /BP
        mbvbox.PlaceholderText = UTdata.mbv; // /BV
        biosmfbox.PlaceholderText = UTdata.biosmf; // /IVN
        biosdbox.Text = UTdata.biosd; // /ID
    }

    public async Task applylang()
    {

    }

    List<string> ifptfiles = new List<string>
    {
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Apollo\\" + "fparts.txt",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Apollo\\" + "FPTW64.exe",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Apollo\\" + "Idrvdll32e.dll",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Apollo\\" + "Pmxdll32e.dll",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Gemini\\" + "fparts.txt",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Gemini\\" + "FPTW64.exe",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Gemini\\" + "Idrvdll32e.dll",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Gemini\\" + "Pmxdll32e.dll",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Jasper\\" + "FPTW64.exe",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "SkyKaby\\" + "fparts.txt",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "SkyKaby\\" + "FPTW64.exe",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "SkyKaby\\" + "Idrvdll32e.dll",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "SkyKaby\\" + "Pmxdll32e.dll",
        UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\" + "Tiger\\" + "FPTW64.exe"
    };

    List<string> afufiles = new List<string>
    {
        UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe",
        UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "amigendrv64.sys"
    };

    List<string> amidefiles = new List<string>
    {
        UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe",
        UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIFLDRV64.sys"
    };

    public async Task DlRes(string resname)
    {
        if (resname == "IFPT")
        {
            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = await UT.GetLang("wait.download");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "download.png");
            };
            await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("fptw3"), UT.utpath + "\\Unowhy Tools\\Temps\\IFPT.zip", progress, cancellationToken.Token);
            await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
            await Task.Delay(100);
            await Task.Run(() =>
            {
                ZipFile.ExtractToDirectory(UT.utpath + "\\Unowhy Tools\\Temps\\IFPT.zip", UT.utpath + "\\Unowhy Tools\\Temps\\IFPT", true);
            });
        }
        if (resname == "AFU")
        {
            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = await UT.GetLang("wait.download");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "download.png");
            };
            await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("afuwin"), UT.utpath + "\\Unowhy Tools\\Temps\\AFUWin.zip", progress, cancellationToken.Token);
            await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
            await Task.Delay(1000);
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    ZipFile.ExtractToDirectory(UT.utpath + "\\Unowhy Tools\\Temps\\AFUWin.zip", UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU", true);
                });
            });
        }
        if (resname == "AMIDE")
        {
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
        }
    }

    public Bios(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    private async void ifptdumpexp_Click_Tiger(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-BIOS_" + UTdata.sn.Replace(" ", "_");
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptdumppath_Tiger.Text = fb.FileName;
            }
        }
    }

    private async void ifptflashexp_Click_Tiger(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptflashpath_Tiger.Text = fb.FileName;
                if (fb.FileName.Contains(".all.bin") || fb.FileName.Contains(".all.rom")) ifptalleeprom_Tiger.IsChecked = true;
                if (fb.FileName.Contains(".desc.bin") || fb.FileName.Contains(".desc.rom")) ifptdesc_Tiger.IsChecked = true;
                if (fb.FileName.Contains(".bios.bin") || fb.FileName.Contains(".bios.rom")) ifptbios_Tiger.IsChecked = true;
                if (fb.FileName.Contains(".me.bin") || fb.FileName.Contains(".me.rom")) ifptme_Tiger.IsChecked = true;
                UT.DialogIShow(await UT.GetLang("regionselectwarn"), "ic.png");
            }
        }
    }

    private async void ifptdumpbtn_Click_Tiger(object sender, RoutedEventArgs e)
    {
        if (!(ifptdumppath_Tiger.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbdumpwarn"), "upload.png"))
            {
                UT.SendAction("UTB.IFPTDump_Tiger");
                if (ifptfiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.dump"), "upload.png");
                    string path = ifptdumppath_Tiger.Text;
                    if (ifptalleeprom_Tiger.IsChecked == true) path = path.Replace(".bin", ".all.bin").Replace(".rom", ".all.rom");
                    if (ifptdesc_Tiger.IsChecked == true) path = path.Replace(".bin", ".desc.bin").Replace(".rom", ".desc.rom");
                    if (ifptbios_Tiger.IsChecked == true) path = path.Replace(".bin", ".bios.bin").Replace(".rom", ".bios.rom");
                    if (ifptme_Tiger.IsChecked == true) path = path.Replace(".bin", ".me.bin").Replace(".rom", ".me.rom");
                    string extarg = "";
                    if (ifptalleeprom_Tiger.IsChecked == true) extarg = "";
                    if (ifptdesc_Tiger.IsChecked == true) extarg = "-desc";
                    if (ifptbios_Tiger.IsChecked == true) extarg = "-bios";
                    if (ifptme_Tiger.IsChecked == true) extarg = "-me";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Tiger\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -d \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Tiger";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();
                    if (File.Exists(path))
                    {
                        UT.DialogIShow(await UT.GetLang("extregionchange"), "ic.png");
                        UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                    }
                }
            }
        }
    }

    private async void ifptflashbtn_Click_Tiger(object sender, RoutedEventArgs e)
    {
        if (!(ifptflashpath_Tiger.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbflashwarn"), "download.png"))
            {
                UT.SendAction("UTB.IFPTFlash_Tiger");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.flash"), "download.png");
                    string path = ifptflashpath_Tiger.Text;
                    string extarg = "";
                    if (ifptalleeprom_Tiger.IsChecked == true) extarg = "";
                    if (ifptdesc_Tiger.IsChecked == true) extarg = "-desc";
                    if (ifptbios_Tiger.IsChecked == true) extarg = "-bios";
                    if (ifptme_Tiger.IsChecked == true) extarg = "-me";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Tiger\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -f \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Tiger";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();

                    UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
                    Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
                }
            }
        }
    }

    private void ifptdumpexp_Click_Jasper(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-BIOS_" + UTdata.sn.Replace(" ", "_");
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptdumppath_Jasper.Text = fb.FileName;
            }
        }
    }

    private async void ifptflashexp_Click_Jasper(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptflashpath_Jasper.Text = fb.FileName;
                if (fb.FileName.Contains(".all.bin") || fb.FileName.Contains(".all.rom")) ifptalleeprom_Jasper.IsChecked = true;
                if (fb.FileName.Contains(".desc.bin") || fb.FileName.Contains(".desc.rom")) ifptdesc_Jasper.IsChecked = true;
                if (fb.FileName.Contains(".bios.bin") || fb.FileName.Contains(".bios.rom")) ifptbios_Jasper.IsChecked = true;
                if (fb.FileName.Contains(".me.bin") || fb.FileName.Contains(".me.rom")) ifptme_Jasper.IsChecked = true;
                UT.DialogIShow(await UT.GetLang("regionselectwarn"), "ic.png");
            }
        }
    }

    private async void ifptdumpbtn_Click_Jasper(object sender, RoutedEventArgs e)
    {
        if (!(ifptdumppath_Jasper.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbdumpwarn"), "upload.png"))
            {
                UT.SendAction("UTB.IFPTDump_Jasper");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.dump"), "upload.png");
                    string path = ifptdumppath_Jasper.Text;
                    if (ifptalleeprom_Jasper.IsChecked == true) path = path.Replace(".bin", ".all.bin").Replace(".rom", ".all.rom");
                    if (ifptdesc_Jasper.IsChecked == true) path = path.Replace(".bin", ".desc.bin").Replace(".rom", ".desc.rom");
                    if (ifptbios_Jasper.IsChecked == true) path = path.Replace(".bin", ".bios.bin").Replace(".rom", ".bios.rom");
                    if (ifptme_Jasper.IsChecked == true) path = path.Replace(".bin", ".me.bin").Replace(".rom", ".me.rom");
                    string extarg = "";
                    if (ifptalleeprom_Jasper.IsChecked == true) extarg = "";
                    if (ifptdesc_Jasper.IsChecked == true) extarg = "-desc";
                    if (ifptbios_Jasper.IsChecked == true) extarg = "-bios";
                    if (ifptme_Jasper.IsChecked == true) extarg = "-me";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Jasper\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -d \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Jasper";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();
                    if (File.Exists(path))
                    {
                        UT.DialogIShow(await UT.GetLang("extregionchange"), "ic.png");
                        UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                    }
                }
            }
        }
    }

    private async void ifptflashbtn_Click_Jasper(object sender, RoutedEventArgs e)
    {
        if (!(ifptflashpath_Jasper.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbflashwarn"), "download.png"))
            {
                UT.SendAction("UTB.IFPTFlash_Jasper");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.flash"), "download.png");
                    string path = ifptflashpath_Jasper.Text;
                    string extarg = "";
                    if (ifptalleeprom_Jasper.IsChecked == true) extarg = "";
                    if (ifptdesc_Jasper.IsChecked == true) extarg = "-desc";
                    if (ifptbios_Jasper.IsChecked == true) extarg = "-bios";
                    if (ifptme_Jasper.IsChecked == true) extarg = "-me";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Jasper\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -f \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Jasper";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();

                    UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
                    Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
                }
            }
        }
    }

    private void ifptdumpexp_Click_Gemini(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-BIOS_" + UTdata.sn.Replace(" ", "_");
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptdumppath_Gemini.Text = fb.FileName;
            }
        }
    }

    private async void ifptflashexp_Click_Gemini(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptflashpath_Gemini.Text = fb.FileName;
                if (fb.FileName.Contains(".all.bin") || fb.FileName.Contains(".all.rom")) ifptalleeprom_Gemini.IsChecked = true;
                if (fb.FileName.Contains(".desc.bin") || fb.FileName.Contains(".desc.rom")) ifptdesc_Gemini.IsChecked = true;
                if (fb.FileName.Contains(".bios.bin") || fb.FileName.Contains(".bios.rom")) ifptbios_Gemini.IsChecked = true;
                if (fb.FileName.Contains(".txe.bin") || fb.FileName.Contains(".txe.rom")) ifpttxe_Gemini.IsChecked = true;
                UT.DialogIShow(await UT.GetLang("regionselectwarn"), "ic.png");
            }
        }
    }

    private async void ifptdumpbtn_Click_Gemini(object sender, RoutedEventArgs e)
    {
        if (!(ifptdumppath_Gemini.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbdumpwarn"), "upload.png"))
            {
                UT.SendAction("UTB.IFPTDump_Gemini");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.dump"), "upload.png");
                    string path = ifptdumppath_Gemini.Text;
                    if (ifptalleeprom_Gemini.IsChecked == true) path = path.Replace(".bin", ".all.bin").Replace(".rom", ".all.rom");
                    if (ifptdesc_Gemini.IsChecked == true) path = path.Replace(".bin", ".desc.bin").Replace(".rom", ".desc.rom");
                    if (ifptbios_Gemini.IsChecked == true) path = path.Replace(".bin", ".bios.bin").Replace(".rom", ".bios.rom");
                    if (ifpttxe_Gemini.IsChecked == true) path = path.Replace(".bin", ".txe.bin").Replace(".rom", ".txe.rom");
                    string extarg = "";
                    if (ifptalleeprom_Gemini.IsChecked == true) extarg = "";
                    if (ifptdesc_Gemini.IsChecked == true) extarg = "-desc";
                    if (ifptbios_Gemini.IsChecked == true) extarg = "-bios";
                    if (ifpttxe_Gemini.IsChecked == true) extarg = "-txe";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Gemini\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -d \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Gemini";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();
                    if (File.Exists(path))
                    {
                        UT.DialogIShow(await UT.GetLang("extregionchange"), "ic.png");
                        UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                    }
                }
            }
        }
    }

    private async void ifptflashbtn_Click_Gemini(object sender, RoutedEventArgs e)
    {
        if (!(ifptflashpath_Gemini.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbflashwarn"), "download.png"))
            {
                UT.SendAction("UTB.IFPTFlash_Gemini");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.flash"), "download.png");
                    string path = ifptflashpath_Gemini.Text;
                    string extarg = "";
                    if (ifptalleeprom_Gemini.IsChecked == true) extarg = "";
                    if (ifptdesc_Gemini.IsChecked == true) extarg = "-desc";
                    if (ifptbios_Gemini.IsChecked == true) extarg = "-bios";
                    if (ifpttxe_Gemini.IsChecked == true) extarg = "-txe";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Gemini\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -f \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Gemini";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();

                    UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
                    Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
                }
            }
        }
    }

    private void ifptdumpexp_Click_Apollo(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-BIOS_" + UTdata.sn.Replace(" ", "_");
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptdumppath_Apollo.Text = fb.FileName;
            }
        }
    }

    private async void ifptflashexp_Click_Apollo(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptflashpath_Apollo.Text = fb.FileName;
                if (fb.FileName.Contains(".all.bin") || fb.FileName.Contains(".all.rom")) ifptalleeprom_Apollo.IsChecked = true;
                if (fb.FileName.Contains(".desc.bin") || fb.FileName.Contains(".desc.rom")) ifptdesc_Apollo.IsChecked = true;
                if (fb.FileName.Contains(".bios.bin") || fb.FileName.Contains(".bios.rom")) ifptbios_Apollo.IsChecked = true;
                if (fb.FileName.Contains(".txe.bin") || fb.FileName.Contains(".txe.rom")) ifpttxe_Apollo.IsChecked = true;
                UT.DialogIShow(await UT.GetLang("regionselectwarn"), "ic.png");
            }
        }
    }

    private async void ifptdumpbtn_Click_Apollo(object sender, RoutedEventArgs e)
    {
        if (!(ifptdumppath_Apollo.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbdumpwarn"), "upload.png"))
            {
                UT.SendAction("UTB.IFPTDump_Apollo");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.dump"), "upload.png");
                    string path = ifptdumppath_Apollo.Text;
                    if (ifptalleeprom_Apollo.IsChecked == true) path = path.Replace(".bin", ".all.bin").Replace(".rom", ".all.rom");
                    if (ifptdesc_Apollo.IsChecked == true) path = path.Replace(".bin", ".desc.bin").Replace(".rom", ".desc.rom");
                    if (ifptbios_Apollo.IsChecked == true) path = path.Replace(".bin", ".bios.bin").Replace(".rom", ".bios.rom");
                    if (ifpttxe_Apollo.IsChecked == true) path = path.Replace(".bin", ".txe.bin").Replace(".rom", ".txe.rom");
                    string extarg = "";
                    if (ifptalleeprom_Apollo.IsChecked == true) extarg = "";
                    if (ifptdesc_Apollo.IsChecked == true) extarg = "-desc";
                    if (ifptbios_Apollo.IsChecked == true) extarg = "-bios";
                    if (ifpttxe_Apollo.IsChecked == true) extarg = "-txe";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Apollo\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -d \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Apollo";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();
                    if (File.Exists(path))
                    {
                        UT.DialogIShow(await UT.GetLang("extregionchange"), "ic.png");
                        UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                    }
                }
            }
        }
    }

    private async void ifptflashbtn_Click_Apollo(object sender, RoutedEventArgs e)
    {
        if (!(ifptflashpath_Apollo.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbflashwarn"), "download.png"))
            {
                UT.SendAction("UTB.IFPTFlash_Apollo");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.flash"), "download.png");
                    string path = ifptflashpath_Apollo.Text;
                    string extarg = "";
                    if (ifptalleeprom_Apollo.IsChecked == true) extarg = "";
                    if (ifptdesc_Apollo.IsChecked == true) extarg = "-desc";
                    if (ifptbios_Apollo.IsChecked == true) extarg = "-bios";
                    if (ifpttxe_Apollo.IsChecked == true) extarg = "-txe";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Apollo\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -f \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\Apollo";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();

                    UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
                    Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
                }
            }
        }
    }

    private void ifptdumpexp_Click_SkyKaby(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-BIOS_" + UTdata.sn.Replace(" ", "_");
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptdumppath_SkyKaby.Text = fb.FileName;
            }
        }
    }

    private async void ifptflashexp_Click_SkyKaby(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "bin";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                ifptflashpath_SkyKaby.Text = fb.FileName;
                if (fb.FileName.Contains(".all.bin") || fb.FileName.Contains(".all.rom")) ifptalleeprom_SkyKaby.IsChecked = true;
                if (fb.FileName.Contains(".desc.bin") || fb.FileName.Contains(".desc.rom")) ifptdesc_SkyKaby.IsChecked = true;
                if (fb.FileName.Contains(".bios.bin") || fb.FileName.Contains(".bios.rom")) ifptbios_SkyKaby.IsChecked = true;
                if (fb.FileName.Contains(".me.bin") || fb.FileName.Contains(".me.rom")) ifptme_SkyKaby.IsChecked = true;
                UT.DialogIShow(await UT.GetLang("regionselectwarn"), "ic.png");
            }
        }
    }

    private async void ifptdumpbtn_Click_SkyKaby(object sender, RoutedEventArgs e)
    {
        if (!(ifptdumppath_SkyKaby.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbdumpwarn"), "upload.png"))
            {
                UT.SendAction("UTB.IFPTDump_SkyKaby");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.dump"), "upload.png");
                    string path = ifptdumppath_SkyKaby.Text;
                    if (ifptalleeprom_SkyKaby.IsChecked == true) path = path.Replace(".bin", ".all.bin").Replace(".rom", ".all.rom");
                    if (ifptdesc_SkyKaby.IsChecked == true) path = path.Replace(".bin", ".desc.bin").Replace(".rom", ".desc.rom");
                    if (ifptbios_SkyKaby.IsChecked == true) path = path.Replace(".bin", ".bios.bin").Replace(".rom", ".bios.rom");
                    if (ifptme_SkyKaby.IsChecked == true) path = path.Replace(".bin", ".me.bin").Replace(".rom", ".me.rom");
                    string extarg = "";
                    if (ifptalleeprom_SkyKaby.IsChecked == true) extarg = "";
                    if (ifptdesc_SkyKaby.IsChecked == true) extarg = "-desc";
                    if (ifptbios_SkyKaby.IsChecked == true) extarg = "-bios";
                    if (ifptme_SkyKaby.IsChecked == true) extarg = "-me";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\SkyKaby\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -d \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\SkyKaby";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();
                    if (File.Exists(path))
                    {
                        UT.DialogIShow(await UT.GetLang("extregionchange"), "ic.png");
                        UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                    }
                }
            }
        }
    }

    private async void ifptflashbtn_Click_SkyKaby(object sender, RoutedEventArgs e)
    {
        if (!(ifptflashpath_SkyKaby.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbflashwarn"), "download.png"))
            {
                UT.SendAction("UTB.IFPTFlash_SkyKaby");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("IFPT");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.flash"), "download.png");
                    string path = ifptflashpath_SkyKaby.Text;
                    string extarg = "";
                    if (ifptalleeprom_SkyKaby.IsChecked == true) extarg = "";
                    if (ifptdesc_SkyKaby.IsChecked == true) extarg = "-desc";
                    if (ifptbios_SkyKaby.IsChecked == true) extarg = "-bios";
                    if (ifptme_SkyKaby.IsChecked == true) extarg = "-me";
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\SkyKaby\\FPTW64.exe";
                        p.StartInfo.Arguments = $"{extarg} -f \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\IFPT\\SkyKaby";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();

                    UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
                    Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
                }
            }
        }
    }

    private void afudumpexp_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-BIOS_" + UTdata.sn.Replace(" ", "");
            fb.DefaultExt = "rom";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                afudumppath.Text = fb.FileName;
            }
        }
    }

    private void afuflashexp_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "rom";
            fb.Filter = "Unowhy Tools BIOS file|*.rom;*.bin";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                afuflashpath.Text = fb.FileName;
            }
        }
    }

    private async void afudumpbtn_Click(object sender, RoutedEventArgs e)
    {
        if (!(afudumppath.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbdumpwarn"), "upload.png"))
            {
                UT.SendAction("UTB.AFUDump");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("AFU");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.dump"), "upload.png");
                    string path = afudumppath.Text;
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\AFUWINx64.exe";
                        p.StartInfo.Arguments = $"\"{path}\" /O";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();
                    if (File.Exists(path))
                    {
                        UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                    }
                }
            }
        }
    }

    private async void afuflashbtn_Click(object sender, RoutedEventArgs e)
    {
        if (!(afuflashpath.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbflashwarn"), "download.png"))
            {
                UT.SendAction("UTB.AFUFlash");
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
                    if (await UT.CheckInternet())
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("AFU");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.flash"), "download.png");
                    string path = afuflashpath.Text;
                    string extarg = "";
                    if (afuflashmain.IsChecked == true) extarg = extarg + " /P";
                    if (afuflashnvram.IsChecked == true) extarg = extarg + " /N";
                    if (afuflashreboot.IsChecked == true) extarg = extarg + " /REBOOT";
                    if (afuflashsmbios.IsChecked == true) extarg = extarg + " /R";
                    if (afuflashsettings.IsChecked == true) extarg = extarg + " /SP";

                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\AFUWINx64.exe";
                        p.StartInfo.Arguments = $"\"{path}\"{extarg}";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU";
                        p.Start();
                        p.WaitForExit();
                    });
                    await UT.waitstatus.close();
                }
            }
        }
    }

    private async void ButtonExport_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-SMBIOS_" + UTdata.sn.Replace(" ", "_");
            fb.DefaultExt = "json";
            fb.Filter = "Unowhy Tools SMBIOS file (*.json)|*.json";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                UT.SendAction("UTB.SMBIOSExport");
                var jsonData = new
                {
                    mf = mfbox.PlaceholderText,
                    md = mdbox.PlaceholderText,
                    familybox = familybox.PlaceholderText,
                    sn = snbox.PlaceholderText,
                    biosv = biosvbox.PlaceholderText,
                    mbmf = mbmfbox.PlaceholderText,
                    mbmd = mbmdbox.PlaceholderText,
                    mbv = mbvbox.PlaceholderText,
                    biosmf = biosmfbox.PlaceholderText,
                    biosd = biosdbox.SelectedDate.Value.ToString("MM/dd/yyyy")
                };

                File.WriteAllText(fb.FileName, JsonConvert.SerializeObject(jsonData, Formatting.Indented));

                if (JsonConvert.SerializeObject(jsonData, Formatting.Indented) == File.ReadAllText(fb.FileName))
                {
                    UT.DialogIShow(await UT.GetLang("done"), "yes.png");
                }
                else
                {
                    UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                }
            }
        }
    }

    private void ButtonImport_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.DefaultExt = "json";
            fb.Filter = "Unowhy Tools SMBIOS file (*.json)|*.json";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                UT.SendAction("UTB.SMBIOSImport");
                JObject json = JObject.Parse(File.ReadAllText(fb.FileName));

                string mf = (string)json["mf"];
                string md = (string)json["md"];
                string family = (string)json["family"];
                string sn = (string)json["sn"];
                string biosv = (string)json["biosv"];
                string mbmf = (string)json["mbmf"];
                string mbmd = (string)json["mbmd"];
                string mbv = (string)json["mbv"];
                string biosmf = (string)json["biosmf"];
                string biosd = (string)json["biosd"];

                mfbox.Text = mf;
                mdbox.Text = md;
                familybox.Text = family;
                snbox.Text = sn;
                biosvbox.Text = biosv;
                mbmfbox.Text = mbmf;
                mbmdbox.Text = mbmd;
                mbvbox.Text = mbv;
                biosmfbox.Text = biosmf;
                biosdbox.Text = biosd;
            }
        }
    }

    private async void ButtonApply_Click(object sender, RoutedEventArgs e)
    {
        UT.SendAction("UTB.SMBIOSApply");
        if (amidefiles.Any(file => !File.Exists(file)))
        {
            UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");
            if (await UT.CheckInternet())
            {
                await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
                await DlRes("AMIDE");
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

            if (!(mfbox.Text == ""))
            {
                string newtxt = mfbox.Text;
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/SM \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(mdbox.Text == ""))
            {
                string newtxt = mdbox.Text;
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/SP \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(familybox.Text == ""))
            {
                string newtxt = familybox.Text;
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/SF \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(snbox.Text == ""))
            {
                string newtxt = snbox.Text;
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/SS \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(biosvbox.Text == ""))
            {
                string newtxt = biosvbox.Text;
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/IV \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(mbmfbox.Text == ""))
            {
                string newtxt = mbmfbox.Text;
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/BM \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(mbmdbox.Text == ""))
            {
                string newtxt = mbmdbox.Text;
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/BP \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(mbvbox.Text == ""))
            {
                string newtxt = mbvbox.Text;
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/BV \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(biosmfbox.Text == ""))
            {
                string newtxt = biosmfbox.Text;
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/IVN \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(biosdbox.Text == ""))
            {
                string newtxt = biosdbox.SelectedDate.Value.ToString("MM/dd/yyyy");
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/ID \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }

            await Task.Delay(1000);
            await UT.waitstatus.close();

            UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
            Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
        }
    }

    private async void ExpIFPT_Expanded_Tiger(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in IFPTGridDump_Tiger.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in IFPTGridFlash_Tiger.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in ifptregion_Tiger.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        foreach (UIElement element in IFPTGridDump_Tiger.Children)
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
        foreach (UIElement element in IFPTGridFlash_Tiger.Children)
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
        foreach (UIElement element in ifptregion_Tiger.Children)
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

    private async void ExpIFPT_Expanded_Jasper(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in IFPTGridDump_Jasper.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in IFPTGridFlash_Jasper.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in ifptregion_Jasper.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        foreach (UIElement element in IFPTGridDump_Jasper.Children)
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
        foreach (UIElement element in IFPTGridFlash_Jasper.Children)
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
        foreach (UIElement element in ifptregion_Jasper.Children)
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

    private async void ExpIFPT_Expanded_Gemini(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in IFPTGridDump_Gemini.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in IFPTGridFlash_Gemini.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in ifptregion_Gemini.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        foreach (UIElement element in IFPTGridDump_Gemini.Children)
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
        foreach (UIElement element in IFPTGridFlash_Gemini.Children)
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
        foreach (UIElement element in ifptregion_Gemini.Children)
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

    private async void ExpIFPT_Expanded_Apollo(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in IFPTGridDump_Apollo.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in IFPTGridFlash_Apollo.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in ifptregion_Apollo.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        foreach (UIElement element in IFPTGridDump_Apollo.Children)
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
        foreach (UIElement element in IFPTGridFlash_Apollo.Children)
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
        foreach (UIElement element in ifptregion_Apollo.Children)
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

    private async void ExpIFPT_Expanded_SkyKaby(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in IFPTGridDump_SkyKaby.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in IFPTGridFlash_SkyKaby.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in ifptregion_SkyKaby.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        foreach (UIElement element in IFPTGridDump_SkyKaby.Children)
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
        foreach (UIElement element in IFPTGridFlash_SkyKaby.Children)
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
        foreach (UIElement element in ifptregion_SkyKaby.Children)
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

    private async void ExpAFU_Expanded(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in AFUGridDump.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in AFUGridFlash.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in AFUfaGrid1.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in AFUfaGrid2.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        foreach (UIElement element in AFUGridDump.Children)
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
        foreach (UIElement element in AFUGridFlash.Children)
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
        foreach (UIElement element in AFUfaGrid1.Children)
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
        foreach (UIElement element in AFUfaGrid2.Children)
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

    private async void ExpAMIDE_Expanded(object sender, RoutedEventArgs e)
    {
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
}
