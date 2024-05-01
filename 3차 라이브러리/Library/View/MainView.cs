using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MainView
    {
        public MainView() { }

        private string[] modeSelectMenuArr = { "1. USER MODE", "2. MANAGER MODE", "3. PROGRAM EXIT" };
        private string[] managerLoginArr = { "ID :", "PW :" };

        // USER MODE 인지 MANAGER MODE인지 입력받고 return
        public LibraryMode MainModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT MODE]");
  
            LibraryMode selectedMode = (LibraryMode)MyConsole.GetUserSelection(modeSelectMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
            return selectedMode;
        }

        // MANAGER LOGIN
        public LoginDTO ManagerLoginForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[MANAGER LOGIN]");
            
            List<string> managerLoginInputs = MyConsole.GetUserInputs(managerLoginArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.managerLoginExceptionArr);

            return new LoginDTO(managerLoginInputs);
        }
    }
}
