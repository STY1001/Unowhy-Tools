using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Unowhy_Tools
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists("tversion.txt"))    //Check if the file exist
            {
                File.Delete("tversion.txt");    //Delete it if exist
            }

            using (var client = new WebClient())
            {
                client.DownloadFile("https://raw.githubusercontent.com/STY1001/Unowhy-Tools/master/Update/Version.txt", ".\\tversion.txt");     //Download Version file
            }

            string gitver = System.IO.File.ReadAllText(".\\tversion.txt");      //Convert path to string
            string progver = System.IO.File.ReadAllText(".\\version.txt");

            int gitint = Convert.ToInt32(gitver);       //Convert string to int
            int progint = Convert.ToInt32(progver);

            if(progint < gitint)        //Check if there is a new vertion of UT
            {
                var nv = new newver();  
                nv.Show();
            }
            else
            {
                var nov = new nonew();
                nov.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://sty1001.wordpress.com");  //Link button
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/STY1001/Unowhy-Tools");    //Link button
        }
    }
}
