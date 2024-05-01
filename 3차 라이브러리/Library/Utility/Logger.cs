using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Library
{
    // [Logger 개요]
    // 1. Add 함수는 프로그램 아무데서나 호출가능 -> Add로 logDAO 통해서 logDB에 log저장
    // 2. Logger는 ManagerController에서만 직접 참조가능 
    class Logger
    {
        private LogView logView;
        private LogDAO logDAO;

        public Logger()
        {
            logView = new LogView();
            logDAO = LogDAO.GetInstance();
        }

        //================================= RECORD LOG ====================================//

        public static void recordLog(DateTime timeStamp, string user, string action, string note)
        {
            LogDAO.GetInstance().Add(timeStamp, user, action, note);
        }

        public static void recordLog(DateTime timeStamp, string user, string action)
        {
            LogDAO.GetInstance().Add(timeStamp, user, action,"");
        }

        //============================ MANAGEMENT FUNCTIONS ==============================//

        private void DeleteCertainLog()
        {
            // 모든 로그 받아오기
            List<LogDTO> logList = logDAO.GetAllLogs();

            // 로그 내역 없으면 그냥 return
            if (logList.Count() == 0)
            {
                CommonView.RuntimeMessageForm("NO LOGS YET!");
                return;
            }

            // 삭제할 로그 정보를 logView에서 받아오기
            List<string> retList = logView.DeleteLogForm(logList);

            int deletingLogID = int.Parse(retList[0]);

            if (logDAO.CheckIfLogExists(deletingLogID))
            {
                logDAO.Delete(deletingLogID);
                CommonView.RuntimeMessageForm("DELETED CERTAIN LOG!");
            }
            else
            {
                CommonView.RuntimeMessageForm("FAILED : CHECK LOG ID AGAIN!");
            }
        }

        private void SaveLogFile()
        {
            // 로그파일 저장
            List<LogDTO> logList = logDAO.GetAllLogs();

            StringBuilder logFileBuilder = new StringBuilder();

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\LIBRARY_LOG.txt";

            for (int i = 0; i < logList.Count; i++)
            {
                logFileBuilder.Append("LOG ID: ");
                logFileBuilder.Append(logList[i].GetID());
                logFileBuilder.Append(" | ");

                logFileBuilder.Append("TIME: ");
                logFileBuilder.Append(logList[i].GetTime());
                logFileBuilder.Append(" | ");

                logFileBuilder.Append("USER: ");
                logFileBuilder.Append(logList[i].GetUser());
                logFileBuilder.Append(" | ");

                logFileBuilder.Append("ACTION: ");
                logFileBuilder.Append(logList[i].GetAction());
               
                if (logList[i].GetNote() != "")
                {
                    logFileBuilder.Append(" | ");
                    logFileBuilder.Append("NOTE: ");
                    logFileBuilder.Append(logList[i].GetNote());
                }
                logFileBuilder.Append("\n");
            }

            recordLog(DateTime.Now, "manager", "LOG FILE SAVE");

            // 이미 있으면 덮어써줌. 그래서 이미 존재하면 삭제할 필요가 없다
            File.WriteAllText(filePath, logFileBuilder.ToString());

            CommonView.RuntimeMessageForm("LOG FILE SAVED!");
        }

        private void DeleteLogFile()
        {
            // 로그파일 삭제
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\LIBRARY_LOG.txt";

            // 존재하면 삭제
            if (File.Exists(filePath))
            {
                File.Delete(filePath);

                recordLog(DateTime.Now, "manager", "DELETE LOG FILE SUCCESS");

                CommonView.RuntimeMessageForm("LOG FILE DELETED!");
            }
            else
            {
                recordLog(DateTime.Now, "manager", "DELETE LOG FILE FAIL", "LOG FILE DOES NOT EXISTS");

                CommonView.RuntimeMessageForm("LOG FILE DOES NOT EXIST!");
            }
        }

        private void ResetLog()
        {
            // 로그파일 초기화
            logDAO.DeleteAll();

            recordLog(DateTime.Now, "manager", "RESET LOG");

            CommonView.RuntimeMessageForm("LOG RESET COMPLETE!");
        }

        public void StartLogManagement()
        {
            LoggerMenuState mode;

            bool isLoggerRunning = true;

            while (isLoggerRunning)
            {
                mode = logView.LoggerMenuForm();

                switch (mode)
                {
                    case LoggerMenuState.DELETE_LOG:
                        DeleteCertainLog();
                        break;

                    case LoggerMenuState.SAVE_LOG:
                        SaveLogFile();
                        break;

                    case LoggerMenuState.DELETE_LOGFILE:
                        DeleteLogFile();
                        break;

                    case LoggerMenuState.RESET_LOG:
                        ResetLog();
                        break;

                    case LoggerMenuState.GOBACK:
                        isLoggerRunning = false;
                        break;
                }
            }
        }
    }
}
