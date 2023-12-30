namespace Zeiterfassung_Supervisor
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSignout = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSizePlus = new System.Windows.Forms.Button();
            this.btnSizeMinus = new System.Windows.Forms.Button();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader6,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(12, 33);
            this.listView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(672, 376);
            this.listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView.TabIndex = 0;
            this.toolTip.SetToolTip(this.listView, "(Doppelklicken zum öffnen der Excek Tabelle)");
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Benutzername";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Name";
            this.columnHeader6.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Letzte Synchronisierung";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Stunden";
            this.columnHeader3.Width = 65;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Seit Monat";
            this.columnHeader4.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Jahr";
            this.columnHeader5.Width = 80;
            // 
            // btnSignout
            // 
            this.btnSignout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignout.Location = new System.Drawing.Point(572, 418);
            this.btnSignout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSignout.Name = "btnSignout";
            this.btnSignout.Size = new System.Drawing.Size(113, 32);
            this.btnSignout.TabIndex = 1;
            this.btnSignout.Text = "&Beenden";
            this.toolTip.SetToolTip(this.btnSignout, "Programm beenden");
            this.btnSignout.UseVisualStyleBackColor = true;
            this.btnSignout.Click += new System.EventHandler(this.btnSignout_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(12, 418);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(155, 33);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Liste &aktualisieren";
            this.toolTip.SetToolTip(this.btnRefresh, "Liste mit aktuellen Weten vom Server abgleichen");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSizePlus
            // 
            this.btnSizePlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSizePlus.Location = new System.Drawing.Point(291, 418);
            this.btnSizePlus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSizePlus.Name = "btnSizePlus";
            this.btnSizePlus.Size = new System.Drawing.Size(77, 33);
            this.btnSizePlus.TabIndex = 5;
            this.btnSizePlus.Text = "Ver&größern";
            this.toolTip.SetToolTip(this.btnSizePlus, "Liste vergrößern");
            this.btnSizePlus.UseVisualStyleBackColor = true;
            this.btnSizePlus.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSizeMinus
            // 
            this.btnSizeMinus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSizeMinus.Location = new System.Drawing.Point(206, 418);
            this.btnSizeMinus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSizeMinus.Name = "btnSizeMinus";
            this.btnSizeMinus.Size = new System.Drawing.Size(77, 33);
            this.btnSizeMinus.TabIndex = 6;
            this.btnSizeMinus.Text = "Ver&kleinern";
            this.toolTip.SetToolTip(this.btnSizeMinus, "Liste verkleinern");
            this.btnSizeMinus.UseVisualStyleBackColor = true;
            this.btnSizeMinus.Click += new System.EventHandler(this.btnSizeMinus_Click);
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblCopyright.ForeColor = System.Drawing.Color.Gray;
            this.lblCopyright.Location = new System.Drawing.Point(12, 9);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(271, 13);
            this.lblCopyright.TabIndex = 2;
            this.lblCopyright.Text = "Copyright © 2022 - 2023 Christoph Beyer, Schindler AG";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 465);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.btnSizeMinus);
            this.Controls.Add(this.btnSizePlus);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.btnSignout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zeiterfassung - Supervisor";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolTip toolTip;
        private ListView listView;
        private Button btnSignout;
        private Label lblCopyright;
        private Button btnRefresh;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private Button btnSizePlus;
        private Button btnSizeMinus;
    }
}