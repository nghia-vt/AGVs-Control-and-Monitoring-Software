using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace AGVsControlAndMonitoringSoftware
{
    public partial class AGVMonitoringForm : Form
    {
        private int tickStart;
        public static int selectedAGVID;

        public AGVMonitoringForm()
        {
            InitializeComponent();
            InitZedGraph();
            switch (Display.Mode)
            {
                case "Simulation": txtbSetVelocity.Text = AGV.SimSpeed.ToString(); break;
                case "Real Time": txtbSetVelocity.Text = AGV.Speed.ToString(); break;
            }
        }

        private void AGVMonitoringForm_Load(object sender, EventArgs e)
        {
            switch (Display.Mode)
            {
                case "Real Time":
                    if (AGV.ListAGV.Count != 0)
                    {
                        cbbAGV.Text = "AGV#" + AGV.ListAGV[0].ID.ToString();
                        selectedAGVID = AGV.ListAGV[0].ID;
                    }
                    break;
                case "Simulation":
                    if (AGV.SimListAGV.Count != 0) cbbAGV.Text = "AGV#" + AGV.SimListAGV[0].ID.ToString();
                    break;
            }
        }

        private void cbbAGV_DropDown(object sender, EventArgs e)
        {
            // Display AGV in combobox if AGV exist
            cbbAGV.Items.Clear();
            switch (Display.Mode)
            {
                case "Real Time":
                    AGV.ListAGV.ForEach(agv => cbbAGV.Items.Add("AGV#" + agv.ID));
                    break;
                case "Simulation":
                    AGV.SimListAGV.ForEach(agv => cbbAGV.Items.Add("AGV#" + agv.ID));
                    break;
            }
        }

        private void timerGraph_Tick(object sender, EventArgs e) // 100ms
        {
            if (Display.Mode == "Real Time")
            {
                if (AGV.ListAGV.Count == 0) return;
                var agv = AGV.ListAGV.Find(a => a.ID == selectedAGVID);
                if (agv == null) return;

                UpdateMonitoringData(agv, Communicator.lineTrackError);
            }
            else if (Display.Mode == "Simulation")
            {
                if (AGV.SimListAGV.Count == 0) return;
                var agv = AGV.SimListAGV.Find(a => a.ID == selectedAGVID);
                if (agv == null) return;
                agv.Battery = 95;

                Random random = new Random();
                UpdateMonitoringData(agv, 2*random.NextDouble() - 1);
            }
        }

        private void InitZedGraph()
        {
            #region Init Velocity graph
            GraphPane velocityPane = zedGraphVelocity.GraphPane;

            RollingPointPairList velocityPointBuffer = new RollingPointPairList(10000);
            LineItem velocityCurve = velocityPane.AddCurve("velocity", velocityPointBuffer, Color.Red, SymbolType.None);

            RollingPointPairList setPointBuffer = new RollingPointPairList(10000);
            LineItem setPointCurve = velocityPane.AddCurve("desired velocity", setPointBuffer, Color.Blue, SymbolType.None);

            // Add titles
            velocityPane.Title.Text = "Velocity of AGV";
            velocityPane.Title.FontSpec.FontColor = Color.Navy;
            velocityPane.XAxis.Title.Text = "t (s)";
            velocityPane.XAxis.Title.FontSpec.FontColor = Color.Navy;
            velocityPane.XAxis.Title.FontSpec.IsBold = false;
            velocityPane.YAxis.Title.Text = "velocity (cm/s)";
            velocityPane.YAxis.Title.FontSpec.FontColor = Color.Navy;
            velocityPane.YAxis.Title.FontSpec.IsBold = false;

            // Add gridlines to the plot, and make them gray
            velocityPane.XAxis.MajorGrid.IsVisible = true;
            velocityPane.YAxis.MajorGrid.IsVisible = true;
            velocityPane.XAxis.MajorGrid.Color = Color.Gray;
            velocityPane.YAxis.MajorGrid.Color = Color.Gray;

            // Set scale of axis
            velocityPane.XAxis.Scale.Min = 0;
            velocityPane.XAxis.Scale.Max = 30;
            velocityPane.XAxis.Scale.MinorStep = 1;
            velocityPane.XAxis.Scale.MajorStep = 5;

            // Move the legend location
            velocityPane.Legend.Position = LegendPos.InsideTopRight;
            velocityPane.Legend.Border.IsVisible = false;
            velocityPane.Legend.Fill.IsVisible = false;
            velocityPane.Legend.IsHStack = false;

            // Make both curves thicker
            velocityCurve.Line.Width = 2.0F;
            setPointCurve.Line.Width = 2.0F;

            //// Fill the area under the curves
            //velocityCurve.Line.Fill = new Fill(Color.White, Color.FromArgb(255, 175, 175), -90F);
            //setPointCurve.Line.Fill = new Fill(Color.White, Color.FromArgb(255, 175, 175), -90F);

            // Fill the Axis and Pane backgrounds
            velocityPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 210), -45F);
            velocityPane.Fill = new Fill(Color.WhiteSmoke);
            velocityPane.Border.IsVisible = false;

            // Tell ZedGraph to refigure the axes since the data have changed
            zedGraphVelocity.AxisChange();
            #endregion

            #region Init Line Tracking graph
            GraphPane linetrackPane = zedGraphLineTrack.GraphPane;

            RollingPointPairList linetrackPointBuffer = new RollingPointPairList(10000);
            LineItem linetrackCurve = linetrackPane.AddCurve("error", linetrackPointBuffer, Color.Red, SymbolType.None);

            // Add titles
            linetrackPane.Title.Text = "Error of line tracking";
            linetrackPane.Title.FontSpec.FontColor = Color.Navy;
            linetrackPane.XAxis.Title.Text = "t (s)";
            linetrackPane.XAxis.Title.FontSpec.FontColor = Color.Navy;
            linetrackPane.XAxis.Title.FontSpec.IsBold = false;
            linetrackPane.YAxis.Title.Text = "Error";
            linetrackPane.YAxis.Title.FontSpec.FontColor = Color.Navy;
            linetrackPane.YAxis.Title.FontSpec.IsBold = false;

            // Add gridlines to the plot, and make them gray
            linetrackPane.XAxis.MajorGrid.IsVisible = true;
            linetrackPane.YAxis.MajorGrid.IsVisible = true;
            linetrackPane.XAxis.MajorGrid.Color = Color.Gray;
            linetrackPane.YAxis.MajorGrid.Color = Color.Gray;

            // Set scale of axis
            linetrackPane.XAxis.Scale.Min = 0;
            linetrackPane.XAxis.Scale.Max = 30;
            linetrackPane.XAxis.Scale.MinorStep = 1;
            linetrackPane.XAxis.Scale.MajorStep = 5;

            // Move the legend location
            linetrackPane.Legend.Position = LegendPos.InsideTopRight;
            linetrackPane.Legend.Border.IsVisible = false;
            linetrackPane.Legend.Fill.IsVisible = false;
            linetrackPane.Legend.IsHStack = false;

            // Make both curves thicker
            linetrackCurve.Line.Width = 2.0F;

            //// Fill the area under the curves
            //linetrackCurve.Line.Fill = new Fill(Color.White, Color.FromArgb(255, 175, 175), -90F);

            // Fill the Axis and Pane backgrounds
            linetrackPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 210), -45F);
            linetrackPane.Fill = new Fill(Color.WhiteSmoke);
            linetrackPane.Border.IsVisible = false;

            // Tell ZedGraph to refigure the axes since the data have changed
            zedGraphLineTrack.AxisChange();
            #endregion

            tickStart = Environment.TickCount;
        }

        private void DrawGraph(ZedGraphControl zedGraphControl, double value)
        {
            if (zedGraphControl.GraphPane.CurveList.Count <= 0) return;

            // time is measure in seconds
            double time = (Environment.TickCount - tickStart) / 1000.0;

            LineItem curve = zedGraphControl.GraphPane.CurveList[0] as LineItem;
            IPointListEdit pointBuffer = curve.Points as IPointListEdit;
            // add point to buffer to draw
            pointBuffer.Add(time, value);

            if (zedGraphControl == zedGraphVelocity)
            {
                LineItem curve1 = zedGraphControl.GraphPane.CurveList[1] as LineItem;
                IPointListEdit pointBuffer1 = curve1.Points as IPointListEdit;
                // add point to buffer to draw
                if (Display.Mode == "Real Time") pointBuffer1.Add(time, AGV.Speed);
                else if (Display.Mode == "Simulation") pointBuffer1.Add(time, AGV.SimSpeed);
            }

            // make xAxis scroll
            Scale xScale = zedGraphControl.GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - 30.0;
            }

            // re-draw graph
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void UpdateMonitoringData(AGV agv, double linetrackingError)
        {
            // update agv info
            lbStatus.Text = agv.Status;
            lbExitNode.Text = agv.ExitNode.ToString();
            lbOrient.Text = agv.Orientation.ToString();
            lbDistance.Text = Math.Round(agv.DistanceToExitNode, 1).ToString() + " cm";
            lbVelocity.Text = Math.Round(agv.Velocity, 1).ToString() + " cm/s";
            lbBattery.Text = agv.Battery.ToString() + "%";
            prgrbBattery.Value = agv.Battery;
            
            // update current path and highlight current node
            rtxtbCurrentPath.Clear();
            if (agv.Path.Count == 0) rtxtbCurrentPath.SelectedText = "None";
            foreach(int n in agv.Path)
            {
                if (n == agv.ExitNode) rtxtbCurrentPath.SelectionBackColor = Color.Yellow;
                else rtxtbCurrentPath.SelectionBackColor = Color.Lavender;
                if (n != agv.Path[agv.Path.Count - 1]) rtxtbCurrentPath.SelectedText = n.ToString() + "->";
                else rtxtbCurrentPath.SelectedText = n.ToString();
            }            

            // update graph
            DrawGraph(zedGraphVelocity, agv.Velocity);
            DrawGraph(zedGraphLineTrack, linetrackingError);
        }

        private void cbbAGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbbAGV.Text)) return;

            // clear all curve
            zedGraphVelocity.GraphPane.CurveList.Clear();
            zedGraphLineTrack.GraphPane.CurveList.Clear();

            // re-init graph
            InitZedGraph();

            // get new AGVID
            if (String.IsNullOrEmpty(cbbAGV.Text)) return;
            string[] arr = cbbAGV.Text.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            selectedAGVID = Convert.ToInt16(arr[1]);
            
            // Send AGV Info Request to AGV (include Line tracking error)
            if (Display.Mode == "Real Time")
                Communicator.SendAGVInfoRequest((uint)selectedAGVID, 'L');
        }

        private void btnSetVelocity_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtbSetVelocity.Text)) return;
            float sendVelocity = Convert.ToSingle(txtbSetVelocity.Text);
            if (Display.Mode == "Real Time")
            {
                AGV.Speed = sendVelocity;
                foreach (AGV agv in AGV.ListAGV)
                {
                    Communicator.SendVelocitySetting((uint)agv.ID, sendVelocity);
                }
            }
            else if (Display.Mode == "Simulation")
            {
                AGV.SimSpeed = sendVelocity;
            }
            
        }

        private void cbbAGV_KeyDown(object sender, KeyEventArgs e)
        {
            // This will discard the delete keypress (can also use e.Handled = true)
            if (e.KeyCode == Keys.Delete) e.Handled = true;
        }

        private void cbbAGV_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This will discard the keypress (can also use e.Handled = true)
            e.KeyChar = (char)Keys.None;
        }

        private void AGVMonitoringForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Send AGV Info Request to AGV (except Line tracking error)
            if (Display.Mode == "Real Time")
            {
                foreach(AGV agv in AGV.ListAGV) Communicator.SendAGVInfoRequest((uint)agv.ID, 'A');
            }
        }

        private void txtbSetVelocity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != 46) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only allow a float number.", "Velocity Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // checks to make sure only 1 point is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
    }
}
