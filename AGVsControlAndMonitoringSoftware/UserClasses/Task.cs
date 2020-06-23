using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGVsControlAndMonitoringSoftware
{
    class Task
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string PalletCode { get; set; }
        public int AGVID { get; set; }
        public int PickNode { get; set; }
        public int DropNode { get; set; }
        public int PickLevel { get; set; }
        public int DropLevel { get; set; }
        public string Status { get; set; }

        // Constructor of Task
        public Task(string name, string type, string palletCode, int agvID,
                    int pickNode, int dropNode, int pickLevel, int dropLevel, string status)
        {
            this.Name = name;
            this.Type = type;
            this.PalletCode = palletCode;
            this.AGVID = agvID;
            this.PickNode = pickNode;
            this.DropNode = dropNode;
            this.PickLevel = pickLevel;
            this.DropLevel = dropLevel;
            this.Status = status;
        }

        // Information list of real-time Task
        public static List<Task> ListTask = new List<Task>();

        // Information list of simulation Task
        public static List<Task> SimListTask = new List<Task>();

        #region Add Paths for Real-time Mode

        // Add first path to each AGV of ListAGV (Real-Time Mode)
        public static void AddFirstPathOfAGVs()
        {
            foreach (AGV agv in AGV.ListAGV)
            {
                // Clear all path (this do not affect Task.ListTask)
                agv.Tasks.Clear();

                // Find all task of this AGV
                agv.Tasks = Task.ListTask.FindAll(t => t.AGVID == agv.ID);

                // if not having task or path has been initialized, skip to next AGV
                if (agv.Tasks.Count == 0 || agv.Path.Count != 0) continue;

                // Current node of agv isn't pick node of first task, add path bettwen them
                // Current node of agv is pick node, add path form pick node to drop node
                if (agv.ExitNode != agv.Tasks[0].PickNode)
                {
                    agv.Path = Algorithm.A_starFindPath(agv.ExitNode, agv.Tasks[0].PickNode);
                    string frame = Navigation.GetNavigationFrame(agv.Path, agv.Orientation, agv.DistanceToExitNode);
                    Communicator.SendPathData((uint)agv.ID, false, 0, frame, false, 0);
                }
                else
                {
                    agv.Path = Algorithm.A_starFindPath(agv.Tasks[0].PickNode, agv.Tasks[0].DropNode);
                    string frame = Navigation.GetNavigationFrame(agv.Path, agv.Orientation, agv.DistanceToExitNode);
                    Communicator.SendPathData((uint)agv.ID, true, agv.Tasks[0].PickLevel, frame, true, agv.Tasks[0].DropLevel);
                    agv.Tasks[0].Status = "Doing";
                }
            }
        }

        // Add next path to agv when previous path reach goal (agv.ExitNode is being goal) (Real-time Mode)
        public static void AddNextPathOfAGV(AGV agv)
        {
            // Clear old path
            agv.Path.Clear();

            // Remove old task
            if (agv.Tasks.Count != 0 && agv.ExitNode == agv.Tasks[0].DropNode)
            {
                // Store pallet code to ListColumn at this goal node
                RackColumn column = RackColumn.ListColumn.Find(c => c.AtNode == agv.ExitNode);
                column.PalletCodes[agv.Tasks[0].DropLevel - 1] = agv.Tasks[0].PalletCode;

                // Note: remove task in agv.Tasks and also in Task.ListTask
                Task.ListTask.Remove(Task.ListTask.Find(a => a.Name == agv.Tasks[0].Name));
                agv.Tasks.RemoveAt(0);
            }

            // Add path to parking when don't have any task
            if (agv.Tasks.Count == 0)
            {
                // find all parking node
                List<int> parkingNode = new List<int>();
                foreach (Node n in Node.ListNode)
                {
                    if (n.LocationCode.Length == 0) continue;
                    if (n.LocationCode[0] == 'P') parkingNode.Add(n.ID);
                }
                // If current node is parking node or don't have parking node for this agv, do nothing,
                // otherwise, add path to park agv (agv will park at location in order of index)
                if (parkingNode.Contains(agv.ExitNode)) return;
                int parkAtNode = parkingNode.Find(n => parkingNode.IndexOf(n) == AGV.ListAGV.IndexOf(agv));
                if (parkAtNode == 0) return; // be careful: in my node definition, parking nodes don't have node 0

                // Add navigation frame of parking path
                agv.Path = Algorithm.A_starFindPath(agv.ExitNode, parkAtNode);
                string frame = Navigation.GetNavigationFrame(agv.Path, agv.Orientation, agv.DistanceToExitNode);
                Communicator.SendPathData((uint)agv.ID, false, 0, frame, false, 0);

                return;
            }

            // Add next path
            if (agv.ExitNode != agv.Tasks[0].PickNode) 
            {
                agv.Path = Algorithm.A_starFindPath(agv.ExitNode, agv.Tasks[0].PickNode);
                string frame = Navigation.GetNavigationFrame(agv.Path, agv.Orientation, agv.DistanceToExitNode);
                Communicator.SendPathData((uint)agv.ID, false, 0, frame, false, 0);
            }    
            else
            {
                agv.Path = Algorithm.A_starFindPath(agv.Tasks[0].PickNode, agv.Tasks[0].DropNode);
                string frame = Navigation.GetNavigationFrame(agv.Path, agv.Orientation, agv.DistanceToExitNode);
                Communicator.SendPathData((uint)agv.ID, true, agv.Tasks[0].PickLevel, frame, true, agv.Tasks[0].DropLevel);
                agv.Tasks[0].Status = "Doing";
            }
        }

        #endregion

        #region Add Paths for Simulation Mode

        // Add first path to each AGV of SimListAGV (Simulation Mode)
        public static void AddFirstPathOfSimAGVs()
        {
            foreach (AGV agv in AGV.SimListAGV)
            {
                // Clear all path (this do not affect Task.SimListTask)
                agv.Tasks.Clear();

                // Find all task of this AGV
                agv.Tasks = Task.SimListTask.FindAll(t => t.AGVID == agv.ID);

                // if not having task or path has been initialized, skip to next AGV
                if (agv.Tasks.Count == 0 || agv.Path.Count != 0) continue;

                // Current node of agv isn't pick node of first task, add path bettwen them
                // Current node of agv is pick node, add path form pick node to drop node
                if (agv.ExitNode != agv.Tasks[0].PickNode)
                {
                    agv.Path = Algorithm.A_starFindPath(agv.ExitNode, agv.Tasks[0].PickNode);
                }
                else
                {
                    agv.Path = Algorithm.A_starFindPath(agv.Tasks[0].PickNode, agv.Tasks[0].DropNode);
                    agv.Tasks[0].Status = "Doing";
                }

                // Set init navigation array, first path is agv.Path
                string frame = Navigation.GetNavigationFrame(agv.Path, agv.Orientation, agv.DistanceToExitNode);
                agv.navigationArr = frame.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        // Add next path to agv when previous path reach goal (agv.ExitNode is being goal) (Simulation Mode)
        public static void AddNextPathOfSimAGV(AGV agv)
        {
            // Clear old path
            agv.Path.Clear();

            // Remove old task
            if (agv.Tasks.Count != 0 && agv.ExitNode == agv.Tasks[0].DropNode)
            {
                // Store pallet code to SimListColumn at this goal node
                RackColumn column = RackColumn.SimListColumn.Find(c => c.AtNode == agv.ExitNode);
                column.PalletCodes[agv.Tasks[0].DropLevel - 1] = agv.Tasks[0].PalletCode;

                // Note: remove task in agv.Tasks and also in Task.SimListTask
                Task.SimListTask.Remove(Task.SimListTask.Find(a => a.Name == agv.Tasks[0].Name));
                agv.Tasks.RemoveAt(0);
            }

            // Add path to parking when don't have any task
            if (agv.Tasks.Count == 0)
            {
                // find all parking node
                List<int> parkingNode = new List<int>();
                foreach (Node n in Node.ListNode)
                {
                    if (n.LocationCode.Length == 0) continue;
                    if (n.LocationCode[0] == 'P') parkingNode.Add(n.ID);
                }
                // If current node is parking node or don't have parking node for this agv, do nothing,
                // otherwise, add path to park agv (agv will park at location in order of index)
                if (parkingNode.Contains(agv.ExitNode)) return;
                int parkAtNode = parkingNode.Find(n => parkingNode.IndexOf(n) == AGV.SimListAGV.IndexOf(agv));
                if (parkAtNode == 0) return; // be careful: in my node definition, parking nodes don't have node 0
                agv.Path = Algorithm.A_starFindPath(agv.ExitNode, parkAtNode);

                // Add navigation frame of parking path
                string fr = Navigation.GetNavigationFrame(agv.Path, agv.Orientation, agv.DistanceToExitNode);
                agv.navigationArr = fr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                return;
            }

            // Add next path
            if (agv.ExitNode != agv.Tasks[0].PickNode)
                agv.Path = Algorithm.A_starFindPath(agv.ExitNode, agv.Tasks[0].PickNode);
            else
            {
                agv.Path = Algorithm.A_starFindPath(agv.Tasks[0].PickNode, agv.Tasks[0].DropNode);
                agv.Tasks[0].Status = "Doing";
            }

            // Add next navigation frame of next path
            string frame = Navigation.GetNavigationFrame(agv.Path, agv.Orientation, agv.DistanceToExitNode);
            agv.navigationArr = frame.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion
    }
}
