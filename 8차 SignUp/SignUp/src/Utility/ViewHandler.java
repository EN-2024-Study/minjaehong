package utility;

import controller.eventController.EventController;
import view.frame.MainFrame;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.util.List;

// ActionCommand 를 확인하고 Panel 갈아끼워줌
public class ViewHandler extends EventController {

    private MainFrame mainFrame;

    private JPanel loginPanel;
    private JPanel createAccountPanel;
    private JPanel userHomePanel;
    private JPanel editAccountPanel;

    public ViewHandler(MainFrame mainFrame, List<JPanel> panelList){
        this.mainFrame = mainFrame;

        this.loginPanel = panelList.get(0);
        this.createAccountPanel = panelList.get(1);
        this.userHomePanel = panelList.get(2);
        this.editAccountPanel = panelList.get(3);
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "loginPanel_login":
                mainFrame.remove(loginPanel);
                mainFrame.add(userHomePanel);
                break;

            case "loginPanel_createAccount":
                mainFrame.remove(loginPanel);
                mainFrame.add(createAccountPanel);
                break;

            case "createAccountPanel_submit":
            case "createAccountPanel_cancel":
                mainFrame.remove(createAccountPanel);
                mainFrame.add(loginPanel);
                break;

            case "userHomePanel_edit":
                mainFrame.remove(userHomePanel);
                mainFrame.add(editAccountPanel);
                break;

            case "editAccountPanel_submit":
            case "editAccountPanel_cancel":
                mainFrame.remove(editAccountPanel);
                mainFrame.add(userHomePanel);
                break;

            case "userHomePanel_logOut":
            case "userHomePanel_delete":
                mainFrame.remove(userHomePanel);
                mainFrame.add(loginPanel);
                break;
        }
        mainFrame.revalidate();
        mainFrame.repaint();
        mainFrame.setVisible(true);
    }
}
