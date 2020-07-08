namespace AGVsControlAndMonitoringSoftware
{
    partial class AGVMonitoringForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AGVMonitoringForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbDistance = new System.Windows.Forms.Label();
            this.lbOrient = new System.Windows.Forms.Label();
            this.lbExitNode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbAGV = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtbSetVelocity = new System.Windows.Forms.TextBox();
            this.btnSetVelocity = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lbVelocity = new System.Windows.Forms.Label();
            this.zedGraphVelocity = new ZedGraph.ZedGraphControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbBattery = new System.Windows.Forms.Label();
            this.prgrbBattery = new System.Windows.Forms.ProgressBar();
            this.label11 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.zedGraphLineTrack = new ZedGraph.ZedGraphControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerGraph = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.rtxtbCurrentPath = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.lbDistance);
            this.groupBox1.Controls.Add(this.lbOrient);
            this.groupBox1.Controls.Add(this.lbExitNode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox1.Location = new System.Drawing.Point(16, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 110);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "     Position";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(11, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 18);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // lbDistance
            // 
            this.lbDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbDistance.AutoSize = true;
            this.lbDistance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDistance.ForeColor = System.Drawing.Color.Navy;
            this.lbDistance.Location = new System.Drawing.Point(168, 81);
            this.lbDistance.Name = "lbDistance";
            this.lbDistance.Size = new System.Drawing.Size(35, 17);
            this.lbDistance.TabIndex = 11;
            this.lbDistance.Text = "##.#";
            this.lbDistance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbOrient
            // 
            this.lbOrient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbOrient.AutoSize = true;
            this.lbOrient.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOrient.ForeColor = System.Drawing.Color.Navy;
            this.lbOrient.Location = new System.Drawing.Point(168, 56);
            this.lbOrient.Name = "lbOrient";
            this.lbOrient.Size = new System.Drawing.Size(16, 17);
            this.lbOrient.TabIndex = 11;
            this.lbOrient.Text = "#";
            this.lbOrient.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbExitNode
            // 
            this.lbExitNode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbExitNode.AutoSize = true;
            this.lbExitNode.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExitNode.ForeColor = System.Drawing.Color.Navy;
            this.lbExitNode.Location = new System.Drawing.Point(168, 31);
            this.lbExitNode.Name = "lbExitNode";
            this.lbExitNode.Size = new System.Drawing.Size(24, 17);
            this.lbExitNode.TabIndex = 11;
            this.lbExitNode.Text = "##";
            this.lbExitNode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "Initial Exit Node:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(19, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Distance to Exit Node:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(19, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Initial Orientation:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Select AGV to monitor:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbAGV
            // 
            this.cbbAGV.BackColor = System.Drawing.Color.White;
            this.cbbAGV.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.cbbAGV.FormattingEnabled = true;
            this.cbbAGV.IntegralHeight = false;
            this.cbbAGV.ItemHeight = 17;
            this.cbbAGV.Items.AddRange(new object[] {
            "AGV#1",
            "AGV#2"});
            this.cbbAGV.Location = new System.Drawing.Point(198, 7);
            this.cbbAGV.MaxDropDownItems = 6;
            this.cbbAGV.Name = "cbbAGV";
            this.cbbAGV.Size = new System.Drawing.Size(65, 25);
            this.cbbAGV.TabIndex = 26;
            this.cbbAGV.DropDown += new System.EventHandler(this.cbbAGV_DropDown);
            this.cbbAGV.SelectedIndexChanged += new System.EventHandler(this.cbbAGV_SelectedIndexChanged);
            this.cbbAGV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbbAGV_KeyDown);
            this.cbbAGV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbAGV_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtbSetVelocity);
            this.groupBox2.Controls.Add(this.btnSetVelocity);
            this.groupBox2.Controls.Add(this.pictureBox4);
            this.groupBox2.Controls.Add(this.lbVelocity);
            this.groupBox2.Controls.Add(this.zedGraphVelocity);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox2.Location = new System.Drawing.Point(443, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 418);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "      Velocity";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(232, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 19);
            this.label6.TabIndex = 29;
            this.label6.Text = "Set for all AGV:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtbSetVelocity
            // 
            this.txtbSetVelocity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtbSetVelocity.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.txtbSetVelocity.Location = new System.Drawing.Point(337, 17);
            this.txtbSetVelocity.Name = "txtbSetVelocity";
            this.txtbSetVelocity.Size = new System.Drawing.Size(65, 25);
            this.txtbSetVelocity.TabIndex = 32;
            this.txtbSetVelocity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbSetVelocity_KeyPress);
            // 
            // btnSetVelocity
            // 
            this.btnSetVelocity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetVelocity.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnSetVelocity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSetVelocity.FlatAppearance.BorderColor = System.Drawing.Color.Lavender;
            this.btnSetVelocity.FlatAppearance.BorderSize = 0;
            this.btnSetVelocity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetVelocity.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.btnSetVelocity.ForeColor = System.Drawing.Color.Navy;
            this.btnSetVelocity.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSetVelocity.ImageIndex = 3;
            this.btnSetVelocity.Location = new System.Drawing.Point(406, 17);
            this.btnSetVelocity.Name = "btnSetVelocity";
            this.btnSetVelocity.Size = new System.Drawing.Size(54, 25);
            this.btnSetVelocity.TabIndex = 31;
            this.btnSetVelocity.Text = "Set";
            this.btnSetVelocity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSetVelocity.UseVisualStyleBackColor = false;
            this.btnSetVelocity.Click += new System.EventHandler(this.btnSetVelocity_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(11, 1);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(18, 18);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 28;
            this.pictureBox4.TabStop = false;
            // 
            // lbVelocity
            // 
            this.lbVelocity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbVelocity.AutoSize = true;
            this.lbVelocity.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVelocity.ForeColor = System.Drawing.Color.Navy;
            this.lbVelocity.Location = new System.Drawing.Point(35, 27);
            this.lbVelocity.Name = "lbVelocity";
            this.lbVelocity.Size = new System.Drawing.Size(35, 17);
            this.lbVelocity.TabIndex = 11;
            this.lbVelocity.Text = "##.#";
            this.lbVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // zedGraphVelocity
            // 
            this.zedGraphVelocity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphVelocity.Location = new System.Drawing.Point(16, 49);
            this.zedGraphVelocity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.zedGraphVelocity.Name = "zedGraphVelocity";
            this.zedGraphVelocity.ScrollGrace = 0D;
            this.zedGraphVelocity.ScrollMaxX = 0D;
            this.zedGraphVelocity.ScrollMaxY = 0D;
            this.zedGraphVelocity.ScrollMaxY2 = 0D;
            this.zedGraphVelocity.ScrollMinX = 0D;
            this.zedGraphVelocity.ScrollMinY = 0D;
            this.zedGraphVelocity.ScrollMinY2 = 0D;
            this.zedGraphVelocity.Size = new System.Drawing.Size(460, 358);
            this.zedGraphVelocity.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Controls.Add(this.lbBattery);
            this.groupBox3.Controls.Add(this.prgrbBattery);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox3.Location = new System.Drawing.Point(271, 46);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(161, 64);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "     Battery";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(10, 1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(18, 18);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 28;
            this.pictureBox3.TabStop = false;
            // 
            // lbBattery
            // 
            this.lbBattery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbBattery.AutoSize = true;
            this.lbBattery.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBattery.ForeColor = System.Drawing.Color.Navy;
            this.lbBattery.Location = new System.Drawing.Point(20, 33);
            this.lbBattery.Name = "lbBattery";
            this.lbBattery.Size = new System.Drawing.Size(24, 17);
            this.lbBattery.TabIndex = 18;
            this.lbBattery.Text = "##";
            this.lbBattery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prgrbBattery
            // 
            this.prgrbBattery.Location = new System.Drawing.Point(55, 30);
            this.prgrbBattery.Name = "prgrbBattery";
            this.prgrbBattery.Size = new System.Drawing.Size(100, 23);
            this.prgrbBattery.Step = 1;
            this.prgrbBattery.TabIndex = 0;
            this.prgrbBattery.Value = 50;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label11.ForeColor = System.Drawing.Color.Navy;
            this.label11.Location = new System.Drawing.Point(809, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 19);
            this.label11.TabIndex = 27;
            this.label11.Text = "Status:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.ForeColor = System.Drawing.Color.Navy;
            this.lbStatus.Location = new System.Drawing.Point(861, 11);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(48, 17);
            this.lbStatus.TabIndex = 11;
            this.lbStatus.Text = "#####";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox4.Controls.Add(this.pictureBox5);
            this.groupBox4.Controls.Add(this.zedGraphLineTrack);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox4.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox4.Location = new System.Drawing.Point(16, 166);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(416, 297);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "     Line tracking";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(11, 2);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(16, 16);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 28;
            this.pictureBox5.TabStop = false;
            // 
            // zedGraphLineTrack
            // 
            this.zedGraphLineTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.zedGraphLineTrack.Location = new System.Drawing.Point(9, 28);
            this.zedGraphLineTrack.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphLineTrack.Name = "zedGraphLineTrack";
            this.zedGraphLineTrack.ScrollGrace = 0D;
            this.zedGraphLineTrack.ScrollMaxX = 0D;
            this.zedGraphLineTrack.ScrollMaxY = 0D;
            this.zedGraphLineTrack.ScrollMaxY2 = 0D;
            this.zedGraphLineTrack.ScrollMinX = 0D;
            this.zedGraphLineTrack.ScrollMinY = 0D;
            this.zedGraphLineTrack.ScrollMinY2 = 0D;
            this.zedGraphLineTrack.Size = new System.Drawing.Size(398, 258);
            this.zedGraphLineTrack.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(170, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // timerGraph
            // 
            this.timerGraph.Enabled = true;
            this.timerGraph.Tick += new System.EventHandler(this.timerGraph_Tick);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(280, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 19);
            this.label5.TabIndex = 29;
            this.label5.Text = "Current path: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtxtbCurrentPath
            // 
            this.rtxtbCurrentPath.BackColor = System.Drawing.Color.Lavender;
            this.rtxtbCurrentPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtbCurrentPath.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtbCurrentPath.ForeColor = System.Drawing.Color.Navy;
            this.rtxtbCurrentPath.Location = new System.Drawing.Point(375, 10);
            this.rtxtbCurrentPath.Name = "rtxtbCurrentPath";
            this.rtxtbCurrentPath.ReadOnly = true;
            this.rtxtbCurrentPath.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.rtxtbCurrentPath.Size = new System.Drawing.Size(400, 25);
            this.rtxtbCurrentPath.TabIndex = 30;
            this.rtxtbCurrentPath.Text = "";
            // 
            // AGVMonitoringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(934, 475);
            this.Controls.Add(this.rtxtbCurrentPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.cbbAGV);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AGVMonitoringForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGV Monitoring";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AGVMonitoringForm_FormClosed);
            this.Load += new System.EventHandler(this.AGVMonitoringForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbAGV;
        private System.Windows.Forms.Label lbDistance;
        private System.Windows.Forms.Label lbOrient;
        private System.Windows.Forms.Label lbExitNode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbBattery;
        private System.Windows.Forms.ProgressBar prgrbBattery;
        private System.Windows.Forms.Label label11;
        private ZedGraph.ZedGraphControl zedGraphVelocity;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbVelocity;
        private System.Windows.Forms.GroupBox groupBox4;
        private ZedGraph.ZedGraphControl zedGraphLineTrack;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Timer timerGraph;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtxtbCurrentPath;
        private System.Windows.Forms.TextBox txtbSetVelocity;
        private System.Windows.Forms.Button btnSetVelocity;
        private System.Windows.Forms.Label label6;
    }
}