using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace evcommerce.Models
{
    public class UserContext
    {

        public string ConnectionString { get; set; }

        public UserContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        public User GetUser(string login, string password)
        {
            string query = "SELECT * FROM User WHERE User_login = @login AND User_password = @password LIMIT 1";

            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.Add("@login", MySqlDbType.VarChar, 45).Value = login;
                    command.Parameters.Add("@password", MySqlDbType.VarChar, 45).Value = password;
                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User()
                            {
                                Id = Convert.ToInt32(reader["Id_user"]),
                                Name = reader["User_name"].ToString(),
                                Surname = reader["User_surname"].ToString(),
                                Phone = reader["User_phone"].ToString(),
                                Mail = reader["User_mail"].ToString(),
                                Login = reader["User_login"].ToString(),
                                Password = reader["User_password"].ToString(),
                                IsAdmin = Convert.ToBoolean(reader["User_admin"])
                            };
                        }
                    }
                }

                return null;
            }
        }


        public User GetUser(string login)
        {
            string query = "SELECT * FROM User WHERE User_login = @login LIMIT 1";

            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.Add("@login", MySqlDbType.VarChar, 45).Value = login;
                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User()
                            {
                                Id = Convert.ToInt32(reader["Id_user"]),
                                Name = reader["User_name"].ToString(),
                                Surname = reader["User_surname"].ToString(),
                                Phone = reader["User_phone"].ToString(),
                                Mail = reader["User_mail"].ToString(),
                                Login = reader["User_login"].ToString(),
                                Password = reader["User_password"].ToString(),
                                IsAdmin = Convert.ToBoolean(reader["User_admin"])
                            };
                        }
                    }
                }

                return null;
            }
        }


        public void AddUser(User user)
        {


            using (MySqlConnection conn = GetConnection())
            {
                string query = @"INSERT INTO `mydb`.`user` (`User_name`, `User_surname`, `User_phone`, `User_mail`, `User_login`, `User_password`, `User_admin`) 
                                 VALUES (@name, @surname, @phone, @mail, @login, @password, false);";

                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.Add("@name", MySqlDbType.VarChar, 45).Value = user.Name;
                command.Parameters.Add("@surname", MySqlDbType.VarChar, 45).Value = user.Surname;
                command.Parameters.Add("@phone", MySqlDbType.VarChar, 12).Value = user.Phone;
                command.Parameters.Add("@mail", MySqlDbType.VarChar, 45).Value = user.Mail;
                command.Parameters.Add("@login", MySqlDbType.VarChar, 45).Value = user.Login;
                command.Parameters.Add("@password", MySqlDbType.VarChar, 45).Value = user.Password;

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
