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
using System.IO.Pipes;

namespace Unowhy_Tools_Service
{
    internal class Main : ServiceBase
    {
        //public bool ActiveWifiSync;
        public string Serial;
        public string WifiStatus;
        public string WDStatus;
        private NamedPipeServerStream _uts;
        private NamedPipeServerStream _utsw;
        private NamedPipeServerStream _utswd;
        private DispatcherTimer _wifitimer;
        private DispatcherTimer _wdtimer;
        public string Version = "2.1";

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
            _wifitimer = new DispatcherTimer();
            _wifitimer.Interval = TimeSpan.FromSeconds(60);
            _wifitimer.Tick += async (sender, e) => await WifiSync();
            _wifitimer.Start();

            _wdtimer = new DispatcherTimer();
            _wdtimer.Interval = TimeSpan.FromSeconds(60);
            _wdtimer.Tick += async (sender, e) => await WifiSync();
            _wdtimer.Start();

            Task.Run(() => WifiSync());
            Task.Run(() => UTSwait());
            Task.Run(() => UTSWwait());
            Task.Run(() => UTSWDwait());
        }

        public async Task WDDisable()
        {
            WDStatus = "False";

            if (File.Exists("C:\\UTSConfig\\disablewd.txt"))
            {
                WDStatus = File.ReadAllText("C:\\UTSConfig\\disablewd.txt");
            }

            if (WDStatus.Contains("True"))
            {
                Process reg = new Process();
                reg.StartInfo.FileName = "reg";
                reg.StartInfo.Arguments = "add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v \"DisableAntiSpyware\" /t REG_DWORD /d \"1\" /f";
                reg.StartInfo.CreateNoWindow = true;
                reg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                reg.Start();
            }
        }

        public async Task WifiSync()
        {
            WifiStatus = "True";
            Serial = "Null";

            if (File.Exists("C:\\UTSConfig\\wifisync.txt"))
            {
                WifiStatus = File.ReadAllText("C:\\UTSConfig\\wifisync.txt");
            }

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
                    Serial = File.ReadAllText("C:\\UTSConfig\\serial.txt");

                    if (WifiStatus.Contains("True"))
                    {
                        if (CheckInternet())
                        {
                            var web = new HttpClient();
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
                        else
                        {
                            return;
                        }
                    }
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

        public async Task SetWS(string status)
        {
            File.Delete("C:\\UTSConfig\\wifisync.txt");
            File.WriteAllText("C:\\UTSConfig\\wifisync.txt", status);
            WifiStatus = File.ReadAllText("C:\\UTSConfig\\wifisync.txt");
            Task.Run(() => WifiSync());
        }

        public async Task SetWDS(string status)
        {
            File.Delete("C:\\UTSConfig\\disablewd.txt");
            File.WriteAllText("C:\\UTSConfig\\disablewd.txt", status);
            WDStatus = File.ReadAllText("C:\\UTSConfig\\disablewd.txt");
            Task.Run(() => WDDisable());
        }

        public async Task SetSerial(string serial)
        {
            File.Delete("C:\\UTSConfig\\serial.txt");
            File.WriteAllText("C:\\UTSConfig\\serial.txt", serial);
            serial = File.ReadAllText("C:\\UTSConfig\\serial.txt");
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

                            if (clientData.Contains("GetWS"))
                            {
                                ret = WifiStatus;
                            }

                            if (clientData.Contains("SetSN:"))
                            {
                                string newsn = clientData.Replace("SetSN:", "");
                                await SetSerial(newsn);
                                ret = Serial;
                            }

                            if (clientData.Contains("SetWS:"))
                            {
                                string newws = clientData.Replace("SetWS:", "");
                                await SetWS(newws);
                                ret = WifiStatus;
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

        private async Task UTSWDwait()
        {
            while (true)
            {
                _utswd = new NamedPipeServerStream("UTSWD", PipeDirection.InOut);

                try
                {
                    await _utswd.WaitForConnectionAsync();

                    using (StreamReader reader = new StreamReader(_utswd))
                    using (StreamWriter writer = new StreamWriter(_utswd))
                    {
                        while (true)
                        {
                            string ret = "null";
                            string clientData = await reader.ReadLineAsync();

                            if (string.IsNullOrEmpty(clientData))
                            {
                                break;
                            }

                            if (clientData.Contains("GetWDS"))
                            {
                                ret = WDStatus;
                            }

                            if (clientData.Contains("SetWDS:"))
                            {
                                string newws = clientData.Replace("SetWDS:", "");
                                await SetWDS(newws);
                                ret = WDStatus;
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
            _uts?.Dispose();
            _utsw?.Dispose();
            _utswd?.Dispose();
        }
    }
}
