package model.DTO;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

// 한 개의 Directory(폴더)에 대한 정보 담음
public class DirDTO {
    private ArrayList<FileDTO> fileDTOList;
    private String curDirectory;
    private String sourcePathString;
    private int fileCnt;
    private int dirCnt;
    private long totalFileSize;
    private long freeSpaceSize;

    public DirDTO(String curDirectory, String sourcePathString, long freeSpaceSize){
        this.curDirectory = curDirectory;
        this.sourcePathString = sourcePathString;

        this.fileDTOList = new ArrayList<>();
        this.fileCnt = 0;
        this.dirCnt = 0;
        this.totalFileSize = 0;
        this.freeSpaceSize = freeSpaceSize;
    }

    public void addNewFileInfo(Date date, boolean isDirectory, long fileSize, String fileName){
        FileDTO curFile = new FileDTO(date, isDirectory, fileSize, fileName);
        if(curFile.isDirectory()) {
            dirCnt++;
        }
        else {
            fileCnt++;
            totalFileSize += fileSize;
        }

        fileDTOList.add(curFile);
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

    public List<FileDTO> getFileInfoList(){
        return fileDTOList;
    }

    public int getFileCnt() {
        return fileCnt;
    }

    public int getDirCnt() {
        return dirCnt;
    }

    public long getTotalFileSize() {
        return totalFileSize;
    }

    public long getFreeSpaceSize() { return freeSpaceSize; }
}
