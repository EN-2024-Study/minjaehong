using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserView
    {
        public UserView() {
        }

        private string[] userMenuArr = { "전체 도서", "도서 찾기", "도서 대여", "대여 확인", "도서 반납", "반납 확인", "정보 수정", "계정 삭제", "네이버 검색", "요청 도서 내역" };

        private string[] findBookArr = { "FIND BY NAME :", "FIND BY AUTHOR :" };

        private string[] borrowBookArr = { "BOOK ID :" };

        private string[] returnBookArr = { "BOOK ID :" };

        private string[] updateUserArr = { "1. PW :", "2. NAME :", "3. AGE :", "4. PHONENUM(010~) :" };

        // menu 보여주고 user가 select 한 걸 controller 한테 반환
        public UserMenuState UserMenuForm(string curUserName)
        {
            Console.Clear();

            MyConsole.PrintHeader("[USER MENU]");
            MyConsole.PrintUserID(curUserName);
            UserMenuState selectedMode = (UserMenuState)MyConsole.GetUserSelection(userMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
            
            return selectedMode;
        }

        // 1. 도서 찾기
        // 찾을 도서 이름과 작가를 controller에게 전달
        // controller는 BookDAO에 가서 해당 매칭 결과를 List<string> 으로 받음
        // controller는 PrintSelectedBooks로 출력
        public List<string> FindBookForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[FIND BOOK]");
            List<string> filters = MyConsole.GetUserInputs(findBookArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.findBookExceptionArr);

            return filters;
        }

        // 2. 도서 대여
        public List<string> BorrowBookForm(List<BookDTO> bookList)
        {
            Console.Clear();

            MyConsole.PrintHeader("[LETS BORROW A BOOK]");
            CommonView.PrintAllBooks(bookList);
            // ID NUM 입력받아 controller한테 전달
            List<string> borrowInfo = MyConsole.GetUserInputs(borrowBookArr, MyConsole.MENU_STARTX, Console.CursorTop+2, ExceptionHandler.borrowBookExceptionArr);
            
            return borrowInfo;
        }

        // 3. 도서 대여 확인
        // controller가 BookDAO에서 로그인한 USER가 BORROW한 책들을 List<BOOKDTO>로 보내줌
        // 여기서는 PrintSelectedBooks 이용해서 출력
        public void CheckBorrowedForm(List<BookDTO> curUserBorrowedBooks)
        {
            PrintSelectedBooksForm(curUserBorrowedBooks);
            Console.Clear();
        }

        // 4. 도서 반납
        public List<string> ReturnBookForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[LETS RETURN BOOK]");
            
            // ID NUM 입력받아 controller한테 전달
            return MyConsole.GetUserInputs(returnBookArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.returnBookExceptionArr);
        }

        // 5. 도서 반납 확인
        // controller가 BookDAO에서 로그인한 USER가 RETURN한 책들을 List<BOOKDTO>로 보내줌
        // 여기서는 PrintSelectedBooks 이용해서 출력
        public void CheckReturnedForm(List<BookDTO> curUserReturnedBooks)
        {
            PrintSelectedBooksForm(curUserReturnedBooks);
            Console.Clear();
        }

        // 6. 정보 수정
        public List<string> UpdateMyInfoForm()
        {
            Console.Clear();

            // 화면 구성
            MyConsole.PrintHeader("[UPDATE USER]");
            
            // 업데이트할 PW NAME AGE PHONENUM 입력받기
            List<string> updatedUserInfo = MyConsole.GetUserInputs(updateUserArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.updateUserExceptionArr);
            
            // ID는 입력받는게 아니므로 그냥 0번에 추가해서 ID PW NAME AGE PHONENUM 받은 것처럼 만들기
            updatedUserInfo.Insert(0, "");
            return updatedUserInfo;
        }

        public void PrintAllBooksForm(List<BookDTO> allBooks)
        {
            PrintSelectedBooksForm(allBooks);
        }

        public void PrintSelectedBooksForm(List<BookDTO> selectedBooks)
        {
            Console.Clear();

            if (selectedBooks.Count == 0)
            {
                MyConsole.PrintHeader("[NO RESULT]");
            }
            else
            {
                MyConsole.PrintHeader("[YOUR RESULTS]");
                CommonView.PrintAllBooks(selectedBooks);
            }

            MyConsole.WaitForBackSpace();
        }
    }
}
