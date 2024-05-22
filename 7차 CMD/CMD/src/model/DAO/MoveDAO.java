package model.DAO;

import model.VO.OutputVO;

import java.io.IOException;
import java.nio.file.*;

public class MoveDAO{

    public MoveDAO() {

    }

    // 인자가 1개이든 2개이든
    // 모두 service 에서 정제되서 여기로 들어옴
    public OutputVO move(Path sourcePath, Path destinationPath) throws IOException {

        Files.move(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(sourcePath.getFileName() + " moved to " + destinationPath.toString());
    }
}
