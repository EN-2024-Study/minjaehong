package utility;

import constant.Constants;
import controller.*;
import handler.ControllerMapper;
import handler.InputHandler;
import model.DTO.MessageDTO;
import view.CmdView;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;

public class CmdInitializer {

    // VIEW 초기화
    // dir 시 default로 출력되는 구문 초기화
    // 맨 처음에만 os랑 버전 출력하기
    public CmdView createView() throws IOException {
        CmdView cmdView = new CmdView();
        cmdView.setDirCmdIntroString(getDirCmdIntroString());
        cmdView.printMessageDTO(new MessageDTO(getOSandVersionInfo()));
        return cmdView;
    }

    public ArrayList<CommandController> createControllers(CmdView cmdView, Validator validator, RuntimeController runtimeController){
        ArrayList<CommandController> commandControllerList = new ArrayList<>();

        commandControllerList.add(new CdController(cmdView, validator));
        commandControllerList.add(new DirController(cmdView, validator));
        commandControllerList.add(new CopyController(cmdView, validator, runtimeController));
        commandControllerList.add(new MoveController(cmdView, validator, runtimeController));
        commandControllerList.add(new HelpController(cmdView));
        commandControllerList.add(new ClearController(cmdView));
        commandControllerList.add(new ExitController(cmdView));

        return commandControllerList;
    }

    public String getInfoBySystemCmd(String cmdFileName, String command) throws IOException {
        StringBuilder sb = new StringBuilder();
        ProcessBuilder builder = new ProcessBuilder();
        builder.command(cmdFileName, Constants.EXECUTE, command);

        Process process = builder.start();

        BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream(),"MS949"));
        String line;
        while((line = reader.readLine())!=null){
            sb.append(line);
            sb.append("\n");
        }
        reader.close();

        return sb.toString();
    }

    // 처음에만 OS랑 버전 출력하기
    public String getOSandVersionInfo() throws IOException {
        StringBuilder sb = new StringBuilder();
        sb.append(getInfoBySystemCmd(Constants.CMD_EXE, Constants.VER));
        sb.append(Constants.MICROSOFT_LICENSE);
        return sb.toString();
    }

    // dir 시 default로 출력되는 구문 구하기
    public String getDirCmdIntroString() throws IOException {
        return getInfoBySystemCmd(Constants.CMD_EXE, Constants.VOL);
    }

    public Validator createValidator(){
        return new Validator();
    }

    public RuntimeController createRuntimeController(CmdView cmdView){
        return new RuntimeController(cmdView);
    }

    public InputHandler createInputHandler(){
        return new InputHandler();
    }

    public ControllerMapper createControllerMapper(ArrayList<CommandController> controllerList){
        return new ControllerMapper(controllerList);
    }
}
