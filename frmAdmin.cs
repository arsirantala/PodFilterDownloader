using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

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
                if (MessageBox.Show("Are you sure you want to remove the filter?",
                    "Confirmation", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var test = _data.Sections;
                    test.RemoveSection(lbFilters.SelectedItem.ToString());
                    _parser.WriteFile(@"Configuration.ini", _data);
                    lbFilters.Items.Remove(lbFilters.SelectedItem);

                    RefreshLB();
                }
            }
        }

        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            if (!CheckThatInputsAreOK())
            {
                MessageBox.Show("Check the textboxes, every input is mandatory!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (_data.Sections.ContainsSection(txtFilterName.Text.Trim()))
            {
                MessageBox.Show("Filter is already in the list! You have to change the filter name (so its unique) to be able to add a new filter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Check the textboxes, every input is mandatory!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (lbFilters.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure you want to save changes to the selected filter?",
                    "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtFilterName.Text = txtFilterName.Text.Trim();

                    if (!_data.Sections.ContainsSection(txtFilterName.Text))
                    {
                        MessageBox.Show(
                            "There is no such filter in the Configuration.ini file, if you wan't to add a nww filter use the 'add filter' button instead!",
                            "Filter with specified name was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
