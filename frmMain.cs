using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using IxothPodFilterDownloader.Models;

namespace IxothPodFilterDownloader
{
    // ReSharper disable once InconsistentNaming
    public partial class frmMain : Form
    {
        private string _configFile;
        private IniData _data;
        private readonly FileIniDataParser _parser = new FileIniDataParser();
        private readonly ResourceManager _rm = new ResourceManager(typeof(frmMain));
        readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private FileSystemWatcher _fsw;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnBrowsePoDInstallLoc_Click(object sender, EventArgs e)
        {
            var browserDialog = new FolderBrowserDialog {SelectedPath = txtPodInstallationLoc.Text.Trim()};

            var result = browserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtPodInstallationLoc.Text = browserDialog.SelectedPath.Trim();
                UpdateListview();
                timer.Enabled = true;
            }
        }

        private void toolStripMenuItemFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Populate the listview content
        /// </summary>
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

            // persist the checkbox states on the filter files
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
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;

            PersistInstalledFiltersSha256AndContentLength();

            bool updatesFound = false;

            if (lvFilters.Items.Count == 0 || !rbInstalled.Checked)
            {
                btnRefresh.Enabled = true;
                Debug.WriteLine("Empty listview!");
                return;
            }

            // Update server_etag and server_content_length for installed filters
            PersistServerETagAndContentLengthOfInstalledFilters();

            // Compare etag of prev downloaded and server's etag and if they both exist and are different: update is avail for filter
            //
            // If no etags were found (downloaded and server's), then compare instead content lengths and sha256 values instead -
            // If sha256 value are found and content lengths, then compare those. If sha256 values and content lengths differ: update avail
            foreach (ListViewItem lvFiltersItem in lvFilters.Items)
            {
                if (File.Exists($"{txtPodInstallationLoc.Text}\\filter\\{lvFiltersItem.Text}.filter"))
                {
                    if (string.IsNullOrEmpty(_data[lvFiltersItem.Text].GetKeyData("server_etag").Value) &&
                                                string.IsNullOrEmpty(_data[lvFiltersItem.Text].GetKeyData("downloaded_etag").Value))
                    {
                        // Not all servers support etag, use then the content-length instead (not very good approach!)
                        if (!string.IsNullOrEmpty(_data[lvFiltersItem.Text].GetKeyData("downloaded_sha256").Value) &&
                            !string.IsNullOrEmpty(_data[lvFiltersItem.Text].GetKeyData("installed_sha256").Value))
                        {
                            if (_data[lvFiltersItem.Text].GetKeyData("downloaded_sha256").Value !=
                                _data[lvFiltersItem.Text].GetKeyData("installed_sha256").Value)
                            {
                                lvFilters.FindItemWithText(lvFiltersItem.Text).SubItems[1].Text =
                                    _rm.GetString("frmMain_Update_available");
                                updatesFound = true;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_data[lvFiltersItem.Text].GetKeyData("server_content_length").Value) &&
                                !string.IsNullOrEmpty(_data[lvFiltersItem.Text].GetKeyData("downloaded_content_length").Value))
                            {
                                if (_data[lvFiltersItem.Text].GetKeyData("server_content_length").Value !=
                                    _data[lvFiltersItem.Text].GetKeyData("downloaded_content_length").Value)
                                {
                                    lvFilters.FindItemWithText(lvFiltersItem.Text).SubItems[1].Text =
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
                        if (_data[lvFiltersItem.Text].GetKeyData("server_etag").Value !=
                            _data[lvFiltersItem.Text].GetKeyData("downloaded_etag").Value)
                        {
                            lvFilters.FindItemWithText(lvFiltersItem.Text).SubItems[1].Text =
                                _rm.GetString("frmMain_Update_available");
                            updatesFound = true;
                        }
                        else
                        {
                            lvFilters.FindItemWithText(lvFiltersItem.Text).SubItems[1].Text =
                                _rm.GetString("frmMain_Installed");
                        }
                    }
                }
            }

            btnDownloadUpdatedFilters.Enabled = updatesFound;
            
            lvFilters.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            btnRefresh.Enabled = true;
        }

        private void PersistInstalledFiltersSha256AndContentLength(string filtername)
        {
            if (File.Exists($"{txtPodInstallationLoc.Text}\\filter\\{filtername}.filter"))
            {
                PersistInstalledFiltersSha256AndContentLengthHelper(filtername);
            }
        }

        private void PersistInstalledFiltersSha256AndContentLengthHelper(string filtername)
        {
            string temp = filtername;
            if (!filtername.ToLower().Contains(".filter"))
            {
                temp += ".filter";
            }
            var test = _data[filtername.Replace(".filter", "")].GetKeyData("installed_content_length");
            test.Value = File.ReadAllText($"{txtPodInstallationLoc.Text}\\filter\\{temp}").Length.ToString();
            _data[filtername].SetKeyData(test);

            test = _data[filtername.Replace(".filter", "")].GetKeyData("installed_sha256");
            test.Value = Utils.BytesToString(Utils.GetHashSha256($"{txtPodInstallationLoc.Text}\\filter\\{temp}"));
            _data[filtername].SetKeyData(test);
            _parser.WriteFile(_configFile, _data);
        }

        private void PersistInstalledFiltersSha256AndContentLength()
        {
            if (txtPodInstallationLoc.Text.Length > 0 &&
                Directory.Exists(txtPodInstallationLoc.Text))
            {
                foreach (var filter in _data.Sections)
                {
                    if (File.Exists($"{txtPodInstallationLoc.Text}\\filter\\{filter.SectionName}.filter"))
                    {
                        PersistInstalledFiltersSha256AndContentLengthHelper(filter.SectionName);
                    }
                }
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;

            PersistServerETagAndContentLengthOfInstalledFilters();

            // TODO do rest of the logic which is currently done in timer function

            timer.Enabled = true;
        }

        private async void PersistServerETagAndContentLengthOfInstalledFilters()
        {
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
            }
        }

        private void rbInstalled_CheckedChanged(object sender, EventArgs e)
        {
            UpdateListview();

            UpdateButtonStates();
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
            _configFile = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? throw new InvalidOperationException()
                , @"Configuration.ini");

            _data = _parser.ReadFile(_configFile);

            txtPodInstallationLoc.Text =
                Directory.Exists(_data.Global.GetKeyData("PodInstallLocation").Value.Trim()) ?
                    _data.Global.GetKeyData("PodInstallLocation").Value.Trim() : "";

            if (txtPodInstallationLoc.Text.Length == 0)
            {
                txtPodInstallationLoc.Text = $@"{Utils.GetD2InstallLocationFromRegistry()}\Path of Diablo";
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

            timer.Enabled = true;

            // Use file system watcher to monitor if filter files are added to the filter directory, so we can persist
            // a) the sha256 value of new installed filter file and b) content length of it.
            // Can also get the server's etag and content length of the filter and persist those as well (eventually getting rid of timer)
            // Have to keep mind of parallel downloading tasks as they are usng inifile's write method, so that persisting of above info
            // is still thread safe
            if (Directory.Exists($"{txtPodInstallationLoc.Text}\\filter"))
            {
                FileSystemWatcher _fsw = new FileSystemWatcher($"{txtPodInstallationLoc.Text}\\filter", "*.filter")
                {
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size,
                    EnableRaisingEvents = true
                };
                _fsw.Changed += fsw_Changed;
                _fsw.Deleted += fsw_Deleted;
            }

            // TODO do the timer logic in here and later the timer logic will be done only when user
            // presses the refresh button (at later time remove timer entirely)
        }

        private void fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            if (rbInstalled.Checked)
            {
                if (lvFilters.Items.Count > 0)
                {
                    //// TODO fix problem: System.InvalidOperationException: 'Cross-thread operation not valid: Control 'lvFilters' accessed from a thread other than the thread it was created on.'

                    //ListViewItem listViewItem;
                    //listViewItem = lvFilters.FindItemWithText(e.Name);
                    //lvFilters.Items.Remove(listViewItem);

                    //UpdateButtonStates();
                }
            }
        }

