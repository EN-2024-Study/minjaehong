using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LTT
{
    static class ExceptionHandler
    {
        public static bool CheckIfValidLectureID(string lectureID)
        {
            string pattern = @"^\d+$";

            bool isNumber = Regex.IsMatch(lectureID, pattern);

            if (isNumber)
            {
                int id = int.Parse(lectureID);
                // 숫자고 존재하는 강의번호이면
                if (id > 0 && id < 185)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
