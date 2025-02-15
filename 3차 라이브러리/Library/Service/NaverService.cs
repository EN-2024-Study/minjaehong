﻿using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Library
{
    class NaverService
    {
        public static Dictionary<Tuple<string,string>,BookDTO> GetBooksByNaverAPI(RequestDTO requestDTO)
        {
            // List<BookDTO> 초기화
            Dictionary<Tuple<string,string>, BookDTO> naverBookList = new Dictionary<Tuple<string, string>, BookDTO>();

            string bookTitle = requestDTO.GetTitle();
            int howMany = int.Parse(requestDTO.GetHowMany());

            string sort = "sim";
            string query = string.Format("?query={0}&display={1}&sort={2}", bookTitle, howMany, sort);
            string apiURL = "https://openapi.naver.com/v1/search/book.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL + query);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "GET";

            request.Headers.Add("X-Naver-Client-Id", Configuration.GetNaverAPIID());
            request.Headers.Add("X-Naver-Client-Secret", Configuration.GetNaverAPIPW());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseStatus = response.StatusCode.ToString();

            if (responseStatus != "OK")
            {
                return naverBookList;
            }

            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            
            string text = reader.ReadToEnd();

            // JObject : newtonJson에서 제공하는 Json 객체"들" 담기 가능하게 해주는 클래스
            // 추가정보들도 json data로 처리됨
            // 추가 정보 + 검색 객체 배열을 모두 JObject로 받음

            JObject json = JObject.Parse(text);

            // 추가 정보인 total 파싱해서 예외처리
            int total = (int)json["total"];
            if (total < howMany) howMany = total;

            for (int i = 0; i < howMany; i++)
            {
                string title = json["items"][i]["title"].ToString();
                title = title.Replace("</b>", "").Replace("<b>", "");
                
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
                //naverBookList[title] = new BookDTO(title, author, publisher, price, "1", date, isbn, false, true);
                naverBookList.Add(Tuple.Create(title,author), new BookDTO(title, author, publisher, price, "1", date, isbn, false, true));
            }

            response.Close();

            return naverBookList;
        }
    }
}
