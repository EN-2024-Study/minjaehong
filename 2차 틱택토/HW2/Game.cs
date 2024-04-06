using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TicTacToe
{
    class Game : Page
    {
        char[,] grid;
        BitArray p1, p2, com;
        
        public Game(Common common, MyConsole myconsole) : base(common, myconsole)
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
        // 해당 입력 값만 화면에서 지워줌
        private void EraseUserInput(string str, int left, int top)
        {
            // 위치입력: 바로 옆으로 커서 배치
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
            if (turn == 0)
            {
                Console.SetCursorPosition(common.GAME_X, common.GAME_Y - 3);
                Console.Write("무승부");
                Console.SetCursorPosition(common.GAME_X, common.GAME_Y + 10);
                Console.Write("PRESS BACKSPACE TO GO BACK...");
            }
            else if (turn == 2)
            {
                Console.SetCursorPosition(common.GAME_X, common.GAME_Y - 3);
                Console.Write("p1 win");
                Console.SetCursorPosition(common.GAME_X, common.GAME_Y + 10);
                Console.Write("PRESS BACKSPACE TO GO BACK...");
            }
            else
            {
                Console.SetCursorPosition(common.GAME_X, common.GAME_Y - 3);
                Console.WriteLine("p2 win");
                Console.SetCursorPosition(common.GAME_X, common.GAME_Y + 10);
                Console.Write("PRESS BACKSPACE TO GO BACK...");
            }
            Console.ForegroundColor = ConsoleColor.White;
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
                string str;
                int input = 0;

                // 한 명이 승리 했을때까지 돌리기
                while (!IsWinner(p1) && !IsWinner(p2))
                {
                    bool right_input = false;

                    // 위치 입력 : 찍기
                    Console.SetCursorPosition(common.GAME_X, common.GAME_Y + 8);
                    myconsole.write("위치 입력 : ");
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
                            Console.SetCursorPosition(common.GAME_X, common.GAME_Y + 9);
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
                            Console.SetCursorPosition(common.GAME_X, common.GAME_Y + 9);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("0~9 에서 고르시오");
                            Console.ForegroundColor = ConsoleColor.White;

                            // 사용자가 입력한 값만 지워주기
                            EraseUserInput(str, left, top);
                        }
                    }

                    // OX 놔주기
                    PlaceOX(input, turn);
                    
                    // turn 바꿔주기
                    if (turn == 1) turn = 2;
                    else if (turn == 2) turn = 1;

                    // 최신화 반영해서 다시 그려주기
                    myconsole.drawboard(grid);
                    // 만약 칸이 다 찼다면 무승부처리
                    if (IsGridFull())
                    {
                        break;
                    }
                }

                // Winner 출력
                ShowWinner(turn);
                // backspace 받으면 퇴실
                Exception.CheckIfBackSpace();
                // 지우고 나가주기
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