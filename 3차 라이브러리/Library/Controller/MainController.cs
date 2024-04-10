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

        // MainController와 연결되어야하는 애들
        UserFrontController userFrontController;
        ManagerController managerController;
        MainView mainView;

        private MainController()
        {
            // 의존성 주입 느낌
            this.userFrontController = UserFrontController.GetInstance();
            this.managerController = ManagerController.GetInstance();
            this.mainView = MainView.GetInstance();
        }

        public static MainController GetInstance()
        {
            if (instance == null)
            {
                instance = new MainController();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        public void run()
        {
            LibraryMode mode;

            bool isProgramRunning = true;

            // Library 시작점
            while (isProgramRunning)
            {
                mode = mainView.MainModeSelectForm();

                switch (mode)
                {
                    case LibraryMode.USER_MODE:
                        
                        // USERFRONTCONTROLLER 단으로 들어감
                        userFrontController.run();
                        break;

                    case LibraryMode.MANAGER_MODE:
                        // MANAGERMODE이면 별로 할게 없기 때문에
                        // ID PW 확인만 하고 managercontroller를 여기서 바로 실행시켜줘도 됨
                        mainView.ManagerLoginForm();
                        managerController.run();
                        break;
                }
            }
        }
    }
}