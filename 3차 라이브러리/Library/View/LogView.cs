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
        private string[] logDeleteArr = {"LOG ID :"};

        private int LOG_STARTX = 40;
        private int LOG_STARTY = 8;

        public LoggerMenuState LoggerMenuForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[LOG MANAGER MENU]");
            MyConsole.PrintUserID("manager");

            LoggerMenuState selectedMenu = (LoggerMenuState)MyConsole.GetUserSelection(loggerMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
            return selectedMenu;
        }

        public List<string> DeleteLogForm(List<LogDTO> logList)
        {
            Console.Clear();

            MyConsole.PrintHeader("[DELETE LOG]");

            PrintAllLogs(logList);

            List<string> retList= MyConsole.GetUserInputs(logDeleteArr, MyConsole.MENU_STARTX, Console.CursorTop + 2, ExceptionHandler.logDeleteExceptionArr);
            
            return retList;
        }

        public void PrintAllLogs(List<LogDTO> logList)
        {
            Console.SetCursorPosition(LOG_STARTX, LOG_STARTY);

            if (logList.Count == 0)
            {
                MyConsole.PrintHeader("[NO RESULT]");
            }
            for (int i = 0; i < logList.Count; i++)
            {
                Console.WriteLine("");
                Console.CursorLeft = LOG_STARTX;
                Console.WriteLine("────────────────────────────────");
                Console.WriteLine("");
                Console.CursorLeft = LOG_STARTX;
                Console.WriteLine("LOG ID : " + logList[i].GetID());
                Console.CursorLeft = LOG_STARTX;
                Console.WriteLine("TIME   : " + logList[i].GetTime());
                Console.CursorLeft = LOG_STARTX;
                Console.WriteLine("USER   : " + logList[i].GetUser());
                Console.CursorLeft = LOG_STARTX;
                Console.WriteLine("ACTION : " + logList[i].GetAction());

                if (logList[i].GetNote() != "")
                {
                    Console.CursorLeft = LOG_STARTX;
                    Console.WriteLine("NOTE   : " + logList[i].GetNote());
                }
            }
            Console.WriteLine("");
            Console.CursorLeft = LOG_STARTX;
            Console.WriteLine("────────────────────────────────");
        }
    }
}
