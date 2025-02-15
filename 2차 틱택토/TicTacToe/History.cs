﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class History : Page
    {
        public History(GameInfo gameInfo, MyConsole myconsole) : base(gameInfo, myconsole)
        {

        }

        override public int Show()
        {

            myconsole.InitCursorPos();
            myconsole.WriteLine("[HISTORY]");
            myconsole.WriteLine("");
            myconsole.WriteLine("COM WIN : " + gameInfo.comWin.ToString());
            myconsole.WriteLine("USER WIN : " + gameInfo.usrWin.ToString());
            myconsole.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            myconsole.WriteLine("PRESS BACKSPACE TO GO BACK...");
            Console.ForegroundColor = ConsoleColor.White;

            Exception.CheckIfBackSpace();

            // 끝나면 무조건 메뉴로 
            // 4 -> 2
            Console.Clear();
            return -2;
        }
    }
}
