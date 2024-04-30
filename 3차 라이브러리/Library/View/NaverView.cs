using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class NaverView
    {    
        //네이버 검색은 우선 공용으로 사용하는거니까 여따 두자 일단은
        public List<string> SearchByNaverAPIForm()
        {
            //Console.SetWindowSize(150,50);

            string[] searchByNaverAPIArr = { "1. NAME :", "2. NUMBER :" };

            Console.Clear();

            MyConsole.PrintHeader("[REQUEST BY NAVER API]");

            List<string> searchInfo = MyConsole.GetUserInputs(searchByNaverAPIArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY, ExceptionHandler.searchByNaverAPIExceptionArr);

            return searchInfo;
        }

        public List<String> GetRequestBookTitleFromUser()
        {
            string[] requestByUserArr = { "BOOK TITLE :" };

            List<String> requestedInfo = MyConsole.GetUserInputs(requestByUserArr, MyConsole.MENU_STARTX, Console.CursorTop+2, ExceptionHandler.requestByNaverAPIExceptionArr);

            //Console.SetWindowSize(110, 50);

            return requestedInfo;
        }
    }
}
