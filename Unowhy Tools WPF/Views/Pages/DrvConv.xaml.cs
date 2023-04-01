using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class DrvConv : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(DrvRest));
    }

    public void applylang()
    {
        labnew.Text = UT.GetLang("new");
        labold.Text = UT.GetLang("old");
        conv_btn.Text = UT.GetLang("conv");
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        UT.anim.BackBtnAnimForw(BackBTN);
    }

    public DrvConv(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public void oldpath_Click(object sender, RoutedEventArgs e)
    {
        using (var fbd = new FolderBrowserDialog())
        {
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                oldpath.Text = fbd.SelectedPath;
                if (!File.Exists(oldpath.Text + "\\UT-Restore.exe"))
                {
                    UT.DialogIShow(UT.GetLang("conv.nout"), "no.png");
                    oldpath.Text = "";
                }
            }
        }
    }

    public void newpath_Click(object sender, RoutedEventArgs e)
    {
        using (var sfd = new SaveFileDialog())
        {
            sfd.FileName = "UT-Drv_" + DateTime.Now.ToString("HH-mm_dd-MM-yy");
            sfd.DefaultExt = "zip";
            sfd.Filter = "zip (*.zip)|*.zip";
            sfd.FilterIndex = 1;
            sfd.Title = "Unowhy Tools";
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                newpath.Text = sfd.FileName;
            }
        }
    }

    public async void Conv_Click(object sender, RoutedEventArgs e)
    {
        if(newpath.Text == "" || oldpath.Text == "")
        {

        }
        else
        {
            await UT.waitstatus.open();
            await Task.Delay(1000);
            ZipFile.CreateFromDirectory(oldpath.Text, newpath.Text, CompressionLevel.NoCompression, false);
            await UT.waitstatus.close();
            UT.DialogIShow(UT.GetLang("done"), "yes.png");
        }
    }
}
