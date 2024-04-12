using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // static class
    // static class이면 member들도 모두 자동으로 static으로 박힘
    // const member는 static으로 쳐져서 static 명시 안해도 됨
    static class MyConsole
    {
        private const int HEADER_X = 45; // HEADER가 끝나야하는 X좌표
        private const int HEADER_Y = 4; // HEADER가 찍혀야하는 Y좌표

        private const int USERNAME_X = 40;
        private const int USERNAME_Y = 6;

        private const int MENU_STARTX = 40; // MENU가 끝나야하는 X좌표
        private const int MENU_STARTY = 8; // MENU가 찍혀야하는 Y좌표

        private const int BOOK_STARTX = 40;
        private const int BOOK_STARTY = 8;
        
        //===================== HEADER PRINT FUNCTION ==================//
       
        // HEADER 출력 함수
        public static void PrintHeader(string header)
        {
            Console.SetCursorPosition(HEADER_X, HEADER_Y);
            Console.Write(header);
        }

        public static void PrintUserName(string curUserID)
        {
            Console.SetCursorPosition(USERNAME_X, USERNAME_Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("HELLO " + curUserID +"!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        //===================== MENU PRINT FUNCTION ==================//

        // 전체메뉴 출력해줌
        public static void PrintAllMenu(string[] menuArr)
        {
            for (int i = 0; i < menuArr.Length; i++) {
                string menu = menuArr[i];
                int menuLen = menu.Length;
                Console.SetCursorPosition(MENU_STARTX, MENU_STARTY + i);
                Console.Write(menu);
            }
        }

        // 한개의 메뉴만 출력해줌
        public static void PrintMenu(string menu, int idx)
        {
            int menuLen = menu.Length;
            Console.SetCursorPosition(MENU_STARTX, MENU_STARTY + idx);
            Console.Write(menu);
        }

        public static void RenderMenu(string[] menuArr, int beforeSel, int curSel)
        {
            // 전꺼 흰색으로 출력
            Console.ForegroundColor = ConsoleColor.White;
            PrintMenu(menuArr[beforeSel], beforeSel);

            // 이번꺼 빨간색으로 출력
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMenu(menuArr[curSel], curSel);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //===================== BOOK PRINT FUNCTION ==================//

        public static void PrintBooks(List<BookDTO> selectedBooks)
        {
            Console.SetCursorPosition(BOOK_STARTX, BOOK_STARTY);

            for(int i = 0; i < selectedBooks.Count; i++)
            {
                Console.WriteLine("");
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("==============================");
                Console.WriteLine("");
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("ID         : " + selectedBooks[i].GetId());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("NAME       : " + selectedBooks[i].GetName());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("AUTHOR     : " + selectedBooks[i].GetAuthor());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("PUBLISHER  : " + selectedBooks[i].GetPublisher());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("PRICE      : " + selectedBooks[i].GetPrice());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("IN STOCK   : " + selectedBooks[i].GetInStock());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("DATE       : " + selectedBooks[i].GetDate());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("ISBN       : " + selectedBooks[i].GetIsbn());
            }
            Console.WriteLine("");
            Console.CursorLeft = BOOK_STARTX;
            Console.WriteLine("==============================");
        }

        //===================== MEMBER PRINT FUNCTION ==================//

        public static void PrintAllMembers(List<MemberDTO> allMembers)
        {
            Console.SetCursorPosition(BOOK_STARTX, BOOK_STARTY);

            for (int i = 0; i < allMembers.Count; i++)
            {
                Console.WriteLine("");
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("==============================");
                Console.WriteLine("");
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("ID       : " + allMembers[i].GetId());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("PW       : " + allMembers[i].GetPw());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("NAME     : " + allMembers[i].GetName());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("AGE      : " + allMembers[i].GetAge());
                Console.CursorLeft = BOOK_STARTX;
                Console.WriteLine("PHONENUM : " + allMembers[i].GetPhoneNum());
            }
            Console.WriteLine("");
            Console.CursorLeft = BOOK_STARTX;
            Console.WriteLine("==============================");
        }
    }
}
