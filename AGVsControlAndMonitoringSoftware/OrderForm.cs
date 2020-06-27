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
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            // collect pallet in stock
            List<Pallet> palletsInStock = new List<Pallet>();
            switch (Display.Mode)
            {
                case "Real Time": palletsInStock = Pallet.ListPallet.FindAll(p => p.InStock == true); break;
                case "Simulation": palletsInStock = Pallet.SimListPallet.FindAll(p => p.InStock == true); break;
            }

            // add to list view
            foreach (Pallet pallet in palletsInStock)
            {
                lstvwPalletInStock.Items.Add(pallet.Code, 0);
                lstvwPalletInStock.Items[lstvwPalletInStock.Items.Count - 1].SubItems.Add(pallet.StoreTime);
                lstvwPalletInStock.Items[lstvwPalletInStock.Items.Count - 1].SubItems.Add(pallet.AtBlock + "-" + pallet.AtColumn.ToString() + "-" + pallet.AtLevel.ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // collect all selected pallet code
            List<string> selectedPalletCode = new List<string>();
            foreach (ListViewItem item in lstvwPalletInStock.CheckedItems) selectedPalletCode.Add(item.Text);

            // auto add task
            foreach (string palletCode in selectedPalletCode)
            {
                switch (Display.Mode)
                {
                    case "Real Time":
                        Task.OutputAutoAdd(palletCode, Task.ListTask, AGV.ListAGV, RackColumn.ListColumn);
                        Task.AddFirstPathOfAGVs();
                        break;
                    case "Simulation":
                        Task.OutputAutoAdd(palletCode, Task.SimListTask, AGV.SimListAGV, RackColumn.SimListColumn);
                        Task.AddFirstPathOfSimAGVs();
                        break;
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
