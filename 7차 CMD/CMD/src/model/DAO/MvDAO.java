package model.DAO;

import java.io.IOException;
import java.nio.file.*;

public class MvDAO {

    public void executeMove(Path sourcePath, Path destinationPath) throws IOException {

        Files.move(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

    }
}
