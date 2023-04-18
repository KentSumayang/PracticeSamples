using MySql.Data.MySqlClient;
using System.Data;

namespace PersonWebApplication;

public class Connection
{
        private MySqlConnection connection = new MySqlConnection("datasource=10.0.11.141; port=3306; username=devusr; password=a2j?XU4^dU?6DmN@; database=account; Persist Security Info=True;Convert Zero Datetime=True");
        public MySqlConnection GetConnection()
        {
            return connection;
        }
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
    }

