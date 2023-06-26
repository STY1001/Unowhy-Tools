using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Windows;

using Unowhy_Tools;
using System.Diagnostics;
using System;
using Microsoft.Win32;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Net.Http;
using System.Net;
using System.Windows.Input;
using System.Xml.Linq;
using TaskScheduler = Microsoft.Win32.TaskScheduler;
using System.Runtime.Intrinsics.X86;
using System.IO;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class FirstConfig : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public bool InitOK = true;

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void Init(object sender, EventArgs e)
    {
        RootConfigGrid.Visibility = Visibility.Hidden;
        RootStateGrid.Visibility = Visibility.Hidden;

        string sn = await UT.RunReturn("wmic", "bios get serialnumber");
        string pn = await UT.RunReturn("hostname", "");
        string un = File.ReadAllText("C:\\UTSConfig\\serial.txt");

        pn = pn.Replace("\n", "").Replace("\r", "");
        sn = UT.GetLine(sn, 2);
        sn = sn.Replace(" ", "");

        if (pn.Contains("IFP"))
        {
            snbox.Text = pn;
        }

        if (sn.Contains("IFP"))
        {
            snbox.Text = sn;
        }

        if(!un.Contains("Null") && un.Contains("IFP"))
        {
            snbox.Text = un;
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
        if (utcuab == "1")
        {
            upcheck.IsChecked = true;
        }
        else
        {
            upcheck.IsChecked = false;
        }

        TaskScheduler.TaskService ts = new TaskScheduler.TaskService();
        TaskScheduler.Task uttltask = ts.GetTask("Unowhy Tools Tray Launch");
        if (uttltask != null)
        {
            if (uttltask.Definition.Settings.Enabled)
            {
                traycheck.IsChecked = true;
            }
            else
            {
                traycheck.IsChecked = false;
            }
        }
        else
        {
            traycheck.IsChecked = false;
            traycheck.IsEnabled = false;
        }

        StartHello.Visibility = Visibility.Visible;
        RootConfigGrid.Visibility = Visibility.Visible;
        RootStateGrid.Visibility = Visibility.Visible;
        fcimg.Source = UT.GetImgSource("hi.png");
        fctxt.Text = "Hi !";
        btn.Click += step1;
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 1000;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(1000);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 5 };

        TranslateTransform trans = new TranslateTransform();
        RootConfigGrid.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
        RootStateGrid.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);

    }

    public async void step1(object sender, RoutedEventArgs e)
    {
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = 0;
        anim.To = -1000;
        anim.Duration = TimeSpan.FromMilliseconds(300);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

        TranslateTransform trans = new TranslateTransform();
        RootConfigGrid.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
        RootStateGrid.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
        await Task.Delay(300);
        StartHello.Visibility = Visibility.Collapsed;
        StartLang.Visibility = Visibility.Visible;
        fcimg.Source = UT.GetImgSource("language.png");
        fctxt.Text = "Language";
        btn.Click -= step1;
        btn.Click += step2;
        
        anim = new DoubleAnimation();
        anim.From = 1000;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

        trans = new TranslateTransform();
        RootConfigGrid.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
        RootStateGrid.RenderTransform = trans;
        trans.BeginAnimation(TranslateTransform.XProperty, anim);
    }
    
    public async void step2(object sender, RoutedEventArgs e)
    {
        bool IsOK = true;

        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools", true);
        if (lang_en.IsSelected)
        {
            key.SetValue("Lang", "EN");
        }
        else if (lang_fr.IsSelected)
        {
            key.SetValue("Lang", "FR");
        }

        if (IsOK)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = -1000;
            anim.Duration = TimeSpan.FromMilliseconds(300);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

            TranslateTransform trans = new TranslateTransform();
            RootConfigGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            RootStateGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            await Task.Delay(300);
            StartLang.Visibility = Visibility.Collapsed;
            StartSerial.Visibility = Visibility.Visible;
            fcimg.Source = UT.GetImgSource("datamatrix.png");
            fctxt.Text = "Serial Number";
            btn.Click -= step2;
            btn.Click += step3;

            anim = new DoubleAnimation();
            anim.From = 1000;
            anim.To = 0;
            anim.Duration = TimeSpan.FromMilliseconds(600);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

            trans = new TranslateTransform();
            RootConfigGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            RootStateGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }

    public async void step3(object sender, RoutedEventArgs e)
    {
        bool IsOK = false;

        if (UT.CheckInternet())
        {
            await UT.waitstatus.open();
            var web = new HttpClient();
            string ssn = snbox.Text;
            string preurl = "https://storage.gra.cloud.ovh.net/v1/AUTH_765727b4bb3a465fa4e277aef1356869/idfconf"; //"https://idf.hisqool.com/conf";
            string configurl = $"{preurl}/devices/{ssn}/configuration";

            HttpResponseMessage response = await web.GetAsync(configurl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                File.Delete("C:\\UTSConfig\\serial.txt");
                File.WriteAllText("C:\\UTSConfig\\serial.txt", ssn);
                await System.Threading.Tasks.Task.Delay(1000);
                string nsn = File.ReadAllText("C:\\UTSConfig\\serial.txt");
                await UT.waitstatus.close();
                if (!(nsn == ssn))
                {
                    UT.DialogIShow(UT.GetLang("failed"), "no.png");
                    IsOK = true;
                    InitOK = false;
                }
                else
                {
                    IsOK = true;
                }
            }
            else
            {
                await UT.waitstatus.close();
                UT.DialogIShow(UT.GetLang("noid"), "no.png");
            }
        }
        else
        {
            UT.DialogIShow(UT.GetLang("noco"), "nowifi.png");
            IsOK = true;
            InitOK = false;
        }

        if (IsOK)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = -1000;
            anim.Duration = TimeSpan.FromMilliseconds(300);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

            TranslateTransform trans = new TranslateTransform();
            RootConfigGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            RootStateGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            await Task.Delay(300);
            StartSerial.Visibility = Visibility.Collapsed;
            StartUpdate.Visibility = Visibility.Visible;
            fcimg.Source = UT.GetImgSource("update.png");
            fctxt.Text = "Update";
            btn.Click -= step3;
            btn.Click += step4;

            anim = new DoubleAnimation();
            anim.From = 1000;
            anim.To = 0;
            anim.Duration = TimeSpan.FromMilliseconds(600);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

            trans = new TranslateTransform();
            RootConfigGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            RootStateGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }

    public async void step4(object sender, RoutedEventArgs e)
    {
        bool IsOK = true;

        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools", true);
        if (upcheck.IsChecked == true)
        {
            key.SetValue("UpdateStart", "1");

        }
        else
        {
            key.SetValue("UpdateStart", "0");
        }

        if (IsOK)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = -1000;
            anim.Duration = TimeSpan.FromMilliseconds(300);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

            TranslateTransform trans = new TranslateTransform();
            RootConfigGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            RootStateGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            await Task.Delay(300);
            StartUpdate.Visibility = Visibility.Collapsed;
            StartTray.Visibility = Visibility.Visible;
            fcimg.Source = UT.GetImgSource("trayicon.png");
            fctxt.Text = "Tray";
            btn.Click -= step4;
            btn.Click += step5;

            anim = new DoubleAnimation();
            anim.From = 1000;
            anim.To = 0;
            anim.Duration = TimeSpan.FromMilliseconds(600);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

            trans = new TranslateTransform();
            RootConfigGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            RootStateGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }

    public async void step5(object sender, RoutedEventArgs e)
    {
        bool IsOK = true;

        if (traycheck.IsEnabled == true)
        {
            TaskScheduler.TaskService ts = new TaskScheduler.TaskService();
            TaskScheduler.Task uttltask = ts.GetTask("Unowhy Tools Tray Launch");
            if (traycheck.IsChecked == true)
            {
                uttltask.Definition.Settings.Enabled = true;
            }
            else
            {
                uttltask.Definition.Settings.Enabled = false;
            }
            uttltask.RegisterChanges();
        }

        if (IsOK)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = -1000;
            anim.Duration = TimeSpan.FromMilliseconds(300);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

            TranslateTransform trans = new TranslateTransform();
            RootConfigGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            RootStateGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            await Task.Delay(300);
            StartTray.Visibility = Visibility.Collapsed;
            StartDone.Visibility = Visibility.Visible;
            fcimg.Source = UT.GetImgSource("yes.png");
            fctxt.Text = "Done !";
            btn_img.Source = UT.GetImgSource("yes.png");
            btn_txt.Text = "Ok";
            btn.Click -= step5;
            btn.Click += step6;

            anim = new DoubleAnimation();
            anim.From = 1000;
            anim.To = 0;
            anim.Duration = TimeSpan.FromMilliseconds(600);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

            trans = new TranslateTransform();
            RootConfigGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            RootStateGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }
    
    public async void step6(object sender, RoutedEventArgs e)
    {
        bool IsOK = true;

        if (IsOK)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = -1000;
            anim.Duration = TimeSpan.FromMilliseconds(300);
            anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

            TranslateTransform trans = new TranslateTransform();
            RootConfigGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            RootStateGrid.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
            await Task.Delay(300);
            StartDone.Visibility = Visibility.Collapsed;
            btn.Click -= step4;

            await Task.Delay(150);

            var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.RootNavigation.Visibility = Visibility.Visible;
            mainWindow.applylang();
            mainWindow.RootNavigation.TransitionType = Wpf.Ui.Animations.TransitionType.SlideRight;
            UT.NavigateTo(typeof(Dashboard));
            /*
            foreach (UIElement elements in mainWindow.RootNavigation.Items)
            {
                elements.Visibility = Visibility.Visible;
                mainWindow.back.Visibility = Visibility.Collapsed;
                DoubleAnimation opacityAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                DoubleAnimation translateAnimation = new DoubleAnimation
                {
                    From = -50,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };

                TranslateTransform transform = new TranslateTransform();
                elements.RenderTransform = transform;

                elements.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);

                await Task.Delay(50);
            }
            */

            await Task.Delay(1000);
            mainWindow.RootNavigation.TransitionType = Wpf.Ui.Animations.TransitionType.SlideBottom;

            RegistryKey keyf = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools", true);
            if (InitOK)
            {
                keyf.SetValue("Init2", "1");
            }
            else
            {
                keyf.SetValue("Init2", "0");
            }
        }
    }

    public FirstConfig(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
