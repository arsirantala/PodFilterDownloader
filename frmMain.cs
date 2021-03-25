using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using IniParser;
using IniParser.Model;

namespace PodFilterDownloader
{
    public partial class frmMain : Form
    {
        private FileIniDataParser _parser = new FileIniDataParser();
        private IniData _data;
        private string _selectedFilter;

        private string[] _availFilters = new string[0];
        private string[] _availFiltersUrls = new string[0];
        private string[] _availFiltersHomeUrls = new string[0];
        private string[] _availFiltersAuthors = new string[0];
        private int _lastSelectedFilterIndx;
        private List<string> _downloadedFilterEtags = new List<string>();
        private List<string> _availableFilterEtags = new List<string>();

        public string[] AvailFilters { get => _availFilters; set => _availFilters = value; }
        public string[] AvailFiltersUrls { get => _availFiltersUrls; set => _availFiltersUrls = value; }
        public string[] AvailFiltersHomeUrls { get => _availFiltersHomeUrls; set => _availFiltersHomeUrls = value; }
        public string[] AvailFiltersAuthors { get => _availFiltersAuthors; set => _availFiltersAuthors = value; }
        public List<string> DownloadedFilterEtags { get => _downloadedFilterEtags; set => _downloadedFilterEtags = value; }
        public List<string> AvailableFilterEtags { get => _availableFilterEtags; set => _availableFilterEtags = value; }
        public int LastSelectedFilterIndx { get => _lastSelectedFilterIndx; set => _lastSelectedFilterIndx = value; }

