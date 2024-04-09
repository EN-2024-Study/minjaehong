using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserController
    {
        private static UserController instance;

        private UserController() { }

        public static UserController GetInstance()
        {
            if (instance == null)
            {
                instance = new UserController();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        public void run()
        {

        }
    }
}
