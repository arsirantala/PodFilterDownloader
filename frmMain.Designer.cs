namespace PodFilterDownloader
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMoreInfoOnSelectedFilter = new System.Windows.Forms.Button();
            this.btnInstallSelected = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnDownloadUpdatedFilters = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBrowsePoDInstallLoc = new System.Windows.Forms.Button();
            this.txtPodInstallationLoc = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbAvailable = new System.Windows.Forms.RadioButton();
            this.rbInstalled = new System.Windows.Forms.RadioButton();
            this.lvFilters = new System.Windows.Forms.ListView();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemoveSelected);
            this.groupBox1.Controls.Add(this.btnMoreInfoOnSelectedFilter);
            this.groupBox1.Controls.Add(this.btnInstallSelected);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.btnDownloadUpdatedFilters);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.lvFilters);
            this.groupBox1.Location = new System.Drawing.Point(22, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(700, 498);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Click on button to download PoD filter";
            // 
            // btnMoreInfoOnSelectedFilter
            // 
            this.btnMoreInfoOnSelectedFilter.Location = new System.Drawing.Point(198, 291);
            this.btnMoreInfoOnSelectedFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoreInfoOnSelectedFilter.Name = "btnMoreInfoOnSelectedFilter";
            this.btnMoreInfoOnSelectedFilter.Size = new System.Drawing.Size(148, 25);
            this.btnMoreInfoOnSelectedFilter.TabIndex = 4;
            this.btnMoreInfoOnSelectedFilter.Text = "More info on selected filter";
            this.btnMoreInfoOnSelectedFilter.UseVisualStyleBackColor = true;
            this.btnMoreInfoOnSelectedFilter.Click += new System.EventHandler(this.btnMoreInfoOnSelectedFilter_Click);
            // 
            // btnInstallSelected
            // 
            this.btnInstallSelected.Enabled = false;
            this.btnInstallSelected.Location = new System.Drawing.Point(364, 291);
            this.btnInstallSelected.Name = "btnInstallSelected";
            this.btnInstallSelected.Size = new System.Drawing.Size(121, 23);
            this.btnInstallSelected.TabIndex = 5;
            this.btnInstallSelected.Text = "Install selected";
            this.btnInstallSelected.UseVisualStyleBackColor = true;
            this.btnInstallSelected.Click += new System.EventHandler(this.btnInstallSelected_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(25, 338);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(655, 35);
            this.progressBar.TabIndex = 7;
            // 
            // btnDownloadUpdatedFilters
            // 
            this.btnDownloadUpdatedFilters.Location = new System.Drawing.Point(507, 291);
            this.btnDownloadUpdatedFilters.Name = "btnDownloadUpdatedFilters";
            this.btnDownloadUpdatedFilters.Size = new System.Drawing.Size(173, 23);
            this.btnDownloadUpdatedFilters.TabIndex = 6;
            this.btnDownloadUpdatedFilters.Text = "Download updates";
            this.btnDownloadUpdatedFilters.UseVisualStyleBackColor = true;
            this.btnDownloadUpdatedFilters.Click += new System.EventHandler(this.btnDownloadUpdatedFilters_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(605, 55);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBrowsePoDInstallLoc);
            this.groupBox2.Controls.Add(this.txtPodInstallationLoc);
            this.groupBox2.Location = new System.Drawing.Point(25, 406);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(655, 67);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PoD install location";
            // 
            // btnBrowsePoDInstallLoc
            // 
            this.btnBrowsePoDInstallLoc.Location = new System.Drawing.Point(298, 25);
            this.btnBrowsePoDInstallLoc.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowsePoDInstallLoc.Name = "btnBrowsePoDInstallLoc";
            this.btnBrowsePoDInstallLoc.Size = new System.Drawing.Size(56, 25);
            this.btnBrowsePoDInstallLoc.TabIndex = 1;
            this.btnBrowsePoDInstallLoc.Text = "...";
            this.btnBrowsePoDInstallLoc.UseVisualStyleBackColor = true;
            this.btnBrowsePoDInstallLoc.Click += new System.EventHandler(this.btnBrowsePoDInstallLoc_Click);
            // 
            // txtPodInstallationLoc
            // 
            this.txtPodInstallationLoc.Enabled = false;
            this.txtPodInstallationLoc.Location = new System.Drawing.Point(10, 26);
            this.txtPodInstallationLoc.Margin = new System.Windows.Forms.Padding(2);
            this.txtPodInstallationLoc.Name = "txtPodInstallationLoc";
            this.txtPodInstallationLoc.Size = new System.Drawing.Size(266, 20);
            this.txtPodInstallationLoc.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbAvailable);
            this.groupBox3.Controls.Add(this.rbInstalled);
            this.groupBox3.Location = new System.Drawing.Point(25, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(225, 49);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Show filters based of";
            // 
            // rbAvailable
            // 
            this.rbAvailable.AutoSize = true;
            this.rbAvailable.Location = new System.Drawing.Point(104, 19);
            this.rbAvailable.Name = "rbAvailable";
            this.rbAvailable.Size = new System.Drawing.Size(68, 17);
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
            this.rbInstalled.Location = new System.Drawing.Point(13, 19);
            this.rbInstalled.Name = "rbInstalled";
            this.rbInstalled.Size = new System.Drawing.Size(63, 17);
            this.rbInstalled.TabIndex = 0;
            this.rbInstalled.TabStop = true;
            this.rbInstalled.Text = "Installed";
            this.rbInstalled.UseVisualStyleBackColor = true;
            this.rbInstalled.CheckedChanged += new System.EventHandler(this.rbInstalled_CheckedChanged);
            // 
            // lvFilters
            // 
            this.lvFilters.HideSelection = false;
            this.lvFilters.Location = new System.Drawing.Point(25, 97);
            this.lvFilters.Margin = new System.Windows.Forms.Padding(2);
            this.lvFilters.MultiSelect = false;
            this.lvFilters.Name = "lvFilters";
            this.lvFilters.Size = new System.Drawing.Size(655, 179);
            this.lvFilters.TabIndex = 2;
            this.lvFilters.UseCompatibleStateImageBehavior = false;
            this.lvFilters.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvFilters_ItemSelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(742, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFileExit});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // toolStripMenuItemFileExit
            // 
            this.toolStripMenuItemFileExit.Name = "toolStripMenuItemFileExit";
            this.toolStripMenuItemFileExit.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItemFileExit.Text = "Exit";
            this.toolStripMenuItemFileExit.Click += new System.EventHandler(this.toolStripMenuItemFileExit_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHelpAbout});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem2.Text = "Help";
            // 
            // toolStripMenuItemHelpAbout
            // 
            this.toolStripMenuItemHelpAbout.Name = "toolStripMenuItemHelpAbout";
            this.toolStripMenuItemHelpAbout.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItemHelpAbout.Text = "About...";
            this.toolStripMenuItemHelpAbout.Click += new System.EventHandler(this.toolStripMenuItemHelpAbout_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 250;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(25, 291);
            this.btnRemoveSelected.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(148, 25);
            this.btnRemoveSelected.TabIndex = 3;
            this.btnRemoveSelected.Text = "Remove selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 549);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ixoth\'s PoD filter Downloader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBrowsePoDInstallLoc;
        private System.Windows.Forms.TextBox txtPodInstallationLoc;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileExit;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelpAbout;
        private System.Windows.Forms.Button btnMoreInfoOnSelectedFilter;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ListView lvFilters;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbAvailable;
        private System.Windows.Forms.RadioButton rbInstalled;
        private System.Windows.Forms.Button btnDownloadUpdatedFilters;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnInstallSelected;
        private System.Windows.Forms.Button btnRemoveSelected;
    }
}

