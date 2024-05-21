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

    private FileSystem fileSystem;
    private String rootDirectory;
    private String curDirectory;

    private MainView mainView;
    private MainService mainService;

    public MainController(){
        initializeCMD();

        mainView = new MainView();
        mainService = new MainService(fileSystem, rootDirectory);
    }

    // OS에 따른 FileSystem, rootDirectory, seperator 지정해주기
    private void initializeCMD(){
        fileSystem = FileSystems.getDefault();

        Iterable<Path> rootDirectories = fileSystem.getRootDirectories();
        
        Iterator<Path> iterator = rootDirectories.iterator();
        Path directory = iterator.next();
        rootDirectory = directory.toString();

        // 현재 directory rootDirectory 로 최신화
        curDirectory = rootDirectory;
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
                    execCD(parameters);
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
                    break;
            }
        }
    }

    private void execCD(List<String> parameters) throws IOException {
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

    private void execDIR(List<String> parameters) throws IOException {
        OutputVO output = mainService.listDirectory(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void execCOPY(List<String> parameters) throws IOException {
        OutputVO output = mainService.copyFile(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void execMOVE(List<String> parameters) throws IOException {
        OutputVO output = mainService.moveFile(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void execHELP(){ mainView.showHelp(); }

    private void execCLS() throws IOException, InterruptedException {
        mainView.clearPrompt();
    }
}