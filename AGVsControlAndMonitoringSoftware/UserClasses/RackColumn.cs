using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AGVsControlAndMonitoringSoftware
{
    class RackColumn
    {
        public string Block { get; set; }
        public int Number { get; set; }
        public int AtNode { get; set; }
        public Label ColumnLabel = new Label();

        // Store pallet code, PalletCodes[i - 1] for level i of rack
        private static int NumOfLevel = 3;
        public string[] PalletCodes = new string[NumOfLevel];

        public static List<RackColumn> ListColumn = RackColumn.GetRackColums();
        public static List<RackColumn> SimListColumn = RackColumn.GetRackColums();

        private RackColumn(int atNode)
        {
            Node n = Node.ListNode.Find(nd => nd.ID == atNode);
            string[] locationCode = n.LocationCode.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            this.AtNode = n.ID;
            this.Block = locationCode[0];
            this.Number = Convert.ToInt16(locationCode[1]);
        }

        // Get list of rack column
        private static List<RackColumn> GetRackColums()
        {
            List<RackColumn> listCol = new List<RackColumn>();
            foreach (Node n in Node.ListNode)
            {
                if (n.LocationCode.Length == 0 || n.ID == 55 || n.ID == 56) continue;

                RackColumn col = new RackColumn(n.ID);

                if (col.Number == 1 || col.Number == 2 || col.Number == 3)
                {
                    col.ColumnLabel.BackColor = SystemColors.ControlLight;
                    col.ColumnLabel.Size = new Size(60 - 1, 35 - 2);
                    col.ColumnLabel.Name = "ColumnAtNode" + n.ID.ToString();
                    int x = n.X - col.ColumnLabel.Size.Width / 2;
                    int y = n.Y + 35 + 3;
                    col.ColumnLabel.Location = new Point(x, y);
                }
                else if (col.Number == 4 || col.Number == 5 || col.Number == 6)
                {
                    col.ColumnLabel.BackColor = SystemColors.ControlLight;
                    col.ColumnLabel.Size = new Size(60 - 1, 35 - 2);
                    col.ColumnLabel.Name = "ColumnAtNode" + n.ID.ToString();
                    int x = n.X - col.ColumnLabel.Size.Width / 2;
                    int y = n.Y - 35 - 3 - col.ColumnLabel.Size.Height;
                    col.ColumnLabel.Location = new Point(x, y);
                } 

                listCol.Add(col);
            }
            return listCol;
        }

        // Update column color that indicate the number of pallet code in array PalletCodes
        public static void UpdateColumnColor(List<RackColumn> listColumn)
        {
            foreach (RackColumn column in listColumn)
            {
                int codeCount = 0;
                foreach (string code in column.PalletCodes)
                {
                    if (code != null) codeCount++;
                }
    
                if (codeCount == 0) column.ColumnLabel.BackColor = SystemColors.ControlLight;
                else if (codeCount == 1) column.ColumnLabel.BackColor = Color.LightSteelBlue;
                else if (codeCount == 2) column.ColumnLabel.BackColor = Color.CornflowerBlue;
                else if (codeCount == 3) column.ColumnLabel.BackColor = Color.RoyalBlue;
            }
        }

        public static void RemoveLabelColumn(Panel panel, RackColumn col)
        {
            var label = panel.Controls.OfType<Label>().FirstOrDefault(lb => lb.Name == "ColumnAtNode" + col.AtNode.ToString());
            if (label != null) panel.Controls.Remove(label);
        }

        public static void InitializePallet(List<RackColumn> listColumn, List<Pallet> listPallet)
        {
            foreach (Pallet pallet in listPallet)
            {
                // find location of this pallet
                RackColumn col = listColumn.Find(c => (c.Block == pallet.AtBlock) && (c.Number == pallet.AtColumn));
                if (col == null) continue;

                // add to level at this column
                col.PalletCodes[pallet.AtLevel - 1] = pallet.Code;
            }
        }
    }

    class Pallet
    {
        public string Code { get; set; }
        public bool InStock { get; set; }
        public string StoreTime { get; set; }
        public string DeliverTime { get; set; }
        public string AtBlock { get; set; }
        public int AtColumn { get; set; }
        public int AtLevel { get; set; }

        public Pallet(string code, bool isInWarehouse, string storeTime, string block, int column, int level)
        {
            this.Code = code;
            this.StoreTime = storeTime;
            this.InStock = isInWarehouse;
            this.AtBlock = block;
            this.AtColumn = column;
            this.AtLevel = level;
        }

        // Save pallet info (the present and the past)
        public static List<Pallet> ListPallet = new List<Pallet>();
        public static List<Pallet> SimListPallet = new List<Pallet>();

        public static void SaveDeliveryTime(string palletCode, List<Pallet> listPallet)
        {
            Pallet pallet = listPallet.Find(p => p.Code == palletCode);
            if (pallet == null) return;
            pallet.DeliverTime = DateTime.Now.ToString("dddd, MMMM dd, yyyy  h:mm:ss tt");
            pallet.InStock = false;
        }
    }
}
