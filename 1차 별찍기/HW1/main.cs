using System;
using System.Threading;

namespace App
{
    public static class Default
    {
        // 원래 25 10
        // 테스트용도로 좀 내리기
        public static int START_X = 35;
        public static int START_Y = 15;

        public const int LOGO_X = 5;
        public const int LOGO_Y = 5;

        public const int WIDTH = 80; // 약 2배
        public const int HEIGHT = 40;
        public const int MAX_INPUT = 23;

        public static bool flag = false;

        // 동적 커서
        public static int startx;
        public static int starty;

        // START_X START_Y로 커서 옮기기
        // 이거 initCursorPos(int x,int y)로 바꾸기
        public static void initCursorPos()
        {
            startx = START_X;
            starty = START_Y;
        }

        // 중앙에 WRITELINE
        public static void writeLine(string str)
        {
            Console.SetCursorPosition(startx, starty);
            Console.Write(str);

            // 개행처리
            startx = START_X;
            starty++;
        }

        // 중앙에 WRITE
        public static void write(string str)
        {
            Console.SetCursorPosition(startx, starty);
            Console.Write(str);
            startx++;
        }

        // 테두리 그리기
        public static void drawBoard()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                Console.Write("─");
            }

            for (int k = 1; k < HEIGHT; k++)
            {
                Console.SetCursorPosition(0, k);
                Console.Write("│");
            }

            Console.SetCursorPosition(0, HEIGHT);
            for (int i = 0; i < WIDTH; i++)
            {
                Console.Write("─");
            }

            for (int k = 1; k < HEIGHT; k++)
            {
                Console.SetCursorPosition(WIDTH, k);
                Console.Write("│");
            }

            Console.SetCursorPosition(0, 0);
            Console.Write("┌");

            Console.SetCursorPosition(WIDTH, 0);
            Console.Write("┐");

            Console.SetCursorPosition(0, HEIGHT);
            Console.Write("└");

