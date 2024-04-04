using System;
using System.Threading;
using System.Collections.Generic;

namespace HW1
{
    interface Page
    {
        // ������ ����
        // �׻� Console.Clear()�ϰ� ����
        int show();

        // show �ȿ��� ȣ���
        void render();    
    }

    // ��� â
    class Result : Page
    {
        // ���â�� ������ �ʿ䰡 ����
        public void render() { }

        // ���â �Լ�
        public int show()
        {
            // Ŀ�� �ʱ�ȭ
            Default.drawBoard();
            Default.initCursorPos();

            Console.ForegroundColor = ConsoleColor.Green;
            Default.write("�� �� �Է� : ");
            Console.ForegroundColor = ConsoleColor.White;

            // ���� �� �Էµɶ�����
            int rows = checkifValidNum();

            // ================ ��ȿ�� ���� �ԷµǾ����� �Ʒ� ���� =============== // 

            // ���� �޽��� �����ϱ�
            Default.initCursorPos();
            Default.starty += 2;
            Default.write("                                             ");

            int sel = Default.sel;

            // ��� ���
            if (sel == 0) print1(rows);
            else if (sel == 1) print2(rows);
            else if (sel == 2) print3(rows);
            else if (sel == 3) print4(rows);

            Default.starty++;
            Default.write("PRESS BACKSPACE TO GO BACK");
            // backspace �Է� ��ٸ���
            checkifBackSpace();

            // ������ �޴�â����
            Console.Clear();
            return -1;
        }

