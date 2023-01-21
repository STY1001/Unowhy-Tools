using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using static Unowhy_Tools_WPF.UTclass;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Customize : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        pcname_txt.Text = getlang("pcname");
        pcname_desc.Text = getlang("descpcname");
        pcname_btn.Text = getlang("change");
        adminset_txt.Text = getlang("admin");
        adminset_desc.Text = getlang("descadmin");
        adminset_btn.Text = getlang("set");
        adduser_txt.Text = getlang("adduser");
        adduser_desc.Text = getlang("descadduser");
        adduser_btn.Text = getlang("create");
        auset_txt.Text = getlang("adminset");
        auset_desc.Text = getlang("descadminset");
        auset_btn.Text = getlang("open");
    }

    public Customize(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
