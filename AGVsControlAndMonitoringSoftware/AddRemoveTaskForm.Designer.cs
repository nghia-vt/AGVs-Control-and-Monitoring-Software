namespace AGVsControlAndMonitoringSoftware
{
    partial class AddRemoveTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRemoveTaskForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txbPalletCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbbDropNode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbPickNode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbAGV = new System.Windows.Forms.ComboBox();
            this.cbbType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbTaskName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewTask = new System.Windows.Forms.ListView();
            this.TaskName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PalletCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AGVID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PickNode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DropNode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbbTaskName = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Lavender;
            this.groupBox1.Controls.Add(this.checkBox);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txbPalletCode);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbbDropNode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbbPickNode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbbAGV);
            this.groupBox1.Controls.Add(this.cbbType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txbTaskName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 161);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "     Add";
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.checkBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkBox.Location = new System.Drawing.Point(109, 116);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(106, 17);
            this.checkBox.TabIndex = 31;
            this.checkBox.Text = "Pick pallet code";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.Visible = false;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnAdd.ForeColor = System.Drawing.Color.DarkRed;
            this.btnAdd.Location = new System.Drawing.Point(287, 125);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(71, 27);
            this.btnAdd.TabIndex = 30;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txbPalletCode
            // 
            this.txbPalletCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txbPalletCode.Location = new System.Drawing.Point(109, 85);
            this.txbPalletCode.Name = "txbPalletCode";
            this.txbPalletCode.Size = new System.Drawing.Size(72, 25);
            this.txbPalletCode.TabIndex = 24;
            this.txbPalletCode.TextChanged += new System.EventHandler(this.txbPalletCode_TextChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(15, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 19);
            this.label8.TabIndex = 23;
            this.label8.Text = "Pallet Code:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbDropNode
            // 
            this.cbbDropNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDropNode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbbDropNode.FormattingEnabled = true;
            this.cbbDropNode.IntegralHeight = false;
            this.cbbDropNode.ItemHeight = 17;
            this.cbbDropNode.Items.AddRange(new object[] {
            "9-1",
            "9-2",
            "9-3",
            "10-1",
            "10-2",
            "10-3",
            "11-1",
            "11-2",
            "11-3",
            "12-1",
            "12-2",
            "12-3",
            "13-1",
            "13-2",
            "13-3",
            "14-1",
            "14-2",
            "14-3",
            "15-1",
            "15-2",
            "15-3",
            "16-1",
            "16-2",
            "16-3",
            "17-1",
            "17-2",
            "17-3",
            "18-1",
            "18-2",
            "18-3",
            "19-1",
            "19-2",
            "19-3",
            "20-1",
            "20-2",
            "20-3",
            "30-1",
            "30-2",
            "30-3",
            "31-1",
            "31-2",
            "31-3",
            "32-1",
            "32-2",
            "32-3",
            "33-1",
            "33-2",
            "33-3",
            "34-1",
            "34-2",
            "34-3",
            "35-1",
            "35-2",
            "35-3",
            "36-1",
            "36-2",
            "36-3",
            "37-1",
            "37-2",
            "37-3",
            "38-1",
            "38-2",
            "38-3",
            "39-1",
            "39-2",
            "39-3",
            "40-1",
            "40-2",
            "40-3",
            "41-1",
            "41-2",
            "41-3",
            "51-1",
            "52-1",
            "53-1",
            "54-1"});
            this.cbbDropNode.Location = new System.Drawing.Point(287, 85);
            this.cbbDropNode.MaxDropDownItems = 6;
            this.cbbDropNode.Name = "cbbDropNode";
            this.cbbDropNode.Size = new System.Drawing.Size(70, 25);
            this.cbbDropNode.TabIndex = 20;
            this.myToolTip.SetToolTip(this.cbbDropNode, "[drop node] - [level of rack at this node]");
            this.cbbDropNode.DropDown += new System.EventHandler(this.cbbDropNode_DropDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(15, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 19);
            this.label5.TabIndex = 17;
            this.label5.Text = "Task Name:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(204, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 19);
            this.label6.TabIndex = 19;
            this.label6.Text = "Drop Node:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbPickNode
            // 
            this.cbbPickNode.BackColor = System.Drawing.SystemColors.Window;
            this.cbbPickNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPickNode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbbPickNode.FormattingEnabled = true;
            this.cbbPickNode.IntegralHeight = false;
            this.cbbPickNode.ItemHeight = 17;
            this.cbbPickNode.Location = new System.Drawing.Point(287, 55);
            this.cbbPickNode.MaxDropDownItems = 6;
            this.cbbPickNode.Name = "cbbPickNode";
            this.cbbPickNode.Size = new System.Drawing.Size(70, 25);
            this.cbbPickNode.TabIndex = 18;
            this.myToolTip.SetToolTip(this.cbbPickNode, "[pick node] - [level of rack at this node]");
            this.cbbPickNode.DropDown += new System.EventHandler(this.cbbPickNode_DropDown);
            this.cbbPickNode.DropDownClosed += new System.EventHandler(this.cbbPickNode_DropDownClosed);
            this.cbbPickNode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbbPickNode_KeyDown);
            this.cbbPickNode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbPickNode_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(51, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "AGV";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbAGV
            // 
            this.cbbAGV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAGV.DropDownWidth = 70;
            this.cbbAGV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbbAGV.FormattingEnabled = true;
            this.cbbAGV.ItemHeight = 17;
            this.cbbAGV.Location = new System.Drawing.Point(287, 25);
            this.cbbAGV.MaxDropDownItems = 4;
            this.cbbAGV.Name = "cbbAGV";
            this.cbbAGV.Size = new System.Drawing.Size(70, 25);
            this.cbbAGV.TabIndex = 14;
            this.cbbAGV.DropDown += new System.EventHandler(this.cbbAGV_DropDown);
            // 
            // cbbType
            // 
            this.cbbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbbType.FormattingEnabled = true;
            this.cbbType.Items.AddRange(new object[] {
            "Input",
            "Output"});
            this.cbbType.Location = new System.Drawing.Point(109, 55);
            this.cbbType.MaxDropDownItems = 4;
            this.cbbType.Name = "cbbType";
            this.cbbType.Size = new System.Drawing.Size(72, 25);
            this.cbbType.TabIndex = 15;
            this.cbbType.SelectedIndexChanged += new System.EventHandler(this.cbbType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(204, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Pick Node:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbTaskName
            // 
            this.txbTaskName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txbTaskName.Location = new System.Drawing.Point(109, 25);
            this.txbTaskName.Name = "txbTaskName";
            this.txbTaskName.Size = new System.Drawing.Size(72, 25);
            this.txbTaskName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(204, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "AGV:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(15, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Type:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewTask
            // 
            this.listViewTask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TaskName,
            this.Type,
            this.PalletCode,
            this.AGVID,
            this.PickNode,
            this.DropNode});
            this.listViewTask.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.listViewTask.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listViewTask.Location = new System.Drawing.Point(12, 196);
            this.listViewTask.Name = "listViewTask";
            this.listViewTask.Size = new System.Drawing.Size(529, 144);
            this.listViewTask.SmallImageList = this.imageList;
            this.listViewTask.TabIndex = 26;
            this.listViewTask.UseCompatibleStateImageBehavior = false;
            this.listViewTask.View = System.Windows.Forms.View.Details;
            // 
            // TaskName
            // 
            this.TaskName.Text = "Task Name";
            this.TaskName.Width = 95;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Type.Width = 65;
            // 
            // PalletCode
            // 
            this.PalletCode.Text = "Pallet Code";
            this.PalletCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PalletCode.Width = 100;
            // 
            // AGVID
            // 
            this.AGVID.Text = "AGV";
            this.AGVID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AGVID.Width = 65;
            // 
            // PickNode
            // 
            this.PickNode.Text = "Pick Node";
            this.PickNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PickNode.Width = 100;
            // 
            // DropNode
            // 
            this.DropNode.Text = "Drop Node";
            this.DropNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DropNode.Width = 100;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "icon_task.png");
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Lavender;
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbbTaskName);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox2.Location = new System.Drawing.Point(386, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 161);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "     Remove";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(9, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(17, 17);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnRemove.ForeColor = System.Drawing.Color.DarkRed;
            this.btnRemove.Location = new System.Drawing.Point(72, 125);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(77, 27);
            this.btnRemove.TabIndex = 31;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label9.ForeColor = System.Drawing.Color.Navy;
            this.label9.Location = new System.Drawing.Point(20, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 19);
            this.label9.TabIndex = 25;
            this.label9.Text = "Task Name:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbTaskName
            // 
            this.cbbTaskName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTaskName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbbTaskName.FormattingEnabled = true;
            this.cbbTaskName.Location = new System.Drawing.Point(24, 57);
            this.cbbTaskName.MaxDropDownItems = 4;
            this.cbbTaskName.Name = "cbbTaskName";
            this.cbbTaskName.Size = new System.Drawing.Size(72, 25);
            this.cbbTaskName.TabIndex = 17;
            this.cbbTaskName.DropDown += new System.EventHandler(this.cbbTaskName_DropDown);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Segoe UI Semibold", 10.25F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnOK.Location = new System.Drawing.Point(390, 348);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 27);
            this.btnOK.TabIndex = 28;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnCancel.Location = new System.Drawing.Point(458, 348);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 27);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label10.ForeColor = System.Drawing.Color.Navy;
            this.label10.Location = new System.Drawing.Point(15, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 19);
            this.label10.TabIndex = 30;
            this.label10.Text = "List of Tasks:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myToolTip
            // 
            this.myToolTip.AutoPopDelay = 5000;
            this.myToolTip.InitialDelay = 100;
            this.myToolTip.ReshowDelay = 100;
            // 
            // AddRemoveTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(553, 384);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listViewTask);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddRemoveTaskForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add/Remove Task";
            this.Load += new System.EventHandler(this.AddRemoveTaskForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbType;
        private System.Windows.Forms.TextBox txbTaskName;
        private System.Windows.Forms.ComboBox cbbAGV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbDropNode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbbPickNode;
        private System.Windows.Forms.TextBox txbPalletCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView listViewTask;
        private System.Windows.Forms.ColumnHeader TaskName;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader PalletCode;
        private System.Windows.Forms.ColumnHeader AGVID;
        private System.Windows.Forms.ColumnHeader PickNode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbTaskName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ColumnHeader DropNode;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolTip myToolTip;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}