using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // ShoppingController가 호출하는 view모음
    class ShoppingView
    {
        public ShoppingMode ShoppingModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT SHOPPING MODE]");
            MyConsole.PrintAllMenu(Constants.shoppingMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);

            ShoppingMode selectedMode = (ShoppingMode)CommonInput.GetUserSelection(Constants.shoppingMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);
            return selectedMode;
        }

        public void ShoppingForm()
        {

        }

        public void ShoppingResultForm()
        {

        }

        public void ShoppingTableForm()
        {

        }

        public void ShoppingDeleteForm()
        {

        }
    }
}
