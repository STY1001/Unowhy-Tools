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
using System.IO.Compression;
using System.Threading;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class HackBGRT : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();
    public bool utadeployed = false;
    public BitmapImage ImageApply;
    public BitmapImage ImageSource;
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
        await Check();
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        await UT.DeployBack(typeof(Customize), RootGrid, RootBorder);
        await Task.Delay(1000);
        UT.anim.BorderZoomOut(RootBorder);
    }

    public HackBGRT(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        applylang();
    }

    public async Task applylang()
    {
        openimgbtnlab.Text = await UT.GetLang("openimg");
        removebtnlab.Text = await UT.GetLang("uninstall");
    }

    private async void OpenImg_Click(object sender, RoutedEventArgs e)
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
                await UpdatePreview(preimage);
                ImageSource = ResizeImage(preimage);
            }
        }
    }

    private async Task UpdatePreview(BitmapImage image)
    {
        image = ResizeImage(image);
        imagepreview.Source = image;
        imagepreview.Width = image.PixelWidth;
        imagepreview.Height = image.PixelHeight;
        xsizenumbox.Value = image.PixelWidth;
        ysizenumbox.Value = image.PixelHeight;
        ImageApply = image;
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

    private void xsizenumbox_ValueChanged(object sender, RoutedEventArgs e)
    {
        try
        {
            xsizeslider.Value = (double)xsizenumbox.Value;
            if (keepaspectratio.IsChecked == true)
            {
                imagepreview.Height = (double)xsizenumbox.Value * (imagepreview.Height / imagepreview.Width);
                imagepreview.Width = (double)xsizenumbox.Value;
            }
            else
            {
                imagepreview.Width = (double)xsizenumbox.Value;
            }
        }
        catch (Exception)
        {

        }
    }

    private void xsizeslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            xsizenumbox.Value = Math.Round(xsizeslider.Value, 0);
        }
        catch (Exception)
        {

        }
    }

    private void ysizenumbox_ValueChanged(object sender, RoutedEventArgs e)
    {
        try
        {
            ysizeslider.Value = (double)ysizenumbox.Value;
            if (keepaspectratio.IsChecked == true)
            {
                imagepreview.Width = (double)ysizenumbox.Value * (imagepreview.Width / imagepreview.Height);
                imagepreview.Height = (double)ysizenumbox.Value;
            }
            else
            {
                imagepreview.Height = (double)ysizenumbox.Value;
            }
        }
        catch (Exception)
        {

        }
    }

    private void ysizeslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        try
        {
            ysizenumbox.Value = Math.Round(ysizeslider.Value, 0);
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

        var fadeInAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.30),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var zoomAnimation1 = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.50),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var zoomAnimation2 = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.50),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        Storyboard.SetTarget(fadeInAnimation, centeranim);
        Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(UIElement.OpacityProperty));
        Storyboard.SetTarget(zoomAnimation1, centeranim);
        Storyboard.SetTarget(zoomAnimation2, centeranim);
        Storyboard.SetTargetProperty(zoomAnimation1, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
        Storyboard.SetTargetProperty(zoomAnimation2, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
        var storyboard = new Storyboard();
        storyboard.Children.Add(fadeInAnimation);
        storyboard.Children.Add(zoomAnimation1);
        storyboard.Children.Add(zoomAnimation2);
        storyboard.Begin();
    }

    private void positionupper_Click(object sender, RoutedEventArgs e)
    {
        xnumbox.Value = 0;
        ynumbox.Value = -200;
        positionupper.IsChecked = true;

        var fadeInAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.30),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var zoomAnimation1 = new DoubleAnimation
        {
            From = 2,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.50),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        var zoomAnimation2 = new DoubleAnimation
        {
            From = 2,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.50),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        Storyboard.SetTarget(fadeInAnimation, upperanim);
        Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(UIElement.OpacityProperty));
        Storyboard.SetTarget(zoomAnimation1, upperanim);
        Storyboard.SetTarget(zoomAnimation2, upperanim);
        Storyboard.SetTargetProperty(zoomAnimation1, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
        Storyboard.SetTargetProperty(zoomAnimation2, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
        var storyboard = new Storyboard();
        storyboard.Children.Add(fadeInAnimation);
        storyboard.Children.Add(zoomAnimation1);
        storyboard.Children.Add(zoomAnimation2);
        storyboard.Begin();
    }

    public BitmapImage ResizeImage(BitmapImage imgToResize)
    {
        if (imgToResize.Width > 1920 || imgToResize.Height > 1080)
        {
            double ratioX = 1920 / imgToResize.Width;
            double ratioY = 1080 / imgToResize.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(imgToResize.Width * ratio);
            int newHeight = (int)(imgToResize.Height * ratio);

            BitmapImage resizedimage = new BitmapImage();
            resizedimage.BeginInit();
            resizedimage.UriSource = new Uri(imgToResize.UriSource.ToString());
            resizedimage.DecodePixelWidth = newWidth;
            resizedimage.DecodePixelHeight = newHeight;
            resizedimage.EndInit();

            return resizedimage;
        }
        else
        {
            return imgToResize;
        }
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
        if (UT.DialogQShow(installbtnlab.Text, "customize.png"))
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
        if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\setup.exe"))
        {
            UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");

            if (UT.CheckInternet())
            {
                var progress = new System.Progress<double>();
                var cancellationToken = new CancellationTokenSource();
                string dl = await UT.GetLang("wait.download");
                progress.ProgressChanged += async (sender, value) =>
                {
                    await UT.waitstatus.open(dl + " (" + value.ToString("###") + "%)", "download.png");
                };
                await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("hackbgrt"), UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT.zip", progress, cancellationToken.Token);
                await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
                await Task.Delay(1000);
                await Task.Run(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        ZipFile.ExtractToDirectory(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT.zip", UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT", true);
                    });
                });
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
            }
        }

        if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\setup.exe"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.apply"), "customize.png");
            if (HackBGRTInstalled)
            {
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\setup.exe";
                    p.StartInfo.Arguments = "batch uninstall";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
            }
            if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\config.txt"))
            {
                File.Delete(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\config.txt");
            }

            string configtemplate = "boot=MS\r\nimage= x=%x% y=%y% path=\\EFI\\HackBGRT\\splash.bmp\r\nresolution=0x0\r\nlog=1\r\ndebug=0";
            configtemplate = configtemplate.Replace("%x%", xnumbox.Value.ToString());
            configtemplate = configtemplate.Replace("%y%", ynumbox.Value.ToString());
            File.WriteAllText(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\config.txt", configtemplate);

            if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\splash.bmp"))
            {
                File.Delete(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\splash.bmp");
            }

            if (ImageApply != null)
            {
                await ExportToBmp(ImageApply, UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\splash.bmp");
            }

            await Task.Run(() =>
            {
                Process p = new Process();
                p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\setup.exe";
                p.StartInfo.Arguments = "batch install enable-entry";
                p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            });

            await UT.waitstatus.close();
            UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
            Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
        }
    }

    private async Task Check()
    {
        string letter = "null";
        if (!Directory.Exists("A:\\") || Directory.Exists("A:\\EFI"))
        {
            await UT.RunMin("mountvol", "a: /s");
            letter = "A";
        }
        else if (!Directory.Exists("B:\\") || Directory.Exists("A:\\EFI"))
        {
            await UT.RunMin("mountvol", "b: /s");
            letter = "B";
        }

        UT.Write2Log("Checking HackBGRT on letter: " + letter);

        if (File.Exists(letter + ":\\EFI\\HackBGRT\\config.txt"))
        {
            UT.Write2Log("HackBGRT is installed");
            HackBGRTInstalled = true;
            removebtn.IsEnabled = true;
            installbtnimg.Source = UT.GetImgSource("customize.png");
            installbtnlab.Text = await UT.GetLang("update");

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
                    File.WriteAllText(letter + ":\\EFI\\HackBGRT\\configtemp.txt", linepost);
                    var lines2 = File.ReadAllLines(letter + ":\\EFI\\HackBGRT\\configtemp.txt");
                    File.Delete(letter + ":\\EFI\\HackBGRT\\configtemp.txt");
                    foreach (var line2 in lines2.Where(s => s.StartsWith("x=")))
                    {
                        string linepost2 = line2.Replace("x=", "");
                        xFValue = linepost2;
                        UT.Write2Log("x: " + xFValue);
                    }
                    foreach (var line2 in lines2.Where(s => s.StartsWith("y=")))
                    {
                        string linepost2 = line2.Replace("y=", "");
                        yFValue = linepost2;
                        UT.Write2Log("y: " + yFValue);
                    }
                    foreach (var line2 in lines2.Where(s => s.StartsWith("path=")))
                    {
                        string linepost2 = line2.Replace("path=", "");
                        pathFValue = linepost2;
                        UT.Write2Log("path: " + pathFValue);
                    }
                }
            }
            catch (Exception e)
            {
                UT.Write2Log("Error while reading config.txt: " + e.Message);
            }

            UT.Write2Log("x: " + xFValue);
            UT.Write2Log("y: " + yFValue);
            UT.Write2Log("path: " + pathFValue);

            if (pathFValue != "null")
            {
                if (File.Exists(letter + ":" + pathFValue))
                {
                    BitmapImage preimage = new BitmapImage(new Uri(letter + ":" + pathFValue));
                    await UpdatePreview(preimage);
                    ImageSource = ResizeImage(preimage);
                }
            }
            double.TryParse(xFValue, out double xResultF);
            double.TryParse(yFValue, out double yResultF);
            xnumbox.Value = Math.Round(xResultF, 0);
            ynumbox.Value = Math.Round(yResultF, 0);
        }
        else
        {
            UT.Write2Log("HackBGRT is not installed");
            HackBGRTInstalled = false;
            removebtn.IsEnabled = false;
            installbtnimg.Source = UT.GetImgSource("download.png");
            installbtnlab.Text = await UT.GetLang("install");

            if (UT.DialogQShow(await UT.GetLang("nohackbgrt"), "ic.png"))
            {
                if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\ChangeLogoWin64.exe") || !File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe") || !File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "amigendrv64.sys"))
                {
                    UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");

                    if (UT.CheckInternet())
                    {
                        var progress = new System.Progress<double>();
                        var cancellationToken = new CancellationTokenSource();
                        string dl = await UT.GetLang("wait.download");
                        progress.ProgressChanged += async (sender, value) =>
                        {
                            await UT.waitstatus.open(dl + " (" + value.ToString("###") + "%)", "download.png");
                        };
                        await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("changelogo"), UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\ChangeLogoWin64.exe", progress, cancellationToken.Token);

                        progress = new System.Progress<double>();
                        cancellationToken = new CancellationTokenSource();
                        dl = await UT.GetLang("wait.download");
                        progress.ProgressChanged += async (sender, value) =>
                        {
                            await UT.waitstatus.open(dl + " (" + value.ToString("###") + "%)", "download.png");
                        };
                        await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("afuwin"), UT.utpath + "\\Unowhy Tools\\Temps\\AFUWin.zip", progress, cancellationToken.Token);
                        await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
                        await Task.Delay(1000);
                        await Task.Run(() =>
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ZipFile.ExtractToDirectory(UT.utpath + "\\Unowhy Tools\\Temps\\AFUWin.zip", UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU", true);
                            });
                        });
                    }
                    else
                    {
                        UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                    }
                }

                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\ChangeLogoWin64.exe") && File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe") && File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "amigendrv64.sys"))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.dump"), "upload.png");
                    if(File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\dump.rom"))
                    {
                        File.Delete(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\dump.rom");
                    }
                    if(File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\splash.bmp"))
                    {
                        File.Delete(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\splash.bmp");
                    }
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\AFUWINx64.exe";
                        p.StartInfo.Arguments = $"\"{UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\dump.rom"}\" /O";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU";
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.WaitForExit();
                    });
                    if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\dump.rom"))
                    {
                        await UT.waitstatus.open(await UT.GetLang("wait.extract"), "upload.png");
                        await Task.Run(() =>
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\ChangeLogoWin64.exe";
                            p.StartInfo.Arguments = "/i dump.rom /e splash.bmp";
                            p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo";
                            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            p.StartInfo.CreateNoWindow = true;
                            p.Start();
                            p.WaitForExit();
                        });
                        BitmapImage preimage = new BitmapImage(new Uri(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\splash.bmp"));
                        await UpdatePreview(preimage);
                        ImageSource = ResizeImage(preimage);

                        await UT.waitstatus.close();
                    }
                    else
                    {
                        await UT.waitstatus.close();
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                    }
                }
                else
                {
                    UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                }
            }
        }
    }

    bool editdeployed = false;

    private async void EditImgBorder_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!editdeployed)
        {
            var fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.30),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(fadeInAnimation, EditImgBorder);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(UIElement.OpacityProperty));
            var storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);
            storyboard.Begin();

            await Task.Delay(300);

            EditImgBorder.Opacity = 1;
        }
    }

    private async void EditImgBorder_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!editdeployed)
        {
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.30),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(fadeOutAnimation, EditImgBorder);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(UIElement.OpacityProperty));
            var storyboard = new Storyboard();
            storyboard.Children.Add(fadeOutAnimation);
            storyboard.Begin();

            await Task.Delay(300);

            EditImgBorder.Opacity = 0;
        }
    }

    private async void EditImgBorder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        editgrid.Visibility = Visibility.Visible;
        {
            var fadeAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.30),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var zoomAnimation1 = new DoubleAnimation
            {
                From = 1,
                To = 0.9,
                Duration = TimeSpan.FromSeconds(0.50),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var zoomAnimation2 = new DoubleAnimation
            {
                From = 1,
                To = 0.9,
                Duration = TimeSpan.FromSeconds(0.50),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(fadeAnimation, positiongrid);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTarget(zoomAnimation1, positiongrid);
            Storyboard.SetTarget(zoomAnimation2, positiongrid);
            Storyboard.SetTargetProperty(zoomAnimation1, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(zoomAnimation2, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
            var storyboard = new Storyboard();
            storyboard.Children.Add(fadeAnimation);
            storyboard.Children.Add(zoomAnimation1);
            storyboard.Children.Add(zoomAnimation2);
            storyboard.Begin();
        }

        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            editgrid.RenderTransform = transform;
            editgrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }

        {
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.30),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(fadeOutAnimation, EditImgBorder);
            Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(UIElement.OpacityProperty));
            var storyboard = new Storyboard();
            storyboard.Children.Add(fadeOutAnimation);
            storyboard.Begin();

            await Task.Delay(300);

            EditImgBorder.Opacity = 0;
        }

        editdeployed = true;
        await Task.Delay(500);
        positiongrid.Visibility = Visibility.Hidden;
    }

    private async void returnbtn_Click(object sender, RoutedEventArgs e)
    {
        positiongrid.Visibility = Visibility.Visible;
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            DoubleAnimation translateAnimation = new DoubleAnimation
            {
                From = 0,
                To = -50,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            TranslateTransform transform = new TranslateTransform();
            editgrid.RenderTransform = transform;
            editgrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
        }

        {
            var fadeAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.30),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var zoomAnimation1 = new DoubleAnimation
            {
                From = 0.9,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var zoomAnimation2 = new DoubleAnimation
            {
                From = 0.9,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(fadeAnimation, positiongrid);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTarget(zoomAnimation1, positiongrid);
            Storyboard.SetTarget(zoomAnimation2, positiongrid);
            Storyboard.SetTargetProperty(zoomAnimation1, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(zoomAnimation2, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));
            var storyboard = new Storyboard();
            storyboard.Children.Add(fadeAnimation);
            storyboard.Children.Add(zoomAnimation1);
            storyboard.Children.Add(zoomAnimation2);
            storyboard.Begin();
        }

        await Task.Delay(500);
        editgrid.Visibility = Visibility.Hidden;
        editdeployed = false;
    }

    public BitmapImage ResizeImageManual(BitmapImage originalImage, double newWidth, double newHeight)
    {
        var resizedImage = new BitmapImage();
        resizedImage.BeginInit();
        resizedImage.DecodePixelWidth = (int)newWidth;
        resizedImage.DecodePixelHeight = (int)newHeight;
        resizedImage.CacheOption = BitmapCacheOption.OnLoad;
        resizedImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        resizedImage.UriSource = originalImage.UriSource;
        resizedImage.EndInit();

        return resizedImage;
    }

    private async void sizeslider_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        xsizenumbox.Value = imagepreview.Width;
        ysizenumbox.Value = imagepreview.Height;
        await EditImageUpdate();
    }

    public async Task EditImageUpdate()
    {
        UpdatePreview(ResizeImageManual(ImageSource, imagepreview.Width, imagepreview.Height));
    }

    private async void sizenumbox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        await EditImageUpdate();
    }

    private async void removebtn_Click(object sender, RoutedEventArgs e)
    {
        if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\setup.exe"))
        {
            UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");

            if (UT.CheckInternet())
            {
                var progress = new System.Progress<double>();
                var cancellationToken = new CancellationTokenSource();
                string dl = await UT.GetLang("wait.download");
                progress.ProgressChanged += async (sender, value) =>
                {
                    await UT.waitstatus.open(dl + " (" + value.ToString("###") + "%)", "download.png");
                };
                await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("hackbgrt"), UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT.zip", progress, cancellationToken.Token);
                await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
                await Task.Delay(1000);
                await Task.Run(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        ZipFile.ExtractToDirectory(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT.zip", UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT", true);
                    });
                });
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
            }
        }

        if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\setup.exe"))
        {
            await UT.waitstatus.open(await UT.GetLang("wait.delete"), "delete.png");
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT\\setup.exe";
                    p.StartInfo.Arguments = "batch uninstall";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\HackBGRT";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });

            await UT.waitstatus.close();
            UT.DialogIShow(await UT.GetLang("rebootmsg"), "reboot.png");
            Process.Start("shutdown", "-r -t 10 -c \"Unowhy Tools\"");
        }
    }

    private async void EditImgBorder_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (UT.DialogQShow(await UT.GetLang("nohackbgrt"), "ic.png"))
        {
            if (!File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\ChangeLogoWin64.exe") || !File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe") || !File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "amigendrv64.sys"))
            {
                UT.DialogIShow(await UT.GetLang("needres"), "clouddl.png");

                if (UT.CheckInternet())
                {
                    var progress = new System.Progress<double>();
                    var cancellationToken = new CancellationTokenSource();
                    string dl = await UT.GetLang("wait.download");
                    progress.ProgressChanged += async (sender, value) =>
                    {
                        await UT.waitstatus.open(dl + " (" + value.ToString("###") + "%)", "download.png");
                    };
                    await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("changelogo"), UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\ChangeLogoWin64.exe", progress, cancellationToken.Token);

                    progress = new System.Progress<double>();
                    cancellationToken = new CancellationTokenSource();
                    dl = await UT.GetLang("wait.download");
                    progress.ProgressChanged += async (sender, value) =>
                    {
                        await UT.waitstatus.open(dl + " (" + value.ToString("###") + "%)", "download.png");
                    };
                    await UT.DlFilewithProgress(await UT.OnlineDatas.GetUrls("afuwin"), UT.utpath + "\\Unowhy Tools\\Temps\\AFUWin.zip", progress, cancellationToken.Token);
                    await UT.waitstatus.open(await UT.GetLang("wait.extract"), "zip.png");
                    await Task.Delay(1000);
                    await Task.Run(() =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            ZipFile.ExtractToDirectory(UT.utpath + "\\Unowhy Tools\\Temps\\AFUWin.zip", UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU", true);
                        });
                    });
                }
                else
                {
                    UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
                }
            }

            if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\ChangeLogoWin64.exe") && File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "AFUWINx64.exe") && File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\" + "amigendrv64.sys"))
            {
                await UT.waitstatus.open(await UT.GetLang("wait.dump"), "upload.png");
                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\dump.rom"))
                {
                    File.Delete(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\dump.rom");
                }
                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\splash.*"))
                {
                    File.Delete(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\splash.*");
                }
                await Task.Run(() =>
                {
                    Process p = new Process();
                    p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU\\AFUWINx64.exe";
                    p.StartInfo.Arguments = $"\"{UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\dump.rom"}\" /O";
                    p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\AFU";
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    p.WaitForExit();
                });
                if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\dump.rom"))
                {
                    await UT.waitstatus.open(await UT.GetLang("wait.extract"), "upload.png");
                    await Task.Run(() =>
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\ChangeLogoWin64.exe";
                        p.StartInfo.Arguments = "/i dump.rom /e splash";
                        p.StartInfo.WorkingDirectory = UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo";
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        p.WaitForExit();
                    });
                    if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\splash.bmp"))
                    {
                        BitmapImage preimage = new BitmapImage(new Uri(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\splash.bmp"));
                        await UpdatePreview(preimage);
                        ImageSource = ResizeImage(preimage);
                        await UT.waitstatus.close();
                    }
                    else if (File.Exists(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\splash.jpg"))
                    {
                        BitmapImage preimage = new BitmapImage(new Uri(UT.utpath + "\\Unowhy Tools\\Temps\\AMI\\ChangeLogo\\splash.jpg"));
                        await UpdatePreview(preimage);
                        ImageSource = ResizeImage(preimage);
                        await UT.waitstatus.close();
                    }
                    else
                    {
                        await UT.waitstatus.close();
                        UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                    }
                }
                else
                {
                    await UT.waitstatus.close();
                    UT.DialogIShow(await UT.GetLang("failed"), "no.png");
                }
            }
            else
            {
                UT.DialogIShow(await UT.GetLang("failed"), "no.png");
            }
        }
    }
}
