using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LTT
{
    // 정규식 같은 예외처리만 여기서 하고
    // 서비스 예외처리는 controller 안에서 하면 된다
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