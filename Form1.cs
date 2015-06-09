using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Eff2EmtGUI
{
    public partial class Form1 : Form
    {
        // Can be customized with any available text editors. Will be checked from top to bottom, and the first one found will be execute for an .emt file.
        public static List<string> TextEditors = new List<string>()
        {
            "C:\\Program Files (x86)\\Notepad++\\notepad++.exe",
            "C:\\Program Files\\Notepad++\\notepad++.exe",
            "C:\\Windows\notepad.exe"
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textEQFolder.Text = Properties.Settings.Default.EQFolder;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.EQFolder = textEQFolder.Text;

            Properties.Settings.Default.Save();
        }

        private void CheckIfReady()
        {
            labelZonesSelected.Text = listZoneEffs.CheckedItems.Count.ToString();
            labelZoneCount.Text = listZoneEffs.Items.Count.ToString();
            
            buttonConvert.Enabled = (listZoneEffs.CheckedItems.Count > 0);
        }

        private void CheckZoneList()
        {
            listZoneEffs.Items.Clear();
            
            if (Directory.Exists(textEQFolder.Text))
            {
                FileInfo[] _files = new DirectoryInfo(textEQFolder.Text).GetFiles("*_sounds.eff");

                foreach (FileInfo _file in _files)
                {
                    listZoneEffs.Items.Add(_file.Name.Substring(0, _file.Name.Length - "_sounds.eff".Length));
                }
            }

            CheckIfReady();
        }

        private void textEQFolder_TextChanged(object sender, EventArgs e)
        {
            CheckZoneList();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            Array _files = (Array)e.Data.GetData(DataFormats.FileDrop);

            if ((_files == null) || (_files.Length < 1))
            {
                return;
            }

            textEQFolder.Text = Path.GetDirectoryName(_files.GetValue(0).ToString());

            CheckZoneList();

            for (int _filenum = 0; _filenum < _files.Length; _filenum++)
            {
                FileInfo _file = new FileInfo(_files.GetValue(_filenum).ToString());

                if (_file.Exists)
                {
                    //MessageBox.Show(listZoneEffs.Items[0].Name);

                    for (int _zoneindex = 0; _zoneindex < listZoneEffs.Items.Count; _zoneindex++)
                    {
                        if (_file.Name.StartsWith(listZoneEffs.Items[_zoneindex].Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            listZoneEffs.Items[_zoneindex].Checked = true;
                            listZoneEffs.EnsureVisible(_zoneindex);
                            //listZoneEffs.TopIndex = Math.Max(_zoneindex - 3, 0);
                        }
                    }
                }
            }
        }

        private void buttonBrowseEQFolder_Click(object sender, EventArgs e)
        {
            dialogEQFolder.SelectedPath = textEQFolder.Text;
            
            if (dialogEQFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textEQFolder.Text = dialogEQFolder.SelectedPath;

                CheckZoneList();
            }
        }

        private void SetFormEnabled(bool YesNo)
        {
            textEQFolder.Enabled = YesNo;
            buttonBrowseEQFolder.Enabled = YesNo;
            listZoneEffs.Enabled = YesNo;
            buttonConvert.Text = YesNo ? "Convert!" : "Abort";
        }
        
        private void buttonConvert_Click(object sender, EventArgs e)
        {
            switch (buttonConvert.Text)
            {
                case "Convert!":
                    SetFormEnabled(false);

                    progressConversion.Value = 0;
                    progressConversion.Maximum = listZoneEffs.CheckedItems.Count;

                    List<string> _selectedZones = new List<string>(listZoneEffs.CheckedItems.Count);

                    foreach (ListViewItem _zone in listZoneEffs.CheckedItems)
                    {
                        _selectedZones.Add(_zone.Text);
                    }

                    threadConverter.RunWorkerAsync(_selectedZones);
                    break;
                case "Abort":
                    threadConverter.CancelAsync();
                    break;
            }
        }

        private void listZoneEffs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CheckIfReady();
        }

        private void threadConverter_DoWork(object sender, DoWorkEventArgs e)
        {
            
            List<string> _zoneList = e.Argument as List<string>;
            int _zonesConverted = 0;
            e.Result = DialogResult.OK;
            
            foreach (string _zoneNick in _zoneList)
            {
                switch (Eff2EmtConverter.ConvertZone(textEQFolder.Text, _zoneNick))
                {
                    case DialogResult.OK:
                        threadConverter.ReportProgress(++_zonesConverted);
                        break;
                    case DialogResult.Abort:
                        e.Result = DialogResult.Abort;
                        return;
                    case DialogResult.Ignore:
                        break;
                }

                if (threadConverter.CancellationPending)
                {
                    e.Result = DialogResult.Cancel;
                    return;
                }
            }
        }

        private void threadConverter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressConversion.Value = e.ProgressPercentage;

            string _emtFilename = textEQFolder.Text + "\\" + listZoneEffs.CheckedItems[e.ProgressPercentage - 1].Text + ".emt";

            // Don't open every newly merged .emt file if we're batch converting more than 3 files.
            if ((listZoneEffs.CheckedItems.Count < 4) && File.Exists(_emtFilename))
            {
                // Open the file in Notepad++ if available, or Notepad if not. Other text file readers could be added as options here.

                foreach (string _textEditor in TextEditors)
                {
                    if (File.Exists(_textEditor))
                    {
                        System.Diagnostics.Process.Start(_textEditor, _emtFilename);
                        break;
                    }
                }
            }
        }

        private void threadConverter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetFormEnabled(true);

            switch ((DialogResult)e.Result)
            {
                case DialogResult.OK:
                    MessageBox.Show("Conversion process completed.", "Conversion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case DialogResult.Abort:
                    MessageBox.Show("Conversion process aborted.", "Conversion Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("Conversion process cancelled.", "Conversion Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }
    }
}
