
namespace Unowhy_Tools
{
    partial class dialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dialog));
            this.label = new System.Windows.Forms.Label();
            this.yes = new System.Windows.Forms.Button();
            this.no = new System.Windows.Forms.Button();
            this.icon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(48, 21);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(49, 13);
            this.label.TabIndex = 0;
            this.label.Text = "Question";
            // 
            // yes
            // 
            this.yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.yes.ForeColor = System.Drawing.Color.Black;
            this.yes.Location = new System.Drawing.Point(254, 54);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(75, 23);
            this.yes.TabIndex = 1;
            this.yes.Text = "Yes";
            this.yes.UseVisualStyleBackColor = true;
            // 
            // no
            // 
            this.no.DialogResult = System.Windows.Forms.DialogResult.No;
            this.no.ForeColor = System.Drawing.Color.Black;
            this.no.Location = new System.Drawing.Point(173, 54);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(75, 23);
            this.no.TabIndex = 1;
            this.no.Text = "No";
            this.no.UseVisualStyleBackColor = true;
            // 
            // icon
            // 
            this.icon.Image = global::Unowhy_Tools.Properties.Resources.Question;
            this.icon.Location = new System.Drawing.Point(12, 12);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(30, 30);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.icon.TabIndex = 2;
            this.icon.TabStop = false;
            // 
            // dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(341, 89);
            this.Controls.Add(this.icon);
            this.Controls.Add(this.no);
            this.Controls.Add(this.yes);
            this.Controls.Add(this.label);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirmation";
            this.Load += new System.EventHandler(this.dialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button yes;
        private System.Windows.Forms.Button no;
        private System.Windows.Forms.PictureBox icon;
    }
}