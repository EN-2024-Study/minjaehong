using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // RegistrationController가 호출하는 View모음
    class RegistrationView
    {
        public RegistrationMode RegistrationModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT REGISTRATION MODE]");
            MyConsole.PrintAllMenu(Constants.registrationMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);

            RegistrationMode selectedMode = (RegistrationMode)CommonInput.GetUserSelection(Constants.registrationMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);
            return selectedMode;
        }

        public void RegistrationForm()
        {
            
        }

        public void RegistrationResultForm()
        {

        }

        public void RegistrationTableForm()
        {

        }

        public void RegistrationDeleteForm()
        {

        }
    }
}
