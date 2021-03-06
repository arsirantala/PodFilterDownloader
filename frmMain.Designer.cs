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
            this.lblToolUpdateAvailable = new System.Windows.Forms.Label();
            this.xpProgressBar = new Framework.Controls.XpProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnMoreInfoOnSelectedFilter = new System.Windows.Forms.Button();
            this.btnInstallSelected = new System.Windows.Forms.Button();
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
            this.toolStripMenuItemFileFilterAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelpUpdateApp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemHelpVisitApplicationHomeRepository = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelpVisitApplicationHomeWiki = new System.Windows.Forms.ToolStripMenuItem();
            this.gbOuter.SuspendLayout();
            this.gbPoDInstallLocation.SuspendLayout();
            this.gbInstalled_Available.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOuter
            // 
            this.gbOuter.Controls.Add(this.lblToolUpdateAvailable);
            this.gbOuter.Controls.Add(this.xpProgressBar);
            this.gbOuter.Controls.Add(this.btnCancel);
            this.gbOuter.Controls.Add(this.btnRemoveSelected);
            this.gbOuter.Controls.Add(this.btnMoreInfoOnSelectedFilter);
            this.gbOuter.Controls.Add(this.btnInstallSelected);
            this.gbOuter.Controls.Add(this.btnDownloadUpdatedFilters);
            this.gbOuter.Controls.Add(this.btnRefresh);
            this.gbOuter.Controls.Add(this.gbPoDInstallLocation);
            this.gbOuter.Controls.Add(this.gbInstalled_Available);
            this.gbOuter.Controls.Add(this.lvFilters);
            this.gbOuter.Location = new System.Drawing.Point(16, 40);
            this.gbOuter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbOuter.Name = "gbOuter";
            this.gbOuter.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbOuter.Size = new System.Drawing.Size(992, 673);
            this.gbOuter.TabIndex = 1;
            this.gbOuter.TabStop = false;
            this.gbOuter.Text = "Click on button to download PoD filter";
            // 
            // lblToolUpdateAvailable
            // 
            this.lblToolUpdateAvailable.ForeColor = System.Drawing.Color.LightCoral;
            this.lblToolUpdateAvailable.Location = new System.Drawing.Point(822, 25);
            this.lblToolUpdateAvailable.Name = "lblToolUpdateAvailable";
            this.lblToolUpdateAvailable.Size = new System.Drawing.Size(150, 59);
            this.lblToolUpdateAvailable.TabIndex = 12;
            this.lblToolUpdateAvailable.Text = "Tool update available";
            this.lblToolUpdateAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblToolUpdateAvailable.Visible = false;
            // 
            // xpProgressBar
            // 
            this.xpProgressBar.ColorBackGround = System.Drawing.Color.White;
            this.xpProgressBar.ColorBarBorder = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(240)))), ((int)(((byte)(170)))));
            this.xpProgressBar.ColorBarCenter = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(10)))));
            this.xpProgressBar.ColorText = System.Drawing.Color.Black;
            this.xpProgressBar.Location = new System.Drawing.Point(17, 571);
            this.xpProgressBar.Name = "xpProgressBar";
            this.xpProgressBar.Position = 0;
            this.xpProgressBar.PositionMax = 100;
            this.xpProgressBar.PositionMin = 0;
            this.xpProgressBar.Size = new System.Drawing.Size(780, 32);
            this.xpProgressBar.SteepDistance = ((byte)(0));
            this.xpProgressBar.TabIndex = 10;
            this.xpProgressBar.Text = "Checking server for updates";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(827, 571);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(145, 32);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Enabled = false;
            this.btnRemoveSelected.Location = new System.Drawing.Point(17, 613);
            this.btnRemoveSelected.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(197, 40);
            this.btnRemoveSelected.TabIndex = 4;
            this.btnRemoveSelected.Text = "Remove selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // btnMoreInfoOnSelectedFilter
            // 
            this.btnMoreInfoOnSelectedFilter.Enabled = false;
            this.btnMoreInfoOnSelectedFilter.Location = new System.Drawing.Point(240, 613);
            this.btnMoreInfoOnSelectedFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMoreInfoOnSelectedFilter.Name = "btnMoreInfoOnSelectedFilter";
            this.btnMoreInfoOnSelectedFilter.Size = new System.Drawing.Size(268, 40);
            this.btnMoreInfoOnSelectedFilter.TabIndex = 5;
            this.btnMoreInfoOnSelectedFilter.Text = "More info on selected filter";
            this.btnMoreInfoOnSelectedFilter.UseVisualStyleBackColor = true;
            this.btnMoreInfoOnSelectedFilter.Click += new System.EventHandler(this.btnMoreInfoOnSelectedFilter_Click);
            // 
            // btnInstallSelected
            // 
            this.btnInstallSelected.Enabled = false;
            this.btnInstallSelected.Location = new System.Drawing.Point(535, 613);
            this.btnInstallSelected.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnInstallSelected.Name = "btnInstallSelected";
            this.btnInstallSelected.Size = new System.Drawing.Size(190, 40);
            this.btnInstallSelected.TabIndex = 6;
            this.btnInstallSelected.Text = "Install selected";
            this.btnInstallSelected.UseVisualStyleBackColor = true;
            this.btnInstallSelected.Click += new System.EventHandler(this.btnInstallSelected_Click);
            // 
            // btnDownloadUpdatedFilters
            // 
            this.btnDownloadUpdatedFilters.Enabled = false;
            this.btnDownloadUpdatedFilters.Location = new System.Drawing.Point(757, 613);
            this.btnDownloadUpdatedFilters.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnDownloadUpdatedFilters.Name = "btnDownloadUpdatedFilters";
            this.btnDownloadUpdatedFilters.Size = new System.Drawing.Size(215, 40);
            this.btnDownloadUpdatedFilters.TabIndex = 7;
            this.btnDownloadUpdatedFilters.Text = "Download updates";
            this.btnDownloadUpdatedFilters.UseVisualStyleBackColor = true;
            this.btnDownloadUpdatedFilters.Click += new System.EventHandler(this.btnDownloadUpdatedFilters_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(821, 90);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(151, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gbPoDInstallLocation
            // 
            this.gbPoDInstallLocation.Controls.Add(this.btnBrowsePoDInstallLoc);
            this.gbPoDInstallLocation.Controls.Add(this.txtPodInstallationLoc);
            this.gbPoDInstallLocation.Location = new System.Drawing.Point(366, 42);
            this.gbPoDInstallLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbPoDInstallLocation.Name = "gbPoDInstallLocation";
            this.gbPoDInstallLocation.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbPoDInstallLocation.Size = new System.Drawing.Size(436, 76);
            this.gbPoDInstallLocation.TabIndex = 1;
            this.gbPoDInstallLocation.TabStop = false;
            this.gbPoDInstallLocation.Text = "PoD install location";
            // 
            // btnBrowsePoDInstallLoc
            // 
            this.btnBrowsePoDInstallLoc.Location = new System.Drawing.Point(342, 35);
            this.btnBrowsePoDInstallLoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowsePoDInstallLoc.Name = "btnBrowsePoDInstallLoc";
            this.btnBrowsePoDInstallLoc.Size = new System.Drawing.Size(62, 32);
            this.btnBrowsePoDInstallLoc.TabIndex = 1;
            this.btnBrowsePoDInstallLoc.Text = "...";
            this.btnBrowsePoDInstallLoc.UseVisualStyleBackColor = true;
            this.btnBrowsePoDInstallLoc.Click += new System.EventHandler(this.btnBrowsePoDInstallLoc_Click);
            // 
            // txtPodInstallationLoc
            // 
            this.txtPodInstallationLoc.Enabled = false;
            this.txtPodInstallationLoc.Location = new System.Drawing.Point(14, 35);
            this.txtPodInstallationLoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPodInstallationLoc.Name = "txtPodInstallationLoc";
            this.txtPodInstallationLoc.Size = new System.Drawing.Size(312, 30);
            this.txtPodInstallationLoc.TabIndex = 0;
            // 
            // gbInstalled_Available
            // 
            this.gbInstalled_Available.Controls.Add(this.rbAvailable);
            this.gbInstalled_Available.Controls.Add(this.rbInstalled);
            this.gbInstalled_Available.Location = new System.Drawing.Point(17, 42);
            this.gbInstalled_Available.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.gbInstalled_Available.Name = "gbInstalled_Available";
            this.gbInstalled_Available.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.gbInstalled_Available.Size = new System.Drawing.Size(283, 76);
            this.gbInstalled_Available.TabIndex = 0;
            this.gbInstalled_Available.TabStop = false;
            this.gbInstalled_Available.Text = "Show filters based of";
            // 
            // rbAvailable
            // 
            this.rbAvailable.AutoSize = true;
            this.rbAvailable.Location = new System.Drawing.Point(144, 37);
            this.rbAvailable.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.rbAvailable.Name = "rbAvailable";
            this.rbAvailable.Size = new System.Drawing.Size(113, 29);
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
            this.rbInstalled.Location = new System.Drawing.Point(26, 36);
            this.rbInstalled.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.rbInstalled.Name = "rbInstalled";
            this.rbInstalled.Size = new System.Drawing.Size(104, 29);
            this.rbInstalled.TabIndex = 0;
            this.rbInstalled.TabStop = true;
            this.rbInstalled.Text = "Installed";
            this.rbInstalled.UseVisualStyleBackColor = true;
            this.rbInstalled.CheckedChanged += new System.EventHandler(this.rbInstalled_CheckedChanged);
            // 
            // lvFilters
            // 
            this.lvFilters.FullRowSelect = true;
            this.lvFilters.HideSelection = false;
            this.lvFilters.Location = new System.Drawing.Point(17, 130);
            this.lvFilters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvFilters.MultiSelect = false;
            this.lvFilters.Name = "lvFilters";
            this.lvFilters.Size = new System.Drawing.Size(955, 433);
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
            this.menuStrip.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1025, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFileFilterAdmin,
            this.toolStripSeparator1,
            this.toolStripMenuItemFileExit});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(46, 24);
            this.toolStripMenuItemFile.Text = "File";
            // 
            // toolStripMenuItemFileFilterAdmin
            // 
            this.toolStripMenuItemFileFilterAdmin.Name = "toolStripMenuItemFileFilterAdmin";
            this.toolStripMenuItemFileFilterAdmin.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItemFileFilterAdmin.Text = "Filter admin...";
            this.toolStripMenuItemFileFilterAdmin.Click += new System.EventHandler(this.toolStripMenuItemFileFilterAdmin_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripMenuItemFileExit
            // 
            this.toolStripMenuItemFileExit.Name = "toolStripMenuItemFileExit";
            this.toolStripMenuItemFileExit.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItemFileExit.Text = "Exit";
            this.toolStripMenuItemFileExit.Click += new System.EventHandler(this.toolStripMenuItemFileExit_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHelpAbout,
            this.toolStripMenuItemHelpUpdateApp,
            this.toolStripSeparator2,
            this.toolStripMenuItemHelpVisitApplicationHomeRepository,
            this.toolStripMenuItemHelpVisitApplicationHomeWiki});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(55, 24);
            this.toolStripMenuItemHelp.Text = "Help";
            this.toolStripMenuItemHelp.DropDownOpening += new System.EventHandler(this.toolStripMenuItemHelp_DropDownOpening);
            // 
            // toolStripMenuItemHelpAbout
            // 
            this.toolStripMenuItemHelpAbout.Name = "toolStripMenuItemHelpAbout";
            this.toolStripMenuItemHelpAbout.Size = new System.Drawing.Size(389, 26);
            this.toolStripMenuItemHelpAbout.Text = "About...";
            this.toolStripMenuItemHelpAbout.Click += new System.EventHandler(this.toolStripMenuItemHelpAbout_Click);
            // 
            // toolStripMenuItemHelpUpdateApp
            // 
            this.toolStripMenuItemHelpUpdateApp.Name = "toolStripMenuItemHelpUpdateApp";
            this.toolStripMenuItemHelpUpdateApp.Size = new System.Drawing.Size(389, 26);
            this.toolStripMenuItemHelpUpdateApp.Text = "Update app...";
            this.toolStripMenuItemHelpUpdateApp.Click += new System.EventHandler(this.toolStripMenuItemHelpUpdateApp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(386, 6);
            // 
            // toolStripMenuItemHelpVisitApplicationHomeRepository
            // 
            this.toolStripMenuItemHelpVisitApplicationHomeRepository.Name = "toolStripMenuItemHelpVisitApplicationHomeRepository";
            this.toolStripMenuItemHelpVisitApplicationHomeRepository.Size = new System.Drawing.Size(389, 26);
            this.toolStripMenuItemHelpVisitApplicationHomeRepository.Text = "Visit application home repository page...";
            this.toolStripMenuItemHelpVisitApplicationHomeRepository.Click += new System.EventHandler(this.toolStripMenuItemHelpVisitApplicationHomeRepository_Click);
            // 
            // toolStripMenuItemHelpVisitApplicationHomeWiki
            // 
            this.toolStripMenuItemHelpVisitApplicationHomeWiki.Name = "toolStripMenuItemHelpVisitApplicationHomeWiki";
            this.toolStripMenuItemHelpVisitApplicationHomeWiki.Size = new System.Drawing.Size(389, 26);
            this.toolStripMenuItemHelpVisitApplicationHomeWiki.Text = "Visit application home repository wiki page...";
            this.toolStripMenuItemHelpVisitApplicationHomeWiki.Click += new System.EventHandler(this.toolStripMenuItemHelpVisitApplicationHomeWiki_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 727);
            this.Controls.Add(this.gbOuter);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
        private Framework.Controls.XpProgressBar xpProgressBar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileFilterAdmin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelpVisitApplicationHomeRepository;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelpVisitApplicationHomeWiki;
        private System.Windows.Forms.Label lblToolUpdateAvailable;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelpUpdateApp;
    }
}

