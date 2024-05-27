package controller;

import constant.Constants;
import controller.command.CommandController;
import handler.ControllerMapper;
import handler.InputHandler;
import model.DTO.InputDTO;
import model.DTO.MessageDTO;
import utility.CmdFactory;
import utility.RuntimeController;
import utility.Validator;
import view.CmdView;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class FrontController {

    // FACTORY
    private CmdFactory cmdFactory;

    // VIEW
    private CmdView cmdView;

    // SETTINGS
    private String curDirectory;

    // UTILITIES
    private Validator validator;
    private RuntimeController runtimeController;

    // HANDLERS
    private InputHandler inputHandler;
    private ControllerMapper controllerMapper;

    // CONTROLLERS
    private ArrayList<CommandController> controllerList;

    public FrontController() throws IOException {

        this.curDirectory = System.getProperty(Constants.USER_HOME);

        this.cmdFactory = new CmdFactory();

        this.cmdView = cmdFactory.createView();

        this.validator = cmdFactory.createValidator();
        this.runtimeController = cmdFactory.createRuntimeController(cmdView);

        this.controllerList = cmdFactory.createControllers(cmdView, validator, runtimeController);

        this.inputHandler = cmdFactory.createInputHandler();
        this.controllerMapper = cmdFactory.createControllerMapper(controllerList);
    }

    public void run() throws IOException{
        boolean isCmdRunning = true;
        String command;
        List<String> parameters;

        while(isCmdRunning){

            String input = cmdView.getInput(curDirectory);
            InputDTO inputDTO = inputHandler.handleInput(input);

            command = inputDTO.getCommand();
            if(command.isBlank()) continue;
            parameters = inputDTO.getParameters();
            if(validator.checkIfValidParameters(parameters)==false) {
                cmdView.printMessageDTO(new MessageDTO(Constants.WRONG_LABEL));
                continue;
            }

            CommandController curController = controllerMapper.getMappedController(command);

            if(curController==null){
                cmdView.printMessageDTO(new MessageDTO(String.format(Constants.WRONG_CMD, command)));
            }else{
                curDirectory = curController.executeCommand(curDirectory, parameters);
            }
        }
    }
}