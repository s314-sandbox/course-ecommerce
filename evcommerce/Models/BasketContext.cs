using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class BasketContext
    {
        public string ConnectionString { get; set; }

        public BasketContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public Basket GetPosition(int? id)
        {
            string query = "SELECT * FROM Basket WHERE Id_basket = @id";

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
                            return new Basket()
                            {
                                Id = Convert.ToInt32(reader["Id_basket"]),
                                UserId = Convert.ToInt32(reader["Basket_user"]),
                                ItemId = Convert.ToInt32(reader["Basket_item"]),
                                Amount = Convert.ToInt32(reader["Basket_amount"])
                            };
                        }
                    }
                }

                return null;
            }
        }


        public void AddPosition(Item item, int amount)
        {


            using (MySqlConnection conn = GetConnection())
            {
                string query = "INSERT INTO `mydb`.`basket` (`Basket_user`, `Basket_item`, `Basket_amount`) VALUES (@user, @item, @amount);";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@user", MySqlDbType.UInt32).Value = 1; // TODO: Remove hardcoded user by implementing login/sign up m.
                command.Parameters.Add("@item", MySqlDbType.UInt32).Value = item.Id;
                command.Parameters.Add("@amount", MySqlDbType.UInt32).Value = amount;

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<BasketPositionView> GetAllPositionsByUser(int? user)
        {
            List<BasketPositionView> list = new List<BasketPositionView>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(
                                                        @"SELECT Id_basket, Basket_amount, Item_name, Item_number, Item_cost, 
                                                        Sub_category_name, Category_name, Provider_name FROM mydb.basket b

                                                        INNER JOIN mydb.item i
                                                        ON b.Basket_item = i.Id_item
                                                        INNER JOIN mydb.sub_category sc
                                                        ON i.Item_category = sc.Id_sub_category
                                                        INNER JOIN mydb.category c
                                                        ON sc.Sub_category_category = c.Id_category
                                                        INNER JOIN mydb.provider p
                                                        ON i.Item_provider = p.Id_provider

                                                        WHERE b.Basket_user = @user;", 
                                                        conn
                                                        );

                command.Parameters.Add("@user", MySqlDbType.UInt16, 11).Value = user;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BasketPositionView()
                        {
                            Id = Convert.ToInt32(reader["Id_basket"]),
                            ItemName = reader["Item_name"].ToString(),
                            Amount = Convert.ToInt32(reader["Basket_amount"]),
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


        public void RemovePosition(int? id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = "DELETE FROM `mydb`.`basket` WHERE Id_basket = @id AND Basket_user = @user";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@id", MySqlDbType.UInt16, 11).Value = id;
                command.Parameters.Add("@user", MySqlDbType.UInt32).Value = 1; // TODO: Remove hardcoded user by implementing login/sign up m.

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }


        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
