using System;

namespace App
{
    class Result
    {

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
        }

        public void startGame()
        {
            Console.WriteLine("1");
            Console.WriteLine("2");
            Console.WriteLine("3");
            Console.WriteLine("4");

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
                    Console.Clear();
                    render();
                }
                System.Threading.Thread.Sleep(500);
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