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
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(Dashboard));
    }

    public async Task CheckBTN()
    {
        string serial = await UT.UTS.UTSmsg("UTSW", "GetSN");
        sn.Text = serial;

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
        UT.anim.BackBtnAnimForw(BackBTN);

        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        string fp = Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt";
        FileInfo fi = new FileInfo(fp);
        string size;
        if (fi.Length > 1024 * 1024) size = (fi.Length / (1024 * 1024)).ToString() + " MB";
        else if (fi.Length > 1024) size = (fi.Length / 1024).ToString() + " KB";
        else size = fi.Length.ToString() + " B";
        dl.Content = UT.GetLang("clean") + " (" + size + ")";

        await Task.Delay(150);

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

    public void Logs_Clear(object sender, RoutedEventArgs e)
    {
        string fp = Path.GetTempPath() + "\\Unowhy Tools\\Logs\\UT_Logs.txt";
        File.Create(fp).Close();
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
            string configurl = $"https://idf.hisqool.com/conf/devices/{ssn}/configuration";

            HttpResponseMessage response = await web.GetAsync(configurl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await UT.UTS.UTSmsg("UTSW", $"SetSN:{ssn}");
                await Task.Delay(1000);
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
