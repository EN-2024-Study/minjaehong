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

    private boolean checkIfValidInputDTO(InputDTO inputDTO) throws IOException {
        String command = inputDTO.getCommand();
        List<String> parameters = inputDTO.getParameters();

        if(command.isBlank() || command.isEmpty()) return false;

        if(!validator.checkIfValidParameters(parameters)) {
            cmdView.printMessageDTO(new MessageDTO(Constants.WRONG_LABEL));
            return false;
        }

        return true;
    }

    public void run() throws IOException{
        boolean isCmdRunning = true;

        String command;
        List<String> parameters;

        while(isCmdRunning){

            String input = cmdView.getInput(curDirectory);
            InputDTO inputDTO = inputHandler.handleInput(input);

            if(!checkIfValidInputDTO(inputDTO)) continue;

            command = inputDTO.getCommand();
            parameters = inputDTO.getParameters();

            CommandController curController = controllerMapper.getMappedController(command);

            if(command.equals("exit")) isCmdRunning = false;

            if(curController==null){
                cmdView.printMessageDTO(new MessageDTO(String.format(Constants.WRONG_CMD, command)));
            }else{
                View view = curController.executeCommand(curDirectory, parameters);
                view.render();
            }
        }
    }
}