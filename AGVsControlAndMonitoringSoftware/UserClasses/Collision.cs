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
        public float WaitingTime { get; set; } // unit: second

        public Collision(CollisionStatus status, CollisionType type, int[] onAGVs, 
                         int[] at, DateTime happeningTime, float waitingTime)
        {
            this.Status = status;
            this.Type = type;
            this.OnAGVs = onAGVs;
            this.At = at;
            this.HappeningTime = happeningTime;
            this.WaitingTime = waitingTime;
        }

        // List of collisions
        public static List<Collision> SimListCollision = new List<Collision>();
        public static List<Collision> ListCollision = new List<Collision>();

        // Turning time for real-time mode
        private static int TurningTime_LR = 1; // second
        private static int TurningTime_B = 2; // second

        // Turning time for simulation mode
        private static int SimTurningTime = 0;

        // Collision detection
        public static void Detect(List<AGV> listAGV)
        {
            if (listAGV.Count < 2) return;

            // matrix of distance from node to other node (pixel)
            int[,] D = Node.MatrixNodeDistance;

            // Check each pair of agvs
            for (int i = 0; i < listAGV.Count - 1; i++)
            {
                for (int j = i + 1; j < listAGV.Count; j++)
                {
                    AGV agv1 = listAGV[i];
                    AGV agv2 = listAGV[j];

                    // if no path, do not detect 
                    if (agv1.Path.Count == 0 || agv2.Path.Count == 0) continue;

                    // predict coming time to remaining node on the path
                    List<DateTime> timePath1 = new List<DateTime>();
                    List<DateTime> timePath2 = new List<DateTime>();
                    DateTime timeNow = DateTime.Now;
                    timePath1 = PredictTimeOnPath(agv1, timeNow);
                    timePath2 = PredictTimeOnPath(agv2, timeNow);

                    // get remaining path of each agv
                    List<int> remainingPath1 = new List<int>();
                    List<int> remainingPath2 = new List<int>();
                    foreach (int n in agv1.Path)
                    {
                        // don't get node which is passed
                        if (agv1.Path.IndexOf(n) < agv1.Path.IndexOf(agv1.ExitNode)) continue;
                        remainingPath1.Add(n);
                    }
                    foreach (int n in agv2.Path)
                    {
                        // don't get node which is passed
                        if (agv2.Path.IndexOf(n) < agv2.Path.IndexOf(agv2.ExitNode)) continue;
                        remainingPath2.Add(n);
                    }
                    
                    List<int> remainingPath2_reverse = new List<int>(remainingPath2);
                    remainingPath2_reverse.Reverse();

                    // detect overlap nodes on two paths
                    List<int> overLapNodes = new List<int>();

                    for (int w_size = remainingPath1.Count; w_size > 0; w_size--)
                    {
                        for (int idx1 = 0; idx1 <= remainingPath1.Count - w_size; idx1++)
                        {
                            for (int idx2 = 0; idx2 <= remainingPath2_reverse.Count - w_size; idx2++)
                            {
                                var w1 = remainingPath1.GetRange(idx1, w_size);
                                var w2 = remainingPath2_reverse.GetRange(idx2, w_size);

                                if (w1.SequenceEqual(w2))
                                {
                                    overLapNodes = w1;
                                    break;
                                }
                            }
                            if (overLapNodes.Count > 0) break;
                        }
                        if (overLapNodes.Count > 0) break;
                    }
                    
                    // no overlapping
                    if (overLapNodes.Count < 1) continue;

                    // overall time of overlapping
                    int idxOverLapBegin1 = remainingPath1.IndexOf(overLapNodes[0]);
                    int idxOverLapEnd1 = remainingPath1.IndexOf(overLapNodes[overLapNodes.Count - 1]);
                    TimeSpan deltaTOverLap = timePath1[idxOverLapEnd1].Subtract(timePath1[idxOverLapBegin1]);

                    int idxOverLapBegin2 = remainingPath2.IndexOf(overLapNodes[overLapNodes.Count - 1]);
                    DateTime time1 = timePath1[idxOverLapBegin1];
                    DateTime time2 = timePath2[idxOverLapBegin2];
                    TimeSpan deltaT = time1.Subtract(time2).Duration();

                    // detect head-on collision
                    if (deltaT < deltaTOverLap)
                    {
                        // waiting for later AGV to come close to the overlapping zone
                        if ((time1 >= time2 && time1 > timeNow.AddSeconds(AGV.Length / AGV.SimSpeed)) ||
                            (time1 < time2 && time2 > timeNow.AddSeconds(AGV.Length / AGV.SimSpeed))) continue;

                        // AGV coming to overlap-begin-node later must wait or re-route (put at collision.OnAGVs[0])
                        int[] collisionOnAGVs = new int[2];
                        if (time1 >= time2) { collisionOnAGVs[0] = agv1.ID; collisionOnAGVs[1] = agv2.ID; }
                        else { collisionOnAGVs[0] = agv2.ID; collisionOnAGVs[1] = agv1.ID; }

                        // -----------add real-time later--------------
                        Collision collision = new Collision(CollisionStatus.New, CollisionType.HeadOn,
                                                            collisionOnAGVs, overLapNodes.ToArray(), DateTime.Now,
                                                            (float)deltaTOverLap.TotalSeconds + AGV.Length / AGV.SimSpeed);

                        // skip collision that exists
                        if (SimListCollision.Exists(c => c.Type == collision.Type &&
                                                         c.OnAGVs.SequenceEqual(collision.OnAGVs))) continue;
                        Collision.SimListCollision.Add(collision);
                    }

                    // detect creoss collision
                    else if (deltaTOverLap == TimeSpan.Zero) // overlap only one node
                    {
                        // find next node and distance to next node of agv1
                        int idx1 = agv1.Path.IndexOf(agv1.ExitNode);
                        if (idx1 == agv1.Path.Count - 1) continue; // at goal node
                        int nextNodeAGV1 = agv1.Path[idx1 + 1];
                        float disToNextNodeAGV1 = D[agv1.ExitNode, nextNodeAGV1] / Display.Scale - agv1.DistanceToExitNode;

                        // find next node and distance to next node of agv2
                        int idx2 = agv2.Path.IndexOf(agv2.ExitNode);
                        if (idx2 == agv2.Path.Count - 1) continue; // at goal node
                        int nextNodeAGV2 = agv2.Path[idx2 + 1];
                        float disToNextNodeAGV2 = D[agv2.ExitNode, nextNodeAGV2] / Display.Scale - agv2.DistanceToExitNode;

                        // conditions for cross collisions to occur
                        float delta = disToNextNodeAGV1 - disToNextNodeAGV2; // uint: cm
                        if (nextNodeAGV1 == nextNodeAGV2 && Math.Abs(delta) < AGV.Length)
                        {
                            // waiting for later AGV to come close to the collision node
                            if ((delta >= 0 && disToNextNodeAGV1 > AGV.Length) ||
                                (delta < 0 && disToNextNodeAGV2 > AGV.Length)) continue;

                            int[] collisionOnAGVs = new int[2];
                            if (delta >= 0) { collisionOnAGVs[0] = agv1.ID; collisionOnAGVs[1] = agv2.ID; }
                            else { collisionOnAGVs[0] = agv2.ID; collisionOnAGVs[1] = agv1.ID; }

                            // -----------add real-time later--------------
                            Collision collision = new Collision(CollisionStatus.New, CollisionType.Cross,
                                                                collisionOnAGVs, overLapNodes.ToArray(), DateTime.Now, 2.5f);

                            // skip collision that exists
                            if (SimListCollision.Exists(c => c.OnAGVs.SequenceEqual(collision.OnAGVs) ||
                                                             c.OnAGVs.SequenceEqual(new int[2] { collision.OnAGVs[1], collision.OnAGVs[0] })))
                                continue;
                            Collision.SimListCollision.Add(collision);
                        }
                    }
                }
            }
        }

        // Predict coming time to remaining node on the path (first element is now time of current node)
        private static List<DateTime> PredictTimeOnPath(AGV agv, DateTime timeNow)
        {
            // matrix of distance from node to other node (pixel)
            int[,] D = Node.MatrixNodeDistance;

            List<DateTime> timePath = new List<DateTime>();
            timePath.Add(timeNow);
            foreach (int n in agv.Path)
            {
                // don't predict node which is passed and current node
                if (agv.Path.IndexOf(n) <= agv.Path.IndexOf(agv.ExitNode)) continue;

                int pre_n = agv.Path[agv.Path.IndexOf(n) - 1];
                float disToPreNode;
                // disToPreNode is different in this case (pre_n is current node)
                if (agv.Path.IndexOf(n) == agv.Path.IndexOf(agv.ExitNode) + 1)
                    disToPreNode = D[agv.ExitNode, n] / Display.Scale - agv.DistanceToExitNode;
                else disToPreNode = D[pre_n, n] / Display.Scale;
                float time_Line = disToPreNode / AGV.SimSpeed;

                float time_Turning;
                int pre_idx = Array.IndexOf(agv.navigationArr, pre_n.ToString());
                string pre_dir = agv.navigationArr[pre_idx + 1];
                if (pre_dir == "L" || pre_dir == "R") time_Turning = SimTurningTime;
                else if (pre_dir == "B") time_Turning = SimTurningTime;
                else time_Turning = 0;

                timePath.Add(timePath[timePath.Count - 1].AddSeconds(time_Turning + time_Line));
            }
            return timePath;
        }

        // Handle Cross collision of Simulation Mode
        public static CollisionStatus SimHandle(AGV agv, List<Collision> listCollision)
        {
            // Check whether agv have cross colliosion (note: must use LastOfDefault)
            Collision collision = listCollision.LastOrDefault(c => c.OnAGVs[0] == agv.ID);
            if (collision == null) return CollisionStatus.None;

            // if have a new collision, change status to Handling and wait to handle
            if (collision.Status == CollisionStatus.New) collision.Status = CollisionStatus.Handling;

            // Wait to handling collision
            if (DateTime.Now.CompareTo(collision.HappeningTime.AddSeconds(collision.WaitingTime)) >= 0) // finish waiting
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
        HeadOn,
        NodeOccupancy
    }

    public enum CollisionStatus
    {
        New,
        Handling,
        None
    }
}
