using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberModel
    {
        private static MemberModel instance;

        private MemberModel() { }

        public static MemberModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MemberModel();
            }
            return instance;
        }

    }
}
