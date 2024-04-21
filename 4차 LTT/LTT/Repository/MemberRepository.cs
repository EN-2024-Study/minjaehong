using System;
using System.Collections.Generic;

namespace LTT
{
    class MemberRepository
    {
        private static MemberRepository instance;

        Dictionary<string, MemberDTO> userDB; // ID마다 MemberDTO 저장

        private LectureRepository lectureRepository;

        private MemberRepository()
        {
            lectureRepository = LectureRepository.GetInstance();

            // ID : PW
            userDB = new Dictionary<string, MemberDTO>();
            PutDummyData();
        }

        private void PutDummyData()
        {
            userDB.Add("20011738", new MemberDTO("20011738", "12345"));
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

        // ID-PW 맞는지 확인
        public bool CheckIDandPW(string curUserID, string curUserPW)
        {
            if (userDB.ContainsKey(curUserID) && userDB[curUserID].GetPW() == curUserPW) return true;
            else return false;
        }

        // 해당 USER의 userDTO 보내주기
        public MemberDTO GetUserInfo(string curUserID)
        {
            return userDB[curUserID];
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

        public int GetUserShoppingCredit(string curUserID)
        {
            return userDB[curUserID].GetCurrentShoppingCredit();
        }

        public int GetUserRegistrationCredit(string curUserID)
        {
            return userDB[curUserID].GetCurrentRegistrationCredit();
        }

        public int GetUserMaximumCredit(string curUserID)
        {
            return userDB[curUserID].GetMaximumCredit();
        }
    }
}
