using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using static Unowhy_Tools_WPF.UTclass;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class PCinfo : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        labpnc.Text = getlang("pcn");
        labuid.Text = getlang("domuser");
        labmf.Text = getlang("mfm");
        labsn.Text = getlang("serial");
        labbv.Text = getlang("biosversion");
        labwv.Text = getlang("os");
    }

    public PCinfo(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
