using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Library
{
    class NaverService
    {
        // 이걸 connection이랑 같이 두기??
        // const static? 으로 ??
        // 이 인자를 BookDAO로 넘김
        public string clientID = "LzrVM4JtNzEjcjhnf21x";
        public string clientSecretID = "wHE6dYhDob";

        private List<BookDTO> naverBookList = new List<BookDTO>();

        public NaverService() { }

        // 이걸 NaverDAO로 뺄까?
        // API 사용하는 함수들도 모두 DAO 쪽임???
        // 민감한 정보를 사용하는건데 이걸 service에 이대로 둬도 됨??

        public List<BookDTO> GetBooksByNaverAPI(string bookTitle, string num)
        {
            // List<BookDTO> 초기화
            naverBookList.Clear();

            string sort = "sim";
            string query = string.Format("?query={0}&display={1}&sort={2}", bookTitle, num, sort);

            string apiURL = "https://openapi.naver.com/v1/search/book.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL + query);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";

            request.Headers.Add("X-Naver-Client-Id", clientID);
            request.Headers.Add("X-Naver-Client-Secret", clientSecretID);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();

            // JSON-formatted string -> JsonObject instance로 매핑
            // JObject = newtonjson에서 제공하는 Json 객체 담는 클래스
            JObject json = JObject.Parse(text);

            for (int i = 0; i < int.Parse(num); i++)
            {
                string title = json["items"][i]["title"].ToString();
                title = title.Replace("</b>", "").Replace("<b>", "");
                
                // 책 제목 길어지는거 방지
                // 괄호 있는 부분부터 빼주고 trim으로 후행공백 모두 제거해주기
                if (title.Contains("("))
                {
                    int idx = title.IndexOf("(");
                    title = title.Substring(0, idx);
                    title = title.Trim();
                }

                string author = json["items"][i]["author"].ToString();
                string publisher = json["items"][i]["publisher"].ToString().Replace("<b>", "").Replace("</b>", "");
                string isbn = json["items"][i]["isbn"].ToString().Substring(0,9);
                isbn = isbn.Insert(3, "-");
                isbn = isbn.Insert(6, "-");
                string price = json["items"][i]["discount"].ToString();
                string date = json["items"][i]["pubdate"].ToString().Remove(0,2);

                naverBookList.Add(new BookDTO(title, author, publisher, price, "1", date, isbn, false, false));
            }

            response.Close();

            return naverBookList;
        }

        // AddRequestedBook 하기 전에 Service 단에서 미리 예외처리 조건확인해주기
        // 이건 Controller에서 호출함
        // 해당 책의 전체 제목을 쳐야지 true 반환됨. 포함된거 반환안됨
        public bool CheckIfRequestBookExistsInSearchedBooks(string requestedBookTitle, List<BookDTO> searchedBooks)
        {
            for (int i = 0; i < searchedBooks.Count; i++)
            {
                // GetName GetTitle로 바꾸기
                if (searchedBooks[i].GetName() == requestedBookTitle)
                {
                    return true;
                }
            }
            return false;
        }

        // 이미 예외처리가 다 된 놈이 호출됨
        // 예외처리는 위의 CheckIfRequestedBookExistsInSearchedBooks로 된 상태에서 얘가 호출됨
        public bool AddRequestedBook(string requestedBookTitle, List<BookDTO> searchedBooks)
        {
            for(int i = 0; i < searchedBooks.Count; i++)
            {
                if (searchedBooks[i].GetName() == requestedBookTitle)
                {

                }
            }
           
            return false;
        }
    }
}
