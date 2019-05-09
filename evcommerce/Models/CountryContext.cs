using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class CountryContext
    {
        public string ConnectionString { get; set; }

        public CountryContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public List<Country> GetAllCountries()
        {
            List<Country> list = new List<Country>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Countries", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Country()
                        {
                            Id = Convert.ToInt32(reader["Id_country"]),
                            Name = reader["Country_name"].ToString()
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
