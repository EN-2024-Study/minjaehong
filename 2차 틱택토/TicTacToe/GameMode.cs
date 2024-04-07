using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    // InGame 에서 실질적으로 중요한 함수들만 모아놓음
    // 1.GameModeCVP
    // 2.GameModePVP
    // 3.PlaceOXbyUser
    // 4.PlaceOXbyCom
    partial class Game : Page
    {
        // CVP Game일때
        void GameModeCVP()
        {
            myconsole.DrawBoard(grid);
            int input;

            // 둘 중 한 명이 이기기 전까지 돌림
            while (true)
            {
                // user 입력받기
                input = GetUserInput();

                // p1으로 OX 놔주기
                PlaceOXByUser(input, 1);
                myconsole.DrawBoard(grid);
                if (IsGridFull()) break;
                if (IsWinner(p1))
                {
                    gameInfo.usrWin++;
                    break;
                }

                PlaceOXByCom();
                myconsole.DrawBoard(grid);
                if (IsGridFull()) break;
                if (IsWinner(com))
                {
                    gameInfo.comWin++;
                    break;
                }
            }

            // Winner 출력
            PrintWinner(com, p1);
        }

        // PVP 게임일때 
        void GameModePVP()
        {
            int turn = 1;
            int input;

            // 한 명이 승리 했을때까지 돌리기
            while (true)
            {
                // 매턴마다 rendering 해주기
                myconsole.DrawBoard(grid);

                if (IsWinner(p1) || IsWinner(p2) || IsGridFull()) break;

                input = GetUserInput();

                // OX 놔주기
                PlaceOXByUser(input, turn);

                // PVP니까 매번 turn 바꿔주기
                if (turn == 1) turn = 2;
                else if (turn == 2) turn = 1;
            }

            // Winner 출력
            PrintWinner(p1, p2);
        }
        // USER의 OX 놔주는 함수
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
            else if (turn == 2)
            {
                grid[y, x] = 'X';
                p2[input] = true;
            }
        }

        // COM의 OX 놔주는 함수
        // CVP에만 사용됨
        void PlaceOXByCom()
        {

            // 예외 - 맨 처음에는 정해져있음
            // COM의 bitarray에 true 개수가 0개면 com의 첫 수 라는 뜻
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
                    // 아니면 COM이 중앙에 놓기
                    grid[1, 1] = 'X';
                    com[4] = true;
                }
                return;
            }

            // COM이 이길 수 있을때는 이겨야함
            // 담턴에 이길 수 있는지 확인
            // winningrows 순회하며 담턴에 놓았을때 이길 수 있는 경우가 있는지 확인
            for (int i = 0; i < 8; i++)
            {
                int x = winningrows[i, 0];
                int y = winningrows[i, 1];
                int z = winningrows[i, 2];

                // COM이 2개씩 채웠을때 + USER가 남은 자리에 안놓았을때
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

            // USER가 담턴에 이길 수 있을때는 채워서 막아야함
            // COM이 지지만 않으면 됨
            for (int i = 0; i < 8; i++)
            {
                int x = winningrows[i, 0];
                int y = winningrows[i, 1];
                int z = winningrows[i, 2];

                // USER가 이미 2개씩 채웠고 + COM이 남은 자리에 아직 안놓았을때 
                if (p1[x] == false && p1[y] == true && p1[z] == true && com[x] == false)
                {
                    com[x] = true;
                    grid[x / 3, x % 3] = 'X';
                    return;
                }
                if (p1[x] == true && p1[y] == false && p1[z] == true && com[y] == false)
                {
                    com[y] = true;
                    grid[y / 3, y % 3] = 'X';
                    return;
                }
                if (p1[x] == true && p1[y] == true && p1[z] == false && com[z] == false)
                {
                    com[z] = true;
                    grid[z / 3, z % 3] = 'X';
                    return;
                }
            }

            int idx = 0;

            // 딱히 놓을 곳 없으면 모서리부터 선점
            // 모서리에 둘 다 안놓았으면
            if (p1[0] == false && com[0] == false) idx = 0;
            else if (p1[2] == false && com[2] == false) idx = 2;
            else if (p1[6] == false && com[6] == false) idx = 6;
            else if (p1[8] == false && com[8] == false) idx = 8;
            // 모서리도 꽉찼으면
            // 진짜 아무데나 놔도 지지는 않음
            else
            {
                // 앞순서부터 안놔진곳 찾아서 놓기
                for (int i = 0; i < 8; i++)
                {
                    if (p1[i] == false && com[i] == false)
                    {
                        idx = i;
                        break;
                    }
                }
            }

            com[idx] = true;
            grid[idx / 3, idx % 3] = 'X';
            return;
        }
    }
}
