using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class MyConsole
    {
        // 지금 이 포인터 하나만 static으로 올라가고
        // 객체는 new로 생성되는거임
        private static MyConsole instance;
        
        // 중앙 출력을 위한 동적 커서
        private int curx;
        private int cury;

        private MyConsole() { }
        
        public static MyConsole GetInstance()
        {
            if (instance == null)
            {
                return new MyConsole();
            }
            return instance;
        }

        // 중앙 출력일때 맨 초기에 원하는 곳으로 옮기기
        public void InitCursorPos()
        {
            curx = GameInfo.MENU_X;
            cury = 15;
        }

        // 중앙에 WRITELINE
        public void WriteLine(string str)
        {
            Console.SetCursorPosition(curx, cury);
            Console.Write(str);

            // 개행처리
            curx = GameInfo.MENU_X;
            cury++;
        }

        // 중앙에 WRITE
        public void Write(string str)
        {
            Console.SetCursorPosition(curx, cury);
            Console.Write(str);
            curx+=str.Length;
        }

        // 틱택토 보드 그려주기
        public void DrawBoard(char[,] grid)
        {
            // 중앙으로 커서 위치 변경
            InitCursorPos();
            WriteLine("┌───┬───┬───┐");
            WriteLine("│ " + grid[0,0] + " │ "+ grid[0,1] +" │ "+ grid[0,2] +" │");
            WriteLine("├───┼───┼───┤");
            WriteLine("│ " + grid[1, 0] + " │ " + grid[1, 1] + " │ " + grid[1, 2] + " │");
            WriteLine("├───┼───┼───┤");
            WriteLine("│ " + grid[2, 0] + " │ " + grid[2, 1] + " │ " + grid[2, 2] + " │");
            WriteLine("└───┴───┴───┘");
        }
    }
}