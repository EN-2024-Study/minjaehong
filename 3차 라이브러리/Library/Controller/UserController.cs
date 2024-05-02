using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    partial class UserController
    {
        private string curUserID;

        private UserView userView;
        
        private MemberService memberService;
        private BookService bookService;

        public UserController(BookService bookService, MemberService memberService)
        {
            this.bookService = bookService;
            this.memberService = memberService;

            userView = new UserView();
        }

        // UserFrontController에서 LOGIN 후 UserController 실행하기 전에
        // 매번 이 함수가 호출되어 curUserID를 초기화해줌
        public void InitializeUserController(string curUserID)
        {
            this.curUserID = curUserID;
        }

        private void PrintAllBook()
        {
            List<BookDTO> availableBooks = bookService.GetAvailableBooks();

            Logger.recordLog(DateTime.Now, curUserID, "PRINTALLBOOK", "");

            userView.PrintAllBooksForm(availableBooks);
        }

        private void Find()
        {
            // 찾을 책에 대한 정보를 userView에서 받아오기
            List<string> dataFromView = userView.FindBookForm();
            // bookService로 전달해서 매칭된 List<BookDTO> 받아오기
            List<BookDTO> retList = bookService.FindBook(dataFromView);

            Logger.recordLog(DateTime.Now, curUserID, "FIND_BOOK", "");

            // 다시 userView에 전달해서 매칭된 책들 모두 출력해서 보여주기
            userView.PrintSelectedBooksForm(retList);
        }

        private void Borrow()
        {
            List<BookDTO> bookList = bookService.GetAvailableBooks();

            // BORROW할 책에 대한 정보를 userView에서 받아오기
            string bookID = userView.BorrowBookForm(bookList);

            // 해당 책이 존재하면 bookService에 누가 빌린거 적용해주기
            
            // 이미 빌린거면 대여불가
            if(memberService.CheckIfUserBorrowed(curUserID, bookID))
            {
                Logger.recordLog(DateTime.Now, curUserID, "BORROW FAIL", "ALREADY BORROWED");
                
                CommonView.RuntimeMessageForm("YOU ALREADY BORROWED THIS BOOK!");
                
                return;
            }

            // 대여가능하면 대여
            if (bookService.CheckIfBookAvailable(bookID))
            {
                bookService.UpdateBorrowed(bookID);
                memberService.UpdateBorrow(curUserID, bookID);

                Logger.recordLog(DateTime.Now, curUserID, "BORROW SUCCESS", bookID.ToString());

                CommonView.RuntimeMessageForm("BORROW SUCCESSFUL!");
            }
            // 수량부족하거나 존재하지 않으면 대여불가
            else
            {
                Logger.recordLog(DateTime.Now, curUserID, "BORROW FAIL", "DOESNT EXIST");

                CommonView.RuntimeMessageForm("THIS BOOK IS NOT AVAILABLE!");
            }
            return;
        }

        private void CheckBorrow()
        {
            List<string> curUserBorrowedBookIDs = memberService.GetMemberBorrowedBooks(curUserID);
            List<BookDTO> curUserBorrowedBooks = new List<BookDTO>();

            for (int i = 0; i < curUserBorrowedBookIDs.Count; i++)
            {
                string curBookID = curUserBorrowedBookIDs[i];
                curUserBorrowedBooks.Add(bookService.GetBookByID(curBookID));
            }

            Logger.recordLog(DateTime.Now, curUserID, "CHECKED BORROW LIST");
            
            userView.CheckBorrowedForm(curUserBorrowedBooks);
        }

        private void Return()
        {
            // RETURN할 책에 대한 정보를 userView에서 받아오기
            string bookID = userView.ReturnBookForm();
            
            // 만약 진짜로 빌린거면 반납 적용해주기
            if (memberService.CheckIfUserBorrowed(curUserID, bookID))
            {
                bookService.UpdateReturned(bookID);
                memberService.UpdateReturned(curUserID, bookID);

                Logger.recordLog(DateTime.Now, curUserID, "RETURN SUCCESS", bookID);
                
                CommonView.RuntimeMessageForm("BOOK RETURN SUCCESSFUL!");
            }
            else
            {
                Logger.recordLog(DateTime.Now, curUserID, "RETURN FAIL", "DIDNT BORROW");

                CommonView.RuntimeMessageForm("YOU DIDNT BORROW ID " + bookID + " BOOK!");
            }
        }

        private void CheckReturn()
        {
            List<string> curUserReturnedBookIDs = memberService.GetMemberReturnedBooks(curUserID);
            List<BookDTO> curUserReturnedBooks = new List<BookDTO>();

            for (int i = 0; i < curUserReturnedBookIDs.Count; i++)
            {
                string curBookID = curUserReturnedBookIDs[i];
                curUserReturnedBooks.Add(bookService.GetBookByID(curBookID));
            }

            Logger.recordLog(DateTime.Now, curUserID, "CHECKED RETURN LIST");

            userView.CheckReturnedForm(curUserReturnedBooks);
        }

        private void UpdateMyInfo()
        {
            // Update된 member에 대한 정보 받기
            MemberDTO memberDTO = userView.UpdateMyInfoForm();
            memberDTO.SetId(curUserID);

            memberService.UpdateMember(curUserID, memberDTO);

            Logger.recordLog(DateTime.Now, curUserID, "UPDATED INFO");

            CommonView.RuntimeMessageForm("USER INFO UPDATE SUCCESSFUL!");
        }

        private bool DeleteMySelf()
        {
            if (memberService.GetMemberBorrowedBooks(curUserID).Count() == 0)
            {
                memberService.DeleteMember(curUserID);

                Logger.recordLog(DateTime.Now, curUserID, "ACCOUNT DELETE SUCCESS");

                CommonView.RuntimeMessageForm("PERMANANT DELETE SUCCESSFUL!");
                
                return true;
            }
            else
            {
                Logger.recordLog(DateTime.Now, curUserID, "ACCOUNT DELETE FAIL", "DIDNT RETURN ALL BOOKS");
                
                CommonView.RuntimeMessageForm("PLEASE RETURN ALL YOUR BOOKS FIRST");

                return false;
            }
        }

        private bool RequestBookByNaverAPI()
        {
            RequestDTO requestDTO = CommonView.RequestBookForm();

            // naverservice 호출해서 검색된 책들 받기
            Dictionary<string, BookDTO> searchedBooks = NaverService.GetBooksByNaverAPI(requestDTO);

            // 받은 책들 일단 출력
            CommonView.PrintAllBooks(searchedBooks.Values.ToList());

            // request 할 book title 받기
            string requestedBookTitle = userView.RequestBookTitleForm();

            if (searchedBooks.ContainsKey(requestedBookTitle))
            {
                bookService.AddNewBook(searchedBooks[requestedBookTitle]);
                CommonView.RuntimeMessageForm("BOOK REQUEST SUCCESS!");
                return true;
            }
            else
            {
                CommonView.RuntimeMessageForm("BOOK REQUEST FAIL!");
                return false;
            }
        }

        public void RunUserMode()
        {
            UserMenuState selectedMenu;

            bool isUserModeRunning = true;

            while (isUserModeRunning)
            {
                selectedMenu = userView.UserMenuForm(memberService.GetMemberByID(curUserID).GetName());

                switch (selectedMenu)
                {
                    case UserMenuState.GOBACK:
                        isUserModeRunning = false;
                        break;

                    case UserMenuState.PRINTALLBOOK:
                        PrintAllBook();
                        break;

                    case UserMenuState.FINDBOOK:
                        Find();
                        break;

                    case UserMenuState.BORROW:
                        Borrow();
                        break;

                    case UserMenuState.CHECKBORROW:
                        CheckBorrow();
                        break;
                        
                    case UserMenuState.RETURN:
                        Return();
                        break;
                    
                    case UserMenuState.CHECKRETURN:
                        CheckReturn();
                        break;
                    
                    case UserMenuState.UPDATEMYINFO:
                        UpdateMyInfo();
                        break;
                    
                    case UserMenuState.DELETEMYSELF:
                        isUserModeRunning = !DeleteMySelf();
                        break;
                    
                    case UserMenuState.NAVERSEARCH:
                        RequestBookByNaverAPI();
                        break;
                    
                    case UserMenuState.PRINTREQUESTED:
                        break;
                }
            }
        }
    }
}
