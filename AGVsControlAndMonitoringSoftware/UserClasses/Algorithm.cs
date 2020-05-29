using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGVsControlAndMonitoringSoftware
{
    class Algorithm
    {
        // A* finds a path from start to goal.
        // h is the heuristic function. h(n) estimates the cost to reach goal from node n.
        // d(current,neighbor) is the weight of the edge from current to neighbor.
        public static List<int> A_starFindPath(int start, int goal)
        {
            List<Node> Nodes = Node.ListNode;
            int[,] d = Node.MatrixNodeDistance;
            int numOfNode = Nodes.Count;
            // List of nodes already discovered and explored. 
            // Starts off empty
            // Once a node has been 'current' it then goes here
            List<int> closeSet = new List<int>();

            // Initially, only the start node is known.
            List<int> openSet = new List<int>();
            openSet.Add(start);

            // For node n, cameFrom[n] is the node immediately preceding it
            // on the cheapest path from start to n currently known.
            int[] cameFrom = new int[numOfNode];

            // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.
            int[] gScore = new int[numOfNode];
            for (int i = 0; i < numOfNode; i++) { gScore[i] = int.MaxValue; }
            gScore[start] = 0;

            // For node n, fScore[n] = gScore[n] + h(n). fScore[n] represents our current best guess as to
            // how short a path from start to finish can be if it goes through n.
            int[] fScore = new int[numOfNode];
            for (int i = 0; i < numOfNode; i++) { fScore[i] = int.MaxValue; }
            fScore[start] = heuristic(Nodes, start, goal);

            while (openSet.Count != 0) // openSet is not empty
            {
                int current = lowest_fScore(fScore, openSet);

                if (current == goal)
                    return reconstruct_path(cameFrom, start, current);

                // Current node goes into the closed set
                closeSet.Add(current);
                openSet.Remove(current);

                // Find neighbor of current node
                List<int> neighborOfCurrent = new List<int>();
                for (int node = 0; node < numOfNode; node++)
                    if (d[current, node] != 0) { neighborOfCurrent.Add(node); }

                foreach (int neighbor in neighborOfCurrent)
                {
                    // tentative_gScore is the distance from start to the neighbor through current
                    int tentative_gScore = gScore[current] + d[current, neighbor];
                    if (tentative_gScore < gScore[neighbor])
                    {
                        // This path to neighbor is better than any previous one. Record it!
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentative_gScore;
                        fScore[neighbor] = gScore[neighbor] + heuristic(Nodes, neighbor, goal);
                        if (closeSet.Contains(neighbor) == false) // neighbor not in closeSet
                            openSet.Add(neighbor);
                    }
                }
            }

            // Open set is empty but goal was never reached
            return null;
        }

        // Find the node in openSet having the lowest fScore[] value.
        private static int lowest_fScore(int[] fScore, List<int> openSet)
        {
            int min = int.MaxValue, min_index = -1;

            foreach (int node in openSet)
            {
                if (fScore[node] <= min)
                {
                    min = fScore[node];
                    min_index = node;
                }
            }

            return min_index;
        }

        // Calculate heuristic function (Manhattan distance for 4 direction of moving)
        private static int heuristic(List<Node> Nodes, int current, int goal)
        {
            int dx = Math.Abs(Nodes[current].X - Nodes[goal].X);
            int dy = Math.Abs(Nodes[current].Y - Nodes[goal].Y);
            return dx + dy;
        }

        // Recontruct path
        private static List<int> reconstruct_path(int[] cameFrom, int start, int current)
        {
            List<int> totalPath = new List<int>();
            totalPath.Add(current);

            while (current != start)
            {
                current = cameFrom[current];
                totalPath.Add(current);
            }
            totalPath.Reverse();
            return totalPath;
        }
    }
}
