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
using Unowhy_Tools.Properties;

using static Unowhy_Tools.UTclass;
using System.Threading;

namespace Unowhy_Tools
{
    public partial class Adduser : Form
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

        public Adduser()
        {
            InitializeComponent();

            this.Text = getlang("adduser");
            labn.Text = getlang("name");
            labp.Text = getlang("password");
            labc.Text = getlang("confpw");
            admin.Text = getlang("la");
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
            Thread w = new Thread(new ThreadStart(WaitScreen));

            Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_]");

            if (r.IsMatch(sname.ToString().Trim()) || sname == "")
            {
                warn.Text = getlang("an");
                name1.Image = Unowhy_Tools.Properties.Resources.deluser;
            }
            else
            {
                if (conf.Text == pass.Text)
                {
                    string msg = this.Text;
                    Image ico = Resources.adduser;
                    dialog d = new dialog(msg, ico);
                    d.ShowDialog();
                    if (d.DialogResult.Equals(DialogResult.Yes))
                    {
                        w.Start();
                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);

                        if (pass.Text == "")
                        {
                            string arg = ($"user \"{sname}\" /add");
                            runmin("net", arg, false);

                            if (admin.Checked == true)
                            {
                                string arga = ($"localgroup Administrateurs \"{sname}\" /add");
                                runmin("net", arga, false);
                            }
                        }
                        else
                        {
                            string arg = ($"user \"{sname}\" \"{spass}\" /add");
                            runmin("net", arg, false);

                            if (admin.Checked == true)
                            {
                                string arga = ($"localgroup Administrateurs \"{sname}\" /add");
                                runmin("net", arga, false);
                            }
                        }

                        w.Abort();
                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                        string username = sname;
                        var uidf = new userid(username);
                        uidf.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    warn.Text = getlang("pwmis");
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
                warn.Text = getlang("pwmis");
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
                warn.Text = getlang("pwmis");
                pass2.Image = Unowhy_Tools.Properties.Resources.pwcno;
            }
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_]");

            if (r.IsMatch(name.Text.ToString().Trim()) || name.Text == "")
            {
                warn.Text = getlang("an");
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
