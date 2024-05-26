package model.DAO;

import java.io.IOException;
import java.nio.file.*;

public class MvDAO {

    public boolean executeMove(Path sourcePath, Path destinationPath) throws IOException {

        Files.move(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return true;
    }
}
