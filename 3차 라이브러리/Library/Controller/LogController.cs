using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Library
{
    // 일단 Controller라는거 자체가
    // view로부터 입력 받은 것을 처리해주고
    // model이랑 DTO 전달하면서 view와 model을 매개해주는건데
    // Logging 자체가 서비스를 제공하고 사용자 입력을 받는 부분은 아니라서
    // controller라고 보기에는 애매함
    // 그래서 utility로 보는게 어떨까
    // logger?? logHandler ?? LogManager???

    // 1. 특정 action에 대한 Log 정보를 logDB에 저장할때
    // 그리고 이 log 정보 logDTO?? 를 다시 controller한테 넘기는건 또 아닌거 같음
    // 그냥 바로 해당 action을 한 곳에서 logDAO로 바로 접근해서 저장하는게 맞는거 같음
    // 넘기지 않고 static 함수로 logDAO에 넘겨서 저장하는 방식으로 -> ????

    // 그럼 그 log에 대한 클래스는 어떻게 설계해야할까

    // 근데 또 이걸 singleton으로 올리는건 진짜 아닌거 같음

    // loggerController를 만드는거 자체가 log 라는 기능에 비해 굉장히 큰 작업인거 같음
    // log 라는 작업이 사실 controller 상 밑으로 매개변수로 전달해서 처리하는 것보다는
    // 전체 프로그램을 아우르는 관점인 utility성 작업이라고 생각해서
    // 그렇다고 이걸 또 인스턴스변수로 매개변수로 전달하는 것은 진짜 아닌거 같음

    // 그리고 그냥 timeStamp 자체를 string으로 바로 저장?

    class LogController
    {
        private LogView logView;
        private LogRepository logRepository;

        public LogController()
        {
            logView = new LogView();
            logRepository = LogRepository.GetInstance();
        }

        private void DeleteCertainLog()
        {
            // 모든 로그 받아오기
            List<LogDTO> logList = logRepository.GetAllLogs();

            // 삭제할 로그 정보를 logView에서 받아오기
            int deletingLogID = logView.DeleteLogForm(logList);

            if (logRepository.CheckIfLogExists(deletingLogID))
            {
                logRepository.Delete(deletingLogID);
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
            List<LogDTO> logList = logRepository.GetAllLogs();

            StringBuilder logFileBuilder = new StringBuilder();
            string timeStampString;

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\LIBRARY_LOG.txt";

            for (int i = 0; i < logList.Count; i++)
            {
                logFileBuilder.Append("LOG ID: ");
                logFileBuilder.Append(logList[i].GetID());
                logFileBuilder.Append(" // ");

                logFileBuilder.Append("TIME: ");
                timeStampString = logList[i].GetTime().ToString("yyyy-MM-dd HH:mm:ss");
                logFileBuilder.Append(timeStampString);
                logFileBuilder.Append(" // ");

                logFileBuilder.Append("USER: ");
                logFileBuilder.Append(logList[i].GetMode());
                logFileBuilder.Append(" // ");

                logFileBuilder.Append("ACTION: ");
                logFileBuilder.Append(logList[i].GetAction());
                logFileBuilder.Append("\n");
            }

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
                CommonView.RuntimeMessageForm("LOG FILE DELETED!");
            }
            else
            {
                CommonView.RuntimeMessageForm("LOG FILE DOES NOT EXIST!");
            }
        }

        private void ResetLog()
        {
            // 로그파일 초기화
            logRepository.DeleteAll();

            CommonView.RuntimeMessageForm("LOG RESET COMPLETE!");
        }

        public void RunLogController()
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
