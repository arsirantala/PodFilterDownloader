namespace IxothPodFilterDownloader
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gbOuter = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnMoreInfoOnSelectedFilter = new System.Windows.Forms.Button();
            this.btnInstallSelected = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnDownloadUpdatedFilters = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbPoDInstallLocation = new System.Windows.Forms.GroupBox();
            this.btnBrowsePoDInstallLoc = new System.Windows.Forms.Button();
            this.txtPodInstallationLoc = new System.Windows.Forms.TextBox();
            this.gbInstalled_Available = new System.Windows.Forms.GroupBox();
            this.rbAvailable = new System.Windows.Forms.RadioButton();
            this.rbInstalled = new System.Windows.Forms.RadioButton();
            this.lvFilters = new System.Windows.Forms.ListView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.gbOuter.SuspendLayout();
            this.gbPoDInstallLocation.SuspendLayout();
            this.gbInstalled_Available.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOuter
            // 
            this.gbOuter.Controls.Add(this.btnCancel);
            this.gbOuter.Controls.Add(this.btnRemoveSelected);
            this.gbOuter.Controls.Add(this.btnMoreInfoOnSelectedFilter);
            this.gbOuter.Controls.Add(this.btnInstallSelected);
            this.gbOuter.Controls.Add(this.progressBar);
            this.gbOuter.Controls.Add(this.btnDownloadUpdatedFilters);
            this.gbOuter.Controls.Add(this.btnRefresh);
            this.gbOuter.Controls.Add(this.gbPoDInstallLocation);
            this.gbOuter.Controls.Add(this.gbInstalled_Available);
            this.gbOuter.Controls.Add(this.lvFilters);
            this.gbOuter.Location = new System.Drawing.Point(29, 34);
            this.gbOuter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbOuter.Name = "gbOuter";
            this.gbOuter.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbOuter.Size = new System.Drawing.Size(933, 695);
            this.gbOuter.TabIndex = 1;
            this.gbOuter.TabStop = false;
            this.gbOuter.Text = "Click on button to download PoD filter";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(754, 650);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(162, 28);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Enabled = false;
            this.btnRemoveSelected.Location = new System.Drawing.Point(24, 606);
            this.btnRemoveSelected.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(197, 30);
            this.btnRemoveSelected.TabIndex = 4;
            this.btnRemoveSelected.Text = "Remove selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // btnMoreInfoOnSelectedFilter
            // 
            this.btnMoreInfoOnSelectedFilter.Enabled = false;
            this.btnMoreInfoOnSelectedFilter.Location = new System.Drawing.Point(254, 606);
            this.btnMoreInfoOnSelectedFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMoreInfoOnSelectedFilter.Name = "btnMoreInfoOnSelectedFilter";
            this.btnMoreInfoOnSelectedFilter.Size = new System.Drawing.Size(197, 30);
            this.btnMoreInfoOnSelectedFilter.TabIndex = 5;
            this.btnMoreInfoOnSelectedFilter.Text = "More info on selected filter";
            this.btnMoreInfoOnSelectedFilter.UseVisualStyleBackColor = true;
            this.btnMoreInfoOnSelectedFilter.Click += new System.EventHandler(this.btnMoreInfoOnSelectedFilter_Click);
            // 
            // btnInstallSelected
            // 
            this.btnInstallSelected.Enabled = false;
            this.btnInstallSelected.Location = new System.Drawing.Point(475, 606);
            this.btnInstallSelected.Margin = new System.Windows.Forms.Padding(4);
            this.btnInstallSelected.Name = "btnInstallSelected";
            this.btnInstallSelected.Size = new System.Drawing.Size(162, 30);
            this.btnInstallSelected.TabIndex = 6;
            this.btnInstallSelected.Text = "Install selected";
            this.btnInstallSelected.UseVisualStyleBackColor = true;
            this.btnInstallSelected.Click += new System.EventHandler(this.btnInstallSelected_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(24, 650);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(708, 27);
            this.progressBar.TabIndex = 8;
            // 
            // btnDownloadUpdatedFilters
            // 
            this.btnDownloadUpdatedFilters.Enabled = false;
            this.btnDownloadUpdatedFilters.Location = new System.Drawing.Point(685, 606);
            this.btnDownloadUpdatedFilters.Margin = new System.Windows.Forms.Padding(4);
            this.btnDownloadUpdatedFilters.Name = "btnDownloadUpdatedFilters";
            this.btnDownloadUpdatedFilters.Size = new System.Drawing.Size(231, 30);
            this.btnDownloadUpdatedFilters.TabIndex = 7;
            this.btnDownloadUpdatedFilters.Text = "Download updates";
            this.btnDownloadUpdatedFilters.UseVisualStyleBackColor = true;
            this.btnDownloadUpdatedFilters.Click += new System.EventHandler(this.btnDownloadUpdatedFilters_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(816, 83);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 28);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gbPoDInstallLocation
            // 
            this.gbPoDInstallLocation.Controls.Add(this.btnBrowsePoDInstallLoc);
            this.gbPoDInstallLocation.Controls.Add(this.txtPodInstallationLoc);
            this.gbPoDInstallLocation.Location = new System.Drawing.Point(338, 32);
            this.gbPoDInstallLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbPoDInstallLocation.Name = "gbPoDInstallLocation";
            this.gbPoDInstallLocation.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbPoDInstallLocation.Size = new System.Drawing.Size(441, 60);
            this.gbPoDInstallLocation.TabIndex = 1;
            this.gbPoDInstallLocation.TabStop = false;
            this.gbPoDInstallLocation.Text = "PoD install location";
            // 
            // btnBrowsePoDInstallLoc
            // 
            this.btnBrowsePoDInstallLoc.Location = new System.Drawing.Point(330, 22);
            this.btnBrowsePoDInstallLoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowsePoDInstallLoc.Name = "btnBrowsePoDInstallLoc";
            this.btnBrowsePoDInstallLoc.Size = new System.Drawing.Size(75, 30);
            this.btnBrowsePoDInstallLoc.TabIndex = 1;
            this.btnBrowsePoDInstallLoc.Text = "...";
            this.btnBrowsePoDInstallLoc.UseVisualStyleBackColor = true;
            this.btnBrowsePoDInstallLoc.Click += new System.EventHandler(this.btnBrowsePoDInstallLoc_Click);
            // 
            // txtPodInstallationLoc
            // 
            this.txtPodInstallationLoc.Enabled = false;
            this.txtPodInstallationLoc.Location = new System.Drawing.Point(10, 25);
            this.txtPodInstallationLoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPodInstallationLoc.Name = "txtPodInstallationLoc";
            this.txtPodInstallationLoc.Size = new System.Drawing.Size(306, 22);
            this.txtPodInstallationLoc.TabIndex = 0;
            // 
            // gbInstalled_Available
            // 
            this.gbInstalled_Available.Controls.Add(this.rbAvailable);
            this.gbInstalled_Available.Controls.Add(this.rbInstalled);
            this.gbInstalled_Available.Location = new System.Drawing.Point(21, 32);
            this.gbInstalled_Available.Margin = new System.Windows.Forms.Padding(4);
            this.gbInstalled_Available.Name = "gbInstalled_Available";
            this.gbInstalled_Available.Padding = new System.Windows.Forms.Padding(4);
            this.gbInstalled_Available.Size = new System.Drawing.Size(300, 60);
            this.gbInstalled_Available.TabIndex = 0;
            this.gbInstalled_Available.TabStop = false;
            this.gbInstalled_Available.Text = "Show filters based of";
            // 
            // rbAvailable
            // 
            this.rbAvailable.AutoSize = true;
            this.rbAvailable.Location = new System.Drawing.Point(139, 23);
            this.rbAvailable.Margin = new System.Windows.Forms.Padding(4);
            this.rbAvailable.Name = "rbAvailable";
            this.rbAvailable.Size = new System.Drawing.Size(86, 21);
            this.rbAvailable.TabIndex = 1;
            this.rbAvailable.Text = "Available";
            this.rbAvailable.UseVisualStyleBackColor = true;
            this.rbAvailable.CheckedChanged += new System.EventHandler(this.rbInstalled_CheckedChanged);
            // 
            // rbInstalled
            // 
            this.rbInstalled.AutoSize = true;
            this.rbInstalled.Checked = true;
            this.rbInstalled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbInstalled.Location = new System.Drawing.Point(18, 23);
            this.rbInstalled.Margin = new System.Windows.Forms.Padding(4);
            this.rbInstalled.Name = "rbInstalled";
            this.rbInstalled.Size = new System.Drawing.Size(80, 21);
            this.rbInstalled.TabIndex = 0;
            this.rbInstalled.TabStop = true;
            this.rbInstalled.Text = "Installed";
            this.rbInstalled.UseVisualStyleBackColor = true;
            this.rbInstalled.CheckedChanged += new System.EventHandler(this.rbInstalled_CheckedChanged);
            // 
            // lvFilters
            // 
            this.lvFilters.HideSelection = false;
            this.lvFilters.Location = new System.Drawing.Point(24, 117);
            this.lvFilters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvFilters.MultiSelect = false;
            this.lvFilters.Name = "lvFilters";
            this.lvFilters.Size = new System.Drawing.Size(892, 469);
            this.lvFilters.TabIndex = 3;
            this.lvFilters.UseCompatibleStateImageBehavior = false;
            this.lvFilters.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvFilters_ItemSelectionChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(989, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFileExit});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(46, 24);
            this.toolStripMenuItemFile.Text = "File";
            // 
            // toolStripMenuItemFileExit
            // 
            this.toolStripMenuItemFileExit.Name = "toolStripMenuItemFileExit";
            this.toolStripMenuItemFileExit.Size = new System.Drawing.Size(116, 26);
            this.toolStripMenuItemFileExit.Text = "Exit";
            this.toolStripMenuItemFileExit.Click += new System.EventHandler(this.toolStripMenuItemFileExit_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHelpAbout});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(55, 24);
            this.toolStripMenuItemHelp.Text = "Help";
            // 
            // toolStripMenuItemHelpAbout
            // 
            this.toolStripMenuItemHelpAbout.Name = "toolStripMenuItemHelpAbout";
            this.toolStripMenuItemHelpAbout.Size = new System.Drawing.Size(142, 26);
            this.toolStripMenuItemHelpAbout.Text = "About...";
            this.toolStripMenuItemHelpAbout.Click += new System.EventHandler(this.toolStripMenuItemHelpAbout_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 754);
            this.Controls.Add(this.gbOuter);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ixoth\'s PoD filter Downloader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.gbOuter.ResumeLayout(false);
            this.gbPoDInstallLocation.ResumeLayout(false);
            this.gbPoDInstallLocation.PerformLayout();
            this.gbInstalled_Available.ResumeLayout(false);
            this.gbInstalled_Available.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOuter;
        private System.Windows.Forms.GroupBox gbPoDInstallLocation;
        private System.Windows.Forms.Button btnBrowsePoDInstallLoc;
        private System.Windows.Forms.TextBox txtPodInstallationLoc;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileExit;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelpAbout;
        private System.Windows.Forms.Button btnMoreInfoOnSelectedFilter;
        private System.Windows.Forms.ListView lvFilters;
        private System.Windows.Forms.GroupBox gbInstalled_Available;
        private System.Windows.Forms.RadioButton rbAvailable;
        private System.Windows.Forms.RadioButton rbInstalled;
        private System.Windows.Forms.Button btnDownloadUpdatedFilters;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnInstallSelected;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnCancel;
    }
}

