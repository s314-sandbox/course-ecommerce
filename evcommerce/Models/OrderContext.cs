using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class OrderContext
    {
        public string ConnectionString { get; set; }

        public OrderContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public void AddOrderedItem(Order order)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = "INSERT INTO `mydb`.`order` (`Order_order_info`, `Order_item`, `Order_amount`) VALUES (@info, @item, @amount);";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@info", MySqlDbType.UInt32, 11).Value = order.OrderInfoId;
                command.Parameters.Add("@item", MySqlDbType.UInt32, 11).Value = order.ItemId;
                command.Parameters.Add("@amount", MySqlDbType.UInt32, 11).Value = order.Amount;

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }


        public List<OrderedItemModel> GetOrderedItemsByInfoId(int? id)
        {
            List<OrderedItemModel> list = new List<OrderedItemModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(
                                                        @"SELECT Order_amount, Item_name, Item_number, Item_cost, 
                                                        Sub_category_name, Category_name, Provider_name FROM mydb.`order` o

                                                        INNER JOIN mydb.`item` i
                                                        ON o.Order_item = i.Id_item
                                                        INNER JOIN mydb.sub_category sc
                                                        ON i.Item_category = sc.Id_sub_category
                                                        INNER JOIN mydb.category c
                                                        ON sc.Sub_category_category = c.Id_category
                                                        INNER JOIN mydb.provider p
                                                        ON i.Item_provider = p.Id_provider

                                                        WHERE o.Order_order_info = @id;",
                                                        conn
                                                        );

                command.Parameters.Add("@id", MySqlDbType.UInt16, 11).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new OrderedItemModel()
                        {
                            ItemName = reader["Item_name"].ToString(),
                            Amount = Convert.ToInt32(reader["Order_amount"]),
                            VendorCode = reader["Item_number"].ToString(),
                            Cost = Convert.ToInt32(reader["Item_cost"]),
                            SubCategory = reader["Sub_category_name"].ToString(),
                            Category = reader["Category_name"].ToString(),
                            Provider = reader["Provider_name"].ToString(),
                        });
                    }
                }
            }

            return list;
        }


        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
