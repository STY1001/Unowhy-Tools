using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;

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
        hsqm_txt.Text = UT.getlang("labhsmq");
        hsqm_desc.Text = UT.getlang("deschism");
        hsqm_start_txt.Text = UT.getlang("start");
        hsqm_stop_txt.Text = UT.getlang("stop");
        hsqm_enable_txt.Text = UT.getlang("enable");
        hsqm_disable_txt.Text = UT.getlang("disable");
        hsqm_delete_txt.Text = UT.getlang("delserv");
    }

    public HisqoolManager(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
