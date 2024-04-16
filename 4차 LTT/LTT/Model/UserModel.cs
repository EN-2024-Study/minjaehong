using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class UserModel
    {
        Dictionary<string, string> userDB;
        Dictionary<string, List<LectureDTO>> shoppingDB;

        public UserModel()
        {
            // ID : PW
            userDB = new Dictionary<string, string>();
            // ID : SHOPPINGLIST
            shoppingDB = new Dictionary<string, List<LectureDTO>>();

            userDB.Add("20011738", "12345");
            shoppingDB.Add("20011738", new List<LectureDTO>());
        }

        public bool CheckIfValidLogin(List<string> loginInfo)
        {
            string id = loginInfo[0];
            string pw = loginInfo[1];

            if(userDB.ContainsKey(id) && userDB[id] == pw) return true;
            else return false;
        }

        public List<LectureDTO> GetUserShoppingList(string userID)
        {
            // 해당 USER의 장바구니 목록 보내주기
            return shoppingDB[userID];
        }
    }
}
