using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ProductsControl.Properties;
using MySql.Data.MySqlClient;

namespace ProductsControl.Config
{
    class DatabaseConfig
    {

        public MySqlConnection connectDatabase()
        {
            MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder();
            
            connectionStringBuilder.Server = Properties.Properties.Server;
            connectionStringBuilder.Database = Properties.Properties.Database;
            connectionStringBuilder.UserID = Properties.Properties.UserId;
            connectionStringBuilder.Password = Properties.Properties.Password;
            connectionStringBuilder.SslMode = 0;

            MySqlConnection connectionDb = new MySqlConnection(connectionStringBuilder.ToString());

            connectionDb.Open();

            return connectionDb;
        }
    }
}
