using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ManagerController
    {
        private static ManagerController instance;

        // 생성자로 MVC 연결
        private ManagerController() {
            this.bookModel = BookModel.GetInstance();
            this.memberModel = MemberModel.GetInstance();
            this.view = ManagerView.GetInstance();
        }

        public static ManagerController GetInstance()
        {
            if (instance == null)
            {
                instance = new ManagerController();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        // Controller는 Data 전달을 위해 두 개를 참조해야함
        BookModel bookModel;
        MemberModel memberModel;
        ManagerView view;

        public void run()
        {
            ManagerMenuState selectedMenu; // magic number로 switch문 더 쉽게

            BookDTO book;
            int bookId;
            List<string> dataFromView;

            // MANAGER_MODE 일때
            while (true)
            {
                selectedMenu = view.ManagerMenuForm();

                switch (selectedMenu)
                {
                    case ManagerMenuState.FINDBOOK:

                        dataFromView = view.FindBookForm();
                        List<BookDTO> retList = bookModel.FindBook(dataFromView);
                        view.PrintSelectedBooksForm(retList);
                        break;

                    case ManagerMenuState.ADDBOOK:

                        List<string> newBookInfo = view.AddBookForm();
                        BookDTO newBook = new BookDTO(newBookInfo);
                        bookModel.AddNewBook(newBook);
                        break;
                    
                    case ManagerMenuState.DELETEBOOK:

                        int deletingBookID = view.DeleteBookForm();
                        bookModel.DeleteBook(deletingBookID);
                        break;
                    
                    case ManagerMenuState.UPDATEBOOK:

                        int updatingBookID = view.UpdateBookSelectForm();
                        List<string> updatedBookInfo = view.UpdateBookForm();
                        BookDTO updatedBook = new BookDTO(updatedBookInfo);
                        updatedBook.SetId(updatingBookID);
                        bookModel.UpdateBook(updatedBook);
                        break;

                    case ManagerMenuState.PRINTALLBOOK:
                        List<BookDTO> allBooks = bookModel.GetAllBooks();
                        view.PrintAllBooksForm(allBooks);
                        break;

                    case ManagerMenuState.MEMBERMANAGEMENT:
                        break;
                    
                    case ManagerMenuState.BORROWLIST:
                        break;
                    
                    case ManagerMenuState.NAVERSEARCH:
                        break;
                    
                    case ManagerMenuState.LOGMANAGEMENT:
                        break;
                    
                    case ManagerMenuState.REQUESTEDBOOK:
                        break;
                    
                    default:
                        break;
                }
            }
        }
    }
}