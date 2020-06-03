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
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRemoveTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aGVsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRemoveAGVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aGVManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.Priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PalletCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnFloor = new System.Windows.Forms.Panel();
            this.cntxMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hidePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbModeStatus = new System.Windows.Forms.Label();
            this.mnstrHomeScr.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cntxMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnstrHomeScr
            // 
            this.mnstrHomeScr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mnstrHomeScr.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.mnstrHomeScr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.communicationToolStripMenuItem,
            this.taskToolStripMenuItem,
            this.aGVsToolStripMenuItem,
            this.ordersToolStripMenuItem,
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
            // 
            // aGVsToolStripMenuItem
            // 
            this.aGVsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRemoveAGVToolStripMenuItem,
            this.aGVManagementToolStripMenuItem});
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
            // aGVManagementToolStripMenuItem
            // 
            this.aGVManagementToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aGVManagementToolStripMenuItem.Image")));
            this.aGVManagementToolStripMenuItem.Name = "aGVManagementToolStripMenuItem";
            this.aGVManagementToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.aGVManagementToolStripMenuItem.Text = "&Monitoring";
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ordersToolStripMenuItem.Image")));
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(139, 23);
            this.ordersToolStripMenuItem.Text = "&Warehouse Data";
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportToolStripMenuItem.Image")));
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(78, 23);
            this.reportToolStripMenuItem.Text = "&Report";
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
            this.groupBox2.Size = new System.Drawing.Size(382, 187);
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
            this.listViewAGVs.Size = new System.Drawing.Size(376, 161);
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
            this.groupBox3.Location = new System.Drawing.Point(757, 336);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(382, 231);
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
            this.Priority,
            this.PalletCode});
            this.listViewTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTasks.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.listViewTasks.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listViewTasks.Location = new System.Drawing.Point(3, 23);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(376, 205);
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
            // Priority
            // 
            this.Priority.Text = "Priority";
            this.Priority.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Priority.Width = 65;
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
            this.hidePathToolStripMenuItem});
            this.cntxMenuStrip.Name = "cntxMenuStrip";
            this.cntxMenuStrip.Size = new System.Drawing.Size(172, 48);
            this.cntxMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.cntxMenuStrip_Opening);
            // 
            // showPathToolStripMenuItem
            // 
            this.showPathToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showPathToolStripMenuItem.Image")));
            this.showPathToolStripMenuItem.Name = "showPathToolStripMenuItem";
            this.showPathToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.showPathToolStripMenuItem.Text = "Show current path";
            // 
            // hidePathToolStripMenuItem
            // 
            this.hidePathToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("hidePathToolStripMenuItem.Image")));
            this.hidePathToolStripMenuItem.Name = "hidePathToolStripMenuItem";
            this.hidePathToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.hidePathToolStripMenuItem.Text = "Hide current path";
            this.hidePathToolStripMenuItem.Click += new System.EventHandler(this.hidePathToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            // HomeScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(1159, 689);
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
            this.cntxMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem aGVManagementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operationManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
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
        private System.Windows.Forms.ColumnHeader Priority;
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
    }
}

