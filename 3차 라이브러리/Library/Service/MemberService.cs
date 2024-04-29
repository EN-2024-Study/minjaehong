using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // controller에서 호출하면
    // 조건 확인 후 repository 호출해서 CRUD 작업 실행
    class MemberService
    {
        private MemberRepository memberRepository;
        private HistoryRepository historyRepository;

        public MemberService()
        {
            this.memberRepository = MemberRepository.GetInstance();
            this.historyRepository = HistoryRepository.GetInstance();

            List<string> defaultUser1 = new List<string> { "dog", "dog123", "DOG", "5", "010-1234-5678" };
            List<string> defaultUser2 = new List<string> { "cat", "cat123", "CAT", "4", "010-5647-8497" };
            List<string> defaultUser3 = new List<string> { "dolphin", "dolphin123", "DOLPHIN", "7", "010-3781-3649" };
            AddNewMember(new MemberDTO(defaultUser1));
            AddNewMember(new MemberDTO(defaultUser2));
            AddNewMember(new MemberDTO(defaultUser3));
        }

        //============= CHECK FUNCTIONS ==============//

        public bool CheckIfMemberExists(string userID)
        {
            return memberRepository.CheckIfMemberExists(userID);
        }

        public bool CheckIfValidLogin(List<string> loginInfo)
        {
            return memberRepository.CheckIfValidLogin(loginInfo);
        }

        // USER가 진짜 빌린 책인지 확인
        public bool CheckIfUserBorrowed(string curUserID, MiniDTO miniDTO)
        {
            string bookID = miniDTO.GetBookID();
            if (historyRepository.CheckIfUserBorrowed(curUserID, bookID)) return true;
            else return false;
        }

        //============= GET FUNCTIONS ==============//

        public MemberDTO GetMemberByID(string memberID)
        {
            return memberRepository.GetMember(memberID);
        }

        public List<MemberDTO> GetAllMember()
        {
            return memberRepository.GetAllMember();
        }

        // 로그인한 USER가 BORROW한 BOOK들에 대한 정보 반환 -> BOOKID LIST로
        public List<int> GetMemberBorrowedBooks(string curUserID)
        {
            return historyRepository.GetMemberBorrowedBooks(curUserID);
        }

        // 로그인한 USER가 RETURN한 BOOK들에 대한 정보 반환 -> BOOKID LIST로
        public List<int> GetMemberReturnedBooks(string curUserID)
        {
            return historyRepository.GetMemberReturnedBooks(curUserID);
        }

        //============== REPOSITORY CRUD FUNCTIONS ==============//

        public bool AddNewMember(MemberDTO newMember)
        {
            string newMemberID = newMember.GetId();

            // 이미 존재하면 취소
            if (CheckIfMemberExists(newMemberID)) return false;

            memberRepository.Add(newMember);
            return true;
        }

        public bool DeleteMember(string deletingMemberID)
        {
            // 존재하지 않는 ID이면 취소
            if (!CheckIfMemberExists(deletingMemberID)) return false;

            // 1. memberDB에서 삭제하고
            memberRepository.Delete(deletingMemberID);
            // 2. 해당 member의 historyDB 내역도 모두 삭제
            historyRepository.DeleteMemberHistory(deletingMemberID);

            return true;
        }

        public bool UpdateMember(string updatingMemberID, MemberDTO updatingMember)
        {
            // 만약 Member가 존재하면
            if (CheckIfMemberExists(updatingMemberID))
            {
                MemberDTO originalMember = GetMemberByID(updatingMemberID);

                if (updatingMember.GetPw() == "") updatingMember.SetPw(originalMember.GetPw());
                if (updatingMember.GetName() == "") updatingMember.SetName(originalMember.GetName());
                if (updatingMember.GetAge() == "") updatingMember.SetAge(originalMember.GetAge());
                if (updatingMember.GetPhoneNum() == "") updatingMember.SetPhoneNum(originalMember.GetPhoneNum());

                // 조건확인 후 CRUD
                memberRepository.Update(updatingMemberID, updatingMember);
                return true;
            }
            return false;
        }

        // memberRepository가 아닌 HistoryRepository에서 처리해주면 됨
        // memberRepository는 할게 없음. 그냥 HisoryRepository 함수들 호출해주면 됨
        // historyDB에 (borrwer_id, book_id, returned) 로 저장만 해주면 그게 borrow 한 상황 그 자체임
        // 여기서 MemberRepository 호출 절대 하지말기
        // 그래도 Service니까 조건은 따지고 호출해줘야함
        // 그니까 조건만 따지고 memberRepository는 절대 호출하지말고 historyRepository로 모든 상황 해결하기
        public bool UpdateBorrow(string curUserID, MiniDTO miniDTO)
        {
            string bookID = miniDTO.GetBookID();
            
            bool isBorrowed = historyRepository.CheckIfUserBorrowed(curUserID, bookID);

            // 이미 빌린 책이면 대여불가
            if (isBorrowed) return false;
            
            // 아니면 진짜로 대여해주기
            historyRepository.AddBorrowHistory(curUserID, bookID);
            return true;
        }

        // memberRepository가 아닌 HistoryRepository에서 처리해주면 됨
        // memberRepository는 할게 없음. 그냥 HisoryRepository 함수들 호출해주면 됨
        // historyDB에 (borrwer_id, book_id, returned) 로 저장만 해주면 그게 borrow 한 상황 그 자체임
        // 여기서 MemberRepository 호출 절대 하지말기
        // 그래도 Service니까 조건은 따지고 호출해줘야함
        // 그니까 조건만 따지고 memberRepository는 절대 호출하지말고 historyRepository로 모든 상황 해결하기
        public bool UpdateReturned(string curUserID, MiniDTO miniDTO)
        {
            string bookID = miniDTO.GetBookID();
            
            bool isBorrowed = historyRepository.CheckIfUserBorrowed(curUserID, bookID);

            // 빌린 책이 아니면 반납 불가
            if (!isBorrowed) return false;

            // 아니면 진짜로 반납해주기
            historyRepository.AddReturnHistory(curUserID, bookID);
            return true;
        }
    }
}
