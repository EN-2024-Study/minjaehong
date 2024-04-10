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
        void AddNewMember(MemberDTO newMember)
        {
            // <ID, MemberDTO> 형식으로 DB에 저장
            memberDB.Add(newMember.GetId(), newMember);
        }

        // controller에서 ID넘기면 삭제해줌
        void DeleteMember(string deletingMemberID)
        {
            memberDB.Remove(deletingMemberID);
        }

        // 무조건 존재하는 member에 대한 ID 값만 들어옴
        // 내 정보 수정할때 띄우려고
        MemberDTO GetMember(string requestedMemberID)
        {
            return memberDB[requestedMemberID];
        }

        // controller에서 MemberDTO로 넘어오면
        // 다른 값들 찾아서 업뎃해줌
        bool UpdateMember(MemberDTO updatingMember)
        {
            string updatingMemberID = updatingMember.GetId();

            if (updatingMember.GetName() != "") memberDB[updatingMemberID].SetName(updatingMember.GetName());
            if (updatingMember.GetAge() != "") memberDB[updatingMemberID].SetAge(updatingMember.GetAge());
            if (updatingMember.GetPhoneNum() != "") memberDB[updatingMemberID].SetPhoneNum(updatingMember.GetPhoneNum());
            
            return true;
        }

        //=================== BORROW AND RETURN ===================//

        public void UpdateBorrow(string curUserID, MiniDTO miniDTO)
        {
            // string으로 받아왔으니 int로 형변환 시키기 -> bookID는 int니까
            int bookID = int.Parse(miniDTO.GetBookID());

            memberDB[curUserID].GetBorrowedBookList().Add()
            // 남은 수량 계산
            int curNum = int.Parse(bookDB[bookID].GetQuantity());
            int borrowNum = int.Parse(miniDTO.GetBookNum());
            int updatedBookNum = curNum - borrowNum;

            // bookDB에 적용하기
            bookDB[bookID].SetQuantity(updatedBookNum.ToString());
        }

        public void UpdateReturn()
        {

        }
    }
}
