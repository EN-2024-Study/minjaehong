using System;

using System.Collections;

namespace TicTacToe
{
    class Game : Page
    {
        char[,] grid;
        BitArray p1, p2, com;
        
        public Game(GameInfo gameInfo, MyConsole myconsole) : base(gameInfo, myconsole)
        {
            grid = new char[3, 3];
            p1 = new BitArray(9);
            p2 = new BitArray(9);
            com = new BitArray(9);
        }

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

        bool IsWinner(BitArray visited)
        {
            if ((visited[0] & visited[4] & visited[8]) || // 대각선 2개
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

        void PlaceOX(int input, int turn)
        {
            // 사용자는 1~9까지 입력하고 배열에서는 0~8로 인식
            // 그래서 하나 까주기
            input--;

            // grid 배열에서의 열 행 구하기
            int y = input / 3;
            int x = input % 3;

            if (turn == 1)
            {
                grid[y, x] = 'O';
                p1[input] = true;
            }
            else if(turn == 2)
            {
                grid[y, x] = 'X';
                p2[input] = true;
            }
        }

        void ShowWinner(int turn)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(GameInfo.GAME_X, GameInfo.GAME_Y - 3);

            if (turn == 0) Console.Write("무승부");
            else if(turn==1) Console.Write("p2 win");
            else if(turn==2) Console.Write("p1 win");

            Console.SetCursorPosition(GameInfo.GAME_X, GameInfo.GAME_Y + 10);
            Console.Write("PRESS BACKSPACE TO GO BACK...");
            Console.ForegroundColor = ConsoleColor.White;
        }

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

        void PlaceOXByCom()
        {
            if (GetBitSetCount(com) == 0)
            {
                // 사용자가 첫수로 중앙에 놨으면
                if (p1[4])
                {
                    // 아무 모서리
                    // 여기서는 0 0에 놓기
                    grid[0, 0] = 'X';
                    com[0] = true;
                }
                else
                {
                    // 아니면 내가 중앙에 놓기
                    grid[1, 1] = 'X';
                    com[4] = true;
                }
                return;
            }

            // 내가 이길 수 있을때 이기기
            for (int i = 0; i < 8; i++)
            {
                int x = winningrows[i, 0];
                int y = winningrows[i, 1];
                int z = winningrows[i, 2];

                // 2개씩 채워져있을때 + p1이 안채웠을때
                if (com[x] == false && com[y] == true && com[z] == true && p1[x] == false)
                {
                    com[x] = true;
                    grid[z / 3, z % 3] = 'X';
                    return;
                }
                if (com[x] == true && com[y] == false && com[z] == true && p1[y] == false)
                {
                    com[y] = true;
                    grid[y / 3, y % 3] = 'X';
                    return;
                }
                if (com[x] == true && com[y] == true && com[z] == false && p1[z] == false)
                {
                    com[z] = true;
                    grid[z / 3, z % 3] = 'X';
                    return;
                }
            }

            // 플레이어가 다음번에 이길 수 있을때 채워서 막기
            for (int i = 0; i < 8; i++)
            {
                int x = winningrows[i, 0];
                int y = winningrows[i, 1];
                int z = winningrows[i, 2];

                // 2개씩 채워져있을때 + 내가 남은 자리에 놓을 수 있을때 
                if(p1[x]==false && p1[y]==true && p1[z]==true && com[x]==false)
                {
                    com[x] = true;
                    grid[x / 3, x % 3] = 'X';
                    return;
                }
                if(p1[x]==true && p1[y]==false && p1[z] == true && com[y]==false)
                {
                    com[y] = true;
                    grid[y / 3,y % 3] = 'X';
                    return;
                }
                if(p1[x]==true && p1[y]==true && p1[z] == false && com[z]==false)
                {
                    com[z] = true;
                    grid[z / 3, z % 3] = 'X';
                    return;
                }
            }

            // 딱히 놓을 곳 없으면
            for(int i = 0; i < 8; i++)
            {
                if(p1[i]==false && com[i] == false)
                {
                    com[i] = true;
                    grid[i / 3, i % 3] = 'X';
                    return;
                }
            }

            return;
        }

        int GetBitSetCount(BitArray bitset)
        {
            int ret = 0;
            for(int i = 0; i < bitset.Length; i++)
            {
                if (bitset[i] == true) ret++;
            }
            return ret;
        }

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
                    Console.Write("0~9 에서 고르시오");
                    Console.ForegroundColor = ConsoleColor.White;

                    // 사용자가 입력한 값만 지워주기
                    EraseUserInput(str, left, top);
                }
            }
            return input;
        }

        void GameModeCVP()
        {
            myconsole.DrawBoard(grid);
            int turn = 0;
            int input;
            
            // p1이 승리했거나 com이 승리했을때 종료
            while (!IsWinner(p1) && !IsWinner(com))
            {
                // user 입력받기
                input = GetUserInput();

                // p1이 OX 놔주기
                PlaceOX(input, 1);
                myconsole.DrawBoard(grid);
                if (IsGridFull()) break;
                if (IsWinner(p1))
                {
                    turn = 2;
                    gameInfo.usrWin++;
                    break;
                }

                PlaceOXByCom();
                myconsole.DrawBoard(grid);
                if (IsGridFull()) break;
                if (IsWinner(com))
                {
                    turn = 1;
                    gameInfo.comWin++;
                    break;
                }
            }
            // Winner 출력
            ShowWinner(turn);
        }

        void GameModePVP()
        {
            int turn = 1;
            int input;

            // 한 명이 승리 했을때까지 돌리기
            while (!IsWinner(p1) && !IsWinner(p2))
            {
                // rendering
                myconsole.DrawBoard(grid);

                input = GetUserInput();

                // OX 놔주기
                PlaceOX(input, turn);

                // PVP니까 매번 turn 바꿔주기
                if (turn == 1) turn = 2;
                else if (turn == 2) turn = 1;

                // 만약 칸이 다 찼다면 무승부처리
                if (IsGridFull())
                {
                    break;
                }
            }
            // Winner 출력
            ShowWinner(turn);
        }

        // 틱택토 게임 화면
        override public int Show()
        {
            IntializeGrid();
            InitializeBitset();

            if(gameInfo.mode == GameInfo.Mode.CVP) GameModeCVP();
            if(gameInfo.mode == GameInfo.Mode.PVP) GameModePVP();

            // backspace 입력 대기
            Exception.CheckIfBackSpace();
            // 화면 다 지우고 
            Console.Clear();
            // menu page로 복귀
            return -1;
        }
    }
}