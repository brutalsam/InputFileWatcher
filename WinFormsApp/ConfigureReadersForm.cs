using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FileImporter.Common;

namespace WinFormsApp
{
    public partial class ConfigureReadersForm : Form
    {
        private Dictionary<string, TypeStorage> inputReaderTypes;
        private Dictionary<string, TypeStorage> configuredReaderTypes;
        public ConfigureReadersForm(Dictionary<string, TypeStorage> enabledReaderTypes)
        {
            InitializeComponent();
            inputReaderTypes = enabledReaderTypes;
            configuredReaderTypes = enabledReaderTypes;
        }

        private void ConfigureReadersForm_Load(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            foreach (var value in inputReaderTypes.Values)
            {
                checkedListBox1.Items.Add(value.ReaderType.Name, value.AllowedToUse);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            foreach (var item in configuredReaderTypes)
            {
                item.Value.AllowedToUse =
                    checkedListBox1.CheckedItems.Contains(item.Value.ReaderType.Name);
            }

            Close();
        }

        public Dictionary<string, TypeStorage> GetConfiguredTypes()
        {
            return configuredReaderTypes;
        }
    }
}

