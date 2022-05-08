
namespace Unowhy_Tools
{
    partial class newver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newver));
            this.newverlab = new System.Windows.Forms.Label();
            this.git = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newverlab
            // 
            this.newverlab.AutoSize = true;
            this.newverlab.ForeColor = System.Drawing.Color.White;
            this.newverlab.Location = new System.Drawing.Point(15, 19);
            this.newverlab.Name = "newverlab";
            this.newverlab.Size = new System.Drawing.Size(170, 13);
            this.newverlab.TabIndex = 0;
            this.newverlab.Text = "New version is available on Github";
            this.newverlab.Click += new System.EventHandler(this.label1_Click);
            // 
            // git
            // 
            this.git.Location = new System.Drawing.Point(72, 56);
            this.git.Name = "git";
            this.git.Size = new System.Drawing.Size(75, 23);
            this.git.TabIndex = 1;
            this.git.Text = "Github";
            this.git.UseVisualStyleBackColor = true;
            this.git.Click += new System.EventHandler(this.button1_Click);
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(153, 56);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(32, 23);
            this.ok.TabIndex = 2;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.button2_Click);
            // 
            // newver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(197, 91);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.git);
            this.Controls.Add(this.newverlab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "newver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Checker";
            this.Load += new System.EventHandler(this.newver_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label newverlab;
        private System.Windows.Forms.Button git;
        private System.Windows.Forms.Button ok;
    }
}