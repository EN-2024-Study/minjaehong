package utility;

import constant.Constants;

import java.io.File;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;

public class Validator {
    private final String rootDirectory;
    private final String separater;

    public Validator(){
        this.rootDirectory = Paths.get(System.getProperty(Constants.USER_HOME)).getRoot().toString();
        this.separater = System.getProperty("file.separator");
    }

    public boolean checkIfValidParameters(List<String> parameters) {

        for (String parameter : parameters) {
            String temp = parameter;

            if (checkIfStartingFromRootDirectory(parameter)) {
                Path path = Paths.get(parameter);
                String root = path.getRoot().toString();

                temp = path.toString().substring(root.length());
            }

            if (temp.contains("?") || temp.contains(":") || temp.contains("*")
                    || temp.contains("\\\\") || temp.contains("//")
                    || temp.contains("|") || temp.contains("<") || temp.contains(">")) {
                return false;
            }
        }

        return true;
    }

    // rootDirectory에서 시작하는지 확인
    public boolean checkIfStartingFromRootDirectory(String directoryPath) {
        if (directoryPath.startsWith(rootDirectory) || directoryPath.startsWith(rootDirectory.toLowerCase()) ||
                directoryPath.startsWith(separater) || directoryPath.startsWith("/")) {
            return true;
        }
        return false;
    }

    // 특정 PATH 가 존재하는지 확인
    public boolean checkIfDirectoryExists(Path path) {

        File file = path.toFile();

        if (file.exists()) return true;

        return false;
    }

    // destinationFile이 존재하지 않아도 잘 작동함
    public boolean checkIfDirectory(Path path){
        File destinationFile = path.toFile();

        if(destinationFile.isDirectory()) return true;

        return false;
    }
}
