using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace AGVsControlAndMonitoringSoftware
{
    public partial class HomeScreenForm : Form
    {
        public HomeScreenForm()
        {
            InitializeComponent();
            Communicator.SerialPort.DataReceived += new SerialDataReceivedEventHandler(this.SerialPort_ReceiveData);
        }

        private void HomeScreenForm_Load(object sender, EventArgs e)
        {
            // Update existing pallets in stock and add to RackColumn
            Pallet.ListPallet = DBUtility.GetPalletInfoFromDB<List<Pallet>>("PalletInfoTable");
            RackColumn.InitializePallet(RackColumn.ListColumn, Pallet.ListPallet);

            Pallet.SimListPallet = DBUtility.GetPalletInfoFromDB<List<Pallet>>("SimPalletInfoTable");
            RackColumn.InitializePallet(RackColumn.SimListColumn, Pallet.SimListPallet);
        }

        private void rdbtnRealTime_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbtnRealTime.Checked) return;
            rdbtnRealTime.ForeColor = Color.DarkRed;
            rdbtnSimulation.ForeColor = SystemColors.ActiveCaptionText;
            rdbtnRealTime.BackColor = Color.MintCream;
            rdbtnSimulation.BackColor = Color.Lavender;
            btnPauseRun.Visible = false;
            if (Communicator.SerialPort.IsOpen)
                lbModeStatus.Text = "     Connection: " + Communicator.SerialPort.PortName + " ["
                                                   + Communicator.SerialPort.BaudRate.ToString() + "-"
                                                   + Communicator.SerialPort.Parity + "-"
                                                   + Communicator.SerialPort.DataBits.ToString() + "-"
                                                   + Communicator.SerialPort.StopBits + "]";
            else lbModeStatus.Text = "     Please set communication to run.";

            Display.Mode = "Real Time";
            timerComStatus.Enabled = true;

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
            lbModeStatus.Text = "     Simulation mode is running.";

            Display.Mode = "Simulation";
            timerComStatus.Enabled = false;

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

        private void timerGUI_Tick(object sender, EventArgs e)
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

                // Update location of AGV icon (label)
                foreach (AGV agv in AGV.ListAGV)
                {
                    Display.LabelAGV[agv.ID].Location = Display.UpdatePositionAGV(agv.ID);

                    //  Display agv carrying pallet
                    if (agv.Tasks.Count != 0 && agv.Tasks[0].Status == "Doing")
                        Display.LabelAGV[agv.ID].BackColor = Color.CornflowerBlue;
                    else Display.LabelAGV[agv.ID].BackColor = Color.Silver;

                    // If goal was reached, remove old path, get next path (if exist)
                    if (agv.Path.Count == 0) continue;
                    if (agv.ExitNode == agv.Path.LastOrDefault()) Task.AddNextPathOfAGV(agv);
                }

                // Update button add pallet
                if (isPickPalletInput1 == true)
                {
                    btnAddPallet1.Text = "Add";
                    btnAddPallet1.BackColor = Color.WhiteSmoke;
                }
                if (isPickPalletInput2 == true)
                {
                    btnAddPallet2.Text = "Add";
                    btnAddPallet2.BackColor = Color.WhiteSmoke;
                }
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

                // Update position of AGV icon (label)
                foreach (AGV agv in AGV.SimListAGV)
                {
                    if (agv.Path.Count != 0) agv.Velocity = AGV.SimSpeed; else agv.Velocity = 0f;
                    Display.SimLabelAGV[agv.ID].Location = Display.SimUpdatePositionAGV(agv.ID, agv.Velocity);

                    //  Display agv carrying pallet
                    if (agv.Tasks.Count != 0 && agv.Tasks[0].Status == "Doing")
                        Display.SimLabelAGV[agv.ID].BackColor = Color.CornflowerBlue;
                    else Display.SimLabelAGV[agv.ID].BackColor = Color.Silver;
                }

                // Update button add pallet
                if (isPickSimPalletInput1 == true)
                {
                    btnAddPallet1.Text = "Add";
                    btnAddPallet1.BackColor = Color.WhiteSmoke;
                }
                if (isPickSimPalletInput2 == true)
                {
                    btnAddPallet2.Text = "Add";
                    btnAddPallet2.BackColor = Color.WhiteSmoke;
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
                    {
                        Display.AddLabelAGV(pnFloor, agv.ID, agv.ExitNode, agv.Orientation, agv.DistanceToExitNode);

                        // Send AGV Init/Info Request to AGV (except Line tracking error)
                        if (agv.IsInitialized == true) Communicator.SendAGVInfoRequest((uint)agv.ID, 'A');
                        else Communicator.SendAGVInitRequest((uint)agv.ID);
                    }
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
                    Task.AddFirstPathOfAGVs();
                    break;
                case "Simulation":
                    Task.AddFirstPathOfSimAGVs();
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

        private void communicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rdbtnRealTime.Checked == true)
            {
                using (COMSettingForm SettingForm = new COMSettingForm())
                {
                    SettingForm.ShowDialog();
                }

                if (Communicator.SerialPort.IsOpen)
                    lbModeStatus.Text = "     Connection: " + Communicator.SerialPort.PortName + " ["
                                                       + Communicator.SerialPort.BaudRate.ToString() + "-"
                                                       + Communicator.SerialPort.Parity + "-"
                                                       + Communicator.SerialPort.DataBits.ToString() + "-"
                                                       + Communicator.SerialPort.StopBits + "]";
                else lbModeStatus.Text = "     Disconnected. Please set communication to run.";
            }
        }
        
        private void SerialPort_ReceiveData(object sender, SerialDataReceivedEventArgs e)
        {
            Communicator.GetData();
        }

        private void agvMonitoringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AGVMonitoringForm agvMonitoringForm = new AGVMonitoringForm();
            agvMonitoringForm.Show();
        }

        private void rtxtbComStatus_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtxtbComStatus.SelectionStart = rtxtbComStatus.Text.Length;

            // scroll it automatically
            rtxtbComStatus.ScrollToCaret();
        }

        public static List<string> textComStatus = new List<string>();
        public static List<Color> colorComStatus = new List<Color>();
        private void timerComStatus_Tick(object sender, EventArgs e)
        {
            // Update serial port status
            if (textComStatus.Count != 0)
            {
                rtxtbComStatus.SelectionColor = colorComStatus[0];
                rtxtbComStatus.SelectedText = textComStatus[0];

                textComStatus.RemoveAt(0);
                colorComStatus.RemoveAt(0);
            }
        }

        private void warehouseDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarehouseDataForm warehouseDataForm = new WarehouseDataForm();
            warehouseDataForm.Show();
        }

        private static List<int> existedPalletCode = new List<int>();
        int autoPalletNumber = 0;
        public static bool isPickPalletInput1 = true;
        public static bool isPickPalletInput2 = true;
        public static bool isPickSimPalletInput1 = true;
        public static bool isPickSimPalletInput2 = true;
        private void btnAddPallet1_Click(object sender, EventArgs e)
        {
            if (isPickPalletInput1 == false || isPickSimPalletInput1 == false) return;

            if (Display.Mode == "Real Time")
            {
                if (AGV.ListAGV.Count == 0) return;
                // add new task
                foreach (Pallet pallet in Pallet.ListPallet)
                    existedPalletCode.Add(Convert.ToInt16(pallet.Code.Substring(2)));
                foreach (Task task in Task.ListTask)
                    existedPalletCode.Add(Convert.ToInt16(task.PalletCode.Substring(2)));
                for (int i = 1; i < 10000; i++)
                {
                    if (existedPalletCode.Contains(i)) continue;
                    else
                    {
                        autoPalletNumber = i;
                        break;
                    }
                }
                existedPalletCode.Clear();
                string palletCode = "PL" + autoPalletNumber.ToString("0000");
                Task.InputAutoAdd(palletCode, 53, Task.ListTask, AGV.ListAGV, RackColumn.ListColumn);
                Task.AddFirstPathOfAGVs();

                // display pallet code
                btnAddPallet1.Text = palletCode;
                btnAddPallet1.BackColor = Color.CornflowerBlue;
                isPickPalletInput1 = false;
            }
            else if (Display.Mode == "Simulation")
            {
                if (AGV.SimListAGV.Count == 0) return;
                foreach (Pallet pallet in Pallet.SimListPallet)
                    existedPalletCode.Add(Convert.ToInt16(pallet.Code.Substring(2)));
                foreach (Task task in Task.SimListTask)
                    existedPalletCode.Add(Convert.ToInt16(task.PalletCode.Substring(2)));
                for (int i = 1; i < 10000; i++)
                {
                    if (existedPalletCode.Contains(i)) continue;
                    else
                    {
                        autoPalletNumber = i;
                        break;
                    }
                }
                existedPalletCode.Clear();
                string palletCode = "PL" + autoPalletNumber.ToString("0000");
                Task.InputAutoAdd(palletCode, 53, Task.SimListTask, AGV.SimListAGV, RackColumn.SimListColumn);
                Task.AddFirstPathOfSimAGVs();

                btnAddPallet1.Text = palletCode;
                btnAddPallet1.BackColor = Color.CornflowerBlue;
                isPickSimPalletInput1 = false;
            }            
        }

        private void btnAddPallet2_Click(object sender, EventArgs e)
        {
            if (isPickPalletInput2 == false || isPickSimPalletInput2 == false) return;

            if (Display.Mode == "Real Time")
            {
                if (AGV.ListAGV.Count == 0) return;
                foreach (Pallet pallet in Pallet.ListPallet)
                    existedPalletCode.Add(Convert.ToInt16(pallet.Code.Substring(2)));
                foreach (Task task in Task.ListTask)
                    existedPalletCode.Add(Convert.ToInt16(task.PalletCode.Substring(2)));
                for (int i = 1; i < 10000; i++)
                {
                    if (existedPalletCode.Contains(i)) continue;
                    else
                    {
                        autoPalletNumber = i;
                        break;
                    }
                }
                existedPalletCode.Clear();
                string palletCode = "PL" + autoPalletNumber.ToString("0000");
                Task.InputAutoAdd(palletCode, 54, Task.ListTask, AGV.ListAGV, RackColumn.ListColumn);
                Task.AddFirstPathOfAGVs();

                btnAddPallet2.Text = palletCode;
                btnAddPallet2.BackColor = Color.CornflowerBlue;
                isPickPalletInput2 = false;
            }
            else if (Display.Mode == "Simulation")
            {
                if (AGV.SimListAGV.Count == 0) return;
                foreach (Pallet pallet in Pallet.SimListPallet)
                    existedPalletCode.Add(Convert.ToInt16(pallet.Code.Substring(2)));
                foreach (Task task in Task.SimListTask)
                    existedPalletCode.Add(Convert.ToInt16(task.PalletCode.Substring(2)));
                for (int i = 1; i < 10000; i++)
                {
                    if (existedPalletCode.Contains(i)) continue;
                    else
                    {
                        autoPalletNumber = i;
                        break;
                    }
                }
                existedPalletCode.Clear();
                string palletCode = "PL" + autoPalletNumber.ToString("0000");
                Task.InputAutoAdd(palletCode, 54, Task.SimListTask, AGV.SimListAGV, RackColumn.SimListColumn);
                Task.AddFirstPathOfSimAGVs();

                btnAddPallet2.Text = palletCode;
                btnAddPallet2.BackColor = Color.CornflowerBlue;
                isPickSimPalletInput2 = false;
            }
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm();
            orderForm.Show();
        }

        private void taskManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskMonitoringForm taskMonitoringForm = new TaskMonitoringForm();
            taskMonitoringForm.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.ShowDialog();
        }

        private void ShowHideLabelLocationCode(bool isShow)
        {
            lbA.Visible = isShow;
            lbA1.Visible = isShow;
            lbA2.Visible = isShow;
            lbA3.Visible = isShow;
            lbA4.Visible = isShow;
            lbA5.Visible = isShow;
            lbA6.Visible = isShow;
            lbB.Visible = isShow;
            lbB1.Visible = isShow;
            lbB2.Visible = isShow;
            lbB3.Visible = isShow;
            lbB4.Visible = isShow;
            lbB5.Visible = isShow;
            lbB6.Visible = isShow;
            lbC.Visible = isShow;
            lbC1.Visible = isShow;
            lbC2.Visible = isShow;
            lbC3.Visible = isShow;
            lbC4.Visible = isShow;
            lbC5.Visible = isShow;
            lbC6.Visible = isShow;
            lbD.Visible = isShow;
            lbD1.Visible = isShow;
            lbD2.Visible = isShow;
            lbD3.Visible = isShow;
            lbD4.Visible = isShow;
            lbD5.Visible = isShow;
            lbD6.Visible = isShow;
        }

        private void showBlockColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showBlockColumnToolStripMenuItem.Checked == false)
            {
                showBlockColumnToolStripMenuItem.Checked = true;
                ShowHideLabelLocationCode(true);
            }
            else
            {
                showBlockColumnToolStripMenuItem.Checked = false;
                ShowHideLabelLocationCode(false);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "This software is designed for a Thesis Project:" + 
                             "\nMonitoring & Controlling of Automated Guided Vehicles (AGVs) System in Warehouse." +
                             "\nAt Ho Chi Minh City University of Technology (HCMUT)." + 
                             "\n\nDesigned by Vo Trong Nghia." +
                             "\nGithub: votronghia/AGVs-Control-and-Monitoring-Software.";
            MessageBox.Show(message, "About this software", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void timerCheckConn_Tick(object sender, EventArgs e)
        {            
            if (!Communicator.SerialPort.IsOpen && COMSettingForm.btnConnClicked)
            {
                try { Communicator.SerialPort.Open(); }
                catch (Exception err)
                {
                    if (Communicator.isReconnecting == false)
                    {
                        Display.UpdateComStatus("status", 0, err.Message, System.Drawing.Color.Red);
                        Communicator.isReconnecting = true;
                    }
                }
                if (Communicator.SerialPort.IsOpen)
                {
                    Display.UpdateComStatus("status", 0, "Reconnection OK (" + Communicator.SerialPort.PortName + ")", System.Drawing.Color.Blue);
                    Communicator.isReconnecting = false;
                }
            }
        }
    }
}
