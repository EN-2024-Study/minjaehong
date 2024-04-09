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

            string[] ManagerMenuArr = { "도서 찾기", "도서 추가", "도서 삭제", "도서 수정", "회원 관리", "대여 내역", "네이버 검색", "로그 관리", "요청 도서" };

            MyConsole.PrintHeader("[MANAGER MENU]");
            MyConsole.PrintAllMenu(ManagerMenuArr);
            ManagerMenuState selectedMenu = (ManagerMenuState)MyConsole.GetUserSelection(ManagerMenuArr);

            return selectedMenu;
        }

        // USER가 입력한 걸 List로 반환
        // 해당 List 해석에 따른 BookDTO 반환은 Model에서 해줌
        public List<string> FindBookForm()
        {
            Console.Clear();

            string[] FindBookArr = { "제목으로 찾기 :", "작가명으로 찾기", "출판사로 찾기" };
            
            MyConsole.PrintHeader("[FIND BOOK]");
            MyConsole.PrintAllMenu(FindBookArr);
            return MyConsole.GetUserInputs(FindBookArr.Length);
        }

        public BookDTO AddBookForm()
        {
            Console.Clear();
            BookDTO newBook = new BookDTO();

            Console.WriteLine("          [Add Book]");
            Console.WriteLine("");

            Console.WriteLine("          book name : ");            
            Console.WriteLine("        book author : ");
            Console.WriteLine("         book price : ");
            Console.WriteLine("      book quantity : ");

            newBook.SetName(Console.ReadLine());
            newBook.SetAuthor(Console.ReadLine());
            newBook.SetPrice(Console.ReadLine());
            newBook.SetQuantity(Console.ReadLine());

            return newBook;
        }

        // view에서 controller로 id만 전달
        public int DeleteBookForm()
        {
            Console.Clear();
            Console.WriteLine("DELETING BOOK ID : ");
            int deletingBookId = int.Parse(Console.ReadLine());
            return deletingBookId;
        }

        // view에서 controller로 id만 전달
        public int UpdateBookSelectForm()
        {
            Console.Clear();
            int updatingBookId;
            Console.WriteLine("UPDATING BOOK ID :");
            updatingBookId = int.Parse(Console.ReadLine());
            return updatingBookId;
        }

        // view에서 controller로 BookDTO로 전달
        public BookDTO UpdateBookForm()
        {
            Console.Clear();
            BookDTO updatingBook = new BookDTO();

            Console.WriteLine("[Update Book]");
            Console.WriteLine("");

            Console.WriteLine("book name : ");
            updatingBook.SetName(Console.ReadLine());

            Console.WriteLine("book author: ");
            updatingBook.SetAuthor(Console.ReadLine());

            Console.WriteLine("book price : ");
            updatingBook.SetPrice(Console.ReadLine());

            Console.WriteLine("book quantity: ");
            updatingBook.SetQuantity(Console.ReadLine());

            return updatingBook;
        }

        public void PrintAllBookForm(List<BookDTO> bookList)
        {
            Console.Clear();

            for(int i = 0; i < bookList.Count; i++)
            {
                BookDTO curBook = bookList[i];

                Console.WriteLine("====================================\n");
                Console.WriteLine("book id : " + curBook.GetId());
                Console.WriteLine("book name : " + curBook.GetName());
                Console.WriteLine("book author : " + curBook.GetAuthor());
                Console.WriteLine("book price : " + curBook.GetPrice());
                Console.WriteLine("book quantity : " + curBook.GetQuantity());
                Console.WriteLine("");
            }
            Console.ReadLine();
        }
    }
}
