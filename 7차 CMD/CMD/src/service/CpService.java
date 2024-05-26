package service;

import constants.Constants;
import constants.OverwriteEnum;
import model.DAO.CpDAO;
import model.VO.MessageVO;
import utility.RuntimeExceptionHandler;
import utility.Validator;

import java.io.*;
import java.nio.file.*;
import java.util.ArrayList;
import java.util.List;

public class CpService extends ActionCmdService<MessageVO> {

    private CpDAO cpDAO;

    public CpService(Validator validator, RuntimeExceptionHandler runtimeExceptionHandler) {
        super(validator, runtimeExceptionHandler);
        this.cpDAO = new CpDAO();
    }

    @Override
    public MessageVO handleCommand(String curDirectory, List<String> parameters) throws IOException {

        // 1. 인자 개수 안맞으면 return
        if (parameters.size() < 1 || parameters.size() > 2) return new MessageVO(Constants.WRONG_COMMAND);

        // 2. 애초에 source가 존재하지 않으면 return
        Path sourcePath = getNormalizedPath(curDirectory, parameters.get(0));
        if (!validator.checkIfDirectoryExists(sourcePath)) {
            return new MessageVO(Constants.CANT_FIND_CERTAIN_FILE);
        }

        // 인자 1개이든 2개이든 올바른 destinationPath를 반환
        Path destinationPath = getDestinationPath(curDirectory, parameters);

        // source 랑 destination이 directory인지 확인
        boolean isSourceDirectory = validator.checkIfDirectory(sourcePath);
        boolean isDestinationDirectory = validator.checkIfDirectory(destinationPath);
        boolean isSourceFile = !isSourceDirectory;
        boolean isDestinationFile = !isDestinationDirectory;

        // 그리고 그에 맞는 하위 handle 함수를 호출

        // FILE TO FILE
        if (isSourceFile && isDestinationFile) {
            return handleFileToFileCopy(sourcePath, destinationPath);
        }

        // FILE TO DIRECTORY
        if (isSourceFile && isDestinationDirectory) {
            return handleFileToDirectoryCopy(sourcePath, destinationPath);
        }

        // DIRECTORY TO FILE
        if (isSourceDirectory && isDestinationFile) {
            return handleDirectoryToFileCopy(sourcePath, destinationPath);
        }

        // DIRECTORY TO DIRECTORY
        if (isSourceDirectory && isDestinationDirectory) {
            return handleDirectoryToDirectoryCopy(sourcePath, destinationPath);
        }

        return new MessageVO("IF THIS IS PRINTED SOMETHING IS WRONG");
    }

    //======================================= COPY FILE TO FILE ========================================//

    private MessageVO handleFileToFileCopy(Path sourcePath, Path destinationPath) throws IOException {

        boolean doesDestinationExist = validator.checkIfDirectoryExists(destinationPath);

        if (!doesDestinationExist) {
            return handleFileToNonExistingFile(sourcePath, destinationPath);
        }

        return handleFileToExistingFile(sourcePath, destinationPath);
    }

    private MessageVO handleFileToNonExistingFile(Path sourcePath, Path destinationPath) throws IOException {
        cpDAO.executeCopy(sourcePath, destinationPath);
        return new MessageVO(Constants.ONE_FILE_COPIED);
    }

    private MessageVO handleFileToExistingFile(Path sourcePath, Path destinationPath) throws IOException {

        // 같은 파일일때
        if (sourcePath.equals(destinationPath)) {
            return new MessageVO(Constants.CANT_COPY_TO_SAME_FILE);
        }

        // 존재한다면 이미 있는 file overwrite할건지 물어보고 해야함
        OverwriteEnum permission = askOverwritePermission(sourcePath.toFile(), destinationPath);

        if (permission.equals(OverwriteEnum.NO)) {
            return new MessageVO(Constants.ZERO_FILE_COPIED);
        }

        cpDAO.executeCopy(sourcePath, destinationPath);

        return new MessageVO(Constants.ONE_FILE_COPIED);
    }

