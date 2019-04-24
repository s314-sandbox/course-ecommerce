using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class SubCategoryContext
    {

        public string ConnectionString { get; set; }

        public SubCategoryContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public List<SubCategory> GetAllSubCategories(int? parent)
        {
            List<SubCategory> list = new List<SubCategory>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Sub_category WHERE Sub_category_category = @parent", conn);
                command.Parameters.Add("@parent", MySqlDbType.UInt16, 11).Value = parent;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SubCategory()
                        {
                            Id = Convert.ToInt32(reader["Id_sub_category"]),
                            Category = Convert.ToInt32(reader["Sub_category_category"]),
                            Name = reader["Sub_category_name"].ToString()
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
