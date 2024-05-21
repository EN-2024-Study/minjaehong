package model.DAO;

import model.VO.OutputVO;

import java.io.IOException;
import java.nio.file.FileSystem;
import java.util.AbstractMap;
import java.util.Map;

public class CdDAO extends CmdDAO {
    public CdDAO(FileSystem fileSystem, String rootDirectory) {
        super(fileSystem, rootDirectory);
    }

    public Map.Entry<String, OutputVO> cd(String curDirectory, String destination) throws IOException {

        String changedDirectory = getCanonicalPath(curDirectory, destination);

        if(checkIfDirectoryExists(changedDirectory)){

            if(isDirectory(changedDirectory)){
                return new AbstractMap.SimpleEntry<>(changedDirectory, new OutputVO(""));
            }

            return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO("디렉터리 이름이 올바르지 않습니다."));
        }

        return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO("지정된 경로를 찾을 수 없습니다."));
    }
}
