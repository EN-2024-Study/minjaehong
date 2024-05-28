package controller.eventController;

import view.panel.UserHomePanel;

import java.awt.event.ActionEvent;

public class UserHomeController implements EventController {

    private UserHomePanel userHomePanel;

    public UserHomeController(UserHomePanel userHomePanel){
        this.userHomePanel = userHomePanel;
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {

    }
}
