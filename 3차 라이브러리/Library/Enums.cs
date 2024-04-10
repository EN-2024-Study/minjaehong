using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum LibraryMode { USER_MODE = 0, MANAGER_MODE = 1 };

    public enum UserFrontMode { USER_LOGIN = 0, USER_CREATE_ACCOUNT = 1 };

    public enum ManagerMenuState { FINDBOOK = 0, ADDBOOK = 1, DELETEBOOK = 2, UPDATEBOOK = 3, PRINTALLBOOK = 4, 
                                    MEMBERMANAGEMENT=5, BORROWLIST=6, NAVERSEARCH=7, LOGMANAGEMENT=8, REQUESTEDBOOK=9};
    public enum UserMenuState { FIND = 0, BORROW = 1, CHECKBORROW = 2, RETURN = 3, CHECKRETURN = 4, UPDATEINFO = 5,
                                    DELETEMYSELF = 6, NAVERSEARCH = 7, REQUESTED = 8};
}