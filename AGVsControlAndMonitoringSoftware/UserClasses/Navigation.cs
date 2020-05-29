using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGVsControlAndMonitoringSoftware
{
    class Navigation
    {
        // Create the navigation frame will be sent to actuator system
        public static string GetNavigationFrame(List<int> path, char initOrient, float initDistance)
        {
            List<Node> nodes = Node.ListNode;
            // Foreach node in path, calculate the vector to next node
            int[,] vector = new int[path.Count - 1, 2];
            for (int i = 0; i < path.Count - 1; i++) // don't calculate goal node 
            {
                int dX = (nodes[path[i + 1]].X - nodes[path[i]].X);
                int dY = (nodes[path[i + 1]].Y - nodes[path[i]].Y);
                vector[i, 0] = dX / (int)Math.Sqrt(dX * dX + dY * dY);
                vector[i, 1] = dY / (int)Math.Sqrt(dX * dX + dY * dY);
            }

            // Determin turn-direction of start node and add it to frame
            string frame = null;
            char startOrient = ConvertToOrient(vector[0, 0].ToString() + vector[0, 1].ToString());
            if (initDistance == 0f || startOrient == initOrient)
            {
                frame += GetDir(initOrient, startOrient) + ",";
            }
            else
            {
                initOrient = GetOppositeOrient(initOrient);
                frame += "B," + path[0].ToString() + "," + GetDir(initOrient, startOrient) + ",";
            }

            // Determin turn-direction other node and add it to frame
            for (int i = 0; i < path.Count - 2; i++)
            {
                char currentOrient = ConvertToOrient(vector[i, 0].ToString() + vector[i, 1].ToString());
                char nextOrient = ConvertToOrient(vector[i + 1, 0].ToString() + vector[i + 1, 1].ToString());

                frame += path[i + 1].ToString() + "," + GetDir(currentOrient, nextOrient) + ",";
            }

            // Add goal node
            frame += path[path.Count - 1] + ",G"; // Goal node
            return frame;
        }

        private static char ConvertToOrient(string vector)
        {
            if (vector == "10") return 'E';
            else if (vector == "-10") return 'W';
            else if (vector == "01") return 'S';
            else return 'N'; // vector == "0-1"
        }
        private static string GetDir(char currentOrient, char nextOrient)
        {
            // Case go Ahead
            if (nextOrient == currentOrient) return "A";
            else
            {
                // Case go Left, Right, Backward
                switch (nextOrient)
                {
                    case 'E':
                        if (currentOrient == 'S') return "L";
                        else if (currentOrient == 'N') return "R";
                        else return "B";
                    case 'S':
                        if (currentOrient == 'W') return "L";
                        else if (currentOrient == 'E') return "R";
                        else return "B";
                    case 'W':
                        if (currentOrient == 'N') return "L";
                        else if (currentOrient == 'S') return "R";
                        else return "B";
                    case 'N':
                        if (currentOrient == 'E') return "L";
                        else if (currentOrient == 'W') return "R";
                        else return "B";
                    default: return null;
                }
            }
        }

        private static char GetOppositeOrient(char orient)
        {
            if (orient == 'E') return 'W';
            else if (orient == 'W') return 'E';
            else if (orient == 'S') return 'N';
            else return 'S'; // orient == 'N'
        }
    }
}
