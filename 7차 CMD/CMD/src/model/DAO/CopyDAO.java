package model.DAO;

import model.VO.OutputVO;

import java.io.IOException;
import java.nio.file.*;

public class CopyDAO{
    public CopyDAO() {

    }

    public OutputVO copy(Path sourcePath, Path destinationPath) throws IOException {

        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new OutputVO(sourcePath.getFileName() + " copied at " + destinationPath.toString());
    }
}
