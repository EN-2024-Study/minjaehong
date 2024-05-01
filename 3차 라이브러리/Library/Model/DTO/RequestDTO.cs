using System.Collections.Generic;

namespace Library
{
    class RequestDTO
    {
        private string title;
        private string howMany;

        public RequestDTO(List<string> requestInfo)
        {
            title = requestInfo[0];
            howMany = requestInfo[1];
        }

        public string GetTitle() { return title; }
        public string GetHowMany() { return howMany; }
    }
}
