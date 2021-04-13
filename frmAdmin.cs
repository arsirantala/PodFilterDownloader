using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
// ReSharper disable InconsistentNaming

namespace IxothPodFilterDownloader
{
    public partial class frmAdmin : Form
    {
        private string _configFile;
        private IniData _data;
        private readonly FileIniDataParser _parser = new FileIniDataParser();

        public frmAdmin()
        {
            InitializeComponent();
        }

        private void RefreshLB()
        {
            _data = _parser.ReadFile(_configFile);

            lbFilters.Items.Clear();

            foreach (var section in _data.Sections)
            {
                lbFilters.Items.Add(section.SectionName);
            }

            if (lbFilters.Items.Count > 0)
            {
                lbFilters.SelectedIndex = 0;
            }
        }

        private void frmAdmin_Shown(object sender, EventArgs e)
        {
            _configFile = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ??
                throw new InvalidOperationException(), @"Configuration.ini");

            RefreshLB();

            if (!UpdateAndDownload.NetworkIsAvailable())
            {
                btnRestoreDefaultsFromInternet.Enabled = false;
            }
        }

        private void lbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFilters.SelectedItem != null)
            {
                txtFilterAuthor.Text = _data[lbFilters.SelectedItem.ToString()].GetKeyData("author").Value;
                txtFilterDescription.Text = _data[lbFilters.SelectedItem.ToString()].GetKeyData("description").Value;
                txtFilterDownloadUrl.Text = _data[lbFilters.SelectedItem.ToString()].GetKeyData("download_url").Value;
                txtFilterHomeUrl.Text = _data[lbFilters.SelectedItem.ToString()].GetKeyData("home_repo_url").Value;
                var test = _data.Sections;
                txtFilterName.Text = test.GetSectionData(lbFilters.SelectedItem.ToString()).SectionName;
            }
        }

        private void btnDeleteFilter_Click(object sender, EventArgs e)
        {
            if (lbFilters.SelectedItem != null)
            {
                if (MessageBox.Show(Utils.GetLocalizedString("frmAdmin_Are_you_sure_you_want_to_remove_the_filter"),
                    Utils.GetLocalizedString("frmMain_Confirmation"), MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var test = _data.Sections;
                    test.RemoveSection(lbFilters.SelectedItem.ToString());
                    _parser.WriteFile(_configFile, _data);
                    lbFilters.Items.Remove(lbFilters.SelectedItem);

                    RefreshLB();
                }
            }
        }

        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            if (!CheckThatInputsAreOK())
            {
                MessageBox.Show(Utils.GetLocalizedString("frmAdmin_Check_the_textboxes__every_input_is_mandatory"),
                    Utils.GetLocalizedString("frmMain_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_data.Sections.ContainsSection(txtFilterName.Text.Trim()))
            {
                MessageBox.Show(
                    Utils.GetLocalizedString("frmAdmin_Filter_is_already_in_the_list__You_have_to_change_the_filter_name__so_its_unique__to_be_able_to_add_a_new_filter"), 
                    Utils.GetLocalizedString("frmMain_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFilterAuthor.Text = txtFilterAuthor.Text.Trim();
            txtFilterName.Text = txtFilterName.Text.Trim();
            txtFilterDescription.Text = txtFilterDescription.Text.Trim();
            txtFilterHomeUrl.Text = txtFilterHomeUrl.Text.Trim();
            txtFilterDownloadUrl.Text = txtFilterDownloadUrl.Text.Trim();

            SectionData filter = new SectionData(txtFilterName.Text);
            filter.Keys.AddKey("author", txtFilterAuthor.Text);
            filter.Keys.AddKey("download_url", txtFilterDownloadUrl.Text);
            filter.Keys.AddKey("home_repo_url", txtFilterHomeUrl.Text);
            filter.Keys.AddKey("server_etag");
            filter.Keys.AddKey("downloaded_etag");
            filter.Keys.AddKey("downloaded_sha256");
            filter.Keys.AddKey("installed_sha256");
            filter.Keys.AddKey("server_content_length");
            filter.Keys.AddKey("installed_content_length");
            filter.Keys.AddKey("description", txtFilterDescription.Text);
            filter.Keys.AddKey("selected_for_updates", "True");

            _data.Sections.Add(filter);

            _parser.WriteFile(_configFile, _data);

            RefreshLB();
        }

        private bool CheckThatInputsAreOK()
        {
            if (txtFilterAuthor.BackColor == Color.Red ||
                txtFilterName.BackColor == Color.Red ||
                txtFilterDescription.BackColor == Color.Red ||
                txtFilterDownloadUrl.BackColor == Color.Red ||
                txtFilterHomeUrl.BackColor == Color.Red)
            {
                return false;
            }

            return true;
        }

        private void btnSaveFilterChanges_Click(object sender, EventArgs e)
        {
            if (!CheckThatInputsAreOK())
            {
                MessageBox.Show(Utils.GetLocalizedString("frmAdmin_Check_the_textboxes__every_input_is_mandatory"), 
                    Utils.GetLocalizedString("frmMain_Error"), MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (lbFilters.SelectedItem != null)
            {
                if (MessageBox.Show(Utils.GetLocalizedString("frmAdmin_Are_you_sure_you_want_to_save_changes_to_the_selected_filter"),
                    Utils.GetLocalizedString("frmMain_Confirmation"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtFilterName.Text = txtFilterName.Text.Trim();

                    if (!_data.Sections.ContainsSection(txtFilterName.Text))
                    {
                        MessageBox.Show(
                            Utils.GetLocalizedString("frmAdmin_There_is_no_such_filter_in_the_Configuration_ini_file__if_you_wan_t_to_add_a_new_filter_use_the__add_filter__button_instead"),
                            Utils.GetLocalizedString("frmAdmin_Filter_with_specified_name_was_not_found"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var test = _data[txtFilterName.Text].GetKeyData("author");
                    test.Value = txtFilterAuthor.Text.Trim();
                    _data[txtFilterName.Text].SetKeyData(test);

                    test = _data[txtFilterName.Text].GetKeyData("description");
                    test.Value = txtFilterDescription.Text.Trim();
                    _data[txtFilterName.Text].SetKeyData(test);

                    test = _data[txtFilterName.Text].GetKeyData("download_url");
                    test.Value = txtFilterDownloadUrl.Text.Trim();
                    _data[txtFilterName.Text].SetKeyData(test);

                    test = _data[txtFilterName.Text].GetKeyData("home_repo_url");
                    test.Value = txtFilterHomeUrl.Text.Trim();
                    _data[txtFilterName.Text].SetKeyData(test);

                    _parser.WriteFile(_configFile, _data);

                    RefreshLB();
                }
            }
        }

        private void TextboxValidatorHelper(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                textBox.Focus();
                textBox.BackColor = Color.Red;
            }
            else
            {
                textBox.BackColor = Color.White;
            }
        }

        private void txtFilterAuthor_Validating(object sender, CancelEventArgs e)
        {
            TextboxValidatorHelper(txtFilterAuthor);
        }

        private void txtFilterName_Validating(object sender, CancelEventArgs e)
        {
            TextboxValidatorHelper(txtFilterName);
        }

        private void txtFilterDownloadUrl_Validating(object sender, CancelEventArgs e)
        {
            TextboxValidatorHelper(txtFilterDownloadUrl);
        }

        private void txtFilterHomeUrl_Validating(object sender, CancelEventArgs e)
        {
            TextboxValidatorHelper(txtFilterHomeUrl);
        }

        private void txtFilterDescription_Validating(object sender, CancelEventArgs e)
        {
            TextboxValidatorHelper(txtFilterDescription);
        }

        private void btnRestoreDefaultsFromInternet_Click(object sender, EventArgs e)
        {
            btnRestoreDefaultsFromInternet.Enabled = false;

            if (MessageBox.Show(Utils.GetLocalizedString("frmAdmin_Are_you_sure_you_want_to_restore_default_filters__This_will_override_every_filters_you_have_defined_in_this_application"),
                Utils.GetLocalizedString("frmMain_Confirmation"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string content = UpdateAndDownload.DownloadFileContent("https://raw.githubusercontent.com/arsirantala/PodFilterDownloader/main/Configuration.ini");
                if (!string.IsNullOrEmpty(content))
                {
                    File.WriteAllText(_configFile, content);
                    RefreshLB();
                }
                else
                {
                    MessageBox.Show(Utils.GetLocalizedString("frmAdmin_There_was_an_error_when_attempted_to_download_the_default_Configuration_ini_file_from_the_applications_home_repo_in_the_internet__Please_try_again_later"),
                        Utils.GetLocalizedString("frmMain_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            btnRestoreDefaultsFromInternet.Enabled = true;
        }
    }
}
