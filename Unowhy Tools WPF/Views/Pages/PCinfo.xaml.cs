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
        labpnc.Text = UT.GetLang("pcn");
        labuid.Text = UT.GetLang("domuser");
        labmf.Text = UT.GetLang("mfm");
        labsn.Text = UT.GetLang("serial");
        labbv.Text = UT.GetLang("biosversion");
        labwv.Text = UT.GetLang("os");
    }

    public void infoapply()
    {
        uid.Text = UTdata.UserID;
        pcn.Text = UTdata.HostName;
        mf.Text = UT.GetLine(UTdata.mf, 1) + " " + UT.GetLine(UTdata.md, 1);
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
