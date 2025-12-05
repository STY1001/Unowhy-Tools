namespace Unowhy_Tools_Installer
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.desktop_check = new System.Windows.Forms.CheckBox();
            this.statusbar = new System.Windows.Forms.ProgressBar();
            this.desc = new System.Windows.Forms.Label();
            this.sty1001 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.TextBox();
            this.version = new System.Windows.Forms.Label();
            this.ok_btn = new ReaLTaiizor.Controls.Button();
            this.install_btn = new ReaLTaiizor.Controls.Button();
            this.install_img = new System.Windows.Forms.PictureBox();
            this.ok_img = new System.Windows.Forms.PictureBox();
            this.desktop_img = new System.Windows.Forms.PictureBox();
            this.utlogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.install_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ok_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desktop_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utlogo)).BeginInit();
            this.SuspendLayout();
            // 
            // desktop_check
            // 
            this.desktop_check.AutoSize = true;
            this.desktop_check.Checked = true;
            this.desktop_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.desktop_check.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desktop_check.ForeColor = System.Drawing.Color.White;
            this.desktop_check.Location = new System.Drawing.Point(39, 148);
            this.desktop_check.Name = "desktop_check";
            this.desktop_check.Size = new System.Drawing.Size(268, 17);
            this.desktop_check.TabIndex = 1;
            this.desktop_check.Text = "Shortcut on desktop / Raccourcie sur le bureau";
            this.desktop_check.UseVisualStyleBackColor = true;
            // 
            // statusbar
            // 
            this.statusbar.Location = new System.Drawing.Point(-5, 235);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(321, 17);
            this.statusbar.Step = 1;
            this.statusbar.TabIndex = 0;
            // 
            // desc
            // 
            this.desc.AutoSize = true;
            this.desc.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desc.ForeColor = System.Drawing.Color.White;
            this.desc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.desc.Location = new System.Drawing.Point(12, 106);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(286, 13);
            this.desc.TabIndex = 36;
            this.desc.Text = "The ultimate all-in-one utility for your Unowhy device !";
            // 
            // sty1001
            // 
            this.sty1001.AutoSize = true;
            this.sty1001.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sty1001.ForeColor = System.Drawing.Color.White;
            this.sty1001.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sty1001.Location = new System.Drawing.Point(12, 123);
            this.sty1001.Name = "sty1001";
            this.sty1001.Size = new System.Drawing.Size(133, 13);
            this.sty1001.TabIndex = 35;
            this.sty1001.Text = "by STY1001 | sty1001.com";
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.Color.Black;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.ForeColor = System.Drawing.Color.White;
            this.status.Location = new System.Drawing.Point(16, 173);
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Size = new System.Drawing.Size(279, 20);
            this.status.TabIndex = 0;
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.ForeColor = System.Drawing.Color.White;
            this.version.Location = new System.Drawing.Point(242, 124);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(53, 13);
            this.version.TabIndex = 39;
            this.version.Text = "Ver 31.07";
            // 
            // ok_btn
            // 
            this.ok_btn.BackColor = System.Drawing.Color.Transparent;
            this.ok_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ok_btn.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.ok_btn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ok_btn.Image = null;
            this.ok_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ok_btn.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.ok_btn.Location = new System.Drawing.Point(46, 204);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.ok_btn.Size = new System.Drawing.Size(249, 23);
            this.ok_btn.TabIndex = 5;
            this.ok_btn.Text = "Ok";
            this.ok_btn.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ok_btn.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // install_btn
            // 
            this.install_btn.BackColor = System.Drawing.Color.Transparent;
            this.install_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.install_btn.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.install_btn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.install_btn.Image = null;
            this.install_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.install_btn.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.install_btn.Location = new System.Drawing.Point(46, 203);
            this.install_btn.Name = "install_btn";
            this.install_btn.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.install_btn.Size = new System.Drawing.Size(249, 23);
            this.install_btn.TabIndex = 4;
            this.install_btn.Text = "Install / Installer (Press I)";
            this.install_btn.TextAlignment = System.Drawing.StringAlignment.Center;
            this.install_btn.Click += new System.EventHandler(this.Install_Click);
            // 
            // install_img
            // 
            this.install_img.Image = global::Unowhy_Tools_Installer.Properties.Resources.download;
            this.install_img.Location = new System.Drawing.Point(15, 202);
            this.install_img.Name = "install_img";
            this.install_img.Size = new System.Drawing.Size(24, 24);
            this.install_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.install_img.TabIndex = 2;
            this.install_img.TabStop = false;
            // 
            // ok_img
            // 
            this.ok_img.Image = global::Unowhy_Tools_Installer.Properties.Resources.yes;
            this.ok_img.Location = new System.Drawing.Point(16, 203);
            this.ok_img.Name = "ok_img";
            this.ok_img.Size = new System.Drawing.Size(24, 24);
            this.ok_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ok_img.TabIndex = 2;
            this.ok_img.TabStop = false;
            // 
            // desktop_img
            // 
            this.desktop_img.Image = global::Unowhy_Tools_Installer.Properties.Resources.desktop;
            this.desktop_img.Location = new System.Drawing.Point(12, 145);
            this.desktop_img.Name = "desktop_img";
            this.desktop_img.Size = new System.Drawing.Size(20, 20);
            this.desktop_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.desktop_img.TabIndex = 2;
            this.desktop_img.TabStop = false;
            // 
            // utlogo
            // 
            this.utlogo.Image = global::Unowhy_Tools_Installer.Properties.Resources.UTLogoWhite3_0;
            this.utlogo.Location = new System.Drawing.Point(13, 12);
            this.utlogo.Name = "utlogo";
            this.utlogo.Size = new System.Drawing.Size(282, 92);
            this.utlogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.utlogo.TabIndex = 0;
            this.utlogo.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(310, 238);
            this.Controls.Add(this.install_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.install_img);
            this.Controls.Add(this.version);
            this.Controls.Add(this.status);
            this.Controls.Add(this.desc);
            this.Controls.Add(this.sty1001);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.ok_img);
            this.Controls.Add(this.desktop_img);
            this.Controls.Add(this.desktop_check);
            this.Controls.Add(this.utlogo);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unowhy Tools Installer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.install_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ok_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desktop_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utlogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox utlogo;
        private System.Windows.Forms.CheckBox desktop_check;
        private System.Windows.Forms.PictureBox desktop_img;
        private System.Windows.Forms.PictureBox install_img;
        private System.Windows.Forms.ProgressBar statusbar;
        private System.Windows.Forms.Label desc;
        private System.Windows.Forms.Label sty1001;
        private System.Windows.Forms.TextBox status;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.PictureBox ok_img;
        private ReaLTaiizor.Controls.Button ok_btn;
        private ReaLTaiizor.Controls.Button install_btn;
    }
}

