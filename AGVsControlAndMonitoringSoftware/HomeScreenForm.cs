using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGVsControlAndMonitoringSoftware
{
    public partial class HomeScreenForm : Form
    {
        public HomeScreenForm()
        {
            InitializeComponent();
        }

        private void HomeScreenForm_Load(object sender, EventArgs e)
        {

            // View data on DataGridView 
            //dgv_ThongTin.DataSource = DBUtility.GetDataFromDB<DataTable>("NodeInfoTable");
        }

        private void rdbtnRealTime_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbtnRealTime.Checked) return;
            rdbtnRealTime.ForeColor = Color.DarkRed;
            rdbtnSimulation.ForeColor = SystemColors.ActiveCaptionText;
            rdbtnRealTime.BackColor = Color.MintCream;
            rdbtnSimulation.BackColor = Color.Lavender;
            btnPauseRun.Visible = false;
            Display.Mode = "Real Time";

            // Add label of columns at node
            RackColumn.SimListColumn.ForEach(col => RackColumn.RemoveLabelColumn(pnFloor, col));
            RackColumn.ListColumn.ForEach(col => pnFloor.Controls.Add(col.ColumnLabel));
            
            // Remove simulation label, add real time label
            AGV.SimListAGV.ForEach(agv => Display.RemoveLabelAGV(pnFloor, agv.ID));
            foreach (AGV agv in AGV.ListAGV)
                Display.AddLabelAGV(pnFloor, agv.ID, agv.ExitNode, agv.Orientation, agv.DistanceToExitNode);

            // Clear draw path in panel
            Display.Points = new Point[] { new Point(), new Point() };
            pnFloor.Refresh();
        }

        private void rdbtnSimulation_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbtnSimulation.Checked) return;
            rdbtnSimulation.ForeColor = Color.DarkRed;
            rdbtnRealTime.ForeColor = SystemColors.ActiveCaptionText;
            rdbtnSimulation.BackColor = Color.MintCream;
            rdbtnRealTime.BackColor = Color.Lavender;
            btnPauseRun.Visible = true;
            Display.Mode = "Simulation";

            // Add label of columns at node
            RackColumn.ListColumn.ForEach(col => RackColumn.RemoveLabelColumn(pnFloor, col));          
            RackColumn.SimListColumn.ForEach(col => pnFloor.Controls.Add(col.ColumnLabel));
            

            // Remove real time label, add simulation label
            AGV.ListAGV.ForEach(agv => Display.RemoveLabelAGV(pnFloor, agv.ID));
            foreach (AGV agv in AGV.SimListAGV)
                Display.AddLabelAGV(pnFloor, agv.ID, agv.ExitNode, agv.Orientation, agv.DistanceToExitNode);

            // Clear draw path in panel
            Display.Points = new Point[] { new Point(), new Point() };
            pnFloor.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Show time now
            DateTime time = DateTime.Now;
            lbTime.Text = time.ToString("dddd, MMMM dd, yyyy     h:mm:ss tt");

            if (Display.Mode == "Real Time")
            {
                // Update data in listView AGVs
                Display.UpdateListViewAGVs(listViewAGVs, AGV.ListAGV);
                // Update data in listView Tasks
                Display.UpdateListViewTasks(listViewTasks, Task.ListTask);
                // Update column color of SimListColumn
                RackColumn.UpdateColumnColor(RackColumn.ListColumn);

                // --------------------Add later----------------
            }
            else if (Display.Mode == "Simulation")
            {
                // Update data in listView AGVs
                Display.UpdateListViewAGVs(listViewAGVs, AGV.SimListAGV);
                // Update data in listView Tasks
                Display.UpdateListViewTasks(listViewTasks, Task.SimListTask);
                // Update column color of SimListColumn
                RackColumn.UpdateColumnColor(RackColumn.SimListColumn);

                // If pause simulation is selected
                if (Display.SimPause == true) return;

                // Detect collision
                Collision.Detect(AGV.SimListAGV);

                // Update location of AGV icon (label)
                foreach (AGV agv in AGV.SimListAGV)
                {
                    if (agv.Path.Count != 0) agv.Velocity = AGV.SimSpeed; else agv.Velocity = 0f;
                    Display.SimLabelAGV[agv.ID].Location = Display.SimUpdatePositionAGV(agv.ID, agv.Velocity);

                    //  Display agv carrying pallet
                    if (agv.Tasks.Count != 0 && agv.Tasks[0].Status == "Doing")
                        Display.SimLabelAGV[agv.ID].BackColor = Color.CornflowerBlue;
                    else Display.SimLabelAGV[agv.ID].BackColor = Color.Silver;
                }
            }  
        }

        private void pnFloor_Paint(object sender, PaintEventArgs e)
        {
            // Draw lines with Points is got by Display.AddPath function
            Graphics graphic = pnFloor.CreateGraphics();
            Pen pen = new Pen(Display.LineColor, Display.LineWidth);
            graphic.DrawLines(pen, Display.Points);
            graphic.Dispose();
        }

        private void AddRemoveAGV_Click(object sender, EventArgs e)
        {
            // Clone a list of ListAGV, clone all item in it (because of reference type)
            List<AGV> oldList = new List<AGV>();
            switch (Display.Mode)
            {
                case "Real Time":
                    foreach (AGV a in AGV.ListAGV)
                        oldList.Add(new AGV(a.ID, a.ExitNode, a.Orientation, a.DistanceToExitNode, a.Status));
                    break;
                case "Simulation":
                    foreach (AGV a in AGV.SimListAGV)
                        oldList.Add(new AGV(a.ID, a.ExitNode, a.Orientation, a.DistanceToExitNode, a.Status));
                    break;
            }
            
            // Open add/remove agv form
            using (AddRemoveAGVForm AddRemoveForm = new AddRemoveAGVForm())
            {
                AddRemoveForm.ShowDialog();
            }

            // Show AGV icon on panel (use label)
            oldList.ForEach(agv => Display.RemoveLabelAGV(pnFloor, agv.ID));
            switch (Display.Mode)
            {
                case "Real Time":
                    foreach (AGV agv in AGV.ListAGV)
                        Display.AddLabelAGV(pnFloor, agv.ID, agv.ExitNode, agv.Orientation, agv.DistanceToExitNode);
                    break;
                case "Simulation":
                    foreach (AGV agv in AGV.SimListAGV)
                        Display.AddLabelAGV(pnFloor, agv.ID, agv.ExitNode, agv.Orientation, agv.DistanceToExitNode);
                    break;
            }
        }

        private void addRemoveTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open add/remove task form
            using (AddRemoveTaskForm AddRemoveForm = new AddRemoveTaskForm())
            {
                AddRemoveForm.ShowDialog();
            }

            // Add fist path to each AGV
            switch (Display.Mode)
            {
                case "Real Time":

                    // -----------Add later----------------------

                    break;
                case "Simulation":
                    Task.AddFirstPathOfAGVs(AGV.SimListAGV);
                    break;
            }
        }

        private void cntxMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            List<string> strItems = new List<string>();
            switch (Display.Mode)
            {
                case "Real Time": AGV.ListAGV.ForEach(a => strItems.Add("AGV#" + a.ID.ToString()));
                    break;
                case "Simulation": AGV.SimListAGV.ForEach(a => strItems.Add("AGV#" + a.ID.ToString()));
                    break;
            }

            showPathToolStripMenuItem.DropDownItems.Clear();
            foreach(string items in strItems)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(items, imgList.Images[0]);
                showPathToolStripMenuItem.DropDownItems.Add(item);
                item.Click += new EventHandler(AGVDrawPath_Click);
            }
        }

        private void AGVDrawPath_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string[] arrItem = item.Text.Split(new char[] {'#'}, StringSplitOptions.RemoveEmptyEntries);
            int agvID = Convert.ToInt16(arrItem[1]);

            switch (Display.Mode)
            {
                case "Real Time":
                    int i = AGV.ListAGV.FindIndex(a => a.ID == agvID);
                    if (AGV.ListAGV[i].Path.Count == 0) return;
                    Display.AddPath(pnFloor, AGV.ListAGV[i].Path, Color.Blue, 4);
                    break;
                case "Simulation":
                    int j = AGV.SimListAGV.FindIndex(a => a.ID == agvID);
                    if (AGV.SimListAGV[j].Path.Count == 0) return;
                    Display.AddPath(pnFloor, AGV.SimListAGV[j].Path, Color.Blue, 4);
                    break;
            } 
        }

        private void hidePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display.Points = new Point[] { new Point(), new Point() };
            pnFloor.Refresh();
        }

        private void btnPauseRun_Click(object sender, EventArgs e)
        {
            btnPauseRun.Text = btnPauseRun.Text == "Pause" ? btnPauseRun.Text = "Run" : btnPauseRun.Text = "Pause";
            if (btnPauseRun.Text == "Pause")
            {
                btnPauseRun.ImageIndex = 2;
                Display.SimPause = false;
            }
            else
            {
                btnPauseRun.ImageIndex = 3;
                Display.SimPause = true;
            }
        }
    }
}
