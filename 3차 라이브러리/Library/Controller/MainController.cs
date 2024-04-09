using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MainController
    {
        private static MainController instance;

        public static MainController GetInstance()
        {
            if (instance == null)
            {
                instance = new MainController();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        // Main 내에서만 사용하는 참조변수들
        UserController userController;
        ManagerController managerController;
        MainView mainView;

        private MainController()
        {
            // 의존성 주입 느낌
            this.userController = UserController.GetInstance();
            this.managerController = ManagerController.GetInstance();
            this.mainView = MainView.GetInstance();
        }

        public void run()
        {
            LibraryMode mode;

            // Library 시작점
            while (true)
            {
                mode = mainView.ModeSelectForm();

                switch (mode)
                {
                    case LibraryMode.USER_MODE:
                        mainView.UserLoginForm();
                        userController.run();
                        break;
                    case LibraryMode.MANAGER_MODE:
                        mainView.ManagerLoginForm();
                        managerController.run();
                        break;
                }
            }
        }
    }
}