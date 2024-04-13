﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // MODE AND MENU STATE
    public enum LibraryMode { USER_MODE = 0, MANAGER_MODE = 1 };

    public enum UserFrontMode { GOBACK = -1, USER_LOGIN = 0, USER_CREATE_ACCOUNT = 1 };

    public enum ManagerMenuState { GOBACK = -1, PRINTALLBOOK = 0, FINDBOOK = 1, ADDBOOK = 2, DELETEBOOK = 3, UPDATEBOOK = 4, 
                                    MEMBERMANAGEMENT=5, BORROWLIST=6, NAVERSEARCH=7, LOGMANAGEMENT=8, REQUESTEDBOOK=9};
    public enum UserMenuState { GOBACK = -1, PRINTALLBOOK = 0, FIND = 1, BORROW = 2, CHECKBORROW = 3, RETURN = 4, CHECKRETURN = 5, UPDATEINFO = 6,
                                    DELETEMYSELF = 7, NAVERSEARCH = 8, REQUESTED = 9};

    public enum ExceptionState {
                                // FORMAT EXCEPTION
                                INT_ONLY, ENGLISH_ONLY, ENGLISH_INT_ONLY, PHONENUM_ONLY, ISBN_ONLY, DATE_ONLY,
                                    
                                // RUNTIME EXCEPTION
                                EXISTING_ID, NONEXISTING_ID, DONT_HAVE_CERTAIN_BOOK, NOT_ENOUGH_QUANTITY
            
                                };
}