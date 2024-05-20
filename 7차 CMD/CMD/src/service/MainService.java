package service;

import model.MainModel;
import model.OutputVO;

import java.io.IOException;
import java.nio.file.*;
import java.util.Map;

public class MainService {

    private MainModel mainModel;

    public MainService(FileSystem fileSystem, String rootDirectory) {
        this.mainModel = new MainModel(fileSystem, rootDirectory);
    }

    public Map.Entry<String,OutputVO> changeDirectory(String curDirectory, String destination) throws IOException {
        return mainModel.cd(curDirectory, destination);
    }

    public OutputVO listFiles(String curDirectory, String parameter) throws IOException {

        String source;

        if (parameter.isEmpty()) {
            source = curDirectory;
        } else {
            source = parameter;
        }

        return mainModel.dir(curDirectory, source);
    }

    public OutputVO copyFile(String curDirectory, String parameters) throws IOException {
        String[] parameterArr = parameters.split(" ");

        if (parameterArr.length == 0) {
            return new OutputVO("parameter number is 0");
        }

        if (parameterArr.length == 1) {
            return mainModel.copy(curDirectory, parameterArr[0]);
        }

        if (parameterArr.length == 2) {
            return mainModel.copy(curDirectory, parameterArr[0], parameterArr[1]);
        }

        return new OutputVO("parameter number is wrong");
    }

    public OutputVO moveFile(String curDirectory, String parameters) throws IOException {

        String[] parameterArr = parameters.split(" ");

        if (parameterArr.length == 0) {
            return new OutputVO("parameter number is 0");
        }

        if (parameterArr.length == 1) {
            return mainModel.move(curDirectory, parameterArr[0]);
        }

        if(parameterArr.length == 2){
            return mainModel.move(curDirectory, parameterArr[0], parameterArr[1]);
        }

        return new OutputVO("parameter number is wrong");
    }
}