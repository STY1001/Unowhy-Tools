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

    public void applylang()
    {
        labol.Text = UT.GetLang("logsopen");
        labdl.Text = UT.GetLang("logsdel");
        dl.Content = UT.GetLang("delete");
        ol.Content = UT.GetLang("open");
        lablang.Text = UT.GetLang("lang");
        labus.Text = UT.GetLang("cuab");
        labsn.Text = UT.GetLang("pcserial");
        labtray.Text = UT.GetLang("boottray");
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        UT.anim.BackBtnAnim(BackBTN);
        await System.Threading.Tasks.Task.Delay(150);
        UT.anim.TransitionBack(RootGrid);
        await System.Threading.Tasks.Task.Delay(200);
        UT.NavigateTo(typeof(Dashboard));
    }

    public async System.Threading.Tasks.Task CheckBTN()
    {
        string serial = await UT.UTS.UTSmsg("UTSW", "GetSN");
        sn.Text = serial;

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
        
        RegistryKey lcs = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
        string utlst = lcs.GetValue("Lang").ToString();
        if (utlst == "EN")
        {
            lang_en.IsSelected = true;
        }
        else
        {
            lang_fr.IsSelected = true;
        }

        string utcuab = lcs.GetValue("UpdateStart").ToString();
        if(utcuab == "1")
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
        dl.Content = UT.GetLang("clean") + " (" + size + ")";
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

        await UT.DeployBack(typeof(Dashboard), RootGrid);

        string fp = Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt";
        FileInfo fi = new FileInfo(fp);
        string size;
        if (fi.Length > 1024 * 1024) size = (fi.Length / (1024 * 1024)).ToString() + " MB";
        else if (fi.Length > 1024) size = (fi.Length / 1024).ToString() + " KB";
        else size = fi.Length.ToString() + " B";
        dl.Content = UT.GetLang("clean") + " (" + size + ")";

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
    }

    public void Logs_Clear(object sender, RoutedEventArgs e)
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
        dl.Content = UT.GetLang("clean") + " (" + size + ")";
    }

    public void Logs_Open(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt",
            UseShellExecute = true
        });
    }

    public async void Apply_Settings(object sender, RoutedEventArgs e)
    {
        await UT.waitstatus.open();
        bool ok = true;

        if (UT.CheckInternet())
        {
            var web = new HttpClient();
            string ssn = sn.Text;
            string preurl = "https://storage.gra.cloud.ovh.net/v1/AUTH_765727b4bb3a465fa4e277aef1356869/idfconf"; //"https://idf.hisqool.com/conf";
            string configurl = $"{preurl}/devices/{ssn}/configuration";

            HttpResponseMessage response = await web.GetAsync(configurl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await UT.UTS.UTSmsg("UTSW", $"SetSN:{ssn}");
                await System.Threading.Tasks.Task.Delay(1000);
                string nsn = await UT.UTS.UTSmsg("UTSW", "GetSN"); 
                if(!(nsn == ssn))
                {
                    await UT.waitstatus.close();
                    UT.DialogIShow(UT.GetLang("failed"), "no.png");
                    await UT.waitstatus.open();
                }
            }
            else
            {
                await UT.waitstatus.close();
                ok = false;
                UT.DialogIShow(UT.GetLang("noid"), "no.png");
                await UT.waitstatus.open();
            }
        }
        else
        {
            await UT.waitstatus.close();
            UT.DialogIShow(UT.GetLang("noco"), "nowifi.png");
            await UT.waitstatus.open();
        }

        if (ok)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools", true);
            if (lang_en.IsSelected)
            {
                key.SetValue("Lang", "EN");
            }
            else if (lang_fr.IsSelected)
            {
                key.SetValue("Lang", "FR");
            }

            if (us.IsChecked == true)
            {
                key.SetValue("UpdateStart", "1");

            }
            else
            {
                key.SetValue("UpdateStart", "0");
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
