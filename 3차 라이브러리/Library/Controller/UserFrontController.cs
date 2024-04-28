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
        
        MemberService memberService; // LOGIN 확인 작업에 쓰임

        public UserFrontController()
        {
            memberService = MemberService.GetInstance();

            userController = new UserController();

            userFrontView = new UserFrontView();
        }

        void UserLogin()
        {
            List<string> loginInfo = userFrontView.UserLoginForm();
            string curUserID = loginInfo[0];

            // ID가 존재를 하고 && ID PW 이 둘 다 맞으면
            if (memberService.CheckIfMemberExists(curUserID) && memberService.CheckIfValidLogin(loginInfo))
            {
                // userController에게 userID 전달해서 세팅하고 실행
                userController.InitializeUserController(curUserID);
                userController.RunUserMode();
            }
            else
            {
                CommonView.RuntimeMessageForm("CHECK YOUR ID AND PASSWORD!");
            }
        }

        void UserCreateAccount()
        {
            bool isCreateAccountSuccessful = false;

            List<string> dataFromView = userFrontView.UserCreateAccountForm();

            MemberDTO newMember = new MemberDTO(dataFromView);
            isCreateAccountSuccessful = memberService.AddNewMember(newMember);

            if (isCreateAccountSuccessful)
            {
                CommonView.RuntimeMessageForm("NEW ACCOUNT IS CREATED!");
            }
            else
            {
                CommonView.RuntimeMessageForm("THIS ID ALREADY EXISTS!");
            }
        }

        public void RunUserFrontMode()
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
                        UserLogin();
                        break;

                    case UserFrontMode.USER_CREATE_ACCOUNT:
                        UserCreateAccount();
                        break;
                };
            }
        }
    }
}
