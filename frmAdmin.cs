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

        private void frmAdmin_Shown(object sender, EventArgs e)
        {
            _configFile = Path.Combine(
                Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ??
                throw new InvalidOperationException(), @"Configuration.ini");

            _data = _parser.ReadFile(_configFile);

            foreach (var section in _data.Sections)
            {
                lbFilters.Items.Add(section.SectionName);
            }

            if (lbFilters.Items.Count > 0)
            {
                lbFilters.SelectedIndex = 0;
            }
        }

        private void lbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFilters.SelectedItem != null)
            {
                txtFilterAuthor.Text = _data[lbFilters.SelectedItem.ToString()].GetKeyData("author").Value;
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
                lbFilters.Items.Remove(lbFilters.SelectedItem);
            }
        }

        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            if (lbFilters.Items.Contains(txtFilterName.Text))
            {
                MessageBox.Show("Filter is already in the list!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // TODO modify ini file

            lbFilters.Items.Add(txtFilterName.Text);
        }

        private void btnSaveFilterChanges_Click(object sender, EventArgs e)
        {
            // TODO modify ini file

        }
    }
}
