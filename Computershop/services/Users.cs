using System.Collections.Generic;
using Computershop;
using MySql.Data.MySqlClient;

namespace ComputerShop.Services
{
    internal class Users : IDatabase
    {
        Connect conn = new Connect();

        public object AddRecord(string username, string password, string email, string fullname)
        {
            string salt = GenerateSalt();
            string hashedPassword = ComputeHmacSha256(password, salt);
            conn._connection.Open();

            string sql = "INSERT INTO `users`(`UserName`, `FullName`, `Password`, `Email`, `Salt`) VALUES (@username,@password,@email,@fullname,@salt)";

            MySqlCommand cmd = new MySqlCommand(sql, conn._connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", hashedPassword);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@fullname", fullname);
            cmd.Parameters.AddWithValue("@salt", salt);

            cmd.ExecuteNonQuery();

            conn._connection.Close();

            return new { message = "Sikeres regisztráció!" };
        }

        public DataView GetAllData()
        {
            conn._connection.Open();

            string sql = "SELECT * FROM users";

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn._connection);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            conn._connection.Close();

            return dt.DefaultView;
        }

        public bool GetData(string username, string password)
        {
            conn._connection.Open();

            string sql = "SELECT * FROM users WHERE UserName = @username AND Password = @password";

            MySqlCommand cmd = new MySqlCommand(sql, conn._connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                conn._connection.Close();
                return true;

            }
            else
            {
                conn._connection.Close();
                return false;
            }
        }

        public string GenerateSalt()
        {
            byte[] salt = new byte[16];

            using (var rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string ComputeHmacSha256(string password, string salt)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(salt)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }
    }
}