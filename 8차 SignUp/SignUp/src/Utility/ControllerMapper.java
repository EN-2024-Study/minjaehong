package Utility;

import controller.panelcontroller.*;

import java.awt.event.ActionEvent;
import java.util.List;

public class ControllerMapper {

    private Executable createAccountController;
    private Executable findAccountController;
    private Executable loginController;
    private Executable userHomeController;
    private Executable panelChangeController;

    public ControllerMapper(List<Executable> controllerList){
        this.loginController = controllerList.get(0);
        this.createAccountController = controllerList.get(1);
        this.findAccountController = controllerList.get(2);
        this.userHomeController = controllerList.get(3);
        this.panelChangeController = controllerList.get(4);
    }

    public Executable getMappedController(ActionEvent e){
        String actionCommand = e.getActionCommand();

        if(actionCommand.equals("createAccountPanel_cancel")){
            return panelChangeController;
        }

        if(actionCommand.startsWith("loginPanel")){
            return loginController;
        }

        if(actionCommand.startsWith("createAccountPanel")){
            return createAccountController;
        }

        if(actionCommand.startsWith("findPasswordPanel")){
            return findAccountController;
        }

        if(actionCommand.startsWith("userHomePanel")){
            return userHomeController;
        }

        return null;
    }
}
