package controller;

import model.VO.DirVO;
import model.VO.InputVO;
import model.VO.MessageVO;
import service.*;
import utility.Validator;
import view.MainView;

import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.BitSet;
import java.util.List;
import java.util.Map;

public class MainController {

    private String rootDirectory;
    private String curDirectory;

    private Validator validator;

    private MainView mainView;
    
    private CdService cdService;
    private DirService dirService;
    private MvService mvService;
    private CpService cpService;

    public MainController(){
        initializeCMD();

        this.mainView = new MainView();
        this.cdService = new CdService(validator);
        this.dirService = new DirService(validator);
        this.mvService = new MvService(validator);
        this.cpService = new CpService(validator);
    }

    // service들에 쓸 validator와 curDirectory 시작 시 초기화
    private void initializeCMD(){
        this.curDirectory = System.getProperty("user.home");
        Path curPath = Paths.get(curDirectory);
        this.rootDirectory = curPath.getRoot().toString();
        System.out.println(rootDirectory);

        this.validator = new Validator(rootDirectory);
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
                case "&":
                case "&&":
                    mainView.printMessageVO(new MessageVO(String.format("%s은(는) 예상되지 않았습니다.\n", command)));
                default:
                    mainView.printMessageVO(new MessageVO(String.format("%s은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는 배치 파일이 아닙니다.\n", command)));
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

    // parameter 로 여러개가 들어오면 여러개의 DirVO를 DirDAO에서 가져와 view로 보냄
    private void execDIR(List<String> parameters) throws IOException {

        if (parameters.size()==0) parameters.add(curDirectory);

        int parameterLength = parameters.size();

        BitSet bitset = new BitSet(parameterLength);

        for(int i=0;i<parameterLength;i++) {
            DirVO dirVO = dirService.handleCommand(curDirectory, parameters);

            bitset.set(i,dirVO.checkIfDirectoryExists());

            // 상황에 따른 파일을 찾을 수 없습니다 출력 #1
            if((i>1 && bitset.get(i)==true && bitset.get(i-1)==false)){
                mainView.printMessageVO(new MessageVO("\n파일을 찾을 수 없습니다\n"));
            }

            mainView.printDirVO(dirVO);
            parameters.remove(0);

            // 상황에 따른 파일을 찾을 수 없습니다 출력 #2
            if(bitset.get(i)==false && i==parameterLength-1){
                mainView.printMessageVO(new MessageVO("\n파일을 찾을 수 없습니다\n"));
            }
        }
    }

    private void execCOPY(List<String> parameters) throws IOException {
        MessageVO output = cpService.handleCommand(curDirectory, parameters);
        mainView.printMessageVO(output);
    }

    private void execMOVE(List<String> parameters) throws IOException {
        MessageVO output = mvService.handleCommand(curDirectory, parameters);
        mainView.printMessageVO(output);
    }

    private void execHELP(){ mainView.printHelp(); }

    private void execCLS(){

    }
}