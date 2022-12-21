namespace Unowhy_Tools
{
    partial class restore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(restore));
            this.browse = new ReaLTaiizor.Controls.Button();
            this.path = new System.Windows.Forms.TextBox();
            this.rest = new ReaLTaiizor.Controls.Button();
            this.conv = new ReaLTaiizor.Controls.Button();
            this.textconv = new System.Windows.Forms.Label();
            this.labres = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labconv = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
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
            this.browse.Location = new System.Drawing.Point(297, 153);
            this.browse.Name = "browse";
            this.browse.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.browse.Size = new System.Drawing.Size(53, 22);
            this.browse.TabIndex = 0;
            this.browse.Text = "Browse";
            this.browse.TextAlignment = System.Drawing.StringAlignment.Center;
            this.browse.Click += new System.EventHandler(this.button1_Click);
            // 
            // path
            // 
            this.path.BackColor = System.Drawing.Color.Black;
            this.path.ForeColor = System.Drawing.Color.White;
            this.path.Location = new System.Drawing.Point(13, 125);
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(337, 22);
            this.path.TabIndex = 1;
            // 
            // rest
            // 
            this.rest.BackColor = System.Drawing.Color.Transparent;
            this.rest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rest.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.rest.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rest.Image = null;
            this.rest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rest.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.rest.Location = new System.Drawing.Point(297, 178);
            this.rest.Name = "rest";
            this.rest.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.rest.Size = new System.Drawing.Size(53, 22);
            this.rest.TabIndex = 0;
            this.rest.Text = "Restore";
            this.rest.TextAlignment = System.Drawing.StringAlignment.Center;
            this.rest.Click += new System.EventHandler(this.rest_Click);
            // 
            // conv
            // 
            this.conv.BackColor = System.Drawing.Color.Transparent;
            this.conv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.conv.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.conv.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conv.Image = null;
            this.conv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.conv.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.conv.Location = new System.Drawing.Point(297, 62);
            this.conv.Name = "conv";
            this.conv.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.conv.Size = new System.Drawing.Size(53, 22);
            this.conv.TabIndex = 0;
            this.conv.Text = "Convert";
            this.conv.TextAlignment = System.Drawing.StringAlignment.Center;
            this.conv.Click += new System.EventHandler(this.conv_Click);
            // 
            // textconv
            // 
            this.textconv.AutoSize = true;
            this.textconv.ForeColor = System.Drawing.Color.White;
            this.textconv.Location = new System.Drawing.Point(13, 42);
            this.textconv.Name = "textconv";
            this.textconv.Size = new System.Drawing.Size(258, 13);
            this.textconv.TabIndex = 2;
            this.textconv.Text = "Convert an old backup (<18.0) to the new format";
            // 
            // labres
            // 
            this.labres.AutoSize = true;
            this.labres.ForeColor = System.Drawing.Color.White;
            this.labres.Location = new System.Drawing.Point(40, 105);
            this.labres.Name = "labres";
            this.labres.Size = new System.Drawing.Size(46, 13);
            this.labres.TabIndex = 3;
            this.labres.Text = "Restore";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools.Properties.Resources.download;
            this.pictureBox1.Location = new System.Drawing.Point(17, 101);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // labconv
            // 
            this.labconv.AutoSize = true;
            this.labconv.ForeColor = System.Drawing.Color.White;
            this.labconv.Location = new System.Drawing.Point(35, 15);
            this.labconv.Name = "labconv";
            this.labconv.Size = new System.Drawing.Size(47, 13);
            this.labconv.TabIndex = 3;
            this.labconv.Text = "Convert";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Unowhy_Tools.Properties.Resources.convert;
            this.pictureBox2.Location = new System.Drawing.Point(12, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Unowhy_Tools.Properties.Resources.convert;
            this.pictureBox3.Location = new System.Drawing.Point(271, 62);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(22, 22);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Unowhy_Tools.Properties.Resources.folder;
            this.pictureBox4.Location = new System.Drawing.Point(271, 153);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(22, 22);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Unowhy_Tools.Properties.Resources.download;
            this.pictureBox5.Location = new System.Drawing.Point(271, 178);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(22, 22);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 5;
            this.pictureBox5.TabStop = false;
            // 
            // restore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(362, 215);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labconv);
            this.Controls.Add(this.labres);
            this.Controls.Add(this.textconv);
            this.Controls.Add(this.path);
            this.Controls.Add(this.rest);
            this.Controls.Add(this.conv);
            this.Controls.Add(this.browse);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "restore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drv GUI for UT";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.Button browse;
        private System.Windows.Forms.TextBox path;
        private ReaLTaiizor.Controls.Button rest;
        private ReaLTaiizor.Controls.Button conv;
        private System.Windows.Forms.Label textconv;
        private System.Windows.Forms.Label labres;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labconv;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}