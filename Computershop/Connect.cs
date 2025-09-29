using MySql.Data.MySqlClient;

namespace ComputerShop
{
    internal class Connect
    {
        public MySqlConnection _connection;

        private string _host;
        private string _db;
        private string _user;
        private string _pass;

        private string _connectionString;

        public Connect()
        {
            _host = "localhost";
            _db = "computershop";
            _user = "root";
            _pass = "";

            _connectionString = $"SERVER={_host};DATABASE={_db};UID={_user};PASSWORD={_pass};SslMode=None";

            _connection = new MySqlConnection(_connectionString);
        }
    }
}