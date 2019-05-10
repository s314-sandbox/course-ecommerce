using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class ItemContext
    {
        public string ConnectionString { get; set; }

        public ItemContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public Item GetItem(int? id)
        {
            string query = "SELECT * FROM Item WHERE Id_item = @id";

            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.Add("@id", MySqlDbType.UInt16, 11).Value = id;
                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Item()
                            {
                                Id = Convert.ToInt32(reader["Id_item"]),
                                Name = reader["Item_name"].ToString(),
                                Number = reader["Item_number"].ToString(),
                                Category = Convert.ToInt32(reader["Item_category"]),
                                Provider = Convert.ToInt32(reader["Item_provider"]),
                                Cost = Convert.ToInt32(reader["Item_cost"])
                            };
                        }
                    }
                }

                return null;
            }
        }

        

        public ItemListViewModel GetItemInfo(int? id)
        {
            string query = @"SELECT Id_item, Item_name, Item_number, Category_name, Sub_category_name, Provider_name, Item_cost FROM mydb.item i

                                                          INNER JOIN mydb.sub_category sc
                                                          ON i.Item_category = sc.Id_sub_category
                                                          INNER JOIN mydb.category c
                                                          ON sc.Sub_category_category = c.Id_category
                                                          INNER JOIN mydb.provider p
                                                          ON i.Item_provider = p.Id_provider

                                                          WHERE Id_item = @id; ";

            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.Add("@id", MySqlDbType.UInt16, 11).Value = id;
                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ItemListViewModel()
                            {
                                Id = Convert.ToInt32(reader["Id_item"]),
                                Name = reader["Item_name"].ToString(),
                                VendorCode = reader["Item_number"].ToString(),
                                Category = reader["Category_name"].ToString(),
                                SubCategory = reader["Sub_category_name"].ToString(),
                                Provider = reader["Provider_name"].ToString(),
                                Cost = Convert.ToInt32(reader["Item_cost"])
                            };
                        }
                    }
                }

                return null;
            }
        }


        public List<ItemListViewModel> GetAllItemsByCategory(int? category, int? page = 1)
        {
            List<ItemListViewModel> list = new List<ItemListViewModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(@"SELECT Id_item, Item_name, Item_number, Category_name, Sub_category_name, Provider_name, Item_cost FROM mydb.item i

                                                          INNER JOIN mydb.sub_category sc
                                                          ON i.Item_category = sc.Id_sub_category
                                                          INNER JOIN mydb.category c
                                                          ON sc.Sub_category_category = c.Id_category
                                                          INNER JOIN mydb.provider p
                                                          ON i.Item_provider = p.Id_provider

                                                          WHERE Item_category = @category_id ORDER BY Item_cost ASC LIMIT 10 OFFSET @page_offset; ",
                                                          conn);

                command.Parameters.Add("@category_id", MySqlDbType.UInt16, 11).Value = category;
                command.Parameters.AddWithValue("@page_offset", (page - 1) * 10);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ItemListViewModel()
                        {
                            Id = Convert.ToInt32(reader["Id_item"]),
                            Name = reader["Item_name"].ToString(),
                            VendorCode = reader["Item_number"].ToString(),
                            Category = reader["Category_name"].ToString(),
                            SubCategory = reader["Sub_category_name"].ToString(),
                            Provider = reader["Provider_name"].ToString(),
                            Cost = Convert.ToInt32(reader["Item_cost"])
                        });
                    }
                }
            }

            return list;
        }


        public int GetItemCount(int? category)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM Item WHERE Item_category = @category_id", conn))
                {
                    cmd.Parameters.Add("@category_id", MySqlDbType.UInt16, 11).Value = category;
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count;
                }
            }
        }


        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
