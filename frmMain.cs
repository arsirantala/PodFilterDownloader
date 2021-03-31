using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using Microsoft.Win32;

namespace IxothPodFilterDownloader
{
    // ReSharper disable once InconsistentNaming
    public partial class frmMain : Form
    {
        private string _configFile;
        private IniData _data;
        private readonly FileIniDataParser _parser = new FileIniDataParser();
        private SHA256 _sha256 = SHA256.Create();
        private ResourceManager _rm = new ResourceManager(typeof(frmMain));
        CancellationTokenSource _cts = new CancellationTokenSource();

        /// <summary>
        /// Compute the file's hash
        /// </summary>
        /// <param name="filename">Path to file from which the sha256 is calculated</param>
        /// <returns>Sha256 value</returns>
        public byte[] GetHashSha256(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                return _sha256.ComputeHash(stream);
            }
        }

        public string BytesToString(byte[] bytes)
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
                    MessageBox.Show(_rm.GetString("frmMain_You_need_to_define_the_install_location"), 
                        _rm.GetString("frmMain_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((_data[filtername].GetKeyData("downloaded_etag").Value ==
                 _data[filtername].GetKeyData("server_etag").Value) && _data[filtername].GetKeyData("downloaded_content_length").Value ==
                _data[filtername].GetKeyData("server_content_length").Value)
            {
                if (!silent)
                    if (MessageBox.Show(_rm.GetString("frmMain_The_downloaded_file_is_the_same__Do_you_want_to_re_download_it"), _rm.GetString("frmMain_Info"),
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
                    _data[filtername].GetKeyData("server_content_length").Value)
                {
                    if (!silent)
                        MessageBox.Show(
                        _rm.GetString("frmMain_Already_downloaded_filter_file_was_copied_to_Pod_filter_directory__as_it_was_the_same_as_previously_downloaded"),
                        _rm.GetString("frmMain_Info"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnInstallSelected.Enabled = true;
                    btnBrowsePoDInstallLoc.Enabled = true;
                    DownloadFileFinal(author, filtername, silent);
                    return;
                }
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

                DownloadFileFinal(author, filtername, silent);
            }
        }

        private void DownloadFileFinal(string author, string filtername, bool silent)
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

            test = _data[filtername].GetKeyData("downloaded_sha256");
            test.Value = BytesToString(GetHashSha256($"{txtPodInstallationLoc.Text}\\filter\\{filtername}.filter"));
            _data[filtername].SetKeyData(test);

            test = _data[filtername].GetKeyData("server_etag");
            test.Value = _data[filtername].GetKeyData("downloaded_etag").Value;
            _data[filtername].SetKeyData(test);

            test = _data[filtername].GetKeyData("server_content_length");
            test.Value = _data[filtername].GetKeyData("downloaded_content_length").Value;
            _data[filtername].SetKeyData(test);
            _parser.WriteFile(_configFile, _data);

            //MessageBox.Show(@"Loaded and copied filter file to Pod filter directory", @"Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnInstallSelected.Enabled = true;
            btnBrowsePoDInstallLoc.Enabled = true;

            if (!silent)
            {
                var temp = lvFilters.FindItemWithText(filtername);
                lvFilters.Items.Remove(temp);
            }
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // TODO Handle errors
            //if (e.Cancelled || e.Error != null)
            //{

            //}

            DownloadFileFinal("", "", false);
        }

        private void btnBrowsePoDInstallLoc_Click(object sender, EventArgs e)
        {
            var browserDialog = new FolderBrowserDialog();

            browserDialog.SelectedPath = txtPodInstallationLoc.Text.Trim();

            var result = browserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtPodInstallationLoc.Text = browserDialog.SelectedPath.Trim();
                UpdateListview();
                timer.Enabled = true;
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

        private string GetD2InstallLocationFromRegistry()
        {
            string d2LoDInstallPath = "";
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    "Software\\Blizzard Entertainment\\Diablo II"))
                {
                    Object o = key?.GetValue("InstallPath");
                    if (o != null)
                    {
                        d2LoDInstallPath = o.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return d2LoDInstallPath;
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

            if (txtPodInstallationLoc.Text.Length == 0)
            {
                txtPodInstallationLoc.Text = $@"{GetD2InstallLocationFromRegistry()}\Path of Diablo";
            }

            UpdateListview();

            rbAvailable.Text = _rm.GetString("frmMain_Available");
            rbInstalled.Text = _rm.GetString("frmMain_Installed");
            gbPoDInstallLocation.Text = _rm.GetString("frmMain_gbPoD_Install_Location");
            gbInstalled_Available.Text = _rm.GetString("frmMain_gbInstalled_Available");
            gbOuter.Text = _rm.GetString("frmMain_gbOuter");
            btnRefresh.Text = _rm.GetString("frmMain_btnRefresh");
            btnRemoveSelected.Text = _rm.GetString("frmMain_btnRemove_Selected");
            btnMoreInfoOnSelectedFilter.Text = _rm.GetString("frmMain_btnMore_Info_On_Selected_Filter");
            btnInstallSelected.Text = _rm.GetString("frmMain_btnInstall_Selected");
            btnDownloadUpdatedFilters.Text = _rm.GetString("frmMain_btnDownload_updates");
            toolStripMenuItemFile.Text = _rm.GetString("frmMain_File_Menu");
            toolStripMenuItemHelp.Text = _rm.GetString("frmMain_Help_Menu");
            toolStripMenuItemFileExit.Text = _rm.GetString("frmMain_File_Exit_Menuitem");
            toolStripMenuItemHelpAbout.Text = _rm.GetString("frmMain_About");
            btnCancel.Text = _rm.GetString("frmMain_Cancel");
        }

        private void UpdateListview()
        {
            lvFilters.BeginUpdate();
            lvFilters.Clear();
            lvFilters.View = View.Details;
            lvFilters.FullRowSelect = true;
            lvFilters.CheckBoxes = rbInstalled.Checked;
            lvFilters.Columns.Add(_rm.GetString("frmMain_Filter"), -1, HorizontalAlignment.Left);
            lvFilters.Columns.Add(_rm.GetString("frmMain_State"), -1, HorizontalAlignment.Left);
            lvFilters.Columns.Add(_rm.GetString("frmMain_Description"), -1, HorizontalAlignment.Left);

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
                        new[] { section.SectionName, filterExists ? _rm.GetString("frmMain_Installed") : _rm.GetString("frmMain_Available"),
                            _data[section.SectionName].GetKeyData("description").Value }, lvg));
                    lvFilters.Items[lvFilters.Items.Count - 1].Tag = section.SectionName;
                    lvFilters.Items[lvFilters.Items.Count - 1].Checked = bool.Parse(_data[section.SectionName].GetKeyData("selected_for_updates").Value);
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

            foreach (ListViewItem lvFiltersItem in lvFilters.Items)
            {
                test = _data[lvFiltersItem.Text].GetKeyData("selected_for_updates");
                test.Value = lvFiltersItem.Checked.ToString();
                _data[lvFiltersItem.Text].SetKeyData(test);
            }

            _parser.WriteFile(_configFile, _data);
        }

