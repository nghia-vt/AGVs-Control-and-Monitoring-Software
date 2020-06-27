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
    public partial class WarehouseDataForm : Form
    {
        public WarehouseDataForm()
        {
            InitializeComponent();
        }

        private void WarehouseDataForm_Load(object sender, EventArgs e)
        {
            // View data on DataGridView
            dgvNodeInfo.DataSource = DBUtility.GetNodeInfoFromDB<DataTable>("NodeInfoTable");
            if (Display.Mode == "Real Time") dgvPalletInfo.DataSource = DBUtility.GetPalletInfoFromDB<DataTable>("PalletInfoTable");
            else if (Display.Mode == "Simulation") dgvPalletInfo.DataSource = DBUtility.GetPalletInfoFromDB<DataTable>("SimPalletInfoTable");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update data on DataGridView
            if (Display.Mode == "Real Time") dgvPalletInfo.DataSource = DBUtility.GetPalletInfoFromDB<DataTable>("PalletInfoTable");
            else if (Display.Mode == "Simulation") dgvPalletInfo.DataSource = DBUtility.GetPalletInfoFromDB<DataTable>("SimPalletInfoTable");
            
            // set color of pallet which is out of stock
            foreach (DataGridViewRow row in dgvPalletInfo.Rows)
            {
                if (Convert.ToBoolean(row.Cells[1].Value) == false) // cell[1] - InStock
                    row.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void myTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set color of pallet which is out of stock
            foreach (DataGridViewRow row in dgvPalletInfo.Rows)
            {
                if (Convert.ToBoolean(row.Cells[1].Value) == false) // cell[1] - InStock
                    row.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void dgvPalletInfo_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // set color of pallet which is out of stock
            foreach (DataGridViewRow row in dgvPalletInfo.Rows)
            {
                if (Convert.ToBoolean(row.Cells[1].Value) == false) // cell[1] - InStock
                    row.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPalletInfo.SelectedRows.Count < 1) return;
            DialogResult result = MessageBox.Show("Are you sure want to delete these pallets from database ?",
                                                      "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string tableName;
                if (Display.Mode == "Real Time") tableName = "PalletInfoTable";
                else if (Display.Mode == "Simulation") tableName = "SimPalletInfoTable";
                else return;

                foreach (DataGridViewRow row in dgvPalletInfo.SelectedRows)
                {
                    if (row.Cells[0].Value == null) break;
                    DBUtility.DeletePalletFromDB(tableName, row.Cells[0].Value.ToString());
                    dgvPalletInfo.Rows.RemoveAt(row.Index);
                } 
            }
        }

        private void WarehouseDataForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Clear all list pallet and column for update
            Pallet.ListPallet.Clear();
            Pallet.SimListPallet.Clear();
            RackColumn.ListColumn.ForEach(c => Array.Clear(c.PalletCodes, 0, 3));
            RackColumn.SimListColumn.ForEach(c => Array.Clear(c.PalletCodes, 0, 3));

            // Update existing pallets in stock and add to RackColumn
            Pallet.ListPallet = DBUtility.GetPalletInfoFromDB<List<Pallet>>("PalletInfoTable");
            RackColumn.InitializePallet(RackColumn.ListColumn, Pallet.ListPallet);

            Pallet.SimListPallet = DBUtility.GetPalletInfoFromDB<List<Pallet>>("SimPalletInfoTable");
            RackColumn.InitializePallet(RackColumn.SimListColumn, Pallet.SimListPallet);
        }
    }
}
