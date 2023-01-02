
namespace Unowhy_Tools
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.langlab = new System.Windows.Forms.Label();
            this.langsel = new System.Windows.Forms.ComboBox();
            this.cbupdate = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.okbtn = new ReaLTaiizor.Controls.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.clean = new ReaLTaiizor.Controls.Button();
            this.loglab = new System.Windows.Forms.Label();
            this.log2lab = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.logopen = new ReaLTaiizor.Controls.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // langlab
            // 
            this.langlab.AutoSize = true;
            this.langlab.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.langlab.ForeColor = System.Drawing.Color.White;
            this.langlab.Location = new System.Drawing.Point(36, 13);
            this.langlab.Name = "langlab";
            this.langlab.Size = new System.Drawing.Size(64, 13);
            this.langlab.TabIndex = 0;
            this.langlab.Text = "Language :";
            // 
            // langsel
            // 
            this.langsel.BackColor = System.Drawing.Color.Black;
            this.langsel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.langsel.ForeColor = System.Drawing.Color.White;
            this.langsel.FormattingEnabled = true;
            this.langsel.Items.AddRange(new object[] {
            "English",
            "French"});
            this.langsel.Location = new System.Drawing.Point(103, 10);
            this.langsel.Name = "langsel";
            this.langsel.Size = new System.Drawing.Size(121, 21);
            this.langsel.TabIndex = 2;
            this.langsel.SelectedIndexChanged += new System.EventHandler(this.langsel_SelectedIndexChanged);
            // 
            // cbupdate
            // 
            this.cbupdate.AutoSize = true;
            this.cbupdate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbupdate.ForeColor = System.Drawing.Color.White;
            this.cbupdate.Location = new System.Drawing.Point(31, 42);
            this.cbupdate.Name = "cbupdate";
            this.cbupdate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbupdate.Size = new System.Drawing.Size(150, 17);
            this.cbupdate.TabIndex = 3;
            this.cbupdate.Text = "Check update at startup";
            this.cbupdate.UseVisualStyleBackColor = true;
            this.cbupdate.CheckedChanged += new System.EventHandler(this.cbupdate_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools.Properties.Resources.language;
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Unowhy_Tools.Properties.Resources.update;
            this.pictureBox2.Location = new System.Drawing.Point(10, 40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Unowhy_Tools.Properties.Resources.startup;
            this.pictureBox3.Location = new System.Drawing.Point(15, 45);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(10, 10);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Unowhy_Tools.Properties.Resources.yes;
            this.pictureBox4.Location = new System.Drawing.Point(144, 145);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 20);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 4;
            this.pictureBox4.TabStop = false;
            // 
            // okbtn
            // 
            this.okbtn.BackColor = System.Drawing.Color.Transparent;
            this.okbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okbtn.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.okbtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okbtn.Image = null;
            this.okbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.okbtn.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.okbtn.Location = new System.Drawing.Point(165, 143);
            this.okbtn.Name = "okbtn";
            this.okbtn.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.okbtn.Size = new System.Drawing.Size(72, 23);
            this.okbtn.TabIndex = 5;
            this.okbtn.Text = "OK";
            this.okbtn.TextAlignment = System.Drawing.StringAlignment.Center;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Unowhy_Tools.Properties.Resources.delete;
            this.pictureBox5.Location = new System.Drawing.Point(10, 69);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(20, 20);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // clean
            // 
            this.clean.BackColor = System.Drawing.Color.Transparent;
            this.clean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clean.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.clean.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clean.Image = null;
            this.clean.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.clean.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.clean.Location = new System.Drawing.Point(126, 69);
            this.clean.Name = "clean";
            this.clean.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.clean.Size = new System.Drawing.Size(98, 20);
            this.clean.TabIndex = 5;
            this.clean.Text = "Cleanup";
            this.clean.TextAlignment = System.Drawing.StringAlignment.Center;
            this.clean.Click += new System.EventHandler(this.clean_Click);
            // 
            // loglab
            // 
            this.loglab.AutoSize = true;
            this.loglab.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loglab.ForeColor = System.Drawing.Color.White;
            this.loglab.Location = new System.Drawing.Point(33, 72);
            this.loglab.Name = "loglab";
            this.loglab.Size = new System.Drawing.Size(75, 13);
            this.loglab.TabIndex = 0;
            this.loglab.Text = "Cleanup logs";
            // 
            // log2lab
            // 
            this.log2lab.AutoSize = true;
            this.log2lab.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log2lab.ForeColor = System.Drawing.Color.White;
            this.log2lab.Location = new System.Drawing.Point(33, 102);
            this.log2lab.Name = "log2lab";
            this.log2lab.Size = new System.Drawing.Size(61, 13);
            this.log2lab.TabIndex = 0;
            this.log2lab.Text = "Open logs";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Unowhy_Tools.Properties.Resources.script;
            this.pictureBox6.Location = new System.Drawing.Point(10, 99);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(20, 20);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 4;
            this.pictureBox6.TabStop = false;
            // 
            // logopen
            // 
            this.logopen.BackColor = System.Drawing.Color.Transparent;
            this.logopen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logopen.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.logopen.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logopen.Image = null;
            this.logopen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.logopen.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.logopen.Location = new System.Drawing.Point(126, 99);
            this.logopen.Name = "logopen";
            this.logopen.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.logopen.Size = new System.Drawing.Size(98, 20);
            this.logopen.TabIndex = 5;
            this.logopen.Text = "Open";
            this.logopen.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(238, 167);
            this.Controls.Add(this.logopen);
            this.Controls.Add(this.clean);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbupdate);
            this.Controls.Add(this.log2lab);
            this.Controls.Add(this.langsel);
            this.Controls.Add(this.loglab);
            this.Controls.Add(this.langlab);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label langlab;
        private System.Windows.Forms.ComboBox langsel;
        private System.Windows.Forms.CheckBox cbupdate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private ReaLTaiizor.Controls.Button okbtn;
        private System.Windows.Forms.PictureBox pictureBox5;
        private ReaLTaiizor.Controls.Button clean;
        private System.Windows.Forms.Label loglab;
        private System.Windows.Forms.Label log2lab;
        private System.Windows.Forms.PictureBox pictureBox6;
        private ReaLTaiizor.Controls.Button logopen;
    }
}