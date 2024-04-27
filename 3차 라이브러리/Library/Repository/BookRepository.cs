using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // 여기서 조건 따지지말기
    // 조건은 모두 Service에서 따지고 온다
    // 여기에 조건이 있으면 안됨
    class BookRepository
    {
        Dictionary<int, BookDTO> bookDB;

        private int keyID;

        //===================== SINGELTON ========================//

        private static BookRepository instance;

        private BookRepository()
        {
            keyID = 1;
            bookDB = new Dictionary<int, BookDTO>();
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

        public bool CheckIfBookExists(int bookID) { return bookDB.ContainsKey(bookID); }
        public Dictionary<int, BookDTO> GetBookDB() { return bookDB; }
        public BookDTO GetBookByID(int bookID) { return bookDB[bookID]; }


        //==================== CRUD ===================//
        public void Add(BookDTO book)
        {
            book.SetId(keyID);
            bookDB.Add(keyID, book);
            keyID++;
        }

        public void Delete(int bookID)
        {
            bookDB.Remove(bookID);
        }

        public void Update(int bookID, BookDTO book)
        {
            // 기존꺼 삭제하고 업데이트된 책을 추가
            Delete(bookID);
            bookDB.Add(bookID, book);
        }

        public void UpdateStock(int bookID, string updatedStock)
        {
            bookDB[bookID].SetInStock(updatedStock);
        }
    }
}