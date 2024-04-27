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
        RuntimeView runtimeView;

        BookService bookService;
        MemberService memberService;
        
        // 생성자로 필요한 MVC 연결
        public ManagerController() {
            bookService = BookService.GetInstance();
            memberService = MemberService.GetInstance();

            managerView = new ManagerView();
            runtimeView = new RuntimeView();
        }

        void PrintAllBook()
        {
            List<BookDTO> allBooks = bookService.GetAllBooks();
            managerView.PrintAllBooksForm(allBooks);
        }
        
        void FindBook()
        {
            List<string> dataFromView = managerView.FindBookForm();
            List<BookDTO> retList = bookService.FindBook(dataFromView);
            managerView.PrintSelectedBooksForm(retList);
        }

        void AddBook()
        {
            List<string> newBookInfo = managerView.AddBookForm();
            BookDTO newBook = new BookDTO(newBookInfo);
            bookService.AddNewBook(newBook);
            runtimeView.RuntimeMessageForm("BOOK IS SUCCESSFULLY ADDED!");
        }

        void DeleteBook()
        {
            int deletingBookID = managerView.DeleteBookForm();
            bool executionSuccess = bookService.DeleteBook(deletingBookID);

            if (executionSuccess)
            {
                runtimeView.RuntimeMessageForm("BOOK IS SUCCESSFULLY DELETED!");
            }
            else
            {
                runtimeView.RuntimeMessageForm("THERE IS NO SUCH BOOK!");
            }
        }

        void UpdateBook()
        {
            int updatingBookID = managerView.UpdateBookSelectForm();

            // BOOK이 존재하면 수정 폼으로 이동
            if (bookService.CheckIfBookExists(updatingBookID))
            {
                List<string> updatedBookInfo = managerView.UpdateBookForm();
                BookDTO updatedBook = new BookDTO(updatedBookInfo);
                updatedBook.SetId(updatingBookID);
                bookService.UpdateBook(updatingBookID, updatedBook);
                runtimeView.RuntimeMessageForm("BOOK IS SUCCESSFULLY UPDATED!");
            }
            else
            {
                runtimeView.RuntimeMessageForm("THERE IS NO SUCH BOOK!");
            }
        }

        void MemberManagement()
        {
            List<MemberDTO> allMembers = memberService.GetAllMember();
            managerView.PrintAllMembersForm(allMembers);
        }

        public void RunManagerMode()
        {
            ManagerMenuState selectedMenu;
            bool isManagerModeRunning = true;

            while (isManagerModeRunning)
            {
                selectedMenu = managerView.ManagerMenuForm();
                
                switch (selectedMenu)
                {
                    case ManagerMenuState.GOBACK:
                        isManagerModeRunning = false;
                        break;

                    case ManagerMenuState.PRINTALLBOOK:
                        PrintAllBook();
                        break;

                    case ManagerMenuState.FINDBOOK:
                        FindBook();
                        break;

                    case ManagerMenuState.ADDBOOK:
                        AddBook();
                        break;
                    
                    case ManagerMenuState.DELETEBOOK:
                        DeleteBook();
                        break;
                    
                    case ManagerMenuState.UPDATEBOOK:
                        UpdateBook();
                        break;

                    case ManagerMenuState.MEMBERMANAGEMENT:
                        MemberManagement();
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