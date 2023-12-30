namespace Zeiterfassung_Supervisor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnSignIn = new System.Windows.Forms.Button();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnReportError = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnSignIn
            // 
            this.btnSignIn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignIn.Image = global::Zeiterfassung_Supervisor.Properties.Resources._194_40;
            this.btnSignIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSignIn.Location = new System.Drawing.Point(123, 42);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(279, 72);
            this.btnSignIn.TabIndex = 1;
            this.btnSignIn.Text = "&Anmelden";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.Color.Gray;
            this.lblCopyright.Location = new System.Drawing.Point(12, 178);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(271, 13);
            this.lblCopyright.TabIndex = 3;
            this.lblCopyright.Text = "Copyright © 2022 - 2023 Christoph Beyer, Schindler AG";
            // 
            // btnAbout
            // 
            this.btnAbout.Image = global::Zeiterfassung_Supervisor.Properties.Resources._1001_20;
            this.btnAbout.Location = new System.Drawing.Point(472, 155);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(41, 33);
            this.btnAbout.TabIndex = 7;
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnReportError
            // 
            this.btnReportError.Image = global::Zeiterfassung_Supervisor.Properties.Resources._105_20;
            this.btnReportError.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportError.Location = new System.Drawing.Point(322, 155);
            this.btnReportError.Name = "btnReportError";
            this.btnReportError.Size = new System.Drawing.Size(143, 33);
            this.btnReportError.TabIndex = 8;
            this.btnReportError.Text = "Einen &Fehler melden";
            this.btnReportError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnReportError, "Einen Fehler an die lokale IT melden\r\n[STRG] + klicken zum anzeigen der Log-Datei" +
        "\r\n");
            this.btnReportError.UseVisualStyleBackColor = true;
            this.btnReportError.Click += new System.EventHandler(this.btnReportError_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 200);
            this.Controls.Add(this.btnReportError);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.btnSignIn);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zeiterfassung (Supervisor) Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnSignIn;
        private Label lblCopyright;
        private Button btnAbout;
        private Button btnReportError;
        private ToolTip toolTip1;
    }
}