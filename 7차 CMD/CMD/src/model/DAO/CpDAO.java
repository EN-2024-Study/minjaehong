package model.DAO;

import model.VO.MessageVO;

import java.io.File;
import java.io.IOException;
import java.nio.file.*;
import java.util.Arrays;
import java.util.LinkedList;

public class CpDAO {

    public MessageVO executeCopy(Path sourcePath, Path destinationPath) throws IOException {

        // 이미 있는 파일이면 알아서 복사됨
        // 없는 파일이면 해당 이름으로 새로운게 생김

        // 1. source가 파일일때
        // copy file ABC
        // 1-1 ABC가 파일이면 알아서 복사됨
        // 1-2 ABC가 디렉토리이면 디렉토리 안에 복사됨. 이건 내가 구현해줘야함

        // 2. source가 디렉토리일때
        // copy directory ABC
        // 2-1 ABC가 파일이면
        // 2-2 ABC가 디렉토리이면

        Files.copy(sourcePath, destinationPath, StandardCopyOption.REPLACE_EXISTING);

        return new MessageVO(sourcePath.getFileName() + " copied at " + destinationPath);
    }
}
