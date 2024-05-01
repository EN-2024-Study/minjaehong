using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserFrontController
    {
        private UserController userController; // 추후에 호출해야되서 필요함

        private UserFrontView userFrontView; // FrontController에서 사용하는 view
   
        private MemberService memberService; // LOGIN 확인 작업에 쓰임
        
        public UserFrontController(BookService bookService, MemberService memberService)
        {
            this.userController = new UserController(bookService, memberService);

            this.userFrontView = new UserFrontView();

            this.memberService = memberService;
        }

        private void UserLogin()
        {
            List<string> loginInfo = userFrontView.UserLoginForm();
            string curUserID = loginInfo[0];

            // ID가 존재를 하고 && ID PW 이 둘 다 맞으면
            if (memberService.CheckIfMemberExists(curUserID) && memberService.CheckIfValidLogin(loginInfo))
            {
                Logger.recordLog(DateTime.Now, curUserID, "LOGIN SUCCESS");

                // userController에게 userID 전달해서 세팅하고 실행
                userController.InitializeUserController(curUserID);
                userController.RunUserMode();
            }
            else
            {
                Logger.recordLog(DateTime.Now, curUserID, "LOGIN FAIL", "INCORRECT ID AND PW");

                CommonView.RuntimeMessageForm("CHECK YOUR ID AND PASSWORD!");
            }
        }

        private void UserCreateAccount()
        {
            bool isCreateAccountSuccessful = false;

            List<string> dataFromView = userFrontView.UserCreateAccountForm();

            MemberDTO newMember = new MemberDTO(dataFromView);
            isCreateAccountSuccessful = memberService.AddNewMember(newMember);

            if (isCreateAccountSuccessful)
            {
                Logger.recordLog(DateTime.Now, newMember.GetId(), "CREATE ACCOUNT SUCCESS", "");
                
                CommonView.RuntimeMessageForm("NEW ACCOUNT IS CREATED!");
            }
            else
            {
                Logger.recordLog(DateTime.Now, newMember.GetId(), "CREATE ACCOUNT FAIL", "ALREADY EXISTING ID");

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
