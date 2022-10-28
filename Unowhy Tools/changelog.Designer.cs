using System;

namespace Unowhy_Tools
{
    partial class changelog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(changelog));
            this.webclog = new System.Windows.Forms.WebBrowser();
            this.webdown = new ReaLTaiizor.Controls.Button();
            this.webup = new ReaLTaiizor.Controls.Button();
            this.SuspendLayout();
            // 
            // webclog
            // 
            this.webclog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webclog.Location = new System.Drawing.Point(0, 0);
            this.webclog.MinimumSize = new System.Drawing.Size(20, 20);
            this.webclog.Name = "webclog";
            this.webclog.ScriptErrorsSuppressed = true;
            this.webclog.ScrollBarsEnabled = false;
            this.webclog.Size = new System.Drawing.Size(813, 450);
            this.webclog.TabIndex = 0;
            this.webclog.Url = new System.Uri("", System.UriKind.Relative);
            this.webclog.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.keynav);
            // 
            // webdown
            // 
            this.webdown.BackColor = System.Drawing.Color.Transparent;
            this.webdown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.webdown.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.webdown.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.webdown.Image = null;
            this.webdown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.webdown.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.webdown.Location = new System.Drawing.Point(781, 420);
            this.webdown.Name = "webdown";
            this.webdown.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.webdown.Size = new System.Drawing.Size(32, 30);
            this.webdown.TabIndex = 1;
            this.webdown.Text = "▼";
            this.webdown.TextAlignment = System.Drawing.StringAlignment.Center;
            this.webdown.Click += new System.EventHandler(this.webdown_Click);
            this.webdown.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.keynav);
            // 
            // webup
            // 
            this.webup.BackColor = System.Drawing.Color.Transparent;
            this.webup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.webup.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.webup.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.webup.Image = null;
            this.webup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.webup.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.webup.Location = new System.Drawing.Point(781, 0);
            this.webup.Name = "webup";
            this.webup.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.webup.Size = new System.Drawing.Size(32, 30);
            this.webup.TabIndex = 1;
            this.webup.Text = "▲";
            this.webup.TextAlignment = System.Drawing.StringAlignment.Center;
            this.webup.Click += new System.EventHandler(this.webup_Click);
            this.webup.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.keynav);
            // 
            // changelog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(813, 450);
            this.Controls.Add(this.webup);
            this.Controls.Add(this.webdown);
            this.Controls.Add(this.webclog);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "changelog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Change Log";
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.keynav);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webclog;
        private ReaLTaiizor.Controls.Button webdown;
        private ReaLTaiizor.Controls.Button webup;
    }
}