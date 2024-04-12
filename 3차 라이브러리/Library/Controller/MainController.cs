using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MainController
    { 
        // MainController와 연결되어야하는 애들
        UserFrontController userFrontController;
        ManagerController managerController;
        MainView mainView;

        public MainController()
        {
            // 의존성 주입 느낌
            userFrontController = new UserFrontController();
            managerController = new ManagerController();
            
            mainView = new MainView();
        }

        public void Run()
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
                        userFrontController.Run();
                        break;

                    case LibraryMode.MANAGER_MODE:

                        // MANAGERMODE이면 별로 할게 없기 때문에
                        // ID PW 확인만 하고 managercontroller를 여기서 바로 Run해줘도 됨
                        List<string> loginInfo = mainView.ManagerLoginForm();

                        string inputID = loginInfo[0];
                        string inputPW = loginInfo[1];

                        // ID PW 이 둘 다 맞으면
                        if (Constants.managerID == inputID && Constants.managerPW == inputPW)
                        {
                            managerController.Run();
                        }
                        break;
                }
            }
        }
    }
}