using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using static Unowhy_Tools_WPF.UTclass;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class HisqoolManager : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        hsqm_txt.Text = getlang("labhsmq");
        hsqm_desc.Text = getlang("deschism");
        hsqm_start_txt.Text = getlang("start");
        hsqm_stop_txt.Text = getlang("stop");
        hsqm_enable_txt.Text = getlang("enable");
        hsqm_disable_txt.Text = getlang("disable");
        hsqm_delete_txt.Text = getlang("delserv");
    }

    public HisqoolManager(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
