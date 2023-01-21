using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using static Unowhy_Tools_WPF.UTclass;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Repair : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        shell_txt.Text = getlang("shell");
        shell_desc.Text = getlang("descshell");
        shell_btn.Text = getlang("change");
        tis_txt.Text = getlang("fixboot");
        tis_desc.Text = getlang("descstart");
        tis_btn.Text = getlang("del");
        wre_txt.Text = getlang("winre");
        wre_desc.Text = getlang("descwinre");
        wre_btn.Text = getlang("open");
        bim_txt.Text = getlang("bootim");
        bim_desc.Text = getlang("descbootim");
        bim_btn.Text = getlang("repair");
        whe_txt.Text = getlang("enwhe");
        whe_desc.Text = getlang("descenwhe");
        whe_btn.Text = getlang("enable");
        iaf_txt.Text = getlang("bcdfail");
        iaf_desc.Text = getlang("descbcdfail");
        iaf_btn.Text = getlang("del");
    }

    public Repair(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
