package model.VO;

import java.util.Date;

// 한개의 파일에 대한 정보를 담음
public class FileVO {
    private Date lastModifiedDate;
    private boolean isDirectory;
    private long fileSize;
    private String fileName;


    public FileVO(Date date, boolean isDirectory, long fileSize, String fileName){
        this.lastModifiedDate = date;
        this.isDirectory = isDirectory;
        this.fileSize = fileSize;
        this.fileName = fileName;
    }

    public Date getLastModifiedDate() {
        return lastModifiedDate;
    }

    public boolean isDirectory() {
        return isDirectory;
    }

    public long getFileSize() {
        return fileSize;
    }

    public String getFileName() {
        return fileName;
    }
}
