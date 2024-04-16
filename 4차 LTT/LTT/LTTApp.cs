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
        public static void InitializeDB()
        {
            LectureModel.GetInstance();
        }

        public static void Main()
        {

            InitializeDB();

            Console.CursorVisible = false;

            LoginController loginController = new LoginController();
            loginController.Run();
        }
    }
}
