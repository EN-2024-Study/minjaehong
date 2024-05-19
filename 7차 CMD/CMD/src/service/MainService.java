package service;

import model.OutputVO;

import java.io.File;
import java.io.IOException;
import java.nio.file.*;

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

    private boolean checkIfStartingFromRootDirectory(String directoryPath) {

        // windows 기준 C:\ 로 시작하거나 구분자만 들어왔으면 root 부터 시작하는거임
        // macOS 기준 ??
        if (directoryPath.startsWith(rootDirectory) || directoryPath.startsWith(seperator) || directoryPath.startsWith("/")) {
            return true;
        }
        return false;
    }

    // 상대경로나 절대경로로 들어온걸 정규경로로 return 해줌
    // 진짜로 존재하는 놈이면 진짜 정규경로 return
    // 없으면 빈 문자열 return(isEmpty로 판단가능)
    private String getCanonicalPath(String curDirectory, String directoryPath) throws IOException {

        String canonicalPath = "";

        // 1. root 부터 시작하고 존재를 진짜로 하는 놈이면
        if (checkIfStartingFromRootDirectory(directoryPath)) {
            directory = new File(directoryPath);

            if(directory.exists())  canonicalPath = directory.getCanonicalPath();
        }
        // 2. 상대경로로 들어왔으면 조작해서 확인하기(.. or ../../ 같은거 or 내부 directory 일때)
        else {
            directoryPath = curDirectory + seperator + directoryPath;
            directory = new File(directoryPath);

            if (directory.exists()) {
                canonicalPath = directory.getCanonicalPath();
            }
        }
        return canonicalPath;
    }

    public String changeDirectory(String curDirectory, String directoryPath) throws IOException {

        String changedDirectory = getCanonicalPath(curDirectory, directoryPath);

        if(changedDirectory.isEmpty()) return curDirectory;
        else return changedDirectory;
    }

    public OutputVO listFiles(String curDirectory, String parameter) throws IOException {

        String directoryPath;

        if(parameter.isEmpty()){
            directoryPath = curDirectory;
        }else{
            directoryPath = parameter;
        }

        directoryPath = getCanonicalPath(curDirectory, parameter);
        if(directoryPath.isEmpty()){
            return new OutputVO("there is no directory");
        }

        StringBuilder sb = new StringBuilder();

        directory = new File(directoryPath);

        if(directory.isDirectory()){
            File[] filesList = directory.listFiles();

            if (filesList != null) {
                for (File file : filesList) {
                    if (file.isDirectory()) {
                        sb.append("Directory : ");
                    } else {
                        sb.append("File : ");
                    }
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

        if(fileNames.length==0){
            return new OutputVO("wrong parameters");
        }
        if(fileNames.length==1){
            String fileName = fileNames[0];

            fileName = getCanonicalPath(curDirectory, fileName);

            file = new File(fileName);

            if(file.exists()){
                return new OutputVO("cannot copy to the same file");
            }else{
                File orgFile = new File(fileName);
                File outFile = new File(fileName);

                try {
                    Files.copy(orgFile.toPath(), outFile.toPath(), StandardCopyOption.REPLACE_EXISTING);
                }catch(IOException e){

                }
            }
        }

        return new OutputVO(fileNames.toString());
    }

    // File 클래스에 replace 함수가 있음
    public void moveFile(String input) {

    }
}
