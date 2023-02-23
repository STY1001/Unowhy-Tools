using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Net;

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
        en_txt.Text = UT.getlang("enable");
        dis_txt.Text = UT.getlang("disable");
        rep_txt.Text = UT.getlang("repair");
    }

    public void CheckBTN()
    {
        string out1 = UT.returncmd("reagentc.exe", "/info");
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
                //var i = new info(getlang("winremsg"), Properties.Resources.no);
                //i.ShowDialog();
            }
            else
            {
                en.IsEnabled = true;
                rep.IsEnabled = false;
                dis.IsEnabled = false;
            }
        }
    }

    public void Enable_Click(object sender, RoutedEventArgs e)
    {
        UTdata.WinRE = true;
        UT.runmin("reagentc.exe", "/enable");
        CheckBTN();
    }

    public void Disable_Click(object sender, RoutedEventArgs e)
    {
        UTdata.WinRE = false;
        UT.runmin("reagentc.exe", "/disable");
        CheckBTN();
    }

    public void Repair_Click(object sender, RoutedEventArgs e)
    {
        if (UT.CheckInternet())
        {
            using (var web = new WebClient())
            {
                web.DownloadFile("https://dl.dropbox.com/s/lahofrvpejlclkx/Winre.wim", "C:\\Windows\\System32\\Recovery\\WinRE.wim");
            }

            UT.runmin("reagentc.exe", "/setreimage /path C:\\Windows\\System32\\Recovery");
            UT.runmin("reagentc.exe", "/enable");

            UTdata.WinRE = true;

            CheckBTN();
        }
    }

    public WinRE(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
        CheckBTN();
    }
}
