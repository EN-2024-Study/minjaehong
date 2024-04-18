using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LTT
{
    static class ExceptionHandler
    {
        public static bool CheckIfValidLectureID(string lectureID)
        {   
            int id;
            bool isNumber = int.TryParse(lectureID, out id);
            // int형 숫자고 유효한 lectureID이면
            if (isNumber && id > 0 && id < 185) return true;
            else return false;
        }
    }
}