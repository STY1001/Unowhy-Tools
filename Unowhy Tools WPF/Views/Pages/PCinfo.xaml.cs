using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;

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
        labpnc.Text = UT.getlang("pcn");
        labuid.Text = UT.getlang("domuser");
        labmf.Text = UT.getlang("mfm");
        labsn.Text = UT.getlang("serial");
        labbv.Text = UT.getlang("biosversion");
        labwv.Text = UT.getlang("os");
    }

    public PCinfo(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
