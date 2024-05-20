package model.DAO;

import java.io.File;
import java.io.IOException;
import java.nio.file.*;

public class CmdDAO {

    private File directory;
    private File file;

    protected FileSystem fileSystem;
    protected String rootDirectory;
    protected String seperator;

    protected CmdDAO(FileSystem fileSystem, String rootDirectory){
        this.fileSystem = fileSystem;
        this.rootDirectory = rootDirectory;
        this.seperator = fileSystem.getSeparator();
    }

    // 그냥 default 접근지정자써?

    // windows 기준 C:\ 로 시작하거나 구분자만 들어왔으면 root 부터 시작하는거임
    // macOS 기준 ??
    protected final boolean checkIfStartingFromRootDirectory(String directoryPath) {
        if (directoryPath.startsWith(rootDirectory) || directoryPath.startsWith(seperator) || directoryPath.startsWith("/")) {
            return true;
        }
        return false;
    }

    // 상대경로나 절대경로로 들어온걸 정규경로로 return 해줌
    protected final String getCanonicalPath(String curDirectory, String directoryPath) throws IOException {

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
    protected final boolean checkIfDirectoryExists(String path) {
        file = new File(path);

        if (file.exists()) return true;
        else return false;
    }

    protected final boolean isDirectory(String destination){
        File destinationFile = new File(destination);
        if(destinationFile.isDirectory()) return true;
        else return false;
    }
}