        private void toolStripMenuItemHelpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                $"{_rm.GetString("frmMain_About_Message")}{Environment.NewLine}{_rm.GetString("frmMain_Version")}: {Application.ProductVersion}{Environment.NewLine}{Environment.NewLine}{_rm.GetString("frmMain_Copyright")} 2021{Environment.NewLine}{_rm.GetString("frmMain_AllRightsReserved")}",
                _rm.GetString("frmMain_About"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMoreInfoOnSelectedFilter_Click(object sender, EventArgs e)
        {
            if (lvFilters.SelectedItems.Count == 0)
            {
                MessageBox.Show(_rm.GetString("frmMain_No_Filter_Selected_In_LV"), _rm.GetString("frmMain_Error"), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Process.Start(_data[lvFilters.SelectedItems[0].Text].GetKeyData("home_repo_url").Value);
        }

        private void ReportProgress(object sender, ProgressReportModel e)
        {
            progressBar.Value = e.PercentageComplete;
            //PrintResults(e.SitesDownloaded);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;

            if (txtPodInstallationLoc.Text.Length > 0 && 
                (txtPodInstallationLoc.Text.Length > 0 && 
                 Directory.Exists(txtPodInstallationLoc.Text)))
            {
                foreach (var filter in _data.Sections)
                {
                    if (File.Exists($"{txtPodInstallationLoc.Text}\\filter\\{filter.SectionName}.filter"))
                    {
                        var test = _data[filter.SectionName].GetKeyData("installed_content_length");
                        test.Value = new FileInfo($"{txtPodInstallationLoc.Text}\\filter\\{filter.SectionName}.filter").Length.ToString();
                        _data[filter.SectionName].SetKeyData(test);

                        test = _data[filter.SectionName].GetKeyData("installed_sha256");
                        test.Value = BytesToString(GetHashSha256($"{txtPodInstallationLoc.Text}\\filter\\{filter.SectionName}.filter"));
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
                    if (File.Exists($"{txtPodInstallationLoc.Text}\\filter\\{filter.SectionName}.filter"))
                    {
                        var url = _data[filter.SectionName].GetKeyData("download_url").Value;
                        var webRequest = WebRequest.Create(url);
                        webRequest.Method = "HEAD";

                        using (var webResponse = webRequest.GetResponse())
                        {
                            var test = _data[filter.SectionName].GetKeyData("server_etag");
                            test.Value = webResponse.Headers["ETag"];
                            _data[filter.SectionName].SetKeyData(test);

                            test = _data[filter.SectionName].GetKeyData("server_content_length");
                            test.Value = webResponse.ContentLength.ToString();
                            _data[filter.SectionName].SetKeyData(test);

                            _parser.WriteFile(_configFile, _data);

                            if (string.IsNullOrEmpty(_data[filter.SectionName].GetKeyData("server_etag").Value) &&
                                string.IsNullOrEmpty(_data[filter.SectionName].GetKeyData("downloaded_etag").Value))
                            {
                                // Not all servers support etag, use then the content-length instead (not very good approach!)
                                if (!string.IsNullOrEmpty(_data[filter.SectionName].GetKeyData("downloaded_sha256").Value) &&
                                    !string.IsNullOrEmpty(_data[filter.SectionName].GetKeyData("installed_sha256").Value))
                                {
                                    if (_data[filter.SectionName].GetKeyData("downloaded_sha256").Value !=
                                        _data[filter.SectionName].GetKeyData("installed_sha256").Value)
                                    {
                                        lvFilters.FindItemWithText(filter.SectionName).SubItems[1].Text =
                                            _rm.GetString("frmMain_Update_available");
                                        updatesFound = true;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(_data[filter.SectionName].GetKeyData("server_content_length").Value) &&
                                        !string.IsNullOrEmpty(_data[filter.SectionName].GetKeyData("downloaded_content_length").Value))
                                    {
                                        if (_data[filter.SectionName].GetKeyData("server_content_length").Value !=
                                            _data[filter.SectionName].GetKeyData("downloaded_content_length").Value)
                                        {
                                            lvFilters.FindItemWithText(filter.SectionName).SubItems[1].Text =
                                                _rm.GetString("frmMain_Update_available");
                                            updatesFound = true;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Write("oops");
                                    }
                                }
                            }
                            else
                            {
                                if (_data[filter.SectionName].GetKeyData("server_etag").Value !=
                                    _data[filter.SectionName].GetKeyData("downloaded_etag").Value)
                                {
                                    lvFilters.FindItemWithText(filter.SectionName).SubItems[1].Text =
                                        _rm.GetString("frmMain_Update_available");
                                    updatesFound = true;
                                }
                                else
                                {
                                    lvFilters.FindItemWithText(filter.SectionName).SubItems[1].Text =
                                        _rm.GetString("frmMain_Installed");
                                }
                            }
                        }
                    }
                }
            }

            btnDownloadUpdatedFilters.Enabled = updatesFound;
            
            lvFilters.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            btnRefresh.Enabled = true;
        }

        private async void btnDownloadUpdatedFilters_Click(object sender, EventArgs e)
        {
            if (!rbInstalled.Checked)
            {
                MessageBox.Show(_rm.GetString("frmMain_Please_check_the_installed_radio_button"), _rm.GetString("frmMain_Feature_not_available"),
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (lvFilters.CheckedItems.Count == 0)
            {
                MessageBox.Show(_rm.GetString("frmMain_Click_Please_use_checkboxes_to_select_some_filtes_you_want_to_update_first"), _rm.GetString("frmMain_Feature_not_available"),
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            btnDownloadUpdatedFilters.Enabled = false;

            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            List<string> filters = new List<string>();
            foreach (ListViewItem lvFiltersItem in lvFilters.Items)
            {
                if (lvFiltersItem.SubItems[1].Text == _rm.GetString("frmMain_Update_available"))
                {
                    if (lvFiltersItem.Checked)
                        filters.Add(lvFiltersItem.Text);
                }
            }

            var results =
                await UpdateAndDownload.RunGetFilterContentInParallelAsync(progress, _data,
                    txtPodInstallationLoc.Text, filters);
            
            bool updatesWereDone = false;

            foreach (var result in results)
            {
                File.WriteAllText($"{txtPodInstallationLoc.Text}\\filter\\{result.FilterName}.filter", result.Content);
                var test = _data[result.FilterName].GetKeyData("server_etag");
                test.Value = result.ETag;
                _data[result.FilterName].SetKeyData(test);

                test = _data[result.FilterName].GetKeyData("server_content_length");
                test.Value = result.ContentLength;
                _data[result.FilterName].SetKeyData(test);

                // TODO downloaded_etag, downloaded_sha256, installed_sha256, downloaded_content_length,
                // installed_content_length, server_content_length

                _parser.WriteFile(_configFile, _data);

                updatesWereDone = true;
            }

            //foreach (ListViewItem item in lvFilters.Items)
            //{
            //    if (item.SubItems[1].Text == _rm.GetString("frmMain_Update_available"))
            //    {
            //        DownloadFilterFile(item.Text, _data[item.Text].GetKeyData("download_url").Value, _data[item.Text].GetKeyData("author").Value, true);

            //        updatesWereDone = true;
            //    }
            //}

            if (updatesWereDone) timer.Enabled = true;
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;

            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var results = 
                await UpdateAndDownload.RunGetFilterHttpHeadersInParallelAsync(progress, _data,
                    txtPodInstallationLoc.Text);

            foreach (var result in results)
            {
                var test = _data[result.FilterName].GetKeyData("server_etag");
                test.Value = result.ETag;
                _data[result.FilterName].SetKeyData(test);

                test = _data[result.FilterName].GetKeyData("server_content_length");
                test.Value = result.ContentLength;
                _data[result.FilterName].SetKeyData(test);

                _parser.WriteFile(_configFile, _data);
            }

            // TODO do rest of the logic which is currently done in timer function

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
                //btnDownloadUpdatedFilters.Enabled = true;
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
                //btnDownloadUpdatedFilters.Enabled = false;
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
                timer.Enabled = true;
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (!rbInstalled.Checked)
            {
                MessageBox.Show(_rm.GetString("frmMain_Please_check_the_installed_radio_button"), _rm.GetString("frmMain_Feature_not_available"),
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            if (lvFilters.SelectedItems.Count == 0)
            {
                MessageBox.Show(_rm.GetString("frmMain_No_Filter_Selected_In_LV"), _rm.GetString("frmMain_Error"), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"{_rm.GetString("frmMain_Are_you_sure_you_want_to_remove_file")} {lvFilters.SelectedItems[0].Text}?", _rm.GetString("frmMain_Confirmation"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete($"{txtPodInstallationLoc.Text}\\filter\\{lvFilters.SelectedItems[0].Text}.filter");

                //UpdateListview();
                var temp = lvFilters.FindItemWithText(lvFilters.SelectedItems[0].Text);
                lvFilters.Items.Remove(temp);
            }
        }

        private void lvFilters_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            UpdateButtonStates();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            timer.Enabled = true;

            // TODO do the timer logic in here and later the timer logic will be done only when user
            // presses the refresh button (at later time remove timer entirely)
        }
    }
}