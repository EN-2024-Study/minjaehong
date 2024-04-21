using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // MainController가 호출하는 View모음
    class MainView
    {
        private string[] modeSelectMenuArr = { "1. 강의조회", "2. 관심과목 담기", "3. 수강신청", "4. 수강내역 조회" };

        public MainView() { }

        public MainMode MainModeSelectForm(string curUserID)
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT MODE]");
            MyConsole.PrintUserID(curUserID);

            MainMode selectedMode = (MainMode)MyConsole.GetUserSelection(modeSelectMenuArr,MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
            return selectedMode;
        }

    }
}
