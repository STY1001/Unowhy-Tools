using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class WinRE : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        en_txt.Text = UT.GetLang("enable");
        dis_txt.Text = UT.GetLang("disable");
        rep_txt.Text = UT.GetLang("repair");
    }

    public async Task CheckBTN()
    {
        string out1 = await UT.RunReturn("reagentc.exe", "/info");
        if (out1.Contains("Enable"))
        {
            en.IsEnabled = false;
            dis.IsEnabled = true;
            rep.IsEnabled = false;
        }
        else
        {
            if (UTdata.WinRE == true)
            {
                en.IsEnabled = false;
                rep.IsEnabled = true;
                dis.IsEnabled = false;
                UT.DialogIShow(UT.GetLang("winremsg"), "no.png");
            }
            else
            {
                en.IsEnabled = true;
                rep.IsEnabled = false;
                dis.IsEnabled = false;
            }
        }
    }

    public async void Enable_Click(object sender, RoutedEventArgs e)
    {
        UTdata.WinRE = true;
        await UT.RunMin("reagentc.exe", "/enable");
        await CheckBTN();
    }

    public async void Disable_Click(object sender, RoutedEventArgs e)
    {
        UTdata.WinRE = false;
        await UT.RunMin("reagentc.exe", "/disable");
        await CheckBTN();
    }

    public async void Repair_Click(object sender, RoutedEventArgs e)
    {
        if (UT.CheckInternet())
        {
            using (var web = new WebClient())
            {
                web.DownloadFileAsync(new System.Uri("https://dl.dropbox.com/s/lahofrvpejlclkx/Winre.wim"), "C:\\Windows\\System32\\Recovery\\WinRE.wim");
            }

            await UT.RunMin("reagentc.exe", "/setreimage /path C:\\Windows\\System32\\Recovery");
            await UT.RunMin("reagentc.exe", "/enable");

            UTdata.WinRE = true;

            await CheckBTN();
        }
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN();
    }

    public WinRE(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
