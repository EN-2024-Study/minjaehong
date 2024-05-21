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
    // curDirectory에 source와 동일한 이름으로 복사하는거임
    public OutputVO copy(String curDirectory, String source) throws IOException {

        // curDirectory 기준으로 source의 절대경로 찾아주기
        Path sourcePath = getNormalizedPath(curDirectory, source);
        // 해당 source 가 실제로 존재하는지 검사
        if (checkIfDirectoryExists(sourcePath) == false) {
            return new OutputVO("지정된 파일을 찾을 수 없습니다.");
        }

        // 현재 들어온 source 를 기준으로 curDirectory 에 PATH 만들어주기
        Path destinationPath = Paths.get(curDirectory, String.valueOf(sourcePath.getFileName()));

        // destination이 이미 존재하는지 검사 -> 이미 존재하면 덮어쓸까 물어봐야함
        if(checkIfDirectoryExists(destinationPath)){
            return new OutputVO("destination file already exists in curDirectory");
        }

        // 여기서부터는 진짜로 copy 작업 수행
        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(sourcePath.toString() + " copied at " + curDirectory);
    }

    // 2. 인자1 인자2
    public OutputVO copy(String curDirectory, String source, String destination) throws IOException {

        Path sourcePath = getNormalizedPath(curDirectory, source);

        // source가 진짜 존재하는 놈인지 검사
        if (checkIfDirectoryExists(sourcePath) == false) {
            return new OutputVO("지정된 파일을 찾을 수 없습니다");
        }

        Path destinationPath = getNormalizedPath(curDirectory, destination);

        // destination이 이미 존재하는지 검사
        if (checkIfDirectoryExists(destinationPath) == true) {
            return new OutputVO("destination file already exist");
        }

        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(source + " copied at " + destination);
    }
}
