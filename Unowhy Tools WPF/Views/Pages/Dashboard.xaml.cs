using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Windows;

using Unowhy_Tools;
using System.Diagnostics;
using System;
using Microsoft.Win32;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Dashboard : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public void GoForw(object sender, RoutedEventArgs e)
    {
        UT.anim.TransitionForw(RootGrid);
    }

    public async void Init(object sender, EventArgs e)
    {
        DoubleAnimation anim = new DoubleAnimation();
        anim.From = -50;
        anim.To = 0;
        anim.Duration = TimeSpan.FromMilliseconds(600);
        anim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };
        TranslateTransform trans = new TranslateTransform();
        quickoption.RenderTransform = trans;

        DoubleAnimation anim2 = new DoubleAnimation();
        anim2.From = 0;
        anim2.To = 1;
        anim2.Duration = TimeSpan.FromMilliseconds(600);
        anim2.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseInOut, Power = 5 };

        quickoption.BeginAnimation(Border.OpacityProperty, anim2);
        trans.BeginAnimation(TranslateTransform.YProperty, anim);

        string ver = "Version " + UT.version.getver() + " (Build " + UT.version.getverbuild().ToString() + ") ";

        if (UT.version.isdeb()) ver = ver + "(Debug/Beta)";
        else ver = ver + "(Release)";

        verlab.Text = ver;

        applylang();
        pcname.Text = UT.GetLine(UTdata.HostName, 1);
        if (pcname.Text.Contains("Lenovo"))
        {
            pcname.Text = "Unowhy-Win11";
        }

        if (UT.CheckInternet())
        {
            RegistryKey lcs = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utcuab = lcs.GetValue("UpdateStart").ToString();
            if (utcuab == "1")
            {
                lababout2.Text = UT.GetLang("update.check");
                if (await UT.version.newver())
                {
                    Color white = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                    Color gray = (Color)ColorConverter.ConvertFromString("#bebebe");
                    lababout2.Text = UT.GetLang("newver");
                    lababout2.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(white);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(gray);
                    await Task.Delay(500);
                    lababout2.Foreground = new SolidColorBrush(white);
                }
                else
                {
                    lababout2.Text = "Unowhy Tools";
                }
            }
        }
        else
        {
            lababout2.Text = "Unowhy Tools";
        }
    }

    public Dashboard(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public void applylang()
    {
        lababout.Text = UT.GetLang("about");
        labinfo.Text = UT.GetLang("pcinfo");
        labset.Text = UT.GetLang("settings");
    }

    public void Taskmgr(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start("taskmgr.exe");
    }
    
    public void CMD(object sender, RoutedEventArgs e)
    {
        //System.Diagnostics.Process.Start("cmd.exe");
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.WorkingDirectory = "C:\\Windows\\System32\\";
        p.Start();
    }
    
    public void Regedit(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start("regedit.exe");
    }

    public void Gpedit(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start("mmc.exe", "gpedit.msc");
    }

    public void Guide(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = "https://github.com/STY1001/Unowhy-Tools/blob/master/GUIDE.md", UseShellExecute = true });
    }
}
