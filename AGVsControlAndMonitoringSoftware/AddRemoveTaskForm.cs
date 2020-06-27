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
    public partial class AddRemoveTaskForm : Form
    {
        public AddRemoveTaskForm()
        {
            InitializeComponent();
        }

        private List<Task> listOldTask = new List<Task>();
        private List<Task> listNewTask = new List<Task>();

        private void AddRemoveTaskForm_Load(object sender, EventArgs e)
        {
            switch (Display.Mode)
            {
                case "Real Time":
                    // Create a copy of current ListTask
                    listOldTask = new List<Task>(Task.ListTask);
                    // Put existing Task in ListTask and listViewTask
                    foreach (Task task in Task.ListTask)
                    {
                        listViewTask.Items.Add(task.Name, 0);
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add(task.Type);
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add(task.PalletCode);
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("AGV#" + task.AGVID.ToString());
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("Node " + task.PickNode.ToString() + "-" + task.PickLevel.ToString());
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("Node " + task.DropNode.ToString() + "-" + task.DropLevel.ToString());
                    }
                    break;
                case "Simulation":
                    // Create a copy of current SimListTask
                    listOldTask = new List<Task>(Task.SimListTask);
                    // Put existing Task in SimListTask and listViewTask
                    foreach (Task task in Task.SimListTask)
                    {
                        listViewTask.Items.Add(task.Name, 0);
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add(task.Type);
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add(task.PalletCode);
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("AGV#" + task.AGVID.ToString());
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("Node " + task.PickNode.ToString() + "-" + task.PickLevel.ToString());
                        listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("Node " + task.DropNode.ToString() + "-" + task.DropLevel.ToString());
                    }
                    break;
            }
        }

        private void cbbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbType.Text == "Input")
            {
                // Hide checkBox at pallet code
                checkBox.Visible = false;
                txbPalletCode.Clear();
                txbPalletCode.ReadOnly = false;
                cbbPickNode.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (cbbType.Text == "Output")
            {
                // Show checkBox at pallet code
                checkBox.Visible = true;
                txbPalletCode.Clear();
                if (checkBox.Checked == false)
                {
                    txbPalletCode.ReadOnly = true;
                    cbbPickNode.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                else
                {
                    txbPalletCode.ReadOnly = false;
                    cbbPickNode.DropDownStyle = ComboBoxStyle.Simple;
                }
            }
        }

        private void cbbAGV_DropDown(object sender, EventArgs e)
        {
            // Display AGV in combobox if AGV exist
            cbbAGV.Items.Clear();
            
            switch (Display.Mode)
            {
                case "Real Time":
                    AGV.ListAGV.ForEach(agv => cbbAGV.Items.Add("AGV#" + agv.ID));
                    break;
                case "Simulation":
                    AGV.SimListAGV.ForEach(agv => cbbAGV.Items.Add("AGV#" + agv.ID));
                    break;
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            txbPalletCode.Clear();
            if (checkBox.Checked == false)
            {
                txbPalletCode.ReadOnly = true;
                cbbPickNode.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                txbPalletCode.ReadOnly = false;
                cbbPickNode.DropDownStyle = ComboBoxStyle.Simple;
            }
        }

        private void cbbPickNode_DropDown(object sender, EventArgs e)
        {
            List<RackColumn> listColumn = new List<RackColumn>();
            switch (Display.Mode)
            {
                case "Real Time": listColumn = RackColumn.ListColumn;
                    break;
                case "Simulation": listColumn = RackColumn.SimListColumn;
                    break;
            }

            if (cbbType.Text == "Input")
            {
                // Add pick node items into comboBox
                cbbPickNode.Items.Clear();
                cbbPickNode.Items.AddRange(new string[2] { "53-1", "54-1" });
            }
            else if (cbbType.Text == "Output")
            {
                cbbPickNode.Items.Clear();

                // If use pallet code to pick, only clear all items
                if (checkBox.Checked == true) return;

                // If don't use pallet code to pick, add pick node items into comboBox
                foreach (RackColumn column in listColumn)
                {
                    if (column.AtNode == 51 || column.AtNode == 52 || column.AtNode == 53 || column.AtNode == 54) continue;
                    for (int i = 0; i < column.PalletCodes.Length; i++)
                    {
                        if (column.PalletCodes[i] == null) continue;
                        cbbPickNode.Items.Add(column.AtNode.ToString() + "-" + (i + 1).ToString());
                    }
                }
            }
        }

        private void cbbDropNode_DropDown(object sender, EventArgs e)
        {
            List<RackColumn> listColumn = new List<RackColumn>();
            List<Task> listTask = new List<Task>();
            switch (Display.Mode)
            {
                case "Real Time":
                    listColumn = RackColumn.ListColumn;
                    listTask = Task.ListTask;
                    break;
                case "Simulation":
                    listColumn = RackColumn.SimListColumn;
                    listTask = Task.SimListTask;
                    break;
            }

            if (cbbType.Text == "Input")
            {
                cbbDropNode.Items.Clear();

                // Add drop node items into comboBox
                foreach (RackColumn column in listColumn)
                {
                    if (column.AtNode == 51 || column.AtNode == 52 || column.AtNode == 53 || column.AtNode == 54) continue;
                    for (int i = 0; i < column.PalletCodes.Length; i++)
                    {
                        // exclude the level existing or having in task list
                        List<int> onGoingDropLevel = Task.PalletOnGoing<List<int>>(column, listTask);
                        if (column.PalletCodes[i] != null || onGoingDropLevel.Contains(i + 1)) continue;
                        cbbDropNode.Items.Add(column.AtNode.ToString() + "-" + (i + 1).ToString());
                    }
                }
            }
            else if (cbbType.Text == "Output")
            {
                // Add drop node items into comboBox
                cbbDropNode.Items.Clear();
                cbbDropNode.Items.AddRange(new string[2] { "51-1", "52-1" });
            }
        }

        private void cbbPickNode_DropDownClosed(object sender, EventArgs e)
        {
            // If task type is Input, or type is Output and use palletcode to pick, do notthing
            if (cbbType.Text == "Input") return;
            if (cbbType.Text == "Output" && checkBox.Checked == true) return;

            List<RackColumn> listColumn = new List<RackColumn>();
            switch (Display.Mode)
            {
                case "Real Time": listColumn = RackColumn.ListColumn;
                    break;
                case "Simulation": listColumn = RackColumn.SimListColumn;
                    break;
            }

            // If task type is Output, get palletCode and put it in txbPalletCode
            string[] textArr = cbbPickNode.Text.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (textArr.Length == 0) return;

            RackColumn column = listColumn.Find(c => c.AtNode.ToString() == textArr[0]); // textArr[0] is node
            txbPalletCode.Text = column.PalletCodes[Convert.ToInt16(textArr[1]) - 1]; // textArr[1] is level
        }

        private void txbPalletCode_TextChanged(object sender, EventArgs e)
        {
            // Don't use this event for input task
            if (cbbType.Text == "Input") return;
            // Don't use this event for output task with no using pick pallet code
            if (cbbType.Text == "Output" && checkBox.Checked == false) return;

            // Don't use drop down on cbbPickNode
            cbbPickNode.DropDownStyle = ComboBoxStyle.Simple;

            List<RackColumn> listColumn = new List<RackColumn>();
            switch (Display.Mode)
            {
                case "Real Time": listColumn = RackColumn.ListColumn;
                    break;
                case "Simulation": listColumn = RackColumn.SimListColumn;
                    break;
            }

            // Find the column has this pallet code
            foreach (RackColumn column in listColumn)
            {
                string atNodeStr = null;
                if (column.AtNode == 51 || column.AtNode == 52 || column.AtNode == 53 || column.AtNode == 54) continue;
                for (int i = 0; i < column.PalletCodes.Length; i++)
                {
                    if (column.PalletCodes[i] == null) continue;
                    if (column.PalletCodes[i] == txbPalletCode.Text)
                    {
                        atNodeStr = column.AtNode.ToString() + "-" + (i + 1).ToString();
                        break;
                    }
                }
                cbbPickNode.Text = atNodeStr;
                if (atNodeStr != null) break;
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txbTaskName.Text) || String.IsNullOrEmpty(cbbType.Text) ||
                String.IsNullOrEmpty(txbPalletCode.Text) || String.IsNullOrEmpty(cbbAGV.Text) || 
                String.IsNullOrEmpty(cbbPickNode.Text) || String.IsNullOrEmpty(cbbDropNode.Text))
                return;

            // Check whether TaskName exist in old and new list or not
            foreach (Task t in listOldTask.Concat(listNewTask).ToList())
            {
                if (txbTaskName.Text == t.Name)
                {
                    MessageBox.Show("Task Name already exists.\nPlease choose other Task Name.", "Error",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // If task type is input, check whether PalletCode exist or not
            if (cbbType.Text == "Input")
            {
                foreach (Task t in listOldTask.Concat(listNewTask).ToList())
                {
                    if (txbPalletCode.Text == t.PalletCode)
                    {
                        MessageBox.Show("Pallet Code already exists.\nPlease choose other Pallet Code.", "Error",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                List<RackColumn> listColumn = new List<RackColumn>();
                switch (Display.Mode)
                {
                    case "Real Time": listColumn = RackColumn.ListColumn;
                        break;
                    case "Simulation": listColumn = RackColumn.SimListColumn;
                        break;
                }

                // check whether PalletCode exist in listColumn or not
                foreach (RackColumn column in listColumn)
                {
                    if (column.AtNode == 51 || column.AtNode == 52 || column.AtNode == 53 || column.AtNode == 54) continue;
                    for (int i = 0; i < column.PalletCodes.Length; i++)
                    {
                        if (column.PalletCodes[i] == null) continue;
                        if (column.PalletCodes[i] == txbPalletCode.Text)
                        {
                            MessageBox.Show("Pallet Code already exists.\nPlease choose other Pallet Code.", "Error",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            // If not exist, add new Task into listNewTask
            string[] agvID = cbbAGV.Text.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            string[] pickAt = cbbPickNode.Text.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            string[] dropAt = cbbDropNode.Text.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            Task task = new Task(txbTaskName.Text, cbbType.Text, txbPalletCode.Text, 
                                 Convert.ToInt16(agvID[1]), Convert.ToInt16(pickAt[0]), Convert.ToInt16(dropAt[0]),
                                 Convert.ToInt16(pickAt[1]), Convert.ToInt16(dropAt[1]), "Waiting");
            listNewTask.Add(task);

            // Put new Task in listView
            listViewTask.Items.Add(task.Name, 0);
            listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add(task.Type);
            listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add(task.PalletCode);
            listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("AGV#" + task.AGVID.ToString());
            listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("Node " + task.PickNode.ToString() + "-" + task.PickLevel.ToString());
            listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("Node " + task.DropNode.ToString() + "-" + task.DropLevel.ToString());

            // Clear textBox for next adding
            txbTaskName.Clear();
            txbPalletCode.Clear();
            txbTaskName.Clear();
        }

        private void cbbTaskName_DropDown(object sender, EventArgs e)
        {
            // Display TaskName in combobox if TaskName exist on listView
            cbbTaskName.Items.Clear();
            foreach (Task task in listOldTask.Concat(listNewTask).ToList())
            {
                cbbTaskName.Items.Add(task.Name);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Remove a Task has Name in comboBox
            if (String.IsNullOrEmpty(cbbTaskName.Text) == false)
            {
                List<Task> listAll = listOldTask.Concat(listNewTask).ToList();
                Task taskToRemove = listAll.Find(t => { return t.Name == cbbTaskName.Text; });
                if (listOldTask.Contains(taskToRemove))
                    listOldTask.Remove(taskToRemove);
                if (listNewTask.Contains(taskToRemove))
                    listNewTask.Remove(taskToRemove);
            }

            // Put remaining Task in listView
            listViewTask.Items.Clear();
            foreach (Task task in listOldTask.Concat(listNewTask).ToList())
            {
                listViewTask.Items.Add(task.Name, 0);
                listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add(task.Type);
                listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add(task.PalletCode);
                listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("AGV#" + task.AGVID.ToString());
                listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("Node " + task.PickNode.ToString() + "-" + task.PickLevel.ToString());
                listViewTask.Items[listViewTask.Items.Count - 1].SubItems.Add("Node " + task.DropNode.ToString() + "-" + task.DropLevel.ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Add infomation to list Task
            switch (Display.Mode)
            {
                case "Real Time":
                    // Clear done task during openning this form
                    if (Task.ListTask.Count < listOldTask.Count)
                        listOldTask.RemoveRange(0, listOldTask.Count - Task.ListTask.Count);
                    Task.ListTask = listOldTask.Concat(listNewTask).ToList();
                    break;
                case "Simulation":
                    // Clear done task during openning this form
                    if (Task.SimListTask.Count < listOldTask.Count) 
                        listOldTask.RemoveRange(0, listOldTask.Count - Task.SimListTask.Count);
                    Task.SimListTask = listOldTask.Concat(listNewTask).ToList();
                    break;
            }

            //Clear old list and new list for next time adding new Task
            listOldTask.Clear();
            listNewTask.Clear();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            listOldTask.Clear();
            listNewTask.Clear();
            this.Close();
        }

        private void cbbPickNode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This will discard the keypress (can also use e.Handled = true)
            e.KeyChar = (char)Keys.None;
        }

        private void cbbPickNode_KeyDown(object sender, KeyEventArgs e)
        {
            // This will discard the delete keypress (can also use e.Handled = true)
            if (e.KeyCode == Keys.Delete) e.Handled = true;
        }

    }
}
