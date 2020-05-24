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
        public static void AddLabelAGV(Panel panel, int agvID, int initExitNode, char initOrientation, int initDistanceToExitNode)
        {
            Label lbAGV = new Label();
            lbAGV.BackColor = Color.Silver;
            lbAGV.Font = new Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, 
                                   System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbAGV.Size = new Size(56, 56);
            lbAGV.Text = "AGV#" + agvID.ToString();
            lbAGV.TextAlign = ContentAlignment.MiddleCenter;
            lbAGV.Name = "AGV" + agvID.ToString();

            // Init the Location of new AGV
            List<Node> Nodes = Node.ListNode;
            int x = Nodes[initExitNode].X - lbAGV.Size.Width / 2;
            int y = Nodes[initExitNode].Y - lbAGV.Size.Height / 2;
            switch (initOrientation)
            {
                case 'E':
                    lbAGV.Location = new Point(x + initDistanceToExitNode, y);
                    break;
                case 'W':
                    lbAGV.Location = new Point(x - initDistanceToExitNode, y);
                    break;
                case 'N':
                    lbAGV.Location = new Point(x, y - initDistanceToExitNode);
                    break;
                case 'S':
                    lbAGV.Location = new Point(x, y + initDistanceToExitNode);
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

        // Update position AGV icon in simulation mode
        public static Point SimUpdatePositionAGV(int agvID, int speed)
        {
            // Find AGV in SimListAGV, get current point
            var index = AGV.SimListAGV.FindIndex(a => a.ID == agvID);
            AGV agv = AGV.SimListAGV[index];
            Point location = Display.SimLabelAGV[agvID].Location;

            // Get navigation frame array. Note: string is a reference type,
            // so any change in navigationArr is also in AGV.SimListAGV[index].navigationArr
            string[] navigationArr = agv.navigationArr;

            // return old point when agv has no path
            if (agv.Path.Count == 0) return location;

            char orient = new char();
            int sp = new int();
            // Check whether current point is a node or not 
            // Note: shift location of label to center (+LabelAGV[].Width/2, +LabelAGV[].Height/2)
            var node = Node.ListNode.FirstOrDefault(n =>
            {
                return (n.X == location.X + SimLabelAGV[agvID].Width / 2)
                    && (n.Y == location.Y + SimLabelAGV[agvID].Height / 2);
            });

            // Current point is not a node and current position is start node, 
            // it means initDistance to start node != 0, so go backward once then keep go ahead
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
            // If goal was reached, no update location, remove old path, get next path (if exist)
            else if (node.ID == agv.Path.LastOrDefault())
            {
                // Update AGV information
                agv.ExitNode = node.ID;  // Update ExitNode
                orient = UpdateOrient(agv.Orientation, "A");
                agv.Orientation = orient;  // Update Orientation
                agv.DistanceToExitNode = 0; // Update Distance to ExitNode
                agv.Status = "Stop"; // Update Status

                // Add next path
                Task.AddNextPathOfAGV(agv);
                
                return location;
            }
            // Current point is at start node and initDistance to start node == 0
            // Turn direction once then keep go ahead
            else if (node.ID == agv.Path[0] && agv.DistanceToExitNode == 0)
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

                // If this node is pick node, remove pallet code that was picked
                if (agv.Tasks.Count != 0 && node.ID == agv.Tasks[0].PickNode)
                {
                    RackColumn column = RackColumn.SimListColumn.Find(c => c.AtNode == agv.ExitNode);
                    column.PalletCodes[agv.Tasks[0].PickLevel - 1] = null;
                }
            }
            // Current point is a node but start node
            else
            {
                int idx = Array.FindIndex(navigationArr, item => item == node.ID.ToString());
                string dir = navigationArr[idx + 1];
                orient = UpdateOrient(agv.Orientation, dir);
            }

            // Modify speed to make sure AGV icon can reach next node
            int i = agv.Path.FindIndex(n => n == agv.ExitNode);
            int nextNode = agv.Path[i + 1];
            int dnx = (location.X + SimLabelAGV[agvID].Width / 2) - Node.ListNode[nextNode].X;
            int dny = (location.Y + SimLabelAGV[agvID].Height / 2) - Node.ListNode[nextNode].Y;
            int nd = (int)Math.Sqrt(dnx * dnx + dny * dny);
            
            if (agv.ExitNode == agv.Path[0])
            {
                // At first node of path, having 4 cases of agv location
                // (dnx * dny != 0) for 2 cases and (dnx * dnx0 > 0 || dny * dny0 > 0) for the others
                int dnx0 = (location.X + SimLabelAGV[agvID].Width / 2) - Node.ListNode[agv.Path[0]].X;
                int dny0 = (location.Y + SimLabelAGV[agvID].Height / 2) - Node.ListNode[agv.Path[0]].Y;
                int nd0 = (int)Math.Sqrt(dnx0 * dnx0 + dny0 * dny0);
                if (dnx * dny != 0 || dnx * dnx0 > 0 || dny * dny0 > 0)
                {
                    nextNode = agv.Path[0];
                    nd = nd0;
                }
            }
            sp = (nd % speed == 0) ? speed : (nd % speed);            

            // Update AGV information before update location
            if (node != null) agv.ExitNode = node.ID;  // Update ExitNode
            agv.Orientation = orient;  // Update Orientation
            int exitNode = agv.ExitNode;
            int dx = (location.X + SimLabelAGV[agvID].Width / 2) - Node.ListNode[exitNode].X;
            int dy = (location.Y + SimLabelAGV[agvID].Height / 2) - Node.ListNode[exitNode].Y;
            agv.DistanceToExitNode = (int)Math.Sqrt(dx * dx + dy * dy); // Update Distance to ExitNode
            agv.Status = "Running"; // Update Status

            // Update next location of AGV icon in panel
            switch (orient)
            {
                case 'E':
                    location = new Point(location.X + sp, location.Y);
                    break;
                case 'W':
                    location = new Point(location.X - sp, location.Y);
                    break;
                case 'S':
                    location = new Point(location.X, location.Y + sp);
                    break;
                case 'N':
                    location = new Point(location.X, location.Y - sp);
                    break;
            }

            return location;
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
                    listView.Items[listView.Items.Count - 1].SubItems.Add(agv.DistanceToExitNode.ToString());
                    listView.Items[listView.Items.Count - 1].SubItems.Add(agv.Velocity.ToString());
                }
                else
                {
                    listView.Items[listData.IndexOf(agv)].SubItems[0].Text = " AGV#" + agv.ID.ToString();
                    listView.Items[listData.IndexOf(agv)].SubItems[1].Text = agv.Status.ToString();
                    listView.Items[listData.IndexOf(agv)].SubItems[2].Text = agv.ExitNode.ToString();
                    listView.Items[listData.IndexOf(agv)].SubItems[3].Text = agv.Orientation.ToString();
                    listView.Items[listData.IndexOf(agv)].SubItems[4].Text = agv.DistanceToExitNode.ToString();
                    listView.Items[listData.IndexOf(agv)].SubItems[5].Text = agv.Velocity.ToString();
                    
                    if (agv.Status == "Running")
                        listView.Items[listData.IndexOf(agv)].BackColor = Color.PaleGreen;
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
                    listView.Items[listView.Items.Count - 1].SubItems.Add(task.Priority);
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
                    listView.Items[listData.IndexOf(task)].SubItems[6].Text = task.Priority;
                    listView.Items[listData.IndexOf(task)].SubItems[7].Text = task.PalletCode;

                    if (task.Status == "Doing")
                        listView.Items[listData.IndexOf(task)].BackColor = Color.PaleGreen;
                    else
                        listView.Items[listData.IndexOf(task)].BackColor = Color.White;
                }
            }
        }
    }
}
