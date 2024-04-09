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

        public static MainView GetInstance()
        {
            if (instance == null)
            {
                instance = new MainView();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        private MainView()
        {
            
        }

        string[] modeSelectMenuArr = { "1. USER MODE", "2. MANAGER MODE" };

        public LibraryMode ModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT MODE]");

            MyConsole.PrintAllMenu(modeSelectMenuArr);
            LibraryMode selectedMode = (LibraryMode)MyConsole.GetUserSelection(modeSelectMenuArr);

            return selectedMode;
        }

        public void UserLoginForm()
        {
            Console.Clear();

            bool loginSuccess = false;

            Console.WriteLine("[USER LOGIN]");
            Console.WriteLine("ID : ");
            Console.WriteLine("PW : ");

            
            /*
            while (!loginSuccess)
            {

            }
            */
        }

        public void ManagerLoginForm()
        {
            Console.Clear();

            bool loginSuccess = false;

            Console.WriteLine("[MANAGER LOGIN]");
            Console.WriteLine("ID : ");
            Console.WriteLine("PW : ");

            /*
            while (!loginSuccess)
            {

            }
            */
        }
    }
}
