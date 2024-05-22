package model.VO;

import java.util.Date;

public class FileInfo{
    private Date lastModifiedDate;
    private boolean isDirectory;
    private long fileSize;
    private String fileName;

    FileInfo(Date date, boolean isDirectory, long fileSize, String fileName){
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
