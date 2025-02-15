﻿using System;
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
            UserFrontMode selectedMode = (UserFrontMode)MyConsole.GetUserSelection(userFrontMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
            
            return selectedMode;
        }

        // 1. LOGIN
        public LoginDTO UserLoginForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[USER LOGIN]");
            List<string> userLoginInfo = MyConsole.GetUserInputs(userLoginArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.userLoginExceptionArr);
            
            return new LoginDTO(userLoginInfo);
        }

        // 2. CREATE ACCOUNT
        // USER가 입력한 정보를 List<string> 형태로 다시 controller한테 넘겨줌
        // controller는 이걸 바탕으로 MemberDTO를 만들어서 memberDB에 저장해주면 됨
        public MemberDTO UserCreateAccountForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[USER CREATE ACCOUNT]");
            List<string> newMemberInfo = MyConsole.GetUserInputs(createAccountArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.userCreateAccountExceptionArr);

            return new MemberDTO(newMemberInfo);
        }
    }
}
