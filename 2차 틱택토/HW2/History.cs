using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class History : Page
    {
        public History(Common common, MyConsole myconsole) : base(common, myconsole)
        {

        }

        override public int Show()
        {

            myconsole.InitCursorPos();
            myconsole.writeLine("[history]");
            myconsole.writeLine("com win : " + common.comWin.ToString());
            myconsole.writeLine("usr1 win : " + common.usr1Win.ToString());
            myconsole.writeLine("usr2 win : " + common.usr2Win.ToString());
            myconsole.writeLine("PRESS BACK SPACE TO GO BACK");

            Console.ReadLine();

            // 끝나면 무조건 메뉴로 
            // 4 -> 2
            Console.Clear();
            return -2;
        }
    }
}
