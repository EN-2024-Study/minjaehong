using System;

namespace App
{
    class Result
    {
        int sel;
        public Result(int sel)
        {
            this.sel = sel;
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

                    // leftarrow �ƴϸ� ��� ��
                    if (key == ConsoleKey.LeftArrow) return;
                }
            }
        }

        public bool showResult()
        {
            int rows = 0;

            while (true)
            {
                Console.Write("�� �� �Է� : ");
                string str = Console.ReadLine();

                // parse �����ϸ� true ��ȯ�ϰ� row�� int��
                // �ƴϸ� false�� row�� 0
                int check = 0;
                bool checkifInt = int.TryParse(str, out check);

                // ���� ���̸� break
                if (checkifInt)
                {
                    rows = check;
                    break;
                }
            }

            if (sel == 0) print1(rows);
            else if (sel == 1) print2(rows);
            else if (sel == 2) print3(rows);
            else if (sel == 3) print4(rows);

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
            Console.WriteLine("1. ��� ���� �����");
            changeColorToWhite();
            if (sel == 1) changeColortoRed();
            Console.WriteLine("2. 1���� �ݴ�� ���");
            changeColorToWhite();
            if (sel == 2) changeColortoRed();
            Console.WriteLine("3. �� �ð�");
            changeColorToWhite();
            if (sel == 3) changeColortoRed();
            Console.WriteLine("4. ���̾�");
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