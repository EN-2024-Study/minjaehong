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
        public static void FindLectureForm()
        {
            MyConsole.PrintHeader("[LETS FIND LECTURES]");
            MyConsole.PrintAllMenu(Constants.lectureFindMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);

            List<String> results = new List<string>();
            for (int i = 0; i < Constants.lectureFindMenuArr.Length; i++)
            {
                results.Add(CommonInput.GetUserInput(Constants.INPUT_STARTX, Constants.INPUT_STARTY+i));
            }

            Console.SetCursorPosition(40, 20);
            for(int i = 0; i < results.Count; i++)
            {
                Console.WriteLine(results[i]);
            }

            Console.ReadLine();
        }
        
        // view에 조회된 강의들을 보여줌
        // view의 일부분을 구성해줌
        public static void ShowLectureTable(List<LectureDTO> list)
        {

        }

        // view에 인자로 들어온 강의들을 시간표로 보여줌
        // view의 일부분을 구성해줌
        public static void ShowTimeTable(List<LectureDTO> list)
        {

        }
    }
}
