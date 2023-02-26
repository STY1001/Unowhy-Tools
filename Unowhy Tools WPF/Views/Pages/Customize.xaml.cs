using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Threading.Tasks;
using System;
using System.Windows;

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
        pcname_txt.Text = UT.GetLang("pcname");
        pcname_desc.Text = UT.GetLang("descpcname");
        pcname_btn.Text = UT.GetLang("change");
        adminset_txt.Text = UT.GetLang("admin");
        adminset_desc.Text = UT.GetLang("descadmin");
        adminset_btn.Text = UT.GetLang("set");
        adduser_txt.Text = UT.GetLang("adduser");
        adduser_desc.Text = UT.GetLang("descadduser");
        adduser_btn.Text = UT.GetLang("create");
        auset_txt.Text = UT.GetLang("adminset");
        auset_desc.Text = UT.GetLang("descadminset");
        auset_btn.Text = UT.GetLang("open");
    }

    public async Task CheckBTN()
    {
        await UT.Check();
        if (UTdata.Admin == true) adminset.IsEnabled = false;
        else adminset.IsEnabled = true;
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN();
    }

    public Customize(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public async void adminset_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(UT.GetLang("admin"), "admin.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("net", $"localgroup Administrateurs /add \"{UTdata.User}\"");
            await CheckBTN();
            await UT.waitstatus.close();
            if (!adminset.IsEnabled)
            {
                UT.DialogIShow(UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("failed"), "no.png");
            }
        }
    }
}
