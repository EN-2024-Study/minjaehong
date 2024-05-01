using System.Collections.Generic;

namespace Library
{
    class LoginDTO
    {
        private string id;
        private string pw;

        public LoginDTO(List<string> loginInfo)
        {
            this.id = loginInfo[0];
            this.pw = loginInfo[1];
        }

        public string GetID() { return id; }
        public string GetPW() { return pw; }
    }
}
