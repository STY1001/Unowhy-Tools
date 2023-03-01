using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class PCname : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        labold.Text = UT.GetLang("ancat");
        labnew.Text = UT.GetLang("cncat");
        pcn_btn.Text = UT.GetLang("change");
    }

    private void CheckBTN()
    {
        old.Text = UTdata.HostName;
    }

    public PCname(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
        CheckBTN();
    }

    public async void Change_Click(object sender, RoutedEventArgs e)
    {
        Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""]");

        if (r.IsMatch(newbox.Text.ToString().Trim()) || newbox.Text.Length < 2 || newbox.Text.Length > 15 || newbox.Text.Contains(" "))
        {
            UT.DialogIShow(UT.GetLang("avert"), "no.png")
        }
        else
        {
            if (UT.DialogQShow(UT.GetLang("pcname"), "customize.png"))
            {
                string name = newbox.Text;
                string arg = ($"-Command \"& {{Rename-Computer -NewName \"{name}\" -Force}}\"");

                await UT.RunMin("powershell", arg);
                old.Text = await UT.RunReturn("hostname", "");
            }
        }
    }
}
