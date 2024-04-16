using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class LoginController
    {
        LoginView loginView;

        MainController mainController;

        UserModel userModel;

        public LoginController()
        {
            loginView = new LoginView();
            userModel = new UserModel();
            mainController = new MainController();
        }

        public void Run()
        {
            bool isProgramRunning = true;

            // login 기능밖에 없어서
            // login 확인만 해주면 됨
            while (isProgramRunning)
            {
                // 사용자가 입력을 했으면
                List<string> userInputInfo = loginView.LoginForm();

                // 만약 ID PW가 맞으면
                // 그때서야 MainController 생성해서 Run 호출
                if (userModel.CheckIfValidLogin(userInputInfo))
                {
                    string userID = userInputInfo[0];
                    mainController.Initialize(userID);
                    mainController.Run();
                }
            }
        }
    }
}
