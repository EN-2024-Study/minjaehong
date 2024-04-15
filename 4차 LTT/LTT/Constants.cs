using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    static class Constants
    {
        public const int HEADER_X = 45; // HEADER가 시작하는 X좌표
        public const int HEADER_Y = 4; // HEADER가 시작하는 Y좌표

        public const int MENU_STARTX = 40; // MENU가 끝나야하는 X좌표
        public const int MENU_STARTY = 6; // MENU가 찍혀야하는 Y좌표

        public const int INPUT_STARTX = 60;
        public const int INPUT_STARTY = 6;

        public static string[] modeSelectMenuArr = { "1. 강의조회", "2. 관심과목 담기", "3. 수강신청", "4. 수강내역 조회" };

        public static string[] lectureFindMenuArr = { "1. 개설학과 전공", "2. 이수구분", "3. 교과목명", "4. 교수명", "5. 학년" };

        public static string[] shoppingMenuArr = { "1. 관심과목 검색", "2. 관심과목 내역", "3. 관심과목 시간표", "4. 관심과목 삭제" };

        public static string[] registrationMenuArr = { "1. 수강신청", "2. 수강신청 내역", "3. 수강신청 시간표", "4. 수강과목 삭제" };
    }
}
