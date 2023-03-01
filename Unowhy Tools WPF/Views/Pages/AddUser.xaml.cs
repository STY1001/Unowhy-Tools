using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class AddUser : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        labu.Text = UT.GetLang("name");
        labp.Text = UT.GetLang("password");
        labc.Text = UT.GetLang("confpw");
        laba.Content = UT.GetLang("la");
        labb.Text = UT.GetLang("create");
    }

    public AddUser(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public void pass_Changed(object sender, RoutedEventArgs e)
    {
        if (passbox1.Text == "")
        {
            pimg1.Source = UT.GetImgSource("passno.png");
        }
        else
        {
            pimg1.Source = UT.GetImgSource("passyes.png");
        }

        if (passbox1.Text == passbox2.Text)
        {
            pimg2.Source = UT.GetImgSource("pwcyes.png");
        }
        else
        {
            pimg2.Source = UT.GetImgSource("pwcno.png");
        }
    }

    public void name_Changed(object sender, RoutedEventArgs e)
    {
        Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""]");

        if (r.IsMatch(namebox.Text.ToString().Trim()) || namebox.Text == "" || namebox.Text.Contains(" "))
        {
            uimg1.Source = UT.GetImgSource("deluser.png");
        }
        else
        {
            uimg1.Source = UT.GetImgSource("user.png");
        }
    }
    
    public async void Create_Changed(object sender, RoutedEventArgs e)
    {
        Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""]");

        if (r.IsMatch(namebox.Text.ToString().Trim()) || namebox.Text == "" || namebox.Text.Contains(" "))
        {
            UT.DialogIShow(UT.GetLang("an"), "no.png");
        }
        else
        {
            if (passbox1.Text == passbox2.Text)
            {
                if (UT.DialogQShow(UT.GetLang("adduser"), "adduser.png")
                {
                    await UT.waitstatus.open();

                    if (passbox1.Text == "")
                    {
                        await UT.RunMin("net", $"user \"{namebox.Text}\" /add");

                        if (laba.IsChecked == true)
                        {
                            await UT.RunMin("net", $"localgroup Administrateurs \"{namebox.Text}\" /add");
                        }
                    }
                    else
                    {
                        await UT.RunMin("net", $"user \"{namebox.Text}\" \"{passbox1.Text}\" /add");

                        if (laba.IsChecked == true)
                        {
                            await UT.RunMin("net", $"localgroup Administrateurs \"{namebox.Text}\" /add");
                        }
                    }

                    await UT.waitstatus.close();

                    UT.DialogIShow(UT.GetLang("adduser.id") + $": \"{namebox.Text}\"", "user.png");
                }
            }
            else
            {
                UT.DialogIShow(UT.GetLang("pwmis"), "no.png");
            }
        }
    }
}
