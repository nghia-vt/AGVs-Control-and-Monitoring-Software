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
    }
}
