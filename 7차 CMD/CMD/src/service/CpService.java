package service;

import model.DAO.CpDAO;
import model.VO.MessageVO;
import utility.Validator;

import java.io.IOException;
import java.nio.file.*;
import java.util.List;

public class CpService extends CmdService<MessageVO>{

    private CpDAO cpDAO;

    public CpService(Validator validator){
        super(validator);
        cpDAO = new CpDAO();
    }

    private Path getDestinationPath(String curDirectory, Path sourcePath, List<String> parameters) throws IOException {
        Path destinationPath = null;

        if(parameters.size()==1){
            destinationPath = Paths.get(curDirectory, String.valueOf(sourcePath.getFileName()));
        }

        if(parameters.size()==2){
            destinationPath = getNormalizedPath(curDirectory, parameters.get(1));
        }

        return destinationPath;
    }

    @Override
    public MessageVO handleCommand(String curDirectory, List<String> parameters) throws IOException {

        // 인자 개수 안맞는거 예외처리
        if(parameters.size() > 2) return new MessageVO("명령 구문이 올바르지 않습니다");

        // source가 존재하지 않으면 예외처리 (source가 파일이든 디렉터리이든 존재하지 않으면 이 구문 뜨는건 똑같음)
        Path sourcePath = getNormalizedPath(curDirectory, parameters.get(0));
        if (validator.checkIfDirectoryExists(sourcePath) == false) {
            return new MessageVO("지정된 파일을 찾을 수 없습니다.");
        }

        Path destinationPath = getDestinationPath(curDirectory, sourcePath, parameters);
        boolean doesDestinationExists = validator.checkIfDirectoryExists(destinationPath);

        return cpDAO.copy(sourcePath, destinationPath, doesDestinationExists);
    }
}

//1. copy 파일 파일
//
//매개변수 3개 이상 (0)
//        C:\Users\USER\Desktop> copy "i am test file.txt" aaa.txt bbb.txt
//        명령 구문이 올바르지 않습니다.
//
//존재 안하는 파일 TO 존재 하는 파일 (0)
//존재 안하는 파일 TO 존재 안하는 파일 (0)
//        -> 지정된 파일을 찾을 수 없습니다
//
//존재하는 파일 TO 존재하는 파일
//        C:\Users\USER\Desktop> copy "i am test file.txt" aaa.txt
//        1개 파일이 복사되었습니다.
//
//존재하는 파일 TO 존재 안하는 파일
//        존재 안하는 파일을 생성하고 복사해줌