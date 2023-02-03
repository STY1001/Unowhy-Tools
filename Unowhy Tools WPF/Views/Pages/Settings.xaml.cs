using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

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
        labol.Text = UT.getlang("logsopen");
        labdl.Text = UT.getlang("logsdel");
        dl.Content = UT.getlang("delete");
        ol.Content = UT.getlang("open");
        lablang.Text = UT.getlang("lang");
    }

    public void CheckBTN()
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

        dl.Content = UT.getlang("clean") + " (" + size + ")";
    }

    public Settings(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
        CheckBTN();
    }
}
