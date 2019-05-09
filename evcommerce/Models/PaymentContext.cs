using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class PaymentContext
    {
        public string ConnectionString { get; set; }

        public PaymentContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public List<Payment> GetAllPaymentTypes()
        {
            List<Payment> list = new List<Payment>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Payment", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Payment()
                        {
                            Id = Convert.ToInt32(reader["Id_payment"]),
                            Name = reader["Payment_name"].ToString()
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
