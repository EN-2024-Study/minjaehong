using System;

using System.Collections;

namespace TicTacToe
{
    partial class Game : Page
    {
        char[,] grid; // 출력할때 참조할 grid
        BitArray p1, p2, com; // 각자 어디에 뒀는지 기록하는 용도

        // 이기는 방법 가짓수
        int[,] winningrows = {
            {0,4,8 },
            {2,4,6 },
            {0,1,2 },
            {3,4,5 },
            {6,7,8 },
            {0,3,6 },
            {1,4,7 },
            {2,5,8 }
        };

        // 생성자로 게임에 사용할 3x3배열과 bitArray 3개 생성
        public Game(GameInfo gameInfo, MyConsole myconsole) : base(gameInfo, myconsole)
        {
            grid = new char[3, 3];
            p1 = new BitArray(9);
            p2 = new BitArray(9);
            com = new BitArray(9);
        }

        // 틱택토 게임 화면 띄우기
        override public int Show()
        {
            IntializeGrid();
            InitializeBitset();

            if (gameInfo.mode == GameInfo.Mode.CVP) GameModeCVP();
            if (gameInfo.mode == GameInfo.Mode.PVP) GameModePVP();

            // backspace 입력 대기
            Exception.CheckIfBackSpace();
            // 화면 다 지우고 
            Console.Clear();
            // menu page로 복귀
            return -1;
        }

        // 매번 게임 시작 전 Grid 초기화
        void IntializeGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    grid[i, k] = ' ';
                }
            }
        }

        // 매번 게임 시작 전 BitSet 초기화
        void InitializeBitset()
        {
            p1.SetAll(false);
            p2.SetAll(false);
            com.SetAll(false);
        }

        // 사용자가 잘못된 값을 입력했을때
        // 해당 입력 값만 화면에서 지워주는 함수
        // == "위치 입력 : " 오른쪽에 씌여진 것들 모두 제거해줌
        private void EraseUserInput(string str, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            // 기존 사용자 입력값 지우기
            string dummy = "";
            for (int i = 0; i < str.Length * 2; i++) dummy += " ";
            Console.Write(dummy);
        }

        // 꽉 찼는지 확인
        private bool IsGridFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (grid[i, k] == ' ') return false;
                }
            }
            return true;
        }

        // winningrows 8가지를 모두 돌며 winner인지 확인하는 함수
        bool IsWinner(BitArray visited)
        {
            for(int i = 0; i < 8; i++)
            {
                int x = winningrows[i, 0];
                int y = winningrows[i, 1];
                int z = winningrows[i, 2];
                if(visited[x] & visited[y] & visited[z]) return true;

            }
            return false;
        }

                // winner이면 console창에 winner라고 print해주는 함수
        void PrintWinner(BitArray bs1, BitArray bs2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(GameInfo.GAME_X, GameInfo.GAME_Y - 3);

            if (gameInfo.mode == GameInfo.Mode.CVP)
            {
                if (IsWinner(bs1)) Console.Write("COM WON!");
                else if (IsWinner(bs2)) Console.Write("USER WON!");
                else Console.Write("DRAW!");
            }

            if(gameInfo.mode==GameInfo.Mode.PVP)
            {
                if (IsWinner(bs1)) Console.Write("P1 WON!");
                else if (IsWinner(bs2)) Console.Write("P2 WON!");
                else Console.Write("DRAW!");
            }

            Console.SetCursorPosition(GameInfo.GAME_X, GameInfo.GAME_Y + 10);
            Console.Write("PRESS BACKSPACE TO GO BACK...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // 해당 빈칸에 놓을 수 있는지 확인
        // input으로는 이미 유효처리가 된 숫자가 들어옴
        bool IsPossible(int input)
        {
            input--;

            // grid 배열에서의 열 행 구하기
            int y = input / 3;
            int x = input % 3;

            // 이미 놓아진 곳이면 다시 입력받기
            if (grid[y, x] != ' ') return false;
            else return true;
        }

        // bitset 내 true값 count
        int GetBitSetCount(BitArray bitset)
        {
            int ret = 0;
            for(int i = 0; i < bitset.Length; i++)
            {
                if (bitset[i] == true) ret++;
            }
            return ret;
        }

        // 사용자 입력 받아서 유효한 grid 내 번호 return
        int GetUserInput()
        {
            string str;
            int input = 0;
            bool right_input = false;

            // 위치 입력 : 찍기
            Console.SetCursorPosition(GameInfo.GAME_X, GameInfo.GAME_Y + 8);
            myconsole.Write("위치 입력 : ");
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            // 유효한 값 입력받을때까지 돌기
            while (!right_input)
            {
                Console.SetCursorPosition(left, top);
                str = Console.ReadLine();

                // 유효한 값이면
                if ((input = Exception.CheckIfValidInput(str)) != 0 && IsPossible(input))
                {
                    Console.SetCursorPosition(GameInfo.GAME_X, GameInfo.GAME_Y + 9);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("                                  ");
                    Console.ForegroundColor = ConsoleColor.White;
                    EraseUserInput(str, left, top);
                    right_input = true;
                }
                // 틀린 값이면
                else
                {
                    // 경고 문자 띄우고
                    Console.SetCursorPosition(GameInfo.GAME_X, GameInfo.GAME_Y + 9);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("1~9 중 빈칸인 곳을 고르시오");
                    Console.ForegroundColor = ConsoleColor.White;

                    // 사용자가 입력한 값만 지워주기
                    EraseUserInput(str, left, top);
                }
            }
            return input;
        }
    }
}