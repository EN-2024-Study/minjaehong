using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // static vs const
    // const : 컴파일 시간에 알려진 변경할 수 없는 값입니다. 프로그램 수명 동안 변경하지 마세요.
    // static class : 프로그램이 시작되어 클래스가 인스턴스화되기 전에 static 생성자가 호출됩니다.
    // static member : 
    class Constants
    {
        public static string managerID = "manager";
        public static string managerPW = "manager123";

        // MAINVIEW
        public static string[] modeSelectMenuArr = { "1. USER MODE", "2. MANAGER MODE" };

        public static string[] managerLoginArr = { "ID", "PW" };

        // ManagerView
        public static string[] managerMenuArr = { "전체 도서", "도서 찾기", "도서 추가", "도서 삭제", "도서 수정", "회원 관리", "대여 내역", "네이버 검색", "로그 관리", "요청 도서" };

        public static string[] findBookArr = { "FIND BY NAME", "FIND BY AUTHOR" };

        public static string[] addBookArr = { "1. NAME", "2. AUTHOR", "3. PUBLISHER", "4. PRICE", "5. IN STOCK", "6. DATE","7. ISBN"};
        
        public static string[] deleteBookArr = { "DELETING BOOK ID : " };

        public static string[] updateBookSelectArr = { "UPDATING BOOK ID : " };

        public static string[] updateBookArr = { "1. NAME", "2. AUTHOR", "3. PUBLISHER", "4. PRICE", "5. IN STOCK", "6. DATE", "7. ISBN"};

        // UserFrontView
        public static string[] createAccountArr = { "ID", "PW", "NAME", "AGE", "PHONENUM" };
        
        public static string[] userLoginArr = { "ID", "PW" };
        
        public static string[] userFrontMenuArr = { "LOGIN", "CREATE ACCOUNT" };

        // UserView
        public static string[] userMenuArr = { "전체 도서", "도서 찾기", "도서 대여", "대여 확인", "도서 반납", "반납 확인", "정보 수정", "계정 삭제", "네이버 검색", "요청 도서 내역" };

        //public static string[] findBookArr = { "FIND BY NAME", "FIND BY AUTHOR" };

        public static string[] borrowBookArr = {"BOOK ID"};

        public static string[] returnBookArr = {"BOOK ID"};

        public static string[] updateUserArr = { "1. PW", "2. NAME", "3. AGE", "4. PHONENUM" };
    }
}
