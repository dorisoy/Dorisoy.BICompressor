using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Concurrent;

namespace Dorisoy.BICompressor
{
    /// <summary>
    /// Dorisoy 批量图片压缩器（BICompressor）
    /// </summary>
    public partial class BICompressor : Form
    {
        #region Public Methods
        public BICompressor()
        {
            InitializeComponent();
            progressBar1.Maximum = 100;
        }
        #endregion

        private FolderBrowserDialog mProblematicBrowseDialog;
        private NumericUpDown mQualityTextBox;
        private ConcurrentQueue<TaskFile> TaskFiles = new ConcurrentQueue<TaskFile>();

        public string SavePath1 { get; set; }
        public string SavePath2 { get; set; }
        public string SavePath3 { get; set; }

        #region Private Methods
        private void InitializeComponent()
        {
            this.mInputLabel = new System.Windows.Forms.Label();
            this.mInputTextBox = new System.Windows.Forms.TextBox();
            this.mInputBrowseButton = new System.Windows.Forms.Button();
            this.mInputBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.mOutputLabel = new System.Windows.Forms.Label();
            this.mOutputTextBox = new System.Windows.Forms.TextBox();
            this.mOutputBrowseButton = new System.Windows.Forms.Button();
            this.mOutputBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.mProblematicLabel = new System.Windows.Forms.Label();
            this.mProblematicTextBox = new System.Windows.Forms.TextBox();
            this.mProblematicBrowseButton = new System.Windows.Forms.Button();
            this.mProblematicBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.mResolutionGroupBox = new System.Windows.Forms.GroupBox();
            this.mQualityTextBox = new System.Windows.Forms.NumericUpDown();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.mResolutionPercentRadioButton = new System.Windows.Forms.RadioButton();
            this.mResolutionDimensionTextBox = new System.Windows.Forms.TextBox();
            this.mResolutionDimensionRadioButton = new System.Windows.Forms.RadioButton();
            this.mResolutionPercentTextBox = new System.Windows.Forms.TextBox();
            this.mQualityLabel = new System.Windows.Forms.Label();
            this.mCompressButton = new System.Windows.Forms.Button();
            this.mBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mResolutionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mQualityTextBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mInputLabel
            // 
            this.mInputLabel.AutoSize = true;
            this.mInputLabel.Location = new System.Drawing.Point(8, 18);
            this.mInputLabel.Name = "mInputLabel";
            this.mInputLabel.Size = new System.Drawing.Size(59, 12);
            this.mInputLabel.TabIndex = 0;
            this.mInputLabel.Text = "文件目录:";
            // 
            // mInputTextBox
            // 
            this.mInputTextBox.Location = new System.Drawing.Point(73, 15);
            this.mInputTextBox.Name = "mInputTextBox";
            this.mInputTextBox.Size = new System.Drawing.Size(438, 21);
            this.mInputTextBox.TabIndex = 0;
            this.mInputTextBox.TabStop = false;
            this.mInputTextBox.Text = "D:\\PICM\\04-ipt";
            // 
            // mInputBrowseButton
            // 
            this.mInputBrowseButton.Location = new System.Drawing.Point(533, 13);
            this.mInputBrowseButton.Name = "mInputBrowseButton";
            this.mInputBrowseButton.Size = new System.Drawing.Size(60, 23);
            this.mInputBrowseButton.TabIndex = 1;
            this.mInputBrowseButton.Text = "选择";
            this.mInputBrowseButton.UseVisualStyleBackColor = true;
            this.mInputBrowseButton.Click += new System.EventHandler(this.InputBrowseButton_Click);
            // 
            // mOutputLabel
            // 
            this.mOutputLabel.AutoSize = true;
            this.mOutputLabel.Location = new System.Drawing.Point(20, 63);
            this.mOutputLabel.Name = "mOutputLabel";
            this.mOutputLabel.Size = new System.Drawing.Size(47, 12);
            this.mOutputLabel.TabIndex = 0;
            this.mOutputLabel.Text = "保存到:";
            // 
            // mOutputTextBox
            // 
            this.mOutputTextBox.Location = new System.Drawing.Point(73, 60);
            this.mOutputTextBox.Name = "mOutputTextBox";
            this.mOutputTextBox.Size = new System.Drawing.Size(438, 21);
            this.mOutputTextBox.TabIndex = 0;
            this.mOutputTextBox.TabStop = false;
            this.mOutputTextBox.Text = "D:\\PICM\\out";
            // 
            // mOutputBrowseButton
            // 
            this.mOutputBrowseButton.Location = new System.Drawing.Point(533, 60);
            this.mOutputBrowseButton.Name = "mOutputBrowseButton";
            this.mOutputBrowseButton.Size = new System.Drawing.Size(123, 21);
            this.mOutputBrowseButton.TabIndex = 2;
            this.mOutputBrowseButton.Text = "选择";
            this.mOutputBrowseButton.UseVisualStyleBackColor = true;
            this.mOutputBrowseButton.Click += new System.EventHandler(this.OutputBrowseButton_Click);
            // 
            // mProblematicLabel
            // 
            this.mProblematicLabel.AutoSize = true;
            this.mProblematicLabel.Location = new System.Drawing.Point(8, 110);
            this.mProblematicLabel.Name = "mProblematicLabel";
            this.mProblematicLabel.Size = new System.Drawing.Size(59, 12);
            this.mProblematicLabel.TabIndex = 0;
            this.mProblematicLabel.Text = "压缩失败:";
            // 
            // mProblematicTextBox
            // 
            this.mProblematicTextBox.Location = new System.Drawing.Point(73, 107);
            this.mProblematicTextBox.Name = "mProblematicTextBox";
            this.mProblematicTextBox.Size = new System.Drawing.Size(438, 21);
            this.mProblematicTextBox.TabIndex = 0;
            this.mProblematicTextBox.TabStop = false;
            this.mProblematicTextBox.Text = "D:\\PICM\\pr";
            // 
            // mProblematicBrowseButton
            // 
            this.mProblematicBrowseButton.Location = new System.Drawing.Point(533, 107);
            this.mProblematicBrowseButton.Name = "mProblematicBrowseButton";
            this.mProblematicBrowseButton.Size = new System.Drawing.Size(123, 21);
            this.mProblematicBrowseButton.TabIndex = 3;
            this.mProblematicBrowseButton.Text = "选择";
            this.mProblematicBrowseButton.UseVisualStyleBackColor = true;
            this.mProblematicBrowseButton.Click += new System.EventHandler(this.mProblematicBrowseButton_Click);
            // 
            // mResolutionGroupBox
            // 
            this.mResolutionGroupBox.Controls.Add(this.mQualityTextBox);
            this.mResolutionGroupBox.Controls.Add(this.checkBox2);
            this.mResolutionGroupBox.Controls.Add(this.checkBox1);
            this.mResolutionGroupBox.Controls.Add(this.mResolutionPercentRadioButton);
            this.mResolutionGroupBox.Controls.Add(this.mResolutionDimensionTextBox);
            this.mResolutionGroupBox.Controls.Add(this.mResolutionDimensionRadioButton);
            this.mResolutionGroupBox.Controls.Add(this.mResolutionPercentTextBox);
            this.mResolutionGroupBox.Controls.Add(this.mQualityLabel);
            this.mResolutionGroupBox.Location = new System.Drawing.Point(12, 174);
            this.mResolutionGroupBox.Name = "mResolutionGroupBox";
            this.mResolutionGroupBox.Size = new System.Drawing.Size(644, 161);
            this.mResolutionGroupBox.TabIndex = 5;
            this.mResolutionGroupBox.TabStop = false;
            this.mResolutionGroupBox.Text = "配置";
            // 
            // mQualityTextBox
            // 
            this.mQualityTextBox.Location = new System.Drawing.Point(241, 47);
            this.mQualityTextBox.Name = "mQualityTextBox";
            this.mQualityTextBox.Size = new System.Drawing.Size(77, 21);
            this.mQualityTextBox.TabIndex = 10;
            this.mQualityTextBox.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(478, 104);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(144, 16);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "压缩完是否删除源文件";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(388, 104);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "是否覆盖";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // mResolutionPercentRadioButton
            // 
            this.mResolutionPercentRadioButton.AutoSize = true;
            this.mResolutionPercentRadioButton.Location = new System.Drawing.Point(19, 47);
            this.mResolutionPercentRadioButton.Name = "mResolutionPercentRadioButton";
            this.mResolutionPercentRadioButton.Size = new System.Drawing.Size(65, 16);
            this.mResolutionPercentRadioButton.TabIndex = 3;
            this.mResolutionPercentRadioButton.Text = "按比例:";
            this.mResolutionPercentRadioButton.UseVisualStyleBackColor = true;
            this.mResolutionPercentRadioButton.CheckedChanged += new System.EventHandler(this.ResolutionPercentRadioButton_CheckedChanged);
            // 
            // mResolutionDimensionTextBox
            // 
            this.mResolutionDimensionTextBox.Location = new System.Drawing.Point(90, 99);
            this.mResolutionDimensionTextBox.MaxLength = 4;
            this.mResolutionDimensionTextBox.Name = "mResolutionDimensionTextBox";
            this.mResolutionDimensionTextBox.Size = new System.Drawing.Size(70, 21);
            this.mResolutionDimensionTextBox.TabIndex = 5;
            // 
            // mResolutionDimensionRadioButton
            // 
            this.mResolutionDimensionRadioButton.AutoSize = true;
            this.mResolutionDimensionRadioButton.Checked = true;
            this.mResolutionDimensionRadioButton.Location = new System.Drawing.Point(19, 100);
            this.mResolutionDimensionRadioButton.Name = "mResolutionDimensionRadioButton";
            this.mResolutionDimensionRadioButton.Size = new System.Drawing.Size(65, 16);
            this.mResolutionDimensionRadioButton.TabIndex = 4;
            this.mResolutionDimensionRadioButton.TabStop = true;
            this.mResolutionDimensionRadioButton.Text = "按尺寸:";
            this.mResolutionDimensionRadioButton.UseVisualStyleBackColor = true;
            this.mResolutionDimensionRadioButton.CheckedChanged += new System.EventHandler(this.ResolutionDimensionRadioButton_CheckedChanged);
            // 
            // mResolutionPercentTextBox
            // 
            this.mResolutionPercentTextBox.Enabled = false;
            this.mResolutionPercentTextBox.Location = new System.Drawing.Point(90, 46);
            this.mResolutionPercentTextBox.MaxLength = 3;
            this.mResolutionPercentTextBox.Name = "mResolutionPercentTextBox";
            this.mResolutionPercentTextBox.Size = new System.Drawing.Size(70, 21);
            this.mResolutionPercentTextBox.TabIndex = 6;
            // 
            // mQualityLabel
            // 
            this.mQualityLabel.AutoSize = true;
            this.mQualityLabel.Location = new System.Drawing.Point(200, 51);
            this.mQualityLabel.Name = "mQualityLabel";
            this.mQualityLabel.Size = new System.Drawing.Size(35, 12);
            this.mQualityLabel.TabIndex = 0;
            this.mQualityLabel.Text = "品质:";
            // 
            // mCompressButton
            // 
            this.mCompressButton.Location = new System.Drawing.Point(12, 410);
            this.mCompressButton.Name = "mCompressButton";
            this.mCompressButton.Size = new System.Drawing.Size(678, 33);
            this.mCompressButton.TabIndex = 8;
            this.mCompressButton.Text = "压缩";
            this.mCompressButton.UseVisualStyleBackColor = true;
            this.mCompressButton.Click += new System.EventHandler(this.CompressButton_Click);
            // 
            // mBackgroundWorker
            // 
            this.mBackgroundWorker.WorkerReportsProgress = true;
            this.mBackgroundWorker.WorkerSupportsCancellation = true;
            this.mBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mBackgroundWorker_DoWork);
            this.mBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mBackgroundWorker_ProgressChanged);
            this.mBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mBackgroundWorker_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 457);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(696, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(692, 16);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 380);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.mInputLabel);
            this.tabPage1.Controls.Add(this.mResolutionGroupBox);
            this.tabPage1.Controls.Add(this.mProblematicBrowseButton);
            this.tabPage1.Controls.Add(this.mOutputBrowseButton);
            this.tabPage1.Controls.Add(this.mProblematicLabel);
            this.tabPage1.Controls.Add(this.mOutputTextBox);
            this.tabPage1.Controls.Add(this.mProblematicTextBox);
            this.tabPage1.Controls.Add(this.mOutputLabel);
            this.tabPage1.Controls.Add(this.mInputBrowseButton);
            this.tabPage1.Controls.Add(this.mInputTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(670, 354);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "选项";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(599, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "部分";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(670, 354);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "文件";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.size,
            this.status,
            this.Id});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(664, 348);
            this.dataGridView1.TabIndex = 0;
            // 
            // name
            // 
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            this.name.Width = 400;
            // 
            // size
            // 
            this.size.HeaderText = "大小";
            this.size.Name = "size";
            // 
            // status
            // 
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.Width = 150;
            // 
            // Id
            // 
            this.Id.HeaderText = "标识";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(670, 354);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "日志";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(670, 354);
            this.textBox1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // BICompressor
            // 
            this.ClientSize = new System.Drawing.Size(696, 479);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mCompressButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BICompressor";
            this.Text = "Batch Image Compressor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BICompressorForm_Closing);
            this.Load += new System.EventHandler(this.BICompressorForm_Load);
            this.mResolutionGroupBox.ResumeLayout(false);
            this.mResolutionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mQualityTextBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        private void BICompressorForm_Load(object sender, EventArgs e)
        {
            mResolutionDimensionTextBox.Text = mDefaultResolutionDimension.ToString();
            mResolutionPercentTextBox.Text = mDefaultResolutionPercent.ToString();
            mQualityTextBox.Text = mDefaultQuality.ToString();

            mParallelOptions = new ParallelOptions();
            mParallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

            this.CenterToParent();
        }

        private void BICompressorForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (mBackgroundWorker.IsBusy && !PromptCancel())
            {
                e.Cancel = true;
            }
        }

        private void InputBrowseButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(mInputTextBox.Text))
            {
                mInputBrowseDialog.SelectedPath = mInputTextBox.Text;
            }

            if (mInputBrowseDialog.ShowDialog() == DialogResult.OK)
            {
                mInputTextBox.Text = mInputBrowseDialog.SelectedPath;
            }
        }

        private void OutputBrowseButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(mOutputTextBox.Text))
            {
                mOutputBrowseDialog.SelectedPath = mOutputTextBox.Text;
            }


            if (mOutputBrowseDialog.ShowDialog() == DialogResult.OK)
            {
                mOutputTextBox.Text = mOutputBrowseDialog.SelectedPath;
            }
        }

        private void mProblematicBrowseButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(mProblematicTextBox.Text))
            {
                mProblematicBrowseDialog.SelectedPath = mProblematicTextBox.Text;
            }
            if (mProblematicBrowseDialog.ShowDialog() == DialogResult.OK)
            {
                mProblematicTextBox.Text = mProblematicBrowseDialog.SelectedPath;
            }
        }

        private void ResolutionDimensionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            mResolutionDimensionTextBox.Enabled = true;
            mResolutionPercentTextBox.Enabled = false;
        }

        private void ResolutionPercentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            mResolutionDimensionTextBox.Enabled = false;
            mResolutionPercentTextBox.Enabled = true;
        }

        private void CompressButton_Click(object sender, EventArgs e)
        {
            if (mBackgroundWorker.IsBusy)
            {
                PromptCancel();
            }
            else
            {
                String validationResult = ValidateUserInput();
                if (!String.IsNullOrEmpty(validationResult))
                {
                    MessageBox.Show(
                        this,
                        validationResult,
                        msTitleBarText,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                mBackgroundWorker.RunWorkerAsync();
                mCompressButton.Text = "取消";
            }
        }

        private void mBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //重置进度
            ResetStatistics(mInputTextBox.Text);

            //压缩
            Compress(mInputTextBox.Text, mOutputTextBox.Text);

            if (mBackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void mBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UPP(e.ProgressPercentage);
            this.Text = msTitleBarText + " - " + (e.ProgressPercentage.ToString() + "%");
        }

        private void mBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string message = null;
            MessageBoxIcon icon;

            if (e.Error != null)
            {
                message = e.Error.Message;
                icon = MessageBoxIcon.Error;
            }
            else if (e.Cancelled)
            {
                message = "已取消!";
                icon = MessageBoxIcon.Warning;
            }
            else
            {
                message = "完成.";
                icon = MessageBoxIcon.Information;
            }

            if (this.checkBox2.Checked)
            {
                RevomeSourcesFile();
            }

            MessageBox.Show(
                this,
                message,
                msTitleBarText,
                MessageBoxButtons.OK,
                icon
            );

            this.Text = msTitleBarText;
            mCompressButton.Text = "压缩";
            UPP(0);
        }


        private void RevomeSourcesFile()
        {
            Parallel.ForEach(
               Directory.GetFiles(mInputTextBox.Text),
               mParallelOptions,
               (file) =>
               {
                   try
                   {
                       var f = new FileInfo(file);
                       f.Delete();
                   }
                   catch (Exception exx)
                   {
                       System.Diagnostics.Debug.Print(exx.Message);
                   }
               }
           );
        }


        private String ValidateUserInput()
        {
            if (String.IsNullOrEmpty(mInputTextBox.Text))
            {
                return "输入目录不能为空！请选择要压缩其图像的有效目录.";
            }
            if (!Directory.Exists(mInputTextBox.Text))
            {
                return "文件目录不存在.";
            }
                 
            if (String.IsNullOrEmpty(mOutputTextBox.Text))
            {
                return "输出目录不能为空！请选择要存储压缩图像的有效目录.";
            }
            if (!Directory.Exists(mOutputTextBox.Text))
            {
                return "输出目录不存在.";
            }

            if (String.IsNullOrEmpty(mProblematicTextBox.Text))
            {
                return "有问题的目录不能为空！请选择一个有效的目录，将所有有问题的图像复制到其中。如果批处理图像压缩器由于任何原因未能压缩图像，则认为这些图像有问题。";
            }
            if (!Directory.Exists(mProblematicTextBox.Text))
            {
                return "转移目录不存在.";
            }

            if (Directory.EnumerateFileSystemEntries(mOutputTextBox.Text).GetEnumerator().MoveNext() && !this.checkBox1.Checked)
            {
                return "为避免覆盖现有文件，输出目录必须为空。";
            }

            if (Directory.EnumerateFileSystemEntries(mProblematicTextBox.Text).GetEnumerator().MoveNext() && !this.checkBox1.Checked)
            {
                return "为了避免覆盖现有文件，有问题的目录必须为空.";
            }

            string invalidResolutionMessage = null;
            try
            {
                if (mResolutionDimensionRadioButton.Enabled)
                {
                    int resolution = System.Convert.ToInt32(mResolutionDimensionTextBox.Text);
                    if (resolution < 1 || resolution > 9999)
                    {
                        invalidResolutionMessage = "分辨率维度必须是范围为[1，9999]的整数.";
                    }
                }

                if (mResolutionPercentRadioButton.Enabled)
                {
                    int resolution = System.Convert.ToInt32(mResolutionPercentTextBox.Text);
                    if (resolution < 1 || resolution > 100)
                    {
                        invalidResolutionMessage = "分辨率百分比必须是[1，100]范围内的整数.";
                    }
                }
            }
            catch (Exception)
            {
            }

            if (!string.IsNullOrEmpty(invalidResolutionMessage))
            {
                return invalidResolutionMessage;
            }

            bool invalidQuality = false;
            try
            {
                int quality = System.Convert.ToInt32(mQualityTextBox.Text);
                invalidQuality = quality < 0 || quality > 100;
            }
            catch (Exception)
            {
                invalidQuality = true;
            }

            if (invalidQuality)
            {
                return "质量必须是范围[0，100]内的整数.";
            }

            return null;
        }

        private void ResetStatistics(string inputDirectory)
        {
            GetImageCount(inputDirectory, ref mImageCount);
            mCompressedCount = 0;
        }

        //progressBar1
        private void UpdateStatistics()
        {
            try
            {
                Interlocked.Add(ref mCompressedCount, 100);
                var p = mCompressedCount / mImageCount;
                mBackgroundWorker.ReportProgress(p > 100 ? 100 : p);
            }
            catch (Exception) { }
        }


        public void UPP(int p)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    progressBar1.Value = p > 100 ? 100 : p;
                    Application.DoEvents();
                }));
                return;
            }
            progressBar1.Value = p > 100 ? 100 : p;
            Application.DoEvents();
        }



        private void Compress(string inputDirectory, string outputDirectory)
        {
            using (EncoderParameters encoderParameters = new EncoderParameters())
            {
                System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
                using (encoderParameters.Param[0] = new EncoderParameter(encoder, System.Convert.ToInt32(mQualityTextBox.Text)))
                {
                    if (this.TaskFiles.Count > 0)
                    {
                        mCompressedCount = this.TaskFiles.Count;
                        CompressFromSelect(outputDirectory, encoderParameters);
                    }
                    else
                    {
                        Compress(inputDirectory, outputDirectory, encoderParameters);
                    }
                }
            }
        }


        private void CompressFromSelect(string outputDirectory, EncoderParameters encoderParameters)
        {
            if (mBackgroundWorker.CancellationPending)
            {
                return;
            }

            mParallelOptions.MaxDegreeOfParallelism = 5;
            Parallel.ForEach(
                this.TaskFiles,
                mParallelOptions,
                (file, loop) =>
                {
                    if (mBackgroundWorker.CancellationPending)
                    {
                        loop.Stop();
                    }

                    try
                    {
                        using (Image image = Image.FromFile(file.Path))
                        {
                            Compress(image, outputDirectory, encoderParameters, file.Name);
                            
                            //报告进度
                            UpdateStatistics();

                            LogText($"压缩{file.Path}");
                        }
                    }
                    catch (Exception exx)
                    {
                        System.Diagnostics.Debug.Print(exx.Message);


                        LogText($"处理失败 {exx.Message}  with  {file.Path}");
                        File.Copy(file.Path,
                            Path.Combine(mProblematicTextBox.Text, Path.GetFileName(file.Path)),
                            this.checkBox1.Checked);
                    }
                    finally 
                    {
                        //更新GridView
                        Task.Run(() =>
                        {
                            //this.TaskFiles.TryDequeue(out TaskFile t);
                            LogText($"更新GridView {file.Name}");
                            InvokeRemoveGridView(this.dataGridView1, file);
                        });
                    }
                }
            );

            if (mBackgroundWorker.CancellationPending)
            {
                return;
            }
        }


        private void Compress(string inputDirectory, string outputDirectory, EncoderParameters encoderParameters)
        {
            if (mBackgroundWorker.CancellationPending)
            {
                return;
            }

            Parallel.ForEach(
                Directory.GetFiles(inputDirectory),
                mParallelOptions,
                (file, loop) =>
                {
                    if (mBackgroundWorker.CancellationPending)
                    {
                        loop.Stop();
                    }

                    if (CanCompress(file))
                    {
                        try
                        {
                            var f = new FileInfo(file);
                            using (Image image = Image.FromFile(file))
                            {
                                Compress(image, outputDirectory, encoderParameters, f.Name);
                                //报告进度
                                UpdateStatistics();
                            }
                        }
                        catch (Exception exx)
                        {
                            System.Diagnostics.Debug.Print(exx.Message);
                            File.Copy(file,
                                Path.Combine(mProblematicTextBox.Text, Path.GetFileName(file)),
                                this.checkBox1.Checked);
                        }
                    }

                }
            );

            if (mBackgroundWorker.CancellationPending)
            {
                return;
            }

            string[] directories = Directory.GetDirectories(inputDirectory);
            foreach (string directory in directories)
            {
                Compress(directory, outputDirectory, encoderParameters);
            }
        }

        private void Compress(Image image, string outputDirectory, EncoderParameters encoderParameters, string fname = "")
        {
            int width = image.Width;
            int height = image.Height;
            AdjustResolution(ref width, ref height);

            using (Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                foreach (PropertyItem propertyItem in image.PropertyItems)
                {
                    bitmap.SetPropertyItem(propertyItem);
                }

                try
                {
                    float xdpi = BitConverter.ToInt32(image.GetPropertyItem(0x011A).Value, 0);
                    float ydpi = BitConverter.ToInt32(image.GetPropertyItem(0x011B).Value, 0);
                    bitmap.SetResolution(xdpi, ydpi);
                }
                catch (ArgumentException)
                {
                }

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;

                    graphics.DrawImage(
                        image,
                        new Rectangle(
                            0, 0,
                            bitmap.Width, bitmap.Height
                        ),
                        0, 0,
                        image.Width, image.Height,
                        GraphicsUnit.Pixel
                    );

                    var name = string.IsNullOrEmpty(fname) ? GetImageName(image) : fname;
                    bitmap.Save(
                        Path.Combine(outputDirectory, name),
                        msImageCodecInfo,
                        encoderParameters
                    );
                }
            }
        }

        private void AdjustResolution(ref int width, ref int height)
        {
            if (mResolutionDimensionTextBox.Enabled)
            {
                float shortEdge = Math.Min(width, height);
                float longEdge = Math.Max(width, height);

                float dimension = System.Convert.ToSingle(mResolutionDimensionTextBox.Text);
                if (dimension < longEdge)
                {
                    shortEdge *= (dimension / longEdge);
                    longEdge = dimension;

                    if (width < height)
                    {
                        width = (int)shortEdge;
                        height = (int)longEdge;
                    }
                    else
                    {
                        width = (int)longEdge;
                        height = (int)shortEdge;
                    }
                }
            }
            else if (mResolutionPercentTextBox.Enabled)
            {
                float factor = System.Convert.ToSingle(mResolutionPercentTextBox.Text) / 100.0f;
                width = (int)(width * factor);
                height = (int)(height * factor);
            }
        }

        private bool PromptCancel()
        {
            DialogResult cancelDialogResult = MessageBox.Show(
                this,
                "确定要放弃吗?",
                msTitleBarText,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            bool shouldCancel = (cancelDialogResult == DialogResult.Yes);
            if (shouldCancel)
            {
                mBackgroundWorker.CancelAsync();
                mCompressButton.Text = "压缩";
            }

            return shouldCancel;
        }

        private static bool CanCompress(string file)
        {
            String extension = Path.GetExtension(file).ToLower();
            return !String.IsNullOrEmpty(extension) &&
                (
                    extension.Equals(".jpg") ||
                    extension.Equals(".jpeg") ||
                    extension.Equals(".bmp") ||
                    extension.Equals(".png") ||
                    extension.Equals(".gif") ||
                    extension.Equals(".tiff")
                );
        }

        private static void GetImageCount(string inputDirectory, ref int count)
        {
            string[] files = Directory.GetFiles(inputDirectory);
            foreach (string file in files)
            {
                if (CanCompress(file))
                {
                    ++count;
                }
            }

            string[] directories = Directory.GetDirectories(inputDirectory);
            foreach (string directory in directories)
            {
                GetImageCount(directory, ref count);
            }
        }

        private static string GetImageName(Image image)
        {
            string dateTime = Encoding.UTF8.GetString(GetDateTime(image).Value);
            return ParseDateTime(dateTime).ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";
        }

        private static PropertyItem GetDateTime(Image image)
        {
            foreach (int tag in msTags)
            {
                try
                {
                    return image.GetPropertyItem(tag);
                }
                catch (ArgumentException)
                {
                    continue;
                }
            }

            return null;
        }

        private static DateTime ParseDateTime(string dateTime)
        {
            foreach (string format in msFormats)
            {
                try
                {
                    return DateTime.ParseExact(
                        dateTime,
                        format,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None
                    );
                }
                catch (ArgumentException)
                {
                    continue;
                }
            }

            return DateTime.Parse(
                dateTime,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None
            );
        }

        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }
        #endregion

        #region Private Members
        private static readonly string msTitleBarText = "Batch Image Compressor";
        private static readonly ImageCodecInfo msImageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);
        private static readonly int[] msTags = new int[] { 0x9003, 0x0132 };
        private static readonly string[] msFormats = new string[] { "yyyy':'MM':'dd HH':'mm':'ss\0" };

        private const int mDefaultResolutionDimension = 1400;
        private const int mDefaultResolutionPercent = 65;
        private const int mDefaultQuality = 60;

        private Label mInputLabel;
        private TextBox mInputTextBox;
        private Button mInputBrowseButton;
        private FolderBrowserDialog mInputBrowseDialog;
        private Label mOutputLabel;
        private TextBox mOutputTextBox;
        private Button mOutputBrowseButton;
        private FolderBrowserDialog mOutputBrowseDialog;
        private Label mProblematicLabel;
        private TextBox mProblematicTextBox;
        private Button mProblematicBrowseButton;
        private GroupBox mResolutionGroupBox;
        private RadioButton mResolutionDimensionRadioButton;
        private TextBox mResolutionDimensionTextBox;
        private RadioButton mResolutionPercentRadioButton;
        private TextBox mResolutionPercentTextBox;
        private Label mQualityLabel;
        private Button mCompressButton;
        private BackgroundWorker mBackgroundWorker;

        private ParallelOptions mParallelOptions;
        private int mImageCount;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar progressBar1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Button button1;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private OpenFileDialog openFileDialog1;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn size;
        private DataGridViewTextBoxColumn status;
        private DataGridViewTextBoxColumn Id;
        private int mCompressedCount;


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Addfiles();
        }

        private void Addfiles()
        {
            DialogResult result;

            if (Directory.Exists(mInputTextBox.Text))
            {
                this.openFileDialog1.InitialDirectory = mInputTextBox.Text;
            }

            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.DefaultExt = ".jpg";
            this.openFileDialog1.AddExtension = true;
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.FilterIndex = 0;
            this.openFileDialog1.Filter = "jpg(*.jpg)|*.jpg|*.bmp|*.png|*.gif|*.tiff||*.*";
            result = this.openFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            this.tabControl1.SelectedIndex = 1;

            var fNames = this.openFileDialog1.FileNames;
            foreach (var fn in fNames)
            {
                var f = new FileInfo(fn);

                var tf = new TaskFile()
                {
                    Name = f.Name,
                    Size = FormatFileSize(f.Length),
                    Status = "等待",
                    Path= fn
                };

                this.TaskFiles.Enqueue(tf);

                AddGridView(this.dataGridView1, tf);
            }
        }


        public void AddGridView(DataGridView dataGridView, TaskFile f)
        {
            try
            {
                int index = dataGridView.Rows.Add();
                dataGridView.Rows[index].Cells[0].Value = f.Name;
                dataGridView.Rows[index].Cells[1].Value = f.Size;
                dataGridView.Rows[index].Cells[2].Value = f.Status;
                dataGridView.Rows[index].Cells[3].Value = f.Id;
            }
            catch (Exception ex)
            {
                LogText(ex.Message + ";" + ex.StackTrace);
            }
        }


        public void LogText(string msg)
        {
            //var dir = AppDomain.CurrentDomain.BaseDirectory + "logs\\";
            //if (!Directory.Exists(dir))
            //    Directory.CreateDirectory(dir);

            //using (var w = new StreamWriter(dir + DateTime.Now.ToString("yyyy-MM-dd") + ".log", true))
            //{
            //    w.WriteLine(DateTime.Now.ToString() + ": " + msg);
            //    w.Close();
            //}

            if (textBox1.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    textBox1.AppendText(msg + "\r\n");
                    if (this.textBox1.Lines.Count() > 50)
                    {
                        this.textBox1.Text = "";
                    }
                    Application.DoEvents();
                }));
                return;
            }

            textBox1.AppendText(msg + "\r\n");
            if (this.textBox1.Lines.Count() > 50)
            {
                this.textBox1.Text = "";
            }
            Application.DoEvents();
        }




        public string FormatFileSize(Int64 fileSize)
        {
            if (fileSize < 0)
                return "0";
            else if (fileSize >= 1024 * 1024 * 1024)
                return string.Format("{0:########0.00} GB", ((Double)fileSize) / (1024 * 1024 * 1024));
            else if (fileSize >= 1024 * 1024)
                return string.Format("{0:####0.00} MB", ((Double)fileSize) / (1024 * 1024));
            else if (fileSize >= 1024)
                return string.Format("{0:####0.00} KB", ((Double)fileSize) / 1024);
            else
                return string.Format("{0} bytes", fileSize);
        }




        public void InvokeUpdateGridView(DataGridView dataGridView, TaskFile task)
        {
            if (dataGridView.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateGridView(dataGridView, task);
                }));
                return;
            }
            UpdateGridView(dataGridView, task);
        }

        public void UpdateGridView(DataGridView dataGridView, TaskFile task)
        {
            try
            {
                foreach (DataGridViewRow r in dataGridView.Rows)
                {
                    if (!r.IsNewRow && (r.Cells["Id"].Value.ToString() == task.Id.ToString()))
                    {
                        r.Cells["status"].Value = "完成";
                        r.Cells["status"].Style.BackColor = Color.LightPink;
                    }
                }
                dataGridView.Sort(dataGridView.Columns["status"], ListSortDirection.Ascending);
            }
            catch (Exception ex)
            {
                LogText(ex.Message + ";" + ex.StackTrace);
            }
        }


        public void InvokeRemoveGridView(DataGridView dataGridView, TaskFile task)
        {
            if (dataGridView.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    RemoveGridView(dataGridView, task);
                }));
                return;
            }
            RemoveGridView(dataGridView, task);
        }
        public void RemoveGridView(DataGridView dataGridView, TaskFile task)
        {
            try
            {
                foreach (DataGridViewRow r in dataGridView.Rows)
                {
                    if (!r.IsNewRow && (r.Cells["Id"].Value.ToString() == task.Id.ToString()))
                    {
                        dataGridView.Rows.Remove(r);
                    }
                }
            }
            catch (Exception ex)
            {
                LogText(ex.Message + ";" + ex.StackTrace);
            }
        }
    }



    public class TaskFile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string Path { get; set; }
    }
}
