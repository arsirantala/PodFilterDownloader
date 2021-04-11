using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using Microsoft.Win32;
// ReSharper disable InconsistentNaming

namespace IxothPodFilterDownloader
{
    public static class Utils
    {
        private static readonly SHA256 Sha256 = SHA256.Create();


        public static void UpdateButtonStates(RadioButton rbInstalled, ListView lvFilters, Button btnRemoveSelected,
            Button btnMoreInfoOnSelectedFilter, Button btnInstallSelected)
        {
            if (rbInstalled.Checked)
            {
                if (lvFilters.SelectedItems.Count == 0)
                {
                    UpdateButtonStateWithInvokeIfNeeded(btnRemoveSelected, false);
                    UpdateButtonStateWithInvokeIfNeeded(btnMoreInfoOnSelectedFilter, false);
                }
                else
                {
                    UpdateButtonStateWithInvokeIfNeeded(btnRemoveSelected, true);
                    UpdateButtonStateWithInvokeIfNeeded(btnMoreInfoOnSelectedFilter, true);
                }
                UpdateButtonStateWithInvokeIfNeeded(btnInstallSelected, false);
            }
            else
            {
                if (lvFilters.SelectedItems.Count == 0)
                {
                    UpdateButtonStateWithInvokeIfNeeded(btnMoreInfoOnSelectedFilter, false);
                    UpdateButtonStateWithInvokeIfNeeded(btnInstallSelected, false);
                }
                else
                {
                    UpdateButtonStateWithInvokeIfNeeded(btnInstallSelected, true);
                    UpdateButtonStateWithInvokeIfNeeded(btnMoreInfoOnSelectedFilter, true);
                }

                UpdateButtonStateWithInvokeIfNeeded(btnRemoveSelected, false);
            }
        }

        private static UpdateAndDownload.BoolEnum CheckFilterHasUpdateHelper(IniData data, string filter)
        {
            UpdateAndDownload.BoolEnum updatesFound;

            if (UpdateAndDownload.HasEtags(filter, data))
            {
                updatesFound = UpdateAndDownload.ETagCheck(filter, data);
            }
            else
            {
                updatesFound = UpdateAndDownload.HasSha256Tags(filter, data) ? 
                    UpdateAndDownload.Sha256Check(filter, data) : 
                    UpdateAndDownload.ContentLengthCheck(filter, data);
            }

            return updatesFound;
        }

