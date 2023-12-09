using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Microsoft.Win32.TaskScheduler;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using System.Threading;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Settings : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async System.Threading.Tasks.Task applylang()
    {
        labol.Text = await UT.GetLang("logsopen");
        labdl.Text = await UT.GetLang("logsdel");
        dl.Content = await UT.GetLang("delete");
        ol.Content = await UT.GetLang("open");
        lablang.Text = await UT.GetLang("lang");
        labus.Text = await UT.GetLang("cuab");
        labsn.Text = await UT.GetLang("pcserial");
        labtray.Text = await UT.GetLang("boottray");
        labws.Text = await UT.GetLang("wifisync");
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        //UT.anim.BackBtnAnim(BackBTN);
        await System.Threading.Tasks.Task.Delay(150);
        //UT.anim.TransitionBack(RootGrid);
        await System.Threading.Tasks.Task.Delay(200);
        UT.NavigateTo(typeof(Dashboard));
    }

    public async System.Threading.Tasks.Task CheckBTN()
    {
        string serial = await UT.UTS.UTSmsg("UTSW", "GetSN");
        sn.Text = serial;

        if((await UT.UTS.UTSmsg("UTSW", "GetWS")).Contains("True"))
        {
            ws.IsChecked = true;
        }
        else
        {
            ws.IsChecked = false;
        }

        if ((await UT.UTS.UTSmsg("UTSW", "GetSN")).Contains("Null"))
        {
            ws.IsChecked = false;
            ws.IsEnabled = false;
            sn.Text = "";
        }

            TaskService ts = new TaskService();
        Microsoft.Win32.TaskScheduler.Task uttltask = ts.GetTask("Unowhy Tools Tray Launch");
        if(uttltask != null)
        {
            if (uttltask.Definition.Settings.Enabled)
            {
                tray.IsChecked = true;
            }
            else
            {
                tray.IsChecked = false;
            }
        }
        else
        {
            tray.IsChecked = false;
            tray.IsEnabled = false;
        }
        
        if (await UT.Config.Get("Lang") == "EN")
        {
            lang_en.IsSelected = true;
        }
        else
        {
            lang_fr.IsSelected = true;
        }

        if(await UT.Config.Get("UpdateStart") == "1")
        {
            us.IsChecked = true;
        }
        else
        {
            us.IsChecked = false;
        }

        string fp = Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt";
        FileInfo fi = new FileInfo(fp);
        string size;
        if (fi.Length > 1024 * 1024) size = (fi.Length / (1024 * 1024)).ToString() + " MB";
        else if (fi.Length > 1024) size = (fi.Length / 1024).ToString() + " KB";
        else size = fi.Length.ToString() + " B";
        dl.Content = await UT.GetLang("clean") + " (" + size + ")";
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in RootStack2.Children)
        {
            element.Visibility = Visibility.Hidden;
        }
        foreach (UIElement element in RootStack3.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(Dashboard), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);


        string fp = Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt";
        FileInfo fi = new FileInfo(fp);
        string size;
        if (fi.Length > 1024 * 1024) size = (fi.Length / (1024 * 1024)).ToString() + " MB";
        else if (fi.Length > 1024) size = (fi.Length / 1024).ToString() + " KB";
        else size = fi.Length.ToString() + " B";
        dl.Content = await UT.GetLang("clean") + " (" + size + ")";

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

            await System.Threading.Tasks.Task.Delay(50);
        }
        foreach (UIElement element in RootStack2.Children)
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

            await System.Threading.Tasks.Task.Delay(50);
        }
        foreach (UIElement element in RootStack3.Children)
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

            await System.Threading.Tasks.Task.Delay(50);
        }
    }

    public async void Logs_Clear(object sender, RoutedEventArgs e)
    {
        string fp = Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt";
        File.Create(fp).Close();
        UT.Write2Log("============");
        UT.Write2Log("Unowhy Tools");
        UT.Write2Log(" by STY1001 ");
        UT.Write2Log("============");
        UT.Write2Log("");
        UT.Write2Log("Start of logs, logs are saved locally and can help to fix issues");
        UT.Write2Log("If you have an issue, please, open an issue on Github and give me logs to help you");
        UT.Write2Log("You can cleanup logs on UT's settings");
        UT.Write2Log("Last logs are on bottom\n\n\n");
        FileInfo fi = new FileInfo(fp);
        string size;
        if (fi.Length > 1024 * 1024) size = (fi.Length / 1024 * 1024).ToString() + " MB";
        else if (fi.Length > 1024) size = (fi.Length / 1024).ToString() + " KB";
        else size = fi.Length.ToString() + " B";
        dl.Content = await UT.GetLang("clean") + " (" + size + ")";
    }

    public void Logs_Open(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt",
            UseShellExecute = true
        });
    }

    public void Live_Logs(object sender, RoutedEventArgs e)
    {
        string logspath = Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt";
        string args = "\"type '" + logspath + "' -wait\"";
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "powershell",
            Arguments = args,
            UseShellExecute = true
        });
    }

    public async void Apply_Settings(object sender, RoutedEventArgs e)
    {
        await UT.waitstatus.open(await UT.GetLang("wait.apply"), "customize.png");
        bool ok = true;

        if(sn.Text == "")
        {

        }
        else
        {
            if (UT.CheckInternet())
            {
                var web = new HttpClient();
                string ssn = sn.Text;
                string preurl = await UT.OnlineDatas.GetUrls("idfconf");
                string configurl = $"{preurl}/devices/{ssn}/configuration";

                HttpResponseMessage response = await web.GetAsync(configurl);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await UT.UTS.UTSmsg("UTSW", "SetSN:" + ssn);
                    await System.Threading.Tasks.Task.Delay(1000);
                    string nsn = await UT.UTS.UTSmsg("UTSW", "GetSN");
                    if (!(nsn == ssn))
                    {
                        await UT.waitstatus.close();
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                        await UT.waitstatus.open(await UT.GetLang("wait.apply"), "customize.png");
                    }
                }
                else
                {
                    await UT.waitstatus.close();
                    ok = false;
                    UT.DialogIShow(await UT.GetLang("noid"), "no.png");
                }
            }
            else
            {
                await UT.waitstatus.close();
                UT.DialogIShow(await UT.GetLang("noco"), "nowifi.png");
                await UT.waitstatus.open(await UT.GetLang("wait.apply"), "customize.png");
            }
        }

        if (ok)
        {
            if (ws.IsEnabled)
            {
                if (ws.IsChecked == true)
                {
                    await UT.UTS.UTSmsg("UTSW", "SetWS:" + "True");
                    await System.Threading.Tasks.Task.Delay(1000);
                    if (!(await UT.UTS.UTSmsg("UTSW", "GetWS") == "True"))
                    {
                        await UT.waitstatus.close();
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                        await UT.waitstatus.open(await UT.GetLang("wait.apply"), "customize.png");
                    }
                }
                else
                {
                    await UT.UTS.UTSmsg("UTSW", "SetWS:" + "False");
                    await System.Threading.Tasks.Task.Delay(1000);
                    if (!(await UT.UTS.UTSmsg("UTSW", "GetWS") == "False"))
                    {
                        await UT.waitstatus.close();
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                        await UT.waitstatus.open(await UT.GetLang("wait.apply"), "customize.png");
                    }
                }
            }

            if (lang_en.IsSelected)
            {
                await UT.Config.Set("Lang", "EN");
            }
            else if (lang_fr.IsSelected)
            {
                await UT.Config.Set("Lang", "FR");
            }

            if (us.IsChecked == true)
            {
                await UT.Config.Set("UpdateStart", "1");

            }
            else
            {
                await UT.Config.Set("UpdateStart", "0");
            }

            if(tray.IsEnabled == true)
            {
                TaskService ts = new TaskService();
                Microsoft.Win32.TaskScheduler.Task uttltask = ts.GetTask("Unowhy Tools Tray Launch");
                if (tray.IsChecked == true)
                {
                    uttltask.Definition.Settings.Enabled = true;
                }
                else
                {
                    uttltask.Definition.Settings.Enabled = false;
                }
                uttltask.RegisterChanges();
            }

            UT.RunAdmin($"-user {UTdata.UserID}");
        }
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN();
    }

    public Settings(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
