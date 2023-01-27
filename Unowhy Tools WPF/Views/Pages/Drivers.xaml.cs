using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;

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
        dlcloud_txt.Text = UT.getlang("bkcloud");
        dlcloud_desc.Text = UT.getlang("descbkcloud");
        dlcloud_btn.Text = UT.getlang("dl");
        bk_txt.Text = UT.getlang("back");
        bk_desc.Text = UT.getlang("drvbr");
        bk_btn.Text = UT.getlang("backup");
        rt_txt.Text = UT.getlang("rest");
        rt_desc.Text = UT.getlang("drvbr");
        rt_btn.Text = UT.getlang("restore");
    }

    public Drivers(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
