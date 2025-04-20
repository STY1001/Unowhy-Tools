using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Unowhy_Tools;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Common;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;
using MenuItem = System.Windows.Controls.MenuItem;

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

    public async Task applylang()
    {
        repo_txt.Text = await UT.GetLang("bkcloudhost");
        repo_desc.Text = await UT.GetLang("bkcloudhostdesc");
        submit.Content = await UT.GetLang("bkcloud.submit");
        repobtn.Content = await UT.GetLang("bkcloud.repo");
        refresh.Content = await UT.GetLang("refresh");
        showold.Content = await UT.GetLang("showold");
        selectall.Content = await UT.GetLang("allver");
        gpudriver.Content = await UT.GetLang("igpudrv");
        restlang = await UT.GetLang("restore");
    }

    public async void Init(object sender, EventArgs e)
    {
        refresh.IsEnabled = false;
        showold.IsEnabled = false;
        versionselect.IsEnabled = false;
        applylang();
        string currentsku = UT.GetWMI("Win32_ComputerSystem", "SystemSKUNumber");
        if (currentsku.Contains("Y13G201S4EI") || currentsku.Contains("Y13G201S4E") || currentsku.Contains("STYL13G2"))
        {
            selectY132024.IsSelected = true;
        }
        else if (currentsku.Contains("Y13G113S4EI") || currentsku.Contains("Y13G113S4E") || currentsku.Contains("STYL13G1"))
        {
            selectY132023.IsSelected = true;
        }
        else if (currentsku.Contains("Y13G012S4EI") || currentsku.Contains("Y13G012S4E"))
        {
            selectY132022.IsSelected = true;
        }
        else if (currentsku.Contains("Y13G011S4EI") || currentsku.Contains("Y13G011S4E"))
        {
            selectY132021.IsSelected = true;
        }
        else if (currentsku.Contains("Y13G010S4EI") || currentsku.Contains("Y13G010S4E"))
        {
            selectY132020.IsSelected = true;
        }
        else if (currentsku.Contains("Y13G002S4EI") || currentsku.Contains("Y13G002S4E"))
        {
            selectY132019.IsSelected = true;
        }
        else if (currentsku.Contains("20180329314"))
        {
            selectY13m3.IsSelected = true;
        }
        else if (currentsku.Contains("Y11G201S2M"))
        {
            selectY13m3.IsSelected = true;
        }
        else if (currentsku.Contains("Y11G001S4E"))
        {
            selectY11G1.IsSelected = true;
        }
        else if (currentsku.Contains("OPSG530S2M") || currentsku.Contains("STYDS5OPS"))
        {
            selectY5OPSi5.IsSelected = true;
        }
        else if (currentsku.Contains("Y14G102S2E") || currentsku.Contains("Y14G310S2MI") || currentsku.Contains("Y14G310S2M"))
        {
            selectY14i3.IsSelected = true;
        }
        else if (currentsku.Contains("Y14G520S2MI") || currentsku.Contains("Y14G520S2M") || currentsku.Contains("Y14G530S2MI") || currentsku.Contains("Y14G530S2M"))
        {
            selectY14i5.IsSelected = true;
        }
        else
        {
            selectall.IsSelected = true;
        }

        if (await UT.CheckInternet())
        {
            repo_img.Source = UT.GetImgSource("clouddl.png");
            repo_desc.Text = await UT.GetLang("bkcloudhostdesc");

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
            repo_desc.Text = await UT.GetLang("nonet");
        }
        showold.IsEnabled = true;
        versionselect.IsEnabled = true;
        refresh.IsEnabled = true;
        versionselect.SelectionChanged += versionselect_SelectionChanged;
    }

    string postdrv = "noop";
    bool shutafter = false;
    public async Task RestoreCloud(string filename, string link, double size, bool ispostgpu)
    {
        if (!ispostgpu)
        {
            postdrv = "noop";
            if (UT.DialogQShow(await UT.GetLang("alsoigpudrv"), "gpu.png"))
            {
                while (postdrv == "noop")
                {
                    System.Windows.Controls.ContextMenu iGPUMenu = new System.Windows.Controls.ContextMenu();
                    MenuItem GJItem = new MenuItem();
                    GJItem.Header = "Sky/Kaby/Apollo/Gemini/Jasper/Comet Lake (Y11 360, Y13, Y14 Plus (Old))";
                    GJItem.Click += async (sender, e) =>
                    {
                        postdrv = "GJ";
                        var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
                        mainWindow.SnackBarService.ShowAsync("iGPU driver post install", "iGPU driver (for Sky/Kaby/Apollo/Gemini/Jasper/Comet Lake) will be installed after non-iGPU drivers installation", SymbolRegular.Checkmark20, ControlAppearance.Success);
                    };
                    iGPUMenu.Items.Add(GJItem);
                    MenuItem TItem = new MenuItem();
                    TItem.Header = "Tiger/Alder Lake (Y5OPS, Y14 Plus, Y14 Plus (New))";
                    TItem.Click += async (sender, e) =>
                    {
                        postdrv = "T";
                        var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
                        mainWindow.SnackBarService.ShowAsync("iGPU driver post install", "iGPU driver (for Tiger Lake) will be installed after non-iGPU drivers installation", SymbolRegular.Checkmark20, ControlAppearance.Success);
                    };
                    iGPUMenu.Items.Add(TItem);
                    iGPUMenu.PlacementTarget = gpudriver;
                    iGPUMenu.IsOpen = true;

                    UT.waitstatus.open("Waiting selection...", "gpu.png");

                    while (iGPUMenu.IsOpen)
                    {
                        await Task.Delay(100);
                    }
                }
            }
        }

        UT.waitstatus.close();

        if (UT.DialogQShow(await UT.GetLang("shutdrvcloud"), "boot.png"))
        {
            shutafter = true;
            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.SnackBarService.ShowAsync("Shut down after driver installation", "Your PC will shut down after driver installation", SymbolRegular.Power20, ControlAppearance.Caution);
        }

        UT.SendAction("DrvCloud");
        DriveInfo drive = new DriveInfo("C");
        if (drive.AvailableFreeSpace > size * 3)
        {
            await UT.waitstatus.open(await UT.GetLang("wait.download"), "clouddl.png");
            string uttemps = UT.utpath + "\\Unowhy Tools\\Temps";
            var progress = new System.Progress<double>();
            var cancellationToken = new CancellationTokenSource();
            string dl = await UT.GetLang("wait.download");
            progress.ProgressChanged += async (sender, value) =>
            {
                await UT.waitstatus.open(dl + " (" + value.ToString("##0.0") + "%)", "null");
            };
            await UT.DlFilewithProgress(link, uttemps + $"\\{filename}", progress, cancellationToken.Token);
            await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
            string source = uttemps + $"\\{filename}";
            string dest = uttemps + "\\Drivers";
            await Task.Run(() =>
            {
                ZipFile.ExtractToDirectory(source, dest, true);
            });
            await UT.waitstatus.open(await UT.GetLang("wait.restore"), "download.png");
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

                if (list.Count == 1)
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.restore"), "null");
                }
                else
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.restore") + " (" + percentage + ")", "null");
                }

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
            File.Delete(uttemps + $"\\{filename}");
            Directory.Delete(uttemps + "\\Drivers", true);
            Directory.CreateDirectory(uttemps + "\\Drivers");

            if (ispostgpu)
            {
                postdrv = "noop";
            }

            if (postdrv == "GJ")
            {
                UT.SendAction("DrvCloud.PostDrv");
                await RestoreCloud("iGPU_GJ.zip", await UT.OnlineDatas.GetUrls("gpudrvgj"), 1337172998, true);
                return;
            }
            if (postdrv == "T")
            {
                UT.SendAction("DrvCloud.PostDrv");
                await RestoreCloud("iGPU_T.zip", await UT.OnlineDatas.GetUrls("gpudrvt"), 2013214094, true);
                return;
            }

            if (postdrv == "noop")
            {
                if (!shutafter)
                {
                    await UT.waitstatus.close();
                    UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
                    Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
                }
                else
                {
                    await UT.waitstatus.close();
                    Process.Start("shutdown", "-s -t 10 -c \"Unowhy Tools\"");
                }
            }
        }
        else
        {
            long needsize = drive.AvailableFreeSpace - (Convert.ToInt64(size) * Convert.ToInt64(3));
            string needsizes = UT.FormatSize(needsize);
            UT.DialogIShow(await UT.GetLang("spacepc") + " " + needsizes, "no.png");
        }
    }

    private bool syncing = false;
    public async Task SyncWithCloud()
    {
        if (!syncing)
        {
            syncing = true;
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

            string langString = await UT.Config.Get("Lang");

            SkeletonStack.Visibility = Visibility.Collapsed;

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
                        string pcmodel = (string)driver["pcmodel"];
                        double size = (double)driver["size"];
                        string link = (string)driver["link"];
                        bool old = (bool)driver["old"];

                        if ((pcmodel == "Y13G201S4EI" || pcmodel == "Y13G201S4E") && !selectY132024.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if ((pcmodel == "Y13G113S4EI" || pcmodel == "Y13G113S4E") && !selectY132023.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if ((pcmodel == "Y13G012S4EI" || pcmodel == "Y13G012S4E") && !selectY132022.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if ((pcmodel == "Y13G011S4EI" || pcmodel == "Y13G011S4E") && !selectY132021.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if ((pcmodel == "Y13G010S4EI" || pcmodel == "Y13G010S4E") && !selectY132020.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if ((pcmodel == "Y13G002S4EI" || pcmodel == "Y13G002S4E") && !selectY132019.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if ((pcmodel == "Y14G102S2E" || pcmodel == "Y14G310S2M" || pcmodel == "Y14G310S2MI") && !selectY14i3.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if ((pcmodel == "Y14G520S2M" || pcmodel == "Y14G520S2MI" || pcmodel == "Y14G530S2MI" || pcmodel == "Y14G530S2M") && !selectY14i5.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if (pcmodel == "20180329314" && !selectY13m3.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if (pcmodel == "OPSG530S2M" && !selectY5OPSi5.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if (pcmodel == "Y11G201S2M" && !selectY11G2.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if (pcmodel == "Y11G001S4E" && !selectY11G1.IsSelected && !selectall.IsSelected)
                        {
                            continue;
                        }
                        if (old && showold.IsChecked == false)
                        {
                            continue;
                        }

                        string description = "null";
                        if (langString == "EN")
                        {
                            description = (string)driver["description"]["en"];
                        }
                        else if (langString == "FR")
                        {
                            description = (string)driver["description"]["fr"];
                        }

                        size = size / (1024 * 1024);
                        string model = UT.skumodel[pcmodel] == null ? pcmodel : UT.skumodel[pcmodel];
                        title = name + "  •  by " + author;
                        if (description == "")
                        {
                            desc = model + "  •  " + size.ToString("0") + " MB";
                        }
                        else
                        {
                            desc = description + "  •  " + model + "  •  " + size.ToString("0") + " MB";
                        }

                        await CreateCard(title, desc, name, link, size);
                    }
                }
            }

            syncing = false;
        }
    }

    string restlang;

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
            Content = restlang
        };

        button.Click += async (sender, e) =>
        {
            await RestoreCloud(filename, link, size, false);
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
        try
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
        catch (Exception ex)
        {

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
        showold.IsEnabled = false;
        versionselect.IsEnabled = false;
        if (await UT.CheckInternet())
        {
            repo_img.Source = UT.GetImgSource("clouddl.png");
            repo_desc.Text = await UT.GetLang("bkcloudhostdesc");

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
            repo_desc.Text = await UT.GetLang("nonet");
        }
        showold.IsEnabled = true;
        versionselect.IsEnabled = true;
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

    private async void versionselect_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        refresh.IsEnabled = false;
        showold.IsEnabled = false;
        versionselect.IsEnabled = false;
        if (await UT.CheckInternet())
        {
            repo_img.Source = UT.GetImgSource("clouddl.png");
            repo_desc.Text = await UT.GetLang("bkcloudhostdesc");

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
            repo_desc.Text = await UT.GetLang("nonet");
        }
        showold.IsEnabled = true;
        versionselect.IsEnabled = true;
        refresh.IsEnabled = true;
    }

    private async void showold_Changed(object sender, RoutedEventArgs e)
    {
        refresh.IsEnabled = false;
        showold.IsEnabled = false;
        versionselect.IsEnabled = false;
        if (await UT.CheckInternet())
        {
            repo_img.Source = UT.GetImgSource("clouddl.png");
            repo_desc.Text = await UT.GetLang("bkcloudhostdesc");

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
            repo_desc.Text = await UT.GetLang("nonet");
        }
        showold.IsEnabled = true;
        versionselect.IsEnabled = true;
        refresh.IsEnabled = true;
    }

    private async void gpudriver_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.Controls.ContextMenu iGPUMenu = new System.Windows.Controls.ContextMenu();
        MenuItem GJItem = new MenuItem();
        GJItem.Header = "Sky/Kaby/Apollo/Gemini/Jasper/Comet Lake (Y11 360, Y13, Y14 Plus (Old))";
        GJItem.Click += async (sender, e) =>
        {
            await RestoreCloud("iGPU_GJ.zip", await UT.OnlineDatas.GetUrls("gpudrvgj"), 1337172998, true);
        };
        iGPUMenu.Items.Add(GJItem);
        MenuItem TItem = new MenuItem();
        TItem.Header = "Tiger/Alder Lake (Y5OPS, Y14 Plus, Y14 Plus (New))";
        TItem.Click += async (sender, e) =>
        {
            await RestoreCloud("iGPU_T.zip", await UT.OnlineDatas.GetUrls("gpudrvt"), 2013214094, true);
        };
        iGPUMenu.Items.Add(TItem);
        iGPUMenu.PlacementTarget = gpudriver;
        iGPUMenu.IsOpen = true;
    }
}
