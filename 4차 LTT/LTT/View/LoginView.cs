using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class LoginView
    {
        public List<string> LoginForm()
        {
            Console.Clear();

            List<string> userLoginInput = new List<string>();

            // 화면 구성
            MyConsole.PrintAllMenu(Constants.loginMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);
            // 입력받기
            for (int i = 0; i < Constants.loginMenuArr.Length; i++)
            {
                userLoginInput.Add(CommonInput.GetUserInput(Constants.INPUT_STARTX, Constants.INPUT_STARTY + i));
            }

            return userLoginInput;
        }
    }
}