    //===================================== COPY FILE TO DIRECTORY =====================================//

    private MessageVO handleFileToDirectoryCopy(Path sourcePath, Path destinationPath) throws IOException {

        boolean doesDestinationExist = validator.checkIfDirectoryExists(destinationPath);

        if (!doesDestinationExist) {
            return handleFileToNonExistingDirectoryCopy(sourcePath, destinationPath);
        }

        return handleFileToExistingDirectoryCopy(sourcePath, destinationPath);
    }

    // destination이 존재하지 않을때는 그냥 파일 새로 생성해서 복사되게 하면 됨
    private MessageVO handleFileToNonExistingDirectoryCopy(Path sourcePath, Path destinationPath) throws IOException {
        cpDAO.executeCopy(sourcePath, destinationPath);
        return new MessageVO(Constants.ONE_FILE_COPIED);
    }

    private MessageVO handleFileToExistingDirectoryCopy(Path sourcePath, Path destinationPath) throws IOException {

        OverwriteEnum permission = OverwriteEnum.WRONG_INPUT;

        File sourceFile = sourcePath.toFile();

        // destination 폴더 내 있는 것들 저장. 겹치는거 있는지 확인해야함
        ArrayList<File> destinationContentsList = getContainingContentsList(destinationPath);

        // 똑같은 파일명이 있으면 overwrite 여부 물어보기
        for(int i=0;i<destinationContentsList.size();i++){
            File curContent = destinationContentsList.get(i);

            if(curContent.getName().equals(sourceFile.getName()) && curContent.isFile()){
                permission = askOverwritePermission(sourceFile, destinationPath);
                // NO면 바로 return
                if (permission.equals(OverwriteEnum.NO)) {
                    return new MessageVO(Constants.ZERO_FILE_COPIED);
                }
                break;
            }
        }

        cpDAO.executeCopy(sourcePath, Paths.get(destinationPath.toString(), sourceFile.getName()));

        return new MessageVO(Constants.ONE_FILE_COPIED);
    }

    //===================================== COPY DIRECTORY TO FILE =====================================//

    private MessageVO handleDirectoryToFileCopy(Path sourcePath, Path destinationPath) throws IOException {

        boolean doesDestinationExist = validator.checkIfDirectoryExists(destinationPath);

        if (!doesDestinationExist) {
            return handleDirectoryToNonExistingFileCopy(sourcePath, destinationPath);
        }

        return handleDirectoryToExistingFileCopy(sourcePath, destinationPath);
    }

    private MessageVO handleDirectoryToNonExistingFileCopy(Path sourcePath, Path destinationPath) throws IOException {
        StringBuilder sb = new StringBuilder();

        ArrayList<File> sourceContentsList = getContainingContentsList(sourcePath);

        BufferedWriter bufferedWriter = new BufferedWriter(new FileWriter(destinationPath.toString()));
        BufferedReader bufferedReader;

        for (File curSourceFile : sourceContentsList) {
            if (curSourceFile.isDirectory()) continue;

            showCurWorkingFile(curSourceFile.getParentFile().getName() + "\\" + curSourceFile.getName() + "\n");

            bufferedReader = new BufferedReader(new FileReader(curSourceFile));
            String line;
            while ((line = bufferedReader.readLine()) != null) {
                bufferedWriter.write(line);
            }
            bufferedReader.close();
        }

        bufferedWriter.close();
        sb.append(Constants.ONE_FILE_COPIED);
        return new MessageVO(sb.toString());
    }

