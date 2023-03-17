using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Threading.Tasks;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class AdminUser : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(Customize));
    }

    public void applylang()
    {
        snpw.Text = UT.GetLang("snpw");
        en.Text = UT.GetLang("enablea");
        dis.Text = UT.GetLang("disablea");
    }

    public AdminUser(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public async void Pass_Click(object sender, RoutedEventArgs e)
    {
        if(UT.DialogQShow(UT.GetLang("snpw"), "key.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("net", $"user {UTdata.AdminName} \"{passbox.Text}\"");
            await UT.waitstatus.close();
        }
    }
    
    public async void Ena_Click(object sender, RoutedEventArgs e)
    {
        if(UT.DialogQShow(UT.GetLang("enablea"), "enable.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("net", $"user {UTdata.AdminName} /active:yes");
            await UT.waitstatus.close();
        }
    }
    
    public async void Dis_Click(object sender, RoutedEventArgs e)
    {
        if(UT.DialogQShow(UT.GetLang("disablea"), "disable.png"))
        {
            await UT.waitstatus.open();
            await UT.RunMin("net", $"user {UTdata.AdminName} /active:no");
            await UT.waitstatus.close();
        }
    }
}
