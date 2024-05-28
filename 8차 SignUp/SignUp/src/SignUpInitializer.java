import utility.ControllerMapper;
import utility.ViewHandler;
import controller.*;
import controller.eventController.*;
import view.frame.MainFrame;
import view.panel.*;

import javax.swing.*;
import java.util.ArrayList;

public class SignUpInitializer {
    private MainFrame mainFrame;

    private LoginPanel loginPanel;
    private CreateAccountPanel createAccountPanel;
    private UserHomePanel userHomePanel;
    private EditAccountPanel editAccountPanel;

    private ArrayList<EventController> controllerList;
    private ArrayList<JPanel> panelList;

    private FrontController frontController;

    public SignUpInitializer(){
        createFrame();
        createPanels();
        createControllers();

        bindFrontControllerToPanels();
    }

    public void run(){
        mainFrame.add(loginPanel);
        mainFrame.setVisible(true);
    }

    private void createFrame(){
        this.mainFrame = new MainFrame();
    }

    private void createPanels(){
        this.loginPanel = new LoginPanel();
        this.createAccountPanel = new CreateAccountPanel();
        this.userHomePanel = new UserHomePanel();
        this.editAccountPanel = new EditAccountPanel();

        panelList = new ArrayList<>();
        panelList.add(loginPanel);
        panelList.add(createAccountPanel);
        panelList.add(userHomePanel);
        panelList.add(editAccountPanel);
    }

    private void createControllers(){
        this.controllerList = new ArrayList<>();
        this.controllerList.add(new LoginController(loginPanel));
        this.controllerList.add(new CreateAccountController(createAccountPanel));
        this.controllerList.add(new UserHomeController(userHomePanel));
        this.controllerList.add(new EditAccountController(editAccountPanel));

        this.frontController = new FrontController(new ControllerMapper(controllerList), new ViewHandler(mainFrame, panelList));
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

        userHomePanel.deleteButton.addActionListener(frontController);
        userHomePanel.editButton.addActionListener(frontController);
        userHomePanel.logOutButton.addActionListener(frontController);

        editAccountPanel.idCheckButton.addActionListener(frontController);
        editAccountPanel.phoneNumCheckButton.addActionListener(frontController);
        editAccountPanel.zipCodeCheckButton.addActionListener(frontController);
        editAccountPanel.submitButton.addActionListener(frontController);
        editAccountPanel.cancelButton.addActionListener(frontController);
    }
}
