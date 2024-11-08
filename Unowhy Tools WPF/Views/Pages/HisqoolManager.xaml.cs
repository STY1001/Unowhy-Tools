using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Unowhy_Tools;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class HisqoolManager : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    Color disabled = (Color)ColorConverter.ConvertFromString("#888888");
    Color enabled = (Color)ColorConverter.ConvertFromString("#FFFFFF");

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async Task applylang()
    {
        hsqm_txt.Text = await UT.GetLang("labhsmq");
        hsqm_desc.Text = await UT.GetLang("deschism");
        hsqm_start_txt.Text = await UT.GetLang("start");
        hsqm_stop_txt.Text = await UT.GetLang("stop");
        hsqm_enable_txt.Text = await UT.GetLang("enable");
        hsqm_disable_txt.Text = await UT.GetLang("disable");
        hsqm_delete_txt.Text = await UT.GetLang("delserv");
        hsmpanel_txt.Text = await UT.GetLang("hsmpanel");
        hsmpanel_desc.Text = await UT.GetLang("deschsmpanel");
        hsmlivelog_txt.Text = await UT.GetLang("hsmlivelog");
        hsmlivelog_desc.Text = await UT.GetLang("deschsmlivelog");
    }

    public async Task CheckBTN(bool check, string step)
    {
        if (check)
        {
            await UT.Check(step);
        }
        hsmpanel.IsEnabled = false;
        hsmlivelog.IsEnabled = false;
        hsqm_delete_txt.Foreground = new SolidColorBrush(enabled);
        hsqm_disable_txt.Foreground = new SolidColorBrush(enabled);
        hsqm_enable_txt.Foreground = new SolidColorBrush(enabled);
        hsqm_start_txt.Foreground = new SolidColorBrush(enabled);
        hsqm_stop_txt.Foreground = new SolidColorBrush(enabled);
        hsqm_delete.IsEnabled = true;
        hsqm_enable.IsEnabled = true;
        hsqm_disable.IsEnabled = true;
        hsqm_start.IsEnabled = true;
        hsqm_stop.IsEnabled = true;
        if (UTdata.HSMExist == true)
        {
            hsqm_delete.IsEnabled = true;
            hsqm_delete_txt.Foreground = new SolidColorBrush(enabled);

            if (UTdata.HSMExeExist == true)
            {
                if (UTdata.HSMEnabled == true)
                {
                    hsqm_disable.IsEnabled = true;
                    hsqm_disable_txt.Foreground = new SolidColorBrush(enabled);
                    hsqm_enable.IsEnabled = false;
                    hsqm_enable_txt.Foreground = new SolidColorBrush(disabled);

                    if (UTdata.HSMRunning == true)
                    {
                        hsqm_stop.IsEnabled = true;
                        hsqm_stop_txt.Foreground = new SolidColorBrush(enabled);
                        hsqm_start.IsEnabled = false;
                        hsqm_start_txt.Foreground = new SolidColorBrush(disabled);
                        hsmpanel.IsEnabled = true;
                        hsmlivelog.IsEnabled = true;
                    }
                    else
                    {
                        hsqm_start.IsEnabled = true;
                        hsqm_start_txt.Foreground = new SolidColorBrush(enabled);
                        hsqm_stop.IsEnabled = false;
                        hsqm_stop_txt.Foreground = new SolidColorBrush(disabled);
                    }
                }
                else
                {
                    hsqm_disable.IsEnabled = false;
                    hsqm_disable_txt.Foreground = new SolidColorBrush(disabled);
                    hsqm_enable.IsEnabled = true;
                    hsqm_enable_txt.Foreground = new SolidColorBrush(enabled);
                    hsqm_start.IsEnabled = false;
                    hsqm_start_txt.Foreground = new SolidColorBrush(disabled);
                    hsqm_stop.IsEnabled = false;
                    hsqm_stop_txt.Foreground = new SolidColorBrush(disabled);
                }
            }
            else
            {
                hsqm_disable.IsEnabled = false;
                hsqm_disable_txt.Foreground = new SolidColorBrush(disabled);
                hsqm_enable.IsEnabled = false;
                hsqm_enable_txt.Foreground = new SolidColorBrush(disabled);
                hsqm_start.IsEnabled = false;
                hsqm_start_txt.Foreground = new SolidColorBrush(disabled);
                hsqm_stop.IsEnabled = false;
                hsqm_stop_txt.Foreground = new SolidColorBrush(disabled);
            }
        }
        else
        {
            hsqm_delete.IsEnabled = false;
            hsqm_delete_txt.Foreground = new SolidColorBrush(disabled);
            hsqm_enable.IsEnabled = false;
            hsqm_enable_txt.Foreground = new SolidColorBrush(disabled);
            hsqm_disable.IsEnabled = false;
            hsqm_disable_txt.Foreground = new SolidColorBrush(disabled);
            hsqm_start.IsEnabled = false;
            hsqm_start_txt.Foreground = new SolidColorBrush(disabled);
            hsqm_stop.IsEnabled = false;
            hsqm_stop_txt.Foreground = new SolidColorBrush(disabled);
        }
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN(false, "none");
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await CheckBTN(false, "none");

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

    public HisqoolManager(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public async void start_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("starthis"), "start.png"))
        {
            UT.SendAction("StartHSM");
            await UT.waitstatus.open(await UT.GetLang("wait.start"), "start.png");
            await UT.serv.start("HiSqoolManager");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "hsqm");
            await UT.waitstatus.close();
            if (!hsqm_start.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void stop_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("stophis"), "stop.png"))
        {
            UT.SendAction("StopHSM");
            await UT.waitstatus.open(await UT.GetLang("wait.stop"), "stop.png");
            await UT.serv.stop("HiSqoolManager");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "hsqm");
            await UT.waitstatus.close();
            if (!hsqm_stop.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void enable_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("enhis"), "enable.png"))
        {
            UT.SendAction("EnableHSM");
            await UT.waitstatus.open(await UT.GetLang("wait.enable"), "enable.png");
            await UT.serv.auto("HiSqoolManager");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "hsqm");
            await UT.waitstatus.close();
            if (!hsqm_enable.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void disable_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("dishis"), "disable.png"))
        {
            UT.SendAction("DisableHSM");
            await UT.waitstatus.open(await UT.GetLang("wait.disable"), "disable.png");
            await UT.serv.dis("HiSqoolManager");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "hsqm");
            await UT.waitstatus.close();
            if (!hsqm_disable.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void del_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("delhismserv"), "delete.png"))
        {
            UT.SendAction("DeleteHSMServ");
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "delete.png");
            await UT.serv.del("HiSqoolManager");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true, "hsqm");
            await UT.waitstatus.close();
            if (!hsqm_stop.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void hsmpanel_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        UT.SendAction("HSMPanel");
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "http://localhost:7654",
            UseShellExecute = true
        });
    }

    public async void hsmlivelog_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        UT.SendAction("HSMLiveLog");
        string logspath = "C:\\Program Files\\Unowhy\\HiSqool Manager\\logs\\logs";
        string args = "\"type '" + logspath + "' -wait\"";
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "powershell",
            Arguments = args,
            UseShellExecute = true
        });
    }
}
