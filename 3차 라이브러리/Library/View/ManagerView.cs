using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ManagerView
    {
        private static ManagerView instance;

        private ManagerView() { }

        public static ManagerView GetInstance()
        {
            if (instance == null)
            {
                instance = new ManagerView();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        // MANAGER MODE의 초기화면
        public ManagerMenuState ManagerMenuForm()
        {
            Console.Clear();

            string[] managerMenuArr = { "도서 찾기", "도서 추가", "도서 삭제", "도서 수정", "전체 도서", "회원 관리", "대여 내역", "네이버 검색", "로그 관리", "요청 도서" };

            MyConsole.PrintHeader("[MANAGER MENU]");
            MyConsole.PrintAllMenu(managerMenuArr);
            ManagerMenuState selectedMenu = (ManagerMenuState)MyConsole.GetUserSelection(managerMenuArr);

            return selectedMenu;
        }

        // 1. 도서 찾기
        // USER가 입력한 걸 List로 반환
        // 해당 List 해석에 따른 BookDTO 반환은 Model에서 해줌
        public List<string> FindBookForm()
        {
            Console.Clear();

            string[] findBookArr = { "FIND BY NAME",
                                     "FIND BY AUTHOR"};
            
            MyConsole.PrintHeader("[FIND BOOK]");
            MyConsole.PrintAllMenu(findBookArr);
            return MyConsole.GetUserInputs(findBookArr.Length);
        }

        // 2. 도서 추가
        // USER가 입력한걸 List로 반환
        // 해당 List 값들로 BookDTO 만드는건 Model에서 해줌
        // Model에서 BookDTO로 만들고 추가까지 해줌
        public List<string> AddBookForm()
        {
            Console.Clear();
            BookDTO newBook = new BookDTO();

            string[] addBookArr = { "1. NAME", "2. AUTHOR", "3. PUBLISHER", "4. PRICE", "5. QUANTITY"};

            MyConsole.PrintHeader("[ADD BOOK]");
            MyConsole.PrintAllMenu(addBookArr);
            
            List<string> bookInfo = MyConsole.GetUserInputs(addBookArr.Length);
            return bookInfo;
        }

        // 3. 도서 삭제
        // view에서 controller로 ID만 전달
        // controller에서는 model로 ID 그대로 전달
        public int DeleteBookForm()
        {
            Console.Clear();

            string[] deleteBookArr = { "DELETING BOOK ID : " };

            // 화면 구성
            MyConsole.PrintHeader("[DELETE BOOK]");
            MyConsole.PrintAllMenu(deleteBookArr);

            // user 입력 받기
            List<string> userInput = MyConsole.GetUserInputs(deleteBookArr.Length);

            int deletingBookID = int.Parse(userInput[0]);

            return deletingBookID;
        }

        // 4. 도서 수정 - 수정할 도서 선택
        // view에서 controller로 ID만 전달
        // 그럼 다시 controller에서 UpdateBookForm 불러서 수정사항 작성시키기
        public int UpdateBookSelectForm()
        {
            Console.Clear();

            string[] updateBookArr = { "UPDATING BOOK ID : " };
            
            int updatingBookID;

            // 화면 구성
            MyConsole.PrintHeader("[UPDATE BOOK]");
            MyConsole.PrintAllMenu(updateBookArr);

            // USER 입력 받기            
            List<string> userInput = MyConsole.GetUserInputs(updateBookArr.Length);
            
            updatingBookID = int.Parse(userInput[0]);
            
            return updatingBookID;
        }

        // 4-2. 도서 수정 - 수정 사항 입력시키기
        // view에서 controller로 List<string> 으로 전달
        // controller에서 BookDTO로 바꿔서 model로 보내주기
        public List<string> UpdateBookForm()
        {
            Console.Clear();

            string[] updateBookArr= { "1. NAME", "2. AUTHOR", "3. PUBLISHER", "4. PRICE", "5. QUANTITY" };

            // 화면 구성
            MyConsole.PrintHeader("[UPDATE BOOK FORM]");
            MyConsole.PrintAllMenu(updateBookArr);

            // USER 입력받기
            List<string> updatedBookInfo = MyConsole.GetUserInputs(updateBookArr.Length);

            return updatedBookInfo;
        }

        public void PrintAllBooksForm(List<BookDTO> allBooks)
        {
            PrintSelectedBooksForm(allBooks);
        }

        public void PrintSelectedBooksForm(List<BookDTO> selectedBooks)
        {
            Console.Clear();

            if (selectedBooks.Count == 0){
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
