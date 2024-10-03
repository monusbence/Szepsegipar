using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Szepsegipar
{
    

    public class DatabaseService
    {
        private string connectionString = "server=localhost;database=szepsegszalon;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
