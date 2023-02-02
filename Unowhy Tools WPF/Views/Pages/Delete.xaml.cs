using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Delete : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        instprog_txt.Text = UT.getlang("unprog");
        instprog_desc.Text = "";
        instprog_btn.Text = UT.getlang("open");
        entu_txt.Text = UT.getlang("ent");
        entu_desc.Text = UT.getlang("descent");
        entu_btn.Text = UT.getlang("del");
        hsq_txt.Text = UT.getlang("delhis");
        hsq_desc.Text = UT.getlang("deschisdel");
        hsq_btn.Text = UT.getlang("uninstall");
        aad_txt.Text = UT.getlang("aadleave");
        aad_desc.Text = UT.getlang("descaadleave");
        aad_btn.Text = UT.getlang("disconnect");
        hsmqf_txt.Text = UT.getlang("delhism");
        hsmqf_desc.Text = UT.getlang("deschismdel");
        hsmqf_btn.Text = UT.getlang("delete");
        tif_txt.Text = UT.getlang("delti");
        tif_desc.Text = UT.getlang("desctidel");
        tif_btn.Text = UT.getlang("delete");
        ridff_txt.Text = UT.getlang("delridf");
        ridff_desc.Text = UT.getlang("descridf");
        ridff_btn.Text = UT.getlang("delete");
        oemf_txt.Text = UT.getlang("deloem");
        oemf_desc.Text = UT.getlang("descdeloem");
        oemf_btn.Text = UT.getlang("delete");
        entf_txt.Text = UT.getlang("delentf");
        entf_desc.Text = UT.getlang("descdelentf");
        entf_btn.Text = UT.getlang("delete");
    }

    public void CheckBTN()
    {
        UT.check();
        entu.IsEnabled = true;
        hsq.IsEnabled = true;
        aad.IsEnabled = true;
        hsmqf.IsEnabled = true;
        tif.IsEnabled = true;
        ridff.IsEnabled = true;
        oemf.IsEnabled = true;
        entf.IsEnabled = true;
        if (UTdata.ENTUser == true) entu.IsEnabled = true;
        else entu.IsEnabled = false;
        if (UTdata.HSQFolderExist == true) hsq.IsEnabled = true;
        else hsq.IsEnabled = false;
        if (UTdata.AAD == true) aad.IsEnabled = true;
        else aad.IsEnabled = false;
        if (UTdata.HSQMFolderExist == true) hsmqf.IsEnabled = true;
        else hsmqf.IsEnabled = false;
        if (UTdata.TIFolderExist == true) tif.IsEnabled = true;
        else tif.IsEnabled = false;
        if (UTdata.RIDFFolderExist == true) ridff.IsEnabled = true;
        else ridff.IsEnabled = false;
        if (UTdata.OEMFolderExist == true) oemf.IsEnabled = true;
        else oemf.IsEnabled = false;
        if (UTdata.ENTFolderExist == true) entf.IsEnabled = true;
        else entf.IsEnabled = false;
    }

    public Delete(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
        CheckBTN();
    }
}
