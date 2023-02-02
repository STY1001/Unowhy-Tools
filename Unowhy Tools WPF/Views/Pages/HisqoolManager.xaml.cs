using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;

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
}
