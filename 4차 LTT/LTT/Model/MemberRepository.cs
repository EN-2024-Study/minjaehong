using System;
using System.Collections.Generic;

namespace LTT
{
    class MemberRepository
    {
        Dictionary<string, MemberDTO> userDB; // ID마다 MemberDTO 저장
        
        private static MemberRepository instance;

        private LectureRepository lectureRepository;

        private MemberRepository()
        {
            lectureRepository = LectureRepository.GetInstance();

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
        public List<LectureDTO> GetUserShoppingBasket(string curUserID) {
            return userDB[curUserID].GetShoppingBasket();
        }

        // 해당 USER의 수강신청 목록 보내주기
        public List<LectureDTO> GetUserRegistrationList(string curUserID)
        {
            return userDB[curUserID].GetRegistration();
        }

        // 특정 USER의 장바구니에 추가하기
        public bool AddToUserShoppingBasket(string curUserID, string lectureID)
        {
            LectureDTO lecture = lectureRepository.GetCertainLecture(lectureID);
            List<LectureDTO> curUserShoppingBasket = GetUserShoppingBasket(curUserID);

            if (curUserShoppingBasket.Contains(lecture))
            {
                return false;
            }
            else
            {
                curUserShoppingBasket.Add(lecture);
                return true;
            }
        }

        // 특정 USER의 장바구니에서 제거하기
        public bool RemoveFromUserShoppingBasket(string curUserID, string lectureID)
        {
            LectureDTO lecture = lectureRepository.GetCertainLecture(lectureID);
            List<LectureDTO> curUserShoppingBasket = GetUserShoppingBasket(curUserID);

            if (curUserShoppingBasket.Contains(lecture))
            {
                curUserShoppingBasket.Remove(lecture);
                return true;
            }
            else
            {
                return false;
            }
        }

        // 특정 USER의 수강신청 내역에 추가하기
        public bool AddToUserRegistration(string curUserID, string lectureID)
        {
            LectureDTO lecture = lectureRepository.GetCertainLecture(lectureID);
            List<LectureDTO> curUserRegistration = GetUserRegistrationList(curUserID);

            if (curUserRegistration.Contains(lecture)) return false;
            else
            {
                curUserRegistration.Add(lecture);
                return true;
            }
        }

        // 특정 USER의 수강신청 내역에서 삭제하기
        public bool RemoveFromUserRegistration(string curUserID, string lectureID)
        {
            LectureDTO lecture = lectureRepository.GetCertainLecture(lectureID);
            List<LectureDTO> curUserRegistration = GetUserRegistrationList(curUserID);
            if (curUserRegistration.Contains(lecture))
            {
                curUserRegistration.Remove(lecture);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
