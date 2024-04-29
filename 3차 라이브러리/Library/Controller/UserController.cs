using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    class UserController
    {
        private string curUserID;

        private UserView userView;

        private MemberService memberService;
        private BookService bookService;

        private LogRepository logRepository;
        
        public UserController(BookService bookService, MemberService memberService)
        {
            this.bookService = bookService;
            this.memberService = memberService;

            this.logRepository = LogRepository.GetInstance();
            
            userView = new UserView();
        }

        // UserFrontController에서 LOGIN 후 UserController.run이 실행되기 전에
        // 매번 이 함수가 호출되어 curUserID를 초기화해줌
        public void InitializeUserController(string curUserID)
        {
            this.curUserID = curUserID;
        }

        private void PrintAllBook()
        {
            List<BookDTO> availableBooks = bookService.GetAvailableBooks();
            userView.PrintAllBooksForm(availableBooks);

            LogDTO logDTO = new LogDTO();
            logDTO.SetAction("PRINTALLBOOK");
            logDTO.SetTime(DateTime.Now);
            logDTO.SetMode(false);
            logRepository.Add(logDTO);
        }

        private void Find()
        {
            // 찾을 책에 대한 정보를 userView에서 받아오기
            List<string> dataFromView = userView.FindBookForm();
            // bookService로 전달해서 매칭된 List<BookDTO> 받아오기
            List<BookDTO> retList = bookService.FindBook(dataFromView);
            // 다시 userView에 전달해서 매칭된 책들 모두 출력해서 보여주기
            userView.PrintSelectedBooksForm(retList);
        }

        private void Borrow()
        {
            List<BookDTO> bookList = bookService.GetAvailableBooks();

            // BORROW할 책에 대한 정보를 userView에서 받아오기
            List<string> dataFromView = userView.BorrowBookForm(bookList);
            // 책 정보를 MiniDTO에 담아주기
            MiniDTO miniDTO = new MiniDTO(dataFromView);

            // 해당 책이 존재하면 bookService에 누가 빌린거 적용해주기
            int bookID = int.Parse(miniDTO.GetBookID());

            // 이미 빌린거면 대여불가
            if(memberService.CheckIfUserBorrowed(curUserID, miniDTO))
            {
                CommonView.RuntimeMessageForm("YOU ALREADY BORROWED THIS BOOK!");
                return;
            }

            // 수량이 부족하거나 존재하지 않으면
            if (!bookService.CheckIfBookAvailable(bookID))
            {
                CommonView.RuntimeMessageForm("THIS BOOK IS NOT AVAILABLE!");
            }

            // 대여가능하면 그제서야 진짜로 대여 저굥
            if (bookService.CheckIfBookAvailable(bookID))
            {
                bookService.UpdateBorrowed(miniDTO);
                memberService.UpdateBorrow(curUserID, miniDTO);
                CommonView.RuntimeMessageForm("BORROW SUCCESSFUL!");

            }
        }

        private void CheckBorrow()
        {
            List<int> curUserBorrowedBookIDs = memberService.GetMemberBorrowedBooks(curUserID);
            List<BookDTO> curUserBorrowedBooks = new List<BookDTO>();

            for (int i = 0; i < curUserBorrowedBookIDs.Count; i++)
            {
                int curId = curUserBorrowedBookIDs[i];
                curUserBorrowedBooks.Add(bookService.GetBookByID(curId));
            }
            userView.CheckBorrowedForm(curUserBorrowedBooks);
        }

        private void Return()
        {
            // RETURN할 책에 대한 정보를 userView에서 받아오기
            List<string> dataFromView = userView.ReturnBookForm();
            // 책 정보를 MiniDTO에 담아주기
            MiniDTO miniDTO = new MiniDTO(dataFromView);

            // 만약 진짜로 빌린거면
            if (memberService.CheckIfUserBorrowed(curUserID, miniDTO))
            {
                // bookService에 누가 반납한거 적용해주기
                bookService.UpdateReturned(miniDTO);
                // 성공하면 memberService로 가서 저장해주기
                memberService.UpdateReturned(curUserID, miniDTO);
                CommonView.RuntimeMessageForm("BOOK RETURN SUCCESSFUL!");
            }
            else
            {
                CommonView.RuntimeMessageForm("YOU DIDNT BORROW ID " + miniDTO.GetBookID() + " BOOK!");
            }
        }

        private void CheckReturn()
        {
            List<int> curUserReturnedBookIDs = memberService.GetMemberReturnedBooks(curUserID);
            List<BookDTO> curUserReturnedBooks = new List<BookDTO>();

            for (int i = 0; i < curUserReturnedBookIDs.Count; i++)
            {
                int curId = curUserReturnedBookIDs[i];
                curUserReturnedBooks.Add(bookService.GetBookByID(curId));
            }
            userView.CheckReturnedForm(curUserReturnedBooks);
        }

        private void UpdateInfo()
        {
            // PW NAME AGE PHONENUM 입력받은거 가져오기
            List<string> updatedUserInfo = userView.UpdateMyInfoForm();

            MemberDTO updatedMember = new MemberDTO(updatedUserInfo);
            // ID는 controller에서 따로 세팅
            updatedMember.SetId(curUserID);
            memberService.UpdateMember(curUserID, updatedMember);
            CommonView.RuntimeMessageForm("USER INFO UPDATE SUCCESSFUL!");
        }

        void DeleteMySelf()
        {
            if (memberService.GetMemberBorrowedBooks(curUserID).Count() == 0)
            {
                memberService.DeleteMember(curUserID);
                CommonView.RuntimeMessageForm("PERMANANT DELETE SUCCESSFUL!");
                // usercontroller 자체를 빠져나가기
                return;
            }
            else
            {
                CommonView.RuntimeMessageForm("PLEASE RETURN ALL YOUR BOOKS FIRST");
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

                    case UserMenuState.FIND:
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
                    
                    case UserMenuState.UPDATEINFO:
                        UpdateInfo();
                        break;
                    
                    case UserMenuState.DELETEMYSELF:
                        DeleteMySelf();
                        isUserModeRunning = false;
                        break;
                    
                    case UserMenuState.NAVERSEARCH:
                        break;
                    
                    case UserMenuState.REQUESTED:
                        break;
                }
            }
        }
    }
}
