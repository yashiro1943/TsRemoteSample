namespace TsRemoteSample
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStartControl = new System.Windows.Forms.Button();
            this.btnStopControl = new System.Windows.Forms.Button();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Content = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.tabPage_LJV7 = new System.Windows.Forms.TabPage();
            this.label_CurrentFPS = new System.Windows.Forms.Label();
            this.pictureBox_CurrentImage = new System.Windows.Forms.PictureBox();
            this.groupBox_CurrentImage = new System.Windows.Forms.GroupBox();
            this.btnCurrentImage_Stop = new System.Windows.Forms.Button();
            this.btnCurrentImage = new System.Windows.Forms.Button();
            this.groupBox_LJV7 = new System.Windows.Forms.GroupBox();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnHighSpeedDataCommunicationFinalize = new System.Windows.Forms.Button();
            this.btnFinalize = new System.Windows.Forms.Button();
            this.btnStartHighSpeedDataCommunication = new System.Windows.Forms.Button();
            this.btnStopHighSpeedDataCommunication = new System.Windows.Forms.Button();
            this.btnHighSpeedDataEthernetCommunicationInitalize = new System.Windows.Forms.Button();
            this.textBox_LJV7CallbackFrequency = new System.Windows.Forms.MaskedTextBox();
            this.textBox_LJV7StartProfileNo = new System.Windows.Forms.TextBox();
            this._lblCallbackFrequency = new System.Windows.Forms.Label();
            this.textBox_LJV7IPAddr = new System.Windows.Forms.TextBox();
            this._lblHighSpeedStartNo = new System.Windows.Forms.Label();
            this.label_LJV7Port_HighSpeed = new System.Windows.Forms.Label();
            this.textBox_LJV7Port_HighSpeedPort = new System.Windows.Forms.TextBox();
            this.label_LJV7IP = new System.Windows.Forms.Label();
            this.textBox_LJV7Port_CommandPort = new System.Windows.Forms.TextBox();
            this.label_LJV7Port_Command = new System.Windows.Forms.Label();
            this.tabPage_Toshiba = new System.Windows.Forms.TabPage();
            this.groupBox_MoveJoint = new System.Windows.Forms.GroupBox();
            this.btnAuto = new System.Windows.Forms.Button();
            this.label_J1 = new System.Windows.Forms.Label();
            this.numericUpDown_J1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_J2 = new System.Windows.Forms.NumericUpDown();
            this.label_J2 = new System.Windows.Forms.Label();
            this.numericUpDown_J3 = new System.Windows.Forms.NumericUpDown();
            this.label_J3 = new System.Windows.Forms.Label();
            this.btnStartMoveJoint = new System.Windows.Forms.Button();
            this.numericUpDown_J5 = new System.Windows.Forms.NumericUpDown();
            this.label_J4 = new System.Windows.Forms.Label();
            this.label_J5 = new System.Windows.Forms.Label();
            this.numericUpDown_J4 = new System.Windows.Forms.NumericUpDown();
            this.groupBox_MovePos = new System.Windows.Forms.GroupBox();
            this.OpenGLCtrl_Orig_Auto = new SharpGL.OpenGLCtrl();
            this.pictureBox_CurrentImage_Auto = new System.Windows.Forms.PictureBox();
            this.btnExportSTL = new System.Windows.Forms.Button();
            this.btnShow3D_Auto = new System.Windows.Forms.Button();
            this.btnStopMove = new System.Windows.Forms.Button();
            this.labWarning_Signs = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnCtrlByMouse = new System.Windows.Forms.Button();
            this.labelX = new System.Windows.Forms.Label();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.labelY = new System.Windows.Forms.Label();
            this.numericUpDownZ = new System.Windows.Forms.NumericUpDown();
            this.labelZ = new System.Windows.Forms.Label();
            this.btnStartMove = new System.Windows.Forms.Button();
            this.numericUpDownT = new System.Windows.Forms.NumericUpDown();
            this.labelC = new System.Windows.Forms.Label();
            this.labelT = new System.Windows.Forms.Label();
            this.numericUpDownC = new System.Windows.Forms.NumericUpDown();
            this.panel_MouseCtrl = new System.Windows.Forms.Panel();
            this.label_MovePos = new System.Windows.Forms.Label();
            this.groupBox_CenterPos = new System.Windows.Forms.GroupBox();
            this.label_Center = new System.Windows.Forms.Label();
            this.btnDrawArc = new System.Windows.Forms.Button();
            this.numericUpDown_CenterX = new System.Windows.Forms.NumericUpDown();
            this.btnTest = new System.Windows.Forms.Button();
            this.numericUpDown_arcAngle = new System.Windows.Forms.NumericUpDown();
            this.label_Centery = new System.Windows.Forms.Label();
            this.numericUpDown_drawAngle = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_CenterY = new System.Windows.Forms.NumericUpDown();
            this.label2_CenterX = new System.Windows.Forms.Label();
            this.btnDrawCycle = new System.Windows.Forms.Button();
            this.groupBox_Toshiba = new System.Windows.Forms.GroupBox();
            this.label_Override = new System.Windows.Forms.Label();
            this.label_ToshibaPort = new System.Windows.Forms.Label();
            this.label_ToshibaIP = new System.Windows.Forms.Label();
            this.domainUpDown_Config = new System.Windows.Forms.DomainUpDown();
            this.btnResetAlarm = new System.Windows.Forms.Button();
            this.btnOverride = new System.Windows.Forms.Button();
            this.numericUpDown_Override = new System.Windows.Forms.NumericUpDown();
            this.textBox_ToshibaIPAddr = new System.Windows.Forms.TextBox();
            this.textBox_ToshibaPort = new System.Windows.Forms.TextBox();
            this.tabPage_3DModel = new System.Windows.Forms.TabPage();
            this.btnChangeRange = new System.Windows.Forms.Button();
            this.maskedTextBox_RangeMin = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox_RangeMax = new System.Windows.Forms.MaskedTextBox();
            this.btn_OpenExcel = new System.Windows.Forms.Button();
            this.btnRstOpenGL = new System.Windows.Forms.Button();
            this.btnShow3D = new System.Windows.Forms.Button();
            this.OpenGLCtrl_Orig = new SharpGL.OpenGLCtrl();
            this.groupBox_Testing = new System.Windows.Forms.GroupBox();
            this.btnStart3DModel_Laser = new System.Windows.Forms.Button();
            this.btnResetAlarm_3D = new System.Windows.Forms.Button();
            this.btnStartControl_3D = new System.Windows.Forms.Button();
            this.btnCurrentImage_Stop_3D = new System.Windows.Forms.Button();
            this.btnStopControl_3D = new System.Windows.Forms.Button();
            this.btnCurrentImage_3D = new System.Windows.Forms.Button();
            this.btnStart3DModel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage_Log = new System.Windows.Forms.TabPage();
            this.btnClearMessage = new System.Windows.Forms.Button();
            this.GetStatusAll = new System.Windows.Forms.Button();
            this.btnGetPosition = new System.Windows.Forms.Button();
            this.textBox_Message = new System.Windows.Forms.TextBox();
            this.timerGetCoordinate = new System.Windows.Forms.Timer(this.components);
            this.btnCloseApp = new System.Windows.Forms.Button();
            this.timer_CurrentFPS = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._StatusStrip.SuspendLayout();
            this.tabControl_main.SuspendLayout();
            this.tabPage_LJV7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CurrentImage)).BeginInit();
            this.groupBox_CurrentImage.SuspendLayout();
            this.groupBox_LJV7.SuspendLayout();
            this.tabPage_Toshiba.SuspendLayout();
            this.groupBox_MoveJoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J4)).BeginInit();
            this.groupBox_MovePos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CurrentImage_Auto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownC)).BeginInit();
            this.panel_MouseCtrl.SuspendLayout();
            this.groupBox_CenterPos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CenterX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_arcAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_drawAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CenterY)).BeginInit();
            this.groupBox_Toshiba.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Override)).BeginInit();
            this.tabPage_3DModel.SuspendLayout();
            this.groupBox_Testing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage_Log.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartControl
            // 
            this.btnStartControl.BackColor = System.Drawing.Color.Green;
            this.btnStartControl.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold);
            this.btnStartControl.ForeColor = System.Drawing.Color.White;
            this.btnStartControl.Location = new System.Drawing.Point(18, 87);
            this.btnStartControl.Name = "btnStartControl";
            this.btnStartControl.Size = new System.Drawing.Size(74, 36);
            this.btnStartControl.TabIndex = 0;
            this.btnStartControl.Text = "啟動";
            this.btnStartControl.UseVisualStyleBackColor = false;
            this.btnStartControl.Click += new System.EventHandler(this.btnStartControl_Click);
            // 
            // btnStopControl
            // 
            this.btnStopControl.BackColor = System.Drawing.Color.Red;
            this.btnStopControl.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold);
            this.btnStopControl.ForeColor = System.Drawing.Color.White;
            this.btnStopControl.Location = new System.Drawing.Point(18, 129);
            this.btnStopControl.Name = "btnStopControl";
            this.btnStopControl.Size = new System.Drawing.Size(74, 33);
            this.btnStopControl.TabIndex = 2;
            this.btnStopControl.Text = "停止";
            this.btnStopControl.UseVisualStyleBackColor = false;
            this.btnStopControl.Visible = false;
            this.btnStopControl.Click += new System.EventHandler(this.btnStopControl_Click);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel_Content});
            this._StatusStrip.Location = new System.Drawing.Point(0, 394);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(840, 22);
            this._StatusStrip.TabIndex = 7;
            this._StatusStrip.Text = "狀態列";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel.Text = "狀態";
            // 
            // toolStripStatusLabel_Content
            // 
            this.toolStripStatusLabel_Content.Name = "toolStripStatusLabel_Content";
            this.toolStripStatusLabel_Content.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel_Content.Text = "內容";
            // 
            // tabControl_main
            // 
            this.tabControl_main.Controls.Add(this.tabPage_LJV7);
            this.tabControl_main.Controls.Add(this.tabPage_Toshiba);
            this.tabControl_main.Controls.Add(this.tabPage_3DModel);
            this.tabControl_main.Controls.Add(this.tabPage_Log);
            this.tabControl_main.Location = new System.Drawing.Point(8, 16);
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(825, 368);
            this.tabControl_main.TabIndex = 8;
            this.tabControl_main.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.OpenGLCtrl_MouseWheel);
            // 
            // tabPage_LJV7
            // 
            this.tabPage_LJV7.Controls.Add(this.label_CurrentFPS);
            this.tabPage_LJV7.Controls.Add(this.pictureBox_CurrentImage);
            this.tabPage_LJV7.Controls.Add(this.groupBox_CurrentImage);
            this.tabPage_LJV7.Controls.Add(this.groupBox_LJV7);
            this.tabPage_LJV7.Location = new System.Drawing.Point(4, 22);
            this.tabPage_LJV7.Name = "tabPage_LJV7";
            this.tabPage_LJV7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_LJV7.Size = new System.Drawing.Size(817, 342);
            this.tabPage_LJV7.TabIndex = 0;
            this.tabPage_LJV7.Text = "LJV7";
            this.tabPage_LJV7.UseVisualStyleBackColor = true;
            // 
            // label_CurrentFPS
            // 
            this.label_CurrentFPS.AutoSize = true;
            this.label_CurrentFPS.BackColor = System.Drawing.Color.Black;
            this.label_CurrentFPS.ForeColor = System.Drawing.Color.White;
            this.label_CurrentFPS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_CurrentFPS.Location = new System.Drawing.Point(322, 6);
            this.label_CurrentFPS.Name = "label_CurrentFPS";
            this.label_CurrentFPS.Size = new System.Drawing.Size(29, 12);
            this.label_CurrentFPS.TabIndex = 86;
            this.label_CurrentFPS.Text = "FPS: ";
            this.label_CurrentFPS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox_CurrentImage
            // 
            this.pictureBox_CurrentImage.BackColor = System.Drawing.Color.Black;
            this.pictureBox_CurrentImage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox_CurrentImage.Location = new System.Drawing.Point(322, 6);
            this.pictureBox_CurrentImage.Name = "pictureBox_CurrentImage";
            this.pictureBox_CurrentImage.Size = new System.Drawing.Size(470, 324);
            this.pictureBox_CurrentImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_CurrentImage.TabIndex = 84;
            this.pictureBox_CurrentImage.TabStop = false;
            // 
            // groupBox_CurrentImage
            // 
            this.groupBox_CurrentImage.Controls.Add(this.btnCurrentImage_Stop);
            this.groupBox_CurrentImage.Controls.Add(this.btnCurrentImage);
            this.groupBox_CurrentImage.Location = new System.Drawing.Point(203, 25);
            this.groupBox_CurrentImage.Name = "groupBox_CurrentImage";
            this.groupBox_CurrentImage.Size = new System.Drawing.Size(113, 305);
            this.groupBox_CurrentImage.TabIndex = 83;
            this.groupBox_CurrentImage.TabStop = false;
            this.groupBox_CurrentImage.Text = "即時影像";
            // 
            // btnCurrentImage_Stop
            // 
            this.btnCurrentImage_Stop.BackColor = System.Drawing.Color.Red;
            this.btnCurrentImage_Stop.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnCurrentImage_Stop.ForeColor = System.Drawing.Color.White;
            this.btnCurrentImage_Stop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCurrentImage_Stop.Location = new System.Drawing.Point(6, 76);
            this.btnCurrentImage_Stop.Name = "btnCurrentImage_Stop";
            this.btnCurrentImage_Stop.Size = new System.Drawing.Size(98, 41);
            this.btnCurrentImage_Stop.TabIndex = 81;
            this.btnCurrentImage_Stop.Text = "停止即時影像";
            this.btnCurrentImage_Stop.UseVisualStyleBackColor = false;
            this.btnCurrentImage_Stop.Visible = false;
            this.btnCurrentImage_Stop.Click += new System.EventHandler(this.btnCurrentImage_Stop_Click);
            // 
            // btnCurrentImage
            // 
            this.btnCurrentImage.BackColor = System.Drawing.Color.Green;
            this.btnCurrentImage.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnCurrentImage.ForeColor = System.Drawing.Color.White;
            this.btnCurrentImage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCurrentImage.Location = new System.Drawing.Point(6, 29);
            this.btnCurrentImage.Name = "btnCurrentImage";
            this.btnCurrentImage.Size = new System.Drawing.Size(98, 41);
            this.btnCurrentImage.TabIndex = 80;
            this.btnCurrentImage.Text = "開啟即時影像";
            this.btnCurrentImage.UseVisualStyleBackColor = false;
            this.btnCurrentImage.Click += new System.EventHandler(this.btnCurrentImage_Click);
            // 
            // groupBox_LJV7
            // 
            this.groupBox_LJV7.Controls.Add(this.btnInitialize);
            this.groupBox_LJV7.Controls.Add(this.btnHighSpeedDataCommunicationFinalize);
            this.groupBox_LJV7.Controls.Add(this.btnFinalize);
            this.groupBox_LJV7.Controls.Add(this.btnStartHighSpeedDataCommunication);
            this.groupBox_LJV7.Controls.Add(this.btnStopHighSpeedDataCommunication);
            this.groupBox_LJV7.Controls.Add(this.btnHighSpeedDataEthernetCommunicationInitalize);
            this.groupBox_LJV7.Controls.Add(this.textBox_LJV7CallbackFrequency);
            this.groupBox_LJV7.Controls.Add(this.textBox_LJV7StartProfileNo);
            this.groupBox_LJV7.Controls.Add(this._lblCallbackFrequency);
            this.groupBox_LJV7.Controls.Add(this.textBox_LJV7IPAddr);
            this.groupBox_LJV7.Controls.Add(this._lblHighSpeedStartNo);
            this.groupBox_LJV7.Controls.Add(this.label_LJV7Port_HighSpeed);
            this.groupBox_LJV7.Controls.Add(this.textBox_LJV7Port_HighSpeedPort);
            this.groupBox_LJV7.Controls.Add(this.label_LJV7IP);
            this.groupBox_LJV7.Controls.Add(this.textBox_LJV7Port_CommandPort);
            this.groupBox_LJV7.Controls.Add(this.label_LJV7Port_Command);
            this.groupBox_LJV7.Location = new System.Drawing.Point(6, 25);
            this.groupBox_LJV7.Name = "groupBox_LJV7";
            this.groupBox_LJV7.Size = new System.Drawing.Size(191, 305);
            this.groupBox_LJV7.TabIndex = 6;
            this.groupBox_LJV7.TabStop = false;
            this.groupBox_LJV7.Text = "LJV7";
            // 
            // btnInitialize
            // 
            this.btnInitialize.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnInitialize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInitialize.Location = new System.Drawing.Point(59, 180);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(59, 35);
            this.btnInitialize.TabIndex = 69;
            this.btnInitialize.Text = "初始儀器";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // btnHighSpeedDataCommunicationFinalize
            // 
            this.btnHighSpeedDataCommunicationFinalize.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnHighSpeedDataCommunicationFinalize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHighSpeedDataCommunicationFinalize.Location = new System.Drawing.Point(126, 222);
            this.btnHighSpeedDataCommunicationFinalize.Name = "btnHighSpeedDataCommunicationFinalize";
            this.btnHighSpeedDataCommunicationFinalize.Size = new System.Drawing.Size(59, 35);
            this.btnHighSpeedDataCommunicationFinalize.TabIndex = 98;
            this.btnHighSpeedDataCommunicationFinalize.Text = "高速結束";
            this.btnHighSpeedDataCommunicationFinalize.UseVisualStyleBackColor = true;
            this.btnHighSpeedDataCommunicationFinalize.Visible = false;
            this.btnHighSpeedDataCommunicationFinalize.Click += new System.EventHandler(this.btnHighSpeedDataCommunicationFinalize_Click);
            // 
            // btnFinalize
            // 
            this.btnFinalize.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnFinalize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFinalize.Location = new System.Drawing.Point(126, 180);
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(59, 35);
            this.btnFinalize.TabIndex = 71;
            this.btnFinalize.Text = "儀器離線";
            this.btnFinalize.UseVisualStyleBackColor = true;
            this.btnFinalize.Visible = false;
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // btnStartHighSpeedDataCommunication
            // 
            this.btnStartHighSpeedDataCommunication.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnStartHighSpeedDataCommunication.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStartHighSpeedDataCommunication.Location = new System.Drawing.Point(61, 263);
            this.btnStartHighSpeedDataCommunication.Name = "btnStartHighSpeedDataCommunication";
            this.btnStartHighSpeedDataCommunication.Size = new System.Drawing.Size(59, 35);
            this.btnStartHighSpeedDataCommunication.TabIndex = 97;
            this.btnStartHighSpeedDataCommunication.Text = "高速連線";
            this.btnStartHighSpeedDataCommunication.UseVisualStyleBackColor = true;
            this.btnStartHighSpeedDataCommunication.Visible = false;
            this.btnStartHighSpeedDataCommunication.Click += new System.EventHandler(this.StartHighSpeedDataCommunication);
            // 
            // btnStopHighSpeedDataCommunication
            // 
            this.btnStopHighSpeedDataCommunication.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnStopHighSpeedDataCommunication.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStopHighSpeedDataCommunication.Location = new System.Drawing.Point(126, 263);
            this.btnStopHighSpeedDataCommunication.Name = "btnStopHighSpeedDataCommunication";
            this.btnStopHighSpeedDataCommunication.Size = new System.Drawing.Size(59, 35);
            this.btnStopHighSpeedDataCommunication.TabIndex = 96;
            this.btnStopHighSpeedDataCommunication.Text = "高速離線";
            this.btnStopHighSpeedDataCommunication.UseVisualStyleBackColor = true;
            this.btnStopHighSpeedDataCommunication.Visible = false;
            this.btnStopHighSpeedDataCommunication.Click += new System.EventHandler(this.btnStopHighSpeedDataCommunication_Click);
            // 
            // btnHighSpeedDataEthernetCommunicationInitalize
            // 
            this.btnHighSpeedDataEthernetCommunicationInitalize.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnHighSpeedDataEthernetCommunicationInitalize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnHighSpeedDataEthernetCommunicationInitalize.Location = new System.Drawing.Point(61, 222);
            this.btnHighSpeedDataEthernetCommunicationInitalize.Name = "btnHighSpeedDataEthernetCommunicationInitalize";
            this.btnHighSpeedDataEthernetCommunicationInitalize.Size = new System.Drawing.Size(59, 35);
            this.btnHighSpeedDataEthernetCommunicationInitalize.TabIndex = 82;
            this.btnHighSpeedDataEthernetCommunicationInitalize.Text = "初始高速";
            this.btnHighSpeedDataEthernetCommunicationInitalize.UseVisualStyleBackColor = true;
            this.btnHighSpeedDataEthernetCommunicationInitalize.Visible = false;
            this.btnHighSpeedDataEthernetCommunicationInitalize.Click += new System.EventHandler(this.btnHighSpeedDataEthernetCommunicationInitalize_Click);
            // 
            // textBox_LJV7CallbackFrequency
            // 
            this.textBox_LJV7CallbackFrequency.Font = new System.Drawing.Font("新細明體", 9F);
            this.textBox_LJV7CallbackFrequency.Location = new System.Drawing.Point(131, 99);
            this.textBox_LJV7CallbackFrequency.Mask = "00000";
            this.textBox_LJV7CallbackFrequency.Name = "textBox_LJV7CallbackFrequency";
            this.textBox_LJV7CallbackFrequency.Size = new System.Drawing.Size(48, 22);
            this.textBox_LJV7CallbackFrequency.TabIndex = 93;
            this.textBox_LJV7CallbackFrequency.Text = "200";
            // 
            // textBox_LJV7StartProfileNo
            // 
            this.textBox_LJV7StartProfileNo.Font = new System.Drawing.Font("新細明體", 9F);
            this.textBox_LJV7StartProfileNo.Location = new System.Drawing.Point(131, 127);
            this.textBox_LJV7StartProfileNo.Name = "textBox_LJV7StartProfileNo";
            this.textBox_LJV7StartProfileNo.Size = new System.Drawing.Size(48, 22);
            this.textBox_LJV7StartProfileNo.TabIndex = 92;
            this.textBox_LJV7StartProfileNo.Text = "2";
            this.textBox_LJV7StartProfileNo.Visible = false;
            // 
            // _lblCallbackFrequency
            // 
            this._lblCallbackFrequency.AutoSize = true;
            this._lblCallbackFrequency.Font = new System.Drawing.Font("新細明體", 9F);
            this._lblCallbackFrequency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._lblCallbackFrequency.Location = new System.Drawing.Point(15, 104);
            this._lblCallbackFrequency.Name = "_lblCallbackFrequency";
            this._lblCallbackFrequency.Size = new System.Drawing.Size(98, 12);
            this._lblCallbackFrequency.TabIndex = 95;
            this._lblCallbackFrequency.Text = "Callback Frequency";
            // 
            // textBox_LJV7IPAddr
            // 
            this.textBox_LJV7IPAddr.Font = new System.Drawing.Font("新細明體", 9F);
            this.textBox_LJV7IPAddr.Location = new System.Drawing.Point(73, 15);
            this.textBox_LJV7IPAddr.Name = "textBox_LJV7IPAddr";
            this.textBox_LJV7IPAddr.Size = new System.Drawing.Size(106, 22);
            this.textBox_LJV7IPAddr.TabIndex = 80;
            this.textBox_LJV7IPAddr.Text = "192.168.0.4";
            // 
            // _lblHighSpeedStartNo
            // 
            this._lblHighSpeedStartNo.AutoSize = true;
            this._lblHighSpeedStartNo.Font = new System.Drawing.Font("新細明體", 9F);
            this._lblHighSpeedStartNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._lblHighSpeedStartNo.Location = new System.Drawing.Point(16, 132);
            this._lblHighSpeedStartNo.Name = "_lblHighSpeedStartNo";
            this._lblHighSpeedStartNo.Size = new System.Drawing.Size(75, 12);
            this._lblHighSpeedStartNo.TabIndex = 94;
            this._lblHighSpeedStartNo.Text = "Starting Profile";
            this._lblHighSpeedStartNo.Visible = false;
            // 
            // label_LJV7Port_HighSpeed
            // 
            this.label_LJV7Port_HighSpeed.AutoSize = true;
            this.label_LJV7Port_HighSpeed.Font = new System.Drawing.Font("新細明體", 9F);
            this.label_LJV7Port_HighSpeed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_LJV7Port_HighSpeed.Location = new System.Drawing.Point(14, 76);
            this.label_LJV7Port_HighSpeed.Name = "label_LJV7Port_HighSpeed";
            this.label_LJV7Port_HighSpeed.Size = new System.Drawing.Size(112, 12);
            this.label_LJV7Port_HighSpeed.TabIndex = 90;
            this.label_LJV7Port_HighSpeed.Text = "TCP Port (High-speed)";
            // 
            // textBox_LJV7Port_HighSpeedPort
            // 
            this.textBox_LJV7Port_HighSpeedPort.Font = new System.Drawing.Font("新細明體", 9F);
            this.textBox_LJV7Port_HighSpeedPort.Location = new System.Drawing.Point(131, 71);
            this.textBox_LJV7Port_HighSpeedPort.Name = "textBox_LJV7Port_HighSpeedPort";
            this.textBox_LJV7Port_HighSpeedPort.Size = new System.Drawing.Size(48, 22);
            this.textBox_LJV7Port_HighSpeedPort.TabIndex = 88;
            this.textBox_LJV7Port_HighSpeedPort.Text = "24692";
            // 
            // label_LJV7IP
            // 
            this.label_LJV7IP.AutoSize = true;
            this.label_LJV7IP.Font = new System.Drawing.Font("新細明體", 9F);
            this.label_LJV7IP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_LJV7IP.Location = new System.Drawing.Point(15, 20);
            this.label_LJV7IP.Name = "label_LJV7IP";
            this.label_LJV7IP.Size = new System.Drawing.Size(52, 12);
            this.label_LJV7IP.TabIndex = 89;
            this.label_LJV7IP.Text = "IP address";
            // 
            // textBox_LJV7Port_CommandPort
            // 
            this.textBox_LJV7Port_CommandPort.Font = new System.Drawing.Font("新細明體", 9F);
            this.textBox_LJV7Port_CommandPort.Location = new System.Drawing.Point(131, 43);
            this.textBox_LJV7Port_CommandPort.Name = "textBox_LJV7Port_CommandPort";
            this.textBox_LJV7Port_CommandPort.Size = new System.Drawing.Size(48, 22);
            this.textBox_LJV7Port_CommandPort.TabIndex = 87;
            this.textBox_LJV7Port_CommandPort.Text = "24691";
            // 
            // label_LJV7Port_Command
            // 
            this.label_LJV7Port_Command.AutoSize = true;
            this.label_LJV7Port_Command.Font = new System.Drawing.Font("新細明體", 9F);
            this.label_LJV7Port_Command.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_LJV7Port_Command.Location = new System.Drawing.Point(14, 48);
            this.label_LJV7Port_Command.Name = "label_LJV7Port_Command";
            this.label_LJV7Port_Command.Size = new System.Drawing.Size(108, 12);
            this.label_LJV7Port_Command.TabIndex = 91;
            this.label_LJV7Port_Command.Text = "TCP Port (Command)";
            // 
            // tabPage_Toshiba
            // 
            this.tabPage_Toshiba.Controls.Add(this.groupBox_MoveJoint);
            this.tabPage_Toshiba.Controls.Add(this.groupBox_MovePos);
            this.tabPage_Toshiba.Controls.Add(this.groupBox_CenterPos);
            this.tabPage_Toshiba.Controls.Add(this.groupBox_Toshiba);
            this.tabPage_Toshiba.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Toshiba.Name = "tabPage_Toshiba";
            this.tabPage_Toshiba.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Toshiba.Size = new System.Drawing.Size(817, 342);
            this.tabPage_Toshiba.TabIndex = 1;
            this.tabPage_Toshiba.Text = "Toshiba";
            this.tabPage_Toshiba.UseVisualStyleBackColor = true;
            // 
            // groupBox_MoveJoint
            // 
            this.groupBox_MoveJoint.Controls.Add(this.btnAuto);
            this.groupBox_MoveJoint.Controls.Add(this.label_J1);
            this.groupBox_MoveJoint.Controls.Add(this.numericUpDown_J1);
            this.groupBox_MoveJoint.Controls.Add(this.numericUpDown_J2);
            this.groupBox_MoveJoint.Controls.Add(this.label_J2);
            this.groupBox_MoveJoint.Controls.Add(this.numericUpDown_J3);
            this.groupBox_MoveJoint.Controls.Add(this.label_J3);
            this.groupBox_MoveJoint.Controls.Add(this.btnStartMoveJoint);
            this.groupBox_MoveJoint.Controls.Add(this.numericUpDown_J5);
            this.groupBox_MoveJoint.Controls.Add(this.label_J4);
            this.groupBox_MoveJoint.Controls.Add(this.label_J5);
            this.groupBox_MoveJoint.Controls.Add(this.numericUpDown_J4);
            this.groupBox_MoveJoint.Location = new System.Drawing.Point(529, 28);
            this.groupBox_MoveJoint.Name = "groupBox_MoveJoint";
            this.groupBox_MoveJoint.Size = new System.Drawing.Size(121, 300);
            this.groupBox_MoveJoint.TabIndex = 50;
            this.groupBox_MoveJoint.TabStop = false;
            this.groupBox_MoveJoint.Text = "軸移動";
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(13, 262);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(88, 33);
            this.btnAuto.TabIndex = 40;
            this.btnAuto.Text = "自動模式";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // label_J1
            // 
            this.label_J1.AutoSize = true;
            this.label_J1.Location = new System.Drawing.Point(11, 36);
            this.label_J1.Name = "label_J1";
            this.label_J1.Size = new System.Drawing.Size(15, 12);
            this.label_J1.TabIndex = 45;
            this.label_J1.Text = "J1";
            // 
            // numericUpDown_J1
            // 
            this.numericUpDown_J1.Location = new System.Drawing.Point(30, 31);
            this.numericUpDown_J1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_J1.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_J1.Name = "numericUpDown_J1";
            this.numericUpDown_J1.Size = new System.Drawing.Size(71, 22);
            this.numericUpDown_J1.TabIndex = 44;
            // 
            // numericUpDown_J2
            // 
            this.numericUpDown_J2.Location = new System.Drawing.Point(30, 59);
            this.numericUpDown_J2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_J2.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDown_J2.Name = "numericUpDown_J2";
            this.numericUpDown_J2.Size = new System.Drawing.Size(71, 22);
            this.numericUpDown_J2.TabIndex = 46;
            // 
            // label_J2
            // 
            this.label_J2.AutoSize = true;
            this.label_J2.Location = new System.Drawing.Point(11, 64);
            this.label_J2.Name = "label_J2";
            this.label_J2.Size = new System.Drawing.Size(15, 12);
            this.label_J2.TabIndex = 47;
            this.label_J2.Text = "J2";
            // 
            // numericUpDown_J3
            // 
            this.numericUpDown_J3.Location = new System.Drawing.Point(30, 87);
            this.numericUpDown_J3.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDown_J3.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_J3.Name = "numericUpDown_J3";
            this.numericUpDown_J3.Size = new System.Drawing.Size(71, 22);
            this.numericUpDown_J3.TabIndex = 48;
            this.numericUpDown_J3.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label_J3
            // 
            this.label_J3.AutoSize = true;
            this.label_J3.Location = new System.Drawing.Point(11, 92);
            this.label_J3.Name = "label_J3";
            this.label_J3.Size = new System.Drawing.Size(15, 12);
            this.label_J3.TabIndex = 49;
            this.label_J3.Text = "J3";
            // 
            // btnStartMoveJoint
            // 
            this.btnStartMoveJoint.Location = new System.Drawing.Point(13, 183);
            this.btnStartMoveJoint.Name = "btnStartMoveJoint";
            this.btnStartMoveJoint.Size = new System.Drawing.Size(88, 31);
            this.btnStartMoveJoint.TabIndex = 54;
            this.btnStartMoveJoint.Text = "移動";
            this.btnStartMoveJoint.UseVisualStyleBackColor = true;
            this.btnStartMoveJoint.Click += new System.EventHandler(this.btnStartMoveJoint_Click);
            // 
            // numericUpDown_J5
            // 
            this.numericUpDown_J5.Enabled = false;
            this.numericUpDown_J5.Location = new System.Drawing.Point(30, 140);
            this.numericUpDown_J5.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDown_J5.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            -2147483648});
            this.numericUpDown_J5.Name = "numericUpDown_J5";
            this.numericUpDown_J5.Size = new System.Drawing.Size(71, 22);
            this.numericUpDown_J5.TabIndex = 50;
            // 
            // label_J4
            // 
            this.label_J4.AutoSize = true;
            this.label_J4.Location = new System.Drawing.Point(11, 118);
            this.label_J4.Name = "label_J4";
            this.label_J4.Size = new System.Drawing.Size(15, 12);
            this.label_J4.TabIndex = 53;
            this.label_J4.Text = "J4";
            // 
            // label_J5
            // 
            this.label_J5.AutoSize = true;
            this.label_J5.Enabled = false;
            this.label_J5.Location = new System.Drawing.Point(11, 144);
            this.label_J5.Name = "label_J5";
            this.label_J5.Size = new System.Drawing.Size(15, 12);
            this.label_J5.TabIndex = 51;
            this.label_J5.Text = "J5";
            // 
            // numericUpDown_J4
            // 
            this.numericUpDown_J4.Location = new System.Drawing.Point(30, 112);
            this.numericUpDown_J4.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDown_J4.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            -2147483648});
            this.numericUpDown_J4.Name = "numericUpDown_J4";
            this.numericUpDown_J4.Size = new System.Drawing.Size(71, 22);
            this.numericUpDown_J4.TabIndex = 52;
            // 
            // groupBox_MovePos
            // 
            this.groupBox_MovePos.Controls.Add(this.OpenGLCtrl_Orig_Auto);
            this.groupBox_MovePos.Controls.Add(this.pictureBox_CurrentImage_Auto);
            this.groupBox_MovePos.Controls.Add(this.btnExportSTL);
            this.groupBox_MovePos.Controls.Add(this.btnShow3D_Auto);
            this.groupBox_MovePos.Controls.Add(this.btnStopMove);
            this.groupBox_MovePos.Controls.Add(this.labWarning_Signs);
            this.groupBox_MovePos.Controls.Add(this.checkBox1);
            this.groupBox_MovePos.Controls.Add(this.btnCtrlByMouse);
            this.groupBox_MovePos.Controls.Add(this.labelX);
            this.groupBox_MovePos.Controls.Add(this.numericUpDownX);
            this.groupBox_MovePos.Controls.Add(this.numericUpDownY);
            this.groupBox_MovePos.Controls.Add(this.labelY);
            this.groupBox_MovePos.Controls.Add(this.numericUpDownZ);
            this.groupBox_MovePos.Controls.Add(this.labelZ);
            this.groupBox_MovePos.Controls.Add(this.btnStartMove);
            this.groupBox_MovePos.Controls.Add(this.numericUpDownT);
            this.groupBox_MovePos.Controls.Add(this.labelC);
            this.groupBox_MovePos.Controls.Add(this.labelT);
            this.groupBox_MovePos.Controls.Add(this.numericUpDownC);
            this.groupBox_MovePos.Controls.Add(this.panel_MouseCtrl);
            this.groupBox_MovePos.Location = new System.Drawing.Point(237, 28);
            this.groupBox_MovePos.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_MovePos.Name = "groupBox_MovePos";
            this.groupBox_MovePos.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_MovePos.Size = new System.Drawing.Size(286, 300);
            this.groupBox_MovePos.TabIndex = 49;
            this.groupBox_MovePos.TabStop = false;
            this.groupBox_MovePos.Text = "直線座標移動";
            // 
            // OpenGLCtrl_Orig_Auto
            // 
            this.OpenGLCtrl_Orig_Auto.DrawRenderTime = false;
            this.OpenGLCtrl_Orig_Auto.FrameRate = 10F;
            this.OpenGLCtrl_Orig_Auto.GDIEnabled = false;
            this.OpenGLCtrl_Orig_Auto.Location = new System.Drawing.Point(4, 19);
            this.OpenGLCtrl_Orig_Auto.Name = "OpenGLCtrl_Orig_Auto";
            this.OpenGLCtrl_Orig_Auto.Size = new System.Drawing.Size(277, 194);
            this.OpenGLCtrl_Orig_Auto.TabIndex = 88;
            this.OpenGLCtrl_Orig_Auto.Visible = false;
            this.OpenGLCtrl_Orig_Auto.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.OpenGLCtrl_Orig_OpenGLDraw_Auto);
            this.OpenGLCtrl_Orig_Auto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OpenGLCtrl_MouseDown);
            this.OpenGLCtrl_Orig_Auto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpenGLCtrl_MouseUp);
            // 
            // pictureBox_CurrentImage_Auto
            // 
            this.pictureBox_CurrentImage_Auto.BackColor = System.Drawing.Color.Black;
            this.pictureBox_CurrentImage_Auto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox_CurrentImage_Auto.Location = new System.Drawing.Point(4, 18);
            this.pictureBox_CurrentImage_Auto.Name = "pictureBox_CurrentImage_Auto";
            this.pictureBox_CurrentImage_Auto.Size = new System.Drawing.Size(277, 196);
            this.pictureBox_CurrentImage_Auto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_CurrentImage_Auto.TabIndex = 85;
            this.pictureBox_CurrentImage_Auto.TabStop = false;
            this.pictureBox_CurrentImage_Auto.Visible = false;
            // 
            // btnExportSTL
            // 
            this.btnExportSTL.Enabled = false;
            this.btnExportSTL.Location = new System.Drawing.Point(125, 251);
            this.btnExportSTL.Name = "btnExportSTL";
            this.btnExportSTL.Size = new System.Drawing.Size(75, 44);
            this.btnExportSTL.TabIndex = 89;
            this.btnExportSTL.Text = "儲存3D圖";
            this.btnExportSTL.UseVisualStyleBackColor = true;
            this.btnExportSTL.Visible = false;
            this.btnExportSTL.Click += new System.EventHandler(this.btnExportSTL_Click);
            // 
            // btnShow3D_Auto
            // 
            this.btnShow3D_Auto.Location = new System.Drawing.Point(206, 251);
            this.btnShow3D_Auto.Name = "btnShow3D_Auto";
            this.btnShow3D_Auto.Size = new System.Drawing.Size(75, 44);
            this.btnShow3D_Auto.TabIndex = 86;
            this.btnShow3D_Auto.Text = "3D";
            this.btnShow3D_Auto.UseVisualStyleBackColor = true;
            this.btnShow3D_Auto.Visible = false;
            this.btnShow3D_Auto.Click += new System.EventHandler(this.btnShow3D_Auto_Click);
            // 
            // btnStopMove
            // 
            this.btnStopMove.Location = new System.Drawing.Point(10, 262);
            this.btnStopMove.Name = "btnStopMove";
            this.btnStopMove.Size = new System.Drawing.Size(88, 33);
            this.btnStopMove.TabIndex = 39;
            this.btnStopMove.Text = "緊急停止";
            this.btnStopMove.UseVisualStyleBackColor = true;
            this.btnStopMove.Click += new System.EventHandler(this.btnStopMove_Click);
            // 
            // labWarning_Signs
            // 
            this.labWarning_Signs.AutoSize = true;
            this.labWarning_Signs.Font = new System.Drawing.Font("新細明體", 9.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labWarning_Signs.ForeColor = System.Drawing.Color.Red;
            this.labWarning_Signs.Location = new System.Drawing.Point(4, 192);
            this.labWarning_Signs.Name = "labWarning_Signs";
            this.labWarning_Signs.Size = new System.Drawing.Size(13, 13);
            this.labWarning_Signs.TabIndex = 38;
            this.labWarning_Signs.Text = "  ";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(27, 168);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 37;
            this.checkBox1.Text = "自動移動";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.btnBox_check);
            // 
            // btnCtrlByMouse
            // 
            this.btnCtrlByMouse.Location = new System.Drawing.Point(104, 234);
            this.btnCtrlByMouse.Name = "btnCtrlByMouse";
            this.btnCtrlByMouse.Size = new System.Drawing.Size(177, 49);
            this.btnCtrlByMouse.TabIndex = 35;
            this.btnCtrlByMouse.Text = "使用滑鼠控制";
            this.btnCtrlByMouse.UseVisualStyleBackColor = true;
            this.btnCtrlByMouse.Click += new System.EventHandler(this.btnCtrlByMouse_Click);
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(9, 36);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(13, 12);
            this.labelX.TabIndex = 24;
            this.labelX.Text = "X";
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(28, 31);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(71, 22);
            this.numericUpDownX.TabIndex = 23;
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.btnAutoStartMove);
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(28, 59);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(71, 22);
            this.numericUpDownY.TabIndex = 25;
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.btnAutoStartMove);
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(9, 64);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(13, 12);
            this.labelY.TabIndex = 26;
            this.labelY.Text = "Y";
            // 
            // numericUpDownZ
            // 
            this.numericUpDownZ.Location = new System.Drawing.Point(28, 87);
            this.numericUpDownZ.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDownZ.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownZ.Name = "numericUpDownZ";
            this.numericUpDownZ.Size = new System.Drawing.Size(71, 22);
            this.numericUpDownZ.TabIndex = 27;
            this.numericUpDownZ.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownZ.ValueChanged += new System.EventHandler(this.btnAutoStartMove);
            // 
            // labelZ
            // 
            this.labelZ.AutoSize = true;
            this.labelZ.Location = new System.Drawing.Point(9, 92);
            this.labelZ.Name = "labelZ";
            this.labelZ.Size = new System.Drawing.Size(12, 12);
            this.labelZ.TabIndex = 28;
            this.labelZ.Text = "Z";
            // 
            // btnStartMove
            // 
            this.btnStartMove.Location = new System.Drawing.Point(10, 223);
            this.btnStartMove.Name = "btnStartMove";
            this.btnStartMove.Size = new System.Drawing.Size(88, 33);
            this.btnStartMove.TabIndex = 33;
            this.btnStartMove.Text = "移動";
            this.btnStartMove.UseVisualStyleBackColor = true;
            this.btnStartMove.Click += new System.EventHandler(this.btnStartMove_Click);
            // 
            // numericUpDownT
            // 
            this.numericUpDownT.Enabled = false;
            this.numericUpDownT.Location = new System.Drawing.Point(28, 140);
            this.numericUpDownT.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDownT.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            -2147483648});
            this.numericUpDownT.Name = "numericUpDownT";
            this.numericUpDownT.Size = new System.Drawing.Size(71, 22);
            this.numericUpDownT.TabIndex = 29;
            this.numericUpDownT.ValueChanged += new System.EventHandler(this.btnAutoStartMove);
            // 
            // labelC
            // 
            this.labelC.AutoSize = true;
            this.labelC.Location = new System.Drawing.Point(9, 118);
            this.labelC.Name = "labelC";
            this.labelC.Size = new System.Drawing.Size(13, 12);
            this.labelC.TabIndex = 32;
            this.labelC.Text = "C";
            // 
            // labelT
            // 
            this.labelT.AutoSize = true;
            this.labelT.Enabled = false;
            this.labelT.Location = new System.Drawing.Point(9, 144);
            this.labelT.Name = "labelT";
            this.labelT.Size = new System.Drawing.Size(12, 12);
            this.labelT.TabIndex = 30;
            this.labelT.Text = "T";
            // 
            // numericUpDownC
            // 
            this.numericUpDownC.Location = new System.Drawing.Point(28, 112);
            this.numericUpDownC.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDownC.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            -2147483648});
            this.numericUpDownC.Name = "numericUpDownC";
            this.numericUpDownC.Size = new System.Drawing.Size(71, 22);
            this.numericUpDownC.TabIndex = 31;
            this.numericUpDownC.ValueChanged += new System.EventHandler(this.btnAutoStartMove);
            // 
            // panel_MouseCtrl
            // 
            this.panel_MouseCtrl.BackColor = System.Drawing.Color.Black;
            this.panel_MouseCtrl.Controls.Add(this.label_MovePos);
            this.panel_MouseCtrl.Location = new System.Drawing.Point(104, 31);
            this.panel_MouseCtrl.Margin = new System.Windows.Forms.Padding(2);
            this.panel_MouseCtrl.Name = "panel_MouseCtrl";
            this.panel_MouseCtrl.Size = new System.Drawing.Size(177, 197);
            this.panel_MouseCtrl.TabIndex = 36;
            this.panel_MouseCtrl.Visible = false;
            this.panel_MouseCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseCtrl_MouseDown);
            this.panel_MouseCtrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseCtrl_MouseMove);
            this.panel_MouseCtrl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseCtrl_MouseUp);
            // 
            // label_MovePos
            // 
            this.label_MovePos.BackColor = System.Drawing.Color.White;
            this.label_MovePos.Enabled = false;
            this.label_MovePos.Location = new System.Drawing.Point(84, 8);
            this.label_MovePos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_MovePos.Name = "label_MovePos";
            this.label_MovePos.Size = new System.Drawing.Size(11, 12);
            this.label_MovePos.TabIndex = 0;
            // 
            // groupBox_CenterPos
            // 
            this.groupBox_CenterPos.Controls.Add(this.label_Center);
            this.groupBox_CenterPos.Controls.Add(this.btnDrawArc);
            this.groupBox_CenterPos.Controls.Add(this.numericUpDown_CenterX);
            this.groupBox_CenterPos.Controls.Add(this.btnTest);
            this.groupBox_CenterPos.Controls.Add(this.numericUpDown_arcAngle);
            this.groupBox_CenterPos.Controls.Add(this.label_Centery);
            this.groupBox_CenterPos.Controls.Add(this.numericUpDown_drawAngle);
            this.groupBox_CenterPos.Controls.Add(this.numericUpDown_CenterY);
            this.groupBox_CenterPos.Controls.Add(this.label2_CenterX);
            this.groupBox_CenterPos.Controls.Add(this.btnDrawCycle);
            this.groupBox_CenterPos.Location = new System.Drawing.Point(655, 28);
            this.groupBox_CenterPos.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_CenterPos.Name = "groupBox_CenterPos";
            this.groupBox_CenterPos.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_CenterPos.Size = new System.Drawing.Size(128, 300);
            this.groupBox_CenterPos.TabIndex = 48;
            this.groupBox_CenterPos.TabStop = false;
            this.groupBox_CenterPos.Text = "圓弧移動";
            // 
            // label_Center
            // 
            this.label_Center.AutoSize = true;
            this.label_Center.Location = new System.Drawing.Point(5, 31);
            this.label_Center.Name = "label_Center";
            this.label_Center.Size = new System.Drawing.Size(59, 12);
            this.label_Center.TabIndex = 51;
            this.label_Center.Text = "圓心座標: ";
            // 
            // btnDrawArc
            // 
            this.btnDrawArc.Location = new System.Drawing.Point(68, 181);
            this.btnDrawArc.Name = "btnDrawArc";
            this.btnDrawArc.Size = new System.Drawing.Size(50, 43);
            this.btnDrawArc.TabIndex = 50;
            this.btnDrawArc.Text = "移動\r\n角度";
            this.btnDrawArc.UseVisualStyleBackColor = true;
            this.btnDrawArc.Click += new System.EventHandler(this.btnDrawArc_Click);
            // 
            // numericUpDown_CenterX
            // 
            this.numericUpDown_CenterX.Location = new System.Drawing.Point(27, 49);
            this.numericUpDown_CenterX.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDown_CenterX.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            -2147483648});
            this.numericUpDown_CenterX.Name = "numericUpDown_CenterX";
            this.numericUpDown_CenterX.Size = new System.Drawing.Size(90, 22);
            this.numericUpDown_CenterX.TabIndex = 44;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(11, 236);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(106, 49);
            this.btnTest.TabIndex = 39;
            this.btnTest.Text = "測試用";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // numericUpDown_arcAngle
            // 
            this.numericUpDown_arcAngle.Location = new System.Drawing.Point(11, 204);
            this.numericUpDown_arcAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown_arcAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericUpDown_arcAngle.Name = "numericUpDown_arcAngle";
            this.numericUpDown_arcAngle.Size = new System.Drawing.Size(50, 22);
            this.numericUpDown_arcAngle.TabIndex = 44;
            this.numericUpDown_arcAngle.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label_Centery
            // 
            this.label_Centery.AutoSize = true;
            this.label_Centery.Location = new System.Drawing.Point(8, 80);
            this.label_Centery.Name = "label_Centery";
            this.label_Centery.Size = new System.Drawing.Size(13, 12);
            this.label_Centery.TabIndex = 47;
            this.label_Centery.Text = "Y";
            // 
            // numericUpDown_drawAngle
            // 
            this.numericUpDown_drawAngle.Location = new System.Drawing.Point(11, 136);
            this.numericUpDown_drawAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown_drawAngle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_drawAngle.Name = "numericUpDown_drawAngle";
            this.numericUpDown_drawAngle.Size = new System.Drawing.Size(50, 22);
            this.numericUpDown_drawAngle.TabIndex = 40;
            this.numericUpDown_drawAngle.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // numericUpDown_CenterY
            // 
            this.numericUpDown_CenterY.Location = new System.Drawing.Point(27, 75);
            this.numericUpDown_CenterY.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDown_CenterY.Minimum = new decimal(new int[] {
            600,
            0,
            0,
            -2147483648});
            this.numericUpDown_CenterY.Name = "numericUpDown_CenterY";
            this.numericUpDown_CenterY.Size = new System.Drawing.Size(90, 22);
            this.numericUpDown_CenterY.TabIndex = 45;
            this.numericUpDown_CenterY.Value = new decimal(new int[] {
            450,
            0,
            0,
            0});
            // 
            // label2_CenterX
            // 
            this.label2_CenterX.AutoSize = true;
            this.label2_CenterX.Location = new System.Drawing.Point(8, 54);
            this.label2_CenterX.Name = "label2_CenterX";
            this.label2_CenterX.Size = new System.Drawing.Size(13, 12);
            this.label2_CenterX.TabIndex = 46;
            this.label2_CenterX.Text = "X";
            // 
            // btnDrawCycle
            // 
            this.btnDrawCycle.Location = new System.Drawing.Point(68, 113);
            this.btnDrawCycle.Name = "btnDrawCycle";
            this.btnDrawCycle.Size = new System.Drawing.Size(50, 43);
            this.btnDrawCycle.TabIndex = 38;
            this.btnDrawCycle.Text = "角度\r\n畫圓";
            this.btnDrawCycle.UseVisualStyleBackColor = true;
            this.btnDrawCycle.Click += new System.EventHandler(this.btnDrawCycle_Click);
            // 
            // groupBox_Toshiba
            // 
            this.groupBox_Toshiba.Controls.Add(this.label_Override);
            this.groupBox_Toshiba.Controls.Add(this.label_ToshibaPort);
            this.groupBox_Toshiba.Controls.Add(this.label_ToshibaIP);
            this.groupBox_Toshiba.Controls.Add(this.domainUpDown_Config);
            this.groupBox_Toshiba.Controls.Add(this.btnResetAlarm);
            this.groupBox_Toshiba.Controls.Add(this.btnOverride);
            this.groupBox_Toshiba.Controls.Add(this.numericUpDown_Override);
            this.groupBox_Toshiba.Controls.Add(this.btnStopControl);
            this.groupBox_Toshiba.Controls.Add(this.textBox_ToshibaIPAddr);
            this.groupBox_Toshiba.Controls.Add(this.btnStartControl);
            this.groupBox_Toshiba.Controls.Add(this.textBox_ToshibaPort);
            this.groupBox_Toshiba.Location = new System.Drawing.Point(20, 28);
            this.groupBox_Toshiba.Name = "groupBox_Toshiba";
            this.groupBox_Toshiba.Size = new System.Drawing.Size(212, 300);
            this.groupBox_Toshiba.TabIndex = 5;
            this.groupBox_Toshiba.TabStop = false;
            this.groupBox_Toshiba.Text = "Toshiba";
            // 
            // label_Override
            // 
            this.label_Override.AutoSize = true;
            this.label_Override.Location = new System.Drawing.Point(16, 213);
            this.label_Override.Name = "label_Override";
            this.label_Override.Size = new System.Drawing.Size(53, 12);
            this.label_Override.TabIndex = 93;
            this.label_Override.Text = "速度上限";
            // 
            // label_ToshibaPort
            // 
            this.label_ToshibaPort.AutoSize = true;
            this.label_ToshibaPort.Font = new System.Drawing.Font("新細明體", 9F);
            this.label_ToshibaPort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_ToshibaPort.Location = new System.Drawing.Point(15, 64);
            this.label_ToshibaPort.Name = "label_ToshibaPort";
            this.label_ToshibaPort.Size = new System.Drawing.Size(108, 12);
            this.label_ToshibaPort.TabIndex = 92;
            this.label_ToshibaPort.Text = "TCP Port (Command)";
            // 
            // label_ToshibaIP
            // 
            this.label_ToshibaIP.AutoSize = true;
            this.label_ToshibaIP.Font = new System.Drawing.Font("新細明體", 9F);
            this.label_ToshibaIP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_ToshibaIP.Location = new System.Drawing.Point(16, 36);
            this.label_ToshibaIP.Name = "label_ToshibaIP";
            this.label_ToshibaIP.Size = new System.Drawing.Size(52, 12);
            this.label_ToshibaIP.TabIndex = 90;
            this.label_ToshibaIP.Text = "IP address";
            // 
            // domainUpDown_Config
            // 
            this.domainUpDown_Config.Items.Add("FREE");
            this.domainUpDown_Config.Items.Add("LEFTY");
            this.domainUpDown_Config.Items.Add("RIGHTY");
            this.domainUpDown_Config.Location = new System.Drawing.Point(16, 181);
            this.domainUpDown_Config.Margin = new System.Windows.Forms.Padding(2);
            this.domainUpDown_Config.Name = "domainUpDown_Config";
            this.domainUpDown_Config.ReadOnly = true;
            this.domainUpDown_Config.Size = new System.Drawing.Size(180, 22);
            this.domainUpDown_Config.TabIndex = 43;
            this.domainUpDown_Config.Text = "請選擇方向";
            this.domainUpDown_Config.Visible = false;
            // 
            // btnResetAlarm
            // 
            this.btnResetAlarm.Location = new System.Drawing.Point(96, 87);
            this.btnResetAlarm.Name = "btnResetAlarm";
            this.btnResetAlarm.Size = new System.Drawing.Size(100, 75);
            this.btnResetAlarm.TabIndex = 2;
            this.btnResetAlarm.Text = "重置警告訊息";
            this.btnResetAlarm.UseVisualStyleBackColor = true;
            this.btnResetAlarm.Click += new System.EventHandler(this.btnResetAlarm_Click);
            // 
            // btnOverride
            // 
            this.btnOverride.Location = new System.Drawing.Point(16, 236);
            this.btnOverride.Name = "btnOverride";
            this.btnOverride.Size = new System.Drawing.Size(180, 49);
            this.btnOverride.TabIndex = 25;
            this.btnOverride.Text = "設定";
            this.btnOverride.UseVisualStyleBackColor = true;
            this.btnOverride.Click += new System.EventHandler(this.btnOverride_Click);
            // 
            // numericUpDown_Override
            // 
            this.numericUpDown_Override.Location = new System.Drawing.Point(75, 208);
            this.numericUpDown_Override.Name = "numericUpDown_Override";
            this.numericUpDown_Override.Size = new System.Drawing.Size(121, 22);
            this.numericUpDown_Override.TabIndex = 24;
            this.numericUpDown_Override.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // textBox_ToshibaIPAddr
            // 
            this.textBox_ToshibaIPAddr.Location = new System.Drawing.Point(73, 31);
            this.textBox_ToshibaIPAddr.Name = "textBox_ToshibaIPAddr";
            this.textBox_ToshibaIPAddr.Size = new System.Drawing.Size(123, 22);
            this.textBox_ToshibaIPAddr.TabIndex = 3;
            this.textBox_ToshibaIPAddr.Text = "192.168.0.124";
            // 
            // textBox_ToshibaPort
            // 
            this.textBox_ToshibaPort.Location = new System.Drawing.Point(129, 59);
            this.textBox_ToshibaPort.Name = "textBox_ToshibaPort";
            this.textBox_ToshibaPort.Size = new System.Drawing.Size(67, 22);
            this.textBox_ToshibaPort.TabIndex = 4;
            this.textBox_ToshibaPort.Text = "1000";
            // 
            // tabPage_3DModel
            // 
            this.tabPage_3DModel.Controls.Add(this.btnChangeRange);
            this.tabPage_3DModel.Controls.Add(this.maskedTextBox_RangeMin);
            this.tabPage_3DModel.Controls.Add(this.maskedTextBox_RangeMax);
            this.tabPage_3DModel.Controls.Add(this.btn_OpenExcel);
            this.tabPage_3DModel.Controls.Add(this.btnRstOpenGL);
            this.tabPage_3DModel.Controls.Add(this.btnShow3D);
            this.tabPage_3DModel.Controls.Add(this.OpenGLCtrl_Orig);
            this.tabPage_3DModel.Controls.Add(this.groupBox_Testing);
            this.tabPage_3DModel.Controls.Add(this.pictureBox1);
            this.tabPage_3DModel.Location = new System.Drawing.Point(4, 22);
            this.tabPage_3DModel.Name = "tabPage_3DModel";
            this.tabPage_3DModel.Size = new System.Drawing.Size(817, 342);
            this.tabPage_3DModel.TabIndex = 3;
            this.tabPage_3DModel.Text = "3DModel";
            this.tabPage_3DModel.UseVisualStyleBackColor = true;
            // 
            // btnChangeRange
            // 
            this.btnChangeRange.Location = new System.Drawing.Point(225, 86);
            this.btnChangeRange.Name = "btnChangeRange";
            this.btnChangeRange.Size = new System.Drawing.Size(75, 42);
            this.btnChangeRange.TabIndex = 93;
            this.btnChangeRange.Text = "更新範圍";
            this.btnChangeRange.UseVisualStyleBackColor = true;
            this.btnChangeRange.Click += new System.EventHandler(this.btnChangeRange_Click);
            // 
            // maskedTextBox_RangeMin
            // 
            this.maskedTextBox_RangeMin.Location = new System.Drawing.Point(225, 56);
            this.maskedTextBox_RangeMin.Margin = new System.Windows.Forms.Padding(2);
            this.maskedTextBox_RangeMin.Mask = "99.9999";
            this.maskedTextBox_RangeMin.Name = "maskedTextBox_RangeMin";
            this.maskedTextBox_RangeMin.Size = new System.Drawing.Size(76, 22);
            this.maskedTextBox_RangeMin.TabIndex = 92;
            // 
            // maskedTextBox_RangeMax
            // 
            this.maskedTextBox_RangeMax.Location = new System.Drawing.Point(225, 31);
            this.maskedTextBox_RangeMax.Margin = new System.Windows.Forms.Padding(2);
            this.maskedTextBox_RangeMax.Mask = "99.9999";
            this.maskedTextBox_RangeMax.Name = "maskedTextBox_RangeMax";
            this.maskedTextBox_RangeMax.Size = new System.Drawing.Size(76, 22);
            this.maskedTextBox_RangeMax.TabIndex = 91;
            // 
            // btn_OpenExcel
            // 
            this.btn_OpenExcel.Location = new System.Drawing.Point(225, 287);
            this.btn_OpenExcel.Name = "btn_OpenExcel";
            this.btn_OpenExcel.Size = new System.Drawing.Size(64, 50);
            this.btn_OpenExcel.TabIndex = 88;
            this.btn_OpenExcel.Text = "開Excel";
            this.btn_OpenExcel.UseVisualStyleBackColor = true;
            this.btn_OpenExcel.Click += new System.EventHandler(this.btn_OpenExcel_Click);
            // 
            // btnRstOpenGL
            // 
            this.btnRstOpenGL.Location = new System.Drawing.Point(225, 220);
            this.btnRstOpenGL.Name = "btnRstOpenGL";
            this.btnRstOpenGL.Size = new System.Drawing.Size(64, 50);
            this.btnRstOpenGL.TabIndex = 88;
            this.btnRstOpenGL.Text = "Reset";
            this.btnRstOpenGL.UseVisualStyleBackColor = true;
            this.btnRstOpenGL.Click += new System.EventHandler(this.btnRstOpenGL_Click);
            // 
            // btnShow3D
            // 
            this.btnShow3D.Location = new System.Drawing.Point(225, 149);
            this.btnShow3D.Name = "btnShow3D";
            this.btnShow3D.Size = new System.Drawing.Size(64, 50);
            this.btnShow3D.TabIndex = 84;
            this.btnShow3D.Text = "3D";
            this.btnShow3D.UseVisualStyleBackColor = true;
            this.btnShow3D.Click += new System.EventHandler(this.btnShow3D_Click);
            // 
            // OpenGLCtrl_Orig
            // 
            this.OpenGLCtrl_Orig.DrawRenderTime = false;
            this.OpenGLCtrl_Orig.FrameRate = 10F;
            this.OpenGLCtrl_Orig.GDIEnabled = false;
            this.OpenGLCtrl_Orig.Location = new System.Drawing.Point(322, 6);
            this.OpenGLCtrl_Orig.Name = "OpenGLCtrl_Orig";
            this.OpenGLCtrl_Orig.Size = new System.Drawing.Size(472, 324);
            this.OpenGLCtrl_Orig.TabIndex = 87;
            this.OpenGLCtrl_Orig.Visible = false;
            this.OpenGLCtrl_Orig.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.OpenGLCtrl_Orig_OpenGLDraw);
            this.OpenGLCtrl_Orig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OpenGLCtrl_MouseDown);
            this.OpenGLCtrl_Orig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpenGLCtrl_MouseUp);
            // 
            // groupBox_Testing
            // 
            this.groupBox_Testing.Controls.Add(this.btnStart3DModel_Laser);
            this.groupBox_Testing.Controls.Add(this.btnResetAlarm_3D);
            this.groupBox_Testing.Controls.Add(this.btnStartControl_3D);
            this.groupBox_Testing.Controls.Add(this.btnCurrentImage_Stop_3D);
            this.groupBox_Testing.Controls.Add(this.btnStopControl_3D);
            this.groupBox_Testing.Controls.Add(this.btnCurrentImage_3D);
            this.groupBox_Testing.Controls.Add(this.btnStart3DModel);
            this.groupBox_Testing.Location = new System.Drawing.Point(12, 24);
            this.groupBox_Testing.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Testing.Name = "groupBox_Testing";
            this.groupBox_Testing.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Testing.Size = new System.Drawing.Size(208, 306);
            this.groupBox_Testing.TabIndex = 86;
            this.groupBox_Testing.TabStop = false;
            this.groupBox_Testing.Text = "進行量測";
            // 
            // btnStart3DModel_Laser
            // 
            this.btnStart3DModel_Laser.Location = new System.Drawing.Point(15, 255);
            this.btnStart3DModel_Laser.Name = "btnStart3DModel_Laser";
            this.btnStart3DModel_Laser.Size = new System.Drawing.Size(178, 44);
            this.btnStart3DModel_Laser.TabIndex = 84;
            this.btnStart3DModel_Laser.Text = "雷射旋轉量測";
            this.btnStart3DModel_Laser.UseVisualStyleBackColor = true;
            this.btnStart3DModel_Laser.Click += new System.EventHandler(this.btnStart3DModel_Laser_Click);
            // 
            // btnResetAlarm_3D
            // 
            this.btnResetAlarm_3D.Location = new System.Drawing.Point(93, 20);
            this.btnResetAlarm_3D.Name = "btnResetAlarm_3D";
            this.btnResetAlarm_3D.Size = new System.Drawing.Size(100, 75);
            this.btnResetAlarm_3D.TabIndex = 4;
            this.btnResetAlarm_3D.Text = "重置警告訊息";
            this.btnResetAlarm_3D.UseVisualStyleBackColor = true;
            this.btnResetAlarm_3D.Click += new System.EventHandler(this.btnResetAlarm_Click);
            // 
            // btnStartControl_3D
            // 
            this.btnStartControl_3D.BackColor = System.Drawing.Color.Green;
            this.btnStartControl_3D.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStartControl_3D.ForeColor = System.Drawing.Color.White;
            this.btnStartControl_3D.Location = new System.Drawing.Point(15, 20);
            this.btnStartControl_3D.Name = "btnStartControl_3D";
            this.btnStartControl_3D.Size = new System.Drawing.Size(74, 36);
            this.btnStartControl_3D.TabIndex = 3;
            this.btnStartControl_3D.Text = "啟動";
            this.btnStartControl_3D.UseVisualStyleBackColor = false;
            this.btnStartControl_3D.Click += new System.EventHandler(this.btnStartControl_Click);
            // 
            // btnCurrentImage_Stop_3D
            // 
            this.btnCurrentImage_Stop_3D.BackColor = System.Drawing.Color.Red;
            this.btnCurrentImage_Stop_3D.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnCurrentImage_Stop_3D.ForeColor = System.Drawing.Color.White;
            this.btnCurrentImage_Stop_3D.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCurrentImage_Stop_3D.Location = new System.Drawing.Point(15, 158);
            this.btnCurrentImage_Stop_3D.Name = "btnCurrentImage_Stop_3D";
            this.btnCurrentImage_Stop_3D.Size = new System.Drawing.Size(98, 41);
            this.btnCurrentImage_Stop_3D.TabIndex = 83;
            this.btnCurrentImage_Stop_3D.Text = "關閉雷射";
            this.btnCurrentImage_Stop_3D.UseVisualStyleBackColor = false;
            this.btnCurrentImage_Stop_3D.Visible = false;
            this.btnCurrentImage_Stop_3D.Click += new System.EventHandler(this.btnCurrentImage_Stop_3D_Click);
            // 
            // btnStopControl_3D
            // 
            this.btnStopControl_3D.BackColor = System.Drawing.Color.Red;
            this.btnStopControl_3D.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStopControl_3D.ForeColor = System.Drawing.Color.White;
            this.btnStopControl_3D.Location = new System.Drawing.Point(15, 62);
            this.btnStopControl_3D.Name = "btnStopControl_3D";
            this.btnStopControl_3D.Size = new System.Drawing.Size(74, 33);
            this.btnStopControl_3D.TabIndex = 5;
            this.btnStopControl_3D.Text = "停止";
            this.btnStopControl_3D.UseVisualStyleBackColor = false;
            this.btnStopControl_3D.Visible = false;
            this.btnStopControl_3D.Click += new System.EventHandler(this.btnStopControl_Click);
            // 
            // btnCurrentImage_3D
            // 
            this.btnCurrentImage_3D.BackColor = System.Drawing.Color.Green;
            this.btnCurrentImage_3D.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnCurrentImage_3D.ForeColor = System.Drawing.Color.White;
            this.btnCurrentImage_3D.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCurrentImage_3D.Location = new System.Drawing.Point(15, 110);
            this.btnCurrentImage_3D.Name = "btnCurrentImage_3D";
            this.btnCurrentImage_3D.Size = new System.Drawing.Size(98, 41);
            this.btnCurrentImage_3D.TabIndex = 82;
            this.btnCurrentImage_3D.Text = "開啟雷射";
            this.btnCurrentImage_3D.UseVisualStyleBackColor = false;
            this.btnCurrentImage_3D.Click += new System.EventHandler(this.btnCurrentImage_3D_Click);
            // 
            // btnStart3DModel
            // 
            this.btnStart3DModel.Location = new System.Drawing.Point(15, 205);
            this.btnStart3DModel.Name = "btnStart3DModel";
            this.btnStart3DModel.Size = new System.Drawing.Size(178, 44);
            this.btnStart3DModel.TabIndex = 6;
            this.btnStart3DModel.Text = "物體旋轉量測";
            this.btnStart3DModel.UseVisualStyleBackColor = true;
            this.btnStart3DModel.Click += new System.EventHandler(this.btnStart3DModel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(322, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(472, 324);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 85;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage_Log
            // 
            this.tabPage_Log.Controls.Add(this.btnClearMessage);
            this.tabPage_Log.Controls.Add(this.GetStatusAll);
            this.tabPage_Log.Controls.Add(this.btnGetPosition);
            this.tabPage_Log.Controls.Add(this.textBox_Message);
            this.tabPage_Log.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Log.Name = "tabPage_Log";
            this.tabPage_Log.Size = new System.Drawing.Size(817, 342);
            this.tabPage_Log.TabIndex = 2;
            this.tabPage_Log.Text = "Logs";
            this.tabPage_Log.UseVisualStyleBackColor = true;
            // 
            // btnClearMessage
            // 
            this.btnClearMessage.Location = new System.Drawing.Point(693, 130);
            this.btnClearMessage.Name = "btnClearMessage";
            this.btnClearMessage.Size = new System.Drawing.Size(98, 53);
            this.btnClearMessage.TabIndex = 25;
            this.btnClearMessage.Text = "清空";
            this.btnClearMessage.UseVisualStyleBackColor = true;
            this.btnClearMessage.Click += new System.EventHandler(this.btnClearMessage_Click);
            // 
            // GetStatusAll
            // 
            this.GetStatusAll.Location = new System.Drawing.Point(693, 13);
            this.GetStatusAll.Margin = new System.Windows.Forms.Padding(2);
            this.GetStatusAll.Name = "GetStatusAll";
            this.GetStatusAll.Size = new System.Drawing.Size(98, 54);
            this.GetStatusAll.TabIndex = 24;
            this.GetStatusAll.Text = "狀態";
            this.GetStatusAll.UseVisualStyleBackColor = true;
            this.GetStatusAll.Click += new System.EventHandler(this.GetStatusAll_Click);
            // 
            // btnGetPosition
            // 
            this.btnGetPosition.Location = new System.Drawing.Point(693, 71);
            this.btnGetPosition.Name = "btnGetPosition";
            this.btnGetPosition.Size = new System.Drawing.Size(98, 53);
            this.btnGetPosition.TabIndex = 15;
            this.btnGetPosition.Text = "座標";
            this.btnGetPosition.UseVisualStyleBackColor = true;
            this.btnGetPosition.Click += new System.EventHandler(this.btnGetPosition_Click);
            // 
            // textBox_Message
            // 
            this.textBox_Message.Location = new System.Drawing.Point(18, 13);
            this.textBox_Message.Multiline = true;
            this.textBox_Message.Name = "textBox_Message";
            this.textBox_Message.ReadOnly = true;
            this.textBox_Message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Message.Size = new System.Drawing.Size(659, 313);
            this.textBox_Message.TabIndex = 0;
            // 
            // timerGetCoordinate
            // 
            this.timerGetCoordinate.Tick += new System.EventHandler(this.timerGetCoordinate_Tick);
            // 
            // btnCloseApp
            // 
            this.btnCloseApp.Font = new System.Drawing.Font("新細明體", 9F);
            this.btnCloseApp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCloseApp.Location = new System.Drawing.Point(758, 1);
            this.btnCloseApp.Name = "btnCloseApp";
            this.btnCloseApp.Size = new System.Drawing.Size(75, 30);
            this.btnCloseApp.TabIndex = 86;
            this.btnCloseApp.Text = "關閉";
            this.btnCloseApp.UseVisualStyleBackColor = true;
            this.btnCloseApp.Click += new System.EventHandler(this.btnCloseApp_Click);
            // 
            // timer_CurrentFPS
            // 
            this.timer_CurrentFPS.Interval = 1000;
            this.timer_CurrentFPS.Tick += new System.EventHandler(this.timer_CurrentFPS_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 416);
            this.Controls.Add(this.btnCloseApp);
            this.Controls.Add(this.tabControl_main);
            this.Controls.Add(this._StatusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "恒耀工業";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.tabControl_main.ResumeLayout(false);
            this.tabPage_LJV7.ResumeLayout(false);
            this.tabPage_LJV7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CurrentImage)).EndInit();
            this.groupBox_CurrentImage.ResumeLayout(false);
            this.groupBox_LJV7.ResumeLayout(false);
            this.groupBox_LJV7.PerformLayout();
            this.tabPage_Toshiba.ResumeLayout(false);
            this.groupBox_MoveJoint.ResumeLayout(false);
            this.groupBox_MoveJoint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_J4)).EndInit();
            this.groupBox_MovePos.ResumeLayout(false);
            this.groupBox_MovePos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_CurrentImage_Auto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownC)).EndInit();
            this.panel_MouseCtrl.ResumeLayout(false);
            this.groupBox_CenterPos.ResumeLayout(false);
            this.groupBox_CenterPos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CenterX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_arcAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_drawAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CenterY)).EndInit();
            this.groupBox_Toshiba.ResumeLayout(false);
            this.groupBox_Toshiba.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Override)).EndInit();
            this.tabPage_3DModel.ResumeLayout(false);
            this.tabPage_3DModel.PerformLayout();
            this.groupBox_Testing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage_Log.ResumeLayout(false);
            this.tabPage_Log.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartControl;
        private System.Windows.Forms.Button btnStopControl;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Content;
        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.TabPage tabPage_LJV7;
        private System.Windows.Forms.TabPage tabPage_Toshiba;
        private System.Windows.Forms.TabPage tabPage_Log;
        private System.Windows.Forms.GroupBox groupBox_Toshiba;
        private System.Windows.Forms.TextBox textBox_ToshibaIPAddr;
        private System.Windows.Forms.TextBox textBox_ToshibaPort;
        private System.Windows.Forms.TextBox textBox_Message;
        private System.Windows.Forms.Button btnResetAlarm;
        private System.Windows.Forms.Button btnGetPosition;
        private System.Windows.Forms.Button GetStatusAll;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Label labelC;
        private System.Windows.Forms.NumericUpDown numericUpDownC;
        private System.Windows.Forms.Label labelT;
        private System.Windows.Forms.NumericUpDown numericUpDownT;
        private System.Windows.Forms.Label labelZ;
        private System.Windows.Forms.NumericUpDown numericUpDownZ;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.Timer timerGetCoordinate;
        private System.Windows.Forms.Button btnStartMove;
        private System.Windows.Forms.Button btnDrawCycle;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.NumericUpDown numericUpDown_Override;
        private System.Windows.Forms.Button btnOverride;
        private System.Windows.Forms.Button btnClearMessage;
        private System.Windows.Forms.NumericUpDown numericUpDown_drawAngle;
        private System.Windows.Forms.DomainUpDown domainUpDown_Config;
        private System.Windows.Forms.NumericUpDown numericUpDown_CenterY;
        private System.Windows.Forms.NumericUpDown numericUpDown_CenterX;
        private System.Windows.Forms.Label label_Centery;
        private System.Windows.Forms.Label label2_CenterX;
        private System.Windows.Forms.GroupBox groupBox_CenterPos;
        private System.Windows.Forms.GroupBox groupBox_MovePos;
        private System.Windows.Forms.NumericUpDown numericUpDown_arcAngle;
        private System.Windows.Forms.Button btnDrawArc;
        private System.Windows.Forms.GroupBox groupBox_LJV7;
        private System.Windows.Forms.MaskedTextBox textBox_LJV7CallbackFrequency;
        private System.Windows.Forms.TextBox textBox_LJV7StartProfileNo;
        private System.Windows.Forms.Label _lblCallbackFrequency;
        private System.Windows.Forms.TextBox textBox_LJV7IPAddr;
        private System.Windows.Forms.Label _lblHighSpeedStartNo;
        private System.Windows.Forms.Label label_LJV7Port_HighSpeed;
        private System.Windows.Forms.TextBox textBox_LJV7Port_HighSpeedPort;
        private System.Windows.Forms.Label label_LJV7IP;
        private System.Windows.Forms.TextBox textBox_LJV7Port_CommandPort;
        private System.Windows.Forms.Label label_LJV7Port_Command;
        private System.Windows.Forms.Label label_ToshibaIP;
        private System.Windows.Forms.Label label_ToshibaPort;
        private System.Windows.Forms.GroupBox groupBox_CurrentImage;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnCurrentImage_Stop;
        private System.Windows.Forms.Button btnFinalize;
        private System.Windows.Forms.Button btnCurrentImage;
        private System.Windows.Forms.Label label_CurrentFPS;
        private System.Windows.Forms.PictureBox pictureBox_CurrentImage;
        private System.Windows.Forms.Button btnCloseApp;
        private System.Windows.Forms.Timer timer_CurrentFPS;
        private System.Windows.Forms.TabPage tabPage_3DModel;
        private System.Windows.Forms.Label label_Center;
        private System.Windows.Forms.Button btnResetAlarm_3D;
        private System.Windows.Forms.Button btnStopControl_3D;
        private System.Windows.Forms.Button btnStartControl_3D;
        private System.Windows.Forms.Button btnStart3DModel;
        private System.Windows.Forms.GroupBox groupBox_MoveJoint;
        private System.Windows.Forms.Label label_J1;
        private System.Windows.Forms.NumericUpDown numericUpDown_J1;
        private System.Windows.Forms.NumericUpDown numericUpDown_J2;
        private System.Windows.Forms.Label label_J2;
        private System.Windows.Forms.NumericUpDown numericUpDown_J3;
        private System.Windows.Forms.Label label_J3;
        private System.Windows.Forms.Button btnStartMoveJoint;
        private System.Windows.Forms.NumericUpDown numericUpDown_J5;
        private System.Windows.Forms.Label label_J4;
        private System.Windows.Forms.Label label_J5;
        private System.Windows.Forms.NumericUpDown numericUpDown_J4;
        private System.Windows.Forms.Label label_Override;
        private System.Windows.Forms.Button btnCurrentImage_Stop_3D;
        private System.Windows.Forms.Button btnCurrentImage_3D;
        private System.Windows.Forms.Button btnHighSpeedDataEthernetCommunicationInitalize;
        private System.Windows.Forms.Button btnStopHighSpeedDataCommunication;
        private System.Windows.Forms.Button btnHighSpeedDataCommunicationFinalize;
        private System.Windows.Forms.Button btnStartHighSpeedDataCommunication;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox_Testing;
        private SharpGL.OpenGLCtrl OpenGLCtrl_Orig;
        private System.Windows.Forms.Button btnShow3D;
        private System.Windows.Forms.Button btnRstOpenGL;
        private System.Windows.Forms.Button btnCtrlByMouse;
        private System.Windows.Forms.Panel panel_MouseCtrl;
        private System.Windows.Forms.Label label_MovePos;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_RangeMax;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_RangeMin;
        private System.Windows.Forms.Button btnChangeRange;
        private System.Windows.Forms.Button btnStart3DModel_Laser;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label labWarning_Signs;
        private System.Windows.Forms.Button btnStopMove;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.PictureBox pictureBox_CurrentImage_Auto;
        private System.Windows.Forms.Button btnShow3D_Auto;
        private SharpGL.OpenGLCtrl OpenGLCtrl_Orig_Auto;
        private System.Windows.Forms.Button btnExportSTL;
        private System.Windows.Forms.Button btn_OpenExcel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

