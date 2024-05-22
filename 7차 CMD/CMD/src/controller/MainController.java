package controller;

import model.VO.DirVO;
import model.VO.InputVO;
import model.VO.MessageVO;
import service.*;
import utility.Validator;
import view.MainView;

import java.io.IOException;
import java.nio.file.FileSystem;
import java.nio.file.FileSystems;
import java.nio.file.Path;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class MainController {

    private String curDirectory;

    private Validator validator;

    private MainView mainView;
    
    private CdService cdService;
    private DirService dirService;
    private MoveService moveService;
    private CopyService copyService;

    public MainController(){
        initializeCMD();

        this.mainView = new MainView();
        this.cdService = new CdService(validator);
        this.dirService = new DirService(validator);
        this.moveService = new MoveService(validator);
        this.copyService = new CopyService(validator);
    }

    // service들에 쓸 validator와 curDirectory 시작 시 초기화
    private void initializeCMD(){
        FileSystem fileSystem = FileSystems.getDefault();

        Iterable<Path> rootDirectories = fileSystem.getRootDirectories();
        
        Iterator<Path> iterator = rootDirectories.iterator();
        Path directory = iterator.next();
        String rootDirectory = directory.toString();

        this.validator = new Validator(fileSystem, rootDirectory);

        this.curDirectory = rootDirectory;
    }

    public void run() throws IOException{
        boolean isCmdRunning = true;
        String command;
        List<String> parameters;

        while(isCmdRunning){
            InputVO input = mainView.getInput(curDirectory);

            command = input.getCommand();
            parameters = input.getParameters();

            switch(command){
                case "cd":
                    curDirectory = execCD(parameters);
                    break;
                case "dir":
                    execDIR(parameters);
                    break;
                case "copy":
                    execCOPY(parameters);
                    break;
                case "move":
                    execMOVE(parameters);
                    break;
                case "help":
                    execHELP();
                    break;
                case "cls":
                    execCLS();
                    break;
                case "exit":
                    isCmdRunning = false;
                    break;
                default:
                    mainView.showWrongCommand(command);
                    break;
            }
        }
    }

    // 바뀔 directory 명이랑 예외가 발생했을 시에 따른 MessageVO 를 CdService에서 pair 객체로 받기
    // pair 객체로 받고 return 하는 놈은 CD 밖에 없음. 얘만 예외임
    private String execCD(List<String> parameters) throws IOException {
        Map.Entry<String, MessageVO> cdResult = cdService.handleCommand(curDirectory, parameters);

        // getValue 로 message 추출 후 view 에 전달
        MessageVO messageVO = cdResult.getValue();
        mainView.printMessageVO(messageVO);

        // getKey 로 바뀐 directory 추출 후 changedDirectory return 해서 curDirectory 최신화
        String changedDirectory = cdResult.getKey();
        return changedDirectory;
    }

    // parameter 로 여러개가 들어오면 여러개의 DirVO를 보내서 출력함
    private void execDIR(List<String> parameters) throws IOException {

        // 인자가 0개이면 대상이 parameter 에 curDirectory 수동 추가
        if (parameters.size()==0) parameters.add(curDirectory);

        // List를 한 개씩 제거하면서 진행
        int parameterLength = parameters.size();
        for(int i=0;i<parameterLength;i++) {
            DirVO dirVO = dirService.handleCommand(curDirectory, parameters);
            mainView.printDirVO(dirVO);
            parameters.remove(0);
            // 맨 마지막 DirVO이고 해당 directory가 존재하지 않을 경우
            if(parameters.size()==0 && dirVO.checkIfDirectoryExists()==false){
                mainView.printMessageVO(new MessageVO("파일을 찾을 수 없습니다\n\n"));
            }
        }
    }

    private void execCOPY(List<String> parameters) throws IOException {
        MessageVO output = copyService.handleCommand(curDirectory, parameters);
        mainView.printMessageVO(output);
    }

    private void execMOVE(List<String> parameters) throws IOException {
        MessageVO output = moveService.handleCommand(curDirectory, parameters);
        mainView.printMessageVO(output);
    }

    private void execHELP(){ mainView.showHelp(); }

    private void execCLS(){

    }
}