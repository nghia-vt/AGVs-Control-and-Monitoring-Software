﻿using System;
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
        public int DistanceToExitNode { get; set; }
        public string Status { get; set; }
        public float Velocity { get; set; }
        
        public List<Task> Tasks = new List<Task>();

        // Current path of an AGV, will be removed when completed
        public List<int> Path = new List<int>();

        // This must be created immediately after assigning Path
        public string[] navigationArr = new string[] { };

        // Constructor of AGV with some initial information
        public AGV(int id, int initExitNode, char initOrientation, int distanceToExitNode, string status)
        {
            this.ID = id;
            this.ExitNode = initExitNode;
            this.Orientation = initOrientation;
            this.DistanceToExitNode = distanceToExitNode;
            this.Status = status;
        }

        // Assume that max mumber of AGV is 100
        public static int MaxNumOfAGVs = 100;

        // Information list of real-time AGV
        public static List<AGV> ListAGV = new List<AGV>();

        // Information list of simulation AGV
        public static List<AGV> SimListAGV = new List<AGV>();
    }
}