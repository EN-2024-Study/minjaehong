using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        // 다른 객체들이 참조할 수 있는 Common 객체 사용하려고
        // 중요한 놈이니까 public
        Common common;
        MyConsole myconsole;

        private List<Page> pages;

        public Program()
        {
            common = Common.GetInstance();
            myconsole = MyConsole.GetInstance();
            
            pages = new List<Page>(4);
            pages.Add(new Front(common, myconsole));
            pages.Add(new Menu(common, myconsole));
            pages.Add(new Game(common, myconsole));
            pages.Add(new History(common, myconsole));
        }

        public void Run()
        {
            while (true)
            {
                common.pageidx += pages[common.pageidx].Show();
            }
        }
    }
}
