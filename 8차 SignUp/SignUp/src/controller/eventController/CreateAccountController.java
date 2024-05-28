package controller.eventController;

import model.dto.AccountDTO;
import model.dto.TextFieldDTO;
import service.SignUpService;
import view.panel.CreateAccountPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;

public class CreateAccountController extends EventController {

    private CreateAccountPanel createAccountPanel;

    public CreateAccountController(CreateAccountPanel createAccountPanel){
        this.createAccountPanel = createAccountPanel;
        this.signUpService = new SignUpService();
    }

    private void checkID(){
        String inputText = createAccountPanel.idTextField.getText();
        boolean exists = signUpService.checkIfIDExists(new TextFieldDTO(inputText));
        showCheckResultMessage(createAccountPanel, createAccountPanel.idTextField, exists);
    }

    private void checkPhoneNum(){
        String inputText = createAccountPanel.phoneNumTextField.getText();
        boolean exists = signUpService.checkIfIDExists(new TextFieldDTO(inputText));
        showCheckResultMessage(createAccountPanel, createAccountPanel.phoneNumTextField, exists);
    }

    private void submit(){
        String userID = createAccountPanel.idTextField.getText();
        String userPW = createAccountPanel.pwTextField.getText();
        String userName = createAccountPanel.nameTextField.getText();
        String userPhoneNum = createAccountPanel.phoneNumTextField.getText();
        String userBirth = createAccountPanel.birthdayTextField.getText();
        String userEmail = createAccountPanel.emailTextField.getText();
        String userZipcode = createAccountPanel.zipCodeTextField.getText();
        String userAddress = createAccountPanel.addressTextField.getText();

        signUpService.addAccount(new AccountDTO(userID, userPW, userName, userPhoneNum, userBirth, userEmail, userZipcode, userAddress));

        JOptionPane.showMessageDialog(createAccountPanel, "createAccount success!");
        clearPanel();
    }

    private void cancel(){
        clearPanel();
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
                checkID();
                break;

            case "createAccountPanel_checkPhoneNum":
                checkPhoneNum();
                break;

            case "createAccountPanel_checkZipCode":
                break;

            case "createAccountPanel_submit":
                submit();
                break;

            case "createAccountPanel_cancel":
                cancel();
                break;
        }
    }
}
