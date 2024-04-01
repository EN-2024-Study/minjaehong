using System;
using System.IO;
using System.Runtime.ConstrainedExecution;

namespace App
{
    public static class Default
    {
        public const int START_X = 25;
        public const int START_Y = 10;
        public const int WIDTH = 80; // 약 2배
        public const int HEIGHT = 40;
        
        // 동적 커서
        public static int startx;
        public static int starty;
        
        public static void initCursorPos()
        {
            startx = START_X;
            starty = START_Y;
        }

        public static void writeLine(string str)
        {
            Console.SetCursorPosition(startx, starty);
            Console.Write(str);

            // 개행처리
            startx = START_X;
            starty++;  
        }

        public static void write(string str)
        {
            Console.SetCursorPosition(startx, starty);
            Console.Write(str);
            startx++;
        }

        // 맨 처음 딱 한번 호출 후 더 이상 호출 안해도 되게
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

                // parse 가능하면 true 반환하고 row는 int값
                // 아니면 false고 row는 0
                int check = 0;
                bool checkifInt = int.TryParse(str, out check);

                // 숫자면
                if (checkifInt)
                {
                    if (check > 0)
                    {
                        return check;
                    }
                    else
                    {
                        // (x,y)
                        Console.SetCursorPosition(Default.START_X, Default.START_Y+2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("0보다 큰 정수 입력바람");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("유효한 값 입력바람    ");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // 여기까지 오는 경우는 틀린경우밖에

                for (int i = 0; i < str.Length*2; i++) dummy += " ";

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

        public void print3(int rows)
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
            for (int i = 0; i < rows; i++)
            {
                int x = 2 * i + 1;
                for (int n = 0; n < rows - i - 1; n++) Default.write(" "); // startx++;
                for (int n = 0; n < x; n++) Default.write("*");
                Default.writeLine("");
            }
        }

        public void print4(int rows)
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

            // 메세지 삭제 효과
            Default.initCursorPos();
            Default.starty += 2;
            Default.write("                      ");

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

        public void printSelectedLine(int sel)
        {
            Default.initCursorPos();
            Console.ForegroundColor = ConsoleColor.Red;
            switch (sel)
            {
                case 0:
                    Console.SetCursorPosition(Default.startx, Default.starty);
                    Console.Write("1. 가운데 정렬 별찍기");
                    break;
                case 1:
                    Console.SetCursorPosition(Default.startx, Default.starty+1);
                    Console.Write("2. 1번의 반대로 찍기");
                    break;
                case 2:
                    Console.SetCursorPosition(Default.startx, Default.starty+2);
                    Console.Write("3. 모래 시계");
                    break;
                case 3:
                    Console.SetCursorPosition(Default.startx, Default.starty+3);
                    Console.Write("4. 다이아");
                    break;
                case 4:
                    Console.SetCursorPosition(Default.startx, Default.starty+4);
                    Console.Write("5. 종료하기");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void eraseSelectedLine(int before)
        {
            Default.initCursorPos();
            switch (before)
            {
                case 0:
                    Console.SetCursorPosition(Default.startx, Default.starty);
                    Console.Write("1. 가운데 정렬 별찍기");
                    break;
                case 1:
                    Console.SetCursorPosition(Default.startx, Default.starty+1);
                    Console.Write("2. 1번의 반대로 찍기");
                    break;
                case 2:
                    Console.SetCursorPosition(Default.startx, Default.starty+2);
                    Console.Write("3. 모래 시계");
                    break;
                case 3:
                    Console.SetCursorPosition(Default.startx, Default.starty+3);
                    Console.Write("4. 다이아");
                    break;
                case 4:
                    Console.SetCursorPosition(Default.startx, Default.starty+4);
                    Console.Write("5. 종료하기");
                    break;
            }
        }

        public void render()
        {
            printSelectedLine(sel);
            eraseSelectedLine(before);
        }

        public void initial_render()
        {
            Default.drawBoard();
            Default.initCursorPos();
            Default.writeLine("1. 가운데 정렬 별찍기");
            Default.writeLine("2. 1번의 반대로 찍기");
            Default.writeLine("3. 모래 시계");
            Default.writeLine("4. 다이아");
            Default.writeLine("5. 종료하기");
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
                        if (sel == 4) { Environment.Exit(0); }

                        Console.Clear();

                        Result result = new Result(sel); // gc?
                        result.showResult();

                        Console.Clear();
                        initial_render();
                    }
                    render();
                }
                System.Threading.Thread.Sleep(100);
            }
        }
    }

    class Intro
    {
        int sel = 0;
        Menu menu;

        public Intro()
        {
            menu = new Menu();
        }

        public void render()
        {
            // 커서 초기화
            Default.initCursorPos();
            if (sel == 0) Console.ForegroundColor = ConsoleColor.Red;
            Default.writeLine("1. 시작");
            Console.ForegroundColor = ConsoleColor.White;
            if (sel == 1) Console.ForegroundColor = ConsoleColor.Red;
            Default.writeLine("2. 종료");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // 메뉴 보여줌
        // 0 시작하기
        // 1 종료하기
        public void startApp()
        {
            render();

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

                        // 넘어가기
                        menu.showMenu();
                    }

                    render();
                }
            }
        }
    }

    class PrintStarApp
    {
        public static void Main()
        {
            Console.Title = "별찍기별찍기";

            // first and last drawBoard
            Default.drawBoard();
            Console.CursorVisible = true;

            Intro intro = new Intro();
            intro.startApp();
        }
    }
}