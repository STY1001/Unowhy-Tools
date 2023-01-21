using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using static Unowhy_Tools_WPF.UTclass;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Drivers : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        dlcloud_txt.Text = getlang("bkcloud");
        dlcloud_desc.Text = getlang("descbkcloud");
        dlcloud_btn.Text = getlang("dl");
        bk_txt.Text = getlang("back");
        bk_desc.Text = getlang("drvbr");
        bk_btn.Text = getlang("backup");
        rt_txt.Text = getlang("rest");
        rt_desc.Text = getlang("drvbr");
        rt_btn.Text = getlang("restore");
    }

    public Drivers(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
