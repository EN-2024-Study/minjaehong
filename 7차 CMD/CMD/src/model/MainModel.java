package model;

import java.io.File;
import java.io.IOException;
import java.nio.file.*;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class MainModel {

    private FileSystem fileSystem;
    private String rootDirectory;
    private String seperator;

    private File directory;
    private File file;

    public MainModel(FileSystem fileSystem, String rootDirectory){
        this.fileSystem = fileSystem;
        this.rootDirectory = rootDirectory;
        this.seperator = fileSystem.getSeparator();
    }

    // windows 기준 C:\ 로 시작하거나 구분자만 들어왔으면 root 부터 시작하는거임
    // macOS 기준 ??
    private boolean checkIfStartingFromRootDirectory(String directoryPath) {
        if (directoryPath.startsWith(rootDirectory) || directoryPath.startsWith(seperator) || directoryPath.startsWith("/")) {
            return true;
        }
        return false;
    }

    // 상대경로나 절대경로로 들어온걸 정규경로로 return 해줌
    public String getCanonicalPath(String curDirectory, String directoryPath) throws IOException {

        String canonicalPath;

        // 1. root 부터 시작하는 놈이면
        if (checkIfStartingFromRootDirectory(directoryPath)) {
        }
        // 2. 상대경로로 들어왔으면 조작해서 확인하기(.. or ../../ 같은거 or 내부 directory 일때)
        else {
            directoryPath = curDirectory + seperator + directoryPath;
        }

        directory = new File(directoryPath);
        canonicalPath = directory.getCanonicalPath();
        return canonicalPath;
    }

    // 특정 경로에 대한 file 이나 directory가 진짜로 존재하는 놈인지 판별해줌
    public boolean checkIfDirectoryExists(String path) {
        file = new File(path);

        if (file.exists()) return true;
        else return false;
    }


    public OutputVO dir(String curDirectory, String source) throws IOException {
        // curDirectory 기준으로 절대경로 찾아주기
        String directoryPath = getCanonicalPath(curDirectory, source);

        // 해당 directory가 실제로 존재하는지 검사
        if (checkIfDirectoryExists(directoryPath) == false) {
            return new OutputVO("directory doesn't exist");
        }

        // 여기서부터는 진짜로 dir 작업 수행
        
        StringBuilder sb = new StringBuilder();

        directory = new File(directoryPath);

        if (directory.isDirectory()) {
            File[] filesList = directory.listFiles();

            if (filesList != null) {
                for (File file : filesList) {
                    // Create a Date object from the last modified time
                    Date lastModifiedDate = new Date(file.lastModified());

                    // Define the date format pattern
                    SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm", Locale.KOREAN);

                    // Format the date
                    String formattedDate = dateFormat.format(lastModifiedDate);
                    sb.append(formattedDate.toString());
                    sb.append(" ");
                    if (file.isDirectory()) {
                        sb.append("<DIR>");
                    } else {
                        sb.append("     ");
                    }
                    sb.append(" ");
                    sb.append(file.length());
                    sb.append(" ");
                    sb.append(file.getName());
                    sb.append("\n");
                }
            }
        } else {
            sb.append("is not a directory");
        }

        return new OutputVO(sb.toString());
    }

    //================================== COPY ==================================//

    public OutputVO copy(String curDirectory, String source) throws IOException {

        // curDirectory 기준으로 source의 절대경로 찾아주기
        source = getCanonicalPath(curDirectory, source);

        // 해당 source 가 실제로 존재하는지 검사
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("original file doesn't exist");
        }

        // 여기서부터는 진짜로 copy 작업 수행
        
        Path sourcePath = Paths.get(source);
        String sourceName = sourcePath.getFileName().toString();

        // 인자가 source 만 들어왔으면 destination이 curDirectory임
        Path destinationPath = Paths.get(curDirectory, seperator, sourceName);

        // destination이 이미 존재하는지 검사
        if(checkIfDirectoryExists(destinationPath.toString())){
            return new OutputVO("destination file already exists in curDirectory");
        }
        
        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);
        
        return new OutputVO(sourceName + " copied at " + curDirectory);
    }

    public OutputVO copy(String curDirectory, String source, String destination) throws IOException {

        // source가 진짜 존재하는 놈인지 검사
        source = getCanonicalPath(curDirectory, source);
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("source file doesnt exist");
        }

        // destination이 이미 존재하는지 검사
        destination = getCanonicalPath(curDirectory, destination);
        if (checkIfDirectoryExists(destination) == true) {
            return new OutputVO("destination file already exist");
        }

        Path sourcePath = Paths.get(source);
        Path destinationPath = Paths.get(destination);

        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(source + " copied at " + destination);
    }

    //================================== MOVE ==================================//

    public OutputVO move(String curDirectory, String source) throws IOException {
        
        source = getCanonicalPath(curDirectory, source);

        // source가 진짜로 존재하는 놈인지 검사
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("original file doesnt exist");
        }

        Path sourcePath = Paths.get(source);
        String sourceName = sourcePath.getFileName().toString();
        Path destinationPath = Paths.get(curDirectory, seperator, sourceName);

        // 옮길려는 곳에 똑같은 이름의 파일이 존재하는지 확인
        if (checkIfDirectoryExists(destinationPath.toString())) {
            return new OutputVO("destination file already exists in curDirectory");
        }
        
        Files.move(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);
        
        return new OutputVO("file move complete");
    }

    public OutputVO move(String curDirectory, String source, String destination) throws IOException {

        // source가 진짜 존재하는 놈인지 검사
        source = getCanonicalPath(curDirectory, source);
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("source file doesnt exist");
        }

        // destination이 이미 존재하는지 검사
        destination = getCanonicalPath(curDirectory, destination);
        if (checkIfDirectoryExists(destination) == true) {
            return new OutputVO("destination file already exist");
        }

        Path sourcePath = Paths.get(source);
        Path destinationPath = Paths.get(destination);

        Files.move(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(source + " moved to " + destination);
    }
}
