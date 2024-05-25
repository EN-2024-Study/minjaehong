package service;

import constants.OverwriteEnum;
import utility.RuntimeExceptionHandler;
import utility.Validator;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public abstract class ActionCmdService<ReturnVO> extends CmdService<ReturnVO>{

    private RuntimeExceptionHandler runtimeExceptionHandler;

    protected ActionCmdService(Validator validator, RuntimeExceptionHandler runtimeExceptionHandler) {
        super(validator);
        this.runtimeExceptionHandler = runtimeExceptionHandler;
    }

    protected Path getDestinationPath(String curDirectory, Path sourcePath, List<String> parameters) throws IOException {
        Path destinationPath = null;

        if (parameters.size() == 1) {
            destinationPath = Paths.get(curDirectory);
        }

        if (parameters.size() == 2) {
            destinationPath = getNormalizedPath(curDirectory, parameters.get(1));
        }

        return destinationPath;
    }

    // Directory가 가지고 있는 Directory들을 List로 return
    // file이면 그냥 자기 자신을 List에 저장해서 return
    // folder면 file folder 모두 저장해서 return
    protected ArrayList<File> getContainingContentsList(Path folderPath) {

        File source = folderPath.toFile();
        ArrayList<File> contentsList = new ArrayList<>();

        if (source.isFile()) {
            contentsList.add(source);
        } else {
            contentsList.addAll(Arrays.asList(source.listFiles()));
        }
        return contentsList;
    }

    // 특정 Directory 내 Directory 이름들만 저장한 ArrayList return
    protected ArrayList<String> getContainingFileAndFolderNameList(List<File> fileList) {

        ArrayList<String> nameList = new ArrayList<>();

        for (File curFile : fileList) {
            nameList.add(curFile.getName());
        }

        return nameList;
    }

    protected void showCurWorkingFile(String curWorkingDirectoryName) throws IOException {
        runtimeExceptionHandler.showCurWorkingFile(curWorkingDirectoryName);
    }

    // runtimeController를 통해 제대로된 입력 받을때까지 overwrite할건지 물어봄
    protected OverwriteEnum askOverwritePermission(File curSourceFile, Path destinationPath) throws IOException {

        File destinationFile = destinationPath.toFile();

        OverwriteEnum permission = OverwriteEnum.WRONG_INPUT;

        boolean isValidPermission = false;

        while (!isValidPermission) {
            permission = runtimeExceptionHandler.handleOverwritePermission(curSourceFile, Paths.get(destinationFile.getName(), curSourceFile.getName()));

            if (!permission.equals(OverwriteEnum.WRONG_INPUT)) {
                isValidPermission = true;
            }
        }
        return permission;
    }
}
