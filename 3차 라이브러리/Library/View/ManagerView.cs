using System;
using System.Collections.Generic;

namespace Library
{
    class ManagerView
    {
        private string[] managerMenuArr = { "전체 도서", "도서 찾기", "도서 추가", "도서 삭제", "도서 수정", "회원 관리", "대여 내역", "네이버 검색", "로그 관리", "요청 도서" };

        private string[] findBookArr = { "FIND BY NAME :", "FIND BY AUTHOR :" };

        private string[] addBookArr = { "1. NAME :", "2. AUTHOR :", "3. PUBLISHER :", "4. PRICE :", "5. IN STOCK :", "6. DATE(XXXXXX) :", "7. ISBN(3-2-4) :" };

        private string[] deleteBookArr = { "DELETING BOOK ID :" };

        private string[] updateBookSelectArr = { "UPDATING BOOK ID :" };

        private string[] updateBookArr = { "1. NAME :", "2. AUTHOR :", "3. PUBLISHER :", "4. PRICE :", "5. IN STOCK :", "6. DATE(XXXXXX) :", "7. ISBN(3-2-4) :" };

        private string[] applyRequestedBookArr = { "APPLYING BOOK ID :" };

        public ManagerView() { 
                
        }

        // MANAGER MODE의 초기화면
        public ManagerMenuState ManagerMenuForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[MANAGER MENU]");
            MyConsole.PrintUserID("manager");

            ManagerMenuState selectedMenu = (ManagerMenuState)MyConsole.GetUserSelection(managerMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
            return selectedMenu;
        }

        // 1. 전체도서 출력
        public void PrintAllBooksForm(List<BookDTO> allBooks)
        {
            Console.Clear();

            MyConsole.PrintHeader("[ALL BOOKS]");
            CommonView.PrintAllBooks(allBooks);

            MyConsole.WaitForBackSpace();
        }

        // 2. 도서 찾기
        // USER가 입력한 걸 List로 반환
        // 해당 List 해석에 따른 BookDTO 반환은 Model에서 해줌
        public List<string> FindBookForm()
        {
            Console.Clear();
            
            MyConsole.PrintHeader("[FIND BOOK]");
            List<string> filters = MyConsole.GetUserInputs(findBookArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.findBookExceptionArr);
            
            return filters;
        }

        public void PrintSelectedBooksForm(List<BookDTO> selectedBooks)
        {
            Console.Clear();

            MyConsole.PrintHeader("[RESULTS]");
            CommonView.PrintAllBooks(selectedBooks);

            MyConsole.WaitForBackSpace();
        }

        // 3. 도서 추가
        // manager가 추가한 책에 대한 정보를 받고 view가 DTO로 변환해서 controller한테 넘겨줌
        public BookDTO AddBookForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[ADD BOOK]");
            List<string> bookInfo = MyConsole.GetUserInputs(addBookArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.addBookExceptionArr);
            
            return new BookDTO(bookInfo);
        }

        // 4. 도서 삭제
        // view에서 controller로 ID만 전달
        // controller에서는 model로 ID 그대로 전달
        public string DeleteBookForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[DELETE BOOK]");
            List<string> userInput = MyConsole.GetUserInputs(deleteBookArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.deleteBookExceptionArr);
            
            string deletingBookID = userInput[0];

            return deletingBookID;
        }

        // 5-1. 도서 수정 - 수정할 도서 선택
        // view에서 controller로 ID만 전달
        // 그럼 다시 controller에서 UpdateBookForm 불러서 수정사항 작성시키기
        public string UpdateBookSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[UPDATE BOOK]");          
            List<string> userInput = MyConsole.GetUserInputs(updateBookSelectArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.updateBookSelectExceptionArr);
            
            string updatingBookID = userInput[0];

            return updatingBookID;
        }

        // 5-2. 도서 수정 - 수정 사항 입력시키기
        // view에서 controller로 List<string> 으로 전달
        // controller에서 BookDTO로 바꿔서 model로 보내주기
        public BookDTO UpdateBookForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[UPDATE BOOK FORM]");
            List<string> updatedBookInfo = MyConsole.GetUserInputs(updateBookArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.updateBookExceptionArr);
            
            return new BookDTO(updatedBookInfo);
        }

        // 6. 전체 멤버 출력
        // controller가 model에서 MemberDTO List로 받아옴
        // 그걸 얘한테 넘겨주면 알아서 출력해줌
        public void PrintAllMembersForm(List<MemberDTO> memberList)
        {
            Console.Clear();

            MyConsole.PrintHeader("[MEMBER INFO]");

            if (memberList.Count == 0)
            {
                MyConsole.PrintHeader("[NO RESULT]");
            }

            Console.SetCursorPosition(MyConsole.BOOK_STARTX, MyConsole.BOOK_STARTY);
            for (int i = 0; i < memberList.Count; i++)
            {
                Console.WriteLine("");
                Console.CursorLeft = MyConsole.BOOK_STARTX;
                Console.WriteLine("────────────────────────────────");
                Console.WriteLine("");
                Console.CursorLeft = MyConsole.BOOK_STARTX;
                Console.WriteLine("ID       : " + memberList[i].GetId());
                Console.CursorLeft = MyConsole.BOOK_STARTX;
                Console.WriteLine("PW       : " + memberList[i].GetPw());
                Console.CursorLeft = MyConsole.BOOK_STARTX;
                Console.WriteLine("NAME     : " + memberList[i].GetName());
                Console.CursorLeft = MyConsole.BOOK_STARTX;
                Console.WriteLine("AGE      : " + memberList[i].GetAge());
                Console.CursorLeft = MyConsole.BOOK_STARTX;
                Console.WriteLine("PHONENUM : " + memberList[i].GetPhoneNum());
            }
            Console.WriteLine("");
            Console.CursorLeft = MyConsole.BOOK_STARTX;
            Console.WriteLine("────────────────────────────────");

            MyConsole.WaitForBackSpace();
        }

        // 7. 요청 도서 승인
        // 승인해줄 요청도서 입력받고 controller한테 주기
        public string ApplyRequestedBookSelectForm()
        {
            List<string> userInput = MyConsole.GetUserInputs(applyRequestedBookArr, MyConsole.MENU_STARTX, Console.CursorTop+2, ExceptionHandler.applyRequestedBookExceptionArr);
            string applyingBookID = userInput[0];

            return applyingBookID;
        }
    }
}
