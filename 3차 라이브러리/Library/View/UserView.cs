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
        public UserMenuState UserMenuForm()
        {
            Console.Clear();

            string[] userMenuArr = { "도서 찾기", "도서 대여", "대여 확인", "도서 반납", "반납 확인", "정보 수정", "계정 삭제", "네이버 검색", "요청 도서 내역" };

            MyConsole.PrintHeader("[USER MENU]");
            MyConsole.PrintAllMenu(userMenuArr);

            return (UserMenuState)MyConsole.GetUserSelection(userMenuArr);
        }

        // 1. 도서 찾기
        // 찾을 도서 이름과 작가를 controller에게 전달
        // controller는 BookModel에 가서 해당 매칭 결과를 List<string> 으로 받음
        // controller는 printBook으로 출력
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

            string[] borrowBookArr = { "BOOK ID", "BOOK NUM" };

            MyConsole.PrintHeader("[LETS BORROW A BOOK]");
            MyConsole.PrintAllMenu(borrowBookArr);
            
            // ID NUM 입력받아 controller한테 전달
            return MyConsole.GetUserInputs(borrowBookArr.Length);
        }

        // 3. 도서 대여 확인
        // controller가 BookModel
        public void CheckBorrowedForm()
        {
            Console.Clear();
        }

        // 4. 도서 반납
        public void ReturnForm()
        {
            Console.Clear();
        }

        // 5. 도서 반납 확인
        public void CheckReturnedForm()
        {
            Console.Clear();
        }

        // 6. 정보 수정
        public void UpdateMyInfoForm()
        {
            Console.Clear();
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
