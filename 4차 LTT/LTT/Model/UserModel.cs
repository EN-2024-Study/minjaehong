using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class UserModel
    {
        Dictionary<string, string> userDB; // ID마다 PW 저장
        Dictionary<string, List<LectureDTO>> shoppingDB; // ID마다 관심과목 저장
        Dictionary<string, List<LectureDTO>> registerDB; // ID마다 수강신청과목 저장

        private static UserModel instance;

        private LectureModel lectureModel;

        private UserModel()
        {
            // ID : PW
            userDB = new Dictionary<string, string>();
            // ID : SHOPPINGLIST
            shoppingDB = new Dictionary<string, List<LectureDTO>>();
            // ID : REGISTERLIST
            registerDB = new Dictionary<string, List<LectureDTO>>();

            lectureModel = LectureModel.GetInstance();

            userDB.Add("20011738", "12345");
            shoppingDB.Add("20011738", new List<LectureDTO>()); // 관심과목 담을 리스트 생성
            registerDB.Add("20011738", new List<LectureDTO>()); // 수강신청과목 담을 리스트 생성
        }

        public static UserModel GetInstance()
        {
            if (instance == null)
            {
                instance = new UserModel();
            }
            return instance;
        }

        //============== SINGLETON ==============//

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

        public List<LectureDTO> GetUserRegistrationList(string userID)
        {
            // 해당 USER의 수강신청 목록 보내주기
            return registerDB[userID];
        }

        public void AddToUserShoppingList(string userID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            shoppingDB[userID].Add(lecture);
        }

        public void DeleteUserShoppingList(string userID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            shoppingDB[userID].Remove(lecture);
        }
    }
}
