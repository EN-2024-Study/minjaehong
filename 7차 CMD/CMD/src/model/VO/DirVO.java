package model.VO;

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class DirVO {
    List<FileInfo> fileInfoList;
    private String curDirectory;
    private String sourcePathString;

    public DirVO(String curDirectory, String sourcePathString){
        this.curDirectory = curDirectory;
        this.sourcePathString = sourcePathString;
        fileInfoList = new ArrayList<>();
    }

    public void addNewFileInfo(Date date, boolean isDirectory, long fileSize, String fileName){
        fileInfoList.add(new FileInfo(date, isDirectory, fileSize, fileName));
    }

    public boolean checkIfDirectoryExists(){
        // sourcePathString이 비어있으면 깡통 VO 임
        // 해당 폴더나 파일이 존재하지 않는다는 의미
        if(sourcePathString.isEmpty()) return false;
        return true;
    }

    public String getCurDirectory(){
        return curDirectory;
    }

    public String getSourcePathString(){
        return sourcePathString;
    }

    public List<FileInfo> getFileInfoList(){
        return fileInfoList;
    }
}
