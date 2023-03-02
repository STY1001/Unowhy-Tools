using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Diagnostics;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class DrvRest : INavigableView<DashboardViewModel>
{
    public DashboardViewModel ViewModel
    {
        get;
    }

    public void applylang()
    {
        labconv.Text = UT.GetLang("convdesc");
        convbtn.Content = UT.GetLang("conv");
        labpath.Text = UT.GetLang("bkpath");
        rest_btn.Text = UT.GetLang("restore");
    }

    public DrvRest(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public void Browse_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new OpenFileDialog())
        {
            fb.DefaultExt = "zip";
            fb.Filter = "zip (*.zip)|*.zip";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                rtpath.Text = fb.FileName;
            }
        }
    }

    public async void Restore_Click(object sender, RoutedEventArgs e)
    {
        if (rtpath.Text == "")
        {

        }
        else
        {
            DriveInfo di = new DriveInfo("C");
            FileInfo fi = new FileInfo(rtpath.Text);
            if (di.AvailableFreeSpace > fi.Length * 2.5)
            {
                await UT.waitstatus.open();
                await Task.Delay(1000);
                string rttemps = Path.GetTempPath() + "\\Unowhy Tools\\Temps\\Drivers";
                await Task.Run(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        ZipFile.ExtractToDirectory(rtpath.Text, rttemps);
                    });
                });
                if (File.Exists(rttemps + "\\UT-Restore.exe"))
                {
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = rttemps + "\\UT-Restore.exe";
                        p.StartInfo.WorkingDirectory = rttemps;
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.WaitForExit();
                    });

                }
                else
                {
                    UT.DialogIShow(UT.GetLang("conv.nout"), "no.png");
                }
                Directory.Delete(rttemps, true);
                Directory.CreateDirectory(rttemps);
                await UT.waitstatus.close();
                UT.DialogIShow(UT.GetLang("rebootmsg"), "reboot.png");
                Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
            }
            else
            {
                UT.DialogIShow(UT.GetLang("space6gpc"), "no.png");
            }
        }
    }
}
