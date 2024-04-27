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
        private static MemberService instance;

        private MemberRepository memberRepository;

        public MemberService()
        {
            this.memberRepository = MemberRepository.GetInstance();
            List<string> defaultUser1 = new List<string> { "dog", "dog123", "DOG", "5", "010-1234-5678" };
            List<string> defaultUser2 = new List<string> { "cat", "cat123", "CAT", "4", "010-5647-8497" };
            List<string> defaultUser3 = new List<string> { "dolphin", "dolphin123", "DOLPHIN", "7", "010-3781-3649" };
            AddNewMember(new MemberDTO(defaultUser1));
            AddNewMember(new MemberDTO(defaultUser2));
            AddNewMember(new MemberDTO(defaultUser3));
        }

        public static MemberService GetInstance()
        {
            if (instance == null)
            {
                instance = new MemberService();
            }
            return instance;
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
            int tryReturningBookID = int.Parse(miniDTO.GetBookID());
            int tryReturningQuantity = int.Parse(miniDTO.GetQuantity());

            // 해당 책을 진짜로 빌렸고 + 반납 개수가 빌린 개수보다 작으면
            if (GetMemberBorrowedBooks(curUserID).Contains(tryReturningBookID) &&
                (tryReturningQuantity <= GetMemberByID(curUserID).GetBorrowedBooks()[tryReturningBookID]))
            {
                // 그럼 진짜로 반납할 수 있는거임
                return true;
            }
            // 아니면 반납 못함
            return false;
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
            return GetMemberByID(curUserID).GetBorrowedBooks().Keys.ToList();
        }

        // 로그인한 USER가 RETURN한 BOOK들에 대한 정보 반환 -> BOOKID LIST로
        public List<int> GetMemberReturnedBooks(string curUserID)
        {
            return GetMemberByID(curUserID).GetReturnedBooks().Keys.ToList();
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

            memberRepository.Delete(deletingMemberID);
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

        public void UpdateBorrowed(string curUserID, MiniDTO miniDTO)
        {
            int bookID = int.Parse(miniDTO.GetBookID());
            int borrowNum = int.Parse(miniDTO.GetQuantity()); // 항상 borrowNum = 1임

            Dictionary<int, int> curUserBorrowedBooks = GetMemberByID(curUserID).GetBorrowedBooks();

            // 이미 빌린 책이면 개수만 추가
            if (curUserBorrowedBooks.ContainsKey(bookID)) curUserBorrowedBooks[bookID] += borrowNum;
            // 아니면 새로 추가
            else curUserBorrowedBooks.Add(bookID, borrowNum);
        }

        public bool UpdateReturned(string curUserID, MiniDTO miniDTO)
        {
            int bookID = int.Parse(miniDTO.GetBookID());
            int borrowNum = int.Parse(miniDTO.GetQuantity()); // 항상 borrowNum = 1임

            Dictionary<int, int> curUserBorrowedBooks = GetMemberByID(curUserID).GetBorrowedBooks();
            Dictionary<int, int> curUserReturnedBooks = GetMemberByID(curUserID).GetReturnedBooks();

            // 만약 반납할 수 있으면
            if (curUserBorrowedBooks.ContainsKey(bookID))
            {
                // 반납 list 에 이미 해당 책이 있으면
                if (curUserReturnedBooks.ContainsKey(bookID)) curUserReturnedBooks[bookID] += borrowNum;
                else curUserReturnedBooks.Add(bookID, borrowNum);

                // 기존꺼에서 빼주기
                curUserBorrowedBooks[bookID] -= borrowNum;

                // 만약 해당 책이 이제 0개면 대여 내역에서 영구 삭제
                if (curUserBorrowedBooks[bookID] == 0) curUserBorrowedBooks.Remove(bookID);
                return true;
            }
            return false;
        }
    }
}
