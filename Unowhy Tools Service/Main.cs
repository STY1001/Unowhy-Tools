using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Threading;

namespace Unowhy_Tools_Service
{
    internal class Main : ServiceBase
    {
        //public bool ActiveWifiSync;
        public string Serial;
        //private NamedPipeServerStream _uts;
        //private NamedPipeServerStream _utsw;
        private DispatcherTimer _timer;
        public string Version = "2.0";

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int state, int value);

        public bool CheckInternet()
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true) return true;
            else return false;
        }

        public Main()
        {

        }

        protected override void OnStart(string[] args)
        {
            if (File.Exists("C:\\UTSConfig\\version.txt"))
            {
                File.Delete("C:\\UTSConfig\\version.txt");
            }
            File.WriteAllText("C:\\UTSConfig\\version.txt", Version);

            //ActiveWifiSync = true;
            Task.Run(() => WifiSync());
            //Task.Run(() => UTSwait());
            //Task.Run(() => UTSWwait());

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(60);
            _timer.Tick += async (sender, e) => await WifiSync();
            _timer.Start();
        }

        public async Task WifiSync()
        {
            string wifistatus = "True";
            Serial = "Null";

            if (File.Exists("C:\\UTSConfig\\wifisync.txt"))
            {
                wifistatus = File.ReadAllText("C:\\UTSConfig\\wifisync.txt");
            }

            if(wifistatus.Contains("True"))
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
                            Serial = File.ReadAllText("C:\\UTSConfig\\serial.txt");
                            string preurl = "https://storage.gra.cloud.ovh.net/v1/AUTH_765727b4bb3a465fa4e277aef1356869/idfconf"; //"https://idf.hisqool.com/conf";
                            string configurl = $"{preurl}/devices/{Serial}/configuration";

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

                                            if (SecurityType == "open")
                                            {
                                                SecurityType = "open";
                                                EncryptionType = "none";
                                                OneX = "false";
                                            }
                                            else if (SecurityType == "WPA/WPA2 PSK")
                                            {
                                                SecurityType = "WPA2PSK";
                                                EncryptionType = "AES";
                                                OneX = "false";
                                            }
                                            else if (SecurityType == "WEP")
                                            {
                                                SecurityType = "open";
                                                EncryptionType = "WEP";
                                                OneX = "false";
                                            }
                                            else if (SecurityType == "802.1x EAP")
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

            string fileName = $@"C:\UTSConfig\WifiXml\{ssid}.xml";
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
        
        /*
        public async Task SetSerial(string serial)
        {
            ActiveWifiSync = false;
            File.Delete("C:\\UTSConfig\\serial.txt");
            File.WriteAllText("C:\\UTSConfig\\serial.txt", serial);
            serial = File.ReadAllText("C:\\UTSConfig\\serial.txt");
            ActiveWifiSync = true;
            Task.Run(() => WifiSync());
        }
        private async Task UTSwait()
        {
            while (true)
            {
                _uts = new NamedPipeServerStream("UTS", PipeDirection.InOut);

                try
                {
                    await _uts.WaitForConnectionAsync();

                    using (StreamReader reader = new StreamReader(_uts))
                    using (StreamWriter writer = new StreamWriter(_uts))
                    {
                        while (true)
                        {
                            string clientData = await reader.ReadLineAsync();

                            if (string.IsNullOrEmpty(clientData))
                            {
                                break;
                            }
                            string ret = "null";

                            if (clientData == "GetVer")
                            {
                                ret = Version;
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

        private async Task UTSWwait()
        {
            while (true)
            {
                _utsw = new NamedPipeServerStream("UTSW", PipeDirection.InOut);

                try
                {
                    await _utsw.WaitForConnectionAsync();

                    using (StreamReader reader = new StreamReader(_utsw))
                    using (StreamWriter writer = new StreamWriter(_utsw))
                    {
                        while (true)
                        {
                            string ret = "null";
                            string clientData = await reader.ReadLineAsync();

                            if (string.IsNullOrEmpty(clientData))
                            {
                                break;
                            }

                            if (clientData.Contains("GetSN"))
                            {
                                ret = Serial;
                            }

                            if (clientData.Contains("SetSN:"))
                            {
                                string newsn = clientData.Replace("SetSN:", "");
                                await SetSerial(newsn);
                                ret = Serial;
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
        */
        protected override void OnStop()
        {
            //_uts?.Dispose();
            //_utsw?.Dispose();
            //ActiveWifiSync = false;
        }
    }
}
