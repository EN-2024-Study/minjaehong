package model.DAO;

import model.DAO.CmdDAO;
import model.VO.OutputVO;

import java.io.IOException;
import java.nio.file.*;

public class CopyDAO extends CmdDAO {
    public CopyDAO(FileSystem fileSystem, String rootDirectory) {
        super(fileSystem, rootDirectory);
    }


    // 1. 인자 1개
    public OutputVO copy(String curDirectory, String source) throws IOException {

        // curDirectory 기준으로 source의 절대경로 찾아주기
        source = getCanonicalPath(curDirectory, source);
        // 해당 source 가 실제로 존재하는지 검사
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("지정된 파일을 찾을 수 없습니다.");
        }
        Path sourcePath = Paths.get(source);

        // 현재 들어온 source 를 기준으로 curDirectory 에 PATH 만들어주기
        String sourceName = sourcePath.getFileName().toString();
        Path destinationPath = Paths.get(curDirectory, seperator, sourceName);

        // destination이 이미 존재하는지 검사 -> 이미 존재하면 덮어쓸까 물어봐야함
        if(checkIfDirectoryExists(destinationPath.toString())){
            return new OutputVO("destination file already exists in curDirectory");
        }

        // 여기서부터는 진짜로 copy 작업 수행
        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(sourceName + " copied at " + curDirectory);
    }

    // 2. 인자1 인자2
    public OutputVO copy(String curDirectory, String source, String destination) throws IOException {

        source = getCanonicalPath(curDirectory, source);
        // source가 진짜 존재하는 놈인지 검사
        if (checkIfDirectoryExists(source) == false) {
            return new OutputVO("source file doesnt exist");
        }
        Path sourcePath = Paths.get(source);

        destination = getCanonicalPath(curDirectory, destination);
        Path destinationPath = Paths.get(destination);


        // destination이 이미 존재하는지 검사
        if (checkIfDirectoryExists(destination) == true) {
            return new OutputVO("destination file already exist");
        }

        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(source + " copied at " + destination);
    }
}