        private void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            PersistInstalledFiltersSha256AndContentLength(e.Name.Replace(".filter", ""));
        }

        private async void InstallSelected(List<string> filters)
        {
            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var results =
                await UpdateAndDownload.RunGetFilterContentInParallelAsync(progress, _data,
                    txtPodInstallationLoc.Text, filters);

            foreach (var result in results)
            {
                File.WriteAllText($"{txtPodInstallationLoc.Text}\\filter\\{result.FilterName}.filter", result.Content);
                var test = _data[result.FilterName].GetKeyData("downloaded_etag");
                test.Value = result.ETag;
                _data[result.FilterName].SetKeyData(test);

                test = _data[result.FilterName].GetKeyData("downloaded_content_length");
                test.Value = result.ContentLength;
                _data[result.FilterName].SetKeyData(test);

                test = _data[result.FilterName].GetKeyData("downloaded_sha256");
                test.Value = Utils.BytesToString(Utils.GetHashSha256($"{txtPodInstallationLoc.Text}\\filter\\{result.FilterName}.filter"));
                _data[result.FilterName].SetKeyData(test);

                _parser.WriteFile(_configFile, _data);
            }

            PersistInstalledFiltersSha256AndContentLength();
        }

        private void btnInstallSelected_Click(object sender, EventArgs e)
        {
            if (lvFilters.SelectedItems.Count == 1)
            {
                btnInstallSelected.Enabled = false;

                List<string> filters = new List<string> { lvFilters.SelectedItems[0].Text };

                InstallSelected(filters);

                var temp = lvFilters.FindItemWithText(lvFilters.SelectedItems[0].Text);
                lvFilters.Items.Remove(temp);

                timer.Enabled = true;
            }
        }

        private void btnDownloadUpdatedFilters_Click(object sender, EventArgs e)
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

            List<string> filters = (from ListViewItem lvFiltersItem in lvFilters.Items
                where lvFiltersItem.SubItems[1].Text == _rm.GetString("frmMain_Update_available")
                where lvFiltersItem.Checked
                select lvFiltersItem.Text).ToList();

            InstallSelected(filters);

            timer.Enabled = true;
        }
    }
}