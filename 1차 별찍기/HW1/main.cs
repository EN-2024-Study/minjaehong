using System;
using System.Threading;

namespace App
{
    // 콘솔창 정보 및 중앙 출력을 위한 static 클래스
    public static class Default
    {
        public static int START_X = 35;
        public static int START_Y = 15;

        public const int LOGO_X = 5;
        public const int LOGO_Y = 5;

        public static int WIDTH = 80;
        public static int HEIGHT = 40;

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
            Console.SetCursorPosition(0, 0);
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

    // 결과 창
    class Result
    {
        int sel;
        public Result(int sel)
        {
            this.sel = sel;
        }

        // 유효한 값인지 확인해주는 함수
        private int checkifValidNum()
        {
            // 줄 수 입력 : 바로 옆 자리 저장
            // Default.startx Default.starty 적용 되어 있음
            int left = Console.CursorLeft;

            while (true)
            {
                string dummy = "";
                string str = Console.ReadLine();

                // ctrl+c 예외처리
                if (str == null) { continue; }

                // parse 가능하면 true 반환하고 row는 int값
                // 아니면 false고 row는 0
                int check = 0;
                bool checkifInt = int.TryParse(str, out check);

                // 0으로 시작하는 숫자문자열이면 문자열처리
                if (str.StartsWith("0"))
                {
                    checkifInt = false;
                }

                // 숫자면
                if (checkifInt)
                {
                    // 콘솔창을 넘어가는 출력이면
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
                            // 성공으로 row 값 반환
                            return check;
                        }
                    }
                    else
                    {
                        // 음수일때
                        Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("0보다 크고 " + Default.MAX_INPUT + "보다 작은 정수만 가능합니다");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                // 문자열일때
                else
                {
                    Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("0보다 크고 " + Default.MAX_INPUT + "보다 작은 정수만 가능합니다");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // ============== 틀린 경우면 입력된 값 가리기 =====================

                for (int i = 0; i < str.Length * 2; i++) dummy += " ";

                // 잘못입력된거 가리기
                Console.SetCursorPosition(left, Default.starty);
                Console.Write(dummy);
                
                Default.drawBoard();

                // 다시 입력받을 수 있게 "줄 수 입력 :" 바로 옆으로 커서 배치
                Console.SetCursorPosition(left, Default.starty);
            }
        }

        // 별찍기 1
        private void print1(int rows)
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

        // 별찍기 2
        private void print2(int rows)
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

        // 별찍기 3
        private void print3(int rows)
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

        // 별찍기 4
        private void print4(int rows)
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

        // backspace 눌렸는지 확인
        private void checkifBackSpace()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                  
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace) return;
                }
            }
        }

        // 결과창 함수
        public void showResult()
        {
            // 커서 초기화
            Default.drawBoard();
            Default.initCursorPos();

            Console.ForegroundColor = ConsoleColor.Green;
            Default.write("줄 수 입력 : ");
            Console.ForegroundColor = ConsoleColor.White;

            // 옳은 값 입력될때까지
            int rows = checkifValidNum();

            // ================ 유효한 값이 입력되었으면 아래 실행 =============== // 
            
            // 오류 메시지 삭제하기
            Default.initCursorPos();
            Default.starty += 2;
            Default.write("                                             ");

            // 결과 출력
            if (sel == 0) print1(rows);
            else if (sel == 1) print2(rows);
            else if (sel == 2) print3(rows);
            else if (sel == 3) print4(rows);

            // BackSpace 입력되었으면 다시 메뉴창으로 
            Default.starty++;
            Default.write("PRESS BACKSPACE TO GO BACK");
            checkifBackSpace();
        }
    }

    class Menu
    {
        int sel = 0;
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
            Default.writeLine("");
            Default.writeLine("PRESS SPACE TO SELECT!");
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
                Thread.Sleep(900);
                eraseLogo();
                Thread.Sleep(900);
            }
        }

        // 쓰레드 - 로고 보여주기
        private void printLogo()
        {
            int starty = Default.LOGO_Y;

            Console.SetCursorPosition(Default.LOGO_X, starty);
            Console.WriteLine("______        _         _    _                 _____  _                ");
            Console.SetCursorPosition(Default.LOGO_X,++starty);
            Console.WriteLine("| ___ \\      (_)       | |  (_)               /  ___|| |               ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("| |_/ / _ __  _  _ __  | |_  _  _ __    __ _  \\ `--. | |_   __ _  _ __ ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("|  __/ |  __|| ||  _ \\ | __|| ||  _ \\  / _  |  `--. \\| __| / _  ||  __|");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("| |    | |   | || | | || |_ | || | | || (_| | /\\__/ /| |_ | (_| || |   ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("\\_|    |_|   |_||_| |_| \\__||_||_| |_| \\__, | \\____/  \\__| \\__,_||_|   ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("                                        __/ |                          ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("                                       |___/                           ");
        }

        // 쓰레드 - 로고 지우기
        private void eraseLogo()
        {
            int starty = Default.LOGO_Y;
            Console.SetCursorPosition(Default.LOGO_X, starty);
            Console.WriteLine("                                                                         ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("                                                                         ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("                                                                         ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("                                                                         ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("                                                                         ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("                                                                         ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("                                                                         ");
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
            Console.WriteLine("                                                                         ");
        }

        // 입력에 따라 색깔 바꿔주기
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

            Default.drawBoard();

            // 로고 쓰레드
            // 테두리 그린 후에 실행
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
                        logothread.Abort();
                        Default.START_X = 28;
                        Default.START_Y = 8;
                        Console.Clear();
                        //Console.Out.Flush();
                        menu.showMenu();
                    }

                    render();
                }
            }
        }
    }

    class PrintStarApp
    {
        protected static void ctrlHandler(Object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
        }

        public static void Main()
        {
            Console.Title = "별찍기별찍기";

            int width = Console.LargestWindowWidth;
            int height = Console.LargestWindowHeight;

            // 창 크기 설정
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ctrlHandler);
            Console.CursorVisible = false;

            Front front = new Front();
            front.startApp();

            Console.Out.Flush();
        }
    }
}