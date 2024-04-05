using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TicTacToe
{
    class Game : Page
    {
        public Game(Common common, MyConsole myconsole) : base(common, myconsole)
        {
            
        }

        char[,] grid = new char[3, 3];
        BitArray p1 = new BitArray(9);
        BitArray p2 = new BitArray(9);
        BitArray com = new BitArray(9);

        void IntializeGrid()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int k = 0; k < 3; k++)
                {
                    grid[i,k] = ' ';
                }
            }
        }
        
        void InitializeBitset()
        {
            p1.SetAll(false);
            p2.SetAll(false);
            com.SetAll(false);
        }

        private bool IsGridFull()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int k = 0; k < 3; k++)
                {
                    if (grid[i,k] == ' ') return false;
                }
            }
            return true;
        }

        // 유효한 값인지 확인해주는 함수
        private int CheckIfValidInput()
        {
            // 위치 입력 : 바로 옆 자리 저장
            // common.startx common.starty 적용 되어 있음 -> ?? 
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            while (true)
            {
                string dummy = "";
                string str = Console.ReadLine();

                // ctrl+c 예외처리
                if (str == null) { continue; }

                // parse 가능하면 true 반환하고 row는 int값
                // 아니면 false고 row는 0
                int check = 0;
                bool checkifInt = int.TryParse(str, out check);

                // 0으로 시작하는 숫자문자열이면 문자열처리
                if (str.StartsWith("0"))
                {
                    checkifInt = false;
                }

                // 숫자면
                if (checkifInt)
                {
                    if (check > 0 && check < 10)
                    {
                        // 성공으로 row 값 반환
                        for (int i = 0; i < str.Length * 2; i++) dummy += " ";

                        // 잘못입력된거 가리기
                        Console.SetCursorPosition(left, top);
                        Console.Write(dummy);

                        // 다시 입력받을 수 있게 "줄 수 입력 :" 바로 옆으로 커서 배치
                        Console.SetCursorPosition(left, top);
                        return check;
                    }
                    else
                    {
                        // 음수일때
                        Console.SetCursorPosition(common.GAME_X, common.GAME_Y + 9);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("0~9 에서 고르시오");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                // 문자열일때
                else
                {
                    Console.SetCursorPosition(common.GAME_X, common.GAME_Y + 9);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("0~9 에서 고르시오");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // ============== 틀린 경우면 입력된 값 가리기 =====================

                for (int i = 0; i < str.Length * 2; i++) dummy += " ";

                // 잘못입력된거 가리기
                Console.SetCursorPosition(left, top);
                Console.Write(dummy);

                // 다시 입력받을 수 있게 "줄 수 입력 :" 바로 옆으로 커서 배치
                Console.SetCursorPosition(left, top);
            }
        }

        bool IsWinner(BitArray visited)
        {
            if((visited[0] & visited[4] & visited[8]) || // 대각선 2개
               (visited[2] & visited[4] & visited[6]) ||
               (visited[0] & visited[1] & visited[2]) || // 가로 3개
               (visited[3] & visited[4] & visited[5]) ||
               (visited[6] & visited[7] & visited[8]) ||
               (visited[0] & visited[3] & visited[6]) || // 세로 3개
               (visited[1] & visited[4] & visited[7]) ||
               (visited[2] & visited[5] & visited[8]))
            {
                return true;
            }
            return false;
        }

        // 틱택토 게임 화면
        override public int Show()
        {
            IntializeGrid();
            InitializeBitset();

            // vs com
            if (common.mode == 1)
            {
                int turn = 1;
                myconsole.drawboard(grid);

                // 한 명이 승리 했을때까지 돌리기
                while (!IsWinner(p1) && !IsWinner(p2))
                {
                    myconsole.writeLine("");
                    myconsole.write("위치 입력 : ");

                    // 유효한 값 입력받기
                    // 이거 호출될때 커서는 위치 입력 : 다음꺼

                    // 사용자는 1~9까지 입력하고 배열에서는 0~8로 인식
                    int input = CheckIfValidInput() - 1;

                    // grid 배열에서의 열 행 구하기
                    int y = input / 3;
                    int x = input % 3;

                    // 이미 놓아진 곳이면 다시 입력받기
                    if (grid[y,x] != ' ') continue;

                    // 성공이면 처리해주고 turn 바꿔주기
                    if (turn == 1)
                    {
                        grid[y, x] = 'O';
                        p1[input] = true;
                        turn = 2;
                    }
                    else
                    {
                        grid[y, x] = 'X';
                        p2[input] = true;
                        turn = 1;
                    }

                    // 최신화 반영해서 다시 그려주기
                    myconsole.drawboard(grid);
                    // 만약 칸이 다 찼다면 무승부처리
                    if (IsGridFull()) {
                        break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                if (turn == 0)
                {
                    Console.SetCursorPosition(common.GAME_X, common.GAME_Y - 3);
                    Console.WriteLine("무승부");
                }
                else if (turn == 2)
                {
                    Console.SetCursorPosition(common.GAME_X, common.GAME_Y - 3);
                    Console.WriteLine("p1 win");
                    common.usr1Win++;
                }
                else
                {
                    Console.SetCursorPosition(common.GAME_X, common.GAME_Y - 3);
                    Console.WriteLine("p2 win");
                    common.usr2Win++;
                }
                Console.ForegroundColor = ConsoleColor.White;

                Console.ReadLine();

                Console.Clear();
                return -1;
            }

            return -1;
        }

        public void Render()
        {

        }
    }

}
