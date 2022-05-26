
namespace Unowhy_Tools
{
    partial class PCInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PCInfo));
            this.labpcname = new System.Windows.Forms.Label();
            this.pcname = new System.Windows.Forms.Label();
            this.labprodname = new System.Windows.Forms.Label();
            this.prodname = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labpcname
            // 
            this.labpcname.AutoSize = true;
            this.labpcname.Location = new System.Drawing.Point(13, 13);
            this.labpcname.Name = "labpcname";
            this.labpcname.Size = new System.Drawing.Size(100, 13);
            this.labpcname.TabIndex = 0;
            this.labpcname.Text = "Windows PC name:";
            // 
            // pcname
            // 
            this.pcname.AutoSize = true;
            this.pcname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pcname.Location = new System.Drawing.Point(13, 26);
            this.pcname.Name = "pcname";
            this.pcname.Size = new System.Drawing.Size(47, 16);
            this.pcname.TabIndex = 0;
            this.pcname.Text = "NAME";
            // 
            // labprodname
            // 
            this.labprodname.AutoSize = true;
            this.labprodname.Location = new System.Drawing.Point(13, 54);
            this.labprodname.Name = "labprodname";
            this.labprodname.Size = new System.Drawing.Size(78, 13);
            this.labprodname.TabIndex = 0;
            this.labprodname.Text = "Product Name:";
            // 
            // prodname
            // 
            this.prodname.AutoSize = true;
            this.prodname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prodname.Location = new System.Drawing.Point(13, 67);
            this.prodname.Name = "prodname";
            this.prodname.Size = new System.Drawing.Size(47, 16);
            this.prodname.TabIndex = 0;
            this.prodname.Text = "NAME";
            // 
            // PCInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(247, 405);
            this.Controls.Add(this.prodname);
            this.Controls.Add(this.pcname);
            this.Controls.Add(this.labprodname);
            this.Controls.Add(this.labpcname);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PCInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labpcname;
        private System.Windows.Forms.Label pcname;
        private System.Windows.Forms.Label labprodname;
        private System.Windows.Forms.Label prodname;
    }
}