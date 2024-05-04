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

        string sn = await UT.RunReturn("wmic", "bios get serialnumber");
        string pn = await UT.RunReturn("hostname", "");
        string un = await UT.UTS.UTSmsg("UTSW", "GetSN");

        pn = pn.Replace("\n", "").Replace("\r", "");
        sn = UT.GetLine(sn, 2).TrimEnd();

        if (pn.Contains("IFP"))
        {
            snbox.Text = pn;
        }

        if (sn.Contains("IFP"))
        {
            snbox.Text = sn;
        }

        if (!un.Contains("Null") && un.Contains("IFP"))
        {
            snbox.Text = un;
        }

        if (await UT.Config.Get("Lang") == "EN")
        {
            lang_en.IsSelected = true;
        }
        else if (await UT.Config.Get("Lang") == "FR")
        {
            lang_fr.IsSelected = true;
        }
        if (await UT.Config.Get("UpdateStart") == "1")
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

        if (lang_en.IsSelected)
        {
            await UT.Config.Set("Lang", "EN");
        }
        else if (lang_fr.IsSelected)
        {
            await UT.Config.Set("Lang", "FR");
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
        if (snbox.Text == "")
        {
            if (UT.DialogQShow("Skip", "question.png"))
            {
                IsOK = true;
                InitOK = true;
            }
            else
            {
                IsOK = false;
            }
        }
        else
        {
            if (await UT.CheckInternet())
            {
                await UT.waitstatus.open(await UT.GetLang("wait.apply"), "datamatrix.png");
                var web = new HttpClient();
                string ssn = snbox.Text;
                string preurl = await UT.OnlineDatas.GetUrls("idfconf");
                string configurl = $"{preurl}/devices/{ssn}/configuration";

                HttpResponseMessage response = await web.GetAsync(configurl);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await UT.UTS.UTSmsg("UTSW", "SetSN:" + ssn);
                    await System.Threading.Tasks.Task.Delay(1000);
                    string nsn = await UT.UTS.UTSmsg("UTSW", "GetSN");
                    await UT.waitstatus.close();
                    if (!(nsn == ssn))
                    {
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                        IsOK = true;
                        InitOK = true;
                    }
                    else
                    {
                        IsOK = true;
                    }
                }
                else
                {
                    await UT.waitstatus.close();
                    UT.DialogIShow(await UT.GetLang("noid"), "no.png");
                }
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                IsOK = true;
                InitOK = true;
            }
        }

        if ((await UT.UTS.UTSmsg("UTSW", "GetSN")).Contains("Null"))
        {
            wifisync.IsChecked = false;
            wifisync.IsEnabled = false;
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

        if (upcheck.IsChecked == true)
        {
            await UT.Config.Set("UpdateStart", "1");

        }
        else
        {
            await UT.Config.Set("UpdateStart", "0");
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
            StartWifi.Visibility = Visibility.Visible;
            fcimg.Source = UT.GetImgSource("wifi.png");
            fctxt.Text = "Wifi";
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

        if (wifisync.IsEnabled)
        {
            await UT.waitstatus.open(await UT.GetLang("wait.apply"), "wifi.png");
            if (wifisync.IsChecked == true)
            {
                await UT.UTS.UTSmsg("UTSW", "SetWS:" + "True");
                await System.Threading.Tasks.Task.Delay(1000);
                if (!(await UT.UTS.UTSmsg("UTSW", "GetWS") == "True"))
                {
                    await UT.waitstatus.close();
                    UT.DialogIShow(await UT.GetLang("failed"), "no.png");
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
                }
            }
            await UT.waitstatus.close();
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
            StartWifi.Visibility = Visibility.Collapsed;
            StartDone.Visibility = Visibility.Visible;
            fcimg.Source = UT.GetImgSource("yes.png");
            fctxt.Text = "Done !";
            btn_img.Source = UT.GetImgSource("yes.png");
            btn_txt.Text = "Ok";
            btn.Click -= step6;
            btn.Click += step7;

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

    public async void step7(object sender, RoutedEventArgs e)
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

            await Task.Delay(150);

            /*var mainWindow = System.Windows.Application.Current.MainWindow as Unowhy_Tools_WPF.Views.MainWindow;
            mainWindow.RootNavigation.Visibility = Visibility.Visible;
            mainWindow.applylang();
            mainWindow.RootNavigation.TransitionType = Wpf.Ui.Animations.TransitionType.None;
            UT.NavigateTo(typeof(Dashboard));
            
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
            

            await Task.Delay(1000);
            mainWindow.RootNavigation.TransitionType = Wpf.Ui.Animations.TransitionType.None;*/

            if (InitOK)
            {
                await UT.Config.Set("Init", "1");
            }
            else
            {
                await UT.Config.Set("Init", "0");
            }

            UT.RunAdmin($"-user {UTdata.UserID}");
        }
    }

    public FirstConfig(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
