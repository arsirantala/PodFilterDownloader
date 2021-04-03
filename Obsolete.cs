namespace IxothPodFilterDownloader
{
    class Obsolete
    {

        /*private void DownloadFilterFile(string filtername, string url, string author, bool silent)
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
        }*/


        //private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    progressBar.Value = e.ProgressPercentage;
        //}

        /*
         * if (string.IsNullOrEmpty(_data[lvFiltersItem.Text].GetKeyData("server_etag").Value) &&
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
         */
    }
}
