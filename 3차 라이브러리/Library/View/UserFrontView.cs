using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserFrontView
    {
        private string[] createAccountArr = { "ID :", "PW :", "NAME :", "AGE :", "PHONENUM(010~) :" };

        private string[] userLoginArr = { "ID :", "PW :" };

        private string[] userFrontMenuArr = { "LOGIN", "CREATE ACCOUNT" };

        public UserFrontView() { }

        public UserFrontMode UserFrontForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[USER MODE]");
            
            return (UserFrontMode)MyConsole.GetUserSelection(userFrontMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
        }

        // 1. LOGIN
        public List<string> UserLoginForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[USER LOGIN]");
           
            //List<string> loginInfo = InputHandler.GetUserInputs(Constants.userLoginArr.Length, ExceptionHandler.userLoginExceptionArr);
            List<string> loginInfo = MyConsole.GetUserInputs(userLoginArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.userLoginExceptionArr);
            
            return loginInfo;
        }

        // 2. CREATE ACCOUNT
        // USER가 입력한 정보를 List<string> 형태로 다시 controller한테 넘겨줌
        // controller는 이걸 바탕으로 MemberDTO를 만들어서 memberDB에 저장해주면 됨
        public List<string> UserCreateAccountForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[USER CREATE ACCOUNT]");

            //List<string> newMemberInfo = InputHandler.GetUserInputs(Constants.createAccountArr.Length, ExceptionHandler.userCreateAccountExceptionArr);
            List<string> newMemberInfo = MyConsole.GetUserInputs(createAccountArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.userCreateAccountExceptionArr);

            return newMemberInfo;
        }
    }
}
