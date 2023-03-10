using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Diagnostics;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;

using Unowhy_Tools;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.IO.Compression;
using System.IO;
using System.Net.Http;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class DebugPage : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    private readonly ISnackbarService _snackbarService;

    public DashboardViewModel ViewModel
    {
        get;
    }

    public DebugPage(DashboardViewModel viewModel, ISnackbarService snackbarService)
    {
        ViewModel = viewModel;
        InitializeComponent();

        verlab.Text = "Debug Page";

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

    public async void Discord_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        await UT.waitstatus.open();
        await Task.Delay(1000);
        await UT.waitstatus.close();
    }

    public async void Update_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        debus.Text = "DL...";
        var web = new HttpClient();
        var filebyte = await web.GetByteArrayAsync("https://bit.ly/UTdebupdateZIP");
        string utemp = Path.GetTempPath() + "Unowhy Tools\\Temps";
        File.WriteAllBytes(utemp + "\\update.zip", filebyte);
        debus.Text = "EX...";
        ZipFile.ExtractToDirectory(utemp + "\\update.zip", utemp + "\\Update");
        string pre = utemp + "\\update";
        string post = Directory.GetCurrentDirectory();

        Process.Start("cmd.exe", $"/c echo Updating Unowhy Tools... & taskkill /f /im \"Unowhy Tools.exe\" & timeout -t 3 & del /s /q \"{post}\\*\" & xcopy \"{pre}\" \"{post}\" /e /h /c /i /y & echo Done ! & powershell -windows hidden -command \"\" & \"Unowhy Tools.exe\" -u {UTdata.UserID}");

    }   

    public void al_click(object sender, System.Windows.RoutedEventArgs e)
    {
        UT.applylang_global();
        instprog_txt.Text = "Hello";
    }

    public void DialoQ_Test(object sender, System.Windows.RoutedEventArgs e)
    {
        if (UT.DialogQShow(dialogtxt.Text, "question.png") == true)
        {
            dqtest.Content = "YES";
        }
        else
        {
            dqtest.Content = "NO";
        }
    }

    public void DialogI_Test(object sender, System.Windows.RoutedEventArgs e)
    {
        UT.DialogIShow(dialogtxt.Text, "about.png");
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        UT.anim.TransitionBack(Grid1);
    }
}
