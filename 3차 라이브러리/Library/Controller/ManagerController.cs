using System.Collections.Generic;

namespace Library
{
    class ManagerController
    {
        // ManagerController와 연결되어야하는 애들
        private LogController logManager;
        private RequestController requestController;

        private ManagerView managerView;

        private BookService bookService;
        private MemberService memberService;
        
        // 생성자로 필요한 MVC 연결
        public ManagerController(BookService bookService, MemberService memberService) {
            this.bookService = bookService; 
            this.memberService = memberService;

            managerView = new ManagerView();

            logManager = new LogController();
            requestController = new RequestController();
        }

        private void PrintAllBook()
        {
            List<BookDTO> availableBooks = bookService.GetAvailableBooks();
            managerView.PrintAllBooksForm(availableBooks);
        }
        
        private void FindBook()
        {
            List<string> dataFromView = managerView.FindBookForm();
            List<BookDTO> retList = bookService.FindBook(dataFromView);
            managerView.PrintSelectedBooksForm(retList);
        }

        private void AddBook()
        {
            List<string> newBookInfo = managerView.AddBookForm();
            BookDTO newBook = new BookDTO(newBookInfo);
            bookService.AddNewBook(newBook);

            CommonView.RuntimeMessageForm("BOOK IS SUCCESSFULLY ADDED!");
        }

        private void DeleteBook()
        {
            int deletingBookID = managerView.DeleteBookForm();
            bool executionSuccess = bookService.DeleteBook(deletingBookID);

            if (executionSuccess) CommonView.RuntimeMessageForm("BOOK IS SUCCESSFULLY DELETED!");
            else CommonView.RuntimeMessageForm("DELETE FAILED : CHECK ID OR BORROWED MEMBERS");
        }

        private void UpdateBook()
        {
            int updatingBookID = managerView.UpdateBookSelectForm();

            // BOOK이 존재하면 수정 폼으로 이동
            if (bookService.CheckIfBookExists(updatingBookID))
            {
                List<string> updatedBookInfo = managerView.UpdateBookForm();
                BookDTO updatedBook = new BookDTO(updatedBookInfo);
                updatedBook.SetId(updatingBookID);
                bookService.UpdateBook(updatingBookID, updatedBook);
                CommonView.RuntimeMessageForm("BOOK IS SUCCESSFULLY UPDATED!");
            }

            else CommonView.RuntimeMessageForm("THERE IS NO SUCH BOOK!");
        }

        private void MemberManagement()
        {
            List<MemberDTO> allMembers = memberService.GetAllMember();
            managerView.PrintAllMembersForm(allMembers);
        }

        private void NaverSearch()
        {
            // NaverAPI 사용해서 처리하는 작업 자체를 RequestController 한테 위임
            requestController.SearchBookByNaverAPI();
        }

        // LogManager 시작
        private void LogManagement()
        {
            logManager.RunLogController();
        }

        private void ApplyRequestedBook()
        {
            // MEMBER들이 요청한 책들 일단 가져오기
            List<BookDTO> requestedBooks = bookService.GetRequestedBooks();
            // MEMBER들이 요청한 책 보여주기
            managerView.PrintAllBooksForm(requestedBooks);

            // 승인할 책 입력받기
            int applyingBookID = managerView.ApplyRequestedBookSelectForm();

            // 승인 함 시도해보기
            bool executionSuccess = bookService.ApplyRequested(applyingBookID);

            // 승인 시도 결과 확인
            if (executionSuccess) CommonView.RuntimeMessageForm("REQUESTED BOOK IS SUCCESSFULLY APPLIED!");
            else CommonView.RuntimeMessageForm("APPLY FAIL : CHECK REQUESTED BOOK ID");
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
                        NaverSearch();
                        break;
                    
                    case ManagerMenuState.LOGMANAGEMENT:
                        LogManagement();
                        break;
                    
                    case ManagerMenuState.APPLYREQUESTEDBOOK:
                        ApplyRequestedBook();
                        break;
                    
                    default:
                        break;
                }
            }
        }
    }
}