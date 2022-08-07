
namespace Unowhy_Tools
{
    partial class PCName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PCName));
            this.ancat = new System.Windows.Forms.Label();
            this.actualname = new System.Windows.Forms.Label();
            this.changename = new System.Windows.Forms.TextBox();
            this.cncat = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.avert = new System.Windows.Forms.TextBox();
            this.close = new ReaLTaiizor.Controls.Button();
            this.ok = new ReaLTaiizor.Controls.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ancat
            // 
            this.ancat.AutoSize = true;
            this.ancat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ancat.ForeColor = System.Drawing.Color.White;
            this.ancat.Location = new System.Drawing.Point(9, 9);
            this.ancat.Name = "ancat";
            this.ancat.Size = new System.Drawing.Size(76, 13);
            this.ancat.TabIndex = 0;
            this.ancat.Text = "Actual name :";
            this.ancat.Click += new System.EventHandler(this.label1_Click);
            // 
            // actualname
            // 
            this.actualname.AutoSize = true;
            this.actualname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actualname.ForeColor = System.Drawing.Color.White;
            this.actualname.Location = new System.Drawing.Point(9, 26);
            this.actualname.Name = "actualname";
            this.actualname.Size = new System.Drawing.Size(45, 17);
            this.actualname.TabIndex = 1;
            this.actualname.Text = "NAME";
            // 
            // changename
            // 
            this.changename.BackColor = System.Drawing.Color.Black;
            this.changename.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changename.ForeColor = System.Drawing.Color.White;
            this.changename.Location = new System.Drawing.Point(12, 75);
            this.changename.Name = "changename";
            this.changename.Size = new System.Drawing.Size(192, 22);
            this.changename.TabIndex = 2;
            // 
            // cncat
            // 
            this.cncat.AutoSize = true;
            this.cncat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cncat.ForeColor = System.Drawing.Color.White;
            this.cncat.Location = new System.Drawing.Point(11, 58);
            this.cncat.Name = "cncat";
            this.cncat.Size = new System.Drawing.Size(84, 13);
            this.cncat.TabIndex = 3;
            this.cncat.Text = "Change name :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools.Properties.Resources.customize;
            this.pictureBox1.Location = new System.Drawing.Point(108, 100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Unowhy_Tools.Properties.Resources.no;
            this.pictureBox2.Location = new System.Drawing.Point(8, 99);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // avert
            // 
            this.avert.BackColor = System.Drawing.Color.Black;
            this.avert.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avert.ForeColor = System.Drawing.Color.White;
            this.avert.Location = new System.Drawing.Point(12, 127);
            this.avert.Multiline = true;
            this.avert.Name = "avert";
            this.avert.ReadOnly = true;
            this.avert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.avert.Size = new System.Drawing.Size(192, 58);
            this.avert.TabIndex = 7;
            this.avert.Visible = false;
            // 
            // close
            // 
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.close.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Image = null;
            this.close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.close.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.close.Location = new System.Drawing.Point(28, 99);
            this.close.Name = "close";
            this.close.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.close.Size = new System.Drawing.Size(73, 21);
            this.close.TabIndex = 8;
            this.close.Text = "Cancel";
            this.close.TextAlignment = System.Drawing.StringAlignment.Center;
            this.close.Click += new System.EventHandler(this.button2_Click);
            // 
            // ok
            // 
            this.ok.BackColor = System.Drawing.Color.Transparent;
            this.ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ok.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.ok.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ok.Image = null;
            this.ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ok.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.ok.Location = new System.Drawing.Point(130, 99);
            this.ok.Name = "ok";
            this.ok.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.ok.Size = new System.Drawing.Size(73, 21);
            this.ok.TabIndex = 8;
            this.ok.Text = "OK";
            this.ok.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // PCName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(216, 126);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.close);
            this.Controls.Add(this.avert);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cncat);
            this.Controls.Add(this.changename);
            this.Controls.Add(this.actualname);
            this.Controls.Add(this.ancat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PCName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows PC name";
            this.Load += new System.EventHandler(this.pcname_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ancat;
        private System.Windows.Forms.Label actualname;
        private System.Windows.Forms.TextBox changename;
        private System.Windows.Forms.Label cncat;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox avert;
        private ReaLTaiizor.Controls.Button close;
        private ReaLTaiizor.Controls.Button ok;
    }
}