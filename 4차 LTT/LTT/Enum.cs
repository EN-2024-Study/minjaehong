using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    public enum MainMode { SEARCH_MODE, SHOPPING_MODE, REGST_MODE, REGST_RESULT_MODE, LOGOUT=-1 }

    public enum ShoppingMode { SHOPPING, SHOPPING_RESULT, SHOPPING_TABLE, SHOPPING_DELETE, GO_BACK=-1 }

    public enum RegistrationMode { REGST, REGST_RESULT, REGST_TABLE, REGST_DELETE, GO_BACK=-1 }
}