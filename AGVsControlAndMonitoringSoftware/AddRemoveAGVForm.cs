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
    public partial class AddRemoveAGVForm : Form
    {
        public AddRemoveAGVForm()
        {
            InitializeComponent();
        }

        private List<AGV> listOldAGV = new List<AGV>();
        private List<AGV> listNewAGV = new List<AGV>();

        private void AddRemoveAGVForm_Load(object sender, EventArgs e)
        {
            switch (Display.Mode)
            {
                case "Real Time":
                    // Create a copy of current ListAGV
                    listOldAGV = new List<AGV>(AGV.ListAGV);
                    // Put existing AGV in ListAGV and listView
                    AGV.ListAGV.ForEach(agv => listViewAGV.Items.Add(" AGV#" + agv.ID, 0));
                    break;
                case "Simulation":
                    // Create a copy of current SimListAGV
                    listOldAGV = new List<AGV>(AGV.SimListAGV);
                    // Put existing AGV in SimListAGV and listView
                    AGV.SimListAGV.ForEach(agv => listViewAGV.Items.Add(" AGV#" + agv.ID, 0));
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txbID.Text) || String.IsNullOrEmpty(cbbExitNode.Text) ||
                String.IsNullOrEmpty(cbbOrientation.Text) || String.IsNullOrEmpty(txbDistance.Text))
                return;

            // Check whether AGV ID exist in old and new list or not
            foreach (AGV a in listOldAGV.Concat(listNewAGV).ToList())
            {
                if (Convert.ToInt16(txbID.Text) == a.ID)
                {
                    MessageBox.Show("AGV ID already exists.\nPlease choose other AGV ID.", "Error",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // If not exist, add new AGV into listNewAGV
            AGV agv = new AGV(Convert.ToInt16(txbID.Text), Convert.ToInt16(cbbExitNode.Text),
                              Convert.ToChar(cbbOrientation.Text), Convert.ToSingle(txbDistance.Text), "Stop");
            if (Display.Mode == "Simulation") agv.IsInitialized = true;
            listNewAGV.Add(agv);

            // Put new AGV ID in listView
            listViewAGV.Items.Add(" AGV#" + agv.ID, 0);

            // Clear textBox for next adding
            txbID.Clear();
            txbDistance.Clear();
        }

        private void cbbID_DropDown(object sender, EventArgs e)
        {
            // Display ID in combobox if AGV exist on listView
            cbbID.Items.Clear();
            foreach (AGV agv in listOldAGV.Concat(listNewAGV).ToList())
            {
                cbbID.Items.Add(agv.ID);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Remove an AGV has ID in comboBox
            if (String.IsNullOrEmpty(cbbID.Text) == false)
            {
                List<AGV> listAll = listOldAGV.Concat(listNewAGV).ToList();
                AGV agvToRemove = listAll.Find(a => { return a.ID == Convert.ToInt16(cbbID.Text); });
                if (listOldAGV.Contains(agvToRemove))
                    listOldAGV.Remove(agvToRemove);
                if (listNewAGV.Contains(agvToRemove))
                    listNewAGV.Remove(agvToRemove);
            }

            // Put remaining AGV in listView
            listViewAGV.Items.Clear();
            foreach (AGV agv in listOldAGV.Concat(listNewAGV).ToList())
            {
                listViewAGV.Items.Add(" AGV#" + agv.ID, 0);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Add infomation to list AGV
            switch (Display.Mode)
            {
                case "Real Time":
                    AGV.ListAGV = listOldAGV.Concat(listNewAGV).ToList();
                    break;
                case "Simulation":
                    AGV.SimListAGV = listOldAGV.Concat(listNewAGV).ToList();
                    break;
            }

            //Clear old list and new list for next time adding new AGV
            listOldAGV.Clear();
            listNewAGV.Clear();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            listOldAGV.Clear();
            listNewAGV.Clear();
            this.Close();
        }

        private void txbID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only allow integer number.", "AGV ID Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txbDistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != 46) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Only allow a float number.", "Distance Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
    }
}
