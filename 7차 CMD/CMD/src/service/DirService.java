package service;

import model.DAO.CmdDAO;
import model.DAO.DirDAO;
import model.VO.OutputVO;

import java.io.IOException;
import java.nio.file.FileSystem;
import java.nio.file.Path;
import java.util.List;

public class DirService {

    private DirDAO dirDAO;
    private CmdDAO cmdDAO;

    private StringBuilder resultSb;

    public DirService(FileSystem fileSystem, String rootDirectory){
        this.cmdDAO = new CmdDAO(fileSystem, rootDirectory);
        this.dirDAO = new DirDAO(fileSystem,rootDirectory);
        this.resultSb = new StringBuilder();
    }

    private boolean checkIfDirectoryExists(Path sourcePath){
        if (cmdDAO.checkIfDirectoryExists(sourcePath)==false) {
            resultSb.append(sourcePath.toString());
            resultSb.append(" 디렉터리\n\n");
            resultSb.append("파일을 찾을 수 없습니다.");
            return false;
        }

        resultSb.append(sourcePath.toString());
        resultSb.append(" 디렉터리\n\n");
        return true;
    }

    // dir 은 인자 제약이 없음
    public OutputVO listDirectory(String curDirectory, List<String> parameters) throws IOException {

        resultSb.setLength(0);
        resultSb.append(" C 드라이브의 볼륨에는 이름이 없습니다.\n 볼륨 일련 번호: 40F1-46D4\n\n");

        // 인자가 0개이면 대상이 curDirectory 임
        // parameter 에 curDirectory 수동 추가
        if (parameters.size()==0) parameters.add(curDirectory);

        for(int i=0;i<parameters.size();i++){
            
            Path sourcePath = cmdDAO.getNormalizedPath(curDirectory, parameters.get(i));

            // 실제로 sourcePath 가 존재하지 않으면 skip
            if(checkIfDirectoryExists(sourcePath)==false) continue;

            resultSb.append(dirDAO.dir(curDirectory, sourcePath));
        }

        return new OutputVO(resultSb.toString());
    }
}