




using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Threading.Tasks;
using Unowhy_Tools_WPF.Views;
using System.Windows;
using System.Diagnostics;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;
using Unowhy_Tools;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class About : INavigableView<DashboardViewModel>
{
    private readonly ISnackbarService _snackbarService;

    public DashboardViewModel ViewModel
    {
        get;
    }

    public About(DashboardViewModel viewModel, ISnackbarService snackbarService)
    {
        ViewModel = viewModel;
        InitializeComponent();


        string ver = "Version " + UT.version.getver() + " (Build " + UT.version.getverbuild().ToString() + ") ";

        if (UT.version.isdeb()) ver = ver + "(Debug/Beta)";
        else ver = ver + "(Release)";

        verlab.Text = ver;


        _snackbarService = snackbarService;

    }

    public void Github_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "https://github.com/STY1001/Unowhy-Tools",
                        UseShellExecute = true
        });
    }

    public void STY1001_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "https://sty1001.cf",
            UseShellExecute = true
        });
    }

    public void Discord_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "https://discord.com/invite/dw3ZJ9u7WS",
            UseShellExecute = true
        });
    }

    public void Update_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _snackbarService.Show("Update checker", "Not done", SymbolRegular.ErrorCircle24, ControlAppearance.Danger);
    }
}
