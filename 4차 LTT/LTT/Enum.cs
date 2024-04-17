using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    public enum MainMode { SEARCH_MODE, SHOPPING_MODE, REGISTER_MODE, REGISTER_RESULT_MODE, LOGOUT=-1 }

    public enum ShoppingMode { SHOPPING, SHOPPING_RESULT, SHOPPING_TABLE, SHOPPING_DELETE, GO_BACK=-1 }

    public enum RegistrationMode { REGISTER, REGISTER_RESULT, REGISTER_TABLE, REGISTER_DELETE, GO_BACK=-1 }
}