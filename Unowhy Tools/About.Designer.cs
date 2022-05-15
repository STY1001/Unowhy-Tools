
namespace Unowhy_Tools
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.aver = new System.Windows.Forms.Label();
            this.asty1001 = new System.Windows.Forms.Label();
            this.website = new System.Windows.Forms.Button();
            this.github = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.alogo = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // aver
            // 
            this.aver.AutoSize = true;
            this.aver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aver.ForeColor = System.Drawing.Color.White;
            this.aver.Location = new System.Drawing.Point(8, 88);
            this.aver.Name = "aver";
            this.aver.Size = new System.Drawing.Size(93, 20);
            this.aver.TabIndex = 12;
            this.aver.Text = "Version 7.0";
            this.aver.Click += new System.EventHandler(this.label2_Click);
            // 
            // asty1001
            // 
            this.asty1001.AutoSize = true;
            this.asty1001.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asty1001.ForeColor = System.Drawing.Color.White;
            this.asty1001.Location = new System.Drawing.Point(9, 108);
            this.asty1001.Name = "asty1001";
            this.asty1001.Size = new System.Drawing.Size(230, 16);
            this.asty1001.TabIndex = 13;
            this.asty1001.Text = "by STY1001 | sty1001.wordpress.com";
            // 
            // website
            // 
            this.website.Location = new System.Drawing.Point(269, 12);
            this.website.Name = "website";
            this.website.Size = new System.Drawing.Size(101, 34);
            this.website.TabIndex = 14;
            this.website.Text = "STY1001\'s WebSite";
            this.website.UseVisualStyleBackColor = true;
            this.website.Click += new System.EventHandler(this.button1_Click);
            // 
            // github
            // 
            this.github.Location = new System.Drawing.Point(269, 50);
            this.github.Name = "github";
            this.github.Size = new System.Drawing.Size(101, 34);
            this.github.TabIndex = 15;
            this.github.Text = "Unowhy Tools\'s Github";
            this.github.UseVisualStyleBackColor = true;
            this.github.Click += new System.EventHandler(this.button2_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(269, 90);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(101, 34);
            this.update.TabIndex = 17;
            this.update.Text = "Check for update";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools.Properties.Resources.laptop;
            this.pictureBox1.Location = new System.Drawing.Point(377, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // alogo
            // 
            this.alogo.Image = global::Unowhy_Tools.Properties.Resources.UT_Logo;
            this.alogo.Location = new System.Drawing.Point(12, 12);
            this.alogo.Name = "alogo";
            this.alogo.Size = new System.Drawing.Size(205, 72);
            this.alogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.alogo.TabIndex = 19;
            this.alogo.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Unowhy_Tools.Properties.Resources.github;
            this.pictureBox2.Location = new System.Drawing.Point(377, 51);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Unowhy_Tools.Properties.Resources.update;
            this.pictureBox3.Location = new System.Drawing.Point(377, 91);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 20;
            this.pictureBox3.TabStop = false;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(420, 133);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.alogo);
            this.Controls.Add(this.update);
            this.Controls.Add(this.github);
            this.Controls.Add(this.website);
            this.Controls.Add(this.asty1001);
            this.Controls.Add(this.aver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label aver;
        private System.Windows.Forms.Label asty1001;
        private System.Windows.Forms.Button website;
        private System.Windows.Forms.Button github;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.PictureBox alogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}