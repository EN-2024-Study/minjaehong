using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Menu : Page
    {
        public Menu(Common common, MyConsole myconsole) : base(common, myconsole)
        {
            
        }

        public void ShowMenu()
        {
            Console.SetCursorPosition(common.MENU_X, common.MENU_Y-2);
            Console.Write("[HELLO USER "+common.username+"!]");
            
            myconsole.InitCursorPos();
            myconsole.writeLine("① VS COM");
            myconsole.writeLine("② VS USER");
            myconsole.writeLine("③ HISTORY");
            myconsole.writeLine("④ 시작으로");
        }

        private void PrintCertainLine(int lineNum)
        {
            switch (lineNum)
            {
                case 0:
                    Console.SetCursorPosition(common.MENU_X, common.MENU_Y);
                    Console.WriteLine("① VS COM");
                    break;
                case 1:
                    Console.SetCursorPosition(common.MENU_X, common.MENU_Y+1);
                    Console.WriteLine("② VS USER");
                    break;
                case 2:
                    Console.SetCursorPosition(common.MENU_X, common.MENU_Y+2);
                    Console.WriteLine("③ HISTORY");
                    break;
                case 3:
                    Console.SetCursorPosition(common.MENU_X, common.MENU_Y+3);
                    Console.WriteLine("④ 시작으로");
                    break;
            }
        }

        public void ColorLineRed(int lineNum)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintCertainLine(lineNum);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ColorLineWhite(int lineNum)
        {
            Console.ForegroundColor = ConsoleColor.White;
            PrintCertainLine(lineNum);
        }

        public void Render(int cur, int before)
        {
            ColorLineRed(cur);
            ColorLineWhite(before);
        }

        override public int Show()
        {
            int mode = 0;
            int before = -1;

            ShowMenu();
            Render(mode, before);

            // 선택할때까지 이 함수에서 대기
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.DownArrow)
                    {
                        before = mode;
                        mode = (mode + 1) % 4;
                    }
                    if (key == ConsoleKey.UpArrow)
                    {
                        before = mode;
                        mode = (mode + 3) % 4;
                    }
                    // 화면이동일때
                    if (key == ConsoleKey.Spacebar)
                    {
                        // 화면 지우고 다음 화면으로 넘어갈 준비
                        Console.Clear();
                        
                        // 시작하기이면 -1
                        if (Common.GetInstance().mode == 3) return -1;
                        // HISTORY이면 +2
                        else if (Common.GetInstance().mode == 2) return 2;
                        // VS COM OR VS USER 이면 +1
                        return 1;
                    }

                    Common.GetInstance().mode = mode;
                    Render(mode, before);
                }
            }
        }
    }
}
