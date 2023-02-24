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
        shell_txt.Text = UT.GetLang("shell");
        shell_desc.Text = UT.GetLang("descshell");
        shell_btn.Text = UT.GetLang("change");
        tis_txt.Text = UT.GetLang("fixboot");
        tis_desc.Text = UT.GetLang("descstart");
        tis_btn.Text = UT.GetLang("del");
        wre_txt.Text = UT.GetLang("winre");
        wre_desc.Text = UT.GetLang("descwinre");
        wre_btn.Text = UT.GetLang("open");
        bim_txt.Text = UT.GetLang("bootim");
        bim_desc.Text = UT.GetLang("descbootim");
        bim_btn.Text = UT.GetLang("repair");
        whe_txt.Text = UT.GetLang("enwhe");
        whe_desc.Text = UT.GetLang("descenwhe");
        whe_btn.Text = UT.GetLang("enable");
        iaf_txt.Text = UT.GetLang("bcdfail");
        iaf_desc.Text = UT.GetLang("descbcdfail");
        iaf_btn.Text = UT.GetLang("del");
    }

    public Repair(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
