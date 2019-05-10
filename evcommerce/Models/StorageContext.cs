using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class StorageContext
    {
        public string ConnectionString { get; set; }

        public StorageContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public Storage GetStorage(int? item_id)
        {
            string query = "SELECT * FROM `mydb`.`storage` WHERE `Storage_item` = @id;";

            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.Add("@id", MySqlDbType.UInt16, 11).Value = item_id;
                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Storage()
                            {
                                Id = Convert.ToInt32(reader["Id_storage"]),
                                ItemId = Convert.ToInt32(reader["Storage_item"]),
                                Amount = Convert.ToInt32(reader["Storage_amount"]),
                            };
                        }
                    }
                }

                return null;
            }
        }


        public void UpdateStorage(Storage storage)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = "UPDATE mydb.storage SET `Storage_amount` = @amount WHERE `Id_storage` = @id;";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@id", MySqlDbType.UInt16, 11).Value = storage.Id;
                command.Parameters.Add("@amount", MySqlDbType.UInt16, 10).Value = storage.Amount;

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
