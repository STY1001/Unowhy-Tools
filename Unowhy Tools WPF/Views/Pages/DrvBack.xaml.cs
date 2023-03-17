using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Windows.Forms;
using System;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class DrvBack : INavigableView<DashboardViewModel>
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
        UT.NavigateTo(typeof(Drivers));
    }

    public void applylang()
    {
        labbkapp.Text = UT.GetLang("bkapp");
        labpath.Text = UT.GetLang("bkpath");
        bk_btn.Text = UT.GetLang("backup");
    }

    public DrvBack(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public void Browse_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.SaveFileDialog())
        {
            fb.FileName = "UT-Drv_" + DateTime.Now.ToString("HH-mm_dd-MM-yy");
            fb.DefaultExt = "zip";
            fb.Filter = "zip (*.zip)|*.zip";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                bkpath.Text = fb.FileName;
            }
        }
    }
    
    public async void Backup_Click(object sender, RoutedEventArgs e)
    {
        if (bkpath.Text == "")
        {

        }
        else
        {
            string drivel = bkpath.Text.Substring(0, 1);
            DriveInfo di = new DriveInfo(drivel);
            long afs = di.AvailableFreeSpace;

            if (afs > 6000000000)
            {
                await UT.waitstatus.open();
                Directory.CreateDirectory(drivel + ":\\UT-Drv-TMP");
                string backuptemp = drivel + ":\\UT-Drv-TMP";
                await UT.Extract("UT-Restore.exe", backuptemp + "\\UT-Restore.exe");
                if (dismbk.IsChecked == true)
                {
                    await UT.RunMin("dism.exe", "/online /export-driver /destination:\"" + backuptemp + "\"");
                }
                else if (psbk.IsChecked == true)
                {
                    await UT.RunMin("powershell.exe", "Export-WindowsDriver -Online -Destination \"" + backuptemp + "\"");
                }
                await Task.Run(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        ZipFile.CreateFromDirectory(backuptemp, bkpath.Text, CompressionLevel.NoCompression, false);
                    });
                });
                Directory.Delete(backuptemp, true);
                FileInfo fi = new FileInfo(bkpath.Text);
                await UT.waitstatus.close();
                if (fi.Length > 1000000)
                {
                    UT.DialogIShow(UT.GetLang("done"), "yes.png");
                }
                else
                {
                    UT.DialogIShow(UT.GetLang("bkfail"), "no.png");
                }
            }
            else
            {
                UT.DialogIShow(UT.GetLang("space6gusb"), "no.png");
            }
        }
    }
}
