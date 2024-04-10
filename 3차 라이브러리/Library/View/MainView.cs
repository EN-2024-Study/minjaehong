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

            string[] modeSelectMenuArr = { "1. USER MODE", "2. MANAGER MODE" };

            MyConsole.PrintHeader("[SELECT MODE]");
            MyConsole.PrintAllMenu(modeSelectMenuArr);

            LibraryMode selectedMode = (LibraryMode)MyConsole.GetUserSelection(modeSelectMenuArr);

            return selectedMode;
        }

        // MANAGER MODE 로그인 담당
        // 로그인 확인 절차 거쳐야함
        public void ManagerLoginForm()
        {
            Console.Clear();

            string[] managerFormArr = { "ID", "PW" };

            MyConsole.PrintHeader("[MANAGER LOGIN]");
            MyConsole.PrintAllMenu(managerFormArr);

        }
    }
}
