package controller;

import view.CmdView;

import java.io.IOException;
import java.util.List;

public abstract class CommandController {

    protected CmdView cmdView;

    public CommandController(CmdView cmdView){
        this.cmdView = cmdView;
    }

    public abstract String executeCommand(String curDirectory, List<String> parameters) throws IOException;
}
