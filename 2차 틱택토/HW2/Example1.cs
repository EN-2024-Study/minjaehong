using System;

// namespace 성격이나 하는 일에 따라 클래스, 메소드 등을 하나의 이름으로 묶어 두는 것
namespace HW2
{

    // static class
    public static class globals
    {
        public static string user_name;
    }

    // 모든 데이터 처리는 얘가함
    public class Model
    {
        public string user_name;
        public int curmode;
    }

    // 모든 콘솔 출력 처리는 얘가 함
    public class View
    {
        public string showStartGame()
        {
            string name;
            Console.Write("username : ");
            name = Console.ReadLine();
            return name;
        }

        /* ========================================== */

        public void showMenu()
        {
            Console.WriteLine("1. vs computer");
            Console.WriteLine("2. vs user");
            Console.WriteLine("3. scoreboard");
            Console.WriteLine("4. quit");
        }

        public void printSelectedLine(int cur)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            switch (cur)
            {
                case 0:
                    Console.SetCursorPosition(0, 0);
                    Console.Write("1. vs computer");
                    break;
                case 1:
                    Console.SetCursorPosition(0, 1);
                    Console.Write("2. vs user");
                    break;
                case 2:
                    Console.SetCursorPosition(0, 2);
                    Console.Write("3. scoreboard");
                    break;
                case 3:
                    Console.SetCursorPosition(0, 3);
                    Console.Write("4. quit");
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
                    Console.Write("1. vs computer");
                    break;
                case 1:
                    Console.SetCursorPosition(0, 1);
                    Console.Write("2. vs user");
                    break;
                case 2:
                    Console.SetCursorPosition(0, 2);
                    Console.Write("3. scoreboard");
                    break;
                case 3:
                    Console.SetCursorPosition(0, 3);
                    Console.Write("4. quit");
                    break;
            }
        }

        public void render(int cur, int before)
        {
            printSelectedLine(cur);
            eraseSelectedLine(before);
        }

        // mode 번호 return
        public int showSelectMode()
        {
            int mode = 0;
            int before = -1;

            Console.WriteLine("Welcome To Play Tic Tac Toe");
            Console.WriteLine("Select Game Mode");

            showMenu();

            // 선택할때까지 이 함수에서 대기
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        before = mode;
                        mode = (mode + 1) % 5;
                    }
                    if (key == ConsoleKey.DownArrow)
                    {
                        before = mode;
                        mode = (mode + 4) % 5;
                    }
                    // console switching
                    if (key == ConsoleKey.Spacebar)
                    {
                        Console.Clear();
                        break;
                    }

                    render(mode, before);
                }
                System.Threading.Thread.Sleep(100);
            }

            // 선택된 모드 반환
            return mode;
        }

        public void showGame()
        {

        }

        public void showScoreMenu()
        {

        } 
    }

    // Model and View를 연결해줌
    public class Controller
    {
        Model model = new Model();
        View view = new View();

        int url = 0;

        // run 안에 while문으로 묶어줘야하나
        public void run()
        {
            // 이름 입력 창
            if (url == 0)
            {
                model.user_name = view.showStartGame();
                // 이름 받으면 바로 넘어가기
                url++;
            }
            
            // 모드 선택 창
            if(url == 1)
            {
                model.curmode = view.showSelectMode();
            }

            // 게임 창
            if (url == 2)
            {

            }
        }
    }

    class Front
    {
        public void showFrontPage()
        {
            Console.WriteLine("username: ");
            globals.user_name = Console.ReadLine();
        }   
    }

    class MyApp
    {
        /*
        public static void Main()
        {
            Console.Title = "틱택토";
            Console.CursorVisible = false;
        }
        */

        public static void Main()
        {
            ConsoleKeyInfo cki;
            Console.Clear();

            Console.CancelKeyPress += new ConsoleCancelEventHandler(myHandler);

            while (true)
            {
                Console.Write("Press any Key, or 'X' to quit");
                Console.WriteLine("CTRL+C to interrupt the read operation");

                cki = Console.ReadKey();

                Console.WriteLine("key pressed:{cki.key}\n");
                if (cki.Key == ConsoleKey.X) break;
            }
        }

        protected static void myHandler(Object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("\nThe read operation has been interrupted");
            Console.WriteLine(" Key Pressed : {args.SpecialKey}");
            Console.WriteLine(" Cancel property : {args.Cancel}");

            Console.WriteLine("Setting the Cancel Property to true...");
            args.Cancel = true;

            Console.WriteLine("Cancel property : {args.cancel}");
            Console.WriteLine("The read operation will resume...\n");
        }
    }
}