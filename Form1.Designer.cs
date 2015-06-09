namespace Eff2EmtGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textEQFolder = new System.Windows.Forms.TextBox();
            this.buttonBrowseEQFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelZonesSelected = new System.Windows.Forms.Label();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelZoneCount = new System.Windows.Forms.Label();
            this.dialogEQFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.listZoneEffs = new System.Windows.Forms.ListView();
            this.progressConversion = new System.Windows.Forms.ProgressBar();
            this.threadConverter = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&EverQuest Folder Location:";
            // 
            // textEQFolder
            // 
            this.textEQFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEQFolder.Location = new System.Drawing.Point(11, 27);
            this.textEQFolder.Name = "textEQFolder";
            this.textEQFolder.Size = new System.Drawing.Size(487, 20);
            this.textEQFolder.TabIndex = 1;
            this.textEQFolder.TextChanged += new System.EventHandler(this.textEQFolder_TextChanged);
            // 
            // buttonBrowseEQFolder
            // 
            this.buttonBrowseEQFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseEQFolder.Location = new System.Drawing.Point(498, 26);
            this.buttonBrowseEQFolder.Name = "buttonBrowseEQFolder";
            this.buttonBrowseEQFolder.Size = new System.Drawing.Size(37, 22);
            this.buttonBrowseEQFolder.TabIndex = 2;
            this.buttonBrowseEQFolder.Text = "&...";
            this.buttonBrowseEQFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseEQFolder.Click += new System.EventHandler(this.buttonBrowseEQFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Zones selected:";
            // 
            // labelZonesSelected
            // 
            this.labelZonesSelected.Location = new System.Drawing.Point(101, 65);
            this.labelZonesSelected.Name = "labelZonesSelected";
            this.labelZonesSelected.Size = new System.Drawing.Size(25, 14);
            this.labelZonesSelected.TabIndex = 6;
            this.labelZonesSelected.Text = "0";
            this.labelZonesSelected.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonConvert
            // 
            this.buttonConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConvert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonConvert.Enabled = false;
            this.buttonConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConvert.Location = new System.Drawing.Point(10, 198);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(96, 28);
            this.buttonConvert.TabIndex = 5;
            this.buttonConvert.Text = "Convert!";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "/";
            // 
            // labelZoneCount
            // 
            this.labelZoneCount.Location = new System.Drawing.Point(132, 65);
            this.labelZoneCount.Name = "labelZoneCount";
            this.labelZoneCount.Size = new System.Drawing.Size(31, 14);
            this.labelZoneCount.TabIndex = 8;
            this.labelZoneCount.Text = "0";
            // 
            // listZoneEffs
            // 
            this.listZoneEffs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listZoneEffs.CheckBoxes = true;
            this.listZoneEffs.Location = new System.Drawing.Point(11, 82);
            this.listZoneEffs.Name = "listZoneEffs";
            this.listZoneEffs.Size = new System.Drawing.Size(524, 112);
            this.listZoneEffs.TabIndex = 4;
            this.listZoneEffs.UseCompatibleStateImageBehavior = false;
            this.listZoneEffs.View = System.Windows.Forms.View.List;
            this.listZoneEffs.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listZoneEffs_ItemChecked);
            // 
            // progressConversion
            // 
            this.progressConversion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressConversion.Location = new System.Drawing.Point(112, 199);
            this.progressConversion.Name = "progressConversion";
            this.progressConversion.Size = new System.Drawing.Size(423, 26);
            this.progressConversion.TabIndex = 9;
            // 
            // threadConverter
            // 
            this.threadConverter.WorkerReportsProgress = true;
            this.threadConverter.WorkerSupportsCancellation = true;
            this.threadConverter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.threadConverter_DoWork);
            this.threadConverter.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.threadConverter_ProgressChanged);
            this.threadConverter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.threadConverter_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 235);
            this.Controls.Add(this.progressConversion);
            this.Controls.Add(this.listZoneEffs);
            this.Controls.Add(this.labelZoneCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.labelZonesSelected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBrowseEQFolder);
            this.Controls.Add(this.textEQFolder);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(461, 209);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EverQuest Sound Effect (EFF) to Sound Emitter (EMT) Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textEQFolder;
        private System.Windows.Forms.Button buttonBrowseEQFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelZonesSelected;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelZoneCount;
        private System.Windows.Forms.FolderBrowserDialog dialogEQFolder;
        private System.Windows.Forms.ListView listZoneEffs;
        private System.Windows.Forms.ProgressBar progressConversion;
        private System.ComponentModel.BackgroundWorker threadConverter;
    }
}

