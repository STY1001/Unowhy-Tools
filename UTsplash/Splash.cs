using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using System.Threading;
using System.Windows.Input;
using CommandLine;

namespace UTsplash
{
    public partial class Splash : Form
    {

        //Set dark mode title bar

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
        }

        public class Options
        {
            [Option('v', "version", Required = false, HelpText = "Version to show")]
            public string Ver { get; set; }

            [Option('d', "debug", Required = false, HelpText = "Is Debug")]
            public bool Deb { get; set; }
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
