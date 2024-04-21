using System.Collections.Generic;

namespace LTT
{
    class LoginController
    {
        LoginView loginView; // login할때 필요한 UI

        MainController mainController; // login 후 MainController 호출하기 위해 필요

        MemberService memberService; // ID PW 확인위해 필요

        string curUserID;

        public LoginController()
        {
            loginView = new LoginView();
            memberService = MemberService.GetInstance(); 
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
                if (memberService.CheckIfValidLogin(userInputInfo))
                {
                    curUserID = userInputInfo[0];
                    mainController = new MainController(curUserID);
                    mainController.Run();
                }
            }
        }
    }
}
