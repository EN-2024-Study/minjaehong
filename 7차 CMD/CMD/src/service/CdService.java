package service;

import model.DAO.CmdDAO;
import model.VO.OutputVO;

import java.io.IOException;
import java.nio.file.FileSystem;
import java.nio.file.Path;
import java.util.AbstractMap;
import java.util.List;
import java.util.Map;

public class CdService {
    private CmdDAO cmdDAO;

    public CdService(FileSystem fileSystem, String rootDirectory){
        cmdDAO = new CmdDAO(fileSystem, rootDirectory);
    }

    public Map.Entry<String, OutputVO> changeDirectory(String curDirectory, List<String> parameters) throws IOException {

        if(parameters.size()==0){
            return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO(curDirectory));
        }

        if(parameters.size()>2){
            return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO("지정된 경로를 찾을 수 없습니다"));
        }

        String destination = parameters.get(0);
        return cd(curDirectory, destination);
    }

    public Map.Entry<String, OutputVO> cd(String curDirectory, String destination) throws IOException {

        Path changedDirectoryPath = cmdDAO.getNormalizedPath(curDirectory, destination);

        if(cmdDAO.checkIfDirectoryExists(changedDirectoryPath)){

            if(cmdDAO.isDirectory(changedDirectoryPath)){
                return new AbstractMap.SimpleEntry<>(changedDirectoryPath.toString(), new OutputVO(""));
            }

            return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO("디렉터리 이름이 올바르지 않습니다."));
        }

        return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO("지정된 경로를 찾을 수 없습니다."));
    }
}