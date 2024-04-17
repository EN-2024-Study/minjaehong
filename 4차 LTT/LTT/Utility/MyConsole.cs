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
        private const int USERID_Y = 5;

        //===================== PRINT FUNCTIONS =====================//

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

        //===================== INPUT FUNCTIONS =====================//

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

        // 지우개 함수
        // 지우고 CursorPosition 제자리로 돌려놓음
        public static void Eraser(int eraseStartX, int eraseStartY)
        {
            Console.SetCursorPosition(eraseStartX, eraseStartY);
            string eraser = "                                         ";
            Console.WriteLine(eraser);
            Console.SetCursorPosition(eraseStartX, eraseStartY);
        }
    }
}
