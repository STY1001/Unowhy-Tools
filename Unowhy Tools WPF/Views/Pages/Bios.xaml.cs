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
        Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe",
        Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIFLDRV64.sys"
    };

    public async Task DlRes(string resname)
    {
        if (resname == "AFU")
        {
            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = UT.GetLang("wait.download");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("###.#") + "%)", "download.png");
            };
            await UT.DlFilewithProgress("https://bit.ly/UTAFUWin", Path.GetTempPath() + "Unowhy Tools\\Temps\\AFUWin.zip", progress, cancellationToken.Token);
            await UT.waitstatus.open(UT.GetLang("wait.extract"), "zip.png");
            await Task.Delay(1000);
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    ZipFile.ExtractToDirectory(Path.GetTempPath() + "Unowhy Tools\\Temps\\AFUWin.zip", Path.GetTempPath() + "\\Unowhy Tools\\Temps\\AMI\\AFU");
                });
            });
        }
        if (resname == "AMIDE")
        {
            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = UT.GetLang("wait.download");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("###.#") + "%)", "download.png");
            };
            await UT.DlFilewithProgress("https://bit.ly/UTAMIDEWin", Path.GetTempPath() + "Unowhy Tools\\Temps\\AMIDEWin.zip", progress, cancellationToken.Token);
            await UT.waitstatus.open(UT.GetLang("wait.extract"), "zip.png");
            await Task.Delay(1000);
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    ZipFile.ExtractToDirectory(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMIDEWin.zip", Path.GetTempPath() + "\\Unowhy Tools\\Temps\\AMI\\AMIDE");
                });
            });
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
            fb.FileName = "UT-BIOS_" + UTdata.sn.Replace(" ", "");
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

    private async void dumpbtn_Click(object sender, RoutedEventArgs e)
    {
        if (!(dumppath.Text == ""))
        {
            if (UT.DialogQShow(UT.GetLang("utbdumpwarn"), "upload.png"))
            {
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(UT.GetLang("needres"), "clouddl.png");
                    if (UT.CheckInternet())
                    {
                        await UT.waitstatus.open(UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("AFU");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(UT.GetLang("wait.dump"), "upload.png");
                    string path = flashpath.Text;
                    string ret = await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe";
                        p.StartInfo.Arguments = $"\"{path}\" /O";
                        p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AFU";
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.WaitForExit();
                        return p.StandardOutput.ReadToEnd();
                    });
                    UT.Write2Log(ret);
                    await UT.waitstatus.close();
                }
            }
        }
    }

    private async void flashbtn_Click(object sender, RoutedEventArgs e)
    {
        if (!(flashpath.Text == ""))
        {
            if (UT.DialogQShow(UT.GetLang("utbflashwarn"), "download.png"))
            {
                if (afufiles.Any(file => !File.Exists(file)))
                {
                    UT.DialogIShow(UT.GetLang("needres"), "clouddl.png");
                    if (UT.CheckInternet())
                    {
                        await UT.waitstatus.open(UT.GetLang("wait.download"), "clouddl.png");
                        await DlRes("AFU");
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        UT.DialogIShow(UT.GetLang("nonet"), "nowifi.png");
                    }
                }
                await Task.Delay(1000);
                if (afufiles.Any(file => File.Exists(file)))
                {
                    await UT.waitstatus.open(UT.GetLang("wait.flash"), "download.png");
                    string path = flashpath.Text;
                    string ret = await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe";
                        p.StartInfo.Arguments = $"\"{path}\" /P /N /REBOOT";
                        p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AFU";
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.WaitForExit();
                        return p.StandardOutput.ReadToEnd();
                    });
                    UT.Write2Log(ret);
                    await UT.waitstatus.close();
                }
            }
        }
    }

    private void ButtonExport_Click(object sender, RoutedEventArgs e)
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
                    UT.DialogIShow(UT.GetLang("done"), "yes.png");
                }
                else
                {
                    UT.DialogIShow(UT.GetLang("failed"), "no.png");
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
        if (amidefiles.Any(file => !File.Exists(file)))
        {
            UT.DialogIShow(UT.GetLang("needres"), "clouddl.png");
            if (UT.CheckInternet())
            {
                await UT.waitstatus.open(UT.GetLang("wait.download"), "clouddl.png");
                await DlRes("AMIDE");
                await UT.waitstatus.close();
            }
            else
            {
                UT.DialogIShow(UT.GetLang("nonet"), "nowifi.png");
            }
        }
        await Task.Delay(1000);
        if (amidefiles.Any(file => File.Exists(file)))
        {
            await UT.waitstatus.open(UT.GetLang("wait.apply"), "customize.png");

            if (!(mfbox.Text == ""))
            {
                string newtxt = mfbox.Text;
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/SM \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }
            if (!(mdbox.Text == ""))
            {
                string newtxt = mdbox.Text;
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/SV \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }
            if (!(skubox.Text == ""))
            {
                string newtxt = skubox.Text;
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/SK \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }
            if (!(snbox.Text == ""))
            {
                string newtxt = snbox.Text;
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/SS \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }
            if (!(biosvbox.Text == ""))
            {
                string newtxt = biosvbox.Text;
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/IV \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }
            if (!(mbmfbox.Text == ""))
            {
                string newtxt = mbmfbox.Text;
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/BM \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }
            if (!(mbmdbox.Text == ""))
            {
                string newtxt = mbmdbox.Text;
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/BP \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }
            if (!(mbvbox.Text == ""))
            {
                string newtxt = mbvbox.Text;
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/BV \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }
            if (!(biosmfbox.Text == ""))
            {
                string newtxt = biosmfbox.Text;
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/IVN \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }
            if (!(biosdbox.Text == ""))
            {
                string newtxt = biosdbox.SelectedDate.Value.ToString("MM/dd/yyyy");
                string ret = await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe";
                    p.StartInfo.Arguments = $"/ID \"{newtxt}\"";
                    p.StartInfo.WorkingDirectory = Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                    return p.StandardOutput.ReadToEnd();
                });
                UT.Write2Log(ret);
            }

            await Task.Delay(1000);
            await UT.waitstatus.open(UT.GetLang("wait.check"), "check.png");

            await CheckBTN(true);

            bool isOK = true;

            if (!(mfbox.Text == ""))
            {
                if (mfbox.Text == mfbox.PlaceholderText)
                {
                    mfbox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }
            if (!(mdbox.Text == ""))
            {
                if (mdbox.Text == mdbox.PlaceholderText)
                {
                    mdbox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }
            if (!(skubox.Text == ""))
            {
                if (skubox.Text == skubox.PlaceholderText)
                {
                    skubox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }
            if (!(snbox.Text == ""))
            {
                if (snbox.Text == snbox.PlaceholderText)
                {
                    snbox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }
            if (!(biosvbox.Text == ""))
            {
                if (biosvbox.Text == biosvbox.PlaceholderText)
                {
                    biosvbox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }
            if (!(mbmfbox.Text == ""))
            {
                if (mbmfbox.Text == mbmfbox.PlaceholderText)
                {
                    mbmfbox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }
            if (!(mbmdbox.Text == ""))
            {
                if (mbmdbox.Text == mbmdbox.PlaceholderText)
                {
                    mbmdbox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }
            if (!(mbvbox.Text == ""))
            {
                if (mbvbox.Text == mbvbox.PlaceholderText)
                {
                    mbvbox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }
            if (!(biosmfbox.Text == ""))
            {
                if (biosmfbox.Text == biosmfbox.PlaceholderText)
                {
                    biosmfbox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }
            if (!(biosdbox.Text == ""))
            {
                if (biosdbox.Text == biosdbox.Text)
                {
                    //biosdbox.Text = "";
                }
                else
                {
                    isOK = false;
                }
            }

            await UT.waitstatus.close();

            if (isOK)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }
}
