using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Customize : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        pcname_txt.Text = UT.getlang("pcname");
        pcname_desc.Text = UT.getlang("descpcname");
        pcname_btn.Text = UT.getlang("change");
        adminset_txt.Text = UT.getlang("admin");
        adminset_desc.Text = UT.getlang("descadmin");
        adminset_btn.Text = UT.getlang("set");
        adduser_txt.Text = UT.getlang("adduser");
        adduser_desc.Text = UT.getlang("descadduser");
        adduser_btn.Text = UT.getlang("create");
        auset_txt.Text = UT.getlang("adminset");
        auset_desc.Text = UT.getlang("descadminset");
        auset_btn.Text = UT.getlang("open");
    }

    public void CheckBTN()
    {
        UT.check();
        if(UTdata.Admin == true) adminset.IsEnabled = false;
        else adminset.IsEnabled = true;
    }

    public Customize(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
        CheckBTN();
    }
}
