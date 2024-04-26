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
    class MyConsole
    {
        private const int HEADER_X = 45; // HEADER가 끝나야하는 X좌표
        private const int HEADER_Y = 4; // HEADER가 찍혀야하는 Y좌표

        private const int USERID_X = 40;
        private const int USERID_Y = 6;

        public const int MENU_STARTX = 40; // MENU가 끝나야하는 X좌표
        public const int MENU_STARTY = 8; // MENU가 찍혀야하는 Y좌표

        private const int BOOK_STARTX = 40;
        private const int BOOK_STARTY = 8;

        private const int INSTRUCTION_X = 40; // INSTRUCTION START X

        //===================== HEADER PRINT FUNCTION ==================//

        // HEADER 출력 함수
        public static void PrintHeader(string header)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(HEADER_X, HEADER_Y);
            Console.WriteLine(header);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintUserID(string userID)
        {
            Console.SetCursorPosition(USERID_X, USERID_Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("HELLO STUDENT " + userID + "!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintMessage(string message, int messageStartX, int messageStartY)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        // 각 메뉴에 맞게 다다음줄부터 INSTRUCTION이 출력됨
        public static void PrintInstructions(int starty)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(INSTRUCTION_X, starty + 2);
            Console.WriteLine("PRESS SPACE TO SELECT MODE");
            Console.SetCursorPosition(INSTRUCTION_X, starty + 3);
            Console.WriteLine("PRESS BACKSPACE TO GO BACK");
            Console.ForegroundColor = ConsoleColor.White;
        }

        //===================== MENU PRINT FUNCTION ==================//


        // menuStartX, menuStartY에서 시작하는 메뉴 중
        // idx에 해당하는 한개의 메뉴만 출력해줌
        private static void PrintMenu(string menu, int idx, int menuStartX, int menuStartY)
        {
            int menuLen = menu.Length;
            Console.SetCursorPosition(menuStartX, menuStartY + idx);
            Console.Write(menu);
        }

        private static void RenderMenu(string[] menuArr, int beforeSel, int curSel, int menuStartX, int menuStartY)
        {
            // 전꺼 흰색으로 출력
            Console.ForegroundColor = ConsoleColor.White;
            PrintMenu(menuArr[beforeSel], beforeSel, menuStartX, menuStartY);

            // 이번꺼 빨간색으로 출력
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMenu(menuArr[curSel], curSel, menuStartX, menuStartY);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //===================== BOOK PRINT FUNCTION ==================//

        public static void PrintBooks(List<BookDTO> selectedBooks)
        {
            Console.SetCursorPosition(BOOK_STARTX, BOOK_STARTY);

            for (int i = 0; i < selectedBooks.Count; i++)
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

        //============= USER INPUT FUNCTIONS ==============//

        private static int FindColonPosition(string menu) { return menu.IndexOf(':'); }

        // 각 메뉴에 대한 사용자 입력받을때 쓰이는 함수
        public static List<string> GetUserInputs(string[] menuArr, int menuStartX, int menuStartY)
        {
            Console.CursorVisible = true;

            // 우선 메뉴 다 출력하기
            for (int i = 0; i < menuArr.Length; i++)
            {
                string menu = menuArr[i];
                Console.SetCursorPosition(menuStartX, menuStartY + i);
                Console.Write(menu);
            }

            PrintInstructions(Console.CursorTop);

            List<string> retList = new List<string>();

            // 그리고 각 메뉴 옆에서 user input 받기
            // 콜론 : 위치 찾아서 그 옆에서 입력받게 하기
            for (int i = 0; i < menuArr.Length; i++)
            {
                Console.SetCursorPosition(menuStartX + FindColonPosition(menuArr[i]) + 2, menuStartY + i);
                retList.Add(Console.ReadLine());
            }

            Console.CursorVisible = false;

            return retList;
        }

        // 각 메뉴에 대한 사용자 선택을 받을때 쓰이는 함수
        public static int GetUserSelection(string[] menuArr, int menuStartX, int menuStartY)
        {
            for (int i = 0; i < menuArr.Length; i++)
            {
                string menu = menuArr[i];
                int menuLen = menu.Length;
                Console.SetCursorPosition(menuStartX, menuStartY + i);
                Console.Write(menu);
            }

            PrintInstructions(Console.CursorTop);

            int beforeSel = 0;
            int curSel = 0;

            int menuNum = menuArr.Length;

            bool pressedSpaceBar = false;

            MyConsole.RenderMenu(menuArr, beforeSel, curSel, menuStartX, menuStartY);

            // SPACEBAR 누를때까지 대기
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
                    MyConsole.RenderMenu(menuArr, beforeSel, curSel, menuStartX, menuStartY);
                }
            }

            return curSel;
        }

        // ENTER키 기다리는 함수
        public static void WaitForEnterKey()
        {
            MyConsole.PrintMessage("PRESS ENTER TO GO BACK...", Console.CursorLeft, Console.CursorTop);

            bool isEnterPressed = false;

            // ENTER 누를때까지 대기
            while (!isEnterPressed)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.Enter)
                    {
                        isEnterPressed = true;
                    }
                }
            }
        }
    }
}