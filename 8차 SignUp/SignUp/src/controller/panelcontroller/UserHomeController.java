package controller.panelcontroller;

import view.panel.UserHomePanel;

import java.awt.event.ActionEvent;

public class UserHomeController implements Executable{

    private UserHomePanel userHomePanel;


    public UserHomeController(UserHomePanel userHomePanel){
        this.userHomePanel = userHomePanel;
    }

    @Override
    public void execute(ActionEvent e) {

    }
}
