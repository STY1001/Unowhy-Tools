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
            this.yes = new System.Windows.Forms.Button();
            this.no = new System.Windows.Forms.Button();
            this.pyes = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pno = new System.Windows.Forms.PictureBox();
            this.progbar = new System.Windows.Forms.ProgressBar();
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
            this.lab.Size = new System.Drawing.Size(132, 13);
            this.lab.TabIndex = 1;
            this.lab.Text = "Uninstalling Unowhy Tools";
            // 
            // yes
            // 
            this.yes.Location = new System.Drawing.Point(296, 59);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(36, 23);
            this.yes.TabIndex = 2;
            this.yes.Text = "Yes";
            this.yes.UseVisualStyleBackColor = true;
            this.yes.Click += new System.EventHandler(this.yes_Click);
            // 
            // no
            // 
            this.no.Location = new System.Drawing.Point(226, 59);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(30, 23);
            this.no.TabIndex = 2;
            this.no.Text = "No";
            this.no.UseVisualStyleBackColor = true;
            this.no.Click += new System.EventHandler(this.no_Click);
            // 
            // pyes
            // 
            this.pyes.Image = global::Unowhy_Tools_Uninstaller.Properties.Resources.yes;
            this.pyes.Location = new System.Drawing.Point(271, 58);
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
            this.pno.Location = new System.Drawing.Point(201, 58);
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
            this.progbar.Size = new System.Drawing.Size(312, 14);
            this.progbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progbar.TabIndex = 4;
            this.progbar.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(336, 85);
            this.Controls.Add(this.progbar);
            this.Controls.Add(this.pno);
            this.Controls.Add(this.pyes);
            this.Controls.Add(this.no);
            this.Controls.Add(this.yes);
            this.Controls.Add(this.lab);
            this.Controls.Add(this.pictureBox1);
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
        private System.Windows.Forms.Button yes;
        private System.Windows.Forms.Button no;
        private System.Windows.Forms.PictureBox pyes;
        private System.Windows.Forms.PictureBox pno;
        private System.Windows.Forms.ProgressBar progbar;
    }
}

