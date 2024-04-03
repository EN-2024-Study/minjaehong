using System;
using System.Threading;

namespace App
{
    // �ܼ�â ���� �� �߾� ����� ���� static Ŭ����
    public static class Default
    {
        public static int START_X = 35;
        public static int START_Y = 15;

        public const int LOGO_X = 5;
        public const int LOGO_Y = 5;

        public static int WIDTH = 80;
        public static int HEIGHT = 40;

        public const int MAX_INPUT = 23;

        public static bool flag = false;

        // ���� Ŀ��
        public static int startx;
        public static int starty;

        // START_X START_Y�� Ŀ�� �ű��
        // �̰� initCursorPos(int x,int y)�� �ٲٱ�
        public static void initCursorPos()
        {
            startx = START_X;
            starty = START_Y;
        }

        // �߾ӿ� WRITELINE
        public static void writeLine(string str)
        {
            Console.SetCursorPosition(startx, starty);
            Console.Write(str);

            // ����ó��
            startx = START_X;
            starty++;
        }

        // �߾ӿ� WRITE
        public static void write(string str)
        {
            Console.SetCursorPosition(startx, starty);
            Console.Write(str);
            startx++;
        }

        // �׵θ� �׸���
        public static void drawBoard()
        {
            Console.SetCursorPosition(0, 0);
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

    // ��� â
    class Result
    {
        int sel;
        public Result(int sel)
        {
            this.sel = sel;
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
                        if (sel == 3 && check % 2 == 0)
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

        // ���â �Լ�
        public void showResult()
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

            // ��� ���
            if (sel == 0) print1(rows);
            else if (sel == 1) print2(rows);
            else if (sel == 2) print3(rows);
            else if (sel == 3) print4(rows);

            // BackSpace �ԷµǾ����� �ٽ� �޴�â���� 
            Default.starty++;
            Default.write("PRESS BACKSPACE TO GO BACK");
            checkifBackSpace();
        }
    }

    class Menu
    {
        int sel = 0;
        int before = -1;

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
                    Console.Write("�� �����ϱ�");
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
                    Console.Write("�� �����ϱ�");
                    break;
            }
        }

        // �ٲ� �͵鸸 ������
        void render()
        {
            printSelectedLine(sel);
            eraseSelectedLine(before);
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
            Default.writeLine("�� �����ϱ�");
            Default.writeLine("");
            Default.writeLine("PRESS SPACE TO SELECT!");
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
                        if (sel == 4)
                        {
                            Environment.Exit(0);
                        }

                        Console.Clear();

                        Result result = new Result(sel); // gc?
                        result.showResult();

                        Console.Clear();
                        initial_render();
                    }
                    render();
                }
            }
        }
    }

    class Front
    {
        int sel = 0;
        Menu menu;

        public Front()
        {
            menu = new Menu();
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
            Console.SetCursorPosition(Default.LOGO_X,++starty);
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

        // �Է¿� ���� ���� �ٲ��ֱ�
        private void render()
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

        public void startApp()
        {
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

                        Console.Clear();

                        // ������ �Ͻ��ߴ� �޴��� �Ѿ��
                        logothread.Abort();
                        Default.START_X = 28;
                        Default.START_Y = 8;
                        Console.Clear();
                        //Console.Out.Flush();
                        menu.showMenu();
                    }

                    render();
                }
            }
        }
    }

    class PrintStarApp
    {
        protected static void ctrlHandler(Object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
        }

        public static void Main()
        {
            Console.Title = "����⺰���";

            int width = Console.LargestWindowWidth;
            int height = Console.LargestWindowHeight;

            // â ũ�� ����
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ctrlHandler);
            Console.CursorVisible = false;

            Front front = new Front();
            front.startApp();

            Console.Out.Flush();
        }
    }
}