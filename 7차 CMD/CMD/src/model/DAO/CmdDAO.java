package model.DAO;

import java.io.File;
import java.io.IOException;
import java.nio.file.*;

public class CmdDAO {

    protected FileSystem fileSystem;
    protected String rootDirectory;
    protected String seperator;

    protected CmdDAO(FileSystem fileSystem, String rootDirectory){
        this.fileSystem = fileSystem;
        this.rootDirectory = rootDirectory;
        this.seperator = fileSystem.getSeparator();
    }

    protected final boolean checkIfStartingFromRootDirectory(String directoryPath) {
        if (directoryPath.startsWith(rootDirectory) || directoryPath.startsWith(seperator) || directoryPath.startsWith("/")) {
            return true;
        }
        return false;
    }

    // 상대경로나 절대경로로 들어온걸 정규경로로 return 해줌
    protected final Path getNormalizedPath(String curDirectory, String directoryPath) throws IOException {

        Path retPath;

        // 1. root 부터 시작하는 놈이면 바로 Path 객체 만들어주기
        // 얘는 C:/ 부터 시작하는 놈인거임
        if (checkIfStartingFromRootDirectory(directoryPath)) {
            retPath = Paths.get(directoryPath);
        }
        // 2. 상대경로로 들어왔으면 curDirectory 이용해서 절대경로로 만들어주기
        // C:/A/B/C/../X/Y 형식으로 만들고
        else {
            retPath = Paths.get(curDirectory, directoryPath);
        }

        // normalize 해주기 (. .. 이런거 다 빼주기)
        retPath = retPath.normalize();
        // uSer 같은거 실제로 USER 일때 대비해서 File 로 한번 바꿔주고 내보내야함
        File file = new File(retPath.toString());
        retPath = Paths.get(file.getCanonicalPath());

        return retPath;
    }

    // 특정 PATH 가 존재하는지 확인
    protected final boolean checkIfDirectoryExists(Path path) {
        File file = new File(path.toString());

        if (file.exists()) return true;

        return false;
    }

    protected final boolean isDirectory(Path path){
        File destinationFile = new File(path.toString());

        if(destinationFile.isDirectory()) return true;

        return false;
    }
}
