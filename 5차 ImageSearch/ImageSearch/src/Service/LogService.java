package Service;

import Model.DAO.LogDAO;
import Model.VO.LogListVO;

import javax.swing.*;
import java.awt.*;
import java.net.URL;
import java.util.ArrayList;

public class LogService {
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
            retList.add(new JLabel(curLog));
        }

        return retList;
    }
}
