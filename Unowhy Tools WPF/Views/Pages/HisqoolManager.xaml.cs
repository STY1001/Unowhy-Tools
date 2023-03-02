using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Threading.Tasks;
using System;
using System.Windows.Media;

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

    public void applylang()
    {
        hsqm_txt.Text = UT.GetLang("labhsmq");
        hsqm_desc.Text = UT.GetLang("deschism");
        hsqm_start_txt.Text = UT.GetLang("start");
        hsqm_stop_txt.Text = UT.GetLang("stop");
        hsqm_enable_txt.Text = UT.GetLang("enable");
        hsqm_disable_txt.Text = UT.GetLang("disable");
        hsqm_delete_txt.Text = UT.GetLang("delserv");
    }

    public async Task CheckBTN()
    {
        await UT.Check();
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
        await UT.waitstatus.open();
        applylang();
        await CheckBTN();
        await UT.waitstatus.close();
    }

    public HisqoolManager(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public async void start_Click(object sender, RoutedEventArgs e)
    {
        if(UT.DialogQShow(UT.GetLang("starthis"), "start.png"))
        {
            await UT.waitstatus.open();
            await UT.serv.start("HiSqoolManager");
            await CheckBTN();
            await UT.waitstatus.close();
            if(!hsqm_start.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void stop_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("stophis"), "stop.png"))
        {
            await UT.waitstatus.open();
            await UT.serv.stop("HiSqoolManager");
            await CheckBTN();
            await UT.waitstatus.close();
            if (!hsqm_stop.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void enable_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("enhis"), "enable.png"))
        {
            await UT.waitstatus.open();
            await UT.serv.auto("HiSqoolManager");
            await CheckBTN();
            await UT.waitstatus.close();
            if (!hsqm_enable.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void disable_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("dishis"), "disable.png"))
        {
            await UT.waitstatus.open();
            await UT.serv.dis("HiSqoolManager");
            await CheckBTN();
            await UT.waitstatus.close();
            if (!hsqm_disable.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void del_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("delhismserv"), "delete.png"))
        {
            await UT.waitstatus.open();
            await UT.serv.del("HiSqoolManager");
            await CheckBTN();
            await UT.waitstatus.close();
            if (!hsqm_stop.IsEnabled)
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
