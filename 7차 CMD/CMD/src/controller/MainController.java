package controller;

import model.InputVO;
import model.OutputVO;
import service.MainService;
import view.MainView;

import java.io.File;
import java.io.IOException;
import java.nio.file.FileSystem;
import java.nio.file.FileSystems;
import java.nio.file.Path;
import java.util.Iterator;

public class MainController {

    private String rootDirectory;
    private FileSystem fileSystem;
    private String separator;

    private String curDirectory;

    private MainView mainView;
    private MainService mainService;
    
    private String command;
    private String parameters;

    public MainController(){
        initializeCMD();

        mainView = new MainView();
        mainService = new MainService(fileSystem, rootDirectory);
    }

    // OS에 따른 FileSystem, rootDirectory, seperator 지정해주기
    private void initializeCMD(){
        fileSystem = FileSystems.getDefault();

        separator = fileSystem.getSeparator();

        Iterable<Path> rootDirectories = fileSystem.getRootDirectories();
        
        Iterator<Path> iterator = rootDirectories.iterator();
        Path directory = iterator.next();
        rootDirectory = directory.toString();
        System.out.println(rootDirectory);
        System.out.println(separator);

        curDirectory = rootDirectory;
    }

    private void handleCD(String parameters) throws IOException {
        curDirectory = mainService.changeDirectory(curDirectory, parameters);
    }

    private void handleDIR(String parameters) throws IOException {
        OutputVO output = mainService.listFiles(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void handleCOPY(String parameters) throws IOException {
        OutputVO output = mainService.copyFile(curDirectory, parameters);
        mainView.printReturnedResult(output);
    }

    private void handleMOVE(String parameters){
        mainService.moveFile(parameters);
    }

    private void handleHELP(){ mainView.showHelp(); }

    private void handleCLS(){
        mainView.clearPrompt();
    }

    public void run() throws IOException {
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
}