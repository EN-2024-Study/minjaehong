using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // 여기서 조건 따지지말기
    // 조건은 모두 Service에서 따지고 온다
    // 여기에 조건이 있으면 안됨
    class MemberRepository
    {
        // ID가 고유키
        Dictionary<string, MemberDTO> memberDB;

        //================== SINGLETON ===============//

        private static MemberRepository instance;

        private MemberRepository() {
            memberDB = new Dictionary<string, MemberDTO>();
        }

        public static MemberRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new MemberRepository();
            }
            return instance;
        }

        //===================== SIMPLE FUNCTIONS ========================//

        // 무조건 존재하는 member에 대한 ID 값만 들어옴
        // 내 정보 수정할때 띄우려고
        public MemberDTO GetMember(string requestedMemberID)
        {
            return memberDB[requestedMemberID];
        }

        public List<MemberDTO> GetAllMember()
        {
            return memberDB.Values.ToList();
        }

        // 로그인한 USER가 BORROW한 BOOK들에 대한 정보 반환 -> BOOKID LIST로
        public List<int> GetMemberBorrowedBooks(string curUserID)
        {
            return memberDB[curUserID].GetBorrowedBooks().Keys.ToList();
        }

        // 로그인한 USER가 RETURN한 BOOK들에 대한 정보 반환 -> BOOKID LIST로
        public List<int> GetMemberReturnedBooks(string curUserID)
        {
            return memberDB[curUserID].GetReturnedBooks().Keys.ToList();
        }

        // 특정 ID 존재유무 파악
        public bool CheckIfMemberExists(string userID)
        {
            foreach (string curKey in memberDB.Keys)
            {
                if (curKey == userID) return true;
            }
            return false;
        }

        // ID PW 유효성 검사
        public bool CheckIfValidLogin(List<string> loginInfo)
        {
            string userID = loginInfo[0];
            string userPW = loginInfo[1];

            foreach (string curKey in memberDB.Keys)
            {
                // ID PW 모두 DB에 있는거랑 동일하면 true return
                if (userID == curKey && userPW == memberDB[curKey].GetPw()) return true;
            }
            return false;

        }

        // USER가 진짜 빌린 책인지 확인
        // 이거 service 꺼인지 repository꺼인지 모르겠음
        public bool CheckIfUserBorrowed(string curUserID, MiniDTO miniDTO)
        {
            int tryReturningBookID = int.Parse(miniDTO.GetBookID());
            int tryReturningQuantity = int.Parse(miniDTO.GetQuantity());

            MemberDTO curUserDTO = memberDB[curUserID];
            // 해당 책을 진짜로 빌렸고
            if (curUserDTO.GetBorrowedBooks().ContainsKey(tryReturningBookID))
            {
                // 반납 개수가 빌린 개수보다 작으면
                if (tryReturningQuantity <= curUserDTO.GetBorrowedBooks()[tryReturningBookID])
                {
                    // 그럼 진짜로 반납할 수 있는거임
                    return true;
                }
            }
            // 아니면 반납 못함
            return false;
        }

        //===================== CRUD ========================//

        public bool Add(MemberDTO newMember)
        {
            memberDB.Add(newMember.GetId(), newMember);
            return true;
        }

        // controller에서 ID넘기면 삭제해줌
        public bool Delete(string deletingMemberID)
        {
            memberDB.Remove(deletingMemberID);
            return true;
        }

        // 기존 member 삭제 후 추가
        public bool Update(string updatingMemberID, MemberDTO updatingMember)
        {
            Delete(updatingMemberID);
            Add(updatingMember);
            return true;
        }
    }
}