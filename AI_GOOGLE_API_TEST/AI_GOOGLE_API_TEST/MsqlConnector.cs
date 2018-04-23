using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AI_GOOGLE_API_TEST
{
    public class MsqlConnector
    {
        private MySqlConnection connection;

        public MsqlConnector()
        {
            Connect("server=127.0.0.1;uid=root;pwd=pass;database=see_food");
        }

        public MsqlConnector(string ip, string port, string username, string password, string databaseName)
        {
            
        }

        public MySqlConnection getConnection() { return connection; }

        private void Connect(string connectionString)
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ExecuteCommand(string command)
        {
            Console.WriteLine(command);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                throw new Exception("Connection closed");
            }
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand(command, connection);
                mySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public MySqlDataReader ExecuteReader(string command)
        {
            Console.WriteLine(command);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                throw new Exception("Connection closed");
            }
            MySqlDataReader mySqlDataReader = null;
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand(command, connection);
                mySqlDataReader = mySqlCommand.ExecuteReader();
                return mySqlDataReader;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
            }
            return mySqlDataReader;
        }

        public bool IsConnected() { return connection.State == System.Data.ConnectionState.Open; }
    }
}
