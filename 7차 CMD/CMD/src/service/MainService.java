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
    private MoveDAO moveDAO;

    public MainService(FileSystem fileSystem, String rootDirectory) {
        this.cdDAO = new CdDAO(fileSystem, rootDirectory);
        this.copyDAO = new CopyDAO(fileSystem, rootDirectory);
        this.moveDAO = new MoveDAO(fileSystem, rootDirectory);
    }

    public OutputVO copyFile(String curDirectory, List<String> parameters) throws IOException {

        if (parameters.size() == 1) {
            return copyDAO.copy(curDirectory, parameters.get(0));
        }

        if (parameters.size() == 2) {
            return copyDAO.copy(curDirectory, parameters.get(0), parameters.get(1));
        }

        return new OutputVO("명령 구문이 올바르지 않습니다");
    }
}