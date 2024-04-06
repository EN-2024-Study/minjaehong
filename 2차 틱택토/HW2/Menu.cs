using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Menu : Page
    {
        public Menu(GameInfo gameInfo, MyConsole myconsole) : base(gameInfo, myconsole)
        {
            
        }

        // 메뉴 출력
        public void ShowMenu()
        {
            Console.SetCursorPosition(GameInfo.MENU_X, GameInfo.MENU_Y-2);
            Console.Write("[HELLO USER "+gameInfo.username+"!]");
            
            myconsole.InitCursorPos();
            myconsole.WriteLine("① VS COM");
            myconsole.WriteLine("② VS USER");
            myconsole.WriteLine("③ HISTORY");
            myconsole.WriteLine("④ 시작으로");
        }

        // 특정 메뉴만 출력하기
        // lineNum 입력받으면 해당 줄에 해당하는 메뉴만 출력
        private void PrintCertainLine(int lineNum)
        {
            switch (lineNum)
            {
                case 0:
                    Console.SetCursorPosition(GameInfo.MENU_X, GameInfo.MENU_Y);
                    Console.WriteLine("① VS COM");
                    break;
                case 1:
                    Console.SetCursorPosition(GameInfo.MENU_X, GameInfo.MENU_Y+1);
                    Console.WriteLine("② VS USER");
                    break;
                case 2:
                    Console.SetCursorPosition(GameInfo.MENU_X, GameInfo.MENU_Y+2);
                    Console.WriteLine("③ HISTORY");
                    break;
                case 3:
                    Console.SetCursorPosition(GameInfo.MENU_X, GameInfo.MENU_Y+3);
                    Console.WriteLine("④ 시작으로");
                    break;
            }
        }

        // 해당 메뉴 빨간색으로 출력하기
        public void ColorLineRed(int lineNum)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintCertainLine(lineNum);
            Console.ForegroundColor = ConsoleColor.White;
        }

        // 해당 메뉴 흰색으로 출력하기
        public void ColorLineWhite(int lineNum)
        {
            Console.ForegroundColor = ConsoleColor.White;
            PrintCertainLine(lineNum);
        }

        // rendering menu
        public void Render(int cur, int beforeMenu)
        {
            ColorLineRed(cur);
            ColorLineWhite(beforeMenu);
        }

        // MENU 화면 보여주고 사용자 입력받기
        override public int Show()
        {
            int selectedMenu = 0;
            int beforeMenu = -1;

            ShowMenu();
            Render(selectedMenu, beforeMenu);

            // 선택할때까지 이 함수에서 대기
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.DownArrow)
                    {
                        beforeMenu = selectedMenu;
                        selectedMenu = (selectedMenu + 1) % 4;
                    }
                    if (key == ConsoleKey.UpArrow)
                    {
                        beforeMenu = selectedMenu;
                        selectedMenu = (selectedMenu + 3) % 4;
                    }
                    // 화면이동일때
                    if (key == ConsoleKey.Spacebar)
                    {
                        // 화면 지우고 다음 화면으로 넘어갈 준비
                        Console.Clear();

                        switch (selectedMenu)
                        {
                            case 0:
                                gameInfo.mode = GameInfo.Mode.CVP;
                                return +1;
                            case 1:
                                gameInfo.mode = GameInfo.Mode.PVP;
                                return +1;
                            case 2:
                                return +2;
                            case 3:
                                return -1;

                        }
                    }

                    Render(selectedMenu, beforeMenu);
                }
            }
        }
    }
}
