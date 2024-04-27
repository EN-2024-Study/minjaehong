using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LibraryApp
    {
        public static string managerID = "manager";
        public static string managerPW = "manager123";

        public static void Main()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(110, 50);

            // MainController.GetInstance() 호출하면서
            // 모든 싱글톤 객체가 생성되고 참조관계가 다 세팅되고 시작함
            MainController mainController = new MainController();
            mainController.StartApp();
        }
    }
}
