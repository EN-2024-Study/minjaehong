using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library
{
    // 여기서 조건 따지지말기
    // 조건은 모두 Service에서 따지고 온다
    // 여기에 조건이 있으면 안됨
    class MemberDAO
    {
        private String connectionString;
        private MySqlConnection connection;
        private MySqlCommand command;

        //================== SINGLETON ===============//

        private static MemberDAO instance;

        private MemberDAO()
        {
            connectionString = "Server=localhost;Database=ensharp;Uid=root;Pwd=1234;";
            connection = new MySqlConnection(connectionString);
            command = connection.CreateCommand();
        }

        public static MemberDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new MemberDAO();
            }
            return instance;
        }

        //================================= CHECK QUERY =================================//

        // 특정 ID 존재유무 파악
        public bool CheckIfMemberExists(string userID)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.checkIfMemberExistsQuery;

            command.Parameters.AddWithValue("@userID", userID);
            bool exists = Convert.ToBoolean(command.ExecuteScalar()); // 어차피 한개밖에 안넘어옴

            connection.Close();

            if (exists) return true;
            else return false;
        }

        // ID PW 유효성 검사
        public bool CheckIfValidLogin(LoginDTO loginDTO)
        {
            string userID = loginDTO.GetID();
            string userPW = loginDTO.GetPW();

            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.checkIfValidLoginQuery;
            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@userPW", userPW);
            bool exists = Convert.ToBoolean(command.ExecuteScalar()); // 어차피 한개밖에 안넘어옴

            connection.Close();

            if (exists) return true;
            else return false;
        }

        //================================== GET QUERY ===================================//

        // 무조건 존재하는 member에 대한 ID 값만 들어옴
        // 이미 service에서 존재성 판단함
        public MemberDTO GetMember(string requestedMemberID)
        {
            MemberDTO member = new MemberDTO();

            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.getMemberByIDQuery;
            command.Parameters.AddWithValue("@requestedMemberID", requestedMemberID);

            MySqlDataReader reader = command.ExecuteReader();

            // 한 개만 왔을거니까 read 한번만 호출
            reader.Read();

            member.SetId(requestedMemberID);
            member.SetPw(reader["pw"].ToString());
            member.SetAge(reader["age"].ToString());
            member.SetName(reader["name"].ToString());
            member.SetPhoneNum(reader["phonenum"].ToString());

            reader.Close();
            connection.Close();

            return member;
        }

        public List<MemberDTO> GetAllMember()
        {
            List<MemberDTO> memberList = new List<MemberDTO>();

            connection.Open();

            command.CommandText = Querys.getAllMembersQuery;

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MemberDTO member = new MemberDTO();
                member.SetId(reader["id"].ToString());
                member.SetPw(reader["pw"].ToString());
                member.SetAge(reader["age"].ToString());
                member.SetName(reader["name"].ToString());
                member.SetPhoneNum(reader["phonenum"].ToString());

                memberList.Add(member);
            }

            reader.Close();
            connection.Close();

            return memberList;
        }

        //================================= CRUD QUERY ==================================//
        
        // service에서 넘겨주면 CRUD 진행
        public bool Add(MemberDTO newMember)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.addNewMemberQuery;
            command.Parameters.AddWithValue("@id", newMember.GetId());
            command.Parameters.AddWithValue("@pw", newMember.GetPw());
            command.Parameters.AddWithValue("@name", newMember.GetName());
            command.Parameters.AddWithValue("@age", newMember.GetAge());
            command.Parameters.AddWithValue("@phonenum", newMember.GetPhoneNum());
            command.ExecuteNonQuery();

            connection.Close();

            return true;
        }

        public bool Delete(string deletingMemberID)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.deleteMemberQuery;
            command.Parameters.AddWithValue("@deletingMemberID", deletingMemberID);
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }

        public bool Update(string updatingMemberID, MemberDTO updatingMember)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.updateMemberQuery;
            command.Parameters.AddWithValue("@updatingMemberID", updatingMember.GetId());
            command.Parameters.AddWithValue("@pw", updatingMember.GetPw());
            command.Parameters.AddWithValue("@name", updatingMember.GetName());
            command.Parameters.AddWithValue("@age", updatingMember.GetAge());
            command.Parameters.AddWithValue("@phonenum", updatingMember.GetPhoneNum());
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }
    }
}