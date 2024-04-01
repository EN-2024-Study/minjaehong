using System;

namespace App
{
    // Result������ Console.Clear ��� ��
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
            string origin = "�� �� �Է� : ";

            while (true)
            {
                string dummy = origin; 
                string str = Console.ReadLine();

                // parse �����ϸ� true ��ȯ�ϰ� row�� int��
                // �ƴϸ� false�� row�� 0
                int check = 0;
                bool checkifInt = int.TryParse(str, out check);

                // ���ڸ�
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
                        Console.WriteLine("0���� ū ���� �Է¹ٶ�");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.SetCursorPosition(0, 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("��ȿ�� �� �Է¹ٶ�");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // ������� ���� ���� Ʋ�����ۿ�

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

                    // backspace �ƴϸ� ��� ��
                    if (key == ConsoleKey.Backspace) return;
                }
            }
        }

        /*
         * 0 �� �� �Է�
         * 1 ����
         * 
         * 3 ���
        */ 
        public bool showResult()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("�� �� �Է� : ");
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
            if (sel == 0) printSelectedLine("1. ��� ���� �����");
            else { Console.WriteLine("1. ��� ���� �����"); }

            if (sel == 1) printSelectedLine("2. 1���� �ݴ�� ���");
            else { Console.WriteLine("2. 1���� �ݴ�� ���"); }

            if (sel == 2) printSelectedLine("3. �� �ð�");
            else { Console.WriteLine("3. �� �ð�"); }

            if (sel == 3) printSelectedLine("4. ���̾�");
            else { Console.WriteLine("4. ���̾�"); }

            if (sel == 4) printSelectedLine("5. �����ϱ�");
            else { Console.WriteLine("5. �����ϱ�"); }
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
                        // ��������
                        if(sel==4) { Environment.Exit(0);  }

                        Console.Clear();
                        // �� ��ü �˾Ƽ� ������??
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
            Console.WriteLine("1. ����");
            Console.ForegroundColor = ConsoleColor.White;
            if (sel == 1) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("2. ����");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // �޴� ������
        // 0 �����ϱ�
        // 1 �����ϱ�
        public void startApp()
        {
            render();

            while (true)
            {
                // Ű �ԷµǾ����� �˻�
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow) sel = (sel + 1) % 2;
                    
                    // console switching
                    if (key == ConsoleKey.Spacebar)
                    {
                        // 1�̸� ��������
                        if (sel == 1) { Environment.Exit(0); }

                        Console.Clear();

                        // �Ѿ��
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