using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Diagnostics;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;

using Unowhy_Tools;
using System.Windows;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class DebugPage : INavigableView<DashboardViewModel>
{
    private readonly ISnackbarService _snackbarService;

    public DashboardViewModel ViewModel
    {
        get;
    }

    public DebugPage(DashboardViewModel viewModel, ISnackbarService snackbarService)
    {
        ViewModel = viewModel;
        InitializeComponent();

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

    public void al_click(object sender, System.Windows.RoutedEventArgs e)
    {
        UT.applylang_global();
        instprog_txt.Text = "Hello";
    }

    public void DialoQ_Test(object sender, System.Windows.RoutedEventArgs e)
    {
        if (UT.DialogQShow(Window.GetWindow(this) , "This is a dialog") == true)
        {
            dqtest.Content = "YES";
        }
        else
        {
            dqtest.Content = "NO";
        }
    }
}
