
namespace Unowhy_Tools
{
    partial class AdminSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminSet));
            this.disable = new System.Windows.Forms.Button();
            this.enable = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.passbox = new System.Windows.Forms.TextBox();
            this.passbtn = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // disable
            // 
            this.disable.Location = new System.Drawing.Point(268, 38);
            this.disable.Name = "disable";
            this.disable.Size = new System.Drawing.Size(124, 23);
            this.disable.TabIndex = 0;
            this.disable.Text = "Disable account";
            this.disable.UseVisualStyleBackColor = true;
            this.disable.Click += new System.EventHandler(this.disable_Click);
            // 
            // enable
            // 
            this.enable.Location = new System.Drawing.Point(268, 9);
            this.enable.Name = "enable";
            this.enable.Size = new System.Drawing.Size(124, 23);
            this.enable.TabIndex = 1;
            this.enable.Text = "Enable account";
            this.enable.UseVisualStyleBackColor = true;
            this.enable.Click += new System.EventHandler(this.enable_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools.Properties.Resources.enable;
            this.pictureBox1.Location = new System.Drawing.Point(241, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Unowhy_Tools.Properties.Resources.disable;
            this.pictureBox2.Location = new System.Drawing.Point(241, 37);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // passbox
            // 
            this.passbox.BackColor = System.Drawing.Color.Black;
            this.passbox.ForeColor = System.Drawing.Color.White;
            this.passbox.Location = new System.Drawing.Point(12, 12);
            this.passbox.Name = "passbox";
            this.passbox.PasswordChar = '•';
            this.passbox.Size = new System.Drawing.Size(201, 20);
            this.passbox.TabIndex = 3;
            // 
            // passbtn
            // 
            this.passbtn.Location = new System.Drawing.Point(42, 38);
            this.passbtn.Name = "passbtn";
            this.passbtn.Size = new System.Drawing.Size(171, 23);
            this.passbtn.TabIndex = 4;
            this.passbtn.Text = "Set new password";
            this.passbtn.UseVisualStyleBackColor = true;
            this.passbtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Unowhy_Tools.Properties.Resources.key;
            this.pictureBox3.Location = new System.Drawing.Point(12, 34);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 24);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // AdminSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(403, 70);
            this.Controls.Add(this.passbtn);
            this.Controls.Add(this.passbox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.enable);
            this.Controls.Add(this.disable);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminSettings";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button disable;
        private System.Windows.Forms.Button enable;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox passbox;
        private System.Windows.Forms.Button passbtn;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}