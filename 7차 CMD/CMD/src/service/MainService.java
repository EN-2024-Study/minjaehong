package service;

import model.OutputVO;

import java.io.File;
import java.io.IOException;
import java.nio.file.*;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

public class MainService {

    private FileSystem fileSystem;
    private String rootDirectory;
    private String seperator;
    
    private File directory;
    private File file;

    public MainService(FileSystem fileSystem, String rootDirectory) {
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
    private String getCanonicalPath(String curDirectory, String directoryPath) throws IOException {

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
    private boolean checkIfFileOrDirectoryExists(String path){
        file = new File(path);

        if(file.exists()) return true;
        else return false;
    }

    public String changeDirectory(String curDirectory, String directoryPath) throws IOException {

        String changedDirectory = getCanonicalPath(curDirectory, directoryPath);

        // 실제로 존재하면
        if(checkIfFileOrDirectoryExists(changedDirectory)==true) return changedDirectory;
        // 안존재하면
        else return curDirectory;
    }

    public OutputVO listFiles(String curDirectory, String parameter) throws IOException {

        String directoryPath;

        if(parameter.isEmpty()){
            directoryPath = curDirectory;
        }else{
            directoryPath = parameter;
        }

        directoryPath = getCanonicalPath(curDirectory, parameter);
        if(checkIfFileOrDirectoryExists(directoryPath)==false){
            return new OutputVO("directory doesnt exist");
        }

        StringBuilder sb = new StringBuilder();

        directory = new File(directoryPath);

        if(directory.isDirectory()){
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
        }else{
            sb.append("is not a directory");
        }

        return new OutputVO(sb.toString());
    }

    // File 클래스에 replace 함수가 있음
    // copy 인자1 인자2
    // copy 인자1
    public OutputVO copyFile(String curDirectory, String parameters) throws IOException {
        String[] fileNames = parameters.split(" ");

        System.out.println(parameters);

        if (fileNames.length == 0) {
            return new OutputVO("wrong parameters");
        }
        if (fileNames.length == 1) {
            String originalFile = fileNames[0];
            originalFile = getCanonicalPath(curDirectory, originalFile);

            if (checkIfFileOrDirectoryExists(originalFile) == false) {
                return new OutputVO("original file doesnt exist");
            }

            Path originalPath = Paths.get(originalFile);
            String originalName = originalPath.getFileName().toString();

            Path newPath = Paths.get(curDirectory, seperator, originalName);

            file = new File(newPath.toString());

            if (file.exists()) {
                return new OutputVO("file already exists");
            } else {
                try {
                    Files.copy(originalPath, newPath, StandardCopyOption.REPLACE_EXISTING);
                } catch (IOException e) {

                }
            }

            return new OutputVO(originalFile + "created at" + curDirectory);
        }

        if (fileNames.length == 2) {
            String originalFile = fileNames[0];
            originalFile = getCanonicalPath(curDirectory, originalFile);
            Path originalPath = Paths.get(originalFile);

            if (checkIfFileOrDirectoryExists(originalFile) == false) {
                return new OutputVO("original file doesnt exist");
            }

            String newFile = fileNames[1];
            newFile = getCanonicalPath(curDirectory, newFile);
            Path newPath = Paths.get(newFile);
            if (checkIfFileOrDirectoryExists(newFile) == true) {
                return new OutputVO("new file already exist");
            }

            System.out.println("original = " + originalPath.toString());
            System.out.println("new      = " + newPath.toString());
            try {
                Files.copy(originalPath, newPath, StandardCopyOption.REPLACE_EXISTING);
            } catch (IOException e) {

            }

            return new OutputVO(originalFile + " created at " + newPath.toString());
        }

        return new OutputVO("somethings wrong");
    }

    // File 클래스에 replace 함수가 있음
    public void moveFile(String input) {

    }
}
