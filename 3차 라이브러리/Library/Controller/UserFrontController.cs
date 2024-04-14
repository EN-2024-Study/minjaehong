using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserFrontController
    {
        UserController userController; // 추후에 호출해야되서 필요함

        UserFrontView userFrontView; // FrontController에서 view 쓰려고 필요함
        RuntimeView runtimeView;

        MemberModel memberModel; // LOGIN 확인 작업에 쓰임

        public UserFrontController()
        {
            memberModel = MemberModel.GetInstance();

            userController = new UserController();

            userFrontView = new UserFrontView();
            runtimeView = new RuntimeView();
        }

        public void Run()
        {
            UserFrontMode userMode;
            bool isUserFrontRunning = true;

            while (isUserFrontRunning)
            {
                // USER MODE 보여주기
                userMode = userFrontView.UserFrontForm();

                switch (userMode)
                {
                    case UserFrontMode.GOBACK:
                        isUserFrontRunning = false;
                        break;

                    case UserFrontMode.USER_LOGIN:

                        List<string> loginInfo = userFrontView.UserLoginForm();

                        // ID가 존재를 하고 && ID PW 이 둘 다 맞으면
                        if (memberModel.CheckIfIdExists(loginInfo) && memberModel.CheckIfValidLogin(loginInfo))
                        {
                            // login 성공한 userID 추출해서
                            string curUserID = loginInfo[0];
                            // userController에게 userID 전달해서 세팅하고 실행
                            userController.InitializeUserController(curUserID); 
                            userController.Run();
                        }
                        else
                        {
                            runtimeView.RuntimeMessageForm("CHECK YOUR ID AND PASSWORD!");
                        }

                        break;

                    case UserFrontMode.USER_CREATE_ACCOUNT:

                        bool isCreateAccountSuccessful = false;

                        List<string> dataFromView = userFrontView.UserCreateAccountForm();
                        
                        MemberDTO newMember = new MemberDTO(dataFromView);
                        isCreateAccountSuccessful = memberModel.AddNewMember(newMember);

                        if (isCreateAccountSuccessful)
                        {
                            runtimeView.RuntimeMessageForm("NEW ACCOUNT IS CREATED!");
                        }
                        else
                        {
                            runtimeView.RuntimeMessageForm("THIS ID ALREADY EXISTS!");
                        }
                        break;
                };
            }
        }
    }
}
