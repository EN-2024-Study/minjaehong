using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MainView
    {
        private static MainView instance;

        private MainView() { }

        public static MainView GetInstance()
        {
            if (instance == null)
            {
                instance = new MainView();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        // USER MODE 인지 MANAGER MODE인지 입력받고 return
        public LibraryMode MainModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT MODE]");
            MyConsole.PrintAllMenu(Constants.modeSelectMenuArr);

            LibraryMode selectedMode = (LibraryMode)MyConsole.GetUserSelection(Constants.modeSelectMenuArr);

            return selectedMode;
        }

        // MANAGER LOGIN
        public List<string> ManagerLoginForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[MANAGER LOGIN]");
            MyConsole.PrintAllMenu(Constants.managerFormArr);

            return MyConsole.GetUserInputs(Constants.managerFormArr.Length);
        }
    }
}
