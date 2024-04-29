using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // 사실 Logger는 기능 위주이지 사용자에게 입력받을게 별로 없음
    // 그래서 굳이 View 클래스를 따로 만들어야 싶긴한데
    // 일단 만들어놓는게 보기 편할거 같아서 일단 만듬
    // 그리고 만드는게 맞는거 같음
    // DELETE LOG 빼고는 그냥 모두 RuntimeView로 해결
    class LogView
    {
        private string[] loggerMenuArr = { "DELETE LOG", "SAVE LOG", "DELETE LOG FILE", "RESET LOG"};

        public LoggerMenuState LoggerMenuForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[Logger MENU]");
            MyConsole.PrintUserID("manager");

            LoggerMenuState selectedMenu = (LoggerMenuState)MyConsole.GetUserSelection(loggerMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
            return selectedMenu;
        }

        public int DeleteLogForm(List<LogDTO> logList)
        {
            Console.Clear();

            int deletingLogID = 0;

            return deletingLogID;
        }
    }
}
