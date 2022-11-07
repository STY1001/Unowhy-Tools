namespace Unowhy_Tools_Uninstaller
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
            this.lab = new System.Windows.Forms.Label();
            this.pyes = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pno = new System.Windows.Forms.PictureBox();
            this.progbar = new System.Windows.Forms.ProgressBar();
            this.no = new ReaLTaiizor.Controls.Button();
            this.yes = new ReaLTaiizor.Controls.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pyes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pno)).BeginInit();
            this.SuspendLayout();
            // 
            // lab
            // 
            this.lab.AutoSize = true;
            this.lab.ForeColor = System.Drawing.Color.White;
            this.lab.Location = new System.Drawing.Point(51, 21);
            this.lab.Name = "lab";
            this.lab.Size = new System.Drawing.Size(146, 13);
            this.lab.TabIndex = 1;
            this.lab.Text = "Uninstalling Unowhy Tools";
            // 
            // pyes
            // 
            this.pyes.Image = global::Unowhy_Tools_Uninstaller.Properties.Resources.yes;
            this.pyes.Location = new System.Drawing.Point(291, 58);
            this.pyes.Name = "pyes";
            this.pyes.Size = new System.Drawing.Size(24, 24);
            this.pyes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pyes.TabIndex = 3;
            this.pyes.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Unowhy_Tools_Uninstaller.Properties.Resources.upload;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pno
            // 
            this.pno.Image = global::Unowhy_Tools_Uninstaller.Properties.Resources.no;
            this.pno.Location = new System.Drawing.Point(221, 58);
            this.pno.Name = "pno";
            this.pno.Size = new System.Drawing.Size(24, 24);
            this.pno.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pno.TabIndex = 3;
            this.pno.TabStop = false;
            // 
            // progbar
            // 
            this.progbar.Location = new System.Drawing.Point(12, 59);
            this.progbar.MarqueeAnimationSpeed = 1;
            this.progbar.Name = "progbar";
            this.progbar.Size = new System.Drawing.Size(331, 14);
            this.progbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progbar.TabIndex = 4;
            this.progbar.Visible = false;
            // 
            // no
            // 
            this.no.BackColor = System.Drawing.Color.Transparent;
            this.no.Cursor = System.Windows.Forms.Cursors.Hand;
            this.no.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.no.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.no.Image = null;
            this.no.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.no.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.no.Location = new System.Drawing.Point(247, 59);
            this.no.Name = "no";
            this.no.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.no.Size = new System.Drawing.Size(29, 23);
            this.no.TabIndex = 5;
            this.no.Text = "No";
            this.no.TextAlignment = System.Drawing.StringAlignment.Center;
            this.no.Click += new System.EventHandler(this.no_Click);
            // 
            // yes
            // 
            this.yes.BackColor = System.Drawing.Color.Transparent;
            this.yes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yes.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.yes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yes.Image = null;
            this.yes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.yes.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.yes.Location = new System.Drawing.Point(315, 59);
            this.yes.Name = "yes";
            this.yes.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.yes.Size = new System.Drawing.Size(37, 23);
            this.yes.TabIndex = 5;
            this.yes.Text = "Yes";
            this.yes.TextAlignment = System.Drawing.StringAlignment.Center;
            this.yes.Click += new System.EventHandler(this.yes_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(355, 85);
            this.Controls.Add(this.yes);
            this.Controls.Add(this.no);
            this.Controls.Add(this.pno);
            this.Controls.Add(this.pyes);
            this.Controls.Add(this.lab);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progbar);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unowhy Tools Uninstaller";
            ((System.ComponentModel.ISupportInitialize)(this.pyes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pno)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lab;
        private System.Windows.Forms.PictureBox pyes;
        private System.Windows.Forms.PictureBox pno;
        private System.Windows.Forms.ProgressBar progbar;
        private ReaLTaiizor.Controls.Button no;
        private ReaLTaiizor.Controls.Button yes;
    }
}

