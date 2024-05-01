using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // controller에서 호출하면
    // 조건 확인 후 DAO 호출해서 CRUD 작업 실행
    // 예외처리와 조건처리가 주된 작업
    class BookService
    {
        private BookDAO bookDAO;
        private HistoryDAO historyDAO;

        public BookService()
        {
            this.bookDAO = BookDAO.GetInstance();
            this.historyDAO = HistoryDAO.GetInstance();

            List<string> defaultBook1 = new List<string> { "book1", "kim", "companyA", "1000", "10", "990317", "111-11-1111" };
            List<string> defaultBook2 = new List<string> { "book2", "kim", "companyB", "2500", "5", "030811", "222-22-2222" };
            List<string> defaultBook3 = new List<string> { "book3", "min", "companyC", "3000", "3", "011103", "333-33-3333" };
            List<string> defaultBook4 = new List<string> { "book4", "min", "companyB", "500", "7", "971227", "444-44-4444" };
            List<string> defaultBook5 = new List<string> { "book5", "park", "companyB", "4000", "8", "150710", "555-55-5555" };
            AddNewBook(new BookDTO(defaultBook1));
            AddNewBook(new BookDTO(defaultBook2));
            AddNewBook(new BookDTO(defaultBook3));
            AddNewBook(new BookDTO(defaultBook4));
            AddNewBook(new BookDTO(defaultBook5));
        }

        //======================== CHECK FUNCTIONS BEFORE CRUDs ==========================//

        public bool CheckIfBookExists(int bookID)
        {
            return bookDAO.CheckIfBookExists(bookID);
        }

        public bool CheckIfBookAvailable(int bookID)
        {
            return bookDAO.CheckIfBookAvailable(bookID);
        }

        public bool CheckIfBookRequested(int bookID)
        {
            return bookDAO.CheckIfBookRequested(bookID);
        }

        public bool CheckIfBookIsBorrowed(int bookID)
        {
            return historyDAO.CheckIfBookIsBorrowed(bookID);
        }

        //============================= SIMPLE GET FUNCTIONS =============================//
        
        public BookDTO GetBookByID(int bookID)
        {
            return bookDAO.GetBookByID(bookID);
        }

        public List<BookDTO> GetAllBooks()
        {
            return bookDAO.GetAllBooks().Values.ToList();
        }

        // PrintAllBook
        // Find에서 이함수로 책 가져오게
        public List<BookDTO> GetAvailableBooks()
        {
            return bookDAO.GetAvailableBooks().Values.ToList();
        }

        public List<BookDTO> GetRequestedBooks()
        {
            return bookDAO.GetRequestedBooks().Values.ToList();
        }

        // BookDTO 조회 - view에서 제목 작가 넘어온거
        // retList controller로 보내주면 다시 view로 넘겨서 print 해줘야함
        public List<BookDTO> FindBook(List<string> dataFromView)
        {
            string bookName = dataFromView[0];
            string author = dataFromView[1];

            List<BookDTO> retList = new List<BookDTO>();

            // bookDAO에서 bookDB 가져오기
            Dictionary<int, BookDTO> bookDB = bookDAO.GetAvailableBooks();

            foreach (int curKey in bookDB.Keys)
            {
                // 삭제된 책이면 continue
                if (bookDB[curKey].GetDeleted()) continue;

                // 입력이 있었는데 맞지 않으면 continue
                if (bookName != "" && !bookDB[curKey].GetName().Contains(bookName)) continue;
                if (author != "" && !bookDB[curKey].GetAuthor().Contains(author)) continue;
                
                retList.Add(bookDB[curKey]);
            }

            return retList;
        }

        //==================================== CRUDs =====================================//

        public void AddNewBook(BookDTO newBook)
        {
            bookDAO.Add(newBook);
        }

        public bool DeleteBook(int deletingBookID)
        {
            // BOOK 존재 확인(bookDB) + BOOK 빌린 사람 없는지 확인(historyDB) 후 진행
            if (CheckIfBookExists(deletingBookID) && !CheckIfBookIsBorrowed(deletingBookID))
            {
                bookDAO.Delete(deletingBookID);
                return true;
            }
            return false;
        }

        // 얘는 책 존재성 확인 후에 호출되서 안에서 예외처리 안해줘도 됨
        // User가 입력한 새 책 정보가 들어옴
        public bool UpdateBook(int updatingBookID, BookDTO updatingBook)
        {
            // 만약 해당 책이 존재하면
            if (CheckIfBookExists(updatingBookID))
            {
                BookDTO originalBook = GetBookByID(updatingBookID);

                if (originalBook.GetDeleted()) return false;

                if (updatingBook.GetName() == "") updatingBook.SetName(originalBook.GetName());
                if (updatingBook.GetAuthor() == "") updatingBook.SetAuthor(originalBook.GetAuthor());
                if (updatingBook.GetPublisher() == "") updatingBook.SetPublisher(originalBook.GetPublisher());
                if (updatingBook.GetPrice() == "") updatingBook.SetPrice(originalBook.GetPrice());
                if (updatingBook.GetInStock() == "") updatingBook.SetInStock(originalBook.GetInStock());

                // 조건확인 후 CRUD
                bookDAO.Update(updatingBookID, updatingBook);
                return true;
            }
            return false;
        }

        public bool UpdateBorrowed(MiniDTO miniDTO)
        {
            int bookID = int.Parse(miniDTO.GetBookID());
            int borrowNum = int.Parse(miniDTO.GetQuantity());

            // BOOK이 존재하고 빌릴 수 있는만큼 QUANTITY가 남아있으면
            if (CheckIfBookExists(bookID))
            {
                // 남은 수량 업뎃
                int curNum = int.Parse(GetBookByID(bookID).GetInStock());
                int updatedStock = curNum - borrowNum;

                bookDAO.UpdateStock(bookID, updatedStock.ToString());

                return true;
            }
            return false;
        }

        public bool UpdateReturned(MiniDTO miniDTO)
        {
            // string으로 받아왔으니 int로 형변환 시키기 -> bookID는 int니까
            int bookID = int.Parse(miniDTO.GetBookID());
            // 계산을 위해 return 책 수 도 int로 형변환 시키기
            int returnedNum = int.Parse(miniDTO.GetQuantity());

            if (CheckIfBookExists(bookID))
            {
                // 남은 수량 업뎃
                int curNum = int.Parse(GetBookByID(bookID).GetInStock());
                int updatedStock = curNum + returnedNum;

                bookDAO.UpdateStock(bookID, updatedStock.ToString());
                return true;
            }
            return false;
        }

        public bool ApplyRequested(int applyingBookID)
        {
            if (CheckIfBookRequested(applyingBookID))
            {
                bookDAO.ApplyRequested(applyingBookID);
                return true;
            }
            return false;
        }
    }
}
