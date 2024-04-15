using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // 2번 관담 눌리면 그때부터는 얘가 담당
    class ShoppingController
    {
        ShoppingView shoppingView;

        public ShoppingController()
        {
            shoppingView = new ShoppingView();
        }

        public void Run()
        {
            bool isShoppingModeRunning = true;

            ShoppingMode mode;

            while (isShoppingModeRunning)
            {
                mode = shoppingView.ShoppingModeSelectForm();

                switch (mode)
                {
                    case ShoppingMode.SHOPPING:
                        break;
                    case ShoppingMode.SHOPPING_RESULT:
                        break;
                    case ShoppingMode.SHOPPING_TABLE:
                        break;
                    case ShoppingMode.SHOPPING_DELETE:
                        break;
                    case ShoppingMode.GO_BACK:
                        isShoppingModeRunning = false;
                        break;
                }
            }

            Console.SetCursorPosition(0, 0);
            Console.Write("go back from shopping");
        }
    }
}
