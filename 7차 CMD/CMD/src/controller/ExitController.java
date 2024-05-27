package controller;

import view.CmdView;

import java.io.IOException;
import java.util.List;

public class ExitController extends CommandController{

    public ExitController(CmdView cmdView){
        super(cmdView);
    }

    @Override
    public String executeCommand(String curDirectory, List<String> parameters) throws IOException {
        cmdView.returnResources();
        return curDirectory;
    }
}