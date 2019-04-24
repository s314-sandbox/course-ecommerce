using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class CategoryContext
    {
        public string ConnectionString { get; set; }
        
        public CategoryContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public List<Category> GetAllCategories()
        {
            List<Category> list = new List<Category>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Category", conn);

                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Category()
                        {
                            Id = Convert.ToInt32(reader["Id_category"]),
                            Name = reader["Category_name"].ToString()
                        });
                    }
                }
            }

            return list;
        }


        public Category GetCategory(int? id)
        {
            string query = "SELECT * FROM Category WHERE Id_category = @id";

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
                            return new Category()
                            {
                                Id = Convert.ToInt32(reader["Id_category"]),
                                Name = reader["Category_name"].ToString()
                            };
                        }
                    }
                }

                return null;
            }
        }


        public void AddCategory(Category category)
        {
            

            using (MySqlConnection conn = GetConnection())
            {
                string query = "INSERT INTO `mydb`.`category` (`Category_name`) VALUES (@name);";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@name", MySqlDbType.VarChar, 45).Value = category.Name;

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }


        public void DeleteCategory(int? id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = "DELETE FROM `mydb`.`category` WHERE Id_category = @id";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@id", MySqlDbType.UInt16, 11).Value = id;

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }


        public void UpdateCategory(Category category)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string query = "UPDATE `mydb`.`category` SET `Category_name` = @name WHERE `Id_category` = @id";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@id", MySqlDbType.UInt16, 11).Value = category.Id;
                command.Parameters.Add("@name", MySqlDbType.VarChar, 45).Value = category.Name;

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
