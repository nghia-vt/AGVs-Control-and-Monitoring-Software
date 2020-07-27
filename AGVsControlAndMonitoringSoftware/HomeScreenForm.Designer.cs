namespace AGVsControlAndMonitoringSoftware
{
    partial class HomeScreenForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScreenForm));
            this.mnstrHomeScr = new System.Windows.Forms.MenuStrip();
            this.communicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aGVsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRemoveAGVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agvMonitoringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRemoveTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warehouseDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbModeStatus = new System.Windows.Forms.Label();
            this.btnPauseRun = new System.Windows.Forms.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.lbTime = new System.Windows.Forms.Label();
            this.rdbtnSimulation = new System.Windows.Forms.RadioButton();
            this.rdbtnRealTime = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewAGVs = new System.Windows.Forms.ListView();
            this.AGVID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExitNode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Orient = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DistanceToExitNode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Velocity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.TaskName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TaskStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TaskAGV = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PickNode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DropNode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PalletCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnFloor = new System.Windows.Forms.Panel();
            this.cntxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hidePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showBlockColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbD = new System.Windows.Forms.Label();
            this.lbC = new System.Windows.Forms.Label();
            this.lbD6 = new System.Windows.Forms.Label();
            this.lbC6 = new System.Windows.Forms.Label();
            this.lbB6 = new System.Windows.Forms.Label();
            this.lbD5 = new System.Windows.Forms.Label();
            this.lbC5 = new System.Windows.Forms.Label();
            this.lbB5 = new System.Windows.Forms.Label();
            this.lbA6 = new System.Windows.Forms.Label();
            this.lbD3 = new System.Windows.Forms.Label();
            this.lbC3 = new System.Windows.Forms.Label();
            this.lbB3 = new System.Windows.Forms.Label();
            this.lbD4 = new System.Windows.Forms.Label();
            this.lbA5 = new System.Windows.Forms.Label();
            this.lbC4 = new System.Windows.Forms.Label();
            this.lbD2 = new System.Windows.Forms.Label();
            this.lbB4 = new System.Windows.Forms.Label();
            this.lbC2 = new System.Windows.Forms.Label();
            this.lbA3 = new System.Windows.Forms.Label();
            this.lbD1 = new System.Windows.Forms.Label();
            this.lbB2 = new System.Windows.Forms.Label();
            this.lbC1 = new System.Windows.Forms.Label();
            this.lbA4 = new System.Windows.Forms.Label();
            this.lbB1 = new System.Windows.Forms.Label();
            this.lbA2 = new System.Windows.Forms.Label();
            this.lbA1 = new System.Windows.Forms.Label();
            this.lbB = new System.Windows.Forms.Label();
            this.lbA = new System.Windows.Forms.Label();
            this.btnAddPallet2 = new System.Windows.Forms.Button();
            this.btnAddPallet1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerGUI = new System.Windows.Forms.Timer(this.components);
            this.rtxtbComStatus = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timerComStatus = new System.Windows.Forms.Timer(this.components);
            this.timerCheckConn = new System.Windows.Forms.Timer(this.components);
            this.mnstrHomeScr.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnFloor.SuspendLayout();
            this.cntxMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnstrHomeScr
            // 
            this.mnstrHomeScr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mnstrHomeScr.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.mnstrHomeScr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.communicationToolStripMenuItem,
            this.aGVsToolStripMenuItem,
            this.taskToolStripMenuItem,
            this.orderToolStripMenuItem,
            this.warehouseDataToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnstrHomeScr.Location = new System.Drawing.Point(0, 0);
            this.mnstrHomeScr.Name = "mnstrHomeScr";
            this.mnstrHomeScr.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mnstrHomeScr.Size = new System.Drawing.Size(1159, 27);
            this.mnstrHomeScr.TabIndex = 0;
            this.mnstrHomeScr.Text = "mnstrHomeScr";
            // 
            // communicationToolStripMenuItem
            // 
            this.communicationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("communicationToolStripMenuItem.Image")));
            this.communicationToolStripMenuItem.Name = "communicationToolStripMenuItem";
            this.communicationToolStripMenuItem.Size = new System.Drawing.Size(134, 23);
            this.communicationToolStripMenuItem.Text = "&Communication";
            this.communicationToolStripMenuItem.Click += new System.EventHandler(this.communicationToolStripMenuItem_Click);
            // 
            // aGVsToolStripMenuItem
            // 
            this.aGVsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRemoveAGVToolStripMenuItem,
            this.agvMonitoringToolStripMenuItem});
            this.aGVsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aGVsToolStripMenuItem.Image")));
            this.aGVsToolStripMenuItem.Name = "aGVsToolStripMenuItem";
            this.aGVsToolStripMenuItem.Size = new System.Drawing.Size(71, 23);
            this.aGVsToolStripMenuItem.Text = "&AGVs";
            // 
            // addRemoveAGVToolStripMenuItem
            // 
            this.addRemoveAGVToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addRemoveAGVToolStripMenuItem.Image")));
            this.addRemoveAGVToolStripMenuItem.Name = "addRemoveAGVToolStripMenuItem";
            this.addRemoveAGVToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.addRemoveAGVToolStripMenuItem.Text = "&Add/Remove AGV";
            this.addRemoveAGVToolStripMenuItem.Click += new System.EventHandler(this.AddRemoveAGV_Click);
            // 
            // agvMonitoringToolStripMenuItem
            // 
            this.agvMonitoringToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("agvMonitoringToolStripMenuItem.Image")));
            this.agvMonitoringToolStripMenuItem.Name = "agvMonitoringToolStripMenuItem";
            this.agvMonitoringToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.agvMonitoringToolStripMenuItem.Text = "&Monitoring";
            this.agvMonitoringToolStripMenuItem.Click += new System.EventHandler(this.agvMonitoringToolStripMenuItem_Click);
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRemoveTaskToolStripMenuItem,
            this.taskManagementToolStripMenuItem});
            this.taskToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("taskToolStripMenuItem.Image")));
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            this.taskToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
            this.taskToolStripMenuItem.Text = "&Tasks";
            // 
            // addRemoveTaskToolStripMenuItem
            // 
            this.addRemoveTaskToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addRemoveTaskToolStripMenuItem.Image")));
            this.addRemoveTaskToolStripMenuItem.Name = "addRemoveTaskToolStripMenuItem";
            this.addRemoveTaskToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
            this.addRemoveTaskToolStripMenuItem.Text = "&Add/Remove Task";
            this.addRemoveTaskToolStripMenuItem.Click += new System.EventHandler(this.addRemoveTaskToolStripMenuItem_Click);
            // 
            // taskManagementToolStripMenuItem
            // 
            this.taskManagementToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("taskManagementToolStripMenuItem.Image")));
            this.taskManagementToolStripMenuItem.Name = "taskManagementToolStripMenuItem";
            this.taskManagementToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
            this.taskManagementToolStripMenuItem.Text = "&Monitoring";
            this.taskManagementToolStripMenuItem.Click += new System.EventHandler(this.taskManagementToolStripMenuItem_Click);
            // 
            // orderToolStripMenuItem
            // 
            this.orderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("orderToolStripMenuItem.Image")));
            this.orderToolStripMenuItem.Name = "orderToolStripMenuItem";
            this.orderToolStripMenuItem.Size = new System.Drawing.Size(73, 23);
            this.orderToolStripMenuItem.Text = "Order";
            this.orderToolStripMenuItem.Click += new System.EventHandler(this.orderToolStripMenuItem_Click);
            // 
            // warehouseDataToolStripMenuItem
            // 
            this.warehouseDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("warehouseDataToolStripMenuItem.Image")));
            this.warehouseDataToolStripMenuItem.Name = "warehouseDataToolStripMenuItem";
            this.warehouseDataToolStripMenuItem.Size = new System.Drawing.Size(139, 23);
            this.warehouseDataToolStripMenuItem.Text = "&Warehouse Data";
            this.warehouseDataToolStripMenuItem.Click += new System.EventHandler(this.warehouseDataToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportToolStripMenuItem.Image")));
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(78, 23);
            this.reportToolStripMenuItem.Text = "&Report";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operationManualToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripMenuItem.Image")));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 23);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // operationManualToolStripMenuItem
            // 
            this.operationManualToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("operationManualToolStripMenuItem.Image")));
            this.operationManualToolStripMenuItem.Name = "operationManualToolStripMenuItem";
            this.operationManualToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.operationManualToolStripMenuItem.Text = "Operation &Manual";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Lavender;
            this.groupBox1.Controls.Add(this.lbModeStatus);
            this.groupBox1.Controls.Add(this.btnPauseRun);
            this.groupBox1.Controls.Add(this.lbTime);
            this.groupBox1.Controls.Add(this.rdbtnSimulation);
            this.groupBox1.Controls.Add(this.rdbtnRealTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox1.Location = new System.Drawing.Point(757, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 110);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode Setting";
            // 
            // lbModeStatus
            // 
            this.lbModeStatus.AutoSize = true;
            this.lbModeStatus.Font = new System.Drawing.Font("Segoe UI Semilight", 10F);
            this.lbModeStatus.ForeColor = System.Drawing.Color.Navy;
            this.lbModeStatus.Image = ((System.Drawing.Image)(resources.GetObject("lbModeStatus.Image")));
            this.lbModeStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbModeStatus.Location = new System.Drawing.Point(25, 25);
            this.lbModeStatus.Name = "lbModeStatus";
            this.lbModeStatus.Size = new System.Drawing.Size(146, 19);
            this.lbModeStatus.TabIndex = 8;
            this.lbModeStatus.Text = "     Please select mode.";
            this.lbModeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPauseRun
            // 
            this.btnPauseRun.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPauseRun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPauseRun.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnPauseRun.FlatAppearance.BorderSize = 0;
            this.btnPauseRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPauseRun.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.btnPauseRun.ForeColor = System.Drawing.Color.Navy;
            this.btnPauseRun.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnPauseRun.ImageIndex = 3;
            this.btnPauseRun.ImageList = this.imgList;
            this.btnPauseRun.Location = new System.Drawing.Point(281, 47);
            this.btnPauseRun.Name = "btnPauseRun";
            this.btnPauseRun.Size = new System.Drawing.Size(70, 30);
            this.btnPauseRun.TabIndex = 7;
            this.btnPauseRun.Text = "Run";
            this.btnPauseRun.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPauseRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPauseRun.UseVisualStyleBackColor = false;
            this.btnPauseRun.Visible = false;
            this.btnPauseRun.Click += new System.EventHandler(this.btnPauseRun_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "icon_agv.ico");
            this.imgList.Images.SetKeyName(1, "icon_task.png");
            this.imgList.Images.SetKeyName(2, "icon_pause.png");
            this.imgList.Images.SetKeyName(3, "icon_run.png");
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.lbTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbTime.Location = new System.Drawing.Point(79, 82);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(227, 19);
            this.lbTime.TabIndex = 6;
            this.lbTime.Text = "dddd, MMMM dd, yyyy  h:mm:ss tt";
            // 
            // rdbtnSimulation
            // 
            this.rdbtnSimulation.AutoSize = true;
            this.rdbtnSimulation.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.rdbtnSimulation.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rdbtnSimulation.Location = new System.Drawing.Point(178, 50);
            this.rdbtnSimulation.Name = "rdbtnSimulation";
            this.rdbtnSimulation.Size = new System.Drawing.Size(91, 23);
            this.rdbtnSimulation.TabIndex = 5;
            this.rdbtnSimulation.Text = "Simulation";
            this.rdbtnSimulation.UseVisualStyleBackColor = true;
            this.rdbtnSimulation.CheckedChanged += new System.EventHandler(this.rdbtnSimulation_CheckedChanged);
            // 
            // rdbtnRealTime
            // 
            this.rdbtnRealTime.AutoSize = true;
            this.rdbtnRealTime.BackColor = System.Drawing.Color.Lavender;
            this.rdbtnRealTime.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.rdbtnRealTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rdbtnRealTime.Location = new System.Drawing.Point(82, 50);
            this.rdbtnRealTime.Name = "rdbtnRealTime";
            this.rdbtnRealTime.Size = new System.Drawing.Size(85, 23);
            this.rdbtnRealTime.TabIndex = 4;
            this.rdbtnRealTime.Text = "Real Time";
            this.rdbtnRealTime.UseVisualStyleBackColor = false;
            this.rdbtnRealTime.CheckedChanged += new System.EventHandler(this.rdbtnRealTime_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(24, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(24, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mode :";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Lavender;
            this.groupBox2.Controls.Add(this.listViewAGVs);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox2.Location = new System.Drawing.Point(757, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(382, 151);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AGVs Monitoring";
            // 
            // listViewAGVs
            // 
            this.listViewAGVs.BackColor = System.Drawing.Color.White;
            this.listViewAGVs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AGVID,
            this.Status,
            this.ExitNode,
            this.Orient,
            this.DistanceToExitNode,
            this.Velocity});
            this.listViewAGVs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAGVs.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.listViewAGVs.Location = new System.Drawing.Point(3, 23);
            this.listViewAGVs.Name = "listViewAGVs";
            this.listViewAGVs.Size = new System.Drawing.Size(376, 125);
            this.listViewAGVs.SmallImageList = this.imgList;
            this.listViewAGVs.TabIndex = 0;
            this.listViewAGVs.UseCompatibleStateImageBehavior = false;
            this.listViewAGVs.View = System.Windows.Forms.View.Details;
            // 
            // AGVID
            // 
            this.AGVID.Text = "AGV ID";
            this.AGVID.Width = 68;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ExitNode
            // 
            this.ExitNode.Text = "ExitNode";
            this.ExitNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Orient
            // 
            this.Orient.Text = "Orient";
            this.Orient.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Orient.Width = 50;
            // 
            // DistanceToExitNode
            // 
            this.DistanceToExitNode.Text = "DistanceToExitNode";
            this.DistanceToExitNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DistanceToExitNode.Width = 70;
            // 
            // Velocity
            // 
            this.Velocity.Text = "Velocity";
            this.Velocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Lavender;
            this.groupBox3.Controls.Add(this.listViewTasks);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox3.Location = new System.Drawing.Point(757, 300);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(382, 215);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tasks Monitoring";
            // 
            // listViewTasks
            // 
            this.listViewTasks.BackColor = System.Drawing.Color.White;
            this.listViewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TaskName,
            this.TaskStatus,
            this.Type,
            this.TaskAGV,
            this.PickNode,
            this.DropNode,
            this.PalletCode});
            this.listViewTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTasks.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.listViewTasks.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listViewTasks.Location = new System.Drawing.Point(3, 23);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(376, 189);
            this.listViewTasks.SmallImageList = this.imgList;
            this.listViewTasks.TabIndex = 27;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.View = System.Windows.Forms.View.Details;
            // 
            // TaskName
            // 
            this.TaskName.Text = "Task Name";
            this.TaskName.Width = 80;
            // 
            // TaskStatus
            // 
            this.TaskStatus.Text = "Status";
            this.TaskStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TaskStatus.Width = 65;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TaskAGV
            // 
            this.TaskAGV.Text = "AGV";
            this.TaskAGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PickNode
            // 
            this.PickNode.Text = "Pick Node";
            this.PickNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PickNode.Width = 75;
            // 
            // DropNode
            // 
            this.DropNode.Text = "Drop Node";
            this.DropNode.Width = 75;
            // 
            // PalletCode
            // 
            this.PalletCode.Text = "Pallet Code";
            this.PalletCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PalletCode.Width = 80;
            // 
            // pnFloor
            // 
            this.pnFloor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnFloor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnFloor.BackgroundImage")));
            this.pnFloor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnFloor.ContextMenuStrip = this.cntxMenuStrip;
            this.pnFloor.Controls.Add(this.lbD);
            this.pnFloor.Controls.Add(this.lbC);
            this.pnFloor.Controls.Add(this.lbD6);
            this.pnFloor.Controls.Add(this.lbC6);
            this.pnFloor.Controls.Add(this.lbB6);
            this.pnFloor.Controls.Add(this.lbD5);
            this.pnFloor.Controls.Add(this.lbC5);
            this.pnFloor.Controls.Add(this.lbB5);
            this.pnFloor.Controls.Add(this.lbA6);
            this.pnFloor.Controls.Add(this.lbD3);
            this.pnFloor.Controls.Add(this.lbC3);
            this.pnFloor.Controls.Add(this.lbB3);
            this.pnFloor.Controls.Add(this.lbD4);
            this.pnFloor.Controls.Add(this.lbA5);
            this.pnFloor.Controls.Add(this.lbC4);
            this.pnFloor.Controls.Add(this.lbD2);
            this.pnFloor.Controls.Add(this.lbB4);
            this.pnFloor.Controls.Add(this.lbC2);
            this.pnFloor.Controls.Add(this.lbA3);
            this.pnFloor.Controls.Add(this.lbD1);
            this.pnFloor.Controls.Add(this.lbB2);
            this.pnFloor.Controls.Add(this.lbC1);
            this.pnFloor.Controls.Add(this.lbA4);
            this.pnFloor.Controls.Add(this.lbB1);
            this.pnFloor.Controls.Add(this.lbA2);
            this.pnFloor.Controls.Add(this.lbA1);
            this.pnFloor.Controls.Add(this.lbB);
            this.pnFloor.Controls.Add(this.lbA);
            this.pnFloor.Controls.Add(this.btnAddPallet2);
            this.pnFloor.Controls.Add(this.btnAddPallet1);
            this.pnFloor.Controls.Add(this.pictureBox1);
            this.pnFloor.Location = new System.Drawing.Point(15, 35);
            this.pnFloor.Name = "pnFloor";
            this.pnFloor.Size = new System.Drawing.Size(725, 645);
            this.pnFloor.TabIndex = 3;
            this.pnFloor.Paint += new System.Windows.Forms.PaintEventHandler(this.pnFloor_Paint);
            // 
            // cntxMenuStrip
            // 
            this.cntxMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPathToolStripMenuItem,
            this.hidePathToolStripMenuItem,
            this.showBlockColumnToolStripMenuItem});
            this.cntxMenuStrip.Name = "cntxMenuStrip";
            this.cntxMenuStrip.Size = new System.Drawing.Size(216, 70);
            this.cntxMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.cntxMenuStrip_Opening);
            // 
            // showPathToolStripMenuItem
            // 
            this.showPathToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showPathToolStripMenuItem.Image")));
            this.showPathToolStripMenuItem.Name = "showPathToolStripMenuItem";
            this.showPathToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.showPathToolStripMenuItem.Text = "Show current path";
            // 
            // hidePathToolStripMenuItem
            // 
            this.hidePathToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("hidePathToolStripMenuItem.Image")));
            this.hidePathToolStripMenuItem.Name = "hidePathToolStripMenuItem";
            this.hidePathToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.hidePathToolStripMenuItem.Text = "Hide current path";
            this.hidePathToolStripMenuItem.Click += new System.EventHandler(this.hidePathToolStripMenuItem_Click);
            // 
            // showBlockColumnToolStripMenuItem
            // 
            this.showBlockColumnToolStripMenuItem.Name = "showBlockColumnToolStripMenuItem";
            this.showBlockColumnToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.showBlockColumnToolStripMenuItem.Text = "Show block - column label";
            this.showBlockColumnToolStripMenuItem.Click += new System.EventHandler(this.showBlockColumnToolStripMenuItem_Click);
            // 
            // lbD
            // 
            this.lbD.AutoSize = true;
            this.lbD.BackColor = System.Drawing.Color.Transparent;
            this.lbD.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbD.ForeColor = System.Drawing.Color.Blue;
            this.lbD.Location = new System.Drawing.Point(398, 364);
            this.lbD.Name = "lbD";
            this.lbD.Size = new System.Drawing.Size(19, 19);
            this.lbD.TabIndex = 9;
            this.lbD.Text = "D";
            this.lbD.Visible = false;
            // 
            // lbC
            // 
            this.lbC.AutoSize = true;
            this.lbC.BackColor = System.Drawing.Color.Transparent;
            this.lbC.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbC.ForeColor = System.Drawing.Color.Blue;
            this.lbC.Location = new System.Drawing.Point(70, 364);
            this.lbC.Name = "lbC";
            this.lbC.Size = new System.Drawing.Size(18, 19);
            this.lbC.TabIndex = 9;
            this.lbC.Text = "C";
            this.lbC.Visible = false;
            // 
            // lbD6
            // 
            this.lbD6.AutoSize = true;
            this.lbD6.BackColor = System.Drawing.Color.Transparent;
            this.lbD6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbD6.ForeColor = System.Drawing.Color.Blue;
            this.lbD6.Location = new System.Drawing.Point(611, 413);
            this.lbD6.Name = "lbD6";
            this.lbD6.Size = new System.Drawing.Size(17, 19);
            this.lbD6.TabIndex = 9;
            this.lbD6.Text = "6";
            this.lbD6.Visible = false;
            // 
            // lbC6
            // 
            this.lbC6.AutoSize = true;
            this.lbC6.BackColor = System.Drawing.Color.Transparent;
            this.lbC6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbC6.ForeColor = System.Drawing.Color.Blue;
            this.lbC6.Location = new System.Drawing.Point(290, 413);
            this.lbC6.Name = "lbC6";
            this.lbC6.Size = new System.Drawing.Size(17, 19);
            this.lbC6.TabIndex = 9;
            this.lbC6.Text = "6";
            this.lbC6.Visible = false;
            // 
            // lbB6
            // 
            this.lbB6.AutoSize = true;
            this.lbB6.BackColor = System.Drawing.Color.Transparent;
            this.lbB6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbB6.ForeColor = System.Drawing.Color.Blue;
            this.lbB6.Location = new System.Drawing.Point(611, 190);
            this.lbB6.Name = "lbB6";
            this.lbB6.Size = new System.Drawing.Size(17, 19);
            this.lbB6.TabIndex = 9;
            this.lbB6.Text = "6";
            this.lbB6.Visible = false;
            // 
            // lbD5
            // 
            this.lbD5.AutoSize = true;
            this.lbD5.BackColor = System.Drawing.Color.Transparent;
            this.lbD5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbD5.ForeColor = System.Drawing.Color.Blue;
            this.lbD5.Location = new System.Drawing.Point(537, 413);
            this.lbD5.Name = "lbD5";
            this.lbD5.Size = new System.Drawing.Size(17, 19);
            this.lbD5.TabIndex = 9;
            this.lbD5.Text = "5";
            this.lbD5.Visible = false;
            // 
            // lbC5
            // 
            this.lbC5.AutoSize = true;
            this.lbC5.BackColor = System.Drawing.Color.Transparent;
            this.lbC5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbC5.ForeColor = System.Drawing.Color.Blue;
            this.lbC5.Location = new System.Drawing.Point(216, 413);
            this.lbC5.Name = "lbC5";
            this.lbC5.Size = new System.Drawing.Size(17, 19);
            this.lbC5.TabIndex = 9;
            this.lbC5.Text = "5";
            this.lbC5.Visible = false;
            // 
            // lbB5
            // 
            this.lbB5.AutoSize = true;
            this.lbB5.BackColor = System.Drawing.Color.Transparent;
            this.lbB5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbB5.ForeColor = System.Drawing.Color.Blue;
            this.lbB5.Location = new System.Drawing.Point(537, 190);
            this.lbB5.Name = "lbB5";
            this.lbB5.Size = new System.Drawing.Size(17, 19);
            this.lbB5.TabIndex = 9;
            this.lbB5.Text = "5";
            this.lbB5.Visible = false;
            // 
            // lbA6
            // 
            this.lbA6.AutoSize = true;
            this.lbA6.BackColor = System.Drawing.Color.Transparent;
            this.lbA6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbA6.ForeColor = System.Drawing.Color.Blue;
            this.lbA6.Location = new System.Drawing.Point(290, 190);
            this.lbA6.Name = "lbA6";
            this.lbA6.Size = new System.Drawing.Size(17, 19);
            this.lbA6.TabIndex = 9;
            this.lbA6.Text = "6";
            this.lbA6.Visible = false;
            // 
            // lbD3
            // 
            this.lbD3.AutoSize = true;
            this.lbD3.BackColor = System.Drawing.Color.Transparent;
            this.lbD3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbD3.ForeColor = System.Drawing.Color.Blue;
            this.lbD3.Location = new System.Drawing.Point(611, 318);
            this.lbD3.Name = "lbD3";
            this.lbD3.Size = new System.Drawing.Size(17, 19);
            this.lbD3.TabIndex = 9;
            this.lbD3.Text = "3";
            this.lbD3.Visible = false;
            // 
            // lbC3
            // 
            this.lbC3.AutoSize = true;
            this.lbC3.BackColor = System.Drawing.Color.Transparent;
            this.lbC3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbC3.ForeColor = System.Drawing.Color.Blue;
            this.lbC3.Location = new System.Drawing.Point(290, 318);
            this.lbC3.Name = "lbC3";
            this.lbC3.Size = new System.Drawing.Size(17, 19);
            this.lbC3.TabIndex = 9;
            this.lbC3.Text = "3";
            this.lbC3.Visible = false;
            // 
            // lbB3
            // 
            this.lbB3.AutoSize = true;
            this.lbB3.BackColor = System.Drawing.Color.Transparent;
            this.lbB3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbB3.ForeColor = System.Drawing.Color.Blue;
            this.lbB3.Location = new System.Drawing.Point(611, 95);
            this.lbB3.Name = "lbB3";
            this.lbB3.Size = new System.Drawing.Size(17, 19);
            this.lbB3.TabIndex = 9;
            this.lbB3.Text = "3";
            this.lbB3.Visible = false;
            // 
            // lbD4
            // 
            this.lbD4.AutoSize = true;
            this.lbD4.BackColor = System.Drawing.Color.Transparent;
            this.lbD4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbD4.ForeColor = System.Drawing.Color.Blue;
            this.lbD4.Location = new System.Drawing.Point(463, 413);
            this.lbD4.Name = "lbD4";
            this.lbD4.Size = new System.Drawing.Size(17, 19);
            this.lbD4.TabIndex = 9;
            this.lbD4.Text = "4";
            this.lbD4.Visible = false;
            // 
            // lbA5
            // 
            this.lbA5.AutoSize = true;
            this.lbA5.BackColor = System.Drawing.Color.Transparent;
            this.lbA5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbA5.ForeColor = System.Drawing.Color.Blue;
            this.lbA5.Location = new System.Drawing.Point(216, 190);
            this.lbA5.Name = "lbA5";
            this.lbA5.Size = new System.Drawing.Size(17, 19);
            this.lbA5.TabIndex = 9;
            this.lbA5.Text = "5";
            this.lbA5.Visible = false;
            // 
            // lbC4
            // 
            this.lbC4.AutoSize = true;
            this.lbC4.BackColor = System.Drawing.Color.Transparent;
            this.lbC4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbC4.ForeColor = System.Drawing.Color.Blue;
            this.lbC4.Location = new System.Drawing.Point(142, 413);
            this.lbC4.Name = "lbC4";
            this.lbC4.Size = new System.Drawing.Size(17, 19);
            this.lbC4.TabIndex = 9;
            this.lbC4.Text = "4";
            this.lbC4.Visible = false;
            // 
            // lbD2
            // 
            this.lbD2.AutoSize = true;
            this.lbD2.BackColor = System.Drawing.Color.Transparent;
            this.lbD2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbD2.ForeColor = System.Drawing.Color.Blue;
            this.lbD2.Location = new System.Drawing.Point(537, 318);
            this.lbD2.Name = "lbD2";
            this.lbD2.Size = new System.Drawing.Size(17, 19);
            this.lbD2.TabIndex = 9;
            this.lbD2.Text = "2";
            this.lbD2.Visible = false;
            // 
            // lbB4
            // 
            this.lbB4.AutoSize = true;
            this.lbB4.BackColor = System.Drawing.Color.Transparent;
            this.lbB4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbB4.ForeColor = System.Drawing.Color.Blue;
            this.lbB4.Location = new System.Drawing.Point(463, 190);
            this.lbB4.Name = "lbB4";
            this.lbB4.Size = new System.Drawing.Size(17, 19);
            this.lbB4.TabIndex = 9;
            this.lbB4.Text = "4";
            this.lbB4.Visible = false;
            // 
            // lbC2
            // 
            this.lbC2.AutoSize = true;
            this.lbC2.BackColor = System.Drawing.Color.Transparent;
            this.lbC2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbC2.ForeColor = System.Drawing.Color.Blue;
            this.lbC2.Location = new System.Drawing.Point(216, 318);
            this.lbC2.Name = "lbC2";
            this.lbC2.Size = new System.Drawing.Size(17, 19);
            this.lbC2.TabIndex = 9;
            this.lbC2.Text = "2";
            this.lbC2.Visible = false;
            // 
            // lbA3
            // 
            this.lbA3.AutoSize = true;
            this.lbA3.BackColor = System.Drawing.Color.Transparent;
            this.lbA3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbA3.ForeColor = System.Drawing.Color.Blue;
            this.lbA3.Location = new System.Drawing.Point(290, 95);
            this.lbA3.Name = "lbA3";
            this.lbA3.Size = new System.Drawing.Size(17, 19);
            this.lbA3.TabIndex = 9;
            this.lbA3.Text = "3";
            this.lbA3.Visible = false;
            // 
            // lbD1
            // 
            this.lbD1.AutoSize = true;
            this.lbD1.BackColor = System.Drawing.Color.Transparent;
            this.lbD1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbD1.ForeColor = System.Drawing.Color.Blue;
            this.lbD1.Location = new System.Drawing.Point(463, 318);
            this.lbD1.Name = "lbD1";
            this.lbD1.Size = new System.Drawing.Size(15, 19);
            this.lbD1.TabIndex = 9;
            this.lbD1.Text = "1";
            this.lbD1.Visible = false;
            // 
            // lbB2
            // 
            this.lbB2.AutoSize = true;
            this.lbB2.BackColor = System.Drawing.Color.Transparent;
            this.lbB2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbB2.ForeColor = System.Drawing.Color.Blue;
            this.lbB2.Location = new System.Drawing.Point(537, 95);
            this.lbB2.Name = "lbB2";
            this.lbB2.Size = new System.Drawing.Size(17, 19);
            this.lbB2.TabIndex = 9;
            this.lbB2.Text = "2";
            this.lbB2.Visible = false;
            // 
            // lbC1
            // 
            this.lbC1.AutoSize = true;
            this.lbC1.BackColor = System.Drawing.Color.Transparent;
            this.lbC1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbC1.ForeColor = System.Drawing.Color.Blue;
            this.lbC1.Location = new System.Drawing.Point(142, 318);
            this.lbC1.Name = "lbC1";
            this.lbC1.Size = new System.Drawing.Size(15, 19);
            this.lbC1.TabIndex = 9;
            this.lbC1.Text = "1";
            this.lbC1.Visible = false;
            // 
            // lbA4
            // 
            this.lbA4.AutoSize = true;
            this.lbA4.BackColor = System.Drawing.Color.Transparent;
            this.lbA4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbA4.ForeColor = System.Drawing.Color.Blue;
            this.lbA4.Location = new System.Drawing.Point(142, 190);
            this.lbA4.Name = "lbA4";
            this.lbA4.Size = new System.Drawing.Size(17, 19);
            this.lbA4.TabIndex = 9;
            this.lbA4.Text = "4";
            this.lbA4.Visible = false;
            // 
            // lbB1
            // 
            this.lbB1.AutoSize = true;
            this.lbB1.BackColor = System.Drawing.Color.Transparent;
            this.lbB1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbB1.ForeColor = System.Drawing.Color.Blue;
            this.lbB1.Location = new System.Drawing.Point(463, 95);
            this.lbB1.Name = "lbB1";
            this.lbB1.Size = new System.Drawing.Size(15, 19);
            this.lbB1.TabIndex = 9;
            this.lbB1.Text = "1";
            this.lbB1.Visible = false;
            // 
            // lbA2
            // 
            this.lbA2.AutoSize = true;
            this.lbA2.BackColor = System.Drawing.Color.Transparent;
            this.lbA2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbA2.ForeColor = System.Drawing.Color.Blue;
            this.lbA2.Location = new System.Drawing.Point(216, 95);
            this.lbA2.Name = "lbA2";
            this.lbA2.Size = new System.Drawing.Size(17, 19);
            this.lbA2.TabIndex = 9;
            this.lbA2.Text = "2";
            this.lbA2.Visible = false;
            // 
            // lbA1
            // 
            this.lbA1.AutoSize = true;
            this.lbA1.BackColor = System.Drawing.Color.Transparent;
            this.lbA1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbA1.ForeColor = System.Drawing.Color.Blue;
            this.lbA1.Location = new System.Drawing.Point(142, 95);
            this.lbA1.Name = "lbA1";
            this.lbA1.Size = new System.Drawing.Size(15, 19);
            this.lbA1.TabIndex = 9;
            this.lbA1.Text = "1";
            this.lbA1.Visible = false;
            // 
            // lbB
            // 
            this.lbB.AutoSize = true;
            this.lbB.BackColor = System.Drawing.Color.Transparent;
            this.lbB.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbB.ForeColor = System.Drawing.Color.Blue;
            this.lbB.Location = new System.Drawing.Point(398, 144);
            this.lbB.Name = "lbB";
            this.lbB.Size = new System.Drawing.Size(17, 19);
            this.lbB.TabIndex = 9;
            this.lbB.Text = "B";
            this.lbB.Visible = false;
            // 
            // lbA
            // 
            this.lbA.AutoSize = true;
            this.lbA.BackColor = System.Drawing.Color.Transparent;
            this.lbA.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbA.ForeColor = System.Drawing.Color.Blue;
            this.lbA.Location = new System.Drawing.Point(70, 144);
            this.lbA.Name = "lbA";
            this.lbA.Size = new System.Drawing.Size(18, 19);
            this.lbA.TabIndex = 9;
            this.lbA.Text = "A";
            this.lbA.Visible = false;
            // 
            // btnAddPallet2
            // 
            this.btnAddPallet2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAddPallet2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddPallet2.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnAddPallet2.FlatAppearance.BorderSize = 0;
            this.btnAddPallet2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPallet2.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnAddPallet2.ForeColor = System.Drawing.Color.Navy;
            this.btnAddPallet2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddPallet2.Location = new System.Drawing.Point(656, 600);
            this.btnAddPallet2.Name = "btnAddPallet2";
            this.btnAddPallet2.Size = new System.Drawing.Size(59, 33);
            this.btnAddPallet2.TabIndex = 9;
            this.btnAddPallet2.Text = "Add";
            this.btnAddPallet2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddPallet2.UseVisualStyleBackColor = false;
            this.btnAddPallet2.Click += new System.EventHandler(this.btnAddPallet2_Click);
            // 
            // btnAddPallet1
            // 
            this.btnAddPallet1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAddPallet1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddPallet1.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnAddPallet1.FlatAppearance.BorderSize = 0;
            this.btnAddPallet1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPallet1.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnAddPallet1.ForeColor = System.Drawing.Color.Navy;
            this.btnAddPallet1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAddPallet1.Location = new System.Drawing.Point(569, 600);
            this.btnAddPallet1.Name = "btnAddPallet1";
            this.btnAddPallet1.Size = new System.Drawing.Size(59, 33);
            this.btnAddPallet1.TabIndex = 9;
            this.btnAddPallet1.Text = "Add";
            this.btnAddPallet1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddPallet1.UseVisualStyleBackColor = false;
            this.btnAddPallet1.Click += new System.EventHandler(this.btnAddPallet1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(249, 585);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timerGUI
            // 
            this.timerGUI.Enabled = true;
            this.timerGUI.Tick += new System.EventHandler(this.timerGUI_Tick);
            // 
            // rtxtbComStatus
            // 
            this.rtxtbComStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtbComStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtxtbComStatus.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtxtbComStatus.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.rtxtbComStatus.Location = new System.Drawing.Point(760, 540);
            this.rtxtbComStatus.Name = "rtxtbComStatus";
            this.rtxtbComStatus.Size = new System.Drawing.Size(380, 137);
            this.rtxtbComStatus.TabIndex = 4;
            this.rtxtbComStatus.Text = "";
            this.rtxtbComStatus.TextChanged += new System.EventHandler(this.rtxtbComStatus_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(762, 518);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Communication Status:";
            // 
            // timerComStatus
            // 
            this.timerComStatus.Interval = 1;
            this.timerComStatus.Tick += new System.EventHandler(this.timerComStatus_Tick);
            // 
            // timerCheckConn
            // 
            this.timerCheckConn.Enabled = true;
            this.timerCheckConn.Interval = 1500;
            this.timerCheckConn.Tick += new System.EventHandler(this.timerCheckConn_Tick);
            // 
            // HomeScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(1159, 689);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rtxtbComStatus);
            this.Controls.Add(this.pnFloor);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mnstrHomeScr);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnstrHomeScr;
            this.Name = "HomeScreenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGVs Control & Monitoring Software";
            this.Load += new System.EventHandler(this.HomeScreenForm_Load);
            this.mnstrHomeScr.ResumeLayout(false);
            this.mnstrHomeScr.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.pnFloor.ResumeLayout(false);
            this.pnFloor.PerformLayout();
            this.cntxMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnstrHomeScr;
        private System.Windows.Forms.ToolStripMenuItem communicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRemoveTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taskManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aGVsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRemoveAGVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agvMonitoringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warehouseDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operationManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerGUI;
        private System.Windows.Forms.RadioButton rdbtnSimulation;
        private System.Windows.Forms.RadioButton rdbtnRealTime;
        private System.Windows.Forms.Panel pnFloor;
        private System.Windows.Forms.ListView listViewAGVs;
        private System.Windows.Forms.ColumnHeader AGVID;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader ExitNode;
        private System.Windows.Forms.ColumnHeader Orient;
        private System.Windows.Forms.ColumnHeader DistanceToExitNode;
        private System.Windows.Forms.ColumnHeader Velocity;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.ColumnHeader TaskName;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader PalletCode;
        private System.Windows.Forms.ColumnHeader PickNode;
        private System.Windows.Forms.ColumnHeader DropNode;
        private System.Windows.Forms.ColumnHeader TaskStatus;
        private System.Windows.Forms.ColumnHeader TaskAGV;
        private System.Windows.Forms.ContextMenuStrip cntxMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hidePathToolStripMenuItem;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Button btnPauseRun;
        private System.Windows.Forms.Label lbModeStatus;
        private System.Windows.Forms.RichTextBox rtxtbComStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerComStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAddPallet2;
        private System.Windows.Forms.Button btnAddPallet1;
        private System.Windows.Forms.ToolStripMenuItem orderToolStripMenuItem;
        private System.Windows.Forms.Label lbD;
        private System.Windows.Forms.Label lbC;
        private System.Windows.Forms.Label lbD6;
        private System.Windows.Forms.Label lbC6;
        private System.Windows.Forms.Label lbB6;
        private System.Windows.Forms.Label lbD5;
        private System.Windows.Forms.Label lbC5;
        private System.Windows.Forms.Label lbB5;
        private System.Windows.Forms.Label lbA6;
        private System.Windows.Forms.Label lbD3;
        private System.Windows.Forms.Label lbC3;
        private System.Windows.Forms.Label lbB3;
        private System.Windows.Forms.Label lbD4;
        private System.Windows.Forms.Label lbA5;
        private System.Windows.Forms.Label lbC4;
        private System.Windows.Forms.Label lbD2;
        private System.Windows.Forms.Label lbB4;
        private System.Windows.Forms.Label lbC2;
        private System.Windows.Forms.Label lbA3;
        private System.Windows.Forms.Label lbD1;
        private System.Windows.Forms.Label lbB2;
        private System.Windows.Forms.Label lbC1;
        private System.Windows.Forms.Label lbA4;
        private System.Windows.Forms.Label lbB1;
        private System.Windows.Forms.Label lbA2;
        private System.Windows.Forms.Label lbA1;
        private System.Windows.Forms.Label lbB;
        private System.Windows.Forms.Label lbA;
        private System.Windows.Forms.ToolStripMenuItem showBlockColumnToolStripMenuItem;
        private System.Windows.Forms.Timer timerCheckConn;
    }
}

