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

        // 사용자 입력을 ConsoleKey 한 개씩 받음
        // ENTER 눌리면 한 개의 string return
        // 그냥 ENTER만 누르면 진짜 아무것도 없음
        // 그냥 ESC만 누르면 Escape 찍혀있음
        // 내부적으로 backspace 처리 필요
        // 커서 내가 처리해줘야함
        // Exception처리는 밖에서 -> 일단 값을 받은 후에 하자
        public static string GetUserInput(int inputStartX, int inputStartY)
        {
            Console.SetCursorPosition(inputStartX, inputStartY);
            Console.CursorVisible = true;

            StringBuilder sb = new StringBuilder(10);

            bool isEnterPressed = false;
            bool isEscPressed = false;

            while (!isEnterPressed && !isEscPressed)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            isEnterPressed = true;
                            break;
                        case ConsoleKey.Backspace:
                            if (sb.Length > 0)
                            {
                                // 일단 stringbuilder에서 지워주고
                                sb.Remove(sb.Length - 1, 1);
                                // 화면에서도 하나 지워주기
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                Console.Write(" ");
                                // 커서처리
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            }
                            break;
                        case ConsoleKey.Spacebar:
                            break;
                        case ConsoleKey.Escape:
                            // 다 지워주고 Escape만 보내기
                            // 받는 놈이 Escape 확인하고 해당 Form 나가주면 된다
                            isEscPressed = true;
                            sb.Clear();
                            sb.Append(key);
                            break;
                        default:
                            Console.Write(key);
                            sb.Append(key);
                            break;
                    }
                }
            }
            
            Console.CursorVisible = false;
            return sb.ToString();
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
