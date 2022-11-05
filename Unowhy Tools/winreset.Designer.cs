namespace Unowhy_Tools
{
    partial class winreset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winreset));
            this.ena = new ReaLTaiizor.Controls.Button();
            this.dis = new ReaLTaiizor.Controls.Button();
            this.rep = new ReaLTaiizor.Controls.Button();
            this.debwinre = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.repmsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // ena
            // 
            this.ena.BackColor = System.Drawing.Color.Transparent;
            this.ena.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ena.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.ena.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ena.Image = null;
            this.ena.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ena.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.ena.Location = new System.Drawing.Point(37, 9);
            this.ena.Name = "ena";
            this.ena.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.ena.Size = new System.Drawing.Size(120, 26);
            this.ena.TabIndex = 0;
            this.ena.Text = "Enable";
            this.ena.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ena.Click += new System.EventHandler(this.ena_Click);
            // 
            // dis
            // 
            this.dis.BackColor = System.Drawing.Color.Transparent;
            this.dis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dis.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.dis.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dis.Image = null;
            this.dis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dis.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.dis.Location = new System.Drawing.Point(191, 8);
            this.dis.Name = "dis";
            this.dis.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.dis.Size = new System.Drawing.Size(120, 26);
            this.dis.TabIndex = 0;
            this.dis.Text = "Disable";
            this.dis.TextAlignment = System.Drawing.StringAlignment.Center;
            this.dis.Click += new System.EventHandler(this.dis_Click);
            // 
            // rep
            // 
            this.rep.BackColor = System.Drawing.Color.Transparent;
            this.rep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rep.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.rep.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rep.Image = null;
            this.rep.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rep.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.rep.Location = new System.Drawing.Point(345, 8);
            this.rep.Name = "rep";
            this.rep.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.rep.Size = new System.Drawing.Size(120, 26);
            this.rep.TabIndex = 0;
            this.rep.Text = "Repair";
            this.rep.TextAlignment = System.Drawing.StringAlignment.Center;
            this.rep.Click += new System.EventHandler(this.rep_Click);
            // 
            // debwinre
            // 
            this.debwinre.AutoSize = true;
            this.debwinre.Location = new System.Drawing.Point(7, 38);
            this.debwinre.Name = "debwinre";
            this.debwinre.Size = new System.Drawing.Size(36, 13);
            this.debwinre.TabIndex = 1;
            this.debwinre.Text = "winre";
            this.debwinre.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools.Properties.Resources.enable;
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Unowhy_Tools.Properties.Resources.disable;
            this.pictureBox2.Location = new System.Drawing.Point(164, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Unowhy_Tools.Properties.Resources.repair;
            this.pictureBox3.Location = new System.Drawing.Point(318, 9);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 24);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // repmsg
            // 
            this.repmsg.AutoSize = true;
            this.repmsg.Location = new System.Drawing.Point(12, 40);
            this.repmsg.Name = "repmsg";
            this.repmsg.Size = new System.Drawing.Size(0, 13);
            this.repmsg.TabIndex = 3;
            // 
            // winreset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(473, 61);
            this.Controls.Add(this.repmsg);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.debwinre);
            this.Controls.Add(this.rep);
            this.Controls.Add(this.dis);
            this.Controls.Add(this.ena);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "winreset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinRE settings for Unowhy Tools";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.Button ena;
        private ReaLTaiizor.Controls.Button dis;
        private ReaLTaiizor.Controls.Button rep;
        private System.Windows.Forms.Label debwinre;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label repmsg;
    }
}