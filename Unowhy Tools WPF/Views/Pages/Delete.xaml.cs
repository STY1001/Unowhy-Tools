using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Delete : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async Task applylang()
    {
        instprog_txt.Text = await UT.GetLang("unprog");
        instprog_desc.Text = "ms-settings:appsfeatures";
        instprog_btn.Content = await UT.GetLang("open");
        entu_txt.Text = await UT.GetLang("ent");
        entu_desc.Text = await UT.GetLang("descent");
        entu_btn.Content = await UT.GetLang("del");
        hsq_txt.Text = await UT.GetLang("delhis");
        hsq_desc.Text = await UT.GetLang("deschisdel");
        hsq_btn.Content = await UT.GetLang("uninstall");
        aad_txt.Text = await UT.GetLang("aadleave");
        aad_desc.Text = await UT.GetLang("descaadleave");
        aad_btn.Content = await UT.GetLang("disconnect");
        hsmqf_txt.Text = await UT.GetLang("delhism");
        hsmqf_desc.Text = await UT.GetLang("deschismdel");
        hsmqf_btn.Content = await UT.GetLang("delete");
        tif_txt.Text = await UT.GetLang("delti");
        tif_desc.Text = await UT.GetLang("desctidel");
        tif_btn.Content = await UT.GetLang("delete");
        ridff_txt.Text = await UT.GetLang("delridf");
        ridff_desc.Text = await UT.GetLang("descridf");
        ridff_btn.Content = await UT.GetLang("delete");
        oemf_txt.Text = await UT.GetLang("deloem");
        oemf_desc.Text = await UT.GetLang("descdeloem");
        oemf_btn.Content = await UT.GetLang("delete");
        entf_txt.Text = await UT.GetLang("delentf");
        entf_desc.Text = await UT.GetLang("descdelentf");
        entf_btn.Content = await UT.GetLang("delete");
    }

    public async Task CheckBTN(bool check)
    {
        if (check)
        {
            await UT.Check();
        }
        entu.IsEnabled = true;
        hsq.IsEnabled = true;
        aad.IsEnabled = true;
        hsmqf.IsEnabled = true;
        tif.IsEnabled = true;
        ridff.IsEnabled = true;
        oemf.IsEnabled = true;
        entf.IsEnabled = true;
        if (UTdata.ENTUser == true) entu.IsEnabled = true;
        else entu.IsEnabled = false;
        if (UTdata.HSQFolderExist == true) hsq.IsEnabled = true;
        else hsq.IsEnabled = false;
        if (UTdata.AAD == true) aad.IsEnabled = true;
        else aad.IsEnabled = false;
        if (UTdata.HSQMFolderExist == true) hsmqf.IsEnabled = true;
        else hsmqf.IsEnabled = false;
        if (UTdata.TIFolderExist == true) tif.IsEnabled = true;
        else tif.IsEnabled = false;
        if (UTdata.RIDFFolderExist == true) ridff.IsEnabled = true;
        else ridff.IsEnabled = false;
        if (UTdata.OEMFolderExist == true) oemf.IsEnabled = true;
        else oemf.IsEnabled = false;
        if (UTdata.ENTFolderExist == true) entf.IsEnabled = true;
        else entf.IsEnabled = false;
        if (check)
        {
            await CheckFolderSize();
        }
    }

    public async Task CheckFolderSize()
    {
        hsq_txt.Text = await UT.GetLang("delhis") + "   (" + await UT.FolderSizeString("C:\\Program Files\\Unowhy\\HiSqool") + ")";
        hsmqf_txt.Text = await UT.GetLang("delhism") + "   (" + await UT.FolderSizeString("C:\\Program Files\\Unowhy\\Hisqool manager") + ")";
        tif_txt.Text = await UT.GetLang("delti") + "   (" + await UT.FolderSizeString("C:\\Program Files\\Unowhy\\TO_INSTALL") + ")";
        ridff_txt.Text = await UT.GetLang("delridf") + "   (" + await UT.FolderSizeString("C:\\ProgramData\\RIDF") + ")";
        oemf_txt.Text = await UT.GetLang("deloem") + "   (" + await UT.FolderSizeString("C:\\Windows\\System32\\OEM") + ")";
        entf_txt.Text = await UT.GetLang("delentf") + "   (" + await UT.FolderSizeString("C:\\ProgramData\\ENT") + ")";
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        await CheckBTN(false);
        await CheckFolderSize();
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootStack.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.UnDeployBack();

        await CheckBTN(false);
        
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
                From = 30,
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

    public Delete(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public async void instprog_Click(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = "ms-settings:appsfeatures",
                        UseShellExecute = true
        });
    }

    public async void entu_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("ent"), "deluser.png"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "deluser.png");
            await UT.RunMin("net", "user ENT /delete");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!entu.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void hsq_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("delhis"), "uninstall.png"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.uninstall"), "uninstall.png");
            await Task.Run(() =>
            {
                Process p = Process.Start("C:\\Program Files\\Unowhy\\HiSqool\\Uninstall Hisqool.exe"); 
                p.WaitForExit();
            });
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!hsq.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void aad_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("aadleave"), "azure.png"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.disconnect"), "azure.png");
            await UT.RunMin("powershell", "start-process -FilePath \"dsregcmd\" -ArgumentList \"/leave\" -nonewwindow");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!aad.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void hsmqf_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("delhism"), "service.png"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "service.png");
            await UT.serv.del("Hisqoolmanager");
            await UT.RunMin("cmd", "/w /c rmdir /s /q \"C:\\Program Files\\Unowhy\\Hisqool manager\"");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!hsmqf.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void tif_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("delti"), "folder.png"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "folder.png");
            await UT.RunMin("cmd", "/w /c del /q /f \"C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\silent_*.*\"");
            await UT.RunMin("cmd", "/w /c rmdir /q /s \"C:\\Program Files\\Unowhy\\TO_INSTALL\"");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!tif.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void ridff_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("delridf"), "folder.png"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "folder.png");
            await UT.RunMin("cmd", "/w /c rmdir /q /s \"C:\\ProgramData\\RIDF\"");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!ridff.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void oemf_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("deloem"), "folder.png"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "folder.png");
            await UT.RunMin("cmd", "/w /c rmdir /q /s \"C:\\Windows\\System32\\OEM\"");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!oemf.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }

    public async void entf_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("delentf"), "folder.png"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "folder.png");
            await UT.RunMin("cmd", "/w /c rmdir /q /s \"C:\\ProgramData\\ENT\"");
            await Task.Delay(1000);
            await UT.waitstatus.open(await UT.GetLang("wait.check"), "check.png");
            await CheckBTN(true);
            await UT.waitstatus.close();
            if (!entf.IsEnabled)
            {
                UT.DialogIShow(await UT.GetLang("done"), "yes.png");
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }
}
