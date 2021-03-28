using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using System.Security.Cryptography;
using System.Text;

namespace PodFilterDownloader
{
    // ReSharper disable once InconsistentNaming
    public partial class frmMain : Form
    {
        private string _configFile;
        private IniData _data;
        private readonly FileIniDataParser _parser = new FileIniDataParser();
        private SHA256 _sha256 = SHA256.Create();

        /// <summary>
        /// Compute the file's hash
        /// </summary>
        /// <param name="filename">Path to file from which the sha256 is calculated</param>
        /// <returns>Sha256 value</returns>
        private byte[] GetHashSha256(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                return _sha256.ComputeHash(stream);
            }
        }
        private string BytesToString(byte[] bytes)
        {
            return bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void DownloadFilterFile(string filtername, string url, string author, bool silent)
        {
            if (txtPodInstallationLoc.Text.Trim().Length == 0)
            {
                btnInstallSelected.Enabled = true;
                btnBrowsePoDInstallLoc.Enabled = true;
                if (!silent)
                    MessageBox.Show(@"You need to define the install location", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((_data[filtername].GetKeyData("downloaded_etag").Value ==
                 _data[filtername].GetKeyData("etag").Value) && _data[filtername].GetKeyData("downloaded_content_length").Value ==
                _data[filtername].GetKeyData("content_length").Value)
            {
                if (!silent)
                    if (MessageBox.Show(@"The downloaded file is the same. Do you want to re download it?", @"Info",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        btnInstallSelected.Enabled = true;
                        btnBrowsePoDInstallLoc.Enabled = true;
                        return;
                    }
            }

            if (File.Exists($"{Path.GetTempPath()}\\{author}_{filtername}_item.filter"))
            {
                if (_data[filtername].GetKeyData("downloaded_content_length").Value ==
                    _data[filtername].GetKeyData("content_length").Value)
                {
                    if (!silent)
                        MessageBox.Show(
                        @"Already downloaded filter file was copied to Pod filter directory, as it was the same as previously downloaded.",
                        @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnInstallSelected.Enabled = true;
                    btnBrowsePoDInstallLoc.Enabled = true;
                    DownloadFileFinal(author, filtername);
                    return;
                }

                // File in temp is the same as one in the server, so just copy the file to PoD filter directory
                //File.Copy($"{Path.GetTempPath()}\\{author}_{filtername}_item.filter",
                //$"{txtPodInstallationLoc.Text}\\filter\\{filtername}.filter", true);
            }

            if (!silent)
                using (var wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += DownloadFileCompleted;
                    wc.DownloadFileAsync(new Uri(url), $"{Path.GetTempPath()}\\{author}_{filtername}_item.filter");
                }
            else
            {
                using (var wc = new WebClient())
                {
                    wc.DownloadFile(new Uri(url), $"{Path.GetTempPath()}\\{author}_{filtername}_item.filter");
                }

                DownloadFileFinal(author, filtername);
            }
        }

        private void DownloadFileFinal(string author, string filtername)
        {
            if (author == "")
            {
                if (rbAvailable.Checked && lvFilters.SelectedItems.Count > 0)
                    author = _data[lvFilters.SelectedItems[0].Text].GetKeyData("author").Value;
                else
                    Debug.WriteLine("author was empty");
            }

            if (filtername == "")
            {
                if (rbAvailable.Checked && lvFilters.SelectedItems.Count > 0)
                    filtername = lvFilters.SelectedItems[0].Text;
                else
                    Debug.WriteLine("filtername issue");
            }

            // Update the ETag value for the downloaded file, so it can be later compared to the Etag for the same file in the internet
            var webRequest = WebRequest.Create(_data[filtername].GetKeyData("download_url").Value);
            webRequest.Method = "HEAD";

            KeyData test;

            using (var webResponse = webRequest.GetResponse())
            {
                test = _data[filtername].GetKeyData("downloaded_etag");
                test.Value = webResponse.Headers["ETag"];
                _data[filtername].SetKeyData(test);

                test = _data[filtername].GetKeyData("downloaded_content_length");
                test.Value = webResponse.ContentLength.ToString();
                _data[filtername].SetKeyData(test);
                _parser.WriteFile(_configFile, _data);
            }

            File.Copy($"{Path.GetTempPath()}\\{author}_{filtername}_item.filter",
                $"{txtPodInstallationLoc.Text}\\filter\\{filtername}.filter", true);

            test = _data[filtername].GetKeyData("sha256");
            test.Value = BytesToString(GetHashSha256($"{txtPodInstallationLoc.Text}\\filter\\{filtername}.filter"));
            _data[filtername].SetKeyData(test);
            _parser.WriteFile(_configFile, _data);

            //if (!string.IsNullOrEmpty(_data[filtername].GetKeyData("date_in_server").Value))
            //    File.SetCreationTime($"{txtPodInstallationLoc.Text}\\filter\\{filtername}.filter", 
            //        DateTime.Parse(_data[filtername].GetKeyData("date_in_server").Value));

            test = _data[filtername].GetKeyData("etag");
            test.Value = _data[filtername].GetKeyData("downloaded_etag").Value;
            _data[filtername].SetKeyData(test);

            test = _data[filtername].GetKeyData("content_length");
            test.Value = _data[filtername].GetKeyData("downloaded_content_length").Value;
            _data[filtername].SetKeyData(test);
            _parser.WriteFile(_configFile, _data);

            //MessageBox.Show(@"Loaded and copied filter file to Pod filter directory", @"Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnInstallSelected.Enabled = true;
            btnBrowsePoDInstallLoc.Enabled = true;

            UpdateListview();
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // TODO Handle errors
            //if (e.Cancelled || e.Error != null)
            //{

            //}

            DownloadFileFinal("", "");
        }

        private void btnBrowsePoDInstallLoc_Click(object sender, EventArgs e)
        {
            var browserDialog = new FolderBrowserDialog();

            var result = browserDialog.ShowDialog();

            if (result == DialogResult.OK) txtPodInstallationLoc.Text = browserDialog.SelectedPath.Trim();
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

            txtPodInstallationLoc.Text = 
                Directory.Exists(_data.Global.GetKeyData("PodInstallLocation").Value.Trim()) ? 
                    _data.Global.GetKeyData("PodInstallLocation").Value.Trim() : "";

            UpdateListview();
        }

        private void UpdateListview()
        {
            lvFilters.BeginUpdate();
            lvFilters.Clear();
            lvFilters.View = View.Details;
            lvFilters.FullRowSelect = true;
            lvFilters.Columns.Add("Filter", -1, HorizontalAlignment.Left);
            lvFilters.Columns.Add("State", -1, HorizontalAlignment.Left);
            lvFilters.Columns.Add("Description", -1, HorizontalAlignment.Left);

            foreach (var section in _data.Sections)
            {
                //string author = _data[section.SectionName].GetKeyData("author").Value;
                bool filterExists =
                    File.Exists($"{txtPodInstallationLoc.Text}\\filter\\{section.SectionName}.filter");
                
                if ((rbInstalled.Checked && filterExists) || (rbAvailable.Checked && !filterExists))
                {
                    // Find group or create a new group
                    var found = false;
                    ListViewGroup lvg = null;
                    foreach (var grp in lvFilters.Groups.Cast<ListViewGroup>().Where(grp =>
                        grp.ToString() == _data[section.SectionName].GetKeyData("author").Value))
                    {
                        found = true;
                        lvg = grp;
                        break;
                    }

                    if (!found)
                    {
                        // Group not found, create
                        lvg = new ListViewGroup(_data[section.SectionName].GetKeyData("author").Value);
                        lvFilters.Groups.Add(lvg);
                    }

                    // Add ListViewItem
                    lvFilters.Items.Add(new ListViewItem(
                        new[] { section.SectionName, filterExists ? "Installed" : "Available",
                            _data[section.SectionName].GetKeyData("description").Value },
                        lvg));
                }
            }

            if (lvFilters.Items.Count == 0)
            {
                lvFilters.Columns[0].Width = lvFilters.Columns[1].Width = lvFilters.Columns[2].Width = 200;
                btnInstallSelected.Enabled = btnDownloadUpdatedFilters.Enabled = btnRemoveSelected.Enabled =
                    btnMoreInfoOnSelectedFilter.Enabled = btnInstallSelected.Enabled = false;
            }
            else
            {
                lvFilters.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvFilters.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvFilters.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            lvFilters.EndUpdate();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            var test = _data.Global.GetKeyData("PodInstallLocation");
            test.Value = txtPodInstallationLoc.Text.Trim();
            _parser.WriteFile(_configFile, _data);
        }

        private void toolStripMenuItemHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                $@"Ixoth\'s PoD filter downloader{Environment.NewLine}Version: {Application.ProductVersion}{Environment.NewLine}{Environment.NewLine}Copyright (C) 2021{Environment.NewLine}All rights reserved",
                @"About...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMoreInfoOnSelectedFilter_Click(object sender, EventArgs e)
        {
            if (lvFilters.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"Please select first some filter in the listview!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Process.Start(_data[lvFilters.SelectedItems[0].Text].GetKeyData("home_repo_url").Value);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;

            // Check the ETag values for the filters in servers, so that filters which have been changed can be indicated that there might be new version available

            // TODO in first start phase, when no etags have been yet written to the ini file, check the already downloaded filters and compare the lengths of those to the 
            // lengths at servers

            if (txtPodInstallationLoc.Text.Length > 0 && (txtPodInstallationLoc.Text.Length > 0 && Directory.Exists(txtPodInstallationLoc.Text)))
            {
                foreach (var filter in _data.Sections)
                {
                    //string author = _data[filter.SectionName].GetKeyData("author").Value;
                    if (File.Exists($"{txtPodInstallationLoc.Text}\\filter\\{filter.SectionName}.filter"))
                    {
                        var test = _data[filter.SectionName].GetKeyData("downloaded_content_length");
                        test.Value = new FileInfo($"{txtPodInstallationLoc.Text}\\filter\\{filter.SectionName}.filter").Length.ToString();
                        _data[filter.SectionName].SetKeyData(test);
                        _parser.WriteFile(_configFile, _data);
                    }
                }
            }

            bool updatesFound = false;

            foreach (var filter in _data.Sections)
            {
                if (lvFilters.Items.Count == 0)
                {
                    btnRefresh.Enabled = true;
                    Debug.WriteLine("Empty listview!");
                    return;
                }

                if (rbInstalled.Checked)
                {
                    //string author = _data[filter.SectionName].GetKeyData("author").Value;
                    if (File.Exists($"{txtPodInstallationLoc.Text}\\filter\\{filter.SectionName}.filter"))
                    {
                        var url = _data[filter.SectionName].GetKeyData("download_url").Value;
                        var webRequest = WebRequest.Create(url);
                        webRequest.Method = "HEAD";

                        using (var webResponse = webRequest.GetResponse())
                        {
                            var test = _data[filter.SectionName].GetKeyData("etag");
                            test.Value = webResponse.Headers["ETag"];
                            _data[filter.SectionName].SetKeyData(test);

                            test = _data[filter.SectionName].GetKeyData("content_length");
                            test.Value = webResponse.ContentLength.ToString();
                            _data[filter.SectionName].SetKeyData(test);

                            test = _data[filter.SectionName].GetKeyData("date_in_server");
                            test.Value = webResponse.Headers["Date"];
                            _data[filter.SectionName].SetKeyData(test);
                            _parser.WriteFile(_configFile, _data);

                            if (string.IsNullOrEmpty(_data[filter.SectionName].GetKeyData("etag").Value) &&
                                string.IsNullOrEmpty(_data[filter.SectionName].GetKeyData("downloaded_etag").Value))
                            {
                                // Not all servers support etag, use then the content-length instead (not very good approach!)
                                if (_data[filter.SectionName].GetKeyData("content_length").Value !=
                                    _data[filter.SectionName].GetKeyData("downloaded_content_length").Value)
                                {
                                    lvFilters.FindItemWithText(filter.SectionName).SubItems[1].Text =
                                        @"Update available";
                                    updatesFound = true;
                                }
                            }
                            else
                            {
                                if (_data[filter.SectionName].GetKeyData("etag").Value !=
                                    _data[filter.SectionName].GetKeyData("downloaded_etag").Value)
                                {
                                    lvFilters.FindItemWithText(filter.SectionName).SubItems[1].Text =
                                        @"Update available";
                                    updatesFound = true;
                                }
                            }
                        }
                    }
                }
            }

            if (updatesFound) btnDownloadUpdatedFilters.Enabled = true;
            
            lvFilters.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            btnRefresh.Enabled = true;
        }

        private void btnDownloadUpdatedFilters_Click(object sender, EventArgs e)
        {
            if (!rbInstalled.Checked)
            {
                MessageBox.Show(@"Please check the installed radio button", @"Feature not available",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            btnDownloadUpdatedFilters.Enabled = false;

            bool updatesWereDone = false;

            foreach (ListViewItem item in lvFilters.Items)
            {
                if (item.SubItems[1].Text == @"Update available")
                {
                    DownloadFilterFile(item.Text, _data[item.Text].GetKeyData("download_url").Value, _data[item.Text].GetKeyData("author").Value, true);

                    updatesWereDone = true;
                }
            }

            if (updatesWereDone) timer.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            timer.Enabled = true;
        }

        private void UpdateButtonStates()
        {
            if (rbInstalled.Checked)
            {
                if (lvFilters.SelectedItems.Count == 0)
                {
                    btnRemoveSelected.Enabled = false;
                    btnMoreInfoOnSelectedFilter.Enabled = false;
                }
                else
                {
                    btnRemoveSelected.Enabled = true;
                    btnMoreInfoOnSelectedFilter.Enabled = true;
                }
                btnInstallSelected.Enabled = false;
                btnDownloadUpdatedFilters.Enabled = true;
            }
            else
            {
                if (lvFilters.SelectedItems.Count == 0)
                {
                    btnMoreInfoOnSelectedFilter.Enabled = false;
                    btnInstallSelected.Enabled = false;
                }
                else
                {
                    btnInstallSelected.Enabled = true;
                    btnMoreInfoOnSelectedFilter.Enabled = true;
                }

                btnRemoveSelected.Enabled = false;
                btnDownloadUpdatedFilters.Enabled = false;
            }
        }

        private void rbInstalled_CheckedChanged(object sender, EventArgs e)
        {
            UpdateListview();

            UpdateButtonStates();
        }

        private void btnInstallSelected_Click(object sender, EventArgs e)
        {
            if (lvFilters.SelectedItems.Count == 1)
            {
                btnInstallSelected.Enabled = false;
                DownloadFilterFile(lvFilters.SelectedItems[0].Text, 
                    _data[lvFilters.SelectedItems[0].Text].GetKeyData("download_url").Value, 
                    _data[lvFilters.SelectedItems[0].Text].GetKeyData("author").Value, false);
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (!rbInstalled.Checked)
            {
                MessageBox.Show(@"Please check the installed radio button", @"Feature not available",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (lvFilters.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"Please select first some filter in the listview!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(@"Are you sure you want to remove file: " + lvFilters.SelectedItems[0].Text, @"Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete($"{txtPodInstallationLoc.Text}\\filter\\{lvFilters.SelectedItems[0].Text}.filter");

                UpdateListview();
            }
        }

        private void lvFilters_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            UpdateButtonStates();
        }
    }
}