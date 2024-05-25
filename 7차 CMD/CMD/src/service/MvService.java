package service;

import constants.OverwriteEnum;
import model.DAO.CpDAO;
import model.DAO.MvDAO;
import model.VO.MessageVO;
import utility.RuntimeExceptionHandler;
import utility.Validator;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

public class MvService extends ActionCmdService<MessageVO> {

    private MvDAO mvDAO;

    public MvService(Validator validator, RuntimeExceptionHandler runtimeExceptionHandler) {
        super(validator, runtimeExceptionHandler);
        this.mvDAO = new MvDAO();
    }

    @Override
    public MessageVO handleCommand(String curDirectory, List<String> parameters) throws IOException {

        // 1. 인자 개수 안맞으면 return
        if (parameters.size() > 2) return new MessageVO("명령 구문이 올바르지 않습니다.\n");

        // 2. 애초에 source가 존재하지 않으면 return
        Path sourcePath = getNormalizedPath(curDirectory, parameters.get(0));
        if (!validator.checkIfDirectoryExists(sourcePath)) {
            return new MessageVO("지정된 파일을 찾을 수 없습니다.\n");
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

        return new MessageVO("IF THIS IS PRINTED SOMETHING IS WRONG");
    }

    //==================================== MOVE NON EXISTING FILE =====================================//

    // move는 destination이 존재하지 않으면 source가 파일이든 폴더이든 그냥 이름만 바꿈. 그래서 바로 mvDAO 호출하고 return하면 됨
    private MessageVO handleNonExistingDestinationMove(Path sourcePath, Path destinationPath) throws IOException{
        boolean isSourceDirectory = validator.checkIfDirectory(sourcePath);

        mvDAO.executeMove(sourcePath, destinationPath);

        if(isSourceDirectory){
            return new MessageVO("1개의 디렉터리를 이동했습니다.\n");
        }
        return new MessageVO("1개의 파일을 이동했습니다.\n");
    }

    //======================================= MOVE FILE TO FILE ========================================//

    private MessageVO handleFileToFileMove(Path sourcePath, Path destinationPath) throws IOException {

        OverwriteEnum permission = askOverwritePermission(sourcePath.toFile(), destinationPath);

        if (permission.equals(OverwriteEnum.NO)) {
            return new MessageVO("0개 파일이 복사되었습니다.\n");
        }

        mvDAO.executeMove(sourcePath, destinationPath);
        return new MessageVO("1개 파일이 복사되었습니다.\n");
    }

    //===================================== MOVE FILE TO DIRECTORY =====================================//

    private MessageVO handleFileToDirectoryMove(Path sourcePath, Path destinationPath) throws IOException {
        OverwriteEnum permission;

        File sourceFile = sourcePath.toFile();

        // destination Directory 폴더 내 파일명들 저장. 겹치는거 있는지 확인해야함
        ArrayList<File> destinationContentsList = getContainingContentsList(destinationPath);

        // 똑같은 파일명이 있고 그게 파일이면 overwrite 여부 물어보기
        for(int i=0;i<destinationContentsList.size();i++){
            File curContent = destinationContentsList.get(i);

            if(curContent.getName().equals(sourceFile.getName()) && curContent.isFile()) {
                permission = askOverwritePermission(sourceFile, destinationPath);
                // NO면 바로 return
                if (permission.equals(OverwriteEnum.NO)) {
                    return new MessageVO("0개 파일을 이동했습니다.\n");
                }
                break;
            }
        }

        mvDAO.executeMove(sourcePath, Paths.get(destinationPath.toString(), sourceFile.getName()));

        return new MessageVO("1개 파일을 이동했습니다.\n");
    }

    //===================================== MOVE DIRECTORY TO FILE =====================================//

    private MessageVO handleDirectoryToFileMove(Path sourcePath, Path destinationPath) throws IOException {

        OverwriteEnum permission = askOverwritePermission(sourcePath.toFile(), destinationPath);

        if (permission.equals(OverwriteEnum.NO)) {
            return new MessageVO("0개 디렉터리를 이동했습니다.\n");
        }

        mvDAO.executeMove(sourcePath, destinationPath);
        return new MessageVO("1개 디렉터리를 이동했습니다.\n");
    }

    //================================== MOVE DIRECTORY TO DIRECTORY ===================================//

    private MessageVO handleDirectoryToDirectoryMove(Path sourcePath, Path destinationPath) throws IOException {
        OverwriteEnum permission = OverwriteEnum.WRONG_INPUT;

        File sourceFile = sourcePath.toFile();

        // destination Directory 폴더 내 있는거 모두 저장. 겹치는거 있는지 확인해야함
        ArrayList<File> destinationContentsList = getContainingContentsList(destinationPath);

        // 똑같은 폴더명이 있고 그게 폴더면 overwrite 여부 물어보기
        for(int i=0;i<destinationContentsList.size();i++){
            File curContent = destinationContentsList.get(i);

            // 똑같은 폴더명이 있고 그게 폴더면 overwrite 여부 물어보기
            // 똑같은게 있으면 절대로 dao까지 가지 않음
            if(curContent.getName().equals(sourceFile.getName()) && curContent.isDirectory()) {
                permission = askOverwritePermission(sourceFile, destinationPath);

                if (permission.equals(OverwriteEnum.NO)) {
                    return new MessageVO("1개 디렉터리를 이동했습니다.\n");
                }else{
                    return new MessageVO("액세스가 거부되었습니다.\n");
                }
            }
        }

        mvDAO.executeMove(sourcePath, Paths.get(destinationPath.toString(), sourceFile.getName()));

        return new MessageVO("1개 디렉터리를 이동했습니다.\n");
    }
}