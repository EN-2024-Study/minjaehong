package controller.panelcontroller;

import view.panel.LoginPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;

public class LoginController implements Executable{

    private LoginPanel loginPanel;

    public LoginController(LoginPanel loginPanel){
        this.loginPanel = loginPanel;

    }

    @Override
    public void execute(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "loginPanel_login":
                System.out.println("loginPanel_login actionCommand");
                break;

            case "loginPanel_findPassword":
                System.out.println("loginPanel_findPassword actionCommand");
                break;

            case "loginPanel_createAccount":
                System.out.println("loginPanel_createAccount actionCommand");
                break;
        }
    }
}
