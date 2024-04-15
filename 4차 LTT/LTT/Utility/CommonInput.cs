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

        public static string GetUserInput(int inputStartX, int inputStartY)
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(inputStartX, inputStartY);
            return Console.ReadLine();
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

        // 메뉴 선택에 쓰임
        // spacebar 누르면 user가 select한 menu의 idx번호 return
        // 이거 모듈로 연산때문에 0부터 시작하는거임
        // 그래서 enum들 모두 0부터 시작해야함
        // 여기서는 int로 return하는데 controller에서는 enum으로 받기 때문에 enum 형변환해야함
        public static int GetUserSelection(string[] menuArr, int menuStartX, int menuStartY)
        {
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
    }
}