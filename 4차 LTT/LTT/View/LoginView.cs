﻿using System;
using System.Collections.Generic;

namespace LTT
{
    class LoginView
    {
        private string[] loginMenuArr = { "STUDENT ID :", "PASSWORD :" };

        public List<string> LoginForm()
        {
            Console.Clear();
            MyConsole.PrintHeader("WELCOME TO LECTURE TIMETABLE!");

            List<string> userLoginInput = MyConsole.GetUserInputs(loginMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);

            return userLoginInput;
        }
    }
}
