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
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using static Unowhy_Tools.UT;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Shapes;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class HackBGRT : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();
    public bool utadeployed = false;
    public BitmapImage ImageApply;
    public bool HackBGRTInstalled;

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void GoBack(object sender, RoutedEventArgs e)
    {
        //UT.anim.BackBtnAnim(BackBTN);
        await System.Threading.Tasks.Task.Delay(150);
        //UT.anim.TransitionBack(RootGrid);
        await System.Threading.Tasks.Task.Delay(200);
        UT.NavigateTo(typeof(Dashboard));
    }

    public async void Init(object sender, EventArgs e)
    {

    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {

    }

    public HackBGRT(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public async Task applylang()
    {
        openimgbtnlab.Text = await UT.GetLang("openimg");
        removebtnlab.Text = await UT.GetLang("uninstall");
    }

    private void OpenImg_Click(object sender, RoutedEventArgs e)
    {
        using (var fb = new System.Windows.Forms.OpenFileDialog())
        {
            fb.Filter = "Image file|*.bmp;*.png;*.jpg";
            fb.FilterIndex = 1;
            fb.Title = "Unowhy Tools";
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK)
            {
                string imgpath = fb.FileName;
                BitmapImage preimage = new BitmapImage(new Uri(imgpath));
                BitmapImage postimage = ResizeImage(preimage);
                imagepreview.Source = postimage;
                ImageApply = postimage;
            }
        }
    }

    private void xnumbox_ValueChanged(object sender, RoutedEventArgs e)
    {
        try
        {
            xslider.Value = (double)xnumbox.Value;
            imagepreview.RenderTransform = new TranslateTransform((double)xnumbox.Value, (double)ynumbox.Value);
            positioncustom.IsChecked = true;
        }
        catch (Exception)
        {

        }
    }

    private void xslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            xnumbox.Value = Math.Round(xslider.Value, 0);
        }
        catch (Exception)
        {

        }
    }

    private void ynumbox_ValueChanged(object sender, RoutedEventArgs e)
    {
        try
        {
            yslider.Value = (double)ynumbox.Value;
            imagepreview.RenderTransform = new TranslateTransform((double)xnumbox.Value, (double)ynumbox.Value);
            positioncustom.IsChecked = true;
        }
        catch (Exception)
        {

        }
    }

    private void yslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            ynumbox.Value = Math.Round(yslider.Value, 0);
        }
        catch (Exception)
        {

        }
    }

    private void positioncenter_Click(object sender, RoutedEventArgs e)
    {
        xnumbox.Value = 0;
        ynumbox.Value = 0;
        positioncenter.IsChecked = true;
    }

    private void positionupper_Click(object sender, RoutedEventArgs e)
    {
        xnumbox.Value = 0;
        ynumbox.Value = -200;
        positionupper.IsChecked = true;
    }

    public BitmapImage ResizeImage(BitmapImage imgToResize)
    {
        int sourceWidth = imgToResize.PixelWidth;
        int sourceHeight = imgToResize.PixelHeight;

        double nPercent = 0;
        double nPercentW = 0;
        double nPercentH = 0;

        nPercentW = ((double)1920 / (double)sourceWidth);
        nPercentH = ((double)1080 / (double)sourceHeight);

        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        BitmapImage b = new BitmapImage();
        b.BeginInit();
        b.UriSource = imgToResize.UriSource;
        b.DecodePixelWidth = destWidth;
        b.DecodePixelHeight = destHeight;
        b.EndInit();

        return b;
    }

    public async Task ExportToBmp(BitmapImage bitmapImage, string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.Save(stream);
        }
    }

    private async void installbtn_Click(object sender, RoutedEventArgs e)
    {
        if (UT.DialogQShow("Install/Update", "customize.png"))
        {
            string sb = await UT.RunReturn("powershell", "Confirm-SecureBootUEFI");
            if (sb.Contains("True"))
            {
                if (UT.DialogQShow(await UT.GetLang("securebootwarn"), "no.png"))
                {
                    await install();
                }
            }
            else
            {
                await install();
            }
        }
    }

    private async Task install()
    {
        if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\setup.exe"))
        {
            if (HackBGRTInstalled)
            {
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\setup.exe";
                    p.StartInfo.Arguments = "batch uninstall";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\setup.exe";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });

                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\config.txt"))
                {
                    File.Delete(UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\config.txt");
                }

                string configtemplate = "boot=MS\r\nimage= x=%x% y=%y% path=\\EFI\\HackBGRT\\splash.bmp\r\nresolution=0x0\r\nlog=1\r\ndebug=0";
                configtemplate = configtemplate.Replace("%x%", xnumbox.Value.ToString());
                configtemplate = configtemplate.Replace("%y%", ynumbox.Value.ToString());
                File.WriteAllText(UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\config.txt", configtemplate);

                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\splash.bmp"))
                {
                    File.Delete(UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\splash.bmp");
                }

                if (ImageApply != null)
                {
                    await ExportToBmp(ImageApply, UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\splash.bmp");
                }

                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\setup.exe";
                    p.StartInfo.Arguments = "batch install enable-entry";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temp\\HackBGRT\\setup.exe";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
        }
    }

    private async Task Check()
    {
        string letter = "null";
        if (Directory.Exists("A:\\"))
        {
            await UT.RunMin("mountvol", "a: /s");
            letter = "A";
        }
        else if (Directory.Exists("B:\\"))
        {
            await UT.RunMin("mountvol", "b: /s");
            letter = "B";
        }

        if (File.Exists(letter + ":\\EFI\\HackBGRT\\config.txt"))
        {
            HackBGRTInstalled = true;

            var lines = File.ReadAllLines(letter + ":\\EFI\\HackBGRT\\config.txt");

            string xFValue = "0";
            string yFValue = "0";
            string pathFValue = "null";

            try
            {

                foreach (var line in lines.Where(s => s.StartsWith("image=")))
                {
                    var linepost = line.Replace("image= ", "");
                    linepost = linepost.Replace(" ", "\n");
                    File.WriteAllText("C:\\Users\\HIDAYET\\Downloads\\HackBGRT-2.3.1\\configtemp.txt", linepost);
                    var lines2 = File.ReadAllLines("C:\\Users\\HIDAYET\\Downloads\\HackBGRT-2.3.1\\configtemp.txt");
                    foreach (var line2 in lines2.Where(s => s.StartsWith("x=")))
                    {
                        string linepost2 = line2.Replace("x=", "");
                        xFValue = linepost2;
                    }
                    foreach (var line2 in lines2.Where(s => s.StartsWith("y=")))
                    {
                        string linepost2 = line2.Replace("y=", "");
                        yFValue = linepost2;
                    }
                    foreach (var line2 in lines2.Where(s => s.StartsWith("path=")))
                    {
                        string linepost2 = line2.Replace("path=", "");
                        pathFValue = linepost2;
                    }
                }
            }
            catch (Exception)
            {

            }

            if (pathFValue != "null")
            {
                if (File.Exists(":" + pathFValue))
                {
                    BitmapImage preimage = new BitmapImage(new Uri(":" + pathFValue));
                    BitmapImage postimage = ResizeImage(preimage);
                    imagepreview.Source = postimage;
                    ImageApply = postimage;
                }
            }
            double.TryParse(xFValue, out double xResultF);
            double.TryParse(yFValue, out double yResultF);
            xnumbox.Value = Math.Round(xResultF, 0);
            ynumbox.Value = Math.Round(yResultF, 0);
        }
        else
        {
            HackBGRTInstalled = false;
        }
    }
}
