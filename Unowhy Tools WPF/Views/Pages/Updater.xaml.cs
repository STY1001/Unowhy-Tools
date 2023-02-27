using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Windows;

using Unowhy_Tools;
using System.Diagnostics;
using Microsoft.Web.WebView2.Core;
using System.Windows.Controls;
using System.Net.Http;
using System.IO;
using System.IO.Compression;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Updater : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public Updater(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        browser.Source = new System.Uri("https://bit.ly/UTclogHTMLPrev"); 
        labimg.Visibility = Visibility.Hidden;
        labtext.Visibility = Visibility.Hidden;
    }

    public async void GithubButton_Click(object sender, RoutedEventArgs e)
    {
        browser.Source = new System.Uri("https://bit.ly/UTreleases"); 
    }
    
    public async void CheckButton_Click(object sender, RoutedEventArgs e)
    {
        labtext.Visibility = Visibility.Visible;
        labimg.Visibility = Visibility.Visible;
        labtext.Text = UT.GetLang("update.check");
        labimg.Source = UT.GetImgSource("wait.png");
        if (await UT.version.newver())
        {
            labimg.Source = UT.GetImgSource("yes.png");
            var web = new HttpClient();
            string newver = await web.GetStringAsync("https://bit.ly/UTnvTXT");
            newver = newver.Insert(2, ".");
            string newverfull = UT.GetLang("newver") + " (" + UT.version.getver() + " -> " + newver + ")";
            labtext.Text = newverfull;
            UpdateBTN.Click += InstallButton_Click;
            updatebtnimg.Source = UT.GetImgSource("download.png");
            updatebtntext.Text = UT.GetLang("unow");
        }
        else
        {
            labtext.Text = UT.GetLang("nover");
            labimg.Source = UT.GetImgSource("no.png");
        }
    }

    public async void InstallButton_Click(object sender, RoutedEventArgs e)
    {
        UpdateBTN.IsEnabled = false;
        labimg.Source = UT.GetImgSource("download.png");
        labtext.Text = UT.GetLang("update.dl");
        var web = new HttpClient();
        var filebyte = await web.GetByteArrayAsync("https://bit.ly/UTupdateZIP");
        string utemp = Path.GetTempPath() + "Unowhy Tools\\Temps";
        File.WriteAllBytes(utemp + "\\update.zip", filebyte);
        ZipFile.ExtractToDirectory(utemp + "\\update.zip", utemp + "\\Update");
        string pre = utemp + "\\update";
        string post = Directory.GetCurrentDirectory();

        Process.Start("cmd.exe", $"/c echo Updating Unowhy Tools... & taskkill /f /im \"Unowhy Tools.exe\" & timeout -t 3 & del /s /q \"{post}\\*\" & xcopy \"{pre}\" \"{post}\" /e /h /c /i /y & echo Done ! & \"Unowhy Tools.exe\"");

    }
}
