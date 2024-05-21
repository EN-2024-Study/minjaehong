package model.DAO;

import model.DAO.CmdDAO;
import model.VO.OutputVO;

import java.io.File;
import java.io.IOException;
import java.nio.file.FileSystem;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Date;
import java.util.LinkedList;
import java.util.Locale;

public class DirDAO extends CmdDAO {
    private File sourceDirectory;
    private StringBuilder sb;

    public DirDAO(FileSystem fileSystem, String rootDirectory) {
        super(fileSystem, rootDirectory);
        sb = new StringBuilder();
    }

    public OutputVO dir(String curDirectory, String source) throws IOException {

        sb.setLength(0);

        // target directory 가 진짜 존재하는지 검사
        source = getCanonicalPath(curDirectory, source);
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("No such file or directory");
        }

        // 여기서부터는 진짜로 dir 작업 수행

        sourceDirectory = new File(source);

        if (sourceDirectory.isDirectory()) {
            LinkedList<File> fileList = new LinkedList<>(Arrays.asList(sourceDirectory.listFiles()));

            System.out.println(sourceDirectory.toString());

            // rootDirectory 일때 예외처리
            // Parent가 존재할때만 fileList에 추가
            if(sourceDirectory.getParentFile()!= null) {
                fileList.add(0, sourceDirectory.getParentFile());
            }

            // 현재 directory fileList에 추가
            fileList.add(0, sourceDirectory);

            long longestSize = 0;
            for (int i=0;i<fileList.size();i++) {
                if (fileList.get(i).length() > longestSize) {
                    longestSize = fileList.get(i).length();
                }
            }

            int maximumSpaceCnt = String.format("%,d", longestSize).length();

            int fileCnt = 0;
            int dirCnt = 0;

            if (fileList != null) {
                for (File file : fileList) {

                    // default로 숨겨진 파일이면 skip
                    if(file.isHidden()==true) continue;

                    Date lastModifiedDate = new Date(file.lastModified());
                    SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm", Locale.KOREAN);
                    String formattedDate = dateFormat.format(lastModifiedDate);

                    sb.append(formattedDate);
                    sb.append(" ");

                    if (file.isDirectory()) {
                        dirCnt++;
                        sb.append("<DIR> ");
                    } else {
                        fileCnt++;
                        sb.append("      ");
                    }

                    // Format the file size with leading spaces
                    String sizeStr = String.format("%,d", file.length());
                    int curFileSizeCnt = sizeStr.length();
                    for (int k = 0; k < maximumSpaceCnt - curFileSizeCnt; k++) sb.append(" ");
                    
                    // 크기가 0이거나 curDirectory 이거나 parentDirectory 이면 크기 표현 안해도 됨
                    if(sizeStr.equals("0") || file.equals(sourceDirectory.getParentFile()) || file.equals(sourceDirectory)){
                        sb.append(" ");
                    }
                    else{
                        sb.append(sizeStr);
                    }
                    
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