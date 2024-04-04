using System;
// dll import 하기 위해 추가
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;

namespace App
{
    // 콘솔창 정보 및 중앙 출력을 위한 static 클래스
    public static class Default
    {
        // 현재 보여줄 화면 idx
        public static int pageidx = 0;
        // 사용자가 선택한 번호
        public static int sel = 0;

        // 중앙출력 시작 좌표
        public static int START_X = 35;
        public static int START_Y = 15;

        // 로고 출력 시작 좌표
        public const int LOGO_X = 5;
        public const int LOGO_Y = 5;

        // 콘솔 창 너비 높이
        public static int WIDTH = 80;
        public static int HEIGHT = 40;

        // 최대 줄 수
        public const int MAX_INPUT = 23;

        // 중앙 출력을 위한 동적 커서
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

    abstract class Page
    {
        // return 1 or -1
        abstract public int show();

        // show 안에서 호출됨
        public void render() { }
    }

    // 결과 창
    class Result : Page
    {
        // 결과창 함수
        public override int show()
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

            int sel = Default.sel;

            // 결과 출력
            if (sel == 0) print1(rows);
            else if (sel == 1) print2(rows);
            else if (sel == 2) print3(rows);
            else if (sel == 3) print4(rows);

            Default.starty++;
            Default.write("PRESS BACKSPACE TO GO BACK");
            // backspace 입력 기다리기
            checkifBackSpace();

            // 무조건 메뉴창으로
            Console.Clear();
            return -1;
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
                        if (Default.sel == 3 && check % 2 == 0)
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
    }

    class Menu : Page
    {
        int sel = 0;
        int before = -1;

        // 입력에 따라 색깔바꿔주기
        public void render()
        {
            printSelectedLine(sel);
            eraseSelectedLine(before);
        }

        override public int show()
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
                        Console.Clear();

                        // 이전 페이지인 시작화면으로 가기
                        if (sel == 4)
                        {
                            return -1;
                        }
                        // 다음 페이지인 결과창으로 가기
                        return 1;
                    }
                    render();

                    // Result에서 표현할 sel 최신화
                    Default.sel = sel;
                }
            }
        }

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
                    Console.Write("⑤ 시작화면");
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
                    Console.Write("⑤ 시작화면");
                    break;
            }
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
            Default.writeLine("⑤ 시작화면");
            Default.writeLine("");
            Default.writeLine("PRESS SPACE TO SELECT!");
        }
    }

    class Front : Page
    {
        int sel = 0;

        // 입력에 따라 색깔 바꿔주기
        public void render()
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

        override public int show()
        {
            // 초기화면 출력 위치로 바꿔주기
            Default.START_X = 35;
            Default.START_Y = 15;

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

                        // 쓰레드 중지
                        logothread.Abort();

                        // 중앙출력을 위해 Default 값 바꾸기
                        Default.START_X = 28;
                        Default.START_Y = 8;
                        Console.Clear();

                        // 다음 페이지인 menu로 넘어가기
                        return 1;
                    }

                    render();
                }
            }
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
            Console.SetCursorPosition(Default.LOGO_X, ++starty);
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
    }
    
    class Program
    {
        private List<Page> pages;

        public Program()
        {
            // 페이지 생성
            pages = new List<Page>(3);
            pages.Add(new Front());
            pages.Add(new Menu());
            pages.Add(new Result());
        }

        // 프로그램 돌리기
        public void run()
        {
            // show가 +1 return하면 다음 페이지로
            // show가 -1 return하면 이전 페이지로
            while (true)
            {
                Default.pageidx += pages[Default.pageidx].show();
            }
        }
    }

    class PrintStarApp
    {
        // ctrl handler
        protected static void ctrlHandler(Object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
        }

        // 콘솔 버퍼 키우기 및 콘솔 창 환경설정
        public static void initializeConsole()
        {
            Console.SetWindowSize(120, 120);
            Console.Title = "별찍기별찍기";
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ctrlHandler);
            Console.CursorVisible = false;
        }

        public static void Main()
        {
            initializeConsole();
            Program p = new Program();
            p.run();
        }
    }
}