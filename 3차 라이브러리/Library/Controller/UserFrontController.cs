using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserFrontController
    {
        private static UserFrontController instance;

        UserController userController; // 추후에 호출해야되서 필요함
        UserFrontView userFrontView; // FrontController에서 view 쓰려고 필요함
        MemberModel memberModel; // LOGIN 확인 작업에 쓰임

        private UserFrontController()
        {
            // 의존성 주입 느낌
            this.userController = UserController.GetInstance();
            this.userFrontView = UserFrontView.GetInstance();
            this.memberModel = MemberModel.GetInstance();
        }

        public static UserFrontController GetInstance()
        {
            if (instance == null)
            {
                instance = new UserFrontController();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        public void run()
        {
            UserFrontMode userMode;

            while (true)
            {
                // USER MODE 보여주기
                userMode = userFrontView.UserFrontForm();

                switch (userMode)
                {
                    case UserFrontMode.USER_LOGIN:

                        List<string> loginInfo = userFrontView.UserLoginForm();

                        // ID PW 이 둘 다 맞으면
                        if (memberModel.CheckIfValidLogin(loginInfo))
                        {
                            string curUserID = loginInfo[0];
                            userController.InitializeUserController(curUserID);
                            userController.run();
                        }
                        break;

                    case UserFrontMode.USER_CREATE_ACCOUNT:

                        List<string> dataFromView = userFrontView.UserCreateAccountForm();
                        MemberDTO newMember = new MemberDTO(dataFromView);
                        memberModel.AddNewMember(newMember);

                        break;
                };
            }
        }
    }
}
