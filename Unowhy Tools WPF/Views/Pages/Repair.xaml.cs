




using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

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

    public Repair(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
