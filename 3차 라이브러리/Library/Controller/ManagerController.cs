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

        // ManagerController와 연결되어야하는 애들
        ManagerView view;
        BookModel bookModel;
        MemberModel memberModel;
        
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

        public void Run()
        {
            ManagerMenuState selectedMenu; // magic number로 switch문 더 쉽게


            BookDTO book;
            List<string> dataFromView;

            bool isManagerModeRunning = true;

            // MANAGER_MODE 일때
            while (isManagerModeRunning)
            {
                selectedMenu = view.ManagerMenuForm();

                switch (selectedMenu)
                {
                    case ManagerMenuState.GOBACK:
                        isManagerModeRunning = false;
                        break;

                    case ManagerMenuState.PRINTALLBOOK:
                        List<BookDTO> allBooks = bookModel.GetAllBooks();
                        view.PrintAllBooksForm(allBooks);
                        break;

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

                    case ManagerMenuState.MEMBERMANAGEMENT:
                        List<MemberDTO> allMembers = memberModel.GetAllMember();
                        view.PrintAllMembersForm(allMembers);
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