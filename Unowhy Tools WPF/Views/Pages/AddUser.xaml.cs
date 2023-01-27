using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class AddUser : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        labu.Text = UT.getlang("name");
        labp.Text = UT.getlang("password");
        labc.Text = UT.getlang("confpw");
        laba.Content = UT.getlang("la");
        labb.Text = UT.getlang("create");
    }

    public AddUser(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
