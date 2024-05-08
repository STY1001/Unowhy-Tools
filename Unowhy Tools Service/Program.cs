using System;
using System.IO;
using System.ServiceProcess;

namespace Unowhy_Tools_Service
{
    internal class Program
    {
        string utspath = "C:\\Unowhy Tools\\Unowhy Tools Service";

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            using (var main = new Main())
            {
                ServiceBase.Run(main);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string exp = e.ExceptionObject.ToString();
            string date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            if(Directory.Exists("C:\\Unowhy Tools\\Unowhy Tools Service\\Logs\\Crash"))
            {
                File.WriteAllText($"C:\\Unowhy Tools\\Unowhy Tools Service\\Logs\\Crash\\crash_{date}.txt", exp);
            }
            else
            {
                Directory.CreateDirectory("C:\\Unowhy Tools\\Unowhy Tools Service\\Logs\\Crash");
                File.WriteAllText($"C:\\Unowhy Tools\\Unowhy Tools Service\\Logs\\Crash\\crash_{date}.txt", exp);
            }
        }
    }
}
