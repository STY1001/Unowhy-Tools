using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.Services.Contracts;
using Wpf.Ui.Mvvm.Contracts;

namespace Unowhy_Tools_WPF.ViewModels;

public class DashboardViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;

    private readonly ITestWindowService _testWindowService;

    private ICommand _navigateCommand;

    private ICommand _openWindowCommand;

    public ICommand NavigateCommand => _navigateCommand ??= new RelayCommand<string>(OnNavigate);

    public ICommand OpenWindowCommand => _openWindowCommand ??= new RelayCommand<string>(OnOpenWindow);

    public DashboardViewModel(INavigationService navigationService, ITestWindowService testWindowService)
    {
        _navigationService = navigationService;
        _testWindowService = testWindowService;
    }

    public void OnNavigatedTo()
    {
        System.Diagnostics.Debug.WriteLine($"INFO | {typeof(DashboardViewModel)} navigated", "Unowhy_Tools_WPF");
    }

    public void OnNavigatedFrom()
    {
        System.Diagnostics.Debug.WriteLine($"INFO | {typeof(DashboardViewModel)} navigated", "Unowhy_Tools_WPF");
    }

    private void OnNavigate(string parameter)
    {
        switch (parameter)
        {
            case "navigate_to_winre":
                _navigationService.Navigate(typeof(Views.Pages.WinRE));
                return;

            case "navigate_to_pcname":
                _navigationService.Navigate(typeof(Views.Pages.PCname));
                return;

            case "navigate_to_adduser":
                _navigationService.Navigate(typeof(Views.Pages.AddUser));
                return;
            
            case "navigate_to_adminuser":
                _navigationService.Navigate(typeof(Views.Pages.AdminUser));
                return;
            
            case "navigate_to_drvback":
                _navigationService.Navigate(typeof(Views.Pages.DrvBack));
                return;

            case "navigate_to_drvrest":
                _navigationService.Navigate(typeof(Views.Pages.DrvRest));
                return;

            case "navigate_to_drvconv":
                _navigationService.Navigate(typeof(Views.Pages.DrvConv));
                return;
            
            case "navigate_to_drivers":
                _navigationService.Navigate(typeof(Views.Pages.Drivers));
                return;
            
            case "navigate_to_customize":
                _navigationService.Navigate(typeof(Views.Pages.Customize));
                return;
            
            case "navigate_to_repair":
                _navigationService.Navigate(typeof(Views.Pages.Repair));
                return;
            
            case "navigate_to_home":
                _navigationService.Navigate(typeof(Views.Pages.Dashboard));
                return;
            
            case "navigate_to_pcinfo":
                _navigationService.Navigate(typeof(Views.Pages.PCinfo));
                return;
            
            case "navigate_to_about":
                _navigationService.Navigate(typeof(Views.Pages.About));
                return;
            
            case "navigate_to_settings":
                _navigationService.Navigate(typeof(Views.Pages.Settings));
                return;
        }
    }

    private void OnOpenWindow(string parameter)
    {
        switch (parameter)
        {
            /*case "open_window_store":
                _testWindowService.Show<Views.Windows.StoreWindow>();
                return;

            case "open_window_manager":
                _testWindowService.Show<Views.Windows.TaskManagerWindow>();
                return;

            case "open_window_editor":
                _testWindowService.Show<Views.Windows.EditorWindow>();
                return;

            case "open_window_settings":
                _testWindowService.Show<Views.Windows.SettingsWindow>();
                return;

            case "open_window_experimental":
                _testWindowService.Show<Views.Windows.ExperimentalWindow>();
                return;*/
        }
    }
}

