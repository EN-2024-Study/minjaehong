package service;

import model.DAO.MoveDAO;
import model.VO.MessageVO;
import utility.Validator;

import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;

public class MoveService extends CmdService<MessageVO>{

    private MoveDAO moveDAO;

    public MoveService(Validator validator) {
        super(validator);
        this.moveDAO = new MoveDAO();
    }

    // 인자 개수에 따라 + directory 인지에 따른 normalizedPath 를 return
    private Path getDestinationPath(String curDirectory, Path sourcePath, List<String> parameters) throws IOException {
        Path destinationPath = null;

        if (parameters.size() == 1) {
            destinationPath = Paths.get(curDirectory, String.valueOf(sourcePath.getFileName()));
        }
        if(parameters.size() == 2){
            destinationPath = getNormalizedPath(curDirectory, parameters.get(1));

            if(validator.isDirectory(destinationPath)){
                destinationPath = Paths.get(destinationPath.toString(), String.valueOf(sourcePath.getFileName()));
            }
        }

        return destinationPath;
    }

    public MessageVO handleCommand(String curDirectory, List<String> parameters) throws IOException {
        
        // 인자 개수 안맞는거 예외처리
        if(parameters.size() > 2) return new MessageVO("명령 구문이 올바르지 않습니다");

        // sourcePath 존재성 검사
        Path sourcePath = getNormalizedPath(curDirectory, parameters.get(0));
        if (validator.checkIfDirectoryExists(sourcePath) == false) {
            return new MessageVO("지정된 파일을 찾을 수 없습니다.");
        }

        // destinationPath 존재성 검사
        Path destinationPath = getDestinationPath(curDirectory, sourcePath, parameters);
        if (validator.checkIfDirectoryExists(destinationPath)) {
            return new MessageVO("destination file already exists in curDirectory");
        }

        // 다 통과했으면 진짜로 move 실행시키기
        return moveDAO.move(sourcePath, destinationPath);
    }
}
