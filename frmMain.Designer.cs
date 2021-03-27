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
            this.lblUpdateAvailable = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMoreInfoOnSelectedFilter = new System.Windows.Forms.Button();
            this.btnDownloadSelectedFilter = new System.Windows.Forms.Button();
            this.lbAvailableFilters = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBrowsePoDInstallLoc = new System.Windows.Forms.Button();
            this.txtPodInstallationLoc = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lvFilters = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbAvailable = new System.Windows.Forms.RadioButton();
            this.rbInstalled = new System.Windows.Forms.RadioButton();
            this.btnDownloadUpdatedFilters = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblUpdateAvailable);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblAuthor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnMoreInfoOnSelectedFilter);
            this.groupBox1.Controls.Add(this.btnDownloadSelectedFilter);
            this.groupBox1.Controls.Add(this.lbAvailableFilters);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(22, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(700, 265);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Click on button to download PoD filter";
            // 
            // lblUpdateAvailable
            // 
            this.lblUpdateAvailable.Location = new System.Drawing.Point(446, 93);
            this.lblUpdateAvailable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateAvailable.Name = "lblUpdateAvailable";
            this.lblUpdateAvailable.Size = new System.Drawing.Size(53, 14);
            this.lblUpdateAvailable.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(346, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Update available";
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new System.Drawing.Point(422, 28);
            this.lblAuthor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(77, 14);
            this.lblAuthor.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Author";
            // 
            // btnMoreInfoOnSelectedFilter
            // 
            this.btnMoreInfoOnSelectedFilter.Location = new System.Drawing.Point(531, 89);
            this.btnMoreInfoOnSelectedFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnMoreInfoOnSelectedFilter.Name = "btnMoreInfoOnSelectedFilter";
            this.btnMoreInfoOnSelectedFilter.Size = new System.Drawing.Size(148, 25);
            this.btnMoreInfoOnSelectedFilter.TabIndex = 2;
            this.btnMoreInfoOnSelectedFilter.Text = "More info on selected filter";
            this.btnMoreInfoOnSelectedFilter.UseVisualStyleBackColor = true;
            this.btnMoreInfoOnSelectedFilter.Click += new System.EventHandler(this.btnMoreInfoOnSelectedFilter_Click);
            // 
            // btnDownloadSelectedFilter
            // 
            this.btnDownloadSelectedFilter.Location = new System.Drawing.Point(531, 28);
            this.btnDownloadSelectedFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnDownloadSelectedFilter.Name = "btnDownloadSelectedFilter";
            this.btnDownloadSelectedFilter.Size = new System.Drawing.Size(148, 25);
            this.btnDownloadSelectedFilter.TabIndex = 1;
            this.btnDownloadSelectedFilter.Text = "Download selected filter";
            this.btnDownloadSelectedFilter.UseVisualStyleBackColor = true;
            this.btnDownloadSelectedFilter.Click += new System.EventHandler(this.btnDownloadSelectedFilter_Click);
            // 
            // lbAvailableFilters
            // 
            this.lbAvailableFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAvailableFilters.FormattingEnabled = true;
            this.lbAvailableFilters.ItemHeight = 20;
            this.lbAvailableFilters.Location = new System.Drawing.Point(25, 28);
            this.lbAvailableFilters.Margin = new System.Windows.Forms.Padding(2);
            this.lbAvailableFilters.Name = "lbAvailableFilters";
            this.lbAvailableFilters.Size = new System.Drawing.Size(301, 84);
            this.lbAvailableFilters.TabIndex = 0;
            this.lbAvailableFilters.SelectedIndexChanged += new System.EventHandler(this.lbAvailableFilters_SelectedIndexChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(25, 131);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(655, 35);
            this.progressBar.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBrowsePoDInstallLoc);
            this.groupBox2.Controls.Add(this.txtPodInstallationLoc);
            this.groupBox2.Location = new System.Drawing.Point(25, 182);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(655, 67);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PoD install location";
            // 
            // btnBrowsePoDInstallLoc
            // 
            this.btnBrowsePoDInstallLoc.Location = new System.Drawing.Point(298, 25);
            this.btnBrowsePoDInstallLoc.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowsePoDInstallLoc.Name = "btnBrowsePoDInstallLoc";
            this.btnBrowsePoDInstallLoc.Size = new System.Drawing.Size(56, 25);
            this.btnBrowsePoDInstallLoc.TabIndex = 2;
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
            this.txtPodInstallationLoc.TabIndex = 1;
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
            this.menuStrip1.Size = new System.Drawing.Size(751, 24);
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
            // lvFilters
            // 
            this.lvFilters.HideSelection = false;
            this.lvFilters.Location = new System.Drawing.Point(22, 361);
            this.lvFilters.Margin = new System.Windows.Forms.Padding(2);
            this.lvFilters.MultiSelect = false;
            this.lvFilters.Name = "lvFilters";
            this.lvFilters.Size = new System.Drawing.Size(696, 193);
            this.lvFilters.TabIndex = 3;
            this.lvFilters.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbAvailable);
            this.groupBox3.Controls.Add(this.rbInstalled);
            this.groupBox3.Location = new System.Drawing.Point(22, 307);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(225, 49);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Show filters based of";
            // 
            // rbAvailable
            // 
            this.rbAvailable.AutoSize = true;
            this.rbAvailable.Location = new System.Drawing.Point(104, 19);
            this.rbAvailable.Name = "rbAvailable";
            this.rbAvailable.Size = new System.Drawing.Size(68, 17);
            this.rbAvailable.TabIndex = 10;
            this.rbAvailable.Text = "Available";
            this.rbAvailable.UseVisualStyleBackColor = true;
            // 
            // rbInstalled
            // 
            this.rbInstalled.AutoSize = true;
            this.rbInstalled.Checked = true;
            this.rbInstalled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbInstalled.Location = new System.Drawing.Point(13, 19);
            this.rbInstalled.Name = "rbInstalled";
            this.rbInstalled.Size = new System.Drawing.Size(63, 17);
            this.rbInstalled.TabIndex = 9;
            this.rbInstalled.TabStop = true;
            this.rbInstalled.Text = "Installed";
            this.rbInstalled.UseVisualStyleBackColor = true;
            // 
            // btnDownloadUpdatedFilters
            // 
            this.btnDownloadUpdatedFilters.Location = new System.Drawing.Point(545, 574);
            this.btnDownloadUpdatedFilters.Name = "btnDownloadUpdatedFilters";
            this.btnDownloadUpdatedFilters.Size = new System.Drawing.Size(173, 23);
            this.btnDownloadUpdatedFilters.TabIndex = 10;
            this.btnDownloadUpdatedFilters.Text = "Download updates";
            this.btnDownloadUpdatedFilters.UseVisualStyleBackColor = true;
            this.btnDownloadUpdatedFilters.Click += new System.EventHandler(this.btnDownloadUpdatedFilters_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(643, 320);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 609);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDownloadUpdatedFilters);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lvFilters);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PoD filter downloader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Button btnDownloadSelectedFilter;
        private System.Windows.Forms.ListBox lbAvailableFilters;
        private System.Windows.Forms.Button btnMoreInfoOnSelectedFilter;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblUpdateAvailable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvFilters;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbAvailable;
        private System.Windows.Forms.RadioButton rbInstalled;
        private System.Windows.Forms.Button btnDownloadUpdatedFilters;
        private System.Windows.Forms.Button btnRefresh;
    }
}

