using System;

namespace HW1
{
    // 콘솔창 정보 및 중앙 출력을 위한 static 클래스
    public static class Default
    {
        // 현재 보여줄 화면 idx
        public static int pageidx = 0;
        // 사용자가 선택한 번호
        public static int sel = 0;

        // 중앙출력 시작 좌표
        public static int START_X = 35;
        public static int START_Y = 15;

        // 로고 출력 시작 좌표
        public const int LOGO_X = 5;
        public const int LOGO_Y = 5;

        // 콘솔 창 너비 높이
        public static int WIDTH = 80;
        public static int HEIGHT = 40;

        // 최대 줄 수
        public const int MAX_INPUT = 23;

        // 중앙 출력을 위한 동적 커서
        public static int startx;
        public static int starty;

        // START_X START_Y로 커서 옮기기
        // 이거 initCursorPos(int x,int y)로 바꾸기
        public static void initCursorPos()
        {
            startx = START_X;
            starty = START_Y;
        }

        // 중앙에 WRITELINE
        public static void writeLine(string str)
        {
            Console.SetCursorPosition(startx, starty);
            Console.Write(str);

            // 개행처리
            startx = START_X;
            starty++;
        }

        // 중앙에 WRITE
        public static void write(string str)
        {
            Console.SetCursorPosition(startx, starty);
            Console.Write(str);
            startx++;
        }

        // 테두리 그리기
        public static void drawBoard()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < WIDTH; i++)
            {
                Console.Write("─");
            }

            for (int k = 1; k < HEIGHT; k++)
            {
                Console.SetCursorPosition(0, k);
                Console.Write("│");
            }

            Console.SetCursorPosition(0, HEIGHT);
            for (int i = 0; i < WIDTH; i++)
            {
                Console.Write("─");
            }

            for (int k = 1; k < HEIGHT; k++)
            {
                Console.SetCursorPosition(WIDTH, k);
                Console.Write("│");
            }

            Console.SetCursorPosition(0, 0);
            Console.Write("┌");

            Console.SetCursorPosition(WIDTH, 0);
            Console.Write("┐");

            Console.SetCursorPosition(0, HEIGHT);
            Console.Write("└");

            Console.SetCursorPosition(WIDTH, HEIGHT);
            Console.Write("┘");
        }
    }
}
