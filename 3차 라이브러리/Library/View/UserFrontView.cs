using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserFrontView
    {
        private static UserFrontView instance;

        private UserFrontView() { }

        public static UserFrontView GetInstance()
        {
            if (instance == null)
            {
                instance = new UserFrontView();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        public UserFrontMode UserFrontForm()
        {
            Console.Clear();

            string[] userFrontMenuArr = { "LOGIN", "CREATE ACCOUNT" };

            MyConsole.PrintHeader("[USER MODE]");
            MyConsole.PrintAllMenu(userFrontMenuArr);

            // UserFrontMode userFrontMode = 
            return (UserFrontMode)MyConsole.GetUserSelection(userFrontMenuArr);

            /*
            // Redirect 느낌으로??
            if (userFrontMode == UserFrontMode.USER_LOGIN)
            {
                UserLoginForm();
            }
            else
            {
                UserCreateAccountForm();                
            }
            */
        }

        // 1. LOGIN
        public List<string> UserLoginForm()
        {
            Console.Clear();

            string[] loginFormArr = { "ID", "PW" };

            MyConsole.PrintHeader("[USER LOGIN]");
            MyConsole.PrintAllMenu(loginFormArr);

            List<string> loginInfo = MyConsole.GetUserInputs(loginFormArr.Length);
            
            return loginInfo;
        }

        // 2. CREATE ACCOUNT
        // USER가 입력한 정보를 List<string> 형태로 다시 controller한테 넘겨줌
        // controller는 이걸 바탕으로 MemberDTO를 만들어서 memberDB에 저장해주면 됨
        public List<string> UserCreateAccountForm()
        {
            Console.Clear();

            string[] createAccountArr = { "ID", "PW", "NAME", "AGE", "PHONENUM" };

            MyConsole.PrintHeader("[USER CREATE ACCOUNT]");
            MyConsole.PrintAllMenu(createAccountArr);
            List<string> newMemberInfo = MyConsole.GetUserInputs(createAccountArr.Length);

            return newMemberInfo;
        }
    }
}
