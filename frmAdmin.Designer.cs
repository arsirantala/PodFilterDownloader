
namespace IxothPodFilterDownloader
{
    partial class frmAdmin
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFilterDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtFilterAuthor = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.btnDeleteFilter = new System.Windows.Forms.Button();
            this.txtFilterHomeUrl = new System.Windows.Forms.TextBox();
            this.lblFilterHomeUrl = new System.Windows.Forms.Label();
            this.txtFilterDownloadUrl = new System.Windows.Forms.TextBox();
            this.lblFilterDownloadUrl = new System.Windows.Forms.Label();
            this.txtFilterName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnSaveFilterChanges = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbFilters = new System.Windows.Forms.ListBox();
            this.btnRestoreDefaultsFromInternet = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRestoreDefaultsFromInternet);
            this.groupBox1.Controls.Add(this.txtFilterDescription);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.txtFilterAuthor);
            this.groupBox1.Controls.Add(this.lblAuthor);
            this.groupBox1.Controls.Add(this.btnAddFilter);
            this.groupBox1.Controls.Add(this.btnDeleteFilter);
            this.groupBox1.Controls.Add(this.txtFilterHomeUrl);
            this.groupBox1.Controls.Add(this.lblFilterHomeUrl);
            this.groupBox1.Controls.Add(this.txtFilterDownloadUrl);
            this.groupBox1.Controls.Add(this.lblFilterDownloadUrl);
            this.groupBox1.Controls.Add(this.txtFilterName);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.btnSaveFilterChanges);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbFilters);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1057, 475);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filters which are supported";
            // 
            // txtFilterDescription
            // 
            this.txtFilterDescription.Location = new System.Drawing.Point(269, 237);
            this.txtFilterDescription.Name = "txtFilterDescription";
            this.txtFilterDescription.Size = new System.Drawing.Size(762, 30);
            this.txtFilterDescription.TabIndex = 7;
            this.txtFilterDescription.Validating += new System.ComponentModel.CancelEventHandler(this.txtFilterDescription_Validating);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(266, 208);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(109, 25);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Description";
            // 
            // txtFilterAuthor
            // 
            this.txtFilterAuthor.Location = new System.Drawing.Point(271, 107);
            this.txtFilterAuthor.Name = "txtFilterAuthor";
            this.txtFilterAuthor.Size = new System.Drawing.Size(762, 30);
            this.txtFilterAuthor.TabIndex = 3;
            this.txtFilterAuthor.Validating += new System.ComponentModel.CancelEventHandler(this.txtFilterAuthor_Validating);
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(268, 78);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(70, 25);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author";
            // 
            // btnAddFilter
            // 
            this.btnAddFilter.Location = new System.Drawing.Point(559, 414);
            this.btnAddFilter.Name = "btnAddFilter";
            this.btnAddFilter.Size = new System.Drawing.Size(118, 41);
            this.btnAddFilter.TabIndex = 13;
            this.btnAddFilter.Text = "Add filter";
            this.btnAddFilter.UseVisualStyleBackColor = true;
            this.btnAddFilter.Click += new System.EventHandler(this.btnAddFilter_Click);
            // 
            // btnDeleteFilter
            // 
            this.btnDeleteFilter.Location = new System.Drawing.Point(273, 414);
            this.btnDeleteFilter.Name = "btnDeleteFilter";
            this.btnDeleteFilter.Size = new System.Drawing.Size(119, 41);
            this.btnDeleteFilter.TabIndex = 12;
            this.btnDeleteFilter.Text = "Delete filter";
            this.btnDeleteFilter.UseVisualStyleBackColor = true;
            this.btnDeleteFilter.Click += new System.EventHandler(this.btnDeleteFilter_Click);
            // 
            // txtFilterHomeUrl
            // 
            this.txtFilterHomeUrl.Location = new System.Drawing.Point(271, 374);
            this.txtFilterHomeUrl.Name = "txtFilterHomeUrl";
            this.txtFilterHomeUrl.Size = new System.Drawing.Size(762, 30);
            this.txtFilterHomeUrl.TabIndex = 11;
            this.txtFilterHomeUrl.Validating += new System.ComponentModel.CancelEventHandler(this.txtFilterHomeUrl_Validating);
            // 
            // lblFilterHomeUrl
            // 
            this.lblFilterHomeUrl.AutoSize = true;
            this.lblFilterHomeUrl.Location = new System.Drawing.Point(268, 345);
            this.lblFilterHomeUrl.Name = "lblFilterHomeUrl";
            this.lblFilterHomeUrl.Size = new System.Drawing.Size(93, 25);
            this.lblFilterHomeUrl.TabIndex = 10;
            this.lblFilterHomeUrl.Text = "Home Url";
            // 
            // txtFilterDownloadUrl
            // 
            this.txtFilterDownloadUrl.Location = new System.Drawing.Point(271, 301);
            this.txtFilterDownloadUrl.Name = "txtFilterDownloadUrl";
            this.txtFilterDownloadUrl.Size = new System.Drawing.Size(762, 30);
            this.txtFilterDownloadUrl.TabIndex = 9;
            this.txtFilterDownloadUrl.Validating += new System.ComponentModel.CancelEventHandler(this.txtFilterDownloadUrl_Validating);
            // 
            // lblFilterDownloadUrl
            // 
            this.lblFilterDownloadUrl.AutoSize = true;
            this.lblFilterDownloadUrl.Location = new System.Drawing.Point(268, 272);
            this.lblFilterDownloadUrl.Name = "lblFilterDownloadUrl";
            this.lblFilterDownloadUrl.Size = new System.Drawing.Size(128, 25);
            this.lblFilterDownloadUrl.TabIndex = 8;
            this.lblFilterDownloadUrl.Text = "Download Url";
            // 
            // txtFilterName
            // 
            this.txtFilterName.Location = new System.Drawing.Point(271, 174);
            this.txtFilterName.Name = "txtFilterName";
            this.txtFilterName.Size = new System.Drawing.Size(762, 30);
            this.txtFilterName.TabIndex = 5;
            this.txtFilterName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFilterName_Validating);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(268, 145);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(64, 25);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name";
            // 
            // btnSaveFilterChanges
            // 
            this.btnSaveFilterChanges.Location = new System.Drawing.Point(836, 414);
            this.btnSaveFilterChanges.Name = "btnSaveFilterChanges";
            this.btnSaveFilterChanges.Size = new System.Drawing.Size(197, 41);
            this.btnSaveFilterChanges.TabIndex = 14;
            this.btnSaveFilterChanges.Text = "Save filter changes";
            this.btnSaveFilterChanges.UseVisualStyleBackColor = true;
            this.btnSaveFilterChanges.Click += new System.EventHandler(this.btnSaveFilterChanges_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filters";
            // 
            // lbFilters
            // 
            this.lbFilters.FormattingEnabled = true;
            this.lbFilters.ItemHeight = 25;
            this.lbFilters.Location = new System.Drawing.Point(18, 78);
            this.lbFilters.Name = "lbFilters";
            this.lbFilters.Size = new System.Drawing.Size(235, 329);
            this.lbFilters.TabIndex = 1;
            this.lbFilters.SelectedIndexChanged += new System.EventHandler(this.lbFilters_SelectedIndexChanged);
            // 
            // btnRestoreDefaultsFromInternet
            // 
            this.btnRestoreDefaultsFromInternet.Location = new System.Drawing.Point(18, 414);
            this.btnRestoreDefaultsFromInternet.Name = "btnRestoreDefaultsFromInternet";
            this.btnRestoreDefaultsFromInternet.Size = new System.Drawing.Size(235, 41);
            this.btnRestoreDefaultsFromInternet.TabIndex = 15;
            this.btnRestoreDefaultsFromInternet.Text = "Restore defaults";
            this.btnRestoreDefaultsFromInternet.UseVisualStyleBackColor = true;
            this.btnRestoreDefaultsFromInternet.Click += new System.EventHandler(this.btnRestoreDefaultsFromInternet_Click);
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 512);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter administration";
            this.Shown += new System.EventHandler(this.frmAdmin_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFilterHomeUrl;
        private System.Windows.Forms.Label lblFilterHomeUrl;
        private System.Windows.Forms.TextBox txtFilterDownloadUrl;
        private System.Windows.Forms.Label lblFilterDownloadUrl;
        private System.Windows.Forms.TextBox txtFilterName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnSaveFilterChanges;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbFilters;
        private System.Windows.Forms.Button btnDeleteFilter;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.TextBox txtFilterAuthor;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtFilterDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnRestoreDefaultsFromInternet;
    }
}