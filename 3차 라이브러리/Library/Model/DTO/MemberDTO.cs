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

        //=================== CONSTRUCTOR =================//

        public MemberDTO() { }

        public MemberDTO(List<string> dataFromView)
        {
            this.id = dataFromView[0];
            this.pw = dataFromView[1];
            this.name = dataFromView[2];
            this.age = dataFromView[3];
            this.phoneNum = dataFromView[4];
        }

        //=================== GETTER =====================//

        public string GetId() { return id; }
        public string GetPw() { return pw; }
        public string GetName() { return name; }
        public string GetAge() { return age; }
        public string GetPhoneNum() { return phoneNum; }

        //=================== SETTER =====================//

        public void SetId(string id) { this.id = id; }
        public void SetPw(string pw) { this.pw = pw; }
        public void SetName(string name) { this.name = name; }
        public void SetAge(string age) { this.age = age; }
        public void SetPhoneNum(string phoneNum) { this.phoneNum = phoneNum; }
    }
}
