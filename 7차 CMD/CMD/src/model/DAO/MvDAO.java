package model.DAO;

import model.VO.MessageVO;

import java.io.IOException;
import java.nio.file.*;

public class MvDAO {

    // 인자가 1개이든 2개이든
    // 모두 service 에서 정제되서 여기로 들어옴
    public MessageVO move(Path sourcePath, Path destinationPath, boolean doesDestinationExists) throws IOException {

        if(sourcePath.toFile().isFile()){
            if(doesDestinationExists){

            }else{

            }
        }

        if(sourcePath.toFile().isDirectory()){
            if(doesDestinationExists){

            }else{

            }
        }

        Files.move(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new MessageVO(sourcePath.getFileName() + " moved to " + destinationPath);
    }
}
