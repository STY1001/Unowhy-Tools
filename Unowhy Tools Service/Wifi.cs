using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.SqlServer.Server;

namespace Unowhy_Tools_Service
{
    internal class Wifi : ServiceBase
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int state, int value);

        public bool CheckInternet()
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true) return true;
            else return false;
        }

        public Wifi() 
        {
            //ServiceName = "UTSW";
        }

        private NamedPipeServerStream _pipeServer;

        protected override void OnStart(string[] args)
        {
            Task.Run(() => WifiSync());
            Task.Run(() => WaitForClientAsync());
        }

        public async Task WifiSync()
        {
            while (true)
            {
                if (CheckInternet())
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\STY1001\Unowhy Tools", true);
                    if (key == null)
                    {
                        return;
                    }
                    else
                    {
                        object d = key.GetValue("Device", null);
                        if (d == null)
                        {
                            return;
                        }
                        else
                        {
                            if (key.GetValue("Device") == "Null")
                            {
                                return;
                            }
                            else
                            {

                                var web = new HttpClient();
                                string sn = key.GetValue("Device").ToString();
                                string configurl = $"https://idf.hisqool.com/conf/devices/{sn}/configuration";

                                HttpResponseMessage response = await web.GetAsync(configurl);
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
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

                                    foreach (JProperty property in mergedJson.Properties())
                                    {
                                        if (property.Name.StartsWith("conf/network/all/"))
                                        {
                                            JToken payload = property.Value["payload"];
                                            JToken options = property.Value["options"];
                                            JToken proxy = options["proxy"];
                                            if (payload != null && options != null && proxy != null)
                                            {
                                                string SSID;
                                                string Password;
                                                string SecurityType;
                                                string EncryptionType;
                                                string ProxyType;
                                                string ProxyAddressManual;
                                                string ProxyPortManual;
                                                string ProxyUrlAutomatic;

                                                SSID = options["ssid"].ToString();
                                                Password = options["password"].ToString();
                                                SecurityType = options["securityType"].ToString();
                                                ProxyType = proxy["type"].ToString();
                                                EncryptionType = null;

                                                if(SecurityType == "open")
                                                {
                                                    SecurityType = "open";
                                                    EncryptionType = "none";
                                                }
                                                else if(SecurityType == "WPA/WPA2 PSK")
                                                {
                                                    SecurityType = "WPA2PSK";
                                                    EncryptionType = "AES";
                                                }
                                                else if(SecurityType == "WEP")
                                                {
                                                    SecurityType = "open";
                                                    EncryptionType = "WEP";
                                                }
                                                else if(SecurityType == "802.1x EAP")
                                                {
                                                    SecurityType = "WPA2Enterprise";
                                                    EncryptionType = "AES";
                                                }

                                                if (ProxyType == "none")
                                                {
                                                    await AddWifi(SSID, Password, SecurityType, EncryptionType);
                                                }
                                                else if (ProxyType == "manual")
                                                {
                                                    ProxyAddressManual = proxy["proxyHostName"].ToString();
                                                    ProxyPortManual = proxy["proxyPort"].ToString();
                                                    await AddWifiProxyManual(SSID, Password, SecurityType, EncryptionType, ProxyAddressManual, ProxyPortManual);
                                                }
                                                else if (ProxyType == "automatic")
                                                {
                                                    ProxyUrlAutomatic = proxy["autoProxyUrl"].ToString();
                                                    await AddWifiProxyAuto(SSID, Password, SecurityType, EncryptionType, ProxyUrlAutomatic);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return;
                }

                await Task.Delay(300000);
            }
        }

        public static async Task AddWifi(string ssid, string password, string securityType, string encType)
        {
            await Task.Run(() =>
            {
                Process psi = new Process();
                psi.StartInfo.FileName = "netsh";
                psi.StartInfo.Arguments = $"wlan add profile filename=\"C:\\{ssid}.xml\"";
                psi.StartInfo.Arguments += $"ssid=\"{ssid}\" keyMaterial=\"{password}\" authentication=\"{securityType}\" encryption=\"{encType}\"";
                psi.Start();
                psi.WaitForExit();
            });
        }

        public static async Task AddWifiProxyManual(string ssid, string password, string securityType, string encType, string proxyAddress, string proxyPort)
        {
            await Task.Run(() =>
            {
                Process psi = new Process();
                psi.StartInfo.FileName = "netsh";
                psi.StartInfo.Arguments = $"wlan add profile filename=\"C:\\{ssid}.xml\"";
                psi.StartInfo.Arguments += $"ssid=\"{ssid}\" keyMaterial=\"{password}\" authentication=\"{securityType}\" encryption=\"{encType}\"";
                psi.StartInfo.Arguments += $"connectiontype=ibss connectionmode=autoMacAddr proxySettings=manual";
                psi.StartInfo.Arguments += $"manualproxy={proxyAddress}:{proxyPort}";
                psi.Start();
                psi.WaitForExit();
            });
            
        }

        public static async Task AddWifiProxyAuto(string ssid, string password, string securityType, string encType, string proxyUrl)
        {
            await Task.Run(() =>
            {
                Process psi = new Process();
                psi.StartInfo.FileName = "netsh";
                psi.StartInfo.Arguments = $"wlan add profile filename=\"C:\\{ssid}.xml\"";
                psi.StartInfo.Arguments += $"ssid=\"{ssid}\" keyMaterial=\"{password}\" authentication=\"{securityType}\" encryption=\"{encType}\"";
                psi.StartInfo.Arguments += $"connectiontype=ibss connectionmode=autoMacAddr proxySettings=autoconfig";
                psi.StartInfo.Arguments += $"autoconfigurl={proxyUrl}";
                psi.Start();
                psi.WaitForExit();
            });
        }

        /*<authEncryption>
                    <authentication>{securityType}</authentication>
                    <encryption>AES</encryption>
                    <useOneX>false</useOneX>
                </authEncryption>*/
        /*
        public static async Task AddWifi(string ssid, string password, string securityType)
        {
            string profileXml = $@"<?xml version=""1.0""?>
            <WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">
                <name>{ssid}</name>
                <SSIDConfig>
                    <SSID>
                        <name>{ssid}</name>
                    </SSID>
                </SSIDConfig>
                <connectionType>ESS</connectionType>
                <connectionMode>auto</connectionMode>
                <MSM>
                    <security>
                        <sharedKey>
                            <keyType>passPhrase</keyType>
                            <protected>false</protected>
                            <keyMaterial>{password}</keyMaterial>
                        </sharedKey>
                    </security>
                </MSM>
            </WLANProfile>";

            string fileName = $@"C:\{ssid}.xml";
            System.IO.File.WriteAllText(fileName, profileXml);

            string arguments = $@"wlan add profile filename=""{fileName}""";
            await RunNetshCommand(arguments);
        }

        public static async Task AddWifiProxyManual(string ssid, string password, string securityType, string proxyAddress, string proxyPort)
        {
            await AddWifi(ssid, password, securityType);
            string arguments = $@"winhttp set proxy {proxyAddress}:{proxyPort}";
            await RunNetshCommand(arguments);
        }

        public static async Task AddWifiProxyAuto(string ssid, string password, string securityType, string proxyUrl)
        {
            await AddWifi(ssid, password, securityType);
            string arguments = $@"winhttp set proxy autoconfigurl={proxyUrl}";
            await RunNetshCommand(arguments);
        }

        public static async Task RunNetshCommand(string arguments)
        {
            await Task.Run(() =>
            {
                Process p = new Process();
                p.StartInfo.FileName = "netsh.exe";
                p.StartInfo.Arguments = arguments;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.WaitForExit();
            });
        }
        */

        private async Task WaitForClientAsync()
        {
            while (true)
            {
                _pipeServer = new NamedPipeServerStream("UTSW", PipeDirection.InOut);

                try
                {
                    await _pipeServer.WaitForConnectionAsync();

                    using (StreamReader reader = new StreamReader(_pipeServer))
                    using (StreamWriter writer = new StreamWriter(_pipeServer))
                    {
                        while (true)
                        {
                            string clientData = await reader.ReadLineAsync();

                            if (string.IsNullOrEmpty(clientData))
                            {
                                break;
                            }

                            await writer.WriteLineAsync("You said: " + clientData);
                            await writer.FlushAsync();
                        }
                    }
                }
                catch (IOException)
                {

                }
            }
        }

        protected override void OnStop()
        {
            _pipeServer?.Dispose();
        }
    }
}
