using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CommandLine;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace UTsplash
{
    public partial class Splash : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
        }

        public void delay(int Time_delay)
        {
            int i = 0;
            System.Timers.Timer _delayTimer = new System.Timers.Timer();
            _delayTimer.Interval = Time_delay;
            _delayTimer.AutoReset = false;
            _delayTimer.Elapsed += (s, args) => i = 1;
            _delayTimer.Start();
            while (i == 0) { };
        }

        public string returncmd(string file, string args)
        {
            IntPtr wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);
            Process get = new Process();
            get.StartInfo.FileName = file;
            get.StartInfo.Arguments = args;
            get.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            get.StartInfo.UseShellExecute = false;
            get.StartInfo.RedirectStandardOutput = true;
            get.StartInfo.CreateNoWindow = true;
            get.Start();
            get.WaitForExit();
            string output = get.StandardOutput.ReadToEnd();
            return output;
        }

        public class Options
        {
            [Option('v', "version", Required = false, HelpText = "Version to show")]
            public string Ver { get; set; }

            [Option('d', "debug", Required = false, HelpText = "Is Debug")]
            public bool Deb { get; set; }

            [Option('c', "crashdetect", Required = false, HelpText = "Detect if main app crashed")]
            public bool Cra { get; set; }
        }

        public void crash()
        {
            for (int i = 1; i > 0; i++)
            {
                string r = returncmd("powershell", "start-process -FilePath \"Tasklist\" -nonewwindow");
                if (r.Contains("Unowhy Tools.exe"))
                {
                    delay(3000);
                }
                else
                {
                    returncmd("taskill", "/f /im \"UTsplash.exe\"");
                }
            }
        }

        public Splash(string[] args)
        {
            InitializeComponent();
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.Ver == null)
                       {
                           label4.Text = "";
                       }
                       else
                       {
                           label4.Text = o.Ver;
                           if (o.Deb == true)
                           {
                               label4.Text = o.Ver + Environment.NewLine + "(Debug)";
                           }
                       }
                       if (o.Cra == true)
                       {
                           crash();
                       }
                   });
        }

        public void close()
        {
            this.Close();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
