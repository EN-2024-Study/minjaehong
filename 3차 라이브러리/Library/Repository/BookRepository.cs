using System;
using System.Collections;
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
    class BookRepository
    {
        Dictionary<int, BookDTO> bookDB;

        private int keyID;

        string connectionString;
        MySqlConnection connection;

        //===================== SINGELTON ========================//

        private static BookRepository instance;

        private BookRepository()
        {
            keyID = 1;
            bookDB = new Dictionary<int, BookDTO>();
            connectionString = "Server=localhost;Database=ensharp;Uid=root;Pwd=1234;";
            connection = new MySqlConnection(connectionString);
        }

        public static BookRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new BookRepository();
            }
            return instance;
        }

        //===================== SIMPLE FUNCTIONS ========================//

        public bool CheckIfBookExists(int bookID) { 

            // subquery 이용 true false 반환
            string checkQuery = string.Format("SELECT EXISTS (SELECT TRUE FROM bookDB WHERE id = '{0}')", bookID);

            connection.Open();
            MySqlCommand command = new MySqlCommand(checkQuery, connection);
            bool exists = Convert.ToBoolean(command.ExecuteScalar());
            connection.Close();

            if (exists) return true;
            else return false;
        }

        public Dictionary<int, BookDTO> GetBookDB() {

            bookDB.Clear();
            
            string getAllBookQuery = string.Format("SELECT * FROM bookDB");

            connection.Open();
            MySqlCommand command = new MySqlCommand(getAllBookQuery, connection);
            MySqlDataReader reader = command.ExecuteReader();

            // 한 개만 왔으므로 read 한번만 호출
            while (reader.Read())
            { 
                BookDTO book = new BookDTO();
                book.SetId(int.Parse(reader["id"].ToString()));
                book.SetName(reader["name"].ToString());
                book.SetAuthor(reader["author"].ToString());
                book.SetPublisher(reader["publisher"].ToString());
                book.SetPrice(reader["price"].ToString());
                book.SetInStock(reader["instock"].ToString());
                book.SetDate(reader["date"].ToString());
                book.SetIsbn(reader["isbn"].ToString());

                bookDB.Add(book.GetId(), book);
            }

            connection.Close();

            return bookDB;
        }

        public BookDTO GetBookByID(int bookID) {
            bookDB.Clear();
            bookDB = GetBookDB();
            return bookDB[bookID]; 
        }


        //==================== CRUD ===================//
        public bool Add(BookDTO book)
        {
            string insertQuery = string.Format("INSERT INTO bookDB (name, author, publisher, price, instock, date, isbn) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                book.GetName(), book.GetAuthor(), book.GetPublisher(), book.GetPrice(), book.GetInStock(), book.GetDate(), book.GetIsbn());

            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();

            return true;
        }

        public bool Delete(int deletingBookID)
        {
            string deleteQuery = string.Format("DELETE FROM bookDB WHERE id = '{0}'", deletingBookID);

            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
            
            return true;
        }

        public void Update(int updatingBookID, BookDTO book)
        {
            // 기존꺼 삭제하고 업데이트된 책을 추가
            
            string updateQuery = string.Format("UPDATE bookDB SET name = '{0}', author = '{1}', publisher = '{2}', price = '{3}', instock = '{4}', date = '{5}', isbn = '{6}' WHERE id = '{7}'",
                book.GetName(), book.GetAuthor(), book.GetPublisher(), book.GetPrice(), book.GetInStock(), book.GetDate(), book.GetIsbn(), book.GetId());

            connection.Open();
            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateStock(int bookID, string updatedStock)
        {
            string updateQuery = string.Format("UPDATE bookDB SET instock = '{0}' WHERE id = '{1}'", updatedStock, bookID);

            connection.Open();
            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}