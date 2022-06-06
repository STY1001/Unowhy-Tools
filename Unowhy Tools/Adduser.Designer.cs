
namespace Unowhy_Tools
{
    partial class Adduser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Adduser));
            this.name = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.labn = new System.Windows.Forms.Label();
            this.labp = new System.Windows.Forms.Label();
            this.admin = new System.Windows.Forms.CheckBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.conf = new System.Windows.Forms.TextBox();
            this.labc = new System.Windows.Forms.Label();
            this.warn = new System.Windows.Forms.Label();
            this.name1 = new System.Windows.Forms.PictureBox();
            this.pass1 = new System.Windows.Forms.PictureBox();
            this.pass2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.name1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pass1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pass2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.BackColor = System.Drawing.Color.Black;
            this.name.ForeColor = System.Drawing.Color.White;
            this.name.Location = new System.Drawing.Point(41, 29);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(282, 20);
            this.name.TabIndex = 0;
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            // 
            // pass
            // 
            this.pass.BackColor = System.Drawing.Color.Black;
            this.pass.ForeColor = System.Drawing.Color.White;
            this.pass.Location = new System.Drawing.Point(40, 70);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '•';
            this.pass.Size = new System.Drawing.Size(282, 20);
            this.pass.TabIndex = 0;
            this.pass.TextChanged += new System.EventHandler(this.pass_TextChanged);
            // 
            // labn
            // 
            this.labn.AutoSize = true;
            this.labn.ForeColor = System.Drawing.Color.White;
            this.labn.Location = new System.Drawing.Point(41, 12);
            this.labn.Name = "labn";
            this.labn.Size = new System.Drawing.Size(41, 13);
            this.labn.TabIndex = 1;
            this.labn.Text = "Name :";
            // 
            // labp
            // 
            this.labp.AutoSize = true;
            this.labp.ForeColor = System.Drawing.Color.White;
            this.labp.Location = new System.Drawing.Point(40, 54);
            this.labp.Name = "labp";
            this.labp.Size = new System.Drawing.Size(59, 13);
            this.labp.TabIndex = 1;
            this.labp.Text = "Password :";
            // 
            // admin
            // 
            this.admin.AutoSize = true;
            this.admin.ForeColor = System.Drawing.Color.White;
            this.admin.Location = new System.Drawing.Point(40, 146);
            this.admin.Name = "admin";
            this.admin.Size = new System.Drawing.Size(84, 17);
            this.admin.TabIndex = 2;
            this.admin.Text = "Local Admin";
            this.admin.UseVisualStyleBackColor = true;
            this.admin.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(299, 193);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(33, 23);
            this.ok.TabIndex = 3;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(195, 192);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools.Properties.Resources.no;
            this.pictureBox1.Location = new System.Drawing.Point(173, 191);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Unowhy_Tools.Properties.Resources.yes;
            this.pictureBox2.Location = new System.Drawing.Point(277, 192);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // conf
            // 
            this.conf.BackColor = System.Drawing.Color.Black;
            this.conf.ForeColor = System.Drawing.Color.White;
            this.conf.Location = new System.Drawing.Point(40, 109);
            this.conf.Name = "conf";
            this.conf.PasswordChar = '•';
            this.conf.Size = new System.Drawing.Size(282, 20);
            this.conf.TabIndex = 0;
            this.conf.TextChanged += new System.EventHandler(this.conf_TextChanged);
            // 
            // labc
            // 
            this.labc.AutoSize = true;
            this.labc.ForeColor = System.Drawing.Color.White;
            this.labc.Location = new System.Drawing.Point(40, 93);
            this.labc.Name = "labc";
            this.labc.Size = new System.Drawing.Size(97, 13);
            this.labc.TabIndex = 1;
            this.labc.Text = "Confirm Password :";
            // 
            // warn
            // 
            this.warn.AutoSize = true;
            this.warn.ForeColor = System.Drawing.Color.White;
            this.warn.Location = new System.Drawing.Point(45, 170);
            this.warn.Name = "warn";
            this.warn.Size = new System.Drawing.Size(0, 13);
            this.warn.TabIndex = 6;
            // 
            // name1
            // 
            this.name1.Image = global::Unowhy_Tools.Properties.Resources.user;
            this.name1.Location = new System.Drawing.Point(13, 25);
            this.name1.Name = "name1";
            this.name1.Size = new System.Drawing.Size(24, 24);
            this.name1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.name1.TabIndex = 5;
            this.name1.TabStop = false;
            // 
            // pass1
            // 
            this.pass1.Image = global::Unowhy_Tools.Properties.Resources.passno;
            this.pass1.Location = new System.Drawing.Point(12, 66);
            this.pass1.Name = "pass1";
            this.pass1.Size = new System.Drawing.Size(24, 24);
            this.pass1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pass1.TabIndex = 5;
            this.pass1.TabStop = false;
            // 
            // pass2
            // 
            this.pass2.Image = global::Unowhy_Tools.Properties.Resources.pwcyes;
            this.pass2.Location = new System.Drawing.Point(12, 107);
            this.pass2.Name = "pass2";
            this.pass2.Size = new System.Drawing.Size(24, 24);
            this.pass2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pass2.TabIndex = 5;
            this.pass2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Unowhy_Tools.Properties.Resources.admin;
            this.pictureBox3.Location = new System.Drawing.Point(13, 141);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 24);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // Adduser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(335, 217);
            this.Controls.Add(this.warn);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pass2);
            this.Controls.Add(this.pass1);
            this.Controls.Add(this.name1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.admin);
            this.Controls.Add(this.labc);
            this.Controls.Add(this.labp);
            this.Controls.Add(this.labn);
            this.Controls.Add(this.conf);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.name);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Adduser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create new user (account)";
            this.Load += new System.EventHandler(this.Adduser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.name1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pass1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pass2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label labn;
        private System.Windows.Forms.Label labp;
        private System.Windows.Forms.CheckBox admin;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox conf;
        private System.Windows.Forms.Label labc;
        private System.Windows.Forms.Label warn;
        private System.Windows.Forms.PictureBox name1;
        private System.Windows.Forms.PictureBox pass1;
        private System.Windows.Forms.PictureBox pass2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}