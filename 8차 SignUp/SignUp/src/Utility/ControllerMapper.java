package utility;

import controller.eventController.*;

import java.awt.event.ActionEvent;
import java.util.List;

public class ControllerMapper {

    private EventController createAccountController;
    private EventController loginController;
    private EventController userHomeController;
    private EventController editAccountController;

    public ControllerMapper(List<EventController> controllerList){
        this.loginController = controllerList.get(0);
        this.createAccountController = controllerList.get(1);
        this.userHomeController = controllerList.get(2);
        this.editAccountController = controllerList.get(3);
    }

    public EventController getMappedController(ActionEvent e){
        String actionCommand = e.getActionCommand();

        if(actionCommand.startsWith("loginPanel")) {
            return loginController;
        }

        if(actionCommand.startsWith("createAccountPanel")) {
            return createAccountController;
        }

        if(actionCommand.startsWith("userHomePanel")) {
            return userHomeController;
        }

        if(actionCommand.startsWith("editAccountPanel")) {
            return editAccountController;
        }

        return null;
    }
}
