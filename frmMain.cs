using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace PodFilterDownloader
{
    public partial class frmMain : Form
    {
        private string _configFile;
        private IniData _data;
        private readonly FileIniDataParser _parser = new FileIniDataParser();
        private string _selectedFilter;

        public frmMain()
        {
            InitializeComponent();
        }

        private void DownloadFilterFile(string url)
        {
            if (txtPodInstallationLoc.Text.Trim().Length == 0)
            {
                btnDownloadSelectedFilter.Enabled = true;
                btnBrowsePoDInstallLoc.Enabled = true;
                MessageBox.Show(@"You need to define the install location", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if ((_data[_selectedFilter].GetKeyData("downloaded_etag").Value ==
                 _data[_selectedFilter].GetKeyData("etag").Value) && _data[_selectedFilter].GetKeyData("downloaded_content_length").Value ==
                _data[_selectedFilter].GetKeyData("content_length").Value)
            {
                if (MessageBox.Show(@"The downloaded file is the same. Do you want to re download it?", @"Info",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    btnDownloadSelectedFilter.Enabled = true;
                    btnBrowsePoDInstallLoc.Enabled = true;
                    return;
                }
            }

            if (File.Exists($"{Path.GetTempPath()}\\{lblAuthor.Text}_{_selectedFilter}_item.filter"))
            {
                if (_data[_selectedFilter].GetKeyData("downloaded_content_length").Value ==
                    _data[_selectedFilter].GetKeyData("content_length").Value)
                {
                    MessageBox.Show(
                        @"Already downloaded filter file was copied to Pod filter directory, as it was the same as previously downloaded.",
                        @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnDownloadSelectedFilter.Enabled = true;
                    btnBrowsePoDInstallLoc.Enabled = true;
                    return;
                }

                // File in temp is the same as one in the server, so just copy the file to PoD filter directory
                File.Copy($"{Path.GetTempPath()}\\{lblAuthor.Text}_{_selectedFilter}_item.filter",
                    $"{txtPodInstallationLoc.Text}\\filter\\" + _selectedFilter + ".filter", true);
            }

            using (var wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += DownloadFileCompleted;
                wc.DownloadFileAsync(new Uri(url),
                    $"{Path.GetTempPath()}\\{lblAuthor.Text}_{_selectedFilter}_item.filter");
            }
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Update the ETag value for the downloaded file, so it can be later compared to the Etag for the same file in the internet
            var webRequest =
                WebRequest.Create(
                    _data[_selectedFilter].GetKeyData("download_url")
                        .Value /*AvailFiltersUrls[lbAvailableFilters.SelectedIndex]*/);
            webRequest.Method = "HEAD";

            KeyData test;

            using (var webResponse = webRequest.GetResponse())
            {
                test = _data[_selectedFilter].GetKeyData("downloaded_etag");
                test.Value = webResponse.Headers["ETag"];
                _data[_selectedFilter].SetKeyData(test);
                _parser.WriteFile(_configFile, _data);

                test = _data[_selectedFilter].GetKeyData("downloaded_content_length");
                test.Value = webResponse.ContentLength.ToString();
                _data[_selectedFilter].SetKeyData(test);
                _parser.WriteFile(_configFile, _data);
            }

            File.Copy(Path.GetTempPath() + "\\" + lblAuthor.Text + "_" + _selectedFilter + "_item.filter",
                txtPodInstallationLoc.Text + "\\filter\\" + _selectedFilter + ".filter", true);

            test = _data[_selectedFilter].GetKeyData("etag");
            test.Value = _data[_selectedFilter].GetKeyData("downloaded_etag").Value;
            _data[_selectedFilter].SetKeyData(test);
            _parser.WriteFile(_configFile, _data);

            test = _data[_selectedFilter].GetKeyData("content_length");
            test.Value = _data[_selectedFilter].GetKeyData("downloaded_content_length").Value;
            _data[_selectedFilter].SetKeyData(test);
            _parser.WriteFile(_configFile, _data);

            lblUpdateAvailable.Text = @"No";


            MessageBox.Show(@"Loaded and copied filter file to Pod filter directory", @"Status", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            btnDownloadSelectedFilter.Enabled = true;
            btnBrowsePoDInstallLoc.Enabled = true;
        }

        private void btnBrowsePoDInstallLoc_Click(object sender, EventArgs e)
        {
            var browserDialog = new FolderBrowserDialog();

            var result = browserDialog.ShowDialog();

            if (result == DialogResult.OK) txtPodInstallationLoc.Text = browserDialog.SelectedPath;
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
            _configFile = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? throw new InvalidOperationException()
                , @"Configuration.ini");

            _data = _parser.ReadFile(_configFile);

            foreach (var section in _data.Sections) lbAvailableFilters.Items.Add(section.SectionName);

            lbAvailableFilters.SelectedIndex = int.Parse(_data.Global.GetKeyData("SelectedAvailableFilterIndex").Value);
            _selectedFilter = lbAvailableFilters.GetItemText(lbAvailableFilters.SelectedItem);

            txtPodInstallationLoc.Text = _data.Global.GetKeyData("PodInstallLocation").Value;
            if (lbAvailableFilters.SelectedIndex != -1)
                lblAuthor.Text = _data[_selectedFilter].GetKeyData("author").Value;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            var test = _data.Global.GetKeyData("PodInstallLocation");
            test.Value = txtPodInstallationLoc.Text;
            _data.Global.SetKeyData(test);
            test = _data.Global.GetKeyData("SelectedAvailableFilterIndex");

            test.Value = lbAvailableFilters.SelectedIndex.ToString();
            _data.Global.SetKeyData(test);

            _parser.WriteFile(_configFile, _data);
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
            DownloadFilterFile(_data[_selectedFilter].GetKeyData("download_url").Value);
        }

        private void btnMoreInfoOnSelectedFilter_Click(object sender, EventArgs e)
        {
            if (lbAvailableFilters.SelectedIndex == -1)
            {
                MessageBox.Show(@"Please select first some filter in the listbox!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Process.Start(_data[_selectedFilter].GetKeyData("home_repo_url").Value);
        }

        private void lbAvailableFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFilter = lbAvailableFilters.GetItemText(lbAvailableFilters.SelectedItem);
            lblAuthor.Text = _data[_selectedFilter].GetKeyData("author").Value;
            lblUpdateAvailable.Text = _data[_selectedFilter].GetKeyData("etag").Value != _data[_selectedFilter].GetKeyData("downloaded_etag").Value ? "Yes" : "No";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Check the ETag values for the filters, so that filters which have been changed can be indicated that there might be new version available

            timer.Enabled = false;

            foreach (var filter in _data.Sections)
            {
                var url = _data[filter.SectionName].GetKeyData("download_url").Value;
                var webRequest = WebRequest.Create(url);
                webRequest.Method = "HEAD";

                using (var webResponse = webRequest.GetResponse())
                {
                    var test = _data[filter.SectionName].GetKeyData("etag");
                    test.Value = webResponse.Headers["ETag"];
                    _data[filter.SectionName].SetKeyData(test);
                    _parser.WriteFile(_configFile, _data);

                    test = _data[filter.SectionName].GetKeyData("content_length");
                    test.Value = webResponse.ContentLength.ToString();
                    _data[filter.SectionName].SetKeyData(test);
                    _parser.WriteFile(_configFile, _data);

                    test = _data[filter.SectionName].GetKeyData("date_in_server");
                    test.Value = webResponse.Headers["Date"];
                    _data[filter.SectionName].SetKeyData(test);
                    _parser.WriteFile(_configFile, _data);
                }
            }
        }
    }
}