package utility;

import java.io.File;
import java.nio.file.FileSystem;
import java.nio.file.Path;

public class Validator {
    private String rootDirectory;
    private String separater;

    public Validator(String rootDirectory){
        this.rootDirectory = rootDirectory;
        this.separater = System.getProperty("file.separator");
    }

    // rootDirectory에서 시작하는지 확인
    public final boolean checkIfStartingFromRootDirectory(String directoryPath) {
        if (directoryPath.startsWith(rootDirectory) || directoryPath.startsWith(separater) || directoryPath.startsWith("/")) {
            return true;
        }
        return false;
    }

    // 특정 PATH 가 존재하는지 확인
    public final boolean checkIfDirectoryExists(Path path) {

        File file = path.toFile();

        if (file.exists()) return true;

        return false;
    }

    public final boolean isDirectory(Path path){
        File destinationFile = path.toFile();

        if(destinationFile.isDirectory()) return true;

        return false;
    }
}
