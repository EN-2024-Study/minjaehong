using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library
{
    // 사실 Log 찍는 일이 서비스를 제공하는 일은 아님
    // 그래서 utility 계열로 생각해도 될듯??

    // LogDAO에서 사실 상 시스템 돌아갈때 호출되는건 Add 함수밖에 없음
    // 이 함수만 static으로 빼고 인스턴스 자체는 managerController에서 넘어갈때 생성되게 하는 것도 나쁘지 않음

    class LogDAO
    {
        private MySqlConnection connection;
        private MySqlCommand command;

        private static LogDAO instance;

        private LogDAO()
        {
            connection = Configuration.GetConnection();
            command = connection.CreateCommand();
        }

        public static LogDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new LogDAO();
            }
            return instance;
        }

        //================================== GET QUERY ===================================//

        public List<LogDTO> GetAllLogs()
        {
            List<LogDTO> logList = new List<LogDTO>();

            connection.Open();
            command.CommandText = Querys.getAllLogsQuery;

            MySqlDataReader reader = command.ExecuteReader();

            string id, curUser, timeStamp, action, note;
            while (reader.Read())
            {
                id = reader["id"].ToString();
                curUser = reader["user"].ToString();
                timeStamp = reader["logTime"].ToString();
                action = reader["action"].ToString();
                note = reader["note"].ToString();

                logList.Add(new LogDTO(id, timeStamp, curUser, action, note));
            }

            reader.Close();
            connection.Close();

            return logList;
        }

        //================================= CHECK QUERY =================================//

        // 특정 ID 존재유무 파악
        // Delete 하기 전 예외처리로 사용
        public bool CheckIfLogExists(int logID)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.checkIfLogExistsQuery;
            command.Parameters.AddWithValue("@logID", logID);
            bool exists = Convert.ToBoolean(command.ExecuteScalar());

            connection.Close();

            return exists; 
        }

        //================================= CRUD QUERY ==================================//

        public bool Add(DateTime timeStamp, string user, string action, string note)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.addNewLogQuery;

            // id는 알아서 autoincrement로 저장
            // DateTime은 ADD할때 바로 string으로 바꿔서 저장해주기
            command.Parameters.AddWithValue("@logTime", timeStamp.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@user", user);
            command.Parameters.AddWithValue("@action", action);
            command.Parameters.AddWithValue("@note", note);
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