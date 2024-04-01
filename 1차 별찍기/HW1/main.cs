using System;
using System.IO;
using System.Runtime.ConstrainedExecution;

namespace App
{
    public static class Default
    {
        public const int START_X = 25;
        public const int START_Y = 10;
        public const int WIDTH = 80; // �� 2��
        public const int HEIGHT = 40;
        
        // ���� Ŀ��
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

            // ����ó��
            startx = START_X;
            starty++;  
        }

        public static void write(string str)
        {
            Console.SetCursorPosition(startx, starty);
            Console.Write(str);
            startx++;
        }

        // �� ó�� �� �ѹ� ȣ�� �� �� �̻� ȣ�� ���ص� �ǰ�
        public static void drawBoard()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                Console.Write("��");
            }

            for (int k = 1; k < HEIGHT; k++)
            {
                Console.SetCursorPosition(0, k);
                Console.Write("��");
            }

            Console.SetCursorPosition(0, HEIGHT);
            for (int i = 0; i < WIDTH; i++)
            {
                Console.Write("��");
            }

            for (int k = 1; k < HEIGHT; k++)
            {
                Console.SetCursorPosition(WIDTH, k);
                Console.Write("��");
            }

            Console.SetCursorPosition(0, 0);
            Console.Write("��");

            Console.SetCursorPosition(WIDTH, 0);
            Console.Write("��");

            Console.SetCursorPosition(0, HEIGHT);
            Console.Write("��");

            Console.SetCursorPosition(WIDTH, HEIGHT);
            Console.Write("��");
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
            // �� �� �Է� : �ٷ� �� �ڸ� ����
            // Default.startx Default.starty ���� �Ǿ� ����
            int left = Console.CursorLeft;

            while (true)
            {
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
                        Console.SetCursorPosition(Default.START_X, Default.START_Y+2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("0���� ū ���� �Է¹ٶ�");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("��ȿ�� �� �Է¹ٶ�    ");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // ������� ���� ���� Ʋ�����ۿ�

                for (int i = 0; i < str.Length*2; i++) dummy += " ";

                // �߸��ԷµȰ� ������
                Console.SetCursorPosition(left, Default.starty);
                Console.Write(dummy);

                // �� �� �Է� : �ٷ� ������ Ŀ�� ��ġ
                Console.SetCursorPosition(left, Default.starty);
            }
        }

        public void print1(int rows)
        {
            // Ŀ�� �ʱ�ȭ
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
            // Ŀ�� �ʱ�ȭ
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
            Default.drawBoard();
            Default.initCursorPos();

            // ���⼭ 0 0 
            Console.ForegroundColor = ConsoleColor.Green;
            Default.write("�� �� �Է� : ");
            Console.ForegroundColor = ConsoleColor.White;

            // ���� �� �Էµɶ�����
            int rows = checkifValidNum();

            // �޼��� ���� ȿ��
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
                    Console.Write("1. ��� ���� �����");
                    break;
                case 1:
                    Console.SetCursorPosition(Default.startx, Default.starty+1);
                    Console.Write("2. 1���� �ݴ�� ���");
                    break;
                case 2:
                    Console.SetCursorPosition(Default.startx, Default.starty+2);
                    Console.Write("3. �� �ð�");
                    break;
                case 3:
                    Console.SetCursorPosition(Default.startx, Default.starty+3);
                    Console.Write("4. ���̾�");
                    break;
                case 4:
                    Console.SetCursorPosition(Default.startx, Default.starty+4);
                    Console.Write("5. �����ϱ�");
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
                    Console.Write("1. ��� ���� �����");
                    break;
                case 1:
                    Console.SetCursorPosition(Default.startx, Default.starty+1);
                    Console.Write("2. 1���� �ݴ�� ���");
                    break;
                case 2:
                    Console.SetCursorPosition(Default.startx, Default.starty+2);
                    Console.Write("3. �� �ð�");
                    break;
                case 3:
                    Console.SetCursorPosition(Default.startx, Default.starty+3);
                    Console.Write("4. ���̾�");
                    break;
                case 4:
                    Console.SetCursorPosition(Default.startx, Default.starty+4);
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
            Default.drawBoard();
            Default.initCursorPos();
            Default.writeLine("1. ��� ���� �����");
            Default.writeLine("2. 1���� �ݴ�� ���");
            Default.writeLine("3. �� �ð�");
            Default.writeLine("4. ���̾�");
            Default.writeLine("5. �����ϱ�");
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
            // Ŀ�� �ʱ�ȭ
            Default.initCursorPos();
            if (sel == 0) Console.ForegroundColor = ConsoleColor.Red;
            Default.writeLine("1. ����");
            Console.ForegroundColor = ConsoleColor.White;
            if (sel == 1) Console.ForegroundColor = ConsoleColor.Red;
            Default.writeLine("2. ����");
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

            // first and last drawBoard
            Default.drawBoard();
            Console.CursorVisible = true;

            Intro intro = new Intro();
            intro.startApp();
        }
    }
}