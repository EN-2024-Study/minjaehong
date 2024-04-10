using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberModel
    {
        private static MemberModel instance;

        private MemberModel() {
            memberDB = new Dictionary<string, MemberDTO>();
        }

        public static MemberModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MemberModel();
            }
            return instance;
        }

        //================== SINGLETON ===============

        // MemberDB
        // ID가 고유키임
        Dictionary<string, MemberDTO> memberDB;

        // controller에서 MemberDTO 형식으로 보내주면
        // 그냥 그대로 저장만 해주면 됨
        public void AddNewMember(MemberDTO newMember)
        {
            // <ID, MemberDTO> 형식으로 DB에 저장
            memberDB.Add(newMember.GetId(), newMember);
        }

        // controller에서 ID넘기면 삭제해줌
        public void DeleteMember(string deletingMemberID)
        {
            memberDB.Remove(deletingMemberID);
        }

        // controller에서 MemberDTO로 넘어오면
        // 다른 값들 찾아서 업뎃해줌
        public bool UpdateMember(MemberDTO updatingMember)
        {
            string updatingMemberID = updatingMember.GetId();

            if (updatingMember.GetName() != "") memberDB[updatingMemberID].SetName(updatingMember.GetName());
            if (updatingMember.GetAge() != "") memberDB[updatingMemberID].SetAge(updatingMember.GetAge());
            if (updatingMember.GetPhoneNum() != "") memberDB[updatingMemberID].SetPhoneNum(updatingMember.GetPhoneNum());

            return true;
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

        //====================== MEMBER INFO  =====================//

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

        //=================== BORROW AND RETURN ===================//

        public void UpdateBorrowed(string curUserID, MiniDTO miniDTO)
        {
            // string으로 받아왔으니 int로 형변환 시키기 -> bookID는 int니까
            int bookID = int.Parse(miniDTO.GetBookID());
            // 빌린 책 수 int로 형변환 해주기
            int borrowNum = int.Parse(miniDTO.GetBookNum());

            Dictionary<int, int> curUserBorrowedBooks = memberDB[curUserID].GetBorrowedBooks();

            // 만약 이미 빌린 책이면
            if (curUserBorrowedBooks.ContainsKey(bookID))
            {
                // 기존꺼에 더해주기만 하면 됨
                curUserBorrowedBooks[bookID] += borrowNum;
            }
            // 새로 빌리는거면
            else
            {
                // 새로 저장해주기
                curUserBorrowedBooks.Add(bookID, borrowNum);
            }
        }

        // 추후에 예외처리 위해 bool 반환
        public bool UpdateReturned(string curUserID, MiniDTO miniDTO)
        {
            // string으로 받아왔으니 int로 형변환 시키기 -> bookID는 int니까
            int bookID = int.Parse(miniDTO.GetBookID());
            // 빌린 책 수 int로 형변환 해주기
            int borrowNum = int.Parse(miniDTO.GetBookNum());

            Dictionary<int, int> curUserBorrowedBooks = memberDB[curUserID].GetBorrowedBooks();
            Dictionary<int, int> curUserReturnedBooks = memberDB[curUserID].GetReturnedBooks();

            // 만약 반납할 수 있으면
            if (curUserBorrowedBooks.ContainsKey(bookID))
            {
                // 반납 내역에 이미 해당 책이 있으면
                if (curUserReturnedBooks.ContainsKey(bookID))
                {
                    curUserReturnedBooks[bookID] += borrowNum;
                }
                else
                {
                    curUserReturnedBooks.Add(bookID, borrowNum);
                }
                
                // 기존꺼에서 빼주기
                curUserBorrowedBooks[bookID] -= borrowNum;

                // 만약 해당 책이 이제 0개면 대여 내역에서 영구 삭제
                if (curUserBorrowedBooks[bookID] == 0) curUserBorrowedBooks.Remove(bookID);
                return true;
            }
            return false;
        }

        //======================== LOGIN ========================//

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
    }
}