    private MessageVO handleDirectoryToExistingFileCopy(Path sourcePath, Path destinationPath) throws IOException {
        StringBuilder sb = new StringBuilder();

        ArrayList<File> sourceFileAndFolderList = getContainingContentsList(sourcePath);

        BufferedWriter bufferedWriter = new BufferedWriter(new FileWriter(destinationPath.toString()));
        BufferedReader bufferedReader;

        OverwriteEnum permission = OverwriteEnum.WRONG_INPUT;
        int copiedCnt = 0;
        for (File curSourceFile : sourceFileAndFolderList) {
            if (curSourceFile.isDirectory()) continue;

            showCurWorkingFile(curSourceFile.getParentFile().getName() + "\\" + curSourceFile.getName() + "\n");

            // ALL이 아닐때만 물어보기
            if (!permission.equals(OverwriteEnum.ALL)) {
                permission = askOverwritePermission(curSourceFile,destinationPath);

                if(permission.equals(OverwriteEnum.NO)) continue;
            }

            bufferedReader = new BufferedReader(new FileReader(curSourceFile));
            String line;
            while ((line = bufferedReader.readLine()) != null) {
                bufferedWriter.write(line);
            }
            bufferedReader.close();
            copiedCnt++;
        }

        bufferedWriter.close();
        sb.append(copiedCnt + Constants.N_FILE_COPIED);
        return new MessageVO(sb.toString());
    }

    //===================================== COPY FILE TO DIRECTORY =====================================//

    private MessageVO handleDirectoryToDirectoryCopy(Path sourcePath, Path destinationPath) throws IOException {

        boolean doesDestinationExist = validator.checkIfDirectoryExists(destinationPath);

        // destination이 존재하지 않을때. 이럴때는 Directory to File 이랑 동일할때임
        if (!doesDestinationExist) {
            handleDirectoryToNonExistingDirectoryCopy(sourcePath, destinationPath);
        }

        return handleDirectoryToExistingDirectoryCopy(sourcePath, destinationPath);
    }

    private MessageVO handleDirectoryToNonExistingDirectoryCopy(Path sourcePath, Path destinationPath) throws IOException {
        return handleDirectoryToFileCopy(sourcePath, destinationPath);
    }

    private MessageVO handleDirectoryToExistingDirectoryCopy(Path sourcePath, Path destinationPath) throws IOException {

        OverwriteEnum permission = OverwriteEnum.WRONG_INPUT;

        // source Directory 내에 있는거 받기
        ArrayList<File> sourceContainingFileList = getContainingContentsList(sourcePath);

        // destination Directory 내에 있는거 받기
        ArrayList<File> destinationContainingFileList = getContainingContentsList(destinationPath);
        ArrayList<String> destinationContainingFileNameList = getContainingFileAndFolderNameList(destinationContainingFileList);

        int copiedCnt = 0;

        for (int i = 0; i < sourceContainingFileList.size(); i++) {
            File curSourceDirectoryFile = sourceContainingFileList.get(i);

            // Directory이면 애초에 볼 필요가 없음 skip
            if (curSourceDirectoryFile.isDirectory()) continue;

            showCurWorkingFile(curSourceDirectoryFile.getParentFile().getName() + "\\" + curSourceDirectoryFile.getName() + "\n");

            // 똑같은 파일명이 있으면 overwrite 여부 물어보기
            // 근데 permission이 ALL이 아닐때만 물어보면 됨
            if (!permission.equals(OverwriteEnum.ALL)) {
                if (destinationContainingFileNameList.contains(curSourceDirectoryFile.getName())) {
                    permission = askOverwritePermission(curSourceDirectoryFile, destinationPath);
                }
                // NO면 skip
                if (permission.equals(OverwriteEnum.NO)) continue;
            }

            if (sourcePath.equals(destinationPath)) {
                return new MessageVO(Constants.CANT_COPY_TO_SAME_FILE);
            }

            cpDAO.executeCopy(curSourceDirectoryFile.toPath(), Paths.get(destinationPath.toString(), curSourceDirectoryFile.getName()));

            copiedCnt++;
        }

        return new MessageVO(copiedCnt + Constants.N_FILE_COPIED);
    }
}