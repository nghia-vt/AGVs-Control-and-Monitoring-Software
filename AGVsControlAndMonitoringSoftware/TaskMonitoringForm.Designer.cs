namespace AGVsControlAndMonitoringSoftware
{
    partial class TaskMonitoringForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskMonitoringForm));
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.TaskName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TaskStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TaskAGV = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PickNode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DropNode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PalletCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timerListView = new System.Windows.Forms.Timer(this.components);
            this.imageListTask = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listViewTasks
            // 
            this.listViewTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTasks.BackColor = System.Drawing.Color.White;
            this.listViewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TaskName,
            this.TaskStatus,
            this.Type,
            this.TaskAGV,
            this.PickNode,
            this.DropNode,
            this.PalletCode});
            this.listViewTasks.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.listViewTasks.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listViewTasks.Location = new System.Drawing.Point(0, 0);
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(559, 320);
            this.listViewTasks.SmallImageList = this.imageListTask;
            this.listViewTasks.TabIndex = 28;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.View = System.Windows.Forms.View.Details;
            // 
            // TaskName
            // 
            this.TaskName.Text = "Task Name";
            this.TaskName.Width = 90;
            // 
            // TaskStatus
            // 
            this.TaskStatus.Text = "Status";
            this.TaskStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TaskStatus.Width = 70;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Type.Width = 70;
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
            this.PickNode.Width = 85;
            // 
            // DropNode
            // 
            this.DropNode.Text = "Drop Node";
            this.DropNode.Width = 85;
            // 
            // PalletCode
            // 
            this.PalletCode.Text = "Pallet Code";
            this.PalletCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PalletCode.Width = 90;
            // 
            // timerListView
            // 
            this.timerListView.Enabled = true;
            this.timerListView.Tick += new System.EventHandler(this.timerListView_Tick);
            // 
            // imageListTask
            // 
            this.imageListTask.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTask.ImageStream")));
            this.imageListTask.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTask.Images.SetKeyName(0, "icon_agv.ico");
            this.imageListTask.Images.SetKeyName(1, "icon_task.png");
            // 
            // TaskMonitoringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(559, 320);
            this.Controls.Add(this.listViewTasks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskMonitoringForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task Monitoring";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.ColumnHeader TaskName;
        private System.Windows.Forms.ColumnHeader TaskStatus;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader TaskAGV;
        private System.Windows.Forms.ColumnHeader PickNode;
        private System.Windows.Forms.ColumnHeader DropNode;
        private System.Windows.Forms.ColumnHeader PalletCode;
        private System.Windows.Forms.Timer timerListView;
        private System.Windows.Forms.ImageList imageListTask;

    }
}