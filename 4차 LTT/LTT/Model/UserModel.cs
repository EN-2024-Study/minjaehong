using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class UserModel
    {
        Dictionary<string, UserDTO> userDB; 

        private static UserModel instance;

        private LectureModel lectureModel;

        private UserModel()
        {
            // ID : PW
            userDB = new Dictionary<string, string>();
            // ID : SHOPPINGLIST
            shoppingBasketDB = new Dictionary<string, List<LectureDTO>>();
            // ID : REGISTERLIST
            registrationDB = new Dictionary<string, List<LectureDTO>>();

            lectureModel = LectureModel.GetInstance();

            // dummydata
            userDB.Add("20011738", "12345");
            shoppingBasketDB.Add("20011738", new List<LectureDTO>()); // 관심과목 담을 리스트 생성
            registrationDB.Add("20011738", new List<LectureDTO>()); // 수강신청과목 담을 리스트 생성

            userDB.Add("211929", "10000");
            shoppingBasketDB.Add("211929", new List<LectureDTO>());
            registrationDB.Add("211929", new List<LectureDTO>());
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

        // 해당 USER의 장바구니 목록 보내주기
        public List<LectureDTO> GetUserShoppingList(string userID) { 
            return shoppingBasketDB[userID];
        }

        // 해당 USER의 수강신청 목록 보내주기
        public List<LectureDTO> GetUserRegistrationList(string userID)
        {
            return registrationDB[userID];
        }

        // 특정 USER의 
        public void AddToUserShoppingBasket(string userID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            shoppingBasketDB[userID].Add(lecture);
        }

        public void RemoveFromUserShoppingBasket(string userID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            shoppingBasketDB[userID].Remove(lecture);
        }

        public void AddToUserRegistration(string userID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            registrationDB[userID].Add(lecture);
        }

        public void RemoveFromUserRegistration(string userID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            registrationDB[userID].Remove(lecture);
        }
    }
}
