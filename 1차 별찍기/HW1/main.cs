using System;
using System.IO;

namespace App
{
    class Result
    {
        int sel;
        public Result(int sel)
        {
            this.sel = sel;
        }

        ~Result()
        {

        }

        public int checkifValidNum()
        {

            // 줄 수 입력 : 바로 옆 자리 저장
            int left = Console.CursorLeft;

            int flag = 0;

            while (true)
            {
                flag = Console.CursorLeft;

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
                        Console.SetCursorPosition(0, 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("0보다 큰 정수 입력바람");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("유효한 값 입력바람    ");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // 여기까지 오는 경우는 틀린경우밖에

                for (int i = 0; i < str.Length*2; i++) dummy += " ";

                // 잘못입력된거 가리기
                Console.SetCursorPosition(left, 0);
                Console.Write(dummy);

                // 줄 수 입력 : 바로 옆으로 커서 배치
                Console.SetCursorPosition(left, 0);
            }
        }

        public void print1(int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                int x = 2 * i + 1;
                for (int n = 0; n < rows - i - 1; n++) Console.Write(" ");
                for (int n = 0; n < x; n++) Console.Write("*");
                Console.WriteLine();
            }
        }

        public void print2(int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                int x = 2 * (rows - i - 1) + 1;
                for (int n = 0; n < i; n++) Console.Write(" ");
                for (int n = 0; n < x; n++) Console.Write("*");
                Console.WriteLine();
            }
        }

        public void print3(int rows)
        {
            print2(rows);
            print1(rows);
        }

        public void print4(int rows)
        {
            print1(rows);
            for (int i = 1; i < rows; i++)
            {
                int x = 2 * (rows - i - 1) + 1;
                for (int n = 0; n < i; n++) Console.Write(" ");
                for (int n = 0; n < x; n++) Console.Write("*");
                Console.WriteLine();
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
            // 여기서 0 0 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("줄 수 입력 : ");
            Console.ForegroundColor = ConsoleColor.White;

            // 옳은 값 입력될때까지
            int rows = checkifValidNum();

            // 메세지 삭제 효과
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("                      ");

            if (sel == 0) print1(rows);
            else if (sel == 1) print2(rows);
            else if (sel == 2) print3(rows);
            else if (sel == 3) print4(rows);

            Console.WriteLine("\nPRESS BACKSPACE TO GO BACK");
            checkifBackSpace();
        }
    }

    class Menu
    {
        int sel = 0; // 0~4
        int before = -1;

        public void printSelectedLine(int sel)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            switch (sel)
            {
                case 0:
                    Console.SetCursorPosition(0, 0);
                    Console.Write("1. 가운데 정렬 별찍기");
                    break;
                case 1:
                    Console.SetCursorPosition(0, 1);
                    Console.Write("2. 1번의 반대로 찍기");
                    break;
                case 2:
                    Console.SetCursorPosition(0, 2);
                    Console.Write("3. 모래 시계");
                    break;
                case 3:
                    Console.SetCursorPosition(0, 3);
                    Console.Write("4. 다이아");
                    break;
                case 4:
                    Console.SetCursorPosition(0, 4);
                    Console.Write("5. 종료하기");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void eraseSelectedLine(int before)
        {
            switch (before)
            {
                case 0:
                    Console.SetCursorPosition(0, 0);
                    Console.Write("1. 가운데 정렬 별찍기");
                    break;
                case 1:
                    Console.SetCursorPosition(0, 1);
                    Console.Write("2. 1번의 반대로 찍기");
                    break;
                case 2:
                    Console.SetCursorPosition(0, 2);
                    Console.Write("3. 모래 시계");
                    break;
                case 3:
                    Console.SetCursorPosition(0, 3);
                    Console.Write("4. 다이아");
                    break;
                case 4:
                    Console.SetCursorPosition(0, 4);
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
            Console.WriteLine("1. 가운데 정렬 별찍기");
            Console.WriteLine("2. 1번의 반대로 찍기");
            Console.WriteLine("3. 모래 시계");
            Console.WriteLine("4. 다이아");
            Console.WriteLine("5. 종료하기");
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
            Console.SetCursorPosition(0, 0);
            if (sel == 0) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1. 시작");
            Console.ForegroundColor = ConsoleColor.White;
            if (sel == 1) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("2. 종료");
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
            //Console.SetWindowSize(80, 40);

            Console.CursorVisible = false;
            Intro intro = new Intro();
            intro.startApp();
        }
    }
}