        private void DownloadFilterFile(string url)
        {
            /* if (Available_filter_etags[lbAvailableFilters.SelectedIndex] == downloaded_filter_etags[lbAvailableFilters.SelectedIndex])
            {
                if (MessageBox.Show("The downloaded file is the same. Do you want to re download it?", "Info", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    btnDownloadSelectedFilter.Enabled = true;
                    btnBrowsePoDInstallLoc.Enabled = true;
                    return;
                }
            } */

            if (txtPodInstallationLoc.Text.Trim().Length == 0)
            {
                btnDownloadSelectedFilter.Enabled = true;
                btnBrowsePoDInstallLoc.Enabled = true;
                MessageBox.Show(@"You need to define the install location", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists($"{Path.GetTempPath()}\\{lblAuthor.Text}_{_selectedFilter}_item.filter"))
            {
                if (DownloadedFilterEtags[lbAvailableFilters.SelectedIndex] == AvailableFilterEtags[lbAvailableFilters.SelectedIndex])
                {
                    // File in temp is the same as one in the server, so just copy the file to PoD filter directory
                    File.Copy($"{Path.GetTempPath()}\\{lblAuthor.Text}_{_selectedFilter}_item.filter",
                        $"{txtPodInstallationLoc.Text}\\filter\\item.filter", true);
                    MessageBox.Show(@"Already downloaded filter file was copied to Pod filter directory, as it was the same as previously downloaded", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDownloadSelectedFilter.Enabled = true;
                    btnBrowsePoDInstallLoc.Enabled = true;
                    return;
                }
            }

            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += DownloadFileCompleted;
                wc.DownloadFileAsync(new Uri(url),
                    $"{Path.GetTempPath()}\\{lblAuthor.Text}_{_selectedFilter}_item.filter");
                // wc.DownloadFileAsync(new Uri(url), txtPodInstallationLoc.Text + "\\filter\\" + _selectedFilter + ".filter");
            }
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Update the ETag value for the downloaded file, so it can be later compared to the Etag for the same file in the internet
            WebRequest webRequest = WebRequest.Create(AvailFiltersUrls[lbAvailableFilters.SelectedIndex]);
            webRequest.Method = "HEAD";

            using (WebResponse webResponse = webRequest.GetResponse())
            {
                DownloadedFilterEtags[lbAvailableFilters.SelectedIndex] = webResponse.Headers["ETag"];
            }

            File.Copy(Path.GetTempPath() + "\\" + lblAuthor.Text + "_" + _selectedFilter + "_item.filter",
                txtPodInstallationLoc.Text + "\\filter\\item.filter", true);

            MessageBox.Show(@"Loaded and copied filter file to Pod filter directory", @"Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDownloadSelectedFilter.Enabled = true;
            btnBrowsePoDInstallLoc.Enabled = true;
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnBrowsePoDInstallLoc_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();

            DialogResult result = browserDialog.ShowDialog();

            if (result == DialogResult.OK) {
                txtPodInstallationLoc.Text = browserDialog.SelectedPath;
            }
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void toolStripMenuItemFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _selectedFilter = lbAvailableFilters.GetItemText(lbAvailableFilters.SelectedItem);
            txtPodInstallationLoc.Text = Properties.Settings.Default.PoDInstallLocation;
            AvailFilters = Properties.Settings.Default.AvailableFilters_names.Split(',');
            AvailFiltersUrls = Properties.Settings.Default.AvailableFilterUrls.Split(',');
            AvailFiltersHomeUrls = Properties.Settings.Default.AvailableFilterHomeUrls.Split(',');
            AvailFiltersAuthors = Properties.Settings.Default.AvailableFilterAuthors.Split(',');
            string[] temp = Properties.Settings.Default.DownloadedFilterEtags.Split(',');

            foreach (string tt in temp)
            {
                DownloadedFilterEtags.Add(tt);
            }

            foreach (string str in AvailFilters)
            {
                lbAvailableFilters.Items.Add(str);
            }

            lbAvailableFilters.SelectedIndex = Properties.Settings.Default.SelectedAvailableFilterIndex;
            LastSelectedFilterIndx = lbAvailableFilters.SelectedIndex;
            lblAuthor.Text = AvailFiltersAuthors[lbAvailableFilters.SelectedIndex];

            var configFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
                , @"Configuration.ini");

            _data = _parser.ReadFile(configFile);

            //string useFullScreenStr = data["UI"]["fullscreen"];

            //data["UI"]["fullscreen"] = "true";
            //parser.WriteFile("Configuration.ini", data);
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default["PodInstallLocation"] = txtPodInstallationLoc.Text;
            Properties.Settings.Default["SelectedAvailableFilterIndex"] = lbAvailableFilters.SelectedIndex;
            Properties.Settings.Default["DownloadedFilterEtags"] = string.Join(",", _downloadedFilterEtags.ToArray());
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItemHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                $@"PoD filter downloader by Ixoth{Environment.NewLine}Version: {Application.ProductVersion}{Environment.NewLine}{Environment.NewLine}Copyright (C) 2021{Environment.NewLine}All rights reserved", 
                @"About...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDownloadSelectedFilter_Click(object sender, EventArgs e)
        {
            btnDownloadSelectedFilter.Enabled = false;
            DownloadFilterFile(AvailFiltersUrls[lbAvailableFilters.SelectedIndex]);
        }

        private void btnMoreInfoOnSelectedFilter_Click(object sender, EventArgs e)
        {
            if (lbAvailableFilters.SelectedIndex == -1)
            {
                MessageBox.Show(@"Please select first some filter in the listbox!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
      
            System.Diagnostics.Process.Start(AvailFiltersHomeUrls[lbAvailableFilters.SelectedIndex]);
        }

        private void lbAvailableFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFilter = lbAvailableFilters.GetItemText(lbAvailableFilters.SelectedItem);
            lblAuthor.Text = AvailFiltersAuthors[lbAvailableFilters.SelectedIndex];
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Check the ETag values for the filters, so that filters which have been changed can be indicated that there might be new version available

            timer.Enabled = false;

            Boolean first = true;
            foreach(string filter in AvailFiltersUrls)
            {
                WebRequest webRequest = WebRequest.Create(filter);
                webRequest.Method = "HEAD";

                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    if (first)
                    {
                        Properties.Settings.Default["AvailableFilterEtags"] = webResponse.Headers["ETag"];
                        first = false;
                    }
                    else
                    {
                        Properties.Settings.Default["AvailableFilterEtags"] = "," + webResponse.Headers["ETag"];
                    }
                    AvailableFilterEtags.Add(webResponse.Headers["ETag"]);
                }
            }
            Properties.Settings.Default.Save();

            // Check the filter files, if they're different in the server vs what has been downloaded in past (to temp directory)

            /* if (downloaded_filter_etags[Last_selected_filter_indx] != Available_filter_etags[Last_selected_filter_indx])
            {
                lbAvailableFilters.Items[Last_selected_filter_indx + 1] += " (different in server)";
            } */

            //int cntr = 0;
            //int maxLength = lbAvailableFilters.Items.Count;

            //while (cntr < maxLength)
            //{
            //    if (DownloadedFilterEtags[cntr] != AvailableFilterEtags[cntr])
            //    {
            //        lbAvailableFilters.Items[cntr + 1] += " (file in server is different than one in temp)";
            //    }

            //    cntr++;
            //}
        }
    }
}
