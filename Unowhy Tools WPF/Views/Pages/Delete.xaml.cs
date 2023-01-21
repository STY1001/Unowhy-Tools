using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using static Unowhy_Tools_WPF.UTclass;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Delete : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        instprog_txt.Text = getlang("unprog");
        instprog_desc.Text = "";
        instprog_btn.Text = getlang("open");
        entu_txt.Text = getlang("ent");
        entu_desc.Text = getlang("descent");
        entu_btn.Text = getlang("del");
        hsq_txt.Text = getlang("delhis");
        hsq_desc.Text = getlang("deschisdel");
        hsq_btn.Text = getlang("uninstall");
        aad_txt.Text = getlang("aadleave");
        aad_desc.Text = getlang("descaadleave");
        aad_btn.Text = getlang("disconnect");
        hsmqf_txt.Text = getlang("delhism");
        hsmqf_desc.Text = getlang("deschismdel");
        hsmqf_btn.Text = getlang("delete");
        tif_txt.Text = getlang("delti");
        tif_desc.Text = getlang("desctidel");
        tif_btn.Text = getlang("delete");
        ridff_txt.Text = getlang("delridf");
        ridff_desc.Text = getlang("descridf");
        ridff_btn.Text = getlang("delete");
        oemf_txt.Text = getlang("deloem");
        oemf_desc.Text = getlang("descdeloem");
        oemf_btn.Text = getlang("delete");
        entf_txt.Text = getlang("delentf");
        entf_desc.Text = getlang("descdelentf");
        entf_btn.Text = getlang("delete");
    }

    public Delete(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
