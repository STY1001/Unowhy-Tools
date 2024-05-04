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
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Documents;
using System.IO.Compression;
using System.Diagnostics;
using System.Windows.Threading;

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
        ExpAFU.IsExpanded = false;
        ExpAMIDE.IsExpanded = false;

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
    }

    public async Task CheckBTN(bool check, string step)
    {
        if (check)
        {
            await UT.Check(step);
        }
        mfbox.PlaceholderText = UTdata.mf; // /SM
        mdbox.PlaceholderText = UTdata.md; // /SV
        skubox.PlaceholderText = UTdata.sku; // /SK
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
            await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("fptw"), UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\IFPT\\FPTW.exe", progress, cancellationToken.Token);
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

    private void ifptdumpexp_Click(object sender, RoutedEventArgs e)
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
                ifptdumppath.Text = fb.FileName;
            }
        }
    }

    private void ifptflashexp_Click(object sender, RoutedEventArgs e)
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
                ifptflashpath.Text = fb.FileName;
            }
        }
    }

    private async void ifptdumpbtn_Click(object sender, RoutedEventArgs e)
    {
        if (!(ifptdumppath.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbdumpwarn"), "upload.png"))
            {
                UT.SendAction("UTB.IFPTDump");
                if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\IFPT\\FPTW.exe"))
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
                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\IFPT\\FPTW.exe"))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.dump"), "upload.png");
                    string path = ifptdumppath.Text;
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\IFPT\\FPTW.exe";
                        p.StartInfo.Arguments = $"-bios -d \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\IFPT";
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

    private async void ifptflashbtn_Click(object sender, RoutedEventArgs e)
    {
        if (!(ifptflashpath.Text == ""))
        {
            if (UT.DialogQShow(await UT.GetLang("utbflashwarn"), "download.png"))
            {
                UT.SendAction("UTB.IFPTFlash");
                if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\IFPT\\FPTW.exe"))
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
                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\IFPT\\FPTW.exe"))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.flash"), "download.png");
                    string path = ifptflashpath.Text;
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\IFPT\\FPTW.exe";
                        p.StartInfo.Arguments = $"-bios -f \"{path}\"";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\IFPT";
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
            fb.FileName = "UT-SMBIOS_" + UTdata.sn.Replace(" ", "");
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
                    sku = skubox.PlaceholderText,
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
                string sku = (string)json["sku"];
                string sn = (string)json["sn"];
                string biosv = (string)json["biosv"];
                string mbmf = (string)json["mbmf"];
                string mbmd = (string)json["mbmd"];
                string mbv = (string)json["mbv"];
                string biosmf = (string)json["biosmf"];
                string biosd = (string)json["biosd"];

                mfbox.Text = mf;
                mdbox.Text = md;
                skubox.Text = sku;
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
                    p.StartInfo.Arguments = $"/SV \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (!(skubox.Text == ""))
            {
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

    private async void ExpIFPT_Expanded(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in IFPTGridDump.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in IFPTGridFlash.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        foreach (UIElement element in IFPTGridDump.Children)
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
        foreach (UIElement element in IFPTGridFlash.Children)
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
