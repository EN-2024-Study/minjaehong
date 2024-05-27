package view;

import constant.Constants;
import model.DTO.*;

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

    public String getInput(String curDirectory) throws IOException {
        printCurDirectory(curDirectory);
        String input = "";

        input = consoleReader.readLine();

        return input;
    }

    public String getOverwritePermission() throws IOException{
        String input = consoleReader.readLine();
        return input;
    }

    public void printMessageDTO(MessageDTO messageDTO) throws IOException {
        consoleWriter.write(messageDTO.getMessage());
        consoleWriter.flush();
    }

    public void printDriveInfo() throws IOException {
        consoleWriter.write(dirCmdIntroString);
        consoleWriter.flush();
    }

    // 한 개의 폴더(DirDTO)에 대한 정보 출력
    public void printDirDTO(DirDTO dirDTO) throws IOException {
        sb.setLength(0);

        // 만약 존재하지 않으면 return
        if (dirDTO.checkIfDirectoryExists() == false) {
            sb.append("\n");
            sb.append(dirDTO.getCurDirectory());
            sb.append(Constants.DIR_DIRECTORY);
            System.out.print(sb);
            return;
        }

        sb.append("\n");
        sb.append(dirDTO.getSourcePathString());
        sb.append(Constants.DIR_DIRECTORY);

        List<FileDTO> fileDTOQueue = dirDTO.getFileInfoList();

        for (FileDTO fileDTO : fileDTOQueue) {
            // DATE
            sb.append(dateFormat.format(fileDTO.getLastModifiedDate()));
            sb.append(" ");

            // DIR OR NOT
            if (fileDTO.isDirectory()) sb.append(Constants.DIR_SYMBOL);
            else sb.append("      ");

            // SIZE
            String fileSize = String.format("%,d", fileDTO.getFileSize());
            if (fileSize.equals("0") || fileDTO.isDirectory() || fileDTO.getFileName().equals(".") || fileDTO.getFileName().equals(".."))
                fileSize = "";
            fileSize = String.format("%" + 10 + "s", fileSize);

            sb.append(fileSize);
            sb.append(" ");

            sb.append(fileDTO.getFileName());
            sb.append("\n");
        }

        sb.append(String.format("%15s", String.format("%,d", dirDTO.getFileCnt())));
        sb.append(Constants.DIR_FILE);
        sb.append(String.format("%15s", String.format("%,d", dirDTO.getTotalFileSize())));
        sb.append(Constants.DIR_BYTE);
        sb.append(String.format("%15s", String.format("%,d", dirDTO.getDirCnt())));
        sb.append(" 디렉터리");
        sb.append(String.format("%15s", String.format("%,d", dirDTO.getFreeSpaceSize())));
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