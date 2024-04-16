using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class CommonInput
    {
        private const int INPUT_STARTX = 60;
        private const int INPUT_STARTY = 8;

        private const int WARNING_STARTX = 40;
        private const int WARNING_STARTY = 6;

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
            for (int i = 0; i < menuArr.Length; i++)
            {
                Console.SetCursorPosition(menuStartX + menuArr[i].Length * 2, menuStartY + i);
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