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
        public bool ActiveWifiSync;
        Unowhy_Tools_Service.Main.Data Data = new Unowhy_Tools_Service.Main.Data();

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

        }

        private NamedPipeServerStream _pipeServer;

        protected override void OnStart(string[] args)
        {
            ActiveWifiSync = true;
            Task.Run(() => WifiSync());
            Task.Run(() => WaitForClientAsync());
        }

        public async Task WifiSync()
        {
            Data.serial = "Null";
            while (ActiveWifiSync == true)
            {
                if (CheckInternet())
                {
                    if (!File.Exists("C:\\UTSConfig\\serial.txt"))
                    {
                        return;
                    }
                    else
                    {
                        if (File.ReadAllText("C:\\UTSConfig\\serial.txt") == "Null")
                        {
                            return;
                        }
                        else
                        {
                            var web = new HttpClient();
                            Data.serial = File.ReadAllText("C:\\UTSConfig\\serial.txt");
                            string configurl = $"https://idf.hisqool.com/conf/devices/{Data.serial}/configuration";

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
                                            string OneX;
                                            string ProxyType;
                                            string ProxyAddressManual;
                                            string ProxyPortManual;
                                            string ProxyUrlAutomatic;

                                            SSID = options["ssid"].ToString();
                                            Password = options["password"].ToString();
                                            SecurityType = options["securityType"].ToString();
                                            ProxyType = proxy["type"].ToString();
                                            EncryptionType = null;
                                            OneX = null;

                                            if(SecurityType == "open")
                                            {
                                                SecurityType = "open";
                                                EncryptionType = "none";
                                                OneX = "false";
                                            }
                                            else if(SecurityType == "WPA/WPA2 PSK")
                                            {
                                                SecurityType = "WPA2PSK";
                                                EncryptionType = "AES";
                                                OneX = "false";
                                            }
                                            else if(SecurityType == "WEP")
                                            {
                                                SecurityType = "open";
                                                EncryptionType = "WEP";
                                                OneX = "false";
                                            }
                                            else if(SecurityType == "802.1x EAP")
                                            {
                                                SecurityType = "open";
                                                EncryptionType = "WEP";
                                                OneX = "true";
                                            }

                                            if (ProxyType == "none")
                                            {
                                                await AddWifi(SSID, Password, SecurityType, EncryptionType, OneX);
                                            }
                                            else if (ProxyType == "manual")
                                            {
                                                ProxyAddressManual = proxy["proxyHostName"].ToString();
                                                ProxyPortManual = proxy["proxyPort"].ToString();
                                                await AddWifiProxyManual(SSID, Password, SecurityType, EncryptionType, OneX, ProxyAddressManual, ProxyPortManual);
                                            }
                                            else if (ProxyType == "automatic")
                                            {
                                                ProxyUrlAutomatic = proxy["autoProxyUrl"].ToString();
                                                await AddWifiProxyAuto(SSID, Password, SecurityType, EncryptionType, OneX, ProxyUrlAutomatic);
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
        
        public async Task AddWifi(string ssid, string password, string securityType, string encType, string oneX)
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
                        <authEncryption>
                            <authentication>{securityType}</authentication>
                            <encryption>{encType}</encryption>
                            <useOneX>{oneX}</useOneX>
                        </authEncryption>
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

        public async Task AddWifiProxyManual(string ssid, string password, string securityType, string encType, string oneX, string proxyAddress, string proxyPort)
        {
            await AddWifi(ssid, password, securityType, encType, oneX);
            string arguments = $@"winhttp set proxy {proxyAddress}:{proxyPort}";
            await RunNetshCommand(arguments);
        }

        public async Task AddWifiProxyAuto(string ssid, string password, string securityType, string encType, string oneX, string proxyUrl)
        {
            await AddWifi(ssid, password, securityType, encType, oneX);
            string arguments = $@"winhttp set proxy autoconfigurl={proxyUrl}";
            await RunNetshCommand(arguments);
        }

        public async Task RunNetshCommand(string arguments)
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

        public async Task SetSerial(string serial)
        {
            ActiveWifiSync = false;
            File.Delete("C:\\UTSConfig\\serial.txt");
            File.WriteAllText("C:\\UTSConfig\\serial.txt", serial);
            serial = File.ReadAllText("C:\\UTSConfig\\serial.txt");
            ActiveWifiSync = true;
            Task.Run(() => WifiSync());
        }

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
                            string ret = "null";
                            string clientData = await reader.ReadLineAsync();

                            if (string.IsNullOrEmpty(clientData))
                            {
                                break;
                            }                            

                            if(clientData.Contains("GetSN"))
                            {
                                ret = Data.serial;
                            }

                            if(clientData.Contains("SetSN:"))
                            {
                                string newsn = clientData.Replace("SetSN:", "");
                                await SetSerial(newsn);
                                ret = Data.serial;
                            }

                            await writer.WriteLineAsync(ret);
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
            ActiveWifiSync = false;
            _pipeServer?.Dispose();
        }
    }
}
