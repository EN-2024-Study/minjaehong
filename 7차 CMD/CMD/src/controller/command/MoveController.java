package controller.command;

import model.DTO.MessageDTO;
import service.MvService;
import utility.RuntimeController;
import utility.Validator;
import view.CmdView;

import java.io.IOException;
import java.util.List;

public class MoveController extends CommandController{

    private MvService mvService;

    public MoveController(CmdView cmdView, Validator validator, RuntimeController runtimeController){
        super(cmdView);
        this.mvService = new MvService(validator, runtimeController);
    }

    @Override
    public String executeCommand(String curDirectory, List<String> parameters) throws IOException {
        MessageDTO messageDTO = mvService.handleCommand(curDirectory, parameters);
        cmdView.printMessageDTO(messageDTO);
        return curDirectory;
    }
}