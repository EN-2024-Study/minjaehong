using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class MyConsole
    {
        private const int HEADER_X = 45; // HEADER가 끝나야하는 X좌표
        private const int HEADER_Y = 4; // HEADER가 찍혀야하는 Y좌표

        private const int USERID_X = 40;
        private const int USERID_Y = 7;

        private const int INSTRUCTION_STARTX = 40;
        private const int INSTRUCTION_STARTY = 8;

        private const int MENU_STARTX = 40; // MENU가 끝나야하는 X좌표
        private const int MENU_STARTY = 6; // MENU가 찍혀야하는 Y좌표

        private const int BOOK_STARTX = 40;
        private const int BOOK_STARTY = 6;

        //===================== HEADER PRINT FUNCTION ==================//

        // HEADER 출력 함수
        // HEADER 위치는 고정되어 있음
        public static void PrintHeader(string header)
        {
            Console.SetCursorPosition(HEADER_X, HEADER_Y);
            Console.Write(header);
        }

        public static void PrintUserID(string userID)
        {
            Console.SetCursorPosition(USERID_X, USERID_Y);
            Console.WriteLine("HELLO "+userID+"!");
        }

        //===================== MENU PRINT FUNCTION ==================//

        // 인자 : 출력할 메뉴 모음 + 출력시작할 X좌표 + 출력시작할 Y좌표
        public static void PrintAllMenu(string[] menuArr, int menuStartX, int menuStartY)
        {
            for (int i = 0; i < menuArr.Length; i++)
            {
                string menu = menuArr[i];
                int menuLen = menu.Length;
                Console.SetCursorPosition(menuStartX, menuStartY + i);
                Console.Write(menu);
            }
        }

        // menuStartX, menuStartY에서 시작하는 메뉴 중
        // idx에 해당하는 한개의 메뉴만 출력해줌
        public static void PrintMenu(string menu, int idx, int menuStartX, int menuStartY)
        {
            int menuLen = menu.Length;
            Console.SetCursorPosition(menuStartX, menuStartY + idx);
            Console.Write(menu);
        }

        public static void RenderMenu(string[] menuArr, int beforeSel, int curSel, int menuStartX, int menuStartY)
        {
            // 전꺼 흰색으로 출력
            Console.ForegroundColor = ConsoleColor.White;
            PrintMenu(menuArr[beforeSel], beforeSel, menuStartX, menuStartY);

            // 이번꺼 빨간색으로 출력
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMenu(menuArr[curSel], curSel, menuStartX, menuStartY);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
