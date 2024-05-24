package view;

import model.VO.*;

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
        System.out.print("\n" + curDirectory + "> ");
    }

    public InputVO getInput(String curDirectory) throws IOException {
        printCurDirectory(curDirectory);
        String input = "";

        input = br.readLine();

        return new InputVO(input);
    }

    public OverwritePermissionVO getOverwritePermission() throws IOException{
        String input = br.readLine();
        return new OverwritePermissionVO(input);
    }

    public void printMessageVO(MessageVO messageVO) {
        System.out.print(messageVO.getMessage());
    }

    //============================= dir 관련 변수들 =============================//

    // 한 개의 폴더(DirVO)에 대한 정보 출력
    public void printDirVO(DirVO dirVO) {
        sb.setLength(0);

        // 만약 존재하지 않으면 return
        if (dirVO.checkIfDirectoryExists() == false) {
            sb.append("\n");
            sb.append(dirVO.getCurDirectory());
            sb.append(" 디렉터리\n");
            System.out.print(sb);
            return;
        }

        sb.append("\n");
        sb.append(dirVO.getSourcePathString());
        sb.append("디렉터리\n");

        List<FileVO> fileVOQueue = dirVO.getFileInfoList();

        for (int i = 0; i < fileVOQueue.size(); i++) {
            FileVO fileVO = fileVOQueue.get(i);

            // DATE
            sb.append(dateFormat.format(fileVO.getLastModifiedDate()));
            sb.append(" ");

            // DIR OR NOT
            if (fileVO.isDirectory()) sb.append("<DIR> ");
            else sb.append("      ");

            // SIZE
            String fileSize = String.format("%,d", fileVO.getFileSize());
            if(fileSize.equals("0") || fileVO.isDirectory() || fileVO.getFileName().equals(".") || fileVO.getFileName().equals("..")) fileSize = "";
            fileSize = String.format("%" + 10 + "s", fileSize);

            sb.append(fileSize);
            sb.append(" ");

            sb.append(fileVO.getFileName());
            sb.append("\n");
        }

        sb.append(String.format("%15s", String.format("%,d", dirVO.getFileCnt())));
        sb.append(" 파일    ");
        sb.append(String.format("%15s", String.format("%,d", dirVO.getTotalFileSize())));
        sb.append(" 바이트\n");
        sb.append(String.format("%15s", String.format("%,d", dirVO.getDirCnt())));
        sb.append(" 디렉터리");
        sb.append(String.format("%15s", String.format("%,d", dirVO.getTotalFileSize())));
        sb.append(" 바이트 남음\n");

        System.out.print(sb);
    }

    public void printHelp() {
        String helpText = "특정 명령어에 대한 자세한 내용이 필요하면 HELP 명령어 이름을 입력하십시오.\n"
                + "CD       현재 디렉터리 이름을 보여주거나 바꿉니다.\n"
                + "CLS      화면을 지웁니다.\n"
                + "COPY     하나 이상의 파일을 다른 위치로 복사합니다.\n"
                + "DIR      디렉터리에 있는 파일과 하위 디렉터리 목록을 보여줍니다.\n"
                + "MOVE     하나 이상의 파일을 한 디렉터리에서 다른 디렉터리로 이동합니다.\n"
                + "HELP     Windows 명령에 대한 도움말 정보를 제공합니다.\n"
                + "EXIT     CMD.EXE 프로그램(명령 인터프리터)을 종료합니다.\n"
                + "도구에 대한 자세한 내용은 온라인 도움말의 명령줄 참조를 참조하십시오.";

        System.out.print(helpText);
    }
}