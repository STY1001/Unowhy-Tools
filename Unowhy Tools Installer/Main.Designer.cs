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
            this.desktop = new System.Windows.Forms.CheckBox();
            this.run = new System.Windows.Forms.CheckBox();
            this.install = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.statusbar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.sty1001 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.status = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // desktop
            // 
            this.desktop.AutoSize = true;
            this.desktop.Checked = true;
            this.desktop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.desktop.ForeColor = System.Drawing.Color.White;
            this.desktop.Location = new System.Drawing.Point(43, 155);
            this.desktop.Name = "desktop";
            this.desktop.Size = new System.Drawing.Size(252, 17);
            this.desktop.TabIndex = 1;
            this.desktop.Text = "Shortcut on desktop / Raccourcie sur le bureau";
            this.desktop.UseVisualStyleBackColor = true;
            // 
            // run
            // 
            this.run.AutoSize = true;
            this.run.Checked = true;
            this.run.CheckState = System.Windows.Forms.CheckState.Checked;
            this.run.ForeColor = System.Drawing.Color.White;
            this.run.Location = new System.Drawing.Point(43, 181);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(236, 17);
            this.run.TabIndex = 1;
            this.run.Text = "Run after installed / Lancer après installation";
            this.run.UseVisualStyleBackColor = true;
            // 
            // install
            // 
            this.install.Location = new System.Drawing.Point(196, 254);
            this.install.Name = "install";
            this.install.Size = new System.Drawing.Size(100, 23);
            this.install.TabIndex = 3;
            this.install.Text = "Install / Installer";
            this.install.UseVisualStyleBackColor = true;
            this.install.Click += new System.EventHandler(this.install_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(42, 254);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(100, 23);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "Cancel / Annuler";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // statusbar
            // 
            this.statusbar.Location = new System.Drawing.Point(15, 207);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(279, 15);
            this.statusbar.Step = 1;
            this.statusbar.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(51, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "A tool for Unowhy Y13 computers !";
            // 
            // sty1001
            // 
            this.sty1001.AutoSize = true;
            this.sty1001.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.sty1001.ForeColor = System.Drawing.Color.White;
            this.sty1001.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sty1001.Location = new System.Drawing.Point(63, 126);
            this.sty1001.Name = "sty1001";
            this.sty1001.Size = new System.Drawing.Size(185, 13);
            this.sty1001.TabIndex = 35;
            this.sty1001.Text = "by STY1001 | sty1001.wordpress.com\r\n";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Unowhy_Tools_Installer.Properties.Resources.no;
            this.pictureBox5.Location = new System.Drawing.Point(14, 253);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(24, 24);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 2;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Unowhy_Tools_Installer.Properties.Resources.download;
            this.pictureBox4.Location = new System.Drawing.Point(168, 253);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(24, 24);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Unowhy_Tools_Installer.Properties.Resources.startup;
            this.pictureBox3.Location = new System.Drawing.Point(16, 178);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Unowhy_Tools_Installer.Properties.Resources.desktop;
            this.pictureBox2.Location = new System.Drawing.Point(16, 152);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools_Installer.Properties.Resources.UTLogoWhite2_0;
            this.pictureBox1.Location = new System.Drawing.Point(13, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(282, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.Color.Black;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.ForeColor = System.Drawing.Color.White;
            this.status.Location = new System.Drawing.Point(15, 227);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(279, 20);
            this.status.TabIndex = 37;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(310, 292);
            this.Controls.Add(this.status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sty1001);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.install);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.run);
            this.Controls.Add(this.desktop);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unowhy Tools Installer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox desktop;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox run;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button install;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.ProgressBar statusbar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label sty1001;
        private System.Windows.Forms.TextBox status;
    }
}

