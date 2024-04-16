using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class MemberRepository
    {
        Dictionary<string, MemberDTO> userDB; // ID마다 MemberDTO 저장
        
        private static MemberRepository instance;

        private LectureRepository lectureModel;

        private MemberRepository()
        {
            // ID : PW
            userDB = new Dictionary<string, MemberDTO>();
            userDB.Add("20011738", new MemberDTO("20011738","12345"));
            userDB.Add("211929", new MemberDTO("211929", "10000"));
        }

        public static MemberRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new MemberRepository();
            }
            return instance;
        }

        //============== SINGLETON ==============//

        public bool CheckIfValidLogin(List<string> loginInfo)
        {
            string ID = loginInfo[0];
            string PW = loginInfo[1];

            if(userDB.ContainsKey(ID) && userDB[ID].GetPW() == PW) return true;
            else return false;
        }

        // 해당 USER의 장바구니 목록 보내주기
        public List<LectureDTO> GetUserShoppingList(string curUserID) {
            return userDB[curUserID].GetShoppingBasket();
        }

        // 해당 USER의 수강신청 목록 보내주기
        public List<LectureDTO> GetUserRegistrationList(string curUserID)
        {
            return userDB[curUserID].GetRegistration();
        }

        // 특정 USER의 장바구니에 추가하기
        public void AddToUserShoppingBasket(string curUserID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            userDB[curUserID].GetShoppingBasket().Add(lecture);
        }

        public void RemoveFromUserShoppingBasket(string curUserID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            userDB[curUserID].GetShoppingBasket().Remove(lecture);
        }

        public void AddToUserRegistration(string curUserID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            userDB[curUserID].GetRegistration().Add(lecture);
        }

        public void RemoveFromUserRegistration(string curUserID, string lectureID)
        {
            LectureDTO lecture = lectureModel.GetCeratainLecture(lectureID);
            userDB[curUserID].GetRegistration().Remove(lecture);
        }
    }
}
