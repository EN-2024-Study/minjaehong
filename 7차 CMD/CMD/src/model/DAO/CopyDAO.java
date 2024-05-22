package model.DAO;

import model.VO.MessageVO;

import java.io.IOException;
import java.nio.file.*;

public class CopyDAO{

    public MessageVO copy(Path sourcePath, Path destinationPath) throws IOException {

        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new MessageVO(sourcePath.getFileName() + " copied at " + destinationPath);
    }
}
