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
        public string Priority { get; set; }
        public string PalletCode { get; set; }
        public int AGVID { get; set; }
        public int PickNode { get; set; }
        public int DropNode { get; set; }
        public int PickLevel { get; set; }
        public int DropLevel { get; set; }
        public string Status { get; set; }

        // Constructor of Task
        public Task(string name, string type, string priority, string palletCode, int agvID,
                    int pickNode, int dropNode, int pickLevel, int dropLevel, string status)
        {
            this.Name = name;
            this.Type = type;
            this.Priority = priority;
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

        // Add first path to each AGV of list AGV
        public static void AddFirstPathOfAGVs(List<AGV> listAGV)
        {
            foreach (AGV agv in listAGV)
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
                    agv.Path.Add(Algorithm.A_starFindPath(agv.ExitNode, agv.Tasks[0].PickNode));
                }
                else
                {
                    agv.Path.Add(Algorithm.A_starFindPath(agv.Tasks[0].PickNode, agv.Tasks[0].DropNode));
                    agv.Tasks[0].Status = "Doing";
                }

                // Set init navigation array, first path is agv.Path[0]
                string frame = Navigation.GetNavigationFrame(agv.Path[0], agv.Orientation, agv.DistanceToExitNode);
                agv.navigationArr = frame.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        // Add next path to agv when previous path reach goal (agv.ExitNode is being goal)
        public static void AddNextPathOfAGV(AGV agv)
        {
            // Remove old path, next path will be at agv.Path[0] again
            // because of List<> characteristic
            agv.Path.RemoveAt(0);

            // Remove old task
            if (agv.ExitNode == agv.Tasks[0].DropNode)
            {
                // Store pallet code to SimListColumn at this goal node
                RackColumn column = RackColumn.SimListColumn.Find(c => c.AtNode == agv.ExitNode);
                column.PalletCodes[agv.Tasks[0].DropLevel - 1] = agv.Tasks[0].PalletCode;

                // Note: remove task in agv.Tasks and also in Task.SimListTask
                Task.SimListTask.Remove(Task.SimListTask.Find(a => a.Name == agv.Tasks[0].Name));
                agv.Tasks.RemoveAt(0);
            }

            // Do nothing when don't have task
            if (agv.Tasks.Count == 0) return;

            // Add next path
            if (agv.ExitNode != agv.Tasks[0].PickNode)
                agv.Path.Add(Algorithm.A_starFindPath(agv.ExitNode, agv.Tasks[0].PickNode));
            else
            {
                agv.Path.Add(Algorithm.A_starFindPath(agv.Tasks[0].PickNode, agv.Tasks[0].DropNode));
                agv.Tasks[0].Status = "Doing";
            }

            // Add next navigation frame of next path
            string frame = Navigation.GetNavigationFrame(agv.Path[0], agv.Orientation, agv.DistanceToExitNode);
            agv.navigationArr = frame.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
