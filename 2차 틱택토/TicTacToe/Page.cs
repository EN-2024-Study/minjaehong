using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    abstract class Page
    {
        // 공용객체 참조할 수 있게 참조변수 선언
        protected GameInfo gameInfo;
        protected MyConsole myconsole;

        public Page(GameInfo gameInfo, MyConsole myconsole)
        {
            this.gameInfo = gameInfo;
            this.myconsole = myconsole;
        }

        // 화면 출력 함수
        // 무조건 Console.Clear 하고 끝냄
        // 다음꺼가 바로 화면 띄울 수 있게
        public abstract int Show();
    }
}