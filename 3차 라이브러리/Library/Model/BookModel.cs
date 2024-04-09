using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BookModel
    {
        private static BookModel instance;
        
        private BookModel()
        {
            keyId = 0;
            bookDB = new Dictionary<int, BookDTO>();
        }

        public static BookModel GetInstance()
        {
            if (instance == null)
            {
                instance = new BookModel();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        // Model에 BookDTO들 저장할 Dictionary 있음
        Dictionary<int, BookDTO> bookDB;
        // Dictionary Key값으로 사용할거임
        private int keyId;

        // book 존재 여부 확인
        public bool FindBookByName(string name)
        {
            for(int i = 0; i < bookDB.Count; i++)
            {
                if (bookDB[i].GetName() == name) return true;
            }
            return false;
        }

        public bool AddNewBook(BookDTO newBook)
        {
            keyId++;
            // db에 넣고 true 반환
            newBook.SetId(keyId);
            bookDB.Add(keyId, newBook);
            return true;
        }

        public bool DeleteBook(int deletingBookId)
        {
            bookDB.Remove(deletingBookId);
            return true;
        }

        public bool UpdateBook(BookDTO updatingBook)
        {
            DeleteBook(updatingBook.GetId());
            AddNewBook(updatingBook);
            return true;
        }

        public List<BookDTO> GetAllBook()
        {
            return bookDB.Values.ToList();
        }
    }
}