using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    // Game에 사용되는 input 처리 및 예외처리는 모두 Exception.cs 에서 함
    static class Exception
    {
        // 유효한 값인지 확인해주는 함수
        // 유효한 값이면 1-9 숫자 반환
        // 유효한 값 아니면 -1 반환
        public static int CheckIfValidInput(string str)
        {
            // parse 가능하면 true 반환하고 row는 int값
            // 아니면 false고 row는 0
            int check = 0;
            bool checkifInt = int.TryParse(str, out check);

            // 0으로 시작하는 숫자문자열이면 문자열처리
            if (str.StartsWith("0")) checkifInt = false;

            // 유효한 숫자면 번호 반환
            if (checkifInt && check > 0 && check < 10) return check;
            // 아니면 0 반환
            else return 0;
        }

        // backspace 눌렀는지 확인
        public static void CheckIfBackSpace()
        {
            bool flag = false;

            while (!flag)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace) flag = true;
                }
            }

            return;
        }
    }
}
