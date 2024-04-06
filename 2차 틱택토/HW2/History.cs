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
            myconsole.writeLine("");
            myconsole.writeLine("com win : " + common.comWin.ToString());
            myconsole.writeLine("usr win : " + common.usrWin.ToString());
            myconsole.writeLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            myconsole.writeLine("PRESS BACKSPACE TO GO BACK...");
            Console.ForegroundColor = ConsoleColor.White;

            Exception.CheckIfBackSpace();

            // 끝나면 무조건 메뉴로 
            // 4 -> 2
            Console.Clear();
            return -2;
        }
    }
}
