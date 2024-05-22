package utility;

import java.io.File;
import java.nio.file.FileSystem;
import java.nio.file.Path;

public class Validator {
    private FileSystem fileSystem;
    private String rootDirectory;
    private String separater;

    public Validator(FileSystem fileSystem, String rootDirectory){
        this.fileSystem = fileSystem;
        this.rootDirectory = rootDirectory;
        this.separater = fileSystem.getSeparator();
    }

    public final boolean checkIfStartingFromRootDirectory(String directoryPath) {
        if (directoryPath.startsWith(rootDirectory) || directoryPath.startsWith(separater) || directoryPath.startsWith("/")) {
            return true;
        }
        return false;
    }

    // 특정 PATH 가 존재하는지 확인
    public final boolean checkIfDirectoryExists(Path path) {
        File file = new File(path.toString());

        if (file.exists()) return true;

        return false;
    }

    public final boolean isDirectory(Path path){
        File destinationFile = new File(path.toString());

        if(destinationFile.isDirectory()) return true;

        return false;
    }
}
