using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;

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
        shell_txt.Text = UT.getlang("shell");
        shell_desc.Text = UT.getlang("descshell");
        shell_btn.Text = UT.getlang("change");
        tis_txt.Text = UT.getlang("fixboot");
        tis_desc.Text = UT.getlang("descstart");
        tis_btn.Text = UT.getlang("del");
        wre_txt.Text = UT.getlang("winre");
        wre_desc.Text = UT.getlang("descwinre");
        wre_btn.Text = UT.getlang("open");
        bim_txt.Text = UT.getlang("bootim");
        bim_desc.Text = UT.getlang("descbootim");
        bim_btn.Text = UT.getlang("repair");
        whe_txt.Text = UT.getlang("enwhe");
        whe_desc.Text = UT.getlang("descenwhe");
        whe_btn.Text = UT.getlang("enable");
        iaf_txt.Text = UT.getlang("bcdfail");
        iaf_desc.Text = UT.getlang("descbcdfail");
        iaf_btn.Text = UT.getlang("del");
    }

    public Repair(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
