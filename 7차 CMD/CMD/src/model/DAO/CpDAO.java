package model.DAO;

import model.VO.MessageVO;

import java.io.IOException;
import java.nio.file.*;

public class CpDAO {

    public MessageVO copy(Path sourcePath, Path destinationPath, boolean doesDestinationExists) throws IOException {

        // 이미 있는 파일이면 알아서 복사됨
        // 없는 파일이면 해당 이름으로 새로운게 생김

        // 1. source가 파일일때
        // copy file ABC
        // 1-1 ABC가 파일이면 알아서 복사됨
        // 1-2 ABC가 디렉토리이면 디렉토리 안에 복사됨. 이건 내가 구현해줘야함

        if(sourcePath.toFile().isFile()){
            // 이미 destination이 존재하면 덮어써야함
            if(doesDestinationExists){

                // 덮어쓸거냐고 물어보기

                // copy -> File to File
                if(destinationPath.toFile().isFile()) {
                    Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);
                }

                // copy -> File to Directory
                if(destinationPath.toFile().isDirectory()){
                    Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);
                }
            }
            // destination이 존재하지 않으면 새로 만들어야함
            // file directory 상관없이 destinationPath 그대로 따라서 생성만 해주면 됨
            else{
                Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);
            }
        }

        // 2. source가 디렉토리일때
        // copy directory ABC
        // 2-1 ABC가 파일이면
        // 2-2 ABC가 디렉토리이면
        
        if(sourcePath.toFile().isDirectory()){
            // 이미 destination이 존재하면 덮어써야함
            if(doesDestinationExists){

                // 덮어쓸거냐고 물어보기

                // copy -> Directory to File
                if(destinationPath.toFile().isFile()) {
                    Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);
                }

                // copy -> Directory to Directory
                if(destinationPath.toFile().isDirectory()){
                    Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);
                }
            }
            // destination이 존재하지 않으면 새로 만들어야함
            else{

            }
        }

        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new MessageVO(sourcePath.getFileName() + " copied at " + destinationPath);
    }
}
