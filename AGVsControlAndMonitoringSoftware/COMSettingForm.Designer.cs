namespace AGVsControlAndMonitoringSoftware
{
    partial class COMSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(COMSettingForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbCOMPort = new System.Windows.Forms.ComboBox();
            this.cbbBaudRate = new System.Windows.Forms.ComboBox();
            this.cbbDataBits = new System.Windows.Forms.ComboBox();
            this.cbbStopBits = new System.Windows.Forms.ComboBox();
            this.cbbParity = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.myImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnReScan = new System.Windows.Forms.Button();
            this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(26, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "COM Port:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(26, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Baud Rate:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(26, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Data Bits:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(26, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Stop Bits:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(26, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "Parity:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbCOMPort
            // 
            this.cbbCOMPort.BackColor = System.Drawing.SystemColors.Window;
            this.cbbCOMPort.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbCOMPort.FormattingEnabled = true;
            this.cbbCOMPort.IntegralHeight = false;
            this.cbbCOMPort.ItemHeight = 17;
            this.cbbCOMPort.Location = new System.Drawing.Point(121, 27);
            this.cbbCOMPort.MaxDropDownItems = 6;
            this.cbbCOMPort.Name = "cbbCOMPort";
            this.cbbCOMPort.Size = new System.Drawing.Size(70, 25);
            this.cbbCOMPort.TabIndex = 15;
            // 
            // cbbBaudRate
            // 
            this.cbbBaudRate.BackColor = System.Drawing.SystemColors.Window;
            this.cbbBaudRate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbBaudRate.FormattingEnabled = true;
            this.cbbBaudRate.IntegralHeight = false;
            this.cbbBaudRate.ItemHeight = 17;
            this.cbbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "256000"});
            this.cbbBaudRate.Location = new System.Drawing.Point(121, 57);
            this.cbbBaudRate.MaxDropDownItems = 6;
            this.cbbBaudRate.Name = "cbbBaudRate";
            this.cbbBaudRate.Size = new System.Drawing.Size(70, 25);
            this.cbbBaudRate.TabIndex = 15;
            this.cbbBaudRate.Text = "115200";
            // 
            // cbbDataBits
            // 
            this.cbbDataBits.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbDataBits.FormattingEnabled = true;
            this.cbbDataBits.IntegralHeight = false;
            this.cbbDataBits.ItemHeight = 17;
            this.cbbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbbDataBits.Location = new System.Drawing.Point(121, 87);
            this.cbbDataBits.MaxDropDownItems = 6;
            this.cbbDataBits.Name = "cbbDataBits";
            this.cbbDataBits.Size = new System.Drawing.Size(70, 25);
            this.cbbDataBits.TabIndex = 15;
            this.cbbDataBits.Text = "8";
            // 
            // cbbStopBits
            // 
            this.cbbStopBits.DropDownWidth = 85;
            this.cbbStopBits.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbStopBits.FormattingEnabled = true;
            this.cbbStopBits.IntegralHeight = false;
            this.cbbStopBits.ItemHeight = 17;
            this.cbbStopBits.Items.AddRange(new object[] {
            "None",
            "One",
            "Two",
            "OnePointFive"});
            this.cbbStopBits.Location = new System.Drawing.Point(121, 117);
            this.cbbStopBits.MaxDropDownItems = 6;
            this.cbbStopBits.Name = "cbbStopBits";
            this.cbbStopBits.Size = new System.Drawing.Size(70, 25);
            this.cbbStopBits.TabIndex = 15;
            this.cbbStopBits.Text = "One";
            // 
            // cbbParity
            // 
            this.cbbParity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbParity.FormattingEnabled = true;
            this.cbbParity.IntegralHeight = false;
            this.cbbParity.ItemHeight = 17;
            this.cbbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cbbParity.Location = new System.Drawing.Point(121, 147);
            this.cbbParity.MaxDropDownItems = 6;
            this.cbbParity.Name = "cbbParity";
            this.cbbParity.Size = new System.Drawing.Size(70, 25);
            this.cbbParity.TabIndex = 15;
            this.cbbParity.Text = "None";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Lavender;
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.btnReScan);
            this.groupBox1.Controls.Add(this.cbbBaudRate);
            this.groupBox1.Controls.Add(this.cbbParity);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbbStopBits);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbbDataBits);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbCOMPort);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 219);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnConnect.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnConnect.ImageIndex = 0;
            this.btnConnect.ImageList = this.myImageList;
            this.btnConnect.Location = new System.Drawing.Point(6, 186);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(108, 27);
            this.btnConnect.TabIndex = 17;
            this.btnConnect.Text = "Connect";
            this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.myToolTip.SetToolTip(this.btnConnect, "Dis/Connect to Port");
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // myImageList
            // 
            this.myImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("myImageList.ImageStream")));
            this.myImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.myImageList.Images.SetKeyName(0, "icon_connect.png");
            this.myImageList.Images.SetKeyName(1, "icon_disconnect.png");
            // 
            // btnReScan
            // 
            this.btnReScan.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.btnReScan.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnReScan.Image = ((System.Drawing.Image)(resources.GetObject("btnReScan.Image")));
            this.btnReScan.Location = new System.Drawing.Point(129, 186);
            this.btnReScan.Name = "btnReScan";
            this.btnReScan.Size = new System.Drawing.Size(80, 27);
            this.btnReScan.TabIndex = 16;
            this.btnReScan.Text = "ReScan";
            this.btnReScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.myToolTip.SetToolTip(this.btnReScan, "Scan for available Port");
            this.btnReScan.UseVisualStyleBackColor = true;
            this.btnReScan.Click += new System.EventHandler(this.btnReScan_Click);
            // 
            // myToolTip
            // 
            this.myToolTip.AutoPopDelay = 5000;
            this.myToolTip.InitialDelay = 100;
            this.myToolTip.ReshowDelay = 100;
            // 
            // COMSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(235, 244);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "COMSettingForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COM Setting";
            this.Load += new System.EventHandler(this.COMSettingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbCOMPort;
        private System.Windows.Forms.ComboBox cbbBaudRate;
        private System.Windows.Forms.ComboBox cbbDataBits;
        private System.Windows.Forms.ComboBox cbbStopBits;
        private System.Windows.Forms.ComboBox cbbParity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReScan;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ToolTip myToolTip;
        private System.Windows.Forms.ImageList myImageList;
    }
}