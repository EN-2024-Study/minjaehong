using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // 이거 굳이 싱글톤으로?
    // 그냥 static 써서 올려?
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

        private const int INPUT_STARTX = 60;
        private const int INPUT_STARTY = 8;
        
        /*
        private static MyConsole instance;

        private MyConsole() { }

        public static MyConsole GetInstance()
        {
            if (instance == null)
            {
                instance = new MyConsole();
            }
            return instance;
        }
        */

        //===================== SINGELTON ========================//

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

        //===================== INPUT FUNCTIONS =================//

        // 정보입력 창에 쓰임
        // 원하는 위치부터 ReadLine 해주는 함수
        // 커서는 각 메뉴의 ":" 바로 옆부터 시작함
        // enter 치면 바로 거기부터 시작
        // 입력 값들을 List로 반환해줌
        public static List<string> GetUserInputs(int rows)
        {
            Console.CursorVisible = true;
            List<string> retList = new List<string>();

            for(int i = 0; i < rows; i++)
            {
                Console.SetCursorPosition(INPUT_STARTX, INPUT_STARTY + i);
                retList.Add(Console.ReadLine()); // ENTER찍으면 하나씩 list에 저장
            }
            Console.CursorVisible = false;
            return retList;
        }

        // 메뉴 선택에 쓰임
        // spacebar 누르면 user가 select한 menu의 idx번호 return
        // 이거 모듈로 연산때문에 0부터 시작하는거임
        // 그래서 enum들 모두 0부터 시작해야함
        // 여기서는 int로 return하는데 controller에서는 enum으로 받기 때문에 enum 형변환해야함
        public static int GetUserSelection(string[] menuArr)
        {
            int beforeSel = 0;
            int curSel = 0;

            int menuNum = menuArr.Length;

            bool pressedSpaceBar = false;

            RenderMenu(menuArr, beforeSel, curSel);

            // SpaceBar 누를때까지 대기
            while (!pressedSpaceBar)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.DownArrow)
                    {
                        beforeSel = curSel;
                        curSel = (curSel + 1) % menuNum;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        beforeSel = curSel;
                        curSel = (curSel + menuNum - 1) % menuNum;
                    }
                    // 화면이동일때
                    else if (key == ConsoleKey.Spacebar)
                    {
                        pressedSpaceBar = true;
                    }
                    else if (key == ConsoleKey.Backspace)
                    {
                        return -1;
                    }
                    RenderMenu(menuArr, beforeSel, curSel);
                }
            }

            return curSel;
        }
    }
}
