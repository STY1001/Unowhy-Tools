
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
            this.okbtn = new System.Windows.Forms.Button();
            this.langsel = new System.Windows.Forms.ComboBox();
            this.cbupdate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // langlab
            // 
            this.langlab.AutoSize = true;
            this.langlab.ForeColor = System.Drawing.Color.White;
            this.langlab.Location = new System.Drawing.Point(12, 27);
            this.langlab.Name = "langlab";
            this.langlab.Size = new System.Drawing.Size(61, 13);
            this.langlab.TabIndex = 0;
            this.langlab.Text = "Language :";
            // 
            // okbtn
            // 
            this.okbtn.Location = new System.Drawing.Point(175, 70);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(31, 23);
            this.okbtn.TabIndex = 1;
            this.okbtn.Text = "OK";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // langsel
            // 
            this.langsel.FormattingEnabled = true;
            this.langsel.Items.AddRange(new object[] {
            "English",
            "French"});
            this.langsel.Location = new System.Drawing.Point(79, 24);
            this.langsel.Name = "langsel";
            this.langsel.Size = new System.Drawing.Size(121, 21);
            this.langsel.TabIndex = 2;
            this.langsel.SelectedIndexChanged += new System.EventHandler(this.langsel_SelectedIndexChanged);
            // 
            // cbupdate
            // 
            this.cbupdate.AutoSize = true;
            this.cbupdate.ForeColor = System.Drawing.Color.White;
            this.cbupdate.Location = new System.Drawing.Point(12, 76);
            this.cbupdate.Name = "cbupdate";
            this.cbupdate.Size = new System.Drawing.Size(140, 17);
            this.cbupdate.TabIndex = 3;
            this.cbupdate.Text = "Check update at startup";
            this.cbupdate.UseVisualStyleBackColor = true;
            this.cbupdate.CheckedChanged += new System.EventHandler(this.cbupdate_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(218, 105);
            this.Controls.Add(this.cbupdate);
            this.Controls.Add(this.langsel);
            this.Controls.Add(this.okbtn);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label langlab;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.ComboBox langsel;
        private System.Windows.Forms.CheckBox cbupdate;
    }
}