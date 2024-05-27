package controller;

import model.DTO.MessageDTO;
import service.CpService;
import utility.RuntimeController;
import utility.Validator;
import view.CmdView;

import java.io.IOException;
import java.util.List;

public class CopyController extends CommandController{
    private CpService cpService;

    public CopyController(CmdView cmdView, Validator validator, RuntimeController runtimeController){
        super(cmdView);
        this.cpService = new CpService(validator, runtimeController);
    }

    @Override
    public String executeCommand(String curDirectory, List<String> parameters) throws IOException {
        MessageDTO output = cpService.handleCommand(curDirectory, parameters);
        cmdView.printMessageDTO(output);
        return curDirectory;
    }
}