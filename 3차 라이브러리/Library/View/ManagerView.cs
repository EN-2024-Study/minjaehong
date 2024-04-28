using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        // USER가 입력한걸 List로 반환
        // 해당 List 값들로 BookDTO 만드는건 Model에서 해줌
        // Model에서 BookDTO로 만들고 추가까지 해줌
        public List<string> AddBookForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[ADD BOOK]");
            List<string> bookInfo = MyConsole.GetUserInputs(addBookArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.addBookExceptionArr);
            
            return bookInfo;
        }

        // 4. 도서 삭제
        // view에서 controller로 ID만 전달
        // controller에서는 model로 ID 그대로 전달
        public int DeleteBookForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[DELETE BOOK]");
            List<string> userInput = MyConsole.GetUserInputs(deleteBookArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.deleteBookExceptionArr);
            int deletingBookID = int.Parse(userInput[0]);

            return deletingBookID;
        }

        // 5-1. 도서 수정 - 수정할 도서 선택
        // view에서 controller로 ID만 전달
        // 그럼 다시 controller에서 UpdateBookForm 불러서 수정사항 작성시키기
        public int UpdateBookSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[UPDATE BOOK]");          
            List<string> userInput = MyConsole.GetUserInputs(updateBookSelectArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.updateBookSelectExceptionArr);
            int updatingBookID = int.Parse(userInput[0]);
            
            return updatingBookID;
        }

        // 5-2. 도서 수정 - 수정 사항 입력시키기
        // view에서 controller로 List<string> 으로 전달
        // controller에서 BookDTO로 바꿔서 model로 보내주기
        public List<string> UpdateBookForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[UPDATE BOOK FORM]");
            List<string> updatedBookInfo = MyConsole.GetUserInputs(updateBookArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.updateBookExceptionArr);
            
            return updatedBookInfo;
        }

        // 6. 전체 멤버 출력
        // controller가 model에서 MemberDTO List로 받아옴
        // 그걸 얘한테 넘겨주면 알아서 출력해줌
        public void PrintAllMembersForm(List<MemberDTO> memberList)
        {
            Console.Clear();

            MyConsole.PrintHeader("[MEMBER INFO]");
            CommonView.PrintAllMembers(memberList);

            MyConsole.WaitForBackSpace();
        }
    }
}
