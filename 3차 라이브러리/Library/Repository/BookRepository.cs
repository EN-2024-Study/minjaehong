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

        string connectionString;
        MySqlConnection connection;
        MySqlCommand command;

        //===================== SINGELTON ========================//

        private static BookRepository instance;

        private BookRepository()
        {
            bookDB = new Dictionary<int, BookDTO>();

            connectionString = "Server=localhost;Database=ensharp;Uid=root;Pwd=1234;";
            connection = new MySqlConnection(connectionString);
            // 해당 DB에 사용할 command 객체 생성
            command = connection.CreateCommand();

            InitializeBookDB();
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

        // 매번 실행결과 확인 위해 지우고 다시 시작
        // 빼도 상관없음
        private void InitializeBookDB()
        {
            string deleteQuery = "DELETE FROM bookDB";
            string autoIncrementInitializeQuery = "ALTER TABLE bookDB AUTO_INCREMENT = 1";

            connection.Open();

            command.CommandText = deleteQuery;
            command.ExecuteNonQuery();
            command.CommandText = autoIncrementInitializeQuery;
            command.ExecuteNonQuery();
            
            connection.Close();
        }

        public bool CheckIfBookExists(int bookID)
        {
            // subquery 이용 true false 반환
            string checkQuery = "SELECT EXISTS (SELECT TRUE FROM bookDB WHERE id = @BookID)";

            connection.Open();
            command.Parameters.Clear();

            command.CommandText = checkQuery;
            command.Parameters.AddWithValue("@BookID", bookID);
            // 어차피 값이 하나밖에 안날라옴
            bool exists = Convert.ToBoolean(command.ExecuteScalar());
            
            connection.Close();

            if (exists) return true;
            else return false;
        }

        public Dictionary<int, BookDTO> GetBookDB()
        {
            bookDB.Clear();

            string getAllBookQuery = "SELECT * FROM bookDB";
            
            connection.Open();

            command.CommandText = getAllBookQuery;
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

            reader.Close();
            connection.Close();

            return bookDB;
        }

        public BookDTO GetBookByID(int bookID)
        {
            bookDB.Clear();
            bookDB = GetBookDB();
            return bookDB[bookID];
        }


        //==================== CRUD ===================//
        public bool Add(BookDTO book)
        {
            string insertQuery = "INSERT INTO bookDB (name, author, publisher, price, instock, date, isbn) " +
                "VALUES (@name, @author, @publisher, @price, @inStock, @date, @isbn)";

            connection.Open();
            command.Parameters.Clear();

            command.CommandText = insertQuery;
            command.Parameters.AddWithValue("@name", book.GetName());
            command.Parameters.AddWithValue("@author", book.GetAuthor());
            command.Parameters.AddWithValue("@publisher", book.GetPublisher());
            command.Parameters.AddWithValue("@price", book.GetPrice());
            command.Parameters.AddWithValue("@inStock", book.GetInStock());
            command.Parameters.AddWithValue("@date", book.GetDate());
            command.Parameters.AddWithValue("@isbn", book.GetIsbn());
            command.ExecuteNonQuery();

            connection.Close();

            return true;
        }


        public bool Delete(int deletingBookID)
        {
            string deleteQuery = "DELETE FROM bookDB WHERE id = @deletingBookID";

            connection.Open();
            command.Parameters.Clear();

            command.CommandText = deleteQuery;
            command.Parameters.AddWithValue("@deletingBookID", deletingBookID);
            command.ExecuteNonQuery();

            connection.Close();

            return true;
        }

        public void Update(int updatingBookID, BookDTO book)
        {
            // 기존꺼 삭제하고 업데이트된 책을 추가

            string updateBookQuery = "UPDATE bookDB SET name = @name, author = @author, publisher = @publisher, " +
                "price = @price, instock = @inStock, date = @date, isbn = @isbn WHERE id = @id";

            connection.Open();
            command.Parameters.Clear();

            command.CommandText = updateBookQuery;
            command.Parameters.AddWithValue("@name", book.GetName());
            command.Parameters.AddWithValue("@author", book.GetAuthor());
            command.Parameters.AddWithValue("@publisher", book.GetPublisher());
            command.Parameters.AddWithValue("@price", book.GetPrice());
            command.Parameters.AddWithValue("@inStock", book.GetInStock());
            command.Parameters.AddWithValue("@date", book.GetDate());
            command.Parameters.AddWithValue("@isbn", book.GetIsbn());
            command.Parameters.AddWithValue("@id", updatingBookID);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdateStock(int bookID, string updatedStock)
        {
            string updateStockQuery = "UPDATE bookDB SET instock = @updatedStock WHERE id = @bookID";

            connection.Open();
            command.Parameters.Clear();

            command.CommandText = updateStockQuery;
            command.Parameters.AddWithValue("@updatedStock", updatedStock);
            command.Parameters.AddWithValue("@bookID", bookID);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}