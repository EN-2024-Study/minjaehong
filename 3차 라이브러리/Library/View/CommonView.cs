using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // 공통적인 UI 구성요소를 모아놓은 static class
    static class CommonView
    {
        private const int RUNTIME_MESSAGE_X = 40;
        private const int RUNTIME_MESSAGE_Y = 4;

        private const int BOOK_STARTX = 40;
        private const int BOOK_STARTY = 8;

        // 사용자의 행동에 따른 동적으로 나타나는 VIEW
        public static void RuntimeMessageForm(string message)
        {
            Console.Clear();
            Console.SetWindowSize(110, 20);

            Console.SetCursorPosition(RUNTIME_MESSAGE_X, RUNTIME_MESSAGE_Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;

            // 여기서 자동으로 backSpace 기다려줌
            MyConsole.WaitForBackSpace();

            Console.SetWindowSize(110, 50);
        }

        // 책 정보 보여주는 UI
        public static void PrintAllBooks(List<BookDTO> bookList)
        {
            Console.SetCursorPosition(BOOK_STARTX, BOOK_STARTY);

            if (bookList.Count == 0)
            {
                MyConsole.PrintHeader("[NO RESULT]");
            }
            else
            {
                for (int i = 0; i < bookList.Count; i++)
                {
                    Console.WriteLine("");
                    Console.CursorLeft = BOOK_STARTX;
                    Console.WriteLine("────────────────────────────────");
                    Console.WriteLine("");
                    Console.CursorLeft = BOOK_STARTX;
                    Console.WriteLine("ID         : " + bookList[i].GetId());
                    Console.CursorLeft = BOOK_STARTX;
                    Console.WriteLine("NAME       : " + bookList[i].GetName());
                    Console.CursorLeft = BOOK_STARTX;
                    Console.WriteLine("AUTHOR     : " + bookList[i].GetAuthor());
                    Console.CursorLeft = BOOK_STARTX;
                    Console.WriteLine("PUBLISHER  : " + bookList[i].GetPublisher());
                    Console.CursorLeft = BOOK_STARTX;
                    Console.WriteLine("PRICE      : " + bookList[i].GetPrice());
                    Console.CursorLeft = BOOK_STARTX;
                    Console.WriteLine("IN STOCK   : " + bookList[i].GetInStock());
                    Console.CursorLeft = BOOK_STARTX;
                    Console.WriteLine("DATE       : " + bookList[i].GetDate());
                    Console.CursorLeft = BOOK_STARTX;
                    Console.WriteLine("ISBN       : " + bookList[i].GetIsbn());
                }
                Console.WriteLine("");
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("────────────────────────────────");
            }
        }

        // USER 정보 보여주는 UI
        public static void PrintAllMembers(List<MemberDTO> memberList)
        {
            Console.SetCursorPosition(BOOK_STARTX, BOOK_STARTY);

            if (memberList.Count == 0)
            {
                MyConsole.PrintHeader("[NO RESULT]");
            }
            for (int i = 0; i < memberList.Count; i++)
            {
                Console.WriteLine("");
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("────────────────────────────────");
                Console.WriteLine("");
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("ID       : " + memberList[i].GetId());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("PW       : " + memberList[i].GetPw());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("NAME     : " + memberList[i].GetName());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("AGE      : " + memberList[i].GetAge());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("PHONENUM : " + memberList[i].GetPhoneNum());
            }
            Console.WriteLine("");
            Console.CursorLeft = BOOK_STARTX;
            Console.WriteLine("────────────────────────────────");
        }
    }
}
