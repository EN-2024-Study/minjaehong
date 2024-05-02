using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library
{
    class Configuration
    {
        private const string naverAPIClientID = "LzrVM4JtNzEjcjhnf21x";
        private const string naverAPIClientPW = "wHE6dYhDob";

        private const string connectionString = "Server=localhost;Database=ensharp;Uid=root;Pwd=1234;";
        
        private static MySqlConnection connection;

        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
            }
            return connection;
        }

        public static string GetNaverAPIID()
        {
            return naverAPIClientID;
        }

        public static string GetNaverAPIPW()
        {
            return naverAPIClientPW;
        }
    }
}