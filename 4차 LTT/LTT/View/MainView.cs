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
        public MainView() { }

        public MainMode MainModeSelectForm(string curUserID)
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT MODE]");
            MyConsole.PrintUserID(curUserID);
            
            MyConsole.PrintAllMenu(Constants.modeSelectMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);

            MainMode selectedMode = (MainMode)CommonInput.GetUserSelection(Constants.modeSelectMenuArr,Constants.MENU_STARTX, Constants.MENU_STARTY);
            return selectedMode;
        }
        

    }
}
