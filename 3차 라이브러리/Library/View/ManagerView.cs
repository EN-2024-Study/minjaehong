using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class ManagerView
    {
        public ManagerView() { }

        // MANAGER MODE의 초기화면
        public ManagerMenuState ManagerMenuForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[MANAGER MENU]");
            MyConsole.PrintAllMenu(Constants.managerMenuArr);
            MyConsole.PrintUserName("manager");

            ManagerMenuState selectedMenu = (ManagerMenuState)InputHandler.GetUserSelection(Constants.managerMenuArr);

            return selectedMenu;
        }

        // 1. 전체도서 출력
        public void PrintAllBooksForm(List<BookDTO> allBooks)
        {
            PrintSelectedBooksForm(allBooks);
        }

        // 2. 도서 찾기
        // USER가 입력한 걸 List로 반환
        // 해당 List 해석에 따른 BookDTO 반환은 Model에서 해줌
        public List<string> FindBookForm()
        {
            Console.Clear();
            
            MyConsole.PrintHeader("[FIND BOOK]");
            MyConsole.PrintAllMenu(Constants.findBookArr);

            return InputHandler.GetUserInputs(Constants.findBookArr.Length);
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

        // 3. 도서 추가
        // USER가 입력한걸 List로 반환
        // 해당 List 값들로 BookDTO 만드는건 Model에서 해줌
        // Model에서 BookDTO로 만들고 추가까지 해줌
        public List<string> AddBookForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[ADD BOOK]");
            MyConsole.PrintAllMenu(Constants.addBookArr);
            
            List<string> bookInfo = InputHandler.GetUserInputs(Constants.addBookArr.Length);
            return bookInfo;
        }

        // 4. 도서 삭제
        // view에서 controller로 ID만 전달
        // controller에서는 model로 ID 그대로 전달
        public int DeleteBookForm()
        {
            Console.Clear();

            // 화면 구성
            MyConsole.PrintHeader("[DELETE BOOK]");
            MyConsole.PrintAllMenu(Constants.deleteBookArr);

            // user 입력 받기
            List<string> userInput = InputHandler.GetUserInputs(Constants.deleteBookArr.Length);

            int deletingBookID = int.Parse(userInput[0]);

            return deletingBookID;
        }

        // 5-1. 도서 수정 - 수정할 도서 선택
        // view에서 controller로 ID만 전달
        // 그럼 다시 controller에서 UpdateBookForm 불러서 수정사항 작성시키기
        public int UpdateBookSelectForm()
        {
            Console.Clear();

            int updatingBookID;

            // 화면 구성
            MyConsole.PrintHeader("[UPDATE BOOK]");
            MyConsole.PrintAllMenu(Constants.updateBookSelectArr);

            // USER 입력 받기            
            List<string> userInput = InputHandler.GetUserInputs(Constants.updateBookSelectArr.Length);
            
            updatingBookID = int.Parse(userInput[0]);
            
            return updatingBookID;
        }

        // 5-2. 도서 수정 - 수정 사항 입력시키기
        // view에서 controller로 List<string> 으로 전달
        // controller에서 BookDTO로 바꿔서 model로 보내주기
        public List<string> UpdateBookForm()
        {
            Console.Clear();

            // 화면 구성
            MyConsole.PrintHeader("[UPDATE BOOK FORM]");
            MyConsole.PrintAllMenu(Constants.updateBookArr);

            // USER 입력받기
            List<string> updatedBookInfo = InputHandler.GetUserInputs(Constants.updateBookArr.Length);

            return updatedBookInfo;
        }

        // 6. 전체 멤버 출력
        // controller가 model에서 MemberDTO List로 받아옴
        // 그걸 얘한테 넘겨주면 알아서 출력해줌
        public void PrintAllMembersForm(List<MemberDTO> allMembers)
        {
            Console.Clear();

            if (allMembers.Count == 0)
            {
                MyConsole.PrintHeader("[NO RESULT]");
            }
            else
            {
                MyConsole.PrintHeader("[YOUR RESULTS]");
                MyConsole.PrintAllMembers(allMembers);
            }

            // 뭐 누르면 화면 바뀜
            Console.ReadLine();
        }
    }
}
