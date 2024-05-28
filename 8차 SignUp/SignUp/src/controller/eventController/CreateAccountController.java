package controller.eventController;

import view.panel.CreateAccountPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;

public class CreateAccountController implements EventController {

    private CreateAccountPanel createAccountPanel;

    public CreateAccountController(CreateAccountPanel createAccountPanel){
        this.createAccountPanel = createAccountPanel;
    }

    private void clearPanel(){
        createAccountPanel.idTextField.setText("");
        createAccountPanel.idTextField.setEnabled(true);
        createAccountPanel.phoneNumTextField.setText("");
        createAccountPanel.phoneNumTextField.setEnabled(true);
        createAccountPanel.zipCodeTextField.setText("");
        createAccountPanel.zipCodeTextField.setEnabled(true);
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();
        String inputText;

        switch(actionCommand){
            case "createAccountPanel_checkId":
                inputText = createAccountPanel.idTextField.getText();
                JOptionPane.showMessageDialog(createAccountPanel, inputText + " available!");
                createAccountPanel.idTextField.setEnabled(false);
                break;

            case "createAccountPanel_checkPhoneNum":
                inputText = createAccountPanel.phoneNumTextField.getText();
                JOptionPane.showMessageDialog(createAccountPanel, inputText + " available!");
                createAccountPanel.phoneNumTextField.setEnabled(false);
                break;// panel 에서 값 받아오기

            case "createAccountPanel_checkZipCode":
                inputText = createAccountPanel.zipCodeTextField.getText();
                JOptionPane.showMessageDialog(createAccountPanel, inputText + " available!");
                createAccountPanel.zipCodeTextField.setEnabled(false);
                break;// panel 에서 값 받아오기

            case "createAccountPanel_submit":
                // service 통해서 실제로 account 하나 생성해주기
                JOptionPane.showMessageDialog(createAccountPanel, "createAccount success!");
                clearPanel();
                break;

            case "createAccountPanel_cancel":
                clearPanel();
                break;
        }
    }
}
