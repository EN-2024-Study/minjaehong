using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace LTT
{
    class LTTApp
    {
        public static void Main()
        {
            Console.SetWindowPosition(0,0);
            Console.SetWindowSize(213, 62);
            Console.CursorVisible = false;

            LoginController loginController = new LoginController();
            loginController.Run();
        }
    }
}
