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
        dlcloud_txt.Text = UT.GetLang("bkcloud");
        dlcloud_desc.Text = UT.GetLang("descbkcloud");
        dlcloud_btn.Text = UT.GetLang("dl");
        bk_txt.Text = UT.GetLang("back");
        bk_desc.Text = UT.GetLang("drvbr");
        bk_btn.Text = UT.GetLang("backup");
        rt_txt.Text = UT.GetLang("rest");
        rt_desc.Text = UT.GetLang("drvbr");
        rt_btn.Text = UT.GetLang("restore");
    }

    public Drivers(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
