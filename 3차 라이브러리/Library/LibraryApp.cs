using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LibraryApp
    {
        public static string managerID = "manager";
        public static string managerPW = "manager123";

        static void PutDummyData()
        {
            BookService bookService = BookService.GetInstance();
            MemberService memberService = MemberService.GetInstance();

            List<string> dummyArr1 = new List<string> { "book1", "kim", "companyA", "1000", "10", "990317", "111-11-1111" };
            List<string> dummyArr2 = new List<string> { "book2", "kim", "companyB", "2500", "5", "030811", "222-22-2222" };
            List<string> dummyArr3 = new List<string> { "book3", "min", "companyC", "3000", "3", "011103", "333-33-3333" };
            List<string> dummyArr4 = new List<string> { "book4", "min", "companyB", "500", "7", "971227", "444-44-4444" };
            List<string> dummyArr5 = new List<string> { "book5", "park", "companyB", "4000", "8", "150710", "555-55-5555" };
            List<string> dummyArr6 = new List<string> { "book6", "lee", "companyC", "1500", "2", "230208", "666-66-6666" };
            bookService.AddNewBook(new BookDTO(dummyArr1));
            bookService.AddNewBook(new BookDTO(dummyArr2));
            bookService.AddNewBook(new BookDTO(dummyArr3));
            bookService.AddNewBook(new BookDTO(dummyArr4));
            bookService.AddNewBook(new BookDTO(dummyArr5));
            bookService.AddNewBook(new BookDTO(dummyArr6));

            List<string> dummyArr7 = new List<string> { "dog", "dog123", "DOG", "5", "010-1234-5678" };
            List<string> dummyArr8 = new List<string> { "cat", "cat123", "CAT", "4", "010-5647-8497" };
            List<string> dummyArr9 = new List<string> { "dolphin", "dolphin123", "DOLPHIN", "7", "010-3781-3649" };
            memberService.AddNewMember(new MemberDTO(dummyArr7));
            memberService.AddNewMember(new MemberDTO(dummyArr8));
            memberService.AddNewMember(new MemberDTO(dummyArr9));
        }

        public static void Main()
        {
            PutDummyData();

            Console.CursorVisible = false;

            // MainController.GetInstance() 호출하면 singleton 생성자들 다 호출되면서
            // 모든 객체와 참조관계가 다 세팅되고 시작함
            MainController mainController = new MainController();
            mainController.Run();
        }
    }
}
