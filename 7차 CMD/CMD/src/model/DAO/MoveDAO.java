package model.DAO;

import model.DAO.CmdDAO;
import model.VO.OutputVO;

import java.io.IOException;
import java.nio.file.*;

public class MoveDAO extends CmdDAO {

    public MoveDAO(FileSystem fileSystem, String rootDirectory) {
        super(fileSystem, rootDirectory);
    }

    public OutputVO move(String curDirectory, String source) throws IOException {

        source = getCanonicalPath(curDirectory, source);
        Path sourcePath = Paths.get(source);
        String sourceName = sourcePath.getFileName().toString();
        Path destinationPath = Paths.get(curDirectory, seperator, sourceName);

        // source가 진짜로 존재하는 놈인지 검사
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("original file doesnt exist");
        }
        // 옮길려는 곳에 똑같은 이름의 파일이 존재하는지 확인
        if (checkIfDirectoryExists(destinationPath.toString())) {
            return new OutputVO("destination file already exists in curDirectory");
        }

        Files.move(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO("file move complete");
    }

    public OutputVO move(String curDirectory, String source, String destination) throws IOException {

        // sourcePath 생성
        source = getCanonicalPath(curDirectory, source);
        Path sourcePath = Paths.get(source);

        // destinationPath 생성
        destination = getCanonicalPath(curDirectory, destination);
        Path destinationPath = Paths.get(destination);

        // source 가 진짜 존재하는지 검사
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("source file doesnt exist");
        }
        // destination이 이미 존재하는지 검사
        if (checkIfDirectoryExists(destination) == true) {
            return new OutputVO("destination file already exist");
        }

        Files.move(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(source + " moved to " + destination);
    }
}
