
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
            this.labmf = new System.Windows.Forms.Label();
            this.mf = new System.Windows.Forms.Label();
            this.labserial = new System.Windows.Forms.Label();
            this.serial = new System.Windows.Forms.Label();
            this.labmodel = new System.Windows.Forms.Label();
            this.model = new System.Windows.Forms.Label();
            this.biosver = new System.Windows.Forms.Label();
            this.labbiosver = new System.Windows.Forms.Label();
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
            // labmf
            // 
            this.labmf.AutoSize = true;
            this.labmf.Location = new System.Drawing.Point(13, 54);
            this.labmf.Name = "labmf";
            this.labmf.Size = new System.Drawing.Size(104, 13);
            this.labmf.TabIndex = 0;
            this.labmf.Text = "Manufacturer Name:";
            // 
            // mf
            // 
            this.mf.AutoSize = true;
            this.mf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mf.Location = new System.Drawing.Point(13, 67);
            this.mf.Name = "mf";
            this.mf.Size = new System.Drawing.Size(47, 16);
            this.mf.TabIndex = 0;
            this.mf.Text = "NAME";
            // 
            // labserial
            // 
            this.labserial.AutoSize = true;
            this.labserial.Location = new System.Drawing.Point(13, 142);
            this.labserial.Name = "labserial";
            this.labserial.Size = new System.Drawing.Size(76, 13);
            this.labserial.TabIndex = 1;
            this.labserial.Text = "Serial Number:";
            // 
            // serial
            // 
            this.serial.AutoSize = true;
            this.serial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serial.Location = new System.Drawing.Point(13, 155);
            this.serial.Name = "serial";
            this.serial.Size = new System.Drawing.Size(47, 16);
            this.serial.TabIndex = 0;
            this.serial.Text = "NAME";
            // 
            // labmodel
            // 
            this.labmodel.AutoSize = true;
            this.labmodel.Location = new System.Drawing.Point(13, 98);
            this.labmodel.Name = "labmodel";
            this.labmodel.Size = new System.Drawing.Size(70, 13);
            this.labmodel.TabIndex = 0;
            this.labmodel.Text = "Model Name:";
            // 
            // model
            // 
            this.model.AutoSize = true;
            this.model.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.model.Location = new System.Drawing.Point(13, 111);
            this.model.Name = "model";
            this.model.Size = new System.Drawing.Size(47, 16);
            this.model.TabIndex = 0;
            this.model.Text = "NAME";
            // 
            // biosver
            // 
            this.biosver.AutoSize = true;
            this.biosver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.biosver.Location = new System.Drawing.Point(13, 202);
            this.biosver.Name = "biosver";
            this.biosver.Size = new System.Drawing.Size(47, 16);
            this.biosver.TabIndex = 0;
            this.biosver.Text = "NAME";
            // 
            // labbiosver
            // 
            this.labbiosver.AutoSize = true;
            this.labbiosver.Location = new System.Drawing.Point(13, 189);
            this.labbiosver.Name = "labbiosver";
            this.labbiosver.Size = new System.Drawing.Size(73, 13);
            this.labbiosver.TabIndex = 1;
            this.labbiosver.Text = "BIOS Version:";
            // 
            // PCInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(247, 405);
            this.Controls.Add(this.labbiosver);
            this.Controls.Add(this.biosver);
            this.Controls.Add(this.labserial);
            this.Controls.Add(this.serial);
            this.Controls.Add(this.model);
            this.Controls.Add(this.mf);
            this.Controls.Add(this.labmodel);
            this.Controls.Add(this.pcname);
            this.Controls.Add(this.labmf);
            this.Controls.Add(this.labpcname);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
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
        private System.Windows.Forms.Label labmf;
        private System.Windows.Forms.Label mf;
        private System.Windows.Forms.Label labserial;
        private System.Windows.Forms.Label serial;
        private System.Windows.Forms.Label labmodel;
        private System.Windows.Forms.Label model;
        private System.Windows.Forms.Label biosver;
        private System.Windows.Forms.Label labbiosver;
    }
}