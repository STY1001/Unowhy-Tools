using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using static System.Net.WebRequestMethods;
using mshtml;
using System.Runtime.ConstrainedExecution;

namespace Unowhy_Tools
{
    public partial class changelog : Form
    {
        //Set dark mode title bar
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        public int scrollTop = 0;

        protected override void OnHandleCreated(EventArgs e)
        {
            DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 35, new[] { 1 }, 4);
            DwmSetWindowAttribute(Handle, 38, new[] { 1 }, 4);
        }

        public changelog()
        {
            InitializeComponent();
            string path = Directory.GetCurrentDirectory();
            string urlp = "file://" + path + "\\clog.html";
            webclog.Navigate(urlp);
        }

        private void webup_Click(object sender, EventArgs e)
        {
            up();
        }

        private void webdown_Click(object sender, EventArgs e)
        {
            down();
        }

        private void up()
        {
            HtmlDocument htmlDoc = webclog.Document;
            int scrollLeft = htmlDoc.GetElementsByTagName("HTML")[0].ScrollLeft;
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop > 180 ? scrollTop - 10 : 0;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
        }

        private void down()
        {
            HtmlDocument htmlDoc = webclog.Document;
            int scrollLeft = htmlDoc.GetElementsByTagName("HTML")[0].ScrollLeft;
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
            scrollTop = scrollTop + 10;
            webclog.Document.Window.ScrollTo(new Point(scrollLeft, scrollTop));
        }
    }
}
