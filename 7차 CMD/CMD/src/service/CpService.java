package service;

import Constants.OverwriteEnum;
import model.DAO.CpDAO;
import model.VO.MessageVO;
import controller.RuntimeController;
import utility.Validator;

import java.io.File;
import java.io.IOException;
import java.nio.file.*;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class CpService extends CmdService<MessageVO> {

    private CpDAO cpDAO;
    private RuntimeController runtimeController;

    public CpService(Validator validator) {
        super(validator);
        this.cpDAO = new CpDAO();
        this.runtimeController = new RuntimeController();
    }

    private Path getDestinationPath(String curDirectory, Path sourcePath, List<String> parameters) throws IOException {
        Path destinationPath = null;

        if (parameters.size() == 1) {
            destinationPath = Paths.get(curDirectory, String.valueOf(sourcePath.getFileName()));
        }

        if (parameters.size() == 2) {
            destinationPath = getNormalizedPath(curDirectory, parameters.get(1));
        }

        return destinationPath;
    }

    // folder가 가지고 있는 파일들을 List로 return
    // file이면 그냥 자기 자신을 List에 저장해서 return
    private ArrayList<File> getContainingFileList(Path folderPath){
        File source = folderPath.toFile();
        ArrayList<File> containingFileList = new ArrayList<>();

        if(source.isFile()){
            containingFileList.add(source);
        }else{
            containingFileList.addAll(Arrays.asList(source.listFiles()));
        }
        return containingFileList;
    }

    // 특정 파일 내 파일명만 저장한 ArrayList return
    private ArrayList<String> getContainingFileNameList(List<File> fileList){
        
        ArrayList<String> nameList = new ArrayList<>();
        
        for (File curFile : fileList) { 
            nameList.add(curFile.getName()); 
        }
        
        return nameList;
    }

    // 제대로된 입력 받을때까지 overwrite할건지 물어봄
    private OverwriteEnum askOverwritePermission(File curSourceFile, Path destinationPath) throws IOException {

        File destinationFile = destinationPath.toFile();

        OverwriteEnum permission = OverwriteEnum.WRONG_INPUT;

        boolean isValidPermission = false;

        while(!isValidPermission){
            permission = runtimeController.handleOverwritePermission(curSourceFile, Paths.get(destinationFile.getName(), curSourceFile.getName()));

            if(!permission.equals(OverwriteEnum.WRONG_INPUT)){
                isValidPermission = true;
            }
        }
        return permission;
    }

    @Override
    public MessageVO handleCommand(String curDirectory, List<String> parameters) throws IOException {

        // 1. 인자 개수 안맞으면 return
        if (parameters.size() > 2) return new MessageVO("명령 구문이 올바르지 않습니다.\n");

        // 2. 애초에 source가 존재하지 않으면 return
        Path sourcePath = getNormalizedPath(curDirectory, parameters.get(0));
        if (validator.checkIfDirectoryExists(sourcePath) == false) {
            return new MessageVO("지정된 파일을 찾을 수 없습니다.\n");
        }

        Path destinationPath = getDestinationPath(curDirectory, sourcePath, parameters);

        // source 랑 destination이 directory인지 확인
        boolean isSourceDirectory = validator.checkIfDirectory(sourcePath);
        boolean isDestinationDirectory = validator.checkIfDirectory(destinationPath);

        // 그리고 그에 맞는 함수를 호출

        // directory to directory
        if(!isSourceDirectory && !isDestinationDirectory){
            return handleFileToFileCopy(sourcePath, destinationPath);
        }

        // file to directory
        if(!isSourceDirectory && isDestinationDirectory){
            return handleFileToDirectoryCopy(sourcePath, destinationPath);
        }

        // directory to file
        if(isSourceDirectory && !isDestinationDirectory){
            return handleDirectoryToFileCopy(sourcePath, destinationPath);
        }

        // directory to directory
        if(isSourceDirectory && isDestinationDirectory){
            return handleDirectoryToDirectoryCopy(sourcePath, destinationPath);
        }

        return new MessageVO("if this is called something is wrong");
    }

    private MessageVO handleFileToFileCopy(Path sourcePath, Path destinationPath) throws IOException {

        OverwriteEnum permission;
        
        boolean doesDestinationExist = validator.checkIfDirectoryExists(destinationPath);

        // destination이 존재하지 않을때
        // 존재안하면 그냥 새로 하나 만들어주면 됨
        if(!doesDestinationExist){
            cpDAO.executeCopy(sourcePath, destinationPath);
            return new MessageVO("1개 파일이 복사되었습니다.\n");
        }

        // 존재한다면 이미 있는 file overwrite할건지 물어보고 해야함
        // 맞는거 입력할때까지 받기
        permission = askOverwritePermission(sourcePath.toFile(), destinationPath);

        if(permission.equals(OverwriteEnum.NO)) {
            return new MessageVO("0개 파일이 복사되었습니다.");
        }

        cpDAO.executeCopy(sourcePath, destinationPath);
        return new MessageVO("1개 파일이 복사되었습니다.\n");
    }

    private MessageVO handleFileToDirectoryCopy(Path sourcePath, Path destinationPath) throws IOException {

        OverwriteEnum permission;
        
        boolean doesDestinationExist = validator.checkIfDirectoryExists(destinationPath);

        // destination이 존재하지 않을때는 그냥 파일 새로 생성해서 복사되게 하면 됨
        if(!doesDestinationExist){
            cpDAO.executeCopy(sourcePath, destinationPath);
            return new MessageVO("1개 파일이 복사되었습니다.\n");
        }

        File sourceFile = sourcePath.toFile();
        File destinationDirectory = destinationPath.toFile();

        // destination Directory 폴더 내 파일명들 저장. 겹치는거 있는지 확인해야함
        ArrayList<File> destinationContainingFileList = getContainingFileList(destinationPath);
        ArrayList<String> destinationContainingFileNameList = getContainingFileNameList(destinationContainingFileList);
        
        // 똑같은 파일명이 있으면 overwrite 여부 물어보기
        if(destinationContainingFileNameList.contains(sourceFile.getName())){
            permission = askOverwritePermission(sourceFile, destinationPath);
            // NO면 바로 return
            if(permission.equals(OverwriteEnum.NO)) {
                return new MessageVO("0개 파일이 복사되었습니다.");
            }
        }

        // 이미 destination의 존재성이 확인되었으므로 안전하게 copy 가능
        cpDAO.executeCopy(sourcePath, Paths.get(destinationPath.toString(), sourceFile.getName()));

        return new MessageVO("1개 파일이 복사되었습니다.");
    }

    private MessageVO handleDirectoryToFileCopy(Path sourcePath, Path destinationPath) {

        boolean doesDestinationExist = validator.checkIfDirectoryExists(destinationPath);

        // destination이 존재하지 않을때
        if(!doesDestinationExist) {
            return new MessageVO("COPY DIRECTORY TO NON-EXSITING FILE");
        }

        return new MessageVO("COPY DIRECTORY TO EXSITING FILE");
    }

    private MessageVO handleDirectoryToDirectoryCopy(Path sourcePath, Path destinationPath) throws IOException {

        OverwriteEnum permission = OverwriteEnum.WRONG_INPUT;

        boolean doesDestinationExist = validator.checkIfDirectoryExists(destinationPath);
        
        // destination이 존재하지 않을때
        // 이럴때는 Directory to File 이랑 동일할때임
        // handleDirectoryToFileCopy로 넘겨주기
        if (!doesDestinationExist) {
            handleDirectoryToFileCopy(sourcePath, destinationPath);
        }

        // 존재하면 sourceDirectory랑 destinationDirectory 의 파일들을 비교해가면서
        // overwrite 할건지 안할건지 물어봐가면서 진행해야함
        File destinationDirectory = destinationPath.toFile();

        // 각 Directory가 가지고 있는 파일들에 대한 ArrayList<File> 구하기
        ArrayList<File> sourceContainingFileList = getContainingFileList(sourcePath);
        ArrayList<File> destinationContainingFileList = getContainingFileList(destinationPath);

        // destination Directory 폴더 내 파일명들 저장. 겹치는거 있는지 확인해야함
        ArrayList<String> destinationContainingFileNameList = getContainingFileNameList(destinationContainingFileList);

        int copiedCnt = 0;

        for (int i = 0; i < sourceContainingFileList.size(); i++) {
            File curSourceDirectoryFile = sourceContainingFileList.get(i);

            // Directory이면 애초에 볼 필요가 없음 skip
            if (curSourceDirectoryFile.isDirectory()) continue;

            // 똑같은 파일명이 있으면 overwrite 여부 물어보기
            if (destinationContainingFileNameList.contains(curSourceDirectoryFile.getName())) {
                permission = askOverwritePermission(curSourceDirectoryFile, destinationPath);
            }
            // NO면 skip
            if(permission.equals(OverwriteEnum.NO)) continue;

            cpDAO.executeCopy(curSourceDirectoryFile.toPath(), Paths.get(destinationPath.toString(), curSourceDirectoryFile.getName()));

            copiedCnt++;
        }

        return new MessageVO(copiedCnt + " 개 파일이 복사되었습니다.");
    }
}