package service;

import constant.Constants;
import constant.OverwriteEnum;
import model.DAO.MvDAO;
import model.DTO.MessageDTO;
import utility.RuntimeController;
import utility.Validator;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

public class MvService extends ActionCmdService<MessageDTO> {

    private final MvDAO mvDAO;

    public MvService(Validator validator, RuntimeController runtimeController) {
        super(validator, runtimeController);
        this.mvDAO = new MvDAO();
    }

    @Override
    public MessageDTO handleCommand(String curDirectory, List<String> parameters) throws IOException {

        // 1. 인자 개수 안맞으면 return
        if (parameters.size() < 1 || parameters.size() > 2) return new MessageDTO(Constants.WRONG_COMMAND);

        // 2. 애초에 source가 존재하지 않으면 return
        Path sourcePath = getNormalizedPath(curDirectory, parameters.get(0));
        if (!validator.checkIfDirectoryExists(sourcePath)) {
            return new MessageDTO(Constants.CANT_FIND_CERTAIN_FILE);
        }

        // 인자 1개이든 2개이든 올바른 destinationPath를 반환
        Path destinationPath = getDestinationPath(curDirectory, parameters);

        // destination이 존재하지 않을때
        if(!validator.checkIfDirectoryExists(destinationPath)){
            return handleNonExistingDestinationMove(sourcePath, destinationPath);
        }

        // 여기까지 왔으면 이제 source destination은 둘 다 존재하는거임
        // source랑 destination이 directory인지 확인
        boolean isSourceDirectory = validator.checkIfDirectory(sourcePath);
        boolean isDestinationDirectory = validator.checkIfDirectory(destinationPath);
        boolean isSourceFile = !isSourceDirectory;
        boolean isDestinationFile = !isDestinationDirectory;

        // 그리고 그에 맞는 하위 handle 함수를 호출

        // FILE TO FILE
        if (isSourceFile && isDestinationFile) {
            return handleFileToFileMove(sourcePath, destinationPath);
        }

        // FILE TO DIRECTORY
        if (isSourceFile && isDestinationDirectory) {
            return handleFileToDirectoryMove(sourcePath, destinationPath);
        }

        // DIRECTORY TO FILE
        if (isSourceDirectory && isDestinationFile) {
            return handleDirectoryToFileMove(sourcePath, destinationPath);
        }

        // DIRECTORY TO DIRECTORY
        if (isSourceDirectory && isDestinationDirectory) {
            return handleDirectoryToDirectoryMove(sourcePath, destinationPath);
        }

        return new MessageDTO("IF THIS IS PRINTED SOMETHING IS WRONG");
    }

    //==================================== MOVE NON EXISTING FILE =====================================//

    // move는 destination이 존재하지 않으면 source가 파일이든 폴더이든 그냥 이름만 바꿈. 그래서 바로 mvDAO 호출하고 return하면 됨
    private MessageDTO handleNonExistingDestinationMove(Path sourcePath, Path destinationPath) throws IOException{
        boolean isSourceDirectory = validator.checkIfDirectory(sourcePath);

        mvDAO.executeMove(sourcePath, destinationPath);

        if(isSourceDirectory){
            return new MessageDTO(Constants.ONE_DIRECTORY_MOVED);
        }
        return new MessageDTO(Constants.ONE_FILE_MOVED);
    }

    //======================================= MOVE FILE TO FILE ========================================//

    private MessageDTO handleFileToFileMove(Path sourcePath, Path destinationPath) throws IOException {

        OverwriteEnum permission = askOverwritePermission(sourcePath.toFile(), destinationPath);

        if (permission.equals(OverwriteEnum.NO)) {
            return new MessageDTO(Constants.ZERO_FILE_MOVED);
        }

        mvDAO.executeMove(sourcePath, destinationPath);
        return new MessageDTO(Constants.ONE_FILE_MOVED);
    }

    //===================================== MOVE FILE TO DIRECTORY =====================================//

    private MessageDTO handleFileToDirectoryMove(Path sourcePath, Path destinationPath) throws IOException {
        OverwriteEnum permission;

        File sourceFile = sourcePath.toFile();

        // destination Directory 폴더 내 파일명들 저장. 겹치는거 있는지 확인해야함
        ArrayList<File> destinationContentsList = getContainingContentsList(destinationPath);

        // 똑같은 파일명이 있고 그게 파일이면 overwrite 여부 물어보기
        for (File curContent : destinationContentsList) {
            if (curContent.getName().equals(sourceFile.getName()) && curContent.isFile()) {
                permission = askOverwritePermission(sourceFile, destinationPath);
                // NO면 바로 return
                if (permission.equals(OverwriteEnum.NO)) {
                    return new MessageDTO(Constants.ZERO_FILE_MOVED);
                }
                break;
            }
        }

        mvDAO.executeMove(sourcePath, Paths.get(destinationPath.toString(), sourceFile.getName()));

        return new MessageDTO(Constants.ONE_FILE_MOVED);
    }

    //===================================== MOVE DIRECTORY TO FILE =====================================//

    private MessageDTO handleDirectoryToFileMove(Path sourcePath, Path destinationPath) throws IOException {

        OverwriteEnum permission = askOverwritePermission(sourcePath.toFile(), destinationPath);

        if (permission.equals(OverwriteEnum.NO)) {
            return new MessageDTO(Constants.ZERO_DIRECTORY_MOVED);
        }

        mvDAO.executeMove(sourcePath, destinationPath);
        return new MessageDTO(Constants.ONE_DIRECTORY_MOVED);
    }

    //================================== MOVE DIRECTORY TO DIRECTORY ===================================//

    private MessageDTO handleDirectoryToDirectoryMove(Path sourcePath, Path destinationPath) throws IOException {
        OverwriteEnum permission = OverwriteEnum.WRONG_INPUT;

        File sourceFile = sourcePath.toFile();

        // destination Directory 폴더 내 있는거 모두 저장. 겹치는거 있는지 확인해야함
        ArrayList<File> destinationContentsList = getContainingContentsList(destinationPath);

        // 똑같은 폴더명이 있고 그게 폴더면 overwrite 여부 물어보기
        for (File curContent : destinationContentsList) {
            // 똑같은 폴더명이 있고 그게 폴더면 overwrite 여부 물어보기
            // 똑같은게 있으면 절대로 dao까지 가지 않음
            if (curContent.getName().equals(sourceFile.getName()) && curContent.isDirectory()) {
                permission = askOverwritePermission(sourceFile, destinationPath);

                if (permission.equals(OverwriteEnum.NO)) {
                    return new MessageDTO(Constants.ONE_DIRECTORY_MOVED);
                } else {
                    return new MessageDTO(Constants.ACCESS_DENIED);
                }
            }
        }

        mvDAO.executeMove(sourcePath, Paths.get(destinationPath.toString(), sourceFile.getName()));

        return new MessageDTO(Constants.ONE_DIRECTORY_MOVED);
    }
}