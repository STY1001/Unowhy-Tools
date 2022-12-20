namespace Unowhy_Tools
{
    partial class backup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(backup));
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labbak = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.TextBox();
            this.back = new ReaLTaiizor.Controls.Button();
            this.browse = new ReaLTaiizor.Controls.Button();
            this.labapp = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.dism = new System.Windows.Forms.RadioButton();
            this.ps = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Unowhy_Tools.Properties.Resources.upload;
            this.pictureBox5.Location = new System.Drawing.Point(271, 180);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(22, 22);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 11;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Unowhy_Tools.Properties.Resources.folder;
            this.pictureBox4.Location = new System.Drawing.Point(271, 155);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(22, 22);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 12;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools.Properties.Resources.upload;
            this.pictureBox1.Location = new System.Drawing.Point(17, 103);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // labbak
            // 
            this.labbak.AutoSize = true;
            this.labbak.ForeColor = System.Drawing.Color.White;
            this.labbak.Location = new System.Drawing.Point(40, 107);
            this.labbak.Name = "labbak";
            this.labbak.Size = new System.Drawing.Size(44, 13);
            this.labbak.TabIndex = 9;
            this.labbak.Text = "Backup";
            // 
            // path
            // 
            this.path.BackColor = System.Drawing.Color.Black;
            this.path.ForeColor = System.Drawing.Color.White;
            this.path.Location = new System.Drawing.Point(13, 127);
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(337, 22);
            this.path.TabIndex = 8;
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.Color.Transparent;
            this.back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.back.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.back.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back.Image = null;
            this.back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.back.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.back.Location = new System.Drawing.Point(297, 180);
            this.back.Name = "back";
            this.back.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.back.Size = new System.Drawing.Size(53, 22);
            this.back.TabIndex = 6;
            this.back.Text = "Backup";
            this.back.TextAlignment = System.Drawing.StringAlignment.Center;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // browse
            // 
            this.browse.BackColor = System.Drawing.Color.Transparent;
            this.browse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browse.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.browse.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browse.Image = null;
            this.browse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.browse.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.browse.Location = new System.Drawing.Point(297, 155);
            this.browse.Name = "browse";
            this.browse.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.browse.Size = new System.Drawing.Size(53, 22);
            this.browse.TabIndex = 7;
            this.browse.Text = "Browse";
            this.browse.TextAlignment = System.Drawing.StringAlignment.Center;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // labapp
            // 
            this.labapp.AutoSize = true;
            this.labapp.ForeColor = System.Drawing.Color.White;
            this.labapp.Location = new System.Drawing.Point(40, 18);
            this.labapp.Name = "labapp";
            this.labapp.Size = new System.Drawing.Size(67, 13);
            this.labapp.TabIndex = 9;
            this.labapp.Text = "Backup app";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Unowhy_Tools.Properties.Resources.settings;
            this.pictureBox2.Location = new System.Drawing.Point(17, 14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // dism
            // 
            this.dism.AutoSize = true;
            this.dism.Checked = true;
            this.dism.ForeColor = System.Drawing.Color.White;
            this.dism.Location = new System.Drawing.Point(121, 45);
            this.dism.Name = "dism";
            this.dism.Size = new System.Drawing.Size(134, 17);
            this.dism.TabIndex = 13;
            this.dism.TabStop = true;
            this.dism.Text = "DISM (recommended)";
            this.dism.UseVisualStyleBackColor = true;
            // 
            // ps
            // 
            this.ps.AutoSize = true;
            this.ps.ForeColor = System.Drawing.Color.White;
            this.ps.Location = new System.Drawing.Point(20, 45);
            this.ps.Name = "ps";
            this.ps.Size = new System.Drawing.Size(82, 17);
            this.ps.TabIndex = 14;
            this.ps.Text = "PowerShell";
            this.ps.UseVisualStyleBackColor = true;
            // 
            // backup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(362, 215);
            this.Controls.Add(this.ps);
            this.Controls.Add(this.dism);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labapp);
            this.Controls.Add(this.labbak);
            this.Controls.Add(this.path);
            this.Controls.Add(this.back);
            this.Controls.Add(this.browse);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "backup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drv GUI for UT";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labbak;
        private System.Windows.Forms.TextBox path;
        private ReaLTaiizor.Controls.Button back;
        private ReaLTaiizor.Controls.Button browse;
        private System.Windows.Forms.Label labapp;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton dism;
        private System.Windows.Forms.RadioButton ps;
    }
}