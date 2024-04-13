using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ManagerController
    { 
        // ManagerController와 연결되어야하는 애들
        ManagerView managerView;
        BookModel bookModel;
        MemberModel memberModel;
        
        // 생성자로 필요한 MVC 연결
        public ManagerController() {
            bookModel = BookModel.GetInstance();
            memberModel = MemberModel.GetInstance();

            managerView = new ManagerView();
        }

        public void Run()
        {
            ManagerMenuState selectedMenu; // magic number로 switch문 더 쉽게

            List<string> dataFromView;

            bool isManagerModeRunning = true;

            // MANAGER_MODE 일때
            while (isManagerModeRunning)
            {
                selectedMenu = managerView.ManagerMenuForm();

                switch (selectedMenu)
                {
                    case ManagerMenuState.GOBACK:
                        isManagerModeRunning = false;
                        break;

                    case ManagerMenuState.PRINTALLBOOK:
                        List<BookDTO> allBooks = bookModel.GetAllBooks();
                        managerView.PrintAllBooksForm(allBooks);
                        break;

                    case ManagerMenuState.FINDBOOK:

                        dataFromView = managerView.FindBookForm();
                        List<BookDTO> retList = bookModel.FindBook(dataFromView);
                        managerView.PrintSelectedBooksForm(retList);
                        break;

                    case ManagerMenuState.ADDBOOK:

                        List<string> newBookInfo = managerView.AddBookForm();
                        BookDTO newBook = new BookDTO(newBookInfo);
                        bookModel.AddNewBook(newBook);
                        break;
                    
                    case ManagerMenuState.DELETEBOOK:

                        int deletingBookID = managerView.DeleteBookForm();
                        bookModel.DeleteBook(deletingBookID);
                        break;
                    
                    case ManagerMenuState.UPDATEBOOK:

                        int updatingBookID = managerView.UpdateBookSelectForm();

                        // BOOK이 존재하면 수정 폼으로 이동
                        if (bookModel.FindBookById(updatingBookID))
                        {
                            List<string> updatedBookInfo = managerView.UpdateBookForm();
                            BookDTO updatedBook = new BookDTO(updatedBookInfo);
                            updatedBook.SetId(updatingBookID);
                            bookModel.UpdateBook(updatedBook);
                        }
                        break;

                    case ManagerMenuState.MEMBERMANAGEMENT:
                        List<MemberDTO> allMembers = memberModel.GetAllMember();
                        managerView.PrintAllMembersForm(allMembers);
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