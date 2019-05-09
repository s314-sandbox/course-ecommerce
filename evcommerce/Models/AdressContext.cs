using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class AdressContext
    {
        public string ConnectionString { get; set; }

        public AdressContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public List<AdressListModel> GetAllAdressesByUser(int? user)
        {
            List<AdressListModel> list = new List<AdressListModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(
                                                        @"SELECT Id_address, Country_name, Address_city, Address_street, Address_house,
	                                                      Address_flat, Address_info                       
                                                          FROM mydb.addresses a

                                                          INNER JOIN mydb.countries c
                                                          ON a.Address_country = c.Id_country

                                                          WHERE a.Address_user = @user;",
                                                          conn
                                                        );

                command.Parameters.Add("@user", MySqlDbType.UInt16, 11).Value = user;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AdressListModel()
                        {
                            Id = Convert.ToInt32(reader["Id_address"]),
                            Country = reader["Country_name"].ToString(),
                            City = reader["Address_city"].ToString(),
                            Street = reader["Address_street"].ToString(),
                            House = Convert.ToInt32(reader["Address_house"]),
                            Flat = reader["Address_flat"].ToString(),
                            Info = reader["Address_info"].ToString()
                        });
                    }
                }
            }

            return list;
        }


        public void AddAddress(Adress address)
        {


            using (MySqlConnection conn = GetConnection())
            {
                string query = @"INSERT INTO `mydb`.`addresses` (`Address_name`, `Address_country`, `Address_city`, `Address_street`,
                                 `Address_house`, `Address_flat`, `Address_info`, `Address_user`) 
                                 VALUES (@name, @country_id, @city, @street, @house, @flat, @info, @user_id);";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@name", MySqlDbType.VarChar, 45).Value = address.Name;
                command.Parameters.Add("@country_id", MySqlDbType.UInt32, 11).Value = address.CountryId;
                command.Parameters.Add("@city", MySqlDbType.VarChar, 45).Value = address.City;
                command.Parameters.Add("@street", MySqlDbType.VarChar, 45).Value = address.Street;
                command.Parameters.Add("@house", MySqlDbType.UInt32, 11).Value = address.House;
                command.Parameters.Add("@flat", MySqlDbType.VarChar, 45).Value = address.Flat;
                command.Parameters.Add("@info", MySqlDbType.VarChar, 300).Value = address.Info;
                command.Parameters.Add("@user_id", MySqlDbType.UInt32, 11).Value = address.UserId;


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
