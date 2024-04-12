using System.Text.RegularExpressions;

namespace Library
{
    static class ExceptionHandler
    {
        // MAIN VIEW
        public static ExceptionState[] managerLoginExceptionArr = { ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_INT_ONLY };
        
        // MANAGER VIEW
        public static ExceptionState[] findBookExceptionArr = { ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_ONLY };

        public static ExceptionState[] addBookExceptionArr = { ExceptionState.ENGLISH_ONLY, ExceptionState.ENGLISH_ONLY, ExceptionState.ENGLISH_INT_ONLY, ExceptionState.INT_ONLY, ExceptionState.INT_ONLY };

        public static ExceptionState[] deleteBookExceptionArr = { ExceptionState.INT_ONLY };

        public static ExceptionState[] updateBookSelectExceptionArr = { ExceptionState.INT_ONLY };

        public static ExceptionState[] updateBookExceptionArr = { ExceptionState.ENGLISH_ONLY, ExceptionState.ENGLISH_ONLY, ExceptionState.ENGLISH_INT_ONLY, ExceptionState.INT_ONLY, ExceptionState.INT_ONLY };

        // USER FRONT VIEW
        public static ExceptionState[] userCreateAccountExceptionArr = { ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_ONLY, ExceptionState.INT_ONLY, ExceptionState.PHONENUM_ONLY };

        public static ExceptionState[] userLoginExceptionArr = { ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_INT_ONLY };

        // USER VIEW
        public static ExceptionState[] borrowBookExceptionArr = { ExceptionState.INT_ONLY };

        public static ExceptionState[] returnBookExceptionArr = { ExceptionState.INT_ONLY };

        public static ExceptionState[] updateUserExceptionArr = { ExceptionState.ENGLISH_INT_ONLY, ExceptionState.ENGLISH_ONLY, ExceptionState.INT_ONLY, ExceptionState.PHONENUM_ONLY };

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
    }
}