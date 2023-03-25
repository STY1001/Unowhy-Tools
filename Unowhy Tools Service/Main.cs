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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Unowhy_Tools_Service
{
    internal class Main : ServiceBase
    {
        private NamedPipeServerStream _pipeServer;
        public string Version = "1.0";

        public Main()
        {
            Task.Run(() => WaitForClientAsync());
        }

        protected override void OnStart(string[] args)
        {
            Task.Run(() => WaitForClientAsync());
        }

        private async Task WaitForClientAsync()
        {
            while (true)
            {
                _pipeServer = new NamedPipeServerStream("UTS", PipeDirection.InOut);

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
                            string ret = "null";

                            if(clientData == "GetVer")
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

        protected override void OnStop()
        {
            _pipeServer?.Dispose();
        }

        public class Data : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private static string _serial;

            public string serial
            {
                get { return _serial; }
                set
                {
                    _serial = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
