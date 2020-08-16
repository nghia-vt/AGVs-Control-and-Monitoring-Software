using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGVsControlAndMonitoringSoftware
{
    class AGV
    {
        public int ID { get; set; }
        public int ExitNode { get; set; }
        public char Orientation { get; set; }
        public float DistanceToExitNode { get; set; } // unit: cm
        public string Status { get; set; }
        public float Velocity { get; set; } // unit: cm/s
        public int Battery { get; set; }  // uint: %
        public bool IsInitialized = false; // only use in Real Time mode
        
        // List of tasks, current task is Task[0], next tasks is after Task[0]
        public List<Task> Tasks = new List<Task>();

        // Current path of an AGV, will be removed when completed
        public List<int> Path = new List<int>();

        // This must be created immediately after assigning Path
        public string[] navigationArr = new string[] { };

        // Constructor of AGV with some initial information
        public AGV(int id, int initExitNode, char initOrientation, float distanceToExitNode, string status)
        {
            this.ID = id;
            this.ExitNode = initExitNode;
            this.Orientation = initOrientation;
            this.DistanceToExitNode = distanceToExitNode;
            this.Status = status;
        }

        // Length of AGV (unit: cm)
        public static float Length = 32;

        // Desire speed of AGVs (unit: cm/s)
        public static float Speed = 20f;
        public static float SimSpeed = 20f;

        // Assume that max mumber of AGV is 100
        public static int MaxNumOfAGVs = 100;

        // Information list of real-time AGV
        public static List<AGV> ListAGV = new List<AGV>();

        // Information list of simulation AGV
        public static List<AGV> SimListAGV = new List<AGV>();
    }
}
