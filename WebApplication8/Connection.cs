using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySQL.Data.EntityFrameworkCore;
using System.Data;
using MySql.Data.MySqlClient;
 
namespace WebApplication8
{
    public class Connection
    {
        private MySqlConnection connection = new MySqlConnection ("datasource=10.0.11.141; port=3306; username=devusr; password=a2j?XU4^dU?6DmN@; database=userdb");
        public MySqlConnection GetConnection()
        {
            return connection;
        }
        public void CloseConnection()
        {
            if(connection.State == ConnectionState.Open)
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
}