using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library
{
    // 여기서 조건 따지지말기
    // 조건은 모두 Service에서 따지고 온다
    // 여기에 조건이 있으면 안됨
    class MemberRepository
    {
        // ID가 고유키
        Dictionary<string, MemberDTO> memberDB;
        
        String connectionString;
        MySqlConnection connection;

        //================== SINGLETON ===============//

        private static MemberRepository instance;

        private MemberRepository()
        {
            memberDB = new Dictionary<string, MemberDTO>();

            connectionString = "Server=localhost;Database=ensharp;Uid=root;Pwd=1234;";
            connection = new MySqlConnection(connectionString);
        }

        public static MemberRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new MemberRepository();
            }
            return instance;
        }

        //============== SIMPLE GET CHECK FUNCTIONS ====================//

        // 무조건 존재하는 member에 대한 ID 값만 들어옴
        // 내 정보 수정할때 띄우려고
        public MemberDTO GetMember(string requestedMemberID)
        {
            MemberDTO member = new MemberDTO();

            string getMemberQuery = string.Format("SELECT * FROM memberDB WHERE id = '{0}'", requestedMemberID);

            connection.Open();
            Console.WriteLine("connection successful!");
            MySqlCommand command = new MySqlCommand(getMemberQuery, connection);
            MySqlDataReader reader = command.ExecuteReader();

            // 한 개만 왔으므로 read 한번만 호출
            reader.Read();

            member.SetId(requestedMemberID);
            member.SetPw(reader["pw"].ToString());
            member.SetAge(reader["age"].ToString());
            member.SetName(reader["name"].ToString());
            member.SetPhoneNum(reader["phonenum"].ToString());

            connection.Close();

            return member;
        }

        public List<MemberDTO> GetAllMember()
        {
            List<MemberDTO> memberList = new List<MemberDTO>();

            string getAllMemberQuery = string.Format("SELECT * FROM memberDB");

            connection.Open();
            MySqlCommand command = new MySqlCommand(getAllMemberQuery, connection);
            MySqlDataReader reader = command.ExecuteReader();

            // 한 개만 왔으므로 read 한번만 호출
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
            connection.Close();

            return memberList;
        }

        // 특정 ID 존재유무 파악
        public bool CheckIfMemberExists(string userID)
        {
            // subquery 이용 true false 반환
            string checkQuery = string.Format("SELECT EXISTS (SELECT TRUE FROM memberDB WHERE id = '{0}')", userID);

            connection.Open();
            MySqlCommand command = new MySqlCommand(checkQuery, connection);
            bool exists = Convert.ToBoolean(command.ExecuteScalar());
            connection.Close();

            if (exists) return true;
            else return false;
        }

        // ID PW 유효성 검사
        public bool CheckIfValidLogin(List<string> loginInfo)
        {
            string userID = loginInfo[0];
            string userPW = loginInfo[1];

            // subquery 이용 true false 반환
            string checkQuery = string.Format("SELECT EXISTS (SELECT TRUE FROM memberDB WHERE id = '{0}' AND pw = '{1}')", userID, userPW);

            connection.Open();
            MySqlCommand command = new MySqlCommand(checkQuery, connection);
            bool exists = Convert.ToBoolean(command.ExecuteScalar());
            connection.Close();

            if (exists) return true;
            else return false;
        }

        //===================== MEMBER CRUD ========================//

        public bool Add(MemberDTO newMember)
        {
            string insertQuery = string.Format("INSERT INTO memberdb (id, pw, name, age, phonenum) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                                                newMember.GetId(), newMember.GetPw(), newMember.GetName(), newMember.GetAge(), newMember.GetPhoneNum());

            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();

            return true;
        }

        // controller에서 ID넘기면 삭제해줌
        public bool Delete(string deletingMemberID)
        {
            string deleteQuery = string.Format("DELETE FROM memberDB WHERE id = '{0}'", deletingMemberID);

            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        // 기존 member 삭제 후 추가
        public bool Update(string updatingMemberID, MemberDTO updatingMember)
        {
            Delete(updatingMemberID);
            Add(updatingMember);
            return true;
        }
    }
}