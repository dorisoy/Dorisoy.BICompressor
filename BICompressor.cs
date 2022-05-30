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
using System.Collections;
using FluentScheduler;

namespace Dorisoy.BICompressor
{
    /// <summary>
    /// Dorisoy 批量图片压缩器（BICompressor）
    /// </summary>
    public partial class BICompressor : Form
    {
        public bool IsRunning = true;
        private string Timer = "00:00";
        private int TimeInterval = 5;
        private Button button2;
        private TextBox txt_sourceFilePath;
        private Label label4;
        private NumericUpDown pud_OnceOfQuantity;
        private Label label5;
        private Label label6;
        private NumericUpDown mud_fileLimitSize;
        private Label label7;
        private int Weekday = 1;

        #region Public Methods
        public BICompressor()
        {
            InitializeComponent();
            progressBar1.Maximum = 100;
        }
        #endregion

        private FolderBrowserDialog mProblematicBrowseDialog;
        private NumericUpDown mQualityTextBox;
        private RadioButton rb_autoCompress;
        private RadioButton radioButton1;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private ComboBox comboBox1;
        private Label label1;
        private Label label3;
        private GroupBox groupBox1;
        private RadioButton radioButton7;
        private RadioButton radioButton6;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton8;
        private RadioButton radioButton9;
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
            this.label6 = new System.Windows.Forms.Label();
            this.mud_fileLimitSize = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pud_OnceOfQuantity = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.button2 = new System.Windows.Forms.Button();
            this.txt_sourceFilePath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.rb_autoCompress = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.mud_fileLimitSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pud_OnceOfQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mQualityTextBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mInputLabel
            // 
            this.mInputLabel.AutoSize = true;
            this.mInputLabel.Location = new System.Drawing.Point(10, 63);
            this.mInputLabel.Name = "mInputLabel";
            this.mInputLabel.Size = new System.Drawing.Size(59, 12);
            this.mInputLabel.TabIndex = 0;
            this.mInputLabel.Text = "压缩目录:";
            this.mInputLabel.Click += new System.EventHandler(this.mInputLabel_Click);
            // 
            // mInputTextBox
            // 
            this.mInputTextBox.Location = new System.Drawing.Point(75, 60);
            this.mInputTextBox.Name = "mInputTextBox";
            this.mInputTextBox.Size = new System.Drawing.Size(438, 21);
            this.mInputTextBox.TabIndex = 0;
            this.mInputTextBox.TabStop = false;
            this.mInputTextBox.Text = "D:\\BIC-input";
            // 
            // mInputBrowseButton
            // 
            this.mInputBrowseButton.Location = new System.Drawing.Point(535, 58);
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
            this.mOutputLabel.Location = new System.Drawing.Point(10, 100);
            this.mOutputLabel.Name = "mOutputLabel";
            this.mOutputLabel.Size = new System.Drawing.Size(59, 12);
            this.mOutputLabel.TabIndex = 0;
            this.mOutputLabel.Text = "压缩输出:";
            // 
            // mOutputTextBox
            // 
            this.mOutputTextBox.Location = new System.Drawing.Point(75, 96);
            this.mOutputTextBox.Name = "mOutputTextBox";
            this.mOutputTextBox.Size = new System.Drawing.Size(438, 21);
            this.mOutputTextBox.TabIndex = 0;
            this.mOutputTextBox.TabStop = false;
            this.mOutputTextBox.Text = "D:\\BIC-out";
            // 
            // mOutputBrowseButton
            // 
            this.mOutputBrowseButton.Location = new System.Drawing.Point(535, 96);
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
            this.mProblematicLabel.Location = new System.Drawing.Point(10, 135);
            this.mProblematicLabel.Name = "mProblematicLabel";
            this.mProblematicLabel.Size = new System.Drawing.Size(59, 12);
            this.mProblematicLabel.TabIndex = 0;
            this.mProblematicLabel.Text = "失败转移:";
            // 
            // mProblematicTextBox
            // 
            this.mProblematicTextBox.Location = new System.Drawing.Point(75, 131);
            this.mProblematicTextBox.Name = "mProblematicTextBox";
            this.mProblematicTextBox.Size = new System.Drawing.Size(438, 21);
            this.mProblematicTextBox.TabIndex = 0;
            this.mProblematicTextBox.TabStop = false;
            this.mProblematicTextBox.Text = "D:\\BIC-pr";
            // 
            // mProblematicBrowseButton
            // 
            this.mProblematicBrowseButton.Location = new System.Drawing.Point(535, 131);
            this.mProblematicBrowseButton.Name = "mProblematicBrowseButton";
            this.mProblematicBrowseButton.Size = new System.Drawing.Size(123, 21);
            this.mProblematicBrowseButton.TabIndex = 3;
            this.mProblematicBrowseButton.Text = "选择";
            this.mProblematicBrowseButton.UseVisualStyleBackColor = true;
            this.mProblematicBrowseButton.Click += new System.EventHandler(this.mProblematicBrowseButton_Click);
            // 
            // mResolutionGroupBox
            // 
            this.mResolutionGroupBox.Controls.Add(this.label6);
            this.mResolutionGroupBox.Controls.Add(this.mud_fileLimitSize);
            this.mResolutionGroupBox.Controls.Add(this.label7);
            this.mResolutionGroupBox.Controls.Add(this.label5);
            this.mResolutionGroupBox.Controls.Add(this.pud_OnceOfQuantity);
            this.mResolutionGroupBox.Controls.Add(this.label4);
            this.mResolutionGroupBox.Controls.Add(this.label3);
            this.mResolutionGroupBox.Controls.Add(this.numericUpDown1);
            this.mResolutionGroupBox.Controls.Add(this.label2);
            this.mResolutionGroupBox.Controls.Add(this.comboBox1);
            this.mResolutionGroupBox.Controls.Add(this.label1);
            this.mResolutionGroupBox.Controls.Add(this.mQualityTextBox);
            this.mResolutionGroupBox.Controls.Add(this.checkBox2);
            this.mResolutionGroupBox.Controls.Add(this.checkBox1);
            this.mResolutionGroupBox.Controls.Add(this.mResolutionPercentRadioButton);
            this.mResolutionGroupBox.Controls.Add(this.mResolutionDimensionTextBox);
            this.mResolutionGroupBox.Controls.Add(this.mResolutionDimensionRadioButton);
            this.mResolutionGroupBox.Controls.Add(this.mResolutionPercentTextBox);
            this.mResolutionGroupBox.Controls.Add(this.mQualityLabel);
            this.mResolutionGroupBox.Location = new System.Drawing.Point(14, 250);
            this.mResolutionGroupBox.Name = "mResolutionGroupBox";
            this.mResolutionGroupBox.Size = new System.Drawing.Size(644, 135);
            this.mResolutionGroupBox.TabIndex = 5;
            this.mResolutionGroupBox.TabStop = false;
            this.mResolutionGroupBox.Text = "选项";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(447, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "KB压缩";
            // 
            // mud_fileLimitSize
            // 
            this.mud_fileLimitSize.Enabled = false;
            this.mud_fileLimitSize.Location = new System.Drawing.Point(364, 67);
            this.mud_fileLimitSize.Maximum = new decimal(new int[] {
            102400,
            0,
            0,
            0});
            this.mud_fileLimitSize.Name = "mud_fileLimitSize";
            this.mud_fileLimitSize.Size = new System.Drawing.Size(77, 21);
            this.mud_fileLimitSize.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "超过：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(599, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "张";
            // 
            // pud_OnceOfQuantity
            // 
            this.pud_OnceOfQuantity.Enabled = false;
            this.pud_OnceOfQuantity.Location = new System.Drawing.Point(542, 35);
            this.pud_OnceOfQuantity.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.pud_OnceOfQuantity.Name = "pud_OnceOfQuantity";
            this.pud_OnceOfQuantity.Size = new System.Drawing.Size(54, 21);
            this.pud_OnceOfQuantity.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(481, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "每次处理：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "/分钟";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(364, 33);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 21);
            this.numericUpDown1.TabIndex = 14;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "定时间隔：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(230, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(57, 20);
            this.comboBox1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "执行时间：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // mQualityTextBox
            // 
            this.mQualityTextBox.Location = new System.Drawing.Point(230, 63);
            this.mQualityTextBox.Name = "mQualityTextBox";
            this.mQualityTextBox.Size = new System.Drawing.Size(57, 21);
            this.mQualityTextBox.TabIndex = 10;
            this.mQualityTextBox.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.mQualityTextBox.ValueChanged += new System.EventHandler(this.mQualityTextBox_ValueChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(119, 103);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(168, 16);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "压缩完是否删除源目录文件";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 103);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "是否覆盖";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // mResolutionPercentRadioButton
            // 
            this.mResolutionPercentRadioButton.AutoSize = true;
            this.mResolutionPercentRadioButton.Location = new System.Drawing.Point(6, 35);
            this.mResolutionPercentRadioButton.Name = "mResolutionPercentRadioButton";
            this.mResolutionPercentRadioButton.Size = new System.Drawing.Size(71, 16);
            this.mResolutionPercentRadioButton.TabIndex = 3;
            this.mResolutionPercentRadioButton.Text = "按比例：";
            this.mResolutionPercentRadioButton.UseVisualStyleBackColor = true;
            this.mResolutionPercentRadioButton.CheckedChanged += new System.EventHandler(this.ResolutionPercentRadioButton_CheckedChanged);
            // 
            // mResolutionDimensionTextBox
            // 
            this.mResolutionDimensionTextBox.Location = new System.Drawing.Point(77, 67);
            this.mResolutionDimensionTextBox.MaxLength = 4;
            this.mResolutionDimensionTextBox.Name = "mResolutionDimensionTextBox";
            this.mResolutionDimensionTextBox.Size = new System.Drawing.Size(57, 21);
            this.mResolutionDimensionTextBox.TabIndex = 5;
            // 
            // mResolutionDimensionRadioButton
            // 
            this.mResolutionDimensionRadioButton.AutoSize = true;
            this.mResolutionDimensionRadioButton.Checked = true;
            this.mResolutionDimensionRadioButton.Location = new System.Drawing.Point(6, 68);
            this.mResolutionDimensionRadioButton.Name = "mResolutionDimensionRadioButton";
            this.mResolutionDimensionRadioButton.Size = new System.Drawing.Size(71, 16);
            this.mResolutionDimensionRadioButton.TabIndex = 4;
            this.mResolutionDimensionRadioButton.TabStop = true;
            this.mResolutionDimensionRadioButton.Text = "按尺寸：";
            this.mResolutionDimensionRadioButton.UseVisualStyleBackColor = true;
            this.mResolutionDimensionRadioButton.CheckedChanged += new System.EventHandler(this.ResolutionDimensionRadioButton_CheckedChanged);
            // 
            // mResolutionPercentTextBox
            // 
            this.mResolutionPercentTextBox.Enabled = false;
            this.mResolutionPercentTextBox.Location = new System.Drawing.Point(77, 34);
            this.mResolutionPercentTextBox.MaxLength = 3;
            this.mResolutionPercentTextBox.Name = "mResolutionPercentTextBox";
            this.mResolutionPercentTextBox.Size = new System.Drawing.Size(57, 21);
            this.mResolutionPercentTextBox.TabIndex = 6;
            // 
            // mQualityLabel
            // 
            this.mQualityLabel.AutoSize = true;
            this.mQualityLabel.Location = new System.Drawing.Point(173, 69);
            this.mQualityLabel.Name = "mQualityLabel";
            this.mQualityLabel.Size = new System.Drawing.Size(41, 12);
            this.mQualityLabel.TabIndex = 0;
            this.mQualityLabel.Text = "品质：";
            this.mQualityLabel.Click += new System.EventHandler(this.mQualityLabel_Click);
            // 
            // mCompressButton
            // 
            this.mCompressButton.Location = new System.Drawing.Point(9, 435);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 471);
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
            this.tabControl1.Size = new System.Drawing.Size(678, 417);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.txt_sourceFilePath);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.rb_autoCompress);
            this.tabPage1.Controls.Add(this.radioButton1);
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
            this.tabPage1.Size = new System.Drawing.Size(670, 391);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "系统配置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(535, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "监控目录";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_sourceFilePath
            // 
            this.txt_sourceFilePath.Location = new System.Drawing.Point(197, 24);
            this.txt_sourceFilePath.Name = "txt_sourceFilePath";
            this.txt_sourceFilePath.Size = new System.Drawing.Size(316, 21);
            this.txt_sourceFilePath.TabIndex = 20;
            this.txt_sourceFilePath.Text = "D:\\BIC-mon";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton7);
            this.groupBox1.Controls.Add(this.radioButton6);
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton8);
            this.groupBox1.Controls.Add(this.radioButton9);
            this.groupBox1.Location = new System.Drawing.Point(12, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(646, 63);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计划";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Enabled = false;
            this.radioButton7.Location = new System.Drawing.Point(560, 29);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(59, 16);
            this.radioButton7.TabIndex = 30;
            this.radioButton7.Text = "星期天";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Enabled = false;
            this.radioButton6.Location = new System.Drawing.Point(470, 29);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(59, 16);
            this.radioButton6.TabIndex = 29;
            this.radioButton6.Text = "星期六";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Enabled = false;
            this.radioButton5.Location = new System.Drawing.Point(380, 29);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(59, 16);
            this.radioButton5.TabIndex = 28;
            this.radioButton5.Text = "星期五";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Enabled = false;
            this.radioButton4.Location = new System.Drawing.Point(290, 29);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(59, 16);
            this.radioButton4.TabIndex = 27;
            this.radioButton4.Text = "星期四";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Enabled = false;
            this.radioButton3.Location = new System.Drawing.Point(200, 29);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(59, 16);
            this.radioButton3.TabIndex = 26;
            this.radioButton3.Text = "星期三";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Enabled = false;
            this.radioButton8.Location = new System.Drawing.Point(110, 29);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(59, 16);
            this.radioButton8.TabIndex = 25;
            this.radioButton8.Text = "星期二";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Checked = true;
            this.radioButton9.Enabled = false;
            this.radioButton9.Location = new System.Drawing.Point(20, 29);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(59, 16);
            this.radioButton9.TabIndex = 24;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "星期一";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // rb_autoCompress
            // 
            this.rb_autoCompress.AutoSize = true;
            this.rb_autoCompress.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rb_autoCompress.Location = new System.Drawing.Point(102, 24);
            this.rb_autoCompress.Name = "rb_autoCompress";
            this.rb_autoCompress.Size = new System.Drawing.Size(71, 16);
            this.rb_autoCompress.TabIndex = 8;
            this.rb_autoCompress.Text = "自动模式";
            this.rb_autoCompress.UseVisualStyleBackColor = true;
            this.rb_autoCompress.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 24);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 16);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "普通模式";
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(598, 58);
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
            this.tabPage2.Size = new System.Drawing.Size(670, 391);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "文件队列";
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
            this.dataGridView1.Size = new System.Drawing.Size(664, 385);
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
            this.tabPage3.Size = new System.Drawing.Size(670, 391);
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
            this.textBox1.Size = new System.Drawing.Size(670, 391);
            this.textBox1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // BICompressor
            // 
            this.ClientSize = new System.Drawing.Size(696, 493);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mCompressButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BICompressor";
            this.Text = "Dorisoy.BICompressor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BICompressorForm_Closing);
            this.Load += new System.EventHandler(this.BICompressorForm_Load);
            this.mResolutionGroupBox.ResumeLayout(false);
            this.mResolutionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mud_fileLimitSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pud_OnceOfQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mQualityTextBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
            this.txt_sourceFilePath.Enabled = false;
            this.button2.Enabled = false;
            //
            InitComBox();


            //load once of quantity
            int onceOfQuantity= AppConfig.GetOnceOfQuantity();
            this.pud_OnceOfQuantity.Value= onceOfQuantity;

            //load fileLimitSize
            int fileLimitSize = AppConfig.GetFileLimitSize();
            this.mud_fileLimitSize.Value = fileLimitSize;

            this.CenterToParent();

        }

        private void InitComBox()
        {
            this.numericUpDown1.Value =0;

            var mylist = new ArrayList();
            for (var i = 0; i < 24; i++)
            {
                if (i >= 10)
                    mylist.Add(new DictionaryEntry(string.Format("{0}:00", i), string.Format("{0}:00", i)));
                else
                    mylist.Add(new DictionaryEntry(string.Format("0{0}:00", i), string.Format("0{0}:00", i)));
            }

            if (mylist.Count > 0)
            {
                comboBox1.DataSource = mylist;
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                comboBox1.SelectedIndex = 0;
            }
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
            string validationResult = ValidateUserInput();
            if (!string.IsNullOrEmpty(validationResult))
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

            var jobs = JobManager.AllSchedules;
            if (!this.rb_autoCompress.Checked && jobs.Count() == 0)
            {
                ProcessCompressor();
            }
            else
            {
                if (jobs.Count() == 0)
                    RunAutoMode();
                else
                {
                    LogText(DateTime.Now.ToString() + "  自动任务取消\n");

                    JobManager.RemoveAllJobs();
                    JobManager.Stop();

                    if (mBackgroundWorker.IsBusy)
                    {
                        PromptCancel();
                    }
                    else
                    {
                        mCompressButton.Text = "开始";
                    }
                }
            }
        }

        private void RunAutoMode()
        {
            if (comboBox1.SelectedValue != null)
            {
                Timer = comboBox1.SelectedValue.ToString();
            }

            this.TimeInterval = Convert.ToInt32(this.numericUpDown1.Value);

            for (int i = 0; i < groupBox1.Controls.Count; i++)
            {
                if (groupBox1.Controls[i] is RadioButton)
                {
                    var temp = (RadioButton)groupBox1.Controls[i];
                    if (temp.Checked)
                    {
                        switch (temp.Text)
                        {
                            case "星期一":
                                Weekday = 1;
                                break;
                            case "星期二":
                                Weekday = 2;
                                break;
                            case "星期三":
                                Weekday = 3;
                                break;
                            case "星期四":
                                Weekday = 4;
                                break;
                            case "星期五":
                                Weekday = 5;
                                break;
                            case "星期六":
                                Weekday = 6;
                                break;
                            case "星期天":
                                Weekday = 7;
                                break;
                        }
                    }
                }
            }

            //开启自动任务
            StartTask();
        }


        private void ProcessCompressor()
        {
            if (mBackgroundWorker.IsBusy)
            {
                PromptCancel();
            }
            else
            {
                mBackgroundWorker.RunWorkerAsync();
                mCompressButton.Text = "取消";
            }
        }


        /// <summary>
        /// 开始任务
        /// </summary>
        private void StartTask()
        {
            if (this.TimeInterval == 0 && Timer != "")
            {
                int hours = 0;
                int minutes = 0;
                var timeArry = Timer.Split(':');
                if (timeArry.Length > 1)
                {
                    hours = Convert.ToInt32(timeArry[0]);
                    minutes = Convert.ToInt32(timeArry[1]);
                }
                switch (Weekday)
                {
                    case 1:
                        RunWeeksTask(DayOfWeek.Monday, hours, minutes);
                        break;
                    case 2:
                        RunWeeksTask(DayOfWeek.Tuesday, hours, minutes);
                        break;
                    case 3:
                        RunWeeksTask( DayOfWeek.Wednesday, hours, minutes);
                        break;
                    case 4:
                        RunWeeksTask(DayOfWeek.Thursday, hours, minutes);
                        break;
                    case 5:
                        RunWeeksTask( DayOfWeek.Friday, hours, minutes);
                        break;
                    case 6:
                        RunWeeksTask( DayOfWeek.Saturday, hours, minutes);
                        break;
                    case 7:
                        RunWeeksTask( DayOfWeek.Sunday, hours, minutes);
                        break;
                }
            }
            else
            {
                RunMinutesTask(TimeInterval);
            }

            JobManager.AddJob(() =>
            {
                this.Invoke((Action)(() =>
                {
                    LogText(DateTime.Now.ToString() + "  自动任务中...\n");

                    this.mCompressButton.Text = $"监控中：{DateTime.Now.ToString("HH:mm:ss")}，点击可取消";
                }));
            }, t =>
            {
                t.ToRunNow().AndEvery(1).Seconds();
            });

            //启动
            JobManager.Start();
        }


        /// <summary>
        /// 按分钟执行
        /// </summary>
        /// <param name="timer"></param>
        public void RunMinutesTask( int timer)
        {
            JobManager.AddJob(() =>
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        ////获取监控目录文件
                        //foreach (var f in new DirectoryInfo(this.txt_sourceFilePath.Text).GetFiles())
                        //{
                        //    //拷贝到待压缩目录
                        //    f.CopyTo(this.mInputTextBox.Text + "\\" + f.Name, true);
                        //}

                        var files = Directory.GetFiles(this.txt_sourceFilePath.Text, "*", SearchOption.AllDirectories);
                        int fileCount = 0;
                        foreach (string file in files)
                        {
                            //copy 指定数量文件到 mInputTextBox.Text  目录 
                            FileInfo fileinfo = new FileInfo(file);
                            if (AppConfig.GetFileExtensionNames().Contains(fileinfo.Extension.ToLower()))
                            {
                                if (fileinfo.Length >= (mud_fileLimitSize.Value * 1024))
                                {
                                    // 拷贝到待压缩目录
                                    fileinfo.CopyTo(this.mInputTextBox.Text + "\\" + fileinfo.Name, true);
                                    if (pud_OnceOfQuantity.Value >0&& ++fileCount >= pud_OnceOfQuantity.Value)
                                    {
                                        break;
                                    }
                                }
                            }

                        }
                        //处理压缩
                        ProcessCompressor();
                    }));
                }
            }, t =>
            {
                t.ToRunNow().AndEvery(timer).Minutes();
            });
        }


        /// <summary>
        /// 按计划执行
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        public void RunWeeksTask(DayOfWeek dayOfWeek, int hours, int minutes)
        {
            JobManager.AddJob(() =>
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        //获取监控目录文件
                        //foreach (var f in new DirectoryInfo(this.txt_sourceFilePath.Text).GetFiles())
                        //{
                        //    //拷贝到待压缩目录
                        //    f.CopyTo(this.mInputTextBox.Text + "\\" + f.Name, true);
                        //}
                        var files = Directory.GetFiles(this.txt_sourceFilePath.Text, "*", SearchOption.AllDirectories);
                        int fileCount = 0;
                        foreach (string file in files)
                        {
                            //copy 指定数量文件到 mInputTextBox.Text  目录 
                            FileInfo fileinfo = new FileInfo(file);
                            if (AppConfig.GetFileExtensionNames().Contains(fileinfo.Extension.ToLower()))
                            {
                                if (fileinfo.Length>=( mud_fileLimitSize.Value*1024))
                                {
                                    // 拷贝到待压缩目录
                                    fileinfo.CopyTo(this.mInputTextBox.Text + "\\" + fileinfo.Name, true);
                                    if (pud_OnceOfQuantity.Value > 0 && ++fileCount >= pud_OnceOfQuantity.Value)
                                    {
                                        break;
                                    }
                                }
                            }

                        }

                        //处理压缩
                        ProcessCompressor();
                    }));
                }
            }, t =>
            {
                t.ToRunEvery(0).Weeks().On(DayOfWeek.Monday).At(hours, minutes);
            });
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

            if (!this.rb_autoCompress.Checked)
            {
                MessageBox.Show(
                    this,
                    message,
                    msTitleBarText,
                    MessageBoxButtons.OK,
                    icon
                );
            }

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
            if (this.rb_autoCompress.Checked && String.IsNullOrEmpty(txt_sourceFilePath.Text))
            {
                return "自动模式下，请指定有效监控目标目录.";
            }

            if (!Directory.Exists(txt_sourceFilePath.Text))
            {
                return "监控目标目录不存在.";
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


        /// <summary>
        /// 处理压缩
        /// </summary>
        /// <param name="outputDirectory"></param>
        /// <param name="encoderParameters"></param>
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

        /// <summary>
        /// 处理压缩
        /// </summary>
        /// <param name="inputDirectory"></param>
        /// <param name="outputDirectory"></param>
        /// <param name="encoderParameters"></param>
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

        /// <summary>
        /// 处理压缩
        /// </summary>
        /// <param name="image"></param>
        /// <param name="outputDirectory"></param>
        /// <param name="encoderParameters"></param>
        /// <param name="fname"></param>
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

        /// <summary>
        /// 调整
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
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

        /// <summary>
        /// 终止后台
        /// </summary>
        /// <returns></returns>
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


            IsRunning = false;
            LogText(DateTime.Now.ToString() + "  自动任务停止\r\n");
            JobManager.Stop();

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

        /// <summary>
        /// 格式化文件大小
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 更新GridView
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="task"></param>
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

        /// <summary>
        /// 更新GridView
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="task"></param>
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

        /// <summary>
        /// 更新GridView
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="task"></param>
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

        /// <summary>
        /// 更新GridView
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="task"></param>
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

        private void mQualityLabel_Click(object sender, EventArgs e)
        {

        }

        private void mQualityTextBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void mInputLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.mCompressButton.Text = "压缩";
            this.mInputLabel.Text = "压缩目录";
            this.checkBox2.Checked = false;
            this.radioButton9.Enabled = false;
            this.txt_sourceFilePath.Enabled = false;
            this.button2.Enabled = false;
            this.button1.Enabled = true;
            this.radioButton8.Enabled = false;
            this.radioButton3.Enabled = false;
            this.radioButton4.Enabled = false;
            this.radioButton5.Enabled = false;
            this.radioButton6.Enabled = false;
            this.radioButton7.Enabled = false;
            this.comboBox1.Enabled = false;
            this.numericUpDown1.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.mCompressButton.Text = "开始";
            this.checkBox2.Checked = true;
            this.txt_sourceFilePath.Enabled = true;
            this.button2.Enabled = true;
            this.mInputLabel.Text = "监控目录";
            this.button1.Enabled = false;
            this.radioButton9.Enabled = true;
            this.radioButton8.Enabled = true;
            this.radioButton3.Enabled = true;
            this.radioButton4.Enabled = true;
            this.radioButton5.Enabled = true;
            this.radioButton6.Enabled = true;
            this.radioButton7.Enabled = true;
            this.comboBox1.Enabled = true;
            this.numericUpDown1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txt_sourceFilePath.Text))
            {
                mInputBrowseDialog.SelectedPath = txt_sourceFilePath.Text;
            }

            if (mInputBrowseDialog.ShowDialog() == DialogResult.OK)
            {
                txt_sourceFilePath.Text = mInputBrowseDialog.SelectedPath;
            }
        }

    }

}
