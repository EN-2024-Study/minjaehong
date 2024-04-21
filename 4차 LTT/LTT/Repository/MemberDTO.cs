using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class MemberDTO
    {
        private readonly string id;
        private readonly string pw;

        private int maximumCredit;

        private List<LectureDTO> shoppingBasket;
        private List<LectureDTO> registration;
         
        public MemberDTO(string id, string pw)
        {
            this.id = id;
            this.pw = pw;

            maximumCredit = 21;

            shoppingBasket = new List<LectureDTO>();
            registration = new List<LectureDTO>();
        }

        public string GetPW() { return pw; }
        public int GetMaximumCredit() { return maximumCredit; }
        public int GetCurrentShoppingCredit()
        {
            int total = 0;

            for(int i = 0; i < shoppingBasket.Count; i++)
            {
                total += int.Parse(shoppingBasket[i].GetCredit());
            }
            return total;
        }

        public int GetCurrentRegistrationCredit()
        {
            int total = 0;

            for (int i = 0; i < registration.Count; i++)
            {
                total += int.Parse(registration[i].GetCredit());
            }
            return total;
        }

        public List<LectureDTO> GetShoppingBasket() { return shoppingBasket; }
        public List<LectureDTO> GetRegistration() { return registration; }
    }
}
