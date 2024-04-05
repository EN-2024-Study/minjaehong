using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Front : Page
    {
        public Front(Common common, MyConsole myconsole) : base(common, myconsole)
        {

        }

        override public int Show()
        {
            // 이름 입력받고 공용객체에 저장
            myconsole.InitCursorPos();
            myconsole.writeLine("[ENTER USERNAME...]");
            myconsole.writeLine("");
            myconsole.write("username : ");
            Common.GetInstance().username = Console.ReadLine();

            // 이름 입력되면 무조건 다음화면으로
            Console.Clear();
            return 1;
        }

    }
}
