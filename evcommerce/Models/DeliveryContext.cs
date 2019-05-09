using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class DeliveryContext
    {
        public string ConnectionString { get; set; }

        public DeliveryContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public List<Delivery> GetAllDeliveryTypes()
        {
            List<Delivery> list = new List<Delivery>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Delivery", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Delivery()
                        {
                            Id = Convert.ToInt32(reader["Id_delivery"]),
                            Name = reader["Delivery_name"].ToString()
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
