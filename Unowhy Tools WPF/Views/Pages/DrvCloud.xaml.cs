using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Wpf.Ui.Controls;
using System.Windows.Controls;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Windows.Shapes;
using Wpf.Ui.Interop.WinDef;
using Newtonsoft.Json;
using System.Net;
using System.Windows.Media.Imaging;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class DrvCloud : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void GoForw(object sender, RoutedEventArgs e)
    {
        //UT.anim.TransitionForw(RootGrid);
    }

    public void applylang()
    {
        repo_txt.Text = UT.GetLang("bkcloudhost");
        repo_desc.Text = UT.GetLang("bkcloudhostdesc");
        submit.Content = UT.GetLang("bkcloud.submit");
        repobtn.Content = UT.GetLang("bkcloud.repo");
        refresh.Content = UT.GetLang("refresh");
    }

    public async void Init(object sender, EventArgs e)
    {
        refresh.IsEnabled = false;
        applylang();

        if (UT.CheckInternet())
        {
            repo_img.Source = UT.GetImgSource("clouddl.png");
            repo_desc.Text = UT.GetLang("bkcloudhostdesc");

            var repocard = repo;
            var sep = separator;
            RootStack.Children.Clear();
            RootStack.Children.Add(repocard);
            RootStack.Children.Add(sep);
            await SyncWithCloud();
        }
        else
        {
            repo_img.Source = UT.GetImgSource("nowifi.png");
            repo_desc.Text = UT.GetLang("nonet");
        }
        refresh.IsEnabled = true;
    }

    public async Task RestoreCloud(string filename, string link, double size)
    {
        DriveInfo drive = new DriveInfo("C");
        if(drive.AvailableFreeSpace > size * 3)
        {
            await UT.waitstatus.open(UT.GetLang("wait.download"), "clouddl.png");
            string uttemps = System.IO.Path.GetTempPath() + "\\Unowhy Tools\\Temps";
            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = UT.GetLang("wait.download");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "clouddl.png");
            };
            await UT.DlFilewithProgress(link, uttemps + $"\\{filename}", progress, cancellationToken.Token);
            await UT.waitstatus.open(UT.GetLang("wait.extract"), "zip.png");
            string source = uttemps + $"\\{filename}";
            string dest = uttemps + "\\Drivers";
            await Task.Run(() =>
            {
                ZipFile.ExtractToDirectory(source, dest);
            });
            await UT.waitstatus.open(UT.GetLang("wait.restore"), "download.png");
            List<string> list = new List<string>();
            foreach (string filePath in Directory.GetFiles(uttemps + $"\\Drivers", "*.inf"))
            {
                list.Add(filePath);
            }
            foreach (string subDirectory in Directory.GetDirectories(uttemps + $"\\Drivers"))
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

                await UT.waitstatus.open(UT.GetLang("wait.restore") + " (" + percentage + ")", "download.png");

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
                p.StartInfo.FileName = uttemps + $"\\Drivers" + "\\UT-Restore.exe";
                p.StartInfo.WorkingDirectory = uttemps + $"\\Drivers";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            });*/
            Directory.Delete(uttemps + "\\Drivers", true);
            Directory.CreateDirectory(uttemps + "\\Drivers");
            await UT.waitstatus.close();
            UT.DialogIShow(UT.GetLang("rebootmsg"), "reboot.png");
            Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
        }
        else
        {
            long needsize = drive.AvailableFreeSpace - (Convert.ToInt64(size) * Convert.ToInt64(3));
            string needsizes = UT.FormatSize(needsize);
            UT.DialogIShow(UT.GetLang("spacepc") + " " + needsizes, "no.png");
        }
    }

    public async Task SyncWithCloud()
    {
        SkeletonStack.Visibility = Visibility.Visible;

        await Task.Delay(1000);

        foreach (Rectangle rec in SkeletonRec)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = -300;
            anim.To = 300;
            anim.RepeatBehavior = RepeatBehavior.Forever;
            anim.Duration = TimeSpan.FromMilliseconds(1000);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };
            TranslateTransform trans = new TranslateTransform();
            rec.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);

            await Task.Delay(50);
        }

        await Task.Delay(1000);
        HttpClient client = new HttpClient();

        string json = await client.GetStringAsync("https://bit.ly/UTbkcloudjson");
        JArray array = JArray.Parse(json);

        string langString = await UT.Config.Get("Lang");

        SkeletonStack.Visibility = Visibility.Hidden;

        string datasurl = UT.online_datas;
        HttpClient web = new HttpClient();
        HttpResponseMessage rep = await web.GetAsync(datasurl);
        if (rep.StatusCode == HttpStatusCode.OK)
        {
            string jsonContent = await web.GetStringAsync(datasurl);
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonContent);
            if (jsonObject.drivers != null && jsonObject.drivers.Count > 0)
            {
                foreach (var driver in jsonObject.drivers)
                {
                    string title;
                    string desc;

                    string name = (string)driver["name"];
                    string author = (string)driver["author"];
                    string pcyear = (string)driver["pcyear"];
                    double size = (double)driver["size"];
                    string link = (string)driver["link"];

                    string description = "null";
                    if (langString == "EN")
                    {
                        description = (string)driver["description"]["en"];
                    }
                    else if (langString == "FR")
                    {
                        description = (string)driver["description"]["fr"];
                    }

                    size = size / (1024 * 1024 * 1024);

                    title = name + "  •  by " + author;
                    if (description == "")
                    {
                        desc = pcyear + "  •  " + size.ToString("#.##") + " GB";
                    }
                    else
                    {
                        desc = description + "  •  " + pcyear + "  •  " + size.ToString("#.##") + " GB";
                    }

                    await CreateCard(title, desc, name, link, size);
                }
            }
        }

        foreach (JObject obj in array)
        {
            
        }
    }

    public async Task CreateCard(string title, string desc, string filename, string link, double size)
    {
        CardControl cardControl = new CardControl
        {
            Margin = new Thickness(10, 0, 10, 10)
        };
        Grid grid = new Grid();
        StackPanel stackPanel = new StackPanel();
        TextBlock titleBlock = new TextBlock
        {
            Text = title,
            FontSize = 13,
            FontWeight = FontWeights.Medium
        };
        TextBlock descBlock = new TextBlock
        {
            Text = desc,
            FontSize = 12,
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bebebe")),
            TextWrapping = TextWrapping.Wrap
        };
        Wpf.Ui.Controls.Button button = new Wpf.Ui.Controls.Button
        {
            Content = UT.GetLang("restore")
        };

        button.Click += async (sender, e) =>
        {
            await RestoreCloud(filename, link, size);
        };

        stackPanel.Children.Add(titleBlock);
        stackPanel.Children.Add(descBlock);

        grid.Children.Add(stackPanel);

        cardControl.Header = grid;

        cardControl.Content = button;

        cardControl.Visibility = Visibility.Hidden;

        RootStack.Children.Add(cardControl);

        cardControl.Visibility = Visibility.Visible;
        DoubleAnimation opacityAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.5),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };

        DoubleAnimation translateAnimation = new DoubleAnimation
        {
            From = 30,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.5),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };

        TranslateTransform transform = new TranslateTransform();
        cardControl.RenderTransform = transform;

        cardControl.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);

        await Task.Delay(50);
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
                From = 30,
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

    public List<Rectangle> SkeletonRec = new List<Rectangle>();

    public DrvCloud(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();


        SkeletonRec.Add(Rec11);
        SkeletonRec.Add(Rec12);
        SkeletonRec.Add(Rec21);
        SkeletonRec.Add(Rec22);
        SkeletonRec.Add(Rec31);
        SkeletonRec.Add(Rec32);
        SkeletonRec.Add(Rec41);
        SkeletonRec.Add(Rec42);
        SkeletonRec.Add(Rec51);
        SkeletonRec.Add(Rec52);
        SkeletonRec.Add(Rec61);
        SkeletonRec.Add(Rec62);
        SkeletonRec.Add(Rec71);
        SkeletonRec.Add(Rec72);
        SkeletonRec.Add(Rec81);
        SkeletonRec.Add(Rec82);
        SkeletonRec.Add(Rec91);
        SkeletonRec.Add(Rec92);
    }

    private async void repo_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = await UT.OnlineDatas.GetUrls("utbkcloud")
        });
    }

    private async void refresh_Click(object sender, RoutedEventArgs e)
    {
        refresh.IsEnabled = false;
        if (UT.CheckInternet())
        {
            repo_img.Source = UT.GetImgSource("clouddl.png");
            repo_desc.Text = UT.GetLang("bkcloudhostdesc");

            var repocard = repo;
            var sep = separator;
            RootStack.Children.Clear();
            RootStack.Children.Add(repocard);
            RootStack.Children.Add(sep);
            await SyncWithCloud();
        }
        else
        {
            repo_img.Source = UT.GetImgSource("nowifi.png");
            repo_desc.Text = UT.GetLang("nonet");
        }
        refresh.IsEnabled = true;
    }

    private async void submit_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = await UT.OnlineDatas.GetUrls("discordinvite")
        });
    }
}
