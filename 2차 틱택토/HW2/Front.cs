using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Front : Page
    {
        public Front(GameInfo gameInfo, MyConsole myconsole) : base(gameInfo, myconsole)
        {

        }

        override public int Show()
        {
            // 이름 입력받고 공용객체에 저장
            myconsole.InitCursorPos();
            myconsole.WriteLine("[ENTER USERNAME...]");
            myconsole.WriteLine("");
            myconsole.Write("username : ");
            GameInfo.GetInstance().username = Console.ReadLine();

            // 이름 입력되면 무조건 다음화면으로
            Console.Clear();
            return 1;
        }

    }
}
