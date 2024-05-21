package model.DAO;

import model.VO.OutputVO;

import java.io.File;
import java.io.IOException;
import java.nio.file.FileSystem;
import java.nio.file.Path;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Date;
import java.util.LinkedList;
import java.util.Locale;

public class DirDAO extends CmdDAO {
    private File sourceDirectory;

    private SimpleDateFormat dateFormat;
    private Date lastModifiedDate;
    private StringBuilder sb;

    public DirDAO(FileSystem fileSystem, String rootDirectory) {
        super(fileSystem, rootDirectory);

        dateFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm", Locale.KOREAN);
        lastModifiedDate = new Date();
        sb = new StringBuilder();
    }

    private long getMaximumSizeCnt(LinkedList<File> fileList){
        long longestSize = 0;
        for (int i=0;i<fileList.size();i++) {
            if (fileList.get(i).length() > longestSize) {
                longestSize = fileList.get(i).length();
            }
        }
        return longestSize;
    }
    
    private void addParentFolders(LinkedList<File> fileList){
        // rootDirectory 일때 예외처리
        // Parent가 존재할때만 fileList에 추가
        if(sourceDirectory.getParentFile()!= null) {
            fileList.add(0, sourceDirectory.getParentFile());
            System.out.println(fileList.get(0).length());
        }
        // 현재 directory fileList에 추가
        fileList.add(0, sourceDirectory);
        System.out.println(fileList.get(0).length());
    }

    public OutputVO dir(String curDirectory, String source) throws IOException {

        sb.setLength(0);

        // target directory 가 진짜 존재하는지 검사
        Path sourcePath = getNormalizedPath(curDirectory, source);
        if (checkIfDirectoryExists(sourcePath) == false) {
            return new OutputVO("파일을 찾을 수 없습니다.");
        }

        // 여기서부터는 진짜로 dir 작업 수행
        if (isDirectory(sourcePath)) {
            sourceDirectory = new File(source);

            // sourcePath directory 안에 있는 File 모두 불러오기
            LinkedList<File> fileList = new LinkedList<>(Arrays.asList(sourceDirectory.listFiles()));
            
            // 정렬해주기 위해 가장 큰 사이즈 찾기
            long longestSize = getMaximumSizeCnt(fileList);
            int maximumSpaceCnt = String.format("%,d", longestSize).length();

            // . .. folder 추가할 수 있으면 추가하기
            addParentFolders(fileList);

            int fileCnt = 0;
            int dirCnt = 0;

            if (fileList != null) {
                for (File file : fileList) {

                    // default로 숨겨진 파일이면 skip
                    if(file.isHidden()==true) continue;

                    lastModifiedDate.setTime(file.lastModified());
                    String dateString = dateFormat.format(lastModifiedDate);

                    sb.append(dateString);
                    sb.append(" ");

                    if (file.isDirectory()) {
                        dirCnt++;
                        sb.append("<DIR> ");
                    } else {
                        fileCnt++;
                        sb.append("      ");
                    }

                    String sizeStr = String.format("%,d", file.length());
                    int curFileSizeCnt = sizeStr.length();
                    if(sizeStr.equals("0") || file.equals(sourceDirectory.getParentFile()) || file.equals(sourceDirectory)){
                        curFileSizeCnt = 0;
                        sizeStr="";
                    }

                    for (int k = 0; k < maximumSpaceCnt - curFileSizeCnt; k++) sb.append(" ");
                    sb.append(sizeStr);
                    sb.append(" ");

                    if(file.equals(sourceDirectory)){
                        sb.append(".");
                    }else if(file.equals(sourceDirectory.getParentFile())){
                        sb.append("..");
                    }else{
                        sb.append(file.getName());
                    }
                    sb.append("\n");
                }
            }

            sb.append(String.format("%d files\n", fileCnt));
            sb.append(String.format("%d directories", dirCnt));

        } else {
            sb.append("is not a directory");
        }

        return new OutputVO(sb.toString());
    }
}