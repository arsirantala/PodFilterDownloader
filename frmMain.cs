using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public const string FilterDirectoryName = "filter";

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnBrowsePoDInstallLoc_Click(object sender, EventArgs e)
        {
            var browserDialog = new FolderBrowserDialog { SelectedPath = txtPodInstallationLoc.Text.Trim() };

            var result = browserDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtPodInstallationLoc.Text = browserDialog.SelectedPath.Trim();
                Utils.UpdateListview(lvFilters, rbInstalled, _rm, txtPodInstallationLoc.Text, _data, rbAvailable, btnInstallSelected,
                    btnDownloadUpdatedFilters, btnRemoveSelected, btnMoreInfoOnSelectedFilter);

                RefreshContent();
            }
        }

        private void toolStripMenuItemFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            frmAbout about = new frmAbout();
            about.ShowDialog(this);
            about.Dispose();
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
            xpProgressBar.Position = e.PercentageComplete;

            if (xpProgressBar.Position >= xpProgressBar.PositionMax)
            {
                xpProgressBar.Text = _rm.GetString("frmMain_Operation_completed");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;

            RefreshContent();
        }

        private async void PersistServerETagAndContentLengthOfInstalledFilters()
        {
            btnCancel.Enabled = true;

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

            btnCancel.Enabled = false;
        }

        private void rbInstalled_CheckedChanged(object sender, EventArgs e)
        {
            Utils.UpdateListview(lvFilters, rbInstalled, _rm, txtPodInstallationLoc.Text, _data, 
                rbAvailable, btnInstallSelected, btnDownloadUpdatedFilters, btnRemoveSelected, 
                btnMoreInfoOnSelectedFilter);

            Utils.UpdateButtonStates(rbInstalled, lvFilters, btnRemoveSelected, btnMoreInfoOnSelectedFilter,
                btnInstallSelected);
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

            // ReSharper disable once LocalizableElement
            if (MessageBox.Show($"{_rm.GetString("frmMain_Are_you_sure_you_want_to_remove_file")}", _rm.GetString("frmMain_Confirmation"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete($"{txtPodInstallationLoc.Text}\\{FilterDirectoryName}\\{lvFilters.SelectedItems[0].Text}.filter");
            }
        }

        private void lvFilters_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Utils.UpdateButtonStates(rbInstalled, lvFilters, btnRemoveSelected, btnMoreInfoOnSelectedFilter,
                btnInstallSelected);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            _cts.Cancel();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            _configFile = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? 
                throw new InvalidOperationException(), @"Configuration.ini");

            _data = _parser.ReadFile(_configFile);

            txtPodInstallationLoc.Text =
                Directory.Exists(_data.Global.GetKeyData("PodInstallLocation").Value.Trim()) ?
                    _data.Global.GetKeyData("PodInstallLocation").Value.Trim() : "";

            if (txtPodInstallationLoc.Text.Length == 0)
            {
                txtPodInstallationLoc.Text = $@"{Utils.GetD2InstallLocationFromRegistry()}\Path of Diablo";
            }

            // Use file system watcher to monitor if filter files are added to the filter directory, so we can persist
            // a) the sha256 value of new installed filter file and b) content length of it.
            // Can also get the server's etag and content length of the filter and persist those as well.
            // Have to keep mind of parallel downloading tasks as they are using inifile's write method, so that persisting of above info
            // is still thread safe
            if (Directory.Exists($"{txtPodInstallationLoc.Text}\\{FilterDirectoryName}"))
            {
                _fsw = new FileSystemWatcher($"{txtPodInstallationLoc.Text}\\{FilterDirectoryName}", "*.filter")
                {
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size,
                    EnableRaisingEvents = true
                };
                _fsw.Changed += fsw_Changed;
                _fsw.Deleted += fsw_Deleted;
            }

            // Populate listview with installed filters
            Utils.UpdateListview(lvFilters, rbInstalled, _rm, txtPodInstallationLoc.Text, _data, rbAvailable, btnInstallSelected,
                btnDownloadUpdatedFilters, btnRemoveSelected, btnMoreInfoOnSelectedFilter);

            // Localize the form
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
            xpProgressBar.Text = _rm.GetString("frmMain_Checking_updates_from_servers");

            RefreshContent();
        }

        private void fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            if (rbInstalled.Checked)
            {
                if (lvFilters.Items.Count > 0)
                {
                    if (lvFilters.InvokeRequired)
                    {
                        lvFilters.Invoke(new MethodInvoker(delegate
                        {
                            ListViewItem listViewItem = lvFilters.FindItemWithText(e.Name.Replace(".filter", ""));
                            lvFilters.Items.Remove(listViewItem);

                            if (lvFilters.SelectedItems.Count == 0)
                            {
                                Utils.UpdateButtonStateWithInvokeIfNeeded(btnRemoveSelected, false);
                                Utils.UpdateButtonStateWithInvokeIfNeeded(btnMoreInfoOnSelectedFilter, false);
                            }
                            else
                            {
                                Utils.UpdateButtonStateWithInvokeIfNeeded(btnRemoveSelected, true);
                                Utils.UpdateButtonStateWithInvokeIfNeeded(btnMoreInfoOnSelectedFilter, true);
                            }

                            Utils.UpdateButtonStateWithInvokeIfNeeded(btnInstallSelected, false);
                        }));
                    }
                }
            }
        }

        private void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Utils.PersistInstalledFiltersSha256AndContentLength(e.Name.Replace(".filter", ""), 
                txtPodInstallationLoc.Text, _data, _parser, _configFile);
        }

        /// <summary>
        /// Install selected filter to PoD filter directory
        /// </summary>
        /// <param name="filters">List of filters to be installed</param>
        private async void InstallSelected(List<string> filters)
        {
            btnCancel.Enabled = true;

            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            var results =
                await UpdateAndDownload.RunGetFilterContentInParallelAsync(progress, _data,
                    txtPodInstallationLoc.Text, filters);

            foreach (var result in results)
            {
                File.WriteAllText($"{txtPodInstallationLoc.Text}\\{FilterDirectoryName}\\{result.FilterName}.filter", result.Content);
                var test = _data[result.FilterName].GetKeyData("downloaded_etag");
                test.Value = result.ETag;
                _data[result.FilterName].SetKeyData(test);

                test = _data[result.FilterName].GetKeyData("installed_content_length");
                test.Value = result.ContentLength;
                _data[result.FilterName].SetKeyData(test);

                test = _data[result.FilterName].GetKeyData("downloaded_sha256");
                test.Value = Utils.BytesToString(Utils.GetHashSha256($"{txtPodInstallationLoc.Text}\\{FilterDirectoryName}\\{result.FilterName}.filter"));
                _data[result.FilterName].SetKeyData(test);

                _parser.WriteFile(_configFile, _data);
            }

            if (rbAvailable.Checked)
            {
                var temp = lvFilters.FindItemWithText(lvFilters.SelectedItems[0].Text);
                lvFilters.Items.Remove(temp);
            }

            btnCancel.Enabled = false;

            RefreshContent();
        }

        private void btnInstallSelected_Click(object sender, EventArgs e)
        {
            if (lvFilters.SelectedItems.Count == 1)
            {
                btnCancel.Enabled = true;

                xpProgressBar.Text = _rm.GetString("frmMain_Installing_selected_filter");

                btnInstallSelected.Enabled = false;

                List<string> filters = new List<string> { lvFilters.SelectedItems[0].Text };

                InstallSelected(filters);
            }
            else
            {
                btnInstallSelected.Enabled = false;
                MessageBox.Show(_rm.GetString("frmMain_No_Filter_Selected_In_LV"), _rm.GetString("frmMain_Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            xpProgressBar.Text = _rm.GetString("frmMain_Updating_selected_filters");

            List<string> filters = (from ListViewItem lvFiltersItem in lvFilters.Items
                                    where lvFiltersItem.SubItems[1].Text == _rm.GetString("frmMain_Update_available")
                                    where lvFiltersItem.Checked
                                    select lvFiltersItem.Text).ToList();

            InstallSelected(filters);
        }

        private void RefreshContent()
        {
            if (lvFilters.Items.Count == 0 || !rbInstalled.Checked)
            {
                btnRefresh.Enabled = true;
                return;
            }

            Utils.PersistInstalledFiltersSha256AndContentLength(txtPodInstallationLoc.Text, _data, _parser,
                _configFile);

            PersistServerETagAndContentLengthOfInstalledFilters();

            btnDownloadUpdatedFilters.Enabled = Utils.CheckIfInstalledFiltersHasUpdates(lvFilters, _data, _rm);
        }
    }
}