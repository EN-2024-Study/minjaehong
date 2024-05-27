package controller.command;

import view.CmdView;

import java.io.IOException;
import java.util.List;

public class ClearController extends CommandController{

    public ClearController(CmdView cmdView){
        super(cmdView);
    }

    @Override
    public String executeCommand(String curDirectory, List<String> parameters) throws IOException {
        cmdView.printClear();
        return curDirectory;
    }
}