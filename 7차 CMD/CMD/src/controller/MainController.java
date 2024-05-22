package controller;

import model.VO.InputVO;
import model.VO.OutputVO;
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

    public void run() throws IOException, InterruptedException {
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

    private String execCD(List<String> parameters) throws IOException {
        // 바뀔 directory 명이랑 예외가 발생했을 시에 따른 OutputVO 를 pair 객체로 받기
        // pair 객체로 return 하는 놈은 CD 밖에 없음. 얘만 예외임
        Map.Entry<String, OutputVO> cdResult = cdService.handleCd(curDirectory, parameters);

        // getValue 로 exceptionMessage 추출 후 view 에 전달
        OutputVO exceptionMessageVO = cdResult.getValue();
        mainView.printReturnedResult(exceptionMessageVO);

        // getKey 로 바뀐 directory 추출 후 changedDirectory return 해서 curDirectory 최신화
        String changedDirectory = cdResult.getKey();
        return changedDirectory;
    }

    private void execDIR(List<String> parameters) throws IOException {
        OutputVO output = dirService.handleCommand(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void execCOPY(List<String> parameters) throws IOException {
        OutputVO output = copyService.handleCommand(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void execMOVE(List<String> parameters) throws IOException {
        OutputVO output = moveService.handleCommand(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void execHELP(){ mainView.showHelp(); }

    private void execCLS() throws IOException, InterruptedException {
        mainView.clearPrompt();
    }
}