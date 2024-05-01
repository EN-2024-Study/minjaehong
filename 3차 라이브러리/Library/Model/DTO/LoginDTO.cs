using System.Collections.Generic;

namespace Library
{
    class LoginDTO
    {
        private string id;
        private string pw;

        public LoginDTO(List<string> loginInfo)
        {
            id = loginInfo[0];
            pw = loginInfo[1];
        }

        public string GetID() { return id; }
        public string GetPW() { return pw; }
    }
}
