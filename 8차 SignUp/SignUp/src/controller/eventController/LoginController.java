package controller.eventController;

import view.panel.LoginPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;

public class LoginController extends EventController {

    private LoginPanel loginPanel;

    public LoginController(LoginPanel loginPanel){
        this.loginPanel = loginPanel;
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "loginPanel_login":
                System.out.println("login");
                break;

            case "loginPanel_findPassword":
                String userInput = JOptionPane.showInputDialog(loginPanel, "Enter your ID:");

                if (userInput != null) {
                    JOptionPane.showMessageDialog(loginPanel, "Your PW is this!");
                }
                break;

            case "loginPanel_createAccount":
                System.out.println("createAccount");
                break;
        }
    }
}
