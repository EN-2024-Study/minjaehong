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
            Console.Write("줄 수 입력 : ");
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
        int sel = 0; // 0~3

        public void changeColortoRed()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public void changeColorToWhite()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void render()
        {
            if (sel == 0) changeColortoRed();
            Console.WriteLine("1. 가운데 정렬 별찍기");
            changeColorToWhite();
            if (sel == 1) changeColortoRed();
            Console.WriteLine("2. 1번의 반대로 찍기");
            changeColorToWhite();
            if (sel == 2) changeColortoRed();
            Console.WriteLine("3. 모래 시계");
            changeColorToWhite();
            if (sel == 3) changeColortoRed();
            Console.WriteLine("4. 다이아");
            changeColorToWhite();
            Console.CursorVisible = false;
        }

        public void startGame()
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
                        sel = (sel + 1) % 4;
                    }
                    if (key == ConsoleKey.DownArrow)
                    {
                        sel = (sel + 3) % 4;
                    }
                    // console switching
                    if (key == ConsoleKey.Spacebar)
                    {
                        Console.Clear();
                        // 이 객체 알아서 지워짐??
                        Result result = new Result(sel);
                        while (result.showResult()) ;
                    }
                    Console.Clear();
                    render();
                }
                System.Threading.Thread.Sleep(100);
            }
        }
    }

    class PrintStartApp
    {
        public static void Main()
        {
            Menu menu = new Menu();
            menu.startGame();
        }
    }
}