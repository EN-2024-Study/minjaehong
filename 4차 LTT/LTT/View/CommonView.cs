using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    static class CommonView
    {
        // 강의조회 and 관심과목 검색에 쓰이는 view
        // 공통적으로 쓰여서 여기에 놓음
        public static List<String> FindLectureForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[LETS FIND LECTURES]");
            MyConsole.PrintAllMenu(Constants.lectureFindMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);

            List<String> filterList = new List<string>();
            for (int i = 0; i < Constants.lectureFindMenuArr.Length; i++)
            {
                filterList.Add(CommonInput.GetUserInput(Constants.INPUT_STARTX, Constants.INPUT_STARTY+i));
            }

            return filterList;
        }
        
        // view에 조회된 강의들을 보여줌
        // view의 일부분을 구성해줌
        public static void ShowLectureTable(List<LectureDTO> lectureList)
        {
            Console.Clear();
            Console.WriteLine("NO 개설학과전공     학수번호 분반 교과목명           이수구분      학년 학점  요일 및 강의시간                  강의실       교수명                강의언어");

            StringBuilder sb = new StringBuilder(150);

            int startX = 0;
            int startY = 0;

            for(int i = 0; i < lectureList.Count; i++)
            {
                sb.Clear();

                startX = 0;
                startY = Console.CursorTop;

                LectureDTO curLecture = lectureList[i];

                Console.SetCursorPosition(startX, startY);
                Console.Write(curLecture.GetLectureID());
                Console.SetCursorPosition(startX+4, startY);
                Console.Write(curLecture.GetDepartment());
                Console.SetCursorPosition(startX+24, startY);
                Console.Write(curLecture.GetNumber());
                Console.SetCursorPosition(startX+32, startY);
                Console.Write(curLecture.GetSection());
                Console.SetCursorPosition(startX+36, startY);
                Console.Write(curLecture.GetName());
                Console.SetCursorPosition(startX+69, startY);
                Console.Write(curLecture.GetCourseType());
                Console.SetCursorPosition(startX+83, startY);
                Console.Write(curLecture.GetYear());
                Console.SetCursorPosition(startX+85, startY);
                Console.Write(curLecture.GetCredit());
                Console.SetCursorPosition(startX+87, startY);
                Console.Write(curLecture.GetTime());
                Console.SetCursorPosition(startX+119, startY);
                Console.Write(curLecture.GetClassroom());
                Console.SetCursorPosition(startX+133, startY);
                Console.Write(curLecture.GetProfessor());
                Console.SetCursorPosition(startX + 160, startY);
                Console.Write(curLecture.GetLanguage());
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        // view에 인자로 들어온 강의들을 시간표로 보여줌
        // view의 일부분을 구성해줌
        public static void ShowTimeTable(List<LectureDTO> list)
        {

        }
    }
}
