using System;

namespace App
{
    // Result에서는 Console.Clear 없어도 됨
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
            string origin = "줄 수 입력 : ";

            while (true)
            {
                string dummy = origin; 
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
                    Console.WriteLine("유효한 값 입력바람");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // 여기까지 오는 경우는 틀린경우밖에

                for (int i = 0; i < str.Length; i++) dummy += " ";

                Console.SetCursorPosition(0, 0);
                Console.Write(dummy);
                Console.SetCursorPosition(0, 0);
                Console.Write(origin);
            }
        }

        public void print1(int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                int x = 2 * i + 1;
                for (int n = 0; n < rows-i-1; n++) Console.Write(" ");
                for (int n = 0; n < x; n++) Console.Write("*");
                Console.WriteLine();
            }
        }

        public void print2(int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                int x = 2 * (rows-i-1) + 1;
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
        public bool showResult()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("줄 수 입력 : ");
            Console.ForegroundColor = ConsoleColor.White;

            int rows = checkifValidNum();

            // (x,y)
            Console.SetCursorPosition(0, 3);

            if (sel == 0) print1(rows);
            else if (sel == 1) print2(rows);
            else if (sel == 2) print3(rows);
            else if (sel == 3) print4(rows);

            Console.WriteLine("\nPRESS BACKSPACE TO GO BACK");
            checkifBackSpace();

            return false;
        }
    }

    class Menu
    {
        int sel = 0; // 0~4

        public void printSelectedLine(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void render()
        {
            if (sel == 0) printSelectedLine("1. 가운데 정렬 별찍기");
            else { Console.WriteLine("1. 가운데 정렬 별찍기"); }

            if (sel == 1) printSelectedLine("2. 1번의 반대로 찍기");
            else { Console.WriteLine("2. 1번의 반대로 찍기"); }

            if (sel == 2) printSelectedLine("3. 모래 시계");
            else { Console.WriteLine("3. 모래 시계"); }

            if (sel == 3) printSelectedLine("4. 다이아");
            else { Console.WriteLine("4. 다이아"); }

            if (sel == 4) printSelectedLine("5. 종료하기");
            else { Console.WriteLine("5. 종료하기"); }
        }

        public void showMenu()
        {
            render();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        sel = (sel + 1) % 5;
                    }
                    if (key == ConsoleKey.DownArrow)
                    {
                        sel = (sel + 4) % 5;
                    }
                    // console switching
                    if (key == ConsoleKey.Spacebar)
                    {
                        // 정상종료
                        if(sel==4) { Environment.Exit(0);  }

                        Console.Clear();
                        // 이 객체 알아서 지워짐??
                        Result result = new Result(sel);
                        result.showResult();
                    }
                    Console.Clear();
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
                    Console.Clear();
                    render();
                }
            }
        }
    }

    class PrintStarApp
    {
        public static void Main()
        {
            Console.CursorVisible = false;
            Intro intro = new Intro();
            intro.startApp();
        }
    }
}