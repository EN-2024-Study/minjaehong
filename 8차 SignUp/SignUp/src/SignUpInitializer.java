import controller.FrontController;
import controller.PanelChangeController;
import controller.panelcontroller.*;
import view.frame.MainFrame;
import view.panel.CreateAccountPanel;
import view.panel.LoginPanel;
import view.panel.UserHomePanel;

import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;

public class SignUpInitializer {
    private MainFrame mainFrame;

    private LoginPanel loginPanel;
    private CreateAccountPanel createAccountPanel;
    private UserHomePanel userHomePanel;

    private ArrayList<Executable> controllerList;
    private ArrayList<JPanel> panelList;

    private FrontController frontController;

    public SignUpInitializer(){
        createFrame();
        createPanels();
        createControllers();

        bindFrontControllerToPanels();
    }

    public void run(){
        mainFrame.add(createAccountPanel);
        mainFrame.setVisible(true);
    }

    private void createFrame(){
        this.mainFrame = new MainFrame();
    }

    private void createPanels(){
        this.loginPanel = new LoginPanel();
        this.createAccountPanel = new CreateAccountPanel();
        this.userHomePanel = new UserHomePanel();
    }

    private void createControllers(){
        this.controllerList = new ArrayList<>();
        this.controllerList.add(new LoginController(loginPanel));
        this.controllerList.add(new CreateAccountController(createAccountPanel));
        this.controllerList.add(new UserHomeController(userHomePanel));

        this.controllerList.add(new PanelChangeController(mainFrame, loginPanel, createAccountPanel, userHomePanel));

        this.frontController = new FrontController(controllerList);
    }

    private void bindFrontControllerToPanels() {
        loginPanel.createAccountButton.addActionListener(frontController);
        loginPanel.loginButton.addActionListener(frontController);
        loginPanel.findPasswordButton.addActionListener(frontController);

        createAccountPanel.idCheckButton.addActionListener(frontController);
        createAccountPanel.phoneNumCheckButton.addActionListener(frontController);
        createAccountPanel.zipCodeCheckButton.addActionListener(frontController);
        createAccountPanel.submitButton.addActionListener(frontController);
        createAccountPanel.cancelButton.addActionListener(frontController);
    }
}
