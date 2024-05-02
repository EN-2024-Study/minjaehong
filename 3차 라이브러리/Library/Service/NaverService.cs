using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Library
{
    // 이 함수 자체를 static으로?
    // 어차피 함수 하나 밖에 안씀
    class NaverService
    {
        public static Dictionary<string,BookDTO> GetBooksByNaverAPI(RequestDTO requestDTO)
        {
            // List<BookDTO> 초기화
            Dictionary<string, BookDTO> naverBookList = new Dictionary<string, BookDTO>();

            string bookTitle = requestDTO.GetTitle();
            string howMany = requestDTO.GetHowMany();

            string sort = "sim";
            string query = string.Format("?query={0}&display={1}&sort={2}", bookTitle, howMany, sort);
            string apiURL = "https://openapi.naver.com/v1/search/book.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL + query);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";

            request.Headers.Add("X-Naver-Client-Id", Configuration.GetNaverAPIID());
            request.Headers.Add("X-Naver-Client-Secret", Configuration.GetNaverAPIPW());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();

            // JSON-formatted string -> JsonObject instance로 매핑
            // JObject = newtonjson에서 제공하는 Json 객체 담는 클래스
            JObject json = JObject.Parse(text);

            for (int i = 0; i < int.Parse(howMany); i++)
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

                // 애초에 API로 받아올때 requested = true로 받아와서
                // 애초에 받아올때부터 기존 bookDB에 있는 책들이랑 구분되게 하기
                naverBookList[title] = new BookDTO(title, author, publisher, price, "1", date, isbn, false, true);
                naverBookList.Add(title, new BookDTO(title, author, publisher, price, "1", date, isbn, false, true);
            }

            response.Close();

            return naverBookList;
        }
    }
}
