using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Library
{
    class HistoryRepository
    {
        string connectionString;
        MySqlConnection connection;

        private static HistoryRepository instance;

        private HistoryRepository()
        {
            connectionString = "Server=localhost;Database=ensharp;Uid=root;Pwd=1234;";
            connection = new MySqlConnection(connectionString);
        }

        public static HistoryRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new HistoryRepository();
            }
            return instance;
        }

        //================= SIMPLE GET CHECK FUNCTIONS ==================//

        // historyDB 참조해서 user가 진짜 빌린 책인지 확인
        public bool CheckIfUserBorrowed(string curUserID, string returningBookID)
        {
            List<int> curUserBorrowedBookList = new List<int>();

            string getAllBorrowedBooksQuery = string.Format("SELECT * FROM historyDB");

            connection.Open();
            MySqlCommand command = new MySqlCommand(getAllBorrowedBooksQuery, connection);
            MySqlDataReader reader = command.ExecuteReader();

            Boolean isBorrowed = false;

            while (reader.Read())
            {
                // 1. 현재 USER의 기록이고
                // 2. 현재 확인하려는 책이고
                // 3. 아직 반납안했으면
                if (reader["borrower_id"].ToString() == curUserID && reader["book_id"].ToString()==returningBookID && !reader.GetBoolean("returned"))
                {
                    isBorrowed = true;
                    break;
                }
            }
            connection.Close();

            return isBorrowed;
        }

        // 현재 USER가 BORROW한 BOOK들의 ID에 대한 정보 반환 -> BOOKID LIST로
        public List<int> GetMemberBorrowedBooks(string curUserID)
        {
            List<int> curUserBorrowedBookList = new List<int>();

            string getAllBorrowedBooksQuery = string.Format("SELECT * FROM historyDB");

            connection.Open();
            MySqlCommand command = new MySqlCommand(getAllBorrowedBooksQuery, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // 현재 USER의 기록이고 아직 반납안했으면
                if (reader["borrower_id"].ToString() == curUserID && !reader.GetBoolean("returned"))
                {
                    curUserBorrowedBookList.Add(int.Parse(reader["book_id"].ToString()));
                }
            }
            connection.Close();

            return curUserBorrowedBookList;
        }

        // 현재 USER가 RETURN한 BOOK들의 ID에 대한 정보 반환 -> BOOKID LIST로
        public List<int> GetMemberReturnedBooks(string curUserID)
        {
            List<int> curUserReturnedBookList = new List<int>();

            string getAllReturnedBooksQuery = string.Format("SELECT * FROM historyDB");

            connection.Open();
            MySqlCommand command = new MySqlCommand(getAllReturnedBooksQuery, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // 현재 USER의 기록이고 반납했으면
                if (reader["borrower_id"].ToString() == curUserID && reader.GetBoolean("returned"))
                {
                    curUserReturnedBookList.Add(int.Parse(reader["book_id"].ToString()));
                }
            }
            connection.Close();

            return curUserReturnedBookList;
        }


        //================ BORROW && RETURN CRUD =================//

        // 여기서 직접 historyDB에 접근하는 쿼리문 적어주기
        // 일단 memberService부터 손보고 시작하자
        public bool AddBorrowHistory(string curUserID, string bookID)
        {
            // returned 는 default로 false니까 그냥 안넣어줘도 됨
            string insertQuery = string.Format("INSERT INTO historyDB (borrower_id, book_id, returned) VALUES('{0}', '{1}', FALSE) ON DUPLICATE KEY UPDATE returned = FALSE;", curUserID, bookID);

            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();

            return true;
        }

        // 여기서 직접 historyDB에 쿼리문으로 접근해서 returned boolean 값 바꿔주기
        // 여기도 service부터 하고 시작
        public bool AddReturnHistory(string curUserID, string bookID)
        {
            string updateQuery = string.Format("UPDATE historyDB SET returned = TRUE WHERE borrower_id = '{0}' AND book_id = '{1}'", curUserID, bookID);

            connection.Open();
            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();

            return true;
        }
    }
}
