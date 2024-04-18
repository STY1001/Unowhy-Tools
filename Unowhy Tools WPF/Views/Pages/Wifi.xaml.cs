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
using System.IO.Pipes;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security.Policy;
using Wpf.Ui.Styles.Controls;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Wifi : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void GoForw(object sender, RoutedEventArgs e)
    {
        //UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        //UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(Dashboard));
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
        string sn = await UT.UTS.UTSmsg("UTSW", "GetSN");
        if (!(sn == "Null") && sn.Contains("IFP"))
        {
            serial.Text = sn;
        }
        ObservableCollection<DataRow> dataRows = new ObservableCollection<DataRow>();

        DataRow prerow = new DataRow();
        prerow.SSID = "SSID   ";
        prerow.Password = "Password   ";
        prerow.SecurityType = "Security Type   ";
        prerow.Hidden = "Hidden ?   ";
        prerow.ProxyType = "Proxy Type   ";
        prerow.ProxyAddressManual = "Proxy IP (Manual)   ";
        prerow.ProxyPortManual = "Proxy Port (Manual)   ";
        prerow.ProxyUrlAutomatic = "Proxy URL (Automatic)   ";
        dataRows.Add(prerow);
        Board.ItemsSource = dataRows;
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        foreach (UIElement element in RootGrid2.Children)
        {
            element.Visibility = Visibility.Hidden;
        }

        await UT.DeployBack(typeof(Dashboard), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);

        foreach (UIElement element in RootGrid2.Children)
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

    public async Task applylang()
    {
        labsn.Text = await UT.GetLang("pcserial");
    }

    public Wifi(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    /*public async void Get_Click(object sender, RoutedEventArgs e)
    {
        WebClient wc = new WebClient();
        string sn = serial.Text;
        string g = wc.DownloadString($"https://idf.hisqool.com/conf/devices/{sn}/configuration");
        string jsonString = g;
        List<dynamic> dataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);

        List<string> urlList = new List<string>();

        foreach (var data in dataList)
        {
            string url = data.url;
            urlList.Add(url);
        }

        List<string> jsonResponseList = new List<string>();

        JObject mergedJson = new JObject();

        foreach (var url in urlList)
        {
            var client = new WebClient();
            JObject json = JObject.Parse(client.DownloadString(url));
            mergedJson.Merge(json);
        }



        DataTable dt = new DataTable();
        dt.Columns.Add("SSID", typeof(string));
        dt.Columns.Add("Password", typeof(string));
        dt.Columns.Add("Security Type", typeof(string));
        dt.Columns.Add("Hidden", typeof(string));
        dt.Columns.Add("Proxy Type", typeof(string));
        dt.Columns.Add("Proxy URL (Automatic)", typeof(string));
        dt.Columns.Add("Proxy Address (Manual)", typeof(string));
        dt.Columns.Add("Proxy Port (Manual)", typeof(string));

        foreach (JProperty property in mergedJson.Properties())
        {
            if (property.Name.StartsWith("conf/network/all/"))
            {
                DataRow row = dt.NewRow();
                JToken payload = property.Value["payload"];
                JToken options = property.Value["options"];
                JToken proxy = options["proxy"];
                if (payload != null && options != null && proxy != null)
                {
                    Console.WriteLine("SSID: " + options["ssid"]);
                    Console.WriteLine("Password: " + options["password"]);
                    Console.WriteLine("Security: " + options["securityType"]);
                    Console.WriteLine("Hidden: " + options["hidden"]);
                    Console.WriteLine("Proxy: " + proxy["type"]);

                    row["SSID"] = options["ssid"];
                    row["Password"] = options["password"];
                    row["Security Type"] = options["securityType"];
                    row["Hidden"] = options["hidden"];
                    row["Proxy Type"] = proxy["type"];

                    if (proxy["type"].ToString() == "none")
                    {
                        Console.WriteLine("No proxy");
                    }
                    else if (proxy["type"].ToString() == "manual")
                    {
                        Console.WriteLine("Manual Proxy");
                        Console.WriteLine("Port: " + proxy["proxyPort"]);
                        Console.WriteLine("IP: " + proxy["proxyHostName"]);
                        row["Proxy Address (Manual)"] = proxy["proxyHostName"];
                        row["Proxy Port (Manual)"] = proxy["proxyPort"];
                    }
                    else if (proxy["type"].ToString() == "automatic")
                    {
                        Console.WriteLine("Auto Proxy");
                        Console.WriteLine("URL: " + proxy["autoProxyUrl"]);
                        row["Proxy URL (Automatic)"] = proxy["autoProxyUrl"];
                    }

                    dt.Rows.Add(row);
                }
            }
        }

        Board.DataContext = dt;
    }*/

    public async Task Get()
    {
        UT.SendAction("UTW.Get");
        if (UT.CheckInternet())
        {
            await UT.waitstatus.open(await UT.GetLang("wait.get"), "clouddl.png");
            await Task.Delay(1000);

            var web = new HttpClient();
            string sn = serial.Text;
            string preurl = await UT.OnlineDatas.GetUrls("idfconf");
            string configurl = $"{preurl}/devices/{sn}/configuration";

            HttpResponseMessage response = await web.GetAsync(configurl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                DoubleAnimation translateAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1000,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseIn, Power = 5 }
                };
                TranslateTransform transform = new TranslateTransform();
                Board.RenderTransform = transform;
                StackClip.Children.Clear();
                transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
                await Task.Delay(500);

                string g = await web.GetStringAsync(configurl);
                string jsonString = g;
                List<dynamic> dataList = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);

                List<string> urlList = new List<string>();

                foreach (var data in dataList)
                {
                    string url = data.url;
                    urlList.Add(url);
                }

                List<string> jsonResponseList = new List<string>();

                JObject mergedJson = new JObject();

                foreach (var url in urlList)
                {
                    JObject json = JObject.Parse(await web.GetStringAsync(url));
                    mergedJson.Merge(json);
                }

                ObservableCollection<DataRow> dataRows = new ObservableCollection<DataRow>();

                DataRow prerow = new DataRow();
                prerow.SSID = "SSID   ";
                prerow.Password = "Password   ";
                prerow.SecurityType = "Security Type   ";
                prerow.Hidden = "Hidden ?   ";
                prerow.ProxyType = "Proxy Type   ";
                prerow.ProxyAddressManual = "Proxy IP (Manual)   ";
                prerow.ProxyPortManual = "Proxy Port (Manual)   ";
                prerow.ProxyUrlAutomatic = "Proxy URL (Automatic)   ";
                dataRows.Add(prerow);

                Image PreClipImage = new Image();
                PreClipImage.Source = UT.GetImgSource("clipboard.png");
                PreClipImage.Visibility = Visibility.Hidden;
                StackClip.Children.Add(PreClipImage);

                foreach (JProperty property in mergedJson.Properties())
                {
                    if (property.Name.StartsWith("conf/network/all/"))
                    {
                        JToken payload = property.Value["payload"];
                        JToken options = property.Value["options"];
                        JToken proxy = options["proxy"];
                        if (payload != null && options != null && proxy != null)
                        {
                            DataRow row = new DataRow();

                            row.SSID = (string)options["ssid"] + "   ";
                            row.Password = (string)options["password"] + "   ";
                            row.SecurityType = (string)options["securityType"] + "   ";
                            if (options["hidden"] != null)
                            {
                                row.Hidden = (string)options["hidden"] + "   ";
                            }
                            else
                            {
                                row.Hidden = "Null" + "   ";
                            }

                            row.ProxyType = proxy["type"] + "   ";

                            if ((string)proxy["type"] == "none")
                            {

                            }
                            else if ((string)proxy["type"] == "manual")
                            {
                                row.ProxyAddressManual = (string)proxy["proxyHostName"] + "   ";
                                row.ProxyPortManual = (string)proxy["proxyPort"] + "   ";
                            }
                            else if ((string)proxy["type"] == "automatic")
                            {
                                row.ProxyUrlAutomatic = (string)proxy["autoProxyUrl"] + "   ";
                            }

                            dataRows.Add(row);

                            Image ClipImage = new Image();
                            ClipImage.Source = UT.GetImgSource("clipboard.png");
                            ClipImage.MouseDown += async (sender, e) =>
                            {
                                await ClipMenuOpen(sender, e, (string)options["ssid"], (string)options["password"], (string)proxy["type"], (string)proxy["proxyHostName"], (string)proxy["proxyPort"], (string)proxy["autoProxyUrl"]);
                            };

                            StackClip.Children.Add(ClipImage);
                        }
                    }
                }
                Board.ItemsSource = dataRows;
                await UT.waitstatus.close();

                await Task.Delay(500);
                translateAnimation = new DoubleAnimation
                {
                    From = -1000,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut, Power = 5 }
                };
                transform = new TranslateTransform();
                Board.RenderTransform = transform;
                StackClip.RenderTransform = transform;
                transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
            }
            else
            {
                await UT.waitstatus.close();
                UT.DialogIShow(await UT.GetLang("noid"), "no.png");
            }            
        }
        else
        {
            UT.DialogIShow(await UT.GetLang("nonet"), "nowifi.png");
        }
    }

    public class DataRow
    {
        public string SSID { get; set; }
        public string Password { get; set; }
        public string SecurityType { get; set; }
        public string Hidden { get; set; }
        public string ProxyType { get; set; }
        public string ProxyAddressManual { get; set; }
        public string ProxyPortManual { get; set; }
        public string ProxyUrlAutomatic { get; set; }
    }

    public async Task ClipMenuOpen(object sender, RoutedEventArgs e, string ssid,  string password, string proxytype, string proxyip, string proxyport, string proxyurl)
    {
        UT.SendAction("UTW.OpenClipMenu");
        await Task.Delay(100);
        System.Windows.Controls.ContextMenu ClipMenu = new System.Windows.Controls.ContextMenu();
        MenuItem SSIDItem = new MenuItem();
        SSIDItem.Header = "Copy SSID";
        SSIDItem.Click += (sender, e) =>
        {
            System.Windows.Forms.Clipboard.SetText(ssid);
        };
        MenuItem PasswordItem = new MenuItem();
        PasswordItem.Header = "Copy Password";
        PasswordItem.Click += (sender, e) =>
        {
            System.Windows.Forms.Clipboard.SetText(password);
        };

        ClipMenu.Items.Add(SSIDItem);
        ClipMenu.Items.Add(PasswordItem);

        if (proxytype == "none")
        {
            MenuItem ProxyIPItem = new MenuItem();
            ProxyIPItem.Header = "Copy Proxy IP";
            ProxyIPItem.IsEnabled = false;
            ClipMenu.Items.Add(ProxyIPItem);
            MenuItem ProxyPortItem = new MenuItem();
            ProxyPortItem.Header = "Copy Proxy Port";
            ProxyPortItem.IsEnabled = false;
            ClipMenu.Items.Add(ProxyPortItem);
            MenuItem ProxyURLItem = new MenuItem();
            ProxyURLItem.Header = "Copy Proxy URL";
            ProxyURLItem.IsEnabled = false;
            ClipMenu.Items.Add(ProxyURLItem);
        }
        else if (proxytype == "manual")
        {
            MenuItem ProxyIPItem = new MenuItem();
            ProxyIPItem.Header = "Copy Proxy IP";
            ProxyIPItem.Click += (sender, e) =>
            {
                System.Windows.Forms.Clipboard.SetText(proxyip);
            };
            ClipMenu.Items.Add(ProxyIPItem);
            MenuItem ProxyPortItem = new MenuItem();
            ProxyPortItem.Header = "Copy Proxy Port";
            ProxyPortItem.Click += (sender, e) =>
            {
                System.Windows.Forms.Clipboard.SetText(proxyport);
            };
            ClipMenu.Items.Add(ProxyPortItem);
            MenuItem ProxyURLItem = new MenuItem();
            ProxyURLItem.Header = "Copy Proxy URL";
            ProxyURLItem.IsEnabled = false;
            ClipMenu.Items.Add(ProxyURLItem);
        }
        else if (proxytype == "automatic")
        {
            MenuItem ProxyIPItem = new MenuItem();
            ProxyIPItem.Header = "Copy Proxy IP";
            ProxyIPItem.IsEnabled = false;
            ClipMenu.Items.Add(ProxyIPItem);
            MenuItem ProxyPortItem = new MenuItem();
            ProxyPortItem.Header = "Copy Proxy Port";
            ProxyPortItem.IsEnabled = false;
            ClipMenu.Items.Add(ProxyPortItem);
            MenuItem ProxyURLItem = new MenuItem();
            ProxyURLItem.Header = "Copy Proxy URL";
            ProxyURLItem.Click += (sender, e) =>
            {
                System.Windows.Forms.Clipboard.SetText(proxyurl);
            };
            ClipMenu.Items.Add(ProxyURLItem);
        }

        ClipMenu.PlacementTarget = (Image)sender;
        ClipMenu.IsOpen = true;
    }

    private async void serial_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if(e.Key == System.Windows.Input.Key.Enter)
        {
            await Get();
        }
    }

    private async void getbtn_Click(object sender, RoutedEventArgs e)
    {
        await Get();
    }

    private async void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        UT.SendAction("EasterEgg.UTW.QR");
        string eeapiurl = await UT.OnlineDatas.GetUrls("eeapi");
        eeapiurl = eeapiurl + "/qr/wifi";
        System.Diagnostics.Process.Start(new ProcessStartInfo
        {
            FileName = eeapiurl,
            UseShellExecute = true
        });
    }
}