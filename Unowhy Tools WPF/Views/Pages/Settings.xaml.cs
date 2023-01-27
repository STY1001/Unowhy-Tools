using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows.Forms;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Settings : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        labol.Text = UT.getlang("logsopen");
        labdl.Text = UT.getlang("logsdel");
        dl.Content = UT.getlang("delete");
        ol.Content = UT.getlang("open");
        lablang.Text = UT.getlang("lang");
    }

    public Settings(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }
}
