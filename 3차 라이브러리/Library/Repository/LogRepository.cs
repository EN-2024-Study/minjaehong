using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library
{
    // 사실 Log 찍는 일이 서비스를 제공하는 일은 아님
    // 그래서 utility 계열로 생각해도 될듯??
    class LogRepository
    {
        private String connectionString;
        private MySqlConnection connection;
        private MySqlCommand command;

        private static LogRepository instance;

        private LogRepository()
        {
            connectionString = "Server=localhost;Database=ensharp;Uid=root;Pwd=1234;";
            connection = new MySqlConnection(connectionString);
            command = connection.CreateCommand();
        }

        public static LogRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new LogRepository();
            }
            return instance;
        }

        //============== SIMPLE GET CHECK FUNCTIONS ====================//

        public List<LogDTO> GetAllLogs()
        {
            List<LogDTO> logList = new List<LogDTO>();

            connection.Open();
            command.CommandText = Querys.getAllLogsQuery;

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                LogDTO logDTO = new LogDTO();
                logDTO.SetID(int.Parse(reader["id"].ToString()));
                logDTO.SetMode(reader.GetBoolean("mode"));
                logDTO.SetTime(reader.GetDateTime("timeStamp"));
                /*
                DateTime value = reader.GetDateTime("timestamp");
                string timeStamp = value.ToString("yyyy-MM-dd HH:mm:ss");
                logDTO.SetTime(timeStamp);
                */
                logDTO.SetAction(reader["action"].ToString());

                logList.Add(logDTO);
            }

            reader.Close();
            connection.Close();

            return logList;
        }

        // 특정 ID 존재유무 파악
        public bool CheckIfLogExists(int logID)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.checkIfLogExistsQuery;
            command.Parameters.AddWithValue("@logID", logID);
            // 어차피 값이 하나밖에 안날라옴
            bool exists = Convert.ToBoolean(command.ExecuteScalar());

            connection.Close();

            return exists; 
        }

        //===================== LOGGER CRUD ========================//

        //public static bool Add(LogDTO newLog, MySqlConnection connection, MySqlCommand command)
        public bool Add(LogDTO newLog)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.addNewLogQuery;

            // id는 알아서 autoincrement로 저장
            command.Parameters.AddWithValue("@timeStamp", newLog.GetTime());
            command.Parameters.AddWithValue("@mode", newLog.GetMode());
            command.Parameters.AddWithValue("@action", newLog.GetAction());
            command.ExecuteNonQuery();

            connection.Close();

            return true;
        }

        public bool Delete(int deletingLogID)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.deleteLogQuery;
            command.Parameters.AddWithValue("@deletingLogID", deletingLogID);
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }

        public bool DeleteAll()
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.deleteAllLogsQuery;
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }
    }
}