        public static UpdateAndDownload.BoolEnum CheckIfFilterHasUpdate(IniData data, string filter)
        {
            return CheckFilterHasUpdateHelper(data, filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lvFilters"></param>
        /// <param name="data"></param>
        /// <param name="rm"></param>
        /// <param name="btnCancel"></param>
        /// <param name="btnRefresh"></param>
        /// <returns></returns>
        public static bool CheckIfInstalledFiltersHasUpdates(ListView lvFilters, IniData data, ResourceManager rm)
        {
            bool updatesFound = false;

            // Compare etag of prev downloaded and server's etag and if they both exist and are different: update is avail for filter
            //
            // If no Etags were found (downloaded and server's), then compare instead content lengths and sha256 values instead -
            // If sha256 value are found and content lengths, then compare those. If sha256 values and content lengths differ: update avail
            foreach (ListViewItem lvFiltersItem in lvFilters.Items)
            {
                if (CheckFilterHasUpdateHelper(data, lvFiltersItem.Text) == UpdateAndDownload.BoolEnum.True)
                {
                    lvFilters.FindItemWithText(lvFiltersItem.Text).SubItems[1].Text =
                        rm.GetString("frmMain_Update_available");
                    updatesFound = true;
                }
                else
                {
                    lvFilters.FindItemWithText(lvFiltersItem.Text).SubItems[1].Text = 
                        rm.GetString("frmMain_Installed");
                }
            }

            lvFilters.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            return updatesFound;
        }

        /// <summary>
        /// Persist the sha256 and content lengths of installed specified filter to the Configuration.ini file
        /// </summary>
        /// <param name="filtername"></param>
        /// <param name="PodInstallationLoc"></param>
        /// <param name="data"></param>
        /// <param name="parser"></param>
        /// <param name="configFile"></param>
        public static void PersistInstalledFiltersSha256AndContentLength(string filtername, string PodInstallationLoc,
            IniData data, FileIniDataParser parser, string configFile)
        {
            if (File.Exists($"{PodInstallationLoc}\\{frmMain.FilterDirectoryName}\\{filtername}.filter"))
            {
                PersistInstalledFiltersSha256AndContentLengthHelper(filtername, data, parser, configFile, PodInstallationLoc);
            }
        }

        /// <summary>
        /// Helper method for PersistInstalledFiltersSha256AndContentLength and PersistInstalledFiltersSha256AndContentLength methods
        /// </summary>
        /// <param name="filtername"></param>
        /// <param name="data"></param>
        /// <param name="parser"></param>
        /// <param name="configFile"></param>
        /// <param name="PodInstallationLoc"></param>
        public static void PersistInstalledFiltersSha256AndContentLengthHelper(string filtername, IniData data, FileIniDataParser parser,
            string configFile, string PodInstallationLoc)
        {
            string temp = filtername;
            if (!filtername.ToLower().Contains(".filter"))
            {
                temp += ".filter";
            }
            var test = data[filtername.Replace(".filter", "")].GetKeyData("installed_content_length");
            test.Value = File.ReadAllText($"{PodInstallationLoc}\\{frmMain.FilterDirectoryName}\\{temp}").Length.ToString();
            data[filtername].SetKeyData(test);

            test = data[filtername.Replace(".filter", "")].GetKeyData("installed_sha256");
            test.Value = BytesToString(GetHashSha256($"{PodInstallationLoc}\\{frmMain.FilterDirectoryName}\\{temp}"));
            data[filtername].SetKeyData(test);
            parser.WriteFile(configFile, data);
        }

        /// <summary>
        /// Persist the sha256 and content lengths of installed filters to the Configuration.ini file
        /// </summary>
        /// <param name="PodInstallationLoc"></param>
        /// <param name="data"></param>
        /// <param name="parser"></param>
        /// <param name="configFile"></param>
        public static void PersistInstalledFiltersSha256AndContentLength(string PodInstallationLoc, IniData data,
            FileIniDataParser parser, string configFile)
        {
            if (PodInstallationLoc.Length > 0 &&
                Directory.Exists(PodInstallationLoc))
            {
                foreach (var filter in data.Sections)
                {
                    if (File.Exists($"{PodInstallationLoc}\\{frmMain.FilterDirectoryName}\\{filter.SectionName}.filter"))
                    {
                        PersistInstalledFiltersSha256AndContentLengthHelper(filter.SectionName, data, parser,
                            configFile, PodInstallationLoc);
                    }
                }
            }
        }

        /// <summary>
        /// Populate the listview content
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="rbInstalled"></param>
        /// <param name="rm"></param>
        /// <param name="PodInstallationLoc"></param>
        /// <param name="data"></param>
        /// <param name="rbAvailable"></param>
        /// <param name="btnInstallSelected"></param>
        /// <param name="btnDownloadUpdatedFilters"></param>
        /// <param name="btnRemoveSelected"></param>
        /// <param name="btnMoreInfoOnSelectedFilter"></param>
        public static void UpdateListview(ListView listView, RadioButton rbInstalled, ResourceManager rm, string PodInstallationLoc,
            IniData data, RadioButton rbAvailable, Button btnInstallSelected, Button btnDownloadUpdatedFilters, Button btnRemoveSelected,
            Button btnMoreInfoOnSelectedFilter)
        {
            if (listView.InvokeRequired)
            {
                // TODO Complete this properly
                MessageBox.Show(@"Invoke required with listview update!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            listView.BeginUpdate();
            listView.Clear();
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.CheckBoxes = rbInstalled.Checked;
            listView.Columns.Add(rm.GetString("frmMain_Filter"), -1, HorizontalAlignment.Left);
            listView.Columns.Add(rm.GetString("frmMain_State"), -1, HorizontalAlignment.Left);
            listView.Columns.Add(rm.GetString("frmMain_Description"), -1, HorizontalAlignment.Left);

            foreach (var section in data.Sections)
            {
                bool filterExists =
                    File.Exists($"{PodInstallationLoc}\\{frmMain.FilterDirectoryName}\\{section.SectionName}.filter");

                if ((rbInstalled.Checked && filterExists) || (rbAvailable.Checked && !filterExists))
                {
                    // Find group or create a new group
                    var found = false;
                    ListViewGroup lvg = null;
                    foreach (var grp in listView.Groups.Cast<ListViewGroup>().Where(grp =>
                        grp.ToString() == data[section.SectionName].GetKeyData("author").Value))
                    {
                        found = true;
                        lvg = grp;
                        break;
                    }

                    if (!found)
                    {
                        // Group not found, create
                        lvg = new ListViewGroup(data[section.SectionName].GetKeyData("author").Value);
                        listView.Groups.Add(lvg);
                    }

                    // Add ListViewItem
                    listView.Items.Add(new ListViewItem(
                        new[] { section.SectionName, filterExists ? 
                                CheckIfFilterHasUpdate(data, section.SectionName) == UpdateAndDownload.BoolEnum.True ?
                                rm.GetString("frmMain_Update_available") : rm.GetString("frmMain_Installed") :
                                rm.GetString("frmMain_Available"),
                            data[section.SectionName].GetKeyData("description").Value }, lvg));
                    listView.Items[listView.Items.Count - 1].Tag = section.SectionName;
                    listView.Items[listView.Items.Count - 1].Checked = bool.Parse(data[section.SectionName].GetKeyData("selected_for_updates").Value);
                }
            }

            if (listView.Items.Count == 0)
            {
                listView.Columns[0].Width = listView.Columns[1].Width = listView.Columns[2].Width = 200;
                btnInstallSelected.Enabled = btnDownloadUpdatedFilters.Enabled = btnRemoveSelected.Enabled =
                    btnMoreInfoOnSelectedFilter.Enabled = btnInstallSelected.Enabled = false;
            }
            else
            {
                listView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            listView.EndUpdate();
        }

        /// <summary>
        /// Helper method for UpdateButtonStates to handle possible cross-thread issues
        /// </summary>
        /// <param name="button">Button control to handle</param>
        /// <param name="state">Button state to updated to enabled property</param>
        public static void UpdateButtonStateWithInvokeIfNeeded(Button button, bool state)
        {
            if (button.InvokeRequired)
                button.Invoke(new MethodInvoker(delegate
                {
                    button.Enabled = state;
                }));
            else
            {
                button.Enabled = state;
            }
        }

        /// <summary>
        /// Write an exception to applications event log
        /// </summary>
        /// <param name="exception">Exception to be written to applications event log</param>
        public static void WriteExceptionToEventLog(Exception exception)
        {
            EventLog mEventLog = new EventLog("") { Source = "IxothPodFilterDownloader" };
            var inner = exception.InnerException;
            var innerStackTrace = "";
            while (inner != null)
            {
                innerStackTrace += $"{inner.StackTrace}{Environment.NewLine}";
                inner = inner.InnerException;
            }

            mEventLog.WriteEntry(
                $"Exception occurred due the reason of: {exception.Message}{Environment.NewLine}Stack trace:{Environment.NewLine}{exception.StackTrace}{Environment.NewLine}Inner stack trace:{Environment.NewLine}{innerStackTrace}",
                EventLogEntryType.FailureAudit);
        }

        /// <summary>
        /// Compute the file's hash
        /// </summary>
        /// <param name="filename">Path to file from which the sha256 is calculated</param>
        /// <returns>Sha256 value</returns>
        public static byte[] GetHashSha256(string filename)
        {
            try
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    return Sha256.ComputeHash(stream);
                }
            }
            catch (Exception ex)
            {
                WriteExceptionToEventLog(ex);
                return new byte[0];
            }
        }

        /// <summary>
        /// Convert byte array to string.
        /// </summary>
        /// <param name="bytes">Byte array to be converted</param>
        /// <returns>Converted byte array as string</returns>
        public static string BytesToString(byte[] bytes)
        {
            return bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
        }

        /// <summary>
        /// Get from Windows registry the installation location of Diablo 2
        /// </summary>
        /// <returns>If install location was found return it, otherwise return empty string</returns>
        public static string GetD2InstallLocationFromRegistry()
        {
            string d2LoDInstallPath = string.Empty;
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
                WriteExceptionToEventLog(ex);
            }

            return d2LoDInstallPath;
        }
    }
}
