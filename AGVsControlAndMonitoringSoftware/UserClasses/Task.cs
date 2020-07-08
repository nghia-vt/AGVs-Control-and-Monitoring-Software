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

                    // update button add pallet
                    if (agv.Tasks[0].PickNode == 53)
                        HomeScreenForm.isPickPalletInput1 = true;
                    else if (agv.Tasks[0].PickNode == 54)
                        HomeScreenForm.isPickPalletInput2 = true;
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
                // Store pallet code to ListColumn at this goal node and save this time
                RackColumn column = RackColumn.ListColumn.Find(c => c.AtNode == agv.ExitNode);
                column.PalletCodes[agv.Tasks[0].DropLevel - 1] = agv.Tasks[0].PalletCode;
                if (column.AtNode != 51 & column.AtNode != 52)
                {
                    Pallet pallet = new Pallet(agv.Tasks[0].PalletCode, true, 
                                               DateTime.Now.ToString("dddd, MMMM dd, yyyy  h:mm:ss tt"),
                                               column.Block, column.Number, agv.Tasks[0].DropLevel);
                    Pallet.ListPallet.Add(pallet);
                    DBUtility.InsertNewPalletToDB("PalletInfoTable", pallet.Code, pallet.InStock, pallet.StoreTime,
                                                   pallet.AtBlock, pallet.AtColumn, pallet.AtLevel);
                }

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

                // update button add pallet
                if (agv.Tasks[0].PickNode == 53)
                    HomeScreenForm.isPickPalletInput1 = true;
                else if (agv.Tasks[0].PickNode == 54)
                    HomeScreenForm.isPickPalletInput2 = true;
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

                    // update button add pallet
                    if (agv.Tasks[0].PickNode == 53)
                        HomeScreenForm.isPickSimPalletInput1 = true;
                    else if (agv.Tasks[0].PickNode == 54)
                        HomeScreenForm.isPickSimPalletInput2 = true;
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
                if (column.AtNode != 51 & column.AtNode != 52)
                {
                    Pallet pallet = new Pallet(agv.Tasks[0].PalletCode, true,
                                               DateTime.Now.ToString("dddd, MMMM dd, yyyy  h:mm:ss tt"),
                                               column.Block, column.Number, agv.Tasks[0].DropLevel);
                    Pallet.SimListPallet.Add(pallet);
                    DBUtility.InsertNewPalletToDB("SimPalletInfoTable", pallet.Code, pallet.InStock, pallet.StoreTime,
                                                   pallet.AtBlock, pallet.AtColumn, pallet.AtLevel);
                }
                
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

                // update button add pallet
                if (agv.Tasks[0].PickNode == 53)
                    HomeScreenForm.isPickSimPalletInput1 = true;
                else if (agv.Tasks[0].PickNode == 54)
                    HomeScreenForm.isPickSimPalletInput2 = true;
            }

            // Add next navigation frame of next path
            string frame = Navigation.GetNavigationFrame(agv.Path, agv.Orientation, agv.DistanceToExitNode);
            agv.navigationArr = frame.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion

        public static void InputAutoAdd(string palletCode, int pickNode, List<Task> listTaskToAdd, List<AGV> listAGV, List<RackColumn> listColumn)
        {
            // auto select agv
            if (listAGV.Count == 0) return;
            int agvID = AutoSelectAGV(listAGV);

            // auto select drop node & level
            int[] drop = AutoSelectDropNode(listTaskToAdd, listColumn); // drop[0] is drop node, drop[1] is drop level
            if (drop[0] == 0 || drop[1] == 0) return; // warehouse is full

            // add new task
            Task newTask = new Task("Auto " + palletCode, "Input", palletCode, agvID, pickNode, drop[0], 1, drop[1], "Waiting");
            listTaskToAdd.Add(newTask);
        }

        public static void OutputAutoAdd(string palletCode, List<Task> listTaskToAdd, List<AGV> listAGV, List<RackColumn> listColumn)
        {
            // auto select agv
            if (listAGV.Count == 0) return;
            int agvID = AutoSelectAGV(listAGV);

            // find pick node & level
            RackColumn col = listColumn.Find(c => c.PalletCodes.Contains(palletCode));
            int pickNode = col.AtNode;
            int pickLevel = Array.IndexOf(col.PalletCodes, palletCode) + 1;

            // select drop node (output1 or output2)
            int dropNode = agvID % 2 == 1 ? 51 : 52;

            Task newTask = new Task("Auto " + palletCode, "Output", palletCode, agvID, pickNode, dropNode, pickLevel, 1, "Waiting");
            listTaskToAdd.Add(newTask);
        }

        #region Function for auto select AGV

        private static int AutoSelectAGV(List<AGV> listAGV)
        {
            int selectedAGVID;
            List<int> totalDistanceAllTasks = new List<int>();

            foreach (AGV agv in listAGV)
            {
                // if this agv has no task, select this agv
                if (agv.Tasks.Count == 0)
                {
                    selectedAGVID = agv.ID;
                    return selectedAGVID;
                }

                // add total remaining distance to a list, totalDistanceAllTasks[i] for listAGV[i]
                totalDistanceAllTasks.Add(CalculateTotalRemainingDistance(agv));
            }

            // find agv has the least total remaining distance
            int indexMin = totalDistanceAllTasks.IndexOf(totalDistanceAllTasks.Min());
            selectedAGVID = listAGV[indexMin].ID;

            return selectedAGVID;
        }

        private static int CalculateTotalRemainingDistance(AGV agv)
        {
            int totalDistance = 0;

            // get list remaning path of this agv
            List<List<int>> listRemainingPath = new List<List<int>>();
            for (int i = 0; i < agv.Tasks.Count; i++)
            {
                if (i == 0 && agv.Tasks[0].Status == "Doing")
                    listRemainingPath.Add(Algorithm.A_starFindPath(agv.ExitNode, agv.Tasks[0].DropNode));
                else if (i == 0 && agv.Tasks[0].Status == "Waiting")
                {
                    listRemainingPath.Add(Algorithm.A_starFindPath(agv.ExitNode, agv.Tasks[0].PickNode));
                    listRemainingPath.Add(Algorithm.A_starFindPath(agv.Tasks[0].PickNode, agv.Tasks[0].DropNode));
                }
                else
                    listRemainingPath.Add(Algorithm.A_starFindPath(agv.Tasks[i].PickNode, agv.Tasks[i].DropNode));

                if (i < agv.Tasks.Count - 1)
                    listRemainingPath.Add(Algorithm.A_starFindPath(agv.Tasks[i].DropNode, agv.Tasks[i + 1].PickNode));
            }

            // calculate total distance of list remaining path
            foreach (List<int> path in listRemainingPath)
            {
                for (int i = 0; i < path.Count - 1; i++)
                    totalDistance += Node.MatrixNodeDistance[path[i], path[i + 1]];
            }

            return totalDistance;
        }

        #endregion

        #region Function for auto select drop node
        // Auto select drop node and rack level, return {0, 0} when warwhouse is full 
        private static int[] AutoSelectDropNode(List<Task> listTask, List<RackColumn> listColumn)
        {
            int[] dropNode = new int[2]; // dropNode[0] is drop node, dropNode[1] is drop level

            // select block has the least number of pallet
            string selectedBlock;
            int blockA_PalletCount = PalletCountInBlock(listTask, listColumn, "A");
            int blockB_PalletCount = PalletCountInBlock(listTask, listColumn, "B");
            int blockC_PalletCount = PalletCountInBlock(listTask, listColumn, "C");
            int blockD_PalletCount = PalletCountInBlock(listTask, listColumn, "D");
            if (blockA_PalletCount < 6 * 3) selectedBlock = "A"; // 6 column and 3 rack level in each block -> 18 pallet max
            else if (blockB_PalletCount < 6 * 3) selectedBlock = "B";
            else if (blockC_PalletCount < 6 * 3) selectedBlock = "C";
            else if (blockD_PalletCount < 6 * 3) selectedBlock = "D";
            else return new int[2] { 0, 0 }; // warehouse is full

            // find column number has the least pallet count
            int columnNumber = ColumnHasLeastPalletCount(listTask, listColumn, selectedBlock);

            // find node base on selected block & column number
            RackColumn col = listColumn.Find(c => c.Block == selectedBlock && c.Number == columnNumber);
            dropNode[0] = col.AtNode;
            
            // select level of that node
            for (int i = 0; i < col.PalletCodes.Length; i++)
            {
                List<int> dropLevelOnGoing = PalletOnGoing<List<int>>(col, listTask);

                // select level not have pallet code and no exist in dropLevelOnGoing
                if (col.PalletCodes[i] == null && dropLevelOnGoing.Contains(i + 1) == false)
                {
                    dropNode[1] = i + 1;
                    break;
                }
            }

            return dropNode;
        }

        private static int PalletCountInBlock(List<Task> listTask, List<RackColumn> listColumn, string block)
        {
            int palletCount = 0;
            List<RackColumn> columnsInBlock = listColumn.FindAll(c => c.Block == block);
            foreach (RackColumn col in columnsInBlock)
            {
                for (int i = 0; i < col.PalletCodes.Length; i++)
                {
                    if (col.PalletCodes[i] != null) palletCount++;
                }

                // count pallet ongoing
                palletCount += PalletOnGoing<int>(col, listTask);
            }
            return palletCount;
        }

        private static int ColumnHasLeastPalletCount(List<Task> listTask, List<RackColumn> listColumn, string block)
        {
            // list of pallet count at column (listColumn_PalletCount[i] for column number i + 1)
            List<int> listColumn_PalletCount = new List<int>();

            // count number of pallet on each column
            List<RackColumn> columns = listColumn.FindAll(c => c.Block == block);

            foreach (RackColumn col in columns)
            {
                int count = 0;
                for (int i = 0; i < col.PalletCodes.Length; i++)
                {
                    if (col.PalletCodes[i] != null) count++;
                }
                // count pallet ongoing
                count += PalletOnGoing<int>(col, listTask);
                listColumn_PalletCount.Add(count);
            }

            int minCount = listColumn_PalletCount.Min();
            if (minCount == listColumn_PalletCount[0]) return 1;
            else if (minCount == listColumn_PalletCount[1]) return 2;
            else if (minCount == listColumn_PalletCount[2]) return 3;
            else if (minCount == listColumn_PalletCount[3]) return 4;
            else if (minCount == listColumn_PalletCount[4]) return 5;
            else return 6;
        }

        public static dynamic PalletOnGoing<T>(RackColumn column, List<Task> listTask)
        {
            int count = 0;
            List<int> listDropLevel = new List<int>();

            List<Task> listTaskOnGoing = listTask.FindAll(t => t.DropNode == column.AtNode);
            count = listTaskOnGoing.Count;
            listTaskOnGoing.ForEach(t => listDropLevel.Add(t.DropLevel));

            if (typeof(T) == typeof(int)) return count;
            else if (typeof(T) == typeof(List<int>)) return listDropLevel;
            else return null;
        }

        #endregion
    }
}
