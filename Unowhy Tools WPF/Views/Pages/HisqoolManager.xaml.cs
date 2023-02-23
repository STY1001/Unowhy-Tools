using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class HisqoolManager : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        hsqm_txt.Text = UT.getlang("labhsmq");
        hsqm_desc.Text = UT.getlang("deschism");
        hsqm_start_txt.Text = UT.getlang("start");
        hsqm_stop_txt.Text = UT.getlang("stop");
        hsqm_enable_txt.Text = UT.getlang("enable");
        hsqm_disable_txt.Text = UT.getlang("disable");
        hsqm_delete_txt.Text = UT.getlang("delserv");
    }

    public void CheckBTN()
    {
        UT.check();
        hsqm_delete.IsEnabled = true;
        hsqm_enable.IsEnabled = true;
        hsqm_disable.IsEnabled = true;
        hsqm_start.IsEnabled = true;
        hsqm_stop.IsEnabled = true;
        if (UTdata.HSMExist == true)
        {
            hsqm_delete.IsEnabled = true;

            if (UTdata.HSMExeExist == true)
            {
                if (UTdata.HSMEnabled == true)
                {
                    hsqm_disable.IsEnabled = true;
                    hsqm_enable.IsEnabled = false;

                    if (UTdata.HSMRunning == true)
                    {
                        hsqm_stop.IsEnabled = true;
                        hsqm_start.IsEnabled = false;
                    }
                    else
                    {
                        hsqm_start.IsEnabled = true;
                        hsqm_stop.IsEnabled = false;
                    }
                }
                else
                {
                    hsqm_disable.IsEnabled = false;
                    hsqm_enable.IsEnabled = true;
                    hsqm_start.IsEnabled = false;
                    hsqm_stop.IsEnabled = false;
                }
            }
            else
            {
                hsqm_disable.IsEnabled = false;
                hsqm_enable.IsEnabled = false;
                hsqm_start.IsEnabled = false;
                hsqm_stop.IsEnabled = false;
            }
        }
        else
        {
            hsqm_delete.IsEnabled = false;
            hsqm_enable.IsEnabled = false;
            hsqm_disable.IsEnabled = false;
            hsqm_start.IsEnabled = false;
            hsqm_stop.IsEnabled = false;
        }
    }

    public HisqoolManager(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();

        CheckBTN();
    }

    public void start_Click(object sender, RoutedEventArgs e)
    {
        if(UT.DialogQShow(UT.getlang("starthis"), "start.png"))
        {
            UT.waitstatus.open();
            UT.serv.start("HiSqoolManager");
            CheckBTN();
            UT.waitstatus.close();
            if(!hsqm_start.IsEnabled)
            {
                UT.DialogIShow(UT.getlang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.getlang("failed"), "no.png");
            }
        }
    }

    public void stop_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.getlang("stophis"), "stop.png"))
        {
            UT.waitstatus.open();
            UT.serv.stop("HiSqoolManager");
            CheckBTN();
            UT.waitstatus.close();
            if (!hsqm_stop.IsEnabled)
            {
                UT.DialogIShow(UT.getlang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.getlang("failed"), "no.png");
            }
        }
    }

    public void enable_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.getlang("enhis"), "enable.png"))
        {
            UT.waitstatus.open();
            UT.serv.auto("HiSqoolManager");
            CheckBTN();
            UT.waitstatus.close();
            if (!hsqm_enable.IsEnabled)
            {
                UT.DialogIShow(UT.getlang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.getlang("failed"), "no.png");
            }
        }
    }

    public void disable_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.getlang("dishis"), "disable.png"))
        {
            UT.waitstatus.open();
            UT.serv.dis("HiSqoolManager");
            CheckBTN();
            UT.waitstatus.close();
            if (!hsqm_disable.IsEnabled)
            {
                UT.DialogIShow(UT.getlang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.getlang("failed"), "no.png");
            }
        }
    }

    public void del_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.getlang("delhismserv"), "delete.png"))
        {
            UT.waitstatus.open();
            UT.serv.del("HiSqoolManager");
            CheckBTN();
            UT.waitstatus.close();
            if (!hsqm_stop.IsEnabled)
            {
                UT.DialogIShow(UT.getlang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.getlang("failed"), "no.png");
            }
        }
    }
}
