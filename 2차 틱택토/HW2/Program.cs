using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        // 다른 객체들이 참조할 수 있는 GameInfo 객체 사용하려고
        // 중요한 놈이니까 public
        GameInfo gameInfo;
        MyConsole myconsole;

        private List<Page> pages;

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
