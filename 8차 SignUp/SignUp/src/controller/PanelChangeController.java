package controller;

import controller.panelcontroller.Executable;
import view.frame.MainFrame;
import view.panel.CreateAccountPanel;

import view.panel.LoginPanel;
import view.panel.UserHomePanel;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.util.ArrayList;

public class PanelChangeController implements Executable {

    private MainFrame mainFrame;

    private LoginPanel loginPanel;
    private CreateAccountPanel createAccountPanel;
    private UserHomePanel userHomePanel;

    public PanelChangeController(MainFrame mainFrame, LoginPanel loginPanel, CreateAccountPanel createAccountPanel,
                                 UserHomePanel userHomePanel){
        this.mainFrame = mainFrame;

        this.loginPanel = loginPanel;
        this.createAccountPanel = createAccountPanel;
        this.userHomePanel = userHomePanel;
    }

    @Override
    public void execute(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "createAccountPanel_cancel":
                mainFrame.remove(createAccountPanel);
                mainFrame.add(loginPanel);
                mainFrame.setVisible(true);
                break;
            case "loginPanel_createAccount":
                mainFrame.remove(loginPanel);
                mainFrame.add(createAccountPanel);
                mainFrame.setVisible(true);
                break;
            case "3":
                break;
            case "4":
                break;
        }
    }
}
