package Service;

import Model.DAO.LogDAO;
import Model.VO.LogListVO;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.net.URL;
import java.util.ArrayList;

public class LogService {

    public void AddLog(String keyWord){
        LogDAO logDAO = LogDAO.GetInstance();
        logDAO.AddLog(keyWord);
    }

    public ArrayList<JLabel> GetAllLogs(){

        // 싱글톤 DAO 받기
        LogDAO logDAO = LogDAO.GetInstance();
        // DAO 통해서 검색결과 받기
        LogListVO logListVO = logDAO.GetLogs();
        ArrayList<String> logList = logListVO.GetLogList();

        // 반환할 ArrayList 생성
        ArrayList<JLabel> retList = new ArrayList<>();
        
        String curLog;
        for (int i = 0; i < logList.size(); i++) {
            curLog = logList.get(i);
            // 새 이미지 return list 에 추가
            JLabel curLogLabel = new JLabel(curLog);
            curLogLabel.setSize(800,50);
            curLogLabel.setBorder(new LineBorder(Color.BLACK, 5,false));
            retList.add(curLogLabel);
        }

        return retList;
    }

    public void DeleteAllLogs(){
        // 싱글톤 DAO 받기
        LogDAO logDAO = LogDAO.GetInstance();

        logDAO.DeleteAll();
    }
}
