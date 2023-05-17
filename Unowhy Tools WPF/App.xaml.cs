using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unowhy_Tools;
using Unowhy_Tools_WPF.Services;
using Unowhy_Tools_WPF.Services.Contracts;
using Unowhy_Tools_WPF.ViewModels;
using Unowhy_Tools_WPF.Views;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace Unowhy_Tools_WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

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
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<ITestWindowService, TestWindowService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddScoped<INavigationWindow, MainWindow>();
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
            services.AddScoped<Views.Pages.FirstConfig>();
            services.AddScoped<TrayWindow>();
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
        /*
        if (e.Args.Length > 0)
        {
            if (e.Args[0] == "-user")
            {
                UTdata.UserID = e.Args[1];
            }
            else
            {
                string user = await UT.RunReturn("whoami", "");
                UTdata.UserID = UT.GetLine(user, 1);
            }

            if (e.Args[])
        }
        else
        {
            string user = await UT.RunReturn("whoami", "");
            UTdata.UserID = UT.GetLine(user, 1);
        }
        */

        bool useTray = false;
        string userId = null;
        bool isHelp = false;

        UT.Write2Log("\n\n\n\n\n\nUnowhy Tools launched\n");

        if(e.Args.Length > 0)
        { 
            string args = string.Join(" ", e.Args);
            UT.Write2Log("Unowhy Tools Args: " + args + "\n\n\n");
        }
        else
        {
            UT.Write2Log("Unowhy Tools Args: No arg" + "\n\n\n");
        }

        if (e.Args.Length > 0)
        {
            for (int i = 0; i < e.Args.Length; i++)
            {
                if (e.Args[i] == "-user")
                {
                    if (i < e.Args.Length - 1)
                    {
                        userId = e.Args[i + 1];
                    }
                    i++;
                }
                else if (e.Args[i] == "-tray")
                {
                    useTray = true;
                }
                else if (e.Args[i] == "-help" || e.Args[i] == "-?")
                {
                    isHelp = true;
                }
            }
        }

        if (userId == null)
        {
            string user = await UT.RunReturn("whoami", "");
            userId = UT.GetLine(user, 1);
        }

        UTdata.UserID = userId;

        if(useTray)
        {
            UTdata.RunTray = true;
        }

        if (isHelp)
        {
            string UTsver = UT.version.getverfull().ToString().Insert(2, ".") + " (Build " + UT.version.getverbuild().ToString() + ") ";

            if (UT.version.isdeb()) UTsver = UTsver + "(Debug)";
            else UTsver = UTsver + "(Release)";
            AllocConsole();
            Console.WriteLine($"Unowhy Tools by STY1001\nVersion {UTsver}\n\n\nArgs for Unowhy Tools:\n");
            Console.WriteLine("-user [string]           Set a custom UserID");
            Console.WriteLine("-tray                    Launch UT in tray mode, can only open 1 tray simultaneously");
            Console.WriteLine("-help | -?               Display help");
            Console.ReadKey();
            Application.Current.Shutdown();
        }
        else
        {
            await _host.StartAsync();
        }   
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
