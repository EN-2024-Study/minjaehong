package model.DAO;

import java.io.IOException;
import java.nio.file.*;

public class CpDAO {

    public boolean executeCopy(Path sourcePath, Path destinationPath) throws IOException {
        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);
        return true;
    }
}
