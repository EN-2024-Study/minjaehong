package controller;

import model.VO.InputVO;
import model.VO.OutputVO;
import service.MainService;
import view.MainView;

import java.io.IOException;
import java.nio.file.FileSystem;
import java.nio.file.FileSystems;
import java.nio.file.Path;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class MainController {

    private String rootDirectory;
    private FileSystem fileSystem;
    private String seperator;

    private String curDirectory;

    private MainView mainView;
    private MainService mainService;
    
    private String command;
    private List<String> parameters;

    public MainController(){
        initializeCMD();

        mainView = new MainView();
        mainService = new MainService(fileSystem, rootDirectory);
    }

    // OS에 따른 FileSystem, rootDirectory, seperator 지정해주기
    private void initializeCMD(){
        fileSystem = FileSystems.getDefault();

        seperator = fileSystem.getSeparator();

        System.out.println(seperator);

        Iterable<Path> rootDirectories = fileSystem.getRootDirectories();
        
        Iterator<Path> iterator = rootDirectories.iterator();
        Path directory = iterator.next();
        rootDirectory = directory.toString();

        // 현재 directory rootDirectory 로 최신화
        curDirectory = rootDirectory;
    }

    // file folder 명에 공백 있을 경우 "" 로 감싸줘야함!
    public void run() throws IOException, InterruptedException {
        boolean isCmdRunning = true;

        while(isCmdRunning){
            InputVO input = mainView.getInput(curDirectory);

            command = input.getCommand();
            parameters = input.getParameters();

            switch(command){
                case "cd":
                    handleCD(parameters);
                    break;
                case "dir":
                    handleDIR(parameters);
                    break;
                case "copy":
                    handleCOPY(parameters);
                    break;
                case "move":
                    handleMOVE(parameters);
                    break;
                case "help":
                    handleHELP();
                    break;
                case "cls":
                    handleCLS();
                    break;
                case "exit":
                    isCmdRunning = false;
                    break;
                default:
                    break;
            }
        }
    }

    private void handleCD(List<String> parameters) throws IOException {
        // 바뀔 directory 명이랑 예외가 발생했을 시에 따른 OutputVO 를 pair 객체로 받기
        // pair 객체로 return 하는 놈은 CD 밖에 없음. 얘만 예외임
        Map.Entry<String, OutputVO> cdResult = mainService.changeDirectory(curDirectory, parameters);

        // curDirectory 최신화
        String changedDirectory = cdResult.getKey();
        curDirectory = changedDirectory;
        
        // OutputVO view에 출력
        OutputVO exceptionMessageVO = cdResult.getValue();
        mainView.printReturnedResult(exceptionMessageVO);
    }

    private void handleDIR(List<String> parameters) throws IOException {
        OutputVO output = mainService.listDirectory(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void handleCOPY(List<String> parameters) throws IOException {
        OutputVO output = mainService.copyFile(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void handleMOVE(List<String> parameters) throws IOException {
        OutputVO output = mainService.moveFile(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void handleHELP(){ mainView.showHelp(); }

    private void handleCLS() throws IOException, InterruptedException {
        mainView.clearPrompt();
    }
}