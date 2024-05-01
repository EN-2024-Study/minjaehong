using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class RequestDTO
    {
        private string title;
        private string howMany;

        public RequestDTO(List<string> requestInfo)
        {
            this.title = requestInfo[0];
            this.howMany = requestInfo[1];
        }

        public string GetTitle()
        {
            return title;
        }

        public string GetHowMany()
        {
            return howMany;
        }
    }
}
