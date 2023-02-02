using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows.Media.Imaging;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class PCinfo : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        labpnc.Text = UT.getlang("pcn");
        labuid.Text = UT.getlang("domuser");
        labmf.Text = UT.getlang("mfm");
        labsn.Text = UT.getlang("serial");
        labbv.Text = UT.getlang("biosversion");
        labwv.Text = UT.getlang("os");
    }

    public void infoapply()
    {
        uid.Text = UTdata.UserID;
        pcn.Text = UTdata.HostName;
        mf.Text = UT.getline(UTdata.mf, 1) + " " + UT.getline(UTdata.md, 1);
        sn.Text = UTdata.sn;
        bv.Text = UTdata.bios;
        wv.Text = UTdata.os;
        if (wv.Text.Contains("11"))
        {
            imgwv.Source = new BitmapImage(new System.Uri("pack://application:,,,/Resources/win11.png"));
        }
        else if (wv.Text.Contains("10"))
        {
            imgwv.Source = new BitmapImage(new System.Uri("pack://application:,,,/Resources/win10.png"));
        }
    }

    public PCinfo(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
        infoapply();
    }
}
