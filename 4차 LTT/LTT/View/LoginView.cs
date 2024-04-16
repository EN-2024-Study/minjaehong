using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class LoginView
    {
        private string[] loginMenuArr = { "STUDENT ID : ", "PASSWORD : " };

        public List<string> LoginForm()
        {
            Console.Clear();
            
            List<string> userLoginInput = CommonInput.GetUserInputs(loginMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);

            return userLoginInput;
        }
    }
}
