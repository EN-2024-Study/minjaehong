package view;

import model.InputVO;
import model.OutputVO;

import java.io.*;
import java.util.*;

public class MainView {

    BufferedReader br;

    public MainView(){
        br = new BufferedReader(new InputStreamReader(System.in));
    }

    private void printCurDirectory(String curDirectory){
        System.out.print(curDirectory + "> ");
    }

    public InputVO getInput(String curDirectory) {
        printCurDirectory(curDirectory);
        String input="";
        try {
            input = br.readLine();
        }catch(IOException e){

        }
        return new InputVO(input);
    }

    public void printReturnedResult(OutputVO result){
        System.out.println(result.getOutput());
        System.out.println();
    }

    public void clearPrompt(){
        try {
            Runtime.getRuntime().exec("cls");
        }catch(IOException e){

        }
    }

    public void showHelp(){
        String helpText = "특정 명령어에 대한 자세한 내용이 필요하면 HELP 명령어 이름을 입력하십시오.\n"
                + "CD       현재 디렉터리 이름을 보여주거나 바꿉니다.\n"
                + "CLS      화면을 지웁니다.\n"
                + "COPY     하나 이상의 파일을 다른 위치로 복사합니다.\n"
                + "DIR      디렉터리에 있는 파일과 하위 디렉터리 목록을 보여줍니다.\n"
                + "MOVE     하나 이상의 파일을 한 디렉터리에서 다른 디렉터리로 이동합니다.\n"
                + "EXIT     CMD.EXE 프로그램(명령 인터프리터)을 종료합니다.\n"
                + "HELP     Windows 명령에 대한 도움말 정보를 제공합니다.\n"
                + "도구에 대한 자세한 내용은 온라인 도움말의 명령줄 참조를 참조하십시오.";

        System.out.println(helpText);
    }
}