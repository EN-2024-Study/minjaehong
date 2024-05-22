package service;

import model.DAO.CopyDAO;
import model.VO.OutputVO;
import utility.Validator;

import java.io.IOException;
import java.nio.file.*;
import java.util.List;

public class CopyService extends CmdService{

    private CopyDAO copyDAO;

    public CopyService(Validator validator){
        super(validator);
        copyDAO = new CopyDAO();
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

    public OutputVO handleCommand(String curDirectory, List<String> parameters) throws IOException {

        Path sourcePath = getNormalizedPath(curDirectory, parameters.get(0));
        if (validator.checkIfDirectoryExists(sourcePath) == false) {
            return new OutputVO("지정된 파일을 찾을 수 없습니다.");
        }

        Path destinationPath = getDestinationPath(curDirectory, sourcePath, parameters);
        if (validator.checkIfDirectoryExists(destinationPath) == true) {
            return new OutputVO("destination file already exist");
        }

        return copyDAO.copy(sourcePath, destinationPath);
    }
}
