using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGVsControlAndMonitoringSoftware
{
    class Collision
    {
        public CollisionStatus Status { get; set; }
        public CollisionType Type { get; set; }
        public int[] OnAGVs { get; set; } // agv handle collision is OnAGVs[0]
        public int[] At { get; set; }
        public DateTime HappeningTime { get; set; }

        public Collision(CollisionStatus status, CollisionType type, int[] onAGVs, int[] at, DateTime happeningTime)
        {
            this.Status = status;
            this.Type = type;
            this.OnAGVs = onAGVs;
            this.At = at;
            this.HappeningTime = happeningTime;
        }

        // List of collisions
        public static List<Collision> SimListCollision = new List<Collision>();
        public static List<Collision> ListCollision = new List<Collision>();

        // Collision detection: Cross collision (online)
        public static void DerectCross(List<AGV> listAGV)
        {
            if (listAGV.Count < 2) return;

            // matrix of distance from node to other node (pixel)
            int[,] D = Node.MatrixNodeDistance;

            for (int i = 0; i < listAGV.Count - 1; i++)
            {
                for (int j = i + 1; j < listAGV.Count; j++)
                {
                    AGV agv1 = listAGV[i];
                    AGV agv2 = listAGV[j];

                    // if no path, do not detect 
                    if (agv1.Path.Count == 0 || agv2.Path.Count == 0) continue;

                    // find next node and distance to next node of agv1
                    int idx1 = agv1.Path.FindIndex(n => n == agv1.ExitNode);
                    if (idx1 == agv1.Path.Count - 1) continue; // at goal node
                    int nextNodeAGV1 = agv1.Path[idx1 + 1];
                    float disToNextNodeAGV1 = D[agv1.ExitNode, nextNodeAGV1] / Display.Scale - agv1.DistanceToExitNode;

                    // find next node and distance to next node of agv2
                    int idx2 = agv2.Path.FindIndex(n => n == agv2.ExitNode);
                    if (idx2 == agv2.Path.Count - 1) continue; // at goal node
                    int nextNodeAGV2 = agv2.Path[idx2 + 1];
                    float disToNextNodeAGV2 = D[agv2.ExitNode, nextNodeAGV2] / Display.Scale - agv2.DistanceToExitNode;

                    // detect cross collision, add this collision to a list
                    float delta = disToNextNodeAGV1 - disToNextNodeAGV2; // uint: cm
                    if (nextNodeAGV1 == nextNodeAGV2 && Math.Abs(delta) < AGV.Length)
                    {
                        if ((delta >= 0 && disToNextNodeAGV1 > AGV.Length) ||
                            (delta < 0 && disToNextNodeAGV2 > AGV.Length)) continue;

                        // AGV coming to collision-node later must wait (put at collision.OnAGVs[0])
                        int[] collisionOnAGVs = new int[2];
                        if (delta >= 0) { collisionOnAGVs[0] = agv1.ID; collisionOnAGVs[1] = agv2.ID;}
                        else { collisionOnAGVs[0] = agv2.ID; collisionOnAGVs[1] = agv1.ID; }

                        // -----------add real-time later--------------
                        Collision collision = new Collision(CollisionStatus.New, CollisionType.Cross, 
                                                            collisionOnAGVs, new int[]{nextNodeAGV1}, DateTime.Now);
                        Collision.SimListCollision.Add(collision);
                    }
                }
            }
        }

        // Handle Cross collision of Simulation Mode
        public static CollisionStatus SimHandleCross(AGV agv, List<Collision> listCollision)
        {
            // Check whether agv have colliosion (note: must use LastOfDefault)
            Collision collision = listCollision.LastOrDefault(c => c.OnAGVs[0] == agv.ID);
            if (collision == null) return CollisionStatus.None;

            // if have a new collision, change status to Handling and wait to handle
            if (collision.Status == CollisionStatus.New) collision.Status = CollisionStatus.Handling;

            // Wait for 2.5 second
            if (DateTime.Now.CompareTo(collision.HappeningTime.AddSeconds(2.5)) >= 0) // finish waiting
            {
                collision.Status = CollisionStatus.None;
                listCollision.Remove(collision);
            }

            return collision.Status;
        }
    }

    public enum CollisionType
    {
        Cross,
        HeadOnLine,
        HeadOnNode,
        NodeOccupancy
    }

    public enum CollisionStatus
    {
        New,
        Handling,
        None
    }
}
