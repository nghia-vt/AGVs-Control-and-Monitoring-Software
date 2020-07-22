using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AGVsControlAndMonitoringSoftware
{
    class DBUtility
    {
        private const string connectionStr = @"Data Source=VOTRONGNGHIA\SQLEXPRESS;
                                     Initial Catalog=agvWarehouseDB;Integrated Security=True";

        // This method return DataTable/List infomation of nodes
        public static dynamic GetNodeInfoFromDB<T>(string tableName)
        {
            List<Node> listNode = new List<Node>();
            DataTable table = new DataTable();
            
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                //SqlComnection
                connection.Open();

                //SqlCommand
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from " + tableName;

                //SqlAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                //Get data
                table.Clear();
                adapter.Fill(table);
                connection.Close();
            }

            // Store DataTable into a List
            // Note: listNode[i].ID = i so instead of using ID, use i 
            // (because of the order of the rows in table)
            listNode = (from DataRow dr in table.Rows
                        select new Node()
                        {
                            ID = Convert.ToInt32(dr["Node"]),
                            X = Convert.ToInt32(dr["pos_X"]),
                            Y = Convert.ToInt32(dr["pos_Y"]),
                            AdjacentNode = (dr["AdjacentNode"].ToString()).
                                            Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries),
                            LocationCode = dr["LocationCode"].ToString()
                        }).ToList();

            if (typeof(T) == typeof(DataTable)) return table;
            else if (typeof(T) == typeof(List<Node>)) return listNode;
            else return null;
        }

        public static dynamic GetPalletInfoFromDB<T>(string tableName)
        {
            DataTable table = new DataTable();
            List<Pallet> listPallet = new List<Pallet>();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                //SqlComnection
                connection.Open(); // don't need because using SqlDataAdapter

                //SqlCommand
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from " + tableName;

                //SqlAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                //Get data
                table.Clear();
                adapter.Fill(table);
                connection.Close(); // don't need because using SqlDataAdapter
            }

            // Store DataTable into a List
            listPallet = (from DataRow dr in table.Rows
                          where Convert.ToBoolean(dr["InStock"]) == true
                          select new Pallet(dr["PalletCode"].ToString(), Convert.ToBoolean(dr["InStock"]), dr["StoreTime"].ToString(),
                                            dr["AtBlock"].ToString(), Convert.ToInt16(dr["AtColumn"]), Convert.ToInt16(dr["AtLevel"]))
                          ).ToList();

            if (typeof(T) == typeof(DataTable)) return table;
            else if (typeof(T) == typeof(List<Pallet>)) return listPallet;
            else return null;
        }

        public static void InsertNewPalletToDB(string tableName, string palletCode, bool inStock, string storeTime,
                                               string block, int column, int level)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                //SqlComnection
                connection.Open();

                //SqlCommand
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "insert into " + tableName + " (PalletCode, InStock, StoreTime, AtBlock, AtColumn, AtLevel)"
                                                     + " values(@palletCode, @inStock, @storeTime, @atBlock, @atColumn, @atLevel)";
                command.Parameters.Add("@palletCode", SqlDbType.NVarChar).Value = palletCode;
                command.Parameters.Add("@inStock", SqlDbType.Bit).Value = inStock;
                command.Parameters.Add("@storeTime", SqlDbType.NVarChar).Value = storeTime;
                command.Parameters.Add("@atBlock", SqlDbType.NVarChar).Value = block;
                command.Parameters.Add("@atColumn", SqlDbType.Int).Value = column;
                command.Parameters.Add("@atLevel", SqlDbType.Int).Value = level;
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static void UpdatePalletDB(string tableName, string palletCode, bool inStock, string deliveryTime, List<Pallet> listPallet)
        {
            Pallet pallet = listPallet.Find(p => p.Code == palletCode);
            if (pallet == null) return;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                //SqlComnection
                connection.Open();

                //SqlCommand
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "update " + tableName
                                    + " set DeliveryTime = @deliveryTime, InStock = @inStock where PalletCode = @palletCode";
                command.Parameters.Add("@deliveryTime", SqlDbType.NVarChar).Value = deliveryTime;
                command.Parameters.Add("@inStock", SqlDbType.Bit).Value = inStock;
                command.Parameters.Add("@palletCode", SqlDbType.NVarChar).Value = palletCode;
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static void DeletePalletFromDB(string tableName, string palletCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                //SqlComnection
                connection.Open();

                //SqlCommand
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "delete " + tableName + " where PalletCode = @palletCode";
                command.Parameters.Add("@palletCode", SqlDbType.NVarChar).Value = palletCode;
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
