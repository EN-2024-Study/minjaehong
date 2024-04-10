using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class LibraryApp
    {
        static string curUserID;

        public static void Main()
        {
            Console.CursorVisible = false;

            // MainController.GetInstance() 호출하면 singleton 생성자들 다 호출되면서
            // 모든 객체와 참조관계가 다 세팅되고 시작함
            MainController mainController = MainController.GetInstance();
            mainController.run();
        }
    }
}
