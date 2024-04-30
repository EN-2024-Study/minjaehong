using System.Text.RegularExpressions;

namespace Library
{
    // 지금으로써는 VIEW에서만 사용되고
    // VIEW에서 InputHandler를 사용할때 InputHandler가 얘를 사용해서 유효성 검사함
    static class ExceptionHandler
    {
        // MAIN VIEW
        public static ExceptionState[] managerLoginExceptionArr = { ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_INT_ONLY };
        
        // MANAGER VIEW

        // 1.TITLE 2.AUTHOR
        public static ExceptionState[] findBookExceptionArr = { ExceptionState.KOREAN_ENGLISH_INT_ONLY, ExceptionState.KOREAN_ENGLISH_INT_ONLY };

        // 1.TITLE 2.AUTHOR
        public static ExceptionState[] addBookExceptionArr = { ExceptionState.KOREAN_ENGLISH_INT_ONLY, ExceptionState.KOREAN_ENGLISH_INT_ONLY, ExceptionState.KOREAN_ENGLISH_INT_ONLY, ExceptionState.INT_ONLY, ExceptionState.INT_ONLY, ExceptionState.DATE_ONLY, ExceptionState.ISBN_ONLY };

        public static ExceptionState[] deleteBookExceptionArr = { ExceptionState.INT_ONLY };

        public static ExceptionState[] updateBookSelectExceptionArr = { ExceptionState.INT_ONLY };

        public static ExceptionState[] updateBookExceptionArr = { ExceptionState.KOREAN_ENGLISH_INT_ONLY, ExceptionState.KOREAN_ENGLISH_INT_ONLY, ExceptionState.KOREAN_ENGLISH_INT_ONLY, ExceptionState.INT_ONLY, ExceptionState.INT_ONLY, ExceptionState.DATE_ONLY, ExceptionState.ISBN_ONLY };

        public static ExceptionState[] applyRequestedBookExceptionArr = { ExceptionState.INT_ONLY };

        // USER FRONT VIEW
        public static ExceptionState[] userCreateAccountExceptionArr = { ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_ONLY, ExceptionState.INT_ONLY, ExceptionState.PHONENUM_ONLY };

        public static ExceptionState[] userLoginExceptionArr = { ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_INT_ONLY };

        // USER VIEW
        public static ExceptionState[] borrowBookExceptionArr = { ExceptionState.INT_ONLY };

        public static ExceptionState[] returnBookExceptionArr = { ExceptionState.INT_ONLY };

        public static ExceptionState[] updateUserExceptionArr = { ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_ONLY, ExceptionState.INT_ONLY, ExceptionState.PHONENUM_ONLY };

        // LOG VIEW
        public static ExceptionState[] logDeleteExceptionArr = { ExceptionState.INT_ONLY };

        // NAVER API
        public static ExceptionState[] searchByNaverAPIExceptionArr = { ExceptionState.KOREAN_ENGLISH_INT_ONLY, ExceptionState.INT_ONLY };

        public static ExceptionState[] requestByNaverAPIExceptionArr = { ExceptionState.KOREAN_ENGLISH_INT_ONLY };

        public static bool CheckIfIntOnlyInput(string input)
        {
            string intOnlyPattern = @"^\d+$";

            Regex regex = new Regex(intOnlyPattern);
            Match match = regex.Match(input);
            
            return match.Success;
        }
        
        public static bool CheckIfEnglishOnlyInput(string input)
        {
            string englishOnlyPattern = @"^[a-zA-Z]+$";

            Regex regex = new Regex(englishOnlyPattern);
            Match match = regex.Match(input);

            return match.Success;
        }

        public static bool CheckIfEnglistAndIntOnlyInput(string input)
        {
            string englishAndIntOnlyPattern = @"^[a-zA-Z0-9]+$";

            Regex regex = new Regex(englishAndIntOnlyPattern);
            Match match = regex.Match(input);

            return match.Success;
        }

        // 010-XXXX-XXXX 만 가능
        public static bool CheckIfPhoneNumInput(string input)
        {
            string phoneNumPattern = @"^010-\d{4}-\d{4}$";

            Regex regex = new Regex(phoneNumPattern);
            Match match = regex.Match(input);

            return match.Success;
        }

        // XXXXXX 형식만 가능
        public static bool CheckIfDateInput(string input)
        {
            string dateNumPattern = @"^\d{6}$";

            Regex regex = new Regex(dateNumPattern);
            Match match = regex.Match(input);

            return match.Success;
        }

        // XXX-XX-XXXX 형식만 가능
        public static bool CheckIfISBNInput(string input)
        {
            string isbnPattern = @"^\d{3}-\d{2}-\d{4}$";

            Regex regex = new Regex(isbnPattern);
            Match match = regex.Match(input);

            return match.Success;
        }

        // KOREAN ENGLISH INT + SPACE ONLY AVAILABLE
        public static bool CheckIfKoreanEnglishIntOnlyInput(string input)
        {
            string bookTitlePattern = @"^[a-zA-Zㄱ-ㅎ가-힣0-9\s]+$";

            Regex regex = new Regex(bookTitlePattern);
            Match match = regex.Match(input);

            return match.Success;
        }
    }
}