        // ��ȿ�� ������ Ȯ�����ִ� �Լ�
        private int checkifValidNum()
        {
            // �� �� �Է� : �ٷ� �� �ڸ� ����
            // Default.startx Default.starty ���� �Ǿ� ����
            int left = Console.CursorLeft;

            while (true)
            {
                string dummy = "";
                string str = Console.ReadLine();

                // ctrl+c ����ó��
                if (str == null) { continue; }

                // parse �����ϸ� true ��ȯ�ϰ� row�� int��
                // �ƴϸ� false�� row�� 0
                int check = 0;
                bool checkifInt = int.TryParse(str, out check);

                // 0���� �����ϴ� ���ڹ��ڿ��̸� ���ڿ�ó��
                if (str.StartsWith("0"))
                {
                    checkifInt = false;
                }

                // ���ڸ�
                if (checkifInt)
                {
                    // �ܼ�â�� �Ѿ�� ����̸�
                    if (check > Default.MAX_INPUT)
                    {
                        Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("����� �ܼ�â�� �Ѿ�� �����Դϴ�        ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (check > 0)
                    {
                        // ���̾� ����ó��
                        if (Default.sel == 3 && check % 2 == 0)
                        {
                            Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("���̾ƴ� Ȧ���� �����մϴ�               ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            // �������� row �� ��ȯ
                            return check;
                        }
                    }
                    else
                    {
                        // �����϶�
                        Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("0���� ũ�� " + Default.MAX_INPUT + "���� ���� ������ �����մϴ�");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                // ���ڿ��϶�
                else
                {
                    Console.SetCursorPosition(Default.START_X, Default.START_Y + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("0���� ũ�� " + Default.MAX_INPUT + "���� ���� ������ �����մϴ�");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // ============== Ʋ�� ���� �Էµ� �� ������ =====================

                for (int i = 0; i < str.Length * 2; i++) dummy += " ";

                // �߸��ԷµȰ� ������
                Console.SetCursorPosition(left, Default.starty);
                Console.Write(dummy);

                Default.drawBoard();

                // �ٽ� �Է¹��� �� �ְ� "�� �� �Է� :" �ٷ� ������ Ŀ�� ��ġ
                Console.SetCursorPosition(left, Default.starty);
            }
        }

        // ����� 1
        private void print1(int rows)
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

        // ����� 2
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

        // ����� 3
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

        // ����� 4
        private void print4(int rows)
        {
            // Ŀ�� �ʱ�ȭ
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

        // backspace ���ȴ��� Ȯ��
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

        // �Է¿� ���� ����ٲ��ֱ�
        public void render()
        {
            printSelectedLine(sel);
            eraseSelectedLine(before);
        }

        public int show()
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
                        Console.Clear();
                        return (sel == 4) ? -1 : 1;
                    }
                    render();

                    // Result���� ǥ���� sel �ֽ�ȭ
                    Default.sel = sel;
                }
            }
        }

        // render���� ���
        void printSelectedLine(int sel)
        {
            Default.initCursorPos();
            Console.ForegroundColor = ConsoleColor.Red;
            switch (sel)
            {
                case 0:
                    Console.SetCursorPosition(Default.startx, Default.starty);
                    Console.Write("�� ��� ���� �����");
                    break;
                case 1:
                    Console.SetCursorPosition(Default.startx, Default.starty + 1);
                    Console.Write("�� 1���� �ݴ�� ���");
                    break;
                case 2:
                    Console.SetCursorPosition(Default.startx, Default.starty + 2);
                    Console.Write("�� �� �ð�");
                    break;
                case 3:
                    Console.SetCursorPosition(Default.startx, Default.starty + 3);
                    Console.Write("�� ���̾�");
                    break;
                case 4:
                    Console.SetCursorPosition(Default.startx, Default.starty + 4);
                    Console.Write("�� ����ȭ��");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        // render���� ���
        void eraseSelectedLine(int before)
        {
            Default.initCursorPos();
            switch (before)
            {
                case 0:
                    Console.SetCursorPosition(Default.startx, Default.starty);
                    Console.Write("�� ��� ���� �����");
                    break;
                case 1:
                    Console.SetCursorPosition(Default.startx, Default.starty + 1);
                    Console.Write("�� 1���� �ݴ�� ���");
                    break;
                case 2:
                    Console.SetCursorPosition(Default.startx, Default.starty + 2);
                    Console.Write("�� �� �ð�");
                    break;
                case 3:
                    Console.SetCursorPosition(Default.startx, Default.starty + 3);
                    Console.Write("�� ���̾�");
                    break;
                case 4:
                    Console.SetCursorPosition(Default.startx, Default.starty + 4);
                    Console.Write("�� ����ȭ��");
                    break;
            }
        }

        // �޴� ��� ���
        // �ʱ⿡ �ѹ��� ���
        void initial_render()
        {
            Default.drawBoard();
            Default.initCursorPos();
            Default.writeLine("�� ��� ���� �����");
            Default.writeLine("�� 1���� �ݴ�� ���");
            Default.writeLine("�� �� �ð�");
            Default.writeLine("�� ���̾�");
            Default.writeLine("�� ����ȭ��");
            Default.writeLine("");
            Default.writeLine("PRESS SPACE TO SELECT!");
        }
    }

    class Front : Page
    {
        int sel = 0;

        // �Է¿� ���� ���� �ٲ��ֱ�
        public void render()
        {
            // Ŀ�� �ʱ�ȭ
            Default.initCursorPos();
            if (sel == 0) Console.ForegroundColor = ConsoleColor.Red;
            Default.writeLine("�� ����");
            Console.ForegroundColor = ConsoleColor.White;
            if (sel == 1) Console.ForegroundColor = ConsoleColor.Red;
            Default.writeLine("�� ����");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public int show()
        {
            // �ʱ�ȭ�� ��� ��ġ�� �ٲ��ֱ�
            Default.START_X = 35;
            Default.START_Y = 15;

            // �ʱ⼼��
            render();
            Default.drawBoard();

            // �ΰ� ������
            // �׵θ� �׸� �Ŀ� ����
            Thread logothread = new Thread(showLogo);
            logothread.Start();

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

                        // ������ ����
                        logothread.Abort();
                        
                        // �߾������ ���� Default �� �ٲٱ�
                        Default.START_X = 28;
                        Default.START_Y = 8;
                        Console.Clear();

                        // ���� �������� menu�� �Ѿ��
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

        // ������ - �ΰ� �����ֱ�
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

        // ������ - �ΰ� �����
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
            // ������ ����
            pages = new List<Page>(3);
            pages.Add(new Front());
            pages.Add(new Menu());
            pages.Add(new Result());
        }

        // ���α׷� ������
        public void run()
        {
            // show�� +1 return�ϸ� ���� ��������
            // show�� -1 return�ϸ� ���� ��������
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

        // �ܼ� ���� Ű��� �� �ܼ� â ȯ�漳��
        public static void initializeConsole()
        {
            Console.SetWindowSize(120, 120);
            Console.Title = "����⺰���";
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