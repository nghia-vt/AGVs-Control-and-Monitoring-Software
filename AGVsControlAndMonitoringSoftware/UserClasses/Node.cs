using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGVsControlAndMonitoringSoftware
{
    class Node
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string[] AdjacentNode { get; set; }
        public string LocationCode { get; set; }

        // List of nodes (get from database). Note: listNode[i].ID = i so instead of using ID, use i 
        public static List<Node> ListNode = DBUtility.GetNodeInfoFromDB<List<Node>>("NodeInfoTable");
        //public static List<Node> ListNode = ManualGetNodeInfo();
        

        // Adjacency Matrix of nodes
        public static int[,] MatrixNodeDistance = Node.CreateAdjacencyMatrix(ListNode);

        // Orient Matrix of nodes
        public static char[,] MatrixNodeOrient = Node.CreateOrientMatrix(ListNode);

        // This method return the matrix of distance (pixel) from node to other node
        // d[i,j] = 0 indicate that no linking from node i to j
        private static int[,] CreateAdjacencyMatrix(List<Node> Nodes)
        {
            int[,] d = new int[Nodes.Count, Nodes.Count];
            for (int i = 0; i < Nodes.Count; i++)
            {
                foreach (string adj in Nodes[i].AdjacentNode)
                {
                    int j = Convert.ToInt32(adj);
                    d[i, j] = (int)Math.Sqrt(Math.Pow(Nodes[i].X - Nodes[j].X, 2) +
                                             Math.Pow(Nodes[i].Y - Nodes[j].Y, 2));
                    d[j, i] = d[i, j];
                }
            }

            return d;
        }

        // This method return the matrix of orient from node to other node
        // orient[i,j] indicate the direction from node i to node j
        private static char[,] CreateOrientMatrix(List<Node> Nodes)
        {
            char[,] orient = new char[Nodes.Count, Nodes.Count];
            for (int i = 0; i < Nodes.Count; i++)
            {
                foreach (string adj in Nodes[i].AdjacentNode)
                {
                    int j = Convert.ToInt32(adj);
                    int[,] vector = new int[1, 2];
                    int dX = (Nodes[j].X - Nodes[i].X);
                    int dY = (Nodes[j].Y - Nodes[i].Y);
                    vector[0, 0] = dX / (int)Math.Sqrt(dX * dX + dY * dY);
                    vector[0, 1] = dY / (int)Math.Sqrt(dX * dX + dY * dY);
                    switch (vector[0, 0].ToString() + vector[0, 1].ToString())
                    {
                        case "10": orient[i, j] = 'E'; break;
                        case "-10": orient[i, j] = 'W'; break;
                        case "01": orient[i, j] = 'S'; break;
                        case "0-1": orient[i, j] = 'N'; break;
                    }
                }
            }

            return orient;
        }
 
        private static List<Node> ManualGetNodeInfo()
        {
            /* uncomment: ListNode = DBUtility.GetNodeInfoFromDB<List<Node>>("NodeInfoTable");
             * comment: ListNode = ManualGetNodeInfo();
             * Add code below to HomeScreenForm form load event and run, then you have listNode.Add(...)
             * After that, comment: ListNode = DBUtility.GetNodeInfoFromDB<List<Node>>("NodeInfoTable");
             * uncomment: List<Node> ListNode = ManualGetNodeInfo();
             * In case you want to use directly from database, just use only ListNode = DBUtility.GetNodeInfoFromDB<List<Node>>("NodeInfoTable");
            foreach (Node n in Node.ListNode)
            {
                char x = '"';
                string str = "new string[]{";
                foreach (string it in n.AdjacentNode)
                {
                    if (it == n.AdjacentNode.LastOrDefault()) str += x.ToString() + it + x.ToString();
                    else str += x.ToString() + it + x.ToString() + ",";
                }
                str += "}";
                Console.Write("listNode.Add(new Node(){");
                Console.Write("ID = {0}, X = {1}, Y = {2}, AdjacentNode = {3}, LocationCode = " + x.ToString() + "{4}" + x.ToString(),
                                    n.ID, n.X, n.Y, str, n.LocationCode);
                Console.WriteLine("});");
            } */

            List<Node> listNode = new List<Node>();
            listNode.Add(new Node() { ID = 0, X = 40, Y = 40, AdjacentNode = new string[] { "1", "21" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 1, X = 127, Y = 40, AdjacentNode = new string[] { "0", "2", "9" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 2, X = 201, Y = 40, AdjacentNode = new string[] { "1", "3", "10" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 3, X = 275, Y = 40, AdjacentNode = new string[] { "2", "4", "11" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 4, X = 363, Y = 40, AdjacentNode = new string[] { "3", "5", "25" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 5, X = 449, Y = 40, AdjacentNode = new string[] { "4", "6", "12" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 6, X = 524, Y = 40, AdjacentNode = new string[] { "5", "7", "13" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 7, X = 598, Y = 40, AdjacentNode = new string[] { "6", "8", "14" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 8, X = 685, Y = 40, AdjacentNode = new string[] { "7", "29" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 9, X = 127, Y = 77, AdjacentNode = new string[] { "1" }, LocationCode = "A-1" });
            listNode.Add(new Node() { ID = 10, X = 201, Y = 77, AdjacentNode = new string[] { "2" }, LocationCode = "A-2" });
            listNode.Add(new Node() { ID = 11, X = 275, Y = 77, AdjacentNode = new string[] { "3" }, LocationCode = "A-3" });
            listNode.Add(new Node() { ID = 12, X = 449, Y = 77, AdjacentNode = new string[] { "5" }, LocationCode = "B-1" });
            listNode.Add(new Node() { ID = 13, X = 524, Y = 77, AdjacentNode = new string[] { "6" }, LocationCode = "B-2" });
            listNode.Add(new Node() { ID = 14, X = 598, Y = 77, AdjacentNode = new string[] { "7" }, LocationCode = "B-3" });
            listNode.Add(new Node() { ID = 15, X = 127, Y = 226, AdjacentNode = new string[] { "22" }, LocationCode = "A-4" });
            listNode.Add(new Node() { ID = 16, X = 201, Y = 226, AdjacentNode = new string[] { "23" }, LocationCode = "A-5" });
            listNode.Add(new Node() { ID = 17, X = 275, Y = 226, AdjacentNode = new string[] { "24" }, LocationCode = "A-6" });
            listNode.Add(new Node() { ID = 18, X = 449, Y = 226, AdjacentNode = new string[] { "26" }, LocationCode = "B-4" });
            listNode.Add(new Node() { ID = 19, X = 524, Y = 226, AdjacentNode = new string[] { "27" }, LocationCode = "B-5" });
            listNode.Add(new Node() { ID = 20, X = 598, Y = 226, AdjacentNode = new string[] { "28" }, LocationCode = "B-6" });
            listNode.Add(new Node() { ID = 21, X = 40, Y = 263, AdjacentNode = new string[] { "0", "22", "42" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 22, X = 127, Y = 263, AdjacentNode = new string[] { "15", "21", "23", "30" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 23, X = 201, Y = 263, AdjacentNode = new string[] { "16", "22", "24", "31" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 24, X = 275, Y = 263, AdjacentNode = new string[] { "17", "23", "25", "32" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 25, X = 363, Y = 263, AdjacentNode = new string[] { "4", "24", "26", "46" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 26, X = 449, Y = 263, AdjacentNode = new string[] { "18", "25", "27", "33" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 27, X = 524, Y = 263, AdjacentNode = new string[] { "19", "26", "28", "34" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 28, X = 598, Y = 263, AdjacentNode = new string[] { "20", "27", "29", "35" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 29, X = 685, Y = 263, AdjacentNode = new string[] { "8", "28", "50" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 30, X = 127, Y = 300, AdjacentNode = new string[] { "22" }, LocationCode = "C-1" });
            listNode.Add(new Node() { ID = 31, X = 201, Y = 300, AdjacentNode = new string[] { "23" }, LocationCode = "C-2" });
            listNode.Add(new Node() { ID = 32, X = 275, Y = 300, AdjacentNode = new string[] { "24" }, LocationCode = "C-3" });
            listNode.Add(new Node() { ID = 33, X = 449, Y = 300, AdjacentNode = new string[] { "26" }, LocationCode = "D-1" });
            listNode.Add(new Node() { ID = 34, X = 524, Y = 300, AdjacentNode = new string[] { "27" }, LocationCode = "D-2" });
            listNode.Add(new Node() { ID = 35, X = 598, Y = 300, AdjacentNode = new string[] { "28" }, LocationCode = "D-3" });
            listNode.Add(new Node() { ID = 36, X = 127, Y = 450, AdjacentNode = new string[] { "43" }, LocationCode = "C-4" });
            listNode.Add(new Node() { ID = 37, X = 201, Y = 450, AdjacentNode = new string[] { "44" }, LocationCode = "C-5" });
            listNode.Add(new Node() { ID = 38, X = 275, Y = 450, AdjacentNode = new string[] { "45" }, LocationCode = "C-6" });
            listNode.Add(new Node() { ID = 39, X = 449, Y = 450, AdjacentNode = new string[] { "47" }, LocationCode = "D-4" });
            listNode.Add(new Node() { ID = 40, X = 524, Y = 450, AdjacentNode = new string[] { "48" }, LocationCode = "D-5" });
            listNode.Add(new Node() { ID = 41, X = 598, Y = 450, AdjacentNode = new string[] { "49" }, LocationCode = "D-6" });
            listNode.Add(new Node() { ID = 42, X = 40, Y = 487, AdjacentNode = new string[] { "21", "43", "51" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 43, X = 127, Y = 487, AdjacentNode = new string[] { "36", "42", "44", "52" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 44, X = 201, Y = 487, AdjacentNode = new string[] { "37", "43", "45" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 45, X = 275, Y = 487, AdjacentNode = new string[] { "38", "44", "46" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 46, X = 363, Y = 487, AdjacentNode = new string[] { "25", "45", "47", "55" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 47, X = 449, Y = 487, AdjacentNode = new string[] { "39", "46", "48", "56" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 48, X = 524, Y = 487, AdjacentNode = new string[] { "40", "47", "49" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 49, X = 598, Y = 487, AdjacentNode = new string[] { "41", "48", "50" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 50, X = 685, Y = 487, AdjacentNode = new string[] { "29", "49", "54" }, LocationCode = "" });
            listNode.Add(new Node() { ID = 51, X = 40, Y = 562, AdjacentNode = new string[] { "42" }, LocationCode = "O-1" });
            listNode.Add(new Node() { ID = 52, X = 127, Y = 562, AdjacentNode = new string[] { "43" }, LocationCode = "O-2" });
            listNode.Add(new Node() { ID = 53, X = 598, Y = 562, AdjacentNode = new string[] { "49" }, LocationCode = "I-1" });
            listNode.Add(new Node() { ID = 54, X = 685, Y = 562, AdjacentNode = new string[] { "50" }, LocationCode = "I-2" });
            listNode.Add(new Node() { ID = 55, X = 363, Y = 562, AdjacentNode = new string[] { "46" }, LocationCode = "P-1" });
            listNode.Add(new Node() { ID = 56, X = 449, Y = 562, AdjacentNode = new string[] { "47" }, LocationCode = "P-2" });
            return listNode;
        }
    }
}
