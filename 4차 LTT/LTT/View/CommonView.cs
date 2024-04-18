using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // 공통적으로 쓰이는 한 개의 VIEW를 구성하는 UI들
    static class CommonView
    {
        private static string[] lectureFindMenuArr = { "1. Department :", "2. CourseType :", "3. CourseName :", "4. Professor :", "5. Year :" };

        // 강의검색 UI
        public static List<String> FindLectureForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[LETS FIND LECTURES]");
            
            List<String> filterList = MyConsole.GetUserInputs(lectureFindMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);

            return filterList;    
        }
        
        // 보여줄게 없을때 보여주는 화면
        public static void NoResultForm()
        {
            Console.Clear();
            MyConsole.PrintHeader("NO RESULT");
        }

        // LectureDTO List를 인자로 주면 해당 강의들을 출력해줌
        public static void ShowLectureTable(List<LectureDTO> lectureList)
        {
            Console.Clear();

            Console.SetCursorPosition(0, 0);
            Console.Write("NO");
            Console.SetCursorPosition(4, 0);
            Console.Write("개설학과전공");
            Console.SetCursorPosition(24, 0);
            Console.Write("학수번호");
            Console.SetCursorPosition(34, 0);
            Console.Write("분반");
            Console.SetCursorPosition(40, 0);
            Console.Write("교과목명");
            Console.SetCursorPosition(73, 0);
            Console.Write("이수구분");
            Console.SetCursorPosition(87, 0);
            Console.Write("학년");
            Console.SetCursorPosition(93, 0);
            Console.Write("학점");
            Console.SetCursorPosition(99, 0);
            Console.Write("요일 및 강의시간");
            Console.SetCursorPosition(131, 0);
            Console.Write("강의실");
            Console.SetCursorPosition(145, 0);
            Console.Write("교수명");
            Console.SetCursorPosition(173, 0);
            Console.WriteLine("강의언어");

            int startX = 0;
            int startY = 0;
            Console.WriteLine("========================================================================================================================================================================================");

            for (int i = 0; i < lectureList.Count; i++)
            {
                startX = 0;
                startY = Console.CursorTop;

                LectureDTO curLecture = lectureList[i];

                Console.SetCursorPosition(startX, startY);
                Console.Write(curLecture.GetLectureID());
                Console.SetCursorPosition(startX+4, startY);
                Console.Write(curLecture.GetDepartment());
                Console.SetCursorPosition(startX+24, startY);
                Console.Write(curLecture.GetCourseNumber());
                Console.SetCursorPosition(startX+34, startY);
                Console.Write(curLecture.GetSection());
                Console.SetCursorPosition(startX+40, startY);
                Console.Write(curLecture.GetName());
                Console.SetCursorPosition(startX+73, startY);
                Console.Write(curLecture.GetCourseType());
                Console.SetCursorPosition(startX+87, startY);
                Console.Write(curLecture.GetYear());
                Console.SetCursorPosition(startX+93, startY);
                Console.Write(curLecture.GetCredit());
                Console.SetCursorPosition(startX+99, startY);
                Console.Write(curLecture.GetTime());
                Console.SetCursorPosition(startX+131, startY);
                Console.Write(curLecture.GetClassroom());
                Console.SetCursorPosition(startX+145, startY);
                Console.Write(curLecture.GetProfessor());
                Console.SetCursorPosition(startX+173, startY);
                Console.Write(curLecture.GetLanguage());
                Console.WriteLine();
            }

            Console.WriteLine("========================================================================================================================================================================================");
        }

        // 강의 시간표 보여주는 UI
        public static void ShowTimeTable(List<LectureDTO> list)
        {

        }
    }
}
