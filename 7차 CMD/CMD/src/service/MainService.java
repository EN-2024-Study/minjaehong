package service;

import model.DAO.*;
import model.VO.*;

import java.io.IOException;
import java.nio.file.*;
import java.util.AbstractMap;
import java.util.List;
import java.util.Map;

public class MainService {

    private CdDAO cdDAO;
    private CopyDAO copyDAO;
    private DirDAO dirDAO;
    private MoveDAO moveDAO;

    public MainService(FileSystem fileSystem, String rootDirectory) {
        this.cdDAO = new CdDAO(fileSystem, rootDirectory);
        this.copyDAO = new CopyDAO(fileSystem, rootDirectory);
        this.dirDAO = new DirDAO(fileSystem,rootDirectory);
        this.moveDAO = new MoveDAO(fileSystem, rootDirectory);
    }

    public Map.Entry<String, OutputVO> changeDirectory(String curDirectory, List<String> parameters) throws IOException {

        if(parameters.size()==0){
            return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO(curDirectory));
        }

        if(parameters.size()>2){
            // 지정된 경로를 찾을 수 없습니다
            return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO("No such file or directory"));
        }

        String destination = parameters.get(0);
        return cdDAO.cd(curDirectory, destination);
    }

    public OutputVO listFiles(String curDirectory, List<String> parameters) throws IOException {

        String source;

        if(parameters.size()>1){
            // 지정된 경로를 찾을 수 없습니다
            new OutputVO("No such file or directory");
        }

        if (parameters.size()==0) {
            source = curDirectory;
        } else {
            source = parameters.get(0);
        }

        return dirDAO.dir(curDirectory, source);
    }

    public OutputVO copyFile(String curDirectory, List<String> parameters) throws IOException {

        if (parameters.size() == 0) {
            return new OutputVO("parameter number is 0");
        }

        if (parameters.size() == 1) {
            return copyDAO.copy(curDirectory, parameters.get(0));
        }

        if (parameters.size() == 2) {
            return copyDAO.copy(curDirectory, parameters.get(0), parameters.get(1));
        }

        // 명령 구문이 올바르지 않습니다
        return new OutputVO("parameter number is wrong");
    }

    public OutputVO moveFile(String curDirectory, List<String> parameters) throws IOException {

        if (parameters.size() == 1) {
            return moveDAO.move(curDirectory, parameters.get(0));
        }

        if(parameters.size() == 2){
            return moveDAO.move(curDirectory, parameters.get(0), parameters.get(1));
        }

        // 명령 구문이 올바르지 않습니다
        return new OutputVO("parameter number is wrong");
    }
}