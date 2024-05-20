package service;

import model.DAO.CdDAO;
import model.DAO.CopyDAO;
import model.DAO.DirDAO;
import model.DAO.MoveDAO;
import model.VO.OutputVO;

import java.io.IOException;
import java.nio.file.*;
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

    public Map.Entry<String, OutputVO> changeDirectory(String curDirectory, String destination) throws IOException {
        return cdDAO.cd(curDirectory, destination);
    }

    public OutputVO listFiles(String curDirectory, String parameter) throws IOException {

        String source;

        if (parameter.isEmpty()) {
            source = curDirectory;
        } else {
            source = parameter;
        }

        return dirDAO.dir(curDirectory, source);
    }

    public OutputVO copyFile(String curDirectory, String parameters) throws IOException {
        String[] parameterArr = parameters.split(" ");

        if (parameterArr.length == 0) {
            return new OutputVO("parameter number is 0");
        }

        if (parameterArr.length == 1) {
            return copyDAO.copy(curDirectory, parameterArr[0]);
        }

        if (parameterArr.length == 2) {
            return copyDAO.copy(curDirectory, parameterArr[0], parameterArr[1]);
        }

        return new OutputVO("parameter number is wrong");
    }

    public OutputVO moveFile(String curDirectory, String parameters) throws IOException {

        String[] parameterArr = parameters.split(" ");

        if (parameterArr.length == 0) {
            return new OutputVO("parameter number is 0");
        }

        if (parameterArr.length == 1) {
            return moveDAO.move(curDirectory, parameterArr[0]);
        }

        if(parameterArr.length == 2){
            return moveDAO.move(curDirectory, parameterArr[0], parameterArr[1]);
        }

        return new OutputVO("parameter number is wrong");
    }
}