package service;

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

    // destination이 존재하지 않으면 새로 만들어야함
    // 근데 만드는건 어차피 Files.copy가 자동으로 해줘서 신경안써도 됨
    private MessageVO createNewAndCopy(Path sourcePath, Path destinationPath) throws IOException {
        // sourceFile이 파일이면
        if(sourcePath.toFile().isFile()) {
            cpDAO.executeCopy(sourcePath, destinationPath);
            return new MessageVO("1 copied complete");
        }
        // sourceFile이 디렉토리이면
        else{
            return new MessageVO("HAVE TO CODE THIS ONE");
        }
    }

    // 여기서부터는 source 와 destination 둘 다 존재할때
    // 파일 to 파일
    // 파일 to 디렉터리 (비어있든 말든 다 됨)
    // 디렉터리 to 파일
    // 디렉터리 to 디렉터리
    private MessageVO getOriginalAndCopy(Path sourcePath, Path destinationPath) throws IOException {

        File sourceFile = sourcePath.toFile();
        File destinationFile = destinationPath.toFile();

        // source 와 destination 이 가지고 있는 파일들에 대한 ArrayList<File> 구하기
        ArrayList<File> sourceContainingFileList = getContainingFileList(sourcePath);
        ArrayList<File> destinationContainingFileList = getContainingFileList(destinationPath);

        // destination 폴더 내 파일명들 저장. 겹치는거 있는지 확인해야함
        ArrayList<String> destinationContainingFileNameList = getContainingFileNameList(destinationContainingFileList);

        int copiedCnt = 0;
        for(int i=0;i<sourceContainingFileList.size();i++){
            File curFile = sourceContainingFileList.get(i);

            // Directory이면 애초에 볼 필요가 없음 skip
            if(curFile.isDirectory()) continue;

            // destination도 똑같은 파일명을 가지고 있으면 overwrite 할건지 물어봐야함
            // 그래서 똑같은 파일명이 있으면 진짜로 물어보기
            if(destinationContainingFileNameList.contains(curFile.getName())){
                // 만약 거절했으면 skip
                if(runtimeController.handleOverwrite(Paths.get(destinationFile.getName(),curFile.getName()))==false){
                    continue;
                }
            }

            // destination이 directory인지 확인
            boolean isDestinationDirectory = validator.checkIfDirectory(destinationPath);
            
            // 만약 destination이 directory이면 destination이 destination/파일명 이어야함
            // 마약 destination이 file이면 destination이 그냥 destination명 그 자체여야함 이름을 바꾸는것도 아니고 이게 폴더도 아니기 때문
            if(isDestinationDirectory) {
                cpDAO.executeCopy(curFile.toPath(), Paths.get(destinationPath.toString(), curFile.getName()));
            }else{
                cpDAO.executeCopy(curFile.toPath(), Paths.get(destinationPath.toString()));
            }

            copiedCnt++;
        }
        return new MessageVO(copiedCnt+" copied complete");
    }


    // 상대경로로 들어오면 안됨 copy newfolder/aaa.txt b.txt 하면
    // newfolder에 있는 파일 전체가 다 복사됨
    // 근데 C:로 aaa.txt의 경로가 들어오면 또 잘먹힘
    @Override
    public MessageVO handleCommand(String curDirectory, List<String> parameters) throws IOException {

        // 1. 인자 개수 안맞으면 return
        if (parameters.size() > 2) return new MessageVO("명령 구문이 올바르지 않습니다.\n");

        // 2. 애초에 source가 존재하지 않으면 return
        Path sourcePath = getNormalizedPath(curDirectory, parameters.get(0));
        if (validator.checkIfDirectoryExists(sourcePath) == false) {
            return new MessageVO("지정된 파일을 찾을 수 없습니다.\n");
        }

        // 3. destination이 애초에 존재하는건지 확인을 하고
        // 존재성에 맞는 함수 호출
        Path destinationPath = getDestinationPath(curDirectory, sourcePath, parameters);

        if(validator.checkIfDirectoryExists(destinationPath)){
            return getOriginalAndCopy(sourcePath, destinationPath);
        }else{
            return createNewAndCopy(sourcePath, destinationPath);
        }
    }
}