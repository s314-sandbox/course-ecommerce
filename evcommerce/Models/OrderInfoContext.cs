using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class OrderInfoContext
    {
        public string ConnectionString { get; set; }

        public OrderInfoContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public int AddOrder(OrderInfo order)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = "INSERT INTO `mydb`.`order_info` (`Order_info_user`, `Order_info_date`, `Order_info_payment`, `Order_info_delivery`, `Order_info_adress`) VALUES (@user, @date, @payment, @delivery, @adress); SELECT last_insert_id();";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@user", MySqlDbType.UInt32, 11).Value = order.UserId;
                command.Parameters.Add("@date", MySqlDbType.DateTime).Value = order.Date;
                command.Parameters.Add("@payment", MySqlDbType.UInt32, 11).Value = order.PaymentId;
                command.Parameters.Add("@delivery", MySqlDbType.UInt32, 11).Value = order.DeliveryId;
                command.Parameters.Add("@adress", MySqlDbType.UInt32, 11).Value = order.AdressId;

                conn.Open();
                int added = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();
                return added;
            }
        }


        public List<OrderInfoEntryModel> GetOrdersOfUser(int user_id)
        {
            List<OrderInfoEntryModel> list = new List<OrderInfoEntryModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(
                                                        @"SELECT Id_order_info, Order_info_date, Payment_name, Delivery_name, CONCAT(Country_name, "", "", Address_city, "", "", Address_street, "", д. "", Address_house, "", кв. "", Address_flat) as ""Address_full"" FROM mydb.order_info oi

                                                          INNER JOIN mydb.user u
                                                          ON oi.Order_info_user = u.Id_user
                                                          INNER JOIN mydb.payment p
                                                          ON oi.Order_info_payment = p.Id_payment
                                                          INNER JOIN mydb.delivery d
                                                          ON oi.Order_info_delivery = d.Id_delivery
                                                          INNER JOIN mydb.addresses a
                                                          ON oi.Order_info_adress = a.Id_address
                                                          INNER JOIN mydb.countries c
                                                          ON a.Address_country = c.Id_country

                                                          WHERE oi.Order_info_user = @user; ",
                                                          conn
                                                        );

                command.Parameters.Add("@user", MySqlDbType.UInt16, 11).Value = user_id;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new OrderInfoEntryModel()
                        {
                            Id = Convert.ToInt32(reader["Id_order_info"]),
                            Date = DateTime.Parse(reader["Order_info_date"].ToString()),
                            Payment = reader["Payment_name"].ToString(),
                            Delivery = reader["Delivery_name"].ToString(),
                            Address = reader["Address_full"].ToString()
                        });
                    }
                }
            }

            return list;
        }


        public List<AdminOrderListEntryModel> GetAllOrders()
        {
            List<AdminOrderListEntryModel> list = new List<AdminOrderListEntryModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(
                                                        @"SELECT Id_order_info, User_name, User_surname, User_phone, Order_info_date, Payment_name, Delivery_name, CONCAT(Country_name, "", "", Address_city, "", "", Address_street, "", д. "", Address_house, "", кв. "", Address_flat) as ""Address_full"" FROM mydb.order_info oi

                                                          INNER JOIN mydb.user u
                                                          ON oi.Order_info_user = u.Id_user
                                                          INNER JOIN mydb.payment p
                                                          ON oi.Order_info_payment = p.Id_payment
                                                          INNER JOIN mydb.delivery d
                                                          ON oi.Order_info_delivery = d.Id_delivery
                                                          INNER JOIN mydb.addresses a
                                                          ON oi.Order_info_adress = a.Id_address
                                                          INNER JOIN mydb.countries c
                                                          ON a.Address_country = c.Id_country

                                                          ORDER BY Order_info_date DESC;",
                                                          conn
                                                        );

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AdminOrderListEntryModel()
                        {
                            Id = Convert.ToInt32(reader["Id_order_info"]),
                            UserName = reader["User_name"].ToString(),
                            UserSurname = reader["User_surname"].ToString(),
                            UserPhone = reader["User_phone"].ToString(),
                            Date = DateTime.Parse(reader["Order_info_date"].ToString()),
                            Payment = reader["Payment_name"].ToString(),
                            Delivery = reader["Delivery_name"].ToString(),
                            Address = reader["Address_full"].ToString()
                        });
                    }
                }
            }

            return list;
        }


        public int GetTotalPriceForOrderInfo(int? info_id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(@"SELECT SUM(Item_cost * Order_amount) FROM mydb.`order` o
                                                    INNER JOIN mydb.`item` i
                                                    ON o.Order_item = i.Id_item
                                                    WHERE o.Order_order_info = @order_info_id;",
                                                    conn))
                {
                    cmd.Parameters.Add("@order_info_id", MySqlDbType.UInt16, 11).Value = info_id;
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count;
                }
            }
        }


        public OrderInfo GetOrderInfo(int? id)
        {
            string query = "SELECT * FROM Order_info WHERE Id_order_info = @id";

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
                            return new OrderInfo()
                            {
                                Id = Convert.ToInt32(reader["Id_order_info"]),
                                UserId = Convert.ToInt32(reader["Order_info_user"]),
                                Date = DateTime.Parse(reader["Order_info_date"].ToString()),
                                PaymentId = Convert.ToInt32(reader["Order_info_payment"]),
                                DeliveryId = Convert.ToInt32(reader["Order_info_delivery"]),
                                AdressId = Convert.ToInt32(reader["Order_info_adress"]),
                            };
                        }
                    }
                }

                return null;
            }
        }


        public OrderInfoEntryModel GetOrderEntry(int? id)
        {
            string query = @"SELECT Id_order_info, Order_info_date, Payment_name, Delivery_name, CONCAT(Country_name, "", "", Address_city, "", "", Address_street, "", д. "", Address_house, "", кв. "", Address_flat) as ""Address_full"" FROM mydb.order_info oi

                                                          INNER JOIN mydb.user u
                                                          ON oi.Order_info_user = u.Id_user
                                                          INNER JOIN mydb.payment p
                                                          ON oi.Order_info_payment = p.Id_payment
                                                          INNER JOIN mydb.delivery d
                                                          ON oi.Order_info_delivery = d.Id_delivery
                                                          INNER JOIN mydb.addresses a
                                                          ON oi.Order_info_adress = a.Id_address
                                                          INNER JOIN mydb.countries c
                                                          ON a.Address_country = c.Id_country

                                                          WHERE oi.Id_order_info = @id;";

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
                            return new OrderInfoEntryModel()
                            {
                                Id = Convert.ToInt32(reader["Id_order_info"]),
                                Date = DateTime.Parse(reader["Order_info_date"].ToString()),
                                Payment = reader["Payment_name"].ToString(),
                                Delivery = reader["Delivery_name"].ToString(),
                                Address = reader["Address_full"].ToString()
                            };
                        }
                    }
                }

                return null;
            }
        }


        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
