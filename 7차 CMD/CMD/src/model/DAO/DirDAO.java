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
    private File directory;
    private File file;

    public DirDAO(FileSystem fileSystem, String rootDirectory) {
        super(fileSystem, rootDirectory);
    }

    public OutputVO dir(String curDirectory, String source) throws IOException {
        // curDirectory 기준으로 절대경로 찾아주기
        String directoryPath = getCanonicalPath(curDirectory, source);

        // 해당 directory가 실제로 존재하는지 검사
        if (checkIfDirectoryExists(directoryPath) == false) {
            return new OutputVO("No such file or directory");
        }

        // 여기서부터는 진짜로 dir 작업 수행

        StringBuilder sb = new StringBuilder();

        directory = new File(directoryPath);

        if (directory.isDirectory()) {
            LinkedList<File> fileList = new LinkedList<>(Arrays.asList(directory.listFiles()));

            System.out.println(directory.toString());

            if(directory.getParentFile()!= null) {
                fileList.add(0, directory.getParentFile());
            }

            fileList.add(0, directory);

            // Determine the maximum length of the file size string
            long longestSize = 0;
            for (File file : fileList) {
                if (file.length() > longestSize) {
                    longestSize = file.length();
                }
            }

            // Format the longest file size to get the required width
            int spaceNum = String.format("%,d", longestSize).length();

            int fileCnt = 0;
            int dirCnt = 0;

            if (fileList != null) {
                for (File file : fileList) {
                    // 숨겨진 파일이면 skip
                    if(file.isHidden()==true) continue;

                    Date lastModifiedDate = new Date(file.lastModified());
                    SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm", Locale.KOREAN);
                    String formattedDate = dateFormat.format(lastModifiedDate);

                    sb.append(formattedDate);
                    sb.append(" ");  // Add space between date and file type/size

                    if (file.isDirectory()) {
                        dirCnt++;
                        sb.append("<DIR> ");
                    } else {
                        fileCnt++;
                        sb.append("      ");
                    }

                    // Format the file size with leading spaces
                    String sizeStr = String.format("%,d", file.length());
                    int curNum = sizeStr.length();
                    for (int k = 0; k < spaceNum - curNum; k++) sb.append(" ");
                    if(sizeStr.equals("0")==false || !file.equals(directory) || !file.equals(directory.getParentFile())) {
                        sb.append(sizeStr);
                    }else{
                        sb.append(" ");
                    }
                    sb.append(" ");

                    if(file.equals(directory)){
                        sb.append(".");
                    }else if(file.equals(directory.getParentFile())){
                        sb.append("..");
                    }else{
                        sb.append(file.getName());
                    }
                    sb.append("\n");
                }
            }

            sb.append(String.format("%d files", fileCnt));
            sb.append(String.format("%d directories", dirCnt));

        } else {
            sb.append("is not a directory");
        }

        return new OutputVO(sb.toString());
    }
}