using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Unowhy_Tools
{
    public partial class Adduser : Form
    {
        string resxFile = "null";

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

        public Adduser()
        {
            //Check the current saved language

            RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
            string utls = utl.GetValue("Lang").ToString();

            string enresx = @".\en.resx";
            string frresx = @".\fr.resx";
            //Chose the ResX file
            if (utls == "EN") resxFile = enresx;    //English   
            else resxFile = frresx;               //French
            
            InitializeComponent();

            ResXResourceSet resxSet = new ResXResourceSet(resxFile);

            this.Text = resxSet.GetString("adduser");
            labn.Text = resxSet.GetString("name");
            labp.Text = resxSet.GetString("password");
            labc.Text = resxSet.GetString("confpw");
            admin.Text = resxSet.GetString("la");
        }

        private void Adduser_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ok_Click(object sender, EventArgs e)
        {
            string sname = name.Text;
            string spass = pass.Text;
            var w = new wait();

            Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_]");

            if (r.IsMatch(sname.ToString().Trim()) || sname == "")
            {
                //Check the current saved language

                RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
                string utls = utl.GetValue("Lang").ToString();

                string enresx = @".\en.resx";
                string frresx = @".\fr.resx";
                //Chose the ResX file
                if (utls == "EN") resxFile = enresx;    //English   
                else resxFile = frresx;                //French
                ResXResourceSet resxSet = new ResXResourceSet(resxFile);

                warn.Text = resxSet.GetString("an");
                name1.Image = Unowhy_Tools.Properties.Resources.deluser;
            }
            else
            {
                if (conf.Text == pass.Text)
                {
                    string msg = this.Text;
                    dialog d = new dialog(msg);
                    d.ShowDialog();
                    if (d.DialogResult.Equals(DialogResult.Yes))
                    {
                        w.Show();
                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);

                        if (pass.Text == "")
                        {
                            string arg = ($"user \"{sname}\" /add");
                            Process p = new Process();
                            p.StartInfo.FileName = "net";
                            p.StartInfo.Arguments = arg;
                            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            p.Start();
                            p.WaitForExit();

                            if (admin.Checked == true)
                            {
                                string arga = ($"localgroup Administrateurs \"{sname}\" /add");
                                Process a = new Process();
                                a.StartInfo.FileName = "net";
                                a.StartInfo.Arguments = arga;
                                a.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                                a.Start();
                                a.WaitForExit();
                            }
                        }
                        else
                        {
                            string arg = ($"user \"{sname}\" \"{spass}\" /add");
                            Process p = new Process();
                            p.StartInfo.FileName = "net";
                            p.StartInfo.Arguments = arg;
                            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            p.Start();
                            p.WaitForExit();

                            if (admin.Checked == true)
                            {
                                string arga = ($"localgroup Administrateurs \"{sname}\" /add");
                                Process a = new Process();
                                a.StartInfo.FileName = "net";
                                a.StartInfo.Arguments = arga;
                                a.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                                a.Start();
                                a.WaitForExit();
                            }
                        }

                        w.Close();
                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                        string username = sname;
                        var uidf = new userid(username);
                        uidf.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    //Check the current saved language

                    RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
                    string utls = utl.GetValue("Lang").ToString();

                    string enresx = @".\en.resx";
                    string frresx = @".\fr.resx";
                    //Chose the ResX file
                    if (utls == "EN") resxFile = enresx;    //English   
                    else resxFile = frresx;                //French
                    ResXResourceSet resxSet = new ResXResourceSet(resxFile);

                    warn.Text = resxSet.GetString("pwmis");
                    pass2.Image = Unowhy_Tools.Properties.Resources.pwcno;
                }
                name1.Image = Unowhy_Tools.Properties.Resources.user;
            }

            
        }

        private void pass_TextChanged(object sender, EventArgs e)
        {
            if(pass.Text == "")
            {
                pass1.Image = Unowhy_Tools.Properties.Resources.passno;
            }
            else
            {
                pass1.Image = Unowhy_Tools.Properties.Resources.passyes;
            }

            if (conf.Text == pass.Text)
            {
                warn.Text = "";
                pass2.Image = Unowhy_Tools.Properties.Resources.pwcyes;
            }
            else
            {
                //Check the current saved language

                RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
                string utls = utl.GetValue("Lang").ToString();

                string enresx = @".\en.resx";
                string frresx = @".\fr.resx";
                //Chose the ResX file
                if (utls == "EN") resxFile = enresx;    //English   
                else resxFile = frresx;                //French
                ResXResourceSet resxSet = new ResXResourceSet(resxFile);

                warn.Text = resxSet.GetString("pwmis");
                pass2.Image = Unowhy_Tools.Properties.Resources.pwcno;
            }
        }

        private void conf_TextChanged(object sender, EventArgs e)
        {
            if(conf.Text == pass.Text)
            {
                warn.Text = "";
                pass2.Image = Unowhy_Tools.Properties.Resources.pwcyes;
            }
            else
            {
                //Check the current saved language

                RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
                string utls = utl.GetValue("Lang").ToString();

                string enresx = @".\en.resx";
                string frresx = @".\fr.resx";
                //Chose the ResX file
                if (utls == "EN") resxFile = enresx;    //English   
                else resxFile = frresx;                //French
                ResXResourceSet resxSet = new ResXResourceSet(resxFile);

                warn.Text = resxSet.GetString("pwmis");
                pass2.Image = Unowhy_Tools.Properties.Resources.pwcno;
            }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_]");

            if (r.IsMatch(name.Text.ToString().Trim()) || name.Text == "")
            {
                //Check the current saved language

                RegistryKey utl = Registry.CurrentUser.OpenSubKey(@"Software\STY1001\Unowhy Tools", false);
                string utls = utl.GetValue("Lang").ToString();

                string enresx = @".\en.resx";
                string frresx = @".\fr.resx";
                //Chose the ResX file
                if (utls == "EN") resxFile = enresx;    //English   
                else resxFile = frresx;                //French
                ResXResourceSet resxSet = new ResXResourceSet(resxFile);

                warn.Text = resxSet.GetString("an");
                name1.Image = Unowhy_Tools.Properties.Resources.deluser;
            }
            else
            {
                warn.Text = "";
                name1.Image = Unowhy_Tools.Properties.Resources.user;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
