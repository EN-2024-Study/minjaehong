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

        // ID PW 맞는지 확인해줌
        public bool CheckIfValidLogin(List<string> loginInfo)
        {
            string ID = loginInfo[0];
            string PW = loginInfo[1];

            if(userDB.ContainsKey(ID) && userDB[ID].GetPW() == PW) return true;
            else return false;
        }

        // USER가 특정 lecture의 courseNumber와 같은 lecture를 이미 가지고 있는지 확인해줌
        public bool CheckIfUserHaveSameCourseNumberLecture(List<LectureDTO> lectureList, LectureDTO lecture)
        {
            foreach(LectureDTO curLecture in lectureList)
            {
                if (curLecture.GetCourseNumber() == lecture.GetCourseNumber())
                {
                    return true;
                }
            }
            return false;
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

        // 특정 USER의 장바구니에 추가하기
        public bool AddToUserShoppingBasket(string curUserID, string lectureID)
        {
            LectureDTO lecture = lectureRepository.GetCertainLecture(lectureID);
            List<LectureDTO> curUserShoppingBasket = GetUserShoppingBasket(curUserID);

            int calculatedCredit = userDB[curUserID].GetCurrentShoppingCredit() + int.Parse(lecture.GetCredit());

            // 장바구니에 동일한 학수번호로 이미 담겨있거나 or 최대수강학점을 넘길때
            if (CheckIfUserHaveSameCourseNumberLecture(curUserShoppingBasket,lecture) || calculatedCredit > userDB[curUserID].GetMaximumCredit())
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

            // 장바구니에 담겨있지 않을때 예외처리
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

            int calculatedCredit = userDB[curUserID].GetCurrentRegistrationCredit() + int.Parse(lecture.GetCredit());

            // 이미 동일한 학수번호로 수강신청되어있거나 or 최대수강학점을 넘으면
            if (CheckIfUserHaveSameCourseNumberLecture(curUserRegistration,lecture) || calculatedCredit > userDB[curUserID].GetMaximumCredit())
            {
                return false;
            }
            else
            {
                // 수강신청으로 추가했으면
                curUserRegistration.Add(lecture);
                // 장바구니에서는 빼주기
                RemoveFromUserShoppingBasket(curUserID, lectureID);
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
                // 수강신청내역에 없을때 예외처리
                return false;
            }
        }
    }
}
