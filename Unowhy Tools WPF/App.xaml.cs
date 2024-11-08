﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
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
            services.AddScoped<Views.Pages.Bios>();
            services.AddScoped<Views.Pages.DrvCloud>();
            services.AddScoped<Views.Pages.HackBGRT>();
            services.AddScoped<Views.Pages.Extra>();
            services.AddScoped<Views.Pages.Compatibility>();
            services.AddScoped<TrayWindow>();
            services.AddScoped<UT>();

            /*
            UT.Data UTdata = new UT.Data();
            if(UTdata.RunTray == true)
            {
                services.AddHostedService<ApplicationHostService>();
                services.AddSingleton<IThemeService, ThemeService>();
                services.AddSingleton<ITaskBarService, TaskBarService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IDialogService, DialogService>();
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<ITestWindowService, TestWindowService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddScoped<DashboardViewModel>();
                services.AddScoped<TrayWindow>();
                services.AddScoped<UT>();
            }
            else
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
            }
            */
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
        AppDomain.CurrentDomain.UnhandledException += HandleCrash;
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

        bool showUpdater = false;
        string rescueUpdate = null;
        bool showConsole = false;
        bool useTray = false;
        string userId = null;
        bool isHelp = false;

        UT.Write2Log("\n\n\n\n\n\nUnowhy Tools launched\n");

        if (e.Args.Length > 0)
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
                else if (e.Args[i] == "-rescueupdate")
                {
                    if (i < e.Args.Length - 1)
                    {
                        rescueUpdate = e.Args[i + 1];
                    }
                    i++;
                }
                else if (e.Args[i] == "-tray")
                {
                    useTray = true;
                }
                else if (e.Args[i] == "-console")
                {
                    showConsole = true;
                }
                else if (e.Args[i] == "-updater")
                {
                    showUpdater = true;
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

        if (useTray)
        {
            UTdata.RunTray = true;
        }

        if (showUpdater)
        {
            UTdata.RunUpdater = true;
        }

        if (rescueUpdate != null)
        {
            string UTsver = UT.version.getverfull().ToString().Insert(2, ".") + " (Build " + UT.version.getverbuild().ToString() + ") ";
            if (UT.version.isdeb()) UTsver = UTsver + "(Debug)";
            else UTsver = UTsver + "(Release)";
            AllocConsole();

            Console.WriteLine($"Unowhy Tools by STY1001\nVersion {UTsver}\n\nRescue update mode\nUpdating UT...\n");

            if (await UT.CheckInternet())
            {
                if (rescueUpdate.Contains("release"))
                {
                    UT.SendAction("RescueUpdate.Release");
                    Console.WriteLine("Release version");
                    string utemp = UT.utpath + "\\Unowhy Tools\\Temps";
                    await System.Threading.Tasks.Task.Delay(1000);
                    var progress = new System.Progress<double>();
                    var cancellationToken = new CancellationTokenSource();
                    progress.ProgressChanged += (sender, value) =>
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.WindowWidth - 1));
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write($"Downloading... (1/2 {value.ToString("##0")}%)");
                    };
                    await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("utupdatezip"), utemp + "\\update.zip", progress, cancellationToken.Token);
                    Console.WriteLine("Done");
                    progress = new System.Progress<double>();
                    cancellationToken = new CancellationTokenSource();
                    progress.ProgressChanged += (sender, value) =>
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.WindowWidth - 1));
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write($"Downloading... (2/2 {value.ToString("##0")}%)");
                    };
                    await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("utszip"), utemp + "\\service.zip", progress, cancellationToken.Token);
                    Console.WriteLine("Done");
                    Console.WriteLine("Extracting...");
                    await System.Threading.Tasks.Task.Delay(1000);
                    ZipFile.ExtractToDirectory(utemp + "\\update.zip", utemp + "\\Update", true);
                    Directory.CreateDirectory(utemp + "\\Update\\Unowhy Tools Service");
                    ZipFile.ExtractToDirectory(utemp + "\\service.zip", utemp + "\\Update\\Unowhy Tools Service", true);
                    Console.WriteLine("Updating...");
                    await System.Threading.Tasks.Task.Delay(1000);
                    string pre = utemp + "\\update";
                    string post = Directory.GetCurrentDirectory();
                    bool trayenabled = false;
                    TaskService ts = new TaskService();
                    Microsoft.Win32.TaskScheduler.Task uttltask = ts.GetTask("Unowhy Tools Tray Launch");
                    if (uttltask != null)
                    {
                        if (uttltask.Definition.Settings.Enabled)
                        {
                            trayenabled = true;
                        }
                        else
                        {
                            trayenabled = false;
                        }
                    }
                    else
                    {
                        trayenabled = false;
                    }
                    string args = $"-user {UTdata.UserID} ";
                    if (UTdata.RunConsole)
                    {
                        args = args + "-console ";
                    }
                    if (UTdata.RunUpdater && trayenabled)
                    {
                        args = args + "-tray ";
                    }
                    Process.Start("cmd.exe", $"/c echo Unowhy Tools by STY1001 & echo Please wait, Unowhy Tools is updating... & echo Killing Unowhy Tools... & taskkill /f /im \"Unowhy Tools.exe\" & echo Stopping Unowhy Tools Service... & net stop UTS & timeout -t 3 & echo Copying files... & del /s /q \"{post}\\*\" & xcopy \"{pre}\" \"{post}\" /e /h /c /i /y & cls & echo Update done... & echo Starting Unowhy Tools Service... & net start UTS & echo Restarting Unowhy Tools... & powershell -windows hidden -command \"\" & \"Unowhy Tools.exe\" {args}");
                    Console.ReadKey();
                }
                else if (rescueUpdate.Contains("debug"))
                {
                    UT.SendAction("RescueUpdate.Debug");
                    Console.WriteLine("Debug version");
                    string utemp = UT.utpath + "\\Unowhy Tools\\Temps";
                    await System.Threading.Tasks.Task.Delay(1000);
                    var progress = new System.Progress<double>();
                    var cancellationToken = new CancellationTokenSource();
                    progress.ProgressChanged += (sender, value) =>
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.WindowWidth - 1));
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write($"Downloading... (1/2 {value.ToString("##0")}%)");
                    };
                    await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("utdebupdatezip"), utemp + "\\update.zip", progress, cancellationToken.Token);
                    Console.WriteLine("Done");
                    progress = new System.Progress<double>();
                    cancellationToken = new CancellationTokenSource();
                    progress.ProgressChanged += (sender, value) =>
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.WindowWidth - 1));
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write($"Downloading... (2/2 {value.ToString("##0")}%)");
                    };
                    await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("utszip"), utemp + "\\service.zip", progress, cancellationToken.Token);
                    Console.WriteLine("Done");
                    Console.WriteLine("Extracting...");
                    await System.Threading.Tasks.Task.Delay(1000);
                    ZipFile.ExtractToDirectory(utemp + "\\update.zip", utemp + "\\Update", true);
                    Directory.CreateDirectory(utemp + "\\Update\\Unowhy Tools Service");
                    ZipFile.ExtractToDirectory(utemp + "\\service.zip", utemp + "\\Update\\Unowhy Tools Service", true);
                    Console.WriteLine("Updating...");
                    await System.Threading.Tasks.Task.Delay(1000);
                    string pre = utemp + "\\update";
                    string post = Directory.GetCurrentDirectory();
                    bool trayenabled = false;
                    TaskService ts = new TaskService();
                    Microsoft.Win32.TaskScheduler.Task uttltask = ts.GetTask("Unowhy Tools Tray Launch");
                    if (uttltask != null)
                    {
                        if (uttltask.Definition.Settings.Enabled)
                        {
                            trayenabled = true;
                        }
                        else
                        {
                            trayenabled = false;
                        }
                    }
                    else
                    {
                        trayenabled = false;
                    }
                    string args = $"-user {UTdata.UserID} ";
                    if (UTdata.RunConsole)
                    {
                        args = args + "-console ";
                    }
                    if (UTdata.RunUpdater && trayenabled)
                    {
                        args = args + "-tray ";
                    }
                    Process.Start("cmd.exe", $"/c echo Unowhy Tools by STY1001 & echo Please wait, Unowhy Tools is updating... & echo Killing Unowhy Tools... & taskkill /f /im \"Unowhy Tools.exe\" & echo Stopping Unowhy Tools Service... & net stop UTS & timeout -t 3 & echo Copying files... & del /s /q \"{post}\\*\" & xcopy \"{pre}\" \"{post}\" /e /h /c /i /y & cls & echo Update done... & echo Starting Unowhy Tools Service... & net start UTS & echo Restarting Unowhy Tools... & powershell -windows hidden -command \"\" & \"Unowhy Tools.exe\" {args}");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Error, select a valid version (debug or release)");
                    Console.ReadKey();
                    System.Windows.Application.Current.Shutdown();
                }
            }
        }

        if (showConsole)
        {
            UT.SendAction("OpenConsole");
            string UTsver = UT.version.getverfull().ToString().Insert(2, ".") + " (Build " + UT.version.getverbuild().ToString() + ") ";
            if (UT.version.isdeb()) UTsver = UTsver + "(Debug)";
            else UTsver = UTsver + "(Release)";
            AllocConsole();

            Console.WriteLine($"Unowhy Tools by STY1001\nVersion {UTsver}\n\nConsole mode\nStarting UT...\n");

            UTdata.RunConsole = true;
        }

        if (isHelp)
        {
            UT.SendAction("LaunchHelp");
            string UTsver = UT.version.getverfull().ToString().Insert(2, ".") + " (Build " + UT.version.getverbuild().ToString() + ") ";
            if (UT.version.isdeb()) UTsver = UTsver + "(Debug)";
            else UTsver = UTsver + "(Release)";
            AllocConsole();

            Console.WriteLine($"Unowhy Tools by STY1001\nVersion {UTsver}\n\n\nArgs for Unowhy Tools:\n");
            Console.WriteLine("-user [string]                           Set a custom UserID");
            Console.WriteLine("-tray                                    Launch UT in tray mode, can only open 1 tray simultaneously");
            Console.WriteLine("-updater                                 Launch UT in updater");
            Console.WriteLine("-rescueupdate [debug|release]            Update UT without launching it, if you cannot update it normally");
            Console.WriteLine("-console                                 Launch UT with console pre opened");
            Console.WriteLine("-help | -?                               Display help");
            Console.ReadKey();
            System.Windows.Application.Current.Shutdown();
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
        UT.Data UTdata = new UT.Data();

        await _host.StopAsync();
        _host.Dispose();

        if (UTdata.RunConsole)
        {
            Console.ReadKey();
            System.Windows.Application.Current.Shutdown();
        }
    }

    private async void HandleCrash(object sender, UnhandledExceptionEventArgs e)
    {
        /*
        UT.Data UTdata = new UT.Data();
        Exception exp = (Exception)e.ExceptionObject;
        UT.Write2Log("=== Unowhy Tools Crash Log ===");
        UT.Write2Log($"Message: {exp.Message}");
        UT.Write2Log($"ToString: {exp.ToString()}");
        MessageBoxResult result = System.Windows.MessageBox.Show($"Unowhy Tools has crashed. Do you want to restart Unowhy Tools ?\n\n\nCrash Log:\n\nMessage:\n{exp.Message}\n\nToString:\n{exp.ToString()}", "Unowhy Tools crash handler", MessageBoxButton.YesNo, MessageBoxImage.Error);
        if (result == MessageBoxResult.Yes)
        {
            var exeName = Process.GetCurrentProcess().MainModule.FileName;
            ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            startInfo.Arguments = $"-user {UTdata.UserID}";
            Process.Start(startInfo);
        }
        await Task.Delay(1000);
        System.Windows.Application.Current.Shutdown();
        */
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private async void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0

        e.Handled = true;

        Guid uuid = Guid.NewGuid();
        string uuidCrash = uuid.ToString();
        UT.Data UTdata = new UT.Data();
        Exception exp = e.Exception;
        UT.Write2Log("\n\n\n=== Unowhy Tools Crash Report ===");
        UT.Write2Log($"Crash ID: {uuidCrash}");
        UT.Write2Log($"Message: {exp.Message}");
        UT.Write2Log($"ToString: {exp.ToString()}");
        UT.Write2Log("=== End of Unowhy Tools Crash Report ===\n\n\n");
        UT.Write2Log("Sending crash report...\n\n");
        await UT.SendCrashReport(uuidCrash, exp, await UT.OnlineDatas.GetUrls("api"));
        MessageBoxResult result = System.Windows.MessageBox.Show($"Unowhy Tools has crashed.\n\n\nReason:\n{exp.Message}\n\n\n - Yes: Restart Unowhy Tools\n\n - No: Close Unowhy Tools\n\n - Cancel: Try to continue (can be unstable)", "Unowhy Tools crash handler", MessageBoxButton.YesNoCancel, MessageBoxImage.Error);
        if (result == MessageBoxResult.Yes)
        {
            var exeName = Process.GetCurrentProcess().MainModule.FileName;
            ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            startInfo.Arguments = $"-user {UTdata.UserID}";
            Process.Start(startInfo);
            Application.Current.Shutdown();
        }
        else if (result == MessageBoxResult.No)
        {
            Application.Current.Shutdown();
        }
        else if (result == MessageBoxResult.Cancel)
        {

        }
    }
}
