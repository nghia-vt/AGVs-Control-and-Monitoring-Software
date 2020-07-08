using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGVsControlAndMonitoringSoftware
{
    public partial class TaskMonitoringForm : Form
    {
        public TaskMonitoringForm()
        {
            InitializeComponent();
        }

        private void timerListView_Tick(object sender, EventArgs e)
        {
            switch (Display.Mode)
            {
                case "Real Time": Display.UpdateListViewTasks(listViewTasks, Task.ListTask); break;
                case "Simulation": Display.UpdateListViewTasks(listViewTasks, Task.SimListTask); break;
            }
        }
    }
}
