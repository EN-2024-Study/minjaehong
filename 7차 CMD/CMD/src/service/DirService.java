package service;

import model.DAO.DirDAO;
import model.VO.DirVO;
import utility.Validator;

import java.io.IOException;
import java.nio.file.Path;
import java.util.List;

public class DirService extends CmdService<DirVO>{

    private DirDAO dirDAO;
    private StringBuilder resultSb;

    public DirService(Validator validator){
        super(validator);
        this.dirDAO = new DirDAO();
        this.resultSb = new StringBuilder();
    }

    private boolean checkIfDirectoryExists(Path sourcePath){
        if (validator.checkIfDirectoryExists(sourcePath)==false) {
            resultSb.append(sourcePath);
            resultSb.append(" 디렉터리\n\n");
            resultSb.append("파일을 찾을 수 없습니다.\n");
            return false;
        }

        resultSb.append(sourcePath);
        resultSb.append(" 디렉터리\n\n");
        return true;
    }

    // 1개의 폴더에 대한 정보 (DirVO)만 넘김
    public DirVO handleCommand(String curDirectory, List<String> parameters) throws IOException {

        Path sourcePath = getNormalizedPath(curDirectory, parameters.get(0));

        // 실제로 sourcePath 가 존재하지 않으면 그냥 DirVO 하나 넘김
        if (checkIfDirectoryExists(sourcePath) == false) return new DirVO(curDirectory, "");

        // dirDAO를 통해서 한 개의 폴더에 대한 정보를 담은 DirVO를 받고 바로 return
        return dirDAO.dir(curDirectory, sourcePath);
    }
}