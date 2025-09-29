using System.Collections.Generic;
using Computershop;
using MySql.Data.MySqlClient;

namespace ComputerShop.Services
{
    internal class Users : IDatabase
    {
        Connect conn = new Connect();
        public ICollection<object> GetAllData()
        {
            ICollection<object> data = new List<object>();
            conn._connection.Open();


            conn._connection.Close();

            return data;
        }

        public object GetData(string username, string password)
        {
            conn._connection.Open();

            string sql = "SELECT * FROM users WHERE username = @username AND password = @password";

            MySqlCommand cmd = new MySqlCommand(sql, conn._connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            MySqlDataReader reader = cmd.ExecuteReader();

            string result = "";
            if (reader.Read() == true)
                result = "Regisztrált tag";
            else
                result = "Nem regisztrált tag";

            conn._connection.Close();

            return new { message = result };
        }


    }
}