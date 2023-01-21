using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using static Unowhy_Tools_WPF.UTclass;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class AdminUser : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        snpw.Text = getlang("snpw");
        en.Text = getlang("enablea");
        dis.Text = getlang("disablea");
    }

    public AdminUser(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
