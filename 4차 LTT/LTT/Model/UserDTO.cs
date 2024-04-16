using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    class UserDTO
    {
        private string id;
        private string pw;

        private int availableCredit;

        private List<LectureDTO> shoppingBasket;
        private List<LectureDTO> registration;

        public UserDTO(string id, string pw)
        {
            this.id = id;
            this.pw = pw;

            availableCredit = 21;

            shoppingBasket = new List<LectureDTO>();
            registration = new List<LectureDTO>();
        }
    }
}
