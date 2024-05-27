package controller;

import model.DTO.MessageDTO;
import service.CpService;
import utility.RuntimeController;
import utility.Validator;
import view.CmdView;

import java.io.IOException;
import java.util.List;

public class HelpController extends CommandController{

    public HelpController(CmdView cmdView){
        super(cmdView);
    }

    @Override
    public String executeCommand(String curDirectory, List<String> parameters) throws IOException {
        cmdView.printHelp();
        return curDirectory;
    }
}
