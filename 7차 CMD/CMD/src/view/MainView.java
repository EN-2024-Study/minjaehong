package view;

import model.VO.DirVO;
import model.VO.FileInfo;
import model.VO.InputVO;
import model.VO.MessageVO;

import java.io.*;
import java.text.SimpleDateFormat;
import java.util.*;

public class MainView {

    BufferedReader br;
    StringBuilder sb;

    private SimpleDateFormat dateFormat;

    public MainView() {
        br = new BufferedReader(new InputStreamReader(System.in));
        sb = new StringBuilder();

        dateFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm", Locale.KOREAN);
    }

    private void printCurDirectory(String curDirectory) {
        System.out.print(curDirectory + "> ");
    }

    public InputVO getInput(String curDirectory) {
        printCurDirectory(curDirectory);
        String input = "";
        try {
            input = br.readLine();
        } catch (IOException e) {

        }
        return new InputVO(input);
    }

    public void printReturnedResult(MessageVO result) {
        System.out.println(result.getOutput());
    }

    //============================= dir 관련 변수들 =============================//

    boolean wasLatestOneExisting = true;

    // 한 개의 DirVO 에 대한 정보 출력
    public void printDirResult(DirVO dirVO) {
        sb.setLength(0);

        // 만약 존재하지 않으면 return
        if (dirVO.checkIfDirectoryExists() == false) {
            sb.append(dirVO.getCurDirectory());
            wasLatestOneExisting = false;
            sb.append(" 디렉터리\n\n");
            System.out.println(sb.toString());
            return;
        }

        if(wasLatestOneExisting==false){
            wasLatestOneExisting = true;
            sb.append("파일을 찾을 수 없습니다\n\n");
        }

        sb.append(dirVO.getSourcePathString());
        sb.append(" ");
        sb.append("디렉터리\n\n");

        List<FileInfo> fileInfoQueue = dirVO.getFileInfoList();

        for (int i = 0; i < fileInfoQueue.size(); i++) {
            FileInfo fileInfo = fileInfoQueue.get(i);

            // DATE
            sb.append(dateFormat.format(fileInfo.getLastModifiedDate()));
            sb.append(" ");

            // DIR OR NOT
            if (fileInfo.isDirectory()) sb.append("<DIR> ");
            else sb.append("      ");

            // SIZE
            String fileSize = String.format("%,d", fileInfo.getFileSize());
            if(fileSize.equals("0") || fileInfo.isDirectory() || fileInfo.getFileName().equals(".") || fileInfo.getFileName().equals("..")) fileSize = "";
            fileSize = String.format("%" + 10 + "s", fileSize);
            sb.append(fileSize);
            sb.append(" ");

            sb.append(fileInfo.getFileName());
            sb.append("\n");
        }


        System.out.println(sb.toString());
    }

    public void showHelp() {
        String helpText = "특정 명령어에 대한 자세한 내용이 필요하면 HELP 명령어 이름을 입력하십시오.\n"
                + "CD       현재 디렉터리 이름을 보여주거나 바꿉니다.\n"
                + "CLS      화면을 지웁니다.\n"
                + "COPY     하나 이상의 파일을 다른 위치로 복사합니다.\n"
                + "DIR      디렉터리에 있는 파일과 하위 디렉터리 목록을 보여줍니다.\n"
                + "MOVE     하나 이상의 파일을 한 디렉터리에서 다른 디렉터리로 이동합니다.\n"
                + "HELP     Windows 명령에 대한 도움말 정보를 제공합니다.\n"
                + "EXIT     CMD.EXE 프로그램(명령 인터프리터)을 종료합니다.\n"
                + "도구에 대한 자세한 내용은 온라인 도움말의 명령줄 참조를 참조하십시오.";

        System.out.println(helpText);
    }

    public void showWrongCommand(String command) {
        System.out.println(String.format("%s은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는 배치 파일이 아닙니다.", command));
    }
}