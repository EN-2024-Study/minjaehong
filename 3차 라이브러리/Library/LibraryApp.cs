using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LibraryApp
    {
        static void PutDummyData()
        {
            BookModel bookModel = BookModel.GetInstance();
            MemberModel memberModel = MemberModel.GetInstance();

            List<string> dummyArr1 = new List<string> { "book1", "kim", "AAA", "1000", "10" };
            List<string> dummyArr2= new List<string> { "book2", "kim", "BBB", "2500", "5" };
            List<string> dummyArr3 = new List<string> { "book3", "min", "BBB", "3000", "3" };
            List<string> dummyArr4 = new List<string> { "book4", "min", "CCC", "500", "7" };
            List<string> dummyArr5 = new List<string> { "book5", "park", "CCC", "4000", "8" };
            List<string> dummyArr6 = new List<string> { "book6", "lee", "CCC", "1500", "2" };
            bookModel.AddNewBook(new BookDTO(dummyArr1));
            bookModel.AddNewBook(new BookDTO(dummyArr2));
            bookModel.AddNewBook(new BookDTO(dummyArr3));
            bookModel.AddNewBook(new BookDTO(dummyArr4));
            bookModel.AddNewBook(new BookDTO(dummyArr5));
            bookModel.AddNewBook(new BookDTO(dummyArr6));

            List<string> dummyArr7 = new List<string> { "dog", "dog123", "강아지", "5", "010" };
            List<string> dummyArr8 = new List<string> { "cat", "cat123", "고양이", "4", "010" };
            List<string> dummyArr9 = new List<string> { "dolphin", "dolphin123", "돌고래", "7", "010" };
            memberModel.AddNewMember(new MemberDTO(dummyArr7));
            memberModel.AddNewMember(new MemberDTO(dummyArr8));
            memberModel.AddNewMember(new MemberDTO(dummyArr9));
        }
        
        public static void Main()
        {
            Console.CursorVisible = false;

            PutDummyData();
            // MainController.GetInstance() 호출하면 singleton 생성자들 다 호출되면서
            // 모든 객체와 참조관계가 다 세팅되고 시작함
            MainController mainController = MainController.GetInstance();
            mainController.run();
        }
    }
}
