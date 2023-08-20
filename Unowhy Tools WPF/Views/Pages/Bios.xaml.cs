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
            dlstatus.Visibility = Visibility.Visible;
            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            progress.ProgressChanged += (sender, value) =>
            {
                dlstatus.Text = "Downloading... (" + value.ToString("###.#") + "%)";
            };
            await UT.DlFilewithProgress("https://bit.ly/UTAFUWin", Path.GetTempPath() + "Unowhy Tools\\Temps\\AFUWin.zip", progress, cancellationToken.Token);
            dlstatus.Visibility = Visibility.Collapsed;
            ZipFile.ExtractToDirectory(Path.GetTempPath() + "Unowhy Tools\\Temps\\AFUWin.zip", Path.GetTempPath() + "\\Unowhy Tools\\Temps\\AMI\\AFU");
        }
        if (resname == "AMIDE")
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
            ZipFile.ExtractToDirectory(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMIDEWin.zip", Path.GetTempPath() + "\\Unowhy Tools\\Temps\\AMI\\AMIDE");
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
        if (UT.DialogQShow(UT.GetLang("utbdumpwarn"), "upload.png"))
        {
            if (afufiles.Any(file => !File.Exists(file)))
            {
                UT.DialogIShow(UT.GetLang("needres"), "download.png");
                if (UT.CheckInternet())
                {
                    await UT.waitstatus.open();
                    await DlRes("AFU");
                    await UT.waitstatus.close();
                }
                else
                {
                    UT.DialogIShow(UT.GetLang("nonet"), "nowifi.png");
                }
            }
            if (afufiles.Any(file => File.Exists(file)))
            {
                await UT.waitstatus.open();
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe", $"\"{flashpath.Text}\" /O");
                await UT.waitstatus.close();
            }
        }
    }

    private async void flashbtn_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("utbflashwarn"), "download.png"))
        {
            if (afufiles.Any(file => !File.Exists(file)))
            {
                UT.DialogIShow(UT.GetLang("needres"), "download.png");
                if (UT.CheckInternet())
                {
                    await UT.waitstatus.open();
                    await DlRes("AFU");
                    await UT.waitstatus.close();
                }
                else
                {
                    UT.DialogIShow(UT.GetLang("nonet"), "nowifi.png");
                }
            }
            if (afufiles.Any(file => File.Exists(file)))
            {
                await UT.waitstatus.open();
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe", $"\"{flashpath.Text}\" /P /N /REBOOT");
                await UT.waitstatus.close();
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
            UT.DialogIShow(UT.GetLang("needres"), "download.png");
            if (UT.CheckInternet())
            {
                await UT.waitstatus.open();
                await DlRes("AMIDE");
                await UT.waitstatus.close();
            }
            else
            {
                UT.DialogIShow(UT.GetLang("nonet"), "nowifi.png");
            }
        }
        if (amidefiles.Any(file => File.Exists(file)))
        {
            if (!(mfbox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/SM {mfbox.Text}");
            }
            if (!(mdbox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/SV {mdbox.Text}");
            }
            if (!(skubox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/SK {skubox.Text}");
            }
            if (!(snbox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/SS {snbox.Text}");
            }
            if (!(biosvbox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/IV {biosvbox.Text}");
            }
            if (!(mbmfbox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/BM {mbmfbox.Text}");
            }
            if (!(mbmdbox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/BP {mbmdbox.Text}");
            }
            if (!(mbvbox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/BV {mbvbox.Text}");
            }
            if (!(biosmfbox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/IVN {biosmfbox.Text}");
            }
            if (!(biosdbox.Text == ""))
            {
                await UT.RunMin(Path.GetTempPath() + "Unowhy Tools\\Temps\\AMI\\AMIDE\\" + "AMIDEWINx64.exe", $"/ID {biosdbox.SelectedDate.Value.ToString("MM/dd/yyyy")}");
            }

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
