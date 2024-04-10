using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberDTO
    {
        private string id;
        private string pw;
        private string name;
        private string age;
        private string phoneNum;

        // <bookID,num>
        private Dictionary<int,int> borrowed; // 빌린 책들 담기
        private Dictionary<int,int> returned; // 반납한 책들 담기

        //=================== CONSTRUCTOR =================//

        // view에서 넘겨준 List<string> 가지고
        // MemberDTO 만들 수 있어야함
        // borrowed랑 returned는 생성만 해주기
        public MemberDTO(List<string> dataFromView)
        {
            this.id = dataFromView[0];
            this.pw = dataFromView[1];
            this.name = dataFromView[2];
            this.age = dataFromView[3];
            this.phoneNum = dataFromView[4];

            borrowed = new Dictionary<int,int>();
            returned = new Dictionary<int,int>();
        }

        //=================== GETTER =====================//

        public string GetId() { return id; }
        public string GetPw() { return pw; }
        public string GetName() { return name; }
        public string GetAge() { return age; }
        public string GetPhoneNum() { return phoneNum; }

        // Member가 borrow 한 Book들에 대한 Dictionary 참조 return
        public Dictionary<int, int> GetBorrowedBooks() { return borrowed; }
        // Member가 return한 Book들에 대한 Dictionary 참조 return
        public Dictionary<int,int> GetReturnedBooks() { return returned; }

        //=================== SETTER =====================//

        public void SetId(string id) { this.id = id; }
        public void SetName(string name) { this.name = name; }
        public void SetAge(string age) { this.age = age; }
        public void SetPhoneNum(string phoneNum) { this.phoneNum = phoneNum; }
    }
}
