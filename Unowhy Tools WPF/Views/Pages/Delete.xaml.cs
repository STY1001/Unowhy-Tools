using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Threading.Tasks;
using System;

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
        instprog_txt.Text = UT.GetLang("unprog");
        instprog_desc.Text = "";
        instprog_btn.Text = UT.GetLang("open");
        entu_txt.Text = UT.GetLang("ent");
        entu_desc.Text = UT.GetLang("descent");
        entu_btn.Text = UT.GetLang("del");
        hsq_txt.Text = UT.GetLang("delhis");
        hsq_desc.Text = UT.GetLang("deschisdel");
        hsq_btn.Text = UT.GetLang("uninstall");
        aad_txt.Text = UT.GetLang("aadleave");
        aad_desc.Text = UT.GetLang("descaadleave");
        aad_btn.Text = UT.GetLang("disconnect");
        hsmqf_txt.Text = UT.GetLang("delhism");
        hsmqf_desc.Text = UT.GetLang("deschismdel");
        hsmqf_btn.Text = UT.GetLang("delete");
        tif_txt.Text = UT.GetLang("delti");
        tif_desc.Text = UT.GetLang("desctidel");
        tif_btn.Text = UT.GetLang("delete");
        ridff_txt.Text = UT.GetLang("delridf");
        ridff_desc.Text = UT.GetLang("descridf");
        ridff_btn.Text = UT.GetLang("delete");
        oemf_txt.Text = UT.GetLang("deloem");
        oemf_desc.Text = UT.GetLang("descdeloem");
        oemf_btn.Text = UT.GetLang("delete");
        entf_txt.Text = UT.GetLang("delentf");
        entf_desc.Text = UT.GetLang("descdelentf");
        entf_btn.Text = UT.GetLang("delete");
    }

    public async Task CheckBTN()
    {
        await UT.Check();
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

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN();
    }

    public Delete(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public void _Click(object sender, RoutedEventArgs e)
    {

    }
}
