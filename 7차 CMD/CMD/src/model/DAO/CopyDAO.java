package model.DAO;

import model.DAO.CmdDAO;
import model.VO.OutputVO;

import java.io.IOException;
import java.nio.file.*;

public class CopyDAO extends CmdDAO {
    public CopyDAO(FileSystem fileSystem, String rootDirectory) {
        super(fileSystem, rootDirectory);
    }

    public OutputVO copy(String curDirectory, String source) throws IOException {

        // curDirectory 기준으로 source의 절대경로 찾아주기
        source = getCanonicalPath(curDirectory, source);
        Path sourcePath = Paths.get(source);
        String sourceName = sourcePath.getFileName().toString();
        // 인자가 source 만 들어왔으면 destination이 curDirectory임
        Path destinationPath = Paths.get(curDirectory, seperator, sourceName);

        // 해당 source 가 실제로 존재하는지 검사
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("original file doesn't exist");
        }
        // destination이 이미 존재하는지 검사
        if(checkIfDirectoryExists(destinationPath.toString())){
            return new OutputVO("destination file already exists in curDirectory");
        }

        // 여기서부터는 진짜로 copy 작업 수행

        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(sourceName + " copied at " + curDirectory);
    }

    public OutputVO copy(String curDirectory, String source, String destination) throws IOException {

        source = getCanonicalPath(curDirectory, source);
        Path sourcePath = Paths.get(source);
        destination = getCanonicalPath(curDirectory, destination);
        Path destinationPath = Paths.get(destination);

        // source가 진짜 존재하는 놈인지 검사
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("source file doesnt exist");
        }

        // destination이 이미 존재하는지 검사
        if (checkIfDirectoryExists(destination) == true) {
            return new OutputVO("destination file already exist");
        }

        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(source + " copied at " + destination);
    }
}
