package controller;

import constant.Constants;
import handler.ControllerMapper;
import handler.InputHandler;
import model.DTO.InputDTO;
import model.DTO.MessageDTO;
import utility.CmdInitializer;
import utility.RuntimeController;
import utility.Validator;
import view.CmdView;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class FrontController {

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

    private CmdInitializer cmdInitializer;

    public FrontController() throws IOException {
        this.cmdInitializer = new CmdInitializer();

        this.cmdView = cmdInitializer.createView();

        this.validator = cmdInitializer.createValidator();
        this.runtimeController = cmdInitializer.createRuntimeController(cmdView);

        this.controllerList = cmdInitializer.createControllers(cmdView, validator, runtimeController);

        this.inputHandler = cmdInitializer.createInputHandler();
        this.controllerMapper = cmdInitializer.createControllerMapper(controllerList);

        initializeSettings();
    }

    private void initializeSettings() throws IOException {
        this.curDirectory = System.getProperty(Constants.USER_HOME);
    }

    public void run() throws IOException{
        boolean isCmdRunning = true;
        String command;
        List<String> parameters;

        while(isCmdRunning){
            String input = cmdView.getInput(curDirectory);

            InputDTO inputDTO = inputHandler.handleInput(input);

            command = inputDTO.getCommand();
            parameters = inputDTO.getParameters();

            if(validator.checkIfValidParameters(parameters)==false) {
                cmdView.printMessageDTO(new MessageDTO(Constants.WRONG_LABEL));
                continue;
            }
            if(command.isBlank()){ continue; }

            CommandController curController = controllerMapper.getMappedController(command);

            if(curController==null){
                cmdView.printMessageDTO(new MessageDTO(String.format(Constants.WRONG_CMD, command)));
            }else{
                curDirectory = curController.executeCommand(curDirectory, parameters);
            }
        }
    }
}