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

            // �� �� �Է� : �ٷ� �� �ڸ� ����
            int left = Console.CursorLeft;

            int flag = 0;

            while (true)
            {
                flag = Console.CursorLeft;

                string dummy = "";
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
                    Console.WriteLine("��ȿ�� �� �Է¹ٶ�    ");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // ������� ���� ���� Ʋ�����ۿ�

                for (int i = 0; i < str.Length*2; i++) dummy += " ";

                // �߸��ԷµȰ� ������
                Console.SetCursorPosition(left, 0);
                Console.Write(dummy);

                // �� �� �Է� : �ٷ� ������ Ŀ�� ��ġ
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
        public void showResult()
        {
            // ���⼭ 0 0 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("�� �� �Է� : ");
            Console.ForegroundColor = ConsoleColor.White;

            // ���� �� �Էµɶ�����
            int rows = checkifValidNum();

            // �޼��� ���� ȿ��
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
                    Console.Write("1. ��� ���� �����");
                    break;
                case 1:
                    Console.SetCursorPosition(0, 1);
                    Console.Write("2. 1���� �ݴ�� ���");
                    break;
                case 2:
                    Console.SetCursorPosition(0, 2);
                    Console.Write("3. �� �ð�");
                    break;
                case 3:
                    Console.SetCursorPosition(0, 3);
                    Console.Write("4. ���̾�");
                    break;
                case 4:
                    Console.SetCursorPosition(0, 4);
                    Console.Write("5. �����ϱ�");
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
                    Console.Write("1. ��� ���� �����");
                    break;
                case 1:
                    Console.SetCursorPosition(0, 1);
                    Console.Write("2. 1���� �ݴ�� ���");
                    break;
                case 2:
                    Console.SetCursorPosition(0, 2);
                    Console.Write("3. �� �ð�");
                    break;
                case 3:
                    Console.SetCursorPosition(0, 3);
                    Console.Write("4. ���̾�");
                    break;
                case 4:
                    Console.SetCursorPosition(0, 4);
                    Console.Write("5. �����ϱ�");
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
            Console.WriteLine("1. ��� ���� �����");
            Console.WriteLine("2. 1���� �ݴ�� ���");
            Console.WriteLine("3. �� �ð�");
            Console.WriteLine("4. ���̾�");
            Console.WriteLine("5. �����ϱ�");
        }

        public void showMenu()
        {
            // �� ó�� ����
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
                        // ��������
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

                    render();
                }
            }
        }
    }

    class PrintStarApp
    {
        public static void Main()
        {
            Console.Title = "����⺰���";
            //Console.SetWindowSize(80, 40);

            Console.CursorVisible = false;
            Intro intro = new Intro();
            intro.startApp();
        }
    }
}