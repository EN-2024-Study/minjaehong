using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Library
{
    // 여기서 조건 따지지말기
    // 조건은 모두 Service에서 따지고 온다
    // 여기에 조건이 있으면 안됨
    class BookRepository
    {
        // CRUD해서 받을 것들을 Dictionary로 저장
        private Dictionary<int, BookDTO> bookDB;

        private string connectionString;
        private MySqlConnection connection;
        private MySqlCommand command;

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
            connection.Open();

            command.CommandText = Querys.bookDBInitializeQuery;
            command.ExecuteNonQuery();

            command.CommandText = Querys.autoIncrementInitializeQuery;
            command.ExecuteNonQuery();
            
            connection.Close();
        }

        public bool CheckIfBookExists(int bookID)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.checkIfBookExistsQuery;
            command.Parameters.AddWithValue("@BookID", bookID);
            // 어차피 값이 하나밖에 안날라옴
            bool exists = Convert.ToBoolean(command.ExecuteScalar());
            
            connection.Close();

            return exists;
        }

        public bool CheckIfBookAvailable(int bookID)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.checkIfBookAvailableQuery;
            command.Parameters.AddWithValue("@BookID", bookID);
            bool exists = Convert.ToBoolean(command.ExecuteScalar());

            connection.Close();

            return exists;
        }

        // 전체 책(삭제된거 포함)을 다 끌고옴
        // 이건 returnedBooks 에서만 필요함
        // returned에서는 삭제된 것들도 다 보여줘야하기 때문임
        // "SELECT * FROM bookDB WHERE deleted = FALSE";
        public Dictionary<int, BookDTO> GetAllBooks()
        {
            bookDB.Clear();

            connection.Open();

            command.CommandText = Querys.getAllBooksQuery;
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
                book.SetDeleted(reader.GetBoolean("deleted"));

                bookDB.Add(book.GetId(), book);
            }

            reader.Close();
            connection.Close();

            return bookDB;
        }

        public Dictionary<int, BookDTO> GetAvailableBooks()
        {
            bookDB.Clear();

            connection.Open();

            command.CommandText = Querys.getAvailableBooksQuery;
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
                book.SetDeleted(reader.GetBoolean("deleted"));

                bookDB.Add(book.GetId(), book);
            }

            reader.Close();
            connection.Close();

            return bookDB;
        }

        // 특정 책만 가져옴
        public BookDTO GetBookByID(int bookID)
        {
            bookDB.Clear();
            bookDB = GetAllBooks();
            return bookDB[bookID];
        }


        //==================== CRUD ===================//
        public bool Add(BookDTO book)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.addNewBookQuery;
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


        // 실제로 delete 하는게 아니라 deleted 값만 TRUE로 바꿔줌
        // 실제로 delete하면 반납내역에서 못띄우는거 방지하기 위해
        public bool Delete(int deletingBookID)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.deleteBookQuery;
            command.Parameters.AddWithValue("@deletingBookID", deletingBookID);
            command.ExecuteNonQuery();

            connection.Close();

            return true;
        }

        public void Update(int updatingBookID, BookDTO book)
        {
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.updateBookQuery;
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
            connection.Open();
            command.Parameters.Clear();

            command.CommandText = Querys.updateBookStockQuery;
            command.Parameters.AddWithValue("@updatedStock", updatedStock);
            command.Parameters.AddWithValue("@bookID", bookID);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}