            Console.SetCursorPosition(WIDTH, HEIGHT);
            Console.Write("┘");
        }
    }

    class Result
    {
        int sel;
        public Result(int sel)
        {
            this.sel = sel;
        }

        public int checkifValidNum()
        {
            // 줄 수 입력 : 바로 옆 자리 저장
            // Default.startx Default.starty 적용 되어 있음
            int left = Console.CursorLeft;

            while (true)
            {
                string dummy = "";
                string str = Console.ReadLine();

                // ctrl+c 예외처리
                if (str == null)
                {
                    continue;
                }

                // parse 가능하면 true 반환하고 row는 int값
                // 아니면 false고 row는 0
                int check = 0;
                bool checkifInt = int.TryParse(str, out check);

                // 숫자면
                if (checkifInt)
                {
                    if (check > Default.MAX_INPUT)
                    {
                        Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("결과가 콘솔창을 넘어가는 숫자입니다        ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (check > 0)
                    {
                        // 다이아 예외처리
                        if (sel == 3 && check % 2 == 0)
                        {
                            Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("다이아는 홀수만 가능합니다               ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            return check;
                        }
                    }
                    else
                    {
                        // (x,y)
                        Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("0보다 크고 " + Default.MAX_INPUT + "보다 작은 정수만 가능합니다");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("0보다 크고 " + Default.MAX_INPUT + "보다 작은 정수만 가능합니다");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // 여기까지 오는 경우는 틀린경우밖에

                for (int i = 0; i < str.Length * 2; i++) dummy += " ";

                // 잘못입력된거 가리기
                Console.SetCursorPosition(left, Default.starty);
                Console.Write(dummy);

                // 줄 수 입력 : 바로 옆으로 커서 배치
                Console.SetCursorPosition(left, Default.starty);
            }
        }

        public void print1(int rows)
        {
            // 커서 초기화
            Default.initCursorPos();
            Default.starty += 3;

            for (int i = 0; i < rows; i++)
            {
                int x = 2 * i + 1;
                for (int n = 0; n < rows - i - 1; n++) Default.write(" "); // startx++;
                for (int n = 0; n < x; n++) Default.write("*");
                Default.writeLine("");
            }
        }

        public void print2(int rows)
        {
            Default.initCursorPos();
            Default.starty += 3;

            for (int i = 0; i < rows; i++)
            {
                int x = 2 * (rows - i - 1) + 1;
                for (int n = 0; n < i; n++) Default.write(" ");
                for (int n = 0; n < x; n++) Default.write("*");
                Default.writeLine("");
            }
        }

        // 모래시계
        public void print3(int rows)
        {
            Default.initCursorPos();
            Default.starty += 3;

            int start = 0;
            if (rows % 2 == 0)
            {
                start = 0;
                rows /= 2;
            }
            else
            {
                start = 1;
                rows = rows / 2 + 1;
            }

            for (int i = 0; i < rows; i++)
            {
                int x = 2 * (rows - i - 1) + 1;
                for (int n = 0; n < i; n++) Default.write(" ");
                for (int n = 0; n < x; n++) Default.write("*");
                Default.writeLine("");
            }

            for (int i = start; i < rows; i++)
            {
                int x = 2 * i + 1;
                for (int n = 0; n < rows - i - 1; n++) Default.write(" "); // startx++;
                for (int n = 0; n < x; n++) Default.write("*");
                Default.writeLine("");
            }
        }

        // 다이아
        public void print4(int rows)
        {
            // 커서 초기화
            Default.initCursorPos();
            Default.starty += 3;

            rows = rows / 2 + 1;

            for (int i = 0; i < rows; i++)
            {
                int x = 2 * i + 1;
                for (int n = 0; n < rows - i - 1; n++) Default.write(" "); // startx++;
                for (int n = 0; n < x; n++) Default.write("*");
                Default.writeLine("");
            }

            for (int i = 1; i < rows; i++)
            {
                int x = 2 * (rows - i - 1) + 1;
                for (int n = 0; n < i; n++) Default.write(" ");
                for (int n = 0; n < x; n++) Default.write("*");
                Default.writeLine("");
            }
        }

        public void checkifBackSpace()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    // backspace 아니면 계속 돔
                    if (key == ConsoleKey.Backspace) return;
                }
            }
        }

        /*
         * 0 줄 수 입력
         * 1 예외
         * 
         * 3 결과
        */
        public void showResult()
        {
            Default.drawBoard();
            Default.initCursorPos();

            // 여기서 0 0 
            Console.ForegroundColor = ConsoleColor.Green;
            Default.write("줄 수 입력 : ");
            Console.ForegroundColor = ConsoleColor.White;

            // 옳은 값 입력될때까지
            int rows = checkifValidNum();

            // 오류 메시지 삭제
            Default.initCursorPos();
            Default.starty += 2;
            Default.write("                                             ");

            if (sel == 0) print1(rows);
            else if (sel == 1) print2(rows);
            else if (sel == 2) print3(rows);
            else if (sel == 3) print4(rows);

            Default.starty++;
            Default.write("PRESS BACKSPACE TO GO BACK");
            checkifBackSpace();
        }
    }

    class Menu
    {
        int sel = 0; // 0~4
        int before = -1;

        // render에서 사용
        void printSelectedLine(int sel)
        {
            Default.initCursorPos();
            Console.ForegroundColor = ConsoleColor.Red;
            switch (sel)
            {
                case 0:
                    Console.SetCursorPosition(Default.startx, Default.starty);
                    Console.Write("① 가운데 정렬 별찍기");
                    break;
                case 1:
                    Console.SetCursorPosition(Default.startx, Default.starty + 1);
                    Console.Write("② 1번의 반대로 찍기");
                    break;
                case 2:
                    Console.SetCursorPosition(Default.startx, Default.starty + 2);
                    Console.Write("③ 모래 시계");
                    break;
                case 3:
                    Console.SetCursorPosition(Default.startx, Default.starty + 3);
                    Console.Write("④ 다이아");
                    break;
                case 4:
                    Console.SetCursorPosition(Default.startx, Default.starty + 4);
                    Console.Write("⑤ 종료하기");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        // render에서 사용
        void eraseSelectedLine(int before)
        {
            Default.initCursorPos();
            switch (before)
            {
                case 0:
                    Console.SetCursorPosition(Default.startx, Default.starty);
                    Console.Write("① 가운데 정렬 별찍기");
                    break;
                case 1:
                    Console.SetCursorPosition(Default.startx, Default.starty + 1);
                    Console.Write("② 1번의 반대로 찍기");
                    break;
                case 2:
                    Console.SetCursorPosition(Default.startx, Default.starty + 2);
                    Console.Write("③ 모래 시계");
                    break;
                case 3:
                    Console.SetCursorPosition(Default.startx, Default.starty + 3);
                    Console.Write("④ 다이아");
                    break;
                case 4:
                    Console.SetCursorPosition(Default.startx, Default.starty + 4);
                    Console.Write("⑤ 종료하기");
                    break;
            }
        }

        // 바뀐 것들만 렌더링
        void render()
        {
            printSelectedLine(sel);
            eraseSelectedLine(before);
        }

        // 메뉴 모두 출력
        // 초기에 한번만 사용
        void initial_render()
        {
            Default.drawBoard();
            Default.initCursorPos();
            Default.writeLine("① 가운데 정렬 별찍기");
            Default.writeLine("② 1번의 반대로 찍기");
            Default.writeLine("③ 모래 시계");
            Default.writeLine("④ 다이아");
            Default.writeLine("⑤ 종료하기");
        }

        public void showMenu()
        {
            // 맨 처음 예외
            initial_render();
            render();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.DownArrow)
                    {
                        before = sel;
                        sel = (sel + 1) % 5;
                    }
                    if (key == ConsoleKey.UpArrow)
                    {
                        before = sel;
                        sel = (sel + 4) % 5;
                    }
                    // console switching
                    if (key == ConsoleKey.Spacebar)
                    {
                        // 정상종료
                        if (sel == 4)
                        {
                            Environment.Exit(0);
                            //Console.Clear();
                            //return; 
                        }

                        Console.Clear();

                        Result result = new Result(sel); // gc?
                        result.showResult();

                        Console.Clear();
                        initial_render();
                    }
                    render();
                }
            }
        }
    }

    class Front
    {
        int sel = 0;
        Menu menu;

        public Front()
        {
            menu = new Menu();
        }

        private void showLogo()
        {
            while (true)
            {
                printLogo();
                Thread.Sleep(700);
                eraseLogo();
                Thread.Sleep(700);
            }
        }

        // 로고 보여주기
        private void printLogo()
        {
            Default.startx = Default.LOGO_X;
            Default.starty = Default.LOGO_Y;

            Default.writeLine("______        _         _    _                 _____  _                ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("| ___ \\      (_)       | |  (_)               /  ___|| |               ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("| |_/ / _ __  _  _ __  | |_  _  _ __    __ _  \\ `--. | |_   __ _  _ __ ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("|  __/ | '__|| || '_ \\ | __|| || '_ \\  / _` |  `--. \\| __| / _` || '__|");
            Default.startx = Default.LOGO_X;
            Default.writeLine("| |    | |   | || | | || |_ | || | | || (_| | /\\__/ /| |_ | (_| || |   ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("\\_|    |_|   |_||_| |_| \\__||_||_| |_| \\__, | \\____/  \\__| \\__,_||_|   ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("                                        __/ |                          ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("                                       |___/                           ");
        }

        // 로고 지우기
        private void eraseLogo()
        {
            Default.startx = Default.LOGO_X;
            Default.starty = Default.LOGO_Y;

            Default.writeLine("                                                                         ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("                                                                         ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("                                                                         ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("                                                                         ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("                                                                         ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("                                                                         ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("                                                                         ");
            Default.startx = Default.LOGO_X;
            Default.writeLine("                                                                         ");
        }

        private void render()
        {
            // 커서 초기화
            Default.initCursorPos();
            if (sel == 0) Console.ForegroundColor = ConsoleColor.Red;
            Default.writeLine("① 시작");
            Console.ForegroundColor = ConsoleColor.White;
            if (sel == 1) Console.ForegroundColor = ConsoleColor.Red;
            Default.writeLine("② 종료");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void startApp()
        {
            // 초기세팅
            render();
            Thread logothread = new Thread(showLogo);
            logothread.Start();

            while (true)
            {
                // 키 입력되었으면 검사
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow) sel = (sel + 1) % 2;

                    // console switching
                    if (key == ConsoleKey.Spacebar)
                    {
                        // 1이면 정상종료
                        if (sel == 1) { Environment.Exit(0); }

                        Console.Clear();

                        // 쓰레드 일시중단 메뉴로 넘어가기
                        //logothread.Suspend();
                        logothread.Abort();
                        Default.START_X = 28;
                        Default.START_Y = 8;
                        menu.showMenu();
                        // 메뉴에서 넘어오면 다시 시작
                        //logothread.Resume();
                    }

                    render();
                }
            }
        }
    }

    class PrintStarApp
    {
        protected static void ctrlcHandler(Object sender, ConsoleCancelEventArgs args)
        {
            // ctrl+c 무효처리
            args.Cancel = true;

            int curleft = Console.CursorLeft;
            int curtop = Console.CursorTop;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(Default.START_X, Default.START_Y - 3);
            Console.Write("ctrl+c 금지입니다");
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(curleft, curtop);

            Thread.Sleep(2000);

            curleft = Console.CursorLeft;
            curtop = Console.CursorTop;

            Console.SetCursorPosition(Default.START_X, Default.START_Y - 3);
            Console.Write("                  ");

            Console.SetCursorPosition(curleft, curtop);
        }

        public static void Main()
        {
            Console.Title = "별찍기별찍기";

            // ctrl+c handling
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ctrlcHandler);
            Default.drawBoard();
            Console.CursorVisible = false;

            Front front = new Front();
            front.startApp();
        }
    }
}