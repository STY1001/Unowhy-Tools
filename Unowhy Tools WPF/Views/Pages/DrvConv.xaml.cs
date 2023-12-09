using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media;

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
        //UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        //UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(DrvRest));
    }

    public async Task applylang()
    {
        labnew.Text = await UT.GetLang("new");
        labold.Text = await UT.GetLang("old");
        conv_btn.Text = await UT.GetLang("conv");
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(DrvRest), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);

        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Visible;
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 10,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            element.RenderTransform = transform;

            element.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);

            await Task.Delay(50);
        }
    }

    public DrvConv(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public async void oldpath_Click(object sender, RoutedEventArgs e)
    {
        using (var fbd = new FolderBrowserDialog())
        {
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                oldpath.Text = fbd.SelectedPath;
                if (!File.Exists(oldpath.Text + "\\UT-Restore.exe"))
                {
                    UT.DialogIShow(await UT.GetLang("conv.nout"), "no.png");
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
            sfd.Filter = "Unowhy Tools Driver file (*.zip)|*.zip";
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
            await UT.waitstatus.open(await UT.GetLang("wait.compress"), "zip.png");
            await Task.Delay(1000);
            ZipFile.CreateFromDirectory(oldpath.Text, newpath.Text, CompressionLevel.NoCompression, false);
            await UT.waitstatus.close();
            UT.DialogIShow(await UT.GetLang("done"), "yes.png");
        }
    }
}
