using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

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
        labol.Text = UT.GetLang("logsopen");
        labdl.Text = UT.GetLang("logsdel");
        dl.Content = UT.GetLang("delete");
        ol.Content = UT.GetLang("open");
        lablang.Text = UT.GetLang("lang");
    }

    public async Task CheckBTN()
    {
        RegistryKey lcs = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
        string utlst = lcs.GetValue("Lang").ToString();
        if (utlst == "EN")
        {
            lang_en.IsSelected = true;
        }
        else
        {
            lang_fr.IsSelected = true;
        }

        string fp = Path.GetTempPath() + "\\UT_Logs.txt";
        FileInfo fi = new FileInfo(fp);
        string size;
        if (fi.Length > 1000000) size = (fi.Length / 1000000).ToString() + " MB";
        else size = (fi.Length / 1000).ToString() + " KB";

        dl.Content = UT.GetLang("clean") + " (" + size + ")";
    }

    public void Logs_Clear(object sender, RoutedEventArgs e)
    {
        string fp = Path.GetTempPath() + "\\UT_Logs.txt";
        File.Create(fp).Close();
        FileInfo fi = new FileInfo(fp);
        string size;
        if (fi.Length > 1000000) size = (fi.Length / 1000000).ToString() + " MB";
        else size = (fi.Length / 1000).ToString() + "KB";
        dl.Content = UT.GetLang("clean") + " (" + size + ")";
    }

    public void Logs_Open(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = Path.GetTempPath() + "\\UT_Logs.txt",
            UseShellExecute = true
        });
    }

    public async void Apply_Settings(object sender, RoutedEventArgs e)
    {
        await UT.waitstatus.open();
        if (lang_en.IsSelected)
        {
            await UT.RunMin("reg", "add \"HKCU\\Software\\STY1001\\Unowhy Tools\" /v Lang /d EN /t REG_SZ /f");
        }
        else if (lang_fr.IsSelected)
        {
            await UT.RunMin("reg", "add \"HKCU\\Software\\STY1001\\Unowhy Tools\" /v Lang /d FR /t REG_SZ /f");
        }

        System.Windows.Forms.Application.Restart();
        System.Windows.Application.Current.Shutdown();
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN();
    }

    public Settings(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
