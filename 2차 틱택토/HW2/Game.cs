using System;

using System.Collections;

namespace TicTacToe
{
    class Game : Page
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

        public Game(GameInfo gameInfo, MyConsole myconsole) : base(gameInfo, myconsole)
        {
            grid = new char[3, 3];
            p1 = new BitArray(9);
            p2 = new BitArray(9);
            com = new BitArray(9);
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

        // User의 OX 놔주는 함수
        // PVP CVP에 둘 다 사용됨
        void PlaceOXByUser(int input, int turn)
        {
            // 사용자는 1~9까지 입력하고 배열에서는 0~8로 인식
            // 그래서 하나 까주기
            input--;

            // grid 배열에서의 열 행 구하기
            int y = input / 3;
            int x = input % 3;

            // p1이면
            if (turn == 1)
            {
                grid[y, x] = 'O';
                p1[input] = true;
            }
            // p2이면
            else if(turn == 2)
            {
                grid[y, x] = 'X';
                p2[input] = true;
            }
        }

        void ShowWinner(BitArray bs1, BitArray bs2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(GameInfo.GAME_X, GameInfo.GAME_Y - 3);

            if (gameInfo.mode == GameInfo.Mode.CVP)
            {
                if (IsWinner(bs1)) Console.Write("com win!");
                else if (IsWinner(bs2)) Console.Write("usr win!");
                else Console.Write("무승부");
            }

            if(gameInfo.mode==GameInfo.Mode.PVP)
            {
                if (IsWinner(bs1)) Console.Write("p1 win!");
                else if (IsWinner(bs2)) Console.Write("p2 win!");
                else Console.Write("무승부");
            }

            Console.SetCursorPosition(GameInfo.GAME_X, GameInfo.GAME_Y + 10);
            Console.Write("PRESS BACKSPACE TO GO BACK...");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Com의 OX 놔주는 함수
        // CVP에만 사용됨
        void PlaceOXByCom()
        {
            // 맨 처음에는 정해져있음
            if (GetBitSetCount(com) == 0)
            {
                // 사용자가 첫 수로 중앙에 놨으면
                if (p1[4])
                {
                    // 아무 모서리에 놔도 됨
                    // 여기서는 0 0에 놓기
                    grid[0, 0] = 'X';
                    com[0] = true;
                }
                else
                {
                    // 아니면 Com이 중앙에 놓기
                    grid[1, 1] = 'X';
                    com[4] = true;
                }
                return;
            }

            // Com이 이길 수 있을때는 이겨야함
            for (int i = 0; i < 8; i++)
            {
                int x = winningrows[i, 0];
                int y = winningrows[i, 1];
                int z = winningrows[i, 2];

                // 2개씩 채워져있을때 + p1이 남은 자리에 안놓았을때
                if (com[x] == false && com[y] == true && com[z] == true && p1[x] == false)
                {
                    com[x] = true;
                    grid[x / 3, x % 3] = 'X';
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

            // User가 담턴에 이길 수 있을때는 채워서 막아야함
            for (int i = 0; i < 8; i++)
            {
                int x = winningrows[i, 0];
                int y = winningrows[i, 1];
                int z = winningrows[i, 2];

                // user꺼가 2개씩 채워져있을때 + Com이 남은 자리에 아직 안놓았을때 
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

            // 딱히 놓을 곳 없으면 아무데나 놓아도 무방
            // 지지만 않으면 된다
            for(int i = 0; i < 8; i++)
            {
                // 둘 다 안놓은 곳에 아무데나 놓기
                // 여기서는 앞순서만
                if(p1[i]==false && com[i] == false)
                {
                    com[i] = true;
                    grid[i / 3, i % 3] = 'X';
                    return;
                }
            }

            return;
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
            
            // 둘 중 한 명이 이기기 전까지 돌림
            while (!IsWinner(p1) && !IsWinner(com))
            {
                // user 입력받기
                input = GetUserInput();

                // p1으로 OX 놔주기
                PlaceOXByUser(input, 1);
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
            ShowWinner(com, p1);
        }

        void GameModePVP()
        {
            int turn = 1;
            int input;

            // 한 명이 승리 했을때까지 돌리기
            while (!IsWinner(p1) && !IsWinner(p2))
            {
                // 매턴마다 rendering 해주기
                myconsole.DrawBoard(grid);

                if (IsGridFull()) break;

                input = GetUserInput();

                // OX 놔주기
                PlaceOXByUser(input, turn);

                // PVP니까 매번 turn 바꿔주기
                if (turn == 1) turn = 2;
                else if (turn == 2) turn = 1;
            }
            // Winner 출력
            ShowWinner(p1, p2);
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