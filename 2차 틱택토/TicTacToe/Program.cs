using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        private List<Page> pages;

        GameInfo gameInfo;
        MyConsole myconsole;

        public Program()
        {
            gameInfo = GameInfo.GetInstance();
            myconsole = MyConsole.GetInstance();
            
            pages = new List<Page>(4);
            pages.Add(new Front(gameInfo, myconsole));
            pages.Add(new Menu(gameInfo, myconsole));
            pages.Add(new Game(gameInfo, myconsole));
            pages.Add(new History(gameInfo, myconsole));
        }

        public void Run()
        {
            while (true)
            {
                gameInfo.pageidx += pages[gameInfo.pageidx].Show();
            }
        }
    }
}
