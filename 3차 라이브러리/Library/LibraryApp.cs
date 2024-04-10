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

            List<string> dummyArr1 = new List<string> { "AAA", "AAA", "AAA", "1000", "10" };
            List<string> dummyArr2= new List<string> { "BBB", "BBB", "BBB", "2500", "5" };
            List<string> dummyArr3 = new List<string> { "CCC", "CCC", "CCC", "3000", "3" };
            bookModel.AddNewBook(new BookDTO(dummyArr1));
            bookModel.AddNewBook(new BookDTO(dummyArr2));
            bookModel.AddNewBook(new BookDTO(dummyArr3));

            List<string> dummyArr4 = new List<string> { "dog", "dog123", "개", "5", "010" };
            List<string> dummyArr5 = new List<string> { "cat", "cat123", "고양이", "4", "010" };
            List<string> dummyArr6 = new List<string> { "dolphin", "dolphin123", "돌고래", "7", "010" };
            memberModel.AddNewMember(new MemberDTO(dummyArr4));
            memberModel.AddNewMember(new MemberDTO(dummyArr5));
            memberModel.AddNewMember(new MemberDTO(dummyArr6));
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
