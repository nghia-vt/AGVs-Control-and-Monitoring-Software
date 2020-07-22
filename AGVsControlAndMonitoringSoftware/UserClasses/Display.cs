using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AGVsControlAndMonitoringSoftware
{
    class Display
    {
        public static string Mode;
        public static Color LineColor;
        public static float LineWidth;
        public static Point[] Points = new Point[2];
        public static Label[] LabelAGV = new Label[AGV.MaxNumOfAGVs]; // LabelAGV[i] for AGV ID = i
        public static Label[] SimLabelAGV = new Label[AGV.MaxNumOfAGVs];
        public static float Scale = 583f / 235; // pixel/cm (2.480851f)
        public static bool SimPause = true; // pause simulation

        // Draw the path from start to goal on panel by updatting the Points array
        public static void AddPath(Panel panel, List<int> path, Color color, float width)
        {
            List<Point> listPoint = new List<Point>();
            List<Node> Nodes = Node.ListNode;
            LineColor = color;
            LineWidth = width;
            for (int i = 0; i < path.Count; i++)
            {
                listPoint.Add(new Point(Nodes[path[i]].X, Nodes[path[i]].Y));
            }
            Points = listPoint.ToArray();
            panel.Refresh();
        }
        
        // Create AGV icon by using Label control, LabelAGV[i] for AGV ID = i
        public static void AddLabelAGV(Panel panel, int agvID, int initExitNode, char initOrientation, float initDistanceToExitNode)
        {
            Label lbAGV = new Label();
            lbAGV.BackColor = Color.Silver;
            lbAGV.Font = new Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, 
                                   System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbAGV.Size = new Size(56, 56);
            lbAGV.Text = "AGV#" + agvID.ToString();
            lbAGV.TextAlign = ContentAlignment.MiddleCenter;
            lbAGV.Name = "AGV" + agvID.ToString();

            // Init the position of new AGV
            List<Node> Nodes = Node.ListNode;
            int initPixelDistance = (int)Math.Round(initDistanceToExitNode * Display.Scale);
            int x = Nodes[initExitNode].X - lbAGV.Size.Width / 2;
            int y = Nodes[initExitNode].Y - lbAGV.Size.Height / 2;
            switch (initOrientation)
            {
                case 'E':
                    lbAGV.Location = new Point(x + initPixelDistance, y);
                    break;
                case 'W':
                    lbAGV.Location = new Point(x - initPixelDistance, y);
                    break;
                case 'N':
                    lbAGV.Location = new Point(x, y - initPixelDistance);
                    break;
                case 'S':
                    lbAGV.Location = new Point(x, y + initPixelDistance);
                    break;
            }

            // Add to Array for use
            switch (Display.Mode)
            {
                case "Real Time":
                    LabelAGV[agvID] = lbAGV;
                    panel.Controls.Add(Display.LabelAGV[agvID]);
                    break;
                case "Simulation":
                    SimLabelAGV[agvID] = lbAGV;
                    panel.Controls.Add(Display.SimLabelAGV[agvID]);
                    break;
            }
        }

        // Remove a label of AGV
        public static void RemoveLabelAGV(Panel panel, int agvID)
        {
            var label = panel.Controls.OfType<Label>().FirstOrDefault(lb => lb.Name == "AGV" + agvID.ToString());
            if (label != null)
            {
                panel.Controls.Remove(label);
                label.Dispose();

                switch (Display.Mode)
                {
                    case "Real Time": Array.Clear(LabelAGV, agvID, 1);
                        break;
                    case "Simulation": Array.Clear(SimLabelAGV, agvID, 1);
                        break;
                }      
            }
        }

        #region Update position AGV icon for Real-time Mode

        // Update position AGV icon in simulation mode (speed: cm/s)
        public static Point UpdatePositionAGV(int agvID)
        {
            // Find AGV in ListAGV
            var index = AGV.ListAGV.FindIndex(a => a.ID == agvID);
            AGV agv = AGV.ListAGV[index];
            Point position = new Point();
 
            // If this node is pick node, remove pallet code that was picked and save this time
            if (agv.Tasks.Count != 0 && agv.ExitNode == agv.Tasks[0].PickNode)
            {
                RackColumn column = RackColumn.ListColumn.Find(c => c.AtNode == agv.ExitNode);

                Pallet.SaveDeliveryTime(column.PalletCodes[agv.Tasks[0].PickLevel - 1], Pallet.ListPallet);
                DBUtility.UpdatePalletDB("PalletInfoTable", column.PalletCodes[agv.Tasks[0].PickLevel - 1], false,
                                          DateTime.Now.ToString("dddd, MMMM dd, yyyy  h:mm:ss tt"), Pallet.ListPallet);

                column.PalletCodes[agv.Tasks[0].PickLevel - 1] = null;
            }

            // Update label position
            List<Node> Nodes = Node.ListNode;
            int pixelDistance = (int)Math.Round(agv.DistanceToExitNode * Display.Scale);
            int x = Nodes[agv.ExitNode].X - Display.LabelAGV[agvID].Size.Width / 2;
            int y = Nodes[agv.ExitNode].Y - Display.LabelAGV[agvID].Size.Height / 2;
            switch (agv.Orientation)
            {
                case 'E':
                    position.X = x + pixelDistance; position.Y = y;
                    break;
                case 'W':
                    position.X = x - pixelDistance; position.Y = y;
                    break;
                case 'N':
                    position.X = x; position.Y = y - pixelDistance;
                    break;
                case 'S':
                    position.X = x; position.Y = y + pixelDistance;
                    break;
            }
            return position;
        }

        #endregion

        #region Update position AGV icon for Simulation Mode

        // Update position AGV icon in simulation mode (speed: cm/s)
        public static Point SimUpdatePositionAGV(int agvID, float speed)
        {
            // Find AGV in SimListAGV, get current point
            var index = AGV.SimListAGV.FindIndex(a => a.ID == agvID);
            AGV agv = AGV.SimListAGV[index];
            Point position = Display.SimLabelAGV[agvID].Location;

            // Handle (waiting method) collision if it happens
            CollisionStatus status = Collision.SimHandle(agv, Collision.SimListCollision);
            if (status == CollisionStatus.Handling)
            {
                // Update agv status and velocity
                agv.Velocity = 0;
                agv.Status = "Stop";

                return position;
            }

            // return old point when agv has no path
            if (agv.Path.Count == 0) return position;

            // Get navigation frame array. Note: string is a reference type,
            // so any change in navigationArr is also in AGV.SimListAGV[index].navigationArr
            string[] navigationArr = agv.navigationArr;            

            // Check whether current point is a node or not 
            // Note: shift position of label to center (+LabelAGV[].Width/2, +LabelAGV[].Height/2)
            var node = Node.ListNode.FirstOrDefault(n =>
            {
                return (n.X == position.X + SimLabelAGV[agvID].Width / 2)
                    && (n.Y == position.Y + SimLabelAGV[agvID].Height / 2);
            });

            // Current point is not a node and current position is start node, 
            // it means initDistance to start node != 0, so go backward once then keep go ahead
            char orient = new char();
            if (node == null && agv.ExitNode == agv.Path[0])
            {
                switch (navigationArr[0])
                {
                    case "A":
                        orient = UpdateOrient(agv.Orientation, "A");
                        break;
                    case "B":
                        orient = UpdateOrient(agv.Orientation, "B");
                        navigationArr[0] = "A";
                        break;
                }
            }
            // Current point is not a node and current position is not start node,
            // so keep go ahead
            else if (node == null) orient = UpdateOrient(agv.Orientation, "A");
            // If goal was reached, no update position, remove old path, get next path (if exist)
            else if (node.ID == agv.Path.LastOrDefault())
            {
                // Update AGV information
                agv.ExitNode = node.ID;  // Update ExitNode
                orient = UpdateOrient(agv.Orientation, "A");
                agv.Orientation = orient;  // Update Orientation
                agv.DistanceToExitNode = 0f; // Update Distance to ExitNode
                agv.Status = "Stop"; // Update Status
                agv.Velocity = 0; // Update Velocity

                // Add next path
                Task.AddNextPathOfSimAGV(agv);
                
                return position;
            }
            // Current point is at start node and initDistance to start node == 0
            // Turn direction once then keep go ahead
            else if (node.ID == agv.Path[0] && agv.DistanceToExitNode == 0f)
            {
                switch (navigationArr[0])
                {
                    case "A":
                        orient = UpdateOrient(agv.Orientation, "A");
                        break;
                    case "B":
                        orient = UpdateOrient(agv.Orientation, "B");
                        navigationArr[0] = "A";
                        break;
                    case "L":
                        orient = UpdateOrient(agv.Orientation, "L");
                        navigationArr[0] = "A";
                        break;
                    case "R":
                        orient = UpdateOrient(agv.Orientation, "R");
                        navigationArr[0] = "A";
                        break;
                }

                // If this node is pick node, remove pallet code that was picked and save this time
                if (agv.Tasks.Count != 0 && node.ID == agv.Tasks[0].PickNode)
                {
                    RackColumn column = RackColumn.SimListColumn.Find(c => c.AtNode == agv.ExitNode);

                    Pallet.SaveDeliveryTime(column.PalletCodes[agv.Tasks[0].PickLevel - 1], Pallet.SimListPallet);
                    DBUtility.UpdatePalletDB("SimPalletInfoTable", column.PalletCodes[agv.Tasks[0].PickLevel - 1], false,
                                          DateTime.Now.ToString("dddd, MMMM dd, yyyy  h:mm:ss tt"), Pallet.SimListPallet);

                    column.PalletCodes[agv.Tasks[0].PickLevel - 1] = null;
                }
            }
            // Current point is a node but start node
            else
            {
                int idx = Array.IndexOf(navigationArr, node.ID.ToString());
                string dir = navigationArr[idx + 1];
                orient = UpdateOrient(agv.Orientation, dir);
            }

            // Modify speed to make sure AGV icon can reach next node
            int i = agv.Path.IndexOf(agv.ExitNode);
            int nextNode = agv.Path[i + 1];
            int dnx = (position.X + SimLabelAGV[agvID].Width / 2) - Node.ListNode[nextNode].X;
            int dny = (position.Y + SimLabelAGV[agvID].Height / 2) - Node.ListNode[nextNode].Y;
            int nd = (int)Math.Sqrt(dnx * dnx + dny * dny);
            
            if (agv.ExitNode == agv.Path[0])
            {
                // At first node of path, having 4 cases of agv position
                // (dnx * dny != 0) for 2 cases and (dnx * dnx0 > 0 || dny * dny0 > 0) for the others
                int dnx0 = (position.X + SimLabelAGV[agvID].Width / 2) - Node.ListNode[agv.Path[0]].X;
                int dny0 = (position.Y + SimLabelAGV[agvID].Height / 2) - Node.ListNode[agv.Path[0]].Y;
                int nd0 = (int)Math.Sqrt(dnx0 * dnx0 + dny0 * dny0);
                if (dnx * dny != 0 || dnx * dnx0 > 0 || dny * dny0 > 0)
                {
                    nextNode = agv.Path[0];
                    nd = nd0;
                }
            }
            int sp = (int)Math.Round(speed * Display.Scale * (100.0 / 1000)); // timer1.Interval = 100ms
            int step = (nd % sp == 0) ? sp : (nd % sp);            

            // Update AGV information before update position
            if (node != null) agv.ExitNode = node.ID;  // Update ExitNode
            agv.Orientation = orient;  // Update Orientation
            int exitNode = agv.ExitNode;
            int dx = (position.X + SimLabelAGV[agvID].Width / 2) - Node.ListNode[exitNode].X;
            int dy = (position.Y + SimLabelAGV[agvID].Height / 2) - Node.ListNode[exitNode].Y;
            agv.DistanceToExitNode = (float)Math.Sqrt(dx * dx + dy * dy) / Display.Scale; // Update Distance to ExitNode
            agv.Status = "Running"; // Update Status

            // Update next position of AGV icon in panel
            switch (orient)
            {
                case 'E':
                    position = new Point(position.X + step, position.Y);
                    break;
                case 'W':
                    position = new Point(position.X - step, position.Y);
                    break;
                case 'S':
                    position = new Point(position.X, position.Y + step);
                    break;
                case 'N':
                    position = new Point(position.X, position.Y - step);
                    break;
            }

            return position;
        }

        // Update new orient for next move direction
        private static char UpdateOrient(char pre_Orient, string direction)
        {
            char orient = new char();
            switch (pre_Orient.ToString() + direction)
            {
                case "EA":
                case "WB":
                case "SL":
                case "NR": orient = 'E';
                    break;
                case "WA":
                case "EB":
                case "NL":
                case "SR": orient = 'W';
                    break;
                case "ER":
                case "WL":
                case "SA":
                case "NB": orient = 'S';
                    break;
                case "EL":
                case "WR":
                case "SB":
                case "NA": orient = 'N';
                    break;
            }
            return orient;
        }

        #endregion

        // Update data in listView AGVs
        public static void UpdateListViewAGVs(ListView listView, List<AGV> listData)
        {
            if (listData.Count == 0) listView.Items.Clear();
            foreach (AGV agv in listData)
            {
                if (listView.Items.Count > listData.Count)
                {
                    listView.Items.Clear();
                }
                else if (listView.Items.Count < listData.Count)
                {
                    listView.Items.Add(" AGV#" + agv.ID, 0);
                    listView.Items[listView.Items.Count - 1].SubItems.Add(agv.Status);
                    listView.Items[listView.Items.Count - 1].SubItems.Add(agv.ExitNode.ToString());
                    listView.Items[listView.Items.Count - 1].SubItems.Add(agv.Orientation.ToString());
                    listView.Items[listView.Items.Count - 1].SubItems.Add(Math.Round(agv.DistanceToExitNode, 1).ToString() + " cm");
                    listView.Items[listView.Items.Count - 1].SubItems.Add(agv.Velocity.ToString() + " cm/s");
                }
                else
                {
                    listView.Items[listData.IndexOf(agv)].SubItems[0].Text = " AGV#" + agv.ID.ToString();
                    listView.Items[listData.IndexOf(agv)].SubItems[1].Text = agv.Status.ToString();
                    listView.Items[listData.IndexOf(agv)].SubItems[2].Text = agv.ExitNode.ToString();
                    listView.Items[listData.IndexOf(agv)].SubItems[3].Text = agv.Orientation.ToString();
                    listView.Items[listData.IndexOf(agv)].SubItems[4].Text = Math.Round(agv.DistanceToExitNode, 1).ToString() + " cm";
                    listView.Items[listData.IndexOf(agv)].SubItems[5].Text = agv.Velocity.ToString() + " cm/s";

                    if (agv.Status == "Running" || agv.Status == "Picking" || agv.Status == "Dropping")
                        listView.Items[listData.IndexOf(agv)].BackColor = Color.PaleGreen;
                    else if (!agv.IsInitialized)
                    {
                        agv.Status = "No initialize";
                        listView.Items[listData.IndexOf(agv)].BackColor = Color.LightGray;
                    }
                    else
                        listView.Items[listData.IndexOf(agv)].BackColor = Color.White;
                }
            }
        }

        // Update data in listView Tasks
        public static void UpdateListViewTasks(ListView listView, List<Task> listData)
        {
            if (listData.Count == 0) listView.Items.Clear();
            foreach (Task task in listData)
            {
                if (listView.Items.Count > listData.Count)
                {
                    listView.Items.Clear();
                }
                else if (listView.Items.Count < listData.Count)
                {
                    listView.Items.Add(task.Name, 1);
                    listView.Items[listView.Items.Count - 1].SubItems.Add(task.Status);
                    listView.Items[listView.Items.Count - 1].SubItems.Add(task.Type);
                    listView.Items[listView.Items.Count - 1].SubItems.Add("AGV#" + task.AGVID.ToString());
                    listView.Items[listView.Items.Count - 1].SubItems.Add("Node " + task.PickNode.ToString() + "-" + task.PickLevel.ToString());
                    listView.Items[listView.Items.Count - 1].SubItems.Add("Node " + task.DropNode.ToString() + "-" + task.DropLevel.ToString());
                    listView.Items[listView.Items.Count - 1].SubItems.Add(task.PalletCode);     
                }
                else
                {
                    listView.Items[listData.IndexOf(task)].SubItems[0].Text = task.Name;
                    listView.Items[listData.IndexOf(task)].SubItems[1].Text = task.Status;
                    listView.Items[listData.IndexOf(task)].SubItems[2].Text = task.Type;
                    listView.Items[listData.IndexOf(task)].SubItems[3].Text = "AGV#" + task.AGVID.ToString();
                    listView.Items[listData.IndexOf(task)].SubItems[4].Text = "Node " + task.PickNode.ToString() + "-" + task.PickLevel.ToString();
                    listView.Items[listData.IndexOf(task)].SubItems[5].Text = "Node " + task.DropNode.ToString() + "-" + task.DropLevel.ToString();
                    listView.Items[listData.IndexOf(task)].SubItems[6].Text = task.PalletCode;

                    if (task.Status == "Doing")
                        listView.Items[listData.IndexOf(task)].BackColor = Color.PaleGreen;
                    else
                        listView.Items[listData.IndexOf(task)].BackColor = Color.White;
                }
            }
        }

        // Updata communication status
        public static void UpdateComStatus(string type, int agvID, string message, Color messageColor)
        {
            string timeNow = DateTime.Now.ToString(" h:mm:ss.fff tt");

            HomeScreenForm.colorComStatus.Add(messageColor);
            if (type == "send")
                HomeScreenForm.textComStatus.Add(timeNow + "\t-> to AGV#" + agvID.ToString() + ":\t" + message + "\n");
            else if (type == "receive")
                HomeScreenForm.textComStatus.Add(timeNow + "\t<- from AGV#" + agvID.ToString() + ":\t" + message + "\n");
            else if (type == "timeout")
                HomeScreenForm.textComStatus.Add(timeNow + "\t...  AGV#" + agvID.ToString() + "\ttimeout" + "\n");
            else if (type == "status")
                HomeScreenForm.textComStatus.Add(timeNow + "\t" + message + "\n");
        }
    }
}
