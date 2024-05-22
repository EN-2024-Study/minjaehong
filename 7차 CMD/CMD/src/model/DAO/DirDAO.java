package model.DAO;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Date;
import java.util.LinkedList;
import java.util.Locale;

public class DirDAO{

    private SimpleDateFormat dateFormat;
    private Date lastModifiedDate;

    private StringBuilder resultSb;
    private StringBuilder fileInfoSb;

    private String dateToFormatString;
    private String fileSize;
    private String fileCntString;
    private String dirCntString;
    private String totalFileSizeString;
    private String totalMemoryLeftString;

    public DirDAO() {
        dateFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm", Locale.KOREAN);
        lastModifiedDate = new Date();

        resultSb = new StringBuilder();
        fileInfoSb = new StringBuilder();
    }

    private long getMaximumFileSize(LinkedList<File> fileList){
        long longestSize = 0;
        for (int i=0;i<fileList.size();i++) {
            if (fileList.get(i).length() > longestSize) {
                longestSize = fileList.get(i).length();
            }
        }
        return longestSize;
    }
    
    private void addParentFolders(File sourceDirectory, LinkedList<File> fileList){
        // rootDirectory 일때 예외처리
        // Parent가 존재할때만 fileList에 추가
        if(sourceDirectory.getParentFile()!= null) {
            fileList.add(0, sourceDirectory.getParentFile());
        }
        // 현재 directory fileList에 추가
        fileList.add(0, sourceDirectory);
    }

    private String getCertainFileInfoToString(File file, int maximumSpaceCnt){

        fileInfoSb.setLength(0);

        // DATE
        lastModifiedDate.setTime(file.lastModified());
        dateToFormatString = dateFormat.format(lastModifiedDate);
        fileInfoSb.append(dateToFormatString);
        fileInfoSb.append(" ");

        // DIR OR NOT
        if (file.isDirectory()) fileInfoSb.append("<DIR> ");
        else fileInfoSb.append("      ");

        // SIZE
        fileSize = String.format("%,d", file.length());
        int curFileSizeCnt = fileSize.length();
        if(file.isDirectory()){
            curFileSizeCnt = 0;
            fileSize="";
        }

        for (int k = 0; k < maximumSpaceCnt - curFileSizeCnt; k++) fileInfoSb.append(" ");
        fileInfoSb.append(fileSize);
        fileInfoSb.append(" ");

        return fileInfoSb.toString();
    }

    // 여기서부터는 진짜로 dir 작업 수행
    // directory가 존재할때만 호출됨
    public String dir(String curDirectory, Path sourcePath) throws IOException {

        resultSb.setLength(0);

        File sourceDirectory = new File(sourcePath.toString());

        if (sourceDirectory.isDirectory()) {
            // sourcePath directory 안에 있는 File 모두 불러오기
            LinkedList<File> fileList = new LinkedList<>(Arrays.asList(sourceDirectory.listFiles()));

            // 정렬해주기 위해 가장 큰 사이즈 찾기
            long longestSize = getMaximumFileSize(fileList);
            int maximumSpaceCnt = String.format("%,d", longestSize).length();

            // . .. folder 추가할 수 있으면 추가하기
            addParentFolders(sourceDirectory, fileList);

            int fileCnt = 0;
            int dirCnt = 0;
            long totalFileSize = 0;

            if (fileList != null) {
                for (File file : fileList) {

                    // default로 숨겨진 파일이면 skip
                    if (file.isHidden() == true) continue;
                    if (file.isDirectory()) dirCnt++;
                    else {
                        fileCnt++;
                        totalFileSize += file.length();
                    }

                    resultSb.append(getCertainFileInfoToString(file, maximumSpaceCnt));

                    if (file.equals(sourceDirectory)) {
                        resultSb.append(".");
                    } else if (file.equals(sourceDirectory.getParentFile())) {
                        resultSb.append("..");
                    } else {
                        resultSb.append(file.getName());
                    }
                    resultSb.append("\n");
                }

                fileCntString = String.format("%,d 파일     ",fileCnt);
                for(int i=0;i<27-fileCntString.length();i++) resultSb.append(" ");
                resultSb.append(fileCntString);
                totalFileSizeString = String.format("%,d", totalFileSize);
                for(int i=0;i<17-totalFileSizeString.length();i++) resultSb.append(" ");
                resultSb.append(totalFileSizeString);
                resultSb.append(" 바이트\n");

                dirCntString = String.format("%,d 디렉터리 ",dirCnt);
                for(int i=0;i<27-dirCntString.length();i++) resultSb.append(" ");
                resultSb.append(dirCntString);
                totalMemoryLeftString = String.format("%,d", sourceDirectory.length());
                for(int i=0;i<17-totalMemoryLeftString.length();i++) resultSb.append(" ");
                resultSb.append(totalMemoryLeftString);
                resultSb.append(" 바이트 남음\n");
            }
        } else {
            resultSb.append(getCertainFileInfoToString(sourceDirectory,10));
            resultSb.append("\n");
        }

        return resultSb.toString();
    }
}