using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unowhy_Tools;
using Unowhy_Tools_WPF.Models;
using Unowhy_Tools_WPF.Services;
using Unowhy_Tools_WPF.Services.Contracts;
using Unowhy_Tools_WPF.ViewModels;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

using Unowhy_Tools;
using System;

namespace Unowhy_Tools_WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static readonly IHost _host = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<ApplicationHostService>();
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<ITaskBarService, TaskBarService>();
            services.AddSingleton<ISnackbarService, SnackbarService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<INotifyIconService, CustomNotifyIconService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<ITestWindowService, TestWindowService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddScoped<INavigationWindow, Views.Container>();
            services.AddScoped<ContainerViewModel>();
            services.AddScoped<Views.Pages.Dashboard>();
            services.AddScoped<DashboardViewModel>();
            services.AddScoped<Views.Pages.About>();
            services.AddScoped<Views.Pages.HisqoolManager>();
            services.AddScoped<Views.Pages.Repair>();
            services.AddScoped<Views.Pages.Delete>();
            services.AddScoped<Views.Pages.Customize>();
            services.AddScoped<Views.Pages.Drivers>();
            services.AddScoped<Views.Pages.WinRE>();
            services.AddScoped<Views.Pages.PCname>();
            services.AddScoped<Views.Pages.AddUser>();
            services.AddScoped<Views.Pages.AdminUser>();
            services.AddScoped<Views.Pages.DrvBack>();
            services.AddScoped<Views.Pages.DrvRest>();
            services.AddScoped<Views.Pages.DrvConv>();
            services.AddScoped<Views.Pages.PCinfo>();
            services.AddScoped<Views.Pages.DebugPage>();
            services.AddScoped<Views.Pages.Settings>();
            services.AddScoped<Views.Pages.Updater>();
            services.AddScoped<Views.Pages.Wifi>();
            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
            services.AddScoped<UT>();
        }).Build();

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetService<T>()
        where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private async void OnStartup(object sender, StartupEventArgs e)
    {
        UT.Data UTdata = new UT.Data();

        if (e.Args.Length > 0)
        {
            if (e.Args[0] == "-u")
            {
                UTdata.UserID = e.Args[1];
            }
        }
        else
        {
            string user = await UT.RunReturn("whoami", "");
            UTdata.UserID = UT.GetLine(user, 1);
        }

        await _host.StartAsync();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}
