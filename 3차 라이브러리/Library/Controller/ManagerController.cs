using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    class ManagerController
    {
        // ManagerController와 연결되어야하는 애들
        private ManagerView managerView;

        private BookService bookService;
        private MemberService memberService;

        private Logger logger;

        // 생성자로 필요한 MVC 연결
        public ManagerController(BookService bookService, MemberService memberService) {
            
            managerView = new ManagerView();

            this.bookService = bookService; 
            this.memberService = memberService;

            logger = new Logger();
        }

        private void PrintAllBooks()
        {
            List<BookDTO> availableBooks = bookService.GetAvailableBooks();

            Logger.recordLog(DateTime.Now, "manager", "PRINT ALL BOOKS");

            managerView.PrintAllBooksForm(availableBooks);
        }
        
        private void FindBook()
        {
            List<string> dataFromView = managerView.FindBookForm();
            List<BookDTO> retList = bookService.FindBook(dataFromView);

            Logger.recordLog(DateTime.Now, "manager", "FIND BOOK");

            managerView.PrintSelectedBooksForm(retList);
        }

        private void AddBook()
        {
            BookDTO newBook = managerView.AddBookForm();

            bookService.AddNewBook(newBook);

            Logger.recordLog(DateTime.Now, "manager", "ADD BOOK SUCCESS", newBook.GetName());

            CommonView.RuntimeMessageForm("BOOK IS SUCCESSFULLY ADDED!");
        }

        private void DeleteBook()
        {
            string deletingBookID = managerView.DeleteBookForm();
            bool executionSuccess = bookService.DeleteBook(deletingBookID);

            if (executionSuccess)
            {
                Logger.recordLog(DateTime.Now, "manager", "DELETE BOOK SUCCESS", deletingBookID);
                
                CommonView.RuntimeMessageForm("BOOK IS SUCCESSFULLY DELETED!");
            }
            else
            {
                Logger.recordLog(DateTime.Now, "manager", "DELETE BOOK FAIL", "INVALID ID OR BEING BORROWED");
                
                CommonView.RuntimeMessageForm("DELETE FAILED : CHECK ID OR BORROWED MEMBERS");
            }
        }

        private void UpdateBook()
        {
            string updatingBookID = managerView.UpdateBookSelectForm();

            // BOOK이 존재하면 수정 폼으로 이동
            if (bookService.CheckIfBookExists(updatingBookID))
            {
                BookDTO updatingBookDTO = managerView.UpdateBookForm();
                updatingBookDTO.SetId(updatingBookID);

                bookService.UpdateBook(updatingBookID, updatingBookDTO);

                Logger.recordLog(DateTime.Now, "manager", "UPDATE BOOK SUCCESS", updatingBookID);

                CommonView.RuntimeMessageForm("BOOK IS SUCCESSFULLY UPDATED!");
            }
            else
            {
                Logger.recordLog(DateTime.Now, "manager", "UPDATE BOOK FAIL", "INVALID ID");

                CommonView.RuntimeMessageForm("THERE IS NO SUCH BOOK!");
            }
        }

        private void PrintAllMembers()
        {
            List<MemberDTO> allMembers = memberService.GetAllMember();

            Logger.recordLog(DateTime.Now, "manager", "MEMBER MANAGEMENT");

            managerView.PrintAllMembersForm(allMembers);
        }

        private void NaverSearch()
        {
            // NaverAPI 사용해서 처리하는 작업 자체를 RequestController 한테 위임
            RequestDTO requestDTO = CommonView.RequestBookForm();

            // Service 호출해서 검색된 책 받기
            Dictionary<string, BookDTO> searchedBooks = NaverService.GetBooksByNaverAPI(requestDTO);

            string requestedBookTitle = requestDTO.GetTitle();
            Logger.recordLog(DateTime.Now, "manager", "NAVER SEARCH", requestedBookTitle);

            // API로 받은 책들 출력하기
            CommonView.PrintAllBooks(searchedBooks.Values.ToList());

            MyConsole.WaitForBackSpace();
        }

        private void StartLogManagement()
        {
            Logger.recordLog(DateTime.Now, "manager", "LOG MANAGEMENT");

            logger.StartLogManagement();
        }

        private void ApplyRequestedBook()
        {
            // MEMBER들이 요청한 책들 일단 가져오기
            List<BookDTO> requestedBooks = bookService.GetRequestedBooks();
            // MEMBER들이 요청한 책 보여주기
            CommonView.PrintAllBooks(requestedBooks);

            if (requestedBooks.Count() == 0) return;

            // 승인할 책 입력받기
            string applyingBookID = managerView.ApplyRequestedBookSelectForm();

            // 승인 함 시도해보기
            bool executionSuccess = bookService.ApplyRequested(applyingBookID);

            // 승인 시도 결과 확인
            if (executionSuccess)
            {
                Logger.recordLog(DateTime.Now, "manager", "APPLY REQUEST BOOK SUCCESS");

                CommonView.RuntimeMessageForm("REQUESTED BOOK IS SUCCESSFULLY APPLIED!");
            }
            else
            {
                Logger.recordLog(DateTime.Now, "manager", "APPLY REQUEST BOOK FAIL", "INVALID ID");

                CommonView.RuntimeMessageForm("APPLY FAIL : CHECK REQUESTED BOOK ID");
            }
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
                        PrintAllBooks();
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

                    case ManagerMenuState.PRINTALLMEMBER:
                        PrintAllMembers();
                        break;
                    
                    case ManagerMenuState.NAVERSEARCH:
                        NaverSearch();
                        break;
                    
                    // 나중에 Logger로 바꾸기?
                    case ManagerMenuState.LOGMANAGEMENT:
                        StartLogManagement();
                        break;
                    
                    case ManagerMenuState.APPLYREQUESTED:
                        ApplyRequestedBook();
                        break;
                    
                    default:
                        break;
                }
            }
        }
    }
}