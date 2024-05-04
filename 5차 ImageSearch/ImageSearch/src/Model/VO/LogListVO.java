package Model.VO;

import java.util.ArrayList;

public class LogListVO {
    // O(1) O(1)
    private ArrayList<String> logList;

    public LogListVO(ArrayList<String> logList){
        this.logList = logList;
    }

    public ArrayList<String> GetLogList(){
        return logList;
    }
}
