package model.VO;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

// 한개의 폴더에 대한 정보 담음
public class DirVO {
    List<FileVO> fileVOList;
    private String curDirectory;
    private String sourcePathString;
    private int fileCnt;
    private int dirCnt;
    private long totalFileSize;
    private long freeSpaceSize;

    public DirVO(String curDirectory, String sourcePathString, long freeSpaceSize){
        this.curDirectory = curDirectory;
        this.sourcePathString = sourcePathString;

        this.fileVOList = new ArrayList<>();
        this.fileCnt = 0;
        this.dirCnt = 0;
        this.totalFileSize = 0;
        this.freeSpaceSize = freeSpaceSize;
    }

    public void addNewFileInfo(Date date, boolean isDirectory, long fileSize, String fileName){
        FileVO curFile = new FileVO(date, isDirectory, fileSize, fileName);
        if(curFile.isDirectory()) {
            dirCnt++;
        }
        else {
            fileCnt++;
            totalFileSize += fileSize;
        }

        fileVOList.add(curFile);
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

    public List<FileVO> getFileInfoList(){
        return fileVOList;
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
