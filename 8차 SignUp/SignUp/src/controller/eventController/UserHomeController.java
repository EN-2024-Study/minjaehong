package controller.eventController;

import utility.ViewHandler;
import view.panel.UserHomePanel;

import java.awt.event.ActionEvent;

public class UserHomeController extends EventController {

    private UserHomePanel userHomePanel;

    public UserHomeController(UserHomePanel userHomePanel, ViewHandler viewHandler){
        this.userHomePanel = userHomePanel;
        this.viewHandler = viewHandler;
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "userHomePanel_edit":
                viewHandler.handleButtonEvent(e);
            case "userHomePanel_logOut":
                viewHandler.handleButtonEvent(e);
            case "userHomePanel_delete":
                viewHandler.handleButtonEvent(e);
        }
    }
}
