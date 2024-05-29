package utility;

import controller.eventController.EventController;
import view.frame.MainFrame;
import view.panel.CreateAccountPanel;
import view.panel.EditAccountPanel;
import view.panel.LoginPanel;
import view.panel.UserHomePanel;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.util.List;

// ActionCommand 를 확인하고 Panel 초기화하고 갈아끼워줌
// Login 시
public class ViewHandler extends EventController {

    private MainFrame mainFrame;

    private LoginPanel loginPanel;
    private CreateAccountPanel createAccountPanel;
    private UserHomePanel userHomePanel;
    private EditAccountPanel editAccountPanel;

    public ViewHandler(MainFrame mainFrame, List<JPanel> panelList){
        this.mainFrame = mainFrame;

        this.loginPanel = (LoginPanel)panelList.get(0);
        this.createAccountPanel = (CreateAccountPanel)panelList.get(1);
        this.userHomePanel = (UserHomePanel)panelList.get(2);
        this.editAccountPanel = (EditAccountPanel)panelList.get(3);
    }

    public void setCookie(String userId){
        userHomePanel.setCookie(userId);
    }

    public void deleteCookie() { userHomePanel.deleteCookie(); }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "loginPanel_login":
                clearLoginPanel();
                mainFrame.remove(loginPanel);
                mainFrame.add(userHomePanel);
                break;

            case "loginPanel_createAccount":
                clearLoginPanel();
                mainFrame.remove(loginPanel);
                mainFrame.add(createAccountPanel);
                break;

            case "createAccountPanel_submit":
            case "createAccountPanel_cancel":
                clearCreateAccountPanel();
                mainFrame.remove(createAccountPanel);
                mainFrame.add(loginPanel);
                break;

            case "userHomePanel_edit":
                mainFrame.remove(userHomePanel);
                mainFrame.add(editAccountPanel);
                break;

            case "editAccountPanel_submit":
            case "editAccountPanel_cancel":
                clearEditAccountPanel();
                mainFrame.remove(editAccountPanel);
                mainFrame.add(userHomePanel);
                break;

            case "userHomePanel_logOut":
            case "userHomePanel_delete":
                deleteCookie();
                mainFrame.remove(userHomePanel);
                mainFrame.add(loginPanel);
                break;
        }

        mainFrame.revalidate();
        mainFrame.repaint();
        mainFrame.setVisible(true);
    }

    private void clearCreateAccountPanel(){
        createAccountPanel.idTextField.setText("");
        createAccountPanel.idTextField.setEnabled(true);
        createAccountPanel.phoneNumTextField.setText("");
        createAccountPanel.phoneNumTextField.setEnabled(true);
        createAccountPanel.zipCodeTextField.setText("");
        createAccountPanel.zipCodeTextField.setEnabled(true);
    }

    private void clearEditAccountPanel(){
        editAccountPanel.pwTextField.setText("");
        editAccountPanel.emailTextField.setText("");
        editAccountPanel.addressTextField.setText("");
        editAccountPanel.zipCodeTextField.setText("");
    }

    private void clearLoginPanel(){
        loginPanel.idTextField.setText("");
        loginPanel.pwTextField.setText("");
    }
}
