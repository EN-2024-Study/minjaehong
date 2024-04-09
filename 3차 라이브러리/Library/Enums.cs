using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum LibraryMode { USER_MODE = 0, MANAGER_MODE = 1 };
    public enum ManagerMenuState { FINDBOOK = 0, ADDBOOK = 1, DELETEBOOK = 2, UPDATEBOOK = 3, 
                                    MEMBERMANAGEMENT=4, BORROWLIST=5, NAVERSEARCH=6, LOGMANAGEMENT=7, REQUESTEDBOOK=8};
    
}