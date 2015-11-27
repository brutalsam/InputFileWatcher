using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FileImporter.Common.Helpers;
using FileImporter.Common;
using System.Threading.Tasks;
using System.IO;

namespace WinFormsApp
{
    public partial class frmMain : Form
    {
        delegate void SetTextCallback(DataTable inputData, string fileName);
        private CustomFileWatcher watcher;
        private Dictionary<string, TypeStorage> readerTypes;
        private ReaderFactory readerFactory;
        private string workingDirectory;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var pluginAssemblyPath = ConfigurationHelper.GetPluginsPath();
            readerTypes = GetReaderTypesHelper.GetFileReaderTypes(pluginAssemblyPath);
            var landingDirectory = ConfigurationHelper.GetLandingDirectory();
            workingDirectory = ConfigurationHelper.GetWorkingDirectory();

            var checkInterval = ConfigurationHelper.GetCheckTimeout();

            readerFactory = new ReaderFactory(readerTypes);
            watcher = new CustomFileWatcher(checkInterval, landingDirectory);
            watcher.ElapsedInterval += OnElapsedInterval;
            tabControl1.TabPages.Clear();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            watcher.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            watcher.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void OnElapsedInterval(object sender)
        {
            var localFileList = watcher.FileList.ToArray();
            foreach (string file in localFileList)
            {
                var fileName = file;
                var workingFile = CopyFileToWorkingDirectory(fileName);
                watcher.FileList.Remove(fileName);
                var fileReader = this.readerFactory.GetFileReader(Path.GetExtension(workingFile));
                if (fileReader != null)
                {
                    var fileProcessor = new FileProcessor(fileReader, workingFile);
                    var task = new Task<DataTable>(fileProcessor.GetDataTableFromFile);
                    task.ContinueWith(result => SetResult(result.Result, workingFile));
                    task.Start();
                }
            }
        }

        private void SetResult(DataTable inputData, string fileName)
        {
            if (this.tabControl1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetResult);
                this.Invoke(d, new object[] { inputData, fileName });
            }
            else
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = inputData;
                DataGridView dgw = new DataGridView();
                dgw.DataSource = bs;
                var tab = new TabPage(fileName);
                tab.Controls.Add(dgw);
                dgw.Dock = DockStyle.Fill;
                this.tabControl1.TabPages.Add(tab);
            }
        }

        private string CopyFileToWorkingDirectory(string fileName)
        {
            if (!Directory.Exists(workingDirectory))
            {
                Directory.CreateDirectory(workingDirectory);
            }

            var workingFileName = String.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(fileName), DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"), Path.GetExtension(fileName));
            var destinationFile = Path.Combine(workingDirectory, workingFileName);
            File.Move(fileName, destinationFile);
            return destinationFile;
        }

        private void btnClearTabs_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
        }

        private void btnConfigureReaders_Click(object sender, EventArgs e)
        {
            ConfigureReadersForm form = new ConfigureReadersForm(readerTypes);
            form.ShowDialog();
            readerTypes = form.GetConfiguredTypes();
        }

    }
}
