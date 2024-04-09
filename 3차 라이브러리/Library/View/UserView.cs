using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserView
    {
        private static UserView instance;

        private UserView() { }

        public static UserView GetInstance()
        {
            if (instance == null)
            {
                instance = new UserView();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

    }
}
