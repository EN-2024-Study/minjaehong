package service;

import model.MainModel;
import model.OutputVO;

import java.io.IOException;
import java.nio.file.*;

public class MainService {

    private MainModel mainModel;

    public MainService(FileSystem fileSystem, String rootDirectory) {
        this.mainModel = new MainModel(fileSystem, rootDirectory);
    }

    public String changeDirectory(String curDirectory, String directoryPath) throws IOException {

        String changedDirectory = mainModel.getCanonicalPath(curDirectory, directoryPath);

        // 실제로 존재하면 바뀐 directory return 아니면 기존꺼 그대로 return
        if (mainModel.checkIfDirectoryExists(changedDirectory) == true) {
            return changedDirectory;
        }
        else {
            return curDirectory;
        }
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

    // File 클래스에 replace 함수가 있음
    // copy 인자1 인자2
    // copy 인자1

    // 덮어쓰겠습니다를 먼저 호출?

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