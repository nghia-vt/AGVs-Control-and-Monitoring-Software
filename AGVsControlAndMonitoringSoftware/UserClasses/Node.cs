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
    }
}
