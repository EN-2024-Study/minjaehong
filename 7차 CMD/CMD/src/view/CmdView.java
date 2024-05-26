package view;

import constants.Constants;
import model.VO.*;

import java.io.*;
import java.text.SimpleDateFormat;
import java.util.*;

public class CmdView {

    private BufferedReader consoleReader;
    private BufferedWriter consoleWriter;
    private StringBuilder sb;

    private SimpleDateFormat dateFormat;
    private String dirCmdIntroString;

    public CmdView() {
        consoleReader = new BufferedReader(new InputStreamReader(System.in));
        consoleWriter = new BufferedWriter(new OutputStreamWriter(System.out));
        sb = new StringBuilder();

        dateFormat = new SimpleDateFormat(Constants.DIR_DATE_FORMAT, Locale.KOREAN);
    }

    public void setDirCmdIntroString(String dirCmdIntroString){
        this.dirCmdIntroString = dirCmdIntroString;
    }

    private void printCurDirectory(String curDirectory) throws IOException {
        consoleWriter.write("\n");
        consoleWriter.write(curDirectory+"> ");
        consoleWriter.flush();
    }

    public InputVO getInput(String curDirectory) throws IOException {
        printCurDirectory(curDirectory);
        String input = "";

        input = consoleReader.readLine();

        return new InputVO(input);
    }

    public OverwritePermissionVO getOverwritePermission() throws IOException{
        String input = consoleReader.readLine();
        return new OverwritePermissionVO(input);
    }

    public void printMessageVO(MessageVO messageVO) throws IOException {
        consoleWriter.write(messageVO.getMessage());
        consoleWriter.flush();
    }

    public void printDriveInfo() throws IOException {
        consoleWriter.write(dirCmdIntroString);
        consoleWriter.flush();
    }

    // 한 개의 폴더(DirVO)에 대한 정보 출력
    public void printDirVO(DirVO dirVO) throws IOException {
        sb.setLength(0);

        // 만약 존재하지 않으면 return
        if (dirVO.checkIfDirectoryExists() == false) {
            sb.append("\n");
            sb.append(dirVO.getCurDirectory());
            sb.append(Constants.DIR_DIRECTORY);
            System.out.print(sb);
            return;
        }

        sb.append("\n");
        sb.append(dirVO.getSourcePathString());
        sb.append(Constants.DIR_DIRECTORY);

        List<FileVO> fileVOQueue = dirVO.getFileInfoList();

        for (int i = 0; i < fileVOQueue.size(); i++) {
            FileVO fileVO = fileVOQueue.get(i);

            // DATE
            sb.append(dateFormat.format(fileVO.getLastModifiedDate()));
            sb.append(" ");

            // DIR OR NOT
            if (fileVO.isDirectory()) sb.append(Constants.DIR_SYMBOL);
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
        sb.append(Constants.DIR_FILE);
        sb.append(String.format("%15s", String.format("%,d", dirVO.getTotalFileSize())));
        sb.append(Constants.DIR_BYTE);
        sb.append(String.format("%15s", String.format("%,d", dirVO.getDirCnt())));
        sb.append(" 디렉터리");
        sb.append(String.format("%15s", String.format("%,d", dirVO.getFreeSpaceSize())));
        sb.append(Constants.DIR_BYTE_LEFT);

        consoleWriter.write(sb.toString());
        consoleWriter.flush();
    }

    public void printHelp() throws IOException {
        consoleWriter.write(Constants.HELP_TEXT);
        consoleWriter.flush();
    }

    public void printClear() throws IOException {
        consoleWriter.write(Constants.CLS_TEXT);
        consoleWriter.flush();
    }

    public void returnResources() throws IOException {
        consoleReader.close();

        consoleWriter.flush();
        consoleWriter.close();
    }
}