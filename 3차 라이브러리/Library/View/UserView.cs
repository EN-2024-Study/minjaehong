using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserView
    {
        private static UserView instance;

        private UserView() { }

        public static UserView GetInstance()
        {
            if (instance == null)
            {
                instance = new UserView();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        // menu 보여주고 user가 select 한 걸 controller 한테 반환
        public UserMenuState UserMenuForm(string curUserID)
        {
            Console.Clear();

            string[] userMenuArr = { "전체 도서", "도서 찾기", "도서 대여", "대여 확인", "도서 반납", "반납 확인", "정보 수정", "계정 삭제", "네이버 검색", "요청 도서 내역" };

            MyConsole.PrintHeader("[USER MENU]");
            MyConsole.PrintAllMenu(userMenuArr);

            MyConsole.PrintUserName(curUserID);

            return (UserMenuState)MyConsole.GetUserSelection(userMenuArr);
        }

        // 1. 도서 찾기
        // 찾을 도서 이름과 작가를 controller에게 전달
        // controller는 BookModel에 가서 해당 매칭 결과를 List<string> 으로 받음
        // controller는 PrintSelectedBooks로 출력
        public List<string> FindBookForm()
        {
            Console.Clear();

            string[] findBookArr = { "FIND BY NAME", "FIND BY AUTHOR" };

            MyConsole.PrintHeader("[FIND BOOK]");
            MyConsole.PrintAllMenu(findBookArr);

            return MyConsole.GetUserInputs(findBookArr.Length);
        }

        // 2. 도서 대여
        public List<string> BorrowBookForm()
        {
            Console.Clear();

            string[] borrowBookArr = { "BOOK ID", "QUANTITY" };

            MyConsole.PrintHeader("[LETS BORROW A BOOK]");
            MyConsole.PrintAllMenu(borrowBookArr);
            
            // ID NUM 입력받아 controller한테 전달
            return MyConsole.GetUserInputs(borrowBookArr.Length);
        }

        // 3. 도서 대여 확인
        // controller가 BookModel에서 로그인한 USER가 BORROW한 책들을 List<BOOKDTO>로 보내줌
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

            string[] returnBookArr = { "BOOK ID", "QUANTITY" };

            MyConsole.PrintHeader("[LETS RETURN BOOK]");
            MyConsole.PrintAllMenu(returnBookArr);

            // ID NUM 입력받아 controller한테 전달
            return MyConsole.GetUserInputs(returnBookArr.Length);
        }

        // 5. 도서 반납 확인
        // controller가 BookModel에서 로그인한 USER가 RETURN한 책들을 List<BOOKDTO>로 보내줌
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

            string[] updateUserArr = { "1. PW","2. NAME","3. AGE","4. PHONENUM" };

            // 화면 구성
            MyConsole.PrintHeader("[UPDATE USER]");
            MyConsole.PrintAllMenu(updateUserArr);

            // 업데이트할 PW NAME AGE PHONENUM 입력받기
            List<string> updatedUserInfo = MyConsole.GetUserInputs(updateUserArr.Length);

            // ID는 입력받는게 아니므로 그냥 0번에 추가해서 ID PW NAME AGE PHONENUM 받은 것처럼 만들기
            updatedUserInfo.Insert(0, "");
            return updatedUserInfo;
        }

        // 7. 계정 삭제
        public void DeleteMyselfForm()
        {

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

                MyConsole.PrintBooks(selectedBooks);
            }

            // 뭐 누르면 화면 바뀜
            Console.ReadLine();
        }

    }
}
